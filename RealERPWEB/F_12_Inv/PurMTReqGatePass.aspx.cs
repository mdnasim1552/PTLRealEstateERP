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
namespace RealERPWEB.F_12_Inv
{
    public partial class PurMTReqGatePass : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                if ((this.Request.QueryString["genno"].ToString().Length > 0))
                {
                    this.ImgbtnFindRes_Click();
                    this.lbtnOk_Click(null, null);
                 

                    if (this.Request.QueryString["Type"].ToString() == "GpaEdit")
                    {
                        this.lbtnSelectAll_Click(null, null);
                        this.getpannelHide();
                    }
                    this.pnlproj.Visible = false;
                    this.Panel1.Visible = true;
                }
                else
                {

                }               

               ((Label)this.Master.FindControl("lblTitle")).Text = "Get Pass";

                // ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["title"].ToString();
                this.txtCurAprovDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurAprovDate_CalendarExtender.EndDate = System.DateTime.Today;
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void getpannelHide()
        {
            this.txtGatemPassNo.Text = this.Request.QueryString["gpref"].ToString();
            this.txtGatemPassNo.ReadOnly = true;

        }

        private void getProjectInfo()
        {
            string comcod = this.GetCompCode();
            string todate = this.GetStdDate(this.txtCurAprovDate.Text.Trim());

            DataSet ds1 = purData.GetTransInfo(comcod, "[dbo].[SP_ENTRY_PURCHASE_05]", "GETINVPROJLIST", todate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            Session["tblproject"] = ds1.Tables[0];
            Session["tblreqinfo"] = ds1.Tables[1];
            this.Load_Project_From_Combo();
        }


        protected void Load_Project_From_Combo()
        {

            DataTable dt = (DataTable)Session["tblproject"];
            this.ddlprjlistfrom.DataTextField = "tfdesc";
            this.ddlprjlistfrom.DataValueField = "tfpactcode";
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "tfpactcode <> '000000000000'";
            DataTable dt1 = dv1.ToTable();

            this.ddlprjlistfrom.DataSource = dt1;
            this.ddlprjlistfrom.DataBind();
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }
        protected void Load_Project_To_Combo()
        {
            string comcod = this.GetCompCode(); 

            DataTable dt = (DataTable)Session["tblproject"];

            string actcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "ttpactcode not in ('" + actcode + "') and ttpactcode<>'000000000000'";
            DataTable dt1 = dv1.ToTable();

            this.ddlprjlistto.DataTextField = "ttdesc";
            this.ddlprjlistto.DataValueField = "ttpactcode";
            this.ddlprjlistto.DataSource = dt1;
            this.ddlprjlistto.DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)ViewState["tblgetPass"];
            //
            string mGetpNo = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPADDANDOTHER", mGetpNo, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;



            string fpactdesc = dt1.Rows[0]["tfpactdesc"].ToString();
            string tpactdesc = dt1.Rows[0]["ttpactdesc"].ToString();
            string mtrref = dt1.Rows[0]["mtrref"].ToString();
            string mtrdat = Convert.ToDateTime(dt1.Rows[0]["mtrdat"]).ToString("dd.MM.yyyy");

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.PurGetPass>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsGatepass", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtfpactdesc", fpactdesc + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtPrjAddress", tpactdesc + "\n" + ds1.Tables[0].Rows[0]["ttpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rpttxtmgatepno", this.txtGatemPassNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtmtrref", "MTRF No: " + mtrref));

            Rpt1.SetParameters(new ReportParameter("txtDate", this.txtCurAprovDate.Text));
            Rpt1.SetParameters(new ReportParameter("txtmtrdat", mtrdat));
            Rpt1.SetParameters(new ReportParameter("Getpassno", "Gate Pass No : " + this.lblGatePassNo1.Text.Trim() + this.txtGatePassNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtgetpNarr.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Gete Pass"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptFassttran = new RealERPRPT.R_12_Inv.RptMaterialTrnsGatepass();

            //TextObject rptCname = rptFassttran.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtadd = rptFassttran.ReportDefinition.ReportObjects["txtCompanyadd"] as TextObject;
            //txtadd.Text = comadd;
            //TextObject rptProjectNameft = rptFassttran.ReportDefinition.ReportObjects["ProjectNamef"] as TextObject;
            //rptProjectNameft.Text = fpactdesc + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString();

            //TextObject rpttxtmgatepno = rptFassttran.ReportDefinition.ReportObjects["rpttxtmgatepno"] as TextObject;
            //rpttxtmgatepno.Text = this.txtGatemPassNo.Text.Trim();
            //TextObject rptdate = rptFassttran.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text =this.txtCurAprovDate.Text;

            //TextObject rpttxtGatePassNo = rptFassttran.ReportDefinition.ReportObjects["Getpassno"] as TextObject;
            //rpttxtGatePassNo.Text = "Gate Pass No : " +this.lblGatePassNo1.Text.Trim() + this.txtGatePassNo2.Text.Trim();

            //TextObject txttoproj = rptFassttran.ReportDefinition.ReportObjects["txttoproj"] as TextObject;
            //txttoproj.Text = tpactdesc + "\n" + ds1.Tables[0].Rows[0]["ttpactadd"].ToString();


            //TextObject txtmtrref = rptFassttran.ReportDefinition.ReportObjects["txtmtrref"] as TextObject;
            //txtmtrref.Text = "MTRF No: "+ mtrref;
            //TextObject txtmtrdat = rptFassttran.ReportDefinition.ReportObjects["txtmtrdat"] as TextObject;
            //txtmtrdat.Text = mtrdat;
            //TextObject txtnarration = rptFassttran.ReportDefinition.ReportObjects["txtnarration"] as TextObject;
            //txtnarration.Text = this.txtgetpNarr.Text.Trim();

            //TextObject txtuserinfo = rptFassttran.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptFassttran.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptFassttran.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptFassttran;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        private void PreviousList()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string GatePassNo = "%" + this.txtGatePassNo.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREGETPASSNO", CurDate1,
                          GatePassNo, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "getpno1";
            this.ddlPrevList.DataValueField = "getpno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }

        protected void lbtnPrevAprovList_Click(object sender, EventArgs e)
        {

            //string comcod = this.GetCompCode();
            //string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREGETPASSNO", CurDate1,
            //              "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlPrevList.Items.Clear();
            //this.ddlPrevList.DataTextField = "getpno1";
            //this.ddlPrevList.DataValueField = "getpno";
            //this.ddlPrevList.DataSource = ds1.Tables[0];
            //this.ddlPrevList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lblpreGatePassNo.Visible = true;
                this.txtGatePassNo.Visible = true;
                this.ImgbtnFinGatePass.Visible = true;

                //this.lbtnPrevAprovList.Visible = true;
                this.ddlPrevList.Visible = true;
                this.lblGatePassNo1.Text = "GPN" + DateTime.Today.ToString("MM") + "-";
                this.txtGatePassNo2.Text = "0000";
                this.txtGatemPassNo.Text = "";

                this.txtCurAprovDate.Enabled = true;

                //this.txtResSearch.Text = "";
                this.ddlPrevList.Items.Clear();
                this.ddlResList.Items.Clear();
                this.ddlResourcelist.Items.Clear();
                this.ddlSpecification.Items.Clear();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.txtgetpNarr.Text = "";
                this.gvAprovInfo.DataSource = null;
                this.gvAprovInfo.DataBind();
                this.Panel1.Visible = false;

                this.lbtnOk.Text = "Ok";
                this.pnlproj.Visible = false;
                this.lbtnPrject.Visible = true;

                return;
            }

            //this.lbtnPrevAprovList.Visible = false;
            this.lblpreGatePassNo.Visible = false;
            this.txtGatePassNo.Visible = false;
            this.ImgbtnFinGatePass.Visible = false;
            this.ddlPrevList.Visible = false;
            this.txtGatePassNo2.ReadOnly = true;            
            this.lbtnOk.Text = "New";
            this.Get_Pass_Info();
            this.VisibleEntry();
            this.StockBal();




        }

        private void StockBal()
        {
            string comcod = this.GetCompCode();
            // this.Request.QueryString["genno"].ToString().Length > 0)


            string ProjectCode = this.Request.QueryString.AllKeys.Contains("frmpactcode") ? this.Request.QueryString["frmpactcode"].ToString() : this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc =  "%";
            string curdate = this.GetStdDate(this.txtCurAprovDate.Text.Trim());

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "STOCKBALANCEGETPASS", ProjectCode, curdate, FindResDesc, "", "", "", "", "", "");
            Session["tblStockbal"] = ds1.Tables[0];
            if (ds1 == null)
                return;


        }

        


        private void VisibleEntry()
        {
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.pnlproj.Visible = false;
                this.Panel1.Visible = true;

            }
            else
            {
                this.pnlproj.Visible = true;
                this.getProjectInfo();
            }
            //this.lCurAppdate.Visible = true;
            //this.lcurApprNo.Visible = true ;
            //this.txtCurAprovDate.Visible = true;
            //this.lblCurAprovNo1.Visible = true;
            //this.txtCurAprovNo2.Visible = true;

            //this.lbtnAprove.Visible = true;
            //this.lblResList.Visible = false;
            //this.lblResList0.Visible = false;
            //this.lblResList1.Visible = false;
            //this.txtResSearch.Visible = false;
            //this.txtSupSearch.Visible = false;
            //this.ImgbtnFindRes.Visible = false;
            //this.ImgbtnFindSup.Visible = false;
            //this.lbtnSelectRes.Visible = false;
            //this.lbtnSelectAll.Visible = false;
            //this.lblSpecification.Visible = false;
            //this.ddlSpecification.Visible = false;
            //this.ddlPayType.Visible = false;
            //this.ddlResList.Visible = false;
            //this.ddlSupList.Visible = false;
            //this.lblResList2.Visible = false;
            //this.ddlResourcelist.Visible = false;

        }

        protected void GetGetPassNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGPassNo = "NEWGPASS";
            if (this.ddlPrevList.Items.Count > 0)
            {
                mGPassNo = this.ddlPrevList.SelectedValue.ToString();
            }

            if (mGPassNo == "NEWGPASS")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETLASTGETPNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblGatePassNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtGatePassNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                    this.ddlPrevList.DataTextField = "maxno1";
                    this.ddlPrevList.DataValueField = "maxno";
                    this.ddlPrevList.DataSource = ds1.Tables[0];
                    this.ddlPrevList.DataBind();
                }

            }
        }



        protected void Get_Pass_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGPassNo = "NEWGPASS";
            if (this.ddlPrevList.Items.Count > 0)
            {
                mGPassNo = this.ddlPrevList.SelectedValue.ToString();
                this.txtCurAprovDate.Enabled = false;
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPURGERPASSINFO", mGPassNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblgetPass"] = this.HiddenSameData(ds1.Tables[0]);

            Session["tblgetPassSInfo"] = ds1.Tables[1];
            //this.lbtnResFooterTotal_Click(null, null);


            if (mGPassNo == "NEWGPASS")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETLASTGETPNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblGatePassNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtGatePassNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblGatePassNo1.Text = ds1.Tables[1].Rows[0]["getpno1"].ToString().Substring(0, 6);
            this.txtGatePassNo2.Text = ds1.Tables[1].Rows[0]["getpno1"].ToString().Substring(6, 5);
            this.txtGatemPassNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.txtCurAprovDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["getpdat"]).ToString("dd.MM.yyyy");
            this.txtgetpNarr.Text = ds1.Tables[1].Rows[0]["getpnar"].ToString();
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgetPass"];

            this.gvAprovInfo.DataSource = dt;
            this.gvAprovInfo.DataBind();
        }

        private void FooterCalculation()
        {
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            if (tbl1.Rows.Count == 0)
                return;
            ((Label)this.gvAprovInfo.FooterRow.FindControl("lblgvFooterTAprovAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(aprovamt)", "")) ?
                    0.00 : tbl1.Compute("Sum(aprovamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "mtreqno";
            dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            // string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else

                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }


        protected void ImgbtnFindRes_Click()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string SerchText = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETMTREQLIST", CurDate1,
                          SerchText, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblsp"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            this.ddlResList.DataTextField = "textfield";
            this.ddlResList.DataValueField = "valuefiled";
            this.ddlResList.DataSource = ds1.Tables[2];
            this.ddlResList.DataBind();
            this.ddlResList_SelectedIndexChanged(null, null);

        }

        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblRes"];
            string reqno = this.ddlResList.SelectedValue.ToString();

            DataView dv = dtres.DefaultView;
            this.ddlResourcelist.Items.Clear();
            dv.RowFilter = "mtreqno in ('" + reqno + "')";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";
            DataTable dtd = dv.ToTable();
            this.ddlResourcelist.DataTextField = "rsirdesc";
            this.ddlResourcelist.DataValueField = "rsircode";
            this.ddlResourcelist.DataSource = dv.ToTable();
            this.ddlResourcelist.DataBind();
            this.ddlResourcelist_SelectedIndexChanged(null, null);
        }
        protected void ddlResourcelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblsp"];
            string reqno = this.ddlResList.SelectedValue.ToString();
            string rsircode = this.ddlResourcelist.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;

            dv.RowFilter = "mtreqno='" + reqno + "' and  rsircode='" + rsircode + "'";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";

            this.ddlSpecification.DataTextField = "textfield";
            this.ddlSpecification.DataValueField = "valuefiled";
            this.ddlSpecification.DataSource = dv.ToTable();
            this.ddlSpecification.DataBind();

        }




        protected void Session_tblAprov_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            int rowindex;
            for (int j = 0; j < this.gvAprovInfo.Rows.Count; j++)
            {
                double getpqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvaprovedQty")).Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvAprovInfo.Rows[j].FindControl("lblgvaprovRate")).Text.Trim()));
                double getpamt = getpqty * rate;
                rowindex = (this.gvAprovInfo.PageIndex) * this.gvAprovInfo.PageSize + j;
                tbl1.Rows[rowindex]["getpqty"] = getpqty;
                tbl1.Rows[rowindex]["getpamt"] = getpamt;
            }

            ViewState["tblgetPass"] = tbl1;
        }
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_tblAprov_Update();
                DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
                string mReqNo = this.ddlSpecification.SelectedValue.ToString().Substring(0, 14);
                //string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
                string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
                string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);

                string frmprjcode = this.Request.QueryString.AllKeys.Contains("frmpactcode") ? this.Request.QueryString["frmpactcode"].ToString() : this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                DataTable dt5 = (DataTable)Session["tblStockbal"];

                DataRow[] dr2 = tbl1.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                            "' and spcfcod = '" + mSpcfCod + "'");

                if (dr2.Length == 0)
                {

                    // (getpno , getpno1, mtreqno, mtreqno1, rsircode, spcfcod , rsirdesc, spcfdesc, rsirunit, mtrfqty, getpqty, rate ,getpamt, mtrdat, mtrref

                    DataRow dr1 = tbl1.NewRow();
                    dr1["mtreqno"] = mReqNo;
                    dr1["rsircode"] = mResCode;
                    dr1["spcfcod"] = mSpcfCod;

                    DataTable tbl2 = (DataTable)ViewState["tblsp"];
                    DataRow[] dr3 = tbl2.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                            "' and spcfcod = '" + mSpcfCod + "'");
                    dr1["mtreqno1"] = dr3[0]["mtreqno1"];
                    dr1["mtrref"] = dr3[0]["mtrref"];
                    dr1["mtrdat"] = dr3[0]["mtrdat"];
                    dr1["tfpactcode"] = dr3[0]["tfpactcode"];
                    dr1["ttpactcode"] = dr3[0]["ttpactcode"];
                    dr1["tfpactdesc"] = dr3[0]["tfpactdesc"];
                    dr1["ttpactdesc"] = dr3[0]["ttpactdesc"];
                    dr1["rsirdesc"] = dr3[0]["rsirdesc"];
                    dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                    dr1["rsirunit"] = dr3[0]["rsirunit"];
                    dr1["mtrfqty"] = dr3[0]["mtrfqty"];
                    dr1["getpqty"] = dr3[0]["balqty"];
                    dr1["balqty"] = dr3[0]["balqty"];
                    dr1["rate"] = dr3[0]["mtrfrat"];
                    dr1["getpamt"] = dr3[0]["mtrfamt"];
                    dr1["stockbal"] =dt5.Select("pactcode = '" + frmprjcode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCod + "'")[0]["balqty"].ToString();


                    //((DataTable)Session["tblStockbal"]).Select("pactcode='" + frmprjcode + "'")[0]["balqty"].ToString();

                  string ddd=  dt5.Select("pactcode = '" + frmprjcode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCod + "'")[0]["balqty"].ToString();

                    tbl1.Rows.Add(dr1);
                }

                ViewState["tblgetPass"] = this.HiddenSameData(tbl1);
                this.Data_Bind();

            }catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }





        }

        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {


            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];

            string mReqNo = this.ddlSpecification.SelectedValue.ToString().Substring(0, 14);
            //string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
            string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);

            string rsircode1 = "", spcfcod1 = "";


            DataTable dt5 = (DataTable)Session["tblStockbal"];
            string frmprjcode = this.Request.QueryString.AllKeys.Contains("frmpactcode") ? this.Request.QueryString["frmpactcode"].ToString() : this.ddlprjlistfrom.SelectedValue.ToString().Trim();

            DataTable tbl2 = (DataTable)ViewState["tblsp"];
            DataView dv1 = tbl2.DefaultView;

            dv1.RowFilter = "mtreqno in('" + mReqNo + "')";
            tbl2 = dv1.ToTable();

            DataRow[] dr2 = tbl1.Select("mtreqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();

                    dr1["mtreqno"] = mReqNo;
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();


                    dr1["mtreqno1"] = tbl2.Rows[i]["mtreqno1"].ToString();
                    dr1["mtrref"] = tbl2.Rows[i]["mtrref"].ToString();
                    dr1["mtrdat"] = tbl2.Rows[i]["mtrdat"].ToString();
                    dr1["tfpactcode"] = tbl2.Rows[i]["tfpactcode"].ToString();


                    dr1["tfpactdesc"] = tbl2.Rows[i]["tfpactdesc"].ToString();
                    dr1["ttpactdesc"] = tbl2.Rows[i]["ttpactdesc"].ToString();

                    dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["mtrfqty"] = tbl2.Rows[i]["mtrfqty"].ToString();
                    dr1["getpqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["balqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["rate"] = tbl2.Rows[i]["mtrfrat"].ToString();
                    dr1["getpamt"] = tbl2.Rows[i]["mtrfamt"].ToString();
                    rsircode1 = tbl2.Rows[i]["rsircode"].ToString();
                    spcfcod1 = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["stockbal"] = dt5.Select("pactcode = '" + frmprjcode + "' and rsircode = '" + rsircode1 + "' and spcfcod = '" + spcfcod1 + "'")[0]["balqty"].ToString();

                    tbl1.Rows.Add(dr1);
                }

                ViewState["tblgetPass"] = this.HiddenSameData(tbl1);
                this.Data_Bind();
            }




        }



        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            this.gvAprovInfo.PageIndex = ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.Data_Bind();
        }
        protected void lbtnUpdatePurAprov_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString() == "GpaEdit")
            {
                this.saveGatePassEdit();
            }
            else
            {
                this.saveGatePass();
            }

        }

        private void saveGatePass()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string message = "";

            this.Session_tblAprov_Update();

            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            DataRow[] dr = tbl1.Select("getpqty>0");
            if (dr.Length == 0)
            {
                message = "Please Input Order Qty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;
            }

            DataRow[] dr5 = tbl1.Select("getpqty<=stockbal");
            string frmprjcode = this.Request.QueryString.AllKeys.Contains("frmpactcode") ? this.Request.QueryString["frmpactcode"].ToString() : this.ddlprjlistfrom.SelectedValue.ToString().Trim();                    
            if(comcod=="3101" || comcod=="3367")
            {               
                if (ASTUtility.Left(frmprjcode, 2)=="11" && dr5.Length == 0)
                {
                    message = "Materials are not available for Store ";
                     ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                     return;

                }

            }

            



            //log Report
            string mmGetpdat = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string getpref = this.txtGatemPassNo.Text.ToString();
            string mtrnar = this.txtgetpNarr.Text.ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDate = System.DateTime.Today.ToString("dd-MMM-yyyy");


            // duplicate check

            switch (comcod)
            {
                case "3101":
                case "3340":
                case "3338":

                case "1205":
                case "3351":
                case "3352":
                case "3348":

                    if (getpref.Length == 0)
                    {

                        message = "Gete Pass No.Should Not Be Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                        return;
                    }


                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDUPGETPASSNO", getpref, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("refno ='" + getpref + "'");

                        DataTable dt1 = dv1.ToTable();
                        if (dt1.Rows.Count == 0)
                            ;
                        else
                        {
                            message = "Found Duplicate Gate Pass No!!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                            return;
                        }
                    }
                    break;

                default:
                    break;



            }



            //For Balace Req Qty

            if (this.Request.QueryString["Type"].ToString().Trim() == "Entry")
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mtREQNO = tbl1.Rows[i]["mtreqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mspcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "BALMTREQQTY", mtREQNO, mRSIRCODE, mspcfcod, "", "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        message = "There is no balance qty in Requisition";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                        return;

                    }

                }

            }
            ////////

            if (this.ddlPrevList.Items.Count == 0)
                this.GetGetPassNo();

            string mGetpNo = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();


            string mrdate = tbl1.Rows[0]["mtrdat"].ToString();
            bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(mrdate), Convert.ToDateTime(mmGetpdat));
            if (!dcon)
            {
                message = "Approved Date is equal or greater Requisition Date";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;
            }

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPB", mGetpNo, mmGetpdat, getpref, mtrnar,
                        PostedByid, Posttrmid, PostSession, PostedDate, "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                message = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;

            }


            foreach (DataRow dr1 in tbl1.Rows)
            {

                string mtREQNO = dr1["mtreqno"].ToString();
                string mRSIRCODE = dr1["rsircode"].ToString();
                string mSPCFCOD = dr1["spcfcod"].ToString();
                double getpqty = Convert.ToDouble(dr1["getpqty"]);
                string getpamt = Convert.ToDouble(dr1["getpamt"]).ToString();
                double mtrfqty = Convert.ToDouble(dr1["mtrfqty"]);
            

                // comcod, getpno,mtreqno, rsircode, spcfcod,  qty, amt

                if (mtrfqty >= getpqty)
                {

                    if (getpqty > 0)
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPA",
                                    mGetpNo, mtREQNO, mRSIRCODE, mSPCFCOD, getpqty.ToString(), getpamt, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        message = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                        return;
                    }

                }

                else
                {
                    message = "Order Qty Less then or Equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                    return;

                }

            }
            this.txtCurAprovDate.Enabled = false;
            //((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Order Process";
                string eventdesc = "Update Process";
                string eventdesc2 = mGetpNo;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void saveGatePassEdit()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string message = "";

            this.Session_tblAprov_Update();

            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            DataRow[] dr = tbl1.Select("getpqty>0");
            if (dr.Length == 0)
            {
                message = "Please Input Order Qty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;

                //((Label)this.Master.FindControl("lblmsg")).Text = "Please Input Order Qty";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //return;
            }
            //log Report
            string mmGetpdat = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mGetpNo = this.Request.QueryString["getpasno"].ToString();
            string getpref = this.Request.QueryString["gpref"].ToString();
            string mtrnar = this.txtgetpNarr.Text.ToString();

            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string mrdate = tbl1.Rows[0]["mtrdat"].ToString();
            bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(mrdate), Convert.ToDateTime(mmGetpdat));
            if (!dcon)
            {
                message = "Approved Date is equal or greater Requisition Date";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);

                return;
            }

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPB", mGetpNo, mmGetpdat, getpref, mtrnar,
                        PostedByid, Posttrmid, PostSession, PostedDate, "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                message = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;
            }


            foreach (DataRow dr1 in tbl1.Rows)
            {

                string mtREQNO = dr1["mtreqno"].ToString();
                string mRSIRCODE = dr1["rsircode"].ToString();
                string mSPCFCOD = dr1["spcfcod"].ToString();
                double getpqty = Convert.ToDouble(dr1["getpqty"]);
                string getpamt = Convert.ToDouble(dr1["getpamt"]).ToString();
                double mtrfqty = Convert.ToDouble(dr1["mtrfqty"]);
                double balqty = Convert.ToDouble(dr1["balqty"]);

                if (balqty >= getpqty)
                {
                    if (getpqty > 0)
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPREQGPASS", "PURREQGPA",
                                    mGetpNo, mtREQNO, mRSIRCODE, mSPCFCOD, getpqty.ToString(), getpamt, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        message = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                        return;
                    }
                }

                else
                {
                    message = "Gate Pass Qty Less or Equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                    return;
                }

            }
            this.txtCurAprovDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);
            //return;
            //((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Gate Pass";
                string eventdesc = "Gate Pass Edit";
                string eventdesc2 = mGetpNo;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            this.Data_Bind();

        }


        protected void gvAprovInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAprovInfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvAprovInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //this.gvAprovInfo.EditIndex = e.NewEditIndex;
            //this.gvAprovInfo_DataBind();

            //// Supplier
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string mResCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            //string mSupCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod1")).Text.Trim();
            //string mSpcfCod = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //DropDownList ddl1 = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlSupname");
            //ddl1.DataTextField = "ssirdesc1";
            //ddl1.DataValueField = "ssircode";
            //ddl1.DataSource =ds1.Tables[0];
            //ddl1.DataBind();
            //ddl1.SelectedValue = mSupCode;

            //// Specification

            //DropDownList ddlspeci = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlspecification");
            //ddlspeci.DataTextField = "spcfdesc";
            //ddlspeci.DataValueField = "spcfcod";
            //ddlspeci.DataSource = ds1.Tables[1];
            //ddlspeci.DataBind();
            //ddlspeci.SelectedValue = mSpcfCod;





        }



        protected void gvAprovInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)ViewState["tblgetPass"];
            string mAPROVNO = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();
            string reqno = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvReqNo")).Text.Trim();
            string rescode = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEAPPRES",
                        mAPROVNO, reqno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvAprovInfo.PageSize) * (this.gvAprovInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                ViewState["tblgetPass"] = dv.ToTable();
                this.Data_Bind();
            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblgetPass"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string message = "";
            string mAPROVNO = this.lblGatePassNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblGatePassNo1.Text.Trim().Substring(3, 2) + this.txtGatePassNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEPURPROGMAM", mAPROVNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                message = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Deleted Successfully" + "');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Order Process";
                string eventdesc = "Delete Process";
                string eventdesc2 = mAPROVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void ImgbtnFinGatePass_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Project_To_Combo();
            this.StockBal();
        }
        protected void lbtnPrject_Click(object sender, EventArgs e)
        {
            this.Get_Pass_Info();
            this.GetRequisitionInfo();
            this.Panel1.Visible = true;
            this.lbtnPrject.Visible = false;

        }
        protected void GetRequisitionInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string toproj = this.ddlprjlistto.SelectedValue.ToString();
            string frmproj = this.ddlprjlistfrom.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETMTRREQUISITION", CurDate1,frmproj, toproj, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblsp"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            this.ddlResList.DataTextField = "textfield";
            this.ddlResList.DataValueField = "valuefiled";
            this.ddlResList.DataSource = ds1.Tables[2];
            this.ddlResList.DataBind();
            this.ddlResList_SelectedIndexChanged(null, null);

        }
    }
}
