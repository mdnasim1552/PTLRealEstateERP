using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_12_Inv
{
    public partial class RptProjectStock : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "acc" ? "MATERIALS STOCK REPORT " : "MATERIALS STOCK REPORT INVENTORY");

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterial();
                if (Request.QueryString.AllKeys.Contains("prjcode") && this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string Complength()
        {
            string comcod = this.GetCompCode();
            string Complength = "";
            switch (comcod)
            {
                // case "3101":
                case "3348":
                    Complength = "Length";
                    break;

                default:
                    Complength = "";

                    break;
            }

            return Complength;


        }

        private string CompCallType()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                case "3101":
                case "2325":
                case "3325":
                    ctype = "GETPURPROJECTNAMELEISURE";
                    break;
                default:
                    ctype = "GETPURPROJECTNAME";
                    break;
            }
            return ctype;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string length = this.Complength();
            string userid = hst["usrid"].ToString();
            string ctype = this.CompCallType();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", ctype, serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.ddlProName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
            }

        }


        //private void GetMaterial()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string pactcode = this.ddlProName.SelectedValue.ToString();
        //    string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
        //    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
        //    //this.listGroup.DataTextField = "rsirdesc";
        //    //this.listGroup.DataValueField = "rsirdesc";
        //    //this.listGroup.DataSource = ds1.Tables[0];
        //    //this.listGroup.DataBind();
        //    //this.listGroup.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
        //    this.DropCheck1.DataTextField = "rsirdesc";
        //    this.DropCheck1.DataValueField = "rsirdesc";
        //    this.DropCheck1.DataSource = ds1.Tables[0];
        //    this.DropCheck1.DataBind();
        //    if (Request.QueryString.AllKeys.Contains("prjcode"))
        //    {
        //        this.DropCheck1.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
        //    }

        //    ds1.Dispose();


        //}

        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.chkResourcelist.DataTextField = "rsirdesc";
            this.chkResourcelist.DataValueField = "rsircode";
            this.chkResourcelist.DataSource = ds1.Tables[0];
            this.chkResourcelist.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.chkResourcelist.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
            }

            ds1.Dispose();


        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStock();
        }
        private void MatStock()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string chalan = this.chln.Checked ? "chalan" : "";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string calltype = (this.Request.QueryString["Type"].ToString() == "acc") ? "RPTPROJECTSTOCK"
                                : (this.Request.QueryString["Type"].ToString() == "invWithSpec") ? "RPTPROSTOCKINVSPC"
                                : "RPTPROSTOCKINV";
            string pactcode = "";
            switch (comcod)
            {
                case "2305":
                case "3325":
                case "3101":
                    if (calltype == "RPTPROSTOCKINV")
                    {
                        pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
                    }
                    else
                    {
                        pactcode = this.ddlProName.SelectedValue.ToString();
                    }
                    break;
                default:
                    pactcode = this.ddlProName.SelectedValue.ToString();
                    break;
            }

            //    //string grpcode = "";


            //    //foreach (ListItem item in listGroup.Items)
            //    //{
            //    //    if (item.Selected)
            //    //    {
            //    //        grpcode += item.Value;
            //    //    }
            //    //}
            //    string[] sec = this.DropCheck1.Text.Trim().Split(',');
            //    if (sec[0].Substring(0, 4) == "0000")
            //        rsircode = "";
            //    else
            //        foreach (string s1 in sec)
            //            rsircode = rsircode + s1.Substring(0, 12);

            string rsircode = "";

            string gp = this.chkResourcelist.SelectedValue.ToString().Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    rsircode = "";
                else
                    foreach (ListItem s1 in chkResourcelist.Items)
                    {
                        if (s1.Selected)
                        {
                            rsircode = rsircode + s1.Value.Substring(0, 12);
                        }
                    }

            }



            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", calltype, pactcode, fdate, tdate, mRptGroup, rsircode, chalan, "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            //Session["tbMatStc"]=ds1.Tables[0];
            Session["tbMatStc"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }






        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string actcode = "";

            switch (Type)
            {

                case "invWithSpec":
                    string isircod = dt1.Rows[0]["rptcod"].ToString();
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["rptcod"].ToString() == isircod)
                        {

                            dt1.Rows[i]["rptdesc1"] = "";
                        }
                        isircod = dt1.Rows[i]["rptcod"].ToString();
                    }
                    break;

                case "inv":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {

                            dt1.Rows[j]["pactdesc"] = "";
                        }
                        pactcode = dt1.Rows[j]["pactcode"].ToString();
                    }
                    break;
            }

            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "acc":
                    this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatStock.DataSource = dt;
                    this.gvMatStock.DataBind();
                    this.FooterCalculation();
                    break;

                case "inv":
                    this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatStock.DataSource = dt;
                    this.gvMatStock.DataBind();
                    this.showStockInv();
                    this.FooterCalculation();
                    break;

                case "invWithSpec":
                    this.gvMatStockSpec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatStockSpec.DataSource = dt;
                    this.gvMatStockSpec.DataBind();
                    break;
            }


        }

        private void showStockInv()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3340":
                    this.gvMatStock.Columns[1].Visible = false;
                    this.gvMatStock.Columns[4].Visible = false;
                    this.gvMatStock.Columns[13].Visible = false;
                    break;

                case "3101":
                case "2325":
                case "3325":
                    this.gvMatStock.Columns[1].Visible = true;
                    this.gvMatStock.Columns[4].Visible = true;
                    this.gvMatStock.Columns[13].Visible = true;
                    break;

                default:
                    this.gvMatStock.Columns[1].Visible = false;
                    this.gvMatStock.Columns[4].Visible = true;
                    this.gvMatStock.Columns[13].Visible = true;
                    break;
            }
        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvFbudgetqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ? 0.00 : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvOpF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opqty)", "")) ? 0.00 : dt.Compute("Sum(opqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvRecamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvqty)", "")) ? 0.00 : dt.Compute("Sum(rcvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvTraninqtyF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninqty)", "")) ? 0.00 : dt.Compute("Sum(trninqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvTranoutqtyF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutqty)", "")) ? 0.00 : dt.Compute("Sum(trnoutqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvDamageF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lqty)", "")) ? 0.00 : dt.Compute("Sum(lqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvTamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00 : dt.Compute("Sum(tqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvIsuamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueqty)", "")) ? 0.00 : dt.Compute("Sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvAcSamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acstock)", "")) ? 0.00 : dt.Compute("Sum(acstock)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvFdiviation")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(diviation)", "")) ? 0.00 : dt.Compute("Sum(diviation)", ""))).ToString("#,##0.00;(#,##0.00); ");

                Session["Report1"] = gvMatStock;
                ((HyperLink)this.gvMatStock.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            }

            else
            {
                return;
            }





        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            if (comcod == "3330")
            {
                this.RtpStockReportBr();
            }

            else
            {
                this.RtpStockReportGeneral();
            }




        }

        private void RtpStockReportBr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string Headertitle = "";
            if (this.Request.QueryString["Type"].ToString() == "inv")
            {
                Headertitle = "Materials Stock Information(Inventory)";
            }

            else
            {
                Headertitle = "Materials Stock Information";
            }
            DataTable dt1 = (DataTable)Session["tbMatStc"];
            if (dt1 == null)
                return;

            //Materials Stock Information

            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.ErptStock>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptProMatStock", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("header", Headertitle));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            //Rpt1.SetParameters(new ReportParameter("txtresdesc", "Resdesc"));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("resdes", resourcedes));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RtpStockReportGeneral()//09-May-2020
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string Headertitle = "";
            if (this.Request.QueryString["Type"].ToString() == "inv")
            {

                Headertitle = "Materials Stock Information(Inventory)";
            }

            DataTable dt1 = (DataTable)Session["tbMatStc"];

            if (comcod == "3315" || comcod == "3316")
            {
                DataView dv = dt1.DefaultView; //only Assure
                dv.RowFilter = ("tqty<>0 or opqty<>0 or rcvqty<>0 or trninqty<>0 or trnoutqty<>0");
                dt1 = dv.ToTable();

            }

            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.ErptStock>();
            LocalReport Rpt1 = new LocalReport();
            if(comcod=="2305" || comcod=="3325" || comcod == "3101")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptProMatStock2Leisure", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            }
            else if (comcod == "3370")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptProMatStock2CPDL", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptProMatStock2", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("header", Headertitle));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvMatStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkgcResDesc = (HyperLink)e.Row.FindControl("hlnkgcResDesc");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bldcod")).ToString();
                string rsircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();
                string rsirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptdesc1")).ToString();
                if (pactcode == "")
                {
                    return;
                }

                else
                {



                    hlnkgcResDesc.Style.Add("color", "blue");
                    string chalan = this.chln.Checked ? "chalan" : "";
                    hlnkgcResDesc.NavigateUrl = "~/F_12_Inv/LinkMatSpeciStock.aspx?pactcode=" + pactcode + "&rsircode=" + rsircode + "&frmdate=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "&pactdesc=" + this.ddlProName.SelectedItem.Text + "&rsirdesc=" + rsirdesc + "&chalan=" + chalan;



                }

            }

        }
        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
    }
}

