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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppTestList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        //Короче, это список где например записаны названия всех полей сущности,  может инициализироваться в MainWindow, например,
        //когда будешь вызывать окошко добавления документа, можно в аргументы передавать его
        List<Field> dict = new List<Field> {new Field("field1"), new Field("field2"), new Field("field3"), new Field("field4"), new Field("field5"),
        new Field("field6"), new Field("field7"), new Field("field8"), new Field("field9"), new Field("field10"),
        new Field("field11"), new Field("field12"), new Field("field13"), new Field("field14"), new Field("field15")};
        List<Field> dict2 = new List<Field>();

        //простенький класс для бинда данных (в шаблонах gridcolumn забинжены поля этого класса)
        class Field
        {
            string title; string fvalue;
            public Field(string title)
            {
                Title = title;
                FValue = title;
            }
            public Field(){}
            public string Title { get => title; set => title = value; }
            public string FValue { get => fvalue; set => fvalue = value; }
            public string toString()
            {
                return "Заголовок: "+ Title + " ; Значение " +  FValue + "\n";  
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.FieldsList.ItemsSource = dict;
        }

        //а это пример считывания. Я для примера запихал в текстбокс, вообще, естественно, все в новый экземпляр сущности
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Field> dict = (List < Field > )FieldsList.ItemsSource;
            StringBuilder teststring = new StringBuilder();
            foreach (Field f in dict)
            {
                teststring.Append(f.toString());
            }
            this.TestTextBox.Text = teststring.ToString();
        }
    }
}
