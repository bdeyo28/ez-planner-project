using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class DuplicateUsernameException : Exception
    {

        public DuplicateUsernameException(string message) : base(message)
        {

        }

        public DuplicateUsernameException(string message, Exception e): base(message, e)
        {

        }
    }
}
