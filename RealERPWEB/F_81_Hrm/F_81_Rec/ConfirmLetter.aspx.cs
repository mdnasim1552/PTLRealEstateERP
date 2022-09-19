using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using View = System.Windows.Forms.View;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{

    public partial class ConfirmLetter : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Confirmation Letter";
                this.GetEmployeeName();
                this.pnlEmp.Visible = false;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lbtnPrevconfmltNo.Visible = false;
                this.ddlPrevconfmlttNo.Visible = false;
                this.pnlEmp.Visible = true;
                this.ShowConfmLtInfo();

                return;
            }
            this.lbtnOk.Text = "Ok";

            this.pnlEmp.Visible = false;
            this.ddlPrevconfmlttNo.Items.Clear();
            this.lbtnPrevconfmltNo.Visible = true;
            this.ddlPrevconfmlttNo.Visible = true;
            this.txtCurDate.Enabled = true;
            this.gvConfmltr.DataSource = null;
            this.gvConfmltr.DataBind();
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetEmployeeName()
        {

            Session.Remove("tblempname");
            string comcod = this.GetComeCode();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETEMPTIDNAME", "", "", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname1";
            //this.ddlEmpName.SelectedValue = "empname1";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds2.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds2.Tables[0];
            ds2.Dispose();

        }
        protected void lbtnSelect_OnClick(object sender, EventArgs e)
        {
            //this.GetEmployeeName(); // comment - tarik - recom nahid sir why ?? 
            string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblConfm"];
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {

                DataRow dr1 = dt.NewRow();
                dr1["empid"] = this.ddlEmpName.SelectedValue.ToString();
                dr1["empname"] = this.ddlEmpName.SelectedItem.Text.Trim();
                dr1["condate"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["confirmd"]; ;
                dr1["desig"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["desig"]; ;
                dr1["section"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["section"]; ;
                dt.Rows.Add(dr1);

            }
            ViewState["tblConfm"] = dt;
            this.Data_DataBind();


        }

        private void ShowConfmLtInfo()
        {
            ViewState.Remove("tblConfm");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mLNNo = "NEWCONFM";
            if (this.ddlPrevconfmlttNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;

                mLNNo = this.ddlPrevconfmlttNo.SelectedValue.ToString();


            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETCONFRMINFO", mLNNo, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ///////Here was
            ViewState["tblConfm"] = ds1.Tables[0];



            if (mLNNo == "NEWCONFM")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "LASTCONFMINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxcfmno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxcfmno1"].ToString().Substring(6);

                return;

            }


            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["confmno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["confmno1"].ToString().Substring(6, 5);

            this.Data_DataBind();
        }

        private void Data_DataBind()
        {

            this.gvConfmltr.DataSource = (DataTable)ViewState["tblConfm"];
            this.gvConfmltr.DataBind();
        }
        protected void gvConfmltr_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void lnkupdate_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string confmno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string entrydate = this.txtCurDate.Text;
            string refno = this.txtconfmRef.Text;
            DataTable dt = (DataTable)ViewState["tblConfm"];
            string empid = dt.Rows[0]["empid"].ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INSERTORUPDATECONFM", confmno,
               empid, entrydate, refno, "", "", "", "", "", "", "", "", "", "", "");



            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

        }

        private void GetPreConfmNo()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETPREVCONFMNO", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevconfmlttNo.DataTextField = "confmno1";
            this.ddlPrevconfmlttNo.DataValueField = "confmno";
            this.ddlPrevconfmlttNo.DataSource = ds1.Tables[0];
            this.ddlPrevconfmlttNo.DataBind();
        }

        protected void lbtnPrevconfmltNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreConfmNo();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            // string comcod = hst["comcod"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            ////string hostname = hst["hostname"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblConfm"];

            string date = this.txtCurDate.Text;
            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.RptConfmLt>();

            string bodytxt = "This has references to your appointment letter, " +
                             "We are pleased to inform you that,on successfully completion of your period of  probation ,your services in " + comnam + " have been confirmed " +
                             "effective form ";


            string bodynxt =
                "We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.";
            string gret = "Your Faithfully";


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_81_Rec.RptConfmlt", list, null, null);



            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("bodytxt", bodytxt));
            Rpt1.SetParameters(new ReportParameter("bodynxt", bodynxt));
            Rpt1.SetParameters(new ReportParameter("gret", gret));

            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("sub", "Subject : Confirmation Of Services."));
            Rpt1.SetParameters(new ReportParameter("hr", "HR Executive"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }
    }
}