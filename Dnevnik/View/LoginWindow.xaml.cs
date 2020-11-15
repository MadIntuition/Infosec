using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dnevnik
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static string userLogin;
        private static string userDB;

        UsersDatabase db;
        public LoginWindow()
        {
            InitializeComponent();
            db = new UsersDatabase();
        }

        public string GetUserLogin { 
            get { return userLogin;  }
            set { userLogin = LogintextBox.Text; } 
        }

        public string GetUserDB
        {
            get { return userDB; }
            set { userDB = LogintextBox.Text + ".sqlite"; }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsAuthorized = db.Authorize(LogintextBox.Text, PasswordBox.Password);         
            
            if (IsAuthorized)
            {
                MainWindow mainWindow = new MainWindow(LogintextBox.Text);
                this.Close();
                mainWindow.ShowDialog();                
            }
            else
            {
                warningLabel.Visibility = Visibility.Visible;
                //MessageBox.Show("Неверный логин или пароль. Попробуйте снова.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            LogintextBox.Text = String.Empty;
            PasswordBox.Password = String.Empty;
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }        
    }
}
