using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_32_Mis
{
    public partial class GPWiseComparePRJ : System.Web.UI.Page
    {
        ProcessAccess AccessData = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Compare";

                this.GetPrjGroupList();
                this.GetPrjList();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetPrjGroupList()
        {
            string comcod = this.GetComeCode();
            string SearchMat = "%%";
            DataSet ds1 = AccessData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETGROUP", SearchMat, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "gencode not like '00000000'";

            this.listGroup.DataTextField = "gendesc";
            this.listGroup.DataValueField = "gencode";
            this.listGroup.DataSource = ds1.Tables[0];
            this.listGroup.DataBind();
            ds1.Dispose();

        }

        private void GetPrjList()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            DataSet ds1 = AccessData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            this.lstProject.DataTextField = "actdesc";
            this.lstProject.DataValueField = "actcode";
            this.lstProject.DataSource = ds1.Tables[0];
            this.lstProject.DataBind();
            ds1.Dispose();

        }




        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            this.GetSelectedData();
        }

        private void GetSelectedData()
        {
            string comcod = this.GetComeCode();

            string grpcode = "";
            string prjList = "";


            foreach (ListItem item in listGroup.Items)
            {
                if (item.Selected)
                {
                    grpcode += item.Value;
                }
            }



            foreach (ListItem item in lstProject.Items)
            {
                if (item.Selected)
                {
                    if (prjList.Length < 120)
                    {
                        prjList += item.Value;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Select Max 10 Project";

                        lstProject.Attributes["style"] = "background-color: red;";
                        return;
                    }
                }
            }

            DataSet ds1 = AccessData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS_01", "RPTGETPROJECTCOMPARE", grpcode, prjList, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }


            ViewState["tblCompareData"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblPrjList"] = ds1.Tables[1];


            this.Data_Bind();


        }
        private void Data_Bind()
        {
            DataTable tblPrj = (DataTable)ViewState["tblPrjList"];
            DataTable tblCompData = (DataTable)ViewState["tblCompareData"];
            for (int i = 0; i < tblPrj.Rows.Count; i++)
            {
                this.gvCompare.Columns[3 + i].HeaderText = tblPrj.Rows[i]["actdesc"].ToString();
                this.gvCompare.Columns[3 + i].Visible = true;
            }


            this.gvCompare.DataSource = tblCompData;
            this.gvCompare.DataBind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            string actcode = dt1.Rows[0]["acgcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["acgcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["acgcode"].ToString();
                    dt1.Rows[j]["gendesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["acgcode"].ToString();

                }
            }
            return dt1;

        }
        protected void gvCompare_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label txtgvgvItmRef = (Label)e.Row.FindControl("txtgvgvItmRef");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();


                if (code == "000000000000")
                {

                    txtgvgvItmRef.Font.Bold = true;
                    e.Row.Attributes["style"] = "background-color:pink;font-size:16px; font-weight:bold;";
                }
            }
        }
    }
}