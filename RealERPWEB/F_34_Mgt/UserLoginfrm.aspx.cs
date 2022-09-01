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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_34_Mgt
{
    public partial class UserLoginfrm : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "USER LOGIN FORM ";

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.ShowUserInfo();

                this.getListModulename();
                //this.ModuleVisible();
                this.GetCompPermission();
                this.getHomeMenu();
            }
        }

        private void GetCompPermission()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASIT", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }


            Session["tblcompper"] = ds2.Tables[0];

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void ibtnFindName_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowUserInfo();

        }

        private void getHomeMenu()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "HOMEMENULINK", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMenuLink.DataSource = ds1.Tables[0];
            this.ddlMenuLink.DataBind();
            this.ddlMenuLink.DataTextField = "modulename";
            this.ddlMenuLink.DataValueField = "url";
            this.ddlMenuLink.DataBind();
        }
        private void getListModulename()
        {

            string comcod = this.GetComeCode();
            ProcessAccess ulogin = new ProcessAccess();//GETCOMMODULE
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULEUPDATE", "", "", "", "", "", "", "", "", "");


            //DataView dv = ds1.Tables[0].DefaultView;
            //dv.RowFilter = ("moduleid<>'AA'");


            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = ds1.Tables[0];
            this.ddlModuleName.DataBind();
            ViewState["tblmoduleName"] = ds1.Tables[0];
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

    
        

        private void ModuleVisible()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string CompName = this.GetComeCode();
            string CompType = ASTUtility.Left(CompName, 1);
            string commod = hst["commod"].ToString();

            this.ddlModuleName.Items[0].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[1].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[2].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[3].Enabled = (CompType == "1" && (commod == "1"));//Tender
            this.ddlModuleName.Items[4].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[5].Enabled = (CompType == "2" && (commod == "1"));//Bgd Land

            this.ddlModuleName.Items[6].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[7].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));//

            this.ddlModuleName.Items[8].Enabled = (CompType == "0" && (commod == "1"));//Land Imp

            this.ddlModuleName.Items[9].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[10].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[11].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[12].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[13].Enabled = (CompType == "1" && (commod == "1"));// Bill

            this.ddlModuleName.Items[14].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[15].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[16].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[17].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[18].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[19].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[20].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));


            this.ddlModuleName.Items[21].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[22].Enabled = (CompType == "0" && (commod == "1"));// land control
            this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            this.ddlModuleName.Items[24].Enabled = (CompType == "0" && (commod == "1"));// Land MIS
            this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));


            this.ddlModuleName.Items[26].Enabled = (CompType == "3" && (commod == "1"));
            this.ddlModuleName.Items[27].Enabled = (CompType == "2" && (commod == "1"));
            this.ddlModuleName.Items[28].Enabled = ((CompType == "8") && (commod == "1"));
            this.ddlModuleName.Items[29].Enabled = ((CompType == "8") && (commod == "1"));//Grp Mis
            this.ddlModuleName.Items[30].Enabled = ((CompType == "7") && (commod == "1"));
            this.ddlModuleName.Items[31].Enabled = ((CompType == "8") && (commod == "1"));




            //this.ddlModuleName.Items[0].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[1].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[2].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[3].Enabled = (CompType == "1" && (commod == "1"));
            //this.ddlModuleName.Items[4].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[5].Enabled = (CompType == "2" && (commod == "1"));

            //this.ddlModuleName.Items[6].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[7].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));

            //this.ddlModuleName.Items[8].Enabled = (CompType == "2" && (commod == "1"));

            //this.ddlModuleName.Items[9].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[10].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[11].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[12].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[13].Enabled = (CompType == "1" && (commod == "1"));

            //this.ddlModuleName.Items[14].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[15].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[16].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[17].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[18].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[19].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[20].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));


            //this.ddlModuleName.Items[21].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[22].Enabled = (CompType == "2" && (commod == "1"));
            //this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[24].Enabled = (CompType == "2" && (commod == "1"));
            //this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));


            //this.ddlModuleName.Items[26].Enabled = (CompType == "3" && (commod == "1"));
            //this.ddlModuleName.Items[27].Enabled = (CompType == "2" && (commod == "1"));
            //this.ddlModuleName.Items[28].Enabled = ((CompType == "8") && (commod == "1"));
            //this.ddlModuleName.Items[29].Enabled = ((CompType == "8") && (commod == "1"));
            //this.ddlModuleName.Items[30].Enabled = ((CompType == "7") && (commod == "1"));






            //this.ddlModuleName.Items[0].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            ////this.ddlModuleName.Items[1].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[1].Enabled = ((CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[2].Enabled = ((CompType == "2") && (commod == "1")) ;
            //this.ddlModuleName.Items[3].Enabled = ((CompType == "1") && (commod == "1")) ;
            //this.ddlModuleName.Items[4].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[5].Enabled = ((CompType == "2") && (commod == "1")) ;
            //this.ddlModuleName.Items[6].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[7].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[8].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[9].Enabled = ((CompType == "2") && (commod == "1"));

            //this.ddlModuleName.Items[10].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[11].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[12].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //this.ddlModuleName.Items[13].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));



            ////this.ListModulename.Items[13].Enabled = (CompType == "1" || CompType == "2" || CompType == "3");

            //this.ddlModuleName.Items[14].Enabled = ((CompType == "1" && (commod == "1")));
            //this.ddlModuleName.Items[15].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[16].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //// this.ddlModuleName.Items[18].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[18]["usrper"]));


            //this.ddlModuleName.Items[17].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[18].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[19].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[20].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[21].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[22].Enabled = ((CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[23].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[24].Enabled = false;
            //this.ddlModuleName.Items[25].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[26].Enabled = ((CompType == "2" && (commod == "1"))) ;
            //this.ddlModuleName.Items[27].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1")));
            //// this.ddlModuleName.Items[30].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) && (Convert.ToBoolean(dt.Rows[30]["usrper"]));
            //this.ddlModuleName.Items[28].Enabled = ((CompType == "3" && (commod == "1"))) ;
            ////this.ddlModuleName.Items[32].Enabled = (CompType == "1");
            //this.ddlModuleName.Items[29].Enabled = ((CompType == "2" && (commod == "1"))) ;
            //this.ddlModuleName.Items[30].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "2" && (commod == "1")) || (CompType == "3" && (commod == "1"))) ;
            //this.ddlModuleName.Items[31].Enabled = ((CompType == "1" && (commod == "1")) || (CompType == "3" && (commod == "1")) || (CompType == "7" && (commod == "1")));
            //this.ddlModuleName.Items[32].Enabled = ((CompType == "2" && (commod == "1"))) ;

            //this.ddlModuleName.Items[33].Enabled = ((CompType == "9") && (commod == "1"));
            //this.ddlModuleName.Items[34].Enabled = ((CompType == "9") && (commod == "1"));
            //this.ddlModuleName.Items[35].Enabled = ((CompType == "9") && (commod == "1"));
            //this.ddlModuleName.Items[36].Enabled = ((CompType == "8") && (commod == "1"));
            //this.ddlModuleName.Items[37].Enabled = ((CompType == "8") && (commod == "1"));
            //this.ddlModuleName.Items[38].Enabled = ((CompType == "7") && (commod == "1")) ;

            //this.ddlModuleName.Items[39].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3")));
            //this.ddlModuleName.Items[40].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3")));
            //this.ddlModuleName.Items[41].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3")));
            //this.ddlModuleName.Items[42].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3")));
            //this.ddlModuleName.Items[43].Enabled = ((CompType == "1" && (commod == "3")) || (CompType == "2" && (commod == "3")) || (CompType == "3" && (commod == "3"))) ;
            //this.ddlModuleName.Items[44].Enabled = ((CompType == "1" && (commod == "4")) || (CompType == "2" && (commod == "4")) || (CompType == "3" && (commod == "1"))) ;


            // this.ddlModuleName.SelectedIndex = 46;


        }


        private void ShowUserInfo()
        {
            Session.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string SearcUser = "%" + this.txtSrcName.Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSER", SearcUser, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUseForm.DataSource = null;
                this.gvUseForm.DataBind();
                return;
            }

            Session["tblUsrinfo"] = ds1.Tables[0];
            Session["tblUsrinfo1"] = ds1.Tables[1];
            this.LoadGrid();


        }



        protected void ibtnFindName_Click(object sender, EventArgs e)
        {
            this.ShowUserInfo();

        }
        private void LoadGrid()
        {

            this.gvUseForm.DataSource = (DataTable)Session["tblUsrinfo"];
            this.gvUseForm.DataBind();



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string usrid = this.lblusrid.Text;

            DataTable dt = (DataTable)Session["tblUsrinfo"];
            var rptlist = dt.DataTableToList<RealEntity.C_34_Mgt.Userinfo>();

            LocalReport rptcb1 = new LocalReport();

            rptcb1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.UserLoginInfo", rptlist, null, null);

            rptcb1.SetParameters(new ReportParameter("compname", comnam));
            rptcb1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptcb1 = new  RealERPRPT.R_34_Mgt.RptUsrPerFrm();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptUname = rptcb1.ReportDefinition.ReportObjects["txtUserName"] as TextObject;
            //rptUname.Text = "User Name: " + this.txtuserid.Text; 

            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Date " + System.DateTime.Now.ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvUseForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUseForm.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvUseForm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvUseForm.EditIndex = -1;
            this.LoadGrid();
        }
        protected void gvUseForm_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = GetComeCode();
            string usrid = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvuserid")).Text.Trim();
            string usrsname = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtusrShorName")).Text.Trim();
            string usrfname = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtusrFullName")).Text.Trim();
            string usrdesig = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvDesig")).Text.Trim();
            string usrpass = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvpass")).Text.Trim();
            string usrrmrk = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvgvrmrk")).Text.Trim();
            string active = (((CheckBox)gvUseForm.Rows[e.RowIndex].FindControl("chkActive")).Checked) ? "1" : "0";
            string empid = ((DropDownList)this.gvUseForm.Rows[e.RowIndex].FindControl("ddlempid")).Text.Trim();

            string usermail = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("TxtWebmailID")).Text.Trim();
            string webmailpwd = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("TxtWebmailPWD")).Text.Trim();

            string userRole = ((DropDownList)this.gvUseForm.Rows[e.RowIndex].FindControl("ddlUserRole")).Text.Trim();


            //if (usrpass.Length == 0)
            //    return;
            usrpass = (usrpass.Length == 0) ? "" : ASTUtility.EncodePassword(usrpass);
            bool result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSORUPDATEUSR", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, empid, usermail, webmailpwd, userRole, "", "", "", "");


            if (!result)
            {
                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+msg+"');", true);
                return;

            }

            msg="User Information Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+msg+"');", true);
            this.gvUseForm.EditIndex = -1;
            this.ShowUserInfo();


            string eventtype = "User Login From";
            string eventdesc = "Update ID";
            string eventdesc2 = "Your profile Updated,";

            if (ConstantInfo.LogStatus == true)
            {               
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);                 
            }
            // for notification
            // title  details recvier id
            bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, usrid);
             

        }
        protected void gvUseForm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvUseForm.EditIndex = e.NewEditIndex;
            this.LoadGrid();

            string comcod = this.GetComeCode();
            //string gcode = ((Label)gvUseForm.Rows[e.NewEditIndex].FindControl("lblsecid")).Text.Trim().Replace("-", "");
            int rowindex = (this.gvUseForm.PageSize) * (this.gvUseForm.PageIndex) + e.NewEditIndex;
            string empcode = ((DataTable)Session["tblUsrinfo"]).Rows[rowindex]["empid"].ToString();
            string userrole = ((DataTable)Session["tblUsrinfo"]).Rows[rowindex]["userrole"].ToString();

           
            DropDownList ddl3 = (DropDownList)this.gvUseForm.Rows[e.NewEditIndex].FindControl("ddlempid");
            DropDownList ddlrole = (DropDownList)this.gvUseForm.Rows[e.NewEditIndex].FindControl("ddlUserRole");

            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%" + ((TextBox)gvUseForm.Rows[e.NewEditIndex].FindControl("txtSrCentrid")).Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl3.DataTextField = "empname";
            ddl3.DataValueField = "empid";
            ddl3.DataSource = ds1;
            ddl3.DataBind();
            ddl3.SelectedValue = empcode;

            ddlrole.SelectedValue = userrole;



        }
        protected void ibtnSrchCentr_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl3 = (DropDownList)this.gvUseForm.Rows[rowindex].FindControl("ddlempid");
            string SearchProject = "%" + ((TextBox)gvUseForm.Rows[rowindex].FindControl("txtSrCentrid")).Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo("", "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl3.DataTextField = "empname";
            ddl3.DataValueField = "empid";
            ddl3.DataSource = ds1;
            ddl3.DataBind();
        }
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            this.ddlModuleName.SelectedValue = "AA";
            Session.Remove("tblusrper");

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.GetComeCode();
            string usrid = Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            ///-------------------------//////////////
            //this.lblcopy.Visible = true;
            // this.ddlcopyuser.Visible = true;
            //this.lbtnCopy.Visible = true;
            // this.LoadCoyUser();
            this.lblId.Visible = true;
            this.txtuserid.Visible = true;
            DataTable tbl01 = (DataTable)Session["tblUsrinfo"];
            DataRow[] dr1 = tbl01.Select("usrid='" + usrid + "'");
            this.txtuserid.Text = dr1[0]["usrname"].ToString();
            ///-------------------------///////////
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
                Session["tblusrper"] = this.HiddenSameData(ds2.Tables[0]);
            else
            {



                DataTable dt = (DataTable)Session["tblcompper"];
                Session["tblusrper"] = this.HiddenSameData(dt);

            }
            this.ShowPer();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            //if (dt1.Rows.Count == 0)
            //    return dt1;

            //        string modulename = dt1.Rows[0]["modulename"].ToString();
            //        for (int j = 1; j < dt1.Rows.Count; j++)
            //        {
            //            if (dt1.Rows[j]["modulename"].ToString() == modulename)
            //            {
            //                modulename = dt1.Rows[j]["modulename"].ToString();
            //                dt1.Rows[j]["modulename"] = "";


            //            }

            //            else
            //            {
            //                modulename = dt1.Rows[j]["modulename"].ToString();
            //            }
            //        }



            //return dt1;
            if (dt1.Rows.Count == 0)
                return dt1;

            string modulename = dt1.Rows[0]["modulename"].ToString();
            string frmid1 = dt1.Rows[0]["frmid1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["frmid1"].ToString() == frmid1)
                {
                    dt1.Rows[j]["frmdesc"] = "";

                }


                if (dt1.Rows[j]["modulename"].ToString() == modulename)
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                    dt1.Rows[j]["modulename"] = "";
                }
                else
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                }



                frmid1 = dt1.Rows[j]["frmid1"].ToString();


            }



            return dt1;

        }
        private void ShowPer()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            this.gvPermission.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPermission.DataSource = (DataTable)Session["tblusrper"];
            this.gvPermission.DataBind();

        }

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            this.chkShowall.Checked = false;
            this.MultiView1.ActiveViewIndex = -1;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvUseForm.Visible = true;
            this.lblId.Visible = false;
            this.txtuserid.Visible = false;
            //this.lblcopy.Visible = false;
            //this.ddlcopyuser.Visible = false;
            //this.lbtnCopy.Visible = false;



            this.LoadGrid();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPer();
        }
        private void Session_update()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int index;
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvPermission.Rows[i].FindControl("chkPermit")).Checked) ? "True" : "False";
                string chkEntry = (((CheckBox)gvPermission.Rows[i].FindControl("chkEntry")).Checked) ? "True" : "False";
                string chkPrint = (((CheckBox)gvPermission.Rows[i].FindControl("chkPrint")).Checked) ? "True" : "False";
                string chkDelete = (((CheckBox)gvPermission.Rows[i].FindControl("chkDelete")).Checked) ? "True" : "False";

                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;
                dt.Rows[index]["entry"] = chkEntry;
                dt.Rows[index]["printable"] = chkPrint;
                dt.Rows[index]["delete"] = chkDelete;

            }
            Session["tblusrper"] = dt;
        }

        protected void gvPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_update();
            this.gvPermission.PageIndex = e.NewPageIndex;
            this.ShowPer();
        }
        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.Session_update();
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataTable dt1 = (DataTable)Session["tblusrper"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "frmid like '" + modname + "'";
            //DataTable dt1 = dv.ToTable();
            bool result = false;


            result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, modname,
                        "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {               
                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+msg+"');", true);
                return;
            }


            DataView dv = new DataView();
            DataSet ds1 = new DataSet("ds1");
            dv = dt1.DefaultView;
            dv.RowFilter = "chkper=True";
            dt1 = dv.ToTable();
            //    string chkper = dt1.Rows[i]["chkper"
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSRPER", ds1, null, null, usrid, "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+msg+"');", true);
                return;
            }



            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
            //    string frmid = dt1.Rows[i]["frmid"].ToString().Trim();
            //    string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
            //    string description = dt1.Rows[i]["dscrption"].ToString().Trim();
            //    string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
            //    string modulename = dt1.Rows[i]["modulename"].ToString().Trim();
            //    string entry = dt1.Rows[i]["entry"].ToString().Trim();
            //    string printable = dt1.Rows[i]["printable"].ToString().Trim();
            //    string delete = dt1.Rows[i]["delete"].ToString().Trim();
            //    if (chkper == "True")
            //    {
            //        result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSRPER", usrid, frmid, frmname,
            //             qrytype, description, modulename, entry, printable, delete, "", "", "", "", "", "");
            //    }
            //}

            // temporay off send notificaition
            //string eventdesc = "Page Permission Update";
            //string eventdesc2 = "Dear User, Some Permission Updated, Please Check, ";
            
            //bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, usrid);
             

            msg="User Permission Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+msg+"');", true);

        }
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            //this.ddlModName.SelectedValue = "00";

            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
                //this.ShowAllPer();
            }
            else
            {
                this.usrSpcPer();
            }
        }
        private void ShowAllPer()
        {
            DataTable dt = (DataTable)Session["tblcompper"];
            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;


                }

            }

            Session["tblusrper"] = this.HiddenSameData(dt);
            this.ShowPer();

        }
        private void usrSpcPer()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";
                    dt.Rows[index]["entry"] = "True";
                    dt.Rows[index]["printable"] = "True";
                    dt.Rows[index]["delete"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";
                    dt.Rows[index]["entry"] = "False";
                    dt.Rows[index]["printable"] = "False";
                    dt.Rows[index]["delete"] = "False";

                }

            }

            Session["tblusrper"] = dt;
            // this.ShowPer();

        }


        private void ShowAllData()
        {


            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);


            DataTable dt = (DataTable)Session["tblcompper"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "frmid like '" + modname + "'";
            DataTable dt2 = dv.ToTable();



            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string delete = dt1.Rows[i]["delete"].ToString().Trim();
                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt2.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["delete"] = delete;

                }

            }




            Session["tblusrper"] = this.HiddenSameData(dt2);
            this.ShowPer();






        }
        private void ShowData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWMODWISEFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
        }
        protected void gvPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;

            string frmid = ((Label)this.gvPermission.Rows[e.RowIndex].FindControl("lgvufrmid")).Text.Trim();

            bool result1 = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, frmid,
                            "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {                
                msg ="User Deleted Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+msg+"');", true);
                return;

            }
            this.ShowData();

        }
        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
            }
            else
            {
                this.ShowData();
            }
        }



        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                if (((CheckBox)this.gvPermission.Rows[i].FindControl("chkAll")).Checked)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                }
                //else
                //{
                //    //((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                //}
            }

        }
        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallView")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";


                }

            }

            Session["tblusrper"] = dt;
        }
        protected void chkallEntry_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallEntry")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "False";


                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkallPrint_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallPrint")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "False";

                }

            }

            Session["tblusrper"] = dt;

        }


        protected void chkallDelete_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallDelete")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "False";

                }

            }

            Session["tblusrper"] = dt;

        }

        //private void LoadCoyUser()
        //{

        //    string comcod = GetComeCode();

        //    DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSER", "%%", "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        this.ddlcopyuser.DataSource = null;
        //        this.ddlcopyuser.DataBind();
        //        return;
        //    }
        //    this.ddlcopyuser.DataTextField = "usrname";
        //    this.ddlcopyuser.DataValueField = "usrid";
        //    this.ddlcopyuser.DataSource = ds1.Tables[0];
        //    this.ddlcopyuser.DataBind();


        //}






        //protected void lbtnCopy_Click(object sender, EventArgs e)
        //{
        //    string comcod = this.GetComeCode();
        //    this.ddlModuleName.SelectedValue.ToString();

        //    string usrid = this.ddlcopyuser.SelectedValue.ToString();
        //    DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //    {
        //        this.gvPermission.DataSource = null;
        //        this.gvPermission.DataBind();
        //        return;
        //    }
        //    if (ds2.Tables[0].Rows.Count > 0)
        //        Session["tblusrper"] = this.HiddenSameData(ds2.Tables[0]);
        //    else
        //    {

        //        DataTable dt = (DataTable)Session["tblcompper"];
        //        Session["tblusrper"] = this.HiddenSameData(dt);
        //    }
        //    this.ShowPer();
        //}

        protected void lbtnLink_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string userid = ((LinkButton)this.gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.ToString();
            this.fromUserid.Text = userid;
            DataTable dt = ((DataTable)Session["tblUsrinfo"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "usrid<>'" + userid + "'";
            //  this.buyername.Text = dv.ToTable().Rows[0]["buyerdesc"].ToString();
            this.ddlUser.DataTextField = "usrname";
            this.ddlUser.DataValueField = "usrid";
            this.ddlUser.DataSource = dv.ToTable();
            this.ddlUser.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }



        protected void lblbtnSave_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComeCode();
            string fromusrid = this.fromUserid.Text.ToString();
            string tousrid = this.ddlUser.SelectedValue.ToString();
            bool result = User.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "COPY_USERTOUSER_PRIVILEGE", fromusrid, tousrid, "", "", "", "", "", "", "");


            if (result == false)
            {

                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+msg+"');", true);
                return;
            }

            else
            {
                msg="Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+msg+"');", true);

            }
    

            //this.lblMsg1.Text = "Sorry! There has some error!";
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblUsrinfo1"];
            this.txtmUesrId.Text = dt.Rows[0]["userid"].ToString();
            this.Bind_EmpId();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openUserModal();", true);
        }
        private void Bind_EmpId()
        {
            string comcod = this.GetComeCode();
            string empcode = ((DataTable)Session["tblUsrinfo"]).Rows[0]["empid"].ToString();
            string SearchProject = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddlmEmpId.DataTextField = "empname";
            ddlmEmpId.DataValueField = "empid";
            ddlmEmpId.DataSource = ds1;
            ddlmEmpId.DataBind();
            ddlmEmpId.SelectedValue = empcode;
        }


        protected void lbtnSaveUser_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();
            string usrid = this.txtmUesrId.Text.Trim();
            string usrsname = this.txtmShortName.Text.Trim();
            string usrfname = this.txtmFullName.Text.Trim();
            string usrdesig = this.txtmDesignation.Text.Trim();
            string usrpass = this.txtmPassword.Text.Trim();
            string usrrmrk = this.txtmGraph.Text.Trim();
            string active = this.chkmUserActive.Checked ? "1" : "0";
            string empid =  this.ddlmEmpId.SelectedValue.ToString();
            string usermail = this.txtmUserEmail.Text.Trim();
            string webmailpwd = this.txtmWebMailPass.Text.Trim();
            string userRole = this.ddlmUserRole.SelectedValue.ToString();
            string homeurl = this.ddlMenuLink.SelectedValue.ToString();

            

            usrpass = (usrpass.Length == 0) ? "" : ASTUtility.EncodePassword(usrpass);
            bool result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSORUPDATEUSR", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, empid, usermail, webmailpwd, userRole, homeurl, "", "", "");


            if (!result)
            {
                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+"New User Created Failed!"+"');", true);
                return;

            }

            msg="New User Created Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+msg+"');", true);
           
            this.ShowUserInfo();


            string eventtype = "User Login From";
            string eventdesc = "Update ID";
            string eventdesc2 = "Your profile Updated,";

            if (ConstantInfo.LogStatus == true)
            {
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            // for notification
            // title  details recvier id
            bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, usrid);
        }

        protected void lnkEditUser_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.Bind_EmpId();
          //  DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string empcode = ((LinkButton)this.gvUseForm.Rows[RowIndex].FindControl("lbtnUserId")).Text.Trim();
            string empid = ((Label)this.gvUseForm.Rows[RowIndex].FindControl("lblempid")).Text.Trim();
            


            DataSet ds1 = User.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETUSERINFOBYID", empcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
       
            
            this.txtmUesrId.Text = ds1.Tables[0].Rows[0]["usrid"].ToString();
            this.txtmShortName.Text = ds1.Tables[0].Rows[0]["usrsname"].ToString();
            this.txtmFullName.Text = ds1.Tables[0].Rows[0]["usrname"].ToString();
            this.txtmDesignation.Text = ds1.Tables[0].Rows[0]["usrdesig"].ToString();
            this.txtmPassword.Text = "";
            this.txtmUserEmail.Text = ds1.Tables[0].Rows[0]["mailid"].ToString();
            this.txtmWebMailPass.Text = ds1.Tables[0].Rows[0]["mailpass"].ToString();
            this.txtmGraph.Text = ds1.Tables[0].Rows[0]["usrrmrk"].ToString();
            
            if (empid != "")
            {
                this.ddlmEmpId.SelectedValue = empid;

            }

            this.ddlmUserRole.SelectedValue = ds1.Tables[0].Rows[0]["userrole"].ToString();
            this.chkmUserActive.Checked= (ds1.Tables[0].Rows[0]["usractive"].ToString()=="True")?true:false;
            this.ddlMenuLink.SelectedValue = ds1.Tables[0].Rows[0]["homeurl"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openUserModal();", true);

        }

        private void ResetData()
        {
            this.txtmUesrId.Text = "";
            this.txtmShortName.Text = "";
            this.txtmFullName.Text = "";
            this.txtmDesignation.Text = "";
            this.txtmPassword.Text = "";
            this.txtmUserEmail.Text = "";
            this.txtmWebMailPass.Text = "";
            this.txtmGraph.Text = "";
            this.ddlmEmpId.SelectedValue = "";
            this.ddlmUserRole.SelectedValue = "";
            this.chkmUserActive.Checked = false;
        }

        protected void ddlmUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}


