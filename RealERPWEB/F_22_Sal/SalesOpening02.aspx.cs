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
    public partial class SalesOpening02 : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetopeningNoDate();
                this.GetProjectName();
                this.ShowOpeningInfo();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetopeningNoDate()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALOPENDATANDNO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lblReceiveNo.Text = (ds1.Tables[0].Rows.Count == 0) ? "000000001" : ds1.Tables[0].Rows[0]["mrno"].ToString();
            this.txtOpeningDate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["mrdate"]).ToString("dd-MMM-yyyy");
            this.txtOpeningDate.Enabled = (ds1.Tables[0].Rows.Count == 0) ? true : false;
            ds1.Dispose();

        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTOPENING", "", "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }

        private void ShowOpeningInfo()
        {
            Session.Remove("tblsopening");
            string comcod = this.GetCompCode();
            string Receiveno = this.lblReceiveNo.Text.Trim();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string UnitName = "%" + this.txtSearchUnit.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALOPENINGINFO", Receiveno, pactcode, UnitName, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSOpening.DataSource = null;
                this.gvSOpening.DataBind();
                return;
            }
            Session["tblsopening"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";

                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }


            }

            return dt1;
        }



        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            int TblRowIndex;
            for (int i = 0; i < this.gvSOpening.Rows.Count; i++)
            {
                double openingamt = Convert.ToDouble("0" + ((TextBox)this.gvSOpening.Rows[i].FindControl("txtopnamt")).Text.Trim());
                TblRowIndex = (gvSOpening.PageIndex) * gvSOpening.PageSize + i;
                dt.Rows[TblRowIndex]["opnamt"] = openingamt;

            }
            Session["tblsopening"] = dt;

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            this.gvSOpening.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSOpening.DataSource = dt;
            this.gvSOpening.DataBind();
            this.FooterCal();
        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnamt)", "")) ? 0.00 : dt.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string Receiveno = this.lblReceiveNo.Text.Trim();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string UnitName = "%" + this.txtSearchUnit.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "PRINTSALOPENINGINFO", Receiveno, pactcode, UnitName, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = HiddenSameData(ds1.Tables[0]);
            ReportDocument rrs2 = new RealERPRPT.R_22_Sal.RptSalesOpening();
            TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtProDate = rrs2.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtProDate.Text = "Date: " + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs2.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs2.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rrs2;
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblsopening"];
            string comcod = this.GetCompCode();
            string mrno = this.lblReceiveNo.Text.Trim();
            string type = "82001";
            string mrdate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string paydate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string pactcode = dt.Rows[i]["pactcode"].ToString();
                string usircode = dt.Rows[i]["usircode"].ToString();
                double openingamt = Convert.ToDouble(dt.Rows[i]["opnamt"].ToString());
                bool Result;
                if (openingamt > 0)
                {
                    Result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMRINF", pactcode, usircode, mrno, type, mrdate, openingamt.ToString(), "", "", "", paydate, "", "", "", "", "");

                    if (Result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }
                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Opening";
                    string eventdesc = "Update Opening";
                    string eventdesc2 = mrno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }


            }




        }
        protected void imgbtnFindUnit_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowOpeningInfo();
        }
        protected void gvSOpening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvSOpening.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}