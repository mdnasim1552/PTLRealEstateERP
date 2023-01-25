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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_62_Mis
{
    public partial class RptClientDis : System.Web.UI.Page
    {
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Discussion History";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private string GetHRcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["hcomcod"].ToString());
        }



        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowEmpData();
        }

        private void ShowEmpData()
        {
            string comcod = this.Getcomcod();

            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds = KpiData.GetTransInfo(comcod, "SP_REPORT_CLIENT_DISCUSSION", "SHOWMKTTEAMSTSTUS", frmdate, todate);
            Session["tbClientDis"] = HiddenSameData(ds.Tables[0]);
            ViewState["tbGppDes"] = ds.Tables[1];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dtpname = (DataTable)ViewState["tbGppDes"];
            //int j = 4;
            //for (int i = 0; i < dtpname.Rows.Count; i++)
            //{

            //    this.gvClientDis.Columns[j].HeaderText = dtpname.Rows[i]["sirdesc"].ToString();
            //    j++;
            //    if (j == 24)
            //        break;
            //}
            DataTable dt = (DataTable)Session["tbClientDis"];
            this.gvClientDis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvClientDis.DataSource = dt;
            this.gvClientDis.DataBind();



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        protected void lnkprint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.RptAllOfficerPerformance();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comnam;


            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfrmdate.Text + " To " + this.txttodate.Text + " )";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource((DataTable)Session["tbClientDis"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                string teamcode = dt1.Rows[0]["teamcode"].ToString();
                string proscod = dt1.Rows[0]["proscod"].ToString();
                for (int j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["teamcode"].ToString() == teamcode)
                    {
                        teamcode = dt1.Rows[j]["teamcode"].ToString();
                        dt1.Rows[j]["teamdesc"] = "";


                        if (dt1.Rows[j]["proscod"].ToString() == proscod)
                        {
                            proscod = dt1.Rows[j]["proscod"].ToString();
                            dt1.Rows[j]["prosdesc"] = "";

                        }
                        else
                        {
                            proscod = dt1.Rows[j]["proscod"].ToString();
                        }


                    }

                    else
                    {
                        teamcode = dt1.Rows[j]["teamcode"].ToString();
                        proscod = dt1.Rows[j]["proscod"].ToString();
                    }

                }
            }

            return dt1;

        }

        protected void gvClientDis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvClientDis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}
