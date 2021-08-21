using System;

using System.Collections.Generic;
using System.Collections;
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
using RealERPLIB;
using RealEntity;

using System.Web.Services;
using System.Data.SqlClient;

namespace RealERPWEB
{
    public partial class SearchMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [WebMethod]
        public static string[] GetCustomers(string prefix)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select ContactName, CustomerId from Customers where ContactName like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["ContactName"], sdr["CustomerId"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }

        protected void Submit(object sender, EventArgs e)
        {
            string customerName = Request.Form[txtSearch.UniqueID];
            string customerId = Request.Form[hfCustomerId.UniqueID];
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + customerName + "\\nID: " + customerId + "');", true);
        }
    }
}