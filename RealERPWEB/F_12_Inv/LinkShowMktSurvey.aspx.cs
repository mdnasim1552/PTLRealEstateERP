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
namespace RealERPWEB.F_12_Inv
{

    public partial class LinkShowMktSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ShowSurvey();


            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowSurvey();
        }

        protected void ShowSurvey()
        {


            this.lblMurketSurvey.Visible = true;
            string comcod = this.GetCompCode();
            string msrno = this.Request.QueryString["msrno"];
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", msrno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            this.lblsurveyby.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : "Survey Completed By: " + ds1.Tables[1].Rows[0]["username"].ToString();
            Session["tblMSR"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            this.txtMSRNarr.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["msrnar"].ToString();

        }




        protected void Data_Bind()
        {
            this.gvMSRInfo.DataSource = (DataTable)Session["tblMSR"];
            this.gvMSRInfo.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescod = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                {
                    rescod = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                rescod = dt1.Rows[j]["rsircode"].ToString();
            }
            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

    }
}