using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    //[Table("What")]
    public class What : INotifyPropertyChanged
    {
        [Key]
        public int ID { get; set; }

        private string who;
        private string are;
        private string you;
        public string Who
        {
            get { return who; }
            set
            {
                who = value;
                OnPropertyChanged("Who");
            }
        }
        public string Are
        {
            get { return are; }
            set
            {
                are = value;
                OnPropertyChanged("Are");
            }
        }
        public string You
        {
            get { return you; }
            set
            {
                you = value;
                OnPropertyChanged("You");
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
