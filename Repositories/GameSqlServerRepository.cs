using ApiCatalagoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> GetGames(int page, int amount)
        {
            var games = new List<Game>();

            var comando = $"select * from Games order by id offset {((page - 1) * amount)} rows fetch next {amount} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"],
                    Description = (string)sqlDataReader["Description"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> GetGames(Guid id)
        {
            Game game = null;

            var comando = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"],
                    Description = (string)sqlDataReader["Description"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> GetGames(string title, string producer)
        {
            var games = new List<Game>();

            var comando = $"select * from Games where Title = '{title}' and Producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Title"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"],
                    Description = (string)sqlDataReader["Description"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task InsertGames(Game game)
        {
            var comando = $"insert Games (Id, Title, Producer, Price, Description) values ('{game.Id}', '{game.Title}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")}, '{game.Description}' )";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task UpdateGames(Game game)
        {
            var comando = $"update Games set Title = '{game.Title}', Producer = '{game.Producer}', Price = {game.Price.ToString().Replace(",", ".")}, Description = '{game.Description}' where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task RemoveGames(Guid id)
        {
            var comando = $"delete from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
