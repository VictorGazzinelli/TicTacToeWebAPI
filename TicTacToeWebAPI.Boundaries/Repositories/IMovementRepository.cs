using System;
using System.Collections.Generic;
using System.Text;
using MovementEntity = TicTacToeWebAPI.Entities.Movement.Movement;

namespace TicTacToeWebAPI.Boundaries.Repositories
{
    public interface IMovementRepository 
    {
        public string Create(MovementEntity movement);
        MovementEntity Retrieve(string id);
    }
}
