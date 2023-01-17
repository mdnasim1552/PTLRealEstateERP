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
namespace RealERPWEB.F_23_CR
{
    public partial class CustCollectBudget : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "COLLECTION BUDGET INFORMATION VIEW/EDIT";

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnPrevBudget_Click(object sender, EventArgs e)
        {
            this.GetPreBgdNum();
        }

        private void GetPreBgdNum()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREBGDNUM", CurDate1, "", "", "", "", "", "", "", "");
            this.ddlPrevBgdList.DataTextField = "bgdnum1";
            this.ddlPrevBgdList.DataValueField = "bgdnum";
            this.ddlPrevBgdList.DataSource = ds1.Tables[0];
            this.ddlPrevBgdList.DataBind();
            ds1.Dispose();



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.GetLastBgdInfo();
                this.GetBudgetInfo();
                this.lbtnPrevBudget.Visible = false;
                this.ddlPrevBgdList.Visible = false;
                return;
            }
            this.lbtnOk.Text = "Ok";

            this.ddlPrevBgdList.Items.Clear();
            this.gvCollectBud.DataSource = null;
            this.gvCollectBud.DataBind();
            this.lbtnPrevBudget.Visible = true;
            this.ddlPrevBgdList.Visible = true;
        }

        private string GetLastBgdInfo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETLASTBGDNO", CurDate1, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {

                this.lblCurBgdNo1.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(0, 6);
                this.txtCurBgdNo2.Text = ds1.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(6, 5);
            }

            return ds1.Tables[0].Rows[0]["maxbgdno"].ToString();

        }


        private void GetBudgetInfo()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetCompCode();
            this.txtCurDate.Text = (this.ddlPrevBgdList.Items.Count > 0) ? Convert.ToDateTime(this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(12)).ToString("dd-MMM-yyyy") : System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.lblCurBgdNo1.Text = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(0, 6) : this.lblCurBgdNo1.Text;
            this.txtCurBgdNo2.Text = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedItem.Text.Trim().Substring(6, 5) : this.txtCurBgdNo2.Text;
            string mBGDNo = (this.ddlPrevBgdList.Items.Count > 0) ? this.ddlPrevBgdList.SelectedValue.ToString() : this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtCurDate.Text.Trim(), 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim(); ;
            string date = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETBUDGETINFO", date, mBGDNo, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblbgd"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {

                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }
            }

            return dt1;
        }

        private void Data_Bind()
        {


            this.gvCollectBud.DataSource = (DataTable)Session["tblbgd"];
            this.gvCollectBud.DataBind();
            this.FooterAmt();
        }

        private void FooterAmt()

        {

            DataTable dt = (DataTable)Session["tblbgd"];
            ((Label)this.gvCollectBud.FooterRow.FindControl("lgvFDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueamt)", "")) ?
                               0 : dt.Compute("sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollectBud.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?
                            0 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");

        }



        protected void gvCollectBud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvCollectBud.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblbgd"];
            int TblRowIndex;
            for (int i = 0; i < this.gvCollectBud.Rows.Count; i++)
            {
                double bgdamt = Convert.ToDouble("0" + ((TextBox)this.gvCollectBud.Rows[i].FindControl("txtgvbgdamt")).Text.Trim());
                TblRowIndex = (gvCollectBud.PageIndex) * gvCollectBud.PageSize + i;
                dt.Rows[TblRowIndex]["bgdamt"] = bgdamt;

            }
            Session["tblbgd"] = dt;
        }


        protected void lFinalUpdateCost_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.SaveValue();
            DataTable tbl2 = (DataTable)Session["tblbgd"];
            string comcod = this.GetCompCode();
            string bgddat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string bgdnum = this.lblCurBgdNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(bgddat, 4) + this.lblCurBgdNo1.Text.Trim().Substring(3, 2) + this.txtCurBgdNo2.Text.Trim();

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string pactcode = tbl2.Rows[i]["pactcode"].ToString();
                string UnitCode = tbl2.Rows[i]["usircode"].ToString();
                double Bgdamt = Convert.ToDouble(tbl2.Rows[i]["bgdamt"].ToString().Trim());
                if (Bgdamt > 0)
                {
                    bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPCOLLECTBGD", bgdnum,
                        pactcode, UnitCode, bgddat, Bgdamt.ToString(), "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Collection Budget";
                string eventdesc = "Update Budget";
                string eventdesc2 = bgdnum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string bgddat = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataView dv = ((DataTable)Session["tblbgd"]).DefaultView;
            dv.RowFilter = ("bgdamt<>0");


            string bgdnum = this.lblCurBgdNo1.Text.Trim() + this.txtCurBgdNo2.Text.Trim();
            ReportDocument rptCust = new RealERPRPT.R_23_CR.RptCollectBgd();
            TextObject txtCompany = rptCust.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rpttxtBudgetNum = rptCust.ReportDefinition.ReportObjects["txtBudgetNum"] as TextObject;
            rpttxtBudgetNum.Text = "Budget No: " + bgdnum;
            TextObject rpttxtDate = rptCust.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtDate.Text = "Date: " + bgddat;
            TextObject txtuserinfo = rptCust.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Collection Budget";
                string eventdesc = "Print Budget";
                string eventdesc2 = "Budget No: " + bgdnum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptCust.SetDataSource(dv.ToTable());
            Session["Report1"] = rptCust;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}