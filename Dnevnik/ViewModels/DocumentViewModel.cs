using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        Database db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Document> documents;
        private Entity selectedDocument;

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

        public Entity SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged("SelectedDocument");
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

        public List<DocumentView> GetDocuments(string tableTitle)
        {
            //здесь метод, который возвращает 
            List<DocumentView> list = new List<DocumentView>();
            DocumentView docView = new DocumentView();
            docView.EntityName = tableTitle;
            var docs = db.GetEntityAnnotationFieldList(tableTitle);
            foreach (DictionaryEntry doc in docs)
            {
                docView.AnnotationFields += String.Format("{0}: {1}. ", doc.Key, doc.Value);
            }
            list.Add(docView);
            return list;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    
    
}
