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
namespace RealERPWEB.F_02_Fea
{
    public partial class RptProjectrevRevenue : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ShowReport();
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
            string prjname = this.lblActDesc.Text.Trim();
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.rptProPricing();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.lblHeader.Text;
                string eventdesc = "Print Report";
                //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            }

            rpcp.SetDataSource((DataTable)Session["tblfeaprj"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void ShowReport()
        {
            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["actcode"].ToString().Trim();
            this.lblActDesc.Text = Request.QueryString["actdesc"].ToString().Trim();
            string mRptGroup = "12";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPTPROPRICING", pactcode, mRptGroup, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaProPricing.DataSource = null;
                this.gvFeaProPricing.DataBind();
                return;
            }
            Session["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }
            return dt1;

        }


        private void Data_Bind()
        {
            this.gvFeaProPricing.DataSource = (DataTable)Session["tblfeaprj"]; ;
            this.gvFeaProPricing.DataBind();
        }


        protected void gvFeaProPricing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgUnitn");
                Label qty = (Label)e.Row.FindControl("lgvQty");
                Label sizeSft = (Label)e.Row.FindControl("lgSize");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");
                Label pamt = (Label)e.Row.FindControl("lgvPamt");
                Label uamt = (Label)e.Row.FindControl("lgvUtility");
                Label camt = (Label)e.Row.FindControl("lgvCamt");
                Label otham = (Label)e.Row.FindControl("lgvOthers");
                Label tamt = (Label)e.Row.FindControl("lgvTamt");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 1) == "B" || ASTUtility.Right(code, 1) == "C")
                {

                    groupdesc.Font.Bold = true;
                    qty.Font.Bold = true;
                    sizeSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    pamt.Font.Bold = true;
                    uamt.Font.Bold = true;
                    camt.Font.Bold = true;
                    camt.Font.Bold = true;
                    otham.Font.Bold = true;
                    tamt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }

            }
        }


    }
}