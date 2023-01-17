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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_12_Inv
{
    public partial class PurPhyStock : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Physical Stock Information";

            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPHYPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();



        }



        protected void ImgbtnFindImpNo_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.ddlProjectName.Visible = false;
                this.lblProjectName.Visible = true;
                this.lgvPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.ShowPhysicalStock();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.ddlProjectName.Visible = true;
                this.lblProjectName.Visible = false;
                this.lgvPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();

            }
            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string eventtype = "Physical Stock";
                string eventdesc = "Show Report";
                string eventdesc2 = "Project Name " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        private void ShowPhysicalStock()
        {
            Session.Remove("tblPhyStk");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "PHYRESOURCESTK", projectname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }
            Session["tblPhyStk"] = HiddenSameData(ds2.Tables[0]);
            this.LoadGrid();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rptcod = dt1.Rows[0]["rptcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rptcod"].ToString() == rptcod)
                {
                    rptcod = dt1.Rows[j]["rptcod"].ToString();
                    dt1.Rows[j]["rptdesc1"] = "";
                }

                else
                    rptcod = dt1.Rows[j]["rptcod"].ToString();
            }

            return dt1;
        }
        private void LoadGrid()
        {
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = (DataTable)Session["tblPhyStk"]; ;
            this.gvRptResBasis.DataBind();
            //this.FooterCal();


        }


        //private void FooterCal() 
        //{
        //    DataTable dt = (DataTable)Session["tblPhyStk"];
        //    double mSUMAM = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stkqty)", "")) ?
        //          0.00 : dt.Compute("sum(stkqty)", "")));

        //    ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFSQty")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);-");

        //}



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblPhyStk"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            var list = dt.DataTableToList<RealEntity.C_12_Inv.ProjStock>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptProPhyStock", list, null, null);


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PHYSICAL STOCK INFORMATION"));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;


            if (ConstantInfo.LogStatus == true)
            {


                string eventtype = "Physical Stock";
                string eventdesc = "Print Report";
                string eventdesc2 = "Project Name " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptPurVarAa();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text;       
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblPhyStk"];
            int TblRowIndex;
            for (int i = 0; i < gvRptResBasis.Rows.Count; i++)
            {
                TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
                double stkqty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvStkQty")).Text.Trim());

                dt1.Rows[TblRowIndex]["stkqty"] = stkqty;

            }
            Session["tblPhyStk"] = dt1;
            this.LoadGrid();
            // this.FooterCal();

        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.lnkbtnTotal_Click(null, null);
            DataTable dt1 = (DataTable)Session["tblPhyStk"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode1 = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string rsircode = dt1.Rows[i]["rptcod"].ToString().Trim();
                string spcfcod = dt1.Rows[i]["spcfcod"].ToString().Trim();
                string stkqty = dt1.Rows[i]["stkqty"].ToString().Trim();
                double stkqty1 = Convert.ToDouble(dt1.Rows[i]["stkqty"].ToString().Trim());
                if (stkqty1 > 0)
                {
                    bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INUPDATEPHYSTK", pactcode1, rsircode, spcfcod,
                       stkqty, "", "", "", "", "", "", "", "", "", "", "");
                }


            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Physical Stock";
                string eventdesc = "Update Stock";
                string eventdesc2 = "Project Name " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.lnkbtnTotal_Click(null, null);
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

    }
}
