using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    public class DocumentView: INotifyPropertyChanged
    {
        public DocumentView()
        {

        }

        private string entityName;
        private string annotationFields;
        [Key]
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
