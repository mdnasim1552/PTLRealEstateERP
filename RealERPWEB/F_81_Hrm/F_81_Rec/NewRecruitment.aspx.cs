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
                this.getDept();
                this.getSection();
                this.getDesig();
                this.getGender();
                getGrade();
                GetInputEntry();
                getAllData();
            }

        }

        private void getSection()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETSECTION", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["tblsec"] = ds3.Tables[0];
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

        private void getGender()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETGENDER", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["dtgender"] = ds3.Tables[0];

        }

        private void getGrade()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETGRADE", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["dtGrade"] = ds3.Tables[0];

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
        private void GetInputEntry()
        {
            string comcod = this.GetComeCode();
          string advno=  this.lbladvnoo.Text;
            DataSet ds = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GEETCODE", advno, "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;
            DataTable dt = ds.Tables[0];
            DataTable dt1 = (DataTable)ViewState["dtDesig"];
            DataTable dt2 = (DataTable)ViewState["tbldept"];
            DataTable dt3 = (DataTable)ViewState["tblsec"];
            DataTable dt4 = (DataTable)ViewState["dtGrade"];
            DataTable dt5 = (DataTable)ViewState["dtgender"];
            DropDownList ddlgval;
            gvNewRec.DataSource = ds.Tables[0];
            gvNewRec.DataBind();


            string gvalue = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gcod = dt.Rows[i]["gcod"].ToString();
                switch (gcod)
                {

                    //joindat
                    case "97003":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text = gvalue;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = true;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Text = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
                        break;

                    //dept
                    case "97005":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "deptdesc";
                        ddlgval.DataValueField = "deptcode";
                        ddlgval.DataSource = dt2;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gvalue;
                        break;

                    //section
                    case "97008":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "sirdesc";
                        ddlgval.DataValueField = "sircode";
                        ddlgval.DataSource = dt3;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gvalue;
                        break;

           
                    //designation
                    case "97007":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "hrgdesc";
                        ddlgval.DataValueField = "hrgcod";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gvalue;
                        break;



                    //gender
                    case "99000":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        ddlgval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dt5;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gvalue;
                        break;
                    //present address
                    case "97103":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text = gvalue;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = true;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        break;
                    //permanent address
                    case "97104":
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text = gvalue;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = true;

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        break;
                    //attach file
                    case "97999":
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = true;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

                        break;
                    default:
                        gvalue = dt.Rows[i]["value"].ToString();
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text = gvalue;
                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).Visible = false;
                        ((FileUpload)this.gvNewRec.Rows[i].FindControl("imgFileUpload")).Visible = false;

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Visible = false;

                        ((TextBox)this.gvNewRec.Rows[i].FindControl("txtjoindat")).Visible = false;

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
  

                //dept
            if (gcode == "97005")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval.Substring(0, 12);
                    dt1.Rows.Add(dr);

                }
                //section

                else if (gcode == "97008")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval.Substring(0, 12);
                    dt1.Rows.Add(dr);

                  
                }
                //designation
                else if (gcode == "97007")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Value.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                 
                }

                //designation
                else if (gcode == "99000")
                {
                    gval = ((DropDownList)this.gvNewRec.Rows[i].FindControl("ddldesig")).SelectedItem.Value.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                }
                else if (gcode == "97103")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

         
                }

                else if (gcode == "97104")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtarea")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);


                }



              

                else if (gcode == "04001")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text == "" ? "0" : ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text;
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);
                }

                else if (gcode == "04002")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text == "" ? "0" : ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text;
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);
                }

                else if (gcode == "04003")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text == "" ? "0" : ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text;

                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);
                }

                else if (gcode == "04004")
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text == "" ? "0" : ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text;

                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);

                }

        
                else if (gcode == "97999")
                {
   
                   
                    gval = imgPath;
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);



                }
                else
                {
                    gval = ((TextBox)this.gvNewRec.Rows[i].FindControl("txtgvVal")).Text.ToString();
                    dr["gcod"] = gcode;
                    dr["gval"] = gval;
                    dt1.Rows.Add(dr);
                }



            }
            if (dt1.Rows.Count == 0 || dt1 == null)
                return;
            string curdate = System.DateTime.Now.ToString();
            string lbladvno = this.lbladvnoo.Text;
            string advno = "";
            if (lbladvno == "")
            {

                DataSet dt = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTSHORTLISTB", "02001", "96001", curdate, curdate, "", "", "");
                 advno = dt.Tables[0].Rows[0]["advno"].ToString();
            }
            else
            {
                 advno = lbladvno;

            }

            List<bool> resultCompA = new List<bool>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
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
                ViewState.Remove("lbladvno");
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
        //remove data
        protected void btnRemove_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string msg = "";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();

            bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "REMOVE_REC_EMP", id, "", "", "", "", "", "");
            if (result)
            {
                msg = "Deleted Successfully";
                this.getAllData();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }
            else
            {
                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }
        //edit
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ViewState.Remove("lbladvno");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lbladvno = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();
            this.lbladvnoo.Text= lbladvno;
            this.GetInputEntry();
        }

        //reset input
        protected void lnkReset_Click(object sender, EventArgs e)
        {
            this.lbladvnoo.Text = "";
            this.resetinput();
        }

        //view data
        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string advno = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();
            string name = ((Label)this.gvAllRec.Rows[index].FindControl("lblname")).Text.ToString();
            string desig = ((Label)this.gvAllRec.Rows[index].FindControl("lbldesig")).Text.ToString();
            string mobile = ((Label)this.gvAllRec.Rows[index].FindControl("lblmobile")).Text.ToString();
            string email = ((Label)this.gvAllRec.Rows[index].FindControl("lblemail")).Text.ToString();
            string preadd = ((Label)this.gvAllRec.Rows[index].FindControl("lblpreadd")).Text.ToString();
            string peradd = ((Label)this.gvAllRec.Rows[index].FindControl("lblpereadd")).Text.ToString();
            string dept = ((Label)this.gvAllRec.Rows[index].FindControl("lbldept")).Text.ToString();

            this.name.InnerText = name;
            this.desig.InnerText = desig;
            this.dept.InnerText = dept;
            this.mobile.InnerText = mobile;
            this.email.InnerText = email;
            this.preadd.InnerText = preadd;
            this.peradd.InnerText = peradd;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ViewEmpModal();", true);
        }

        protected void ApplyJoinning_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetComeCode();
            string empname = ((Label)this.gvAllRec.Rows[index].FindControl("lblname")).Text.ToString().Trim();
            string empdept = "9301";//this.ddlDept.SelectedValue.ToString().Trim().Substring(0, 9);
            string Message;

            bool  result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTEMPNAMELASTIDWISE", empdept, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                Message = "Successfully Added Employee : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
            }
            else
            {
                Message = "Sorry, Data Updated Fail : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
            }
            getAllData();
        }

        protected void gvAllRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string type = this.RadioButtonList1.SelectedValue.ToString();
            //    if (type == "10003")
            //    {
            //        ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
            //        ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = false;
            //        ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;

            //    }
            //    else if (type == "10002")
            //    {
            //        ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
            //        ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
            //        ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
            //    }
            //    else if (type == "10025")
            //    {
            //        ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
            //        ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
            //        ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = true;
            //    }
            //    else if (type == "19999")
            //    {
            //        ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = false;
            //        ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = false;
            //        ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
            //    }

            //}
        }
    }
}