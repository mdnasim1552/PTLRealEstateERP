using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
using RealERPLIB;
namespace RealERPWEB.F_21_MKT
{
    public partial class AllAdvertisement : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtfrmdate.Text = Convert.ToDateTime("01" + System.DateTime.Today.ToString("dd-MMM-yyyy").Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //((Label)this.Master.FindControl("lblTitle")).Text = "All Advertisement";


            }



        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void Data_Bind()
        {
            this.gvTopview.DataSource = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet>)ViewState["tblln"];
            this.gvTopview.DataBind();
        }
        private void LoadTopSheet()
        {
            string comcod = this.GetCompCode();
            string frmdate = this.txtfrmdate.Text;
            string todate = this.txttodate.Text;
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet> lst = objuserman.GetAdvTopSheet(comcod, frmdate, todate);

            ViewState["tblln"] = lst;
            this.Data_Bind();
        }

        protected void lbtnok_OnClick(object sender, EventArgs e)
        {
            this.LoadTopSheet();
            this.newEntry.Visible = true;
        }


        protected void gvTopview_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hbtnEdit");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string adno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_21_Mkt/AdvertisementEntry.aspx?Type=Entry&genno=" + adno;
                //hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval.aspx?Type=Approval&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
    }
}