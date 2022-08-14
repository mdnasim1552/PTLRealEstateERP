﻿using System;
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
using RealERPRPT;
using System.IO;

namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class NewRecruitment : System.Web.UI.Page
    {
        ProcessAccess RecData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getDesig();
                this.getDept();
                GetData();
                getAllData();
            }

        }


        //all department(rakib)
        private void getDept()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETDEPTNAME", "%%", "%%", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["tbldept"] = ds3.Tables[0];
        }
        

        //all designation(rakib)

        private void getDesig()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETDESIG", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["dtDesig"] = ds3.Tables[0];

        }

        //all new recruit emp(rakib)
        private void getAllData()
        {
            Session.Remove("alldata");
            string comcod = this.GetComeCode();
            DataSet ds = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETALLREC", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvAllRec.DataSource = null;
                this.gvAllRec.DataBind();
                return;
            }
   
            this.gvAllRec.DataSource = ds.Tables[0];
            this.gvAllRec.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        //set label and input(rakib)
        private void GetData()
        {
            string comcod = this.GetComeCode();
            DataSet ds = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GEETCODE", "%%", "%%", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;
            DataTable dt = ds.Tables[0];
            DataTable dt1 = (DataTable)ViewState["dtDesig"];
            DataTable dt2 = (DataTable)ViewState["tbldept"];
            DropDownList ddlgval;
            gvNewRec.DataSource = ds.Tables[0];
            gvNewRec.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gcod = dt.Rows[i]["gcod"].ToString();
                switch (gcod)
                {

                    //dept
                    case "97005":

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "deptdesc";
                        ddlgval.DataValueField = "deptcode";
                        ddlgval.DataSource = dt2;
                        ddlgval.DataBind();
                        break;
                    //designation
                    case "97007":

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "hrgdesc";
                        ddlgval.DataValueField = "hrgcod";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
         
                        break;
                    //present address
                    case "97103":
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = true;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        break;
                    //permanent address
                    case "97104":
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = true;

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        break;
                    //attach file
                    case "97999":
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = true;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        break;
                    default:
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;


                        break;

                }
            }
        }


        //save data by gcod(rakib)
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gval");
            string comcod = GetComeCode();
            string gval = "";
            string imgPath = "";

            for (int i = 0; i < this.gvNewRec.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string gcode = ((Label)this.gvNewRec.Rows[i].FindControl("lblgcode")).Text.Trim();
                //name 
                if (gcode == "97001")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                //dept
                else if (gcode == "97005")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval.Substring(0,12);
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                //designation
                else if (gcode == "97007")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                //mobile
                else if (gcode == "97019")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }

                //email
                else if (gcode == "97020")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }


                else if (gcode == "97103")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }

                else if (gcode == "97104")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    if (gval.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }


                else if (gcode == "97999")
                {
                    if (((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).HasFile)
                    {


                        string filePath = ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).PostedFile.FileName;
                        string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file  
                        string ext = Path.GetExtension(filename1);

                        if (ext == ".pdf")
                        {

                            string imgName = Guid.NewGuid() + ext;
                            //sets the image path           
                            imgPath = "~/Upload/HRM/Doc/" + imgName;
                            //then save it to the Folder  
                            ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).SaveAs(Server.MapPath(imgPath));
                        }
                        else
                        {
                            string msgfail = "Please select pdf file only";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgfail + "');", true);
                            return;
                        }
                    }
                    gval = imgPath;
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;


                }
                dt1.Rows.Add(dr);



            }
            if (dt1.Rows.Count == 0 || dt1 == null)
                return;
            string curdate = System.DateTime.Now.ToString();

            DataSet dt = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTSHORTLISTB", "02001", "96001", curdate, curdate, "", "", "");
            string advno = dt.Tables[0].Rows[0]["advno"].ToString();
            List<bool> resultCompA = new List<bool>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {


                //bool result = RecData.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTVEHICLEINF", vehicleId,
                //               dt1.Rows[i]["gcod"].ToString(), dt1.Rows[i]["gvalue"].ToString(), dt1.Rows[i]["gval"].ToString(), remarks, "", "", "", "", "", "", "",
                //                   "", "", "", "", "", "", "", "", "", "", userId);

                bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTUPDATE", advno, dt1.Rows[i]["gcod"].ToString(), dt1.Rows[i]["gval"].ToString(), "", "", "", "", "", "", "");
                resultCompA.Add(result);
            }

            if (resultCompA.Contains(false))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successful" + "');", true);
                getAllData();
                this.resetinput();
            }
        }


        //reset input fields(rakib)
        private void resetinput()
        {
            for (int i = 0; i < this.gvNewRec.Rows.Count; i++)
            {
                ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text = "";
                ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text = "";
                ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).ClearSelection();
            }
        }

       
    }
}