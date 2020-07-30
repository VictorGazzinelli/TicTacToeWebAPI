using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.InversionControl
{
    public class OverrideMappingType : IOverrideMapping
    {
        public Type From { get; set; }
        public object To { get; set; }
    }
}
