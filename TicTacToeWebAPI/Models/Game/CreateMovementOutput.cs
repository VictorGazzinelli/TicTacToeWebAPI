using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeWebAPI.Boundaries.Dtos;

namespace TicTacToeWebAPI.Models.Game
{
    public class CreateMovementOutput
    {
        [Required]
        public string msg { get; set; }
        public string winner { get; set; }
    }
}
