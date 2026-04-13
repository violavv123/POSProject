using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.products
{
    public class GiftCardRepository : IGiftCardRepository
    {
        public GiftCardModel GetByCode(string kodi)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            return GetByCodeInternal(kodi, conn, null);
        }

        public GiftCardModel GetByCode(string kodi, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            return GetByCodeInternal(kodi, conn, tx);
        }

        private GiftCardModel GetByCodeInternal(string kodi, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"SELECT ""Id"",""Kodi"",""Barkodi"",""ShumaFillestare"",""BilanciAktual"",""Status"",""ShitjaIdIssued"",""Activated_At"",
                                   ""Expires_At"",""Created_At"",""Created_By""
                                   FROM ""GiftCard""
                                   WHERE UPPER(""Kodi"") = UPPER(@Kodi)
                                   LIMIT 1;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Kodi", kodi?.Trim().ToUpper() ?? "");
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new GiftCardModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Kodi = reader["Kodi"].ToString(),
                Barkodi = reader["Barkodi"] == DBNull.Value ? null : reader["Barkodi"].ToString(),
                ShumaFillestare = Convert.ToDecimal(reader["ShumaFillestare"]),
                BilanciAktual = Convert.ToDecimal(reader["BilanciAktual"]),
                Statusi = reader["Status"].ToString(),
                ShitjaIdIssued = reader["ShitjaId"] == DBNull.Value ? null : Convert.ToInt32(reader["ShitjaId"]),
                Activated_At = reader["Activated_At"] == DBNull.Value ? null : Convert.ToDateTime(reader["Activated_At"]),
                Expires_At = reader["Expires_At"] == DBNull.Value ? null : Convert.ToDateTime(reader["Expires_At"]),
                Created_At = Convert.ToDateTime(reader["Created_At"]),
                Created_By = reader["Created_By"] == DBNull.Value ? null : Convert.ToInt32(reader["Created_By"])
            };
        }
        public int InsertGiftCard(GiftCardModel card, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"INSERT INTO ""GiftCard""(""Kodi"",""Barkodi"", ""ShumaFillestare"", ""BilanciAktual"", ""Status"", ""ShitjaIdIssued"",
                                   ""Activated_At"", ""Expires_At"", ""Created_At"", ""Created_By"")
                                   VALUES(@Kodi, @Barkodi, @ShumaFillestare, @BilanciAktual, @Statusi, @ShitjaIdIssued, @Activated_At, @Expires_At, @Created_At, @Created_By)
                                   RETURNING ""Id"";";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Kodi", card.Kodi);
            cmd.Parameters.AddWithValue("@Barkodi", (object?)card.Barkodi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ShumaFillestare", card.ShumaFillestare);
            cmd.Parameters.AddWithValue("@BilanciAktual", card.BilanciAktual);
            cmd.Parameters.AddWithValue("@Statusi", card.Statusi);
            cmd.Parameters.AddWithValue("@ShitjaIdIssued", (object?)card.ShitjaIdIssued ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Activated_At", (object?)card.Activated_At ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Expires_At", (object?)card.Expires_At ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Created_At", card.Created_At);
            cmd.Parameters.AddWithValue("@Created_By", (object?)card.Created_By ?? DBNull.Value);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void UpdateBalance(int giftCardId, decimal newBalance, string newStatus, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"UPDATE ""GiftCard""
                                   SET ""BilanciAktual"" = @Bilanci
                                       ""Status"" = @Statusi
                                   WHERE ""Id"" = @Id;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@Bilanci", newBalance);
            cmd.Parameters.AddWithValue("@Statusi", newStatus);
            cmd.Parameters.AddWithValue("@Id", giftCardId);
            cmd.ExecuteNonQuery();
        }

        public int InsertTransaction(GiftCardTransactionModel transaction, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string query = @"INSERT INTO ""GiftCardTransaction""(""GiftCardId"", ""Type"", ""Shuma"", ""ShitjaId"", ""EkzekutimiPagesesId"",
                                   ""Koment"", ""Created_At"", ""Created_By"")
                                   VALUES(@GiftCardId, @Type, @Shuma, @ShitjaId, @EkzekutimiPagesesId, @Koment, @Created_At, @Created_By)
                                   RETURNING ""Id"";";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@GiftCardId", transaction.GiftCardId);
            cmd.Parameters.AddWithValue("@Type", transaction.Type);
            cmd.Parameters.AddWithValue("@Shuma", transaction.Shuma);
            cmd.Parameters.AddWithValue("@ShitjaId", (object?)transaction.ShitjaId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EkzekutimiPagesesId", (object?)transaction.EkzekutimiPagesesId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Koment", (object?)transaction.Created_At ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Created_At", transaction.Created_At);
            cmd.Parameters.AddWithValue("@Created_By", (object?)transaction.Created_By ?? DBNull.Value);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool ExistsByCode(string kodi)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            const string query = @"SELECT COUNT(1) FROM ""GiftCard"" WHERE UPPER(""Kodi"") = UPPER(@Kodi);";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Kodi", kodi?.Trim() ?? "");
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public string GenerateUniqueCode()
        {
            string kodi;
            do
            {
                kodi = $"GC-{DateTime.Now:yyyyMMddHHmmss}-{Random.Shared.Next(100, 999)}";
            }
            while (ExistsByCode(kodi));
            return kodi;
        }

        public List<GiftCardTransactionModel> GetTransactionsByGiftCardId(int giftCardId)
        {
            List<GiftCardTransactionModel> list = new List<GiftCardTransactionModel>();

            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"
                            SELECT 
                                ""Id"",
                                ""GiftCardId"",
                                ""Tipi"",
                                ""Shuma"",
                                ""ShitjaId"",
                                ""EkzekutimiPagesesId"",
                                ""Koment"",
                                ""Created_At"",
                                ""Created_By""
                            FROM ""GiftCardTransaction""
                            WHERE ""GiftCardId"" = @GiftCardId
                            ORDER BY ""Created_At"" DESC;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@GiftCardId", giftCardId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new GiftCardTransactionModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    GiftCardId = Convert.ToInt32(reader["GiftCardId"]),
                    Type = reader["Tipi"].ToString(),
                    Shuma = Convert.ToDecimal(reader["Shuma"]),
                    ShitjaId = reader["ShitjaId"] == DBNull.Value ? null : Convert.ToInt32(reader["ShitjaId"]),
                    EkzekutimiPagesesId = reader["EkzekutimiPagesesId"] == DBNull.Value ? null : Convert.ToInt32(reader["EkzekutimiPagesesId"]),
                    Koment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString(),
                    Created_At = Convert.ToDateTime(reader["Created_At"]),
                    Created_By = reader["Created_By"] == DBNull.Value ? null : Convert.ToInt32(reader["Created_By"])
                });
            }

            return list;
        }
    }
}
