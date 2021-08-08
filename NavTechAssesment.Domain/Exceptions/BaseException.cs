using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Initializes a new exception base instance with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected BaseException(string message) : base(message)
        {
        }
    }
}
