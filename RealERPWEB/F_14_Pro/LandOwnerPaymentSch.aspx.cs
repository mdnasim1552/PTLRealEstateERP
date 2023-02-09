using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_14_Pro
{
    public partial class LandOwnerPaymentSch : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        static int indexofamp;
        Common objcom = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Owner Payment Information";
                this.GetAccCode();
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_OnClick(null, null);
                }
            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAccCode()
        {
            string comcod = this.GetComeCode();
            //string filter = this.txtAccSearch.Text.Trim() + "%";
            string filter = "16%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "",
                "", "", "", "", "");
            this.ddlConAccHead.DataSource = ds1.Tables[0];
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            this.ddlConAccHead.DataBind();
            this.ddlConAccHead.SelectedValue = this.Request.QueryString["prjcode"];


        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                this.ddlConAccHead.Visible = false;
                this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;

                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;

                this.lbtnBack.Visible = false;
                this.ClearScreen();
            }






        }

        private void ClearScreen()
        {
            this.ddlConAccHead.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.gvSpaymentland.DataSource = null;
            this.gvSpaymentland.DataBind();

            // this.lmsg111.Text = "";

        }


        private void LoadGrid()
        {

            string comcod = this.GetComeCode();
            string actcode = this.ddlConAccHead.SelectedValue;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SUPPAYMENT", actcode, "", "", "", "", "", "", "", "");
            Session["tblpaymentSch"] = ds1.Tables[0];


            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpaymentSch"];
            this.gvSpaymentland.DataSource = dt;
            this.gvSpaymentland.DataBind();
            for (int i = 0; i < gvSpaymentland.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpaymentland.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpaymentland.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }

        }

        protected void lbtnusize_OnClick(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
            this.lbtnBack.Visible = true;
            string comcod = this.GetComeCode();
            string actcode = this.ddlConAccHead.SelectedValue;
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            DataTable dtOrder = (DataTable)Session["tblpaymentSch"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();
            //if ((Convert.ToBoolean(dtOrder.Rows[0]["mgtbook"])) == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Booked from Management');", true);
            //    return;
            //}
            this.MultiView1.ActiveViewIndex = 0;
            //Session["UsirBasicInformation"] = dtOrder;
            this.gvSpaymentland.DataSource = dtOrder;
            this.gvSpaymentland.DataBind();
            this.lblCode.Text = usircode;
            DataSet ds1 = accData.GetTransInfo(comcod, "[SP_ENTRY_LANDOWNER]", "LANDOWPAYINFO", actcode, usircode, "", "", "", "", "", "", "");
            //this.ClearScreen();
            this.gvPayment.DataSource = ds1.Tables[0];
            this.gvPayment.DataBind();
            this.FooterCal(ds1.Tables[0]);
            Session["tblPay"] = ds1.Tables[0];


        }
        private void FooterCal(DataTable dt)
        {
            ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0.00 : dt.Compute("sum(schamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.LoadGrid();




        }
        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //DataRow[] dr1 = ASTUtility.PagePermission1 (HttpContext.Current.Request.Url.AbsoluteUri.ToString (), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlConAccHead.SelectedValue.ToString();
            string Usircode = this.lblCode.Text;
            //this.lblCode.Text = usircod;
            //string Usircode = this.lblCode.Text;
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvItmCode3")).Text.Trim();
                string schDate = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                // string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim())).ToString();
                if (Amount != 0)
                {
                    accData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, Gcode, schDate, Amount.ToString(), "", "", "", "", "", "", "", "", "", "");
                }

            }


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);
        }

        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblPay"];
            string PactCode = this.ddlConAccHead.SelectedValue.ToString();
            string Usircode = this.lblCode.Text;
            string Gcode = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lblgvItmCode3")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_LANDOWNER", "DELETEINSTALLMENT", PactCode, Usircode, Gcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("gcod<>''");
                Session["tblPay"] = dv.ToTable();
                this.gvPayment.DataSource = dt;
                this.gvPayment.DataBind();
                this.lTotalPayment_Click(null, null);
            }
        }

        protected void lTotalPayment_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            double Amount = 0;
            DataTable dt = (DataTable)Session["tblPay"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text).ToString("dd-MMM-yyyy");
                dt.Rows[i]["schamt"] = Amt;
                dt.Rows[i]["schdate"] = date;
                ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text = Amt.ToString("#,##0;-#,##0; ");

            }
            Session["tblPay"] = dt;
            Amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0.00 : dt.Compute("sum(schamt)", "")));

            if (Amount > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = Amount.ToString("#,##0.00;(#,##0.00); ");
            }
            Session["Amt11"] = Amount;
        }

        protected void gvSpaymentland_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lgvvounum");

                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + vounum;
            }
        }
    }
}