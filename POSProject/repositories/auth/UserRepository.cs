using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using POSProject.models;

namespace POSProject.repositories.auth
{
    public class UserRepository : IUserRepository
    {
        public UserModel GetByUsername(string username)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT ""id"", ""username"", ""password_hash"", ""role"", ""is_active"", ""created_at"", ""last_login_at""
                             FROM ""perdoruesit""
                             WHERE LOWER(""username"") = LOWER(@username)
                             LIMIT 1;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new UserModel
            {
                Id = Convert.ToInt32(reader["id"]),
                Username = reader["username"].ToString(),
                PasswordHash = reader["password_hash"].ToString(),
                Role = reader["role"].ToString(),
                isActive = Convert.ToBoolean(reader["is_active"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                LastLoginAt = reader["last_login_at"] == DBNull.Value ? null : Convert.ToDateTime(reader["last_login_at"])
            };
        }

        public void UpdateLastLogin(int userId)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"UPDATE ""perdoruesit""
                             SET ""last_login_at"" = NOW()
                             WHERE ""id"" = @id;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", userId);
            cmd.ExecuteNonQuery();
        }

        public bool UsernameExists(string username)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"SELECT COUNT(*) FROM ""perdoruesit"" WHERE ""username"" = @username;";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public int CreateUser(UserModel user)
        {
            using var conn = Db.GetConnection();
            conn.Open();
            string query = @"INSERT INTO ""perdoruesit""(""username"", ""password_hash"", ""role"", ""is_active"", ""created_at"")
                             VALUES(@username, @passwordHash, @role, @isActive, NOW())
                             RETURNING ""id"";";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.Parameters.AddWithValue("@isActive", user.isActive);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
