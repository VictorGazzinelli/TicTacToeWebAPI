using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.Exceptions
{
    public abstract class HttpResponseException : Exception
    {
        public virtual string ExceptionName { get; set; } = "HttpResponseException";
        public virtual int Status { get; set; } = StatusCodes.Status500InternalServerError;

        public HttpResponseException() : base() {}

        public HttpResponseException(string message, Exception innerException = null) : base(message, innerException){}

        public HttpResponseException(Exception exception) : base(exception.Message, exception.InnerException){}
    }
}
