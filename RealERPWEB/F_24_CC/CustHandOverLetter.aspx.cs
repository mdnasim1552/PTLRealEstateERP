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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class CustHandOverLetter : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Handover Letter Information";

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetProjectName();
                this.GetUnitName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "GetPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        private void GetUnitName()
        {

            ViewState.Remove("tblcusaunit");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSUnit = this.txtsrchUnitName.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "GetUNITNAME", pactcode, txtSUnit, "", "", "", "", "", "", "");
            this.ddlUnitName.DataTextField = "uacustname";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds1.Tables[0];
            this.ddlUnitName.DataBind();
            ViewState["tblcusaunit"] = ds1.Tables[0];


        }

        protected void ibtnType_Click(object sender, EventArgs e)
        {
            //this.GetType();
        }



        //private void PreviousAddNumber() 
        //{
        //    string comcod = this.GetCompCode();
        //    string curdate = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
        //    DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREADWORK", curdate, "", "", "", "", "", "", "", "");
        //    this.ddlPrevADNumber.DataTextField = "adno1";
        //    this.ddlPrevADNumber.DataValueField = "adno";
        //    this.ddlPrevADNumber.DataSource = ds1.Tables[0];
        //    this.ddlPrevADNumber.DataBind();
        //    ds1.Dispose();

        //}
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;
            this.GetProjectName();
        }

        protected void ImgbtnFindPreviousList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "GETPREHLET", curdate, "", "", "", "", "", "", "", "");
            this.ddlPrevList.DataTextField = "hletno1";
            this.ddlPrevList.DataValueField = "hletno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
            ds1.Dispose();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlProjectName.Enabled = false;
                this.ddlUnitName.Enabled = false;
                this.lblPreList.Visible = false;
                this.lblPreList.Visible = false;
                this.txtSrchPreviousList.Visible = false;
                this.ImgbtnFindPreviousList.Visible = false;
                this.ddlPrevList.Visible = false;
                this.ShowHLTInfo();
                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Enabled = true;
            this.ddlUnitName.Enabled = true;
            this.lblPreList.Visible = true;
            this.lblPreList.Visible = true;
            this.txtSrchPreviousList.Visible = true;
            this.ImgbtnFindPreviousList.Visible = true;
            this.ddlPrevList.Visible = true;
            this.ddlPrevList.Items.Clear();
            this.gvHLET.DataSource = null;
            this.gvHLET.DataBind();


            //this.ddlProjectName.Visible = true;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            //this.ddlUnitName.Visible = true ;
            //this.lblUnitName.Text = "";
            //this.lblUnitName.Visible = false;
            //this.ddlPrevADNumber.Items.Clear();
            //this.lblPrevious.Visible = true ;
            //this.txtPreAdNo.Visible = true;
            //this.ibtnPreAdNo.Visible = true;
            //this.ddlPrevADNumber.Visible = true;
            //this.PanelItem.Visible = false;
            //this.gvAddWork.DataSource = null;
            //this.gvAddWork.DataBind();
            //this.ddlItemName.Items.Clear();
            //((Label)this.Master.FindControl("lblmsg")).Text = "";
            //this.ddlType.Enabled = true;
            //this.lblSchCode.Text = "";

        }

        protected void GetHLTNumber()
        {
            string comcod = this.GetCompCode();
            string mhltNUmber = "NEWHLT";
            if (this.ddlPrevList.Items.Count > 0)
                mhltNUmber = this.ddlPrevList.SelectedValue.ToString();

            string CurDate1 = this.txtCurDate.Text.Trim();
            if (mhltNUmber == "NEWHLT")
            {
                DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "LASTHLTNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxhltno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxhltno1"].ToString().Substring(6, 5);

                    this.ddlPrevList.DataTextField = "maxhltno1";
                    this.ddlPrevList.DataValueField = "maxhltno";
                    this.ddlPrevList.DataSource = ds2.Tables[0];
                    this.ddlPrevList.DataBind();
                }

                ds2.Dispose();
            }

        }

        private void ShowHLTInfo()
        {

            ViewState.Remove("tblhletinfo");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mHltNo = "NEWHLT";
            if (this.ddlPrevList.Items.Count > 0)
                mHltNo = this.ddlPrevList.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "GETHLTINFO", mHltNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblhletinfo"] = this.HiddenSameData(ds1.Tables[0]);


            if (mHltNo == "NEWHLT")
            {


                DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "LASTHLTNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxhltno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxhltno1"].ToString().Substring(6, 5);
                this.GetHLET();
                return;
            }


            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["hletno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["hletno1"].ToString().Substring(6, 5);
            this.txtRefNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["hletdate"]).ToString("dd-MMM-yyyy");

            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            this.ddlUnitName.SelectedValue = ds1.Tables[1].Rows[0]["usircode"].ToString();
            this.Data_DataBind();



        }
        private void GetHLET()
        {
            string comcod = this.GetCompCode();

            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "GETHLET", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvHLET.DataSource = null;
                this.gvHLET.DataBind();
                return;


            }

            ViewState["tblhletinfo"] = this.HiddenSameData(ds3.Tables[0]);
            this.Data_DataBind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;

            string mgcod = dt1.Rows[0]["mgcod"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgcod"].ToString() == mgcod)
                    dt1.Rows[j]["mgdesc"] = "";

                mgcod = dt1.Rows[j]["mgcod"].ToString();
            }
            return dt1;

        }

        private void Data_DataBind()
        {

            this.gvHLET.DataSource = (DataTable)ViewState["tblhletinfo"];
            this.gvHLET.DataBind();


        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblhletinfo"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptHandOverWork", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Handover Letter"));
            Rpt1.SetParameters(new ReportParameter("projectName", "Project : " + this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("flatNo", "Flat No : " + (((DataTable)ViewState["tblcusaunit"]).Select("usircode='" + this.ddlUnitName.SelectedValue.ToString() + "'"))[0]["unitname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("custName", "Name of Owner : " + (((DataTable)ViewState["tblcusaunit"]).Select("usircode='" + this.ddlUnitName.SelectedValue.ToString() + "'"))[0]["custname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date : " + Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptImp = new RealERPRPT.R_24_CC.RptHandOverWork();
            //TextObject txtCompany = rptImp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtPrjName.Text ="Project : "+ this.ddlProjectName.SelectedItem.Text;
            //TextObject txtFlatNo = rptImp.ReportDefinition.ReportObjects["txtFlatNo"] as TextObject;
            //txtFlatNo.Text ="Flat No : "+ (((DataTable)ViewState["tblcusaunit"]).Select("usircode='" + this.ddlUnitName.SelectedValue.ToString() + "'"))[0]["unitname"].ToString();
            //TextObject txtCustName = rptImp.ReportDefinition.ReportObjects["txtCustName"] as TextObject;
            //txtCustName.Text = "Name of Owner : "+(((DataTable)ViewState["tblcusaunit"]).Select("usircode='" + this.ddlUnitName.SelectedValue.ToString() + "'"))[0]["custname"].ToString();
            //TextObject rpttxtDate = rptImp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text = "Date : " + Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            ////TextObject rpttxtAddNo = rptImp.ReportDefinition.ReportObjects["txtAddNo"] as TextObject;
            ////rpttxtAddNo.Text = "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptImp.SetDataSource( (DataTable)ViewState["tblhletinfo"]);
            //Session["Report1"] = rptImp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ibtnFindUnitName_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string Unitcode=this.ddlUnitName.SelectedValue.ToString();
            //DataRow[] dr = ((DataTable)Session["tblunit"]).Select("usircode='" + Unitcode + "'");
            //if (dr.Length > 0) 
            //{
            //    this.lblCustomerName.Text = dr[0]["custname"].ToString();
            //    return;
            //}
            //this.lblCustomerName.Text = "";
        }
        protected void ibtnPreAdNo_Click(object sender, EventArgs e)
        {
            //this.PreviousAddNumber();
        }
        protected void ibtnFindAdWork_Click(object sender, EventArgs e)
        {
            //this.GetItemName();
        }

        private void SaveValue()
        {
            DataTable dt1 = (DataTable)ViewState["tblhletinfo"];
            int TblRowIndex;
            for (int i = 0; i < this.gvHLET.Rows.Count; i++)
            {
                TblRowIndex = (gvHLET.PageIndex) * gvHLET.PageSize + i;
                string complete = (((CheckBox)this.gvHLET.Rows[i].FindControl("chkcomplete")).Checked) ? "True" : "False";
                string incomplete = (((CheckBox)this.gvHLET.Rows[i].FindControl("chkincomplete")).Checked) ? "True" : "False";
                string signowner = ((TextBox)this.gvHLET.Rows[i].FindControl("txtgvsign")).Text.Trim();






                dt1.Rows[TblRowIndex]["complete"] = complete;
                dt1.Rows[TblRowIndex]["incomplete"] = incomplete;
                dt1.Rows[TblRowIndex]["signowner"] = signowner;
            }
            ViewState["tblhletinfo"] = dt1;
            this.Data_DataBind();
        }



        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlUnitName.Text.Trim();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            string refno = this.txtRefNo.Text;
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblhletinfo"];

            if (this.ddlPrevList.Items.Count == 0)
                this.GetHLTNumber();
            string hletno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();


            result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "INOUPHLETINF", "HLETINFB", hletno, curdate, PactCode, Usircode, refno, "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            foreach (DataRow dr in dt.Rows)
            {
                string Gcode = dr["gcod"].ToString();
                string complete = dr["complete"].ToString();
                string incomplete = dr["incomplete"].ToString();
                string signowner = dr["signowner"].ToString();


                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALMGTHLT", "INOUPHLETINF", "HLETINFA", hletno, Gcode, complete, incomplete, signowner, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }








    }
}