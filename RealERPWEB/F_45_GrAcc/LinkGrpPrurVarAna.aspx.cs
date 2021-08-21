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
namespace RealERPWEB.F_45_GrAcc
{

    public partial class LinkGrpPrurVarAna : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                this.lblAsDate.Text = this.Request.QueryString["Date"].ToString();
                string type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "IssueBasis") ? "Material Evaluation - Based on Issue" : "Material Evaluation - Based on Progress";
                this.GetProjectName();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            return (this.Request.QueryString["comcod"].ToString());

        }

        protected void GetProjectName()
        {


            string comcod = this.GetCompCode();
            string date = this.Request.QueryString["Date"].ToString();
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();



        }



        protected void ImgbtnFindImpNo_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.LoadGrid();
        }


        private void LoadGrid()
        {
            Session.Remove("tblVar");

            string comcod = this.GetCompCode();
            string projectname = this.ddlProjectName.SelectedValue.ToString();
            string frmdate = this.Request.QueryString["Date"].ToString();
            string flrcod = "000";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string CallType = this.Request.QueryString["Type"].ToString() == "IssueBasis" ? "RPTPURVARANALYSIS" : "RPTPURVARANALYSISSBASIS";
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", CallType, projectname, frmdate, flrcod, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }


            Session["tblVar"] = ds2.Tables[0];
            this.GridData();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void GridData()
        {
            this.gvRptResBasis.Columns[6].HeaderText = (this.Request.QueryString["Type"].ToString().Trim() == "IssueBasis") ? "Issue Qty" : "Bud.Cons";
            this.gvRptResBasis.Columns[7].HeaderText = (this.Request.QueryString["Type"].ToString().Trim() == "IssueBasis") ? "Actual Stock" : "Bud. Stock";
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = (DataTable)Session["tblVar"];
            this.gvRptResBasis.DataBind();




            //this.FooterCal();

        }


        //private void FooterCal() 
        //{
        //    DataTable dt = (DataTable)Session["tblVar"];
        //    double mSUMAM = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(varamt)", "")) ?
        //          0.00 : dt.Compute("sum(varamt)", "")));

        //    ((Label)this.gvRptResBasis.FooterRow.FindControl("lfVaramt")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);-");

        //}


        private void FooterCal(DataTable TptTable)
        {
            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
               0.00 : TptTable.Compute("sum(rptamt)", "")));
            this.gvRptResBasis.Columns[6].FooterText = mSUMAM.ToString("#,##0.00;(#,##0.00);-");
            double mSUMQTY = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptqty)", "")) ?
           0.00 : TptTable.Compute("sum(rptqty)", "")));
            this.gvRptResBasis.Columns[4].FooterText = mSUMQTY.ToString("#,##0.00;(#,##0.00);-");


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //string type = Request.QueryString["Type"].ToString().Trim();
            //string Basis = (type == "IssueBasis") ? "Material Evaluation - Based on Issue" : "Material Evaluation - Based on Progress";
            //DataTable dt = (DataTable)Session["tblVar"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string date = this.Request.QueryString["Date"].ToString();
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.rptPurVarAa();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text =this.ddlProjectName.SelectedItem.Text;

            //if (Request.QueryString["Type"].ToString().Trim() == "IssueBasis") 
            //{

            //    TextObject txtheaderbudcon = rptstk.ReportDefinition.ReportObjects["txtheaderbudcon"] as TextObject;
            //    txtheaderbudcon.Text = "Issue Qty";
            //    TextObject txtheaderbudstk = rptstk.ReportDefinition.ReportObjects["txtheaderbudstk"] as TextObject;
            //    txtheaderbudstk.Text = "Actual Stock";


            //}

            //TextObject rptBasis = rptstk.ReportDefinition.ReportObjects["Basis"] as TextObject;
            //rptBasis.Text =Basis;
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + date;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = Basis;
            //    string eventdesc = "Print Report:";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void IndiVidualItemNo()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //DataTable dt = (DataTable)Session["tblRes"];
            //ReportDocument rptResource = new  RptImpResourceBasis();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text = "Project Description "+this.ddlProjectName.SelectedItem.Text.Trim();
            //TextObject rpttxtfdes = rptResource.ReportDefinition.ReportObjects["flrdes"] as TextObject;
            //rpttxtfdes.Text = this.ddlFloorListRpt.SelectedItem.Text.Trim();
            // TextObject rptdate = rptResource.ReportDefinition.ReportObjects["date"] as TextObject;
            // rptdate.Text ="From "+Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy")+" to "+Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            // TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";
        }
        private void DatewiseProImp()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblRes"];
            //ReportDocument rptResource = new RptImpDatawiseResource();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text =comnam;      
            //TextObject rptdate = rptResource.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";

        }



        protected void ddlImpNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();


            //string  vouno=this.ddlProjectName.SelectedValue.ToString();
            //DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETFLOORCOD", vouno, "", "", "", "", "", "", "", "");
            //DataTable dt = ds1.Tables[0];

            //DataRow dr2 = dt.NewRow();
            //dr2["flrcod"] = "000";
            //dr2["flrdes"] = "All Floors-Sum";
            //DataRow dr3 = dt.NewRow();
            //dr3["flrcod"] = "AAA";
            //dr3["flrdes"] = "All Floors-Details";
            //dt.Rows.Add(dr2);
            //dt.Rows.Add(dr3);
            //DataView dv = dt.DefaultView;
            //dv.Sort="flrcod";
            //dt = dv.ToTable();
            //this.ddlFloorListRpt.DataTextField = "flrdes";
            //this.ddlFloorListRpt.DataValueField = "flrcod";
            //this.ddlFloorListRpt.DataSource = dt;
            //this.ddlFloorListRpt.DataBind();



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridData();
        }

        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.GridData();
        }
    }
}
