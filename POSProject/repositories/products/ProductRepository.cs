using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.products
{
    public class ProductRepository : IProductRepository
    {
        public ProductModel GetByBarcode(string barkodi)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""Emri"", ""KategoriaId"", ""CmimiShitjes"", ""Barkodi"",""SasiaNeStok"", ""MinimumStock""
                                 FROM ""Artikujt""
                                 WHERE ""Barkodi"" = @barkodi AND ""Aktiv"" = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@barkodi", barkodi);

                    using var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                        return null;

                    return new ProductModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Emri = reader["Emri"].ToString(),
                        Barkodi = reader["Barkodi"].ToString(),
                        KategoriaId = Convert.ToInt32(reader["KategoriaId"]),
                        CmimiShitjes = Convert.ToDecimal(reader["CmimiShitjes"]),
                        SasiaNeStok = Convert.ToDecimal(reader["SasiaNeStok"]),
                        MinimumStok = Convert.ToDecimal(reader["MinimumStock"]),
                        Aktiv = true
                    };
                }
            }

            return null;
        }

        public ProductModel GetById(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Id"", ""Emri"", ""KategoriaId"", ""CmimiShitjes"", ""Barkodi"", ""SasiaNeStok"", ""MinimumStock""
                         FROM ""Artikujt""
                         WHERE ""Id"" = @id AND ""Aktiv"" = TRUE";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                        return null;

                    return new ProductModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Emri = reader["Emri"].ToString(),
                        Barkodi = reader["Barkodi"].ToString(),
                        KategoriaId = Convert.ToInt32(reader["KategoriaId"]),
                        CmimiShitjes = Convert.ToDecimal(reader["CmimiShitjes"]),
                        SasiaNeStok = Convert.ToDecimal(reader["SasiaNeStok"]),
                        MinimumStok = Convert.ToDecimal(reader["MinimumStock"]),
                        Aktiv = true
                    };
                }
            }

            return null;
        }

        public decimal GetStockById(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT ""SasiaNeStok""
                             FROM ""Artikujt"" 
                             WHERE ""Id"" = @id;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        public void UpdateStock(int artikulliId, decimal sasia, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            string query = @"UPDATE ""Artikujt""
                             SET ""SasiaNeStok"" = ""SasiaNeStok"" - @sasia
                             WHERE ""Id"" = @id;";
            using var cmd = new NpgsqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@sasia", sasia);
            cmd.Parameters.AddWithValue("@id", artikulliId);
            cmd.ExecuteNonQuery();
        }
        
        public List<ProductModel> GetAllForManagement()
        {
            var list = new List<ProductModel>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT
                        a.""Id"",
                        a.""Barkodi"",
                        a.""Emri"",
                        a.""KategoriaId"",
                        k.""Emri"" AS ""Kategoria"",
                        a.""CmimiShitjes"",
                        a.""SasiaNeStok"",
                        a.""Aktiv""
                    FROM ""Artikujt"" a
                    LEFT JOIN ""kategorite"" k ON a.""KategoriaId"" = k.""id""
                    ORDER BY a.""Emri"";";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProductModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Barkodi = reader["Barkodi"].ToString(),
                            Emri = reader["Emri"].ToString(),
                            KategoriaId = Convert.ToInt32(reader["KategoriaId"]),
                            Kategoria = reader["Kategoria"] == DBNull.Value ? null : reader["Kategoria"].ToString(),
                            CmimiShitjes = Convert.ToDecimal(reader["CmimiShitjes"]),
                            SasiaNeStok = Convert.ToDecimal(reader["SasiaNeStok"]),
                            Aktiv = Convert.ToBoolean(reader["Aktiv"])
                        });
                    }
                }
            }

            return list;
        }

        public List<string> GetProductNames()
        {
            var list = new List<string>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"SELECT ""Emri"" FROM ""Artikujt"" ORDER BY ""Emri"";";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["Emri"].ToString());
                    }
                }
            }

            return list;
        }

        public bool ExistsByBarcodeOrName(string barcode, string emri)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM ""Artikujt""
                    WHERE ""Barkodi"" = @Barkodi
                       OR LOWER(""Emri"") = LOWER(@Emri);";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Barkodi", barcode);
                    cmd.Parameters.AddWithValue("@Emri", emri);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"DELETE FROM ""Artikujt"" WHERE ""Id"" = @Id;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(ProductModel model)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE ""Artikujt""
                    SET
                        ""Barkodi"" = @Barkodi,
                        ""Emri"" = @Emri,
                        ""KategoriaId"" = @KategoriaId,
                        ""CmimiShitjes"" = @CmimiShitjes,
                        ""SasiaNeStok"" = @SasiaNeStok,
                        ""Aktiv"" = @Aktiv
                    WHERE ""Id"" = @Id;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Barkodi", model.Barkodi);
                    cmd.Parameters.AddWithValue("@Emri", model.Emri);
                    cmd.Parameters.AddWithValue("@KategoriaId", model.KategoriaId);
                    cmd.Parameters.AddWithValue("@CmimiShitjes", model.CmimiShitjes);
                    cmd.Parameters.AddWithValue("@SasiaNeStok", model.SasiaNeStok);
                    cmd.Parameters.AddWithValue("@Aktiv", model.Aktiv);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insert(ProductModel model)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO ""Artikujt""
                    (""Barkodi"", ""Emri"", ""KategoriaId"", ""CmimiShitjes"", ""SasiaNeStok"", ""Aktiv"")
                    VALUES
                    (@Barkodi, @Emri, @KategoriaId, @CmimiShitjes, @SasiaNeStok, @Aktiv);";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Barkodi", model.Barkodi);
                    cmd.Parameters.AddWithValue("@Emri", model.Emri);
                    cmd.Parameters.AddWithValue("@KategoriaId", model.KategoriaId);
                    cmd.Parameters.AddWithValue("@CmimiShitjes", model.CmimiShitjes);
                    cmd.Parameters.AddWithValue("@SasiaNeStok", model.SasiaNeStok);
                    cmd.Parameters.AddWithValue("@Aktiv", model.Aktiv);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
