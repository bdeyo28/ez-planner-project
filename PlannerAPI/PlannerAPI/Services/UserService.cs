using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlannerAPI.Exceptions;
using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Requests;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAPI.Services
{
    public class UserService : IUserService
    {
        IOrganizerDao _organizerRepo;

        public UserService(IOrganizerDao organizerDao)
        {
            _organizerRepo = organizerDao;
        }

        public List<Organizer> GetAllOrganizers()
        {
            List<Organizer> toReturn = _organizerRepo.GetAllOrganizers();

            if (toReturn == null)
                throw new EmptyListException("No organizers were found");
            else
                return toReturn;
        }

        public string Login(LoginRequest lr)
        {
            Organizer toView = _organizerRepo.GetOrganizerByName(lr.Username);

            bool valid = ValidatePassword(lr.Password, toView.PasswordHash, toView.PasswordSalt);
            if (!valid)
            {
                throw new InvalidPasswordException("This password is not valid");
            }

            string token = GenerateToken(toView);

            return token;
        }

        public Organizer GetOrganizerById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("This ID input is invalid");
            else
            {
                Organizer toReturn = _organizerRepo.GetOrganizerById(id);
                if (toReturn == null)
                    throw new InvalidOrganizerException("No organizer was found for that id");
                else
                    return toReturn;
            }
        }

        public bool ValidatePassword(string password, byte[] hash, byte[] salt)
        {
            using (var hMac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                byte[] newHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != newHash[i])
                        return false;
                }
                return true;
            }
        }

        public string GenerateToken(Organizer organizer)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(AppSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    organizer.Roles.Select(r => new Claim(ClaimTypes.Role, r.SelectedRole.Name))
                    .Append(new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()))),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public void RegisterUser(RegisterUser ru)
        {
            Organizer toTest = _organizerRepo.GetOrganizerByName(ru.Username);

            if (toTest != null)
            {
                throw new DuplicateUsernameException("Username " + ru.Username + " already exists!");
            }

            Role roleName = _organizerRepo.GetOrganizerRole("organizer");
            OrganizerRole bridgeRow = new OrganizerRole();
            bridgeRow.RoleId = roleName.Id;
            bridgeRow.SelectedRole = roleName;

            Organizer toAdd = new Organizer();
            bridgeRow.EnrolledOrganizer = toAdd;
            toAdd.Roles.Add(bridgeRow);
            toAdd.Name = ru.Username;
            toAdd.Email = ru.Email;

            using (var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                byte[] saltBytes = hMac.Key;

                byte[] hashPass = hMac.ComputeHash(Encoding.UTF8.GetBytes(ru.Password));

                toAdd.PasswordHash = hashPass;
                toAdd.PasswordSalt = saltBytes;
            }

            _organizerRepo.AddOrganizer(toAdd);
        }

    }
}
