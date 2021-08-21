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
namespace RealERPWEB.F_19_Audit
{
    public partial class EntrySEC : System.Web.UI.Page
    {
        ProcessAccess SECData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ShowSecInfo();



            }

        }




        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }







        private void ShowSecInfo()

        {
            ViewState.Remove("tblper");
            string comcod = this.GetComeCode();
            DataSet ds1 = SECData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETSECINFO", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvSEC.DataSource = null;
                this.gvSEC.DataBind();
                return;
            }
            if (ds1.Tables[1].Rows.Count > 0)

            {
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["comtitle"].ToString();
            }
            ViewState["tblper"] = ds1.Tables[0];
            this.Data_DataBind();


        }

        private void Data_DataBind()
        {

            this.gvSEC.DataSource = (DataTable)ViewState["tblper"];
            this.gvSEC.DataBind();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblper"];
            for (int i = 0; i < this.gvSEC.Rows.Count; i++)
            {

                string sgval1 = (((CheckBox)gvSEC.Rows[i].FindControl("lblgvsgval1")).Checked) ? "True" : "False";
                string sgval2 = (((CheckBox)gvSEC.Rows[i].FindControl("lblgvsgval2")).Checked) ? "True" : "False";
                string rmrks = ((TextBox)gvSEC.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                dt.Rows[i]["ssecval1"] = sgval1;
                dt.Rows[i]["ssecval2"] = sgval2;
                dt.Rows[i]["rmrks"] = rmrks;


            }
            ViewState["tblper"] = dt;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataSet ds1 = SECData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETSECINFO", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvSEC.DataSource = null;
                this.gvSEC.DataBind();
                return;
            }
            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["comtitle"].ToString();
            }
            ViewState["tblper"] = ds1.Tables[0];



            ReportDocument rptstate = new RealERPRPT.R_19_Audit.RptEntrySEC();

            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtContent = rptstate.ReportDefinition.ReportObjects["txtContent"] as TextObject;
            txtContent.Text = txtrefno.Text.Trim();

            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstate.SetDataSource((DataTable)ViewState["tblper"]);
            Session["Report1"] = rptstate;
            this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lbtnUpPerAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblper"];

                string refno = this.txtrefno.Text.Trim();
                bool result = false;
                result = SECData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSORUPDATESECB", refno, "",
                  "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string seccod = dt.Rows[i]["seccod"].ToString();
                    string rmrks = dt.Rows[i]["rmrks"].ToString();
                    string sseccod = "";
                    for (int j = 1; j <= 2; j++)
                    {
                        sseccod = Convert.ToString("0" + j);
                        bool chkgval = Convert.ToBoolean(dt.Rows[i]["ssecval" + j.ToString()]);

                        if (chkgval)
                            result = SECData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSORUPDATESECA", seccod, sseccod, rmrks,
                         "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                        if (chkgval)
                            break;
                    }


                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;

            }

        }
    }
}