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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpPro : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetPromotionNo();
                this.GetCompany();
                this.GetDepartName();
                this.GetDesignation();
                ibtnEmpList_Click(null,null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPreProNm()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mProNo = "NEWPRO";
            if (this.ddlPrevProList.Items.Count > 0)
                mProNo = this.ddlPrevProList.SelectedValue.ToString();

            string mProDAT = this.txtCurDate.Text.Trim();

            //Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            if (mProNo == "NEWPRO")
            {
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", mProDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mProNo = ds2.Tables[0].Rows[0]["maxprono"].ToString();
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6, 5);
                    this.ddlPrevProList.DataTextField = "maxprono1";
                    this.ddlPrevProList.DataValueField = "maxprono";
                    this.ddlPrevProList.DataSource = ds2.Tables[0];
                    this.ddlPrevProList.DataBind();
                }
            }
        }

        private void GetPromotionNo()
        {

            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6);
        }
        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        private void GetDepartName()
        {
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string txtDeptname = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtDeptname, company, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "deptname";
            this.ddlDepartment.DataValueField = "deptid";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);

        }
        private void GetEmplist()
        {
            Session.Remove("tblempdsg");
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "94%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "94%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string txtEmpname = this.ddlEmpList.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", deptid, txtEmpname, company, "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            Session["tblempdsg"] = ds1.Tables[0];
            this.ddlEmpList_SelectedIndexChanged(null, null);

        }
        private void GetPreProlist()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevPromotion", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevProList.DataTextField = "prono1";
            this.ddlPrevProList.DataValueField = "prono";
            this.ddlPrevProList.DataSource = ds1.Tables[0];
            this.ddlPrevProList.DataBind();
        }
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempdsg"];
            string empid = this.ddlEmpList.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                this.lblDesig.Text = "";
                return;

            }
            this.lblDesig.Text = dr[0]["desig"].ToString();
        }
        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            string txtsrchdesg = this.ddlDesig.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDESIG", txtsrchdesg, "", "", "", "", "", "", "", "");
            this.ddlDesig.DataTextField = "desig";
            this.ddlDesig.DataValueField = "desigid";
            this.ddlDesig.DataSource = ds1.Tables[0];
            this.ddlDesig.DataBind();



        }
        protected void ibtnDepartment_Click(object sender, EventArgs e)
        {
            this.GetDepartName();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ibtnEmpList_Click(null, null);

        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }

        protected void ibtnDesg_Click(object sender, EventArgs e)
        {
            this.GetDesignation();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblpro"];
            string date = Convert.ToDateTime(this.txtCurDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptemppro = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptEmpPromotion();
            TextObject rptdate = rptemppro.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtCurDate.Text).ToString("MMMM dd, yyyy");
            TextObject rpttrnno = rptemppro.ReportDefinition.ReportObjects["txttrnsno"] as TextObject;
            rpttrnno.Text = "Promotion No: " + this.lblCurNo1.Text.Trim() + "-" + this.lblCurNo2.Text.Trim();
            TextObject rpttxtinformation = rptemppro.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
            rpttxtinformation.Text = "Management has decided to promotion you in the following Designation.";


            TextObject txtRemarks = rptemppro.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            txtRemarks.Text = this.txtRemarks.Text.Trim();
            rptemppro.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptemppro.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptemppro;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lbtnPrevProList_Click(object sender, EventArgs e)
        {
            this.GetPreProlist();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlprj.Visible = true;
                this.PnlProRemarks.Visible = true;
                this.lbtnPrevProList.Visible = false;
                this.ddlPrevProList.Visible = false;
                //this.txtCurDate.Enabled = true;
                this.ShowPromotion();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.txtRemarks.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.ddlPrevProList.Items.Clear();
            this.lbtnPrevProList.Visible = true;
            this.ddlPrevProList.Visible = true;
            this.pnlprj.Visible = false;
            this.PnlProRemarks.Visible = false;
            this.gvremppro.DataSource = null;
            this.txtCurDate.Enabled = true;
            this.gvremppro.DataBind();
        }

        private void ShowPromotion()
        {

            ViewState.Remove("tblpro");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mProNo = "NEWPRO";
            if (this.ddlPrevProList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mProNo = this.ddlPrevProList.SelectedValue.ToString();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROINFO", mProNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblpro"] = ds1.Tables[0];


            if (mProNo == "NEWPRO")
            {
                ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTOPROMOTIONNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxprono1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxprono1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["prono1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prodate"]).ToString("dd-MMM-yyyy");
            this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();

            this.Data_DataBind();



        }
        private void Data_DataBind()
        {

            this.gvremppro.DataSource = (DataTable)ViewState["tblpro"];
            this.gvremppro.DataBind();

        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblpro"];
            string empid = this.ddlEmpList.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["sectionid"] = this.ddlDepartment.SelectedValue.ToString();
                dr1["section"] = this.ddlDepartment.SelectedItem.Text.Substring(14);
                dr1["empid"] = empid;
                dr1["empname"] = this.ddlEmpList.SelectedItem.Text.Trim();
                dr1["idcardno"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["idcardno"];
                dr1["pdesigid"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["desigid"];
                dr1["rdesigid"] = this.ddlDesig.SelectedValue.ToString();
                dr1["pdesig"] = this.lblDesig.Text.Trim();
                dr1["rdesig"] = this.ddlDesig.SelectedItem.Text.Trim();
                dt.Rows.Add(dr1);
            }
            else
            {

                dr[0]["rdesigid"] = this.ddlDesig.SelectedValue.ToString();
                dr[0]["rdesig"] = this.ddlDesig.SelectedItem.Text.Trim();
                ViewState["tblpro"] = dt;
            }
            this.Data_DataBind();


        }


        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {

                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblpro"];
                if (ddlPrevProList.Items.Count == 0)
                {
                    this.GetPreProNm();
                }
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string Remarks = this.txtRemarks.Text.Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string pdesigid = dt.Rows[i]["pdesigid"].ToString();
                    string rdesigid = dt.Rows[i]["rdesigid"].ToString();
                    string remarks = dt.Rows[i]["rmrks"].ToString();
                    string rdesig = dt.Rows[i]["rdesig"].ToString();
                    string sectionid = dt.Rows[i]["sectionid"].ToString();

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPDATEEMPPRO", prono, empid, curdate, pdesigid, rdesigid,
                        Remarks, rdesig, sectionid, "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
                msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }
            catch (Exception ex)
            {
                msg = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }

        }



        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartName();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
    }
}