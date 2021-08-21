using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using RealERPLIB;
namespace RealERPWEB.F_34_Mgt
{
    public partial class DataBackup : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand sqlcmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

            }
        }
        public SqlConnection GetConnection()
        {
            DataAccess da = new DataAccess();
            return da.m_Conn;
        }
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            //IF SQL Server Authentication then Connection String  
            //con.ConnectionString = @"Server=MyPC\SqlServer2k8;database=" + YourDBName + ";uid=sa;pwd=password;";  
            //con.ConnectionString = @"Server=SHOVON-PC\MSSQL2K12;database=ASITMFCTUREDB;uid=sa;pwd=;";

            //IF Window Authentication then Connection String  
            //con.ConnectionString = @"Server=SHOVON-PC\MSSQL2K12;database=ASITMFCTUREDB;Integrated Security=true;";
            con = GetConnection();
            string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();//
            string backupDIR = apppath + "DataBaseBackup";
            //string backupDIR = "D:\\BackupDB";
            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }
            try
            {
                con.Open();
                string db_name = GetDatabaseName();
                sqlcmd = new SqlCommand("backup database " + db_name + " to disk='" + backupDIR + "\\" + DateTime.Now.ToString("dd-MMM-yyyy_HHmmss") + ".Bak'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                lblError.Text = "Backup database successfully";
            }
            catch (Exception ex)
            {
                lblError.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
            }
        }
        public string GetDatabaseName()
        {
            var myConnectionString = con.ConnectionString;
            var builder = new System.Data.OleDb.OleDbConnectionStringBuilder(myConnectionString);
            var database = builder["Initial Catalog"];
            return database.ToString();
        }
    }
}