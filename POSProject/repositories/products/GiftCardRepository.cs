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
                ShitjaIdIssued = reader["ShitjaIdIssued"] == DBNull.Value ? null : Convert.ToInt32(reader["ShitjaIdIssued"]),
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
                                   SET ""BilanciAktual"" = @Bilanci,
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
            cmd.Parameters.AddWithValue("@Koment", (object?)transaction.Koment ?? DBNull.Value);
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
                                ""Type"",
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
                    Type = reader["Type"].ToString(),
                    Shuma = Convert.ToDecimal(reader["Shuma"]),
                    ShitjaId = reader["ShitjaId"] == DBNull.Value ? null : Convert.ToInt32(reader["ShitjaId"]),
                    EkzekutimiPagesesId = reader["EkzekutimiPagesesId"] == DBNull.Value ? null : Convert.ToInt32(reader["EkzekutimiPagesesId"]),
                    Koment = reader["Koment"] == DBNull.Value ? null : reader["Koment"].ToString(),
                    Created_At = Convert.ToDateTime(reader["Created_At"]),
                    Created_By = reader["Created_By"] == DBNull.Value ? null : Convert.ToInt32(reader["Created_By"])
                });
            }

            return list;
        }

        public List<GiftCardModel> GetAll(bool onlyAvailable = false)
        {
            List<GiftCardModel> list = new List<GiftCardModel>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT 
                        ""Id"",
                        ""Kodi"",
                        ""Barkodi"",
                        ""ShumaFillestare"",
                        ""BilanciAktual"",
                        ""Status"",
                        ""ShitjaIdIssued"",
                        ""Activated_At"",
                        ""Expires_At"",
                        ""Created_At"",
                        ""Created_By""
                    FROM ""GiftCard""
                ";

                if (onlyAvailable)
                {
                    query += @" 
                        WHERE UPPER(TRIM(""Status"")) = 'AKTIV'
                          AND COALESCE(""BilanciAktual"", 0) > 0
                          AND (""Expires_At"" IS NULL OR ""Expires_At"" >= NOW())
                    ";
                }

                query += @" ORDER BY ""Created_At"" DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new GiftCardModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Kodi = reader["Kodi"]?.ToString(),
                            Barkodi = reader["Barkodi"]?.ToString(),
                            ShumaFillestare = Convert.ToDecimal(reader["ShumaFillestare"]),
                            BilanciAktual = Convert.ToDecimal(reader["BilanciAktual"]),
                            Statusi = reader["Status"]?.ToString(),
                            ShitjaIdIssued = reader["ShitjaIdIssued"] == DBNull.Value ? null : Convert.ToInt32(reader["ShitjaIdIssued"]),
                            Activated_At = reader["Activated_At"] == DBNull.Value ? null : Convert.ToDateTime(reader["Activated_At"]),
                            Expires_At = reader["Expires_At"] == DBNull.Value ? null : Convert.ToDateTime(reader["Expires_At"]),
                            Created_At = reader["Created_At"] == DBNull.Value ? null : Convert.ToDateTime(reader["Created_At"]),
                            Created_By = reader["Created_By"] == DBNull.Value ? null : Convert.ToInt32(reader["Created_By"])
                        });
                    }
                }
            }

            return list;
        }

        public void UseGiftCard(string code, decimal amount, int shitjaId, int ekzekutimiPagesesId, int userId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("Kodi i gift card mungon.");

            if (amount <= 0)
                throw new Exception("Shuma për përdorim të gift card duhet të jetë më e madhe se zero.");

            string getQuery = @"
                SELECT ""Id"", ""BilanciAktual"", ""Status"", ""Expires_At""
                FROM ""GiftCard""
                WHERE ""Kodi"" = @Kodi
                LIMIT 1;";

            int giftCardId;
            decimal bilanciAktual;
            string statusi;
            DateTime? expiresAt;

            using (var cmd = new NpgsqlCommand(getQuery, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Kodi", code.Trim());

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new Exception("Gift card nuk u gjet.");

                    giftCardId = Convert.ToInt32(reader["Id"]);
                    bilanciAktual = Convert.ToDecimal(reader["BilanciAktual"]);
                    statusi = reader["Status"]?.ToString() ?? "";
                    expiresAt = reader["Expires_At"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["Expires_At"]);
                }
            }

            if (!statusi.Trim().Equals("Aktiv", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Gift card nuk është aktive.");

            if (expiresAt.HasValue && expiresAt.Value < DateTime.Now)
                throw new Exception("Gift card ka skaduar.");

            if (bilanciAktual < amount)
                throw new Exception($"Bilanci i gift card është i pamjaftueshëm. Bilanci aktual: {bilanciAktual:0.00}");

            decimal bilanciRi = bilanciAktual - amount;
            string statusiRi = bilanciRi <= 0 ? "Shfrytezuar" : statusi;

            string updateQuery = @"
                UPDATE ""GiftCard""
                SET ""BilanciAktual"" = @BilanciRi,
                    ""Status"" = @StatusiRi
                WHERE ""Id"" = @Id;";

            using (var cmd = new NpgsqlCommand(updateQuery, conn, tx))
            {
                cmd.Parameters.AddWithValue("@BilanciRi", bilanciRi);
                cmd.Parameters.AddWithValue("@StatusiRi", statusiRi);
                cmd.Parameters.AddWithValue("@Id", giftCardId);
                cmd.ExecuteNonQuery();
            }

            string insertTransactionQuery = @"
                INSERT INTO ""GiftCardTransaction""
                (""GiftCardId"", ""Type"", ""Shuma"", ""ShitjaId"", ""EkzekutimiPagesesId"", ""Koment"", ""Created_At"", ""Created_By"")
                VALUES
                (@GiftCardId, @Tipi, @Shuma, @ShitjaId, @EkzekutimiPagesesId, @Koment, @CreatedAt, @CreatedBy);";

            using (var cmd = new NpgsqlCommand(insertTransactionQuery, conn, tx))
            {
                cmd.Parameters.AddWithValue("@GiftCardId", giftCardId);
                cmd.Parameters.AddWithValue("@Tipi", "PERDORIM");
                cmd.Parameters.AddWithValue("@Shuma", amount);
                cmd.Parameters.AddWithValue("@ShitjaId", shitjaId);
                cmd.Parameters.AddWithValue("@EkzekutimiPagesesId", ekzekutimiPagesesId);
                cmd.Parameters.AddWithValue("@Koment", $"Përdorur në shitje #{shitjaId}");
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", userId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
