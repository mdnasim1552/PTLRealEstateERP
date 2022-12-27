using System;
using System.Linq;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_12_Inv
{
    public partial class RptProjectStockEva : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "acc" ? "MATERIALS STOCK REPORT " : "MATERIALS STOCK REPORT INVENTORY");

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterial();
                if (Request.QueryString.AllKeys.Contains("prjcode") && this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
            }

        }

        private string Complength()
        {
            string comcod = this.GetCompCode();
            string Complength = "";
            switch (comcod)
            {
                // case "3101":
                case "3348":
                    Complength = "Length";
                    break;

                default:
                    Complength = "";

                    break;
            }

            return Complength;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.Data_Bind();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStockEva();
        }

        private void MatStockEva()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string gp = this.chkResourcelist.SelectedValue.ToString().Trim();

            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_MAT_STOCK", "GETSTOCKVALUATIONMATWISE", pactcode, "", "", "", "", "", "", "", "");

        }


    private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.chkResourcelist.DataTextField = "rsirdesc";
            this.chkResourcelist.DataValueField = "rsircode";
            this.chkResourcelist.DataSource = ds1.Tables[0];
            this.chkResourcelist.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.chkResourcelist.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
            }

            ds1.Dispose();


        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string length = this.Complength();
            string userid = hst["usrid"].ToString();
            string ctype = this.CompCallType();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", ctype, serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.ddlProName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
            }

        }

        private string CompCallType()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                case "3101":
                case "2325":
                case "3325":
                    ctype = "GETPURPROJECTNAMELEISURE";
                    break;
                default:
                    ctype = "GETPURPROJECTNAME";
                    break;
            }
            return ctype;
        }

        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }

    }
}