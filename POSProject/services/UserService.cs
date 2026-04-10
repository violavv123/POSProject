using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;
using POSProject.repositories.auth;

namespace POSProject.services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public UserModel GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            return _userRepo.GetByUsername(username);
        }

        public bool UsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            return _userRepo.UsernameExists(username);
        }

        public (bool Success, string Message, UserModel User) Login(string username, string password)
        {
            username = username?.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return (false, "Shkruani usernamin dhe passwordin.", null);

            var user = _userRepo.GetByUsername(username);

            if (user == null)
                return (false, "Ky përdorues nuk ekziston.", null);

            if (!user.isActive)
                return (false, "Ky përdorues është jo aktiv. Kontaktoni administratorin.", null);

            bool isValidPassword = PasswordHelper.VerifyPassword(password, user.PasswordHash);
            if (!isValidPassword)
                return (false, "Passwordi është jo i saktë. Provoni përsëri.", null);

            _userRepo.UpdateLastLogin(user.Id);

            return (true, "OK", user);
        }

        public (bool Success, string Message) CreateUser(string username, string password, string confirmPassword)
        {
            username = username?.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                return (false, "Plotësoni të gjitha fushat.");
            }

            if (password != confirmPassword)
                return (false, "Passwordet nuk përputhen.");

            if (password.Length < 6)
                return (false, "Passwordi duhet të jetë të paktën 6 karaktere.");

            if (_userRepo.UsernameExists(username))
                return (false, "Ky username është i zënë. Zgjidhni një tjetër.");

            var user = new UserModel
            {
                Username = username,
                PasswordHash = PasswordHelper.HashPassword(password),
                Role = "cashier",
                isActive = true
            };

            _userRepo.CreateUser(user);

            return (true, "Përdoruesi u krijua me sukses.");
        }

        public void StartSession(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Session.Start(user.Id, user.Username, user.Role);
        }
    }
}

