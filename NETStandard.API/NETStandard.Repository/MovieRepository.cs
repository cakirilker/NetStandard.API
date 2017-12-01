using NETStandard.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System.Data;
using NETStandard.Repository.Interfaces;

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

        public IEnumerable<Movie> GetAll()
        {
            var command = @"SELECT M.*, D.* FROM Movies M INNER JOIN Directors D ON D.Id = M.DirectorId";
            return SqlMapper.Query<Movie, Director, Movie>(Connection, command, (m, d) => { m.Director = d; return m; }, null, Transaction);
        }

        public Movie GetById(int id)
        {
            var command = @"SELECT M.*, D.*
                            FROM Movies M INNER JOIN Directors D ON D.Id = M.DirectorId WHERE M.Id = @Id";
            //return SqlMapper.Query<Movie,Director>(Connection, command, new { Id = id }, Transaction).FirstOrDefault();
            var movie = SqlMapper.Query<Movie, Director, Movie>(
                Connection,
                command,
                (m, d) =>
                {
                    m.Director = d;
                    return m;
                },
                new { Id = id },
                Transaction).FirstOrDefault();
            // We have to use splitOn parameter, if foreign key is not Id or ID

            return movie;
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
