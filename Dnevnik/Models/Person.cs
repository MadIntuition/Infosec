using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dnevnik
{
    //[Table("Person")]
    public class Person : INotifyPropertyChanged
    {
        [Key]
        public int ID_Person { get; set; }

        private string firstName;
        private string lastName;
        private DateTime dateOfBirth = DateTime.Now;
        private bool isSmart;
        private bool isSingle;
        private Int64 telephone;
        //[Required]
        [DisplayName("Firsttt Name")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public DateTime DateOfBirth
        {
            get 
            {
                //if (String.IsNullOrEmpty(dateOfBirth.ToString()))
                //    dateOfBirth = DateTime.Now;
                return dateOfBirth; 
            }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public bool IsSmart
        {
            get { return isSmart; }
            set
            {
                isSmart = value;
                OnPropertyChanged("IsSmart");
            }
        }
        public bool IsSingle
        {
            get { return isSingle; }
            set
            {
                isSingle = value;
                OnPropertyChanged("IsSingle");
            }
        }
        public Int64 Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                OnPropertyChanged("Telephone");
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
