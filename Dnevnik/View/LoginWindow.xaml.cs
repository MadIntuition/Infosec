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
        AuthorizedUsersViewModel viewModel;
        AuthorizedUser user;
        public LoginWindow()
        {
            InitializeComponent();
            viewModel = new AuthorizedUsersViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredlogin = LogintextBox.Text,
                enteredpassword = PasswordBox.Password;

            user = viewModel.GetByLogin(enteredlogin);

            if (user != null)
            {
                CheckCredentials(user, enteredlogin, enteredpassword);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль. Попробуйте снова.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            LogintextBox.Text = String.Empty;
            PasswordBox.Password = String.Empty;
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }


        public void CheckCredentials(AuthorizedUser user, string enteredLogin, string enteredPassword)
        {
            if (user.Login == enteredLogin && user.Password == enteredPassword)
            {
                warningLabel.Visibility = Visibility.Hidden;
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.ShowDialog();
            }
            else
            {
                warningLabel.Visibility = Visibility.Visible;
                
            }
        }
    }
}
