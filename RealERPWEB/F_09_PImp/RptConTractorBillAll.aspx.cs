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
namespace RealERPWEB.F_09_PImp
{
    public partial class RptConTractorBillAll : System.Web.UI.Page
    {
        ProcessAccess ImpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Supplier & Group Wise Payable";
                this.GetProjectName();
                this.rbtnconbill.SelectedIndex = 0;



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = ImpData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetConTractorName();


        }

        private void GetConTractorName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = ImpData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME", pactcode, serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "csirdesc";
            this.ddlSubName.DataValueField = "csircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {

            this.GetConTractorName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetConTractorName();

        }


        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "WorkWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }
        }


















        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            int index = this.rbtnconbill.SelectedIndex;
            this.MultiView1.ActiveViewIndex = index;
            switch (index)
            {
                case 0:
                    this.ShowConBillDetails(); ;
                    break;

                case 1:
                    this.ShowConBillSummary();
                    break;
                    ;





            }



        }





        private void ShowConBillDetails()
        {
            Session.Remove("tblconbill");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string csircode = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = ImpData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSCONBILWORKWISE", pactcode, csircode, date, "", "", "", "", "", "");

            this.txtsub.Text = ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["sub"].ToString();
            this.txtmemo.Text = ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["memono"].ToString();

            if (ds1 == null)
            {
                this.rpconbilldet.DataSource = null;
                this.rpconbilldet.DataBind();
                return;
            }


            // Session["tblconbill"] =ds1.Tables[0];
            Session["tblconbill"] = this.HiddenSameData(ds1.Tables[0]);
            // Session["tblbank"]=ds1.Tables[1];
            this.Data_Bind();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc"] = "";

                }

                rsircode = dt1.Rows[j]["rsircode"].ToString();

            }
            return dt1;

        }

        private void ShowConBillSummary()
        {
            Session.Remove("tblconbill");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string csircode = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = ImpData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSCONBILSUMWWISE", pactcode, csircode, date, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rpconbilldet.DataSource = null;
                this.rpconbilldet.DataBind();
                return;
            }


            Session["tblconbill"] = ds1.Tables[0];
            // Session["tblbank"]=ds1.Tables[1];
            this.Data_Bind();

        }










        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblconbill"];

            int index = this.rbtnconbill.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.rpconbilldet.DataSource = dt;
                    this.rpconbilldet.DataBind();
                    break;

                case 1:
                    this.rpconbilsum.DataSource = dt;
                    this.rpconbilsum.DataBind();
                    break;
                    ;





            }






        }









        private void FooterCalCulation()
        {

            DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "rpcashflow":



                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
                    //              0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFduetopay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netdues)", "")) ?
                    //              0 : dt.Compute("sum(netdues)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFcrlimit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crlimit)", "")) ?
                    //              0 : dt.Compute("sum(crlimit)", ""))).ToString("#,##0;(#,##0); ");

                    // ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFnyetdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nyetdues)", "")) ?
                    //              0 : dt.Compute("sum(nyetdues)", ""))).ToString("#,##0;(#,##0); ");
                    break;






            }



        }








        protected void rpconbilldet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {




            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lrprsirdesc = (Label)e.Item.FindControl("lrprsirdesc");
                TextBox txtrptbillamt = (TextBox)e.Item.FindControl("txtrptbillamt");

                //string wastatus = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "wastatus")).ToString();
                //string mACTCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "actcode")).ToString();
                //string mMemoNo = "INV" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyyMM") + "00" + ASTUtility.Right(this.lblInvNo.Text, 3); //this.GetLastInVNo();
                //string mBATCHCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "batchcode")).ToString();
                string rsircode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "rsircode")).ToString();
                if (rsircode == "")
                    return;
                if (ASTUtility.Right(rsircode, 4) == "AAAA")

                {
                    lrprsirdesc.Attributes["style"] = "font-weight:bold; text-align:right;";
                    txtrptbillamt.Style.Add("font-weight", "bold");
                }

                if (rsircode != "EEEEAAAAAAAA")
                {
                    txtrptbillamt.ReadOnly = true;

                }
                //if(rsircode!="CCAAAA")
                //{
                //    txtrptbillamt.ReadOnly = true; 
                //}
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //DataTable dt = (DataTable)Session["tblpayst"];

                ////List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
                //((Label)e.Item.FindControl("lblrpFamt01")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ?
                //                  0 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ?
                //                  0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ?
                //               0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt04")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ?
                //               0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt05")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ?
                //               0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt06")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ?
                //               0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt07")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ?
                //               0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt08")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ?
                //               0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt09")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ?
                //               0 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ?
                //               0 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ?
                //               0 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ?
                //               0 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ?
                //               0 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
                //               0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



            }


        }


        protected void rpconbilsum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {




            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lrprsirdesc = (Label)e.Item.FindControl("lrprsirdescsum");
                Label lblrppaidamtsum = (Label)e.Item.FindControl("lblrppaidamtsum");
                Label lblrptbillamtsum = (Label)e.Item.FindControl("lblrptbillamtsum");

                //string wastatus = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "wastatus")).ToString();
                //string mACTCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "actcode")).ToString();
                //string mMemoNo = "INV" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyyMM") + "00" + ASTUtility.Right(this.lblInvNo.Text, 3); //this.GetLastInVNo();
                //string mBATCHCODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "batchcode")).ToString();
                string rsircode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "rsircode")).ToString();
                if (rsircode == "")
                    return;
                if (ASTUtility.Right(rsircode, 4) == "AAAA")

                {
                    lrprsirdesc.Attributes["style"] = "font-weight:bold; text-align:right;";
                    lblrppaidamtsum.Style.Add("font-weight", "bold");
                    lblrptbillamtsum.Style.Add("font-weight", "bold");
                }

                //if (rsircode != "CCAAAA")
                //{
                //    txtrptbillamtsum.ReadOnly = true;
                //}

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //DataTable dt = (DataTable)Session["tblpayst"];

                ////List<SalesOpening> lst = (List<SalesOpening>)Session["tbl"];
                //((Label)e.Item.FindControl("lblrpFamt01")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ?
                //                  0 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ?
                //                  0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt03")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ?
                //               0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt04")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ?
                //               0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt05")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ?
                //               0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt06")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ?
                //               0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt07")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ?
                //               0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt08")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ?
                //               0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt09")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ?
                //               0 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ?
                //               0 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ?
                //               0 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ?
                //               0 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ?
                //               0 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)e.Item.FindControl("lblrpFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
                //               0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



            }


        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblconbill"];




            for (int i = 0; i <= rpconbilldet.Items.Count - 1; i++)
            {



                tbl1.Rows[i]["tbillamt"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.rpconbilldet.Items[i].FindControl("txtrptbillamt")).Text.Trim()));
                tbl1.Rows[i]["slno"] = ((TextBox)this.rpconbilldet.Items[i].FindControl("txtslno")).Text.Trim().ToString();



            }

            Session["tblconbill"] = tbl1;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


            string comcod = this.GetCompCode();
            this.SaveValue();

            DataTable dt = (DataTable)Session["tblconbill"];

            DataTable dt1 = ((DataTable)Session["tblconbill"]).Copy();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = "rsircode='EEEEAAAAAAAA'";
            dt1 = dv.ToTable();

            DataTable dt2 = ((DataTable)Session["tblconbill"]);
            string Pactcode = this.ddlProjectName.SelectedValue.ToString();
            string ddlsubcon = this.ddlSubName.SelectedValue.ToString();
            string sub = this.txtsub.Text;
            string memo = this.txtmemo.Text;

            foreach (DataRow dr2 in dt1.Rows)
            {


                double tbillamt = Convert.ToDouble(dr2["tbillamt"].ToString());



                bool result = ImpData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE", "INSERTUPDATESUBCONBILL", Pactcode, ddlsubcon, tbillamt.ToString(), sub, memo, "", "", "", "", "", "", "", "", "", "");



                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

                }



            }

            foreach (DataRow dr3 in dt2.Rows)
            {
                string rsircode = dr3["rsircode"].ToString();
                string flrcod = dr3["flrcod"].ToString();
                string slno = dr3["slno"].ToString();

                bool result = ImpData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE", "INSERTUPDATESUBCONBILLSLNO", Pactcode, ddlsubcon, rsircode, flrcod, slno, "", "", "", "", "", "", "", "", "", "");


                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

                }

            }
        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblconbill"];
            // string comcod = this.GetCompCode();

            if (rbtnconbill.SelectedIndex == 0)
            {
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string csircode = this.ddlSubName.SelectedValue.ToString();
                string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = ImpData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSCONBILWORKWISE", pactcode, csircode, date, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.rpconbilldet.DataSource = null;
                    this.rpconbilldet.DataBind();
                    return;
                }

                DataTable dt = ds1.Tables[0];
                // DataTable dt = ds1.Tables[0];
                DataTable dt1 = (ds1.Tables[0]).Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "rsircode='EEEEAAAAAAAA'";
                dt1 = dv.ToTable();





                ReportDocument rptstk = new ReportDocument();
                LocalReport Rpt1 = new LocalReport();
                var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ContractorBillWorkWise>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillWorkWiseInns", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("txtProjectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(17)));
                Rpt1.SetParameters(new ReportParameter("txtSubcon", this.ddlSubName.SelectedItem.Text.Trim().Substring(13)));
                Rpt1.SetParameters(new ReportParameter("txtDate", this.txtDate.Text));
                Rpt1.SetParameters(new ReportParameter("txtsub", ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["sub"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtmemo", ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["sub"].ToString()));
                Rpt1.SetParameters(new ReportParameter("takainword", "Recommended for payment of TK. " + Convert.ToDouble(dt1.Rows[0]["tbillamt"]).ToString("#,##0.00;(#,##0.00);") + ASTUtility.Trans((Convert.ToDouble(dt1.Rows[0]["tbillamt"])), 2) + " after necessary checking of account sections."));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";







                //ReportDocument rptbill = new RealERPRPT.R_09_PImp.RptConBillWorkWiseInns();

                //string payment = ds1.Tables[0].Rows[0]["tbillamt"].ToString();
                ////TextObject txtref = rptbill.ReportDefinition.ReportObjects["txtref"] as TextObject;
                ////txtref.Text = ds1.Tables[1].Rows[0]["cbillref"].ToString();

                //TextObject txtProjectName = rptbill.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                //txtProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(17);


                //TextObject txtSubConName = rptbill.ReportDefinition.ReportObjects["txtSubcon"] as TextObject;
                //txtSubConName.Text = this.ddlSubName.SelectedItem.Text.Trim().Substring(13);

                //TextObject txtDate = rptbill.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                //txtDate.Text = this.txtDate.Text;

                //TextObject txtsub = rptbill.ReportDefinition.ReportObjects["txtsub"] as TextObject;
                //txtsub.Text = ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["sub"].ToString();// ds1.Tables[2].Rows[0].ds1.Tables[2].Rows[0]["sub"].ToString();
                //TextObject txtmemo = rptbill.ReportDefinition.ReportObjects["txtmemo"] as TextObject;
                //txtmemo.Text = ds1.Tables[2].Rows.Count == 0 ? "" : ds1.Tables[2].Rows[0]["memono"].ToString();  //ds1.Tables[2].Rows[0]["memono"].ToString();


                //TextObject rpttxtTaka = rptbill.ReportDefinition.ReportObjects["takainword"] as TextObject;
                //rpttxtTaka.Text = "Recommended for payment of TK. " + Convert.ToDouble(dt1.Rows[0]["tbillamt"]).ToString("#,##0.00;(#,##0.00);") + ASTUtility.Trans((Convert.ToDouble(dt1.Rows[0]["tbillamt"])), 2) + " after necessary checking of account sections.";
                ////DataTable dt1 = (DataTable)Session["tblbank"];





                ////TextObject txtDate = rptbill.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                ////txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
                //TextObject txtuserinfo = rptbill.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
                //rptbill.SetDataSource(ds1.Tables[0]);
                //Session["Report1"] = rptbill;
            }
            else if (rbtnconbill.SelectedIndex == 1)
            {

                //DataTable dt1 = (DataTable)Session["tblconbill"];
                //DataTable dt = ((DataTable)Session["tblconbill"]).Copy();
                //DataView dv = dt.DefaultView;
                //dv.RowFilter = "rsircode='CCAAAA'";
                //dt = dv.ToTable();
                //ReportDocument rptbillSummary = new RealERPRPT.R_09_PImp.rptTopSheetInnstar();
                //TextObject companyname = rptbillSummary.ReportDefinition.ReportObjects["companyname"] as TextObject;
                //companyname.Text = comnam;
                //TextObject txtProjectName = rptbillSummary.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                //txtProjectName.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(17);


                //TextObject txtSubConName = rptbillSummary.ReportDefinition.ReportObjects["txtSubcon"] as TextObject;
                //txtSubConName.Text = "Contractor: " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
                //TextObject txtDate = rptbillSummary.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //txtDate.Text = this.txtDate.Text;


                //TextObject txtsub = rptbillSummary.ReportDefinition.ReportObjects["txtsub"] as TextObject;
                //txtsub.Text ="Sub: "+ this.txtsub.Text.Trim();
                //TextObject txtmemo = rptbillSummary.ReportDefinition.ReportObjects["txtmemo"] as TextObject;
                //txtmemo.Text ="Work order Memo No: "+this.txtmemo.Text.Trim();  //ds1.Tables[2].Rows[0]["memono"].ToString();

                //TextObject rpttxtTaka = rptbillSummary.ReportDefinition.ReportObjects["takainword"] as TextObject;
                //rpttxtTaka.Text = "Recommended for payment of TK. " + Convert.ToDouble(dt.Rows[0]["tbillamt"]).ToString("#,##0.00;(#,##0.00);") + ASTUtility.Trans((Convert.ToDouble(dt.Rows[0]["tbillamt"])), 2) + " after necessary checking of account sections.";

                //TextObject txtuserinfo = rptbillSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
                //rptbillSummary.SetDataSource(dt1);
                //Session["Report1"] = rptbillSummary;


            }
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }

        private void RptCashFlow()
        {

            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = this.GetCompCode();
            // string comnam = hst["comnam"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)Session["tblpayst"];
            // ReportDocument rptcash = new RealERPRPT.R_32_Mis.RptCashFlowBridge();
            // TextObject txtCompanyName = rptcash.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            // txtCompanyName.Text = comnam;
            // DataTable dt1 = (DataTable)Session["tblbank"];
            // int j=1;
            // for (int i = 0; i < dt1.Rows.Count; i++)
            // {



            //         TextObject rpttxth = rptcash.ReportDefinition.ReportObjects["txtb" + j.ToString()] as TextObject;
            //         rpttxth.Text = dt1.Rows[i]["bankdesc"].ToString();
            //         j++;
            //         if (j == 12)
            //             break;

            //}




            // TextObject txtDate = rptcash.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            // txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            // TextObject txtuserinfo = rptcash.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            // rptcash.SetDataSource(dt);
            // Session["Report1"] = rptcash;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }
}