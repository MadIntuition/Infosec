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
        private DocumentView selectedDocument;
        private string _userLogin;
        private string _selectedEntity;
        EntitiesViewModel entitiesViewModel;


        public DocumentViewModel(string userLogin, string selectedEntity = "")
        {
            db = new Database(userLogin);
            _userLogin = userLogin;
            _selectedEntity = selectedEntity;
            entitiesViewModel = new EntitiesViewModel(userLogin);
        }

        //public IEnumerable<Document> Documents
        //{
        //    get { return documents; }
        //    set
        //    {
        //        documents = value;
        //        OnPropertyChanged("Documents");
        //    }
        //}

        public DocumentView SelectedDocument
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

        /// <summary>
        /// gets AnnotationFields of each Document as a string to show it in MainWindow preview
        /// </summary>
        /// <param name="tableTitle">name of the table = Entity</param>
        /// <returns></returns>
        public List<DocumentView> GetDocumentsForMainWindow(string tableTitle)
        { 
            List<DocumentView> list = new List<DocumentView>();
            
            //TODO: need to test and finish
            var docs = db.GetEntityAnnotationFieldList(tableTitle);
            int amountOfRows = 0;
            List<string> values = new List<string>();

            //thi is to get the total amount of documents
            foreach (DictionaryEntry doc in docs)
            {                
                values = doc.Value as List<string>;
                amountOfRows = values.Count;
            }
            //array of documents
            string[] documentsAnnotationaField = new string[amountOfRows];
            int[] documentsId = new int[amountOfRows];

            foreach (DictionaryEntry doc in docs)
            {
                
                values = doc.Value as List<string>;
                for (int i=0; i < amountOfRows; i++)
                {
                    if (doc.Key.ToString() == "id")
                    {
                        documentsId[i] = Convert.ToInt32(values[i]);
                        continue;
                    }
                    documentsAnnotationaField[i] = documentsAnnotationaField[i] + String.Format("{0}: {1}\n", doc.Key, values[i]);
                    
                }
                
            }

            for (int i = 0; i < amountOfRows; i++)
            {
                DocumentView docView = new DocumentView(documentsId[i], tableTitle, documentsAnnotationaField[i]);
                list.Add(docView);
            }
            return list;
        }

        /// <summary>
        /// converts Documents ordered dictionary to the List of Fields in order to show it for creating/editing doc.
        /// </summary>
        /// <param name="tableTitle">name of the table = Entity</param>
        /// <returns></returns>
        public List<Field> GetFieldsList(string tableTitle)
        {
            List<Field> listOfDocs = new List<Field>();            
            var docs = db.GetFieldNameListOfEntity(tableTitle);
            List<string> lis = docs.ToList();

            for(int i=1; i < docs.Count(); i++)
            {
                Field field = new Field(lis[i]);
                listOfDocs.Add(field);
            }

            return listOfDocs;
        }
        
        public List<Field> GetDocumentByID(string tableTitle, int id)
        {
            List<Field> listOfFieldsValues = new List<Field>();
            var document = db.GetDocumentByID(tableTitle, id);
            Field doc_field;
            List<string> values = new List<string>();
            foreach (DictionaryEntry field in document)
            {
                values = field.Value as List<string>;
                for (int i = 0; i < values.Count(); i++)
                {
                    if (field.Key.ToString() == "id")
                    {
                        //documentsId[i] = Convert.ToInt32(values[i]);
                        continue;
                    }
                    //documentsAnnotationaField[i] = documentsAnnotationaField[i] + String.Format("{0}: {1}. ", doc.Key, values[i]);

                    doc_field = new Field(field.Key.ToString(), values[i].ToString());
                    listOfFieldsValues.Add(doc_field);
                }
                
            }
            return listOfFieldsValues;
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      //это окно для Документа, при помощи которого можно создать новый или отредактировать имеющийся
                      CreateInstanceOfEntityWindow createInstance = new CreateInstanceOfEntityWindow(_selectedEntity, _userLogin);

                      if (createInstance.ShowDialog() == true)
                      {
                          List<Field> list = createInstance.FieldsList.ItemsSource as List<Field>;
                          OrderedDictionary dic = new OrderedDictionary();

                          foreach(Field field in list)
                          {
                              dic.Add(field.Title, field.FValue);
                          }
                          Document document = new Document(dic);

                          db.AddDocument(_selectedEntity, document);
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
                  (editCommand = new RelayCommand((o) =>
                  {
                      // если ни одного объекта не выделено, выходим
                      if (SelectedDocument == null) return;
                      // получаем выделенный объект. SelectedDocument - это выделенный Документ в главном окне.
                      //Field documentField = selectedItem as Field;

                      CreateInstanceOfEntityWindow createInstance = 
                            new CreateInstanceOfEntityWindow(SelectedDocument, _userLogin);

                      if (createInstance.ShowDialog() == true)
                      {
                          List<Field> list = createInstance.FieldsList.ItemsSource as List<Field>;
                          OrderedDictionary dic = new OrderedDictionary();

                          foreach (Field field in list)
                          {
                              dic.Add(field.Title, field.FValue);
                          }
                          Document document = new Document(dic);

                          db.EditDocument(_selectedEntity, SelectedDocument.DocumentID, document);
                      }

                      //if (createInstance.ShowDialog() == true)
                      //{
                      //    //document.Fields = 
                      //    // получаем измененный объект
                      //    person = db.People.Find(createInstance.Person.ID_Person);
                      //    //person = db.People.FirstOrDefault(i => i.ID_Person == createInstance.Person.ID_Person );
                      //    if (person != null)
                      //    {
                      //        person.FirstName = createInstance.Person.FirstName;
                      //        person.LastName = createInstance.Person.LastName;
                      //        person.DateOfBirth = createInstance.Person.DateOfBirth;
                      //        person.Address = createInstance.Person.Address;
                      //        person.EyeColor = createInstance.Person.EyeColor;
                      //        person.Telephone = createInstance.Person.Telephone;

                      //        db.Entry(person).State = EntityState.Modified;
                      //        db.SaveChanges();
                      //    }
                      //}
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

                      //Person person = selectedItem as Person;
                      //db.People.Remove(person);
                      //db.SaveChanges();
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
