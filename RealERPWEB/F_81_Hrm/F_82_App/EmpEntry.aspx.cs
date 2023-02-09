using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntry : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetInformation();
                this.GetEmployeeName();
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PERSONAL INFORMATION";
                this.getLastCardNo();
                this.lblLastCardNo.Visible = true;
                CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdatPerInfo_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Text = "Agreement";
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((Panel)this.Master.FindControl("pilleftDvi")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Aggrement";
            // ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlInformation_SelectedIndexChanged(null, null);
        }
        protected void ddlInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectView();
        }
        private void GetInformation()
        {
            string comcod = this.GetComeCode();
            string txtinformation = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETINFORMATION", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "infodesc";
            this.ddlInformation.DataValueField = "infoid";
            this.ddlInformation.DataSource = ds3.Tables[0];
            this.ddlInformation.DataBind();
        }
        
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void getLastCardNo()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLASTCARDNO", "", "", "", "", "", "", "", "", "");
            this.lblLastCardNo.Text = "Last Card Number :- " + ds5.Tables[0].Rows[0]["lastCard"].ToString().Trim();
        }
        private void GetEmployeeName()
        {
            Session.Remove("tblempname");
            string comcod = this.GetComeCode();
            string txtSProject = (this.Request.QueryString["empid"] != "") ? "%" + this.Request.QueryString["empid"].ToString() + "%" : "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            //this.ddlEmpName.SelectedValue = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds3.Tables[0];
            ds3.Dispose();
            this.ddlEmpName.SelectedValue = (this.Request.QueryString["empid"] == "") ? this.ddlEmpName.Items[0].Value : this.Request.QueryString["empid"].ToString();
            this.SelectView();
        }
        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            this.lblLastCardNo.Visible = false;
            switch (infoid)
            {
                case "01":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetBldMeReFes();
                    this.GetSupervisorName();
                    this.ShowPersonalInformation();
                    this.lblLastCardNo.Visible = true;
                    this.addOcupation.Visible = false;

                    
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                    break;

                case "10":
                    //this.MultiView1.ActiveViewIndex = 1;
                    //this.GetAcaDemicDegree();
                    //this.ShowDegree();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

                    break;

                case "13":

                    //this.MultiView1.ActiveViewIndex = 2;
                    //this.ShowEmpRecord();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "14":
                    //this.MultiView1.ActiveViewIndex = 3;
                    //this.ShowEmpPosition();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "15":
                    //this.MultiView1.ActiveViewIndex = 5;
                    //this.ShowJobRespon();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;


                case "16":
                    //this.MultiView1.ActiveViewIndex = 4;
                    //this.ShowReferecne();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;
                case "20":
                case "21":
                case "26":
                case "27":
                    //this.MultiView1.ActiveViewIndex = 6;
                    //this.ShowParentDT();
                    //this.ShowLastDegree();
                    //this.addOcupation.Visible = true;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "30":
                    //this.MultiView1.ActiveViewIndex = 7;
                    //this.ShowCTCDetails();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    break;
                case "31":
                    //this.MultiView1.ActiveViewIndex = 8;
                    //this.ShowSalaryDetails();
                    //this.addOcupation.Visible = false;
                    //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;


            }
        }

        private void GetBldMeReFes()
        {
            string comcod = this.GetComeCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBLDMEREFES", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];
        }
        private void GetSupervisorName()
        {

            Session.Remove("tblsppname");
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;

            DataRow row = ds3.Tables[0].NewRow();  // NewRow();
            row["empid"] = "000000000000";
            row["empname"] = "None";
            ds3.Tables[0].Rows.Add(row);
            DataView dv = ds3.Tables[0].DefaultView;
            dv.Sort = "empid";
            Session["tblsppname"] = dv.ToTable();

            ds3.Dispose(); 
        }
        private void ShowPersonalInformation()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPERSONALINFO", empid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[0];
            Session["UserLog"] = ds2.Tables[1];
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["empname1"];
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1;
            this.gvPersonalInfo.DataSource = ds2.Tables[0];
            this.gvPersonalInfo.DataBind();

            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    case "01006": //Confirmation Date
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '85%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "01009": //Blood Group
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '90%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01010": //Relationship Status 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '92%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01011":// Religion
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01012": // Festival
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '97%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01013": // Nationality
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '98%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01023": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '99%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01025": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '96%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01994": // Grade
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '34%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01995": // Service Location
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '28%' or gcod like '29%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01996": // Supper Location


                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";

                        ddlgval.DataSource = (DataTable)Session["tblsppname"];
                        //ddlgval.SelectedIndex =-1;

                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01997": // Grade
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '86%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01003": // Datetime
                    case "01007":
                    case "01008":
                    case "01999":
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }
             
        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            switch (infoid)
            {
                case "01":
                    lUpdatPerInfo_Click1(null, null);
                    break;
                //case "10":
                //    lUpdateDegree_Click(null, null);
                //    break;


            }
        }
        protected void lUpdatPerInfo_Click1(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            //string Gvalue="";
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string empname = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
            //Log Entry

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                //new nahid by 20220126

                if (Gcode == "01001")
                {
                    string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    if (Gvalue.Length == 0)
                    {
                        string errMsg = "Please Put ID CARD Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    DataSet copSetup = compUtility.GetCompUtility();
                    if (copSetup == null)

                        return;
                    int idCardLength = copSetup.Tables[0].Rows.Count == 0 ? 0 : Convert.ToInt32(copSetup.Tables[0].Rows[0]["hr_idcardlen"]);
                    if (Gvalue.Length != idCardLength && idCardLength != 0)
                    {
                        string errMsg = "Please Put " + idCardLength + " Digit ID CARD Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");

                    }

                    //// for duplicate value 
                    ///
                    ////
                    ///////////----------------------------------------
                    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETIDCARDNO", Gvalue, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;
                    else
                    {
                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("empid <>'" + empid + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate ID CARD No" + "');", true);

                            return;
                        }
                    }
                    ///////////////----------------------------------------

                }

                if (Gcode == "01003")
                {
                    string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                    if (Gvalue.Length == 0)
                    {
                        // int x = 1;
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        //  value = value+x;
                    }
                    else
                    {
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");
                    }

                }

            }

            //---------------Validation Check---------------------//



            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
            //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editrmid"].ToString();

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;
            string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Editrmid = (dtuser.Rows.Count == 0) ? "" : Terminal;

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPLINF", empid, empname, PostedByid, PostSession, Posttrmid, Posteddat,
                    EditByid, Editdat, Editrmid, "", "", "", "", "", "", "", "", "", "", "", "");




            if (result == false)
                return;

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string gvalueBn = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvValBn")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();


                if (Gcode == "01003" || Gcode == "01007" || Gcode == "01008")
                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

                else if (Gcode == "01999")
                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "",
                            "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", gvalueBn);


                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated Fail" + "');", true);
                    return;
                }

            }
            this.getLastCardNo();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);


        }

        protected void ibtngrdEmpList_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01996": //Supper Visor


                        string comcod = this.GetComeCode();
                        DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
                        string Searchemp = "%" + ((TextBox)gvPersonalInfo.Rows[i].FindControl("txtgrdEmpSrc")).Text.Trim() + "%";
                        DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", Searchemp, "", "", "", "", "", "", "", "");

                        //ddl2.AppendDataBoundItems = true;
                        ddl2.DataTextField = "empname";
                        ddl2.DataValueField = "empid";

                        ddl2.DataSource = ds3.Tables[0];
                        ddl2.DataBind();
                        ds3.Dispose();
                        break;



                }


            }
        }
        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Joindate = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01003": //Join Date

                        Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        //Joindate = ASTUtility.DateFormat(Joindate) ;
                        // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;

                        break;


                    case "01006": //Confirmation Date
                        string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                        if (value == "None")
                            continue;
                        int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                        string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd-MMM-yyyy");
                        ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
                        break;
                }


            }


        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");
                TextBox txtgvValBn = (TextBox)e.Row.FindControl("txtgvValBn");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01002")
                {

                    txtgvname.ReadOnly = true;
                    //txtgvValBn.ReadOnly = false;
                    //txtgvValBn.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc1")).ToString();
                }

                //txtgvValBn.Text = "";

            }


        }
    }
}