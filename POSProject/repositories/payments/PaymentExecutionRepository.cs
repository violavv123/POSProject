using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject.repositories.payments
{
    public class PaymentExecutionRepository : IPaymentExecutionRepository
    {
        public int InsertPaymentExecution(PaymentExecutionModel payment, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"INSERT INTO ""EkzekutimiPageses"" (""ShitjaId"", ""MenyraPagesesId"", ""ShumaPaguar"", ""PaguarMe"", ""CashBack"", ""Valuta"",
                             ""KursiKembimit"", ""ShumaNeValuteBaze"", ""ReferenceNr"", ""Statusi"", ""Koment"", ""PerdoruesiId"", ""Created_At"", ""ShiftId"")
                             VALUES(@ShitjaId, @MenyraPagesesId, @ShumaPaguar, @PaguarMe, @CashBack, @Valuta, @KursiKembimit, @ShumaNeValuteBaze, @ReferenceNr,
                             @Statusi, @Koment, @PerdoruesiId, @Created_At, @ShiftId) RETURNING ""Id"";";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ShitjaId", payment.ShitjaId);
            cmd.Parameters.AddWithValue("@MenyraPagesesId", payment.MenyraPagesesId);
            cmd.Parameters.AddWithValue("@ShumaPaguar", payment.ShumaPaguar);
            cmd.Parameters.AddWithValue("@PaguarMe", payment.PaguarMe);
            cmd.Parameters.AddWithValue("@CashBack", payment.CashBack);
            cmd.Parameters.AddWithValue("@Valuta", payment.Valuta ?? "EUR");
            cmd.Parameters.AddWithValue("@KursiKembimit", payment.KursiKembimit);
            cmd.Parameters.AddWithValue("@ShumaNeValuteBaze", payment.ShumaNeValuteBaze);
            cmd.Parameters.AddWithValue("@ReferenceNr", string.IsNullOrWhiteSpace(payment.ReferenceNr) ? (object)DBNull.Value : payment.ReferenceNr);
            cmd.Parameters.AddWithValue("@Statusi", payment.Statusi ?? "Kompletuar");
            cmd.Parameters.AddWithValue("@Koment", string.IsNullOrWhiteSpace(payment.Koment) ? (object)DBNull.Value : payment.Koment);
            cmd.Parameters.AddWithValue("@PerdoruesiId", payment.PerdoruesiId);
            cmd.Parameters.AddWithValue("@Created_At", payment.CreatedAt);
            cmd.Parameters.AddWithValue("@ShiftId", payment.ShiftId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
