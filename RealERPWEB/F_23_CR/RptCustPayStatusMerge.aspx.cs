using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_23_CR
{
    public partial class RptCustPayStatusMerge : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string Type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "Payment" ? "PAYMENT STATUS MERGE" : "PAYMENT STATUS MERGE");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetComeCode();
                this.GetlinkComp();

                this.GetProjectName();
            }
        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public void GetlinkComp()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", "GETCOMPLINKCOMCOD", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.MCOMCOD.Value = ds1.Tables[0].Rows[0]["MCOMCOD"].ToString();
        }
        private void GetProjectName()
        {
            string comcod = this.MCOMCOD.Value.ToString();
            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", "GETERPPRJLIST_MAP", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ViewState["tblprjlist"] = ds1.Tables[0];

            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetCustomerName();
        }

        private void GetCustomerName()
        {
            ViewState.Remove("tblcustomer");

           // string comcod = this.GetComeCode();
            string comcod = this.MCOMCOD.Value.ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", "GETCUSTLIST_MAP", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();
            ViewState["tblcustomer"] = ds2.Tables[0];
            ds2.Dispose();

            //ddlCustName_SelectedIndexChanged(null,null);

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            // get accounts erp project and customer
            string comcod = this.MCOMCOD.Value.ToString();
            string prjcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", "GETCUSTLIST_MAP", prjcode, custid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;            
            string acccustid = ds1.Tables[0].Rows[0]["accrescode"].ToString();
            string accpactcode = ds1.Tables[0].Rows[0]["accactcode"].ToString();

            //get Data
            this.ShowCustPayment(prjcode, custid, accpactcode, acccustid);

           
        }
        private void ShowCustPayment(string pactcode, string custid, string accpactcode, string acccustid)
        {
             
            Session.Remove("tblCustPayment");
            string acccomcod = this.GetComeCode();
            string comcod = this.MCOMCOD.Value.ToString();
             
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string calltype = "INSTALLMANT_WITHMRRMERGE";
            string procedure = "SP_REPORT_ACCOUNTSMAP";

            DataSet ds2 = purData.GetTransInfo(comcod, procedure, calltype, pactcode, custid, Date, acccomcod, accpactcode, acccustid, "", "", "");
            if (ds2 == null)
            {
                this.gvCustPayment.DataSource = null;
                this.gvCustPayment.DataBind();
                return;
            }

            Session["tblCustPayment"] = this.HiddenSameDate2(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameDate2(DataTable dtable)
        {

            Session.Remove("tblCustPayment");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt1 = dtable;

            switch (Type)
            {
                case "Payment":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }
                    break;
            }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }

        private void Data_Bind()
        { 
            this.FooterCal((DataTable)Session["tblCustPayment"]); 
        }
        private void FooterCal(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            this.gvCustPayment.DataSource = dt;
            this.gvCustPayment.DataBind();


            DataTable dt1 = dt.Copy();
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'CC' ";
            dt1 = dv1.ToTable();

            
            double SAmount = 0;
            double PAmount = 0, BalAmt = 0;
            SAmount = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(schamt)", "")) ? 0.00 : dt1.Compute("Sum(schamt)", "")));
            PAmount = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(paidamt)", "")) ? 0.00 : dt1.Compute("Sum(paidamt)", "")));
            BalAmt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(balamt)", "")) ? 0.00 : dt1.Compute("Sum(balamt)", "")));

            ((Label)this.gvCustPayment.FooterRow.FindControl("lfAmt")).Text = SAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfpayamt")).Text = PAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfbalamt")).Text = BalAmt.ToString("#,##0;(#,##0); ");
            

            Session["Report1"] = gvCustPayment;
            ((HyperLink)this.gvCustPayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        protected void gvCustPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgcResDesc2 = (Label)e.Row.FindControl("lgcResDesc2");
                Label lgvschamt = (Label)e.Row.FindControl("lgvschamt");
                Label lgvpayamt = (Label)e.Row.FindControl("lgvpayamt");
                Label lgvexessamt = (Label)e.Row.FindControl("lgvexessamt");
                Label lgvbalamt = (Label)e.Row.FindControl("lgvbalamt");
                

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();


                if (code == "")
                {
                    return;
                }

                if (code == "BB" || code == "CC")
                {
                    lgcResDesc2.Font.Bold = true;
                    lgvschamt.Font.Bold = true;
                    lgvpayamt.Font.Bold = true;
                    lgvexessamt.Font.Bold = true;
                    lgvbalamt.Font.Bold = true;

                    e.Row.BackColor = System.Drawing.Color.DarkGray;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#C1EEB0'");

                    lgcResDesc2.Attributes["style"] = "font-weight:bold; color:#fff;";
                    lgvschamt.Attributes["style"] = "font-weight:bold; color:#fff;";
                    lgvpayamt.Attributes["style"] = "font-weight:bold; color:#fff;";
                    lgvexessamt.Attributes["style"] = "font-weight:bold; color:#fff;";
                    lgvbalamt.Attributes["style"] = "font-weight:bold; color:#fff;";

                }
            }
        }

       
    }
}