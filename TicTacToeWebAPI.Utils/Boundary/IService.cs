using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeWebAPI.Utils.Boundary
{
    public interface IService<TRequest, TResponse>
    {
        TResponse Run(TRequest request);
    }
}
