
using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using RealERPLIB;
using RealEntity;
using RealERPWEB.Service;

namespace RealERPWEB
{
    public partial class StepofOperationNew : System.Web.UI.Page
    {

        UserService objuserser = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //////if (IsPostBack)
                //////    return;
                ////string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                ////string ModuleID2 = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();
                ////string comcod = ASTUtility.Left(((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim(), 1);
                ////// this.ddlModuleName.Visible = (ModuleID == "AA");
                ////if (ModuleID == "AA" && comcod == "4")
                ////    this.ddlModuleName.SelectedValue = "81";

                ////if (ModuleID == "AA" && comcod == "8")
                ////    this.ddlModuleName.SelectedValue = "36";
                ////if (ModuleID2 != "AA")
                ////    this.ddlModuleName.SelectedValue = ModuleID2;


                ////this.GetCompanyName();
                ////this.ddlModuleName_SelectedIndexChanged(null, null);
                //nahid

                //DataSet ds=(DataSet)Session["tblusrlog"];
                ((Label)this.Master.FindControl("lblTitle")).Text = "STEPS OF OPERATIONS";
                string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                string qmoduleid = this.Request.QueryString["moduleid"] ?? "";
                string ModuleID2 = (qmoduleid.Length > 0) ? qmoduleid : ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();

                //string ModuleID2 = (this.Request.QueryString["Module"].ToString().Length > 0) ? this.Request.QueryString["Module"] : ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();

                if(qmoduleid=="05")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Annual Business Plan";
                    this.Master.Page.Title = "Annual Business Plan";
                }
                else if(qmoduleid == "01")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Land Feasibility";
                    this.Master.Page.Title = "Land Feasibility";
                }
                else if (qmoduleid == "02")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Feasibility Module";
                    this.Master.Page.Title = "Feasibility Module";
                }
                else if (qmoduleid == "04")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Budgetary Control";
                    this.Master.Page.Title = "Budgetary Control";
                }
                else if (qmoduleid == "08")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Project Planning";
                    this.Master.Page.Title = "Project Planning";
                }
                else if (qmoduleid == "09")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Project Implementation";
                    this.Master.Page.Title = "Project Implementation";
                }
                else if (qmoduleid == "13")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Central Warehouse";
                    this.Master.Page.Title = "Central Warehouse";
                }
                else if (qmoduleid == "12")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Inventory Control";
                    this.Master.Page.Title = "Inventory Control";
                }
                else if (qmoduleid == "14")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Procurement";
                    this.Master.Page.Title = "Procurement";
                }
                else if (qmoduleid == "21")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Relation Mgt(CRM)";
                    this.Master.Page.Title = "Customer Relation Mgt(CRM)";
                }
                else if (qmoduleid == "16")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Billing Management";
                    this.Master.Page.Title = "Billing Management";
                }
                else if (qmoduleid == "22")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Sales";
                    this.Master.Page.Title = "Sales";
                }
                else if (qmoduleid == "37")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Landowner's Managment";
                    this.Master.Page.Title = "Landowner's Managment";
                }
                else if (qmoduleid == "23")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Credit Realization/Recovery";
                    this.Master.Page.Title = "Credit Realization/Recovery";
                }
                else if (qmoduleid == "24")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Care";
                    this.Master.Page.Title = "Customer Care";
                }
                else if (qmoduleid == "28")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Marketing Procurement";
                    this.Master.Page.Title = "Marketing Procurement";
                }
                else if (qmoduleid == "29")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Assets";
                    this.Master.Page.Title = "Fixed Assets";
                }
                else if (qmoduleid == "17")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Accounts & Finance";
                    this.Master.Page.Title = "Accounts & Finance";
                }
                else if (qmoduleid == "33")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Documentation";
                    this.Master.Page.Title = "Documentation";
                }
                else if (qmoduleid == "35")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Control Panel";
                    this.Master.Page.Title = "Control Panel";
                }
                else if (qmoduleid == "32")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "MIS Module";
                    this.Master.Page.Title = "MIS Module";
                }
                else if (qmoduleid == "30")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Complain Management";
                    this.Master.Page.Title = "Complain Management";
                }
                else if (qmoduleid == "81")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "HRM";
                    this.Master.Page.Title = "HRM";
                }
                else if (qmoduleid == "38")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Artificial Intelligence(AI)";
                    this.Master.Page.Title = "Artificial Intelligence(AI)";
                }
                else if (qmoduleid == "75")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "KPI";
                    this.Master.Page.Title = "KPI";
                }

                string comcod = ASTUtility.Left(((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim(), 1);

                this.GetCompanyName();
                DataTable dt = (DataTable)ViewState["tblmoduleName"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "usrper = '" + "true" + "'";
                DataTable dt1 = dv.ToTable();
                string TopModule = (qmoduleid.Length > 0) ? qmoduleid : dt1.Rows[0]["moduleid"].ToString();
                //string TopModule = (this.Request.QueryString["Module"].ToString().Length > 0) ? this.Request.QueryString["Module"] : dt1.Rows[0]["moduleid"].ToString();

                if (ModuleID2 == "AA")
                    this.ddlModuleName.SelectedValue = TopModule;

                else if (ModuleID2 != "AA")
                    this.ddlModuleName.SelectedValue = ModuleID2;


                this.ddlModuleName_SelectedIndexChanged(null, null);




                // string SModuleID = dt1.Rows[0]["moduleid"].ToString();

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //   ((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            // ((Panel)this.Master.FindControl("pnlPrint")).Visible = false;

            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;

            //  ((Panel)this.Master.FindControl("pnlTitle")).

        }


        private void GetCompanyName()
        {

            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.ddlCompanyName.DataTextField = "comsnam";
            this.ddlCompanyName.DataValueField = "comcod";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName.SelectedValue = this.GetCompCode();
            this.GetModulename();
            //this.ModuleVisible();
            this.MasComNameAndAdd();


        }
        private void GetModulename()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = hst["usrid"].ToString();
            // string usrperm = "1";
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("moduleid<>'AA' AND usrper='True'");

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = dv.ToTable();
            this.ddlModuleName.DataBind();


            ViewState["tblmoduleName"] = ds1.Tables[0];

        }
        private void MasComNameAndAdd()
        {
            //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "";
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            DataTable dt = ((DataTable)Session["tbllog1"]);
            dt.Rows[0]["comcod"] = comcod;
            Session["tbllog1"] = dt;
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = this.ddlCompanyName.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                                                                                                                  //((Label)this.Master.FindControl("lbladd")).Text = (dr[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? dr[0]["comadd"].ToString().Substring(6) : dr[0]["comadd"].ToString();
                                                                                                                  //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "~/Image/" + "LOGO" + comcod + ".PNG";//"~/Image/LOGO1101.PNG";//


            //string logo = "~/Image/" + "LOGO" + comcod + ".PNG";
            //this.MasComNameAndAdd();
        }
        private void GetUserPermission()
        {




            ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompanyName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(txtuserpass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = this.ddlModuleName.SelectedValue.ToString();
            string modulename = this.ddlModuleName.SelectedItem.Text.Trim();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;
            string Comcode = this.ddlCompanyName.SelectedValue.ToString();
            string ComName = this.ddlCompanyName.SelectedItem.ToString();

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = Comcode;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;


            //string Url1 = "";

            //string userrole = ds5.Tables[0].Rows[0]["userrole"].ToString();

            //string masterurl = (ds5.Tables[4].Rows.Count == 0) ? "" : ds5.Tables[4].Rows[0]["url"].ToString();

            //if (userrole == "2")
            //{
            //    Url1 = "AllGraph.aspx";
            //}
            //else
            //{
            //    if (masterurl != "")
            //    {
            //        Url1 = ds5.Tables[4].Rows[0]["url"].ToString();
            //    }
            //    else
            //    {


            //        if (comcod == "3333")
            //        {
            //            Url1 = "DeafultMenu.aspx?Type=3334";
            //        }

            //        else if (comcod == "3335")
            //        {
            //            Url1 = "MyDashboard.aspx?Type=5020";
            //        }
            //        else if (comcod == "3345")
            //        {
            //            Url1 = "MyDashboard.aspx?Type=5500";
            //        }
            //        else if (comcod == "3109")
            //        {
            //            Url1 = "MyDashboard.aspx?Type=5019";
            //        }
            //        else if (comcod == "3347")
            //        {
            //            Url1 = "HrWinMenu.aspx";
            //        }


            //        else if (comcod.Substring(0, 1) == "8")
            //        {
            //            Url1 = "DeafultMenu.aspx?Type=9000";
            //        }
            //        else if (comcod.Substring(0, 1) == "1")
            //        {
            //            Url1 = "DashboardAll.aspx?Type=7000";
            //        }
            //        else
            //        {
            //            Url1 = "MyDashboard.aspx?Type=";


            //            string UComcode = ASTUtility.Left(Comcode, 1);
            //            if (UComcode == "3")
            //            {
            //                Url1 += "5000";
            //            }
            //            else if (UComcode == "4")
            //            {
            //                Url1 += "7000";
            //            }
            //            else
            //            {
            //                Url1 += "5000";

            //            }


            //        }

            //    }




            //}

            //Response.Redirect(Url1);





            //string Url1 = "MyDashboard.aspx?Type=";

            //string UComcode = ASTUtility.Left(Comcode, 1);
            //if (UComcode == "3")
            //{
            //    Url1 += "8000";
            //}
            //else if (UComcode == "9")
            //{
            //    Url1 = "";
            //    Url1 = "DeafultMenu.aspx?Type=9000";
            //}
            //else
            //{
            //    Url1 += "5000";

            //}

            //Response.Redirect(Url1);  



        }
        private void ModuleVisible()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string CompName = this.ddlCompanyName.SelectedValue.ToString();
            string CompType = ASTUtility.Left(CompName, 1);
            string commod = hst["commod"].ToString();
            DataTable dt = (DataTable)ViewState["tblmoduleName"];

            this.ddlModuleName.Items[0].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[0]["usrper"]));
            this.ddlModuleName.Items[1].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[1]["usrper"]));
            this.ddlModuleName.Items[2].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[2]["usrper"]));
            this.ddlModuleName.Items[3].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[3]["usrper"]));
            this.ddlModuleName.Items[4].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[4]["usrper"]));
            this.ddlModuleName.Items[5].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[5]["usrper"]));

            this.ddlModuleName.Items[6].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[6]["usrper"]));
            this.ddlModuleName.Items[7].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[7]["usrper"]));

            this.ddlModuleName.Items[8].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[8]["usrper"]));

            this.ddlModuleName.Items[9].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[9]["usrper"]));
            this.ddlModuleName.Items[10].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[10]["usrper"]));
            this.ddlModuleName.Items[11].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[11]["usrper"]));
            this.ddlModuleName.Items[12].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[12]["usrper"]));
            this.ddlModuleName.Items[13].Enabled = (CompType == "1" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[13]["usrper"]));

            this.ddlModuleName.Items[14].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[14]["usrper"]));
            this.ddlModuleName.Items[15].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[15]["usrper"]));
            this.ddlModuleName.Items[16].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[16]["usrper"]));
            this.ddlModuleName.Items[17].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[17]["usrper"]));
            this.ddlModuleName.Items[18].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[18]["usrper"]));
            this.ddlModuleName.Items[19].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[19]["usrper"]));
            this.ddlModuleName.Items[20].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[20]["usrper"]));


            this.ddlModuleName.Items[21].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[21]["usrper"]));
            this.ddlModuleName.Items[22].Enabled = (CompType == "0" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[22]["usrper"]));
            this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[23]["usrper"]));
            this.ddlModuleName.Items[24].Enabled = (CompType == "0" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[24]["usrper"]));
            this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[25]["usrper"]));


            this.ddlModuleName.Items[26].Enabled = (CompType == "3" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[26]["usrper"]));
            this.ddlModuleName.Items[27].Enabled = (CompType == "1" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[27]["usrper"]));
            this.ddlModuleName.Items[28].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[28]["usrper"]));


            this.ddlModuleName.Items[29].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[29]["usrper"]));
            this.ddlModuleName.Items[30].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[30]["usrper"]));
            //this.ddlModuleName.Items[31].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[31]["usrper"]));





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string CompName = this.ddlCompanyName.SelectedValue.ToString();
            //string CompType = ASTUtility.Left(CompName, 1);
            //string commod = hst["commod"].ToString();
            //DataTable dt = (DataTable)ViewState["tblmoduleName"];

            //this.ddlModuleName.Items[0].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[0]["usrper"])) && (Convert.ToBoolean(dt.Rows[0]["flag"])));

            //this.ddlModuleName.Items[1].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[1]["usrper"])) && (Convert.ToBoolean(dt.Rows[1]["flag"])));

            //this.ddlModuleName.Items[2].Enabled = ((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[2]["usrper"])) && (Convert.ToBoolean(dt.Rows[2]["flag"]));
            //this.ddlModuleName.Items[3].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[3]["usrper"])) && (Convert.ToBoolean(dt.Rows[3]["flag"])));
            //this.ddlModuleName.Items[4].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[4]["usrper"])) && (Convert.ToBoolean(dt.Rows[4]["flag"])));
            //this.ddlModuleName.Items[5].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[5]["usrper"]))) && (Convert.ToBoolean(dt.Rows[5]["flag"]));

            //this.ddlModuleName.Items[6].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[6]["usrper"])) && (Convert.ToBoolean(dt.Rows[6]["flag"])));

            //this.ddlModuleName.Items[7].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[7]["usrper"])) && (Convert.ToBoolean(dt.Rows[7]["flag"])));
            //this.ddlModuleName.Items[8].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[8]["usrper"])) && (Convert.ToBoolean(dt.Rows[8]["flag"])));

            //this.ddlModuleName.Items[9].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[9]["usrper"])) && (Convert.ToBoolean(dt.Rows[9]["flag"])));


            //this.ddlModuleName.Items[10].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[10]["usrper"])) && (Convert.ToBoolean(dt.Rows[10]["flag"])));
            //this.ddlModuleName.Items[11].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[11]["usrper"])) && (Convert.ToBoolean(dt.Rows[11]["flag"])));
            //this.ddlModuleName.Items[12].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[12]["usrper"])) && (Convert.ToBoolean(dt.Rows[12]["flag"])));
            //this.ddlModuleName.Items[13].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[13]["usrper"])) && (Convert.ToBoolean(dt.Rows[13]["flag"])));

            //this.ddlModuleName.Items[14].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[14]["usrper"])) && (Convert.ToBoolean(dt.Rows[14]["flag"])));
            //this.ddlModuleName.Items[15].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[15]["usrper"])) && (Convert.ToBoolean(dt.Rows[15]["flag"])));
            //this.ddlModuleName.Items[16].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[16]["usrper"])) && (Convert.ToBoolean(dt.Rows[16]["flag"])));

            //////
            //this.ddlModuleName.Items[17].Enabled = (((CompType == "5") && (commod == "1"))  && (Convert.ToBoolean(dt.Rows[17]["usrper"])) && (Convert.ToBoolean(dt.Rows[17]["flag"])));
            //this.ddlModuleName.Items[18].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[18]["usrper"])) && (Convert.ToBoolean(dt.Rows[18]["flag"])));
            //this.ddlModuleName.Items[19].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[19]["usrper"])) && (Convert.ToBoolean(dt.Rows[19]["flag"])));
            //this.ddlModuleName.Items[20].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[20]["usrper"])) && (Convert.ToBoolean(dt.Rows[20]["flag"])));
            //this.ddlModuleName.Items[21].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[21]["usrper"])) && (Convert.ToBoolean(dt.Rows[21]["flag"])));
            //this.ddlModuleName.Items[22].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[22]["usrper"])) && (Convert.ToBoolean(dt.Rows[22]["flag"])));
            //this.ddlModuleName.Items[23].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[23]["usrper"])) && (Convert.ToBoolean(dt.Rows[23]["flag"])));
            //this.ddlModuleName.Items[24].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[24]["usrper"])) && (Convert.ToBoolean(dt.Rows[24]["flag"])));
            //this.ddlModuleName.Items[25].Enabled = (((CompType == "5") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[25]["usrper"])) && (Convert.ToBoolean(dt.Rows[25]["flag"])));
            //this.ddlModuleName.Items[26].Enabled = true;

        }


        //private void ShowUserPerModule()
        //{
        //    List<EClassComModule> lst = objuserser.GetComModule();
        //    Session["tblmodule"] = lst;
        //    this.GetMenuGenerate();
        //}


        //private void GetMenuGenerate()
        //{


        //    List<EClassComModule> lst = (List<EClassComModule>)Session["tblmodule"];
        //    MenuItem FirstParentItem = null;

        //    ((Menu)this.Master.FindControl("Menu1")).Items.Clear();

        //    foreach (EClassComModule lst1 in lst)
        //    {
        //        FirstParentItem = new MenuItem(lst1.modulenam);
        //        FirstParentItem.Value = lst1.moduleid;
        //        // FirstParentItem.Selectable = slct;
        //        FirstParentItem.NavigateUrl = lst1.url;
        //        ((Menu)this.Master.FindControl("Menu1")).Items.Add(FirstParentItem);

        //    }




        //}

        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ModuleID = this.ddlModuleName.SelectedValue.ToString().Trim();
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            this.txtmodid.Text = this.ddlModuleName.SelectedValue.ToString();
            this.modulenam.Text = this.ddlModuleName.SelectedItem.Text;
            ds1.Tables[0].Rows[0]["ModuleID2"] = ModuleID;
            ds1.Tables[0].Rows[0]["modulename"] = this.ddlModuleName.SelectedItem.Text;
            Session["tblusrlog"] = ds1;

        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {


            //System.Web.HttpContext.Current.Session.Remove("tblMainCenter");
            //System.Web.HttpContext.Current.Session.Remove("tblMainTax");
            //System.Web.HttpContext.Current.Session.Remove("tblMainTeam");
            //System.Web.HttpContext.Current.Session.Remove("tblMainCustomer");

            //this.GetUserPermission();
            //this.GetModulename();
            //this.ModuleVisible();

            //DataTable dt = (DataTable)ViewState["tblmoduleName"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "usrper = '" + "true" + "'";
            //DataTable dt1 = dv.ToTable();
            //string TopModule = dt1.Rows[0]["moduleid"].ToString();
            //this.ddlModuleName.SelectedValue = TopModule;
            //this.MasComNameAndAdd();
            //this.ddlModuleName_SelectedIndexChanged(null, null);



            this.GetUserPermission();
            this.GetModulename();
            //this.ModuleVisible();
            //  this.ShowUserPerModule();

            DataTable dt = (DataTable)ViewState["tblmoduleName"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "usrper = '" + "true" + "'";
            DataTable dt1 = dv.ToTable();
            string TopModule = dt1.Rows[0]["moduleid"].ToString();
            this.ddlModuleName.SelectedValue = TopModule;
            this.MasComNameAndAdd();
            this.ddlModuleName_SelectedIndexChanged(null, null);
            //string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;

            //string Url1 = path + "/Dashboard.aspx";
            //Response.Redirect(Url1);

            ///this.LogoBar.Attributes.Add("href", path + "/Dashboard.aspx");
        }

    }
}