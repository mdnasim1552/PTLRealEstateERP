using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_23_CR
{
    public partial class RptDuesReportAll : System.Web.UI.Page
    {
     

        ProcessAccess accData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                // this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //string date1 = "01-" + ASTUtility.Right(date, 8);
               
                this.txtAsDate.Text = date;
                this.GetPrjName();
                 

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

       

        private void GetPrjName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETPROJECTNAME", "", "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectcode.DataSource = dt1;
            this.ddlProjectcode.DataTextField = "actdesc1";
            this.ddlProjectcode.DataValueField = "actcode";
            this.ddlProjectcode.DataBind();
        }

     
        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString () : comcod;
            return comcod;
        }


        private void GetMonCurrentdues()
        {
            Session.Remove("tbldues");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtAsDate.Text.Substring(0, 11).ToString();
            string type1 = this.rbtntype1.SelectedValue.ToString();
            string prjcode = this.ddlProjectcode.SelectedValue.ToString() == "000000000000" ? "18%" : this.ddlProjectcode.SelectedValue.ToString() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETALLCURRENTDUES", "", date, type1, prjcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDues.DataSource = null;
                this.gvDues.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tbldues"] = dt;
            this.Data_Bind();
        }

        private void GetTodayDues()
        {

            Session.Remove("tbldues");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtAsDate.Text.Substring(0, 11).ToString();
            string type1 = this.rbtntype1.SelectedValue.ToString();
            string prjcode = this.ddlProjectcode.SelectedValue.ToString() == "000000000000" ? "18%" : this.ddlProjectcode.SelectedValue.ToString() + "%";         
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETALLTODAYSDUES", "", date, type1, prjcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDues.DataSource = null;
                this.gvDues.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tbldues"] = dt;
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
           
            if (dt1.Rows.Count == 0)
                return dt1;
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string usircode = dt1.Rows[0]["usircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["Unitname"] = "";
                    dt1.Rows[j]["custname"] = "";



                }

                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                    else if (dt1.Rows[j]["usircode"].ToString() == usircode)
                    {
                        dt1.Rows[j]["Unitname"] = "";
                        dt1.Rows[j]["custname"] = "";


                    }


                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();
                }

            }


            return dt1;

        }



               

     


    private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbldues"];
           // this.gvDues.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDues.DataSource = dt;
            this.gvDues.DataBind();
            this.FooterCalculation();
            this.visiblity();

        }

        private void visiblity()
        {
          string radiolist=  this.rbtntype1.SelectedValue.ToString();


            this.gvDues.Columns[7].Visible = false;
            this.gvDues.Columns[8].Visible = false;
            this.gvDues.Columns[9].Visible = false;
            this.gvDues.Columns[10].Visible = false;


            if (radiolist== "Booking")
            {
                this.gvDues.Columns[7].Visible = true;
                this.gvDues.Columns[9].Visible = true;

            }

            else if(radiolist == "CRDUES")
            {
                this.gvDues.Columns[8].Visible = true;
                this.gvDues.Columns[10].Visible = true;

            }

            else
            {
                this.gvDues.Columns[7].Visible = true;
                this.gvDues.Columns[8].Visible = true;
                this.gvDues.Columns[9].Visible = true;
                this.gvDues.Columns[10].Visible = true;
            }





        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbldues"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvDues.FooterRow.FindControl("lblFprbookdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ? 0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvDues.FooterRow.FindControl("lbplFCrdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pinsam)", "")) ? 0.00 : dt.Compute("Sum(pinsam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvDues.FooterRow.FindControl("lblfcbookdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ? 0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvDues.FooterRow.FindControl("lblfcurrcrdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ? 0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvDues.FooterRow.FindControl("lblFtotaldues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todues)", "")) ? 0.00 : dt.Compute("Sum(todues)", ""))).ToString("#,##0.00;(#,##0.00); ");

            Session["Report1"] = gvDues;
             ((HyperLink)this.gvDues.HeaderRow.FindControl("hlbtntbCdataExelas")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }


        protected void lnkOk_Click(object sender, EventArgs e)
        {

            string Type = this.ddlreportType.SelectedValue.ToString();

            if(Type== "Current")
            {
                this.GetMonCurrentdues();
            }

            else
            {
                this.GetTodayDues();

            }

        }


       






        //private DataSet GetDataForReport()
        //{

        //    ////RadioButtonList1_SelectedIndexChanged(null, null);
        //    //string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = GetComcod();
        //    //DataSet ds1 = new DataSet();
        //    //string date1, date2;


        //    //    case "HOTB":
        //    //    case "AsOnDues":
        //    //        string date = this.txtAsDate.Text.Substring(0, 11).ToString();
        //    //        //string frdate = "01" + this.txtAsDate.Text.Trim().Substring(2); //"25-May-2016";
        //    //        ////string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
        //    //        //string endmonth = Convert.ToDateTime(frdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

        //    //        string type1 = this.rbtntype1.SelectedValue.ToString();
        //    //        string prjcode1 = this.ddlProjectName2.SelectedValue.ToString()=="000000000000"?"18%" : this.ddlProjectName2.SelectedValue.ToString()+"%";

        //    //        string calltype =this.ddlreportType.SelectedValue.ToString()== "AsOn"? "GETASONDUES": "GETALLCURRENTDUES";

        //    //        ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", calltype, "", date, type1, prjcode1, "", "", "", "", "");


        //    //        Session["tblCustDues"] = this.HiddenSameData(ds1.Tables[0]);

        //    //        break;




        //    //    case "Trial02":
        //    //        date1 = this.txtDatefromtb.Text.Substring(0, 11).ToString();
        //    //        date2 = this.txtDatetotb.Text.Substring(0, 11).ToString();
        //    //        // string level = this.ddlReportLevel.SelectedValue.ToString();
        //    //        //string Calltype = this.calltype();
        //    //        string leveltb2 = this.ddlReportLeveltb2.SelectedValue.ToString();

        //    //        ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "REPORT_TRIALBALANCE_COMPANYUDDL_0" + leveltb2, date1, date2, "", "", "", "", "", "", "");



        //}






















        private void PrintConTrialBalGen()
        {

            //// Iqbal Nayan
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataSet ds1 = this.GetDataForReport();
            //if (ds1 == null)
            //    return;

            //LocalReport Rpt1 = new LocalReport();
            ////DataTable dt = (DataTable)Session["tblDetails"];
            //var lst = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.TrialHeadOf>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccConTrialBalance", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Rpt1.SetParameters(new ReportParameter("closdram", Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("closcram", Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("netdram", Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("netcram", Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ")));

            //Rpt1.SetParameters(new ReportParameter("date", "As On Date:  " + Convert.ToDateTime(this.txtAsDate.Text.Trim()).ToString("dd-MMM-yyyy")));
            //Rpt1.SetParameters(new ReportParameter("Rpttitle", (this.RadioButtonList1.SelectedValue.ToString() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
            //    : (this.RadioButtonList1.SelectedValue.ToString() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE"));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }








       
      
        
  


      
      
       
       
      
     
    //    private DataTable HiddenSameData(DataTable dt1)
    //    {
    //        if (dt1.Rows.Count == 0)
    //            return dt1;         
    //                if (dt1.Rows.Count == 0)
    //                    return dt1;
    //                string pactcode = dt1.Rows[0]["pactcode"].ToString();
    //                string usircode = dt1.Rows[0]["usircode"].ToString();
    //                for (int j = 1; j < dt1.Rows.Count; j++)
    //                {
    //                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
    //                    {
    //                        pactcode = dt1.Rows[j]["pactcode"].ToString();
    //                        usircode = dt1.Rows[j]["usircode"].ToString();
    //                        dt1.Rows[j]["pactdesc"] = "";
    //                        dt1.Rows[j]["Unitname"] = "";
    //                        dt1.Rows[j]["custname"] = "";
                            
                          

    //                    }

    //                    else
    //                    {
    //                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
    //                            dt1.Rows[j]["pactdesc"] = "";
    //                        else if (dt1.Rows[j]["usircode"].ToString() == usircode)
    //                        {
    //                            dt1.Rows[j]["Unitname"] = "";
    //                            dt1.Rows[j]["custname"] = "";
                           
                               
    //                        }


    //                        pactcode = dt1.Rows[j]["pactcode"].ToString();
    //                        usircode = dt1.Rows[j]["usircode"].ToString();
    //                    }

    //                }

                  


    //        }



    //        return dt1;
     
    //}





    //protected void lnkOk_Click(object sender, EventArgs e)
    //    {
    //        //DataSet ds1 = this.GetDataForReport();


    //        //if (ds1 == null)
    //        //    return;

    //        //if (ds1.Tables[0].Rows.Count == 0)
    //        //{
    //        //    this.gvtbcon.DataSource = null;
    //        //    this.gvtbcon.DataBind();
    //        //    return;

    //        //}

    //        //this.gvtbcon.DataSource = ds1.Tables[0];
    //        //this.gvtbcon.DataBind();


    //        //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
    //        ////((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelcon")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

    //        //DataTable dt = ds1.Tables[0];


    //        //((Label)this.gvtbcon.FooterRow.FindControl("lblfBookduesas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pbookam)", "")) ? 0.00 : dt.Compute("sum(pbookam)", ""))).ToString("#,##0.00;(#,##0.00); "); // Convert.ToDouble(ds1.Tables[0].Rows[0]["pbookam"]).ToString("#,##0;(#,##0); ");
    //        //((Label)this.gvtbcon.FooterRow.FindControl("lblfCurrentduesas")).Text=Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pinsam)", "")) ? 0.00 : dt.Compute("sum(pinsam)", ""))).ToString("#,##0.00;(#,##0.00); ");
    //        //if (dt.Rows.Count>0)
    //        //{
    //        //    Session["Report1"] = gvtbcon;
    //        //    ((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelas")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

    //        //}

    //    }
        protected void gvtbcon_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesccon");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcodecon")).Text;
            string mACTDESC = ((HyperLink)e.Row.FindControl("HLgvDesccon")).Text;
            string mTRNDAT1 = this.txtAsDate.Text;
            string mTRNDAT2 = this.txtAsDate.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcodecon");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");

            string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString().Trim();
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();


            if (code == "")
            {
                return;
            }


            if (grp == "A")
            {
                Label closdram = (Label)e.Row.FindControl("lblgClosDramtcon");
                Label closcramt = (Label)e.Row.FindControl("lblgvClosCramtcon");
                Label netdramt = (Label)e.Row.FindControl("lblgvnetdramtcon");
                Label netcramt = (Label)e.Row.FindControl("lblgvnetcramtcon");
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                closdram.Font.Bold = true;
                closcramt.Font.Bold = true;
                netdramt.Font.Bold = true;
                netcramt.Font.Bold = true;
                //actdesc.Style.Add("text-align", "right");

            }
            else if (grp == "B" && code == "AAAAAAAAAAAA")
            {

                Label closdram = (Label)e.Row.FindControl("lblgClosDramtcon");
                Label closcramt = (Label)e.Row.FindControl("lblgvClosCramtcon");
                Label netdramt = (Label)e.Row.FindControl("lblgvnetdramtcon");
                Label netcramt = (Label)e.Row.FindControl("lblgvnetcramtcon");
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                closdram.Font.Bold = true;
                closcramt.Font.Bold = true;
                netdramt.Font.Bold = true;
                netcramt.Font.Bold = true;

            }
            ///---------------------------------//// 

            if (grp == "B" && ASTUtility.Left(mACTCODE, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (grp == "B" && lebel2 == "")
            {

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
            }
            else
            {
                if (grp == "B" && ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }











            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");
            //    Label dramt = (Label)e.Row.FindControl("lblgvDramtcon");
            //    Label cramt = (Label)e.Row.FindControl("lblgvCramtcon");
            //    Label closdramt = (Label)e.Row.FindControl("lblgvclodramtcon");
            //    Label closcramt = (Label)e.Row.FindControl("lblgvclocramtcon");
            //    string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString();

            //    if (grp == "")
            //    {
            //        return;
            //    }
            //    if (grp == "A")
            //    {

            //        actdesc.Font.Bold = true;
            //        dramt.Font.Bold = true;
            //        cramt.Font.Bold = true;
            //        closdramt.Font.Bold = true;
            //        closcramt.Font.Bold = true;
            //       // actdesc.Style.Add("text-align", "right");


            //    }

            //}
        }


     
     
        protected void chknetbalance_CheckedChanged(object sender, EventArgs e)
        {
           // this.lblnetbalance.Text = (this.chknetbalance.Checked) ? "Net Balance" : "Gross Balance";
        }



        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tbldues"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode <> '' ");
            dt = dv.ToTable();

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptDuesReportAll>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptDuesAllReports", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "All Dues Report"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
    }
}
