using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_21_MKT
{
    public partial class AdvancedSearchFilter : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Advanced Search Filter";
              
            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string Empid = ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
            if (userrole == "1")
            {
                Empid = "%";
            }
            string comcod = this.GetComeCode();
            string Country =  "%";
            string Dist = "%";
            string Zone =  "%";
            string PStat = "%";
            string Area = "%";
            string Block =  "%";
            string Pri = "%";
            string Status = "%";
            string Other = this.ddlOther.SelectedValue.ToString();
            string TxtVal = "%" + this.txtVal.Text + "%";
            string frmdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string srchempid = "%";

            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                 Pri, Status, Other, TxtVal, todate, srchempid);
        }
    }
}