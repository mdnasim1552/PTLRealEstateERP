using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_23_CR
{
    public partial class RptMonthlyCollectionMerge : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = date;
                this.txttodate.Text = Convert.ToDateTime("01" + date.Substring(2)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ViewSelection();

                ((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["Type"] == "MonthlyCollMerge" ? "Monthly Collection(Receipt Type Merge)"
                    : "Monthly Collection Schedule(Merge)";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
           
        }

        public string GetCompCode()
        {
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }
        private void ViewSelection()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "MonthlyCollSchMerge":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "MonthlyCollMerge":
                    this.ddllang.Visible = true;
                    this.lblLang.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void ddllang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvmoncollsch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (code == "")
                {
                    return;
                }

                if (code == "BBBBAAAAAAAA" && comcod=="3348")
                {
                    e.Row.Attributes["style"] = "background-color:#6bb55b;";
                }

                else if (code == "BBBBAAAAAAAA" && comcod == "3349")
                {
                    e.Row.Attributes["style"] = "background-color:#c25d6f;";
                }
            }
        }
        protected void gvmoncoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (code == "")
                {
                    return;
                }

                if (code == "AAAAAAAAAAAA" && comcod == "3348")
                {
                    e.Row.Attributes["style"] = "background-color:#6bb55b;";
                }

                else if (code == "AAAAAAAAAAAA" && comcod == "3349")
                {
                    e.Row.Attributes["style"] = "background-color:#c25d6f;";
                }
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch(type)
            {
                case "MonthlyCollMerge":
                    this.ShowMonCollMerge_MainERP();
                    break;

                case "MonthlyCollSchMerge":
                    this.ShowMonCollShchMerge();
                    break;
            }
        }

        private void ShowMonCollMerge_MainERP()
        {
            try 
            {
                string comcod = GetCompCode();
                string fromDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONHLYMRRECTYPEWISE", "", fromDate, toDate, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvmoncoll.DataSource = null;
                    this.gvmoncoll.DataBind();
                    return;
                }

                Session["tblAccRecAc"]  = ds1.Tables[0];
                Session["tblrectypeAC"] = ds1.Tables[1];

                Session["tblAccRecM"]   = ds1.Tables[2];
                Session["tblrectypeM"]  = ds1.Tables[3];

                Session["tblTotalAmt"]  = ds1.Tables[4];

                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }
        }
        private void ShowMonCollShchMerge()
        {

            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONHLYCOLLECTIONDWISE", "", frmdate, todate, "", "", "", "", "", "");

                if (ds1 == null)
                {
                    this.gvmoncollsch.DataSource = null;
                    this.gvmoncollsch.DataBind();
                    return;
                }

                Session["tblAccRecAc"] = this.HiddenSameData(ds1.Tables[0]);
                Session["tblrectypeAC"] = ds1.Tables[1];

                Session["tblAccRecM"] = this.HiddenSameData(ds1.Tables[2]);
                Session["tblrectypeM"] = ds1.Tables[3];

                Session["tblTotalAmt"] = ds1.Tables[4];

                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "MonthlyCollSchMerge":

                    DateTime cdate = Convert.ToDateTime(dt1.Rows[0]["cdate"].ToString());
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(dt1.Rows[j]["cdate"].ToString()) == cdate)
                        {

                            dt1.Rows[j]["cdate1"] = "";
                        }

                        cdate = Convert.ToDateTime(dt1.Rows[j]["cdate"].ToString());
                    }
                    break;
            }
            return dt1;
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt1 = (DataTable)Session["tblAccRecAc"];
                DataTable dt2 = (DataTable)Session["tblrectypeAC"];
                DataTable dt3 = (DataTable)Session["tblAccRecM"];
                DataTable dt4 = (DataTable)Session["tblrectypeM"];
                DataTable dt5 = (DataTable)Session["tblTotalAmt"];

                DataTable dtMerge = dt3.Copy();
                dtMerge.Merge(dt1);
                dtMerge.Merge(dt5);

                string type = this.Request.QueryString["Type"].ToString();
                int i, j;
                switch (type)
                {
                    case "MonthlyCollMerge":

                        //Gridview data binding Main erp
                        for (i = 7; i < this.gvmoncoll.Columns.Count - 1; i++)
                            this.gvmoncoll.Columns[i].Visible = false;
                        j = 7;
                        for (i = 0; i < dt4.Rows.Count; i++)
                        {
                            this.gvmoncoll.Columns[j].Visible = true;
                            this.gvmoncoll.Columns[j].HeaderText = dt4.Rows[i]["recpdesc"].ToString();
                            j++;
                        }

                        this.gvmoncoll.DataSource = dtMerge;
                        this.gvmoncoll.DataBind();

                        Session["Report1"] = gvmoncoll;
                        ((HyperLink)this.gvmoncoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;

                    case "MonthlyCollSchMerge":
                        //Gridview data binding Main erp
                        for (i = 5; i < this.gvmoncollsch.Columns.Count - 1; i++)
                            this.gvmoncollsch.Columns[i].Visible = false;
                        j = 5;
                        for (i = 0; i < dt4.Rows.Count; i++)
                        {
                            this.gvmoncollsch.Columns[j].Visible = true;
                            this.gvmoncollsch.Columns[j].HeaderText = dt4.Rows[i]["pactdesc"].ToString();
                            j++;
                        }

                        this.gvmoncollsch.DataSource = dtMerge;
                        this.gvmoncollsch.DataBind();

                        Session["Report1"] = gvmoncollsch;
                        ((HyperLink)this.gvmoncollsch.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvmoncoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmoncoll.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

       
    }
}