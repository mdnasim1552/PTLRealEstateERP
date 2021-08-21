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
namespace RealERPWEB.F_34_Mgt
{
    public partial class UserTranlimitdate : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "BACK DATED TRANSACTION LIMIT";
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetUserName();

            }



        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetUserName()
        {


            string comcod = this.GetCompCode();
            string txtUser = "%%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERNAME", txtUser, "", "", "", "", "", "", "", "");
            this.ddlUserName.DataTextField = "usrname";
            this.ddlUserName.DataValueField = "usrid";
            this.ddlUserName.DataSource = ds3.Tables[0];
            this.ddlUserName.DataBind();

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string userid = this.ddlUserName.SelectedValue.ToString();
            string date = this.txtEntryDate.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERTRANLIMIT", userid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvcomlimit.DataSource = null;
                this.gvcomlimit.DataBind();
                return;

            }

            this.gvcomlimit.DataSource = ds1.Tables[0];
            this.gvcomlimit.DataBind();
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string entryusrid = this.ddlUserName.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string SysDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string date = this.txtEntryDate.Text;

            for (int i = 0; i < this.gvcomlimit.Rows.Count; i++)
            {

                // string bday = Convert.ToDouble("0" + ((TextBox)this.gvcomlimit.Rows[i].FindControl("txtgvlimit")).Text.Trim()).ToString();
                string hour = ((DropDownList)this.gvcomlimit.Rows[i].FindControl("ddlhourPart")).SelectedValue.ToString();
                string min = ((DropDownList)this.gvcomlimit.Rows[i].FindControl("ddlminpart")).SelectedValue.ToString();
                string ampm = ((DropDownList)this.gvcomlimit.Rows[i].FindControl("ddlampmpart")).SelectedValue.ToString();
                string uptime = hour + ":" + min + " " + ampm;
                date = date + " " + uptime;
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSORUPDATEBDATTRNSUINF", entryusrid, date, userid, Terminal, Sessionid, SysDate, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;



                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



        }




        protected void gvcomlimit_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)

            {

                List<string> lst = new List<string>();
                List<string> lstm = new List<string>();
                string si;
                for (int i = 1; i <= 12; i++)
                {
                    si = ASTUtility.Right(("0" + i.ToString()), 2);
                    lst.Add(si);
                }

                for (int i = 0; i <= 59; i++)
                {
                    si = ASTUtility.Right(("0" + i.ToString()), 2);
                    lstm.Add(si);
                }
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlhourPart");
                DropDownList ddlminpart = (DropDownList)e.Row.FindControl("ddlminpart");
                DropDownList ddlampmpart = (DropDownList)e.Row.FindControl("ddlampmpart");

                string uptime = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "uptime")).ToString().Trim();
                int indofcolon = uptime.IndexOf(":");
                string hour = ASTUtility.Left(uptime, 2);
                string min = uptime.Substring(indofcolon + 1, 2);
                string ampmpart = ASTUtility.Right(uptime, 2);


                //
                ddl.DataSource = lst;
                ddl.DataBind();
                ddlminpart.DataSource = lstm;
                ddlminpart.DataBind();



                ddl.SelectedValue = hour;
                ddlminpart.SelectedValue = min;
                ddlampmpart.SelectedValue = ampmpart;



            }

        }
    }
}



