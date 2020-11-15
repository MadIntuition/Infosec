using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dnevnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        private static string _userLogin;
        public IEnumerable<Entity> Entities { get; set; }
        public MainWindow(string userLogin)
        {
            InitializeComponent();
            _userLogin = userLogin;

            //this.DataContext = new ApplicationViewModel();
            //db.Entities.Load();
            //Entities = db.Entities.Local.ToBindingList();
            //this.entitiesListBox.ItemsSource = Entities;
        }


        private void CreateTypeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityWindow createEntityWindow = new CreateEntityWindow(_userLogin);
            createEntityWindow.Owner = this;

            createEntityWindow.ShowDialog();
            
        }

        private void LoginMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            this.Close();
            login.ShowDialog();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
                
    }
}
