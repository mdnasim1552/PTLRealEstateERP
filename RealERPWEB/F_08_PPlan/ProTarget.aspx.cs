using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_08_PPlan
{
    public partial class ProTarget : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Construction Planning";
            }

            if (this.ddlProject.Items.Count == 0)
            {
                this.GetProjectName();

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
            string srchTxt = this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.ddlProject.Visible = false;
                this.lblProjectDesc.Visible = true;
                this.PnlColoumn.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.ShowPtarget();
                this.ShowColoumGroup(1);
                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblProjectDesc.Visible = false;
            this.PnlColoumn.Visible = false;
            this.lblStartDate.Text = "";
            this.lblEndDate.Text = "";
            this.lblDuration.Text = "";
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.lbtngvP2.Enabled = true;
            this.lbtngvP3.Enabled = true;
            this.lbtngvP4.Enabled = true;
            this.lbtngvP5.Enabled = true;
            this.lbtngvP6.Enabled = true;
            this.lbtngvP7.Enabled = true;
            this.lbtngvP8.Enabled = true;
            this.lbtngvP9.Enabled = true;
            this.gvProTarget.DataSource = null;
            this.gvProTarget.DataBind();


        }

        private void ShowPtarget()
        {
            Session.Remove("tblptarget");
            Session.Remove("tblpymon");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROJECTTARGET", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProTarget.DataSource = null;
                this.gvProTarget.DataBind();
                return;

            }
            Session["tblptarget"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblpymon"] = ds1.Tables[1];
            DataTable dt = ds1.Tables[1];
            int j = 6;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.gvProTarget.Columns[j].HeaderText = dt.Rows[i]["monyear"].ToString();
                j++;
            }


            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[2].Rows[0]["duration"]).ToString("#,#0;(#,#0); ") + " Month";
            this.EnableButton(Convert.ToInt32(ds1.Tables[2].Rows[0]["duration"]));
            ds1.Dispose();
            // this.FooterCal();


        }


        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblptarget"];

            if (dt.Rows.Count == 0)
            {
                return;
            }

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFBgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ? 0.00
            //    : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFAloqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totrqty)", "")) ? 0.00
            //    : dt.Compute("Sum(totrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFDifqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(difqty)", "")) ? 0.00
            //    : dt.Compute("Sum(difqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //double sum = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
            //   : dt.Compute("Sum(ym1)", "")));

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym1qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ym1)", "")) ? 0.00 : dt.Compute("sum(ym1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym1qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
                : dt.Compute("Sum(ym1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym2qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
                : dt.Compute("Sum(ym2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym3qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
                : dt.Compute("Sum(ym3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym4qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
                : dt.Compute("Sum(ym4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym5qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
                : dt.Compute("Sum(ym5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym6qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
                : dt.Compute("Sum(ym6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym7qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
                : dt.Compute("Sum(ym7)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym8qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
                : dt.Compute("Sum(ym8)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym9qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
                : dt.Compute("Sum(ym9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym10qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
                : dt.Compute("Sum(ym10)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym11qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
               : dt.Compute("Sum(ym11)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym12qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
                : dt.Compute("Sum(ym12)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym13qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
                : dt.Compute("Sum(ym13)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym14qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
                : dt.Compute("Sum(ym14)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym15qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
                : dt.Compute("Sum(ym15)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym16qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
                : dt.Compute("Sum(ym16)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym17qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
                : dt.Compute("Sum(ym17)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym18qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
                : dt.Compute("Sum(ym18)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym19qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
                : dt.Compute("Sum(ym19)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym20qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
                : dt.Compute("Sum(ym20)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym21qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
               : dt.Compute("Sum(ym21)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym22qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
                : dt.Compute("Sum(ym22)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym23qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
                : dt.Compute("Sum(ym23)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym24qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
                : dt.Compute("Sum(ym24)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym25qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
                : dt.Compute("Sum(ym25)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym26qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
                : dt.Compute("Sum(ym26)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym27qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
                : dt.Compute("Sum(ym27)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym28qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
                : dt.Compute("Sum(ym28)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym29qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
                : dt.Compute("Sum(ym29)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym30qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
                : dt.Compute("Sum(ym30)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym31qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
               : dt.Compute("Sum(ym31)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym32qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
                : dt.Compute("Sum(ym32)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym33qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
                : dt.Compute("Sum(ym33)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym34qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
                : dt.Compute("Sum(ym34)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym35qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
                : dt.Compute("Sum(ym35)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym36qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
                : dt.Compute("Sum(ym36)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym37qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
                : dt.Compute("Sum(ym37)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym38qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
                : dt.Compute("Sum(ym38)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym39qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
                : dt.Compute("Sum(ym39)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym40qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
                : dt.Compute("Sum(ym40)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym41qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
               : dt.Compute("Sum(ym41)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym42qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
                : dt.Compute("Sum(ym42)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym43qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
                : dt.Compute("Sum(ym43)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym44qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
                : dt.Compute("Sum(ym44)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym45qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
                : dt.Compute("Sum(ym45)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym46qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
                : dt.Compute("Sum(ym46)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym47qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
                : dt.Compute("Sum(ym47)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym48qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
                : dt.Compute("Sum(ym48)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym49qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
                : dt.Compute("Sum(ym49)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym50qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
                : dt.Compute("Sum(ym50)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym51qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
               : dt.Compute("Sum(ym51)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym52qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
                : dt.Compute("Sum(ym52)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym53qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
                : dt.Compute("Sum(ym53)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym54qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
                : dt.Compute("Sum(ym54)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym55qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
                : dt.Compute("Sum(ym55)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym56qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
                : dt.Compute("Sum(ym56)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym57qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
                : dt.Compute("Sum(ym57)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym58qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
                : dt.Compute("Sum(ym58)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym59qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
                : dt.Compute("Sum(ym59)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym60qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
                : dt.Compute("Sum(ym60)", ""))).ToString("#,##0.00;(#,##0.00); ");



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

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string isircode = dt1.Rows[0]["isircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["isircode"].ToString() == isircode)
                {
                    isircode = dt1.Rows[j]["isircode"].ToString();
                    dt1.Rows[j]["isirdesc"] = "";
                    dt1.Rows[j]["isirunit"] = "";
                }

                else
                {
                    isircode = dt1.Rows[j]["isircode"].ToString();
                }
            }

            return dt1;

        }

        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.Refresh_Analysis_Session();
            this.ShowColoumGroup(Convert.ToInt32(((LinkButton)sender).Text));
        }

        protected void ShowColoumGroup(int i)
        {
            // this.Refresh_Analysis_Session();
            this.Data_Bind();
            i = (i > 9 ? 1 : (i < 1 ? 9 : i));
            this.gvProTarget.Columns[6].Visible = (i == 1);
            this.gvProTarget.Columns[7].Visible = (i == 1);
            this.gvProTarget.Columns[8].Visible = (i == 1);
            this.gvProTarget.Columns[9].Visible = (i == 1);
            this.gvProTarget.Columns[10].Visible = (i == 1);
            this.gvProTarget.Columns[11].Visible = (i == 1);
            this.gvProTarget.Columns[12].Visible = (i == 1);

            this.gvProTarget.Columns[13].Visible = (i == 2);
            this.gvProTarget.Columns[14].Visible = (i == 2);
            this.gvProTarget.Columns[15].Visible = (i == 2);
            this.gvProTarget.Columns[16].Visible = (i == 2);
            this.gvProTarget.Columns[17].Visible = (i == 2);
            this.gvProTarget.Columns[18].Visible = (i == 2);
            this.gvProTarget.Columns[19].Visible = (i == 2);

            this.gvProTarget.Columns[20].Visible = (i == 3);
            this.gvProTarget.Columns[21].Visible = (i == 3);
            this.gvProTarget.Columns[22].Visible = (i == 3);
            this.gvProTarget.Columns[23].Visible = (i == 3);
            this.gvProTarget.Columns[24].Visible = (i == 3);
            this.gvProTarget.Columns[25].Visible = (i == 3);
            this.gvProTarget.Columns[26].Visible = (i == 3);

            this.gvProTarget.Columns[27].Visible = (i == 4);
            this.gvProTarget.Columns[28].Visible = (i == 4);
            this.gvProTarget.Columns[29].Visible = (i == 4);
            this.gvProTarget.Columns[30].Visible = (i == 4);
            this.gvProTarget.Columns[31].Visible = (i == 4);
            this.gvProTarget.Columns[32].Visible = (i == 4);
            this.gvProTarget.Columns[33].Visible = (i == 4);

            this.gvProTarget.Columns[34].Visible = (i == 5);
            this.gvProTarget.Columns[35].Visible = (i == 5);
            this.gvProTarget.Columns[36].Visible = (i == 5);
            this.gvProTarget.Columns[37].Visible = (i == 5);
            this.gvProTarget.Columns[38].Visible = (i == 5);
            this.gvProTarget.Columns[39].Visible = (i == 5);
            this.gvProTarget.Columns[40].Visible = (i == 5);

            this.gvProTarget.Columns[41].Visible = (i == 6);
            this.gvProTarget.Columns[42].Visible = (i == 6);
            this.gvProTarget.Columns[43].Visible = (i == 6);
            this.gvProTarget.Columns[44].Visible = (i == 6);
            this.gvProTarget.Columns[45].Visible = (i == 6);
            this.gvProTarget.Columns[46].Visible = (i == 6);
            this.gvProTarget.Columns[47].Visible = (i == 6);

            this.gvProTarget.Columns[48].Visible = (i == 7);
            this.gvProTarget.Columns[49].Visible = (i == 7);
            this.gvProTarget.Columns[50].Visible = (i == 7);
            this.gvProTarget.Columns[51].Visible = (i == 7);
            this.gvProTarget.Columns[52].Visible = (i == 7);
            this.gvProTarget.Columns[53].Visible = (i == 7);
            this.gvProTarget.Columns[54].Visible = (i == 7);

            this.gvProTarget.Columns[55].Visible = (i == 8);
            this.gvProTarget.Columns[56].Visible = (i == 8);
            this.gvProTarget.Columns[57].Visible = (i == 8);
            this.gvProTarget.Columns[58].Visible = (i == 8);
            this.gvProTarget.Columns[59].Visible = (i == 8);
            this.gvProTarget.Columns[60].Visible = (i == 8);
            this.gvProTarget.Columns[61].Visible = (i == 8);

            this.gvProTarget.Columns[62].Visible = (i == 9);
            this.gvProTarget.Columns[63].Visible = (i == 9);
            this.gvProTarget.Columns[64].Visible = (i == 9);
            this.gvProTarget.Columns[65].Visible = (i == 9);
            this.lblColGroup.Text = Convert.ToString(i);
            // this.FooterQty((DataTable)Session["tblptarget"]);

        }

        private void Data_Bind()
        {
            this.gvProTarget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProTarget.DataSource = (DataTable)Session["tblptarget"];
            this.gvProTarget.DataBind();
            this.FooterCal();
        }

        protected void Refresh_Analysis_Session()
        {
            DataTable tbl1 = (DataTable)Session["tblptarget"];
            int rowindex;
            for (int i = 0; i < this.gvProTarget.Rows.Count; i++)
            {

                for (int j = 1; j <= 60; j++)
                {
                    string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    double gvQty2 = Convert.ToDouble("0" + ((Label)this.gvProTarget.Rows[i].FindControl(gvQty1)).Text.Trim());
                    if (this.gvProTarget.Columns[j + 5].Visible)
                    {
                        rowindex = (this.gvProTarget.PageSize * this.gvProTarget.PageIndex) + i;
                        tbl1.Rows[rowindex]["ym" + j.ToString()] = gvQty2;
                    }
                }

            }
            Session["tblptarget"] = tbl1;
        }

        //private void FooterQty(DataTable dt) 
        //{

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFBgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ? 0.00 
        //        : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFAloqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totrqty)", "")) ? 0.00
        //        : dt.Compute("Sum(totrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFDifqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(difqty)", "")) ? 0.00
        //        : dt.Compute("Sum(difqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym1qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
        //        : dt.Compute("Sum(ym1)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym2qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
        //        : dt.Compute("Sum(ym2)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym3qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
        //        : dt.Compute("Sum(ym3)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym4qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
        //        : dt.Compute("Sum(ym4)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym5qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
        //        : dt.Compute("Sum(ym5)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym6qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
        //        : dt.Compute("Sum(ym6)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym7qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
        //        : dt.Compute("Sum(ym7)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym8qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
        //        : dt.Compute("Sum(ym8)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym9qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
        //        : dt.Compute("Sum(ym9)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym10qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
        //        : dt.Compute("Sum(ym10)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym11qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
        //       : dt.Compute("Sum(ym11)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym12qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
        //        : dt.Compute("Sum(ym12)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym13qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
        //        : dt.Compute("Sum(ym13)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym14qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
        //        : dt.Compute("Sum(ym14)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym15qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
        //        : dt.Compute("Sum(ym15)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym16qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
        //        : dt.Compute("Sum(ym16)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym17qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
        //        : dt.Compute("Sum(ym17)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym18qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
        //        : dt.Compute("Sum(ym18)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym19qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
        //        : dt.Compute("Sum(ym19)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym20qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
        //        : dt.Compute("Sum(ym20)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym21qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
        //       : dt.Compute("Sum(ym21)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym22qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
        //        : dt.Compute("Sum(ym22)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym23qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
        //        : dt.Compute("Sum(ym23)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym24qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
        //        : dt.Compute("Sum(ym24)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym25qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
        //        : dt.Compute("Sum(ym25)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym26qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
        //        : dt.Compute("Sum(ym26)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym27qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
        //        : dt.Compute("Sum(ym27)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym28qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
        //        : dt.Compute("Sum(ym28)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym29qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
        //        : dt.Compute("Sum(ym29)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym30qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
        //        : dt.Compute("Sum(ym30)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym31qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
        //       : dt.Compute("Sum(ym31)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym32qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
        //        : dt.Compute("Sum(ym32)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym33qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
        //        : dt.Compute("Sum(ym33)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym34qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
        //        : dt.Compute("Sum(ym34)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym35qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
        //        : dt.Compute("Sum(ym35)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym36qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
        //        : dt.Compute("Sum(ym36)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym37qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
        //        : dt.Compute("Sum(ym37)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym38qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
        //        : dt.Compute("Sum(ym38)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym39qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
        //        : dt.Compute("Sum(ym39)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym40qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
        //        : dt.Compute("Sum(ym40)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym41qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
        //       : dt.Compute("Sum(ym41)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym42qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
        //        : dt.Compute("Sum(ym42)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym43qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
        //        : dt.Compute("Sum(ym43)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym44qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
        //        : dt.Compute("Sum(ym44)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym45qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
        //        : dt.Compute("Sum(ym45)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym46qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
        //        : dt.Compute("Sum(ym46)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym47qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
        //        : dt.Compute("Sum(ym47)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym48qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
        //        : dt.Compute("Sum(ym48)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym49qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
        //        : dt.Compute("Sum(ym49)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym50qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
        //        : dt.Compute("Sum(ym50)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym51qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
        //       : dt.Compute("Sum(ym51)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym52qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
        //        : dt.Compute("Sum(ym52)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym53qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
        //        : dt.Compute("Sum(ym53)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym54qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
        //        : dt.Compute("Sum(ym54)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym55qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
        //        : dt.Compute("Sum(ym55)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym56qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
        //        : dt.Compute("Sum(ym56)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym57qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
        //        : dt.Compute("Sum(ym57)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym58qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
        //        : dt.Compute("Sum(ym58)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym59qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
        //        : dt.Compute("Sum(ym59)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym60qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
        //        : dt.Compute("Sum(ym60)", ""))).ToString("#,##0.00;(#,##0.00); ");

        //}
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            //this.Refresh_Analysis_Session();
            //DataTable tbl1 = (DataTable)Session["tblptarget"];

            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    double totrqty = 0;
            //    for (int j = 1; j <= 60; j++)
            //    {

            //        totrqty =totrqty+ Convert.ToDouble(tbl1.Rows[i]["ym"+j.ToString()]);
            //    }

            //    double bgdqty = Convert.ToDouble(tbl1.Rows[i]["bgdqty"]); 
            //    tbl1.Rows[i]["totrqty"] = totrqty;
            //    tbl1.Rows[i]["difqty"] = bgdqty - totrqty;
            //}
            //Session["tblptarget"] = tbl1;
            //this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetCompCode();
            //string pactcode = this.ddlProject.SelectedValue.ToString();
            //this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
            //DataTable tbl1 = (DataTable)Session["tblptarget"];
            //DataTable tblym = ((DataTable)Session["tblpymon"]);
            //int count = tblym.Rows.Count;
            //bool result = false;

            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    string Itemcode = tbl1.Rows[i]["isircode"].ToString();
            //    string flrcod = tbl1.Rows[i]["flrcod"].ToString();
            //    for (int j = 0; j < count; j++)
            //    {

            //        string yearmon = tblym.Rows[j]["yearmon"].ToString();

            //        double trqty = Convert.ToDouble(tbl1.Rows[i]["ym" + (j + 1).ToString()]);
            //        if (trqty > 0)
            //            result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INSERTORUPPTARGET", pactcode,
            //                   Itemcode, flrcod, yearmon, trqty.ToString(), "", "", "", "", "", "", "", "", "", "");
            //        if (!result)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);

            //        }
            //    }
            //}

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "PROJECT COMPLETION INFORMATION";
            //    string eventdesc = "Update";
            //    string eventdesc2 = this.ddlProject.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }
        protected void gvProTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Refresh_Analysis_Session();
            this.gvProTarget.PageIndex = e.NewPageIndex;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Data_Bind();
            this.Refresh_Analysis_Session();
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));

        }
    }
}