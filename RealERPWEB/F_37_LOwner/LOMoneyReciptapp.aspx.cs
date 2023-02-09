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

namespace RealERPWEB.F_37_LOwner
{
    public partial class LOMoneyReciptapp : System.Web.UI.Page
    {
        ProcessAccess LownerData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Money Receipt Approval(L/O)";
                this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetProjectName();

            }
        }

        public string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetProjectName()
        {
           
            string comcod =this.GetCompCode();
            string txtSProject = "%";
            DataSet ds1 = LownerData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNERMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            try
            {

                string comcod = this.GetCompCode();
                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string pactcode = (this.ddlProjectName.SelectedValue.ToString()=="000000000000"?"18":this.ddlProjectName.SelectedValue.ToString())+"%";
                DataSet ds1 = LownerData.GetTransInfo(comcod, "SP_ENTRY_LANDOWNERMGT", "GETMONEYRECIPTAPPROVAL", pactcode, frmdate, todate,
                    "", "", "", "", "", "");
                Session["tblapprecpt"] = ds1.Tables[0];
                this.Data_Bind();
                ds1.Dispose();
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;

            }

        }


       


        private void Data_Bind()
        {
            try
            {

                DataTable dt = (DataTable)Session["tblapprecpt"];
                this.gvMoneyRecipt.DataSource = dt;
                this.gvMoneyRecipt.DataBind();
                if (dt.Rows.Count > 0)
                    ((Label)this.gvMoneyRecipt.FooterRow.FindControl("lblgvFMRAmt")).Text = Convert.ToDouble(
                        (Convert.IsDBNull(dt.Compute("sum(paidamt)", ""))
                            ? 0
                            : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;


            }
        }

        protected void gvMoneyRecipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

           
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    HyperLink hlnkMoneyRcptEdit = (HyperLink)e.Row.FindControl("hlnkMoneyRcptEdit");
                    HyperLink hlnkPrintMoneyRcpt = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrint");
                    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                    string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                    string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                    string mrdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrdate")).ToString();

                    hlnkMoneyRcptEdit.NavigateUrl = "~/F_23_CR/MktMoneyReceipt?Type=Management&prjcode=" + pactcode + "&usircode=" + usircode + "&genno=" + mrno;
                    hlnkPrintMoneyRcpt.NavigateUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;

                }
           


        }

        protected void lnkbtnApproved_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            DataTable dt = (DataTable)Session["tblapprecpt"];
            string pactcode = dt.Rows[index]["pactcode"].ToString();
            string usircode= dt.Rows[index]["usircode"].ToString();
            string mrno = dt.Rows[index]["mrno"].ToString();
            string chqno = dt.Rows[index]["chqno"].ToString();
            string recndate = ((TextBox)this.gvMoneyRecipt.Rows[index].FindControl("txtgvrecnDate")).Text.ToString();
            if (recndate.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please select Reconcillation Date');", true);
                return;

            }


            bool   result = LownerData.UpdateTransInfo2(comcod, "SP_ENTRY_LANDOWNERMGT", "UPDATEMRRECONCILE", pactcode, usircode, mrno, chqno, recndate,  userid, Terminal, Sessionid, Date, "", "", "", "", "","","","", "", "", "", "" );
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + LownerData.ErrorObject["Msg"].ToString()+ "');", true);
                return;


            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reconcillation Successfully');", true);
            








        }
    }
}