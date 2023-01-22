using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_21_MKT
{
    public partial class RptCrmNeedBase : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Need Base";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                GridSummary();
            }


        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GridSummary()
        {
            string comcod = this.GetComeCode();
            string Empid = "%";
            string Country = "%";
            string Dist = "%";
            string Zone = "%";
            string PStat = "%";
            string Area = "%";
            string Block = "%";            
            string Pri = "%";
            string Status = "%";
            string Other = "9";
            string TxtVal = "%";
            string srchempid = "%";           
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string mgt = "Management";
           

          

            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                 Pri, Status, Other, TxtVal, todate, srchempid, mgt);


            // DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
            //Pri, Status, Other, TxtVal, todate, srchempid);


            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();


            Session["tblsummData"] = ds3.Tables[0];
            this.dataBindGV();
         
        }

        private void dataBindGV()
        {
            DataTable dt = (DataTable)Session["tblsummData"];
            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("active='True'");

            this.gvSummary.DataSource = dv1.ToTable();
            this.gvSummary.DataBind();
            this.Excel_Bind();

        }

        private void Excel_Bind()
        {

            DataTable dt = (DataTable)Session["tblsummData"];
            if (dt.Rows.Count == 0)
                return;

            Session["Report1"] = gvSummary;
            this.hlbtntbCdataExcel.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            

        }



        protected void lnkgvHeader_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = (DataTable)ViewState["tblHeaderCheck"];
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");


            for (int i = 0; i < gvSummary.Rows[0].Cells.Count; i++)
            {
                string headerRowText = gvSummary.HeaderRow.Cells[i].Text;

                if (gvSummary.Columns[i].Visible == false)
                {
                    DataRow dr = dt1.NewRow();
                    if (headerRowText == "Code")
                    {
                    }
                    else
                    {
                        if (headerRowText != "")
                        {
                            dr["gcod"] = i;
                            dr["gvalue"] = headerRowText;
                            dt1.Rows.Add(dr);
                        }

                    }

                }

            }
            this.gvCurrent.DataSource = dt1;
            this.gvCurrent.DataBind();
            this.gvPrev.DataSource = dt2;
            gvPrev.DataBind();


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "OpenGvModal();", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "onchangetrigger();", true);
        }
        protected void lnkgvListShow_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");
            for (int i = 0; i < gvCurrent.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvCurrent.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvCurrent.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvCurrent.Rows[i].FindControl("chkgv")).Checked;
                if (chkbox)
                {
                    this.gvSummary.Columns[index].Visible = true;
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            for (int i = 0; i < gvPrev.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvPrev.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvPrev.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvPrev.Rows[i].FindControl("chkgv")).Checked;
                if (!chkbox)
                {
                    this.gvSummary.Columns[index].Visible = false;
                }
                else
                {
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            this.Excel_Bind();
            ViewState["tblHeaderCheck"] = dt1;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "onchangetrigger();", true);
        }

      

    }
}