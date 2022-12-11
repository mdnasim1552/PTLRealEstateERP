
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_17_Acc
{
    public partial class RptLetterOfAlotment : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Sales Reports";
                this.GetProjectName();
                this.GetCustomerName();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            if (this.Request.QueryString["Type"].ToString().Trim() == "DueCollAll")
            {
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "pactcode not like '000000000000%'";
                dt = dv1.ToTable();

            }



            this.ddlprjname.DataTextField = "pactdesc";
            this.ddlprjname.DataValueField = "pactcode";
            this.ddlprjname.DataSource = dt;
            this.ddlprjname.DataBind();
            this.ddlprjname_SelectedIndexChanged(null, null);



        }

        private void GetCustomerName()
        {
            string custotype = this.Request.QueryString["Type"].ToString();
            //string calltype = custotype=="LO"? "GETCUSTOMERNAMELANDOWNER" : "GETCUSTOMERNAME";          
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjname.SelectedValue.ToString()==""?"51%": this.ddlprjname.SelectedValue.ToString();
            string txtSProject = "%%";
            string islandowner = this.Request.QueryString["Type"] == "Allotment" ? "0" : "1";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, islandowner, "", "", "", "", "", "");
            this.ddlcustomerName.DataTextField = "custnam";
            this.ddlcustomerName.DataValueField = "custid";
            this.ddlcustomerName.DataSource = ds2.Tables[0];
            this.ddlcustomerName.DataBind();

        }
        

        protected void ddlprjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3370":
                case "3101":
                    this.LetterofAllotmentCPDL();

                    break;
                default:
                    break;
            }

        }

        private void LetterofAllotmentCPDL()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comsnam = hst["comsnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string comfadd = hst["comadd"].ToString().Replace("<br />", "\n");
                string session = hst["session"].ToString();
                string username = hst["username"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string prjname = this.ddlprjname.SelectedValue.ToString();
                string custname = this.ddlcustomerName.SelectedValue.ToString();
                string heading = "CPDL is pleased to other the allotment of Apartment space in your favor" +
                    "\n only subject to the following Terms and conditionslimited thereto, since variation may take place in case of necessity, for strict adherence by the applicant / allottee";
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERDETAILS", prjname, custname, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                string dateofbirth = Convert.ToDateTime(ds2.Tables[0].Rows[0]["dateofbirth"].ToString()).ToString("dd-MMM-yyyy");
                string custid = " ";
                LocalReport Rpt1 = new LocalReport();
                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.AllotmentInfo>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptLetterOfAllotmentCPDL", lst, null, null);
                Rpt1.EnableExternalImages = true;

                Rpt1.SetParameters(new ReportParameter("RptTitle", "LETTER OF ALLOTMENT"));                
                Rpt1.SetParameters(new ReportParameter("dateofbirth", dateofbirth));                
                Rpt1.SetParameters(new ReportParameter("custid", custid));                
                Rpt1.SetParameters(new ReportParameter("heading", heading));                
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string prjname = this.ddlprjname.SelectedValue.ToString();
                string custname = this.ddlcustomerName.SelectedValue.ToString();
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERDETAILS", prjname, custname, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                Session["tblcudtomerdetails"] = ds2.Tables[0];

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}