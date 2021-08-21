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
using System.Drawing;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class LandInterface_Land : System.Web.UI.Page
    {
        ProcessAccess LandData = new ProcessAccess();

        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtdate.Text = "01" + date.Substring(2);
                this.txtdate.Text = firstDay.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Feasibility Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                GetPejList();
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            return (hst["comcod"].ToString());
        }
        private void GetPejList()
        {

            string comcod = this.GetCompCode();
            string projdesc = "%%";
            DataSet ds1 = LandData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETPROJECTNAME", projdesc, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gvPrjInfo_RowDataBound(null, null);
            this.GetBudgetData();
            //this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlinitial.Visible = true;
                    this.pnlcheck.Visible = false;
                    this.pnlLandinfo.Visible = false;

                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;

                    this.pnlapp.Visible = false;


                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                case "1":
                    this.pnlinitial.Visible = false;
                    this.pnlLandinfo.Visible = true;
                    this.pnlcheck.Visible = false;

                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;


                    this.pnlapp.Visible = false;


                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = true;
                    this.pnlLandinfo.Visible = false;

                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;


                    this.pnlapp.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "3":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlLandinfo.Visible = false;

                    this.pnldocument.Visible = true;
                    this.pnlmoredoc.Visible = false;


                    this.pnlapp.Visible = false;


                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "4":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlLandinfo.Visible = false;

                    this.pnlmoredoc.Visible = true;
                    this.pnldocument.Visible = false;


                    this.pnlapp.Visible = false;


                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "5":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlLandinfo.Visible = false;
                    this.pnldocument.Visible = false;
                    this.pnlmoredoc.Visible = false;

                    this.pnlapp.Visible = true;

                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[4].Attributes["style"] = "background: #430000; display:block; ";
                    break;


            }


        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetBudgetData();
        }

        private void GetBudgetData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string frmdate = this.txtdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string actcode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GET_LAND_INTERFACE_STEP", actcode, frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["landinfo"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Land Info.</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["checked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Checked</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["document"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Documentation</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["ispending"]) + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Pending </div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["approval"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Complate</div></div></div>";

            Session["tblfeaprjLand"] = ds2.Tables[0];
            DataTable dt = new DataTable();
            DataView dv;
            //Intial
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("prstatus='true'");
            this.Data_Bind("gvFeaPrjLand", dv.ToTable());

            ////land info
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("isinfo='false'");
            this.Data_Bind("gvLandInfo", dv.ToTable());

            ////Checked
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("ischeked='False' and isinfo='True'");
            this.Data_Bind("gvCheck", dv.ToTable());


            ////Forward



            ////Documentation
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("ischeked='True' and isinfo='True' and  isdoc='False' ");
            this.Data_Bind("gvDocument", dv.ToTable());

            //////More Documentation
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("ischeked='True' and isinfo='True' and  isdoc='True'  and  ispending='False'");

            this.Data_Bind("gvPending", dv.ToTable());



            //pnlApproval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("ischeked='True' and isinfo='True' and  isdoc='True'  and  ispending='True' and  ispending='True' and landstatus='0100000'");
            this.Data_Bind("gvApprov", dv.ToTable());

        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvFeaPrjLand":
                    this.gvFeaPrjLand.DataSource = dt;
                    this.gvFeaPrjLand.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCheck":
                    this.gvCheck.DataSource = dt;
                    this.gvCheck.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvLandInfo":
                    this.gvLandInfo.DataSource = dt;
                    this.gvLandInfo.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvDocument":
                    this.gvDocument.DataSource = dt;
                    this.gvDocument.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvPending":
                    getLandStaqus();
                    this.gvPending.DataSource = dt;
                    this.gvPending.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;



                case "gvApprov":
                    this.gvApprov.DataSource = dt;
                    this.gvApprov.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

            }

            //DataTable dt = (DataTable)Session["tblfeaprjLand"];



        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("chkid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkdat", Type.GetType("System.String"));
            tblt01.Columns.Add("chktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkseson", Type.GetType("System.String"));
            tblt01.Columns.Add("feasid", Type.GetType("System.String"));
            tblt01.Columns.Add("feasdat", Type.GetType("System.String"));
            tblt01.Columns.Add("feastrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("feasseson", Type.GetType("System.String"));
            tblt01.Columns.Add("docid", Type.GetType("System.String"));
            tblt01.Columns.Add("docdat", Type.GetType("System.String"));
            tblt01.Columns.Add("doctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("docseson", Type.GetType("System.String"));
            tblt01.Columns.Add("bakid", Type.GetType("System.String"));
            tblt01.Columns.Add("bakdat", Type.GetType("System.String"));
            tblt01.Columns.Add("baktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("bakseson", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocid", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocdat", Type.GetType("System.String"));
            tblt01.Columns.Add("mordoctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("mordocseson", Type.GetType("System.String"));
            tblt01.Columns.Add("legalid", Type.GetType("System.String"));
            tblt01.Columns.Add("legaldat", Type.GetType("System.String"));
            tblt01.Columns.Add("legaltrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("legalseson", Type.GetType("System.String"));
            tblt01.Columns.Add("forwid", Type.GetType("System.String"));
            tblt01.Columns.Add("forwdat", Type.GetType("System.String"));
            tblt01.Columns.Add("forwtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("forwseson", Type.GetType("System.String"));
            tblt01.Columns.Add("apprid", Type.GetType("System.String"));
            tblt01.Columns.Add("apprdat", Type.GetType("System.String"));
            tblt01.Columns.Add("apprtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("apprseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }


        protected void gvFeaPrjLand_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvproname");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "prstatus")).ToString();


                Label track = (Label)e.Row.FindControl("lgvtrack");

                //for (int i = 0; i < gvFeaPrjLand.Rows.Count; i++)
                //{
                //if (status == "True" && chkid.Trim() =="")
                //{

                //   // track.Attributes["class"] = "color:green;";
                //    track.Attributes.CssStyle.Add("color", "Green");
                //}
                //else if (chkid != "" && feasid.Trim() == "")
                //{
                //    track.Attributes.CssStyle.Add("color", "Maroon");
                //}
                //else if (feasid != "" && docid.Trim() == "")
                //{
                //    track.Attributes.CssStyle.Add("color", "blue");
                //}

                //else if (bakid != "" && legalid.Trim() == "")
                //{
                //    track.Attributes.CssStyle.Add("color", "violet");
                //}
                //else if (legalid != "" && forwid.Trim() == "")
                //{
                //    track.Attributes.CbnSaveLandCode_ClickssStyle.Add("color", "magenta");
                //}
                //else if (forwid != "" && apprid.Trim() == "")
                //{
                //    track.Attributes.CssStyle.Add("color", "gray");
                //}
                //else if (apprid != "")
                //{
                //    track.Attributes.CssStyle.Add("color", "brown");
                //}
                //else if ((docid != "" && legalid.Trim() == "" && bakid.Trim() == "") || (mordocid != "" && legalid.Trim() == ""))
                //{
                //    track.Attributes.CssStyle.Add("color", "YellowGreen");
                //}
                //}


            }


        }
        protected void gvCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string landcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "landcode")).ToString();

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("hyyLnikGvCheck");
                hlnkprj.NavigateUrl = "~/F_99_Allinterface/AddLandInfo?landcode=" + landcode;


            }
        }
        protected void gvFeasibility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamef");
            //    //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
            //    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
            //    //hlink2.NavigateUrl = "~/F_02_Fea/ProjectFeasibility";
            //    hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=FeaEntry&prjcode=" + pactcode;


            //}

        }
        protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvpronamed");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnPreEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //hlink2.NavigateUrl = "~/F_02_Fea/ProjectFeasibility";
                hlnkprj.NavigateUrl = "~/F_02_Fea/ProjectFeasibility?Type=doc&prjcode=" + pactcode;


            }

        }





        /// <summary>
        /// add New land 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LnkAddDetails_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlProjectName.SelectedValue.ToString();
            string pSircod = "14%";
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GET_LAND_PROJECT_DETAILS", prjcode, pSircod, "", "", "", "", "", "", "");



            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);



        }
        protected void lnkAddLand_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.LandmodalTitel.InnerHtml = this.ddlProjectName.SelectedItem.Text.ToString();
            this.hiddenPrjCode.Value = this.ddlProjectName.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalLand();", true);

        }
        protected void bnSaveLandCode_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            string landname = this.txtLandName.Text.Trim();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            if (pactcode == "%%")
            {
                string msg = "select project";

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            string landcode = this.hidlandcode.Value;
            string txunit = this.txtunit.Text.Trim();

            string rate = this.txtstdrate.Text == "" ? "0.00" : this.txtstdrate.Text.Trim();
            string qnty = this.txtqnty.Text == "" ? "0.00" : this.txtqnty.Text.Trim();

            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            if (landname.Length > 0)
            {
                bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTLAND", pactcode, landname, landcode, userid, cdate, txunit, rate, qnty, "", "");
                if (!result)
                {
                    string msg = "Fail";
                    this.txtLandName.Text = "";
                    this.hidlandcode.Value = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }
                else
                {
                    string msg = "Updated";

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    this.txtLandName.Text = "";
                    this.hidlandcode.Value = "";
                    RadioButtonList1_SelectedIndexChanged(null, null);
                    RadioButtonList1.SelectedIndex = 0;
                    this.GetBudgetData();
                }
            }
            else
            {
                string msg = "Fail";

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
            }

        }
        protected void lknEdit_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string pactcode = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lbllandcode")).Text.Trim();
            string lblpactdesc = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lblpactdesc")).Text.Trim();
            string lgbunit = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lgbunit")).Text.Trim();
            string lgRate = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lgRate")).Text.Trim();
            string lgQty = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lgQty")).Text.Trim();




            this.hidlandcode.Value = pactcode;
            this.txtLandName.Text = lblpactdesc;
            this.txtqnty.Text = lgQty;
            this.txtunit.Text = lgbunit;
            this.txtstdrate.Text = lgRate;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalLand();", true);

        }
        protected void lnkAddInfo_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvLandInfo.Rows[Rowindex].FindControl("lbllandcode")).Text.Trim();
            string lblpactdesc = ((Label)this.gvLandInfo.Rows[Rowindex].FindControl("lblpactdesc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalLandInfo();", true);
        }




        protected void lnkInofApproved_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvLandInfo.Rows[Rowindex].FindControl("lbllandcode")).Text.Trim();
            string lblpactdesc = ((Label)this.gvLandInfo.Rows[Rowindex].FindControl("lblpactdesc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "True";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "isinfo", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }
            else
            {

                string msg = "Information fulfil got To Checked Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }

        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();
            string lblpactdesc = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lblpactdescDoc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalLandInfo();", true);
        }

        protected void lnkViewGvCheck_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lbllandcodegvCheck")).Text.Trim();
            string lblpactdesc = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactdescgvCheck")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;



        }
        protected void lnkForwardIsCheked_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lbllandcodegvCheck")).Text.Trim();
            string lblpactdesc = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactdescgvCheck")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "False";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "isinfo", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 2;
            }
            else
            {

                string msg = "Not yeat Information fulfil, got To Back Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 2;
            }
        }
        protected void lnkForward_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lbllandcode")).Text.Trim();
            string lblpactdesc = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactdesc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "False";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "isinfo", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 2;
            }
            else
            {

                string msg = "Not yeat Information fulfil, got To Back Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 2;
            }
        }





        protected void lnkIsCheked_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lbllandcodegvCheck")).Text.Trim();
            string lblpactdesc = ((Label)this.gvCheck.Rows[Rowindex].FindControl("lblpactdescgvCheck")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isCheked = "True";

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isCheked, "isCheked", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }
            else
            {

                string msg = "Information Checked got To Documentation Step";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }
        }
        protected void lnkDocCheked_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();
            string lblpactdesc = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lblpactdescDoc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isCheked = "True";

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isCheked, "isdoc", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }
            else
            {

                string msg = "Information Checked got To Documentation Step";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 1;
            }
        }


        protected void lnkForwardDoc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();
            string lblpactdesc = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lblpactdescDoc")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "False";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "isCheked", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 3;
            }
            else
            {

                string msg = "Not yeat Information fulfil, got To Back Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 3;
            }
        }

        protected void lnkForwardgvPending_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            string lblpactdesc = ((Label)this.gvPending.Rows[Rowindex].FindControl("lblpactdescgvPending")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "False";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "isdoc", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Process Fail";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
            else
            {

                string msg = "Is pending Process, got To Back Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
        }

        protected void lnkForwardgvApprov_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            string lblpactdesc = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovPact")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isinfo = "False";
            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isinfo, "ispending", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Process Fail";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 5;
            }
            else
            {

                string msg = "Is pending Process, got To Back Step";


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 5;
            }

        }




        protected void lnkgvPendingChekced_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            string lblpactdesc = ((Label)this.gvPending.Rows[Rowindex].FindControl("lblpactdescgvPending")).Text.Trim();
            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            string isCheked = "True";

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, isCheked, "ispending", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Not yeat Information fulfil";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
            else
            {

                string msg = "Information Checked got To Documentation Step";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
        }

        private void getLandStaqus()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = this.feaData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SHOWGENCODEDETAILS", "01",
                            "7", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["storedata"] = ds1.Tables[0];



        }
        protected void gvPending_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DataTable tbl1 = (DataTable)ViewState["storedata"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string landstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "landstatus")).ToString().Trim();

                DropDownList ddland = (DropDownList)e.Row.FindControl("ddlLandStatus");
                if (landstatus != "0")
                {
                    ddland.SelectedValue = landstatus;

                }

                ddland.DataTextField = "gdesc";
                ddland.DataValueField = "gcode";
                ddland.DataSource = tbl1;
                ddland.DataBind();

                string landcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "landcode")).ToString();

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("hypGvpenView");
                hlnkprj.NavigateUrl = "~/F_99_Allinterface/AddLandInfo?landcode=" + landcode;


            }
        }

        protected void lnkSavePend_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            string lblpactdesc = ((Label)this.gvPending.Rows[Rowindex].FindControl("lblpactdescgvPending")).Text.Trim();
            string ddland = ((DropDownList)this.gvPending.Rows[Rowindex].FindControl("ddlLandStatus")).SelectedValue.ToString();

            this.hiddenLandCode.Value = landcode;
            this.txtLandName.Text = lblpactdesc;
            // string isCheked = "True";

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTINFOCHECKED", landcode, ddland, "landstatus", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Land Status Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
            else
            {

                string msg = "Land Status Successd";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 4;
            }
        }




        protected void lnkDalilNo_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbldlgcods")).Text.Trim();
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);


        }
        protected void lnkcsknoNo_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvPending.Rows[Rowindex].FindControl("cskgcod2")).Text.Trim();
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);

        }
        protected void lnkrskno_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvPending.Rows[Rowindex].FindControl("rsknogcod2")).Text.Trim();
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lblpactdescgvPending")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);

        }


        protected void lnkrskno3_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvApprov.Rows[Rowindex].FindControl("rsknogcod3")).Text.Trim();
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);

        }

        protected void lnkBSkno_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvPending.Rows[Rowindex].FindControl("bsknogcod2")).Text.Trim();
            string landcode = ((Label)this.gvPending.Rows[Rowindex].FindControl("lbllandcodegvPending")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);

        }
        protected void lnkBSkno3_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvApprov.Rows[Rowindex].FindControl("bsknogcod3")).Text.Trim();
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);

        }

        protected void lnkDalilNoapp3_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvApprov.Rows[Rowindex].FindControl("dlgcod3")).Text.Trim();
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);
        }
        protected void lnkcsknoNoapp_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvApprov.Rows[Rowindex].FindControl("cskgcod3")).Text.Trim();
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);
        }
        protected void gvLandInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string landcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "landcode")).ToString();

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lnkAddInfo");
                hlnkprj.NavigateUrl = "~/F_99_Allinterface/AddLandInfo?landcode=" + landcode;

            }
        }
        protected void gvDocument_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string landcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "landcode")).ToString();

                HyperLink hlnkprj = (HyperLink)e.Row.FindControl("hyyLnikGvDocs");
                hlnkprj.NavigateUrl = "~/F_99_Allinterface/AddLandInfo?landcode=" + landcode;

            }
        }
        protected void lnkDalilNo1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvDocument.Rows[Rowindex].FindControl("dlgcod1")).Text.Trim();
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();
            GetModal(comcod, dlgcod, landcode);



        }
        protected void lnkcsknoNo1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvDocument.Rows[Rowindex].FindControl("cskgcod")).Text.Trim();
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();

            GetModal(comcod, dlgcod, landcode);
        }
        protected void lnkrsknoNo1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvDocument.Rows[Rowindex].FindControl("rsknogcod")).Text.Trim();
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();


            GetModal(comcod, dlgcod, landcode);
        }
        private void GetModal(string comcod, string dlgcod, string landcode)
        {
            DataSet ds1 = LandData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GETRESOURCEIMAGES", landcode, dlgcod, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.imgData.ImageUrl = ds1.Tables[0].Rows[0]["imageurl"] == "" ? "~/images/noimageavl.png" : ds1.Tables[0].Rows[0]["imageurl"].ToString();
            }
            else
            {
                this.imgData.ImageUrl = "~/images/noimageavl.png";

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalImages();", true);
        }
        protected void bsknoq_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string dlgcod = ((Label)this.gvDocument.Rows[Rowindex].FindControl("bsknogcod")).Text.Trim();
            string landcode = ((Label)this.gvDocument.Rows[Rowindex].FindControl("lbllandcodeDoc")).Text.Trim();


            GetModal(comcod, dlgcod, landcode);
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void LnikAddland_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string actcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblactcode")).Text.Trim();
            string landdesc = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovPact")).Text.Trim();
            string landcode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblgvApprovlcode")).Text.Trim();
            string lansircode = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblSircode")).Text.Trim();

            string lblunit = ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblunit")).Text.Trim();


            double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblqty")).Text.Trim()));
            double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvApprov.Rows[Rowindex].FindControl("lblrate")).Text.Trim()));

            double dramt = qty * rate;

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "LANDADDLINKED", landcode, landdesc, userid, cdate, lansircode, qty.ToString(), rate.ToString(), lblunit, dramt.ToString(), actcode, "", "", "");
            if (!result)
            {
                string msg = "Land Link Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 5;
            }
            else
            {

                string msg = "Land Linked Successd";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 5;
            }

        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string landcode = ((Label)this.gvFeaPrjLand.Rows[Rowindex].FindControl("lbllandcode")).Text.Trim();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            bool result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "DELETELANDDATA", landcode, "", "", userid, cdate, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Land Delete Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
            }
            else
            {

                string msg = "Land Delete Successd";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
            }

        }

    }
}