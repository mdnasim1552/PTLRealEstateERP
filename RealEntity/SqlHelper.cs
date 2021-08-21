using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;

namespace RealEntity
{
    public sealed  class SqlHelper
    {
        //public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        //{
        //    if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
        //    if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

        //    // If we receive parameter values, we need to figure out where they go
        //    if ((parameterValues != null) && (parameterValues.Length > 0))
        //    {
        //        SqlParameter[] commandParameters = GetSpParameterSet(connectionString, spName);

        //        AssignParameterValues(commandParameters, parameterValues);

        //        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        //    }
        //    else
        //    {
        //        // Otherwise we can just call the SP without params
        //        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        //    }
        //}
    }
}
