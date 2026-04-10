using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.subjects
{
    public class SubjectRepository : ISubjectRepository
    {
        public List<string> GetAllNames()
        {
            var result = new List<string>();
            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"SELECT ""Pershkrimi""
                             FROM ""Subjektet""
                             ORDER BY ""Pershkrimi"";";
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Pershkrimi"].ToString());
            }
            return result;
        }

        public SubjectModel GetByPershkrimi(string pershkrimi)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string query = @"SELECT ""Id"", ""Pershkrimi"", ""NumriFiskal"", ""Adresa"", ""LlojiSubjektit""
                             FROM ""Subjektet""
                             WHERE LOWER(TRIM(""Pershkrimi"")) = LOWER(TRIM(@pershkrimi))
                             LIMIT 1;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pershkrimi", pershkrimi);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new SubjectModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Pershkrimi = reader["Pershkrimi"].ToString(),
                NumriFiskal = reader["NumriFiskal"]?.ToString(),
                Adresa = reader["Adresa"]?.ToString(),
                LlojiSubjektit = reader["LlojiSubjektit"]?.ToString()
            };
        }
    }
}
