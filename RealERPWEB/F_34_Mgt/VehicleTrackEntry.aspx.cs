using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_34_Mgt
{
    public partial class VehicleTrackEntry : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); //BDAccCommon.toDay().ToString("dd-MMM-yyyy");


                if (this.Request.QueryString["Type"] != "Edit")
                {
                    //this.ddltype_SelectedIndexChanged(null, null);
                }

                if (this.Request.QueryString["Type"] == "Entry")
                {

                    //((Label)this.Master.FindControl("lblTitle")).Text = "GL TRANSACTION";
                }
                else if (this.Request.QueryString["Type"] == "Edit")
                {
                    //((Label)this.Master.FindControl("lblTitle")).Text = "EDIT GL TRANSACTION";
                    //this.EditVoucher();
                }
                GetCompCode();
                Rescode();
                Drivercode();
                Emplyeecode();
            }


            //this.lblvalVoucherNo.Text = (this.Request.QueryString["Type"] == "Entry") ? this.GetVouCherNumber() : this.Request.QueryString["vounum"].ToString();



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        public void Rescode()
        {
            string comcod = this.GetCompCode();
            string sircode = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "GETRESCODE", sircode, "", "", "", "", "", "", "", "");
            this.ddlvehicle.DataTextField = "sirdesc";
            this.ddlvehicle.DataValueField = "sircode";
            this.ddlvehicle.DataSource = ds1.Tables[0];
            this.ddlvehicle.DataBind();
        }
        public void Drivercode()
        {
            string comcod = this.GetCompCode();
            string sircode = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "GETRESCODE", sircode, "", "", "", "", "", "", "", "");

            this.ddlvehicle.DataTextField = "sirdesc";
            this.ddlvehicle.DataValueField = "sircode";
            this.ddlvehicle.DataSource = ds1.Tables[0];
            this.ddlvehicle.DataBind();
        }
        public void Emplyeecode()
        {
            string comcod = this.GetCompCode();
            string sircode = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DAILYACTIVITIES", "GETRESCODE", sircode, "", "", "", "", "", "", "", "");
            this.ddlemp.DataTextField = "sirdesc";
            this.ddlemp.DataValueField = "sircode";
            this.ddlemp.DataSource = ds1.Tables[0];
            this.ddlemp.DataBind();
        }

    }
}