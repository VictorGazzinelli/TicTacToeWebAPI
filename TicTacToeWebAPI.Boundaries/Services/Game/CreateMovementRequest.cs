using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeWebAPI.Boundaries.Dtos;

namespace TicTacToeWebAPI.Boundaries.Services.Game
{
    public class CreateMovementRequest
    {
        public string id { get; set; }
        public string player { get; set; }
        public PositionDTO position { get; set; }
    }
}
