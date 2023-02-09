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
using RealERPWEB.Service;

namespace RealERPWEB.F_17_Acc
{

    public partial class AccPettyCashApp : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        AutoCompleted AutoData = new AutoCompleted();
        UserService userSer = new UserService();
        public static string lblTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //((Label)this.Master.FindControl("lblTitle")).Text = "Petty cash Bill Approval Sheet";
                //this.Master.Page.Title = "Petty cash Bill Approval Sheet";

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this..Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();



                this.Get_Empname();
                this.GetAccHead();
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtBillDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.txtEntryDate.Text = Convert.ToDateTime(this.Request.QueryString["date"]).ToString("dd-MMM-yyyy");
                    this.PreviousList();
                }
            }



        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string date = this.txtEntryDate.Text;
            string petno = this.ddlPrevList.SelectedValue.ToString();
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTPETTYCASHINFO", petno);
            if (ds == null)
                return;

            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = ds.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>();
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCashEmp> lst1 = ds.Tables[1].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCashEmp>();
            LocalReport Rpt1 = new LocalReport();
            string degi = lst1[0].designation;
            string dept = lst1[0].deptname;
            string bundeln = lst1[0].pcblno;
            string empname = lst1[0].empname;
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPettyCashBillApprSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("empname", empname));
            Rpt1.SetParameters(new ReportParameter("actdesc", degi));
            Rpt1.SetParameters(new ReportParameter("sirdesc", dept));
            Rpt1.SetParameters(new ReportParameter("bundeln", bundeln));
            Rpt1.SetParameters(new ReportParameter("date", "Date : " + date));



            Rpt1.SetParameters(new ReportParameter("RptTitle", "PETTY CASH BILL APPROVAL SHEET"));
            //Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void Get_Empname()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%";//(this.Request.QueryString["Type"].ToString().Trim()=="Services")+"%";//?("%" + this.txtEmpSrc.Text.Trim()+ "%");//:("%" + this.txtEmpSrcInfo.Text.Trim()+"%");
            string date = this.txtEntryDate.Text.Trim();
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", "%", txtSProject, "", "", "", "", "", "", "");

            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

        }

        private void GetAccHead()
        {

            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>();
            lst = userSer.GetActHead("%", "", "");

            ViewState["HeadAcc1"] = lst;
        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {


            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];

            string SrchProject = "";// this.txtserceacc.Text.Trim();


            if (SrchProject.Length > 0)
            {

                IEnumerable<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> ProjectQuery;

                if (SrchProject.All(char.IsDigit))
                {
                    int len = SrchProject.Length;
                    ProjectQuery = (from project in lst
                                    where project.actcode.Substring(0, len).Equals(SrchProject)
                                    orderby project.actcode ascending
                                    select project);

                }

                else
                {

                    ProjectQuery = (from project in lst
                                    where project.actdesc.ToUpper().Contains(SrchProject.ToUpper())
                                    orderby project.actcode ascending
                                    select project);
                }


                this.ddlacccode.DataTextField = "actdesc";
                this.ddlacccode.DataValueField = "actcode";
                this.ddlacccode.DataSource = ProjectQuery;
                this.ddlacccode.DataBind();

            }


            else
            {
                this.ddlacccode.DataTextField = "actdesc";
                this.ddlacccode.DataValueField = "actcode";
                this.ddlacccode.DataSource = lst;
                this.ddlacccode.DataBind();
            }




            //----Show Resource code and Specification Code------------// 

            // DataTable dt01 = (DataTable)Session["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));

            if (lst1.Count == 0)
                return;


            if (lst1[0].actelev.ToString() == "2")
            {


                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;


                string actcode = this.ddlacccode.SelectedValue.ToString().Substring(0, 2);
                // if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18" || this.ddlresuorcecode.Items.Count==0)
                this.GetResCode();
            }
            else
            {


                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;

                this.ddlresuorcecode.Items.Clear();


            }
            //---------------------------------------------//
            // this.txtserceacc.Text = "";
        }
        protected void lnkRescode_Click(object sender, EventArgs e)
        {

            this.GetResCode();


        }


        private void GetResCode()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = this.ddlacccode.SelectedValue.ToString();
                string filter1 = "%";

                string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();


                string SearchInfo = "";
                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lstacc = (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];

                string search1 = this.ddlacccode.SelectedValue.ToString().Trim();

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lstacc1 = lstacc.FindAll((p => p.actcode == search1));

                //if (lst1.Count == 0)
                //    return;

                //DataRow[] drac = dt01.Select("actcode='" + search1 + "'");
                string type = lstacc1[0].acttype.ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {


                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);
                ViewState["HeadRsc1"] = lst;

                this.ddlresuorcecode.DataSource = lst;
                this.ddlresuorcecode.DataTextField = "resdesc1";
                this.ddlresuorcecode.DataValueField = "rescode";
                this.ddlresuorcecode.DataBind();
                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    this.ddlresuorcecode.SelectedValue = oldRescode;


                }





                string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst2 = lst.FindAll((p => p.rescode == seaRes));
                if (lst2.Count == 0)
                    return;


            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }





        }
        protected void ddlacccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlacccode.BackColor = System.Drawing.Color.Pink;

            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>)ViewState["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst1 = lst.FindAll((p => p.actcode == search1));

            if (lst1[0].actelev.ToString() == "2")
            {


                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;



                string actcode = this.ddlacccode.SelectedValue.Substring(0, 2);
                this.GetResCode();
            }
            else
            {


                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlresuorcecode.Items.Clear();


            }
            // GetBalanceInfo();
        }

        private void PreviousList()
        {

            string CurDate1 = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_PETYCASH_PREVIOUSLIST", CurDate1);
            if (ds == null)
                return;

            this.ddlPrevList.DataTextField = "pcblno1";
            this.ddlPrevList.DataValueField = "pcblno";
            this.ddlPrevList.DataSource = ds.Tables[0];
            this.ddlPrevList.DataBind();
            if (this.Request.QueryString["genno"].ToString().Length > 0)
            {
                this.ddlPrevList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lnkOk_Click(null, null);
            }

        }
        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (lnkOk.Text.Trim() == "Ok")
            {
                lnkOk.Text = "New";
                this.AccPanel.Visible = true;
                // this.ddlEmpName.Enabled = false;
                this.imgPreVious.Visible = false;
                this.ddlPrevList.Visible = false;
                this.txtEntryDate.Enabled = false;
                this.Get_Info();

            }
            else
            {
                lnkOk.Text = "Ok";
                this.AccPanel.Visible = false;
                this.ddlEmpName.Enabled = true;
                this.imgPreVious.Visible = true;
                this.ddlPrevList.Visible = true;
                this.txtEntryDate.Enabled = true;
                this.ddlPrevList.Items.Clear();
                this.lblCurNo1.Text = "PCB00-";
                this.txtCurNo2.Text = "00000";
                this.gvpetty.DataSource = null;
                this.gvpetty.DataBind();
            }



        }
        protected void GetLastPettyBill()
        {


            string CurDate1 = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mCPRNo = "NEWBILL";
            if (this.ddlPrevList.Items.Count > 0)
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();
            if (mCPRNo == "NEWBILL")
            {
                //string SrchProject = "%" + this.txtSrchProjectName.Text.Trim() + "%";GET_LAST_PETTYBILL
                string comcod = this.GetCompCode();
                //DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_LAST_PEnaTTYBILL", CurDate1);
                DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_LAST_PETTYBILL", CurDate1);

                if (ds == null)
                    return;
                this.lblCurNo1.Text = ds.Tables[0].Rows[0]["maxpcblno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds.Tables[0].Rows[0]["maxpcblno1"].ToString().Substring(6, 5);
                this.ddlPrevList.DataTextField = "maxpcblno1";
                this.ddlPrevList.DataValueField = "maxpcblno1";
                this.ddlPrevList.DataSource = ds.Tables[0];
                this.ddlPrevList.DataBind();




            }
        }
        private void Get_Info()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mCPRNo = "NEWBILL";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtEntryDate.Enabled = false;
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();

            }
            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_PETTY_CASH_BILLINFO", mCPRNo);
            if (ds == null)
                return;

            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> pettycash = ds.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>();

            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCashEmp> Pettycash1 = ds.Tables[1].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCashEmp>();

            ViewState["Pettycash"] = pettycash;
            ViewState["Pettycash1"] = Pettycash1;



            if (mCPRNo == "NEWBILL")
            {

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GET_LAST_PETTYBILL", CurDate1);
                if (ds1 == null)
                    return;
                this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxpcblno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxpcblno1"].ToString().Substring(6, 5);
                this.ddlPrevList.DataTextField = "maxpcblno1";
                this.ddlPrevList.DataValueField = "maxpcblno1";
                this.ddlPrevList.DataSource = ds.Tables[0];
                this.ddlPrevList.DataBind();
                return;
            }



            this.lblCurNo1.Text = ds.Tables[0].Rows[0]["pcblno1"].ToString().Substring(0, 6);
            this.txtCurNo2.Text = ds.Tables[0].Rows[0]["pcblno1"].ToString().Substring(6, 5);
            this.ddlEmpName.SelectedValue = ds.Tables[0].Rows[0]["empid"].ToString();
            this.txtEntryDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["pcbldate"]).ToString("dd-MMM-yyyy");
            this.Data_Bind();


        }
        protected void LbtnAdd_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            string actcode = this.ddlacccode.SelectedValue.ToString();
            string actdesc = this.ddlacccode.SelectedItem.ToString();
            string sircode = (this.ddlresuorcecode.Items.Count == 0) ? "000000000000" : this.ddlresuorcecode.SelectedValue.ToString();
            string sirdesc = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedItem.ToString().Substring(13);
            string billno = this.txtBillno.Text.Trim().ToString();
            DateTime billdate = Convert.ToDateTime(this.TxtBillDate.Text.Trim());
            double amount = ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + this.txtamount.Text.Trim()));
            lst.Add(new RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash(actcode, actdesc, sircode, sirdesc, billno, billdate, "", amount, "", true, ""));
            ViewState["Pettycash"] = lst;
            this.Data_Bind();
        }
        private void Save_Value() //nayan
        {
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            for (int i = 0; i < this.gvpetty.Rows.Count; i++)
            {

                double amount = ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + ((TextBox)this.gvpetty.Rows[i].FindControl("txtamount")).Text.Trim()));
                string billno = ((TextBox)this.gvpetty.Rows[i].FindControl("TxtBillno")).Text.Trim();
                string particulars = ((TextBox)this.gvpetty.Rows[i].FindControl("txtParticulars")).Text.Trim();
                string billdate = Convert.ToDateTime(((TextBox)this.gvpetty.Rows[i].FindControl("txtbilldate")).Text.Trim()).ToString("dd-MMM-yyyy"); ;
                string remarks = ((TextBox)this.gvpetty.Rows[i].FindControl("txtRemarks")).Text.Trim();

                lst[i].amount = amount;
                lst[i].billno = billno;
                lst[i].partculrs = particulars;
                lst[i].billdate = Convert.ToDateTime(billdate);
                lst[i].remarks = remarks;


            }
            ViewState["Pettycash"] = lst;
        }
        private void Data_Bind()
        {
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            if (lst.Count == 0)
            {
                this.gvpetty.DataSource = null;
                this.gvpetty.DataBind();
                return;
            }

            this.gvpetty.DataSource = lst;
            this.gvpetty.DataBind();
            this.FooterCalCulation();

        }

        private void FooterCalCulation()
        {
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            double toamt = lst.Sum(l => l.amount);
            ((Label)this.gvpetty.FooterRow.FindControl("lgvFAmount")).Text = toamt.ToString("#,##0.00;(#,##0.00); ");

        }
        protected void gvpetty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];
            int rowindex = (this.gvpetty.PageSize) * (this.gvpetty.PageIndex) + e.RowIndex;
            lst.RemoveAt(rowindex);
            ViewState["Pettycash"] = lst;
            this.Data_Bind();
        }

        protected void lbtnCalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            this.Save_Value();
            List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash> lst = (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)ViewState["Pettycash"];

            if (this.ddlPrevList.Items.Count == 0)
                this.GetLastPettyBill();
            string pcbillno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtEntryDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string billdate = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string empid = this.ddlEmpName.SelectedValue.ToString();

            DataTable dt = ASITUtility03.ListToDataTable(lst);

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERT_UPDATE_PETTYCASH", ds1, null, null, pcbillno, billdate, empid, usrid, sessionid, trmid, PostedDat, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
    }
}


