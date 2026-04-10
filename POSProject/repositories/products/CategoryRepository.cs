using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.products
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<CategoryModel> GetActiveCategories()
        {
            var list = new List<CategoryModel>();
            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"SELECT ""id"", ""Emri"", ""Aktive""
                             FROM ""kategorite""
                             WHERE ""Aktive"" = TRUE
                             ORDER BY ""Emri"";";
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CategoryModel
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Emri = reader["Emri"].ToString(),
                    Aktive = Convert.ToBoolean(reader["Aktive"])
                });
            }
            return list;
        }
    }
}
