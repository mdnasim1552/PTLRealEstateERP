using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{

    public partial class RptSalesRegressionFunnel : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales Regression Funnel Reports";
                this.GetOpening();
                GetAllSubdata();
                this.GETEMPLOYEEUNDERSUPERVISED();

                ModalDataBind();
                this.lbtnOk_Click(null, null);


            }
        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetOpening()
        {

            //string opndate = (string)ViewState["tblopndate"];
            DateTime curdate = System.DateTime.Today;
            DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
            DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));

            
            this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy"); ;
            this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
           

        }
        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ds2.Dispose();
        }
        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose(); 
        }
        private void ModalDataBind()
        {

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataTable dtprj = (DataTable)ViewState["tblproject"];
            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            //string empid = (userrole == "1" ? "93" : lempid) + "%";
            string comcod = this.GetComeCode();
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "All Employee", "");

            }

            else
            {
                DataTable dts = dv.ToTable();
                var query = (from dtl1 in dts.AsEnumerable()
                             join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                             select new
                             {
                                 gcod = dtl1.Field<string>("gcod"),
                                 gdesc = dtl1.Field<string>("gdesc"),
                                 code = dtl1.Field<string>("code")
                             }).ToList();
                dtE = ASITUtility03.ListToDataTable(query);
                if (dtE.Rows.Count >= 2)
                    dtE.Rows.Add("000000000000", "All Employee", "");
                // if(dtE.Rows.Count>1)
                //dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = "000000000000";

            dtprj.Rows.Add("000000000000", "All Project", "");

            this.ddlProject.DataTextField = "pactdesc";
            this.ddlProject.DataValueField = "pactcode";
            this.ddlProject.DataSource = dtprj;
            this.ddlProject.DataBind();
            this.ddlProject.SelectedValue = "000000000000";


            //profession

            DataTable dtprof = new DataTable();
            dv.RowFilter = ("gcod like '86%'");

            dtprof = dv.ToTable();
            dtprof.Rows.Add("000000000000", "All Peofession", "");

            this.ddlProfession.DataTextField = "gdesc";
            this.ddlProfession.DataValueField = "gcod";
            this.ddlProfession.DataSource = dtprof;
            this.ddlProfession.DataBind();
            this.ddlProfession.SelectedValue = "000000000000";


            DataTable dtRegression = new DataTable();
            dv.RowFilter = ("gcod like '42%'");

            dtRegression = dv.ToTable();
            dtRegression.Rows.Add("000000000000", "All Regression", "");

            this.ddlRegression.DataTextField = "gdesc";
            this.ddlRegression.DataValueField = "gcod";
            this.ddlRegression.DataSource = dtRegression;
            this.ddlRegression.DataBind();
            this.ddlRegression.SelectedValue = "000000000000";

            DataTable dtReason = new DataTable();
            dv.RowFilter = ("gcod like '45%'");

            dtReason = dv.ToTable();
            dtReason.Rows.Add("000000000000", "All Reason", "");

            this.ddlReason.DataTextField = "gdesc";
            this.ddlReason.DataValueField = "gcod";
            this.ddlReason.DataSource = dtReason;
            this.ddlReason.DataBind();
            this.ddlReason.SelectedValue = "000000000000";

            //Lead Status


            dv.RowFilter = ("gcod like '95%'");
            this.ddlleadstatus.DataTextField = "gdesc";
            this.ddlleadstatus.DataValueField = "gcod";
            this.ddlleadstatus.DataSource = dv.ToTable();
            this.ddlleadstatus.DataBind();
            this.ddlleadstatus.Items.Insert(0, new ListItem("All Status", "000000000000"));

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();
            string cdate = this.txtfodate.Text.Trim();
            string cdatef = this.txttodate.Text.Trim();

            string empid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            string prjcode = ((this.ddlProject.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProject.SelectedValue.ToString()) + "%";
            string professioncode = ((this.ddlProfession.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProfession.SelectedValue.ToString()) + "%";

            string leadstatus = (this.ddlleadstatus.SelectedValue.ToString().Trim() == "000000000000" ? "95" : this.ddlleadstatus.SelectedValue.ToString()) + "%";
            string regression = (this.ddlRegression.SelectedValue.ToString().Trim() == "000000000000" ? "42" : this.ddlRegression.SelectedValue.ToString()) + "%";
            string reason = (this.ddlReason.SelectedValue.ToString().Trim() == "000000000000" ? "45" : this.ddlReason.SelectedValue.ToString()) + "%";
 
            string calltype = "GETSALESREGRESSIONFUNNEL";
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE_01", calltype, empid, cdate, prjcode, professioncode, cdatef, regression, reason, leadstatus);
            if (ds1 == null)
            {
                this.gvSaleFunnel.DataSource = null;
                this.gvSaleFunnel.DataBind();
                return;
            }
            
            ViewState["tbldata"] = ds1.Tables[0];
         

            Data_bind();

        }

        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["tbldata"];
            
             
            if (dt.Rows.Count == 0)
                return;
            this.gvSaleFunnel.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.gvSaleFunnel.DataSource = dt;
            this.gvSaleFunnel.DataBind();
             
        }
        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_bind();

        }
        protected void gvSaleFunnel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSaleFunnel.PageIndex = e.NewPageIndex;
            Data_bind();
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }
        protected void ddlProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }
        protected void ddlleadstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }

        protected void gvSaleFunnel_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlRegression_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }
        protected void lnkEditfollowup_Click(object sender, EventArgs e)
        {

            try
            {



                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                string proscod = ((Label)this.gvSaleFunnel.Rows[rowindex].FindControl("lsircode")).Text;
                string gempid = ((Label)this.gvSaleFunnel.Rows[rowindex].FindControl("lblgvempid")).Text;
                string cdate = this.txttodate.Text.Trim();
                DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPROSPECTIVEDISCUSSION", proscod, cdate, "", "", "", "");

                this.rpclientinfo.DataSource = ds1.Tables[0];
                this.rpclientinfo.DataBind();
                this.lblprosname.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["prosdesc"].ToString() : ds1.Tables[0].Rows[0]["prosdesc"].ToString();
                this.lblprosphone.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["phone"].ToString() : ds1.Tables[0].Rows[0]["phone"].ToString();
                this.lblprosaddress.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["haddress"].ToString() : ds1.Tables[0].Rows[0]["haddress"].ToString();
                this.lblnotes.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["virnotes"].ToString() : ds1.Tables[0].Rows[0]["virnotes"].ToString();
                this.lblpreferloc.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["preferloc"].ToString() : ds1.Tables[0].Rows[0]["preferloc"].ToString();
                this.lblaptsize.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["aptsize"].ToString() : ds1.Tables[0].Rows[0]["aptsize"].ToString();
                this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                //this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                this.lbleditempid.Value = gempid;
                //this.lbllaststatus.InnerHtml = "Status:" + "<span style='color:#ffef2f; font-size:14px; font-weight:bold'>" + (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlsdesc"].ToString()) + "</span>";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        
    }
}