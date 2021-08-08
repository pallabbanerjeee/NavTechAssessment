using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Exceptions
{
    public class ConfigEntityException : BaseException
    {
        /// <summary>
        /// Initializes a new ConfigEntity exception instance with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The status code returned by the dependency</param>
        public ConfigEntityException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        /// <summary>
        /// Gets the status code of the exception.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}
