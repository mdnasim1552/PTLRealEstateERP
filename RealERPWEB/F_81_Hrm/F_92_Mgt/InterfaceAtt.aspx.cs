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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class InterfaceAtt : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Interface Attendance";



                this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");





                //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();

                lbtnOk_Click(null, null);


            }

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.HRLateAttData();
            this.ShowHRAttStatus();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void HRLateAttData()
        {
            string comcod = this.GetCompCode();
            string todydate = this.txtFdate.Text;

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE02", "GETHRATTENDENCE", todydate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }


            this.RadioButtonList1.Items[0].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata'>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["present"]).ToString() + "</span>" + "<span class=lbldata2>" + "Total Present" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class=lbldata>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["late"]).ToString() + "</span>" + "<span class=lbldata2>" + "Total Late" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=lbldata>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["earlyLev"]).ToString() + "</span>" + "<span class=lbldata2>" + "Early Leave" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-credit-card fan'> </span>" + "<br>" + "<span class=lbldata>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["onlev"]).ToString() + "</span>" + "<span class=lbldata2>" + "Leave" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-life-ring fan'> </span>" + "<br>" + "<span class=lbldata>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["absnt"]).ToString() + "</span>" + "<span class=lbldata2>" + "Absent" + "</span>";
            this.RadioButtonList1.Items[5].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata'>" + Convert.ToInt32(ds1.Tables[0].Rows[0]["ttlstap"]).ToString() + "</span>" + "<span class='lbldata2'>" + "Total Staff" + "</span>";


            ViewState["tblHRAttendace"] = ds1.Tables[0];
            ViewState["tblHRAttenPersen"] = ds1.Tables[1];

            this.Data_Bind();

        }

        private void ShowHRAttStatus()
        {
            ViewState.Remove("tblemplellandabs");
            string comcod = this.GetCompCode();
            string Date = this.txtFdate.Text;

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE02", "RPTLATEEONANDABSENTDETPR", Date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rplellaabsemp.DataSource = null;
                this.rplellaabsemp.DataBind();
                return;
            }
            ViewState["tblemplellandabs"] = ds1.Tables[0];
            this.Data_Bind();




        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblemplellandabs"];

            this.rplellaabsemp.DataSource = dt;
            this.rplellaabsemp.DataBind();

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    //Total Staff

                    //this.pnlTStaff.Visible = true;
                    //this.pnlPresent.Visible = false;
                    //this.pnlLate.Visible = false;
                    //this.pnlErLeave.Visible = false;
                    //this.pnlLeave.Visible = false;
                    //this.pnlAbsnt.Visible = false;




                    this.RadioButtonList1.Items[0].Attributes["style"] = "background:#5A5C59; display:block";
                    //this.RadioButtonList1.Items[0].Attributes.Add("class","lblactive");  f9f9f9   189697
                    //("class", "hidden");
                    // this.RadioButtonList1.Items[0].Attributes.CssStyle.ToString() = "lblactive";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background-color:#13A6A8; font-size:16px; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;  width:30px;";   

                    break;

                case "1":
                    //Present 
                    //this.pnlTStaff.Visible = false;
                    //this.pnlPresent.Visible = true;
                    //this.pnlLate.Visible = false;
                    //this.pnlErLeave.Visible = false;
                    //this.pnlLeave.Visible = false;
                    //this.pnlAbsnt.Visible = false;

                    this.pnlTStaff.Visible = true;


                    this.RadioButtonList1.Items[1].Attributes["style"] = "background:#5A5C59; display:block";
                    break;
                case "2":
                    //Late

                    //this.pnlTStaff.Visible = false;
                    //this.pnlPresent.Visible = false;
                    //this.pnlLate.Visible = true;
                    //this.pnlErLeave.Visible = false;
                    //this.pnlLeave.Visible = false;
                    //this.pnlAbsnt.Visible = false;

                    this.pnlTStaff.Visible = true;


                    this.RadioButtonList1.Items[2].Attributes["style"] = "background:#5A5C59; display:block";


                    break;


                case "3":
                    //Early leave
                    //this.pnlTStaff.Visible = false;
                    //this.pnlPresent.Visible = false;
                    //this.pnlLate.Visible = false;
                    //this.pnlErLeave.Visible = true;
                    //this.pnlLeave.Visible = false;
                    //this.pnlAbsnt.Visible = false;

                    this.pnlTStaff.Visible = true;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "background:#5A5C59; display:block";

                    break;
                case "4":
                    //On leave
                    //this.pnlTStaff.Visible = false;
                    //this.pnlPresent.Visible = false;
                    //this.pnlLate.Visible = false;
                    //this.pnlErLeave.Visible = false;
                    //this.pnlLeave.Visible = true;
                    //this.pnlAbsnt.Visible = false;

                    this.pnlTStaff.Visible = true;

                    this.RadioButtonList1.Items[4].Attributes["style"] = "background:#5A5C59; display:block";

                    break;

                case "5":
                    //Absent
                    //this.pnlTStaff.Visible = false;
                    //this.pnlPresent.Visible = false;
                    //this.pnlLate.Visible = false;
                    //this.pnlErLeave.Visible = false;
                    //this.pnlLeave.Visible = false;
                    //this.pnlAbsnt.Visible = true;

                    this.pnlTStaff.Visible = true;

                    this.RadioButtonList1.Items[5].Attributes["style"] = "background:#5A5C59; display:block";

                    break;




            }
        }
    }
}