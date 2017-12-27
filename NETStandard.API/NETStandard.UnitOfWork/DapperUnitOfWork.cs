using System;
using NETStandard.Repository;
using System.Data;
using System.Data.SqlClient;
using NETStandard.Interfaces;

namespace NETStandard.UnitOfWork
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed = false;
        private MovieRepository _movieRepository;
        private DirectorRepository _directorRepository;
        public MovieRepository MovieRepository
        {
            get
            {
                return _movieRepository ?? (_movieRepository = new MovieRepository(_transaction));
            }
        }
        public DirectorRepository DirectorRepository
        {
            get { return _directorRepository ?? (_directorRepository = new DirectorRepository(_transaction)); }
        }

        public DapperUnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                //_transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }
        private void ResetRepositories()
        {
            _movieRepository = null;
            _directorRepository = null;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
