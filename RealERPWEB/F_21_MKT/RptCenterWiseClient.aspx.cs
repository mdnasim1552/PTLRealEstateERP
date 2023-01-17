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
    public partial class RptCenterWiseClient : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Center & Branch Wise Client";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetProLocCode();

                this.GetTeamCode();
                this.GetLeadType();
                this.GetBranch();
                this.GetProspectiveClientInfo();
            }
        }

        private void GetBranch()
        {
            string comcod = this.GetCompCode();
            string leadtype = "40%"; //Center Code
            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETLEADSOURCH", leadtype, "", "", "", "", "", "", "", "");
            this.ddlBranchName.DataTextField = "gdesc";
            this.ddlBranchName.DataValueField = "gcod";
            this.ddlBranchName.DataSource = ds1.Tables[0];
            this.ddlBranchName.DataBind();

        }
        private void GetLeadType()
        {
            string comcod = this.GetCompCode();
            string leadtype = "4101%"; //Center Code
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
            string ledtype = this.ddlLeadType.SelectedValue.ToString() == "0000000" ? "%%" : this.ddlLeadType.SelectedValue.ToString();
            string branch = this.ddlBranchName.SelectedValue.ToString() == "0000000" ? "%%" : this.ddlBranchName.SelectedValue.ToString();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = objuserman.GetCenterWiseClient(comcod, frmdate, todate, mob, ledtype, branch);
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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            GetProspectiveClientInfo();
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
            this.GetProspectiveClientInfo();
        }
        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProspectiveClientInfo();
        }
    }
}
