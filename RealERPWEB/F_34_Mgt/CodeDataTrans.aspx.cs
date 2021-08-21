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
    public partial class CodeDataTrans : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "CodeTransfer") ? "CODE DATA TRANSFER INFORMATION " : "SOURCES AND UTILITIS";
                this.SectionView();
                this.GetAccCode();
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "CodeTransfer":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "SrAUtilities":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }



        }

        private void GetAccCode()
        {
            Session.Remove("HeadAcc1");
            string comcod = this.GetComeCode();
            string filter = this.txtserceacc.Text + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODEHEAD", filter, "", "", "", "", "", "", "", "");
            Session["HeadAcc1"] = ds1.Tables[0];
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc1";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            this.ddlAccHead_SelectedIndexChanged(null, null);
        }

        private void GetResCode()
        {

            string comcod = this.GetComeCode();
            string filter1 = "%" + this.txtserDetailsCode.Text + "%";
            string ActCode = this.ddlAccHead.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODECTRNS", filter1, ActCode, "", "", "", "", "", "", "");
            this.ddlresuorcecode.DataSource = ds3.Tables[0];
            this.ddlresuorcecode.DataTextField = "resdesc1";
            this.ddlresuorcecode.DataValueField = "rescode";
            this.ddlresuorcecode.DataBind();
            this.ddlresuorcecode_SelectedIndexChanged(null, null);

        }
        private void GetSpecification()
        {

            string comcod = this.GetComeCode();
            string rescode = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            string filter2 = "%" + this.txtserSpecification.Text + "%";
            DataSet ds4 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlSpclfication.DataSource = dt4;
            this.ddlSpclfication.DataTextField = "spdesc1";
            this.ddlSpclfication.DataValueField = "spcod";
            this.ddlSpclfication.DataBind();


        }

        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
        protected void imgbtnFindDetailsCode_Click(object sender, EventArgs e)
        {
            this.GetResCode();
        }
        protected void imgbtnFindSpecification_Click(object sender, EventArgs e)
        {
            this.GetSpecification();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblAccCodedesc.Text = this.ddlAccHead.SelectedItem.Text.Trim();
                this.lblResCodedesc.Text = (this.ddlresuorcecode.Items.Count > 0) ? this.ddlresuorcecode.SelectedItem.Text.Trim() : "";
                this.lblSpcCodedesc.Text = (this.ddlSpclfication.Items.Count > 0) ? this.ddlSpclfication.SelectedItem.Text.Trim() : "";
                this.ddlAccHead.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlSpclfication.Visible = false;
                this.lblAccCodedesc.Visible = true;
                this.lblResCodedesc.Visible = (this.ddlresuorcecode.Items.Count > 0) ? true : false;
                this.lblSpcCodedesc.Visible = (this.ddlSpclfication.Items.Count > 0) ? true : false;
                this.lbltxtSpecification.Visible = (this.ddlSpclfication.Items.Count > 0) ? true : false;
                this.txtserSpecification.Visible = (this.ddlSpclfication.Items.Count > 0) ? true : false;
                this.imgbtnFindSpecification.Visible = (this.ddlSpclfication.Items.Count > 0) ? true : false;
                this.PnlNewCode.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.GetAccCodeN();
                return;
            }

            this.lbtnOk.Text = "Ok";
            ddlAccHead_SelectedIndexChanged(null, null);
            this.ddlAccHead.Visible = true;
            this.lblAccCodedesc.Visible = false;
            this.lblResCodedesc.Visible = false;
            this.lblSpcCodedesc.Visible = false;
            this.PnlNewCode.Visible = false;
        }
        protected void ddlAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {


            DataTable dt01 = (DataTable)Session["HeadAcc1"];
            string search1 = this.ddlAccHead.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;
            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lbltxtdetailsCode.Visible = true;
                this.txtserDetailsCode.Visible = true;
                this.imgbtnFindDetailsCode.Visible = true;
                this.ddlresuorcecode.Visible = true;
                this.lbltxtSpecification.Visible = true;
                this.txtserSpecification.Visible = true;
                this.imgbtnFindSpecification.Visible = true;
                this.ddlSpclfication.Visible = true;
                this.GetResCode();

            }
            else
            {
                this.ddlresuorcecode.Items.Clear();
                this.ddlSpclfication.Items.Clear();
                this.lbltxtdetailsCode.Visible = false;
                this.txtserDetailsCode.Visible = false;
                this.imgbtnFindDetailsCode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.lbltxtSpecification.Visible = false;
                this.txtserSpecification.Visible = false;
                this.imgbtnFindSpecification.Visible = false;
                this.ddlSpclfication.Visible = false;
            }
        }
        protected void ddlresuorcecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void GetAccCodeN()
        {
            Session.Remove("HeadAccn");
            string comcod = this.GetComeCode();
            string filter = this.txtserceaccN.Text + "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODEHEAD", filter, "", "", "", "", "", "", "", "");
            Session["HeadAccn"] = ds1.Tables[0];
            this.ddlAccHeadN.DataSource = ds1.Tables[0];
            this.ddlAccHeadN.DataTextField = "actdesc1";
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
            this.GetResCodeN();
        }
        protected void imgbtnFindSpecificationN_Click(object sender, EventArgs e)
        {
            this.GetSpecificationN();
        }
        protected void ddlAccHeadN_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt01 = (DataTable)Session["HeadAccn"];
            string search1 = this.ddlAccHeadN.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;
            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lbltxtdetailsCodeN.Visible = true;
                this.txtserDetailsCodeN.Visible = true;
                this.imgbtnFindDetailsCodeN.Visible = true;
                this.ddlresuorcecodeN.Visible = true;
                this.lbltxtSpecificationN.Visible = true;
                this.txtserSpecificationN.Visible = true;
                this.imgbtnFindSpecificationN.Visible = true;
                this.ddlSpclficationN.Visible = true;
                this.GetResCodeN();

            }
            else
            {
                this.ddlresuorcecodeN.Items.Clear();
                this.ddlSpclficationN.Items.Clear();
                this.lbltxtdetailsCodeN.Visible = false;
                this.txtserDetailsCodeN.Visible = false;
                this.imgbtnFindDetailsCodeN.Visible = false;
                this.ddlresuorcecodeN.Visible = false;
                this.lbltxtSpecificationN.Visible = false;
                this.txtserSpecificationN.Visible = false;
                this.imgbtnFindSpecificationN.Visible = false;
                this.ddlSpclficationN.Visible = false;
            }
        }

        private void GetResCodeN()
        {

            string comcod = this.GetComeCode();
            string filter1 = "%" + this.txtserDetailsCodeN.Text + "%";
            string actcode = this.ddlAccHeadN.SelectedValue.ToString();
            DataSet ds3 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODECTRNS", filter1, actcode, "", "", "", "", "", "", "");
            this.ddlresuorcecodeN.DataSource = ds3.Tables[0];
            this.ddlresuorcecodeN.DataTextField = "resdesc1";
            this.ddlresuorcecodeN.DataValueField = "rescode";
            this.ddlresuorcecodeN.DataBind();
            this.ddlresuorcecodeN_SelectedIndexChanged(null, null);

        }

        private void GetSpecificationN()
        {

            string comcod = this.GetComeCode();
            string rescode = this.ddlresuorcecodeN.SelectedValue.ToString().Trim();
            string filter2 = "%" + this.txtserSpecificationN.Text + "%";
            DataSet ds4 = mgtData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlSpclficationN.DataSource = dt4;
            this.ddlSpclficationN.DataTextField = "spdesc1";
            this.ddlSpclficationN.DataValueField = "spcod";
            this.ddlSpclficationN.DataBind();


        }

        protected void ddlresuorcecodeN_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecificationN();
        }

        //private string CompanypostUnpost()
        //{

        //    string comcod = this.GetComeCode();
        //    string unpost = "" ;
        //    switch(comcod)
        //    {

        //        case "3345"://Urban
        //        case "3101"://Urban
        //            unpost = "Unpost";

        //            break;


        //    default:
        //            break;


        //    }
        //    return unpost;



        //}

        //private string GetNetAmount()
        //{ 

        //string comcod=this.GetComeCode();
        //string netamt = "";
        //switch (comcod)
        //{


        //    case "3305"://Rupayan Housing
        //    case "3306":  //Rupayan Housing      
        //    case "3311"://Rupayan Housing  (ctg)
        //    case "2305"://Land (Rupayan)
        //        netamt = "netamout";
        //        break;

        //    default:            
        //        break;


        //}

        //return netamt;


        //}

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.lnkFinalUpdate.Enabled = false;
            bool result;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Preactcode = this.ddlAccHead.SelectedValue.ToString();
            string Prerescode = (this.ddlresuorcecode.SelectedValue.ToString() != "") ? this.ddlresuorcecode.SelectedValue.ToString() : "000000000000";
            string Prespcfcod = (this.ddlSpclfication.SelectedValue.ToString() != "") ? this.ddlSpclfication.SelectedValue.ToString() : "000000000000";
            string Newactcode = this.ddlAccHeadN.SelectedValue.ToString();
            string Newrescode = (this.ddlresuorcecodeN.SelectedValue.ToString() != "") ? this.ddlresuorcecodeN.SelectedValue.ToString() : "000000000000";
            string Newspcfcod = (this.ddlSpclficationN.SelectedValue.ToString() != "") ? this.ddlSpclficationN.SelectedValue.ToString() : "000000000000";
            string date = this.txtCurDate.Text.Trim();
            // string postunpost = this.CompanypostUnpost() ;



            string Calltype = ((Newactcode.Substring(0, 2) == "24" ? "UPDATECANUNIT" : (Preactcode.Substring(0, 2) == "18" && Newactcode.Substring(0, 2) == "18") ? "UPDATEUNITTRANSFER" : "UPDATECODEDATATRANS"));
            //string NetAmount = this.GetNetAmount();


            result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", Calltype, Preactcode, Prerescode, Prespcfcod, Newactcode, Newrescode,
                                    Newspcfcod, date, userid, Terminal, Sessionid, posteddat, "", "", "", "");

            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = mgtData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }


            if (ASTUtility.Left(Newactcode, 2) == "24")
            {
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPCUINFO", Newactcode, Newrescode, date, "", "",
                                        "", "", "", "", "", "", "", "", "", "");
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
    }
}