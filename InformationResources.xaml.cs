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

namespace ISAS.Inventory
{
    /// <summary>
    /// Логика взаимодействия для InformationResources.xaml
    /// </summary>
    public partial class InformationResources : Window
    {
        List<InformationResource> current_resources = InformationResource.ReadFromXml("CurrentIR.xml");
        public InformationResources()
        {
            InitializeComponent();
            //MessageBox.Show(inf_resources[0].Title);
            this.InfResList.ItemsSource = InformationResource.ReadFromXml("InformationResources.xml");
            this.InfResCur.ItemsSource = InformationResource.ReadFromXml("CurrentIR.xml");
        }

        private void AddResouceClick(object sender, RoutedEventArgs e)
        {
            InformationResourcesAddNew addingWindow = new InformationResourcesAddNew();

            if (addingWindow.ShowDialog() == true)
            {
                this.InfResList.ItemsSource = InformationResource.ReadFromXml("InformationResources.xml");
                MessageBox.Show("Информационный ресурс \""+ addingWindow.ResourceTitle+"\" успешно добавлен");        
            }

        }

        private void ToCurrentTableClick(object sender, RoutedEventArgs e)
        {
            List<InformationResource> selected_resources = InfResList.SelectedItems.Cast<InformationResource>().ToList();
            List<InformationResource> helpList=new List<InformationResource>();
            helpList.AddRange(current_resources);
            //MessageBox.Show(selected_resources[0].Title);
            foreach (InformationResource res in selected_resources)
            {
                helpList.Add(res);
            }
            this.InfResCur.ItemsSource = helpList;
            current_resources.Clear();
            current_resources.AddRange(helpList);
        }

        private void SaveCurResClick(object sender, RoutedEventArgs e)
        {
            InformationResource.DeleteAllFromXml("CurrentIR.xml");
            foreach (InformationResource res in current_resources)
            {
                res.WriteToXml("CurrentIR.xml");          
            }
            MessageBox.Show("Изменения сохранены");
        }

        private void DeleteFromCurResClick(object sender, RoutedEventArgs e)
        {
            List<InformationResource> selected_resources = InfResCur.SelectedItems.Cast<InformationResource>().ToList();
            List<InformationResource> helpList = new List<InformationResource>();
            helpList.AddRange(current_resources);
            foreach (InformationResource res in selected_resources)
            {
                helpList.Remove(res);
            }
            this.InfResCur.ItemsSource = helpList;
            current_resources.Clear();
            current_resources.AddRange(helpList);
        }

        private void DeleteResouceClick(object sender, RoutedEventArgs e)
        {
            List<InformationResource> selected_resources = InfResList.SelectedItems.Cast<InformationResource>().ToList();
            List<InformationResource> helpList = new List<InformationResource>();
            helpList.AddRange(current_resources);
            foreach (InformationResource res in selected_resources)
            {
                helpList.Remove(res);
                res.DeleteFromXml("InformationResources.xml", res.Title);
            }
            this.InfResList.ItemsSource =InformationResource.ReadFromXml("InformationResources.xml") ;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (!(InfResCur.SelectedItem is null))
            {
                InformationResource selected_resource = (InformationResource)InfResCur.SelectedItem;
                //selected_resource = InformationResource.FindResourceXml("CurrentIR.xml", selected_resource.Title);
                Editing editWindow = new Editing(selected_resource);
                if (editWindow.ShowDialog() == true)
                {
                    selected_resource.Count = editWindow.Count;
                    selected_resource.Description = editWindow.Description;
                    selected_resource.Personaldata = editWindow.Personaldata;
                    selected_resource.Commercial_data = editWindow.Commercial_data;
                    selected_resource.Statesecret = editWindow.Statesecret;
                    selected_resource.EditXml("CurrentIR.xml");
                }
                this.InfResCur.ItemsSource = InformationResource.ReadFromXml("CurrentIR.xml");
            }
        }
    }
}
