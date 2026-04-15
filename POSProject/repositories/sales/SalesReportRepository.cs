using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using POSProject.models;

namespace POSProject.repositories.sales
{
    public class SalesReportRepository : ISalesReportRepository
    {
        public DataTable GetSales(SalesFilterModel filter)
        {
            DataTable dt = new DataTable();

            using (var connection = Db.GetConnection())
            {
                connection.Open();

                string query = @"SELECT 
                                s.""NrFatures"" AS ""NrFatures"",
                                s.""DataShitjes"" AS ""DataShitjes"",
                                p.""username"" AS ""Cashier"",
                                s.""ShiftId"" AS ""ShiftId"",
                                s.""Totali"" AS ""Totali"",
                                s.""Koment"" AS ""Koment""
                                FROM ""Shitjet"" s
                                INNER JOIN ""perdoruesit"" p
                                    ON s.""perdoruesi_id"" = p.""id""
                                LEFT JOIN ""CashierShifts"" cs
                                    ON s.""ShiftId"" = cs.""Id""
                                WHERE s.""DataShitjes""::date >= @fromDate
                                  AND s.""DataShitjes""::date <= @toDate";

                if (!string.IsNullOrWhiteSpace(filter.Cashier))
                {
                    query += @" AND p.""username"" ILIKE @cashier";
                }

                query += @" ORDER BY s.""DataShitjes"" DESC, s.""Id"" DESC;";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@fromDate", NpgsqlDbType.Date).Value = filter.FromDate.Date;
                    cmd.Parameters.Add("@toDate", NpgsqlDbType.Date).Value = filter.ToDate.Date;

                    if (!string.IsNullOrWhiteSpace(filter.Cashier))
                        cmd.Parameters.AddWithValue("@cashier", filter.Cashier.Trim() + "%");

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetTopSoldProducts(SalesFilterModel filter)
        {
            DataTable dt = new DataTable();

            using (var connection = Db.GetConnection())
            {
                connection.Open();

                string query = @"SELECT
                                a.""Emri"" AS ""Produkti"",
                                a.""Barkodi"" AS ""Barkodi"",
                                k.""Emri"" AS ""Kategoria"",
                                SUM(sd.""Sasia"") AS ""SasiaShitur"",
                                a.""CmimiShitjes"" AS ""Cmimi"",
                                SUM(sd.""Vlera"") AS ""TotaliFituar"",
                                a.""SasiaNeStok"" AS ""StokuMbetur""
                                FROM ""ShitjetDetale"" sd
                                INNER JOIN ""Shitjet"" s
                                    ON sd.""ShitjaId"" = s.""Id""
                                INNER JOIN ""Artikujt"" a
                                    ON sd.""ArtikulliId"" = a.""Id""
                                LEFT JOIN ""kategorite"" k
                                    ON a.""KategoriaId"" = k.""id""
                                WHERE s.""DataShitjes""::date >= @fromDate
                                  AND s.""DataShitjes""::date <= @toDate
                                GROUP BY
                                    a.""Id"",
                                    a.""Emri"",
                                    a.""Barkodi"",
                                    k.""Emri"",
                                    a.""CmimiShitjes"",
                                    a.""SasiaNeStok""
                                ORDER BY SUM(sd.""Sasia"") DESC, SUM(sd.""Vlera"") DESC;";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@fromDate", NpgsqlDbType.Date).Value = filter.FromDate.Date;
                    cmd.Parameters.Add("@toDate", NpgsqlDbType.Date).Value = filter.ToDate.Date;

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
