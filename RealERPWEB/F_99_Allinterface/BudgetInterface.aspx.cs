using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using AjaxControlToolkit;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class BudgetInterface : System.Web.UI.Page
    {
        // private UserManagerKPI objUser = new UserManagerKPI();
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess KpiData = new ProcessAccess();
        public static string Url = "";
        public static string name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Budget Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                RadioButtonList1.SelectedIndex = 1;
                this.GetProjectType();
                this.ddlProjectType_SelectedIndexChanged(null, null);
                RadioButtonList1_SelectedIndexChanged(null, null);
                //this.GetProfession();
                //RadioButtonList1.Items.RemoveAt(0);
                //RadioButtonList1.Items.RemoveAt(2);
                //this.Createtable();
                //this.GetNewClient();
                //RadioButtonList1.Items[0].Attributes.CssStyle.Add("visibility", "hidden");
                this.HyperLink6.NavigateUrl = "~/F_04_Bgd/RptBgdAll.aspx?comcod=" + comcod;
            }

        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            //GetBudgetData();
            RadioButtonList1_SelectedIndexChanged(null, null);


        }
        protected void GetProjectType()
        {
            string comcod = this.GetCompCode();
            //  string txtprosearch = this.txtSrcProject.Text.Trim() + "%";
            // string Level2 = (this.Request.QueryString["Type"].ToString().Trim() == "ProDetails") ? "LEVEL2" : "";
            DataSet ds4 = KpiData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETPROJECTTYPE", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.ddlProjectType.Items.Clear();
                return;
            }

            DataRow dr = ds4.Tables[0].NewRow();
            dr["comcod"] = comcod;
            dr["catdesc"] = "All Project Type";
            dr["catcode"] = "00000";
            dr["slno"] = "9";
            ds4.Tables[0].Rows.InsertAt(dr, 0);
            ds4.Tables[0].DefaultView.Sort = "slno ASC";

            //this.ddlProjectType.Items.Insert(0, "--Select--");
            this.ddlProjectType.DataTextField = "catdesc";
            this.ddlProjectType.DataValueField = "catcode";
            this.ddlProjectType.DataSource = ds4.Tables[0];
            this.ddlProjectType.DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            string ptype = dt1.Rows[0]["ptype"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ptype"].ToString() == ptype)
                {
                    ptype = dt1.Rows[j]["ptype"].ToString();
                    dt1.Rows[j]["pdesc"] = "";

                }

                else
                {
                    ptype = dt1.Rows[j]["ptype"].ToString();

                }
            }
            return dt1;

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gvPrjInfo_RowDataBound(null, null);
            //this.GetBudgetData();
            this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlPrjLink.Visible = true;
                    this.pnlBgd.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                case "1":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "2":
                    //this.pnlPrjLink.Visible = true;

                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "3":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "4":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "5":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "6":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "7":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["style"] = "background: #430000; display:none; ";
                    break;
                case "8":
                    this.pnlBgd.Visible = true;
                    this.pnlPrjLink.Visible = false;
                    this.RadioButtonList1.Items[8].Attributes["style"] = "background: #430000; display:none; ";
                    break;
            }


        }

        protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBudgetData();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private string GetLastclId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();

            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "GETCLIENTSINFO", "",
                                  "%%", userid, "", "", "", "", "");


            string lastid = ds1.Tables[1].Rows[0]["sircode"].ToString();
            return (lastid);


        }
        private void GetBudgetData()
        {
            //04. 05. Budget- General 





            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan' > </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class='lbldata2'>" + "Project Link" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pen-square fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class=lbldata2>" + "Project Information" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pen-square fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class=lbldata2>" + "Pre Construction" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class=lbldata2>" + "Construction Budget" + "</span>";

            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class=lbldata2>" + "General Budget" + "</span>";
            this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class=lbldata2>" + "Planning" + "</span>";

            this.RadioButtonList1.Items[6].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=''>" + "</span>" + "<span class='lbldata2'>" + "Construction Level" + "</span>";

            //this.RadioButtonList1.Items[7].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class=lbldata2>" + "Work Execution" + "</span>";
            //this.RadioButtonList1.Items[8].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class=lbldata2>" + "Constr. Progress" + "</span>";




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            DateTime date = Convert.ToDateTime(this.txtdate.Text);
            string ptype = this.ddlProjectType.SelectedValue.ToString()=="00000"? "%": this.ddlProjectType.SelectedValue.ToString()+"%";

            DataSet dskpi = KpiData.GetTransInfo(comcod, "SP_REPORT_BUDGET_INTERFACE", "RPTBUDGETINTERFACE", date.ToString("dd-MMM-yyyy"), "", ptype, "", "", "", "", "");

            DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "PRINFCODELISTPLAN", "", "", "", "", "", "", "", "");
            if (dskpi == null || ds1 == null)
                return;
            DataTable dta = dskpi.Tables[1];

            this.ttlcustomer.Text = Convert.ToDouble(dta.Rows[0]["ttlprj"]).ToString("#,##0;(#,##0); ");



            DataTable dt = new DataTable();
            DataView dv = new DataView();
            DataTable dt0 = new DataTable();

            dt0 = ((DataTable)dskpi.Tables[0]).Copy();

            dv = dt0.DefaultView;
            dt = dv.ToTable();
            //dv = dt0.DefaultView;
            //dv.Sort = "actcode asc";
            //dt = dv.ToTable();

            ViewState["ALLBudugetData"] = dt;
            ViewState["tblprjlink"] = ds1.Tables[0]; ;
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["ALLBudugetData"];
            DataTable dt1 = (DataTable)ViewState["tblprjlink"];
            this.gvCodeBook.DataSource = dt1;//HiddenSameData(dt);
            this.gvCodeBook.DataBind();
            this.gvPrjInfo.DataSource = HiddenSameData(dt);
            this.gvPrjInfo.DataBind();
            if (dt.Rows.Count == 0)
                return;
            string viewtype = this.RadioButtonList1.SelectedValue.ToString();


            if (viewtype == "1")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = false;
                this.gvPrjInfo.Columns[8].Visible = false;
                this.gvPrjInfo.Columns[9].Visible = false;

                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = false;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;
            }
            if (viewtype == "2")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = false;
                this.gvPrjInfo.Columns[8].Visible = false;
                this.gvPrjInfo.Columns[9].Visible = false;
                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = false;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;

            }
            else if (viewtype == "3")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = true;
                this.gvPrjInfo.Columns[8].Visible = true;
                this.gvPrjInfo.Columns[9].Visible = false;

                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = false;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;



            }
            else if (viewtype == "4")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = true;
                this.gvPrjInfo.Columns[8].Visible = true;
                this.gvPrjInfo.Columns[9].Visible = true;

                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = false;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;
            }
            else if (viewtype == "5")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = true;
                this.gvPrjInfo.Columns[8].Visible = true;
                this.gvPrjInfo.Columns[9].Visible = true;


                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = true;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;
            }
            else if (viewtype == "6")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = false;
                this.gvPrjInfo.Columns[8].Visible = false;
                this.gvPrjInfo.Columns[9].Visible = false;


                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = false;
                //this.gvPrjInfo.Columns[13].Visible = false;
                //this.gvPrjInfo.Columns[14].Visible = false;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = true;


            }
            else if (viewtype == "7")
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = true;
                this.gvPrjInfo.Columns[8].Visible = true;
                this.gvPrjInfo.Columns[9].Visible = true;



                this.gvPrjInfo.Columns[10].Visible = false;
                this.gvPrjInfo.Columns[11].Visible = false;
                //this.gvPrjInfo.Columns[12].Visible = true;
                //this.gvPrjInfo.Columns[13].Visible = true;
                //this.gvPrjInfo.Columns[14].Visible = true;
                //this.gvPrjInfo.Columns[15].Visible = false;
                //this.gvPrjInfo.Columns[16].Visible = false;
                //this.gvPrjInfo.Columns[17].Visible = false;
                //this.gvPrjInfo.Columns[18].Visible = false;



            }
            else
            {
                this.gvPrjInfo.Columns[0].Visible = true;
                this.gvPrjInfo.Columns[1].Visible = true;
                this.gvPrjInfo.Columns[2].Visible = true;
                this.gvPrjInfo.Columns[3].Visible = true;
                this.gvPrjInfo.Columns[4].Visible = true;
                this.gvPrjInfo.Columns[5].Visible = true;
                this.gvPrjInfo.Columns[6].Visible = true;
                this.gvPrjInfo.Columns[7].Visible = true;
                this.gvPrjInfo.Columns[8].Visible = true;
                this.gvPrjInfo.Columns[9].Visible = true;

                this.gvPrjInfo.Columns[10].Visible = true;
                this.gvPrjInfo.Columns[11].Visible = true;
                //this.gvPrjInfo.Columns[12].Visible = true;
                //this.gvPrjInfo.Columns[13].Visible = true;
                //this.gvPrjInfo.Columns[14].Visible = true;
                //this.gvPrjInfo.Columns[15].Visible = true;
                //this.gvPrjInfo.Columns[16].Visible = true;
                //this.gvPrjInfo.Columns[17].Visible = true;
                //this.gvPrjInfo.Columns[18].Visible = false;
            }


        }

        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                HyperLink custLink = (HyperLink)e.Row.FindControl("hyplProjectS");
                HyperLink lnkapp = (HyperLink)e.Row.FindControl("lnkapp");
                HyperLink lnkBgdLock = (HyperLink)e.Row.FindControl("lnkBgdLock");
                // custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";
                int index = this.RadioButtonList1.SelectedIndex;
                switch (index)
                {



                    case 1:
                        custLink.NavigateUrl = "~/F_04_Bgd/PrjInformation?Type=Report&prjcode=" + pactcode;
                        break;
                    case 2:
                        custLink.NavigateUrl = "~/F_08_PPlan/PrjCompFlowchart?Type=Report&prjcode=" + pactcode;
                        break;
                    case 3:
                        lnkapp.Visible = true;
                        lnkBgdLock.Visible = true;
                        custLink.NavigateUrl = "~/F_04_Bgd/BgdPrjAna?InputType=BgdMain&prjcode=" + pactcode + "&sircode=";
                        lnkapp.NavigateUrl = "~/F_04_Bgd/AddBudget?Type=Mgt&prjcode=" + pactcode;
                        lnkBgdLock.NavigateUrl = "~/F_04_Bgd/BgdPrjAna?InputType=BgdSub&prjcode=" + pactcode + "&sircode=";
                        break;
                    case 4:
                        custLink.NavigateUrl = "~/F_04_Bgd/BgdMaster?InputType=BgdMain&prjcode=" + pactcode;

                        break;
                    case 5:
                        custLink.NavigateUrl = "~/F_08_PPlan/ProTargetTimeBasis?Type=GrpWise&prjcode=" + pactcode + "&sircode=&flrcod=";
                        break;

                    case 6:
                        custLink.NavigateUrl = "~/F_04_Bgd/BgdLevelRate?Type=Level&prjcode=" + pactcode;
                        break;
                    //case 7:
                    //    custLink.NavigateUrl = "~/F_09_PImp/PurIssueEntry?Type=Report&prjcode=" + pactcode;
                    //    break;
                    //case 8:
                    //    custLink.NavigateUrl = "~/F_32_Mis/RptConstruProgressSum?Type=Report&comcod=" + comcod;
                    //    break;

                    default:
                        break;
                }

            }
        }

        protected void gvCodeBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvCodeBook.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            //

            string rescode1 = ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtgvInfCod1")).Text.Trim();
            string rescode = rescode1.Substring(0, 2) + rescode1.Substring(3, 2) + rescode1.Substring(6, 3) + rescode1.Substring(10, 2) + rescode1.Substring(13);
            int rowindex = (gvCodeBook.PageSize) * (this.gvCodeBook.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            // string teamcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["catcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlProName");
            DropDownList ddlteam = (DropDownList)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("ddlteam");
            Panel pnlteam = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("pnlTeam");

            Panel pnl02 = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("Panel2");
            //if ((this.Request.QueryString["BookName"] == "Project") && (ASTUtility.Right(rescode, 3) != "000"))
            //{
            ViewState["gindex"] = e.NewEditIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SearchProject = "%"; //+ ((TextBox)gvCodeBook.Rows[e.NewEditIndex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT_FS", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = actcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();
            pnl02.Visible = true;
            //}
            //else
            //{
            //    pnl02.Visible = false;
            //    ddl2.Items.Clear();

            //}


        }
        protected void gvCodeBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            //((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //string sQryParm = ((Label)this.Master.FindControl("lblTitle")).Text;
            string mInfGrp = "PRJ";
            string mInfCode = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfCod1")).Text.Trim().Replace("-", "");
            string mInfDesc = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDesc")).Text.Trim();//.ToUpper();
            string mInfDes2 = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvInfDes2")).Text.Trim();//.ToUpper();
            string mUnitFPS = ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvUnitFPS")).Text.Trim();//.ToUpper();
            string mStdQtyF = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvStdQtyF")).Text.Trim()).ToString();
            string mConsArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvcarea")).Text.Trim()).ToString();
            string mSalArea = Convert.ToDouble("0" + ((TextBox)this.gvCodeBook.Rows[e.RowIndex].FindControl("txtgvsarea")).Text.Trim()).ToString();
            string Type = "Project";

            string mProCode = "";

            if (Type == "Project")
            {
                mProCode = ((DropDownList)this.gvCodeBook.Rows[e.RowIndex].FindControl("ddlProName")).Text.Trim();

                if (mProCode != "")
                {
                    DataSet ds2 = KpiData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "CHECKEDDUPACCCODE_FS", mProCode, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("infcod <>'" + mInfCode + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Account Code";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                            //this.ddlPrevReqList.Items.Clear();
                            return;
                        }
                    }

                }
            }









            bool result = KpiData.UpdateTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "PRINFCODEUPDATE",
                          mInfGrp, mInfCode, mInfDesc, mInfDes2, mUnitFPS, mStdQtyF, mConsArea, mSalArea, mProCode,
                          "", "", "", "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.gvCodeBook.EditIndex = -1;
            // this.ShowCodeList(mInfGrp);

            //this.gvCodeBookDataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Update CodeBook";
            //    string eventdesc2 = mInfCode;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }

        protected void gvCodeBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvCodeBook.EditIndex = -1;
            this.Data_Bind();
        }

        protected void gvCodeBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCodeBook.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            string SearchProject = "%";
            DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_ENTRY_LP_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            //ddl2.SelectedValue = actcode;

        }

       
    }
}