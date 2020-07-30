using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeWebAPI.Models.Game
{
    public class CreateGameOutput
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string firstPlayer { get; set; }
    }
}
