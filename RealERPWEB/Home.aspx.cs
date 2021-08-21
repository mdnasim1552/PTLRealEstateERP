using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RealERPLIB;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace RealERPWEB
{
    public partial class Home : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();
        DataTable tbl_component = new DataTable();
        DataTable tbl_tdwk = new DataTable();
        DataTable tbl_topactivity = new DataTable();
        DataTable tbl_offlineUser = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "ERP DASHBOARD";
                (this.Master.FindControl("mySidenav")).Visible = false;
                (this.Master.FindControl("DDPrintOpt")).Visible = false;
                (this.Master.FindControl("lnkPrint")).Visible = false;
                GetModuleList();


                if (Session["sesspid"] == null)
                {
                    Session["sesspid"] = "0";
                }

            }
        }


        private void GetModuleList()
        {

            DataTable dt = (DataTable)Session["dsmodule"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            //  DataSet ds2 = ulogin.GetTransInfo(comcod, "SP_UTILITY_USER_DASHBOARD", "GETMODULELIST", usrid, "", "", "", "", "", "", "", "");
            string component = "";
            foreach (DataRow row in dt.Rows)
            {
                string bgcolor = row["bgcolor"].ToString() == "" ? "bg-blue1" : row["bgcolor"].ToString();
                string url = this.ResolveUrl("~/Index.aspx" + row["qrytype"]);
                component += "<div class='col-12 col-sm-3 col-lg-3' ><a href ='" + url + "' ><div class='card-metric' style='cursor:pointer;'><div class='metric " + bgcolor + " badge text-white'><div class='has-badge'><h3><span class='badgeContent'>" +
                    row["menuname"] + "</span><img class='boximg'  src='" + row["imgpath"] + "'/></h3></div></div></div></a></div>";
            }
            this.moduleload.InnerHtml = component;

        }
    }
}



