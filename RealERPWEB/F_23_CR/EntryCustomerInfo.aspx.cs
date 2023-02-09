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
namespace RealERPWEB.F_23_CR
{
    public partial class EntryCustomerInfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();
        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CUSTOMER INFORMATION VIEW/EDIT";


                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            string userid = hst["usrid"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
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
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = true;
                this.lblProjectmDesc.Visible = false;
                this.lblProjectdesc.Visible = false;
                this.ddlProjectName.Enabled = false;


                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }



        private void ClearScreen()
        {
            this.ddlProjectName.Enabled = true;
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            this.lperInfo.Visible = false;
            this.lbtnBack.Visible = false;
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();

        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "DETAILSIRINFINFORMATION", PactCode, "%", "51", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }

            this.gvSpayment.Columns[5].Visible = false;
            this.gvSpayment.Columns[6].Visible = false;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //string prjname = this.ddlProjectName.SelectedItem.Text;
            //string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////item info
            //DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            //string UsirCode = this.lblCode.Text;
            //string ItemName = basicinfo.Rows[0]["udesc"].ToString();
            //string size = Convert.ToDouble(basicinfo.Rows[0]["usize"]).ToString("#,##0.00;(#,##0.00); ");
            ////ToString("#,##0;(#,##0); ")
            //string unit = basicinfo.Rows[0]["munit"].ToString();

            //string concat1 = ItemName + " , " + "Unit Size: " + size + " " + unit;

            ////direct cost
            //string ldiscounttT = this.ldiscountt.Text;
            //string ldiscountpP = this.ldiscountp.Text;

            //string salesteams = ddlSalesTeam.SelectedItem.Text;
            //DataSet dss = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "COMBINEDTABLEFORSALES", PactCode, UsirCode, "", "", "", "", "", "", "");
            //ReportDocument rpcp = new RealERPRPT.R_22_Sal.RptPCPayment() ;

            ////TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////CompName.Text = comname;

            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Project Name: " + prjname.ToString().Substring(13);


            //TextObject txtItemName = rpcp.ReportDefinition.ReportObjects["txtItemName"] as TextObject;
            //txtItemName.Text = "Unit Description: " + concat1;


            //TextObject txtdist = rpcp.ReportDefinition.ReportObjects["txtdist"] as TextObject;
            //txtdist.Text = "Discount in Tk. " + ldiscounttT;

            //TextObject txtdisp = rpcp.ReportDefinition.ReportObjects["txtdisp"] as TextObject;
            //txtdisp.Text = "Discount in (%) " + ldiscountpP;

            //TextObject txtsalest = rpcp.ReportDefinition.ReportObjects["txtsalest"] as TextObject;
            //txtsalest.Text = "Sales Team: " + salesteams;


            //TextObject txtNetTotalPayment = rpcp.ReportDefinition.ReportObjects["txtNetTotalPayment"] as TextObject;
            //txtNetTotalPayment.Text = this.lblValNetTotalPayment.Text.Trim() ;


            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //TextObject txtcominfo = rpcp.ReportDefinition.ReportObjects["txtcominfo"] as TextObject;
            //txtcominfo.Text = ASTUtility.Cominformation();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales With Payment Schedule";
            //    string eventdesc = "Print Payment Schedule";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " Unit Description: " + concat1;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rpcp.SetDataSource(dss.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnusize_Click(object sender, EventArgs e)
        {

            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();
            if ((Convert.ToBoolean(dtOrder.Rows[0]["mgtbook"])) == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Booked from Management');", true);
                return;
            }
            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;

            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;

            if (this.Request.QueryString["Type"].ToString().Trim() == "Loan")
            {
                this.MultiView1.ActiveViewIndex = 1;
                this.ShowCustLoan();
                return;
            }

            else if (this.Request.QueryString["Type"].ToString().Trim() == "Registration")
            {
                this.MultiView1.ActiveViewIndex = 2;
                this.ShowRegistration();
                return;
            }


            this.CustInf();
            this.BtnEnabled();
        }

        private void BtnEnabled()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Sales":
                    ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = true;


                    break;

                case "Cust":
                    ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lUpdatPerInfo")).Enabled = false;


                    break;
            }
        }


        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            this.LoadGrid();



        }

        private void CustInf()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[6];
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            Session["tpripay"] = ds1.Tables[4];
            this.gvPersonalInfo.DataBind();
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.gvPersonalInfo.Rows[0].Enabled = false;
            }


        }



        private void ShowCustLoan()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CUSTLOANINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvLoanInformation.DataSource = ds1.Tables[0];
            this.gvLoanInformation.DataBind();


        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Personal Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }









        protected void lUpdateLoanInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvLoanInformation.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvLoanInformation.Rows[i].FindControl("lblgvItmCodeloan")).Text.Trim();
                string gtype = ((Label)this.gvLoanInformation.Rows[i].FindControl("lgvgvalloan")).Text.Trim();
                string Gvalue = ((TextBox)this.gvLoanInformation.Rows[i].FindControl("txtgvValloan")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTLOAN", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Loan Info";
                string eventdesc = "Update Loan Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void ShowRegistration()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CUSTREGISTRATION", PactCode, UsirCode, "", "", "", "", "", "", "");
            this.gvRegStatus.DataSource = ds1.Tables[0];
            this.gvRegStatus.DataBind();


        }

        protected void lUpdateRegis_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvRegStatus.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvRegStatus.Rows[i].FindControl("lblgvItmCodeReg")).Text.Trim();
                string reclegdept = ((TextBox)this.gvRegStatus.Rows[i].FindControl("txtgvValRecleg")).Text.Trim();
                string protoclient = ((TextBox)this.gvRegStatus.Rows[i].FindControl("txtgvValprotoclient")).Text.Trim();

                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPCUSTREG", PactCode, Usircode, Gcode, reclegdept, protoclient, "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void gvSpayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvSpayment.EditIndex = -1;
            this.gvSpayment.DataSource = (DataTable)ViewState["tblData"];
            this.gvSpayment.DataBind();
        }


        protected void gvSpayment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvSpayment.EditIndex = e.NewEditIndex;
            this.gvSpayment.DataSource = (DataTable)ViewState["tblData"];
            this.gvSpayment.DataBind();
            int rowindex = (gvSpayment.PageSize) * (this.gvSpayment.PageIndex) + e.NewEditIndex;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string usircode = ((DataTable)ViewState["tblData"]).Rows[rowindex]["usircode"].ToString();
            string proscod = ((DataTable)ViewState["tblData"]).Rows[rowindex]["proscod"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[e.NewEditIndex].FindControl("ddlClientName");
            ViewState["gindex"] = e.NewEditIndex;
            string comcod = objcom.GetCompCode();
            string SearchClient = "%" + ((TextBox)this.gvSpayment.Rows[e.NewEditIndex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = proscod; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

            //ddl2.Visible = true;
        }
        protected void gvSpayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = objcom.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = ((Label)this.gvSpayment.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();//.ToUpper();
            string Proscode = ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedValue.ToString();

            if (Proscode != "")
            {
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPMKTASALLINK", pactcode, usircode, Proscode, "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == true)
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Successfully Updated');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Fail');", true);
                    return;
                }

                int rowindex = (gvSpayment.PageSize) * (this.gvSpayment.PageIndex) + e.RowIndex;
                DataTable dt = (DataTable)ViewState["tblData"];

                dt.Rows[rowindex]["proscod"] = Proscode;
                dt.Rows[rowindex]["prosdesc"] = ((DropDownList)this.gvSpayment.Rows[e.RowIndex].FindControl("ddlClientName")).SelectedItem.Text;
                ViewState["tblData"] = dt;
                this.gvSpayment.EditIndex = -1;
                this.gvSpayment.DataSource = dt;
                this.gvSpayment.DataBind();


                for (int i = 0; i < gvSpayment.Rows.Count; i++)
                {
                    string usircode1 = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                    LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                    if (lbtn1 != null)
                        if (lbtn1.Text.Trim().Length > 0)
                            lbtn1.CommandArgument = usircode1;
                }





            }

        }

        protected void ibtnSrchClient_Click(object sender, ImageClickEventArgs e)
        {

            string comcod = objcom.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[rowindex].FindControl("ddlClientName");
            string SearchClient = "%" + ((TextBox)gvSpayment.Rows[rowindex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
        }



        protected void ibtnFindSalesteam_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}

