using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Reflection;

namespace WindowsFormsApp1
{
  public  static class comm
    {
        public  enum DataTyte { BOOL=0, WORD=1,INT16=2,INT32=3,REAL=4,STRING=5 }

        public  static readonly string Connection = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.ToString();
        public static readonly int MaxCount =int.Parse(ConfigurationManager.AppSettings["MaxCount"]);
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)

        {

            var props = typeof(T).GetProperties();

            var dt = new DataTable();

            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            if (collection.Count() > 0)

            {

                for (int i = 0; i < collection.Count(); i++)

                {

                    ArrayList tempList = new ArrayList();

                    foreach (PropertyInfo pi in props)

                    {

                        object obj = pi.GetValue(collection.ElementAt(i), null);

                        tempList.Add(obj);

                    }

                    object[] array = tempList.ToArray();

                    dt.LoadDataRow(array, true);

                }

            }

            return dt;

        }



    }
}
