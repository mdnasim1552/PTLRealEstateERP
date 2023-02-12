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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_34_Mgt
{

    public partial class DataSyncSystem : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        ProcessAccess SecAccData = new ProcessAccess("ASITINTERIOR", "Secondary");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //  ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Voucher 360 <sup>0";
                //this.Master.Page.Title = "Voucher 360 <sup>0</sup>";
                CommonButton();
                this.txtfromdate.Text = Fromdate();
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.lbtnOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc2 = "";
                    string comcod = this.GetCompCode();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }

            }
        }

        private  string Fromdate()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_LASTSYNCDATE", "", "", "", "", "", "");
            if (ds1 == null)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                return Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
            }
            else
            {
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    return Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
                }
                else
                {
                    string scomcod = "3368";
                    DataSet ds3 = SecAccData.GetTransInfo(scomcod, "SP_UTILITY_DATA_SYNC", "GET_LASTSYNCDATE", "", "", "", "", "", "");
                    if (ds3 != null)
                    {
                       this.TxtAccMiss.Text= Convert.ToString(Convert.ToInt32(ds3.Tables[1].Rows[0]["totalacccode"]) - Convert.ToInt32(ds1.Tables[1].Rows[0]["totalacccode"]));
                      this.TxtResMiss.Text = Convert.ToString(Convert.ToInt32(ds3.Tables[2].Rows[0]["totalrescode"]) - Convert.ToInt32(ds1.Tables[2].Rows[0]["totalrescode"]));

                    }

                    return Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).AddDays(1).ToString("dd-MMM-yyyy");
                }
            }

            }
         
            
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lbtnDeleteVoucher_Click);

        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = GetCompCode();           
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["AllVoucher"];
            DataTable dtdet = (DataTable)Session["AllVoucherDet"];
            if (dt.Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found to Sync";

                return;
            }
            DataSet ds = new DataSet("ds1");
            ds.Merge(dt);
            ds.Tables[0].TableName = "tblvoucher";


            DataSet ds2 = new DataSet("ds2");
            ds2.Merge(dtdet);
            ds2.Tables[0].TableName = "tblvoucherdet";
            var xx = ds.GetXml();
            var xx2 = ds2.GetXml();

            bool result = AccData.UpdateXmlTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "UPDATE_VOUCHER", ds, ds2, null, userid, Terminal, Sessionid, Posteddat);

            if (result==true)
            {
                this.txtfromdate.Text = this.Fromdate();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sync Successfully";

            }



        }

        private string GetCompCode()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = "3368"; //this.GetCompCode();
            DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            DateTime todate = Convert.ToDateTime(this.txttodate.Text);

            if (frmdate.Month == todate.Month && frmdate.Year == todate.Year)
            {
                DataSet ds1 = SecAccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_PERIODIC_ALL_TRANSECTION", frmdate.ToString("dd-MMM-yyyy"), todate.ToString("dd-MMM-yyyy"), "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvAccVoucher.DataSource = null;
                    this.gvAccVoucher.DataBind();
                    return;
                }

                Session["AllVoucher"] = ds1.Tables[0];
                Session["AllVoucherDet"] = ds1.Tables[1];

                this.Data_Bind();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Not Eligible with Selected Date Range";
                return;
            }

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string events = hst["events"].ToString();
            //if (Convert.ToBoolean(events) == true)
            //{
            //    string eventtype = "Show Data " + ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Show Data " + ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc2 = "Data Show Date Range : " + "From " + frmdate + "To " + todate + "Voucher Type " + voutype;

            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            //}

        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "<span class='fa fa-trash'></span> Remove Selected Voucher";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).CssClass = "btn btn-danger btn-sm";







        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["AllVoucher"];


            this.gvAccVoucher.DataSource = dt;
            this.gvAccVoucher.DataBind();
            //this.FooterCalCulation();
            //this.VoucherUsrSumm();
            //this.VoucherCount();




        }

        private void VoucherUsrSumm()
        {
          //  DataTable dt2 = (DataTable)Session["tblusrvoucount"];


          //  if (dt2.Rows.Count == 0)
          //      return;

          //  this.gvvoucountsum.DataSource = dt2;
          //  this.gvvoucountsum.DataBind();

          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFpdc")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(pdcvou)", "")) ?
          // 0 : dt2.Compute("sum(pdcvou)", ""))).ToString("#,##0;(#,##0); ");
          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFCash")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(cashvou)", "")) ?
          //0 : dt2.Compute("sum(cashvou)", ""))).ToString("#,##0;(#,##0); ");
          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFBank")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(bankvou)", "")) ?
          //0 : dt2.Compute("sum(bankvou)", ""))).ToString("#,##0;(#,##0); ");
          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFContra")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(contravou)", "")) ?
          //0 : dt2.Compute("sum(contravou)", ""))).ToString("#,##0;(#,##0); ");
          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFJour")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(jourvou)", "")) ?
          //0 : dt2.Compute("sum(jourvou)", ""))).ToString("#,##0;(#,##0); ");
          //  ((Label)this.gvvoucountsum.FooterRow.FindControl("lgvFTotalvou")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tonum)", "")) ?
          //0 : dt2.Compute("sum(tonum)", ""))).ToString("#,##0;(#,##0); ");


        }

        private void VoucherCount()
        {

            DataTable dt1 = (DataTable)Session["AllVoucher"];
            if (dt1.Rows.Count == 0)
                return;

            //this.pnlvouCount.Visible = true;
            //this.lbltoCashVoucher.Text = Convert.ToDouble(dt1.Rows[0]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            //this.lbltoBankVoucher.Text = Convert.ToDouble(dt1.Rows[1]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            //this.lbltoContraVoucher.Text = Convert.ToDouble(dt1.Rows[2]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            //this.lbltoJournalVoucher.Text = Convert.ToDouble(dt1.Rows[3]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            //this.lbltoPdcVoucher.Text = Convert.ToDouble(dt1.Rows[4]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            //this.lbltotalvoucher.Text = Convert.ToDouble(dt1.Rows[5]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
        }
        private void FooterCalCulation()
        {
            //DataTable dt = (DataTable)Session["tblunposted"];
            //if (dt.Rows.Count == 0)
            //    return;

            //((Label)this.gvAccVoucher.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            //0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");
            //Session["Report1"] = gvAccVoucher;
            //((HyperLink)this.gvAccVoucher.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

         //   Hashtable hst = (Hashtable)Session["tblLogin"];
         //   string comcod = hst["comcod"].ToString();
         //   string comnam = hst["comnam"].ToString();
         //   string comadd = hst["comadd1"].ToString();
         //   string compname = hst["compname"].ToString();
         //   string username = hst["username"].ToString();
         //   string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
         //   string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
         //   string userinfo = ASTUtility.Concat(compname, username, printdate);
         //   string vouType = this.ddlvoucher.SelectedItem.Text.ToString();
         ////   Session_update();

         //   DataTable dt = (DataTable)Session["tblunposted"];
         //   DataTable dt2 = (DataTable)Session["tblusrvoucount"];

         //   if (dt.Rows.Count == 0)
         //       return;


         //   // GET DATA FROM EMP STATUS
           
         //   int index;
         //   for (int i = 0; i < this.gvAccVoucher.Rows.Count; i++)
         //   {
         //       string isPrint  = (((CheckBox)gvAccVoucher.Rows[i].FindControl("checkPrint")).Checked) ? "True" : "False";
         //       if (isPrint == "True")
         //       {
         //           ((CheckBox)gvAccVoucher.HeaderRow.FindControl("checkTopPrintAll")).Checked = true;
         //       }
         //       index = (this.gvAccVoucher.PageSize) * (this.gvAccVoucher.PageIndex) + i;
         //       dt.Rows[index]["isprint"] = isPrint;
         //   }

         //   string isCheckPrint = ((CheckBox)gvAccVoucher.HeaderRow.FindControl("checkTopPrintAll")).Checked ? "1" : "";
         //   if(isCheckPrint == "1")
         //   {
         //       DataView dv = dt.DefaultView;
         //       dv.RowFilter = "isprint = 'True'";
         //       dt = dv.ToTable();
         //   }
             
           
             

             


         //   string txtUser = ((TextBox)gvAccVoucher.HeaderRow.FindControl("txtSearusrname")).Text.Trim();
         //   if (txtUser.Length > 0)
         //   {
         //       DataView dv0 = dt.DefaultView;
         //       dv0.RowFilter = "usrname LIKE '%" + txtUser + "%'";
         //       dt = dv0.ToTable();

         //       DataView dv1 = dt2.DefaultView;
         //       dv1.RowFilter = "usrname LIKE '%" + txtUser + "%'";
         //       dt2 = dv1.ToTable();
         //   }






            ////store dt table all user
            //string[] array1 = new string[dt.Rows.Count];
            ////store dt2 table all user
            //string[] array2 = new string[dt2.Rows.Count];

            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    array1[i] = dt.Rows[i]["usrid"].ToString();

            //}

            //for (int i = 0; i < dt2.Rows.Count; ++i)
            //{
            //    array2[i] = dt2.Rows[i]["usrid"].ToString();
            //}

            ////store matching userid from arary1 and array2
            //string[] DifferArray = array1.Intersect(array2).ToArray();

            //DataTable dtarr = new DataTable();
            //dtarr.Columns.Add("usrid");

            ////convert to data table from DifferArray 
            //foreach (string str in DifferArray)
            //{
            //    DataRow dr = dtarr.NewRow();

            //    dr["usrid"] = str;
            //    dtarr.Rows.Add(dr);
            //}

            ////fetch matching user data  from dt2
            //var query = (from dtl1 in dt2.AsEnumerable()
            //             join dtl2 in dtarr.AsEnumerable() on dtl1.Field<string>("usrid") equals dtl2.Field<string>("usrid")
            //             select new
            //             {
            //                 comcod = dtl1.Field<string>("comcod"),

            //                 usrid = dtl1.Field<string>("usrid"),

            //                 usrname = dtl1.Field<string>("usrname"),
            //                 cashvou = dtl1.Field<decimal>("cashvou"),
            //                 bankvou = dtl1.Field<decimal>("bankvou"),
            //                 contravou = dtl1.Field<decimal>("contravou"),
            //                 jourvou = dtl1.Field<decimal>("jourvou"),
            //                 pdcvou = dtl1.Field<decimal>("pdcvou"),
            //                 tonum = dtl1.Field<decimal>("tonum"),
            //                 isprint = dtl1.Field<string>("isprint")



            //             }).ToList();
            //DataTable dtE = ASITUtility03.ListToDataTable(query);

           // var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>();
           // var list1 = dt2.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VouTopSheetSum>();
           //// var list1 = dtE.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VouTopSheetSum>();


           // LocalReport Rpt1 = new LocalReport();
           // switch (comcod)
           // {
           //     case "3101":
           //     case "3368":
           //         Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherTopSheetFinaly", list, list1, null);
           //         break;
           //     default:
           //         Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherTopSheet", list, list1, null);
           //         break;
           // }
           // Rpt1.EnableExternalImages = true;
           // Rpt1.SetParameters(new ReportParameter("compName", comnam));
           // Rpt1.SetParameters(new ReportParameter("rptTitle", "All Voucher Top Sheet"));
           // Rpt1.SetParameters(new ReportParameter("txtDate", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
           // Rpt1.SetParameters(new ReportParameter("vouType", "Type: " + vouType));
           // Rpt1.SetParameters(new ReportParameter("vouPDC", this.lbltoPdcVoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("vouCash", this.lbltoCashVoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("vouBank", this.lbltoBankVoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("vouContra", this.lbltoContraVoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("vouJournal", this.lbltoJournalVoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("vouTotal", this.lbltotalvoucher.Text));
           // Rpt1.SetParameters(new ReportParameter("userStatusPrint", isCheckPrint));
           // Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
           // Rpt1.SetParameters(new ReportParameter("txtUserInfo", userinfo));

           // Session["Report1"] = Rpt1;
           // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
           //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


           // string events = hst["events"].ToString();
           // if (Convert.ToBoolean(events) == true)
           // {
           //     string eventtype = "Print " + ((Label)this.Master.FindControl("lblTitle")).Text;
           //     string eventdesc = "Print Datat  " + ((Label)this.Master.FindControl("lblTitle")).Text;
           //     string eventdesc2 = "Data Show Date Range : " + "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") +
           //         " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "Voucher Type :" + vouType;

           //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



           // }

        }
        protected void gvAccVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
            //    HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");
            //    HyperLink hlnkChequePrint = (HyperLink)e.Row.FindControl("hlnkChequePrint");

            //    Label lblvounum = (Label)e.Row.FindControl("lblvounum");
            //    Label lblgvvouamt = (Label)e.Row.FindControl("lblgvvouamt");



            //    string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
            //    string voutype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "voutype")).ToString().Trim();

            //    string vounum1 = vounum.Substring(0, 2);

            //    string paytype = this.ChboxPayee.Checked ? "0" : "1";

            //    string cquepbl = (this.checkpb.Checked) == false ? "0" : "1";
            //    string woutchqdat = (this.withoutchqdate.Checked) ? "1" : "0";

            //    if (voutype == "U")
            //    {
            //        lblvounum.Attributes["style"] = "font-weight:bold; color:magenta;";
            //        lblgvvouamt.Attributes["style"] = "font-weight:bold; color:magenta;";


            //    }


            //    if (this.checkpb.Checked == true)
            //    {
            //        hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
            //        hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;
            //        if (vounum1 == "BD" || vounum1 == "CT")
            //        {
            //            hlnkChequePrint.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=AccCheque&vounum=" + vounum + "&paytype=" + paytype + "&pbl=" + cquepbl + "&woutchqdat=" + woutchqdat;
            //        }
            //        else
            //        {
            //            hlnkChequePrint.Visible = false;
            //        }
            //    }



            //    else
            //    {
            //        hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
            //        hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;
            //        if (vounum1 == "BD" || vounum1 == "CT")
            //        {
            //            hlnkChequePrint.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=AccCheque&vounum=" + vounum + "&paytype=" + paytype + "&pbl=" + cquepbl + "&woutchqdat=" + woutchqdat;
            //        }
            //        else
            //        {
            //            hlnkChequePrint.Visible = false;
            //        }
            //    }


            //}
        }

      

        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["AllVoucher"];
          

            for (int i = 0; i < this.gvAccVoucher.Rows.Count; i++)
            {
                if (((CheckBox)this.gvAccVoucher.Rows[i].FindControl("chkCol")).Checked)
                {
                   dt.Rows[i].Delete();                 
                   
                   
                    
                }
            }

            Session["AllVoucher"] = dt;
            this.Data_Bind();

        }

        protected void gvAccVoucher_Sorting(object sender, GridViewSortEventArgs e)
        {

            //    Session["tblunposted"]
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["AllVoucher"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            // Session["SortedView"] = sortedView;
            gvAccVoucher.DataSource = sortedView;
            gvAccVoucher.DataBind();
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

    }
}