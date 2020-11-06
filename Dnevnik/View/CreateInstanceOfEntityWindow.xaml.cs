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
        public Person Person { get; private set; }
        public CreateInstanceOfEntityWindow(Person p)
        {
            InitializeComponent();
            Person = p;
            this.DataContext = Person;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
