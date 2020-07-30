using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicTacToeWebAPI.Utils.Messages
{
    public class BadRequestMessage
    {
        [Required]
        public string msg { get; set; }

        public BadRequestMessage(string msg = "Something went wrong when processing your request")
        {
            this.msg = msg;
        }
    }
}
