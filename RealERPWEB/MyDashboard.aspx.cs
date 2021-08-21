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
using RealERPWEB.Service;

namespace RealERPWEB
{

    public partial class MyDashboard : System.Web.UI.Page
    {
        //UserManager userManager = new UserManager();
        UserService objuserser = new UserService();
        ProcessAccess GrpData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk1")).Text = "";
            if (!IsPostBack)
            {

                ((Panel)this.Master.FindControl("pnlHead")).Visible = false;
                //this.pnlHead.Visible = false;
                if (Session["tblusrlog"] != null)
                {
                    DataSet ds2 = ((DataSet)Session["tblusrlog"]);
                    this.UserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
                    this.UserImg2.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
                }

                if (Session["tbllog1"] != null)
                {

                    this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                    this.lbladd.Text = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();
                    this.Image1.ImageUrl = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
                    this.lblLoginInfo.Text = "User: " + ((DataTable)Session["tbllog1"]).Rows[0]["usrsname"].ToString() + ", Session:" + ((DataTable)Session["tbllog1"]).Rows[0]["session"].ToString(); //+ ", Login Time: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
                                                                                                                                                                                                       //((Image)this.Master.FindControl("Image1"))
                }


                this.Panel8.Visible = (this.Request.QueryString["Type"] == "5000");
                this.Paneluddl.Visible = (this.Request.QueryString["Type"] == "5500");
                this.panelConstruction.Visible = (this.Request.QueryString["Type"] == "7000");
                this.pnlEdison.Visible = (this.Request.QueryString["Type"] == "5020");
                this.pnlkMIS.Visible = (this.Request.QueryString["Type"] == "5004");
                this.PnlERPSt.Visible = (this.Request.QueryString["Type"] == "5019");


                if (this.Request.QueryString["Type"] == "9000")
                {
                    this.CallCompanyList();

                }

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            //this.rbtnList1.DataTextField = "comsnam";
            //this.rbtnList1.DataValueField = "comcod";
            //this.rbtnList1.DataSource = ds1.Tables[0];
            //this.rbtnList1.DataBind();
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string comcod = "";
            //string comsnam = "";
            ////string Url1 = "F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=";
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //switch (rbtnList1.SelectedIndex)
            //{
            //    case 0:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();

            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 1:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;

            //    case 2:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 3:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 4:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1);
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 5:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;


            //    case 6:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 7:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1); 
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            //    case 8:
            //        this.PnlGrpDetails.Visible = true;
            //        this.PnlGrp.Visible = false;
            //        //comcod = rbtnList1.SelectedValue.ToString();
            //        //comsnam = rbtnList1.SelectedItem.Text.ToString();
            //        ////Url1 += comcod;
            //        ////Response.Redirect(Url1);
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            //        break;
            // }
        }
        public string GetCompCodeS()
        {

            return rbtnList1.SelectedValue.ToString();
        }
        public string GetCompCodeS1()
        {
            return rbtnList1.SelectedItem.Text.ToString();
        }

        protected void btnSales_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "23";
            ds1.Tables[0].Rows[0]["moduleid2"] = "23";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnPur_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "14";
            ds1.Tables[0].Rows[0]["moduleid2"] = "14";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void btnImp_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "09";
            ds1.Tables[0].Rows[0]["moduleid2"] = "09";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void btnAcc_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "15";
            ds1.Tables[0].Rows[0]["moduleid2"] = "15";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetCompCodeS();
            //string comsnam = this.GetCompCodeS1();

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpAccInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnOver_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_36_GrMgtInter/GrpDashBoardAll.aspx?comcod=" + comcod + "', target='_blank');</script>";

        }



        protected void lnkbtnGeneral_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            //  hst["commod"] = "1";

            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('CompanyOverAllReport.aspx?comcod=" + comcod + "', target='_self');</script>";

            //if (comcod == "3339"||comcod=="3336")
            //{
            //    ((Label)this.Master.FindControl ("lblprintstk1")).Text = @"<script>window.open('AllGraph.aspx?Type=5000', target='_self');</script>";

            //}
            //else
            //{
            //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('CompanyOverAllReport.aspx', target='_self');</script>";

            //}

            //((Label)this.Master.FindControl ("lblprintstk1")).Text = @"<script>window.open('AllGraph.aspx?Type=5000', target='_self');</script>";


        }
        protected void lnkbtnHr_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "80";
            ds1.Tables[0].Rows[0]["moduleid2"] = "80";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            ////hst["commod"] = "4";
            //((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('MyDashboard.aspx?Type=7000', target='_self');</script>";
        }
        protected void lnkbtnMKT_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "21";
            ds1.Tables[0].Rows[0]["moduleid2"] = "21";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            //// hst["commod"] = "3";
            // ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('MyDashboard.aspx?Type=5004', target='_self');</script>";
        }
        protected void lnkbtnGrp_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "1";
            this.loginBtn1_Click(null, null);
            //((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('DeafultMenu.aspx?Type=9000', target='_self');</script>";
        }
        protected void lnkbtnGrpOP_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //hst["commod"] = "1";
            this.loginBtn_Click(null, null);
            //((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('DeafultMenu.aspx?Type=9000', target='_self');</script>";
        }
        protected void loginBtn1_Click(object sender, EventArgs e)
        {
            //UserLogin ulog = new UserLogin();
            //DataSet ds1 = ulog.GetHitCounter();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(txtuserpass);
            DataTable dt5 = ((DataTable)Session["tbllog"]).Copy();
            DataView dv = dt5.DefaultView;
            dv.RowFilter = "comcod like '8%'";
            dt5 = dv.ToTable();
            string comcod = dt5.Rows[0]["comcod"].ToString();

            string modulid = "46";
            DataSet ds5 = GrpData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, "", "", "", "", "", "");

            Session["tblusrlog"] = ds5;
            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = comcod;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = "";// this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;


            //string Url1 = "DeafultMenu.aspx?Type=9000";
            string Url1 = "~/F_45_GrAcc/RptGrpMisDailyActiviteisJq.aspx";
            Response.Redirect(Url1);

        }
        protected void loginBtn_Click(object sender, EventArgs e)
        {
            //UserLogin ulog = new UserLogin();
            //DataSet ds1 = ulog.GetHitCounter();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(txtuserpass);
            DataTable dt5 = ((DataTable)Session["tbllog"]).Copy();
            DataView dv = dt5.DefaultView;
            dv.RowFilter = "comcod like '8%'";
            dt5 = dv.ToTable();
            string comcod = dt5.Rows[0]["comcod"].ToString();

            string modulid = "46";
            DataSet ds5 = GrpData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, "", "", "", "", "", "");

            Session["tblusrlog"] = ds5;
            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = comcod;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = "";// this.ddlModuleName.SelectedValue.ToString();
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;


            //string Url1 = "DeafultMenu.aspx?Type=9000";
            string Url1 = "~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx";
            Response.Redirect(Url1);

        }
        protected void lnkbtnAbp_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "05";
            ds1.Tables[0].Rows[0]["moduleid2"] = "05";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnProc_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "11";
            ds1.Tables[0].Rows[0]["moduleid2"] = "11";


            //string ModuleID = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid"].ToString().Trim();
            //string ModuleID2 = ((DataSet)Session["tblusrlog"]).Tables[0].Rows[0]["moduleid2"].ToString().Trim();

            //hst["commod"] = "1";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnSale_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "22";
            ds1.Tables[0].Rows[0]["moduleid2"] = "22";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnCR_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "23";
            ds1.Tables[0].Rows[0]["moduleid2"] = "23";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnCC_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "24";
            ds1.Tables[0].Rows[0]["moduleid2"] = "24";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnACC_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];

            ds1.Tables[0].Rows[0]["moduleid"] = "17";
            ds1.Tables[0].Rows[0]["moduleid2"] = "17";

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnlAND_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "01";
            ds1.Tables[0].Rows[0]["moduleid2"] = "01";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }


        protected void lnkbtnFeasi_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "02";
            ds1.Tables[0].Rows[0]["moduleid2"] = "02";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMatr_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sub = comcod.Substring(0, 1);
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = sub == "3" ? "04" : sub == "2" ? "51" : sub == "1" ? "04" : "04";
            ds1.Tables[0].Rows[0]["moduleid2"] = sub == "3" ? "04" : sub == "2" ? "51" : sub == "1" ? "04" : "04";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }


        protected void lnkbtnProd_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "13";
            ds1.Tables[0].Rows[0]["moduleid2"] = "13";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnGoodsInv_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "12";
            ds1.Tables[0].Rows[0]["moduleid2"] = "12";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnM_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "12";
            ds1.Tables[0].Rows[0]["moduleid2"] = "12";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnAssets_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "29";
            ds1.Tables[0].Rows[0]["moduleid2"] = "29";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnStepOpra_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sub = comcod.Substring(0, 1);
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = sub == "3" ? "68" : sub == "2" ? "71" : sub == "1" ? "70" : "68";
            ds1.Tables[0].Rows[0]["moduleid2"] = sub == "3" ? "68" : sub == "2" ? "71" : sub == "1" ? "70" : "68";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnStepOpraTra_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "38";
            ds1.Tables[0].Rows[0]["moduleid2"] = "38";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnExport_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "19";
            ds1.Tables[0].Rows[0]["moduleid2"] = "19";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void btnConstruction_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "09";
            ds1.Tables[0].Rows[0]["moduleid2"] = "09";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }


        protected void linkcentral_Click_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "09";
            ds1.Tables[0].Rows[0]["moduleid2"] = "09";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }


        protected void lnkSettings_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "35";
            ds1.Tables[0].Rows[0]["moduleid2"] = "35";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnHRM_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "81";
            ds1.Tables[0].Rows[0]["moduleid2"] = "81";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;


            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('DeafultMenu.aspx?Type=7000', target='_self');</script>";
        }
        protected void lnkbtnDocu_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "33";
            ds1.Tables[0].Rows[0]["moduleid2"] = "33";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnEva_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "32";
            ds1.Tables[0].Rows[0]["moduleid2"] = "32";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnPlaning_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "08";
            ds1.Tables[0].Rows[0]["moduleid2"] = "08";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "01";
            ds1.Tables[0].Rows[0]["moduleid2"] = "01";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "01";
            ds1.Tables[0].Rows[0]["moduleid2"] = "01";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMis_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "32";
            ds1.Tables[0].Rows[0]["moduleid2"] = "32";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        //Edit by uzzal

        protected void linkmanage_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sub = comcod.Substring(0, 1);
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = sub == "3" ? "35" : sub == "2" ? "58" : sub == "1" ? "35" : "35";
            ds1.Tables[0].Rows[0]["moduleid2"] = sub == "3" ? "35" : sub == "2" ? "58" : sub == "1" ? "35" : "35";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;

        }
        protected void linkdocumt_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "33";
            ds1.Tables[0].Rows[0]["moduleid2"] = "33";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;

        }
        protected void LinkBtnaudit_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "19";
            ds1.Tables[0].Rows[0]["moduleid2"] = "19";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;

        }
        protected void linkcentral_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "13";
            ds1.Tables[0].Rows[0]["moduleid2"] = "13";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;

        }

        protected void lnkBill_OnClick(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "16";
            ds1.Tables[0].Rows[0]["moduleid2"] = "16";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lbtnTender_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "07";
            ds1.Tables[0].Rows[0]["moduleid2"] = "07";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lbtnControlcosnt_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "58";
            ds1.Tables[0].Rows[0]["moduleid2"] = "58";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lbtnMisCont_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "60";
            ds1.Tables[0].Rows[0]["moduleid2"] = "60";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";
            Session["tblusrlog"] = ds1;


        }
        protected void lnkbtnStepOpraCon_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "70";
            ds1.Tables[0].Rows[0]["moduleid2"] = "70";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx?moduleid=', target='_self');</script>";

            Session["tblusrlog"] = ds1;
        }
        protected void lbtnMyDashBoard_Click(object sender, EventArgs e)
        {
            //F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=

            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('HRMAllInOne.aspx', target='_self');</script>";
        }
    }
}

