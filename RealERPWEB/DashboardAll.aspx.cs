using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using RealEntity;
using RealERPLIB;
namespace RealERPWEB
{
    public partial class DashboardAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.FindControl("pnlTitle").Visible = false;
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        //protected void lnkabp_Click(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "05";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "05";

        //    //Response.Write("<script>window.open('StepofOperation.aspx','_blank');</script>");

        //    // Response.Redirect("StepofOperation.aspx");

        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        //protected void lnkconp_Click(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "35";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "35";
        //    // Response.Redirect("StepofOperation.aspx");

        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;

        //}

        //protected void lbtnTender_Click(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "07";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "07";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        //protected void lnkbtnMatr_Click(object sender, EventArgs e)
        //{

        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "04";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "04";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        //protected void lnkbtnGoodsInv_Click(object sender, EventArgs e)
        //{

        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "12";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "12";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        //protected void lnkBill_OnClick(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "16";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "16";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        //protected void lnkbtnAssets_Click(object sender, EventArgs e)
        //{

        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "29";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "29";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}

        //protected void linkdocumt_Click(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "33";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "33";
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;

        //}

        protected void lnkbtnGeneral_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            //  hst["commod"] = "1";

            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('CompanyOverAllReport.aspx?comcod=" + comcod + "', target='_self');</script>";

        }

    }
}
