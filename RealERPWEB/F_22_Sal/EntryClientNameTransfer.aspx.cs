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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class EntryClientNameTransfer : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static bool result;
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.GetProjectName();
                this.GetUnitName();
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");                       
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));              
               

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

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
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPNAMEMAINTENANCE", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        private void GetUnitName()
        {

            Session.Remove("tblunit");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSUnit = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETUNITNAME", pactcode, txtSUnit, "", "", "", "", "", "", "");
            this.ddlUnitName.DataTextField = "udesc1";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds1.Tables[0];
            this.ddlUnitName.DataBind();
            Session["tblunit"] = ds1.Tables[0];

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
               // this.ddlUnitName_SelectedIndexChanged(null, null);
                this.ddlProjectName.Enabled = false;
                this.ddlUnitName.Enabled = false;
                this.lblUnitName.Visible = true;
                this.LoadGrid();


                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.ddlUnitName.Visible = true;
            
           
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
           
            
            this.ddlProjectName.Enabled = true;
            this.ddlUnitName.Enabled = true;
            this.PnlNarration.Visible = false;
           

        }

       private void LoadGrid()
        {
            ViewState.Remove("tblPersonainfo");
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = this.ddlUnitName.SelectedValue.ToString();         
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            ViewState["tblPersonainfo"] = ds1.Tables[0];

            DataTable dt = ds1.Tables[0];
            string gcod = "01020";
            DataRow[] dr1 = dt.Select("gcod='" + gcod + "'");
            if (dr1.Length > 0)
            {

                if (dr1[0]["gdesc1"].ToString().Length == 0)
                {
                    dr1[0]["gdesc1"] = this.lblcustomerid.Text.Trim();

                }
            }

            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            this.GridTextDDLVisible();

        }


        private void GridTextDDLVisible()
        {

            DataTable dt = ((DataTable)ViewState["tblPersonainfo"]).Copy();
            DataTable dtFile = (DataTable)ViewState["tblFileNo"];
            DropDownList ddlgvFile;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01009": //BirthDay
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        break;
                    case "01010": //MarriageDay
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = "";
                        break;

                    case "01021": //File No
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = true;
                        ddlgvFile = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo"));
                        ddlgvFile.DataTextField = "fileno";
                        ddlgvFile.DataValueField = "id";
                        ddlgvFile.DataSource = dtFile;
                        ddlgvFile.DataBind();
                        ddlgvFile.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        break;

                }
            }

        }

        private bool XmlDataInsert(string pactcode ,string usircode, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXMLCLIENTNAMECHANGE", ds1, null, null, pactcode, usircode, Date);

            if (!resulta)
            {

                return false;
            }


            return true;
        }

        private void logdata()
        {

            string date1 = this.txtCurTransDate.Text;

            //bool data = this.XmlDataInsert(PactCode, Usircode, date1, ds1);

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlUnitName.SelectedValue.ToString();
            string msg = "";

           this.LogStatus(); 


            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();





                string Gvalue = "";

                if (Gcode == "01009")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "01010")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                }
                else if (Gcode == "01021")
                {
                    Gvalue = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).SelectedValue;
                }
                else
                {
                    Gvalue = Gvalue1;
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    msg = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            msg = "Update Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            this.LoadGrid();
            

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Personal Info";
                string eventdesc2 = "Project Name: " + PactCode + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LogStatus()
        {
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = this.ddlUnitName.SelectedValue.ToString();         
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
           // DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SHOWREQINFORMATION", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            bool result = this.XmlDataInsert(PactCode, UsirCode, ds1);
        }









        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }

      
    }
}