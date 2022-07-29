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
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccSalJournal : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "Details" ? "SALES JOURNAL Details" : (type=="Complaint" ?"Complaint Journal Details": "SALES JOURNA");
                this.Master.Page.Title = "SALES JOURNAL INFORMATION";
                this.CreateTable();


                this.txtdate.Text = ((this.Request.QueryString["Date1"].ToString()).Length == 0 || type== "Complaint") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.Request.QueryString["Date1"].ToString();



                string prjcode = this.Request.QueryString["prjcode"].ToString();
                string usircode = this.Request.QueryString["usircode"].ToString();
                this.lnkDetail.NavigateUrl = "~/F_22_Sal/LinkMktSalsPayment.aspx?Type=&prjcode=" + prjcode + "&usircode=" + usircode;

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            Session["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            string Billno = this.txtSrchProject.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILlACC", Billno, "", "", "", "", "", "", "", "");
            this.ddlProject.Items.Clear();
            this.ddlProject.DataTextField = "textfield";
            this.ddlProject.DataValueField = "billno";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string Project = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtSrchProject.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            //  string Project = this.txtSrchProject.Text.Trim() + "%";
            string Type = this.Request.QueryString["Type"].ToString()== "Complaint"? "Complaint":"";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPROJECTNAME", Project, Type, "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
            ViewState["tblSchedule"] = ds1.Tables[1];            
            this.GetUnitName();

        }
        private void GetUnitName()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string Unit = (this.Request.QueryString["usircode"].ToString()).Length == 0 ? "%" + this.txtSrchUnit.Text.Trim() + "%" : this.Request.QueryString["usircode"].ToString() + "%";
            // string Unit = this.txtSrchUnit.Text.Trim() + "%";
            string schcode = (this.Request.QueryString["schcode"].ToString()).Length == 0 ? "" : this.Request.QueryString["schcode"].ToString();


            DataTable dt = (DataTable)ViewState["tblSchedule"];
            string schdesc = dt.Rows.Count == 0 ? "" : dt.Rows[0]["schdesc"].ToString();
            string callType = "";
            if (schdesc.Trim().Length == 0)
            {
                callType = "GETUNITNAME";
            }
            else
            {
                callType = "GETUNITNAMESCH";
            }

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", callType, pactcode, Unit, schcode, "", "", "", "", "", "");
            if (ds2 == null)
                return;        

            this.ddlUnitName.DataTextField = "usirdesc";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds2.Tables[0];
            this.ddlUnitName.DataBind();
            ds2.Dispose();

        }

        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDrAmt")).Text = (accData.ToDramt).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCrAmt")).Text = (accData.ToCramt).ToString("#,##0;(#,##0); - ");



        }

        protected void ibtnvounu_Click(object sender, EventArgs e)
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetCompCode();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {


            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                if ((this.Request.QueryString["Date1"].ToString()).Length >= 0)
                    this.GetProjectName();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");
            this.CreateTable();

            this.ddlProject.Items.Clear();
            this.ddlUnitName.Items.Clear();

            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.lnkFinalUpdate.Enabled = true;
            //this.txtcurrentvou.Enabled = false;
            //this.txtCurrntlast6.Enabled = false;
        }



        protected void imgSearchProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgSearchUnit_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }


        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            if (accData.ToDramt != accData.ToCramt)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();

            string voudat = this.txtdate.Text.Substring(0, 11);
            this.ibtnvounu_Click(null, null);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";

            string schcode = "";
            if (this.Request.QueryString["Type"].ToString() == "Details")
            {
                schcode = this.Request.QueryString.AllKeys.Contains("schcode") ? this.Request.QueryString["schcode"].ToString() : "";
            }


            //Existing   Purchase No  

            for (int i = 0; i < dgv2.Rows.Count; i++)
            {

                string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                DataSet ds4;
                if (ASTUtility.Left(actcode, 2) == "18")
                {

                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGSVOUCHER", actcode, rescode, schcode, "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0) continue;
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in Sale Journal";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }



            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
               
                
                
                
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((Label)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = "";
                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    if (ASTUtility.Left(actcode, 2) == "18")
                    {                   
                        resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INORUPSALJOURNAL", actcode, rescode, vounum, schcode, "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Journal";
                    string eventdesc = "Update Journal";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                //this.txtcurrentvou.Enabled = false;
                //this.txtCurrntlast6.Enabled = false;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private string chkExitingVoucher()
        {
            string type = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3352":    // p2p 360
                    type = "EXISTINGSVOUCHERSCH";
                    break;
                default:
                    type = "EXISTINGSVOUCHER";
                    break;
            }
            return type;

        }
        private string getVoucherCallType()
        {
            string type = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3352":    // p2p 360

                    type = "INORUPSALJOURNALSCH";
                    break;
                default:
                    type = "INORUPSALJOURNAL";
                    break;
            }
            return type;

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
                //if (dt.Rows.Count == 0)
                //    return;
                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Naration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Sales Journal";
                //    string eventdesc = "Print Journal";
                //    string eventdesc2 = "Voucher No.: " + vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        private string ComSalesJournal()
        {

            string comcod = this.GetCompCode();
            string Calltype = "";

            switch (comcod)
            {

                case "3330":
                    Calltype = "GETACCDETBRISGESALESJOURNAL";
                    break;

                default:
                    Calltype = "GETACCDETSALESJOURNAL";
                    break;




            }

            return Calltype;

        }
        protected void lbtnSelec_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
           
            DataSet ds1 = new DataSet();


            if(this.Request.QueryString["Type"].ToString() == "Complaint")
            {
                string dgno = this.Request.QueryString["DgNo"].ToString();
                ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETRECEIVABLEINFO", dgno, "", "", "", "", "", "", "", "");
            }
            else
            {
                string Pactcode = this.ddlProject.SelectedValue.ToString();
                string UnitCode = this.ddlUnitName.SelectedValue.ToString();
                string Type = this.Request.QueryString["Type"].ToString().Trim();

                string CallType = (Type == "Consolidate") ? "GETACCSALESJOURNAL" : this.ComSalesJournal();
                string schcode = "";
                if (this.Request.QueryString["Type"].ToString() == "Details")
                {
                    schcode = this.Request.QueryString.AllKeys.Contains("schcode") ? this.Request.QueryString["schcode"].ToString() : "";
                }
                ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, Pactcode, UnitCode, schcode, "", "", "", "", "", "");
            }
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)Session["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);


                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {

                    if (ASTUtility.Left(dgAccCode, 2) == "18")
                        return;

                    else
                        dr2[0]["trncram"] = Convert.ToDouble(dr2[0]["trncram"]) + dgTrnCrAmt;



                }


                else
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["subcode"] = dgResCode;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["subdesc"] = dgResDesc;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["trncram"] = dgTrnCrAmt;
                    tblt01.Rows.Add(dr1);
                }
            }
            //if (tblt01.Rows.Count == 0)
            //    return;
            DataView dv = tblt01.DefaultView;
            dv.Sort = "actcode";

            Session["tblt01"] = HiddenSameData(dv.ToTable());
            dgv2.DataSource = tblt01;
            dgv2.DataBind();
            calculation();
            this.ibtnvounu.Visible = true;
            this.txtCurrntlast6.ReadOnly = false;
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }

            }

            return dt1;
        }




        protected void imgSearchBillno_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadBillCombo();
        }


        //protected void lnkDetail_Click(object sender, EventArgs e)
        //{

        //    //((HyperLink)this.gvReqStatus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        //    //LinkButton hlink1 = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

        //    HyperLink hlink1 = (HyperLink)FindControl("lnkDetail");

        //    string prjcode = this.Request.QueryString["prjcode"].ToString();
        //    string usircode = this.Request.QueryString["usircode"].ToString();


        //   // HyperLink hlink1 = (LinkButton).sedter("lnkDetail");
        //       // string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
        //        //if (code == "")
        //        //{
        //        //    return;
        //        //}

        //    hlink1.NavigateUrl = "LinkMktSalsPayment.aspx?Type=&prjcode=" + prjcode + "&usircode=" + usircode;

        //}
    }
}