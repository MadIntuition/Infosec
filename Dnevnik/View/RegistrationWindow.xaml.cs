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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        
        AuthorizedUsersViewModel viewModel;
        public RegistrationWindow()
        {
            InitializeComponent();
            viewModel = new AuthorizedUsersViewModel();
            //user = new AuthorizedUser();
            //this.DataContext = user;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizedUser searchUser = viewModel.GetByLogin(LoginTextBox.Text);

            if (searchUser == null)
            {
                AuthorizedUser createUser = new AuthorizedUser
                {
                    Name = NameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    DateOfBirth = DateTime.Now,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text
                };
                viewModel.Create(createUser);
                viewModel.Dispose();
                MessageBox.Show("Пользователь успешно зарегистрирован в системе!", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                LoginTextBox.BorderBrush = Brushes.Red;
                LoginTextBox.BorderThickness = new Thickness(10);
                MessageBox.Show("Пользователь с таким логином уже существует, выберите другой логин.",
                    "Упс!", MessageBoxButton.OK, MessageBoxImage.Information);
            }       
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
