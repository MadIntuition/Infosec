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
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();

            //db = new ApplicationContext();
            //db.Entities.Load();
            //Entities = db.Entities.Local.ToBindingList();

            this.DataContext = new ApplicationViewModel();
            //db.People.Load();
            //this.DataContext = db.People.Local.ToBindingList();
        }
                
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //PhoneWindow phoneWindow = new PhoneWindow(new Phone());
            //if (phoneWindow.ShowDialog() == true)
            //{
            //    Phone phone = phoneWindow.Phone;
            //    db.Phones.Add(phone);
            //    db.SaveChanges();
            //}
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //// если ни одного объекта не выделено, выходим
            //if (phonesList.SelectedItem == null) return;
            //// получаем выделенный объект
            //Phone phone = phonesList.SelectedItem as Phone;

            //PhoneWindow phoneWindow = new PhoneWindow(new Phone
            //{
            //    Id = phone.Id,
            //    Company = phone.Company,
            //    Price = phone.Price,
            //    Title = phone.Title
            //});

            //if (phoneWindow.ShowDialog() == true)
            //{
            //    // получаем измененный объект
            //    phone = db.Phones.Find(phoneWindow.Phone.Id);
            //    if (phone != null)
            //    {
            //        phone.Company = phoneWindow.Phone.Company;
            //        phone.Title = phoneWindow.Phone.Title;
            //        phone.Price = phoneWindow.Phone.Price;
            //        db.Entry(phone).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //}
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //// если ни одного объекта не выделено, выходим
            //if (phonesList.SelectedItem == null) return;
            //// получаем выделенный объект
            //Phone phone = phonesList.SelectedItem as Phone;
            //db.Phones.Remove(phone);
            //db.SaveChanges();
        }

        private void CreateTypeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityWindow createEntityWindow = new CreateEntityWindow();
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
