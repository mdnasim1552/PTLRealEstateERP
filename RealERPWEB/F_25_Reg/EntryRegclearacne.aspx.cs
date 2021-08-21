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
namespace RealERPWEB.F_25_Reg
{
    public partial class EntryRegclearacne : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Registration Status- All Project";



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
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.ddlProjectName.SelectedValue.ToString()) + "%";



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETCUSTREGISTATION", ProjectCode, date, "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvRegis.DataSource = null;
                this.gvRegis.DataBind();
                return;
            }
            Session["tblAccRec"] = ds2.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblAccRec"];
            this.gvRegis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRegis.DataSource = dt;
            this.gvRegis.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFtocost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(suamt)", "")) ?
                0.00 : dt.Compute("Sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgFEncash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFinproamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(inproamt)", "")) ?
            0.00 : dt.Compute("Sum(inproamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgvFtoreceivedt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recamt)", "")) ?
                0.00 : dt.Compute("Sum(recamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFdelcharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
            0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
            0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgvFtodelay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(delayadis)", "")) ?
            0.00 : dt.Compute("Sum(delayadis)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvRegis;
            ((HyperLink)this.gvRegis.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }



        protected void gvRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRegis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }





        private void printDuesCollection()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptDuesCollection();
            //DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            //TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rpttxtCompName.Text = comnam;



            //TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            //txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            //TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            //txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            //TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            //txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            //TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            //TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            //rpttxttoduesupto.Text = "Receivable Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            //TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            //rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            //TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            //rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");






            //TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptRcvList.SetDataSource((DataTable)Session["tblAccRec"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Received List Info";
            //    string eventdesc = "Print Report MR";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptRcvList.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptRcvList;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }


        protected void gvRegis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvRegis.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvRegis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvRegis.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string regcode = ((Label)this.gvRegis.Rows[e.NewEditIndex].FindControl("lgvregcode")).Text.Trim();
            int rowindex = (gvRegis.PageSize) * (this.gvRegis.PageIndex) + e.NewEditIndex;
            DropDownList ddl2 = (DropDownList)this.gvRegis.Rows[e.NewEditIndex].FindControl("ddlregistd");
            ViewState["gindex"] = e.NewEditIndex; ;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SearchProject = "%" + ((TextBox)gvRegis.Rows[e.NewEditIndex].FindControl("txtSerachreg")).Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETREGCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "regdesc";
            ddl2.DataValueField = "regcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = regcode;




        }
        protected void gvRegis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblAccRec"];
            int rowindex = (int)ViewState["gindex"];
            string pactcode = ((DataTable)Session["tblAccRec"]).Rows[rowindex]["pactcode"].ToString();
            string usircode = ((DataTable)Session["tblAccRec"]).Rows[rowindex]["usircode"].ToString();
            string regcode = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlregistd")).SelectedValue.ToString();
            if (regcode == "0000000")
            {
                this.gvRegis.EditIndex = -1;
                this.Data_Bind();
                return;
            }
            string regisdat = this.txtdate.Text.Trim();
            DataRow[] dr2 = dt.Select("pactcode = '" + pactcode + "' and usircode='" + usircode + "'");
            if (dr2.Length > 0)
            {
                dr2[0]["regcode"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlregistd")).SelectedValue.ToString();
                dr2[0]["regdesc"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlregistd")).SelectedItem.Text;
            }

            bool resulta = false;
            resulta = CustData.UpdateTransInfo(comcod, "SP_ENTRY_REGISTRATION", "INSOUPREGISTDINF", pactcode, usircode, regcode, regisdat, "",
                                    "", "", "", "", "", "", "", "", "", "");


            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.gvRegis.EditIndex = -1;
            Session["tblAccRec"] = dt;
            this.Data_Bind();

        }

        protected void ibtnSrchregis_Click(object sender, EventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlregistd");
            string SearchProject = "%" + ((TextBox)gvRegis.Rows[rowindex].FindControl("txtSerachreg")).Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETREGCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "regdesc";
            ddl2.DataValueField = "regcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();


        }
    }
}










