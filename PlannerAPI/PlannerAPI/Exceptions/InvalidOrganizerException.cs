using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class InvalidOrganizerException : Exception
    {
        public InvalidOrganizerException(string message) : base(message)
        {

        }

        public InvalidOrganizerException(string message, Exception e) : base(message, e)
        {

        }
    }
}
