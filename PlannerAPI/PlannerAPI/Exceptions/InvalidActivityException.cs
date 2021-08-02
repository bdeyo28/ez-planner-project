using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class InvalidActivityException : Exception
    {
        public InvalidActivityException(string message) : base(message)
        {

        }

        public InvalidActivityException(string message, Exception e) : base(message, e)
        {

        }

    }
}
