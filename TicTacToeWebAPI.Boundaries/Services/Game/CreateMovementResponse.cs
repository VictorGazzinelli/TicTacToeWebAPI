using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Boundaries.Services.Game
{
    public class CreateMovementResponse
    {
        public string msg { get; set; }
        public string winner { get; set; }
    }
}
