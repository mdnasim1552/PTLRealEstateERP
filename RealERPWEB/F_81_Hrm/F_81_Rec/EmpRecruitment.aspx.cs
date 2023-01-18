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
    public partial class EmpRecruitment : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.SelectView();
                this.ddlPreInfo.Enabled = false;

                return;
            }
            this.lbtnOk.Text = "Ok";

            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
            this.lblRef.Text = "";
            this.ddlPreInfo.Items.Clear();
            this.ddlPreInfo.Enabled = true;
            this.lblPre.Visible = true;
            this.txtPreSer.Visible = true;
            this.ddlPreInfo.Visible = true;
            this.ImgbtnFindPre.Visible = true;


        }

        private void SelectView()
        {
            if (this.ddlPreInfo.Items.Count == 0)
            {
                this.GetRefno();
                this.lblPre.Visible = false;
                this.txtPreSer.Visible = false;
                this.ddlPreInfo.Visible = false;
                this.ImgbtnFindPre.Visible = false;
            }
            else
            {

            }
            this.GetDDLVal();
            this.ShowPersonalInformation();
        }
        private void GetPreRef()
        {
            if (this.lbtnOk.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string date = this.txtDate.Text;
            string txtser = "%" + this.txtPreSer.Text + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETPREREF", date, txtser, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPreInfo.DataTextField = "advno1";
            this.ddlPreInfo.DataValueField = "advno";
            this.ddlPreInfo.DataSource = ds1.Tables[0];
            this.ddlPreInfo.DataBind();
            Session["Advinfo"] = ds1.Tables[0];
            if (this.ddlPreInfo.Items.Count != 0)
            {

                this.lblRef.Text = ds1.Tables[0].Rows[0]["advno"].ToString();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Invaild Search Input";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                this.lblRef.Text = "";
            }

        }

        private void GetRefno()
        {

            try
            {

                string comcod = this.GetComeCode();
                string addmidate = this.txtDate.Text;
                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETREFNO", addmidate,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                this.lblRef.Text = ds2.Tables[0].Rows[0]["advno"].ToString();

                ds2.Dispose();


            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        private void GetDDLVal()
        {


            string comcod = this.GetComeCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETDDLINF", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];



        }

        private void ShowPersonalInformation()
        {

            string comcod = this.GetComeCode();
            string refno = this.lblRef.Text;

            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "SHOWINITIALINFO", refno, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt = ds2.Tables[0];
            //DataRow[] dr = dt.Select("gcod='01002'");

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


                    case "01001": //Grade  
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("grp= 'A'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        //string data1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01004": //District  
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("grp= 'B'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01007": //Course Selection 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("grp= 'C'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01010": // reference
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("grp= 'D'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    //case "01060": // Buy Application Form
                    //    dv1 = dt1.DefaultView;
                    //    dv1.RowFilter = ("gcod like '52%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "gdesc";
                    //    ddlgval.DataValueField = "gcod";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;


                    default:
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }




            }


        }




        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string refno = "";
            string addate = this.txtDate.Text.Trim();
            string StdName = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETREFNO", addate,
                       "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                return;
            if (this.ddlPreInfo.Items.Count < 0)
            {
                this.lblRef.Text = ds2.Tables[0].Rows[0]["advno"].ToString();
                refno = ds2.Tables[0].Rows[0]["advno"].ToString();
            }
            else
            {
                refno = this.lblRef.Text;
            }
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                bool result = HRData.UpdateTransInfo2(comcod, "SP_ENTRY_RECRUITMENT", "INSERTUPDATEININFO", refno, Gcode, gtype, Gvalue, addate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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
            //    if (code == "01002")
            //    {

            //        txtgvname.ReadOnly = true;

            //    }

            //}


        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {

        }


        protected void ImgbtnFindPre_Click(object sender, ImageClickEventArgs e)
        {
            this.GetPreRef();
        }
        protected void ibtngrdEmpList_Click(object sender, ImageClickEventArgs e)
        {

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                string comcod = this.GetComeCode();
                DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
                string Searchemp = "%" + ((TextBox)gvPersonalInfo.Rows[i].FindControl("txtgrdEmpSrc")).Text.Trim() + "%";
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETPOST", Searchemp, "", "", "", "", "", "", "", "");
                ddl2.DataTextField = "gdesc";
                ddl2.DataValueField = "gcod";

                switch (Gcode)
                {
                    case "01010": //Post


                        //string comcod = this.GetComeCode();
                        //DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
                        //string Searchemp = "%" + ((TextBox)gvPersonalInfo.Rows[i].FindControl("txtgrdEmpSrc")).Text.Trim() + "%";
                        //DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_RECRUITMENT", "GETPOST", Searchemp, "", "", "", "", "", "", "", "");
                        //ddl2.DataTextField = "gdesc";
                        //ddl2.DataValueField = "gcod";
                        ddl2.DataSource = ds3.Tables[0];
                        ddl2.DataBind();
                        ds3.Dispose();
                        break;
                    case "01007": //Department

                        ddl2.DataSource = ds3.Tables[1];
                        ddl2.DataBind();
                        ds3.Dispose();
                        break;
                    case "01004": //Department

                        ddl2.DataSource = ds3.Tables[2];
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
                    //case "01003": //Join Date

                    //    Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                    //        : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    //Joindate = ASTUtility.DateFormat(Joindate) ;
                    //    // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;

                    //    break;


                    //case "01006": //Confirmation Date
                    //    string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                    //    int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                    //    string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd-MMM-yyyy");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = ConDate;
                    //    break;
                }


            }


        }
        protected void ddlPreInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prevAdv = this.ddlPreInfo.SelectedValue.Trim().ToString();
            this.lblRef.Text = (((DataTable)Session["Advinfo"]).Select("advno='" + prevAdv + "'"))[0]["advno"].ToString();


            //if (this.ddlPreInfo.Items.Count > 0)
            //    this.lblCode.Text = this.ddlPreInfo.SelectedValue.Trim().ToString();
            //else
            //    this.lblCode.Text = "";
        }
    }
}