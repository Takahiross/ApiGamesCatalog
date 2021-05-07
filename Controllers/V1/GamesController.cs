using ApiCatalagoJogos.Exceptions;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.Service;
using ApiCatalagoJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.GetGame(page, amount);
            if (games.Count() == 0)
                return NoContent();
            return Ok(games);
        }

        [HttpGet("{idGame:guid")]
        public async Task<ActionResult<GameViewModel>> GetGameById([FromRoute] Guid idGame)
        {
            var game = await _gameService.GetGameById(idGame);
            if (game == null)
                return NoContent();
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.InsertGame(gameInputModel);
                return Ok(game);
            }
            catch (GameAlreadyExistException ex)
            {
                return UnprocessableEntity("There's alreade a game with that name for this producer.");
            }
        }

        [HttpPut("{idGame: guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("There is no such game.");
            }
        }

        [HttpPatch("{idGame: guid/price/{price: double}}")]
        public async Task<ActionResult> UpdateGamePrice([FromRoute] Guid idGame, [FromBody] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("There is no such game.");
            }
        }

        [HttpDelete("{idGame: guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);
                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("There's no such game.");
            }
        }
    }
}
