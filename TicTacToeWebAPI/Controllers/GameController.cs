using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeWebAPI.Boundaries.Services.Game;
using TicTacToeWebAPI.Models.Game;
using TicTacToeWebAPI.Utils.Boundary;
using TicTacToeWebAPI.Utils.InversionControl;
using TicTacToeWebAPI.Utils.Messages;

namespace TicTacToeWebAPI.Controllers
{
    [Produces("application/json")]
    [RequireHttps]
    [ApiController]
    public class GameController : ControllerBase
    {
        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <response code="200"> Returns the newly created game </response>
        [HttpPost]
        [Route("game")]
        [ProducesResponseType(typeof(CreateGameOutput), 200)]
        public CreateGameOutput CreateGame()
        {
            CreateGameResponse createGameResponse = DependencyResolver.Instance()
                .GetInstanceOf<IService<CreateGameRequest, CreateGameResponse>>()
                .Run(new CreateGameRequest());

            return new CreateGameOutput()
            {
                id = createGameResponse.id,
                firstPlayer = createGameResponse.firstPlayer
            };
        }

        /// <summary>
        /// Creates a new movement in a game with given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST game/{id}/movement
        ///     {        
        ///       "id": "fbf7d720-df90-48c4-91f7-9462deafefb8",
        ///       "player": "X",
        ///       "position": {
        ///           "x": 0,
        ///           "y": 1
        ///       }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">
        /// The movement is valid and will return 200 status code
        /// </response>
        /// <response code="400">
        /// The movement is invalid and will return 400 status code with a message saying what bad was found in the request 
        /// </response>
        [HttpPost]
        [Route("game/{id}/movement")]
        [ProducesResponseType(typeof(CreateMovementOutput), 200)]
        [ProducesResponseType(typeof(BadRequestMessage), 400)]
        public CreateMovementOutput CreateMovement(CreateMovementInput createMovementInput)
        {
            CreateMovementResponse createMovementResponse = DependencyResolver.Instance()
                .GetInstanceOf<IService<CreateMovementRequest, CreateMovementResponse>>()
                .Run(new CreateMovementRequest(){ 
                    id = createMovementInput.id,
                    player = createMovementInput.player,
                    position = createMovementInput.position
                });

            return new CreateMovementOutput()
            {
                msg = createMovementResponse.msg,
                winner = createMovementResponse.winner
            };
        }
    }
}
