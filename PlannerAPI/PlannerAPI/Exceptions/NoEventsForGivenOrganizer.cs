using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class NoEventsForGivenOrganizer : Exception
    {

        public NoEventsForGivenOrganizer(string message) : base(message)
        {

        }

        public NoEventsForGivenOrganizer(string message, Exception e) : base(message, e)
        {

        }
    }
}
