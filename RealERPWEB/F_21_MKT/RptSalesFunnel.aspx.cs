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
    public partial class RptSalesFunnel : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");


                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales Funnel Reports";
                this.Master.Page.Title = "Sales Funnel Reports";

                //string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfodate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                //this.txttodate.Text =  Convert.ToDateTime(txtfodate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                // this.GetOpenDateKPI();
                this.rbtnlst.SelectedIndex = 0;
                this.GetOpening();
                //this.rbtnlst_SelectedIndexChanged(null, null);
              // this.chkcondate_CheckedChanged(null, null);

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
        private void GetOpenDateKPI()

        {
            ViewState.Remove("tblopndate");
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETKPIOPENDATE", "", "", "", "", "", "", "", "", "");
            ViewState["tblopndate"] = Convert.ToDateTime(ds2.Tables[0].Rows[0]["cdate"]).ToString("dd-MMM-yyyy");
            
            
            //if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
            //{

            //    this.txtfodate.Text = Convert.ToDateTime(txtfodate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //    return;
            //}
            //else
            //{

            //    this.txtfodate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["cdate"]).ToString("dd-MMM-yyyy");
            //}




        }

        private void GetOpening()
        {

            //string opndate = (string)ViewState["tblopndate"];
            DateTime curdate = System.DateTime.Today;
            DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
            DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));
            if (this.rbtnlst.SelectedIndex == 0)
            {
                this.lblcondate.Visible = true;
                this.txtcondate.Visible = true;
                //this.txtcondate.Text = "";
                this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy");
                this.txttodate.Text = todate.ToString("dd-MMM-yyyy");                
                this.txtcondate.Text = curdate.ToString("dd-MMM-yyyy");


            }
            else
            {
                this.lblcondate.Visible = true;
                this.txtcondate.Visible = true;
                this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy"); ;
                this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
                this.txtcondate.Text = todate.ToString("dd-MMM-yyyy");


            }

        }

        protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
        {

            ////string opndate = (string)ViewState["tblopndate"];
            //DateTime curdate = System.DateTime.Today;
            //DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
            //DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));
            //if (this.rbtnlst.SelectedIndex == 0)
            //{
            //    this.lblcondate.Visible = true;
            //    this.txtcondate.Visible = true; 
            //   // this.txtcondate.Text = "";
            //    // this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy");
            //    // this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
            //    this.txtcondate.Text = curdate.ToString("dd-MMM-yyyy");


            //}
            //else
            //{
            //    this.lblcondate.Visible = true;
            //    this.txtcondate.Visible = true;
            //   // this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy"); ;
            //    //this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
            //    this.txtcondate.Text = curdate.ToString("dd-MMM-yyyy");


            //}
        }


        //protected void chkcondate_CheckedChanged(object sender, EventArgs e)
        //{


        //    string opndate = (string)ViewState["tblopndate"];  
        //    if (this.chkcondate.Checked == false)
        //    {
        //        this.txtcondate.Visible = false;
        //        this.txtcondate.Text = "";
        //        this.txtfodate.Text = opndate;
        //        this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

        //    }
        //    else
        //    {
        //        this.txtcondate.Visible = true;
        //        this.txtfodate.Text = opndate;
        //        this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        //        this.txtcondate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


        //    }
        //}

        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            string filter = comcod == "3374" ? "namdesgsec" : "";
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", filter, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
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

        //-------------------------------DashBoard------------------------------------------------
        private void ModalDataBind()
        {
            string comcod = this.GetComeCode();
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataTable dtprj = ((DataTable)ViewState["tblproject"]).Copy();
            DataView dvp = dtprj.DefaultView;
            dvp.RowFilter = ("comcod='" + comcod + "'");
            dtprj = dvp.ToTable();

            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            //string empid = (userrole == "1" ? "93" : lempid) + "%";
            
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "Choose Employee..", "");

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
                    dtE.Rows.Add("000000000000", "Choose Employee..", "");
                // if(dtE.Rows.Count>1)
                //dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = "000000000000";

            dtprj.Rows.Add("000000000000", "Choose Project..", "");

            this.ddlProject.DataTextField = "pactdesc";
            this.ddlProject.DataValueField = "pactcode";
            this.ddlProject.DataSource = dtprj;
            this.ddlProject.DataBind();
            this.ddlProject.SelectedValue = "000000000000";


            //profession
            DataTable dtprof = new DataTable();
            dv.RowFilter = ("gcod like '86%'");
            dtprof = dv.ToTable();
            dtprof.Rows.Add("000000000000", "Choose Peofession..", "");
            this.ddlProfession.DataTextField = "gdesc";
            this.ddlProfession.DataValueField = "gcod";
            this.ddlProfession.DataSource = dtprof;
            this.ddlProfession.DataBind();
            this.ddlProfession.SelectedValue = "000000000000";


            DataTable dtsource = new DataTable();
            dv.RowFilter = ("gcod like '31%'");
            dtsource = dv.ToTable();
            dtsource.Rows.Add("000000000000", "Choose Source..", "");
            this.ddlSource.DataTextField = "gdesc";
            this.ddlSource.DataValueField = "gcod";
            this.ddlSource.DataSource = dtsource;
            this.ddlSource.DataBind();
            this.ddlSource.SelectedValue = "000000000000";


            //Lead Status           
            dv.RowFilter = ("gcod like '95%'");
            this.ddlleadstatus.DataTextField = "gdesc";
            this.ddlleadstatus.DataValueField = "gcod";
            this.ddlleadstatus.DataSource = dv.ToTable();
            this.ddlleadstatus.DataBind();
            this.ddlleadstatus.Items.Insert(0, new ListItem("Choose Status", ""));


            //Prefered Location
            DataTable dtpreloc = new DataTable();
            dv.RowFilter = ("gcod like '89%'");
            dtpreloc = dv.ToTable();
            dtpreloc.Rows.Add("000000000000", "Choose Pref Loc..", "");
            this.ddlPrefLocation.DataTextField = "gdesc";
            this.ddlPrefLocation.DataValueField = "gcod";
            this.ddlPrefLocation.DataSource = dtpreloc;
            this.ddlPrefLocation.DataBind();
            this.ddlPrefLocation.SelectedValue = "000000000000";

            //Apartment Size
            DataTable dtaptmnt = new DataTable();
            dv.RowFilter = "gcod like '33%'";
            dtaptmnt = dv.ToTable();
            dtaptmnt.Rows.Add("000000000000", "Choose Apt. Size..", "");
            this.ddlAptSize.DataTextField = "gdesc";
            this.ddlAptSize.DataValueField = "gcod";
            this.ddlAptSize.DataSource = dtaptmnt;
            this.ddlAptSize.DataBind();
            this.ddlAptSize.SelectedValue = "000000000000";

            //Budget
            DataTable dtbudget = new DataTable();
            dv.RowFilter = "gcod like '37%'";
            dtbudget = dv.ToTable();
            dtbudget.Rows.Add("000000000000", "Choose Budget..", "");
            this.ddlBudget.DataTextField = "gdesc";
            this.ddlBudget.DataValueField = "gcod";
            this.ddlBudget.DataSource = dtbudget;
            this.ddlBudget.DataBind();
            this.ddlBudget.SelectedValue = "000000000000";

        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);

        }
        protected void gvSaleFunnel_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_bind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();
            string cdate = this.txtfodate.Text.Trim();
            string cdatef = this.txttodate.Text.Trim();

            string empid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            string prjcode = ((this.ddlProject.SelectedValue.ToString() == "") ? "%" : this.ddlProject.SelectedValue.ToString()) + "%";
            string professioncode = ((this.ddlProfession.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProfession.SelectedValue.ToString()) + "%";

            string leadstatus = (this.ddlleadstatus.SelectedValue.ToString().Trim() == "" ? "95" : this.ddlleadstatus.SelectedValue.ToString()) + "%";
            string sourch = ((this.ddlSource.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSource.SelectedValue.ToString()) + "%";
            string prefLocation = ((this.ddlPrefLocation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlPrefLocation.SelectedValue.ToString()) + "%";
            string aptSizeCode = ((this.ddlAptSize.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlAptSize.SelectedValue.ToString()) + "%";
            string budgetCode = ((this.ddlBudget.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBudget.SelectedValue.ToString()) + "%";
            string condate =this.txtcondate.Text;
            string type = this.rbtnlst.SelectedValue.ToString();
            string calltype = (type == "Stand By" ? "GETSALESFUNNEL" : "GETSALESFUNNELCONVERSATION");

            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", calltype, empid, cdate, prjcode, professioncode, cdatef, sourch, condate, leadstatus, prefLocation, aptSizeCode, budgetCode);
            if (ds1 == null)
            {
                this.grpBox.Visible = false;
                this.gvSaleFunnel.DataSource = null;
                this.gvSaleFunnel.DataBind();
                return;
            }
            this.grpBox.Visible = true;
            ViewState["tbldata"] = ds1.Tables[0];
            ViewState["tbldataCount"] = ds1.Tables[1];
            ViewState["tbldataTeams"] = ds1.Tables[2];
            ViewState["tblteamDetails"] = ds1.Tables[3];
            ViewState["tblleadPrj"] = ds1.Tables[4];
            ViewState["tblleadPrjEmp"] = ds1.Tables[5];

            ViewState["tblleadProfess"] = ds1.Tables[6];
            ViewState["tblleadProfessEmp"] = ds1.Tables[7];

            ViewState["tblleadSrc"] = ds1.Tables[8];
            ViewState["tblleadSrcEmp"] = ds1.Tables[9];
            ViewState["tblteamlead"] = ds1.Tables[10];
            ViewState["tblleadStatus"] = ds1.Tables[11];
            
            Data_bind();



        }

        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["tbldata"];
            DataTable dtc = (DataTable)ViewState["tbldataCount"];
            DataTable dtcteam = (DataTable)ViewState["tbldataTeams"];
            DataTable dtcteamDets = (DataTable)ViewState["tblteamDetails"];
            DataTable dtcprj = (DataTable)ViewState["tblleadPrj"];
            DataTable dtcprjTeam = (DataTable)ViewState["tblleadPrjEmp"];
            DataTable dtcProf = (DataTable)ViewState["tblleadProfess"];
            DataTable dtcProfEmp = (DataTable)ViewState["tblleadProfessEmp"];
            DataTable dtcsrc = (DataTable)ViewState["tblleadSrc"];
            DataTable dtcsrcemp = (DataTable)ViewState["tblleadSrcEmp"];
            DataTable dtteamlead = (DataTable)ViewState["tblteamlead"];
            DataTable tbllead = (DataTable)ViewState["tblleadStatus"];
            


            if (dt.Rows.Count == 0)
                return;
            this.gvSaleFunnel.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.gvSaleFunnel.DataSource = dt;
            this.gvSaleFunnel.DataBind();
            Session["Report1"] = gvSaleFunnel;
            ((HyperLink)this.gvSaleFunnel.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            var jsonSerialiser = new JavaScriptSerializer();

            var lst = dtc.DataTableToList<SalFunnelgraph>();
            var lst2 = dtcteam.DataTableToList<kpiTeamsgraph>();
            var lst3 = dtcteamDets.DataTableToList<kpiTeamsgraph>();
            var lst4 = dtcprj.DataTableToList<kpiPrjgraph>();
            var lst5 = dtcprjTeam.DataTableToList<kpiPrjgraph>();
            var lst6 = dtcProf.DataTableToList<kpiPrjgraph>();
            var lst7 = dtcProfEmp.DataTableToList<kpiPrjgraph>();

            var lst8 = dtcsrc.DataTableToList<kpiPrjgraph>();
            var lst9 = dtcsrcemp.DataTableToList<kpiPrjgraph>();
            var lst10 = dtteamlead.DataTableToList<kpiTeamsgraph>();
            var lst11 = tbllead.DataTableToList<kpiLead>();


            var data = jsonSerialiser.Serialize(lst);
            var data1 = jsonSerialiser.Serialize(lst2);
            var data2 = jsonSerialiser.Serialize(lst3);
            var data3 = jsonSerialiser.Serialize(lst4);
            var data4 = jsonSerialiser.Serialize(lst5);
           //profession
            var data5 = jsonSerialiser.Serialize(lst6);
            var data6 = jsonSerialiser.Serialize(lst7);
            // source 

            var data8 = jsonSerialiser.Serialize(lst8);
            var data9 = jsonSerialiser.Serialize(lst9);
            var data10 = jsonSerialiser.Serialize(lst10);
            var data11 = jsonSerialiser.Serialize(lst10);
            var data12 = jsonSerialiser.Serialize(lst11);


            var gtype = this.ddlgrpType.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + data + "','" + data1 + "','" + data2 + "','" + data3 + "','" + data4 + "','" + data5 + "','" + data6 + "','" + data8 + "','" + data9 + "','" + data10 + "','" + data11 + "','" + gtype + "','" + data12 + "')", true);

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
        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        [Serializable]
        public class SalFunnelgraph
        {
            public decimal total { get; set; }
            public decimal query { get; set; }
            public decimal lead { get; set; }
            public decimal qualiflead { get; set; }
            public decimal finalnego { get; set; }
            public decimal nego { get; set; }
            public decimal hold { get; set; }
            public decimal win { get; set; }
        }
        [Serializable]

        public class kpiTeamsgraph
        {
            public decimal total { get; set; }

            public string teamcode { get; set; }
            public string usrname { get; set; }
            public decimal query { get; set; }
            public decimal lead { get; set; }
            public decimal qualiflead { get; set; }
            public decimal finalnego { get; set; }
            public decimal nego { get; set; }
            public decimal hold { get; set; }
            public decimal win { get; set; }
        }

        [Serializable]
        public class kpiLead
        {
            public string la { get;set;}
            public string lb { get;set;}
            public string lc { get;set;}
            public string ld { get;set;}
            public string le { get;set;}
            public string lf { get;set;}
             
        }
        protected void ddlgrpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_bind();
        }
        [Serializable]

        public class kpiPrjgraph
        {
            public decimal total { get; set; }

            public string prjcode { get; set; }
            public string teamcode { get; set; }
            public string usrname { get; set; }
            public string prjname { get; set; }
            public decimal query { get; set; }
            public decimal lead { get; set; }
            public decimal qualiflead { get; set; }
            public decimal finalnego { get; set; }
            public decimal nego { get; set; }
            public decimal hold { get; set; }
            public decimal win { get; set; }
        }

        protected void ddlPrefLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }

        protected void ddlAptSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }

        protected void ddlBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }
    }
}