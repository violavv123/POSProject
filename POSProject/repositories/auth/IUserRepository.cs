using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.auth
{
    public interface IUserRepository
    {
        UserModel GetByUsername(string username);
        void UpdateLastLogin(int userId);
        bool UsernameExists(string username);
        int CreateUser(UserModel user);
        List<string> GetCashierUsernames();
    }
}
