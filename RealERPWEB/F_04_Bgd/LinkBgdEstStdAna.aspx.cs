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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{
    public partial class LinkBgdEstStdAna : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                this.lblValProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
                this.lblValWorkName.Text = this.Request.QueryString["isirdesc"].ToString();
                this.lblValFloor.Text = this.Request.QueryString["flrdes"].ToString();
                this.ShowData();


            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void ShowData()
        {
            ViewState.Remove("tblestana");
            string comcod = this.GetComCode();
            string pactocde = this.Request.QueryString["pactcode"].ToString();
            string isircode = this.Request.QueryString["isircode"].ToString();
            string flrcod = this.Request.QueryString["flrcod"].ToString();
            string CallType = (this.chkAllSInf.Checked) ? "GETBGDALLEWORK" : "GETBGDEWORK";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", CallType, pactocde, isircode, flrcod, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvEstAna.DataSource = null;
                this.gvEstAna.DataBind();

            }
            ViewState["tblestana"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();
        }

        private void Data_Bind()
        {

            this.gvEstAna.DataSource = (DataTable)ViewState["tblestana"];
            this.gvEstAna.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblestana"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFtoWeight")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toweight)", "")) ?
                       0 : dt.Compute("sum(toweight)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFtotalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toqty)", "")) ?
                                0 : dt.Compute("sum(toqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFgtotalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gtqty)", "")) ?
                         0 : dt.Compute("sum(gtqty)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblestana"];

            var list = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.LandBgdStdAna>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.rptFloorResource", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("projectName", "Project Name:-" + ((Label)this.lblValProjectName).Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("workName", "Work Name:-" + ((Label)this.lblValWorkName).Text));
            Rpt1.SetParameters(new ReportParameter("floorName", "Floor Name:-" + ((Label)this.lblValFloor).Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Constraction Budget 02";
                string eventdesc = "Print Report";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string isircode = this.Request.QueryString["isircode"].ToString();
            string flrcod = this.Request.QueryString["flrcod"].ToString();

            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblestana"];
            bool result = false;
            foreach (DataRow dr in dt.Rows)
            {
                string wrkcode = dr["wrkcode"].ToString();
                string rsircode = dr["rsircode"].ToString();
                string length = dr["lnght"].ToString();
                string qty = dr["qty"].ToString();
                string weight = dr["weight"].ToString();
                string tobase = dr["tbase"].ToString();
                string wastage = dr["wastage"].ToString();
                string bnumber = dr["bnumber"].ToString();
                string gtqty = dr["gtqty"].ToString();


                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "INSORUPDATBGDEWRK", pactcode, isircode, flrcod, wrkcode,
                        rsircode, length, qty, weight, tobase, wastage, bnumber, gtqty, "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                    return;
                }
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            this.ShowData();




        }










        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string wrkcode = dt1.Rows[0]["wrkcode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["wrkcode"].ToString() == wrkcode)
                    dt1.Rows[j]["wrkdesc"] = "";
                wrkcode = dt1.Rows[j]["wrkcode"].ToString();

            }



            return dt1;

        }



        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblestana"];
            int rowindex;
            for (int i = 0; i < this.gvEstAna.Rows.Count; i++)
            {
                string bnumber = ((TextBox)this.gvEstAna.Rows[i].FindControl("txtbasenumber")).Text.Trim();
                double Length = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvlength")).Text.Trim());
                double Qty = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvqty")).Text.Trim());
                double weight = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvweight")).Text.Trim());
                double tobase = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvtobase")).Text.Trim());
                double Wastage = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvwastage")).Text.Trim());
                double toweight = Length * Qty * weight;
                double toqty = Length * Qty * weight * tobase;
                double wasqty = Length * Qty * weight * tobase * Wastage * 0.01;
                double gqty = toqty + wasqty;

                rowindex = (gvEstAna.PageIndex) * gvEstAna.PageSize + i;
                dt.Rows[rowindex]["bnumber"] = bnumber;
                dt.Rows[rowindex]["lnght"] = Length;
                dt.Rows[rowindex]["qty"] = Qty;
                dt.Rows[rowindex]["weight"] = weight;
                dt.Rows[rowindex]["toweight"] = toweight;
                dt.Rows[rowindex]["tbase"] = tobase;
                dt.Rows[rowindex]["toqty"] = toqty;
                dt.Rows[rowindex]["wastage"] = Wastage;
                dt.Rows[rowindex]["gtqty"] = gqty;
            }


            ViewState["tblestana"] = dt;

        }

        protected void lnkbtnTotql_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowData();


        }
    }

}

