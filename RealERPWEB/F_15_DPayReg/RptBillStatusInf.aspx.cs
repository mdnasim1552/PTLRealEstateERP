using System;
using System.Collections;
using System.Collections.Generic;
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
using RealERPRPT;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class RptBillStatusInf : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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

                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "BILL REGISTER";
                // ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.ShowBill();
                this.rblpaytype.SelectedIndex = 0;


            }


        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public string GetCompCode()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RpBillStatusInfo();
            TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date : " + txtReceiveDate.Text;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["BillAmt"];

            rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        private void ShowBill()
        {
            try
            {
                Session.Remove("BillAmt");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string ttsrch = "%";
                string searchbill = (rblpaytype.SelectedIndex == 0 ? "resource" : "");
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, searchbill, "", "", "", "", "", "", "");
                Session["BillAmt"] = ds1.Tables[0];


                this.ddlproj.Items.Clear();
                this.ddlres.Items.Clear();

                this.ddlproj.DataTextField = "pactdesc";
                this.ddlproj.DataValueField = "pactcode";
                this.ddlproj.DataSource = ds1.Tables[3];
                this.ddlproj.DataBind();
                this.ddlproj.SelectedValue = "000000000000";

                this.ddlres.DataTextField = "sirdesc1";
                this.ddlres.DataValueField = "sircode";
                this.ddlres.DataSource = ds1.Tables[4];
                this.ddlres.DataBind();

                ds1.Dispose();
            }
            catch (Exception)
            {


            }


        }
        protected void btnAllBill_Click(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)Session["BillAmt"]).Copy();
            string pactcode = ((this.ddlproj.SelectedValue.Trim() == "000000000000") ? "" : this.ddlproj.SelectedValue.Trim()) + "%";
            string sircode = ((this.ddlres.SelectedValue.Trim() == "000000000000") ? "" : this.ddlres.SelectedValue.Trim()) + "%";
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode like '" + pactcode + "' and sircode like '" + sircode + "'");
            dt = dv.ToTable();
            this.Data_Bind(dt);

            //if (dt.Rows.Count > 0)
            //{
            //    this.total.Visible = true;
            //}


        }


        private void Data_Bind(DataTable dt)
        {


            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
                //this.txtFTotal.Text = Convert.ToDouble ((Convert.IsDBNull (dt.Compute ("Sum(amt)", "")) ? 0.00 : dt.Compute ("Sum(amt)", ""))).ToString ("#,##0;(#,##0); -");



                Session["Report1"] = gvPayment;
                ((HyperLink)this.gvPayment.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }



        }




        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {
            //this.txtReceiveDate.Text = ASTUtility.DateInVal(this.txtReceiveDate.Text);
            //this.txtRefno.Focus();
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hbtnvbillno = (HyperLink)e.Row.FindControl("hbtnvbillno");
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno1")).ToString();

                string custbill = ASTUtility.Left(billno, 3).ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                // TextBox txtgvrevdat = (TextBox)e.Row.FindControl("txtgvrevdat");

                DateTime paydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "paydat"));
                DateTime revdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "revdat"));

                if (paydat != revdat)
                {
                    //   txtgvrevdat.Attributes["style"] = "color:red;";

                }


                if (custbill == "PBL")
                {
                    // hbtnvbillno.NavigateUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;


                    // hbtnvbillno.NavigateUrl = "~/F_99_Allinterface/PuchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                    hbtnvbillno.NavigateUrl = "~/F_15_DPayReg/RptPurBillTracking.aspx?Type=PurBilltk&genno=" + billno;
                    //RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurBilltk
                }
                else if (custbill == "CBL")
                {


                    hbtnvbillno.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal.aspx?Type=BillEdit&prjcode=&sircode=&genno=" + billno;

                }

                else if (custbill == "GBL")
                {


                    hbtnvbillno.NavigateUrl = "~/F_34_Mgt/OtherReqEntry.aspx?Type=OreqPrint&prjcode&genno=" + billno;

                }



                else
                {

                }

                if (gvPayment.Rows.Count > 0)
                {
                    gvPayment.UseAccessibleHeader = true;
                    gvPayment.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //gvPayment.FooterRow.TableSection = TableRowSection.TableFooter;


                }



                // hlnkempname.NavigateUrl = "~/F_21_Kpi/LinkEmpMonthWiseEva.aspx?empid=" + empid + "&empname=" + empname + "&Date1=" + fromdate + "&Date2=" + todate;

            }

        }


        private void SaveData()
        {
            DataTable dt = (DataTable)Session["BillAmt"];
            //int i = 0;
            //foreach (GridViewRow gvr1 in gvPayment.Rows)
            //{

            //    string revdate = ((TextBox)gvr1.FindControl("txtgvrevdat")).Text.Trim();
            //    dt.Rows[i++]["revdat"] = revdate;
            //}
            Session["BillAmt"] = dt;
            this.Data_Bind(dt);

        }


        protected void lbtnTotal_Click(object sernder, EventArgs e)
        {
            this.SaveData();

        }



        protected void lbtnfUpdate_Click(object sender, EventArgs e)
        {

            this.SaveData();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["BillAmt"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INSORUPBILLMATURED", ds1, null, null, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        }
    }
}