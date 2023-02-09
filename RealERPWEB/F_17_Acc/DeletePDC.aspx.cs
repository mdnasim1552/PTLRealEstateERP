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
    public partial class DeletePDC : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Delete Post Dated Cheque information";
                //this.Master.Page.Title = "Delete Post Dated Cheque information";
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetBankName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }

        private void GetBankName()
        {

            string comcod = this.GetCompCode();
            string SeachBankName = "%" + this.txtserchBankName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", SeachBankName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlBankName.Items.Clear();
                return;
            }

            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["comcod"] = this.GetCompCode();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "All Bank";
            dr1["actdesc1"] = "000000000000- All Bank";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.Sort = ("actcode");
            dt = dv.ToTable();

            this.ddlBankName.DataTextField = "actdesc1";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = dt;
            this.ddlBankName.DataBind();
            ds1.Dispose();

        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.pnlGridPage.Visible = true;
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }


        private void ShowData()
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = " ";
            try
            {
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");

                int startRow = PageNumber * 100;
                int endRow = (PageNumber + 1) * 100;
                string SrchChequeno = "%" + this.txtserchChequeno.Text.Trim() + "%";
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTCHEQUEUPDATE", frmdate, todate, "PV%", startRow.ToString(), endRow.ToString(), SrchChequeno, BankName, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = this.HiddenSameDate(ds1.Tables[0]);
                Session["tbltopage"] = ds1.Tables[1];
                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            }

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked = true;
            //    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = false;
            //    ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = false;
            //}
            //DataTable dt1 = (DataTable)Session["tblMrr"]; 
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string actcode = ((Label)dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                string rescode = ((Label)dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                string chequeno = ((Label)dgv1.Rows[i].FindControl("lgvchnono")).Text.Trim();
                string vounum = ((Label)dgv1.Rows[i].FindControl("lgvPVnum")).Text.Trim();

                ((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                ((LinkButton)dgv1.Rows[i].FindControl("lbok")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);




                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = actcode + rescode + vounum + chequeno;
            }


            this.CalculatrGridTotal();



        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["cactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";
                    if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        dt1.Rows[j]["cactdesc"] = "";
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                }

            }
            return dt1;

        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";

                dt.Rows[i]["chkmv"] = chkmr;


                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string postrmid = hst["teamid"].ToString();

            
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string actcode = code.Substring(0, 12).ToString();
            string rescode = code.Substring(12, 12).ToString();
            string vounum = code.Substring(24, 14);
            string chequeno = code.Substring(38).ToString();
            DataTable dt = (DataTable)Session["tblMrr"];
            DataRow[] dr = dt.Select(" actcode='" + actcode + "' and rescode='" + rescode + "' and vounum='" + vounum + "' and chequeno='" + chequeno + "'");

            string isunum = dr[0]["isunum"].ToString();
            string chequqdat = Convert.ToDateTime(dr[0]["chequedat"]).ToString("dd-MMM-yyyy");
            string cactcode = dr[0]["cactcode"].ToString();

            string Chk = dr[0]["chkmv"].ToString();
            if (Chk == "False")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check CheckBox";
                return;
            }

            try
            {
                // -----------Update Patment A Table-----------------//

                bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEPDCVOUCHER", "", vounum, actcode, rescode, cactcode, chequeno, userid, Postdat, Sessionid, postrmid,
                                "", "", "", "");

                if (!resultpa)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Successfully.";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Post Dated Cheque Delete";
                    string eventdesc = "Delete Post Dated Cheque";
                    string eventdesc2 = vounum + actcode + rescode + cactcode + chequeno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                this.Data_Bind();
                this.CheckValue();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void imgBtnFirst_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = 0;
            this.ShowData();
            this.lblCurPage.Text = "1";
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            this.imgBtnPerv.Enabled = false;
            this.imgBtnNext.Enabled = true;

        }
        protected void imgBtnNext_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = PageNumber + 1;

            if (PageNumber == pageCount)
            {
                PageNumber = PageNumber - 1;
                this.imgBtnNext.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnPerv.Enabled = true;
            this.ShowData();
        }

        protected void imgBtnPerv_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = PageNumber - 1;
            if (PageNumber < 0)
            {
                PageNumber = 0;
                this.imgBtnPerv.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.ShowData();
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnNext.Enabled = true;
        }
        protected void imgBtnLast_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = pageCount - 1;
            this.ShowData();
            this.lblCurPage.Text = pageCount.ToString();
            this.lblCurPage.ToolTip = "Page " + (pageCount) + " of " + pageCount;
            this.imgBtnNext.Enabled = false;
            this.imgBtnPerv.Enabled = true;
        }
        protected void imgbtnSearchCheqNO_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }

    }
}


