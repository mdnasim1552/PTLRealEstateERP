using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_04_Bgd
{
    public partial class CatagoryLink : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Catagory(Work Item)";
                //Master.Page.Title = "Catagory(Work Item)";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCatagory();
                this.ShowInformation();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCatagory()
        {
            string comcod = this.GetComeCode();
            Session.Remove("tblcdesc");
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                //this.ddlcatagory.DataSource = null;
                //this.ddlcatagory.DataBind();
                return;
            }
            Session["tblcdesc"] = ds1.Tables[0];




        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lnkok_Click(null, null);

            //string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //ReportDocument rptAccCode = new RealERPRPT.R_17_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Sub-CodeBook";
            //    string eventdesc = "Print Sub-CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            // ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            Session.Remove("tblcat");
            string srchSupplier = "%%";

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETWORKLISTCATLINK", "", "", srchSupplier, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvcat.DataSource = null;
                this.gvcat.DataBind();
                return;
            }
            Session["tblcat"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            try
            {
                DataTable tbl1 = (DataTable)Session["tblcat"];

                this.gvcat.DataSource = tbl1;
                this.gvcat.DataBind();

            }
            catch (Exception ex)
            {
            }


        }
        protected void Session_Update()
        {
            DataTable dt = (DataTable)Session["tblcat"];
            int i = 0;
            foreach (GridViewRow gvr1 in gvcat.Rows)
            {

                string cattype = ((DropDownList)gvr1.FindControl("ddlcatagory")).SelectedValue.ToString();
                string catdesc = ((DropDownList)gvr1.FindControl("ddlcatagory")).SelectedValue.ToString();
                //double crlimit = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvlimit")).Text.Trim()));
                //double period = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text.Trim()));
                //((TextBox)gvr1.FindControl("txtgvlimit")).Text = crlimit.ToString("#,##0;(#,##0); ");
                //((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text = period.ToString("#,##0;(#,##0); ");
                //TblRowIndex2 = (this.grvacc.PageIndex) * this.grvacc.PageSize + j;

                dt.Rows[i]["cattype"] = cattype;
                dt.Rows[i]["catdesc"] = catdesc;
                i++;

            }

            Session["tblcat"] = dt;
            this.Data_Bind();
        }

        protected void lbtnSame_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblcat"];

            string cattype = dt.Rows[0]["cattype"].ToString();
            string catdesc = dt.Rows[0]["catdesc"].ToString();
            foreach (DataRow dr1 in dt.Rows)
            {

                //string cattype = ((DropDownList)gvr1.FindControl("ddlcatagory")).SelectedValue.ToString();
                //string catdesc = ((DropDownList)gvr1.FindControl("ddlcatagory")).SelectedValue.ToString();
                //double crlimit = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvlimit")).Text.Trim()));
                //double period = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text.Trim()));
                //((TextBox)gvr1.FindControl("txtgvlimit")).Text = crlimit.ToString("#,##0;(#,##0); ");
                //((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text = period.ToString("#,##0;(#,##0); ");
                //TblRowIndex2 = (this.grvacc.PageIndex) * this.grvacc.PageSize + j;

                dr1["cattype"] = cattype;
                dr1["catdesc"] = catdesc;


            }

            Session["tblcat"] = dt;
            this.Data_Bind();
        }

        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }


        protected void lbtnUpdateMat_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Session_Update();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataTable dt = (DataTable)Session["tblcat"];
                DataSet ds1 = new DataSet("ds1");
                ds1.Merge(dt);
                ds1.Tables[0].TableName = "tbl1";


                bool result = da.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "UPDATECATCODE", ds1, null, null, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                //    if (!resulta)

                //foreach (DataRow dr in tblt05.Rows)
                //{

                //    string ssircode = dr["sircode"].ToString();
                //    string crlimit = dr["limit"].ToString();
                //    string period = dr["period"].ToString();

                //    bool resulta = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSORUPSUPLIMIT", ssircode, crlimit, period, "", "", "",
                //                "", "", "", "", "", "", "", "", "");
                //    if (!resulta)
                //    {
                //       ((Label)this.Master.FindControl("lblmsg")).Text = this.da.ErrorObject["Msg"].ToString();
                //        return;
                //    }


                //}


                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }


        protected void lbtnSameValue_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["storedata"];
            int i = 0;
            double period = 0.00;
            foreach (DataRow dr1 in dt.Rows)
            {
                if (i == 0)
                {
                    i++;
                    period = Convert.ToDouble(dr1["period"]);
                    continue;
                }
                dr1["period"] = period;
            }
            Session["storedata"] = dt;
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            Session_Update();

        }
        protected void gvcat_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlcatagory");
                DataTable dts = (DataTable)Session["tblcdesc"];
                string cattype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cattype")).ToString().Trim();


                ddl1.DataTextField = "description";
                ddl1.DataValueField = "code";
                ddl1.DataSource = dts;
                ddl1.DataBind();
                ddl1.SelectedValue = cattype;

            }

        }

    }
}
