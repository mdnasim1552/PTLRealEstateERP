using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Security.Application;

namespace RealEntity
{
    public static class SerializeHelper
    {
        public static string SerializeObject(object value)
        {
            if (value == null)
                return "null";

            System.Type type = value.GetType();
            //if (type == typeof(string))
            //    return AntiXss.JavaScriptEncode((string)value);
            if (type == typeof(bool))
                return ((bool)value).ToString().ToLowerInvariant();
            else if (type == typeof(DateTime))
                return "'" + ((DateTime)value).ToShortDateString() + " " + ((DateTime)value).ToShortTimeString() + "'";
            else
                return value.ToString();
        }
    }
}
