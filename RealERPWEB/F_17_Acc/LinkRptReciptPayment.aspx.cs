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
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using RealEntity;
namespace RealERPWEB.F_17_Acc
{
    public partial class LinkRptReciptPayment : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = this.Request.QueryString["Type"].ToString(); // this.txtDateto.Text;
                ((Label)this.Master.FindControl("lblTitle")).Text = type == "payment" ? "Payment Details" : "Receipt Details";
                txtDateFrom.Text = this.Request.QueryString["Date1"].ToString(); // this.txtDateFrom.Text;
                txtDateto.Text = this.Request.QueryString["Date2"].ToString();
                this.btnok_Click(null, null);


            }

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


        protected void btnok_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString(); // this.txtDateFrom.Text;
            string todate = this.Request.QueryString["Date2"].ToString(); // this.txtDateto.Text;
                                                                          //string Calltype = this.GetCallType(type);
            if (type == "payment")
            {
                this.panellnk.Visible = true;
            }
            else
            {
                this.panellnk.Visible = false;
            }

            string payment = (type == "payment") ? "payment" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RECEIVIABLE", "RECEPIPTPAYMENT", frmdate, todate, payment, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvbgdvsexegp.DataSource = null;
                this.gvbgdvsexegp.DataBind();
                return;
            }
            DataTable dt = new DataTable();
            if (this.Request.QueryString["paycode"].Length > 0)
            {
                this.panellnk.Visible = false;

                DataTable dt1 = ds1.Tables[1];
                if (dt1 == null)
                {
                    return;
                }

                DataView dv1 = dt1.DefaultView;
                dv1.RowFilter = ("paycode like '26%'");
                dt = dv1.ToTable();
            }
            else
            {

                dt = ds1.Tables[1];
            }

            Session["tblplanexe"] = dt;
            Session["tblgplanexe"] = dt;
            Session["tblbplanexe"] = ds1.Tables[0];
            Session["tblmainhead"] = dt;
            Session["tblpaymentabp"] = ds1.Tables[2];


            this.LoadGrid();
            //this.lblvalprostdate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["prjstdate"]).ToString("dd-MMM-yyyy");
            //this.lblvalproendate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["prjenddate"]).ToString("dd-MMM-yyyy");
            //this.hlnkvalprodelduratin.Text = ds1.Tables[3].Rows[0]["deldur"].ToString() + " Day's";
            //string frmdate = Convert.ToDateTime("01" + this.txtfrmDate.Text.Substring(2)).ToString("dd-MMM-yyyy");
            //this.hlnkvalprodelduratin.NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ImpPlan02&comcod=" + this.GetCompCode() + "&pactcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + date;


        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblplanexe"];
            DataTable dt1 = (DataTable)Session["tblpaymentabp"];
            this.gvbgdvsexegp.DataSource = dt;
            this.gvbgdvsexegp.DataBind();
            this.FooterCal();
            this.ShowGraph();
            this.abppanel.Visible = true;
            this.abpamt.Text = (dt1.Rows.Count == 0) ? Convert.ToDouble(0.00).ToString("#,##0.00;(#,##0.00);") : Convert.ToDouble(dt1.Rows[0]["monthbgd"]).ToString("#,##0.00;(#,##0.00);");
            //var chkstring = ((Label)this.gvbgdvsexegp.FooterRow.FindControl("lgvFtoal")).Text.Trim();
            double actual = (dt.Rows.Count == 0) ? 0.00 : Convert.ToDouble("0" + ((Label)this.gvbgdvsexegp.FooterRow.FindControl("lgvFtoal")).Text.Trim());
            double abp = Convert.ToDouble(this.abpamt.Text.Trim());
            this.abpper.Text = ((abp == 0.00) ? "0.00" : (actual / abp).ToString("#,##0.00;(#,##0.00);")) + "%";

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblplanexe"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("paycode like '%0000000000'");
            dt = dv.ToTable();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvbgdvsexegp.FooterRow.FindControl("lgvFtoal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvbgdvsexegp.FooterRow.FindControl("lgvFpertoal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(peramt)", "")) ?
                      0 : dt.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void lnkgvWDescgp_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mpaycode = ((DataTable)Session["tblplanexe"]).Rows[index]["mpaycode"].ToString();
            string mACTCODE = ((DataTable)Session["tblplanexe"]).Rows[index]["paycode"].ToString();
            string lebel2 = ((DataTable)Session["tblplanexe"]).Rows[index]["actelev"].ToString();

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            if (mACTCODE.Substring(8, 4) != "0000")
            {
                string mACTDESC = ((DataTable)Session["tblplanexe"]).Rows[index]["actdesc"].ToString();
                string mRESCODE = ((DataTable)Session["tblplanexe"]).Rows[index]["arescode"].ToString();
                string mTRNDAT1 = this.txtDateFrom.Text;
                string mTRNDAT2 = this.txtDateto.Text;
                string mCOMCOD = this.GetCompCode();


                string opnoption = "withoutopening";

                if (lebel2 == "")
                {


                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                                                                           "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&opnoption=" + opnoption + "', target='_blank');</script>";
                }
                else
                {

                    if (mRESCODE != "000000000000")

                        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=" + opnoption + "', target='_blank');</script>";
                }







                return;

            }



            string colst = ((DataTable)Session["tblplanexe"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblplanexe"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("paycode  like '%0000000000'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("mpaycode='" + mpaycode + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";


            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["mpaycode"] != mpaycode)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("mpaycode='" + mpaycode + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbplanexe"]).Copy();
                dv = dtb.DefaultView;
                dv.RowFilter = ("mpaycode='" + mpaycode + "' and paycode not like '%0000000000'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }

            dv = dt.DefaultView;
            dv.Sort = ("mpaycode, paycode");
            Session["tblplanexe"] = dv.ToTable();
            this.LoadGrid();

        }

        private void ShowGraph()
        {

            DataTable dt = ((DataTable)Session["tblgplanexe"]).Copy();
            DataTable dt2 = ((DataTable)Session["tblbplanexe"]).Copy();
            DataTable dt3 = ((DataTable)Session["tblmainhead"]).Copy();
            //DataTable floor = ((DataTable)Session["tblfloor"]).Copy();

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("isircode<>'BBBBAAAAAAAA'");
            //dt = dv.ToTable();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.EClassReciptvspayment>();
            var lst2 = dt2.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.EClassReciptvspayment>();
            var lstmainhead = dt3.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.EClassReciptvspayment>();


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            var json2 = jsonSerialiser.Serialize(lst2);
            var json3 = jsonSerialiser.Serialize(lstmainhead);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + json + "','" + json2 + "', '" + json3 + "')", true);


        }

        protected void gvBgdVsExgp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton lnkgvWDescgp = (LinkButton)e.Row.FindControl("lnkgvWDescgp");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lgvBudgetAmt = (Label)e.Row.FindControl("lgvBudgetAmtgp");
                Label lgvpercent = (Label)e.Row.FindControl("lgvpercent");
                //Label lgvExAmtgp = (Label)e.Row.FindControl("lgvExAmtgp");
                //Label lgvbgdpro = (Label)e.Row.FindControl("lgvbgdpro");
                //Label lgvacpro = (Label)e.Row.FindControl("lgvacpro");
                //Label lgvbgddur = (Label)e.Row.FindControl("lgvbgddur");
                // HyperLink hlnkgvexedur = (HyperLink)e.Row.FindControl("hlnkgvexedur");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();
                string arescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "arescode")).ToString();



                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Right(code, 10) == "0000000000")
                {

                    lnkgvWDescgp.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvBudgetAmt.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvpercent.Attributes["style"] = "color:maroon;font-weight:bold;";
                    //lgvExAmtgp.Attributes["style"] = "color:maroon;font-weight:bold;";
                    //lgvbgdpro.Attributes["style"] = "color:maroon;font-weight:bold;";
                    //lgvacpro.Attributes["style"] = "color:maroon;font-weight:bold;";
                    //lgvbgddur.Attributes["style"] = "color:maroon;font-weight:bold;";
                    //hlnkgvexedur.Attributes["style"] = "color:maroon;font-weight:bold;";
                }

                else if (code != "000000000000" && arescode != "000000000000")
                {

                    lnkgvWDescgp.Attributes["style"] = "color:blue;";
                    lgvBudgetAmt.Attributes["style"] = "color:blue;";
                    lgvpercent.Attributes["style"] = "color:blue;";



                }

                else if (code != "000000000000" && arescode == "000000000000")
                {

                    lnkgvWDescgp.Attributes["style"] = "font-weight:bold;";
                    lgvBudgetAmt.Attributes["style"] = "font-weight:bold;";
                    lgvpercent.Attributes["style"] = "font-weight:bold;";



                }


            }
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
    }
}