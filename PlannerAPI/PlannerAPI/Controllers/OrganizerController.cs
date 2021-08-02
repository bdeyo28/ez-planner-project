using PlannerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannerAPI.Exceptions;
using PlannerAPI.Models;
using PlannerAPI.Persistence;
using PlannerAPI.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PlannerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/Organizer")]
    public class OrganizerController : ControllerBase
    {
        IPlannerService _service;
        public OrganizerController(IPlannerService service)
        {
            _service = service;
        }

        [HttpGet("User")]
        public IActionResult GetUserAsOrganizer()
        {
            try
            {
                int userId = int.Parse(this.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);

                return this.Accepted(_service.GetUserAsOrganizer(userId));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganizerById(int id)
        {
            try
            {
                return this.Accepted(_service.GetOrganizerById(id));
            }
            catch (InvalidIdException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("Name/{name}")]
        public IActionResult GetOrganizerByName(string name)
        {
            try
            {
                return this.Accepted(_service.GetOrganizerByName(name));
            }
            catch (InvalidIdException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrganizer(Organizer toAdd)
        {
            try
            {
                _service.AddOrganizer(toAdd);
                return this.Accepted();
            }
            catch(InvalidOrganizerException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult EditOrganizer(Organizer updated)
        {
            try
            {
                _service.EditOrganizer(updated);
                return this.Accepted();
            }
            catch(InvalidOrganizerException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrganizer(int id)
        {
            try
            {
                _service.RemoveOrganizer(id);
                return this.Accepted();
            }
            catch (InvalidOrganizerException e) { return this.BadRequest(e.Message); }
            catch (InvalidIdException e) { return this.BadRequest(e.Message); }
        
        }
    }
}

