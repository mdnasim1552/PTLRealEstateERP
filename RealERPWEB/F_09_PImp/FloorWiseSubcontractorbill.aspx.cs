using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using RealERPRPT;

namespace RealERPWEB.F_09_PImp
{
    public partial class FloorWiseSubcontractorbill : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
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

              

                this.GetProjectName();
                this.GetCataGory();


            }

            }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RptPrjFloorWiseBill();
            //string Type = this.Request.QueryString["Type"].ToString().Trim();
            //switch (Type)
            //{
            //    case "FloorWiseSubcontractorbill":

            //        this.RptPrjFloorWiseBill();
            //        break; 

            //}
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {
           
            string comcod = this.GetCompCode();
           


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETProjectsub", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "pactdesc";
            this.ddlprjlist.DataValueField = "pactcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            this.GetSubContractor();
        }

        private void GetSubContractor()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();

           
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME01", pactcode, "%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlcontractorlist.DataTextField = "csirdesc";
            this.ddlcontractorlist.DataValueField = "csircode";
            this.ddlcontractorlist.DataSource = ds1.Tables[0];
            this.ddlcontractorlist.DataBind();
       


        }
       
        private void GetCataGory()
        {
            string comcod = this.GetCompCode();
           

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCATAGORY", "", "","" ,"", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlcatagory.DataValueField = "rsircode";
            this.ddlcatagory.DataTextField = "rsirdesc";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
           
        }

        protected void ddlprjlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSubContractor();
        }

        protected void lbtnok_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string csircode=  this.ddlcontractorlist.SelectedValue.ToString() == "000000000000" ? "98%" : this.ddlcontractorlist.SelectedValue.ToString() + "%";
            string billtcode=  this.ddlcatagory.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlcatagory.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCONSTATEMENT", pactcode, csircode, billtcode, "", "", "", "", "", "");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCONSTATEMENT", pactcode, csircode, billtcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
           
                return;
            }
            Session["tblflrwisbillForReport"] = ds2.Tables[0];
            Session["tblflrwisbill"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
       
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string csircode= dt1.Rows[0]["csircode"].ToString();
            string rsircode = "";
            if (dt1.Rows[0]["billtcode"] != null)
            {
                rsircode = dt1.Rows[0]["billtcode"].ToString();
            }


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    //if (dt1.Rows[j]["csircode"].ToString() == csircode)
                    //{
                    //    dt1.Rows[j]["actdesc"] = "";
                    //}                        
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

                if (dt1.Rows[j]["csircode"].ToString() == csircode)
                {
                    csircode = dt1.Rows[j]["csircode"].ToString();
                    dt1.Rows[j]["conname"] = "";
                }

                else
                {
                    csircode = dt1.Rows[j]["csircode"].ToString();
                }
                if (dt1.Rows[j]["billtcode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["billtcode"].ToString();
                    dt1.Rows[j]["category"] = "";
                }

                else
                {
                    rsircode = dt1.Rows[j]["billtcode"].ToString();
                }
            }
            return dt1;

        }
        private DataTable HiddenSameDataReport(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string csircode = dt1.Rows[0]["csircode"].ToString();



            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                   
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

                
            }
            return dt1;
        }

        protected void gvflrwisbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvflrwisbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblflrwisbill"];
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvflrwisbill.DataSource = dt;
            this.gvflrwisbill.DataBind();
            

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvflrwisbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        private void RptPrjFloorWiseBill()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblflrwisbillForReport"];//Session["tblflrwisbill"];
            if (dt1==null || dt1.Rows.Count == 0)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_09_PIMP.SubConBill.RptPrjFloorWiseBill>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptPrjFloorWiseBill", lst, null, null);
            Rpt1.EnableExternalImages = true;

            string selectedProjectText = ddlprjlist.SelectedItem.Text;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("selectedProjectText", selectedProjectText));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
    }
}