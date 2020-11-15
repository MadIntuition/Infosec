using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dnevnik
{
    class Database
    {
        private readonly string fileName;
        private readonly string connectionString;

        public Database(string fileName)
        {
            this.fileName = fileName;
            connectionString = $"Data Source={fileName};Version=3;";
        }

        public void CreateFile()
        {
            try
            {
                SQLiteConnection.CreateFile(fileName);
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool CreateNewEntity(string tableTitle, IEnumerable<string> fieldsNames)
        {
            string query = "select exists (select 1 from sqlite_master where type='table' and name=@name)";
            SQLiteParameter parameter = new SQLiteParameter("@name", tableTitle);

            bool exists = Convert.ToBoolean(ExecuteScalar(query, parameter));
            if (exists)
                return false;

            query = "create table " + tableTitle;// + " (" + string.Join(", ", fieldsNames.Select(x => x + " text").Prepend("id integer primary key autoincrement")) + ")";
            return ExecuteNonQuery(query) > 0;
        }

        public DataTable GetEntityFieldList(string tableTitle, int[] annotationFields)
        {
            string query = "select * from " + tableTitle;
            return ReadRows(query, annotationFields);
        }

        public IEnumerable<string> GetEntities()
        {
            string query = "select * from sqlite_master where type='table'";
            return ReadRows(query, 0).AsEnumerable().Select(row => row.Field<string>(0));
        }

        private DataTable ReadRows(string query, params int[] annotationFields)
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            for (int i = 0; i < annotationFields.Length; i++)
                            {
                                int colNum = annotationFields[i];
                                if (colNum >= reader.FieldCount)
                                    throw new ArgumentOutOfRangeException();
                                DataColumn column = new DataColumn(reader.GetName(colNum), reader.GetFieldType(colNum));
                                table.Columns.Add(column);
                            }
                        }

                        while (reader.Read())
                        {
                            DataRow row = table.NewRow();
                            for (int i = 0; i < annotationFields.Length; i++)
                            {
                                int colNum = annotationFields[i];
                                string name = reader.GetName(colNum);
                                object value = reader.GetValue(colNum);
                                row[name] = value;
                            }

                            table.Rows.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                return table;
            }
        }

        public bool AddDocument(string table, Document doc)
        {
            string query = "insert into " + table + " (" + string.Join(", ", doc.Fields.Keys.Cast<string>()) + ") values (" + string.Join(", ", Enumerable.Repeat("?", doc.Fields.Count)) + ")";
            SQLiteParameter[] parameters = new SQLiteParameter[doc.Fields.Count];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new SQLiteParameter(DbType.String, doc.Fields[i]);
            }
            return ExecuteNonQuery(query, parameters) > 0;
        }

        public bool EditDocument(string table, int id, Document doc)
        {
            string query = "update " + table + " set " + string.Join(", ", doc.Fields.Keys.Cast<string>().Select(x => x + "=?")) + " where id=@id";
            SQLiteParameter[] parameters = new SQLiteParameter[doc.Fields.Count + 1];

            parameters[0] = new SQLiteParameter("@id", id);
            for (int i = 1; i < parameters.Length; i++)
            {
                parameters[i] = new SQLiteParameter(DbType.String, doc.Fields[i - 1]);
            }
            return ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteDocument(string table, int id)
        {
            string query = "delete from " + table + " where id=@id";
            SQLiteParameter parameter = new SQLiteParameter("@id", id);
            return ExecuteNonQuery(query, parameter) > 0;
        }

        private int ExecuteNonQuery(string query, params SQLiteParameter[] parameters)
        {
            int res = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters);
                    MessageBox.Show(command.CommandText.ToString());
                    res = command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return res;
            }
        }

        private object ExecuteScalar(string query, params SQLiteParameter[] parameters)
        {
            object res = null;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddRange(parameters);
                    MessageBox.Show(command.CommandText.ToString());
                    res = command.ExecuteScalar();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return res;
            }
        }
    }
}
