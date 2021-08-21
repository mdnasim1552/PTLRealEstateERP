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
    public class DataAccessOLDB
    {
        
        public OleDbConnection m_Conn;
        public Hashtable m_Erroobj;
        DataTable dtreturn = new DataTable();
        //public string DBConnstr = "Data Source=AST05\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;Integrated Security=SSPI";
        //public const string DBConnstr = "Data Source=AST01\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;User ID=sa;Password=";
        public DataAccessOLDB()
        {  // Jet OLEDB:Database Password=asit123"
            // "F:/Emdad Project/ASTRealERP/COMPDB/ASITCOMPDB.mdb"

            string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();//.ApplicationVirtualPath.ToString();
            string appvirpath = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            // System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();//.ApplicationVirtualPath.ToString();
            // apppath = apppath + "COMPDB/ASITCOMPDB.mdb";
            apppath = apppath + "COMPDB\\ASITCOMPDB.mdb";
            m_Conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + apppath + ";Jet OLEDB:Database Password=@*asit*%#");
           

            //m_Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + apppath + ";Jet OLEDB:Database Password=@*asit*%#");
                            
          
            m_Erroobj = new Hashtable();
        }
        //private string DBConnstr()
        //{
        //    //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/RCRMWEB");
        //    //System.Configuration.KeyValueConfigurationElement ii = rootWebConfig1.AppSettings.Settings["DBConnstr"];         
        //    string ii;
        //    System.Configuration.Configuration Config1;
        //    if (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath != null)
        //    {
        //        ii = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.ToString();
        //        Config1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ii);
        //    }
        //    else
        //    {
        //        //ii = System.Windows.Forms.Application.StartupPath.ToString();
        //        ii = System.Windows.Forms.Application.ProductName + ".EXE";
        //        ii = System.IO.Path.Combine(Environment.CurrentDirectory, ii);
        //        Config1 = System.Configuration.ConfigurationManager.OpenExeConfiguration(ii);

        //    }
            
        //    ii = Config1.AppSettings.Settings["DBConnstr"].Value.ToString().Trim();
        //    ii = ii + SetPassword();
        //    return ii;
        //}


        public Hashtable ErrorObject
        {
            get
            {
                return this.m_Erroobj;
            }
        }


        public DataTable GetTable(string SQL)
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
                return ds.Tables[0];
            }

            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public DataSet GetDataSet(String SQL)
        {
            try
            {
                OleDbDataAdapter adp = new OleDbDataAdapter(SQL, this.m_Conn);
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
        public DataTable GetTable(OleDbCommand Cmd)
        {
            try
            {
                OleDbDataAdapter adp = new OleDbDataAdapter();
                adp.SelectCommand = Cmd;
                Cmd.Connection = this.m_Conn;
                DataTable dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public DataSet GetDataSet(OleDbCommand Cmd)
        {
            try
            {
                OleDbDataAdapter adp = new OleDbDataAdapter();
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
        public Boolean ExecuteCommand(string SQL)
        {
            
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = SQL;
            cmd.Connection = this.m_Conn;

            try
            {
                this.m_Conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return false;
            }
            finally
            {
                this.m_Conn.Close();
            }
        }
        public Boolean ExecuteCommand(OleDbCommand cmd)
        {
            cmd.Connection = this.m_Conn;
            try
            {
                this.m_Conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return false;
            }
            finally
            {
                this.m_Conn.Close();
            }
        }
        

        private void SetError(Exception ex)
        {
            this.m_Erroobj["Src"] = ex.Source;
            this.m_Erroobj["Msg"] = ex.Message;
            this.m_Erroobj["Location"] = ex.StackTrace;
        }

        ////private string SetPassword()
        ////{
        ////    //AccessDataSource ads = new AccessDataSource();
        ////    //ads.DataFile = "~/App_Data/ASITsecurity.mdb";

        ////    //ads.SelectCommand = "SELECT * from passWord2";

        ////    //// Add the AccessDataSource to the Page.Controls collection.
        ////    //// Page.Controls.Add(ads);
        ////    ////string ss = ((DataView)ads.Select(DataSourceSelectArguments.Empty))[0]["pass"].ToString();
        ////    //string ss = ((DataView)ads.Select(DataSourceSelectArguments.Empty))[0]["pass"].ToString();

        ////    return ";Jet OLEDB:Database Password=1234";// ss;


        ////}
        //private string strConnection()
        //{
        //    AccessDataSource ds = new AccessDataSource();
        //    string ff = ds.DataFile = "~/App_Data/ASITEBusDB.mdb";
        //    ff = ff.Replace("~", "EBusWeb");
            
        //   SqlDataSource sq = new SqlDataSource();
        //   string ccs= sq.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ff + "; Jet OLEDB:Database Password=1234";
        //    //sq.ProviderName = "System.Data.OleDb";
        //   return ccs;
        //}


        
    }
}
