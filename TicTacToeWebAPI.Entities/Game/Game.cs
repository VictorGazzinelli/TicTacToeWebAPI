using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToeWebAPI.Utils.Enums;

namespace TicTacToeWebAPI.Entities.Game
{
    public class Game
    {
        public string id { get; set; }
        public TypePlayer currentPlayer  { get; set; }
        public int turn { get; set; }
        public string[,] board { get; set; }

        public void SwitchPlayer()
        {
            if (this.currentPlayer == TypePlayer.X)
                this.currentPlayer = TypePlayer.O;
            else
                this.currentPlayer = TypePlayer.X;
        }

        public bool PositionIsPlayed(int x, int y)
        {
            return board[x,y] != null;
        }
    }
}
