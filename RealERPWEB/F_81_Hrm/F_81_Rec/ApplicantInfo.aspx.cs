
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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class ApplicantInfo : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCompany();
                //this.GetOffice();
                //this.GetDesignation();
                //this.GetEmployeeName();
                this.GetInformation();
            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtSComp = this.txtSComp.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETCOMPANYNAME", txtSComp, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "degname";
            this.ddlCompany.DataValueField = "degid";
            this.ddlCompany.DataSource = ds3.Tables[0];
            this.ddlCompany.DataBind();
            this.GetOffice();
        }
        private void GetOffice()
        {
            string comcod = this.GetComeCode();
            string txtSOffice = this.txtSOffice.Text + "%";
            string txtcompany = this.ddlCompany.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETOFFICENAME", txtSOffice, txtcompany, "", "", "", "", "", "", "");
            this.ddlOffice.DataTextField = "degname";
            this.ddlOffice.DataValueField = "degid";
            this.ddlOffice.DataSource = ds3.Tables[0];
            this.ddlOffice.DataBind();
            this.GetDesignation();
        }

        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            string txtDSearch = this.txtDesSearch.Text + "%";
            string txtoffice = this.ddlOffice.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETEMPDESIGNATION", txtDSearch, txtoffice, "", "", "", "", "", "", "");
            this.ddlDesig.DataTextField = "degname";
            this.ddlDesig.DataValueField = "degid";
            this.ddlDesig.DataSource = ds3.Tables[0];
            this.ddlDesig.DataBind();
            this.GetEmployeeName();
        }

        private void GetEmployeeName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = this.txtEmpSrc.Text + "%";
            string txtDesig = this.ddlDesig.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETEMPTNAME", txtSProject, txtDesig, "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

        }

        private void GetInformation()
        {

            string comcod = this.GetComeCode();
            string txtinformation = this.txtInformation.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETINFORMATION", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "infodesc";
            this.ddlInformation.DataValueField = "infoid";
            this.ddlInformation.DataSource = ds3.Tables[0];
            this.ddlInformation.DataBind();
        }

        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void ibtnInformation_Click(object sender, ImageClickEventArgs e)
        {
            this.GetInformation();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlEmpName.Visible = false;
                this.lblEmpName.Visible = true;
                this.ddlInformation.Visible = false;
                this.lblInformation.Visible = true;
                this.lblDesig.Visible = true;
                this.ddlDesig.Visible = false;
                this.ddlCompany.Visible = false;
                this.lblCompany.Visible = true;
                this.ddlOffice.Visible = false;
                this.lblOffice.Visible = true;
                this.lbtnOk.Text = "New";
                this.lblCompany.Text = this.ddlCompany.SelectedItem.Text;
                this.lblOffice.Text = this.ddlOffice.SelectedItem.Text;
                this.lblEmpName.Text = this.ddlEmpName.SelectedItem.Text;
                this.lblInformation.Text = this.ddlInformation.SelectedItem.Text;
                this.lblDesig.Text = this.ddlDesig.SelectedItem.Text;
                this.SelectView();
                return;
            }

            this.ddlEmpName.Visible = true;
            this.lblEmpName.Visible = false;
            this.ddlInformation.Visible = true;
            this.lblInformation.Visible = false;
            this.lblDesig.Visible = false;
            this.ddlDesig.Visible = true;

            this.ddlCompany.Visible = true;
            this.lblCompany.Visible = false;
            this.ddlOffice.Visible = true;
            this.lblOffice.Visible = false;

            this.lbtnOk.Text = "Ok";
            this.MultiView1.ActiveViewIndex = -1;
            this.lblEmpName.Text = "";
            this.lblInformation.Text = "";

        }

        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            switch (infoid)
            {
                case "01":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetBldMeReFes();
                    this.ShowPersonalInformation();
                    break;

                case "10":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowDegree();
                    break;

                case "13":

                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEmpRecord();
                    break;

                case "14":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowEmpPosition();
                    break;

                case "15":

                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowReferecne();
                    break;

                case "16":
                    this.MultiView1.ActiveViewIndex = 5;

                    break;



            }

        }

        private void GetBldMeReFes()
        {


            string comcod = this.GetComeCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETBLDMEREFES", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];



        }

        private void ShowPersonalInformation()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "EMP_PERSONAL_INFO", empid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataTable dt = ds2.Tables[0];
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = this.lblEmpName.Text.Trim();
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
                    case "01009": //Blood Group
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '90%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
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
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }

        }

        private void ShowDegree()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "EMPACADEGREE", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvDegree.DataSource = null;
                this.gvDegree.DataBind();
                return;

            }
            this.gvDegree.DataSource = ds3.Tables[0];
            this.gvDegree.DataBind();


        }

        private void ShowEmpRecord()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "EMPRECORD", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvEmpRec.DataSource = null;
                this.gvEmpRec.DataBind();
                return;

            }
            this.gvEmpRec.DataSource = ds3.Tables[0];
            this.gvEmpRec.DataBind();



        }

        private void ShowEmpPosition()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "EMPPOSITION", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvAssocia.DataSource = null;
                this.gvAssocia.DataBind();
                return;

            }
            this.gvAssocia.DataSource = ds3.Tables[0];
            this.gvAssocia.DataBind();



        }

        private void ShowReferecne()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "EMPPREF", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvRef.DataSource = null;
                this.gvRef.DataBind();
                return;

            }
            this.gvRef.DataSource = ds3.Tables[0];
            this.gvRef.DataBind();




        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string empname = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
            bool result = HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPLINF", empid, empname, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
                return;

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                if (Gcode == "01003")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }


                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? "0" + Gvalue : Gvalue;

                HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lUpdateDegree_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvDegree.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvDegree.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Degree = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgcvDegree")).Text.Trim();
                string Group = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvGroup")).Text.Trim();
                string Institute = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvInstituee")).Text.Trim();
                string Result = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvResult")).Text.Trim();
                string PassisnYr = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvPass")).Text.Trim();
                if (Degree.Length > 0)
                    HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Degree, "", Group, Institute, Result, PassisnYr, "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
        protected void lUpdateEmprecord_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvEmpRec.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvEmpRec.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvEmpRec.Rows[i].FindControl("lgvgval")).Text.Trim();
                string ComName = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgcvCompany")).Text.Trim();
                string Desig = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvDesig")).Text.Trim();
                string txtgvesDuration = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDuration")).Text.Trim();

                if (ComName.Length > 0)
                    HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, ComName, "", Desig, txtgvesDuration, "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void lUpdateEmpAssocia_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvAssocia.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvAssocia.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvAssocia.Rows[i].FindControl("lgvgval")).Text.Trim();
                string OrgName = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgcvOrgName")).Text.Trim();
                string Position = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgvPostion")).Text.Trim();


                if (OrgName.Length > 0)
                    HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, OrgName, "", Position, "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void lUpdateRef_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvRef.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvRef.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvRef.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Name = ((TextBox)this.gvRef.Rows[i].FindControl("txtgcvName")).Text.Trim();
                string OrgName = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvOrgname")).Text.Trim();
                string Designation = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvDesig")).Text.Trim();
                string Phone = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvPhone")).Text.Trim();
                string Mobile = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvMobile")).Text.Trim();
                if (Name.Length > 0)
                    HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Name, "", OrgName, Designation, Phone, Mobile, "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01002")
                {

                    txtgvname.ReadOnly = true;

                }

            }


        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOffice();

        }
        protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetDesignation();

        }
        protected void ddlDesig_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetEmployeeName();
        }
    }
}