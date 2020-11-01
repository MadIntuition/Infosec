using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    public class ApplicationViewModel1 : INotifyPropertyChanged
    {
        private Entities selectedEntity;

        public ObservableCollection<Entities> Entities { get; set; }
        public Entities SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                selectedEntity = value;
                OnPropertyChanged("SelectedEntity");
            }
        }

        //public ApplicationViewModel()
        //{
        //    Entities = new ObservableCollection<Entities> { };
            
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Entities> entities;

        public IEnumerable<Entities> Entities
        {
            get { return entities; }
            set
            {
                entities = value;
                OnPropertyChanged("Entities");
            }
        }
        IEnumerable<Person> people;

        public IEnumerable<Person> Person
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged("Person");
            }
        }
        public ApplicationViewModel()
        {
            db = new ApplicationContext();

            db.Entities.Load();
            Entities = db.Entities.Local.ToBindingList();

            db.People.Load();
            Person = db.People.Local.ToBindingList();
        }
        // команда добавления
        //public RelayCommand AddCommand
        //{
        //    get
        //    {
        //        return addCommand ??
        //          (addCommand = new RelayCommand((o) =>
        //          {
        //              PhoneWindow phoneWindow = new PhoneWindow(new Phone());
        //              if (phoneWindow.ShowDialog() == true)
        //              {
        //                  Entities phone = phoneWindow.Phone;
        //                  db.Entities.Add(phone);
        //                  db.SaveChanges();
        //              }
        //          }));
        //    }
        //}
        // команда редактирования
        //public RelayCommand EditCommand
        //{
        //    get
        //    {
        //        return editCommand ??
        //          (editCommand = new RelayCommand((selectedItem) =>
        //          {
        //              if (selectedItem == null) return;
        //              // получаем выделенный объект
        //              Phone phone = selectedItem as Phone;

        //              Phone vm = new Phone()
        //              {
        //                  Id = phone.Id,
        //                  Company = phone.Company,
        //                  Price = phone.Price,
        //                  Title = phone.Title
        //              };
        //              PhoneWindow phoneWindow = new PhoneWindow(vm);


        //              if (phoneWindow.ShowDialog() == true)
        //              {
        //                  // получаем измененный объект
        //                  phone = db.Entities.Find(phoneWindow.Phone.Id);
        //                  if (phone != null)
        //                  {
        //                      phone.Company = phoneWindow.Phone.Company;
        //                      phone.Title = phoneWindow.Phone.Title;
        //                      phone.Price = phoneWindow.Phone.Price;
        //                      db.Entry(phone).State = EntityState.Modified;
        //                      db.SaveChanges();
        //                  }
        //              }
        //          }));
        //    }
        //}
        // команда удаления
        //public RelayCommand DeleteCommand
        //{
        //    get
        //    {
        //        return deleteCommand ??
        //          (deleteCommand = new RelayCommand((selectedItem) =>
        //          {
        //              if (selectedItem == null) return;
        //              // получаем выделенный объект
        //              //Phone phone = selectedItem as Phone;
        //              db.Entities.Remove(phone);
        //              db.SaveChanges();
        //          }));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
