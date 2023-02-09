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
namespace RealERPWEB.F_02_Fea
{
    public partial class FeaPrjGenInformation : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT INFORMATION";
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            if (this.ddlPrjName.Items.Count == 0)
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


            //string comcod = this.GetComCode();
            //string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            //DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlProjectName.DataTextField = "infdesc";
            //this.ddlProjectName.DataValueField = "infcod";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();


            Session.Remove("tblpro");
            string comcod = this.GetCompCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrjName.DataTextField = "infdesc";
            this.ddlPrjName.DataValueField = "infcod";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            Session["tblpro"] = ds1.Tables[0];

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlPrjName.SelectedItem.Text;
                this.ddlPrjName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Visible = true;
            this.lblProjectdesc.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvProjectInfo.DataSource = null;
            this.gvProjectInfo.DataBind();
        }

        private void LoadGrid()
        {




            string comcod = this.GetCompCode();
            string fpactcode = this.ddlPrjName.SelectedValue.ToString();

            // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();


            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "PROJECTINFO", pactcode, fpactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }

            this.gvProjectInfo.DataSource = ds1.Tables[0];
            this.gvProjectInfo.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }





        protected void lUpdatProInfo_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fpactcode = this.ddlPrjName.SelectedValue.ToString();
            string pactcode = (((DataTable)Session["tblpro"]).Select("infcod='" + fpactcode + "'"))[0]["actcode"].ToString();
            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string Gunit = "";//((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtResunit")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                feaData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit, fpactcode, "", "", "", "", "", "", "", "", "");

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }







        //this.lblMsg.Visible = true;

        //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
        //if (!Convert.ToBoolean(dr1[0]["entry"]))
        //{
        // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
        //    return;
        //}










        //Hashtable hst = (Hashtable)Session["tblLogin"];
        //string comcod = hst["comcod"].ToString();
        //string pactcode = this.ddlPrjName.SelectedValue.ToString();
        //bool result = false;
        //for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
        //{
        //    string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
        //    string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
        //    string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

        //    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
        //    //if (Gvalue.Length > 0)
        //    //{
        //    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "INSERTORUPFEAPRJINF", pactcode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");
        //    if (!result)
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
        //        return;
        //    }
        //    //}
        //}
        //this.lblMsg.Text = "Updated Successfully";
        // v

    }
}



