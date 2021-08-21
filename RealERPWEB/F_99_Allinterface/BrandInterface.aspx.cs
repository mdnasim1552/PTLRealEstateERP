using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealEntity;
using RealEntity.C_21_Mkt;
using RealERPLIB;
using RealERPRDLC;
using System.Web.Services;
namespace RealERPWEB.F_99_Allinterface
{

    public partial class BrandInterface : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess GetData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Brand Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                //this.GetSMSMAILTempalte();
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;


            }
        }

        private string GetCompCode()
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
            string comcod = this.GetCompCode();
            string Data1 = "";
            string Data2 = "";

            string viewData = this.RadioButtonList1.SelectedValue.ToString();
            switch (viewData)
            {
                case "0":
                    Data1 = "0131%";
                    Data2 = "0139%";
                    break;
                case "1":
                    Data1 = "2131%";
                    Data2 = "2139%";
                    break;
                case "2":
                    Data1 = "2161%";
                    Data2 = "2169%";
                    break;
                case "3":
                    Data1 = "2431%";
                    Data2 = "2439%";
                    break;

            }




            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", Data1, Data2, "", "", "", "", "", "", "");

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

                //view.RowFilter = "gcod=" + template;

                view.RowFilter = "gcod='" + template + "'";
                dt1 = view.ToTable();
                if (dt1.Rows.Count > 0)
                {
                    this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
                }
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
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.Send_Busi_Dev();
                    break;
                case "1":
                    this.Send_Suspect();
                    break;
                case "2":
                    this.Send_Prospect();
                    break;
                case "3":
                    this.Send_Customer();
                    break;

            }

        }
        //Business Development
        #region
        private void GetYEARLAND()
        {
            ViewState.Remove("tblempname");
            string comcod = this.GetCompCode();
            DataSet ds3 = GetData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETLANDDDLYEARINFO", "", "", "", "", "", "", "", "", "");
            this.ddlyearland.DataTextField = "sirdesc";
            this.ddlyearland.DataValueField = "sircode";
            this.ddlyearland.DataSource = ds3.Tables[0];
            this.ddlyearland.DataBind();
            this.GetGridData();
        }
        protected void ddlyearland_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetGridData();
        }

        private void GetGridData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string comcod = this.GetCompCode();
            string yearcod = ASTUtility.Left(ddlyearland.SelectedValue.ToString(), 7) + "%";
            DataSet ds3 = GetData.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE_01", "LINFOSUM", null, null, null, yearcod, Empid, "%", "%", "%", "%", "%",
               "%", "%", "%", "%", "%");

            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();


            ViewState["tblsummData"] = ds3.Tables[0];
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            DataView dv1 = ds3.Tables[0].Copy().DefaultView;
            dv1.RowFilter = ("active='True'");

            this.gvSummary.DataSource = dv1.ToTable();
            this.gvSummary.DataBind();
            Session["tblBusinessData"] = dv1.ToTable();


        }
        protected void chkAllBD_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsummData"];
            int i, index;
            if (((CheckBox)this.gvSummary.HeaderRow.FindControl("chkAllBD")).Checked)
            {

                for (i = 0; i < this.gvSummary.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSummary.Rows[i].FindControl("chkstatus")).Checked = true;
                    index = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvSummary.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSummary.Rows[i].FindControl("chkstatus")).Checked = false;
                    index = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "False";

                }

            }

            ViewState["tblsummData"] = dt;
        }

        private void Send_Busi_Dev()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveCheckedValue_BD();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblBusinessData"];
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
            foreach (DataRow dr in dtfinal.Rows)
            {
                string userid = dr["dealcode"].ToString();

                string pactcode = dr["sircode"].ToString();
                string phone = dr["cphone"].ToString();
                string email = dr["cmail"].ToString();
                string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string mailattch = "";
                string Usircode = "";

                bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), pactcode, Usircode, userid, tdate, ntype, smsst, smscontent, mailst,
                    mailcontent, mailattch, phone, email);

            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            this.GetGridData();
        }

        private void SaveCheckedValue_BD()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblBusinessData"];
            int index;

            for (int i = 0; i < this.gvSummary.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = ((Label)this.gvSummary.Rows[i].FindControl("lsircode")).Text.ToString();

                string phone = ((Label)this.gvSummary.Rows[i].FindControl("lblPhone")).Text.ToString(); ;
                string email = ((Label)this.gvSummary.Rows[i].FindControl("lblMail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvSummary.Rows[i].FindControl("chkstatus")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvSummary.Rows[i].FindControl("lblDealid")).Text.ToString();/// need 


                index = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + i;

                dt1.Rows[index]["smstatus"] = checkstatus;


                //DataRow dr1 = dt1.NewRow();


                //dr1["dealcode"] = refno;
                //dr1["sircode"] = actcode;

                //dr1["smstatus"] = checkstatus;
                //dr1["cphone"] = phone;
                //dr1["cmail"] = email;

                //dt1.Rows.Add(dr1);

                Session["tblBusinessData"] = dt1;
            }
        }
        #endregion


        //Suspect
        #region
        private void GetLeadType()
        {
            string comcod = this.GetCompCode();
            string leadtype = "3101%";
            DataSet ds1 = this.GetData.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETLEADSOURCH", leadtype, "", "", "", "", "", "", "", "");

            this.ddlLeadType.DataTextField = "gdesc";
            this.ddlLeadType.DataValueField = "gcod";
            this.ddlLeadType.DataSource = ds1.Tables[0];
            this.ddlLeadType.DataBind();
        }
        private void GetProspectiveClientInfo()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string mob = "%" + this.txtmobno.Text + "%";
            string ledtype = this.ddlLeadType.SelectedValue.ToString();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = objuserman.GetLeadAddUserInfo(comcod, frmdate, todate, mob, ledtype);
            Session["tblUserInfo"] = lst;
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst =
                (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>)Session["tblUserInfo"];
            //this.gvAdDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAdDetails.DataSource = lst;
            this.gvAdDetails.DataBind();


            string pbranch = "";
            int i = 0;
            foreach (GridViewRow gv1 in gvAdDetails.Rows)
            {

                string brach = ((Label)gv1.FindControl("lblgvbranchcode")).Text.Trim();
                if (i == 0)
                {
                    i++;
                    pbranch = brach;
                    continue;

                }
                if (pbranch != brach)
                {



                    gv1.Attributes["style"] = "background-color:#d1e8fc; font-weight:bold;";

                }

                pbranch = brach;


            }
        }
        protected void ddlLeadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnSearch_OnClick(null, null);
        }
        protected void lbtnSearch_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string mob = "%" + this.txtmobno.Text + "%";
            string ledtype = this.ddlLeadType.SelectedValue.ToString();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = objuserman.GetLeadAddUserInfo(comcod, frmdate, todate, mob, ledtype);
            Session["tblUserInfo"] = lst;
            this.Data_Bind();
        }
        protected void chkAllSus_CheckedChanged(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblUserInfo"];

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst =
               (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>)Session["tblUserInfo"];

            DataTable dt = ASITUtility03.ListToDataTable(lst);
            int i, index;
            if (((CheckBox)this.gvAdDetails.HeaderRow.FindControl("chkAllSus")).Checked)
            {

                for (i = 0; i < this.gvAdDetails.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkSpec")).Checked = true;
                    index = (this.gvAdDetails.PageSize) * (this.gvAdDetails.PageIndex) + i;
                    dt.Rows[index]["chekstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvAdDetails.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkSpec")).Checked = false;
                    index = (this.gvAdDetails.PageSize) * (this.gvAdDetails.PageIndex) + i;
                    dt.Rows[index]["chekstatus"] = "False";

                }

            }

            Session["tblUserInfo"] = dt.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>();
        }
        private void Send_Suspect()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveCheckedValue_Susp();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst =
               (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>)Session["tblUserInfo"];

            DataTable dt = ASITUtility03.ListToDataTable(lst);


            //DataTable dt = (DataTable)Session["tblUserInfo"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "chekstatus=True";
            dtfinal = view.ToTable();
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
            foreach (DataRow dr in dtfinal.Rows)
            {
                string userid = dr["userid"].ToString();

                string pactcode = dr["pactcode"].ToString();
                string phone = dr["mob"].ToString();
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

            this.lbtnSearch_OnClick(null, null);
        }

        private void SaveCheckedValue_Susp()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst =
               (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>)Session["tblUserInfo"];

            DataTable dt1 = ASITUtility03.ListToDataTable(lst);



            int index;
            for (int i = 0; i < this.gvAdDetails.Rows.Count; i++)
            {

                string actcode = ((Label)this.gvAdDetails.Rows[i].FindControl("lblPactCode")).Text.ToString();
                string phone = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclmob")).Text.ToString(); ;
                string email = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclemail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkSpec")).Checked == true ? "True" : "False";

                string refno = ((Label)this.gvAdDetails.Rows[i].FindControl("lblgvusrid")).Text.ToString();/// need 
                index = (this.gvAdDetails.PageSize) * (this.gvAdDetails.PageIndex) + i;



                dt1.Rows[index]["chekstatus"] = checkstatus;


                //DataRow dr1 = dt1.NewRow();


                //dr1["userid"] = refno;
                //dr1["pactcode"] = actcode;

                //dr1["chekstatus"] = checkstatus;
                //dr1["mob"] = phone;
                //dr1["email"] = email;

                //dt1.Rows.Add(dr1);

                Session["tblUserInfo"] = dt1.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>(); ;
            }
        }

        #endregion

        //Prospect
        #region
        private void GetProspectClient()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usertype = hst["usrrmrk"].ToString();
            string comcod = this.GetCompCode();
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string Logempid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            DataSet ds3 = GetData.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE_01", "CLNTINFOSUM", null, null, null, "8301%", "%", "%", "%", "%", "%", "%",
                "%", "%", "%");
            if (ds3 == null)
            {
                this.gvProspect.DataSource = null;
                this.gvProspect.DataBind();
                return;
            }


            this.gvProspect.DataSource = ds3.Tables[0];//ds3.Tables[0];//
            this.gvProspect.DataBind();
            Session["tblProspect"] = ds3.Tables[0];

        }

        protected void chkAllPros_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblProspect"];
            int i, index;
            if (((CheckBox)this.gvProspect.HeaderRow.FindControl("chkAllPros")).Checked)
            {

                for (i = 0; i < this.gvProspect.Rows.Count; i++)
                {
                    ((CheckBox)this.gvProspect.Rows[i].FindControl("chkstatus")).Checked = true;
                    index = (this.gvProspect.PageSize) * (this.gvProspect.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvProspect.Rows.Count; i++)
                {
                    ((CheckBox)this.gvProspect.Rows[i].FindControl("chkstatus")).Checked = false;
                    index = (this.gvProspect.PageSize) * (this.gvProspect.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "False";

                }

            }

            Session["tblProspect"] = dt;
        }

        private void Send_Prospect()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveCheckedValue_Pros();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblProspect"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "smstatus=True";
            dtfinal = view.ToTable();
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
        private void SaveCheckedValue_Pros()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblProspect"];
            int index;

            for (int i = 0; i < this.gvProspect.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = ((Label)this.gvProspect.Rows[i].FindControl("lsircode")).Text.ToString();


                //string ntype = ((Label)this.gvAdDetails.Rows[i].FindControl("lblAdno")).Text.ToString(); 


                string phone = ((Label)this.gvProspect.Rows[i].FindControl("lblgvphone")).Text.ToString(); ;
                string email = ((Label)this.gvProspect.Rows[i].FindControl("lblgvemail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvProspect.Rows[i].FindControl("chkstatus")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvProspect.Rows[i].FindControl("lblusercode")).Text.ToString();/// need 

                index = (this.gvProspect.PageSize) * (this.gvProspect.PageIndex) + i;

                dt1.Rows[index]["smstatus"] = checkstatus;

                //DataRow dr1 = dt1.NewRow();


                //dr1["usercode"] = refno;
                //dr1["sircode"] = actcode;

                //dr1["smstatus"] = checkstatus;
                //dr1["phone"] = phone;
                //dr1["email"] = email;

                //dt1.Rows.Add(dr1);

                Session["tblProspect"] = dt1;
            }
        }

        #endregion


        //Customer
        #region

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "18%";
            DataSet ds1 = GetData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "All Project";
            ds1.Tables[0].Rows.Add(dr1);


            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = "000000000000";
            this.ddlProjectName_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.gvClientLetter.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvClientLetter.DataSource = (DataTable)Session["tblloan"];
            this.gvClientLetter.DataBind();
        }
        private void ShowLetterInfo()
        {
            Session.Remove("tblloan");
            string comcod = this.GetCompCode();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = this.GetData.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE_01", "RPTLETTERANDCUST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvClientLetter.DataSource = null;
                this.gvClientLetter.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[1]);
            Session["tblloan"] = dt;
            this.gvClientLetter.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvClientLetter.DataSource = dt;
            this.gvClientLetter.DataBind();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    Session["Report1"] = gvClientLetter;
                    ((HyperLink)this.gvClientLetter.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
                }

            }





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

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowLetterInfo();

        }

        private void Send_Customer()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveCheckedValue_Cust();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblloan"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "smstatus=True";
            dtfinal = view.ToTable();

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


            foreach (DataRow dr in dtfinal.Rows)
            {
                string userid = dr["usircode"].ToString();

                string pactcode = dr["pactcode"].ToString();
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

            this.ShowLetterInfo();

        }

        private void SaveCheckedValue_Cust()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblloan"];

            int index;
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

                index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;

                dt1.Rows[index]["smstatus"] = checkstatus;

                //DataRow dr1 = dt1.NewRow();


                //dr1["usircode"] = refno;


                //dr1["smstatus"] = checkstatus;
                //dr1["phone"] = phone;
                //dr1["email"] = email;

                //dt1.Rows.Add(dr1);

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
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkstatus")).Checked = true;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkstatus")).Checked = false;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["smstatus"] = "False";

                }

            }

            Session["tblloan"] = dt;
        }

        #endregion

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSMSMAILTempalte();
            this.GetAddWrkData();
            //this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlBusDev.Visible = true;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    this.GetYEARLAND();

                    break;

                case "1":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = true;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";



                    this.GetLeadType();
                    this.GetProspectiveClientInfo();

                    break;
                case "2":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = true;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";

                    this.GetProspectClient();
                    break;
                case "3":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = true;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";

                    this.GetProjectName();

                    break;
                case "4":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "5":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "6":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "7":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "8":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "9":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "10":
                    this.pnlBusDev.Visible = false;
                    this.pnlSuspect.Visible = false;
                    this.pnlProspect.Visible = false;
                    this.pnlCustomer.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;


            }


        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetAddWrkData();
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetAddWrkData();
        }
        private void GetAddWrkData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            //string frmdate = this.txtdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";


            DataSet ds2 = GetData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMODDASH", todate, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }

            //this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Initial</div></div></div>";

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Land Owner</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + "</div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Prospect</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + "</div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Suspect</div></div></div>";

            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Customer</div></div></div>";

            //this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + "</div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Payment Update</div></div></div>";

            //this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Cus. Payment dues</div></div></div>";

            //this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + "</div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Dues Reminder</div></div></div>";

            //this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + "</div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Client Birth Day</div></div></div>";

            //this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Client Marr. Day</div></div></div>";

            //this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + "</div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Pro. Client DOB</div></div></div>";

            //this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + "</div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>Pre. Client DOM</div></div></div>";

            //Session["tbladdwrk"] = ds2.Tables[0];

            //DataTable dt = new DataTable();
            //DataView dv;
            ////Intial
            //dt = ((DataTable)ds2.Tables[0]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("approvbyid=''");
            //this.Data_Bind("grvRptCliMod", dt);

            //////Checked
            //dt = ((DataTable)ds2.Tables[0]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("chkbyid='' and auditid='' and approvbyid=''");
            //this.Data_Bind("gvcltmodchk", dv.ToTable());
            ////Forward

            //////Audit
            //dt = ((DataTable)ds2.Tables[0]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("chkbyid<>'' and auditid='' and approvbyid=''");
            //this.Data_Bind("gvCltmodaduit", dv.ToTable());

            //////Approval
            //dt = ((DataTable)ds2.Tables[0]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("chkbyid<>'' and auditid<>'' and approvbyid=''");
            //this.Data_Bind("gvCltmodapp", dv.ToTable());
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "grvRptCliMod":
                    //this.grvRptCliMod.DataSource = dt;
                    //this.grvRptCliMod.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvcltmodchk":
                    //this.gvcltmodchk.DataSource = dt;
                    //this.gvcltmodchk.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvCltmodaduit":
                    //this.gvCltmodaduit.DataSource = dt;
                    //this.gvCltmodaduit.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCltmodapp":
                    //this.gvCltmodapp.DataSource = dt;
                    //this.gvCltmodapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;


            }

            //DataTable dt = (DataTable)Session["tblfeaprjLand"];



        }






    }
}