using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class NoActivitiesForGivenEventException : Exception
    {
        public NoActivitiesForGivenEventException(string message) :base(message)
        {

        }

        public NoActivitiesForGivenEventException(string message, Exception e) : base(message, e)
        {

        }

    }
}
