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

namespace RealERPWEB.F_34_Mgt
{
    public partial class PurReqAdjst : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "ReqAdjust") ? "REQUISITION  ADJUSTMENT INPUT/EDIT SCREEN" : "PROFIT MARGIN";
                this.SelectView();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    this.GetProjectName();
                    this.GetAdjNo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "ProMargin":
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;



            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void ShowProMarginInfo()
        {
            Session.Remove("tblreqadj");
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "SHOWPROMARGIN", date, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                return;

            }

            Session["tblreqadj"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGv();


        }
        private void FooteCalculation()
        {
            DataTable dt = (DataTable)Session["tblreqadj"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ProMargin":
                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='BBBBAAAAAAAA'");
                    DataTable dt1 = dv.ToTable();
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFAdvSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(advsamt)", "")) ? 0.00 : dt1.Compute("sum(advsamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFTCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(actcost)", "")) ? 0.00 : dt1.Compute("sum(actcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFNetP")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netpramt)", "")) ? 0.00 : dt1.Compute("sum(netpramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFSales")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(samt)", "")) ? 0.00 : dt1.Compute("sum(samt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFSR")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(sreamt)", "")) ? 0.00 : dt1.Compute("sum(sreamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFSU")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(sunreamt)", "")) ? 0.00 : dt1.Compute("sum(sunreamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProMar.FooterRow.FindControl("lblgvFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balamt)", "")) ? 0.00 : dt1.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }
        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.ShowData();
                this.GetAdjNo();

            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.lblProjectdesc.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.ddlProjectName.Visible = true;
                this.lblProjectdesc.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
            }

        }

        private void ShowData()
        {

            Session.Remove("tblreqadj");
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string mrfno = "%" + this.txtSrcmrfno.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "REQSATIONSTATUS", date, pactcode, mrfno, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblreqadj"] = dt1;
            this.LoadGv();

        }

        private void GetAdjNo()
        {
            string comcod = this.GetComeCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "GETADJMENTNO", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lbladjstmentno.Text = ds1.Tables[0].Rows[0]["maxadjno1"].ToString();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "ReqAdjust":
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        {

                            reqno = dt1.Rows[j]["reqno"].ToString();
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                        }

                        else
                        {

                            reqno = dt1.Rows[j]["reqno"].ToString();

                        }

                    }
                    break;
                case "ProMargin":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        grp = dt1.Rows[j]["grp"].ToString();


                    }


                    break;

            }
            return dt1;

        }


        private void LoadGv()
        {
            DataTable dt = (DataTable)Session["tblreqadj"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    //this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();

                    Session["Report1"] = gvReqStatus;
                    ((HyperLink)this.gvReqStatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                case "ProMargin":
                    this.gvProMar.DataSource = dt;
                    this.gvProMar.DataBind();
                    this.FooteCalculation();
                    break;



            }




        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblreqadj"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    for (int i = 0; i < this.gvReqStatus.Rows.Count; i++)
                    {
                        double balqty = Convert.ToDouble("0" + ((Label)this.gvReqStatus.Rows[i].FindControl("lblgvBalqty")).Text.Trim());
                        double adjstqty = Convert.ToDouble("0" + ((TextBox)this.gvReqStatus.Rows[i].FindControl("txtgvadjqty")).Text.Trim());
                        adjstqty = balqty >= adjstqty ? adjstqty : 0.00;
                      //  if (balqty<= adjstqty)
                        int rowindex = (this.gvReqStatus.PageSize) * (this.gvReqStatus.PageIndex) + i;
                        dt.Rows[rowindex]["adjstqty"] = adjstqty;
                    }

                    break;

                case "ProMargin":
                    for (int i = 0; i < this.gvProMar.Rows.Count; i++)
                    {
                        double prmar = ASTUtility.StrPosOrNagative(((TextBox)this.gvProMar.Rows[i].FindControl("txtgvProfitmar")).Text.Trim());
                        dt.Rows[i]["prmar"] = prmar;
                    }

                    break;
            }

            Session["tblreqadj"] = dt;




        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGv();
        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string adjno = this.lbladjstmentno.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy").Substring(7, 4) + this.lbladjstmentno.Text.Trim().Substring(3, 2) + ASTUtility.Right(this.lbladjstmentno.Text.Trim(), 5);
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblreqadj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string reqno = dt.Rows[i]["reqno"].ToString();

                string rsircode = dt.Rows[i]["rsircode"].ToString();
                string spccode = dt.Rows[i]["spcfcod"].ToString();
                double adsjtqty = Convert.ToDouble(dt.Rows[i]["adjstqty"]);
                if (adsjtqty != 0)
                {


                    bool result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "INSORUPDATEREQADJST", adjno, reqno,
                             pactcode, rsircode, spccode, adsjtqty.ToString(), date, userid, Terminal, Sessionid, "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = PurData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
            }

           
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated successfully";
            ((LinkButton)this.gvReqStatus.FooterRow.FindControl("lbtnFinalUpdate")).Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Req Adj";
                string eventdesc2 = adjno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ReqAdjust":
                    this.PrintReqAdjustment();
                    break;
                case "ProMargin":
                    this.RptPoMargin();
                    break;

            }
        }

        private void PrintReqAdjustment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
           
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblreqadj"];

          
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("pactcode <> '' or pactcode <> '1899AAAAAAAA'");
            //dt = dv.ToTable();

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_34_Mgt.EClassEnventory.RequisationAdjust>();          
            Rpt1 = RptSetupClass1.GetLocalReport("R_34_Mgt.RptReqAdjustment", list, null, null);
            Rpt1.EnableExternalImages = true;        

            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));           
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Requisition Adjustment"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void RptPoMargin()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblreqadj"];
            ReportDocument rptsaldperiod = new RealERPRPT.R_34_Mgt.RptProMargin();
            TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rptdate.Text = "As on Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaldperiod.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Calculation";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsaldperiod;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void lbtnUpdateProMar_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetComeCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblreqadj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                string pactcode = dt.Rows[i]["pactcode"].ToString();
                double prmar = Convert.ToDouble(dt.Rows[i]["prmar"].ToString());
                if (prmar != 0)
                {


                    bool result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "INSORUPDATEPRMAR", pactcode, prmar.ToString(),
                             "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + PurData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);

            this.ShowProMarginInfo();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update PROFIT MARGIN";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void gvProMar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvPDesc = (Label)e.Row.FindControl("lblgvPDesc");
                Label lblgvAdvSal = (Label)e.Row.FindControl("lblgvAdvSal");
                Label lblgvTCost = (Label)e.Row.FindControl("lblgvTCost");
                Label lblgvNetP = (Label)e.Row.FindControl("lblgvNetP");
                Label lblgvSales = (Label)e.Row.FindControl("lblgvSales");
                Label lblgvSR = (Label)e.Row.FindControl("lblgvSR");
                Label lblgvSU = (Label)e.Row.FindControl("lblgvSU");
                Label lblgvBalAmt = (Label)e.Row.FindControl("lblgvBalAmt");

                TextBox txtgvProfitmar = (TextBox)e.Row.FindControl("txtgvProfitmar");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();



                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lblgvPDesc.Font.Bold = true;
                    lblgvAdvSal.Font.Bold = true;
                    lblgvTCost.Font.Bold = true;
                    lblgvNetP.Font.Bold = true;
                    lblgvSales.Font.Bold = true;
                    lblgvSR.Font.Bold = true;
                    lblgvSU.Font.Bold = true;
                    lblgvBalAmt.Font.Bold = true;
                    txtgvProfitmar.ReadOnly = true;

                    lblgvPDesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowProMarginInfo();
        }
        protected void imgbtnFindmrfno_Click(object sender, EventArgs e)
        {
            this.ShowData();

        }

        protected void txtgvadjqty_TextChanged(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;                             
            double balqty = Convert.ToDouble("0" + ((Label)this.gvReqStatus.Rows[index].FindControl("lblgvBalqty")).Text.Trim());
            double adjqty = Convert.ToDouble("0" + ((TextBox)this.gvReqStatus.Rows[index].FindControl("txtgvadjqty")).Text.Trim());

            if (balqty<adjqty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Adjusted quantity must be equal or less balance quantity');", true);
                return;
            }
        }

       
    }
}
