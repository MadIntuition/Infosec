﻿using System;
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
        //Database db;
        EntitiesViewModel entityViewModel;
        DocumentViewModel docViewModel;
        private static string _userLogin;
        ///ApplicationContext db = new ApplicationContext();
        public List<Entity> Entities { get; set; }
        public MainWindow(string userLogin)
        {
            InitializeComponent();
            _userLogin = userLogin;

            entityViewModel = new EntitiesViewModel(userLogin);
            UserLogin.Header = $"хей йо, {userLogin} ";

            Entities = entityViewModel.GetEntities().ToList();
            //this.DataContext = entityViewModel;
            //this.DataContext = Entities;
            this.entitiesListBox.ItemsSource = Entities;

            //---------------
            docViewModel = new DocumentViewModel(userLogin);
            //if (!String.IsNullOrEmpty(entityViewModel.SelectedEntity.EntityName))
            //{
            //    var data = GetList(entityViewModel.SelectedEntity.EntityName);
            //    this.DataContext = data;
            //    this.instancesListBox.ItemsSource = data;
            //}
                
        }


        private void CreateTypeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityWindow createEntityWindow = new CreateEntityWindow(_userLogin);
            createEntityWindow.Owner = this;

            createEntityWindow.ShowDialog();
            Entities = entityViewModel.GetEntities().ToList();
            this.entitiesListBox.ItemsSource = Entities;
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

        public List<Field> GetList(string tableName)
        {
            //var data = db.GetEntityFieldListNEW(tableName);

            List<Field> dict = new List<Field>
            {
                new Field("field1"), new Field("field2"), new Field("field3"), new Field("field4"), new Field("field5"),
                new Field("field6"), new Field("field7")
            };
            return dict;
        }
    }
}
