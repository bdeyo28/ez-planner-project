using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class InvalidEventException : Exception
    {
        public InvalidEventException(string message) : base(message)
        {

        }

        public InvalidEventException(string message, Exception e) : base(message, e)
        {

        }

    }
}
