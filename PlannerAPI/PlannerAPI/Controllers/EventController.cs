using PlannerAPI.Exceptions;
using PlannerAPI.Models;
using PlannerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannerAPI.Persistence;
using Microsoft.AspNetCore.Cors;
using PlannerAPI.Models.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PlannerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/Event")]
    public class EventController : ControllerBase
    {
        IPlannerService _service;

        public EventController(IPlannerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            try
            {
                if(this.User.Claims.Any(c => c.Type == ClaimTypes.Role.ToString() && c.Value == "Admin"))
                {
                    return this.Accepted(_service.GetAllEvents());
                }
                else
                {
                    int userId = int.Parse(this.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);

                    return Ok(_service.GetEventsByOrganizerId(userId));
                }
            }
            catch(EmptyListException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            try
            {
                return this.Accepted(_service.GetEventById(id));
            }
            catch(InvalidIdException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("Search")]
        public IActionResult GetEventByName(string name)
        {
            try
            {
                return this.Accepted(_service.GetEventByName(name));
            }
            catch(InvalidNameException e) { return this.BadRequest(e.Message); }
            catch(Exception e) { return this.BadRequest(e.Message); }

        }

        [HttpPost]
        public IActionResult AddEvent(Event toAdd)
        {
            try
            {
                var user = this.User;

                int userId = int.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);

                if (userId != toAdd.OrganizerId)
                    return this.Unauthorized("Can not add events for someone else!");

                _service.AddEvent(toAdd);
                return this.Accepted();
            }
            catch(InvalidEventException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("Activities/{id}")]
        public IActionResult GetEventActivities(int id)
        {
            try
            {
                return this.Accepted(_service.GetEventActivities(id));
            }
            catch(NoActivitiesForGivenEventException e) { return this.BadRequest(e.Message); }
            catch(InvalidIdException e) { return this.BadRequest(e.Message);  }
        }

        [HttpGet("Attendees/{id}")]
        public IActionResult GetEventAttendees(int id)
        {
            try
            {
                return this.Accepted(_service.GetEventAttendees(id));
            }
            catch(NoAttendeesForGivenEventException e) { return this.BadRequest(e.Message); }
            catch(InvalidIdException e) { return this.BadRequest(e.Message); }
        }

        [HttpGet("Organizer/{id}")]
        public IActionResult GetEventOrganizer(int id)
        {
            try
            {
                return this.Accepted(_service.GetEventOrganizer(id));
            }
            catch(NoOrganizerForGivenEventException e) { return this.BadRequest(e.Message); }
            catch(InvalidIdException e) { return this.BadRequest(e.Message); }
        }

        [HttpPut]
        public IActionResult EditEvent(Event updated)
        {
            try
            {
                var user = this.User;

                int userId = int.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);

                if (userId != updated.OrganizerId)
                    return this.Unauthorized("Can not edit events for someone else!");

                _service.EditEvent(updated);
                return this.Accepted();
            }
            catch(InvalidEventException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                _service.RemoveEvent(id);
                return this.Accepted();
            }
            catch (InvalidEventException e)
            {
                return this.BadRequest(e.Message);
            }
            catch (InvalidIdException e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
