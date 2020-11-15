using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
    {
        private IEnumerable<Entity> entities;

        public IEnumerable<Entity> Entities
        {
            get { return entities; }
            set
            {
                entities = value;
                OnPropertyChanged("Entities");
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
