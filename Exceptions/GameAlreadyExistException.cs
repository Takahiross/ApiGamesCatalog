using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Exceptions
{
    public class GameAlreadyExistException : Exception
    {
        public GameAlreadyExistException() : base ("This game is already registered.")
        { }
    }
}
