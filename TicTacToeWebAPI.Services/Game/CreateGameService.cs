using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeWebAPI.Boundaries.Repositories;
using TicTacToeWebAPI.Boundaries.Services.Game;
using TicTacToeWebAPI.Utils.Boundary;
using TicTacToeWebAPI.Utils.Enums;
using GameEntity = TicTacToeWebAPI.Entities.Game.Game;

namespace TicTacToeWebAPI.Services.Game
{
    public class CreateGameService : IService<CreateGameRequest, CreateGameResponse>
    {
        #region Repositories
        private readonly IGameRepository gameRepository;
        #endregion

        #region Constructors
        public CreateGameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        #endregion

        public CreateGameResponse Run(CreateGameRequest request)
        {
            GameEntity gameEntity = new GameEntity()
            {
                currentPlayer = new Random().Next() % 2 == 0 ?
                    TypePlayer.X :
                    TypePlayer.O
            };

            string id = gameRepository.Create(gameEntity);

            return new CreateGameResponse()
            {
                id = id,
                firstPlayer = TypePlayerExtension.ToString(gameEntity.currentPlayer)
            };
        }
    }
}
