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
namespace RealERPWEB.F_29_Fxt
{
    public partial class RptdeptITStock : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "IT Stock Reports";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetCompanyName();


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            if (ds5.Tables[0].Rows.Count == 0)
                return;
            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            Session["tblcompany"] = ds5.Tables[0];
            this.GetDepartment();
            // this.ddlCompanyAgg_SelectedIndexChanged(null, null);
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = this.txtsrchdeptagg.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "GETDEPARTMENT", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddldepartmentagg.DataTextField = "actdesc";
            this.ddldepartmentagg.DataValueField = "actcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string deptcode = ((this.ddldepartmentagg.SelectedValue == "000000000000") ? "%" : this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 8)) + "%";
            string txtSProject = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "GETSECTION", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }

        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.ddlProjectName_SelectedIndexChanged(null,null);
        }

        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);

            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CompanyName = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2) + "%";
            string projectcode = ((this.ddldepartmentagg.SelectedValue.ToString() == "000000000000") ? "" : this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "RPTITSTOCKREPORT", frmdate, CompanyName, projectcode, section, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDeptITStock.DataSource = null;
                this.gvDeptITStock.DataBind();
                return;
            }

            Session["tblStock"] = HiddenSameData(ds1.Tables[0]);
            Session["tblmatdesc"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string companycode = "";
            string deptno = "";

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["companycode"].ToString() == companycode && dt1.Rows[j]["deptno"].ToString() == deptno)
                {
                    companycode = dt1.Rows[j]["companycode"].ToString();
                    deptno = dt1.Rows[j]["deptno"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["companycode"].ToString() == companycode)
                    {
                        dt1.Rows[j]["companyname"] = "";
                    }

                    if (dt1.Rows[j]["deptno"].ToString() == deptno)
                    {
                        dt1.Rows[j]["deptname"] = "";

                    }
                    companycode = dt1.Rows[j]["companycode"].ToString();
                    deptno = dt1.Rows[j]["deptno"].ToString();
                }
            }

            return dt1;
        }



        private void Data_Bind()
        {
            int i, j;
            for (i = 8; i < this.gvDeptITStock.Columns.Count - 1; i++)
                this.gvDeptITStock.Columns[i].Visible = false;
            j = 8;
            DataTable dtp = (DataTable)Session["tblmatdesc"];
            for (i = 0; i < dtp.Rows.Count; i++)
            {

                this.gvDeptITStock.Columns[j].Visible = true;
                this.gvDeptITStock.Columns[j].HeaderText = dtp.Rows[i]["resdesc"].ToString();


                j++;
            }


            this.gvDeptITStock.DataSource = (DataTable)Session["tblStock"];
            this.gvDeptITStock.DataBind();
            this.FooterCalCulation();

        }


        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)Session["tblStock"];
            if (dt.Rows.Count == 0)
            {
                return;
            }

            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0.00 : dt.Compute("sum(p1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0.00 : dt.Compute("sum(p2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0.00 : dt.Compute("sum(p3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0.00 : dt.Compute("sum(p4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0.00 : dt.Compute("sum(p5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0.00 : dt.Compute("sum(p6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0.00 : dt.Compute("sum(p7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0.00 : dt.Compute("sum(p8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0.00 : dt.Compute("sum(p9)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0.00 : dt.Compute("sum(p10)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0.00 : dt.Compute("sum(p11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0.00 : dt.Compute("sum(p12)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0.00 : dt.Compute("sum(p13)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0.00 : dt.Compute("sum(p14)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0.00 : dt.Compute("sum(p15)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0.00 : dt.Compute("sum(p16)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0.00 : dt.Compute("sum(p17)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0.00 : dt.Compute("sum(p18)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0.00 : dt.Compute("sum(p19)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFstockp20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0.00 : dt.Compute("sum(p20)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDeptITStock.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ? 0.00 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");
            Session["Report1"] = gvDeptITStock;
            ((HyperLink)this.gvDeptITStock.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblStock"];
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.RptDeptITStock>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_29_Fxt.RptDeptITStock", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "IT STOCK REPORTS"));
            int j = 1;
            DataTable dtp = (DataTable)Session["tblmatdesc"];
            for (int i = 0; i < dtp.Rows.Count; i++)
            {

                Rpt1.SetParameters(new ReportParameter("txtDesc" + j, dtp.Rows[i]["resdesc"].ToString()));
                j++;
            }
            Rpt1.SetParameters(new ReportParameter("txtDate", date));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {

        }



        private void GetEmployeeDetails(string empid, string deptno)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "GETFTXEMPLOYEEDETAILS", deptno, empid, "", "", "", "", "", "", "");
            Session["tblempmaterial"] = (ds1.Tables[0]);

            this.Data_Bind02();



        }

        private void Data_Bind02()
        {
            DataTable dt = (DataTable)Session["tblempmaterial"];
            this.gvMatDetails.DataSource = dt;
            this.gvMatDetails.DataBind();
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);

            this.FooterCal();


        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblempmaterial"];
            if (dt.Rows.Count == 0)
            {
                return;
            }

            ((Label)this.gvMatDetails.FooterRow.FindControl("gvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0;(#,##0); ");


        }


        protected void btngvempName_Click(object sender, EventArgs e)
        {
            // this.lbmodalheading.Text = "Individual Monthly Over Time Details Information";
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int rownumber = this.gvDeptITStock.PageSize * this.gvDeptITStock.PageIndex + RowIndex;
            string empid = ((DataTable)Session["tblStock"]).Rows[RowIndex]["empid"].ToString();
            string deptcode = ((DataTable)Session["tblStock"]).Rows[RowIndex]["deptno"].ToString();


            this.GetEmployeeDetails(empid, deptcode);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }
        protected void btnIssueno_Click(object sender, EventArgs e)
        {
            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            DataTable dt = (DataTable)Session["tblempmaterial"];
            string comcod = this.GetComeCode();


            string issueno = dt.Rows[rownum]["issueno"].ToString();
            string deptno = dt.Rows[rownum]["deptno"].ToString();
            string rsircode = dt.Rows[rownum]["rsircode"].ToString();
            string empid = dt.Rows[rownum]["empid"].ToString();



            bool result = HRData.UpdateTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "DELETEISSUENO", issueno, deptno, rsircode, empid, "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Main Head";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal();", true);

            }


            DataView dv = dt.DefaultView;
            Session.Remove("tblempmaterial");
            Session["tblempmaterial"] = dv.ToTable();
            this.Data_Bind02();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
            this.lblheadermsg.Text = "delete Successfully";
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblempmaterial"];
            int rowindex;

            for (int i = 0; i < this.gvMatDetails.Rows.Count; i++)
            {

                double gvtxtqty = Convert.ToDouble("0" + ((TextBox)this.gvMatDetails.Rows[i].FindControl("gvtxtqty")).Text.Trim());


                rowindex = (this.gvMatDetails.PageSize) * (this.gvMatDetails.PageIndex) + i;
                dt.Rows[rowindex]["qty"] = gvtxtqty;

            }

            Session["tblempmaterial"] = dt;
        }

        protected void lbtnUpdatMat_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblempmaterial"];
            string comcod = this.GetComeCode();
            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string issueno = dt.Rows[i]["issueno"].ToString();
                string deptno = dt.Rows[i]["deptno"].ToString();
                string rsircode = dt.Rows[i]["rsircode"].ToString();
                string empid = dt.Rows[i]["empid"].ToString();
                string issuedate = dt.Rows[i]["issuedate"].ToString();

                string qty = dt.Rows[i]["qty"].ToString();


                result = HRData.UpdateTransInfo(comcod, "SP_REPORT_FXTASSET_STOCK", "INSERTORUPDATE_IssueNo", issueno, deptno, rsircode, empid, issuedate, qty, "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;


            }

            this.Data_Bind02();

            this.lblheadermsg.Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal();", true);
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);


        }
        protected void btnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind02();


        }
    }
}
