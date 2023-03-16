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
//using RMGiRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_34_Mgt
{
    public partial class RptUserLogDetails : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string today = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = today;

                // this.txttodate.Attributes.Add("data-default-date", today);
                this.GetUserName();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "USER LOG DETAILS LIST";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetUserName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETUSERNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlUserName.DataTextField = "usrsname";
            this.ddlUserName.DataValueField = "usrid";
            this.ddlUserName.DataSource = ds1.Tables[0];
            this.ddlUserName.DataBind();
            this.ddlUserName.SelectedValue = "0000000";
            ds1.Dispose();


        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetUserName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.UserLogStatus();
        }

        private void UserLogStatus()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usercode = this.ddlUserName.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string modtyp = ddltype.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLogType.DataSource = null;
                this.gvLogType.DataBind();

                this.gvstatus.DataSource = null;
                this.gvstatus.DataBind();
                return;
            }
            if (modtyp != "0")
            {
                DataView dv = ds1.Tables[0].DefaultView;
                DataView dv1 = ds1.Tables[1].DefaultView;
                dv.RowFilter = "grp = '" + modtyp + "'";
                dv1.RowFilter = "grp = '" + modtyp + "'";
                DataTable dt1 = dv.ToTable();
                Session["UserLog"] = HiddenSameData(dv.ToTable());
                Session["UserLogDetails"] = (dv1.ToTable());
            }
            else
            {
                Session["UserLog"] = HiddenSameData(ds1.Tables[0]);
                Session["UserLogDetails"] = (ds1.Tables[1]);
            }

            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "User Input List";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlUserName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            string vonum = dt1.Rows[0]["number"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if (dt1.Rows[j]["number"].ToString() == vonum)
                {

                    vonum = dt1.Rows[j]["number"].ToString();
                    dt1.Rows[j]["number"] = "";
                    dt1.Rows[j]["valdat"] = "";
                    dt1.Rows[j]["entrydat"] = "";


                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    vonum = dt1.Rows[j]["number"].ToString();
                }
            }



            return dt1;

        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["UserLog"];

            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("rgp='A'");
            this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLogType.DataSource = dt;
            this.gvLogType.DataBind();

            //DataView dv = dt.Copy().DefaultView;
            //dv.RowFilter = ("rgp='A'");
            //this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvLogType.DataSource = dv.ToTable();
            //this.gvLogType.DataBind();

            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("rgp='B'");
            this.gvLogType2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLogType2.DataSource = dv1.ToTable();
            this.gvLogType2.DataBind();

            if (dt.Rows.Count == 0)
                return;
            if (dv.ToTable().Rows.Count != 0)
            {
                Session["Report1"] = gvLogType;
                ((HyperLink)this.gvLogType.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }


            //DataTable dt = (DataTable)Session["UserLog"];

            //this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvLogType.DataSource = dt;
            //this.gvLogType.DataBind();


            //if (dt.Rows.Count == 0)
            //    return;
            //Session["Report1"] = gvLogType;
            //((HyperLink)this.gvLogType.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //

            DataTable dt1 = (DataTable)Session["UserLogDetails"];

            this.gvstatus.DataSource = dt1;
            this.gvstatus.DataBind();

        }
        protected void gvLogType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvType");
                string gennum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gennum")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (gennum == "")
                {
                    return;
                }

                if (gennum.Length == 9)
                {
                    // hlink1.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + gennum + "&paytype=" + "0";
                    hlink1.NavigateUrl = ("~/F_22_Sal/RptMktMoneyReceipt.aspx?Type=CustCare&genno=" + gennum);

                }



                else if (gennum.Substring(0, 2) == "BC" || gennum.Substring(0, 2) == "BD" || gennum.Substring(0, 2) == "CC"
                 || gennum.Substring(0, 2) == "CD" || gennum.Substring(0, 2) == "CT" || gennum.Substring(0, 2) == "JV")
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + gennum;//?type=ConstRpt&prjcode="+code;
                                                                                         // hlink1.Style.Add("color", "blue");
                }
                else if (gennum.Substring(0, 2) == "PV" || gennum.Substring(0, 2) == "DV")
                {
                    // hlink1.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + gennum + "&paytype=" + "0";
                    hlink1.NavigateUrl = ("~/F_17_Acc/RptAccVouher02.aspx?vounum=" + gennum);

                }





                else if (gennum.Substring(0, 3) == "REQ")
                {
                    hlink1.NavigateUrl = ("~/F_14_Pro/RptPurchasetracking.aspx?Type=Purchasetrk&reqno=" + gennum);
                }


                else if (gennum.Substring(0, 3) == "PAP")
                {
                    hlink1.NavigateUrl = "~/F_14_Pro/PurAprovEntry.aspx?InputType=ProposalEdit&genno=" + gennum;
                }

                else if (gennum.Substring(0, 3) == "POR")
                {
                    hlink1.NavigateUrl = "~/F_14_Pro/PurWrkOrderEntry.aspx?InputType=OrderEdit&genno=" + gennum;
                }


                else if (gennum.Substring(0, 3) == "MRR")
                {
                    hlink1.NavigateUrl = "~/F_12_Inv/PurMRREntry.aspx?Type=Mgt&prjcode=&genno=" + gennum + "&sircode=";
                }


                else if (gennum.Substring(0, 3) == "PBL")
                {
                    hlink1.NavigateUrl = "~/F_14_Pro/PurBillEntry.aspx?Type=BillEdit&genno=" + gennum + "&sircode=";
                }


                else if (gennum.Substring(0, 3) == "ISU")
                {
                    hlink1.NavigateUrl = "~/F_12_Inv/PurMatIssue.aspx?Type=Mgt&genno=" + gennum;
                }


                else if (gennum.Substring(0, 3) == "TRN")
                {
                    hlink1.NavigateUrl = "~/F_12_Inv/MaterialsTransfer.aspx?Type=Entry&genno=" + gennum;
                }


                else if (gennum.Substring(0, 3) == "WEN")
                {
                    hlink1.NavigateUrl = "~/F_09_PImp/PurIssueEntry.aspx?Type=Report&genno=" + gennum;
                }

                else if (gennum.Substring(0, 3) == "WEN")
                {
                    hlink1.NavigateUrl = "~/F_09_PImp/PurIssueEntry.aspx?Type=Report&genno=" + gennum;
                }

                else if (gennum.Substring(0, 3) == "LIS")
                {
                    hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=ConBillPrint&lisuno=" + gennum + "&pactcode=" + pactcode;
                }



                else if (gennum.Substring(0, 3) == "CBL")
                {

                    hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=ConBillFinalization&billno=" + gennum;

                }


                //    PurSubConBillFinal.aspx?Type=BillEdit&prjcode=&genno=&sircode=






            }
        }

        protected void lbtngvEditUser_Click(object sender, EventArgs e)
        {

            this.lbmodalheading.Text = "Edit Voucher Details";

            //GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int RowIndex = gvr.RowIndex;
            //int rownumber = this.gvLogType.PageSize * this.gvLogType.PageIndex + RowIndex;

            //string vounum = ((DataTable)Session["UserLog"]).Rows[rownumber]["gennum"].ToString();
            ////string vounum = ((DataTable)Session["UserLog"]).Rows[RowIndex]["gennum"].ToString();

            //this.LabelVounum.Text = vounum;
            //this.GetEditDet(vounum);

            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rownumber = this.gvLogType.PageSize * this.gvLogType.PageIndex + rownum;         
            string vounum = ((DataTable)Session["UserLog"]).Rows[rownumber]["gennum"].ToString();
            this.LabelVounum.Text = vounum;
            this.GetEditDet(vounum);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
           
        }

        private void GetEditDet(string vounum)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHERRECORD", vounum, "", "", "", "", "", "", "", "");
            DataTable dt = HiddenSameDataEditModal(ds1.Tables[0]);//this.HiddenSameDatadet(ds1.Tables[0]);
            DataTable dt1 = ds1.Tables[1];//this.HiddenSameDatadet(ds1.Tables[0]);
            this.mgvbreakdown.DataSource = dt;
            this.mgvbreakdown.DataBind();
            if (dt1.Rows.Count != 0)
            {
                this.LabelControl.Text = dt1.Rows[0]["cactdesc"].ToString() == "" ? "" : dt1.Rows[0]["cactdesc"].ToString();
                this.LabelRefNo.Text = dt1.Rows[0]["refnum"].ToString() == "" ? "" : dt1.Rows[0]["refnum"].ToString();
                DateTime voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]);
                this.LabelVoudat.Text = dt1.Rows[0]["voudat"].ToString() == "" ? "" : voudat.ToString("dd-MMM-yyyy");
                this.LabelNar.Text = dt1.Rows[0]["venar"].ToString() == "" ? "" : dt1.Rows[0]["venar"].ToString();
            }
            //int overmin = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(ovmin)", "")) ? 0.00 : dt.Compute("sum(ovmin)", "")));
            //int ovhour = (int)(overmin / 60);
            //int rovermin = (int)(overmin % 60);
            //rovermin = rovermin > 30 ? 1 : 0;
            //ovhour = ovhour + rovermin;

            //((Label)this.mgvbreakdown.FooterRow.FindControl("mlgvFDelday")).Text = Convert.ToDouble(ovhour).ToString("#,##0;(#,##0); ");


            ds1.Dispose();

        }

        private DataTable HiddenSameDataEditModal(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string vstatus = dt1.Rows[0]["vstatus"].ToString();
            //string vonum = dt1.Rows[0]["number"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vstatus"].ToString() == vstatus)
                {
                    vstatus = dt1.Rows[j]["vstatus"].ToString();
                    dt1.Rows[j]["vstatus"] = "";

                }
                else
                {
                    vstatus = dt1.Rows[j]["vstatus"].ToString();
                }
            }
            return dt1;

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintLogAll();
        }


        private void PrintLogAll()
        {
            string rptDt = "Date: From: " + this.txtfromdate.Text.ToString() + " To " + this.txttodate.Text.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string username1 = "Name: " + this.ddlUserName.SelectedItem.ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["UserLog"];
            DataTable dt2 = (DataTable)Session["UserLogDetails"];

            var list = dt.DataTableToList<RealEntity.C_34_Mgt.EClassUserLog>();
            var list2 = dt2.DataTableToList<RealEntity.C_34_Mgt.EClassUserLogSummary>();

            //var list = dt.DataTableToList<MFGOBJ.C_34_Mgt.UserLogDetails>();


            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass1.GetLocalReport("R_34_Mgt.UserLogDetails", list, list2, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            rpt1.SetParameters(new ReportParameter("Date", rptDt));
            rpt1.SetParameters(new ReportParameter("rptTitle", "User Log Details"));
            //rpt1.SetParameters(new ReportParameter("FromToDate", rptDt));
            //rpt1.SetParameters(new ReportParameter("username", username1));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat("", username, printdate)));

            ////rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvLogType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLogType.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLogType2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLogType2.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}

