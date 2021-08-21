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
namespace RealERPWEB.F_23_CR
{
    public partial class LinkMktMoneyReceipt : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = " Account Receivable, Receipt, Balance Status";
                this.Master.Page.Title = " Account Receivable, Receipt, Balance Status";
                this.ShowData();

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




        private void ShowData()
        {



            string comcod = this.GetCompCode();
            string pactcode = this.Request["prjcode"].ToString();
            string usircode = this.Request["usircode"].ToString();
            string date = this.Request["Date1"].ToString();
            string gcod = this.Request.QueryString["genno"].ToString() + "%";

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPROUNITDUESDETAILS", pactcode, usircode, date, gcod, "", "", "", "", "");

            Session["tbldues"] = this.HiddenSameData(ds2.Tables[0]);
            // DataTable dtstatus = ds2.Tables[0];

            if (ds2 == null)
            {

                this.gvmoneyreceipt.DataSource = null;
                this.gvmoneyreceipt.DataBind();
                return;
            }
            this.Data_Bind();






            //this.lbtnUpdate.Visible = true;
        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string gcod = dt1.Rows[0]["gcod"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    gcod = dr1["gcod"].ToString();
                    i++;
                    continue;
                }

                if (dr1["gcod"].ToString() == gcod)
                {

                    dr1["gdesc"] = "";

                }


                gcod = dr1["gcod"].ToString();
            }



            return dt1;

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldues"];
            this.gvmoneyreceipt.DataSource = dt;
            this.gvmoneyreceipt.DataBind();
            this.FooterCalCulation();


        }


        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)Session["tbldues"];
            if (dt.Rows.Count == 0)
            {
                return;

            }




            ((Label)this.gvmoneyreceipt.FooterRow.FindControl("lblgvFreceivable")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(receivable)", "")) ? 0.00 : dt.Compute("Sum(receivable)", ""))).ToString("#,##0;-#,##0; ");
            ((Label)this.gvmoneyreceipt.FooterRow.FindControl("lblgvFpaidamoun")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0;-#,##0; ");



        }


        private string CompanyPrintMR()
        {

            string comcod = this.GetCompCode();
            string mrprint = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    //case "3101":
                    mrprint = "MRPrint1";
                    break;


                case "2325":
                case "3325":
                    mrprint = "MRPrint2";
                    break;



                //case "3101":
                case "3335":
                    // case "3101":
                    mrprint = "MRPrint3";
                    break;

                case "3337":
                case "3336":
                case "3101":
                    mrprint = "MRPrint4";
                    break;

                case "3339":
                    //case "3101":
                    mrprint = "MRPrint5";
                    break;


                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }


        protected void lblShow_Click(object sender, EventArgs e)
        {

            //((Label)this.Master.FindControl("lblmsg")).Text = " ";

            //string comcod = this.GetCompCode() ;
            //string mrno = this.ddlMRNO.SelectedValue.ToString();
            //DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SHOWMRNO", mrno, "", "", "", "", "", "", "", "");
            //DataTable dtstatus = ds2.Tables[0];

            //if (dtstatus == null)
            //    return;

            //this.grvacc.DataSource = ds2.Tables[0];
            //this.grvacc.DataBind();

            //if (dtstatus.Rows.Count > 0)
            //{
            //    this.lblrecdate.Text = Convert.ToDateTime(dtstatus.Rows[0]["mrdate"]).ToString("dd-MMM-yyyy");
            //    ((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dtstatus.Compute("Sum(paidamt)", "")) ? 0.00 : dtstatus.Compute("Sum(paidamt)", ""))).ToString("#,##0;-#,##0; ");
            //    ((Label)this.grvacc.FooterRow.FindControl("lblgvFdisamt")).Text = Convert.ToDouble((Convert.IsDBNull(dtstatus.Compute("Sum(disamt)", "")) ? 0.00 : dtstatus.Compute("Sum(disamt)", ""))).ToString("#,##0;-#,##0; ");


            //    //this.lbtnUpdate.Visible = true;
            //}
            //if (Request.QueryString["Type"].Trim() == "CustCare")
            //{
            //    ((LinkButton)this.grvacc.FooterRow.FindControl("lnkDelete")).Visible = false;
            //}

        }


        private void SaveValue()
        {

            DataTable dt1 = (DataTable)Session["tbldues"];

            double bgdqty, exeqty, prebqty, balqty, exosqty, sysqty, ordrate, billam, balam;
            string mbnumber, pagenumber;
            int i = 0;

            foreach (GridViewRow gvr1 in gvmoneyreceipt.Rows)
            {


                string paydate = ((TextBox)gvr1.FindControl("txtgvpaydate")).Text.Trim();
                double paidamt = Convert.ToDouble("0" + ((TextBox)gvr1.FindControl("txtgvpaidamount")).Text.Trim());
                dt1.Rows[i]["paydate"] = paydate;
                dt1.Rows[i]["paidamt"] = paidamt;
                i++;
            }



            Session["tbldues"] = dt1;

        }

        protected void lnkbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //if (!Convert.ToBoolean(dr1[0]["entry"]))
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;
                //}

                this.SaveValue();
                DataTable dt1 = (DataTable)Session["tbldues"];

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt1);
                ds1.Tables[0].TableName = "tbl1";


                string pactcode = (this.Request.QueryString["genno"].ToString() == "02901" || this.Request.QueryString["genno"].ToString() == "02903" || this.Request.QueryString["genno"].ToString() == "02904" || this.Request.QueryString["genno"].ToString() == "02905" || this.Request.QueryString["genno"].ToString() == "02907") ? ("25" + this.Request.QueryString["prjcode"].ToString().Substring(2)) : this.Request.QueryString["prjcode"].ToString();
                string usircode = this.Request.QueryString["usircode"].ToString();

                string EditByid = hst["usrid"].ToString();
                string Editdat = System.DateTime.Today.ToString("dd-MMM-yyyy");

                bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEMRINF", ds1, null, null, pactcode, usircode, EditByid, Editdat,
                       "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void gvmoneyreceipt_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Particulars";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Receivable";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Received";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.Attributes["style"] = "font-weight:bold;";
                cell03.ColumnSpan = 8;
                gvrow.Cells.Add(cell03);



                gvmoneyreceipt.Controls[0].Controls.AddAt(0, gvrow);
            }


        }
        protected void gvmoneyreceipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;

                // e.Row.Cells[15].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvpaidamount = (TextBox)e.Row.FindControl("txtgvpaidamount");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                if (vounum == "")
                    return;
                txtgvpaidamount.Attributes["style"] = (vounum == "00000000000000") ? "color:black; text-align:right;" : "color:red; text-align:right;";
                txtgvpaidamount.ReadOnly = (vounum != "00000000000000");


            }
        }

    }
}











