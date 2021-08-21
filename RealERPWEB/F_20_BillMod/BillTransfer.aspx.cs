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
namespace RealERPWEB.F_20_BillMod
{

    public partial class BillTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        DataTable dttemp = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Transfer Information Input/Edit Screen";


                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.Load_Project_From_Combo();
            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.Load_Project_From_Combo();
        }
        protected void ImgbtnFindProject0_Click(object sender, EventArgs e)
        {

        }



        protected void Load_Project_From_Combo()
        {
            Session.Remove("prlist");

            string comcod = this.GetCompCode();
            string fproject = "%" + this.txtProjectSearchF.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETDEPTNAME", fproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlprjlistfrom.DataTextField = "deptdesc";
            this.ddlprjlistfrom.DataValueField = "deptcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            Session["prlist"] = ds1.Tables[0];
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["prlist"];
            string deptcode = this.ddlprjlistfrom.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "deptcode not in ('" + deptcode + "')";
            this.ddlprjlistto.DataTextField = "deptdesc";
            this.ddlprjlistto.DataValueField = "deptcode";
            this.ddlprjlistto.DataSource = dv1.ToTable();
            this.ddlprjlistto.DataBind();

        }







        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";

                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;
                this.lblddlProjectTo.Text = this.ddlprjlistto.SelectedItem.Text;
                this.lblddlProjectFrom.Visible = true;
                this.lblddlProjectTo.Visible = true;
                this.ddlprjlistfrom.Visible = false;
                this.ddlprjlistto.Visible = false;
                this.ShowData();



            }

            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lblddlProjectFrom.Visible = false;
                this.lblddlProjectTo.Visible = false;
                this.ddlprjlistfrom.Visible = true;
                this.ddlprjlistto.Visible = true;
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                lbtnOk.Text = "Ok";

            }

        }


        private void ShowData()
        {

            Session.Remove("tblrcvbill");
            string comcod = this.GetCompCode();
            string frmDept = this.ddlprjlistfrom.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETBILLINFO", frmDept, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }

            Session["tblrcvbill"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblrcvbill"];
            int index;
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string appamt = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvaprovedamt")).Text).ToString();
                string chkrcv = (((CheckBox)this.gvPayment.Rows[i].FindControl("chkrcv")).Checked) ? "True" : "False";
                index = (gvPayment.PageIndex) * gvPayment.PageSize + i;
                dt.Rows[index]["chkrcv"] = chkrcv;
                dt.Rows[index]["appamt"] = appamt;


            }
            Session["tblrcvbill"] = dt;

        }


        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblrcvbill"];
            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblrcvbill"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvPayment.FooterRow.FindControl("lblgFsubamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lblgvFapprovedamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(appamt)", "")) ? 0.00 : dt.Compute("Sum(appamt)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblrcvbill"];
            bool result = true;
            string frmdept = this.ddlprjlistfrom.SelectedValue.ToString();
            string todept = this.ddlprjlistto.SelectedValue.ToString();
            string rcvdate = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
            foreach (DataRow dr in dt1.Rows)
            {

                string slnum = dr["slnum"].ToString().Trim();
                string RcvIssue = dr["chkrcv"].ToString().Trim();
                string appamt = dr["appamt"].ToString();
                if (RcvIssue == "True")
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "INSORUPILLTRSINF", slnum, frmdept, todept, rcvdate, appamt, "", "", "", "",
                                                           "", "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
            }



           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";




            ((LinkButton)this.gvPayment.FooterRow.FindControl("lbtnUpdate")).Enabled = false;
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }






    }
}
