using System;
using System.Collections.Generic;
using System.Text;
using GameEntity = TicTacToeWebAPI.Entities.Game.Game;
using MovementEntity = TicTacToeWebAPI.Entities.Movement.Movement;

namespace TicTacToeWebAPI.Boundaries.Repositories
{
    public interface IGameRepository
    {
        string Create(GameEntity game);
        GameEntity Retrieve(string id);
        GameEntity RegisterMovement(string id, MovementEntity movement);
    }
}
