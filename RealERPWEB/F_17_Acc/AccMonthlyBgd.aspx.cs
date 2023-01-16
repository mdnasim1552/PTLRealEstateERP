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
    public partial class AccMonthlyBgd : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Working Budget";
                //this.Master.Page.Title = "Working Budget";

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetYearMonth();
                this.GetDepartment();
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtbgddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                //this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                CommonButton();

            }
        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(LnkfTotal_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetYearMonth()
        {
            //string comcod = this.GetCompCode();
            //string yr = this.Request.QueryString["year"];
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlyearmon.DataTextField = "yearmon";
            //this.ddlyearmon.DataValueField = "ymon";
            //this.ddlyearmon.DataSource = ds1.Tables[0];
            //this.ddlyearmon.DataBind();
            //string month = System.DateTime.Today.ToString("MM");
            //this.ddlyearmon.SelectedValue = yr + month; 
            //ds1.Dispose();
            //System.DateTime.Today.ToString ("yyyyMM");

            string comcod = this.GetCompCode();
            //string yr = this.Request.QueryString["year"];
            string yr = System.DateTime.Today.ToString("yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            string month = System.DateTime.Today.ToString("MM");
            this.ddlyearmon.SelectedValue = yr + month;

            this.ddltomonth.DataTextField = "yearmon";
            this.ddltomonth.DataValueField = "ymon";
            this.ddltomonth.DataSource = ds1.Tables[0];
            this.ddltomonth.DataBind();
            string month1 = System.DateTime.Today.AddMonths(1).ToString("MM");
            this.ddltomonth.SelectedValue = yr + month1;
            ds1.Dispose();

        }

        private void GetDepartment()
        {
            string comcod = this.GetCompCode();
            //string yr = this.Request.QueryString["year"];
            string yr = System.DateTime.Today.ToString("yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETDEPARTMENTINF", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlDepartment.DataTextField = "deptdesc";
            this.ddlDepartment.DataValueField = "deptcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            ds1.Dispose();

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //
            //

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.lbtnOk.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                // this.GetLastBgdInfo();
                this.GetBudgetInfo();
                //this.lbtnPrevBudget.Visible = false;
                this.ddlyearmon.Enabled = false;
                return;
            }
         ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            this.lbtnOk.Text = "Ok";

            this.txtCurDate.Enabled = true;
            //  this.ddlPrevBgdList.Items.Clear();
            this.MultiView1.ActiveViewIndex = -1;
            // this.lbtnPrevBudget.Visible = true;
            this.ddlyearmon.Enabled = true;


        }
        private void GetBudgetInfo()
        {
            Session.Remove("AccTbl01");
            string comcod = this.GetCompCode();

            // this.lblCurBgdNo1.Text =(this.ddlPrevBgdList.Items.Count > 0) ?this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(0, 6):this.lblCurBgdNo1.Text ;
            //this.txtCurBgdNo2.Text = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(6, 5) : this.txtCurBgdNo2.Text;
            // string mBGDNo = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedValue.ToString() : this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtCurDate.Text.Trim(), 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim(); ;

            // this.txtCurDate.Enabled=(this.ddlPrevBgdList.Items.Count > 0) ?false:true;

            string yearMon = this.ddlyearmon.SelectedValue.ToString();
            string deptcode = this.ddlDepartment.SelectedValue.ToString();


            //string filter = (this.Request.QueryString["actcode"].ToString () == "55010001") ? "55010001%" :
            //    (this.Request.QueryString["actcode"].ToString () == "55010010") ? "55010010%" :
            //    (this.Request.QueryString["actcode"].ToString () == "6101") ? "6101%" :
            //    (this.Request.QueryString["actcode"].ToString () == "6102") ? "6102%" :
            //    (this.Request.QueryString["actcode"].ToString () == "9101") ? "9101%" :
            //    (this.Request.QueryString["actcode"].ToString () == "9101") ? "9101%" :
            //    (this.Request.QueryString["actcode"].ToString () == "9102") ? "9102%" :
            //    (this.Request.QueryString["actcode"].ToString () == "9103") ? "9103%" :
            //    (this.Request.QueryString["actcode"].ToString () == "7101") ? "7101%" :
            //    (this.Request.QueryString["actcode"].ToString () == "8101") ? "8101%" :
            //    (this.Request.QueryString["actcode"].ToString () == "1") ? "1[1-9]%" :
            //    (this.Request.QueryString["actcode"].ToString () == "1") ? "1[1-9]%" : 
            //    (this.Request.QueryString["actcode"].ToString () == "2") ? "2[1-9]%" : this.txtFilter.Text.Trim () + "%";


            string filter = ((this.Request.QueryString["actcode"].ToString().Trim().Length > 0 ? this.Request.QueryString["actcode"].ToString() : this.txtFilter.Text.Trim())) + "%";




            //string filter = (this.Request.QueryString["rType"].ToString().Length == 0) ? this.txtFilter.Text.Trim() + "%" : this.Request.QueryString["rType"].ToString() + "%";
            // string filter = this.txtFilter.Text+"%";
            // string filter = "%"+this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBUDGETINFO", yearMon, filter,
                         deptcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.txtCurDate.Text = (ds1.Tables[1].Rows.Count == 0) ? this.txtCurDate.Text.Trim() : Convert.ToDateTime(ds1.Tables[1].Rows[0]["bgddat"]).ToString("dd-MMM-yyyy");

            Session["AccTbl01"] = ds1.Tables[0];
            this.dgv2_DataBind();
            this.TotalCalculation1();





        }

        //private void GetLastBgdInfo() 
        //{
        //    string comcod = this.GetCompCode();
        //    string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //     DataSet  ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETLASTBGDNO", CurDate1, "", "", "", "", "", "", "", "");

        //        if (ds1.Tables[0].Rows.Count > 0)
        //        {

        //            this.lblCurBgdNo1.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(0, 6);
        //            this.txtCurBgdNo2.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(6, 5);
        //        }



        //}

        //private void GetBudgetNo() 
        //{
        //    string comcod = this.GetCompCode();
        //    string mBGDNO = "NEWBGD";
        //    if (this.ddlPrevBgdList.Items.Count > 0)
        //        mBGDNO = this.ddlPrevBgdList.SelectedValue.ToString();
        //    string mBGDDAT =this.txtCurDate.Text.Trim();
        //    if (mBGDNO == "NEWBGD")
        //    {
        //        DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETLASTBGDNO", mBGDDAT,
        //               "", "", "", "", "", "", "", "");
        //        if (ds2 == null)
        //            return;
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {

        //            this.lblCurBgdNo1.Text = ds2.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(0, 6);
        //            this.txtCurBgdNo2.Text = ds2.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(6, 5);

        //            this.ddlPrevBgdList.DataTextField = "maxbgdno1";
        //            this.ddlPrevBgdList.DataValueField = "maxbgdno";
        //            this.ddlPrevBgdList.DataSource = ds2.Tables[0];
        //            this.ddlPrevBgdList.DataBind();
        //        }
        //    }   

        //}

        //protected void lbtnPrevBudget_Click(object sender, EventArgs e)
        //{
        //    this.GetPreBgdNum();
        //}
        //private void GetPreBgdNum() 
        //{
        //    string comcod = this.GetCompCode();
        //    string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPREBGDNUM", CurDate1, "", "", "", "", "", "", "", "");
        //    this.ddlPrevBgdList.DataTextField = "bgdnum1";
        //    this.ddlPrevBgdList.DataValueField = "bgdnum";
        //    this.ddlPrevBgdList.DataSource=ds1.Tables[0];
        //    this.ddlPrevBgdList.DataBind();
        //    ds1.Dispose();



        //}
        //protected void lnkSubmit_Click(object sender, EventArgs e)
        //{

        //    this.GetBudgetInfo();
        //    //this.lblmsg01.Text = "";
        //    this.Panel2.Visible = false;
        //    this.dgv2.Visible = true;
        //    this.lblacccode1.Visible = true;
        //    this.txtFilter.Visible = true;
        //   // this.ImageButton1.Visible = true;

        //}
        protected void ibtnAccCode_Click(object sender, EventArgs e)
        {
            this.GetBudgetInfo();
        }





        private void SessionUpdate()
        {

            DataTable tblt01 = (DataTable)Session["AccTbl01"];
            int TblRowIndex;

            for (int i = 0; i < this.dgv2.Rows.Count; i++)
            {
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                //string dgvTrnRemarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                TblRowIndex = (dgv2.PageIndex) * dgv2.PageSize + i;

                tblt01.Rows[TblRowIndex]["Dr"] = dgvTrnDrAmt;
                tblt01.Rows[TblRowIndex]["Cr"] = dgvTrnCrAmt;
                //  tblt01.Rows[TblRowIndex]["Remarks"] = dgvTrnRemarks;
            }
            Session["AccTbl01"] = tblt01;
        }
        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                double dgvTrnRate;
                double dgvTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                // double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtRate")).Text.Trim()));
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                if (dgvTrnDrAmt == 0 && dgvTrnCrAmt == 0)
                {
                    dgvTrnRate = 0;
                }
                else
                {
                    dgvTrnRate = (dgvTrnQty == 0 ? 0.00 : (dgvTrnDrAmt + dgvTrnCrAmt) / dgvTrnQty);
                }
                ((Label)this.dgv3.Rows[j].FindControl("gvlblRate")).Text = dgvTrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["qty"] = dgvTrnQty;
                tblt02.Rows[TblRowIndex2]["rate"] = dgvTrnRate;
                tblt02.Rows[TblRowIndex2]["Dr"] = dgvTrnDrAmt;
                tblt02.Rows[TblRowIndex2]["Cr"] = dgvTrnCrAmt;

            }
            Session["AccTbl02"] = tblt02;

            this.dgv3_DataBind();
        }
        protected void dgv2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='normalrow'");

                //e.Row.Attributes.Add("onmouseover", "this.className='highlightrow'");
                //e.Row.Attributes.Add("onmouseout", "this.className='normalrow'");
            }

        }
        protected void gvlnkLevel_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 1;
            Session.Remove("AccTbl02");
            this.SessionUpdate();
        }
        private void ShowResource()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acccode01 = this.txtActcode.Text.Trim().Substring(0, 12);
            //string filter2 = this.txtResSearch.Text.Trim() + "%";
            DataTable tblt01 = (DataTable)Session["AccTbl01"];
            DataView dv = tblt01.DefaultView;
            dv.RowFilter = "actcode like '" + acccode01 + "%'";
            string type = dv.ToTable().Rows[0]["acttype"].ToString();


            //string type = lstacc1[0].acttype.ToString().Trim();
            string SearchInfo = "";
            if (type.Length > 0)
            {

                int len;
                string[] ar = type.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(rescode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(rescode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }


            string voudat = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            //string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
            string YearMon = this.ddlyearmon.SelectedValue.ToString();
            string deptcode = this.ddlDepartment.SelectedValue.ToString();
            string ressearch = "%" + this.txtResSearch.Text + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBGDRES", SearchInfo, acccode01, YearMon, deptcode, ressearch, "", "", "", "");
            Session["AccTbl02"] = ds2.Tables[0];
            this.dgv3_DataBind();
        }

        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        private void TotalCalculation2()
        {
            this.SessionUpdate2();

        }
        protected void ibtnDetailsCode_Click(object sender, EventArgs e)
        {
            this.ShowResource();
        }


        protected void dgv2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session.Remove("RowIndex");
            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                if (row.RowType.ToString() == "DataRow")
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    int RowIndex = gvr.RowIndex;
                    Session["RowIndex"] = RowIndex;
                    this.ShowActCode();
                    gvr.BackColor = System.Drawing.Color.Blue;

                }
            }
        }
        private void ShowActCode()
        {
            int rowin = (int)Session["RowIndex"];
            int rowin1 = (dgv2.PageIndex * dgv2.PageSize) + rowin;
            this.txtActcode.Text = ((Label)this.dgv2.Rows[rowin].FindControl("lblAccdesc")).Text;
            this.ShowResource();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {

            this.MultiView1.ActiveViewIndex = 0;
            this.GetBudgetInfo();

        }
        private void UpdateTable02()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SessionUpdate2();
            string comcod = this.GetCompCode();
            //if (this.ddlPrevBgdList.Items.Count == 0)
            //    this.GetBudgetNo();
            string voudat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
            string actcode = this.txtActcode.Text.Trim().Substring(0, 12);
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            string deptcode = this.ddlDepartment.SelectedValue.ToString();

            for (int i = 0; i < tblt03.Rows.Count; i++)
            {
                string rescode = tblt03.Rows[i]["rescode"].ToString();
                string bgdqty = tblt03.Rows[i]["qty"].ToString();
                string Dramt = Convert.ToDouble(tblt03.Rows[i]["Dr"]).ToString();
                string Cramt = Convert.ToDouble(tblt03.Rows[i]["Cr"]).ToString();
                // string bgdamt = Convert.ToString(Dramt - Cramt);


                //if ((Dramt - Cramt) != 0)
                //{
                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTORUPBGDINF", "00000000000000", actcode,
                        rescode, voudat, bgdqty, Cramt.ToString(), Dramt.ToString(), yearmon, deptcode, "", "", "", "", "", "");
                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //}     
            }
            this.txtCurDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Monthly Budget";
                string eventdesc = "Update Details Budget";
                string eventdesc2 = yearmon;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ////---------------Check Dr. and Cr------//
            //this.TotalCalculation1();
            //DataTable tblt07 = (DataTable)Session["AccTbl01"];
            //for (int i = 0; i < tblt07.Rows.Count; i++)
            //{
            //    double Dramt01 = Convert.ToDouble(tblt07.Rows[i]["Dr"]);
            //    double Cramt01 = Convert.ToDouble(tblt07.Rows[i]["Cr"]);
            //    if (Dramt01 > 0 && Cramt01 > 0)
            //    {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Choose Only Dr. Or Cr. Amount.";
            //        return;
            //    }
            //    //else
            //    //{


            //    //}
            //}
            //-------------------------//
            //this.TotalCalculation1();
            this.UpdateTable01();


        }
        private void UpdateTable01()
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetCompCode();

                //if (this.ddlPrevBgdList.Items.Count == 0)
                //    this.GetBudgetNo();

                string voudat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                // string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(voudat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();
                string rescode = "000000000000";
                string bgdqty = "0";
                string yearmon = this.ddlyearmon.SelectedValue.ToString();
                string deptcode = this.ddlDepartment.SelectedValue.ToString();


                DataTable tblt05 = (DataTable)Session["AccTbl01"];
                for (int i = 0; i < tblt05.Rows.Count; i++)
                {
                    string actcode = tblt05.Rows[i]["actcode"].ToString();
                    string actlev = tblt05.Rows[i]["actelev"].ToString();
                    string Dramt = Convert.ToDouble(tblt05.Rows[i]["Dr"]).ToString();
                    string Cramt = Convert.ToDouble(tblt05.Rows[i]["Cr"]).ToString();
                    // string bgdamt = Convert.ToString(Dramt - Cramt);
                    //if ((Dramt - Cramt) != 0 && actlev != "2")
                    if (actlev != "2")
                    {

                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INSERTORUPBGDINF", "00000000000000", actcode, rescode, voudat, bgdqty,
                                 Cramt, Dramt, yearmon, deptcode, "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }
                this.txtCurDate.Enabled = false;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Monthly Budget";
                    string eventdesc = "Update Budget";
                    string eventdesc2 = yearmon;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception e)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + e.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }
        protected void LnkfTotal_Click(object sender, EventArgs e)
        {
            this.TotalCalculation1();
        }
        private void TotalCalculation1()
        {
            this.SessionUpdate();
            DataTable tblt06 = (DataTable)Session["AccTbl01"];
            if (tblt06.Rows.Count == 0)
                return;


            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Dr)", "")) ?
            0.00 : tblt06.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Cr)", "")) ?
            0.00 : tblt06.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        }




        protected void dgv2ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2.PageIndex = ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex;
            this.dgv2_DataBind();
            this.TotalCalculation1();


        }
        protected void dgv2_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["AccTbl01"];
            this.dgv2.DataSource = tbl1;
            this.dgv2.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.dgv2.PageSize);
            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = true;
            ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex = this.dgv2.PageIndex;

            Session["Report1"] = dgv2;
            ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }



        protected void dgv3_DataBind()
        {
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            this.dgv3.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgv3.DataSource = tblt03;
            this.dgv3.DataBind();
            if (tblt03.Rows.Count == 0)
                return;
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftDramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Dr)", "")) ?
            0.00 : tblt03.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Cr)", "")) ?
            0.00 : tblt03.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3.PageIndex = e.NewPageIndex;
            this.dgv3_DataBind();
        }

        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mBGDDAT = Convert.ToDateTime(this.txtbgddate.Text).ToString("dd-MMM-yyyy");
            string tomonth = this.ddltomonth.SelectedValue.ToString();
            string frmonth = this.ddlyearmon.SelectedValue.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "COPY_MONTHLY_BUDGET", mBGDDAT, tomonth, frmonth, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.ddlyearmon.SelectedValue = tomonth;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Copy Success";
                this.GetBudgetInfo();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Copy Invalid";


            }
        }



        protected void CpyCHeck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CpyCHeck.Checked)
            {
                this.CopyTo.Visible = true;
                this.Copybtn.Visible = true;
                this.datediv.Visible = true;
            }
            else
            {
                this.CopyTo.Visible = false;
                this.Copybtn.Visible = false;
                this.datediv.Visible = false;
            }
        }

        protected void lnkbtnUpdateRes_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);




            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.UpdateTable02();

        }


    }
}