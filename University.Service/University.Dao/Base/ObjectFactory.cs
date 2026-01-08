using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;

namespace University.Dao.Base
{
    public class ObjectFactory<T> where T : new()
    {
        public DataTable ConvertToTable(List<T> objCollection)
        {
            DataTable table = new DataTable();

            Type objectType = typeof(T);

            PropertyInfo[] objectProperties = objectType.GetProperties();

            //create a column for each property in the class
            foreach (PropertyInfo propertyItem in objectProperties)
            {
                table.Columns.Add(new DataColumn(propertyItem.Name, propertyItem.PropertyType));
            }

            foreach (T item in objCollection)
            {
                //create a new row based on the table structure we just created
                DataRow row = table.NewRow();

                //copy object data to the datarow
                foreach (PropertyInfo propertyItem in objectProperties)
                {
                    row[propertyItem.Name] = propertyItem.GetValue(item, null);
                }

                //add row to the table
                table.Rows.Add(row);
            }

            return table;
        }

        public List<T> ConvertToList(DataTable datatable)
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();

                foreach (DataColumn DataColumn in datatable.Columns)
                {
                    columnsNames.Add(DataColumn.ColumnName);
                    Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject(row, columnsNames));
                }

                return Temp;
            }
            catch
            {
                return Temp;
            }

        }

        private T getObject(DataRow row, List<string> columnsName)
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }
    }
}
