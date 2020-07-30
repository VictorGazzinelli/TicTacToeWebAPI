using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.Enums
{
    public enum TypePlayer
    {
        EMPTY = 0,
        X = 1,
        O = 2
    }

    public static class TypePlayerExtension
    {
        public static TypePlayer GetByPlay(string play)
        {
            switch (play)
            {
                case "X":
                    return TypePlayer.X;
                case "O":
                    return TypePlayer.O;
                default:
                    return TypePlayer.EMPTY;
            }
        }

        public static TypePlayer GetByCode(int code)
        {
            switch(code)
            {
                case 1:
                    return TypePlayer.X;
                case 2:
                    return TypePlayer.O;
                default:
                    return TypePlayer.EMPTY;
            }
        }

        public static string ToString(TypePlayer typePlayer)
        {
            switch (typePlayer)
            {
                case TypePlayer tp when tp == TypePlayer.O:
                    return "O";
                case TypePlayer tp when tp == TypePlayer.X:
                    return "X";
                default:
                    return null;
            }
        }
    }
}
