using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class NoAttendeesForGivenEventException : Exception
    {
        public NoAttendeesForGivenEventException(string message) : base(message)
        {

        }

        public NoAttendeesForGivenEventException(string message, Exception e) : base(message, e)
        {

        }
    }
}
