using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Service
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGame(int page, int amount);
        Task<GameViewModel> GetGameById(Guid id);
        Task<GameViewModel> InsertGame(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
