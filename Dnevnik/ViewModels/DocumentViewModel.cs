using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik.ViewModels
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        Database db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Document> documents;
        private Entity selectedEntity;

        public DocumentViewModel(string fileName)
        {
            db = new Database(fileName);
        }
                
        public IEnumerable<Document> Documents
        {
            get { return documents; }
            set
            {
                documents = value;
                OnPropertyChanged("Documents");
            }
        }

        public Entity SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                selectedEntity = value;
                OnPropertyChanged("SelectedEntity");
            }
        }
        public IEnumerable<Entity> GetEntities()
        {
            foreach (string entity in db.GetEntities())
            {
                yield return new Entity()
                {
                    EntityName = entity
                };
            }
        }
        public IEnumerable<Document> GetDocuments(string tableTitle, int[] annotationFields)
        {
            //здесь должен быть метод, который возвращает IEnumerable<Document>

            //db.GetEntityFieldList(tableTitle, annotationFields);
            //foreach (var entity in ...)
            //{

            //}
        }



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
