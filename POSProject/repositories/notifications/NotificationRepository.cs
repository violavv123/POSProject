using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.notifications
{
    public class NotificationRepository : INotificationRepository
    {
        public List<NotificationModel> GetNotifications(bool onlyUnread, int daysBack = 30, int maxRows = 200)
        {
            var list = new List<NotificationModel>();
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"", ""Type"", ""Severity"", ""Title"", ""Message"", ""RelatedEntity"", ""RelatedEntityId"", ""IsRead"",
                                 ""Created_At"", ""Created_By""
                                 FROM ""Notifications""
                                 WHERE (@OnlyUnread = FALSE OR ""IsRead"" = FALSE)
                                   AND ""Created_At"" >= NOW() - (@DaysBack * INTERVAL '1 day')
                                 ORDER BY ""IsRead"" ASC, ""Created_At"" DESC
                                 LIMIT @MaxRows;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OnlyUnread", onlyUnread);
                    cmd.Parameters.AddWithValue("@DaysBack", daysBack);
                    cmd.Parameters.AddWithValue("@MaxRows", maxRows);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NotificationModel
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Type = reader["Type"]?.ToString(),
                                Severity = reader["Severity"]?.ToString(),
                                Title = reader["Title"]?.ToString(),
                                Message = reader["Message"]?.ToString(),
                                RelatedEntity = reader["RelatedEntity"] == DBNull.Value ? null : reader["RelatedEntity"].ToString(),
                                RelatedEntityId = reader["RelatedEntityId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["RelatedEntityId"]),
                                IsRead = Convert.ToBoolean(reader["IsRead"]),
                                Created_At = Convert.ToDateTime(reader["Created_At"]),
                                Created_By = reader["Created_By"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Created_By"])
                            });

                        }
                    }
                }
            }
            return list;
        }

        public void MarkAsRead(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE ""Notifications""
                                 SET ""IsRead"" = TRUE
                                 WHERE ""Id"" = @Id;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int GetUnreadCount(int daysBack = 30)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(*)
                                 FROM ""Notifications""
                                 WHERE ""IsRead"" = FALSE
                                   AND ""Created_At"" >= NOW() - (@DaysBack * INTERVAL '1 day');";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DaysBack", daysBack);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void Create(string type, string severity, string title, string message, string relatedEntity = null, int? relatedEntityId = null, int? createdBy = null)
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
    }
}
