using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeWebAPI.Boundaries.Dtos;

namespace TicTacToeWebAPI.Models.Game
{
    public class CreateMovementInput
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string player { get; set; }
        [Required]
        public PositionDTO position { get; set; }
    }
}
