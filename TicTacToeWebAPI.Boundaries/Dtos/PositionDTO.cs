using System;
using System.Collections.Generic;
using System.Text;
using Position = TicTacToeWebAPI.Entities.Base.Position;

namespace TicTacToeWebAPI.Boundaries.Dtos
{
    public class PositionDTO
    {
        public int x { get; set; }
        public int y { get; set; }

        public PositionDTO()
        {

        }

        public PositionDTO(Position position)
        {
            this.x = x;
            this.y = y;
        }
    }
}
