using NETStandard.Entities;
using NETStandard.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System.Data;

namespace NETStandard.Repository
{
    public class MovieRepository : BaseRepository, IRepository<Movie>
    {
        public MovieRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public int Add(Movie t)
        {
            var command = @"INSERT INTO Movies (Title,Description,Duration) VALUES (@title, @desc, @duration) SELECT CAST(SCOPE_IDENTITY() as int)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@title", t.Title);
            parameters.Add("@desc", t.Description);
            parameters.Add("@duration", t.Duration);
            // To get inserted item's id.
            //var id = SqlMapper.Query<int>(connection, command, parameters).Single();
            return SqlMapper.Execute(Connection, command, parameters, Transaction);
        }

        public int Delete(int id)
        {
            var command = @"DELETE FROM Movies Where Id = @Id";
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@Id", id);
            return SqlMapper.Execute(Connection, command, new { Id = id }, Transaction);
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Movie> FindBy(Func<Movie, bool> predicate)
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

        public IEnumerable<Movie> GetAll()
        {
            var command = @"SELECT Id,Title,Description,Duration FROM Movies";
            return SqlMapper.Query<Movie>(Connection, command, transaction: Transaction);
        }

        public IEnumerable<Movie> GetExecute()
        {
            var command = @"SELECT Id,Title,Description,Duration FROM Movies";
            return SqlMapper.Query<Movie>(Connection, command, transaction: Transaction);
        }

        public Movie GetById(int id)
        {
            var command = @"SELECT Id,Title,Description,Duration
                            FROM Movies WHERE Id = @Id";
            return SqlMapper.Query<Movie>(Connection, command, new { Id = id }, Transaction).FirstOrDefault();
        }

        public int Update(Movie t)
        {
            var command = @"UPDATE Movies SET Title = @title, Description = @desc, Duration= @duration WHERE Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@title", t.Title);
            parameters.Add("@desc", t.Description);
            parameters.Add("@duration", t.Duration);
            parameters.Add("@Id", t.Id);
            return SqlMapper.Execute(Connection, command, parameters, Transaction);
        }
    }
}
