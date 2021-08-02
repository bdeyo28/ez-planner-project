using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Exceptions
{
    public class InvalidScheduleException : Exception
    {
        public InvalidScheduleException(string message) : base(message)
        {

        }

        public InvalidScheduleException(string message, Exception e) : base(message, e)
        {

        }
    }
}
