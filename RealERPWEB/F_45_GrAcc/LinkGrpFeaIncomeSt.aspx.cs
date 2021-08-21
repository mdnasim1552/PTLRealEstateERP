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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpFeaIncomeSt : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowReport();
            ((Label)this.Master.FindControl("lblTitle")).Text = "Income Statement";
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            string prjname = this.lblActDesc.Text.Trim();
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.rptFeaInSt();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = "Project Name: " + prjname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            }

            rpcp.SetDataSource((DataTable)Session["tblfeaInSt"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void ShowReport()
        {
            Session.Remove("tblfeaInSt");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            this.lblActDesc.Text = Request.QueryString["actdesc"].ToString().Trim();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPTINCOMESTMENT", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaIncomeSt.DataSource = null;
                this.gvFeaIncomeSt.DataBind();
                return;
            }
            Session["tblfeaInSt"] = ds2.Tables[0];

            this.Data_Bind();



        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grpcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grpcod"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grpcod"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grpcod"].ToString();
                }

            }
            return dt1;

        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfeaInSt"];
            this.gvFeaIncomeSt.DataSource = dt;
            this.gvFeaIncomeSt.DataBind();

        }




        protected void gvFeaIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string comcod = this.Request.QueryString["comcod"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                HyperLink oamt = (HyperLink)e.Row.FindControl("HLgOamt");
                HyperLink tamt = (HyperLink)e.Row.FindControl("HLgvrevisedvalue");
                Label Oper = (Label)e.Row.FindControl("lgvOpar");
                Label Rper = (Label)e.Row.FindControl("lgvRpar");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    oamt.Font.Bold = true;
                    tamt.Font.Bold = true;
                    Oper.Font.Bold = true;
                    Rper.Font.Bold = true;
                    //groupdesc.Style.Add("text-align", "right");


                }
                if (ASTUtility.Right(code, 4) == "0000")
                {

                    groupdesc.Font.Bold = true;
                    oamt.Font.Bold = true;
                    tamt.Font.Bold = true;
                    //groupdesc.Style.Add("text-align", "right");


                }

            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvrevisedvalue");
            string InfCode = ((Label)e.Row.FindControl("lgvInfoCode")).Text;
            if (InfCode == "000000000000")
            {

                string ActDesc = Request.QueryString["actdesc"].ToString().Trim();
                string pactcode = Request.QueryString["actcode"].ToString().Trim();
                hlink1.NavigateUrl = "LinkGrpFeaProjectrevRevenue.aspx?comcod=" + comcod + "&actcode=" + pactcode + "&actdesc=" + ActDesc;
            }

            HyperLink hlink2 = (HyperLink)e.Row.FindControl("HLgOamt");
            if (InfCode == "000000000000")
            {

                string ActDesc = Request.QueryString["actdesc"].ToString().Trim();
                string pactcode = Request.QueryString["actcode"].ToString().Trim();
                hlink2.NavigateUrl = "LinkGrpFeaRevenueACost.aspx?comcod=" + comcod + "&actcode=" + pactcode + "&actdesc=" + ActDesc + "&Type=Revenue";
            }



            else if (InfCode == "60AAAAAAAAAA")
            {

                string ActDesc = Request.QueryString["actdesc"].ToString().Trim();
                string pactcode = Request.QueryString["actcode"].ToString().Trim();
                hlink2.NavigateUrl = "LinkGrpFeaRevenueACost.aspx?comcod=" + comcod + "&actcode=" + pactcode + "&actdesc=" + ActDesc + "&Type=Cost";
            }


        }
    }
}