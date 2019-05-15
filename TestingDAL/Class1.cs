using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDAL
{
    public class Class1
    {
        private static readonly string Connection = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.ToString();

        public static DBOperator GetDBOperator()
        {
            try
            {
                //string 
                switch (DBOperatorFactory.DBType.ToUpper())
                {
                    case "SQLSERVER":
                        return new SqlDBOperator(DBOperatorFactory.Connection);
                    case "ORACLE":
                        return null;
                    case "OLEDB":
                        return null;
                    case "ODBC":
                        return null;
                    default:

                        return new SqlDBOperator(DBOperatorFactory.Connection);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
