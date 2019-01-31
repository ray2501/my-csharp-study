using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using pnotes.Data;
using System.Linq;

namespace pnotes.Services
{
    public class PGNoteService: INoteService
    {
        private string connectionString;

        public PGNoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get  {
                return new NpgsqlConnection(connectionString);
            }
        }

        public async Task<IEnumerable<Note>> GetAllAsync() {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                IEnumerable<Note> result = 
		    await dbConnection.QueryAsync<Note>("SELECT * FROM Notes order by Created");
                return result;
            }
        }
        public async Task<Note> GetNoteByIdAsync(Guid id) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM Notes" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                IEnumerable<Note> result = await dbConnection.QueryAsync<Note>(sQuery, new { Id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task AddNoteAsync(Note note) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO Notes (Title, Body, Created)"
                                + " VALUES(@Title, @Body, now())";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sQuery, note);
            }
        }
        public async Task UpdateNoteAsync(Note note) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE Notes SET Title = @Title, Body = @Body"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sQuery, note);
            }
        }
        public async Task DeleteNoteByIdAsync(Guid id) {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM Notes" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(sQuery, new { Id = id });
            }
        }
    }
}
