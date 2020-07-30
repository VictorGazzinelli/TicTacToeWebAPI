using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.InversionControl
{
    public class Mapping
    {
        public string Name { get; private set; }
        public Type From { get; private set; }
        public Type To { get; private set; }

        public Mapping(Type from, Type to) : this(string.Empty, from, to){ }

        public Mapping(string name, Type from, Type to)
        {
            this.Name = name;
            this.From = from;
            this.To = to;
        }

        public override string ToString() =>
            $"{this.From.Name}-{this.To.Name}";
    }
}
