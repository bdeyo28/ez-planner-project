using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class InvalidPasswordException : Exception
    {

        public InvalidPasswordException(string message): base(message)
        {

        }

        public InvalidPasswordException(string message, Exception e) : base(message, e)
        {

        }
    }
}
