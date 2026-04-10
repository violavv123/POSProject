using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.sales
{
    public class SaleRepository : ISaleRepository
    {
        public int InsertSale(SaleModel sale, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"INSERT INTO ""Shitjet""(""NrFatures"", ""DataShitjes"", ""Totali"", ""Koment"", ""perdoruesi_id"", ""SubjektiId"",
                             ""ShiftId"", ""TotaliPaZbritje"", ""ZbritjaTotale"", ""TotaliFinal"")
                             VALUES (@NrFatures, @DataShitjes, @Totali, @Koment, @PerdoruesiId, @SubjektiId, @ShiftId, @TotaliPaZbritje,
                             @ZbritjaTotale, @TotaliFinal)
                             RETURNING ""Id"";";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@NrFatures", sale.NrFatures);
            cmd.Parameters.AddWithValue("@DataShitjes", sale.DataShitjes);
            cmd.Parameters.AddWithValue("@Totali", sale.Totali);
            cmd.Parameters.AddWithValue("@Koment", string.IsNullOrWhiteSpace(sale.Koment) ? (object)DBNull.Value : sale.Koment);
            cmd.Parameters.AddWithValue("@PerdoruesiId", sale.PerdoruesiId);
            cmd.Parameters.AddWithValue("@SubjektiId", sale.SubjektiId.HasValue ? sale.SubjektiId.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ShiftId", sale.ShiftId);
            cmd.Parameters.AddWithValue("@TotaliPaZbritje", sale.TotaliPaZbritje);
            cmd.Parameters.AddWithValue("@ZbritjaTotale", sale.ZbritjaTotale);
            cmd.Parameters.AddWithValue("@TotaliFinal", sale.TotaliFinal);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void InsertSaleDetail(SaleDetailModel saleDetail, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"INSERT INTO ""ShitjetDetale""(""ShitjaId"",""ArtikulliId"",""Sasia"",""Cmimi"",""Vlera"",""Zbritja"",""CmimiFinal"")
                             VALUES (@ShitjaId, @ArtikulliId, @Sasia, @Cmimi, @Vlera, @Zbritja, @CmimiFinal);";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@ShitjaId", saleDetail.ShitjaId);
            cmd.Parameters.AddWithValue("@ArtikulliId", saleDetail.ArtikulliId);
            cmd.Parameters.AddWithValue("@Sasia", saleDetail.Sasia);
            cmd.Parameters.AddWithValue("@Cmimi", saleDetail.Cmimi);
            cmd.Parameters.AddWithValue("@Vlera", saleDetail.Vlera);
            cmd.Parameters.AddWithValue("@Zbritja", saleDetail.Zbritja);
            cmd.Parameters.AddWithValue("@CmimiFinal", saleDetail.CmimiFinal);
            cmd.ExecuteNonQuery();
        }
    }
}
