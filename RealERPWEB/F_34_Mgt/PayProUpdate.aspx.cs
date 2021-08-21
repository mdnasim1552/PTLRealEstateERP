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
namespace RealERPWEB.F_34_Mgt
{
    public partial class PayProUpdate : System.Web.UI.Page
    {
        public static string Narration = "";
        public static double TAmount = 0;
        //public static string useridapp = "";
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // string Type=this.Request.QueryString["Type"].ToString();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "App" ? "Payment Proposal Approve" : "Payment Proposal Rpt";
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

        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {

            try
            {
                Session.Remove("tblPPno");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string username = hst["username"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string caltyp = (Request.QueryString["Type"].ToString().Trim() == "App") ? "GETPPROPOSAL" : "GETPPROPOSALRPT";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", caltyp, frmdate, todate, "", "", "", "", "", "", "");
                //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTCHEQUEUPDATE", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvAPPr.DataSource = null;
                    this.gvAPPr.DataBind();
                    this.gvAPPRPT.DataSource = null;
                    this.gvAPPRPT.DataBind();
                    return;
                }
                Session["tblPPno"] = this.HiddenSameDate(ds1.Tables[0]);
                // Session["tblPPno"] = ds1.Tables[0];
                this.Data_Bind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPPno"];
            string qstring = Request.QueryString["Type"].ToString().Trim();
            switch (qstring)
            {
                case "App":   // <asp:ListItem Value="01BGD">01. Budget</asp:ListItem>
                    this.gvAPPr.DataSource = dt;
                    this.gvAPPr.DataBind();

                    for (int i = 0; i < this.gvAPPr.Rows.Count; i++)
                    {
                        string billno = ((Label)this.gvAPPr.Rows[i].FindControl("gvbillno")).Text.Trim();
                        string ppno = ((Label)this.gvAPPr.Rows[i].FindControl("gvppno")).Text.Trim();
                        ((CheckBox)gvAPPr.Rows[i].FindControl("gvchk")).Visible = (!((CheckBox)gvAPPr.Rows[i].FindControl("gvchk")).Checked);
                        ((LinkButton)gvAPPr.Rows[i].FindControl("lbok")).Visible = (!((CheckBox)gvAPPr.Rows[i].FindControl("gvchk")).Checked);

                        LinkButton lbtn1 = (LinkButton)gvAPPr.Rows[i].FindControl("lbok");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = ppno + billno;
                    }

                    break;
                case "Rpt":
                    this.gvAPPRPT.DataSource = dt;
                    this.gvAPPRPT.DataBind();

                    break;
            }
        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            string useridapp = dt1.Rows[0]["useridapp"].ToString();
            string ppno = dt1.Rows[0]["ppno"].ToString();
            string ppno1 = dt1.Rows[0]["ppno1"].ToString();
            string bildat = dt1.Rows[0]["bildat"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["useridapp"].ToString() == useridapp)
                {
                    useridapp = dt1.Rows[j]["useridapp"].ToString();
                    dt1.Rows[j]["usrappname"] = "";
                }
                else
                {
                    useridapp = dt1.Rows[j]["useridapp"].ToString();
                }
            }

            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ppno"].ToString() == ppno)
                {
                    ppno = dt1.Rows[j]["ppno"].ToString();
                    dt1.Rows[j]["ppno1"] = "";
                }
                else
                {
                    ppno = dt1.Rows[j]["ppno"].ToString();
                }

            }
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["bildat"].ToString() == bildat)
                {
                    bildat = dt1.Rows[j]["bildat"].ToString();
                    dt1.Rows[j]["bildat"] = "";
                }
                else
                {
                    bildat = dt1.Rows[j]["bildat"].ToString();
                }
            }
            return dt1;

        }
        protected void CalculatrGridTotal()
        {
            // DataTable dttotal = (DataTable)Session["tblMrr"];
            // ((Label)this.dgv1.FooterRow.FindControl("lgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(cramt)", "")) ?
            //0.00 : dttotal.Compute("Sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblPPno"];
            for (int i = 0; i < this.gvAPPr.Rows.Count; i++)
            {
                string apprv = (((CheckBox)this.gvAPPr.Rows[i].FindControl("gvchk")).Checked) ? "1" : "0";
                dt.Rows[i]["apprv"] = apprv;
                ((CheckBox)this.gvAPPr.Rows[i].FindControl("gvchk")).Enabled = (((CheckBox)this.gvAPPr.Rows[i].FindControl("gvchk")).Checked) ? false : true;
                ((LinkButton)this.gvAPPr.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.gvAPPr.Rows[i].FindControl("gvchk")).Checked) ? false : true;
            }
            Session["tblPPno"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {

            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string ppno = code.Substring(0, 14).ToString();
            string billno = code.Substring(14).ToString();

            bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "UPDATEPAYPRO", ppno, billno, userid, Terminal, "", "",
                                    "", "", "", "", "", "", "", "", "");
            if (!resultpa)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            if (resultpa)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Payment Proposal approved!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string qstring = Request.QueryString["Type"].ToString().Trim();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblPPno"];
            ReportDocument rptppa = null;
            if (qstring == "Rpt")
            {
                rptppa = new RealERPRPT.R_34_Mgt.RptPayProApproved();//R_17_Acc.rptPostDatCheque();

                TextObject rptCname = rptppa.ReportDefinition.ReportObjects["CompName"] as TextObject;
                rptCname.Text = comnam;
                TextObject rptDate = rptppa.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                rptDate.Text = "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rptppa.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptppa.SetDataSource(dt);

                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptppa.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptppa;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }

        }
        protected void lbtnUpdateAllVou_Click(object sender, EventArgs e)
        {

        }
        protected void gvAPPr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblt01"];
            int rowindex = (this.gvAPPr.PageSize) * (this.gvAPPr.PageIndex) + e.RowIndex;
            //string voudat = this.txtEntryDate.Text.Substring(0, 11);
            //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
            //                 this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

            // string billno = ((Label)this.gvAPPr.Rows[i].FindControl("gvbillno")).Text.Trim();

            string ppno = ((Label)this.gvAPPr.Rows[rowindex].FindControl("gvppno")).Text.Trim();
            string billno = ((Label)this.gvAPPr.Rows[rowindex].FindControl("gvbillno")).Text.Trim();
            //string billno = this.gvAPPr.Rows[rowindex]["gvbillno"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "DELETEPP", ppno, billno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            this.lnkOk_Click(null, null);
            //DataView dv = dt.DefaultView;
            //Session["tblt01"] = dv.ToTable();
            //this.Data_Bind();

        }
        protected void gvAPPRPT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("gvrproname");
                Label amt = (Label)e.Row.FindControl("gvramt");
                Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprvbyid")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "X" || ASTUtility.Right(code, 1) == "Y")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void gvAPPr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("gvproname");
                Label amt = (Label)e.Row.FindControl("gvamt");
                Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "postedbyid")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "X" || ASTUtility.Right(code, 1) == "Y")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
    }
}


