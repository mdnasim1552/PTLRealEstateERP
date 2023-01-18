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

    public partial class ClientInitial : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtcurdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                // this.LoadddlPaper();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Entry Initial";
                this.GetProLocCode();
                this.GetProspectiveClientInfo();
                this.GetTeamCode();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void GetProspectiveClientInfo()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string mob = "%" + this.txtmobno.Text + "%";
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = objuserman.GetAddUserInfo(comcod, frmdate, todate, mob);
            Session["tblUserInfo"] = lst;
            this.Data_Bind();
        }
        private void GetTeamCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETMARKETEMP", srchoption, "", "", "", "", "", "", "", "");

            this.ddlTeam.DataTextField = "empname";
            this.ddlTeam.DataValueField = "empid";
            this.ddlTeam.DataSource = ds1.Tables[0];
            this.ddlTeam.DataBind();
            ds1.Dispose();





        }
        public string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadddlPaper()
        {
            string comcod = this.GetCompCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName> lst = objuserman.GetPaperName(comcod);
            this.ddlAd.DataTextField = "papname";
            this.ddlAd.DataValueField = "papcod";
            this.ddlAd.DataSource = lst;
            this.ddlAd.DataBind();
        }
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

            this.ddlAd.DataTextField = "gdesc";
            this.ddlAd.DataValueField = "gcod";
            this.ddlAd.DataSource = lsta;
            this.ddlAd.DataBind();



            this.ddlBranch.DataTextField = "gdesc";
            this.ddlBranch.DataValueField = "gcod";
            this.ddlBranch.DataSource = lstb;
            this.ddlBranch.DataBind();


            this.ddllead.DataTextField = "gdesc";
            this.ddllead.DataValueField = "gcod";
            this.ddllead.DataSource = lstlq;
            this.ddllead.DataBind();


            this.ddlleadst.DataTextField = "gdesc";
            this.ddlleadst.DataValueField = "gcod";
            this.ddlleadst.DataSource = lstlst;
            this.ddlleadst.DataBind();


            this.ddlpro.DataTextField = "gdesc";
            this.ddlpro.DataValueField = "gcod";
            this.ddlpro.DataSource = lst1;
            this.ddlpro.DataBind();

            this.ddllocation.DataTextField = "gdesc";
            this.ddllocation.DataValueField = "gcod";
            this.ddllocation.DataSource = lst2;
            this.ddllocation.DataBind();

            this.ddlProject.DataTextField = "gdesc";
            this.ddlProject.DataValueField = "gcod";
            this.ddlProject.DataSource = lstpro;
            this.ddlProject.DataBind();

            this.DdlCreateDept.DataTextField = "gdesc";
            this.DdlCreateDept.DataValueField = "gcod";
            this.DdlCreateDept.DataSource = leadcrdept;
            this.DdlCreateDept.DataBind();

            this.DdlProductType.DataTextField = "gdesc";
            this.DdlProductType.DataValueField = "gcod";
            this.DdlProductType.DataSource = ProductType;
            this.DdlProductType.DataBind();




        }
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CheckMobile(string comcod, string usrid, string mobile)
        {

            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETEXISTINGUSER", mobile, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("userid <>'" + usrid + "'");
                DataTable dt1 = dv1.ToTable();

                if (dt1.Rows.Count == 0)
                {

                    var result = new { Message = "Success", result = true };
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(result);
                    return json;

                }




                else
                {

                    var result = new { Message = "Duplicate", result = false };
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(result);

                    return json;



                }
            }




        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            this.lbtnAdd.Text = "Add";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();



            //    string mob = this.txtmobile.Text;
            //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst1 = objuserman.GetExistingClient (comcod, mob);
            //    //this.lbluserid.Text = lst1[0].userid;
            //    this.txtname.Text = lst1[0].name;
            //    this.txtemail.Text = lst1[0].email;
            //    this.txtmobile.Text = lst1[0].mob;
            //   // this.txtinfo.Text = lst1[0].info;
            //    //this.txtsentto.Text = lst1[0].sendto;
            //    this.txtsize.Text = lst1[0].size.ToString ();
            //    this.ddlpro.SelectedItem.Text = lst1[0].pro;
            //    this.ddllocation.SelectedItem.Text = lst1[0].locat;

            string comcod = this.GetCompCode();
            string userid = this.lbluserid.Text.Length > 0 ? this.lbluserid.Text : this.GetUsrid();
            string mob = this.txtmobile.Text.Trim();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETEXISTINGUSER", mob, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("userid <>'" + userid + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Mobile";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];


            string potedbyid = hst["usrid"].ToString();
            string branch = this.ddlBranch.SelectedValue.ToString();
            string adno = this.ddlAd.SelectedValue;
            string name = this.txtname.Text.Trim();
            string info = this.txtinfo.Text.Trim();
            string email = this.txtemail.Text.Trim();
            string location = this.ddllocation.SelectedItem.Text;
            string locaid = this.ddllocation.SelectedValue;
            string profession = this.ddlpro.SelectedItem.Text;
            string proid = this.ddlpro.SelectedValue;
            double size = Convert.ToDouble("0" + txtsize.Text.Trim());
            string sendto = txtsentto.Text.Trim();
            DateTime entdate = Convert.ToDateTime(this.txtcurdate.Text);
            string leadtype = this.ddllead.SelectedValue.ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string leadst = this.ddlleadst.SelectedValue.ToString();
            string altphone = this.TxtAltPHone.Text.ToString();
            string nbrno = this.TxtNbrNo.Text.ToString();
            string prodtype = this.DdlProductType.SelectedValue.ToString();
            string reqsize = this.TxtReqSize.Text.ToString();
            string mettingdat = this.TxtMeetingdatetime.Value.ToString();
            string visitdate = this.TxtVisitdatetime.Value.ToString();
            string createdept = this.DdlCreateDept.SelectedValue.ToString();
            string Compname = this.TxtCompname.Text.ToString();
            string designation = this.TxtDesignation.Text.ToString();


            lst.Add(new EClassAdvertisement.EClassClentEntry
            {
                userid = userid,
                branch = branch,
                leadtype = leadtype,
                adno = adno,
                name = name,
                mob = mob,
                email = email,
                info = info,
                locat = location,
                pro = profession,
                locaid = locaid,
                proid = proid,
                size = size,
                sendto = sendto,
                entdate = entdate.ToString("dd-MMM-yyyy"),
                postedbyid = potedbyid,
                pactcode = pactcode,
                leadst = leadst,
                altphone = altphone,
                nbrclno = nbrno,
                prodtype = prodtype,
                reqsize = reqsize,
                mettingdat = mettingdat,
                visitdat = visitdate,
                createdept = createdept,
                compname = Compname,
                designation = designation
            });


            // this.SaveValue ();
            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ASITUtility03.ListToDataTable(lst);
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = _processAccess.UpdateXmlTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "INUPCLIENTINF", ds1, null, null, "", "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                this.lbtnReset_OnClick(null, null);

            }

            this.lbluserid.Text = "";
            this.lblfuserid.Text = "";
            this.GetProspectiveClientInfo();
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal();", true);




        }
        private void Data_Bind()
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst =
                (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblUserInfo"];
            this.gvAdDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
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

        public string GetfUsrid()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETUSERID", "", "", "", "", "", "", "", "", "");
            // string usrid =ds2.Tables[0].Rows[0]["userid"].ToString();
            // this.lblfuserid.Text = ds2.Tables[0].Rows[0]["userid"].ToString();


            return "'" + ds2.Tables[0].Rows[0]["userid"].ToString() + "'";
        }

        private string GetUsrid()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETUSERID", "", "", "", "", "", "", "", "", "");
            // string usrid =ds2.Tables[0].Rows[0]["userid"].ToString();
            // this.lblfuserid.Text = ds2.Tables[0].Rows[0]["userid"].ToString();      

            return ds2.Tables[0].Rows[0]["userid"].ToString();
        }
        protected void lbtnReset_OnClick(object sender, EventArgs e)
        {
            this.txtname.Text = "";
            this.txtmobile.Text = "";
            this.txtinfo.Text = "";
            this.txtemail.Text = "";
            this.txtsize.Text = "";
            this.txtsentto.Text = "";
        }


        protected void lbtnEdit_OnClick(object sender, EventArgs e)
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblUserInfo"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string id = lst[rowindex].userid;

            var lst1 = lst.FindAll(u => u.userid == id);
            this.lbluserid.Text = lst1[0].userid;
            this.txtname.Text = lst1[0].name;
            this.txtemail.Text = lst1[0].email;
            this.txtmobile.Text = lst1[0].mob;
            this.txtinfo.Text = lst1[0].info;
            this.txtsentto.Text = lst1[0].sendto;
            this.txtsize.Text = lst1[0].size.ToString();
            this.ddlpro.SelectedItem.Text = lst1[0].pro;
            this.ddllocation.SelectedItem.Text = lst1[0].locat;
            this.ddlAd.SelectedValue = lst1[0].adno;
            this.ddlProject.SelectedValue = lst1[0].pactcode;
            this.ddlleadst.SelectedValue = lst1[0].leadst;
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


            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblUserInfo"];
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

        protected void lbtnSearch_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            string mob = "%" + this.txtmobno.Text + "%";
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = objuserman.GetAddUserInfo(comcod, frmdate, todate, mob);
            Session["tblUserInfo"] = lst;
            this.Data_Bind();
        }

        protected void gvAdDetails_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAdDetails.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        //protected void txtmobile_OnTextChanged(object sender, EventArgs e)
        //{
        //    string comcod = this.GetCompCode ();
        //    string mob = this.txtmobile.Text;
        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst1 = objuserman.GetExistingClient (comcod, mob);
        //    //this.lbluserid.Text = lst1[0].userid;
        //    this.txtname.Text = lst1[0].name;
        //    this.txtemail.Text = lst1[0].email;
        //    this.txtmobile.Text = lst1[0].mob;
        //   // this.txtinfo.Text = lst1[0].info;
        //    //this.txtsentto.Text = lst1[0].sendto;
        //    this.txtsize.Text = lst1[0].size.ToString ();
        //    this.ddlpro.SelectedItem.Text = lst1[0].pro;
        //    this.ddllocation.SelectedItem.Text = lst1[0].locat;
        //}
        protected void lnkbtnAssign_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAssign();", true);
        }
        protected void lnkbtnAssignTeam_Click(object sender, EventArgs e)
        {

            // this.SaveValue ();
            string comcod = this.GetCompCode();
            string teamcode = this.ddlTeam.SelectedValue.ToString();
            string usrid = "";
            foreach (GridViewRow gv1 in gvAdDetails.Rows)

            {

                string gusrid = ((Label)gv1.FindControl("lblgvusrid")).Text.Trim();
                CheckBox chkassign = ((CheckBox)gv1.FindControl("chkassign"));
                if (chkassign.Checked && chkassign.Enabled)
                {

                    usrid = usrid + gusrid;
                }
            }
            string assigndate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool result = _processAccess.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_TEAM", "ASSIGNTOTEAM", teamcode, usrid, assigndate, "", "", "", "", "", "", "", "", "",
             "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.GetProspectiveClientInfo();



        }
        protected void gvAdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkassign = (CheckBox)e.Row.FindControl("chkassign");
                string assignid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assignid")).ToString();

                if (assignid.Length > 0)
                {
                    chkassign.Enabled = false;
                    chkassign.Checked = true;
                }

            }
        }
    }
}







