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
    /// Interaction logic for CreateInstanceOfEntity.xaml
    /// </summary>
    public partial class CreateInstanceOfEntityWindow : Window
    {   
        Database db;
        public CreateInstanceOfEntityWindow(Entity entity, string userName)
        {
            InitializeComponent();
            db = new Database(userName);
            var data = GetList(entity.EntityName);

            this.FieldsList.ItemsSource = data;
            
        }
        
        public CreateInstanceOfEntityWindow(Document document)
        {
            InitializeComponent();
            //this.FieldsList.ItemsSource = GetList();
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

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
