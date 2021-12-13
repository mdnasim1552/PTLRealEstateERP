using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using AjaxControlToolkit;
using System.IO;
namespace RealERPWEB.F_51_LBgd
{
    public partial class LinkLandPurInfo : System.Web.UI.Page
    {
        public static int i, j;
        public static string Url = "";
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Purchase Information";
                this.GetAllSubdata();
                this.LoadGrid();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetAllSubdata()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETGENINFO", "", "", "", "", "", "", "", "", "");
            ViewState["tblsubddl"] = ds2.Tables[0];
            //ViewState["tblstatus"] = ds2.Tables[1];
            ds2.Dispose();
        }

        private void LoadGrid()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            string patcode = this.Request.QueryString["patcode"].ToString();
            string Lsircode = this.Request.QueryString["sircode"].ToString();
            string type = "";
            if (this.Request.QueryString.AllKeys.Contains("type"))
            {
                type = this.Request.QueryString["type"].ToString();
            }
            string cDate = (type == "DocUpload") ? "0201%" : "0202%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "SHOWLANDHISTORY", patcode, Lsircode, cDate, "", "", "", "", "", "");
            Session["Cust"] = ds1.Tables[1];
            Session["CustTb0"] = ds1.Tables[0];

            //DataTable dt3=(DataTable)Session["Cust"];
            this.lblPactDesc.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            this.lblCustcode.Text = ds1.Tables[1].Rows[0]["usirdesc"].ToString();

            this.Customer.Text = (type == "SaleDocUpload") ? "Plot Info" : "Land Owner";
            //DataRow[] dr = ds1.Tables[0].Select("gcod='01001'");
            //dr[0]["gdesc1"] = (((DataTable)Session["Cust"]).Select("usircode='" + Lsircode + "'"))[0]["usirdesc"];



            if (type == "DocUpload")
            {
                pnlDocUpload.Visible = true;
                this.pnlBaIn.Visible = false;
                this.pnlPlIn.Visible = false;
                this.lUpdatPerInfo.Visible = false;
                //this.gvPersonalInfo.DataSource = null;
                //this.gvPersonalInfo.DataBind();

                this.ddlDocType.DataTextField = "gdesc";
                this.ddlDocType.DataValueField = "gcod";
                this.ddlDocType.DataSource = ds1.Tables[2];
                this.ddlDocType.DataBind();
                ds1.Dispose();
                this.btnShowimg_Click(null, null);

            }
            else if (type == "SaleDocUpload")
            {
                pnlDocUpload.Visible = true;
                this.pnlBaIn.Visible = false;
                this.pnlPlIn.Visible = false;
                this.lUpdatPerInfo.Visible = false;
                //this.gvPersonalInfo.DataSource = null;
                //this.gvPersonalInfo.DataBind();

                this.ddlDocType.DataTextField = "gdesc";
                this.ddlDocType.DataValueField = "gcod";
                this.ddlDocType.DataSource = ds1.Tables[2];
                this.ddlDocType.DataBind();
                ds1.Dispose();
                this.btnShowimg_Click(null, null);

            }
            else
            {
                pnlDocUpload.Visible = false;
                this.pnlBaIn.Visible = true;
                this.pnlPlIn.Visible = true;
                this.lUpdatPerInfo.Visible = true;
                this.Data_Bind();

            }


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["CustTb0"];
            this.BasicInfo();
            this.ShowPlotInfo();



        }

        private void BasicInfo()
        {
            DataTable dt = (DataTable)Session["CustTb0"];
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = ("gcod1='0101'");
            this.GvOwnerLand.DataSource = dv2.ToTable();
            this.GvOwnerLand.DataBind();

            //DataTable dt1 = ((DataTable)ViewState["tblsubddl"]).Copy();

            DropDownList ddlgval;
            for (int i = 0; i < dv2.ToTable().Rows.Count; i++)
            {

                string gcod = dv2.ToTable().Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0101007":
                        ((Panel)this.GvOwnerLand.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Visible = false;

                        break;

                    case "0101101": //Dealing Person workdone

                        DataView dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                        //dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '93%'");
                        ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        if (ddlgval.Items.Count > 0)
                            ddlgval.SelectedIndex = 0;
                        ddlgval.SelectedValue = ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    default:
                        ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.GvOwnerLand.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }
            }





        }

        private void ShowPlotInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();


            DataTable dt1 = (DataTable)ViewState["tblsubddl"];


            DataTable dt = (DataTable)Session["CustTb0"];
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = ("gcod1='0102'");
            this.gvplot.DataSource = dv2.ToTable();
            this.gvplot.DataBind();

            DataView dv1;
            DropDownList ddlgval;
            for (int i = 0; i < dv2.ToTable().Rows.Count; i++)
            {

                string gcod = dv2.ToTable().Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0102001": //Zone
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlTh")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlMoz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        if (ddlgval.Items.Count > 0)
                            ddlgval.SelectedIndex = 0;
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;



                    case "0102003": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlTh")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlMoz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Visible = false;



                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        if (ddlgval.Items.Count > 0)
                            ddlgval.SelectedIndex = 0;
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;


                    case "0102005": //Thana

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlMoz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;
                    case "0102007": //Moaza                   
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlTh")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "0"));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                        break;


                    default:
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlTh")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).Visible = false;

                        ((Panel)this.gvplot.Rows[i].FindControl("pnlMoz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).Visible = false;

                        break;

                }

            }
        }
        protected void ddlvalZone_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            DataTable dt = (DataTable)Session["CustTb0"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("gcod1='0102'");

            for (int i = 0; i < dv1.ToTable().Rows.Count; i++)
            {

                string gcod = dv1.ToTable().Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0102003": //District   
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Dist = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalZone")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + Dist + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0102005": //Thana//Police Station   

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Thana = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + Thana + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0102007": //Moaza                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Moaza = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalTh")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + Moaza + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;


                }
            }
        }
        protected void ddlvald_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            DataTable dt = (DataTable)Session["CustTb0"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("gcod1='0102'");

            for (int i = 0; i < dv1.ToTable().Rows.Count; i++)
            {

                string gcod = dv1.ToTable().Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0102005": //Thana//Police Station   

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Thana = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + Thana + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();

                        break;
                    case "0102007": //Moaza                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Moaza = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalTh")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + Moaza + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;


                }
            }
        }
        protected void ddlvalTh_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            DataTable dt = (DataTable)Session["CustTb0"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("gcod1='0102'");

            for (int i = 0; i < dv1.ToTable().Rows.Count; i++)
            {

                string gcod = dv1.ToTable().Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0102007": //Moaza                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdValplot")).Visible = false;
                        string Moaza = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalTh")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + Moaza + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();

                        break;


                }
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string patcode = this.Request.QueryString["patcode"].ToString();
            string ssirCode = this.Request.QueryString["sircode"].ToString();

            for (int i = 0; i < this.GvOwnerLand.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GvOwnerLand.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.GvOwnerLand.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvVal")).Text.Trim();


                if (Gcode == "0101007")
                {

                    Gvalue = (((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.GvOwnerLand.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

                else if (Gcode == "0101101")
                {

                    Gvalue = ((DropDownList)this.GvOwnerLand.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }


                CustData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "INSERTUPDATELANDPURINF", patcode, ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            for (int i = 0; i < this.gvplot.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvplot.Rows[i].FindControl("lblgvItmCodeplot")).Text.Trim();
                string gtype = ((Label)this.gvplot.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvValplot")).Text.Trim();
                if (Gcode == "0102001")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalZone")).SelectedValue.ToString();
                }
                else if (Gcode == "0102003")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedValue.ToString();
                }
                else if (Gcode == "0102005")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalTh")).SelectedValue.ToString();
                }
                else if (Gcode == "0102007")
                {
                    Gvalue = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalMoz")).SelectedValue.ToString();
                }

                CustData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "INSERTUPDATELANDPURINF", patcode, ssirCode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Land INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.Request.QueryString["custid"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (code == "01001")
            //    {

            //        txtgvname.ReadOnly = true;

            //    }

            //}

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt= ((DataTable)Session["CustTb0"]).Copy();

            //  DataRow[] drname = dt.Select("gcod='01001'");
            // string Landownname = drname[0]["gdesc1"].ToString();

            // DataRow[] drk = dt.Select("gcod='03001'");
            // string khata = drk[0]["gdesc1"].ToString();
            //  //

            // DataView dv1 = ((DataTable)Session["CustTb0"]).DefaultView;
            // dv1.RowFilter = ("gcod not in ('01001', '03001') ");


            //ReportDocument rptstk = new RealERPRPT.R_51_LBgd.RptLandHistoryInfo();
            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = this.lblPactDesc.Text.Substring(5);
            //TextObject txtUnitName = rptstk.ReportDefinition.ReportObjects["txtUnitName"] as TextObject;
            //txtUnitName.Text = Landownname +  ", "  + khata;




            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            ////if (ConstantInfo.LogStatus == true)
            ////{
            ////    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            ////    string eventdesc = "Print Report";
            ////    string eventdesc2 = "";
            ////    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            ////}
            //rptstk.SetDataSource(dv1.ToTable());


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string procode = this.Request.QueryString["patcode"].ToString();
            string sircode = this.Request.QueryString["sircode"].ToString();
            string doctype = this.ddlDocType.SelectedValue.ToString();
            this.lblMesg.Text = "";
            this.lblMesg2.Text = "";
            string docname = "";
            if (doctype == "0000000")
            {
                this.lblMesg2.Text = "Select Doc Type";
                this.ddlDocType.Focus();

                return;
            }
            else
            {
                string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);

                if (AsyncFileUpload1.HasFile)
                {

                    Random rnd = new Random();
                    string rndnumber = rnd.Next().ToString();
                    string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                    AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Lands/") + rndnumber + extension);

                    docname = this.txtdocsName.Text.ToString();
                    //  Url = procode + extension;

                    Url = rndnumber + extension;
                }
            }



            // i = i + 1;     

            //Url = Url.Substring(0,(Url.Length-1));

            //
            string type = this.Request.QueryString["type"].ToString();
            string calltype = "";

            if (type == "SaleDocUpload")
            {
                calltype = "FLAT_IMAGE_UPLOAD";
            }
            else
            {
                calltype = "LAND_IMAGE_UPLOAD";
            }

            bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", calltype, procode, sircode, doctype, Url, docname, type, "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg2.Text = "";
                this.txtdocsName.Text = "";
                this.lblMesg2.Text = "Successfully Updated ";
                this.btnShowimg_Click(null, null);

            }

        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblimgPath");

            DataTable tbfilePath = new DataTable();
            tbfilePath.Columns.Add("filePath", Type.GetType("System.String"));
            tbfilePath.Columns.Add("filePath1", Type.GetType("System.String"));
            tbfilePath.Columns.Add("supinfo", Type.GetType("System.String"));
            tbfilePath.Columns.Add("procode", Type.GetType("System.String"));
            ViewState["tblimgPath"] = tbfilePath;

            DataTable tbl2 = (DataTable)ViewState["tblimgPath"];
            string comcod = this.GetCompCode();
            // string ssircode = this.ddlBestSupplier.SelectedValue.ToString();

            string procode = this.Request.QueryString["patcode"].ToString();
            string sircode = this.Request.QueryString["sircode"].ToString();
            string doctype = (this.ddlDocType.SelectedValue.ToString() == "0000000") ? "%" : this.ddlDocType.SelectedValue.ToString() + "%";
            string calltype = "";
            string type = this.Request.QueryString["type"].ToString();

            if (type == "SaleDocUpload")
            {
                calltype = "FLAT_IMAGE_SHOW";
            }
            else
            {
                calltype = "LAND_IMAGE_SHOW";
            }

            DataSet ds = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", calltype, procode, sircode, doctype, "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];

            ListViewEmpAll.DataSource = tbl1;
            ListViewEmpAll.DataBind();
            ViewState["tblimgPath"] = tbl2;
            this.btnDelall.Visible = true;
        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = "~/Upload/Lands/" + imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }
            }


        }
        protected void btnDelall_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string procode = ((Label)this.ListViewEmpAll.Items[j].FindControl("lpactcode")).Text.ToString();
                string sircode = ((Label)this.ListViewEmpAll.Items[j].FindControl("llsircode")).Text.ToString();
                string gcod = ((Label)this.ListViewEmpAll.Items[j].FindControl("lgcod")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                string lblid = ((Label)this.ListViewEmpAll.Items[j].FindControl("lblid")).Text.ToString();
                string type = this.Request.QueryString["type"].ToString();
                string calltype = "";
                if (type == "SaleDocUpload")
                {
                    calltype = "FLAT_IMAGE_DELETE";
                }
                else
                {
                    calltype = "LAND_IMAGE_DELETE";
                }


                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {


                    bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", calltype, procode, sircode, gcod, filesname, lblid, "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname.Replace("~", ""));
                        this.lblMesg.Text = " Files Removed ";
                        //this.ShowProjectFiles();
                    }
                }

            }

            this.btnShowimg_Click(null, null);
        }
        protected void GvOwnerLand_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }


}

