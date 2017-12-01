using Dapper;
using NETStandard.Entities;
using NETStandard.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NETStandard.Repository
{
    public class DirectorRepository : BaseRepository, IRepository<Director>
    {
        public DirectorRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public int Add(Director t)
        {
            var command = @"INSERT INTO Directors (Firstname,Lastname) VALUES (@firstname,@lastname) SELECT CAST(SCOPE_IDENTITY() as int)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@firstname", t.Firstname);
            parameters.Add("@lastname", t.Lastname);
            return SqlMapper.Execute(Connection, command, parameters, Transaction);
        }

        public int Delete(int id)
        {
            var command = @"DELETE FROM Directors Where Id = @Id";
            return SqlMapper.Execute(Connection, command, new { Id = id }, Transaction);
        }

        public IEnumerable<Director> GetAll()
        {
            var command = @"SELECT Id,Firstname,Lastname FROM Directors";
            return SqlMapper.Query<Director>(Connection, command, transaction: Transaction);
        }

        public Director GetById(int id)
        {
            var command = @"SELECT Id,Firstname,Lastname
                            FROM Directors WHERE Id = @Id";
            return SqlMapper.Query<Director>(Connection, command, new { Id = id }, Transaction).FirstOrDefault();
        }

        public int Update(Director t)
        {
            var command = @"UPDATE Directors SET Firstname = @firstname, Lastname = @lastname WHERE Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@firstname", t.Firstname);
            parameters.Add("@lastname", t.Lastname);
            parameters.Add("@Id", t.Id);
            return SqlMapper.Execute(Connection, command, parameters, Transaction);
        }
    }
}
