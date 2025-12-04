using System.Configuration;
using System.Data;
using System.Windows;
using WPFStudentDetails.Models;

namespace WPFStudentDetails
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User? CurrentUser { get; set; }
    }

}
