using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public static class NotificationService
    {
        public static void Create(string type, string severity, string title, string message, string relatedEntity = null, int? relatedEntityId = null, int? createdBy = null)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO ""Notifications""
                                (""Type"", ""Severity"", ""Title"", ""Message"", ""RelatedEntity"", ""RelatedEntityId"", ""Created_At"", ""Created_By"")
                                VALUES(@Type, @Severity, @Title, @Message, @RelatedEntity, @RelatedEntityId, @Created_At, @Created_By);";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Severity", severity);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@RelatedEntity", (object?)relatedEntity ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RelatedEntityId", (object?)relatedEntityId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Created_By", (object?)createdBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Created_At", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetUnreadNotificationsCount()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM ""Notifications"" WHERE ""IsRead"" = FALSE;";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
