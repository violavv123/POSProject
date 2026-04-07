using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject
{
    public class Db
    {
        private static string connString= "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=POS";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}
