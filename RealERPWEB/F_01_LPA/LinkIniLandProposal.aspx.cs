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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_01_LPA
{
    public partial class LinkIniLandProposal : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Development Proposal";
                this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
                this.ShowProjectInfo();


            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }







        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptPriLandProposal();//RptFeaProject();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;
            TextObject txtDu = rpcp.ReportDefinition.ReportObjects["txtDu"] as TextObject;
            txtDu.Text = "Date: " + System.DateTime.Now.ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString(); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void ShowProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ProjectCode = this.Request.QueryString["pactcode"].ToString(); ;
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "PRILDPROJECTINFO", ProjectCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }
            this.ViewState["tblfeaprj"] = ds1.Tables[1];
            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();


        }

        protected void gvProjectInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvItmCode = (Label)e.Row.FindControl("lblgvItmCode");
                Label lgcResDesc1 = (Label)e.Row.FindControl("lgcResDesc1");
                Label lgvgval = (Label)e.Row.FindControl("lgvgval");
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");
                TextBox txtgvname1 = (TextBox)e.Row.FindControl("txtgvVal2");
                TextBox txtgvname2 = (TextBox)e.Row.FindControl("txtgvVal3");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(code, 3) == "030")
                {

                    txtgvname1.Visible = false;
                    txtgvname2.Visible = false;

                }
                if (code == "03100")
                {


                    txtgvname.ReadOnly = true;
                    txtgvname1.ReadOnly = true;
                    txtgvname2.ReadOnly = true;
                    lblgvItmCode.Style.Add("color", "blue");
                    lgcResDesc1.Style.Add("color", "blue");
                    lgvgval.Style.Add("color", "blue");
                    txtgvname.Style.Add("color", "blue");
                    txtgvname1.Style.Add("color", "blue");
                    txtgvname2.Style.Add("color", "blue");

                }

            }
        }
    }
}