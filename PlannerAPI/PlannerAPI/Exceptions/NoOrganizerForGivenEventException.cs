using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class NoOrganizerForGivenEventException : Exception 
    {
        public NoOrganizerForGivenEventException(string message) : base(message)
        {

        }

        public NoOrganizerForGivenEventException(string message, Exception e) : base(message, e)
        {

        }
    }
}
