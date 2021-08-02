using System;
using System.Collections.Generic;
using PlannerAPI.Models;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Repos;
using PlannerAPI.Exceptions;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;

namespace PlannerAPI.Services
{
    public class PlannerService : IPlannerService
    {
        IEventDao _eventRepo;
        IActivityDao _activityRepo;
        IAttendeeDao _attendeeRepo;
        IOrganizerDao _organizerRepo;

        public PlannerService(IEventDao eventDao, IActivityDao activityDao, IAttendeeDao attendeeDao, IOrganizerDao organizerDao)
        {
            _eventRepo = eventDao;
            _activityRepo = activityDao;
            _attendeeRepo = attendeeDao;
            _organizerRepo = organizerDao;
        }

        // SEND EMAIL TO ATTENDEES

        // BRDIGE GETS FOR EVENTACTIVITIES / EVENTATTENDEES / EVENTORGANIZER

        public List<Event> GetEventsByOrganizerId(int id)
        {
            List<Event> toReturn = _eventRepo.GetEventsByOrganizerId(id);

            if (id <= 0)
                throw new InvalidIdException("Invalid id for this organizer");
            if (toReturn == null)
                throw new NoEventsForGivenOrganizer("No events were found with this organizer.");

            return toReturn;
        }
        public List<Activity> GetEventActivities(int id)
        {
            List<Activity> toReturn = _eventRepo.GetEventActivities(id);

            if (id <= 0)
                throw new InvalidIdException("Invalid id for this event");
            if (toReturn == null)
                throw new NoActivitiesForGivenEventException("No activities were found for this event");

            return toReturn;
        }

        public List<Attendee> GetEventAttendees(int id)
        {
            List<Attendee> toReturn = _eventRepo.GetEventAttendees(id);

            if (id <= 0)
                throw new InvalidIdException("Invalid id for this event");
            if (toReturn == null)
                throw new NoAttendeesForGivenEventException("No attendees were found for this event");

            return toReturn;
        }

        public Organizer GetEventOrganizer(int id)
        {
            Organizer toReturn = _eventRepo.GetEventOrganizer(id);

            if (id <= 0)
                throw new InvalidIdException("Invalid id for this event");
            if (toReturn == null)
                throw new NoOrganizerForGivenEventException("No organizer was found for this event");

            return toReturn;

        }

        // GET ALL FOR OBJECTS 
        public List<Event> GetAllEvents()
        {
            List<Event> toReturn = _eventRepo.GetAllEvents();

            if (toReturn == null)
                throw new EmptyListException("No events were found");
            else
                return toReturn;
        }

        public List<Attendee> GetAllAttendees()
        {
            List<Attendee> toReturn = _attendeeRepo.GetAllAttendees();

            if (toReturn == null)
                throw new EmptyListException("No attendees were found");
            else
                return toReturn;
        }

        public List<Activity> GetAllActivities()
        {
            List<Activity> toReturn = _activityRepo.GetAllActivities();

            if (toReturn == null)
                throw new EmptyListException("No activities were found");
            else
                return toReturn;
        }

        //public Schedule GetSchedule()
        //{
        //    Schedule toReturn = _scheduleDao.GetSchedule();
        //    return toReturn;
        //}

        // ADD OBJECTS

        public void AddEvent(Event toAdd)
        {
            if (toAdd == null || toAdd.OrganizerId <= 0)
                throw new NullObjectException("This event is null or has null properties");
            else
                _eventRepo.AddEvent(toAdd);
        }

        public void AddAttendee(Attendee toAdd)
        {
            if (toAdd == null || toAdd.EventId <= 0)
                throw new NullObjectException("This attendee is null or has null properties");
            else
                _attendeeRepo.AddAttendee(toAdd);
        }

        public void AddOrganizer(Organizer toAdd)
        {
            if (toAdd == null)
                throw new NullObjectException("This organizer is null or has null properties");
            else
                _organizerRepo.AddOrganizer(toAdd);
        }

        public void AddActivity(Activity toAdd)
        {
            if (toAdd == null || toAdd.EventId <= 0)
                throw new NullObjectException("This activity is null or has null properties");
            else
                _activityRepo.AddActivity(toAdd);
        }

        // REMOVE OBJECTS

        public void RemoveActivity(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Can't remove activity with this ID");
            else
            {
                Activity activity = new Activity { Id = id };
                if (activity == null)
                    throw new InvalidActivityException("Activity with this ID does not exist");
                else
                    _activityRepo.RemoveActivity(activity);
            }

        }

        public void RemoveEvent(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Can't remove event with this ID");
            else
            {
                Event thisEvent = new Event { Id = id };
                if (thisEvent == null)
                    throw new InvalidEventException("Event with this ID does not exist");
                else
                    _eventRepo.RemoveEvent(thisEvent);
            }
        }

        public void RemoveOrganizer(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Can't remove organizer with this ID");
            else
            {
                Organizer organizer = new Organizer { Id = id };
                if (organizer == null)
                    throw new InvalidOrganizerException("Organizer with that ID does not exist");
                else
                    _organizerRepo.RemoveOrganizer(organizer);
            }
        }

        public void RemoveAttendee(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Can't remove attendee with this ID");
            else
            {
                Attendee attendee = new Attendee { Id = id };
                if (attendee == null)
                    throw new InvalidAttendeeException("Attendee with that ID does not exist");
                else
                    _attendeeRepo.RemoveAttendee(attendee);
            }
        }

        // EDIT OBJECTS

        public void EditAttendee(Attendee updated)
        {
            if (updated == null || updated.Email == null || updated.Name == null || updated.EventId <= 0)
                throw new NullObjectException("This attendee is null or has null properties");
            else
                _attendeeRepo.EditAttendee(updated);
        }

        public void EditEvent(Event updated)
        {
            if (updated == null || updated.EventName == null)
                throw new NullObjectException("This event is null or has null properties");
            else
                _eventRepo.EditEvent(updated);
        }

        public void EditOrganizer(Organizer updated)
        {
            if (updated == null || updated.Name == null || updated.Email == null)
                throw new NullObjectException("This organizer is null or has null properties");
            else
                _organizerRepo.EditOrganizer(updated);
        }

        public void EditActivity(Activity updated)
        {
            if (updated == null || updated.Name == null || updated.EventId <= 0)
                throw new NullObjectException("This activity is null or has null properties");
            else
                _activityRepo.EditActivity(updated);
        }

        // GET BY OBJECT ID'S

        public Organizer GetUserAsOrganizer(int id)
        {
            Organizer toReturn = _organizerRepo.GetUserAsOrganizer(id);

            if (id <= 0)
                throw new InvalidIdException("Invalid Id");
            if (toReturn == null)
                throw new InvalidIdException("This user organizer Id can not be found");

            return toReturn;

        }

        public Activity GetActivityById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("This ID input is invalid");
            else
            {
                Activity toReturn = _activityRepo.GetActivityById(id);
                if (toReturn == null)
                    throw new InvalidActivityException("No activity was found for that id");
                else
                    return toReturn;
            }
        }

        public Event GetEventById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("This ID input is invalid");
            else
            {
                Event toReturn = _eventRepo.GetEventById(id);
                if (toReturn == null)
                    throw new InvalidEventException("No event was found for that id");
                else
                    return toReturn;
            }
        }

        public Event GetEventByName(string name)
        {
            if (name == null)
                throw new InvalidNameException("This name input was not valid");
            else
            {
                Event toReturn = _eventRepo.GetEventByName(name);
                if (toReturn == null)
                    throw new InvalidEventException("No event was found for that id");
                else
                    return toReturn;
            }
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

        public Organizer GetOrganizerByName(string name)
        {
            if (name == null)
                throw new InvalidNameException("This name input was not valid");
            else
            {
                Organizer toReturn = _organizerRepo.GetOrganizerByName(name);
                if (toReturn == null)
                    throw new InvalidOrganizerException("No organizer was found for that name");
                else 
                    return toReturn;
            }
        }

        public Attendee GetAttendeeById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("This ID input is invalid");
            else
            {
                Attendee toReturn = _attendeeRepo.GetAttendeeById(id);
                if (toReturn == null)
                    throw new InvalidAttendeeException("No attendee was found for that id");
                else
                    return toReturn;
            }
        }

    }
}
