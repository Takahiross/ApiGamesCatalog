using ApiCatalagoJogos.Entities;
using ApiCatalagoJogos.Exceptions;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.Repositories;
using ApiCatalagoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task Delete(Guid id)
        {
            var game = _gameRepository.GetGames(id);
            if (game == null)
                throw new GameAlreadyExistException();
            await _gameRepository.RemoveGames(id);
        }

        public async Task<List<GameViewModel>> GetGame(int page, int amount)
        {
            var games = await _gameRepository.GetGames(page, amount);
            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Producer = game.Producer,
                Price = game.Price,
                Description = game.Description
            }).ToList();
        }

        public async Task<GameViewModel> GetGameById(Guid id)
        {
            var game = await _gameRepository.GetGames(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Producer = game.Producer,
                Price = game.Price,
                Description = game.Description
            };
        }

        public async Task<GameViewModel> InsertGame(GameInputModel game)
        {
            var entityGame = await _gameRepository.GetGames(game.Title, game.Producer);
            if (entityGame.Count > 0)
                throw new GameAlreadyExistException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Title = game.Title,
                Producer = game.Producer,
                Price = game.Price,
                Description = game.Description
            };

            await _gameRepository.InsertGames(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Title = gameInsert.Title,
                Producer = gameInsert.Producer,
                Price = gameInsert.Price,
                Description = gameInsert.Description
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.GetGames(id);
            if (entityGame == null)
                throw new GameAlreadyExistException();

            entityGame.Title = game.Title;
            entityGame.Producer = game.Producer;
            entityGame.Price = game.Price;

            await _gameRepository.UpdateGames(entityGame);
        }

        public async Task Update(Guid id, double price)
        {
            var entityGame = await _gameRepository.GetGames(id);
            if (entityGame == null)
                throw new GameAlreadyExistException();
            entityGame.Price = price;
            await _gameRepository.UpdateGames(entityGame);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
