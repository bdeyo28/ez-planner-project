using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannerAPI.Models;
using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Requests;
using PlannerAPI.Persistence;
using PlannerAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("/api/User")]
    public class UserController : ControllerBase
    {

        IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult RegisterUser(RegisterUser user)
        {
            _service.RegisterUser(user);

            return Ok(true);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult LoginUser(LoginRequest login)
        {
            string token = _service.Login(login);

            return Ok(new {login.Username, token });
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<Organizer> orgList = _service.GetAllOrganizers();

            return Ok(orgList.Select(o => new UserView(o)));

        }
    }
}
