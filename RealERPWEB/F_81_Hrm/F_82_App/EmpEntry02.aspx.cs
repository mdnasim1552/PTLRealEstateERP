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
using System.IO;
using Microsoft.Office.Interop.Excel;
using RealERPLIB;
using RealERPRPT;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using AjaxControlToolkit;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntry02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            
                GetNewEmpData();
                ShowPersonalInformation();





            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetNewEmpData()
        {
            string comcod = this.GetComeCode();
            string advno = this.Request.QueryString["advno"].ToString().Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETRECEMP", advno, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                return;

            Session["empinfo"] = ds1.Tables[0];

        }
         private void ShowPersonalInformation()
        {

            string comcod = this.GetComeCode();
            string gdesc1 = "";


            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "EMPPERSONALINFO", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt;
            DataTable dt2;
            DataView dv2;
            DataView dv3;

            DataTable ddt = (DataTable)Session["empinfo"];

            dv2 = ds2.Tables[0].DefaultView;
            dv2.RowFilter = "gcod >= '01001' and gcod <= 01020";
            dt = dv2.ToTable();

            dv3 = ds2.Tables[0].DefaultView;
            dv3.RowFilter = "gcod >= '01021' and gcod <= 99999";
            dt2 = dv3.ToTable();


   
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = ddt.Rows[0]["name"].ToString();
            this.txtname.InnerText= ddt.Rows[0]["name"].ToString()??"";
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1;

            //first step
            this.gvPersonalInfo.DataSource = dt;
            this.gvPersonalInfo.DataBind();
            //secnond step
            //this.gvPersonalInfo2.DataSource = dt2;
            //this.gvPersonalInfo2.DataBind();




            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    //name

                    case "01002":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["name"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;
                        //doj
                    case "01003":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["doj"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;


                    //email
                    case "01016":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["email"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;

                    //mobile
                    case "01014":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["mobile"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;
                    //permanent address
                    case "01017":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["peradd"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;
                    //present address
                    case "01018":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ddt.Rows[0]["preadd"].ToString();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

                        break;

                }

            }

            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{

            //    string Gcode = dt2.Rows[i]["gcod"].ToString();

            //    switch (Gcode)
            //    {




            //        case "01999":
            //            ((Panel)this.gvPersonalInfo2.Rows[i].FindControl("Panegrd")).Visible = false;
            //            ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Items.Clear();
            //            ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Visible = false;
            //            ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((LinkButton)this.gvPersonalInfo2.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

            //            break;


            //        default:
            //            ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            ((Panel)this.gvPersonalInfo2.Rows[i].FindControl("Panegrd")).Visible = false;
            //            ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Items.Clear();
            //            ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Visible = false;
            //            ((LinkButton)this.gvPersonalInfo2.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

            //            break;

            //    }

            //}



        }



        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gval");
            string comcod = GetComeCode();
            string gval = "";
            string empid = "";
            string empdept = "9301";
            string empname = this.txtname.InnerText;


            DataSet dt = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTEMPNAMELASTIDWISE", empdept, empname, "", "", "");
            if (dt == null || dt.Tables[0].Rows.Count == 0)
                return;
            empid = dt.Tables[0].Rows[0]["empid"].ToString();


            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                //name 
                if (gcode == "01002")
                {
                    gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
               
                //mobile
                else if (gcode == "01014")
                {
                    gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }

                //email
                else if (gcode == "01016")
                {
                    gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                else
                {

                    gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                }


            }
            //for (int i = 0; i < this.gvPersonalInfo2.Rows.Count; i++)
            //{
            //    DataRow dr = dt1.NewRow();
            //    string gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //    //name 
            //    if (gcode == "01002")
            //    {
            //        gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
            //        dr["gcod"] = gcode;
            //        dr["gval"] = gval;
            //        if (gval.Length == 0)
            //        {
            //            string Message = "Select Type to continue";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
            //            return;
            //        }
            //    }

            //    //mobile
            //    else if (gcode == "01014")
            //    {
            //        gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
            //        dr["gcod"] = gcode;
            //        dr["gval"] = gval;
            //        if (gval.Length == 0)
            //        {
            //            string Message = "Select Type to continue";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
            //            return;
            //        }
            //    }

            //    //email
            //    else if (gcode == "01016")
            //    {
            //        gval = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.ToString();
            //        dr["gcod"] = gcode;
            //        dr["gval"] = gval;
            //        if (gval.Length == 0)
            //        {
            //            string Message = "Select Type to continue";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
            //            return;
            //        }
            //    }

            //    dt1.Rows.Add(dr);
            //}


            if (dt1.Rows.Count == 0 || dt1 == null)
                return;
  
            List<bool> resultCompA = new List<bool>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTHRDINF", empid, dt1.Rows[i]["gcod"].ToString(), dt1.Rows[i]["gval"].ToString(), "", "", "", "", "", "", "");
                resultCompA.Add(result);
            }

      

        }
       

        private string getLastEmpid()
        {
            string comcod = this.GetComeCode();
            string compny = "9301";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "LASTEMPID", compny, "", "", "", "", "", "", "", "");
            //int empid= Convert.ToInt32(ds1.Tables[0].Rows[0]["lastempid"]) + 1;
            string empid = ds1.Tables[0].Rows[0]["lastempid"].ToString().Remove(6);
           int empid2= Convert.ToInt32(empid) + 1;
            return empid2.ToString();
        }



    }
}