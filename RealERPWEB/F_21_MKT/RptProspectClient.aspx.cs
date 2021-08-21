using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_21_MKT
{
    public partial class RptProspectClient : System.Web.UI.Page
    {
        ProcessAccess GetData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetProspectClient();
                this.GetSMSMAILTempalte();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Prospect Notification";

            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProspectClient()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usertype = hst["usrrmrk"].ToString();
            string comcod = this.GetComeCode();
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string Logempid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            DataSet ds3 = GetData.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE_01", "CLNTINFOSUM", null, null, null, "8301%", "%", "%", "%", "%", "%", "%",
                "%", "%", "%");
            if (ds3 == null)
            {
                this.gvSummary.DataSource = null;
                this.gvSummary.DataBind();
                return;
            }


            this.gvSummary.DataSource = ds3.Tables[0];//ds3.Tables[0];//
            this.gvSummary.DataBind();
            Session["tblProspect"] = ds3.Tables[0];

        }
        protected void ddlSmsMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSMSMAILTempalte();
        }
        private void GetSMSMAILTempalte()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", "2161%", "2169%", "", "", "", "", "", "", "");

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

                view.RowFilter = "gcod=" + template;
                dt1 = view.ToTable();

                this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
            }

            else
            {
                view.RowFilter = "gcod=" + template;
                dt1 = view.ToTable();

                this.lblTemMSg.Text = dt1.Rows[0]["mailcont"].ToString();
            }

            ViewState["tblsmsmailtempcont"] = dt;
        }
        protected void lnkSend_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            this.SaveCheckedValue();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblProspect"];
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
                string userid = dr["usercode"].ToString();

                string pactcode = dr["sircode"].ToString();
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

            this.GetProspectClient();
        }
        private void SaveCheckedValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblProspect"];


            for (int i = 0; i < this.gvSummary.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = ((Label)this.gvSummary.Rows[i].FindControl("lsircode")).Text.ToString();


                //string ntype = ((Label)this.gvAdDetails.Rows[i].FindControl("lblAdno")).Text.ToString(); 


                string phone = ((Label)this.gvSummary.Rows[i].FindControl("lblgvphone")).Text.ToString(); ;
                string email = ((Label)this.gvSummary.Rows[i].FindControl("lblgvemail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvSummary.Rows[i].FindControl("chkstatus")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvSummary.Rows[i].FindControl("lblusercode")).Text.ToString();/// need 


                DataRow dr1 = dt1.NewRow();


                dr1["usercode"] = refno;
                dr1["sircode"] = actcode;

                dr1["smstatus"] = checkstatus;
                dr1["phone"] = phone;
                dr1["email"] = email;

                dt1.Rows.Add(dr1);

                Session["tblProspect"] = dt1;
            }
        }

    }
}