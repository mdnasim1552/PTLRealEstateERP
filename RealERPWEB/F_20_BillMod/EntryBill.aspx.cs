using System;
using System.Collections;
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

namespace RealERPWEB.F_20_BillMod
{
    public partial class EntryBill : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Status Information";

                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.GetProjectName();
                this.GetPartyName();
                this.GetBillNature();
                this.GetDepartment();
                this.TableCreate();

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtsrchProject.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
        }
        private void GetPartyName()
        {

            string comcod = this.GetCompCode();
            string SearchParty = "%" + this.txtSrhParty.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETPARTYNAME", SearchParty, "", "", "", "", "", "", "", "");
            this.ddlPartyName.DataTextField = "prdesc";
            this.ddlPartyName.DataValueField = "prcode";
            this.ddlPartyName.DataSource = ds1.Tables[0];
            this.ddlPartyName.DataBind();
            ds1.Dispose();
        }

        private void GetBillNature()
        {
            string comcod = this.GetCompCode();
            string srchBillnature = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETBILLNATURE", srchBillnature, "", "", "", "", "", "", "", "");
            this.ddlBillNature.DataTextField = "rpdesc";
            this.ddlBillNature.DataValueField = "rpcode";
            this.ddlBillNature.DataSource = ds1.Tables[0];
            this.ddlBillNature.DataBind();
            ds1.Dispose();


        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            string srchdept = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETDEPTNAME", srchdept, "", "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "deptdesc";
            this.ddlDepartment.DataValueField = "deptcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            ds1.Dispose();


        }

        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("slnum", Type.GetType("System.String"));
            tblt01.Columns.Add("rcvdate", Type.GetType("System.String"));
            tblt01.Columns.Add("billnature", Type.GetType("System.String"));
            tblt01.Columns.Add("billndesc", Type.GetType("System.String"));
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("paycode", Type.GetType("System.String"));
            tblt01.Columns.Add("paydesc", Type.GetType("System.String"));
            tblt01.Columns.Add("refno", Type.GetType("System.String"));
            tblt01.Columns.Add("deptcode", Type.GetType("System.String"));
            tblt01.Columns.Add("deptdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("received", Type.GetType("System.String"));
            tblt01.Columns.Add("amt", Type.GetType("System.Double"));
            tblt01.Columns.Add("narration", Type.GetType("System.String"));
            ViewState["tblpayment"] = tblt01;


        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
            this.ddlProject.Focus();

        }
        protected void ibtnFindParty_Click(object sender, EventArgs e)
        {
            this.GetPartyName();
            this.ddlPartyName.Focus();
        }
        protected void ibtnnature_Click(object sender, EventArgs e)
        {
            this.GetBillNature();
            this.ddlBillNature.Focus();
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lblAddToTable_Click(object sender, EventArgs e)
        {

        }

        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {
            this.txtReceiveDate.Text = ASTUtility.DateInVal(this.txtReceiveDate.Text);
            this.txtBillno.Focus();
        }
        protected void txtpaymentdate_TextChanged(object sender, EventArgs e)
        {
            this.txtReceivedUser.Text = ASTUtility.DateInVal(this.txtReceivedUser.Text);
            this.lbtnAddTable.Focus();
        }
        protected void lbtnAddTable_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblpayment"];

            string slnum = (dt.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
            this.lblslnum.Text = slnum;
            DataRow dr1 = dt.NewRow();
            dr1["slnum"] = slnum;
            dr1["rcvdate"] = this.txtReceiveDate.Text;
            dr1["billnature"] = this.ddlBillNature.SelectedValue.ToString();
            dr1["billndesc"] = this.ddlBillNature.SelectedItem.Text.Trim();
            dr1["actcode"] = this.ddlProject.SelectedValue.ToString();
            dr1["actdesc"] = this.ddlProject.SelectedItem.Text.Trim();
            dr1["paycode"] = this.ddlPartyName.SelectedValue.ToString();
            dr1["paydesc"] = this.ddlPartyName.SelectedItem.Text.Trim();
            dr1["refno"] = this.txtBillno.Text.Trim();
            dr1["deptcode"] = this.ddlDepartment.SelectedValue.ToString(); ;
            dr1["deptdesc"] = this.ddlDepartment.SelectedItem.Text;
            dr1["received"] = this.txtReceivedUser.Text;
            dr1["amt"] = ASTUtility.StrPosOrNagative(this.txtBillAmount.Text.Trim());
            dr1["narration"] = this.txtNarration.Text;
            dt.Rows.Add(dr1);
            ViewState["tblpayment"] = dt;
            this.Data_Bind();
            this.txtReceiveDate.Focus();
        }

        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ? 0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");

            }



        }

        private string GetSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["slnum"].ToString();
        }


        private string IncrmentSlNum()
        {
            //string isunum="000000000";
            string slnum = (Convert.ToInt32(this.lblslnum.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + slnum), 9));



        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;

                string slnum = this.GetSlNum();
                this.lblslnum.Text = slnum;
                foreach (DataRow dr in dt1.Rows)
                {

                    string rcvdate = ASTUtility.DateFormat(dr["rcvdate"].ToString());
                    string refno = dr["refno"].ToString().Trim();
                    string actcode = dr["actcode"].ToString();
                    string paycode = dr["paycode"].ToString();
                    string billnature = dr["billnature"].ToString();
                    string amt = Convert.ToDouble("0" + dr["amt"].ToString()).ToString();
                    string deptcode = dr["deptcode"].ToString();
                    string received = dr["received"].ToString();
                    string narration = dr["narration"].ToString();

                    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "INSERTORUPBILLINF", slnum, rcvdate, refno, actcode, paycode, billnature, amt,
                                                              deptcode, received, narration, "", "", "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }

                    slnum = this.IncrmentSlNum();
                    this.lblslnum.Text = slnum;
                }


                ((LinkButton)this.gvPayment.FooterRow.FindControl("lbtnUpdate")).Enabled = false;
                this.lbtnRefresh.Focus();

                //Log Report





            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }


        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                tbl1.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
                tbl1.Rows[i]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvbillamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["received"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvreceived")).Text.Trim();

            }
            ViewState["tblpayment"] = tbl1;

        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblpayment");
            this.TableCreate();
            this.gvPayment.DataSource = null;
            this.gvPayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }
    }
}