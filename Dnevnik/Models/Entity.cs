using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dnevnik
{
    //[Table("Entities")]
    public class Entity : INotifyPropertyChanged
    {
        [Key]
        public int ID_Entity { get; set; }

        private string entityName;
        private string annotationFields;

        public string EntityName
        {
            get { return entityName; }
            set
            {
                entityName = value;
                OnPropertyChanged("EntityName");
            }
        }
        public string AnnotationFields
        {
            get { return annotationFields; }
            set
            {
                annotationFields = value;
                OnPropertyChanged("AnnotationFields");
            }
        }
        //private Dictionary<string, string> entity;


        //public Dictionary<string, string> EntityName
        //{
        //    get { return entity; }
        //    set
        //    {
        //        entity = value;
        //        OnPropertyChanged("EntityName");
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
