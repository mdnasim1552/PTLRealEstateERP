using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_24_CC
{
    public partial class RptProjectwiseClient : System.Web.UI.Page
    {
        ProcessAccess GetData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetProjectName();
                this.GetSMSMAILTempalte();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Notification";

            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void ddlSmsMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSMSMAILTempalte();
        }
        private void GetSMSMAILTempalte()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", "2431%", "2439%", "", "", "", "", "", "", "");
            if(ds1 == null)
            {
                return;
            }
            DataTable dt1 = new DataTable();
            DataView view = new DataView();
            view.Table = ds1.Tables[0];
            string contype = this.ddlSmsMail.SelectedValue.ToString();

            if (contype == "01")
            {
                view.RowFilter = "active='True'";
                dt1 = view.ToTable();
            }
            else
            {
                view.RowFilter = "mactive='True'";
                dt1 = view.ToTable();
            }
            ViewState["SMSMAILCONTTENT"] = dt1;
            this.SMS_MAIL_BIND();
        }

        private void SMS_MAIL_BIND()
        {
            DataTable dt = (DataTable)ViewState["SMSMAILCONTTENT"];
            this.ddlSMSMAILTEMP.DataTextField = "gdesc";
            this.ddlSMSMAILTEMP.DataValueField = "gcod";
            this.ddlSMSMAILTEMP.DataSource = dt;
            this.ddlSMSMAILTEMP.DataBind();
            ddlSMSMAILTEMP_SelectedIndexChanged(null, null);
        }
        protected void ddlSMSMAILTEMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["SMSMAILCONTTENT"];

            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            string template = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            string contype = this.ddlSmsMail.SelectedValue.ToString();

            if (contype == "01")
            {

                view.RowFilter = "gcod='" + template + "'";
                dt1 = view.ToTable();

                if (dt1.Rows.Count > 0)
                {
                    this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
                }
                //this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
            }

            else
            {
                view.RowFilter = "gcod=" + template;
                dt1 = view.ToTable();

                this.lblTemMSg.Text = dt1.Rows[0]["mailcont"].ToString();
            }

            ViewState["tblsmsmailtempcont"] = dt;
        }

        private void ShowLetterInfo()
        {
            Session.Remove("tblloan");
            //this.lblletterinfo.Visible = true;
            //this.lblClientInfo.Visible = true;
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = this.GetData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE_01", "RPTLETTERANDCUST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLetter.DataSource = null;
                this.gvLetter.DataBind();
                //this.gvClientLetter.DataSource = null;
                //this.gvClientLetter.DataBind();
                return;
            }

            this.gvLetter.DataSource = ds1.Tables[0];
            this.gvLetter.DataBind();
            Session["tblloan"] = ds1.Tables[1];
            this.Data_Bind();

        }
        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds1 = GetData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }
        private void Data_Bind()
        {
            this.gvClientLetter.DataSource = (DataTable)Session["tblloan"];
            this.gvClientLetter.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowLetterInfo();
        }
        protected void lnkSend_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            this.SaveCheckedValue();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblloan"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "smstatus=True";
            dtfinal = view.ToTable();
            //var lst = dtfinal.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();
            //Session["tblUserInfo"] = lst;
            DataTable dt1 = (DataTable)ViewState["tblsmsmailtempcont"];
            string smscontent = "";
            string mailcontent = "";
            string smsst = "";
            string mailst = "";
            if (smsstatus == "01")
            {
                smscontent = this.lblTemMSg.Text.ToString();
                smsst = "Y";
            }
            else
            {
                mailcontent = this.lblTemMSg.Text.ToString();
                mailst = "Y";
            }



            //string compsms = hst["compsms"].ToString();
            //if (compsms == "True")
            //{
            foreach (DataRow dr in dtfinal.Rows)
            {
                string userid = dr["usircode"].ToString();

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string phone = dr["phone"].ToString();
                string email = dr["email"].ToString();
                string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string mailattch = "";
                string Usircode = "";

                bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), pactcode, Usircode, userid, tdate, ntype, smsst, smscontent, mailst,
                    mailcontent, mailattch, phone, email);

            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            //}


            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Contact With Administrator";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}
            this.ShowLetterInfo();
        }
        private void SaveCheckedValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblloan"];


            for (int i = 0; i < this.gvClientLetter.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = this.ddlProjectName.SelectedValue.ToString();

                //string ntype = ((Label)this.gvAdDetails.Rows[i].FindControl("lblAdno")).Text.ToString(); 


                string phone = ((Label)this.gvClientLetter.Rows[i].FindControl("lgvclientMob")).Text.ToString(); ;
                string email = ((Label)this.gvClientLetter.Rows[i].FindControl("lgvclientemail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkstatus")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvClientLetter.Rows[i].FindControl("lgvletcodec")).Text.ToString();/// need 


                DataRow dr1 = dt1.NewRow();


                dr1["usircode"] = refno;


                dr1["smstatus"] = checkstatus;
                dr1["phone"] = phone;
                dr1["email"] = email;

                dt1.Rows.Add(dr1);

                Session["tblloan"] = dt1;
            }
        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblloan"];
            int i, index;
            if (((CheckBox)this.gvClientLetter.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletterc")).Checked = true;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletterc")).Checked = false;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "False";

                }

            }

            Session["tblloan"] = dt;
        }
    }
}