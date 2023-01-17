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
namespace RealERPWEB.F_01_LPA
{
    public partial class PriLandProposal : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        static int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Initial Land Proposal";

                if (Request.QueryString.AllKeys.Contains("prjcode"))
                {
                    if (this.Request.QueryString["prjcode"].Length > 0)
                    {
                        this.lnkbtnSerOk_Click(null, null);
                    }
                }


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
            Session.Remove("tblpro");
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];

            Session["tblpro"] = ds1.Tables[0];
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
                this.GetProjectInfo();

                return;
            }
            this.lbtnOk.Text = "Ok";

            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;



        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            ReportDocument rpcp = new RealERPRPT.R_01_LPA.RptPriLandProposal();//RptFeaProject();
            DataTable dt1 = (DataTable)ViewState["tblfeaprj"];


            DataView dv = new DataView(dt1);
            dv.RowFilter = "prgdesc1<>''"; // query example = "id = 10"

            DataTable dt = dv.ToTable();


            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;
            TextObject txtDu = rpcp.ReportDefinition.ReportObjects["txtDu"] as TextObject;
            txtDu.Text = "Date: " + System.DateTime.Now.ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rpcp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void GetProjectInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fpactcode = this.ddlProjectName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "PRILDPROJECTINFO", pactcode, fpactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }
            this.ViewState["tblfeaprj"] = ds1.Tables[1];
            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();
            this.txtcdate.Text = ds1.Tables[2].Rows.Count > 0 ? Convert.ToDateTime(ds1.Tables[2].Rows[0]["cdate"]).ToString("dd-MMM-yyyy") : System.DateTime.Now.ToString("dd-MMM-yyyy");


        }
        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();






            string fpactcode = this.ddlProjectName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();

            bool result = false;
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue1 = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                DropDownList ddlloc = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc")) as DropDownList;

                string Gvalue = "";

                if (Gcode == "02041" || Gcode == "02045")
                {
                    Gvalue = ddlloc.SelectedValue.ToString();
                }
                else
                {
                    Gvalue = Gvalue1;
                }

                string Gunit = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtResunit")).Text.Trim();

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? (0 + Gvalue).ToString() : Gvalue;

                if (Gcode == "03100")
                    continue;
                result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSORUPFEALDPRIPRJINF", pactcode, Gcode, gtype, Gvalue, fpactcode, Gunit, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSETLDPLOG", pactcode, date, userid, Terminal, Sessionid, Posteddat, "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string cdate = this.txtcdate.Text;
            result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "INSOUPPCDATE", fpactcode, cdate, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Date Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.GetProjectInfo();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Info";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvProjectInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvItmCode = (Label)e.Row.FindControl("lblgvItmCode");
                Label lgcResDesc1 = (Label)e.Row.FindControl("lgcResDesc1");
                Label lgvgval = (Label)e.Row.FindControl("lgvgval");
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");
                DropDownList ddlcataloc = (DropDownList)e.Row.FindControl("ddlcataloc");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgcod")).ToString();
                string val = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgdesc1")).ToString();

                if (code == "02041")
                {
                    txtgvname.Visible = false;
                    ddlcataloc.Visible = true;


                    string comcod = this.GetComCode();
                    DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLOCATION", "", "", "", "", "", "", "", "", "");
                    ddlcataloc.DataTextField = "prgdesc";
                    ddlcataloc.DataValueField = "prgcod";
                    ddlcataloc.DataSource = ds1.Tables[0];
                    ddlcataloc.DataBind();
                    ddlcataloc.SelectedValue = val;

                }
                else if (code == "02045")
                {
                    txtgvname.Visible = false;
                    ddlcataloc.Visible = true;

                    string comcod = this.GetComCode();
                    DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                    ddlcataloc.DataTextField = "prgdesc";
                    ddlcataloc.DataValueField = "prgcod";
                    ddlcataloc.DataSource = ds1.Tables[0];
                    ddlcataloc.DataBind();
                    ddlcataloc.SelectedValue = val;
                }
                else
                {
                    txtgvname.Visible = true;
                    ddlcataloc.Visible = false;

                }
                if (code == "")
                {
                    return;
                }


            }
        }
        protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}