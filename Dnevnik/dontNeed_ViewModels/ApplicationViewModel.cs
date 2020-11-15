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
        private Entity selectedEntity;
        IEnumerable<Entity> entities;

        public IEnumerable<Entity> Entities
        {
            get { return entities; }
            set
            {
                entities = value;
                OnPropertyChanged("Entities");
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
        IEnumerable<Person> people;
        public IEnumerable<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged("People");
            }
        }
        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.People.Load();
            People = db.People.Local.ToBindingList();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      CreateInstanceOfEntityWindow createInstance = new CreateInstanceOfEntityWindow(new Person());

                      if (createInstance.ShowDialog() == true)
                      {
                          Person person = createInstance.Person;
                          db.People.Add(person);
                          db.SaveChanges();
                      }                                            
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // если ни одного объекта не выделено, выходим
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Person person = selectedItem as Person;

                      Person new_person = new Person()                      
                      {
                          ID_Person = person.ID_Person,
                          FirstName = person.FirstName,
                          LastName = person.LastName,
                          DateOfBirth = person.DateOfBirth,
                          Address = person.Address,
                          EyeColor = person.EyeColor,
                          Telephone = person.Telephone
                      };
                      CreateInstanceOfEntityWindow createInstance = new CreateInstanceOfEntityWindow(new_person);

                      if (createInstance.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          person = db.People.Find(createInstance.Person.ID_Person);
                          //person = db.People.FirstOrDefault(i => i.ID_Person == createInstance.Person.ID_Person );
                          if (person != null)
                          {
                              person.FirstName = createInstance.Person.FirstName;
                              person.LastName = createInstance.Person.LastName;
                              person.DateOfBirth = createInstance.Person.DateOfBirth;
                              person.Address = createInstance.Person.Address;
                              person.EyeColor = createInstance.Person.EyeColor;
                              person.Telephone = createInstance.Person.Telephone;

                              db.Entry(person).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // если ни одного объекта не выделено, выходим
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Person person = selectedItem as Person;
                      db.People.Remove(person);
                      db.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
