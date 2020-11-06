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
        public IEnumerable<NewEntity> Entities;
        public CreateEntityWindow()
        {
            InitializeComponent();
            newEntityFieldsGrid.DataContext = new EntityViewModel();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getData();
            this.Close();
        }
    }
}
