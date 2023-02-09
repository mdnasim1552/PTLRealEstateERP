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
namespace RealERPWEB.F_14_Pro
{
    public partial class SuppLimitCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Limit Information";

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
            Session.Remove("storedata");
            string srchSupplier = "%" + this.txtsrch.Text.Trim() + "%";

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETSUPPLIERLIMIT", "", "", srchSupplier, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {
            }


        }
        protected void Session_Update()
        {
            DataTable tbl1 = (DataTable)Session["storedata"];
            int TblRowIndex2;
            for (int j = 0; j < this.grvacc.Rows.Count; j++)
            {
                double crlimit = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvlimit")).Text.Trim()));
                double period = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text.Trim()));
                ((TextBox)this.grvacc.Rows[j].FindControl("txtgvlimit")).Text = crlimit.ToString("#,##0;(#,##0); ");
                ((TextBox)this.grvacc.Rows[j].FindControl("txtgvperiod")).Text = period.ToString("#,##0;(#,##0); ");
                TblRowIndex2 = (this.grvacc.PageIndex) * this.grvacc.PageSize + j;

                tbl1.Rows[TblRowIndex2]["limit"] = crlimit;
                tbl1.Rows[TblRowIndex2]["period"] = period;
            }
            Session["storedata"] = tbl1;
            this.Data_Bind();
        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }

        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_Update();
            this.grvacc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnUpdateMat_Click(object sender, EventArgs e)
        {
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
                DataTable tblt05 = (DataTable)Session["storedata"];

                foreach (DataRow dr in tblt05.Rows)
                {

                    string ssircode = dr["sircode"].ToString();
                    string crlimit = dr["limit"].ToString();
                    string period = dr["period"].ToString();

                    bool resulta = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSORUPSUPLIMIT", ssircode, crlimit, period, "", "", "",
                                "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = this.da.ErrorObject["Msg"].ToString();
                        return;
                    }


                }


               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

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

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;


                int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;

                string sircode = ((DataTable)Session["storedata"]).Rows[index]["sircode"].ToString();
                string sirdesc = ((DataTable)Session["storedata"]).Rows[index]["sirdesc"].ToString();
                //this.lblmSupName.Text = sirdesc.ToString();
                spanSupName.InnerText = sirdesc.ToString();
                this.lblmSupCode.Text = sircode.ToString();
                string comcod = this.GetComeCode();
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETSUPPLIERCATEGORY", sircode, "", "", "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0)
                    return;

                DataTable dt = ds1.Tables[0];
                
                this.chkCategoryName.DataTextField = "sirdesc1";
                this.chkCategoryName.DataValueField = "sircode";
                this.chkCategoryName.DataSource = ds1.Tables[0];
                this.chkCategoryName.DataBind();

                //foreach (DataRow item in ds1.Tables[0].Rows)
                //{
                //    if (item["IsChecked"].ToString() == "1")
                //        this.chkCategoryName.SelectedValue = item["sircode"].ToString();

                //}

                foreach (ListItem litem in chkCategoryName.Items)
                {

                    string catcode = litem.Value;

                    string ischecked = dt.Select("sircode='" + catcode + "'")[0]["IsChecked"].ToString();
                    if (ischecked == "1")
                    {
                        litem.Selected = true;
                    }        
                
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openSupModal();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }

        }

        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string category = "";
            string supcode = this.lblmSupCode.Text.ToString();
            string gp = this.chkCategoryName.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    category = "";
                else
                    foreach (ListItem s1 in chkCategoryName.Items)
                    {
                        if (s1.Selected)
                        {
                            category = category + s1.Value.Substring(0, 9);
                        }
                    }

            }
            bool resulta = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPDATSUPCATEGORY", supcode, category, "", "", "", "",
                               "", "", "", "", "", "", "", "", "");
            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = this.da.ErrorObject["Msg"].ToString();
                return;
            }
               ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

            //Session["tblstatus"] = ds1.Tables[0];

        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label supdesc = (Label)e.Row.FindControl("lbldesc");
                string supcat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "supcat")).ToString();
                if(supcat== "A")
                {
                    supdesc.BackColor = System.Drawing.Color.SkyBlue;
                }
              
               
            }
        }
    }
}
