using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeWebAPI.Boundaries.Repositories;
using TicTacToeWebAPI.Utils.Enums;
using GameEntity = TicTacToeWebAPI.Entities.Game.Game;
using MovementEntity = TicTacToeWebAPI.Entities.Movement.Movement;

namespace TicTacToeWebAPI.Repositories.Game
{
    public class GameRepository : IGameRepository
    {
        static List<GameEntity> list_Games_In_Memory = new List<GameEntity>();

        public string Create(GameEntity game)
        {
            string id = Guid.NewGuid().ToString();
            game.id = id;
            game.board = new string[3,3];
            list_Games_In_Memory.Add(game);

            return id;
        }

        public GameEntity RegisterMovement(string id, MovementEntity movement)
        {
            GameEntity game = list_Games_In_Memory.Where(g => String.Equals(g.id, id))
                .FirstOrDefault();

            if (game == null)
                return null;

            int index = list_Games_In_Memory.IndexOf(game);
            game.board[movement.x, movement.y] = TypePlayerExtension.ToString(movement.player);
            game.SwitchPlayer();
            game.turn += 1;
            list_Games_In_Memory[index] = game;

            return game;
        }

        public GameEntity Retrieve(string id)
        {
            return list_Games_In_Memory.Where(g => String.Equals(g.id, id))
                .FirstOrDefault();
        }
    }
}
