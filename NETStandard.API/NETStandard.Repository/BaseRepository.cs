using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace NETStandard.Repository
{
    public class BaseRepository : IDisposable
    {
        protected IDbConnection connection;
        public BaseRepository()
        {
            this.connection = new SqlConnection("server=DESKTOP-M9T66IU\\SQLEXPRESS;database=XamarinFormsExample;integrated security=true;MultipleActiveResultSets=true");
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
