using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace University.Dto.Base
{
    public abstract class Mapper<T>
    {
        protected abstract T PopulateItem(IDataRecord dr);

        public List<T> MapAll(IDataReader dbReader)
        {
            List<T> lstMap = new List<T>();

            while (dbReader.Read())
            {
                lstMap.Add(PopulateItem(dbReader));
            }

            return lstMap;
        }

        public T Map(IDataReader dbReader)
        {
            T objT = default(T);
            while (dbReader.Read())
            {
                objT = PopulateItem(dbReader);
            }

            if (objT is T)
                return (T)objT;
            else
                return default(T);
        }

        public void MapProperty(object obj, string strPropertyName, object objValue)
        {
            Type t = obj.GetType();
            System.Reflection.PropertyInfo prInfo = t.GetProperty(strPropertyName);

            if ((prInfo != null) && prInfo.CanWrite)
            {
                Type prType = prInfo.PropertyType;

                if (prType.IsGenericType && (prType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    prType = prType.GetGenericArguments()[0];

                    if (prType == typeof(DateTime))
                    {
                        if (objValue != DBNull.Value)
                        {
                            objValue = Convert.ToDateTime(objValue);
                        }
                        else
                        {
                            objValue = null;
                        }
                    }
                }
                else if (prType == typeof(decimal))
                {
                    objValue = (objValue != DBNull.Value ? Convert.ToDecimal(objValue) : 0);
                }
                else if (prType == typeof(bool))
                {
                    objValue = (objValue != DBNull.Value ? Convert.ToBoolean(objValue) : false);
                }
                else if (prType == typeof(DateTime))
                {
                    objValue = Convert.ToDateTime(objValue);
                }
                else
                {
                    objValue = (objValue != DBNull.Value ? objValue.ToString() : String.Empty);
                }

                prInfo.SetValue(obj, objValue, null);
            }
        }

    }
}
