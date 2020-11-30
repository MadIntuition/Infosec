using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UsersDatabase users = new UsersDatabase();

            User user1 = new User("rtst1", "123");
            users.Register(user1);
            user1.CreateDatabase();

            User user2 = new User("leidoo", "123");
            users.Register(user2);
            user2.CreateDatabase();

            Document doc = new Document();
            doc.Fields.Add("field2", "1");
            doc.Fields.Add("field4", "2");

            user1.Db.CreateNewEntity("Persons", new List<string>() { "field1", "field2", "field3", "field4" }, new bool[] { false, true, false, true });

            user1.Db.AddDocument("Persons", doc);
            user1.Db.AddDocument("Persons", doc);
            user1.Db.AddDocument("Persons", doc);

            doc.Fields["field2"] = 1;
            user1.Db.EditDocument("Persons", 2, doc);

            user1.Db.DeleteDocument("Persons", 3);

            OrderedDictionary dict = user1.Db.GetEntityAnnotationFieldList("Persons");
            DataTable dt = new DataTable();

            string[] keys = new string[dict.Count];
            dict.Keys.CopyTo(keys, 0);

            List<string>[] values = new List<string>[dict.Count];
            dict.Values.CopyTo(values, 0);

            dt.Columns.AddRange(keys.Select(x => new DataColumn(x)).ToArray());

            for (int i = 0; i < values[0].Count; i++)
            {
                DataRow row = dt.NewRow();
                row.ItemArray = values.Select(x => x[i]).ToArray();
                dt.Rows.Add(row);
            }

            Table.DataContext = dt.DefaultView;
        }
    }
}
