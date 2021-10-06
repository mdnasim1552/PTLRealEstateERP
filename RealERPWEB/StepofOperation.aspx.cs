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
using RealEntity;
using RealERPLIB;
using RealERPWEB.Service;

namespace RealERPWEB
{
    public partial class StepofOperation : System.Web.UI.Page
    {
        UserService objuserser = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (IsPostBack)
                //    return;
                // string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
                string ModuleID2;

                string comcod = this.GetCompCode();

                if (comcod == "3333" || comcod == "3347")
                {
                    //// string moduleid = this.Request.QueryString["moduleid"].ToString();
                    // DataSet ds1 = (DataSet)Session["tblusrlog"];
                    // ds1.Tables[0].Rows[0]["moduleid"] = moduleid;
                    // ds1.Tables[0].Rows[0]["moduleid2"] = moduleid;
                    // Session["tblusrlog"] = ds1;

                }

                else
                {
                    if (this.Request.QueryString["moduleid"].Length > 0)
                    {
                        string moduleid = this.Request.QueryString["moduleid"].ToString();
                        DataSet ds1 = (DataSet)Session["tblusrlog"];
                        ds1.Tables[0].Rows[0]["moduleid"] = moduleid;
                        ds1.Tables[0].Rows[0]["moduleid2"] = moduleid;
                        Session["tblusrlog"] = ds1;
                    }


                }



                ModuleID2 = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();


                //string comcod = ASTUtility.Left(((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["comcod"].ToString().Trim(), 1);
                //// this.ddlModuleName.Visible = (ModuleID == "AA");
                //if (ModuleID == "AA" && comcod == "4")
                //    this.ddlModuleName.SelectedValue = "81";

                //if (ModuleID == "AA" && comcod == "9")
                //    this.ddlModuleName.SelectedValue = "41";
                //if (ModuleID == "AA" && comcod == "8")
                //    this.ddlModuleName.SelectedValue = "46";

                //if (ModuleID == "AA" && comcod == "1")
                //    this.ddlModuleName.SelectedValue = "07";
                //if (ModuleID2 != "AA")
                //    this.ddlModuleName.SelectedValue = ModuleID2;


                this.GetCompanyName();
                DataTable dt = (DataTable)ViewState["tblmoduleName"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "usrper = '" + "true" + "'";
                DataTable dt1 = dv.ToTable();
                string SModuleID = dt1.Rows[0]["moduleid"].ToString();

                if (ModuleID2 == "AA")
                    this.ddlModuleName.SelectedValue = SModuleID;

                else if (ModuleID2 != "AA")
                    this.ddlModuleName.SelectedValue = ModuleID2;

                this.ddlModuleName_SelectedIndexChanged(null, null);


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
            ((Panel)this.Master.FindControl("pnlTitle")).Visible = false;
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;

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

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            ProcessAccess ulogin = new ProcessAccess();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", userid, "", "", "", "", "", "", "", "");
            DataView dv;
            dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("moduleid<>'AA'");

            //string id = this.Request.QueryString["moduleid"];
            //if (this.Request.QueryString["moduleid"].Length > 0)
            //{
            //    dv = ds1.Tables[0].DefaultView;
            //    dv.RowFilter = ("moduleid='"+id+"'");
            //}

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = dv.ToTable();
            this.ddlModuleName.DataBind();

            ViewState["tblmoduleName"] = ds1.Tables[0];


        }
        private void MasComNameAndAdd()
        {
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = this.ddlCompanyName.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            ((Label)this.Master.FindControl("lbladd")).Text = (dr[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? dr[0]["comadd"].ToString().Substring(6) : dr[0]["comadd"].ToString();
            //((Image)this.Master.FindControl("Image1")).ImageUrl = "~/Image/" + "LOGO" + comcod + ".PNG";

        }
        private void GetUserPermission()
        {
            ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompanyName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            string comcod = this.ddlCompanyName.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = this.ddlModuleName.SelectedValue.ToString();
            string modulename = this.ddlModuleName.SelectedItem.Text.Trim();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, "", "", "", "", "", "", "");
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


            this.ddlModuleName.Items[21].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[21]["usrper"]));
            this.ddlModuleName.Items[22].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[22]["usrper"]));
            this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[23]["usrper"]));
            this.ddlModuleName.Items[24].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[24]["usrper"]));
            this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[25]["usrper"]));


            this.ddlModuleName.Items[26].Enabled = (CompType == "3" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[26]["usrper"]));
            this.ddlModuleName.Items[27].Enabled = (CompType == "1" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[27]["usrper"]));
            this.ddlModuleName.Items[28].Enabled = (CompType == "2" && (commod == "1")) && (Convert.ToBoolean(dt.Rows[28]["usrper"]));


            this.ddlModuleName.Items[29].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[29]["usrper"]));
            this.ddlModuleName.Items[30].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[30]["usrper"]));
            this.ddlModuleName.Items[31].Enabled = (CompType == "8") && (Convert.ToBoolean(dt.Rows[31]["usrper"]));





            //  this.ddlModuleName.Items[30].Enabled = ((CompType == "7") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[30]["usrper"]));



            //this.ddlModuleName.Items[0].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[0]["usrper"]));

            //this.ddlModuleName.Items[1].Enabled = ((CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[1]["usrper"]));
            //this.ddlModuleName.Items[2].Enabled = ((CompType == "2") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[2]["usrper"]));
            //this.ddlModuleName.Items[3].Enabled = ((CompType == "1") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[3]["usrper"]));
            //this.ddlModuleName.Items[4].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[4]["usrper"]));
            //this.ddlModuleName.Items[5].Enabled = ((CompType == "2") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[5]["usrper"]));
            //this.ddlModuleName.Items[6].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[6]["usrper"]));
            //this.ddlModuleName.Items[7].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[7]["usrper"]));
            //this.ddlModuleName.Items[8].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[8]["usrper"]));
            //this.ddlModuleName.Items[9].Enabled = ((CompType == "2") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[9]["usrper"]));

            //this.ddlModuleName.Items[10].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[10]["usrper"]));
            //this.ddlModuleName.Items[11].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[11]["usrper"]));
            //this.ddlModuleName.Items[12].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[12]["usrper"]));
            //this.ddlModuleName.Items[13].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[13]["usrper"]));



            ////this.ListModulename.Items[13].Enabled = (CompType == "1" || CompType == "2" || CompType == "3");

            //this.ddlModuleName.Items[14].Enabled = ((CompType == "1" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[14]["usrper"]));
            //this.ddlModuleName.Items[15].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[15]["usrper"]));
            //this.ddlModuleName.Items[16].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[16]["usrper"]));
            //// this.ddlModuleName.Items[18].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[18]["usrper"]));


            //this.ddlModuleName.Items[17].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[17]["usrper"]));
            //this.ddlModuleName.Items[18].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[18]["usrper"]));
            //this.ddlModuleName.Items[19].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[19]["usrper"]));
            //this.ddlModuleName.Items[20].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[20]["usrper"]));
            //this.ddlModuleName.Items[21].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[21]["usrper"]));
            //this.ddlModuleName.Items[22].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[22]["usrper"]));
            //this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[23]["usrper"]));
            //this.ddlModuleName.Items[24].Enabled = false;
            //this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[25]["usrper"]));
            //this.ddlModuleName.Items[26].Enabled = ((CompType == "2" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[26]["usrper"]));
            //this.ddlModuleName.Items[27].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[27]["usrper"]));
            //// this.ddlModuleName.Items[30].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[30]["usrper"]));
            //this.ddlModuleName.Items[28].Enabled = ((CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[28]["usrper"]));
            ////this.ddlModuleName.Items[32].Enabled = (CompType == "1");
            //this.ddlModuleName.Items[29].Enabled = ((CompType == "2" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[29]["usrper"]));
            //this.ddlModuleName.Items[30].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[30]["usrper"]));
            //this.ddlModuleName.Items[31].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")) || (CompType == "7" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[31]["usrper"]));
            //this.ddlModuleName.Items[32].Enabled = ((CompType == "2" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[32]["usrper"]));

            //this.ddlModuleName.Items[33].Enabled = ((CompType == "9") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[33]["usrper"]));
            //this.ddlModuleName.Items[34].Enabled = ((CompType == "9") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[34]["usrper"]));
            //this.ddlModuleName.Items[35].Enabled = ((CompType == "9") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[35]["usrper"]));
            //this.ddlModuleName.Items[36].Enabled = ((CompType == "8") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[36]["usrper"]));
            //this.ddlModuleName.Items[37].Enabled = ((CompType == "8") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[37]["usrper"]));
            //this.ddlModuleName.Items[38].Enabled = ((CompType == "7") && (commod == "1")) && (Convert.ToBoolean(dt.Rows[38]["usrper"]));

            //this.ddlModuleName.Items[39].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) && (Convert.ToBoolean(dt.Rows[39]["usrper"]));
            //this.ddlModuleName.Items[40].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) && (Convert.ToBoolean(dt.Rows[40]["usrper"]));
            //this.ddlModuleName.Items[41].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) && (Convert.ToBoolean(dt.Rows[41]["usrper"]));
            //this.ddlModuleName.Items[42].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) && (Convert.ToBoolean(dt.Rows[42]["usrper"]));
            //this.ddlModuleName.Items[43].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) && (Convert.ToBoolean(dt.Rows[43]["usrper"]));

            //this.ddlModuleName.Items[44].Enabled = ((CompType == "1" && (commod == "4")) || (CompType == "2" && (commod == "4")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[44]["usrper"]));








        }

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
        private void ShowUserPerModule()
        {
            List<EClassComModule> lst = objuserser.GetComModule();
            Session["tblmodule"] = lst;
            this.GetMenuGenerate();
        }

        private void GetMenuGenerate()
        {


            List<EClassComModule> lst = (List<EClassComModule>)Session["tblmodule"];
            MenuItem FirstParentItem = null;

            ((Menu)this.Master.FindControl("Menu1")).Items.Clear();

            foreach (EClassComModule lst1 in lst)
            {
                FirstParentItem = new MenuItem(lst1.modulenam);
                FirstParentItem.Value = lst1.moduleid;
                // FirstParentItem.Selectable = slct;
                FirstParentItem.NavigateUrl = lst1.url;
                ((Menu)this.Master.FindControl("Menu1")).Items.Add(FirstParentItem);

            }




        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetUserPermission();
            this.GetModulename();
            //this.ModuleVisible();
            this.ShowUserPerModule();

            DataTable dt = (DataTable)ViewState["tblmoduleName"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "usrper = '" + "true" + "'";
            DataTable dt1 = dv.ToTable();
            string TopModule = dt1.Rows[0]["moduleid"].ToString();
            this.ddlModuleName.SelectedValue = TopModule;
            this.MasComNameAndAdd();
            this.ddlModuleName_SelectedIndexChanged(null, null);

        }

    }
}