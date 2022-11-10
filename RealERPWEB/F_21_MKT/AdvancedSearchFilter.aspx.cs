using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_21_MKT
{
    public partial class AdvancedSearchFilter : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Advanced Search Filter";
                this.GETEMPLOYEEUNDERSUPERVISED();


            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
       
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            try{
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userrole = hst["userrole"].ToString();
                string Empid = ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
                if (userrole == "1")
                {
                    Empid = "%";
                }
                string comcod = this.GetComeCode();
                string Country = "%";
                string Dist = "%";
                string Zone = "%";
                string PStat = "%";
                string Area = "%";
                string Block = "%";
                string Pri = "%";
                string Status = "%";
                string Other = this.ddlOther.SelectedValue.ToString();
                string TxtVal = "%" + this.txtVal.Text + "%";
                string frmdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string srchempid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmpid.SelectedValue.ToString());

                DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "GET_PROSPECT_DETAILS", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                     Pri, Status, Other, TxtVal, todate, srchempid);



                if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                    return;
                ViewState["tblempsup"] = ds3.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblempsup"];

                this.lblname.Text = dt1.Rows[0]["sircode"].ToString();
                this.lblconper.Text = dt1.Rows[0]["sirdesc"].ToString();
                this.lblmbl.Text = dt1.Rows[0]["phone"].ToString();
                this.lblhomead.Text = dt1.Rows[0]["caddress"].ToString();
                this.lblprof.Text = dt1.Rows[0]["cprof"].ToString();
                this.lblstatus.Text = dt1.Rows[0]["virnotes"].ToString();
            }
            catch (Exception ex)
            {

            }
           

        }
        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            try
            {
                string comcod = GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string Empid = hst["empid"].ToString();
                DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", Empid, "", "", "", "", "", "", "", "");
                ViewState["tblempsup"] = ds1.Tables[0];

                DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
                ViewState["tblsubddl"] = ds2.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblsubddl"];
                DataTable dtemp = (DataTable)ViewState["tblempsup"];
                DataView dv;
                dv = dt1.Copy().DefaultView;
                string ddlempid = this.ddlEmpid.SelectedValue.ToString();

                string userrole = hst["userrole"].ToString();
                string lempid = hst["empid"].ToString();
                //string empid = (userrole == "1" ? "93" : lempid) + "%";

                DataTable dtE = new DataTable();
                dv.RowFilter = ("gcod like '93%'");
                if (userrole == "1")
                {
                   
                    dtE = dv.ToTable();
                    dtE.Rows.Add("000000000000", "Choose Employee..", "");

                }

                else
                {
                    DataTable dts = dv.ToTable();
                    var query = (from dtl1 in dts.AsEnumerable()
                                 join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                                 select new
                                 {
                                     gcod = dtl1.Field<string>("gcod"),
                                     gdesc = dtl1.Field<string>("gdesc"),
                                     code = dtl1.Field<string>("code")
                                 }).ToList();
                    dtE = ASITUtility03.ListToDataTable(query);
                    if (dtE.Rows.Count >= 2)
                        dtE.Rows.Add("000000000000", "Choose Employee..", "");
                    // if(dtE.Rows.Count>1)
                    //dtE.Rows.Add("000000000000", "Choose Employee..", "");
                }
                ddlEmpid.ClearSelection();
                this.ddlEmpid.DataTextField = "gdesc";
                this.ddlEmpid.DataValueField = "gcod";
                this.ddlEmpid.DataSource = dtE;
                this.ddlEmpid.DataBind();

            }
            catch (Exception ex)
            {

            }
           

        }
    }
}