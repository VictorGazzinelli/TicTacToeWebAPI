using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToeWebAPI.Utils.Enums;
using Position = TicTacToeWebAPI.Entities.Base.Position;

namespace TicTacToeWebAPI.Entities.Movement
{
    public class Movement : Position
    {
        public string id { get; set; }
        public string gameId { get; set; }
        public TypePlayer player { get; set; }
        public DateTime createdAt { get; set; }
        public int turn { get; set; }
    }
}
