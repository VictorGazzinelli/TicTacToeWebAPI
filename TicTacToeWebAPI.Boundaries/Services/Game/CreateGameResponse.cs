using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Boundaries.Services.Game
{
    public class CreateGameResponse
    {
        public string id { get; set; }
        public string firstPlayer { get; set; }
    }
}
