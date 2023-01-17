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
namespace RealERPWEB.F_03_Fin
{
    public partial class FinanceBgdEntry : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "ProCost") ? "COST BUDGET ENTRY" : (this.Request.QueryString["Type"].ToString() == "ProRevenue") ? "SALES BUDGET ENTRY" : "EQUITY BUDGET ENTRY";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "GETPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }


        private void ViewSection()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ProCost":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "ProRevenue":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "ProEquity":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;





            }

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.ShowView();
                return;
            }

            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
        }



        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ProCost":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowCost();
                    this.ShowColoumGroup(1);
                    break;

                case "ProRevenue":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowRevenue();
                    break;

                case "ProEquity":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEquity();
                    this.ShowColoumGroup1(1);
                    break;
            }

        }
        private void ShowCost()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();


            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FINANCE", "GETPROJECTCOST", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvProCost.DataSource = null;
                this.gvProCost.DataBind();
                return;

            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.lblsstartdatevalc.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["sstartdate"]).ToString("dd-MMM-yyyy");
            this.lblsenddatevalc.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["senddate"]).ToString("dd-MMM-yyyy");
            this.lblduration.Text = Convert.ToInt32(ds2.Tables[1].Rows[0]["duration"]).ToString();
            this.EnableButton(Convert.ToInt32(ds2.Tables[1].Rows[0]["duration"]));
            DateTime datefrm = Convert.ToDateTime(this.lblsstartdatevalc.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.lblsenddatevalc.Text.Trim());
            for (int i = 9; i < 68; i++)
            {
                if (datefrm > dateto)
                    break;
                this.gvProCost.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
            }

            ds2.Dispose();
        }


        private void ShowEquity()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();


            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FINANCE", "GETPROJECTEQUITY", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvProRevnue.DataSource = null;
                this.gvProRevnue.DataBind();
                return;

            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            this.lblsstartdatevale.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["sstartdate"]).ToString("dd-MMM-yyyy");
            this.lblsenddatevale.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["senddate"]).ToString("dd-MMM-yyyy");
            this.lblduratione.Text = Convert.ToInt32(ds2.Tables[1].Rows[0]["duration"]).ToString();
            this.EnableButton1(Convert.ToInt32(ds2.Tables[1].Rows[0]["duration"]));
            DateTime datefrm = Convert.ToDateTime(this.lblsstartdatevale.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.lblsenddatevale.Text.Trim());
            for (int i = 5; i < 64; i++)
            {
                if (datefrm > dateto)
                    break;
                this.gvProRevnue.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
            }

            ds2.Dispose();

        }
        private void EnableButton(int duration)
        {
            switch (duration)
            {

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    this.lbtngvP2.Enabled = false;
                    this.lbtngvP3.Enabled = false;
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    this.lbtngvP3.Enabled = false;
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;



                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;


                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;


                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    this.lbtngvP9.Enabled = false;
                    break;

                default:
                    break;


            }


            //  int Duration=this.lblDuration.Text.IndexOf(

        }


        private void EnableButton1(int duration)
        {
            switch (duration)
            {

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    this.lbtngvPe2.Enabled = false;
                    this.lbtngvPe3.Enabled = false;
                    this.lbtngvPe4.Enabled = false;
                    this.lbtngvPe5.Enabled = false;
                    this.lbtngvPe6.Enabled = false;
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    this.lbtngvPe3.Enabled = false;
                    this.lbtngvPe4.Enabled = false;
                    this.lbtngvPe5.Enabled = false;
                    this.lbtngvPe6.Enabled = false;
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;



                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    this.lbtngvPe4.Enabled = false;
                    this.lbtngvPe5.Enabled = false;
                    this.lbtngvPe6.Enabled = false;
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;


                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    this.lbtngvPe5.Enabled = false;
                    this.lbtngvPe6.Enabled = false;
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;

                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    this.lbtngvPe6.Enabled = false;
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;


                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                    this.lbtngvPe7.Enabled = false;
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;

                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    this.lbtngvPe8.Enabled = false;
                    this.lbtngvPe9.Enabled = false;
                    break;

                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    this.lbtngvPe9.Enabled = false;
                    break;

                default:
                    break;


            }


            //  int Duration=this.lblDuration.Text.IndexOf(

        }



        private void ShowRevenue()
        {

            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FINANCE", "GETPROJECTREVENUE", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvPrjsalrev.DataSource = null;
                this.gvPrjsalrev.DataBind();
                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];


            this.lblsstartdateval.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["sstartdate"]).ToString("dd-MMM-yyyy");
            this.lblinstallmentval.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["toinsment"]).ToString("#,##0;(#,##0); ");
            this.lblsenddateval.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["senddate"]).ToString("dd-MMM-yyyy");
            this.lblSaleablesftval.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["salarea"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblbookingval.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["bookper"]).ToString("#,##0.00;(#,##0.00); ");
            ds2.Dispose();
            this.Data_Bind();
        }

        private void SaveRevenue()
        {
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            double Salarea = Convert.ToDouble(this.lblSaleablesftval.Text.Trim());
            for (int i = 0; i < this.gvPrjsalrev.Rows.Count; i++)
            {
                double percnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvpercent")).Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvrate")).Text.Trim()));
                double persft = Salarea * percnt * 0.01;
                dt.Rows[i]["percnt"] = percnt;
                dt.Rows[i]["persft"] = persft;
                dt.Rows[i]["rate"] = rate;
                dt.Rows[i]["salam"] = persft * rate;

            }

        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveRevenue();
            this.Data_Bind();

        }
        protected void lbtnFUpdatesale_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveRevenue();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string yearmon = dt.Rows[i]["ymon"].ToString();
                string percnt = Convert.ToDouble(dt.Rows[i]["percnt"]).ToString();
                string persft = Convert.ToDouble(dt.Rows[i]["persft"]).ToString();
                string salam = Convert.ToDouble(dt.Rows[i]["salam"]).ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCE", "INORUPPROREVENUE", PactCode, yearmon, percnt, persft, salam, "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    return;

                }

            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            string subgrp = dt1.Rows[0]["subgrp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["subgrp"].ToString() == subgrp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["subgrpdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["subgrp"].ToString() == subgrp)
                    {
                        dt1.Rows[j]["subgrpdesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();

                }

            }
            return dt1;

        }



        private void Data_Bind()
        {
            string comcod = this.GetComCode();
            string Type = this.Request.QueryString["Type"].ToString();

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            switch (Type)
            {

                case "ProCost":
                    this.gvProCost.DataSource = dt;
                    this.gvProCost.DataBind();
                    //this.FooterCal();
                    break;


                case "ProRevenue":
                    this.gvPrjsalrev.DataSource = dt;
                    this.gvPrjsalrev.DataBind();
                    this.FooterCal();
                    break;

                case "ProEquity":
                    this.gvProRevnue.DataSource = dt;
                    this.gvProRevnue.DataBind();
                    break;
            }


        }
        private void FooterCal()
        {
            string comcod = this.GetComCode();
            try
            {
                DataTable dt = (DataTable)ViewState["tblfeaprj"];
                if (dt.Rows.Count == 0)
                    return;

                string Type = this.Request.QueryString["Type"].ToString();
                switch (Type)
                {

                    case "ProCost":

                        break;


                    case "ProRevenue":
                        ((Label)this.gvPrjsalrev.FooterRow.FindControl("lbkgvFpercent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(percnt)", "")) ?
                         0.00 : dt.Compute("Sum(percnt)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvPrjsalrev.FooterRow.FindControl("lblgvFsft")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(persft)", "")) ?
                            0.00 : dt.Compute("Sum(persft)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvPrjsalrev.FooterRow.FindControl("lgvFsalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salam)", "")) ?
                         0.00 : dt.Compute("Sum(salam)", ""))).ToString("#,##0;(#,##0); ");
                        break;



                    case "ProEquity":
                        this.MultiView1.ActiveViewIndex = 2;
                        break;

                }

            }

            catch (Exception ex)
            {
            }

        }


        private void UpdateProjectSaleAndCost()
        {

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Description = dt.Rows[i]["infdesc"].ToString();
                string Qty = Convert.ToDouble(dt.Rows[i]["qty"]).ToString();
                string Esamt = Convert.ToDouble(dt.Rows[i]["estam"]).ToString();
                string Aadam = Convert.ToDouble(dt.Rows[i]["aadam"]).ToString();
                string Exadam = Convert.ToDouble(dt.Rows[i]["eadam"]).ToString();
                string savam = Convert.ToDouble(dt.Rows[i]["savam"]).ToString();
                double Toam = Convert.ToDouble(dt.Rows[i]["totalam"]);
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPFEAPRJCTCOST02", PactCode, ResCode, Description, Qty, Esamt, Aadam, Exadam, savam, Toam.ToString(), "", "", "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.ShowCost();

        }














        protected void lbtnFUpdateSales_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ResCode = dt.Rows[i]["infcod"].ToString();
                string Desc = dt.Rows[i]["infdesc"].ToString();
                double lsizes = Convert.ToDouble(dt.Rows[i]["lsizes"]);
                string lowner = Convert.ToDouble(dt.Rows[i]["lowner"]).ToString();
                string company = Convert.ToDouble(dt.Rows[i]["company"]).ToString();
                string Pflowner = Convert.ToDouble(dt.Rows[i]["pflowner"]).ToString();
                string adjmnt = Convert.ToDouble(dt.Rows[i]["adjmnt"]).ToString();

                //if (lsizes > 0)
                //{
                //feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPDATEFEAPRJCT", PactCode, ResCode, Size, Number, Amt, salrate, "", "", "", "", "", "", "","","");
                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPFEAPRJCTSALE02", PactCode, ResCode, Desc, lsizes.ToString(), lowner, company, Pflowner, adjmnt, "", "", "", "", "", "", "");
                // }


            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }





        protected void lbtnTotalsalrev_Click(object sender, EventArgs e)
        {
            double toamt = 0.00, tlsizes = 0.00;
            for (int i = 0; i < this.gvPrjsalrev.Rows.Count; i++)
            {

                string code = ((Label)this.gvPrjsalrev.Rows[i].FindControl("lblgvItmCodsalrev")).Text.Trim();
                double lsizes = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim()));
                double amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvamtsalrev")).Text.Trim()));
                double rate = (lsizes != 0) ? amt / lsizes : 0;
                amt = (lsizes != 0) ? Math.Abs(rate) * lsizes : 0;
                ((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvamtsalrev")).Text = amt.ToString("#,##0;-#,##0; ");
                ((Label)this.gvPrjsalrev.Rows[i].FindControl("lblgratesalrev")).Text = rate.ToString("#,##0;(#,##0); ");

                if (ASTUtility.Left(code, 4) != "0103")
                {
                    tlsizes = tlsizes + lsizes;
                }
                toamt = toamt + amt;
            }

            ((Label)this.gvPrjsalrev.FooterRow.FindControl("lgvFlsisessalrev")).Text = tlsizes.ToString("#,##0;(#,##0); ");
            ((Label)this.gvPrjsalrev.FooterRow.FindControl("lgvFamtsalrev")).Text = toamt.ToString("#,##0;(#,##0); ");
        }
        protected void lbtnFUpdatesalrev_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPrjsalrev.Rows.Count; i++)
            {

                string Code = ((Label)this.gvPrjsalrev.Rows[i].FindControl("lblgvItmCodsalrev")).Text.Trim();
                string Desc = ((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvItemdescsalrev")).Text.Trim();
                // Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim()));
                //  string lsizes = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim()).ToString();
                string lsizes = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtglsizessalrev")).Text.Trim())).ToString();
                double salamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgvamtsalrev")).Text.Trim()));
                string brorat = Convert.ToDouble("0" + ((TextBox)this.gvPrjsalrev.Rows[i].FindControl("txtgbroratsalrev")).Text.Trim()).ToString();
                //if (salamt > 0)
                //{

                bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INORUPPROFEASALREGFV", PactCode, Code, Desc, lsizes, salamt.ToString(), brorat, "", "", "", "", "", "", "", "", "");
                // }
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //this.ShowRevenue(); 

        }






        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");


                }

            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblfeaprj"];

            int rowindex;
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ProRevenue":
                    for (int i = 0; i < this.gvProCost.Rows.Count; i++)
                    {

                        double pconstam = Convert.ToDouble("0" + ((TextBox)this.gvProCost.Rows[i].FindControl("txtgvpreconstam")).Text.Trim());

                        for (int j = 1; j <= 60; j++)
                        {
                            string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                            double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvProCost.Rows[i].FindControl(gvQty1)).Text.Trim());
                            if (this.gvProCost.Columns[j + 6].Visible)
                            {
                                rowindex = (this.gvProCost.PageSize * this.gvProCost.PageIndex) + i;
                                tbl1.Rows[rowindex]["ym" + j.ToString()] = gvQty2;
                            }
                        }
                        rowindex = (this.gvProCost.PageSize * this.gvProCost.PageIndex) + i;
                        tbl1.Rows[rowindex]["pconstam"] = pconstam;


                    }

                    break;



                case "ProEquity":
                    for (int i = 0; i < this.gvProRevnue.Rows.Count; i++)
                    {

                        double pconstam = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvProRevnue.Rows[i].FindControl("txtgvpreconstamEqty")).Text.Trim()));

                        for (int j = 1; j <= 60; j++)
                        {
                            string gvQty1 = "txtgvQtyEqty" + ASTUtility.Right("00" + j.ToString(), 3);
                            double gvQty2 = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvProRevnue.Rows[i].FindControl(gvQty1)).Text.Trim()));
                            if (this.gvProCost.Columns[j + 6].Visible)
                            {
                                rowindex = (this.gvProRevnue.PageSize * this.gvProRevnue.PageIndex) + i;
                                tbl1.Rows[rowindex]["ym" + j.ToString()] = gvQty2;
                            }
                        }
                        rowindex = (this.gvProRevnue.PageSize * this.gvProRevnue.PageIndex) + i;
                        tbl1.Rows[rowindex]["pconstam"] = pconstam;


                    }

                    break;

            }



            ViewState["tblfeaprj"] = tbl1;
        }
        protected void ShowColoumGroup(int i)
        {

            this.Data_Bind();
            i = (i > 9 ? 1 : (i < 1 ? 9 : i));
            this.gvProCost.Columns[9].Visible = (i == 1);
            this.gvProCost.Columns[10].Visible = (i == 1);
            this.gvProCost.Columns[11].Visible = (i == 1);
            this.gvProCost.Columns[12].Visible = (i == 1);
            this.gvProCost.Columns[13].Visible = (i == 1);
            this.gvProCost.Columns[14].Visible = (i == 1);
            this.gvProCost.Columns[15].Visible = (i == 1);

            this.gvProCost.Columns[16].Visible = (i == 2);
            this.gvProCost.Columns[17].Visible = (i == 2);
            this.gvProCost.Columns[18].Visible = (i == 2);
            this.gvProCost.Columns[19].Visible = (i == 2);
            this.gvProCost.Columns[20].Visible = (i == 2);
            this.gvProCost.Columns[21].Visible = (i == 2);
            this.gvProCost.Columns[22].Visible = (i == 2);

            this.gvProCost.Columns[23].Visible = (i == 3);
            this.gvProCost.Columns[24].Visible = (i == 3);
            this.gvProCost.Columns[25].Visible = (i == 3);
            this.gvProCost.Columns[26].Visible = (i == 3);
            this.gvProCost.Columns[27].Visible = (i == 3);
            this.gvProCost.Columns[28].Visible = (i == 3);
            this.gvProCost.Columns[29].Visible = (i == 3);

            this.gvProCost.Columns[30].Visible = (i == 4);
            this.gvProCost.Columns[31].Visible = (i == 4);
            this.gvProCost.Columns[32].Visible = (i == 4);
            this.gvProCost.Columns[33].Visible = (i == 4);
            this.gvProCost.Columns[34].Visible = (i == 4);
            this.gvProCost.Columns[35].Visible = (i == 4);
            this.gvProCost.Columns[36].Visible = (i == 4);

            this.gvProCost.Columns[37].Visible = (i == 5);
            this.gvProCost.Columns[38].Visible = (i == 5);
            this.gvProCost.Columns[39].Visible = (i == 5);
            this.gvProCost.Columns[40].Visible = (i == 5);
            this.gvProCost.Columns[41].Visible = (i == 5);
            this.gvProCost.Columns[42].Visible = (i == 5);
            this.gvProCost.Columns[43].Visible = (i == 5);

            this.gvProCost.Columns[44].Visible = (i == 6);
            this.gvProCost.Columns[45].Visible = (i == 6);
            this.gvProCost.Columns[46].Visible = (i == 6);
            this.gvProCost.Columns[47].Visible = (i == 6);
            this.gvProCost.Columns[48].Visible = (i == 6);
            this.gvProCost.Columns[49].Visible = (i == 6);
            this.gvProCost.Columns[50].Visible = (i == 6);

            this.gvProCost.Columns[51].Visible = (i == 7);
            this.gvProCost.Columns[52].Visible = (i == 7);
            this.gvProCost.Columns[53].Visible = (i == 7);
            this.gvProCost.Columns[54].Visible = (i == 7);
            this.gvProCost.Columns[55].Visible = (i == 7);
            this.gvProCost.Columns[56].Visible = (i == 7);

            this.gvProCost.Columns[57].Visible = (i == 8);
            this.gvProCost.Columns[58].Visible = (i == 8);
            this.gvProCost.Columns[59].Visible = (i == 8);
            this.gvProCost.Columns[60].Visible = (i == 8);
            this.gvProCost.Columns[61].Visible = (i == 8);
            this.gvProCost.Columns[62].Visible = (i == 8);
            this.gvProCost.Columns[63].Visible = (i == 8);
            this.gvProCost.Columns[64].Visible = (i == 8);


            this.gvProCost.Columns[65].Visible = (i == 9);
            this.gvProCost.Columns[66].Visible = (i == 9);
            this.gvProCost.Columns[67].Visible = (i == 9);
            this.gvProCost.Columns[68].Visible = (i == 9);
            this.lblColGroup.Text = Convert.ToString(i);
            this.FooterAmt((DataTable)ViewState["tblfeaprj"]);

        }


        private void ShowColoumGroup1(int i)
        {
            this.Data_Bind();
            i = (i > 9 ? 1 : (i < 1 ? 9 : i));
            this.gvProRevnue.Columns[5].Visible = (i == 1);
            this.gvProRevnue.Columns[6].Visible = (i == 1);
            this.gvProRevnue.Columns[7].Visible = (i == 1);
            this.gvProRevnue.Columns[8].Visible = (i == 1);
            this.gvProRevnue.Columns[9].Visible = (i == 1);
            this.gvProRevnue.Columns[10].Visible = (i == 1);
            this.gvProRevnue.Columns[11].Visible = (i == 1);

            this.gvProRevnue.Columns[12].Visible = (i == 2);
            this.gvProRevnue.Columns[13].Visible = (i == 2);
            this.gvProRevnue.Columns[14].Visible = (i == 2);
            this.gvProRevnue.Columns[15].Visible = (i == 2);
            this.gvProRevnue.Columns[16].Visible = (i == 2);
            this.gvProRevnue.Columns[17].Visible = (i == 2);
            this.gvProRevnue.Columns[18].Visible = (i == 2);

            this.gvProRevnue.Columns[19].Visible = (i == 3);
            this.gvProRevnue.Columns[20].Visible = (i == 3);
            this.gvProRevnue.Columns[21].Visible = (i == 3);
            this.gvProRevnue.Columns[22].Visible = (i == 3);
            this.gvProRevnue.Columns[23].Visible = (i == 3);
            this.gvProRevnue.Columns[24].Visible = (i == 3);
            this.gvProRevnue.Columns[25].Visible = (i == 3);

            this.gvProRevnue.Columns[26].Visible = (i == 4);
            this.gvProRevnue.Columns[27].Visible = (i == 4);
            this.gvProRevnue.Columns[28].Visible = (i == 4);
            this.gvProRevnue.Columns[29].Visible = (i == 4);
            this.gvProRevnue.Columns[30].Visible = (i == 4);
            this.gvProRevnue.Columns[31].Visible = (i == 4);
            this.gvProRevnue.Columns[32].Visible = (i == 4);

            this.gvProRevnue.Columns[33].Visible = (i == 5);
            this.gvProRevnue.Columns[34].Visible = (i == 5);
            this.gvProRevnue.Columns[35].Visible = (i == 5);
            this.gvProRevnue.Columns[36].Visible = (i == 5);
            this.gvProRevnue.Columns[37].Visible = (i == 5);
            this.gvProRevnue.Columns[38].Visible = (i == 5);
            this.gvProRevnue.Columns[39].Visible = (i == 5);

            this.gvProRevnue.Columns[40].Visible = (i == 6);
            this.gvProRevnue.Columns[41].Visible = (i == 6);
            this.gvProRevnue.Columns[42].Visible = (i == 6);
            this.gvProRevnue.Columns[43].Visible = (i == 6);
            this.gvProRevnue.Columns[44].Visible = (i == 6);
            this.gvProRevnue.Columns[45].Visible = (i == 6);
            this.gvProRevnue.Columns[46].Visible = (i == 6);

            this.gvProRevnue.Columns[47].Visible = (i == 7);
            this.gvProRevnue.Columns[48].Visible = (i == 7);
            this.gvProRevnue.Columns[49].Visible = (i == 7);
            this.gvProRevnue.Columns[50].Visible = (i == 7);
            this.gvProRevnue.Columns[51].Visible = (i == 7);
            this.gvProRevnue.Columns[52].Visible = (i == 7);

            this.gvProRevnue.Columns[53].Visible = (i == 8);
            this.gvProRevnue.Columns[54].Visible = (i == 8);
            this.gvProRevnue.Columns[55].Visible = (i == 8);
            this.gvProRevnue.Columns[56].Visible = (i == 8);
            this.gvProRevnue.Columns[57].Visible = (i == 8);
            this.gvProRevnue.Columns[58].Visible = (i == 8);
            this.gvProRevnue.Columns[59].Visible = (i == 8);
            this.gvProRevnue.Columns[60].Visible = (i == 8);


            this.gvProRevnue.Columns[61].Visible = (i == 9);
            this.gvProRevnue.Columns[62].Visible = (i == 9);
            this.gvProRevnue.Columns[63].Visible = (i == 9);
            this.gvProRevnue.Columns[64].Visible = (i == 9);
            this.lblColGroupe.Text = Convert.ToString(i);
            this.FooterAmt((DataTable)ViewState["tblfeaprj"]);

        }
        private void FooterAmt(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {


                case "ProCost":
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcostam)", "")) ?
                        0.00 : dt.Compute("Sum(tcostam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFalloamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tdiscost)", "")) ?
                        0.00 : dt.Compute("Sum(tdiscost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFdifamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(diffam)", "")) ?
                     0.00 : dt.Compute("Sum(diffam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFpreconstam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pconstam)", "")) ?
                   0.00 : dt.Compute("Sum(pconstam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym1qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
                       : dt.Compute("Sum(ym1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym2qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
                      : dt.Compute("Sum(ym2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym3qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
                      : dt.Compute("Sum(ym3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym4qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
                      : dt.Compute("Sum(ym4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym5qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
                      : dt.Compute("Sum(ym5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym6qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
                      : dt.Compute("Sum(ym6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym7qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
                      : dt.Compute("Sum(ym7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym8qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
                      : dt.Compute("Sum(ym8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym9qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
                      : dt.Compute("Sum(ym9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym10qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
                      : dt.Compute("Sum(ym10)", ""))).ToString("#,##0;(#,##0); ");




                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym11qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
                        : dt.Compute("Sum(ym11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym12qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
                      : dt.Compute("Sum(ym12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym13qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
                      : dt.Compute("Sum(ym13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym14qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
                      : dt.Compute("Sum(ym14)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym15qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
                      : dt.Compute("Sum(ym15)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym16qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
                      : dt.Compute("Sum(ym16)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym17qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
                      : dt.Compute("Sum(ym17)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym18qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
                      : dt.Compute("Sum(ym18)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym19qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
                      : dt.Compute("Sum(ym19)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym20qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
                      : dt.Compute("Sum(ym20)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym21qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
                        : dt.Compute("Sum(ym21)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym22qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
                      : dt.Compute("Sum(ym22)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym23qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
                      : dt.Compute("Sum(ym23)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym24qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
                      : dt.Compute("Sum(ym24)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym25qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
                      : dt.Compute("Sum(ym25)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym26qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
                      : dt.Compute("Sum(ym26)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym27qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
                      : dt.Compute("Sum(ym27)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym28qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
                      : dt.Compute("Sum(ym28)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym29qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
                      : dt.Compute("Sum(ym29)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym30qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
                      : dt.Compute("Sum(ym30)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym31qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
                        : dt.Compute("Sum(ym31)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym32qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
                      : dt.Compute("Sum(ym32)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym33qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
                      : dt.Compute("Sum(ym33)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym34qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
                      : dt.Compute("Sum(ym34)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym35qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
                      : dt.Compute("Sum(ym35)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym36qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
                      : dt.Compute("Sum(ym36)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym37qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
                      : dt.Compute("Sum(ym37)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym38qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
                      : dt.Compute("Sum(ym38)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym39qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
                      : dt.Compute("Sum(ym39)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym40qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
                      : dt.Compute("Sum(ym40)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym41qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
                        : dt.Compute("Sum(ym41)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym42qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
                      : dt.Compute("Sum(ym42)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym43qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
                      : dt.Compute("Sum(ym43)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym44qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
                      : dt.Compute("Sum(ym44)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym45qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
                      : dt.Compute("Sum(ym45)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym46qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
                      : dt.Compute("Sum(ym46)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym47qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
                      : dt.Compute("Sum(ym47)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym48qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
                      : dt.Compute("Sum(ym48)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym49qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
                      : dt.Compute("Sum(ym49)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym50qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
                      : dt.Compute("Sum(ym50)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym51qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
                        : dt.Compute("Sum(ym51)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym52qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
                      : dt.Compute("Sum(ym52)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym53qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
                      : dt.Compute("Sum(ym53)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym54qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
                      : dt.Compute("Sum(ym54)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym55qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
                      : dt.Compute("Sum(ym55)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym56qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
                      : dt.Compute("Sum(ym56)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym57qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
                      : dt.Compute("Sum(ym57)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym58qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
                      : dt.Compute("Sum(ym58)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym59qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
                      : dt.Compute("Sum(ym59)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProCost.FooterRow.FindControl("lblgvFym60qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
                      : dt.Compute("Sum(ym60)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "ProEquity":

                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFalloamountEqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tdiseqty)", "")) ?
                            0.00 : dt.Compute("Sum(tdiseqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFpreconstamEqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pconstam)", "")) ?
                       0.00 : dt.Compute("Sum(pconstam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
                           : dt.Compute("Sum(ym1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
                          : dt.Compute("Sum(ym2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
                          : dt.Compute("Sum(ym3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
                          : dt.Compute("Sum(ym4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
                          : dt.Compute("Sum(ym5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
                          : dt.Compute("Sum(ym6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
                          : dt.Compute("Sum(ym7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
                          : dt.Compute("Sum(ym8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
                          : dt.Compute("Sum(ym9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
                          : dt.Compute("Sum(ym10)", ""))).ToString("#,##0;(#,##0); ");




                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
                            : dt.Compute("Sum(ym11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
                          : dt.Compute("Sum(ym12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
                          : dt.Compute("Sum(ym13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
                          : dt.Compute("Sum(ym14)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
                          : dt.Compute("Sum(ym15)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
                          : dt.Compute("Sum(ym16)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
                          : dt.Compute("Sum(ym17)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
                          : dt.Compute("Sum(ym18)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
                          : dt.Compute("Sum(ym19)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
                          : dt.Compute("Sum(ym20)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
                            : dt.Compute("Sum(ym21)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
                          : dt.Compute("Sum(ym22)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
                          : dt.Compute("Sum(ym23)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
                          : dt.Compute("Sum(ym24)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
                          : dt.Compute("Sum(ym25)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
                          : dt.Compute("Sum(ym26)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
                          : dt.Compute("Sum(ym27)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
                          : dt.Compute("Sum(ym28)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
                          : dt.Compute("Sum(ym29)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
                          : dt.Compute("Sum(ym30)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
                            : dt.Compute("Sum(ym31)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
                          : dt.Compute("Sum(ym32)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
                          : dt.Compute("Sum(ym33)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
                          : dt.Compute("Sum(ym34)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
                          : dt.Compute("Sum(ym35)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
                          : dt.Compute("Sum(ym36)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
                          : dt.Compute("Sum(ym37)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
                          : dt.Compute("Sum(ym38)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
                          : dt.Compute("Sum(ym39)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
                          : dt.Compute("Sum(ym40)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
                            : dt.Compute("Sum(ym41)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
                          : dt.Compute("Sum(ym42)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
                          : dt.Compute("Sum(ym43)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
                          : dt.Compute("Sum(ym44)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
                          : dt.Compute("Sum(ym45)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
                          : dt.Compute("Sum(ym46)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
                          : dt.Compute("Sum(ym47)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
                          : dt.Compute("Sum(ym48)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
                          : dt.Compute("Sum(ym49)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
                          : dt.Compute("Sum(ym50)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
                            : dt.Compute("Sum(ym51)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
                          : dt.Compute("Sum(ym52)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
                          : dt.Compute("Sum(ym53)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
                          : dt.Compute("Sum(ym54)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
                          : dt.Compute("Sum(ym55)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
                          : dt.Compute("Sum(ym56)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
                          : dt.Compute("Sum(ym57)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
                          : dt.Compute("Sum(ym58)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
                          : dt.Compute("Sum(ym59)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProRevnue.FooterRow.FindControl("lblgvFymEqty60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
                          : dt.Compute("Sum(ym60)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }







        }


        protected void gvProCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProCost.PageIndex = e.NewPageIndex;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));


        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            this.SaveValue();
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));

            DataTable tbl1 = (DataTable)Session["tblfeaprj"];
            // DataTable tblym = ((DataTable)Session["tblpymon"]);

            bool result = false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string infcod = tbl1.Rows[i]["infcod"].ToString();
                DateTime datefrm = Convert.ToDateTime(this.lblsstartdatevalc.Text.Trim());
                DateTime dateto = Convert.ToDateTime(this.lblsenddatevalc.Text.Trim());
                int count = Convert.ToInt32(this.lblduration.Text);
                string PreConsamt = Convert.ToDouble(tbl1.Rows[i]["pconstam"]).ToString();


                for (int j = 0; j <= count; j++)
                {

                    string yearmon = datefrm.ToString("yyyyMM");
                    double costam = Convert.ToDouble(tbl1.Rows[i]["ym" + (j + 1).ToString()]);

                    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCE", "INORUPPROCOST", pactcode, infcod, yearmon, PreConsamt, costam.ToString(), "", "", "", "", "", "", "", "", "", "");
                    datefrm = datefrm.AddMonths(1);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);




        }
        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblfeaprj"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                double todiscost = Convert.ToDouble(tbl1.Rows[i]["pconstam"]);
                for (int j = 1; j <= 60; j++)
                    todiscost = todiscost + Convert.ToDouble(tbl1.Rows[i]["ym" + j.ToString()]);



                tbl1.Rows[i]["tdiscost"] = todiscost;
                tbl1.Rows[i]["diffam"] = Convert.ToDouble(tbl1.Rows[i]["tcostam"]) - todiscost;

            }
            Session["tblfeaprj"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));
        }


        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.ShowColoumGroup(Convert.ToInt32(((LinkButton)sender).Text));
        }
        protected void lbtngvPe_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.ShowColoumGroup1(Convert.ToInt32(((LinkButton)sender).Text));

        }
        protected void lbtnTotalrevEqty_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblfeaprj"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                double tdiseqty = Convert.ToDouble(tbl1.Rows[i]["pconstam"]);
                for (int j = 1; j <= 60; j++)
                    tdiseqty = tdiseqty + Convert.ToDouble(tbl1.Rows[i]["ym" + j.ToString()]);
                tbl1.Rows[i]["tdiseqty"] = tdiseqty;


            }
            Session["tblfeaprj"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));
        }
        protected void lbtnUpdateEqty_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            this.SaveValue();
            this.ShowColoumGroup1(Convert.ToInt32(this.lblColGroup.Text));
            DataTable tbl1 = (DataTable)Session["tblfeaprj"];
            // DataTable tblym = ((DataTable)Session["tblpymon"]);

            bool result = false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string infcod = tbl1.Rows[i]["infcod"].ToString();
                DateTime datefrm = Convert.ToDateTime(this.lblsstartdatevale.Text.Trim());
                DateTime dateto = Convert.ToDateTime(this.lblsenddatevale.Text.Trim());
                int count = Convert.ToInt32(this.lblduratione.Text);
                string PreConsamt = Convert.ToDouble(tbl1.Rows[i]["pconstam"]).ToString();


                for (int j = 0; j <= count; j++)
                {

                    string yearmon = datefrm.ToString("yyyyMM");
                    double costam = Convert.ToDouble(tbl1.Rows[i]["ym" + (j + 1).ToString()]);

                    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FINANCE", "INORUPPROREVENUE02", pactcode, infcod, yearmon, PreConsamt, costam.ToString(), "", "", "", "", "", "", "", "", "", "");
                    datefrm = datefrm.AddMonths(1);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);

        }
        protected void gvProRevnue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProRevnue.PageIndex = e.NewPageIndex;
            this.ShowColoumGroup1(Convert.ToInt32(this.lblColGroup.Text));

        }
    }
}