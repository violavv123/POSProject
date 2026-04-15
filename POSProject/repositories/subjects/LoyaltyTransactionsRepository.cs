using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public class LoyaltyTransactionsRepository : ILoyaltyTransactionsRepository
    {
        public void Insert(LoyaltyTransactionModel model, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"INSERT INTO ""LoyaltyTransactions""
                            (""LoyaltyAccountId"", ""ShitjaId"", ""Tipi"", ""Piket"",
                             ""VleraEuro"", ""Pershkrimi"", ""ReferenceNr"",
                             ""PerdoruesiId"", ""Created_At"")
                             VALUES
                            (@LoyaltyAccountId, @ShitjaId, @Tipi, @Piket,
                             @VleraEuro, @Pershkrimi, @ReferenceNr,
                             @PerdoruesiId, @CreatedAt);";

            using (var cmd = new NpgsqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@LoyaltyAccountId", model.LoyaltyAccountId);
                cmd.Parameters.AddWithValue("@ShitjaId", (object?)model.ShitjaId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipi", model.Tipi);
                cmd.Parameters.AddWithValue("@Piket", model.Piket);
                cmd.Parameters.AddWithValue("@VleraEuro", (object?)model.VleraEuro ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pershkrimi", (object?)model.Pershkrimi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReferenceNr", (object?)model.ReferenceNr ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PerdoruesiId", model.PerdoruesiId);
                cmd.Parameters.AddWithValue("@CreatedAt", model.Created_At);

                cmd.ExecuteNonQuery();
            }
        }

        public List<LoyaltyTransactionModel> GetByAccountId(int loyaltyAccountId)
        {
            List<LoyaltyTransactionModel> list = new List<LoyaltyTransactionModel>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""LoyaltyAccountId"", ""ShitjaId"", ""Tipi"", ""Piket"",
                                        ""VleraEuro"", ""Pershkrimi"", ""ReferenceNr"",
                                        ""PerdoruesiId"", ""Created_At""
                                 FROM ""LoyaltyTransactions""
                                 WHERE ""LoyaltyAccountId"" = @LoyaltyAccountId
                                 ORDER BY ""Created_At"" DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoyaltyAccountId", loyaltyAccountId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToModel(reader));
                        }
                    }
                }
            }

            return list;
        }

        public List<LoyaltyTransactionModel> GetBySaleId(int shitjaId)
        {
            List<LoyaltyTransactionModel> list = new List<LoyaltyTransactionModel>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""LoyaltyAccountId"", ""ShitjaId"", ""Tipi"", ""Piket"",
                                        ""VleraEuro"", ""Pershkrimi"", ""ReferenceNr"",
                                        ""PerdoruesiId"", ""Created_At""
                                 FROM ""LoyaltyTransactions""
                                 WHERE ""ShitjaId"" = @ShitjaId
                                 ORDER BY ""Created_At"" DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ShitjaId", shitjaId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToModel(reader));
                        }
                    }
                }
            }

            return list;
        }

        public LoyaltyTransactionModel GetById(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""LoyaltyAccountId"", ""ShitjaId"", ""Tipi"", ""Piket"",
                                        ""VleraEuro"", ""Pershkrimi"", ""ReferenceNr"",
                                        ""PerdoruesiId"", ""Created_At""
                                 FROM ""LoyaltyTransactions""
                                 WHERE ""Id"" = @Id
                                 LIMIT 1;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToModel(reader);
                        }
                    }
                }
            }

            return null;
        }

        private LoyaltyTransactionModel MapReaderToModel(NpgsqlDataReader reader)
        {
            return new LoyaltyTransactionModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                LoyaltyAccountId = Convert.ToInt32(reader["LoyaltyAccountId"]),
                ShitjaId = reader["ShitjaId"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["ShitjaId"]),
                Tipi = reader["Tipi"].ToString(),
                Piket = Convert.ToDecimal(reader["Piket"]),
                VleraEuro = reader["VleraEuro"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["VleraEuro"]),
                Pershkrimi = reader["Pershkrimi"] == DBNull.Value
                    ? null
                    : reader["Pershkrimi"].ToString(),
                ReferenceNr = reader["ReferenceNr"] == DBNull.Value
                    ? null
                    : reader["ReferenceNr"].ToString(),
                PerdoruesiId = Convert.ToInt32(reader["PerdoruesiId"]),
                Created_At = Convert.ToDateTime(reader["Created_At"])
            };
        }
    }
}
