using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace NETStandard.Repository
{
    public class BaseRepository : IDisposable
    {
        protected IDbConnection Connection { get { return Transaction.Connection; } }
        protected IDbTransaction Transaction { get; private set; }
        public BaseRepository(IDbTransaction transaction)
        {
            //this.Connection = new SqlConnection("server=DESKTOP-M9T66IU\\SQLEXPRESS;database=XamarinFormsExample;integrated security=true;MultipleActiveResultSets=true");
            this.Transaction = transaction;
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
