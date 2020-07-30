using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeWebAPI.Boundaries.Dtos;
using TicTacToeWebAPI.Boundaries.Repositories;
using TicTacToeWebAPI.Boundaries.Services.Game;
using TicTacToeWebAPI.Utils.Boundary;
using TicTacToeWebAPI.Utils.Enums;
using TicTacToeWebAPI.Utils.Exceptions;
using TicTacToeWebAPI.Utils.Transaction;
using GameEntity = TicTacToeWebAPI.Entities.Game.Game;
using MovementEntity = TicTacToeWebAPI.Entities.Movement.Movement;

namespace TicTacToeWebAPI.Services.Game
{
    public class CreateMovementService : IService<CreateMovementRequest, CreateMovementResponse>
    {
        #region Repositories
        private readonly IGameRepository gameRepository;
        private readonly IMovementRepository movementRepository;
        #endregion

        #region Constructors
        public CreateMovementService(IGameRepository gameRepository, IMovementRepository movementRepository)
        {
            this.gameRepository = gameRepository;
            this.movementRepository = movementRepository;
        }
        #endregion

        public CreateMovementResponse Run(CreateMovementRequest request)
        {
            GameEntity game = gameRepository.Retrieve(request.id);

            if (game == null)
                throw new BadRequestException("Partida não encontrada");

            if (game.currentPlayer != TypePlayerExtension.GetByPlay(request.player))
                throw new BadRequestException("Não é turno do jogador");

            if (!PositionIsValid(request.position, game))
                throw new BadRequestException("Espaço inválido");

            MovementEntity movement = new MovementEntity()
            {
                gameId = request.id,
                player = TypePlayerExtension.GetByPlay(request.player),
                turn = game.turn,
                x = request.position.x,
                y = request.position.y
            };

            game = TransactionHandler.HandleTransaction(() =>
            {
                movementRepository.Create(movement);
                return gameRepository.RegisterMovement(request.id, movement);
            });

            string winner = CheckForWinner(game.board);
            string msg = null;

            if (game.turn >= 9 && winner == null)
            {
                winner = "Draw";
            }

            if (winner != null)
                msg = "Partida finalizada";
            else
                msg = "Partida em andamento";

            return new CreateMovementResponse()
            {
                msg = msg,
                winner = winner
            };
        }

        private string CheckForWinner(string[,] board)
        {
            for(int i = 0; i < 3; i++)
            {
                // Check for columns
                if (String.Equals(board[i, 0], board[i, 1]) && String.Equals(board[i, 1], board[i, 2]))
                    return board[i, 0];
                // Check for rows
                if(String.Equals(board[0, i], board[1, i]) && String.Equals(board[1, i], board[2, i]))
                    return board[0, i];
            }

            // Check for main diagonal
            if (String.Equals(board[0, 0], board[1, 1]) && String.Equals(board[1, 1], board[2, 2]))
                return board[0, 0];

            // Check for secondary diagonal
            if (String.Equals(board[2, 0], board[1, 1]) && String.Equals(board[1, 1], board[0, 2]))
                return board[2, 0];

            return null;
        }

        private bool PositionIsValid(PositionDTO position, GameEntity game)
        {
            return (position.x >= 0 && position.y >= 0) &&
                   (position.x <= 2 && position.y <= 2) &&
                   !game.PositionIsPlayed(position.x, position.y);
        }
    }
}
