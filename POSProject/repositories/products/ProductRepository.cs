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
        
    }
}
