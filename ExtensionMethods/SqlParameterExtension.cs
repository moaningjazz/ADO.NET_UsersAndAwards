using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAndAwards.ExtensionMethods
{
    public static class SqlParameterExtension
    {
        //Метод расширения для более быстрого создания Sql параметров
        public static SqlParameter CreateParameter<T>(this SqlParameter sqlPrameter, 
            DbType dbType, 
            ParameterDirection direction,
            string parameterName,
            T value)
        {
            return new SqlParameter()
            {
                DbType = dbType,
                Direction = direction,
                ParameterName = parameterName,
                Value = value
            };
        }
    }
}
