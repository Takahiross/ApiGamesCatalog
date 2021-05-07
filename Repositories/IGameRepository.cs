using ApiCatalagoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGames(int page, int amout);
        Task<Game> GetGames(Guid id);
        Task<List<Game>> GetGames(string name, string producer);
        Task InsertGames(Game game);
        Task UpdateGames(Game game);
        Task RemoveGames(Guid id);
    }
}
