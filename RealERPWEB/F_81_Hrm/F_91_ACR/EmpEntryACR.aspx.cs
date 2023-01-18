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
namespace RealERPWEB.F_81_Hrm.F_91_ACR
{
    public partial class EmpEntryACR : System.Web.UI.Page
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

                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.GetCompany();
                //this.SectionName();
                //this.GetEmployeeName();
            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }

        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            string Section = this.ddlSectionName.SelectedValue.ToString() + "%";
            string txtSProject = "%" + this.txtEmpSrc.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETEMPTNAME", Section, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblACREmp"] = ds3.Tables[0];
            this.ddlEmpName_SelectedIndexChanged(null, null);

        }

        protected void ibtnEmpList_Click(object sender, ImageClickEventArgs e)
        {
            if (this.infoOk.Text == "New")
                return;
            this.GetEmployeeName();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            //string company = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds = this.PreAcrInfo();
            DataTable dt1 = ds.Tables[0];
            string date = Convert.ToDateTime(dt1.Rows[0]["acrdate"]).ToString("dd MMMM, yyyy");
            ReportDocument rptempacr = new RealERPRPT.R_81_Hrm.R_91_ACR.RptEmpACR();
            TextObject rptdate = rptempacr.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
            TextObject rptCompName = rptempacr.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCompName.Text = this.ddlCompany.SelectedItem.Text;
            TextObject rptSec = rptempacr.ReportDefinition.ReportObjects["txtSec"] as TextObject;
            rptSec.Text = this.lblSec.Text;
            TextObject rptSalary = rptempacr.ReportDefinition.ReportObjects["txtSal"] as TextObject;
            rptSalary.Text = "Salary: " + this.lbltotalsal.Text + " /-";
            TextObject rptNaDeg = rptempacr.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            rptNaDeg.Text = this.lblEmp.Text + " - " + this.lblDesignation.Text;
            TextObject rptacrno = rptempacr.ReportDefinition.ReportObjects["txtacrsno"] as TextObject;
            rptacrno.Text = "ACR No: " + this.lblCurTransNo1.Text.Trim() + "" + this.txtCurTransNo2.Text.Trim();
            rptempacr.SetDataSource(dt1);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempacr.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempacr;

            lbljavascript.Text = "<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");

            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }
        private void Get_ACRsno()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string aCRNo = "NEWACR";
            if (this.ddlPrevISSList.Items.Count > 0)
                aCRNo = this.ddlPrevISSList.SelectedValue.ToString();

            string date = this.GetStdDate(this.txtCurTransDate.Text);
            if (aCRNo == "NEWACR")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "LASTACRNO", date, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    aCRNo = ds3.Tables[0].Rows[0]["maxacrno"].ToString();
                    this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxacrdt"].ToString().Trim()).ToString("dd.MM.yyyy");
                    this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxacrno1"].ToString().Substring(0, 6);
                    this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxacrno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxacrno1";
                    this.ddlPrevISSList.DataValueField = "maxacrno";
                    this.ddlPrevISSList.DataSource = ds3.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }

        }

        private void ShowACRInfo()
        {

            string comcod = this.GetComeCode();
            string section = this.ddlSectionName.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "EMPACRINFO", section, empid, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblACR"] = this.HiddenSameData(ds2.Tables[0]);
            Session["tblData"] = ds2.Tables[1];
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                    grp = dt1.Rows[j]["grp"].ToString();
            }

            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblACR"];
            this.gvACREntry.DataSource = dt;
            this.gvACREntry.DataBind();
            this.ShowSalAllow();
        }

        protected void infoOk_Click(object sender, EventArgs e)
        {
            if (infoOk.Text.Trim() == "Ok")
            {

                this.infoOk.Text = "New";

                this.txtCurTransDate.Enabled = true;
                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    DataSet ds = this.PreAcrInfo();
                    Session["tblACR"] = this.HiddenSameData(ds.Tables[0]);
                    Session["tblData"] = ds.Tables[1];
                    this.ddlCompany.SelectedValue = ds.Tables[0].Rows[0]["compname"].ToString();
                    this.lblSec.Text = ds.Tables[0].Rows[0]["secname"].ToString();
                    this.lblEmp.Text = ds.Tables[0].Rows[0]["empname"].ToString();
                    this.lblDesignation.Text = ds.Tables[0].Rows[0]["desg"].ToString();
                    //this.ddlEmpName.Visible = false;
                    //this.ddlSectionName.Visible = false;

                    this.Data_Bind();
                    this.Load_Cur_Trans_NO();
                    //this.Get_ACRsno();

                }
                else
                {
                    this.Get_ACRsno();
                    this.ShowACRInfo();
                    this.lblSec.Text = this.ddlSectionName.SelectedItem.Text;
                    this.lblEmp.Text = this.ddlEmpName.SelectedItem.Text;

                }

                this.ddlEmpName.Visible = false;
                this.ddlSectionName.Visible = false;
                this.lblSec.Visible = true;
                this.lblEmp.Visible = true;
                this.lbPrAcr.Visible = false;
                this.ddlPrevISSList.Visible = false;
                this.txtPreNum.Visible = false;
                this.imgBtnPreAcr.Visible = false;
                this.lblDesignation.Visible = true;
                //this.ddlEmpName.Enabled = false;
                this.ddlCompany.Enabled = false;
                //this.ddlSectionName.Enabled = false;
                this.lbSection.Enabled = false;
                this.txtSection.Enabled = false;
                this.imgSecSearch.Enabled = false;
                this.lblSec.Enabled = false;
                this.lblEmp.Enabled = false;
                this.lbltxtTotalSal.Visible = true;

            }
            else
            {
                //this.lblSec.Enabled = true;
                //this.lblDesignation.Text = "";
                this.lblSec.Visible = false;
                this.lblEmp.Visible = false;
                this.lblDesignation.Visible = true;
                this.ddlEmpName.Visible = true;
                this.ddlSectionName.Visible = true;

                this.ddlPrevISSList.Items.Clear();
                this.lbPrAcr.Visible = true;
                this.ddlPrevISSList.Visible = true;
                Session["tblACR"] = null;
                this.gvACREntry.DataSource = null;
                this.gvACREntry.DataBind();
                this.infoOk.Text = "Ok";
                this.txtCurTransDate.Enabled = true;
                //this.ddlEmpName.Enabled = true;
                this.txtPreNum.Visible = true;
                this.imgBtnPreAcr.Visible = true;
                this.ddlCompany.Enabled = true;
                //this.ddlSectionName.Enabled = true;
                this.lbSection.Enabled = true;
                this.txtSection.Enabled = true;
                this.imgSecSearch.Enabled = true;
                this.lblSec.Enabled = true;
                this.lblEmp.Enabled = true;
                this.lbltxtTotalSal.Visible = false;
                this.lbltotalsal.Text = "";


            }

        }
        private void ShowSalAllow()
        {
            double SalAdd = 0, SallSub = 0;
            Session.Remove("tblsaladd");
            Session.Remove("tblsalsub");
            Session.Remove("tblallowadd");
            Session.Remove("tblallowsub");
            DataTable dtr = (DataTable)Session["tblData"];
            DataView dvr = new DataView();
            DataTable dtr1 = new DataTable();

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '040%'");
            dtr1 = dvr.ToTable();
            Session["tblsaladd"] = dtr1;
            SalAdd = Convert.ToDouble((Convert.IsDBNull(dtr1.Compute("sum(gval)", "")) ? 0.00 : dtr1.Compute("sum(gval)", "")));

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '041%'");
            dtr1 = dvr.ToTable();
            Session["tblsalsub"] = dtr1;
            SallSub = Convert.ToDouble((Convert.IsDBNull(dtr1.Compute("sum(gval)", "")) ? 0.00 : dtr1.Compute("sum(gval)", "")));

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '070%'");
            dtr1 = dvr.ToTable();
            Session["tblallowadd"] = dtr1;

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '071%'");
            dtr1 = dvr.ToTable();
            Session["tblallowsub"] = dtr1;

            this.lbltotalsal.Text = (SalAdd - SallSub).ToString("#,##0;(#,##0); ");


        }

        protected DataSet PreAcrInfo()
        {
            string acrno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "PrevACRInfo", acrno, "", "", "", "", "", "", "", "");
            return ds;


        }
        protected void imgbtnCompany_Click(object sender, ImageClickEventArgs e)
        {
            if (this.infoOk.Text == "New")
                return;
            this.GetCompany();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void lUpdatACRInfo_Click(object sender, EventArgs e)
        {

            if (ddlPrevISSList.Items.Count == 0)
            {
                this.Get_ACRsno();
            }
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            string acrno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + date.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSERTORUPDATEACRINFB", acrno, date, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
                return;


            for (int i = 0; i < this.gvACREntry.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvACREntry.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvACREntry.Rows[i].FindControl("lgvgval")).Text.Trim();
                string gdesc = ((TextBox)this.gvACREntry.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string evl1 = ((TextBox)this.gvACREntry.Rows[i].FindControl("txtgvEv1")).Text.Trim();
                string evl2 = ((TextBox)this.gvACREntry.Rows[i].FindControl("txtgvEv2")).Text.Trim();
                string evl3 = ((TextBox)this.gvACREntry.Rows[i].FindControl("txtgvEv3")).Text.Trim();

                HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSERTORUPDATEACRINFA", acrno, empid, Gcode, gdesc, evl1, evl2, evl3, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void Load_Prev_ACR_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GetPrevACRList", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevISSList.DataTextField = "acrno1";
            this.ddlPrevISSList.DataValueField = "acrno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblACREmp"];
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                this.lblDesignation.Text = "";
                return;

            }
            this.lblDesignation.Text = dr[0]["desig"].ToString();
        }
        private void SectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = this.txtSection.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETSECNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlSectionName.DataTextField = "actdesc";
            this.ddlSectionName.DataValueField = "actcode";
            this.ddlSectionName.DataSource = ds4.Tables[0];
            this.ddlSectionName.DataBind();
            this.ddlSectionName_SelectedIndexChanged(null, null);

        }
        protected void imgSecSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.infoOk.Text == "New")
                return;
            this.SectionName();

        }
        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgBtnPreAcr_Click(object sender, ImageClickEventArgs e)
        {
            if (this.infoOk.Text == "New")
                return;
            this.Load_Prev_ACR_List();
        }
    }
}