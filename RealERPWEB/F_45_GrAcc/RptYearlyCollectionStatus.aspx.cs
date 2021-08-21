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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class RptYearlyCollectionStatus : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetCompanyList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Yearly Collection Forcasting";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void GetCompanyList()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", "", "", "", "", "", "", "", "", "");
            this.cblCompany.DataTextField = "comsnam";
            this.cblCompany.DataValueField = "comcod";
            this.cblCompany.DataSource = ds1.Tables[0];
            this.cblCompany.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("tblAccRec");
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
                if (mon > 12)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                    return;
                }
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                string comcod = "";
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                    comcod += (this.cblCompany.Items[i].Selected ? this.cblCompany.Items[i].Value.ToString() : "");

                DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_GRPACC_YEARLYBGD", "RPTYCOLLECTFORCASTING", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvyCollection.DataSource = null;
                    this.gvyCollection.DataBind();
                    return;
                }
                Session["tblAccRec"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();

            }

            catch (Exception ex)
            {


            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comcod"].ToString() == comcod)
                    dt1.Rows[j]["companyname"] = "";
                comcod = dt1.Rows[j]["comcod"].ToString();
            }


            return dt1;

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblAccRec"];
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam1)", "")) ? 0.00 : dt.Compute("sum(dueam1)", "")));
            amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam2)", "")) ? 0.00 : dt.Compute("sum(dueam2)", "")));
            amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam3)", "")) ? 0.00 : dt.Compute("sum(dueam3)", "")));
            amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam4)", "")) ? 0.00 : dt.Compute("sum(dueam4)", "")));
            amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam5)", "")) ? 0.00 : dt.Compute("sum(dueam5)", "")));
            amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam6)", "")) ? 0.00 : dt.Compute("sum(dueam6)", "")));
            amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam7)", "")) ? 0.00 : dt.Compute("sum(dueam7)", "")));
            amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam8)", "")) ? 0.00 : dt.Compute("sum(dueam8)", "")));
            amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam9)", "")) ? 0.00 : dt.Compute("sum(dueam9)", "")));
            amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam10)", "")) ? 0.00 : dt.Compute("sum(dueam10)", "")));
            amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam11)", "")) ? 0.00 : dt.Compute("sum(dueam11)", "")));
            amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam12)", "")) ? 0.00 : dt.Compute("sum(dueam12)", "")));


            this.gvyCollection.Columns[6].Visible = (amt1 != 0);
            this.gvyCollection.Columns[7].Visible = (amt2 != 0);
            this.gvyCollection.Columns[8].Visible = (amt3 != 0);
            this.gvyCollection.Columns[9].Visible = (amt4 != 0);
            this.gvyCollection.Columns[10].Visible = (amt5 != 0);
            this.gvyCollection.Columns[11].Visible = (amt6 != 0);
            this.gvyCollection.Columns[12].Visible = (amt7 != 0);
            this.gvyCollection.Columns[13].Visible = (amt8 != 0);
            this.gvyCollection.Columns[14].Visible = (amt9 != 0);
            this.gvyCollection.Columns[15].Visible = (amt10 != 0);
            this.gvyCollection.Columns[16].Visible = (amt11 != 0);
            this.gvyCollection.Columns[17].Visible = (amt12 != 0);

            this.gvyCollection.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 6; i < 18; i++)
            {
                if (frmdate > todate)
                    break;

                this.gvyCollection.Columns[i].HeaderText = frmdate.ToString("MMM yy");
                frmdate = frmdate.AddMonths(1);

            }

            this.gvyCollection.DataSource = dt;
            this.gvyCollection.DataBind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblAccRec"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptYearlyColForcasting();


            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            int j = 1;
            for (int i = 1; i < 14; i++)
            {

                if (frmdate > todate)
                    break;
                string header = frmdate.ToString("MMM yy");
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["amt" + i.ToString()] as TextObject;
                rpttxth.Text = header;
                frmdate = frmdate.AddMonths(1);
                if (j == 13)
                    break;

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvyCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvyCollection.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked)
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblCompany.Items.Count; i++)
                {
                    cblCompany.Items[i].Selected = false;

                }

            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvyCollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lgactdescyc = (Label)e.Row.FindControl("lgactdescyc");
                Label lgvtobgdcost = (Label)e.Row.FindControl("lgvtobgdcost");
                Label lgvtosoldvalue = (Label)e.Row.FindControl("lgvtosoldvalue");
                Label lgvtotreceivedyc = (Label)e.Row.FindControl("lgvtotreceivedyc");
                Label lgvpredueayc = (Label)e.Row.FindControl("lgvpredueayc");
                Label lgvdueam1 = (Label)e.Row.FindControl("lgvdueam1");
                Label lgvdueam2 = (Label)e.Row.FindControl("lgvdueam2");
                Label lgvdueam3 = (Label)e.Row.FindControl("lgvdueam3");
                Label lgvdueam4 = (Label)e.Row.FindControl("lgvdueam4");
                Label lgvdueam5 = (Label)e.Row.FindControl("lgvdueam5");
                Label lgvdueam6 = (Label)e.Row.FindControl("lgvdueam6");
                Label lgvdueam7 = (Label)e.Row.FindControl("lgvdueam7");
                Label lgvdueam8 = (Label)e.Row.FindControl("lgvdueam8");
                Label lgvdueam9 = (Label)e.Row.FindControl("lgvdueam9");
                Label lgvdueam10 = (Label)e.Row.FindControl("lgvdueam10");
                Label lgvdueam11 = (Label)e.Row.FindControl("lgvdueam11");
                Label lgvdueam12 = (Label)e.Row.FindControl("lgvdueam12");
                Label lgvtdueam = (Label)e.Row.FindControl("lgvtdueam");
                Label lgvgtdueam = (Label)e.Row.FindControl("lgvgtdueam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    lgactdescyc.Font.Bold = true;
                    lgvtobgdcost.Font.Bold = true;
                    lgvtosoldvalue.Font.Bold = true;
                    lgvtotreceivedyc.Font.Bold = true;
                    lgvpredueayc.Font.Bold = true;
                    lgvdueam1.Font.Bold = true;
                    lgvdueam2.Font.Bold = true;
                    lgvdueam3.Font.Bold = true;
                    lgvdueam4.Font.Bold = true;
                    lgvdueam5.Font.Bold = true;
                    lgvdueam6.Font.Bold = true;
                    lgvdueam7.Font.Bold = true;
                    lgvdueam8.Font.Bold = true;
                    lgvdueam9.Font.Bold = true;
                    lgvdueam10.Font.Bold = true;
                    lgvdueam11.Font.Bold = true;
                    lgvdueam12.Font.Bold = true;
                    lgvtdueam.Font.Bold = true;
                    lgvgtdueam.Font.Bold = true;
                    lgactdescyc.Style.Add("text-align", "right");


                }

            }


        }
    }
}