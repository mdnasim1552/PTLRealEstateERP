using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptAllSalarySummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetMonth();
                lnkOk_Click(null,null);
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetMonth()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            this.ddlmon.DataTextField = "mnam";
            this.ddlmon.DataValueField = "yearmon";
            this.ddlmon.DataSource = ds2.Tables[0];
            this.ddlmon.DataBind();
            //this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("dd-MM-yyyy").Trim();
            this.ddlmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();
        }

        protected void rbtnAtten_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelVisivility();
        }

        private void PanelVisivility()
        {
            int rdbutton = this.rbtnAtten.SelectedIndex;
            switch (rdbutton)
            {
                case 0:
                    this.PnlBankSumary.Visible = true;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;
                    break;
                case 1:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = true;
                    this.PnlNetComparison.Visible = false;

                    break;
                case 2:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = true;

                    break;
                default:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;

                    break;
            }
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.PanelVisivility();
            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.GetBankSummary();

                    break;

                case 1:
                    this.GetModeOfPayment();
                    break;

                case 2:
                    this.GetNetPayComparison();
                    break;

                default:
                    break;
            }
            

        }

        private void GetNetPayComparison()
        {
            string comcod = this.GetComCode();
            string prevmon = "";
            string monthid = this.ddlmon.SelectedValue.ToString();
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).AddMonths(-1).ToString("yyyyMM");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).ToString("yyyyMM");
                    break;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTNETCOMPARISONMONTHWISE", monthid, prevmon, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblmondesc"] = ds1.Tables[1];
            this.Data_bind();
        }

        private void GetBankSummary()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTBANKSUMMARY", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            this.Data_bind();
        }
        private void GetModeOfPayment()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTBANKDEATILSSUMMARY", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblbankdesc"] =ds1.Tables[1];
            this.Data_bind();
            //this.exportexcel();
        }

        private void exportexcel()
        {
    
        }

        private void Data_bind()
        {
            string comcod = this.GetComCode();

            DataTable dt = (DataTable)Session["tblSalSummary"];
            
            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:


                    this.GvBankSummary.DataSource = dt;
                    this.GvBankSummary.DataBind();
                    Session["Report1"] = GvBankSummary;
                    ((HyperLink)this.GvBankSummary.HeaderRow.FindControl("hlbtntbCdataExcelbank")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                case 1:
                    DataTable dtp = (DataTable)Session["tblbankdesc"];
                    if (dtp == null)
                    {
                        return;
                    }
                    int i, j;
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 2;
                  
                    for (i = 0; i < dtp.Rows.Count; i++)
                    {

                       
                        this.GvModPayment.Columns[j].HeaderText = dtp.Rows[i]["bankname"].ToString();

                        j++;
                       
                    }

                    this.GvModPayment.DataSource = dt;
                    this.GvModPayment.DataBind();

                    Session["Report1"] = GvModPayment;
                    ((HyperLink)this.GvModPayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case 2:

                   
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 1;
                    DataTable dtmon = (DataTable)Session["tblmondesc"];
                    if (dtmon == null)
                    {
                        return;
                    }
                    for (i = 0; i < dtmon.Rows.Count; i++)
                    {


                        this.GvNetComparison.Columns[j].HeaderText = dtmon.Rows[i]["monname"].ToString();

                        j++;

                    }

                    this.GvNetComparison.DataSource = dt;
                    this.GvNetComparison.DataBind();

                    Session["Report1"] = GvNetComparison;
                    ((HyperLink)this.GvNetComparison.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                default:
                    break;

            }

        }
    }
}