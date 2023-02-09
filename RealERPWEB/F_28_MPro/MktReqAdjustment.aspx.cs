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

namespace RealERPWEB.F_28_MPro
{
    public partial class MktReqAdjustment : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.SelectView();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    this.GetProjectName();
                    this.GetAdjNo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string fxtast = "";
            string Aproval = "";
            string ReFindProject = "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProjectName.DataTextField = "actdesc1";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.ddlProjectName.Enabled = false;
                this.ShowData();
                this.GetAdjNo();

            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.ddlProjectName.Enabled = true;
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
            }

        }

        private void ShowData()
        {
            Session.Remove("tblreqadj");
            string comcod = this.GetComeCode();
            string date = this.txtDate.Text;
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string mrfno = "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "REQUISITION_STATUS", date, pactcode, mrfno, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblreqadj"] = dt1;
            this.LoadGv();

        }

        private void GetAdjNo()
        {
            string comcod = this.GetComeCode();
            string date = this.txtDate.Text;
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_REQADJSTMENT", "GETADJMENTNO", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.lbladjstmentno.Text = ds1.Tables[0].Rows[0]["maxadjno1"].ToString().Substring(0,6);
            this.txtAdjustNo2.Text = ds1.Tables[0].Rows[0]["maxadjno1"].ToString().Substring(6, 5);

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "ReqAdjust":
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        {

                            reqno = dt1.Rows[j]["reqno"].ToString();
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                        }

                        else
                        {

                            reqno = dt1.Rows[j]["reqno"].ToString();

                        }

                    }
                    break;
               
            }
            return dt1;

        }


        private void LoadGv()
        {
            DataTable dt = (DataTable)Session["tblreqadj"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();

                    if(dt.Rows.Count>0)
                    {
                        Session["Report1"] = gvReqStatus;
                        ((HyperLink)this.gvReqStatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";                      

                    }
                    break;
            }

        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblreqadj"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ReqAdjust":
                    for (int i = 0; i < this.gvReqStatus.Rows.Count; i++)
                    {
                        double balqty = Convert.ToDouble("0" + ((Label)this.gvReqStatus.Rows[i].FindControl("lblgvBalqty")).Text.Trim());
                        double adjstqty = Convert.ToDouble("0" + ((TextBox)this.gvReqStatus.Rows[i].FindControl("txtgvadjqty")).Text.Trim());
                        adjstqty = balqty >= adjstqty ? adjstqty : 0.00;
                        //  if (balqty<= adjstqty)
                        int rowindex = (this.gvReqStatus.PageSize) * (this.gvReqStatus.PageIndex) + i;
                        dt.Rows[rowindex]["adjstqty"] = adjstqty;
                    }

                    break;
            }

            Session["tblreqadj"] = dt;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGv();
        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string adjno = this.lbladjstmentno.Text.Trim().Substring(0, 3) + this.txtDate.Text.Trim().Substring(7, 4) + 
                this.lbladjstmentno.Text.Trim().Substring(3, 2) + this.txtAdjustNo2.Text.Trim();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblreqadj"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string reqno = dt.Rows[i]["reqno"].ToString();
                string rsircode = dt.Rows[i]["rsircode"].ToString();
                double adsjtqty = Convert.ToDouble(dt.Rows[i]["adjstqty"]);
                if (adsjtqty != 0)
                {

                    bool result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "INSERT_UPDATE_REQ_ADJ", adjno, reqno,
                             pactcode, rsircode, adsjtqty.ToString(), date, userid, Terminal, Sessionid, "", "", "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + PurData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Requisiton Adjustment Updated successfully" + "');", true);
            ((LinkButton)this.gvReqStatus.FooterRow.FindControl("lbtnFinalUpdate")).Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Req Adj";
                string eventdesc2 = adjno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
        }
      
        protected void imgbtnFindmrfno_Click(object sender, EventArgs e)
        {
            this.ShowData();

        }

        protected void txtgvadjqty_TextChanged(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            double balqty = Convert.ToDouble("0" + ((Label)this.gvReqStatus.Rows[index].FindControl("lblgvBalqty")).Text.Trim());
            double adjqty = Convert.ToDouble("0" + ((TextBox)this.gvReqStatus.Rows[index].FindControl("txtgvadjqty")).Text.Trim());

            if (balqty<adjqty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Adjusted Quantity must be Equal or Less than Balance Quantity" + "');", true);
                return;
            }
        }
    }
}