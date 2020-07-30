using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.Exceptions
{
    public class BadRequestException : HttpResponseException
    {
        public override string ExceptionName { get; set; } = "BadRequestException";
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;

        public BadRequestException() : base(){}

        public BadRequestException(string message) : base(message){}
    }
}
