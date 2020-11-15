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
            db = new Database(userLogin);
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

            foreach (var field in fieldsCollection)
            {
                yield return field.FieldName;
            }
        }

        private void CreateEntity_Click(object sender, RoutedEventArgs e)
        {
            bool success =
            db.CreateNewEntity(EntityName.Text, getFieldsNames());
            //getData();
            if (success)
            {
                MessageBox.Show("Успешный успех", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("што-то пошло не так", "не ура", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.Close();
        }
    }
}
