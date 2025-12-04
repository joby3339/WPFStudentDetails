using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WPFStudentDetails.Models;

namespace WPFStudentDetails.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private UserRole _selectedRole;
        private string _errorMessage = string.Empty;
        private bool _hasError;
        private List<User> _users = new();

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                ClearError();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ClearError();
            }
        }

        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged(nameof(HasError));
            }
        }

        public LoginViewModel()
        {
            LoadUsers();
        }

        public async Task<LoginResult> LoginAsync()
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(Username))
            {
                return new LoginResult(false, "Username is required.", null);
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return new LoginResult(false, "Password is required.", null);
            }

            // Simulate async operation
            await Task.Delay(500);

            // Find user
            var user = _users.FirstOrDefault(u => 
                u.Username.Equals(Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == Password &&
                u.Role == SelectedRole &&
                u.IsActive);

            if (user != null)
            {
                return new LoginResult(true, string.Empty, user);
            }

            return new LoginResult(false, "Invalid username, password, or role combination.", null);
        }

        private void LoadUsers()
        {
            try
            {
                var usersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Users.json");
                
                if (File.Exists(usersFilePath))
                {
                    var jsonContent = File.ReadAllText(usersFilePath);
                    _users = JsonSerializer.Deserialize<List<User>>(jsonContent) ?? new List<User>();
                }
                else
                {
                    // Create default users if file doesn't exist
                    CreateDefaultUsers();
                    SaveUsers();
                }
            }
            catch (Exception ex)
            {
                // If there's an error loading users, create default ones
                CreateDefaultUsers();
                // Log error in a real application
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        private void CreateDefaultUsers()
        {
            _users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    Password = "admin123",
                    Role = UserRole.Admin,
                    FullName = "System Administrator",
                    IsActive = true
                },
                new User
                {
                    Username = "teacher",
                    Password = "teach123",
                    Role = UserRole.Teacher,
                    FullName = "John Teacher",
                    IsActive = true
                },
                new User
                {
                    Username = "student",
                    Password = "stud123",
                    Role = UserRole.Student,
                    FullName = "Jane Student",
                    IsActive = true
                }
            };
        }

        private void SaveUsers()
        {
            try
            {
                var usersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Users.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonContent = JsonSerializer.Serialize(_users, options);
                File.WriteAllText(usersFilePath, jsonContent);
            }
            catch (Exception ex)
            {
                // Log error in a real application
                System.Diagnostics.Debug.WriteLine($"Error saving users: {ex.Message}");
            }
        }

        private void ClearError()
        {
            if (HasError)
            {
                HasError = false;
                ErrorMessage = string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LoginResult
    {
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }
        public User? User { get; }

        public LoginResult(bool isSuccessful, string errorMessage, User? user)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            User = user;
        }
    }
}