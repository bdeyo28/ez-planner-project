using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string message) : base(message)
        {

        }

        public EmptyListException(string message, Exception e) : base(message, e)
        {

        }
    }
}
