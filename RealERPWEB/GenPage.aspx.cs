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

namespace RealERPWEB
{
    public partial class GenPage : System.Web.UI.Page
    {
        ProcessAccess genRpModule = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.ViewSection();


            }

        }

        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString();



            this.pnlBusinessPlan.Visible = (Type == "01") ? true : (Type == "All") ? true : false;
            this.pnlLandProcure.Visible = (Type == "02") ? true : (Type == "All") ? true : false;
            this.pnlpreconst.Visible = (Type == "03") ? true : (Type == "All") ? true : false;
            this.pnlbgd.Visible = (Type == "04") ? true : (Type == "All") ? true : false;
            //this.pnlfinance.Visible = (Type == "05") ? true : (Type == "All") ? true : false;
            this.pnlProPlan.Visible = (Type == "06") ? true : (Type == "All") ? true : false;
            this.pnlProImp.Visible = (Type == "07") ? true : (Type == "All") ? true : false;
            this.pnlInven.Visible = (Type == "08") ? true : (Type == "All") ? true : false;
            this.pnlCentral.Visible = (Type == "09") ? true : (Type == "All") ? true : false;
            this.pnlProCure.Visible = (Type == "10") ? true : (Type == "All") ? true : false;
            this.pnlAcc.Visible = (Type == "11") ? true : (Type == "All") ? true : false;
            //this.pnlMgtAcc.Visible = (Type == "12") ? true : (Type == "All") ? true : false;
            //this.pnlAudit.Visible = (Type == "13") ? true : (Type == "All") ? true : false;
            this.pnlMkt.Visible = (Type == "14") ? true : (Type == "All") ? true : false;
            this.pnlSales.Visible = (Type == "15") ? true : (Type == "All") ? true : false;
            this.pnlCR.Visible = (Type == "16") ? true : (Type == "All") ? true : false;
            this.pnlCC.Visible = (Type == "17") ? true : (Type == "All") ? true : false;
            // this.pnlReg.Visible = (Type == "18") ? true : (Type == "All") ? true : false;
            this.pnlFxtAst.Visible = (Type == "19") ? true : (Type == "All") ? true : false;
            // this.pnlDailyAct.Visible = (Type == "20") ? true : (Type == "All") ? true : false;
            this.pnlMis.Visible = (Type == "21") ? true : (Type == "All") ? true : false;
            this.pnlDoc.Visible = (Type == "22") ? true : (Type == "All") ? true : false;
            this.pnlHR.Visible = (Type == "23") ? true : (Type == "All") ? true : false;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();

            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataSet ds1 = this.genRpModule.GetTransInfo(comcod, "SP_REPORT_MIS", "GENRPTMODULEDES","","", "", "", "", "","","","");


            //ReportDocument rptstk = new RealERPRPT.R_26_Alert.RptGenModuleDesc();
            ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            ////txtCompany.Text = comnam;


            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;



            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //rptstk.SetDataSource(ds1.Tables[0]);

            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}