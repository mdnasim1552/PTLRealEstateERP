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
    public partial class CodeDataTransMR : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Money Receipt Edit Information ";

                //this.lblHeaderTitle.Text = (this.Request.QueryString["Type"].ToString().Trim() == "MasterBgd") ? "CODE DATA TRANSFER INFORMATION " : "SOURCES AND UTILITIS";

                this.GetAccCode();

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //  this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.Visibility();

            }
        }

        private void Visibility()
        {

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "MRSegregation":
                    this.txttransferamt.Visible = true;
                    this.lbltransferamt.Visible = true;
                    break;

                default:
                    break;


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetAccCode()
        {
            Session.Remove("HeadAcc1");
            string comcod = this.GetComeCode();
            string filter = this.txtserceacc.Text + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSPROJECTNAME", filter, "", "", "", "", "", "", "", "");
            Session["HeadAcc1"] = ds1.Tables[0];
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            this.ddlAccHead_SelectedIndexChanged(null, null);
        }

        private void GetUnitCode()
        {

            string comcod = this.GetComeCode();
            string filter1 = "%" + this.txtserDetailsCode.Text + "%";
            string ActCode = this.ddlAccHead.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETUNITNO", filter1, ActCode, "", "", "", "", "", "", "");
            this.ddlUnitcode.DataSource = ds3.Tables[0];
            this.ddlUnitcode.DataTextField = "udesc";
            this.ddlUnitcode.DataValueField = "usircode";
            this.ddlUnitcode.DataBind();
            this.ddlUnitcode_SelectedIndexChanged(null, null);

        }
        private void GetMR()
        {
            ViewState.Remove("tblmr");
            string comcod = this.GetComeCode();
            string ActCode = this.ddlAccHead.SelectedValue.ToString();
            string Unitcode = this.ddlUnitcode.SelectedValue.Substring(0, 12).ToString().Trim();
            string filter2 = "%" + this.txtserMr.Text + "%";
            DataSet ds4 = mgtData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETMONEYRECNO", filter2, ActCode, Unitcode, "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlMR.DataSource = dt4;
            this.ddlMR.DataTextField = "mrdesc";
            this.ddlMR.DataValueField = "mrno";
            this.ddlMR.DataBind();
            ViewState["tblmr"] = ds4.Tables[0];
            ds4.Dispose();

        }

        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
        protected void imgbtnFindDetailsCode_Click(object sender, EventArgs e)
        {
            this.GetUnitCode();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblAccCodedesc.Text = this.ddlAccHead.SelectedItem.Text.Trim();
                this.lblUnitCodedesc.Text = (this.ddlUnitcode.Items.Count > 0) ? this.ddlUnitcode.SelectedItem.Text.Trim() : "";
                this.lblMRdesc.Text = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedItem.Text.Trim() : "";
                this.ddlAccHead.Visible = false;
                this.ddlUnitcode.Visible = false;
                this.ddlMR.Visible = false;
                this.ddlMrN.Visible = true;
                this.lblAccCodedesc.Visible = true;
                this.lblUnitCodedesc.Visible = (this.ddlUnitcode.Items.Count > 0) ? true : false;
                this.lblMRdesc.Visible = (this.ddlMR.Items.Count > 0) ? true : false;
                this.lbltxtMr.Visible = (this.ddlMR.Items.Count > 0) ? true : false;
                this.txtserMr.Visible = (this.ddlMR.Items.Count > 0) ? true : false;
                this.imgbtnFindMr.Visible = (this.ddlMR.Items.Count > 0) ? true : false;
                this.PnlNewCode.Visible = true;
                this.GetAccCodeN();
                return;
            }

            this.lbtnOk.Text = "Ok";
            ddlAccHead_SelectedIndexChanged(null, null);
            this.ddlAccHead.Visible = true;
            this.ddlUnitcode.Visible = true;
            this.ddlMR.Visible = true;
            this.lblAccCodedesc.Visible = false;
            this.lblUnitCodedesc.Visible = false;
            this.lblMRdesc.Visible = false;
            this.PnlNewCode.Visible = false;
            this.lbltxtMr.Visible = true;
            this.txtserMr.Visible = true;
            this.imgbtnFindMr.Visible = true;
        }
        protected void ddlAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {


            //DataTable dt01 = (DataTable)Session["HeadAcc1"];
            //string search1 = this.ddlAccHead.SelectedValue.ToString().Trim();
            //DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            //if (dr1.Length == 0)
            //    return;
            this.GetUnitCode();


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void GetAccCodeN()
        {
            Session.Remove("HeadAccn");
            string comcod = this.GetComeCode();
            string filter = this.txtserceaccN.Text + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSPROJECTNAME", filter, "", "", "", "", "", "", "", "");
            Session["HeadAccn"] = ds1.Tables[0];
            this.ddlAccHeadN.DataSource = ds1.Tables[0];
            this.ddlAccHeadN.DataTextField = "actdesc";
            this.ddlAccHeadN.DataValueField = "actcode";
            this.ddlAccHeadN.DataBind();
            this.ddlAccHeadN_SelectedIndexChanged(null, null);

        }
        protected void imgbtnFindAccountN_Click(object sender, EventArgs e)
        {
            this.GetAccCodeN();
        }
        protected void imgbtnFindDetailsCodeN_Click(object sender, EventArgs e)
        {
            this.GetUnitCodeN();
        }

        protected void ddlAccHeadN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt01 = (DataTable)Session["HeadAccn"];
            //string search1 = this.ddlAccHeadN.SelectedValue.ToString().Trim();
            //DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            //if (dr1.Length == 0)
            //    return;


            //    this.txtserDetailsCodeN.Visible = true;
            //    this.imgbtnFindDetailsCodeN.Visible = true;
            //    this.ddlMrN.Visible = true;

            this.GetUnitCodeN();


            //this.ddlMrN.Items.Clear();
            //this.txtserDetailsCodeN.Visible = false;
            //this.imgbtnFindDetailsCodeN.Visible = false;
            //this.ddlMrN.Visible = false;


        }

        private void GetUnitCodeN()
        {

            string comcod = this.GetComeCode();
            string filter1 = this.txtserDetailsCodeN.Text + "%";
            string actcode = this.ddlAccHeadN.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GETUNITNO", filter1, actcode, "", "", "", "", "", "", "");
            this.ddlMrN.DataSource = ds3.Tables[0];
            this.ddlMrN.DataTextField = "udesc";
            this.ddlMrN.DataValueField = "usircode";
            this.ddlMrN.DataBind();


        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"];

            switch (Type)
            {
                case "MRSegregation":
                    this.UpdateMRSegregation();
                    break;

                default:

                    this.UpdateMrTranfer();
                    break;



            }






        }

        private void UpdateMrTranfer()
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string comcod = this.GetComeCode();
            string Preactcode = this.ddlAccHead.SelectedValue.ToString();
            string Prerescode = (this.ddlUnitcode.Items.Count > 0) ? this.ddlUnitcode.SelectedValue.ToString() : "000000000000";
            string Prespcfcod = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(0, 9).ToString() : "000000000000";
            string vounum = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(9, 14).ToString() : "000000000000";
            string Chqno = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(23).ToString() : "000000000000";
            string Newactcode = this.ddlAccHeadN.SelectedValue.ToString();
            string Newrescode = (this.ddlMrN.Items.Count > 0) ? this.ddlMrN.SelectedValue.ToString() : "000000000000";
            // string Newspcfcod = (this.ddlSpclficationN.Items.Count > 1) ? this.ddlSpclficationN.SelectedValue.ToString() : "000000000000";
            bool result = mgtData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEMRDATATRANS", Preactcode, Prerescode, Prespcfcod, Newactcode, Newrescode,
                                    vounum, Chqno, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



        }

        private void UpdateMRSegregation()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            DataTable dt = (DataTable)ViewState["tblmr"];



            string comcod = this.GetComeCode();
            string Preactcode = this.ddlAccHead.SelectedValue.ToString();
            string Prerescode = (this.ddlUnitcode.Items.Count > 0) ? this.ddlUnitcode.SelectedValue.ToString() : "000000000000";
            string mrvandcheque = this.ddlMR.SelectedValue.ToString();
            string Prespcfcod = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(0, 9).ToString() : "000000000000";
            string vounum = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(9, 14).ToString() : "000000000000";
            string Chqno = (this.ddlMR.Items.Count > 0) ? this.ddlMR.SelectedValue.Substring(23).ToString() : "000000000000";
            string Newactcode = this.ddlAccHeadN.SelectedValue.ToString();

            double toamt = (dt.Select("mrno='" + mrvandcheque + "'")).Length == 0 ? 0 : Convert.ToDouble(dt.Select("mrno='" + mrvandcheque + "'")[0]["mramt"]);
            double trnamt = Convert.ToDouble("0" + this.txttransferamt.Text.Trim());

            if (trnamt <= 0)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please fill transfer amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            if (trnamt >= toamt)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Amount within the mr amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string Newrescode = (this.ddlMrN.Items.Count > 0) ? this.ddlMrN.SelectedValue.ToString() : "000000000000";
            // string Newspcfcod = (this.ddlSpclficationN.Items.Count > 1) ? this.ddlSpclficationN.SelectedValue.ToString() : "000000000000";
            bool result = mgtData.UpdateTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEMRSEGREGATE", Preactcode, Prerescode, Prespcfcod, Newactcode, Newrescode,
                                    vounum, Chqno, toamt.ToString(), trnamt.ToString(), "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.lnkFinalUpdate.Enabled = false;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
        protected void ddlUnitcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMR();
        }
        protected void imgbtnFindMr_Click(object sender, EventArgs e)
        {
            this.GetMR();
        }
    }
}