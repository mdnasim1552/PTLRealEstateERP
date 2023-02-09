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
    public partial class RptLandOwnerPaySch : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        Common objcom = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Owner Payment Information";
                this.GetAccCode();
                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string actcode = this.Request.QueryString["actcode"];
                string rescode = this.Request.QueryString["usircode"];
                if (actcode.Length > 0 && rescode.Length > 0)
                {
                    this.LoadGrid();
                    lbtnusize_OnClick(null, null);
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
            string actcode = this.Request.QueryString["actcode"].Length > 0 ? this.Request.QueryString["actcode"] : this.ddlConAccHead.SelectedValue;
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
            string actcode = this.Request.QueryString["actcode"].Length > 0 ? this.Request.QueryString["actcode"] : this.ddlConAccHead.SelectedValue;
            string usircode = this.Request.QueryString["usircode"].Length > 0 ? this.Request.QueryString["usircode"] : Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string date = this.txtFDate.Text;

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
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "RPTLANDOWPAYINFO", actcode, usircode, date, "", "", "", "", "", "");
            //this.ClearScreen();

            Session["tblPay"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Footercalculation(ds1.Tables[0]);
            this.LandData_Bind();

        }


        private DataTable HiddenSameData(DataTable dtable)
        {

            if (dtable.Rows.Count == 0)
                return dtable;

            string gcod = dtable.Rows[0]["gcod"].ToString();

            DataTable dt1 = dtable;
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();
            //this.gvSpayment.DataSource = dtOrder;
            //this.gvSpayment.DataBind();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";

                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["schdate"] = "";
                }

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }
            return dt1;
        }
        private void LandData_Bind()
        {
            DataTable dt = (DataTable)Session["tblPay"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.Footercalculation(dt);
        }
        private void Footercalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            double schamt = 0, payamt = 0; int i = 0;
            DateTime date = Convert.ToDateTime(this.txtFDate.Text);
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                DateTime schdat = dr["schdate"].ToString().Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dr["schdate"]);
                DateTime paydat = dr["paiddate"].ToString().Trim() == "" ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dr["paiddate"]);
                if (date >= schdat)
                {
                    schamt = schamt + Convert.ToDouble(dr["schamt"]);
                }
                if (date >= paydat)
                {
                    payamt = payamt + Convert.ToDouble(dr["paidamt"]);
                }

            }

            double paydue = schamt - payamt;

            double tscham = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ?
                    0.00 : dt.Compute("sum(schamt)", "")));
            double tpaidamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                   0.00 : dt.Compute("sum(paidamt)", "")));

            ((Label)this.gvPayment.FooterRow.FindControl("lfamt")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ?
                    0.00 : dt.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lpayfamt")).Text =
               Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                   0.00 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvPayment.FooterRow.FindControl("ltotaldue")).Text = Convert.ToDouble(tscham - tpaidamt).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lpayfdue")).Text = paydue.ToString("#,##0;(#,##0); ");




        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.LoadGrid();




        }



        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01a = new TableCell();
                cell01a.Text = "Agreement";
                cell01a.HorizontalAlign = HorizontalAlign.Center;
                cell01a.ColumnSpan = 5;
                cell01a.BackColor = System.Drawing.Color.Yellow;
                cell01a.Attributes["style"] = "font-weight:bold;font-size:14px";


                TableCell cell01b = new TableCell();
                cell01b.Text = "Payment";
                cell01b.HorizontalAlign = HorizontalAlign.Center;
                cell01b.ColumnSpan = 5;
                cell01b.BackColor = System.Drawing.Color.Green;
                cell01b.ForeColor = System.Drawing.Color.White;
                cell01b.Attributes["style"] = "font-weight:bold;font-size:14px";

                gvrow.Cells.Add(cell01a);
                gvrow.Cells.Add(cell01b);


                gvPayment.Controls[0].Controls.AddAt(0, gvrow);


                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    e.Row.Cells[0].Visible = false;
                //    e.Row.Cells[1].Visible = false;
                //    e.Row.Cells[2].Visible = false;

                //}

            }
        }
    }
}