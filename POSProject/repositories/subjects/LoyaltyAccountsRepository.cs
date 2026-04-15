using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public class LoyaltyAccountsRepository : ILoyaltyAccountsRepository
    {
        public LoyaltyAccountsModel GetById(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""SubjektiId"", ""LoyaltyCardNr"", ""PiketAktuale"",
                                        ""PiketTotaleFituara"", ""PiketTotaleShfrytezuara"",
                                        ""Aktiv"", ""Created_At"", ""Updated_At""
                                 FROM ""LoyaltyAccounts""
                                 WHERE ""Id"" = @Id;";

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

        public LoyaltyAccountsModel GetBySubjektiId(int subjektiId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""SubjektiId"", ""LoyaltyCardNr"", ""PiketAktuale"",
                                        ""PiketTotaleFituara"", ""PiketTotaleShfrytezuara"",
                                        ""Aktiv"", ""Created_At"", ""Updated_At""
                                 FROM ""LoyaltyAccounts""
                                 WHERE ""SubjektiId"" = @SubjektiId
                                 LIMIT 1;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SubjektiId", subjektiId);

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

        public LoyaltyAccountsModel GetByCardNumber(string cardNr)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""SubjektiId"", ""LoyaltyCardNr"", ""PiketAktuale"",
                                        ""PiketTotaleFituara"", ""PiketTotaleShfrytezuara"",
                                        ""Aktiv"", ""Created_At"", ""Updated_At""
                                 FROM ""LoyaltyAccounts""
                                 WHERE ""LoyaltyCardNr"" = @LoyaltyCardNr
                                 LIMIT 1;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoyaltyCardNr", cardNr);

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

        public List<LoyaltyAccountsModel> GetAll()
        {
            List<LoyaltyAccountsModel> list = new List<LoyaltyAccountsModel>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""SubjektiId"", ""LoyaltyCardNr"", ""PiketAktuale"",
                                        ""PiketTotaleFituara"", ""PiketTotaleShfrytezuara"",
                                        ""Aktiv"", ""Created_At"", ""Updated_At""
                                 FROM ""LoyaltyAccounts""
                                 ORDER BY ""Id"" DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToModel(reader));
                    }
                }
            }

            return list;
        }
        public int Insert(LoyaltyAccountsModel model, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"INSERT INTO ""LoyaltyAccounts""
                            (""SubjektiId"", ""LoyaltyCardNr"", ""PiketAktuale"",
                             ""PiketTotaleFituara"", ""PiketTotaleShfrytezuara"",
                             ""Aktiv"", ""Created_At"", ""Updated_At"")
                             VALUES
                            (@SubjektiId, @LoyaltyCardNr, @PiketAktuale,
                             @PiketTotaleFituara, @PiketTotaleShfrytezuara,
                             @Aktiv, @CreatedAt, @UpdatedAt)
                             RETURNING ""Id"";";

            using (var cmd = new NpgsqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@SubjektiId", model.SubjektiId);
                cmd.Parameters.AddWithValue("@LoyaltyCardNr", model.LoyaltyCardNr);
                cmd.Parameters.AddWithValue("@PiketAktuale", model.PiketAktuale);
                cmd.Parameters.AddWithValue("@PiketTotaleFitura", model.PiketTotaleFituara);
                cmd.Parameters.AddWithValue("@PiketTotaleShfrytezuara", model.PiketTotaleShfrytezuara);
                cmd.Parameters.AddWithValue("@Aktiv", model.Aktiv);
                cmd.Parameters.AddWithValue("@CreatedAt", model.Created_At);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.Updated_At ?? DBNull.Value);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void UpdatePoints(int loyaltyAccountId, decimal piketAktuale, decimal piketFituara, decimal piketShfrytezuara, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"UPDATE ""LoyaltyAccounts""
                             SET ""PiketAktuale"" = @PiketAktuale,
                                 ""PiketTotaleFituara"" = @PiketTotaleFituara,
                                 ""PiketTotaleShfrytezuara"" = @PiketTotaleShfrytezuara,
                                 ""Updated_At"" = @UpdatedAt
                             WHERE ""Id"" = @Id;";

            using (var cmd = new NpgsqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Id", loyaltyAccountId);
                cmd.Parameters.AddWithValue("@PiketAktuale", piketAktuale);
                cmd.Parameters.AddWithValue("@PiketTotaleFituara", piketFituara);
                cmd.Parameters.AddWithValue("@PiketTotaleShfrytezuara", piketShfrytezuara);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(LoyaltyAccountsModel model, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"UPDATE ""LoyaltyAccounts""
                             SET ""SubjektiId"" = @SubjektiId,
                                 ""LoyaltyCardNr"" = @LoyaltyCardNr,
                                 ""PiketAktuale"" = @PiketAktuale,
                                 ""PiketTotaleFituara"" = @PiketTotaleFituara,
                                 ""PiketTotaleShfrytezuara"" = @PiketTotaleShfrytezuara,
                                 ""Aktiv"" = @Aktiv,
                                 ""Updated_At"" = @UpdatedAt
                             WHERE ""Id"" = @Id;";

            using (var cmd = new NpgsqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@SubjektiId", model.SubjektiId);
                cmd.Parameters.AddWithValue("@LoyaltyCardNr", model.LoyaltyCardNr);
                cmd.Parameters.AddWithValue("@PiketAktuale", model.PiketAktuale);
                cmd.Parameters.AddWithValue("@PiketTotaleFituara", model.PiketTotaleFituara);
                cmd.Parameters.AddWithValue("@PiketTotaleShfrytezuara", model.PiketTotaleShfrytezuara);
                cmd.Parameters.AddWithValue("@Aktiv", model.Aktiv);
                cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.Updated_At ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }
        public void SetActiveStatus(int loyaltyAccountId, bool aktiv, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"UPDATE ""LoyaltyAccounts""
                             SET ""Aktiv"" = @Aktiv,
                                 ""Updated_At"" = @UpdatedAt
                             WHERE ""Id"" = @Id;";

            using (var cmd = new NpgsqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@Id", loyaltyAccountId);
                cmd.Parameters.AddWithValue("@Aktiv", aktiv);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
        private LoyaltyAccountsModel MapReaderToModel(NpgsqlDataReader reader)
        {
            return new LoyaltyAccountsModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                SubjektiId = Convert.ToInt32(reader["SubjektiId"]),
                LoyaltyCardNr = reader["LoyaltyCardNr"].ToString(),
                PiketAktuale = Convert.ToDecimal(reader["PiketAktuale"]),
                PiketTotaleFituara = Convert.ToDecimal(reader["PiketTotaleFituara"]),
                PiketTotaleShfrytezuara = Convert.ToDecimal(reader["PiketTotaleShfrytezuara"]),
                Aktiv = Convert.ToBoolean(reader["Aktiv"]),
                Created_At = Convert.ToDateTime(reader["Created_At"]),
                Updated_At = reader["Updated_At"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["Updated_At"])
            };
        }
    }
}
