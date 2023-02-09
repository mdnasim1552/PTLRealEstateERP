using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_04_Bgd
{
    public partial class ProjectMapView : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Information";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    Get_Project_Details();
                    GetLandList();
                }
            }

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetLandList()
        {
            Session.Remove("tblland");
            string comcod = this.GetComCode();
            string ProjectCode = this.Request.QueryString["prjcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GET_PROJECT_WISE_LAND_LIST", ProjectCode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = comcod;
            dr[1] = ProjectCode;
            dr[2] = "000000000000";
            dr[3] = "--Select Land--";
            dr[4] = "000000000000";
            dt.Rows.Add(dr);

            this.ddlLandinfo.DataTextField = "landdetails";
            this.ddlLandinfo.DataValueField = "landcode";
            this.ddlLandinfo.DataSource = dt;
            this.ddlLandinfo.DataBind();
            this.ddlLandinfo.SelectedValue = "000000000000";
            ViewState["tblland"] = ds1.Tables[0];


        }
        public void Get_Project_Details()
        {
            string mid = "1CIUvBHTzzsODBB86Dw4MXM0EIsn3UmN7";
            string lat = "23.834331392037353";
            string lon = "90.46946762983457";
            string zoom = "21";
            string comcod = this.GetComCode();
            string ProjectCode = this.Request.QueryString["prjcode"].ToString();
            string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + ProjectCode + "'"))[0]["factcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, fpactcode, "", "", "", "", "", "", "");
            if (ds1 != null)
            {
                DataView dv = ds1.Tables[0].DefaultView;
                dv.RowFilter = "gcod like '15%'";
                DataTable dt = dv.ToTable();
                lat = dt.Rows[1]["gdesc1"].ToString();
                lon = dt.Rows[2]["gdesc1"].ToString();
                zoom = dt.Rows[3]["gdesc1"].ToString();
                ViewState["Mapinfo"] = dv.ToTable();
                this.MapView.InnerHtml = "<iframe src=\"https://www.google.com/maps/d/u/0/embed?mid=" + mid + "&ll=" + lat + "," + lon + "&z=" + zoom + "\" width=\"1050\" height=\"610\"></iframe>";
            }

        }
        private void GET_LAND_INFO_DETAILS()
        {

            string landcode = this.ddlLandinfo.SelectedValue.ToString();
            string comcod = this.GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GETLANDINFODETAILS", landcode, "", "", "", "", "", "", "", "");

            ViewState["tbldata"] = ds1.Tables[0];

            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string mid = ((DataTable)ViewState["Mapinfo"]).Rows[0]["gdesc1"].ToString();
            string lat = ((DataTable)ViewState["Mapinfo"]).Rows[1]["gdesc1"].ToString();
            string lon = ((DataTable)ViewState["Mapinfo"]).Rows[2]["gdesc1"].ToString();
            string zoom = ((DataTable)ViewState["Mapinfo"]).Rows[3]["gdesc1"].ToString();

            DataTable tbl1 = (DataTable)ViewState["tbldata"];


            this.gvLandINfo.DataSource = tbl1;
            this.gvLandINfo.DataBind();

            DataView dv = tbl1.DefaultView;
            dv.RowFilter = "gcod like '15%'";
            DataTable dt = dv.ToTable();
            lat = dt.Rows[0]["gdesc1"].ToString();
            lon = dt.Rows[1]["gdesc1"].ToString();
            zoom = dt.Rows[2]["gdesc1"].ToString();
            this.MapView.InnerHtml = "<iframe src=\"https://www.google.com/maps/d/u/0/embed?mid=" + mid + "&ll=" + lat + "," + lon + "&z=" + zoom + "\" width=\"1050\" height=\"610\"></iframe>";

        }

        protected void gvLandINfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hyprrr = (HyperLink)e.Row.FindControl("hyprrr");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "02001" || code == "02003" || code == "02006" || code == "02009" || code == "02012")
                {

                    hyprrr.Visible = true;
                    //txtgvname.ReadOnly = true;

                }

            }
        }

        protected void ddlLandinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GET_LAND_INFO_DETAILS();
        }
    }
}