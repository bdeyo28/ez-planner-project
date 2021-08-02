using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class NullObjectException : Exception
    {
        public NullObjectException(string message) : base(message)
        {

        }

        public NullObjectException(string message, Exception e) : base(message, e)
        {

        }
    }
}
