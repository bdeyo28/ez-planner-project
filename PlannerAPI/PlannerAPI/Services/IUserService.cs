using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Requests;
using System.Collections.Generic;

namespace PlannerAPI.Services
{
    public interface IUserService
    {
        string GenerateToken(Organizer organizer);
        List<Organizer> GetAllOrganizers();
        Organizer GetOrganizerById(int id);
        string Login(LoginRequest lr);
        void RegisterUser(RegisterUser ru);
        bool ValidatePassword(string password, byte[] hash, byte[] salt);
    }
}