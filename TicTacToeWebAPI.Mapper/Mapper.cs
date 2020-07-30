using System;
using System.Collections.Generic;
using System.Data;
using TicTacToeWebAPI.Boundaries.Repositories;
using TicTacToeWebAPI.Boundaries.Services.Game;
using TicTacToeWebAPI.Repositories.Game;
using TicTacToeWebAPI.Repositories.Movement;
using TicTacToeWebAPI.Services.Game;
using TicTacToeWebAPI.Utils.Boundary;
using TicTacToeWebAPI.Utils.InversionControl;

namespace TicTacToeWebAPI.Mapper
{
    public class Mapper
    {
        private static Mapper _instance = new Mapper();
        public static Mapper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Mapper();
                return _instance;
            }
        }

        private Mapper() { }

        public void RegisterMappings() => DependencyResolver.Instance().LoadMappings(ListMappings());

        private Mapping[] ListMappings() 
        {
            List<Mapping> mappingList = new List<Mapping>();

            #region Service Mapping

            #region Game

            mappingList.Add(new Mapping(typeof(IService<CreateGameRequest, CreateGameResponse>), typeof(CreateGameService)));
            mappingList.Add(new Mapping(typeof(IService<CreateMovementRequest, CreateMovementResponse>), typeof(CreateMovementService)));

            #endregion

            #endregion

            #region Repository Mapping

            mappingList.Add(new Mapping(typeof(IGameRepository), typeof(GameRepository)));
            mappingList.Add(new Mapping(typeof(IMovementRepository), typeof(MovementRepository)));

            #endregion

            return mappingList.ToArray();
        }
    }
}
