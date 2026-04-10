using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject.repositories.payments
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        public decimal GetLatestRate(string valuta)
        {
            if (string.IsNullOrWhiteSpace(valuta) || valuta.Trim().ToUpper() == "EUR")
                return 1m;

            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"SELECT ""KursiNeEuro""
                             FROM ""KursetKembimit""
                             WHERE UPPER(""Valuta"") = @valuta
                             AND ""Aktiv"" = TRUE
                             ORDER BY ""CreatedAt"" DESC
                             LIMIT 1;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@valuta", valuta.Trim().ToUpper());
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 1m : Convert.ToDecimal(result);
        }
    }
}
