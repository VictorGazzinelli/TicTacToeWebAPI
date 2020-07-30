using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TicTacToeWebAPI.Entities.Base;
using TicTacToeWebAPI.Models.Game;
using TicTacToeWebAPI.Tests.Config;
using TicTacToeWebAPI.Utils.Exceptions;

namespace TicTacToeWebAPI.Tests
{
    [TestClass]
    public class GameControllerTest : TestBase
    {
        const string NEW_MOVEMENT_ENDPOINT = "game/fbf7d720-df90-48c4-91f7-9462deafefb8/movement";
        public async Task<CreateGameOutput> CreateNewGame() => await CallEndpoint<CreateGameOutput>("game", HttpMethod.Post, null);
        private string SwitchPlayer(string player)
        {
            if (String.Equals(player, "X"))
                return "O";
            else
                return "X";
        }

        [TestMethod("Create new game returns valid id and firstPlayer")]
        public async Task CreateNewGameReturnsValidIdAndFirstPlayer()
        {
            //Assign
            Guid guid;

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();

            //Assert
            Assert.IsTrue(Guid.TryParse(createGameOutput.id, out guid));
            Assert.IsTrue(String.Equals(createGameOutput.firstPlayer, "X") || String.Equals(createGameOutput.firstPlayer, "O"));
        }

        [TestMethod("Returns BadRequestException on new movement with invalid game id")]
        [ExpectedException(typeof(BadRequestException), "Não é turno do jogador")]
        public async Task ReturnsErrorOnNewMovementOfInexistentGameId()
        {
            //Assign
            dynamic input = new DynamicDictionary();
            input.id = "ghijklmn-opqr-stuv-wxyz-ghijklmnopqr";
            input.player = "X";
            input.position = new
            {
                x = 0,
                y = 1
            };

            //Act
            dynamic endpointResponse = await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            //Assert.Fail();
        }

        [TestMethod("Returns BadRequestException on invalid player")]
        [ExpectedException(typeof(BadRequestException), "Não é turno do jogador")]
        public async Task ReturnsErrorOnInvalidPlayer()
        {
            //Assign
            dynamic input = new DynamicDictionary();

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();
            input.id = createGameOutput.id;
            input.player = SwitchPlayer(createGameOutput.firstPlayer);
            input.position = new
            {
                x = 1,
                y = 0,
            };
            dynamic endpointResponse = await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            //Assert.Fail();
        }

        [TestMethod("Creates a valid game with winner on row")]
        public async Task NewGameWithWinnerOnRow()
        {
            //Assign
            dynamic input = new DynamicDictionary();

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();
            input.id = createGameOutput.id;
            string firstPlayer = createGameOutput.firstPlayer;
            input.player = firstPlayer;
            input.position = new
            {
                x = 0,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 2,
            };
            CreateMovementOutput endpointResponse = await CallEndpoint<CreateMovementOutput>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            Assert.IsTrue(String.Equals(firstPlayer, endpointResponse.winner));
            Assert.IsTrue(String.Equals("Partida finalizada", endpointResponse.msg));
        }

        [TestMethod("Creates a valid game with winner on column")]
        public async Task NewGameWithWinnerOnColumn()
        {
            //Assign
            dynamic input = new DynamicDictionary();

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();
            input.id = createGameOutput.id;
            string firstPlayer = createGameOutput.firstPlayer;
            input.player = firstPlayer;
            input.position = new
            {
                x = 0,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 0,
            };
            CreateMovementOutput endpointResponse = await CallEndpoint<CreateMovementOutput>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            Assert.IsTrue(String.Equals(firstPlayer, endpointResponse.winner));
            Assert.IsTrue(String.Equals("Partida finalizada", endpointResponse.msg));
        }

        [TestMethod("Creates a valid game with winner on main diagonal")]
        public async Task NewGameWithWinnerOnDiagonal()
        {
            //Assign
            dynamic input = new DynamicDictionary();

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();
            input.id = createGameOutput.id;
            string firstPlayer = createGameOutput.firstPlayer;
            input.player = firstPlayer;
            input.position = new
            {
                x = 0,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 2,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 2,
            };
            CreateMovementOutput endpointResponse = await CallEndpoint<CreateMovementOutput>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            Assert.IsTrue(String.Equals(firstPlayer, endpointResponse.winner));
            Assert.IsTrue(String.Equals("Partida finalizada", endpointResponse.msg));
        }

        [TestMethod("Creates a valid game with tie as the result")]
        public async Task NewGameWithTie()
        {
            //Assign
            dynamic input = new DynamicDictionary();

            //Act
            CreateGameOutput createGameOutput = await CreateNewGame();
            input.id = createGameOutput.id;
            string firstPlayer = createGameOutput.firstPlayer;
            input.player = firstPlayer;
            input.position = new
            {
                x = 0,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 2,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 2,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 0,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 0,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 2,
                y = 1,
            };
            await CallEndpoint<dynamic>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);
            input.player = SwitchPlayer(input.player);
            input.position = new
            {
                x = 1,
                y = 2,
            };
            CreateMovementOutput endpointResponse = await CallEndpoint<CreateMovementOutput>(NEW_MOVEMENT_ENDPOINT, HttpMethod.Post, input.dictionary);

            //Assert
            Assert.IsTrue(String.Equals("Draw", endpointResponse.winner));
            Assert.IsTrue(String.Equals("Partida finalizada", endpointResponse.msg));
        }
    }
}
