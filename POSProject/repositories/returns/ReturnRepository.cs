using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject.repositories.returns
{
    public class ReturnRepository : IReturnRepository
    {
        public DataTable GetRefundMethods()
        {
            const string query = @"SELECT ""Id"", ""Pershkrimi"", ""Tipi""
                                   FROM ""MenyratPageses""
                                   WHERE ""Aktiv"" = TRUE
                                   ORDER BY ""Pershkrimi"";";

            using var conn = Db.GetConnection();
            using var da = new NpgsqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataRow GetSaleHeaderByInvoiceNumber(string nrFatures)
        {
            const string query = @"SELECT s.""Id"", s.""NrFatures"", s.""DataShitjes"", s.""Totali"", COALESCE(p.""username"", 'N/A') as ""CashierName""
                                   FROM ""Shitjet"" s
                                   LEFT JOIN ""perdoruesit"" p ON p.""id"" = s.""perdoruesi_id""
                                   WHERE s.""NrFatures"" = @NrFatures
                                   LIMIT 1;";

            using var conn = Db.GetConnection();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NrFatures", nrFatures);

            using var da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public DataTable GetSaleLinesForReturn(int shitjaId)
        {
            const string query = @"SELECT sd.""Id"" as ""ShitjaDetaliId"",
                                   sd.""ArtikulliId"",
                                   a.""Barkodi"",
                                   a.""Emri"",
                                   sd.""Sasia"" as ""SasiaShitur"",
                                   COALESCE(( SELECT SUM(rd.""Sasia"")
                                              FROM ""ReturnDetails"" rd
                                              WHERE rd.""ShitjaDetaliId"" = sd.""Id""), 0) AS ""SasiaKthyer"",
                                   sd.""Sasia"" - COALESCE(( SELECT SUM( rd.""Sasia"")
                                                              FROM ""ReturnDetails"" rd
                                                              WHERE rd.""ShitjaDetaliId"" = sd.""Id""),0) AS ""SasiaLejuarPerKthim"",
                                   sd.""Cmimi"",
                                   0::numeric(18,2) AS ""SasiaPerKthim"",
                                   0::numeric(18,2) AS ""VleraKthimit""
                                   FROM ""ShitjetDetale"" sd
                                   INNER JOIN ""Artikujt"" a ON  a.""Id"" = sd.""ArtikulliId""
                                   WHERE sd.""ShitjaId"" = @ShitjaId
                                   ORDER BY sd.""Id"";";
            using var conn = Db.GetConnection();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ShitjaId", shitjaId);
            using var da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int InsertReturn(ReturnModel model, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"INSERT INTO ""Returns""(""ShitjaId"", ""NumriKthimit"", ""TotaliKthimit"", ""Arsyeja"", ""PerdoruesiId"", ""ShiftId"", ""Created_At"")
                                   VALUES (@ShitjaId, @NumriKthimit, @TotaliKthimit, @Arsyeja, @PerdoruesiId, @ShiftId, CURRENT_TIMESTAMP)
                                   RETURNING ""Id"";";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ShitjaId", model.ShitjaId);
            cmd.Parameters.AddWithValue("@NumriKthimit", model.NumriKthimit);
            cmd.Parameters.AddWithValue("@TotaliKthimit", model.TotaliKthimit);
            cmd.Parameters.AddWithValue("@Arsyeja", (object?)model.Arsyeja ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PerdoruesiId", model.PerdoruesiId);
            cmd.Parameters.AddWithValue("@ShiftId", model.ShiftId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void InsertReturnDetail(ReturnDetailModel detail, int returnId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"INSERT INTO ""ReturnDetails""(""ReturnId"", ""ShitjaDetaliId"", ""ArtikulliId"", ""Sasia"",""Cmimi"", ""Vlera"")
                                   VALUES(@ReturnId, @ShitjaDetaliId , @ArtikulliId, @Sasia, @Cmimi, @Vlera);";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ReturnId", returnId);
            cmd.Parameters.AddWithValue("@ShitjaDetaliId", detail.ShitjaDetaliId);
            cmd.Parameters.AddWithValue("@ArtikulliId", detail.ArtikulliId);
            cmd.Parameters.AddWithValue("@Sasia", detail.Sasia);
            cmd.Parameters.AddWithValue("@Cmimi", detail.Cmimi);
            cmd.Parameters.AddWithValue("@Vlera", detail.Vlera);
            cmd.ExecuteNonQuery();
        }

        public void UpdateStockForReturn(int artikulliId, decimal sasia, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"UPDATE ""Artikujt""
                                   SET ""SasiaNeStok"" = ""SasiaNeStok"" + @Sasia
                                   WHERE ""Id"" = @ArtikulliId;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Sasia", sasia);
            cmd.Parameters.AddWithValue("@ArtikulliId", artikulliId);
            cmd.ExecuteNonQuery();
        }

        public void InsertRefundPayment(RefundPaymentModel payment, int returnId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"INSERT INTO ""RefundPayments""(""ReturnId"", ""MenyraPagesesId"", ""Shuma"", ""ReferenceNr"", ""PerdoruesiId"", ""Created_At"")
                                   VALUES(@ReturnId, @MenyraPagesesId, @Shuma, @ReferenceNr, @PerdoruesiId, CURRENT_TIMESTAMP);";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ReturnId", returnId);
            cmd.Parameters.AddWithValue("@MenyraPagesesId", payment.MenyraPagesesId);
            cmd.Parameters.AddWithValue("@Shuma", payment.Shuma);
            cmd.Parameters.AddWithValue("@ReferenceNr", (object ?)payment.ReferenceNr ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PerdoruesiId", Session.UserId);
            cmd.ExecuteNonQuery();

        }

        public decimal GetSoldQuantity(int shitjaDetaliId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"SELECT ""Sasia"" FROM ""ShitjetDetale""
                                   WHERE ""Id"" = @Id;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Id", shitjaDetaliId);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public decimal GetAlreadyReturnedQuantity(int shitjaDetaliId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"SELECT COALESCE(SUM(""Sasia""), 0)
                                   FROM ""ReturnDetails""
                                   WHERE ""ShitjaDetaliId"" = @ShitjaDetaliId;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ShitjaDetaliId", shitjaDetaliId);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public string GetPaymentMethodType(int menyraPagesesId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"SELECT ""Tipi""
                           FROM ""MenyratPageses""
                           WHERE ""Id"" = @Id;";

            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Id", menyraPagesesId);
            return Convert.ToString(cmd.ExecuteScalar()) ?? "";
        }

        public void InsertCashMovement(int shiftId, string tipi, decimal shuma, string arsyeja, string koment, int perdoruesiId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"
                                    INSERT INTO ""CashMovements""
                                    (""ShiftId"", ""Tipi"", ""Shuma"", ""Arsyeja"", ""Koment"", ""PerdoruesiId"", ""Created_At"")
                                    VALUES
                                    (@ShiftId, @Tipi, @Shuma, @Arsyeja, @Koment, @PerdoruesiId, CURRENT_TIMESTAMP);";

            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ShiftId", shiftId);
            cmd.Parameters.AddWithValue("@Tipi", tipi);
            cmd.Parameters.AddWithValue("@Shuma", shuma);
            cmd.Parameters.AddWithValue("@Arsyeja", arsyeja);
            cmd.Parameters.AddWithValue("@Koment", (object?)koment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PerdoruesiId", perdoruesiId);
            cmd.ExecuteNonQuery();
        }
    }
}
