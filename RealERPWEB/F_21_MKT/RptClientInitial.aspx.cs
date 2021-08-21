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
namespace RealERPWEB.F_21_MKT
{
    public partial class RptClientInitial : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //  this.txtcurdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                // this.LoadddlPaper();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "MktLead") ? "Suspect Notification" : "Client Entry Initial";
                this.GetProLocCode();

                this.GetTeamCode();
                this.GetLeadType();
                this.GetProspectiveClientInfo();
                this.GetSMSMAILTempalte();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private void GetLeadType()
        {
            string comcod = this.GetCompCode();
            string leadtype = "3101%";
            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETLEADSOURCH", leadtype, "", "", "", "", "", "", "", "");

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
        private void GetTeamCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETMARKETEMP", srchoption, "", "", "", "", "", "", "", "");

            //this.ddlTeam.DataTextField = "empname";
            //this.ddlTeam.DataValueField = "empid";
            //this.ddlTeam.DataSource = ds1.Tables[0];
            //this.ddlTeam.DataBind(); 
            ds1.Dispose();





        }
        public string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        //private void LoadddlPaper()
        //{
        //    string comcod = this.GetCompCode();

        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName> lst = objuserman.GetPaperName(comcod);
        //    this.ddlAd.DataTextField = "papname";
        //    this.ddlAd.DataValueField = "papcod";
        //    this.ddlAd.DataSource = lst;
        //    this.ddlAd.DataBind();
        //}
        private void GetProLocCode()
        {
            string comcod = this.GetCompCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetProAndLocatio(comcod);
            var lsta = lst.FindAll(l => l.gcod.Substring(0, 2) == "31"); // Advertisement
            var lstb = lst.FindAll(l => l.gcod.Substring(0, 2) == "40"); // Branch
            var lstlq = lst.FindAll(l => l.gcod.Substring(0, 2) == "42");  // Lead Quality
            var lstlst = lst.FindAll(l => l.gcod.Substring(0, 2) == "95");  // Lead Status
            var leadcrdept = lst.FindAll(l => l.gcod.Substring(0, 2) == "41");  // Lead Create Department
            var ProductType = lst.FindAll(l => l.gcod.Substring(0, 2) == "65");  //Product Type
            var lst1 = lst.FindAll(l => l.gcod.Substring(0, 2) == "86");
            var lst2 = lst.FindAll(l => l.gcod.Substring(0, 2) == "89");
            var lstpro = lst.FindAll(l => l.gcod.Substring(0, 2) == "18");

            //this.ddlAd.DataTextField = "gdesc";
            //this.ddlAd.DataValueField = "gcod";
            //this.ddlAd.DataSource = lsta;
            //this.ddlAd.DataBind();



            //this.ddlBranch.DataTextField = "gdesc";
            //this.ddlBranch.DataValueField = "gcod";
            //this.ddlBranch.DataSource = lstb;
            //this.ddlBranch.DataBind();


            //this.ddllead.DataTextField = "gdesc";
            //this.ddllead.DataValueField = "gcod";
            //this.ddllead.DataSource = lstlq;
            //this.ddllead.DataBind();


            //this.ddlleadst.DataTextField = "gdesc";
            //this.ddlleadst.DataValueField = "gcod";
            //this.ddlleadst.DataSource = lstlst;
            //this.ddlleadst.DataBind();


            //this.ddlpro.DataTextField = "gdesc";
            //this.ddlpro.DataValueField = "gcod";
            //this.ddlpro.DataSource = lst1;
            //this.ddlpro.DataBind();

            //this.ddllocation.DataTextField = "gdesc";
            //this.ddllocation.DataValueField = "gcod";
            //this.ddllocation.DataSource = lst2;
            //this.ddllocation.DataBind();

            //this.ddlProject.DataTextField = "gdesc";
            //this.ddlProject.DataValueField = "gcod";
            //this.ddlProject.DataSource = lstpro;
            //this.ddlProject.DataBind();

            //this.DdlCreateDept.DataTextField = "gdesc";
            //this.DdlCreateDept.DataValueField = "gcod";
            //this.DdlCreateDept.DataSource = leadcrdept;
            //this.DdlCreateDept.DataBind();

            //this.DdlProductType.DataTextField = "gdesc";
            //this.DdlProductType.DataValueField = "gcod";
            //this.DdlProductType.DataSource = ProductType;
            //this.DdlProductType.DataBind();




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
        protected void lbtnEdit_OnClick(object sender, EventArgs e)
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblUserInfo"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string id = lst[rowindex].userid;

            var lst1 = lst.FindAll(u => u.userid == id);
            //this.lbluserid.Text = lst1[0].userid;
            //this.txtname.Text = lst1[0].name;
            //this.txtemail.Text = lst1[0].email;
            //this.txtmobile.Text = lst1[0].mob;
            //this.txtinfo.Text = lst1[0].info;
            //this.txtsentto.Text = lst1[0].sendto;
            //this.txtsize.Text = lst1[0].size.ToString();
            //this.ddlpro.SelectedItem.Text = lst1[0].pro;
            //this.ddllocation.SelectedItem.Text = lst1[0].locat;
            //this.ddlAd.SelectedValue = lst1[0].adno;
            //this.ddlProject.SelectedValue = lst1[0].pactcode;
            //this.ddlleadst.SelectedValue = lst1[0].leadst;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>)Session["tblUserInfo"];
            //string comcod = this.GetCompCode ();
            //string frmdate = this.txtfrmdate.Text.ToString();
            //string todate = this.txttodate.Text.ToString();
            //List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = objuserman.RptClientInfo(comcod,frmdate,todate);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_21_Mkt.RptClietList", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        [WebMethod(EnableSession = false)]
        protected void gvAdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ddlLeadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnSearch_OnClick(null, null);
        }
        protected void ddlSmsMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSMSMAILTempalte();
        }

        private void GetSMSMAILTempalte()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", "2131%", "2139%", "", "", "", "", "", "", "");

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

        private void SaveCheckedValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = ASITUtility03.ListToDataTable((List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblUserInfo"]);


            for (int i = 0; i < this.gvAdDetails.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = ((Label)this.gvAdDetails.Rows[i].FindControl("lblPactCode")).Text.ToString();


                //string ntype = ((Label)this.gvAdDetails.Rows[i].FindControl("lblAdno")).Text.ToString(); 


                string phone = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclmob")).Text.ToString(); ;
                string email = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclemail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkSpec")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvAdDetails.Rows[i].FindControl("lblgvusrid")).Text.ToString();/// need 


                DataRow dr1 = dt1.NewRow();


                dr1["userid"] = refno;
                dr1["pactcode"] = actcode;

                dr1["chekstatus"] = checkstatus;
                dr1["mob"] = phone;
                dr1["email"] = email;

                dt1.Rows.Add(dr1);

                Session["tblUserInfo"] = dt1;
            }
        }
        protected void lnkSend_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.SaveCheckedValue();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblUserInfo"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "chekstatus=True";
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


            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Contact With Administrator";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}
            this.lbtnSearch_OnClick(null, null);
        }
    }
}