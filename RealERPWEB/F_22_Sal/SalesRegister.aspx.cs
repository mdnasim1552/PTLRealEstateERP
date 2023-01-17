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
namespace RealERPWEB.F_22_Sal
{
    public partial class SalesRegister : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES REGISTER ";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ViewSection();



            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void ViewSection()
        {

            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "Sales":
                    this.GetProjectName();
                    this.GetDepartment();
                    this.txtSaleDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;

                    break;
                case "Management":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }




        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }




        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAMEOFIN", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            string DeptSearch = "%%";
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMKTACUSTDEPT", DeptSearch, "", "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "gdesc";
            this.ddlDepartment.DataValueField = "gcod";
            this.ddlDepartment.DataSource = ds4.Tables[0];
            this.ddlDepartment.DataBind();
            ds4.Dispose();


        }



        protected void CreateTable()
        {
            ViewState.Remove("tblsaleregis");
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("salesno", Type.GetType("System.String"));
            dttemp.Columns.Add("udesc", Type.GetType("System.String"));
            dttemp.Columns.Add("saledate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("saleamt", Type.GetType("System.Double"));
            dttemp.Columns.Add("deptcode", Type.GetType("System.String"));
            dttemp.Columns.Add("deptname", Type.GetType("System.String"));
            dttemp.Columns.Add("custname", Type.GetType("System.String"));
            dttemp.Columns.Add("executive", Type.GetType("System.String"));
            ViewState["tblsaleregis"] = dttemp;

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.PanelDetails.Visible = true;
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.CreateTable();


                return;
            }


            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.PanelDetails.Visible = false;
            this.gvSalReg.DataSource = null;
            this.gvSalReg.DataBind();
            this.txtsaleamt.Text = "";
            this.txtClient.Text = "";
            this.txtExecutive.Text = "";


        }
        protected void lblAddToTable_Click(object sender, EventArgs e)
        {
            string udesc = this.txtUnitName.Text.Trim();
            DataTable dt = (DataTable)ViewState["tblsaleregis"];
            string salesno = (dt.Rows.Count == 0) ? this.GetSaleNo() : this.IncrmentSaleNo();
            this.lblserialno.Text = salesno;

            DataRow[] dr1 = dt.Select("udesc = '" + udesc + "'"); //repchqno
            if (dr1.Length == 0)
            {

                DataRow dr2 = dt.NewRow();
                dr2["salesno"] = salesno;
                dr2["udesc"] = this.txtUnitName.Text.Trim();
                dr2["saledate"] = Convert.ToDateTime(this.txtSaleDate.Text.Trim()).ToString("dd-MMM-yyyy");
                dr2["saleamt"] = Convert.ToDouble("0" + this.txtsaleamt.Text.Trim());
                dr2["deptcode"] = this.ddlDepartment.SelectedValue.ToString();
                dr2["deptname"] = this.ddlDepartment.SelectedItem.Text.Trim(); ;
                dr2["custname"] = this.txtClient.Text.Trim();
                dr2["executive"] = this.txtExecutive.Text.Trim();
                dt.Rows.Add(dr2);
            }
            else
            {
                dr1[0]["saledate"] = Convert.ToDateTime(this.txtSaleDate.Text.Trim()).ToString("dd-MMM-yyyy");
                dr1[0]["saleamt"] = Convert.ToDouble("0" + this.txtsaleamt.Text.Trim());
                dr1[0]["deptcode"] = this.ddlDepartment.SelectedValue.ToString();
                dr1[0]["deptname"] = this.ddlDepartment.SelectedItem.Text.Trim(); ;
                dr1[0]["custname"] = this.txtClient.Text.Trim();
                dr1[0]["executive"] = this.txtExecutive.Text.Trim();



            }


            ViewState["tblsaleregis"] = dt;
            this.txtUnitName.Focus();
            this.Data_Bind();

        }

        private string GetSaleNo()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSALESNO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["salesno"].ToString();
        }


        private string IncrmentSaleNo()
        {
            //string isunum="000000000";
            string salesno = (Convert.ToInt32(this.lblserialno.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000" + salesno), 6));



        }



        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsaleregis"];
            string Type = Request.QueryString["Type"].ToString();

            switch (Type)
            {
                case "Sales":
                    this.gvSalReg.DataSource = tbl1;
                    this.gvSalReg.DataBind();
                    if (tbl1.Rows.Count > 0)
                    {
                        ((Label)this.gvSalReg.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(saleamt)", "")) ? 0.00
                            : tbl1.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0); -");
                    }

                    break;
                case "Management":

                    this.gvSalRegEdit.DataSource = tbl1;
                    this.gvSalRegEdit.DataBind();
                    if (tbl1.Rows.Count > 0)
                    {
                        ((Label)this.gvSalRegEdit.FooterRow.FindControl("txtFTotaledit")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(saleamt)", "")) ? 0.00
                            : tbl1.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0); -");
                    }


                    break;

            }







        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsaleregis"];
            for (int i = 0; i < this.gvSalReg.Rows.Count; i++)
            {
                tbl1.Rows[i]["saledate"] = ((TextBox)this.gvSalReg.Rows[i].FindControl("txtgvsaldate")).Text.Trim();
                tbl1.Rows[i]["saleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalReg.Rows[i].FindControl("txtgvsalamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["custname"] = ((TextBox)this.gvSalReg.Rows[i].FindControl("txtgvcustname")).Text.Trim();
                tbl1.Rows[i]["executive"] = ((TextBox)this.gvSalReg.Rows[i].FindControl("txtgvexecutive")).Text.Trim();
            }
            ViewState["tblsaleregis"] = tbl1;


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
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
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsaleregis"];
                bool result = true;
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string salesno = "";
                if (this.Request.QueryString["Type"] == "Sales")
                {
                    salesno = this.GetSaleNo();
                    this.lblserialno.Text = salesno;
                }


                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    //  string salesno = dt1.Rows[i]["salesno"].ToString(); 
                    string udesc = dt1.Rows[i]["udesc"].ToString(); // this.ddlpaytype.SelectedValue.ToString();repchqno
                    string saledate = Convert.ToDateTime(dt1.Rows[i]["saledate"].ToString()).ToString("dd-MMM-yyyy");
                    string deptcode = dt1.Rows[i]["deptcode"].ToString();
                    string custname = dt1.Rows[i]["custname"].ToString();
                    string executive = dt1.Rows[i]["executive"].ToString();
                    double saleamt = Convert.ToDouble(dt1.Rows[i]["saleamt"].ToString());
                    result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSORUPDATESALREGIS", salesno, pactcode, udesc, saledate, deptcode, custname, executive, saleamt.ToString(), "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }


                    if (this.Request.QueryString["Type"] == "Sales")
                    {
                        salesno = this.IncrmentSaleNo();
                        this.lblserialno.Text = salesno;
                    }
                }

                ((LinkButton)this.gvSalReg.FooterRow.FindControl("lbtnFinalUpdate")).Enabled = false;



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void ibtnFindSalesNo_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblsaleregis");
            string comcod = this.GetCompCode();
            string SelesNo = "%" + this.txtSrcSalesNo.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SHOWSALEREGISINFO", SelesNo, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalRegEdit.DataSource = null;
                this.gvSalRegEdit.DataBind();
                return;

            }
            ViewState["tblsaleregis"] = ds1.Tables[0];
            this.Data_Bind();


        }

        protected void ibtnSrchDept_Click(object sender, ImageClickEventArgs e)
        {

            string comcod = this.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvSalRegEdit.Rows[rowindex].FindControl("ddlDeptName");
            string SearchDept = "%" + ((TextBox)gvSalRegEdit.Rows[rowindex].FindControl("txtDeptSearch")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMKTACUSTDEPT", SearchDept, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "gdesc";
            ddl2.DataValueField = "gcod";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ds1.Dispose();
        }
        protected void gvSalRegEdit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvSalRegEdit.EditIndex = -1;
            this.Data_Bind();
        }

        protected void gvSalRegEdit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvSalRegEdit.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            int rowindex = (this.gvSalRegEdit.PageSize) * (this.gvSalRegEdit.PageIndex) + e.NewEditIndex;

            string deptcode = ((DataTable)ViewState["tblsaleregis"]).Rows[rowindex]["deptcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvSalRegEdit.Rows[e.NewEditIndex].FindControl("ddlDeptName");
            ViewState["gindex"] = e.NewEditIndex;

            string comcod = this.GetCompCode();
            string SearchDept = "%" + ((TextBox)gvSalRegEdit.Rows[e.NewEditIndex].FindControl("txtDeptSearch")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMKTACUSTDEPT", SearchDept, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "gdesc";
            ddl2.DataValueField = "gcod";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = deptcode;
        }
        protected void gvSalRegEdit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lblmsg02.Text = "You have no permission";
                return;
            }
            bool result = false;

            string comcod = this.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string salesno = ((Label)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("lbgvsalesnoedit")).Text;
            string pactcode = ((DataTable)ViewState["tblsaleregis"]).Rows[rowindex]["pactcode"].ToString();

            string udesc = ((TextBox)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("txtgvunitnameedit")).Text;
            string saledate = Convert.ToDateTime(((TextBox)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("txtgvsaldateedit")).Text).ToString("dd-MMM-yyyy");
            string deptcode = ((DropDownList)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("ddlDeptName")).SelectedValue.ToString();
            string custname = ((TextBox)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("txtgvcustnameedit")).Text;
            string executive = ((TextBox)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("txtgvexecutiveedit")).Text;
            string saleamt = Convert.ToDouble("0" + ((TextBox)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("txtgvsalamtedit")).Text).ToString();

            result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSORUPDATESALREGIS", salesno, pactcode, udesc, saledate, deptcode, custname, executive, saleamt.ToString(), "", "", "", "", "", "", "");


            if (!result)
            {
                this.lblmsg02.Text = "Updated Fail";
                return;
            }

            this.lblmsg02.Text = "Updated Successfully";
            DataTable dt = (DataTable)ViewState["tblsaleregis"];
            dt.Rows[rowindex]["udesc"] = udesc;
            dt.Rows[rowindex]["saledate"] = saledate;
            dt.Rows[rowindex]["deptcode"] = deptcode;
            dt.Rows[rowindex]["deptname"] = ((DropDownList)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("ddlDeptName")).SelectedItem.ToString();
            dt.Rows[rowindex]["custname"] = custname;
            dt.Rows[rowindex]["executive"] = executive;
            dt.Rows[rowindex]["saleamt"] = saleamt;
            ViewState["tblsaleregis"] = dt;
            this.gvSalRegEdit.EditIndex = -1;
            this.Data_Bind();

        }
        protected void gvSalRegEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();

            DataTable dt = (DataTable)ViewState["tblsaleregis"];
            string salesno = ((Label)this.gvSalRegEdit.Rows[e.RowIndex].FindControl("lbgvsalesnoedit")).Text;
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETESALESNO", salesno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvSalRegEdit.PageSize) * (this.gvSalRegEdit.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblsaleregis");
            ViewState["tblsaleregis"] = dv.ToTable();
            this.Data_Bind();



        }

        protected void gvSalRegEdit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalRegEdit.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}