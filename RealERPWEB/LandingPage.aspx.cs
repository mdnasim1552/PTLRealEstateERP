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

using RealERPLIB;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
//using ASITFunLib;

namespace RealERPWEB
{

    public partial class LandingPage : System.Web.UI.Page
    {
        ProcessAccess ulogin = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Panel)this.Master.FindControl("pnlTitle")).Visible = false;
                this.pnlHousing.Visible = (this.Request.QueryString["Type"] == "HousingAll");
                this.pnlHousingStd.Visible = (this.Request.QueryString["Type"] == "HousingStd");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = date;
                this.txtdatetopfive.Text = date;
                ShowData(date);

            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string date2 = this.txtdate.Text;
            this.txtdatetopfive.Text = date2;
            ShowData(date2);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShowModal()", true);
            // do something
        }

        protected void lbtnOkA_Click(object sender, EventArgs e)
        {
            string date2 = this.txtdatetopfive.Text;
            this.txtdate.Text = date2;
            ShowData(date2);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShowModalA()", true);
            // do something
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ShowData(string date)
        {
            //this.tableid.Visible = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string usercode = "0000000";
            string fdate = date;
            string tdate = date;   //"18-Feb-2020";//System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
            DataSet dsmoduledata = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");
            DataView dv = dsmoduledata.Tables[0].DefaultView;
            dv.RowFilter = "usrper = '" + "true" + "'";
            DataTable dt1 = dv.ToTable();
            Session["Alldata"] = ds1;
            string innmodulelist = "";

            foreach (DataRow dr in dt1.Rows)
            {
                innmodulelist += @"<a href = 'StepofOperation.aspx?moduleid=" + dr["moduleid"] + "' class='list-group-item'>" + dr["modulename"] + "<span class='float-right badge badge-light round'></span></a>";
            }
            this.modlist.InnerHtml = innmodulelist;

            this.todaywrkcount.InnerHtml = ds1.Tables[2].Rows[0]["tcount"].ToString();
            int i = 0;
            string innHTML = "";
            foreach (DataRow dr in ds1.Tables[3].Rows)
            {
                string url1 = "";

                if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                {

                    byte[] ifff = (byte[])dr["usrimg"];
                    url1 = "data:image;base64," + Convert.ToBase64String(ifff);
                }
                else
                {
                    url1 = "Image/human_avatar.png";
                }
                i++;
                innHTML += @"<div class='col-md-12'><div class='col-md-3 img-rounded'><img src='" + url1 + "' class='img-fluid img-circle' alt='' height='100' width='100' /></div>" +
                                "<div class='col-md-7'><h4><strong>" + dr["usrname"] + "</strong></h4><h5>" + dr["usrdesig"] + "</h5></div></div>";

            }
            this.offlineUserCount.InnerHtml = Convert.ToString(i);
            this.OfflineUsers.InnerHtml = innHTML;

            string toactivity = "";
            foreach (DataRow dr in ds1.Tables[4].Rows)
            {
                //byte [] ifff=;
                string url = "";
                string func = "";
                if (dr["usrimg"] != null && dr["usrimg"].ToString() != "")
                {

                    byte[] ifff = (byte[])dr["usrimg"];
                    url = "data:image;base64," + Convert.ToBase64String(ifff);
                }
                else
                {
                    url = "Image/human_avatar.png";
                }


                func = dr["entryid"].ToString() + "," + dr["dateA"].ToString();


                toactivity += @"<div class='col-md-12' id='replacepart'><div class='col-md-3 img-rounded' id='Imageurl'><a href='#' onclick='GetData(" + func + ")'><img src='" + url + "' class='img-fluid img-circle' alt='' height='100' width='100' /></a></div>" +
                               "<div class='col-md-7' ><h4><strong>" + dr["usersname"] + "</strong></h4><h5><p class='list-group-item-text text-truncate'>" +
                                "<span class='text-dark font-size-sm'>" + dr["usrdesig"] + "</span> – <span class='badge badge-success'>" + dr["tcount"] + "</span> </p></h5></div>" +
                               "</div>";

            }
            this.TopActivity.InnerHtml = toactivity;
        }
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetData(string userdata, string dateA)
        {

            ProcessAccess ulogin = new ProcessAccess();
            Common common = new Common();
            string comcod = common.GetCompCode();
            string usrid = userdata;
            string usercode = "0000000";
            string fdate = dateA;
            string tdate = dateA;
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "%" + usrid + "%", "", "", "", "", "");
            List<DetailData> lst1 = ds1.Tables[1].DataTableToList<DetailData>();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst1);
            return json;
            //return js.Serialize(lst1);
        }
        //protected void lnkbtnGeneral_Click(object sender, EventArgs e)
        //{
        //    // Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //  hst["commod"] = "1";

        //    string comcod = this.GetCompCode();

        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('CompanyOverAllReport.aspx?comcod=" + comcod + "', target='_self');</script>";


        //}
    }

    public class DetailData
    {
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string entryid { get; set; }
        public string entryuser { get; set; }
        public string tcount { get; set; }
        public string usrimg { get; set; }
    }
}