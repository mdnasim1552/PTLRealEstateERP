using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace RealERPLIB
{
    public class SystemDataAccess
    {
        public OleDbConnection m_Conn;
        private Hashtable m_Erroobj;
        DataTable dtreturn = new DataTable();
        //public string DBConnstr = "Data Source=AST05\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;Integrated Security=SSPI";
        //public const string DBConnstr = "Data Source=AST01\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;User ID=sa;Password=";
        public SystemDataAccess()
        {
            m_Conn = new OleDbConnection(this.DBConnstr());
            m_Erroobj = new Hashtable();
        }

        public SystemDataAccess(string mDBName)
        {
            m_Conn = new OleDbConnection(this.DBConnstr());
            m_Conn.ConnectionString = m_Conn.ConnectionString.Replace("ASTREALERPDB", mDBName + "DB");

        }

        private string DBConnstr()
        {
            //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/RCRMWEB");
            //System.Configuration.KeyValueConfigurationElement ii = rootWebConfig1.AppSettings.Settings["DBConnstr"];         
            string ii;
            System.Configuration.Configuration Config1;
            if (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath != null)
            {
                ii = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.ToString();
                Config1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ii);
            }
            else
            {
                //ii = System.Windows.Forms.Application.StartupPath.ToString();
                ii = System.Windows.Forms.Application.ProductName + ".EXE";
                ii = System.IO.Path.Combine(Environment.CurrentDirectory, ii);
                Config1 = System.Configuration.ConfigurationManager.OpenExeConfiguration(ii);

            }
            ii = Config1.AppSettings.Settings["OLEDBConnstr"].Value.ToString().Trim();
            return ii;
        }
        public Hashtable ErrorObject
        {
            get
            {
                return this.m_Erroobj;
            }
        }


        private void SetError(Exception ex)
        {
            this.m_Erroobj["Src"] = ex.Source;
            this.m_Erroobj["Msg"] = ex.Message;
            this.m_Erroobj["Location"] = ex.StackTrace;
        }

        public DataSet GetDataSet(string SQL)
        {

            try
            {
            
                OleDbDataAdapter adp = new OleDbDataAdapter();
                OleDbCommand Cmd = new OleDbCommand();
                Cmd.CommandText = SQL;
                adp.SelectCommand = Cmd;
                Cmd.Connection = this.m_Conn;
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }

            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }


        }
    }
}
