using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.payments
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        public List<PaymentMethodModel> GetActiveMethods()
        {
            var list = new List<PaymentMethodModel>();
            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"SELECT ""Id"", ""Pershkrimi"", ""Shkurtesa"", ""Tipi"", ""ValutaDefault"", ""KerkonReference"", ""Aktiv"", ""Created_At"",
                             ""Updated_At"", ""Rendorja""
                             FROM ""MenyratPageses""
                             WHERE ""Aktiv"" = TRUE
                             ORDER BY COALESCE(""Rendorja"", 999), ""Pershkrimi"";";

            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new PaymentMethodModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Pershkrimi = reader["Pershkrimi"].ToString(),
                    Shkurtesa = reader["Shkurtesa"].ToString(),
                    Tipi = reader["Tipi"].ToString(),
                    ValutaDefault = reader["ValutaDefault"]?.ToString() ?? "EUR",
                    KerkonReference = reader["KerkonReference"] != DBNull.Value && Convert.ToBoolean(reader["KerkonReference"]),
                    Aktiv = Convert.ToBoolean(reader["Aktiv"]),
                    Rendorja = reader["Rendorja"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Rendorja"])
                });
            }

            return list;
        }

        public DataTable GetAll()
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT ""Id"", ""Pershkrimi"", ""Shkurtesa"", ""Tipi"", ""ValutaDefault"", ""KerkonReference"", ""Aktiv"", ""Created_At"",
                            ""Updated_At"", ""Rendorja""
                            FROM ""MenyratPageses""
                            ORDER BY ""Rendorja"", ""Pershkrimi"";";
            using var cmd = new NpgsqlCommand(query, conn);
            using var da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetActiveCurrencies()
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT ""Valuta"" 
                             FROM ""KursetKembimit""
                             WHERE ""Aktiv"" = TRUE
                             ORDER BY ""Valuta"";";
            using var cmd = new NpgsqlCommand(query, conn);
            using var da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool ExistsByPershkrimi(string pershkrimi, int? excludeId = null)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT COUNT(*)
                             FROM ""MenyratPageses""
                             WHERE LOWER(TRIM(""Pershkrimi"")) = LOWER(TRIM(@Pershkrimi))
                             AND (@Id IS NULL OR ""Id"" <> @Id);";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Pershkrimi", pershkrimi);
            cmd.Parameters.AddWithValue("@Id", (object?)excludeId ?? DBNull.Value);
            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public bool ExistsByShkurtesa(string shkurtesa, int? excludeId = null)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT COUNT(*) 
                             FROM ""MenyratPageses""
                             WHERE LOWER(TRIM(""Shkurtesa"")) = LOWER(TRIM(@Shkurtesa))
                             AND (@Id IS NULL OR ""Id"" <> @Id);";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Shkurtesa", shkurtesa);
            cmd.Parameters.AddWithValue("@Id", (object?)excludeId ?? DBNull.Value);
            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public bool ExistsByRendorja(int rendorja, int? excludeId = null)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT COUNT(*)
                             FROM ""MenyratPageses""
                             WHERE ""Rendorja"" = @Rendorja
                             AND (@Id IS NULL OR ""Id"" <> @Id);";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Rendorja", rendorja);
            cmd.Parameters.AddWithValue("@Id", (object?)excludeId ?? DBNull.Value);
            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        public void Insert(PaymentMethodModel method)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"INSERT INTO ""MenyratPageses""(""Pershkrimi"", ""Shkurtesa"", ""Tipi"", ""ValutaDefault"", ""KerkonReference"", ""Aktiv"", ""Created_At"",
                             ""Updated_At"", ""Rendorja"")
                             VALUES(@Pershkrimi, @Shkurtesa, @Tipi, @ValutaDefault, @KerkonReference, TRUE, CURRENT_TIMESTAMP, NULL ,@Rendorja);";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Pershkrimi", method.Pershkrimi);
            cmd.Parameters.AddWithValue("@Shkurtesa", method.Shkurtesa);
            cmd.Parameters.AddWithValue("@Tipi", method.Tipi);
            cmd.Parameters.AddWithValue("@ValutaDefault", method.ValutaDefault);
            cmd.Parameters.AddWithValue("@KerkonReference", method.KerkonReference);
            cmd.Parameters.AddWithValue("@Rendorja", method.Rendorja);
            cmd.ExecuteNonQuery();
        }

        public void Update(PaymentMethodModel method)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"UPDATE ""MenyratPageses""
                             SET ""Pershkrimi"" = @Pershkrimi,
                             ""Shkurtesa"" = @Shkurtesa,
                             ""KerkonReference"" = @KerkonReference,
                             ""Tipi"" = @Tipi,
                             ""ValutaDefault"" = @ValutaDefault,
                             ""Rendorja"" = @Rendorja,
'                            ""Updated_At"" = CURRENT_TIMESTAMP
                             WHERE ""Id"" = @Id;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Pershkrimi", method.Pershkrimi);
            cmd.Parameters.AddWithValue("@Shkurtesa", method.Shkurtesa);
            cmd.Parameters.AddWithValue("@KerkonReference", method.KerkonReference);
            cmd.Parameters.AddWithValue("@Tipi", method.Tipi);
            cmd.Parameters.AddWithValue("@ValutaDefault", method.ValutaDefault);
            cmd.Parameters.AddWithValue("@Rendorja", method.Rendorja);
            cmd.Parameters.AddWithValue("@Id", method.Id);
            cmd.ExecuteNonQuery();
        }

        public void Deactivate(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"UPDATE ""MenyratPageses""
                             SET ""Aktiv"" = FALSE,
                                 ""Updated_At"" = CURRENT_TIMESTAMP
                             WHERE ""Id"" = @Id;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
