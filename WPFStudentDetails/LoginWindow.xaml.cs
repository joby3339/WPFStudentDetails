using System.Windows;
using System.Windows.Controls;
using WPFStudentDetails.ViewModels;
using WPFStudentDetails.Models;

namespace WPFStudentDetails
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
            
            // Set default role selection
            UserRoleComboBox.SelectedIndex = 0; // Default to Admin
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.Password = passwordBox.Password;
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRole = GetSelectedRole();
            if (selectedRole == null)
            {
                _viewModel.ErrorMessage = "Please select a user role.";
                _viewModel.HasError = true;
                return;
            }

            _viewModel.SelectedRole = selectedRole.Value;
            var loginResult = await _viewModel.LoginAsync();

            if (loginResult.IsSuccessful)
            {
                // Store the current user information
                App.CurrentUser = loginResult.User;
                
                // Open main window
                var mainWindow = new MainWindow();
                mainWindow.Show();
                
                // Close login window
                this.Close();
            }
            else
            {
                _viewModel.ErrorMessage = loginResult.ErrorMessage;
                _viewModel.HasError = true;
            }
        }

        private void QuickLogin_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tagData)
            {
                var parts = tagData.Split(',');
                if (parts.Length == 3)
                {
                    UsernameTextBox.Text = parts[0];
                    PasswordBox.Password = parts[1];
                    
                    // Set the role in combo box
                    foreach (ComboBoxItem item in UserRoleComboBox.Items)
                    {
                        if (item.Tag?.ToString() == parts[2])
                        {
                            UserRoleComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private UserRole? GetSelectedRole()
        {
            if (UserRoleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var roleString = selectedItem.Tag?.ToString();
                if (System.Enum.TryParse<UserRole>(roleString, out var role))
                {
                    return role;
                }
            }
            return null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Focus();
        }
    }
}