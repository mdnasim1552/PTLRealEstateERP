using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace RealERPLIB
{
    public class DataAccess
    {
        public SqlConnection m_Conn;
        private Hashtable m_Erroobj;
        //public string DBConnstr = "Data Source=AST05\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;Integrated Security=SSPI";
        //public const string DBConnstr = "Data Source=AST01\\MSSQLSVR2K5;initial Catalog=ASTSOFTDB;User ID=sa;Password=";

        public DataAccess()
        {
            m_Conn = new SqlConnection(this.DBConnstr());
            m_Erroobj = new Hashtable();
        }
   
        public DataAccess(string mDBName) 
        {
            m_Conn = new SqlConnection(this.DBConnstr());
            m_Conn.ConnectionString = m_Conn.ConnectionString.Replace("ASITINTERIORDB", mDBName + "DB");
        
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
            ii = Config1.AppSettings.Settings["DBConnstr"].Value.ToString().Trim();
            return ii;
        }
        public DataSet GetDataSetTicket(SqlCommand Cmd)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = Cmd;

                Cmd.CommandTimeout = 120;
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
        public Hashtable ErrorObject
        {
            get
            {
                return this.m_Erroobj;
            }
        }
        public DataTable GetTable(string SQl)
        {
            //for string : have to edit with proper code
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
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
            //for string : have to edit with proper code
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(SQL, this.m_Conn);
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
        public DataTable GetTable(SqlCommand Cmd)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
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
        public DataSet GetDataSet(SqlCommand Cmd)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = Cmd;
                Cmd.CommandTimeout = 0;
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
            SqlCommand cmd = new SqlCommand();     
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
        public Boolean ExecuteCommand(SqlCommand cmd)
        {
            cmd.Connection = this.m_Conn;
            cmd.CommandTimeout = 120;
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
        public SqlDataReader ExecuteReader(SqlCommand cmd) 
        {
            cmd.Connection = this.m_Conn;
            cmd.CommandTimeout = 120;
            try
            {
                this.m_Conn.Close();
                this.m_Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
            //31222083
        }
        private void SetError(Exception ex)
        {
             
                this.m_Erroobj["Src"] = ex.Source;
                this.m_Erroobj["Msg"] = ex.Message;
                this.m_Erroobj["Location"] = ex.StackTrace;                               
        }
    }
}
