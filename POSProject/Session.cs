using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProject
{
    public static class Session
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }

        public static string Role { get; set; }

        public static bool isLoggedIn { get; set; }

        public static void Start(int userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
            isLoggedIn = true;
        }

        public static void Clear()
        {
            UserId = 0;
            Username = null;
            Role = null;
            isLoggedIn = false;
        }
    }
}
