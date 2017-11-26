using NETStandard.Entities;
using NETStandard.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;

namespace NETStandard.Repository
{
    public class MovieRepository : BaseRepository, IRepository<Movie>
    {
        public async Task<bool> AddAsync(Movie t)
        {
            try
            {
                var command = @"INSERT INTO Movies (Title,Description,Duration) VALUES (@title, @desc, @duration) SELECT CAST(SCOPE_IDENTITY() as int)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@title", t.Title);
                parameters.Add("@desc", t.Description);
                parameters.Add("@duration", t.Duration);
                // To get inserted item's id.
                //var id = SqlMapper.Query<int>(connection, command, parameters).Single();
                return Convert.ToBoolean(await SqlMapper.ExecuteAsync(connection, command, parameters));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var command = @"DELETE FROM Movies Where Id = @Id";
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@Id", id);
                return Convert.ToBoolean(await SqlMapper.ExecuteAsync(connection, command, new { Id = id }));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> FindByAsync(Func<Movie, bool> predicate)
        {
            throw new NotImplementedException();
            //db.Query<Person, Address, Person>(
            //sql,
            //(person, address) => {
            //    person.Address = address;
            //    return person;
            //},
            //splitOn: "AddressId"
            //).ToList();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var command = @"SELECT Id,Title,Description,Duration
                            FROM Movies";
            return await SqlMapper.QueryAsync<Movie>(connection, command);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            try
            {
                var command = @"SELECT Id,Title,Description,Duration
                            FROM Movies WHERE Id = @Id";
                var res = await SqlMapper.QueryAsync<Movie>(connection, command, new { Id = id });
                return res.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Movie t)
        {
            try
            {
                var command = @"UPDATE Movies SET Title = @title, Description = @desc, Duration= @duration WHERE Id = @Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@title", t.Title);
                parameters.Add("@desc", t.Description);
                parameters.Add("@duration", t.Duration);
                parameters.Add("@Id", t.Id);
                return Convert.ToBoolean(await SqlMapper.ExecuteAsync(connection, command, parameters));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
