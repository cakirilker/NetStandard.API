using NETStandard.Interfaces;
using System;
using System.Data;
using System.Collections.Generic;

namespace NETStandard.Repository
{
    public abstract class DapperRepositoryBase : IDisposable
    {
        protected IDbConnection Connection { get { return Transaction.Connection; } }
        protected IDbTransaction Transaction { get; private set; }

        public DapperRepositoryBase(IDbTransaction transaction)
        {
            this.Transaction = transaction;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
