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
    public partial class MktLandOwnerDetails : System.Web.UI.Page
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

                ((Label)this.Master.FindControl("lblTitle")).Text = "LAND OWNER'S DETAILS ";

                Session.Remove("Unit");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

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
            string usrid = hst["usrid"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, usrid, "", "", "", "", "", "", "");
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
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string ddldesc = hst["ddldesc"].ToString();
                this.lblProjectmDesc.Text = (ddldesc == "False" ? this.ddlProjectName.SelectedItem.Text.Trim().ToString() : this.ddlProjectName.SelectedItem.Text.Substring(13));

                //this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
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
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";
            string musircode = "52";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "DETAILSIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
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
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ibtnSrchClient_Click(object sender, EventArgs e)
        {

            string comcod = objcom.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[rowindex].FindControl("ddlClientName");
            string SearchClient = "%" + ((TextBox)gvSpayment.Rows[rowindex].FindControl("txtSerachClient")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
            ddl2.DataTextField = "prosdesc";
            ddl2.DataValueField = "proscod";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_RENTMGT", "GETCLEINTNAME", pactcode, SearchClient, "", "", "", "", "", "", "");
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
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_RENTMGT", "INSERTORUPMKTASALLINK", pactcode, usircode, Proscode, "", "", "", "", "", "", "", "", "", "", "", "");
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
            this.CustInf();

        }




        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.LoadGrid();



        }

        private void CustInf()

        {
            // Session.Remove("tblcost");
            // Session.Remove("tblPay");
            // Session.Remove("tpripay");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = hst["comcod"].ToString();
            // string UsirCode = this.lblCode.Text;
            // string PactCode = this.ddlProjectName.SelectedValue.ToString();
            // DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO",PactCode, UsirCode, "", "", "", "", "", "", "");
            // Session["UserLog"] = ds1.Tables[6];
            //// this.gvPersonalInfo.DataSource = ds1.Tables[0];
            // Session["tpripay"] = ds1.Tables[4];
            // //this.gvPersonalInfo.DataBind();

            // Session["tblcost"] = ds1.Tables[1];

            /////
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
            this.gvCost.DataSource = ds1.Tables[1];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[1];
            this.Calculation();
            this.lbtnTotalCost_Click(null, null);


        }

        private void Calculation()
        {

            DataTable dtcost = (DataTable)Session["tblcost"];
            DataTable dtpay = (DataTable)Session["tblPay"];

            double tocost;
            tocost = Convert.ToDouble((Convert.IsDBNull(dtcost.Compute("sum(uamt)", "")) ? 0.00 :
                 dtcost.Compute("sum(uamt)", "")));
            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = tocost.ToString("#,##0;(#,##0); ");

        }



        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
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
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Personal Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void lFinalUpdateCost_Click1(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvCost.Rows[i].FindControl("lblgvGcod")).Text.Trim();
                string UNumber = ((TextBox)this.gvCost.Rows[i].FindControl("txtgUnitnum")).Text.Trim();
                string Usize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim()).ToString();
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvCost.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                double disamt = 0;

                //if (Amt!=0)
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALGINF1", PactCode, Usircode, Gcode, UNumber, Usize, Amt.ToString(), Remarks, disamt.ToString(), "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Revenue Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            double Amount = 0;
            double Usize = 0;
            // double PaidAmt = 0;

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                //Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));

                double dUsize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double dAmt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                Amount += dAmt;
                Usize += dUsize;
                double dRate = (dUsize > 0) ? (dAmt / dUsize) : 0.00;
                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text = dAmt.ToString("#,##0; -#,##0; ");
                ((Label)this.gvCost.Rows[i].FindControl("lgvRate")).Text = dRate.ToString("#,##0.00;(#,##0.00); ");



            }

            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = Amount.ToString("#,##0;(#,##0); ");

            //double AcAmt = Convert.ToDouble("0" + this.lblAcAmt.Text);
            //if (Amount > 0)
            //{
            //    //double discount = (AcAmt - Amount);
            //    //double discountp = (discount * 100) / Amount;
            //    //this.ldiscountt.Text = Math.Round((AcAmt - Amount), 0).ToString("#,##0;(#,##0);");
            //    //this.ldiscountp.Text = discountp.ToString("#,##0;(#,##0);") + '%';
            //}
            Session["amt"] = Amount;
        }

        protected void lFinalUpdateCost_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvCost.Rows[i].FindControl("lblgvGcod")).Text.Trim();
                string UNumber = ((TextBox)this.gvCost.Rows[i].FindControl("txtgUnitnum")).Text.Trim();
                string Usize = Convert.ToDouble('0' + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvUSize")).Text.Trim()).ToString();
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvCost.Rows[i].FindControl("txtgvuamt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvCost.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                //if (Amt!=0)
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALGINF1", PactCode, Usircode, Gcode, UNumber, Usize, Amt.ToString(), Remarks, "", "", "", "", "", "", "", "");

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Updated Successfully');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Revenue Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
    }
}
