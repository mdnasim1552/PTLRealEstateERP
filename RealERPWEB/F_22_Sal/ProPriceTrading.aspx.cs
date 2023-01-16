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
namespace RealERPWEB.F_22_Sal
{
    public partial class ProPriceTrading : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                {
                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("../AcceessError.aspx");
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                    this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                }
                this.LoadGrid();
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tbTrading"];
            for (int i = 0; i < this.gvTrading.Rows.Count; i++)
            {
                //string Pactcode = ((Label)this.gvTrading.Rows[i].FindControl("lblgvpactCod")).Text.Trim();
                //string pactdesc = ((TextBox)this.gvTrading.Rows[i].FindControl("txtPactdesc")).Text.Trim();
                string munit = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgUnit")).Text.Trim();
                double Storied = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvStrid")).Text.Trim());
                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                string Flrno = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvFlr")).Text.Trim();
                double pqty = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvPqty")).Text.Trim());
                double dRate = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double pamt = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvPamt")).Text.Trim());
                double utility = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvUtility")).Text.Trim());
                double BD = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvBD")).Text.Trim());
                double Comm = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvComm")).Text.Trim());
                double OthCost = Convert.ToDouble('0' + ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvOthCost")).Text.Trim());
                string purdate = (((TextBox)this.gvTrading.Rows[i].FindControl("txtgvPurDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvPurDat")).Text.Trim();
                string handohdate = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvHanoDat")).Text.Trim();
                string facing = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvUFacing")).Text.Trim();
                string Location = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvLoc")).Text.Trim();
                string devname = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvDevName")).Text.Trim();
                string ptype = ((TextBox)this.gvTrading.Rows[i].FindControl("txtgvPType")).Text.Trim();

                double tamt = Convert.ToDouble((dUsize * dRate) + pamt + utility + BD + Comm + OthCost);
                rowindex = (this.gvTrading.PageSize * this.gvTrading.PageIndex) + i;
                //tblt02.Rows[rowindex]["pactdesc"] = pactdesc;
                tblt02.Rows[rowindex]["unit"] = munit;
                tblt02.Rows[rowindex]["usize"] = dUsize;
                tblt02.Rows[rowindex]["Storied"] = Storied;
                tblt02.Rows[rowindex]["rate"] = dRate;
                tblt02.Rows[rowindex]["uamt"] = Convert.ToDouble(dUsize * dRate);
                tblt02.Rows[rowindex]["parqty"] = pqty;
                tblt02.Rows[rowindex]["paramt"] = pamt;
                tblt02.Rows[rowindex]["tamt"] = tamt;
                tblt02.Rows[rowindex]["facing"] = facing;
                tblt02.Rows[rowindex]["flrno"] = Flrno;
                tblt02.Rows[rowindex]["utility"] = utility;
                tblt02.Rows[rowindex]["BD"] = BD;
                tblt02.Rows[rowindex]["Comm"] = Comm;
                tblt02.Rows[rowindex]["Othamt"] = OthCost;
                tblt02.Rows[rowindex]["purdat"] = purdate;
                tblt02.Rows[rowindex]["handodat"] = handohdate;
                tblt02.Rows[rowindex]["Location"] = Location;
                tblt02.Rows[rowindex]["devname"] = devname;
                tblt02.Rows[rowindex]["ptype"] = ptype;


            }
            ViewState["tbTrading"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tbTrading"];

            string comcod = this.GetCompCode();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                string rescode = dt.Rows[i]["rescode"].ToString();
                string unit = dt.Rows[i]["unit"].ToString();
                string storied = dt.Rows[i]["storied"].ToString();
                double dUsize = Convert.ToDouble(dt.Rows[i]["usize"].ToString());
                string flrno = dt.Rows[i]["flrno"].ToString();
                string Pqty = dt.Rows[i]["parqty"].ToString();
                string rate = Convert.ToDouble(dt.Rows[i]["rate"].ToString()).ToString();
                string Pamt = dt.Rows[i]["paramt"].ToString();
                string utility = dt.Rows[i]["utility"].ToString();
                string bd = dt.Rows[i]["bd"].ToString();
                string comm = dt.Rows[i]["comm"].ToString();
                string othamt = dt.Rows[i]["othamt"].ToString();
                string purdat = dt.Rows[i]["purdat"].ToString();
                string handodat = dt.Rows[i]["handodat"].ToString();
                string facing = dt.Rows[i]["facing"].ToString();
                string location = dt.Rows[i]["location"].ToString();
                string devname = dt.Rows[i]["devname"].ToString();
                string ptype = dt.Rows[i]["ptype"].ToString();
                if (dUsize > 0)
                {
                    MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_04", "INSERTUPDATEPURTRADING", Pactcode, rescode, unit, storied, dUsize.ToString(), flrno, Pqty,
                        rate.ToString(), Pamt, utility, bd, comm, othamt, purdat, handodat, facing, location, devname, ptype, "", "");
                }


            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Product Pricing(Trading)";
                string eventdesc = "Product Pricing(Trading) Update";
                string eventdesc2 = "Product Pricing(Trading)";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tbTrading");

            string comcod = this.GetCompCode();
            string pactcode = "%" + this.txtProCode.Text + "%";

            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "SHOWPURTRADING", pactcode, "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            ViewState["tbTrading"] = HiddenSameData(ds4.Tables[0]);
            this.Data_bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }
            return dt1;

        }

        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tbTrading"];
            this.gvTrading.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvTrading.DataSource = tblt05;
            this.gvTrading.DataBind();
            if (tblt05.Rows.Count == 0)
                return;
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uamt)", "")) ?
            //    0.00 : tblt05.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lFUsize")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(usize)", "")) ?
            //    0.00 : tblt05.Compute("Sum(usize)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvPAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pamt)", "")) ?
            //    0.00 : tblt05.Compute("Sum(pamt)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(tamt)", "")) ?
            //    0.00 : tblt05.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvfAptqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uqty)", "")) ?
            // 0.00 : tblt05.Compute("Sum(uqty)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvfParkingqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pqty)", "")) ?
            // 0.00 : tblt05.Compute("Sum(pqty)", ""))).ToString("#,##0;(#,##0); ");

            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvPUtility")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(utility)", "")) ?
            //0.00 : tblt05.Compute("Sum(utility)", ""))).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvTrading.FooterRow.FindControl("lgvPCooprative")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(cooperative)", "")) ?
            //0.00 : tblt05.Compute("Sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RealERPRPT.R_22_Sal.rptUnitFxInf();
            //DataTable dt1 = (DataTable)ViewState["tbTrading"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "uamt>0";
            //rptstk.SetDataSource(dv1);


            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Unit Fixation";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //dv1.RowFilter = "";

        }


        //protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        TextBox txt01 = (TextBox)e.Row.FindControl("txtItemdesc");
        //        TextBox txt02 = (TextBox)e.Row.FindControl("txtgUnitnum");
        //        TextBox txt03 = (TextBox)e.Row.FindControl("txtgvUSize");
        //        TextBox txt04 = (TextBox)e.Row.FindControl("txtgvUqty");
        //        TextBox txt05 = (TextBox)e.Row.FindControl("txtgvRate");
        //        TextBox txt06 = (TextBox)e.Row.FindControl("txtgvbstat");
        //        TextBox txt07 = (TextBox)e.Row.FindControl("txtgvRemarks");
        //        TextBox txt08 = (TextBox)e.Row.FindControl("txtgvPqty");
        //        TextBox txt09 = (TextBox)e.Row.FindControl("txtgvPamt");


        //        string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

        //        if (code == "")
        //        {
        //            return;
        //        }
        //        if (code.Substring(9, 3) == "000")
        //        {

        //            txt01.ReadOnly = true;
        //            txt02.ReadOnly = true;
        //            txt03.ReadOnly = true;
        //            txt04.ReadOnly = true;
        //            txt05.ReadOnly = true;
        //            txt06.ReadOnly = true;
        //            txt07.ReadOnly = true;

        //        }

        //    }
        //}
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        protected void gvTrading_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvTrading.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
        protected void gvTrading_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetCompCode();
            string pactCode = ((Label)this.gvTrading.Rows[e.RowIndex].FindControl("lblgvpactCod")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "DELETETRADING", pactCode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "ProductPricing (Trading)";
                string eventdesc = "Delete Trading Code";
                string eventdesc2 = "Project Name: " + pactCode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void ibtnOk_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadGrid();
        }
    }
}
