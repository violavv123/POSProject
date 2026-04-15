using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable GetAllSubjects()
        {
            DataTable dt = new DataTable();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"", ""Pershkrimi"", ""NumriFiskal"", ""Adresa"", ""LlojiSubjektit""
                                 FROM ""Subjektet""
                                 ORDER BY ""Pershkrimi"";";
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void Insert(SubjectModel subject)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO ""Subjektet""(""Pershkrimi"", ""NumriFiskal"", ""Adresa"", ""LlojiSubjektit"")
                                 VALUES(@Pershkrimi, @NumriFiskal, @Adresa, @LlojiSubjektit);";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Pershkrimi", subject.Pershkrimi);
                    cmd.Parameters.AddWithValue("@NumriFiskal", (object?)subject.NumriFiskal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Adresa", (object?)subject.Adresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LlojiSubjektit", (object?)subject.LlojiSubjektit ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SubjectModel subject)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE ""Subjektet""
                                 SET 
                                   ""Pershkrimi"" = @Pershkrimi,
                                   ""NumriFiskal"" = @NumriFiskal,
                                   ""Adresa"" = @Adresa,
                                   ""LlojiSubjektit"" = @LlojiSubjektit
                                 WHERE ""Id"" = @Id;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", subject.Id);
                    cmd.Parameters.AddWithValue("@Pershkrimi", subject.Pershkrimi);
                    cmd.Parameters.AddWithValue("@NumriFiskal", (object?)subject.NumriFiskal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Adresa", (object?)subject.Adresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LlojiSubjektit", (object?)subject.LlojiSubjektit ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"DELETE FROM ""Subjektet"" WHERE ""Id"" = @Id;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
