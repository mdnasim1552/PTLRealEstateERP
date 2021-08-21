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
    public partial class LinkMktEntryUnit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this.gvUnit.Columns[1].Visible = (Convert.ToBoolean(dr1[0]["entry"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "UNIT FIXATION INFORMATION ";
                this.GetProjectName();
                this.GetGroup();
                this.ChangeName();
                this.lbtnOk_Click(null, null);

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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtSProject = Request.QueryString["pactcode"].ToString() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        private void GetGroup()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETGROUP", "", "", "", "", "", "", "", "", "");
            this.ddlGroup.DataTextField = "grpdesc";
            this.ddlGroup.DataValueField = "grpcode";
            this.ddlGroup.DataSource = ds1.Tables[0];
            this.ddlGroup.DataBind();
            this.ddlGroup_SelectedIndexChanged(null, null);
        }

        private void ChangeName()
        {
            string comcod = this.GetCompCode();
            this.gvUnit.Columns[8].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Parking";
            this.gvUnit.Columns[12].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Qty" : "Apt. Qty";
            this.gvUnit.Columns[13].Visible = (ASTUtility.Left(comcod, 1) == "2") ? false : true;
            this.gvUnit.Columns[14].Visible = (ASTUtility.Left(comcod, 1) == "2") ? false : true;
        }

        private void GetFloor()
        {

            string comcod = this.GetCompCode();
            string grpcode = this.ddlGroup.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETFLOORCODE", grpcode, "", "", "", "", "", "", "", "");
            this.ddlFloor.DataTextField = "flrdesc";
            this.ddlFloor.DataValueField = "flrcode";
            this.ddlFloor.DataSource = ds1.Tables[0];
            this.ddlFloor.DataBind();
            this.ddlFloor_SelectedIndexChanged(null, null);

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;

            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {

                this.PanelGroup.Visible = true;
                //this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.Request.QueryString["pactcode"].ToString() + "-" + this.Request.QueryString["pactdesc"].ToString();

                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                //this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
                //this.ibtnFindProject.Enabled = false;
                this.LoadGrid();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.PanelGroup.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            //this.ibtnFindProject.Enabled = true;
            //this.lblProjectmDesc.Text = "";
            //this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvUnit.DataSource = null;
            this.gvUnit.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblUnit"];
            for (int i = 0; i < this.gvUnit.Rows.Count; i++)
            {
                string UsirCode = ((Label)this.gvUnit.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                if (ASTUtility.Right(UsirCode, 3) == "000")
                    continue;
                string udesc = ((TextBox)this.gvUnit.Rows[i].FindControl("txtItemdesc")).Text.Trim();
                string munit = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgUnitnum")).Text.Trim();
                string facing = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUFacing")).Text.Trim();
                string view = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUView")).Text.Trim();
                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double dRate = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double Qty = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUqty")).Text.Trim());
                string status = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvbstat")).Text.Trim();

                double pqty = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPqty")).Text.Trim());
                double pamt = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPamt")).Text.Trim());
                double utility = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUtility")).Text.Trim());
                double cooperative = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvPCooprative")).Text.Trim());

                double MinBMoney = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvBookingMoney")).Text.Trim());
                string chkper = (((CheckBox)gvUnit.Rows[i].FindControl("chk")).Checked) ? "True" : "False";



                string Remarsk = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                double tamt = Convert.ToDouble((dUsize * dRate) + pamt + utility + cooperative);
                rowindex = (this.gvUnit.PageSize * this.gvUnit.PageIndex) + i;
                tblt02.Rows[rowindex]["udesc"] = udesc;
                tblt02.Rows[rowindex]["munit"] = munit;
                tblt02.Rows[rowindex]["usize"] = dUsize;
                tblt02.Rows[rowindex]["urate"] = dRate;
                tblt02.Rows[rowindex]["uamt"] = Convert.ToDouble(dUsize * dRate);
                tblt02.Rows[rowindex]["uqty"] = Qty;
                tblt02.Rows[rowindex]["bstat"] = status;
                tblt02.Rows[rowindex]["pqty"] = pqty;
                tblt02.Rows[rowindex]["pamt"] = pamt;
                tblt02.Rows[rowindex]["tamt"] = tamt;
                tblt02.Rows[rowindex]["minbam"] = MinBMoney;
                tblt02.Rows[rowindex]["urmrks"] = Remarsk;

                tblt02.Rows[rowindex]["facing"] = facing;
                tblt02.Rows[rowindex]["uview"] = view;
                tblt02.Rows[rowindex]["utility"] = utility;
                tblt02.Rows[rowindex]["cooperative"] = cooperative;
                tblt02.Rows[rowindex]["mgtbook"] = chkper;


            }
            ViewState["tblUnit"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblUnit"];

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string UsirCode = dt.Rows[i]["usircode"].ToString();
                if (ASTUtility.Right(UsirCode, 3) == "000")
                    continue;
                string Udesc = dt.Rows[i]["udesc"].ToString();
                string UNumber = dt.Rows[i]["munit"].ToString();
                double dUsize = Convert.ToDouble(dt.Rows[i]["usize"].ToString());
                double Uqty = Convert.ToDouble(dt.Rows[i]["uqty"].ToString());
                double duqty = (Uqty <= 0) ? 1 : Uqty;
                string qty = duqty.ToString();
                string Amt = Convert.ToDouble(dt.Rows[i]["uamt"].ToString()).ToString();
                string bstat = dt.Rows[i]["bstat"].ToString();
                string Uramrks = dt.Rows[i]["urmrks"].ToString();
                string Pqty = dt.Rows[i]["pqty"].ToString();
                string Pamt = dt.Rows[i]["pamt"].ToString();
                string Minbam = dt.Rows[i]["minbam"].ToString();
                string facing = dt.Rows[i]["facing"].ToString();
                string view = dt.Rows[i]["uview"].ToString();
                string utility = dt.Rows[i]["utility"].ToString();
                string cooprative = dt.Rows[i]["cooperative"].ToString();
                string chkper = dt.Rows[i]["mgtbook"].ToString().Trim();
                if (dUsize > 0)
                {
                    bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALINF", PactCode, UsirCode, UNumber, dUsize.ToString(), qty, Udesc,
                        bstat, Uramrks, Amt, Pqty, Pamt, Minbam, facing, view, utility, cooprative, chkper, "", "", "", "");

                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";

                    }


                }


            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Update Fixation";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tblUnit");

            string comcod = this.GetCompCode();
            string PactCode = this.Request.QueryString["pactcode"].ToString();

            //string PactCode = this.ddlProjectName.SelectedValue.ToString();

            string group = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? this.ddlGroup.SelectedValue.ToString() + "%" : this.ddlFloor.SelectedValue.ToString() + "%";
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, group, "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            ViewState["tblUnit"] = ds4.Tables[0];
            this.Data_bind();

        }

        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tblUnit"];
            this.gvUnit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvUnit.DataSource = tblt05;
            this.gvUnit.DataBind();
            if (tblt05.Rows.Count == 0)
                return;
            ((Label)this.gvUnit.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uamt)", "")) ?
              0.00 : tblt05.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lFUsize")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(usize)", "")) ?
              0.00 : tblt05.Compute("Sum(usize)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lgvPAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pamt)", "")) ?
              0.00 : tblt05.Compute("Sum(pamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lgvTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(tamt)", "")) ?
              0.00 : tblt05.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lgvfAptqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uqty)", "")) ?
           0.00 : tblt05.Compute("Sum(uqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lgvfParkingqty")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(pqty)", "")) ?
           0.00 : tblt05.Compute("Sum(pqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvUnit.FooterRow.FindControl("lgvPUtility")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(utility)", "")) ?
          0.00 : tblt05.Compute("Sum(utility)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lgvPCooprative")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(cooperative)", "")) ?
          0.00 : tblt05.Compute("Sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new RealERPRPT.R_22_Sal.rptUnitFxInf();
            DataTable dt1 = (DataTable)ViewState["tblUnit"];
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "uamt>0";
            rptstk.SetDataSource(dv1);

            TextObject txtCompname = rptstk.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            txtCompname.Text = comnam;
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            txtprojectname.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Print Report";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            dv1.RowFilter = "";

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds3 = new DataSet();
            DataTable dt1 = new DataTable();
            if (this.chkAllSInf.Checked)
            {
                this.ddlFloor_SelectedIndexChanged(null, null);
                //ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFANDUNITINFO", PactCode, "", "", "", "", "", "", "", "");
                //if (ds3 == null)
                //    return;
                //this.gvUnit.Columns[1].Visible = false;
            }

            else
            {
                ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.gvUnit.Columns[1].Visible = true;
                ViewState["tblUnit"] = ds3.Tables[0];
                this.Data_bind();

            }


        }


        protected void gvUnit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = ((Label)this.gvUnit.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEUNITENTRY", pactcode, UsirCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                return;
            }
            if (result == true)
            {
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Delete Fixation";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt01 = (TextBox)e.Row.FindControl("txtItemdesc");
                TextBox txt02 = (TextBox)e.Row.FindControl("txtgUnitnum");
                TextBox txt03 = (TextBox)e.Row.FindControl("txtgvUSize");
                TextBox txt04 = (TextBox)e.Row.FindControl("txtgvUqty");
                TextBox txt05 = (TextBox)e.Row.FindControl("txtgvRate");
                TextBox txt06 = (TextBox)e.Row.FindControl("txtgvbstat");
                TextBox txt07 = (TextBox)e.Row.FindControl("txtgvRemarks");
                TextBox txt08 = (TextBox)e.Row.FindControl("txtgvPqty");
                TextBox txt09 = (TextBox)e.Row.FindControl("txtgvPamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string sales = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sales")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Substring(9, 3) == "000")
                {

                    txt01.ReadOnly = true;
                    txt02.ReadOnly = true;
                    txt03.ReadOnly = true;
                    txt04.ReadOnly = true;
                    txt05.ReadOnly = true;
                    txt06.ReadOnly = true;
                    txt07.ReadOnly = true;

                }


                if (sales == "True")
                {
                    txt01.Style.Add("color", "red");


                }

            }
        }


        protected void gvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvUnit.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        protected void ibtnGroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        protected void ibtnFloor_Click(object sender, EventArgs e)
        {
            this.GetFloor();

        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloor();
        }
        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkAllSInf.Checked)
            {
                ViewState.Remove("tblUnit");
                string comcod = this.GetCompCode();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string group = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? this.ddlGroup.SelectedValue.ToString() + "%" : this.ddlFloor.SelectedValue.ToString() + "%";

                string totno = (this.ddlFloor.SelectedValue.ToString() == "000000000") ? "" : (this.txttotalno.Text.Trim() == "") ? "" : this.txttotalno.Text.Trim();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFANDUNITINFO", PactCode, group, totno, "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.gvUnit.Columns[1].Visible = false;
                ViewState["tblUnit"] = ds3.Tables[0];
                this.Data_bind();
            }
        }
        protected void ibtnTotal_Click(object sender, EventArgs e)
        {
            this.ddlFloor_SelectedIndexChanged(null, null);
        }
    }
}
