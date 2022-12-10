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
            string txtSProject = "16%";


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
           // this.ddlProjectName_SelectedIndexChanged(null, null);



        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}