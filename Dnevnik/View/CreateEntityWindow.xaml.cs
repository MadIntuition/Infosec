using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CreateEntityWindow.xaml
    /// </summary>
    public partial class CreateEntityWindow : Window
    {
        Database db;
        public IEnumerable<NewEntity> Entities;
        public CreateEntityWindow(string userLogin)
        {
            InitializeComponent();
            newEntityFieldsGrid.DataContext = new EntityViewModel();
            db = new Database(userLogin+".sqlite");
        }

        // This snippet is much safer in terms of preventing unwanted
        // Exceptions because of missing [DisplayNameAttribute].
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        public IEnumerable<NewEntity> getData()
        {
            Entities = newEntityFieldsGrid.ItemsSource as IEnumerable<NewEntity>;

            return Entities;
        }
        public IEnumerable<string> getFieldsNames()
        {
            List<NewEntity> fieldsCollection = getData().ToList();
            if (fieldsCollection.Count == 0)
                WarningLabel.Content = "заполни таблицу э";
            //MessageBox.Show("заполни таблицу  э", "!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                foreach (var field in fieldsCollection)
                {
                    yield return field.FieldName;
                }
            }
            
        }
        public IEnumerable<string> getImportantFieldsNames()
        {
            List<NewEntity> fieldsCollection = getData().ToList();
            
            foreach (var field in fieldsCollection)
            {
                if (field.Importance)
                    yield return field.FieldName;
            }
        }
        public bool[] getImportantFields()
        {
            List<NewEntity> fieldsCollection = getData().ToList();

            bool[] mas = new bool[fieldsCollection.Count()];
            int i = 0;
            foreach (var field in fieldsCollection)
            {
                if (field.Importance)
                    mas[i] = true;
                else
                    mas[i] = false;

                i++;
            }
            return mas;
        }

        private void CreateEntity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(EntityName.Text))
                {
                    EntityName.BorderBrush = Brushes.Red;
                    EntityName.BorderThickness = new Thickness(3);
                    WarningLabel.Content = "название сущности то уж напиши ";

                }
                else
                {
                    WarningLabel.Content = "";
                    EntityName.BorderThickness = new Thickness(1);
                    EntityName.BorderBrush = Brushes.Gray;
                    if (getFieldsNames().Count() != 0)
                    {
                        if  (getImportantFieldsNames().Count() != 0) 
                            CreateNewEntity();
                        else
                            WarningLabel.Content = "выбери хотяб одно важное поле";
                    }
                }
            }
            catch
            {
                MessageBox.Show("што-то пошло не так", "не ура", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void CreateNewEntity()
        {
            bool success = db.CreateNewEntity(EntityName.Text, getFieldsNames(), getImportantFields());//+getImportantFieldsNames()
            if (success)
            {
                MessageBox.Show("Успешный успех", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("што-то пошло не так", "не ура", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
