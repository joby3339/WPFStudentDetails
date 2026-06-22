using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFStudentDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Display current user information
            if (App.CurrentUser != null)
            {
                UserNameText.Text = App.CurrentUser.FullName;
                UserRoleText.Text = $"({App.CurrentUser.Role})";
            }
            else
            {
                // If no user is logged in, redirect to login
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?", 
                "Confirm Logout", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Clear current user
                App.CurrentUser = null;

                // Show login window
                var loginWindow = new LoginWindow();
                loginWindow.Show();

                // Close main window
                this.Close();
            }
        }
    }
}