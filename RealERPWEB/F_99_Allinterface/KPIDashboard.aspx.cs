using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
using RealOBJ;
using RealEntity;
using AjaxControlToolkit;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class KPIDashboard : System.Web.UI.Page
    {
        // private UserManagerKPI objUser = new UserManagerKPI();
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess KpiData = new ProcessAccess();
        public static string Url = "";
        public static string name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                                    (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES/CRM INTERFACE";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
                this.GetProfession();
                this.GetKpiData();
                this.Createtable();
                this.GetNewClient();
                this.LoadddlEmp();

            }

        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            GetKpiData();
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetKpiData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":
                    this.PnlClientAssign.Visible = true;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;
                    //this.lbtnOk_OnClick(null, null);

                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "1":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = true;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "2":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = true;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "3":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = true;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";

                    break;

                case "4":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = true;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "5":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = true;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "6":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = true;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[6].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "7":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = true;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[7].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "8":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = true;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[8].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "9":
                    this.PnlClientAssign.Visible = false;
                    this.pnlMontDisc.Visible = false;
                    this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = true;

                    this.RadioButtonList1.Items[9].Attributes["style"] = "background: #430000; display:block; ";

                    break;


            }
        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
        }

        private string GetLastclId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();

            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "GETCLIENTSINFO", "",
                                  "%%", userid, "", "", "", "", "");


            string lastid = ds1.Tables[1].Rows[0]["sircode"].ToString();
            return (lastid);


        }

        private void LoadddlEmp()
        {
            string comcod = this.GetCompCode();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EempList> lst = objuserman.GetEmpList(comcod);

            this.ddlTeam.DataTextField = "emplist";
            this.ddlTeam.DataValueField = "empid";
            this.ddlTeam.DataSource = lst;
            this.ddlTeam.DataBind();
        }

        //protected void lbtnOk_OnClick(object sender, EventArgs e)
        //{


        //    if (this.lbtnOk.Text == "Ok")
        //    {
        //        this.lbtnOk.Text = "New";
        //        this.GetProspectClient();

        //        return;
        //    }
        //    this.lbtnOk.Text = "Ok";
        //    this.gvAdDetails.DataSource = null;
        //    this.gvAdDetails.DataBind();

        //}

        //private void GetProspectClient()
        //{
        //    string comcod = this.GetCompCode();
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string usrid = hst["usrid"].ToString();
        //    string qtype = "MktClAss";
        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = objuserman.GetAllEmp(comcod, qtype);

        //    Session["tblClientInfo"] = lst;
        //    this.Data_Bind();

        //}

        //private void Data_Bind()
        //{
        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst =
        //        (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
        //    this.gvAdDetails.DataSource = lst;
        //    this.gvAdDetails.DataBind();


        //    string qtype = this.Request.QueryString["Type"].ToString();

        //    if (qtype == "MktAcceptClient")
        //    {
        //        //((LinkButton)this.gvAdDetails.FooterRow.FindControl("lUpdate")).Visible = false;


        //    }



        //}
        private void SaveValue()
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
            for (int i = 0; i < this.gvAdDetails.Rows.Count; i++)
            {
                bool chkid = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkempid")).Checked;
                if (chkid == true)
                {
                    string rmks = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclrmks")).Text.Trim();
                    bool chks = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkempid")).Checked ? true : false;

                    lst[i].rmks = rmks;
                    lst[i].chk = chks;
                }

            }
            Session["tblClientInfo"] = lst;


        }

        private string ComClienAssign()
        {
            string comcod = this.GetCompCode();
            string cassign = "";
            switch (comcod)
            {
                case "1205": //P2P
                case "3351"://P2P
                case "3352"://P2P
                    cassign = "Not Assign";
                    break;


                default:

                    break;



            }
            return cassign;



        }


        private void GetKpiData()
        {
            //
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            DateTime date = Convert.ToDateTime(this.txtdate.Text);
            string cassign = this.ComClienAssign();

            DataSet dskpi = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "GETEMPKPIINTERFACE", userid, date.ToString("dd-MMM-yyyy"), cassign, "", "", "", "", "", "");
            if (dskpi == null)
                return;
            DataTable dta = dskpi.Tables[5];

            this.ttlcustomer.Text = Convert.ToDouble(dta.Rows[0]["ttcust"]).ToString("#,##0;(#,##0); ");
            //this.RadioButtonList1.Items[5]. = false;
            //this.RadioButtonList1.Items[1].Enabled = false;
            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["assign"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Client Assign" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["mnthdis"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Months Discussion" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["sunit"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Sold Unit" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["aapnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Today's Follow Up" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disappnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Discussion " + "</span>";

            this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disnew"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Discussion (New)" + "</span>";

            this.RadioButtonList1.Items[6].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disdabnk"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Discu.(Data Bank)" + "</span>"; ;
            this.RadioButtonList1.Items[7].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["nextappnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Next App." + "</span>"; ;
            this.RadioButtonList1.Items[8].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["bday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Birth Day" + "</span>"; ;
            this.RadioButtonList1.Items[9].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["mday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Marriage Day" + "</span>"; ;


            DataTable dt = new DataTable();
            DataView dv = new DataView();
            DataTable dt0 = new DataTable();

            dt0 = ((DataTable)dskpi.Tables[0]).Copy();

            dv = dt0.DefaultView;
            dv.Sort = "cdate asc";
            dt = dv.ToTable();
            this.Data_Bind("gvclient", dt);



            DataTable dt1 = new DataTable();
            dt1 = ((DataTable)dskpi.Tables[0]).Copy();
            dv = dt1.DefaultView;
            dv.RowFilter = ("pactdesc='" + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvSoldUnit", dt);

            // DateTime date = Convert.ToDateTime(this.txtdate.Text);
            DateTime todayd1 = Convert.ToDateTime(date.ToString("dd-MMM-yyyy") + " 12:00 am");
            DateTime todayd2 = Convert.ToDateTime(date.ToString("dd-MMM-yyyy") + " 11:59 pm");

            //DateTime todayd1 =Convert.ToDateTime( System.DateTime.Today.ToString ("dd-MMM-yyyy")+" 12:00 am");
            //DateTime todayd2 = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy") + " 11:59 pm");
            // DateTime fday =Convert.ToDateTime( "01-" + date1.ToString("MMM-yyyy")+" 11:59 pm");

            DataTable dt2 = new DataTable();
            dt2 = ((DataTable)dskpi.Tables[1]).Copy();
            dv = dt2.DefaultView;
            dv.RowFilter = ("napnt >='" + todayd1 + "' and napnt <='" + todayd2 + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvAppo", dt);



            DataTable dt3 = new DataTable();
            dt3 = ((DataTable)dskpi.Tables[1]).Copy();
            dv = dt3.DefaultView;
            dv.RowFilter = ("discount <>1 and cdate1 ='" + date.ToString("dd-MMM-yyyy") + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvDisappon", dt);

            DataTable dt4 = new DataTable();
            dt4 = ((DataTable)dskpi.Tables[1]).Copy();
            dv = dt4.DefaultView;
            dv.RowFilter = ("discount =1 and cdate1 ='" + date.ToString("dd-MMM-yyyy") + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvDissNew", dt);



            string toapp = Convert.ToDateTime(date).AddDays(1).ToString("dd-MMM-yyyy");

            DataTable dt6 = new DataTable();
            dt6 = ((DataTable)dskpi.Tables[1]).Copy();
            dv = dt6.DefaultView;
            dv.RowFilter = ("napnt > '" + todayd2 + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvNextAppt", dt);

            DataTable dt5 = ((DataTable)dskpi.Tables[2]).Copy();
            this.Data_Bind("gvDataBank", dt5);




            DataTable dt7 = ((DataTable)dskpi.Tables[3]).Copy();
            dv = dt7.DefaultView;
            dv.RowFilter = ("gcod ='810100103009'");
            this.Data_Bind("gvClientBrthDay", dv.ToTable());


            DataTable dt8 = ((DataTable)dskpi.Tables[3]).Copy();
            dv = dt8.DefaultView;
            dv.RowFilter = ("gcod ='810100103010'");
            this.Data_Bind("gvCliMarrDay", dv.ToTable());

            DataTable dt9 = ((DataTable)dskpi.Tables[4]).Copy();
            this.Data_Bind("gvAdDetails", dt9);
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = dt9.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();
            Session["tblClientInfo"] = lst;
        }

        private void Data_Bind(string gv, DataTable dt)
        {

            //comcod, invno, mlccod, styleid,  invdate,   ordrqty, rate, actdesc, buyername
            switch (gv)
            {
                case "gvAdDetails":
                    this.gvAdDetails.DataSource = dt;//HiddenSameData(dt);
                    this.gvAdDetails.DataBind();
                    break;
                case "gvclient":
                    this.gvclient.DataSource = dt;//HiddenSameData(dt);
                    this.gvclient.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvSoldUnit":
                    this.gvSoldUnit.DataSource = dt;//HiddenSameData(dt);
                    this.gvSoldUnit.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvAppo":
                    this.gvAppo.DataSource = dt;//HiddenSameData(dt);
                    this.gvAppo.DataBind();
                    //if (dt.Rows.Count == 0)
                    //    return;
                    //for (int i = 0; i < this.gvAppo.Rows.Count; i++)
                    //{
                    //    DateTime tDate = Convert.ToDateTime(this.txtdate.Text.ToString());
                    //    DateTime bmday = Convert.ToDateTime(dt.Rows[i]["napnt1"].ToString());

                    //    if (tDate == bmday)
                    //    {
                    //       // this.gvAppo.Rows[i].BackColor = Color.SkyBlue;
                    //        this.gvAppo.Rows[i].ForeColor = Color.Black;

                    //    }
                    //}


                    break;
                case "gvDisappon":
                    this.gvDisappon.DataSource = dt;//HiddenSameData(dt);
                    this.gvDisappon.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvDissNew":
                    this.gvDissNew.DataSource = dt;//HiddenSameData(dt);
                    this.gvDissNew.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvNextAppt":
                    //if (dt.Rows.Count == 0)
                    //    return;

                    this.gvNextAppt.DataSource = dt;//HiddenSameData(dt);
                    this.gvNextAppt.DataBind();


                    //for (int i = 0; i < this.gvNextAppt.Rows.Count; i++)
                    //{


                    //    DateTime tDate = Convert.ToDateTime(this.txtdate.Text.ToString());
                    //    DateTime bmday = Convert.ToDateTime(dt.Rows[i]["napnt1"].ToString());
                    //    DateTime Nday = tDate.AddDays(1);

                    //    DateTime N7day = Nday.AddDays(7);



                    //    if (bmday == Nday)
                    //    {
                    //        this.gvNextAppt.Rows[i].BackColor = Color.SkyBlue;
                    //        this.gvNextAppt.Rows[i].ForeColor = Color.Black;

                    //    }
                    //    else if (bmday < Nday || bmday < N7day)
                    //    {
                    //        this.gvNextAppt.Rows[i].BackColor = Color.LightGray;
                    //        this.gvNextAppt.Rows[i].ForeColor = Color.Black;

                    //    }



                    //}
                    break;

                case "gvDataBank":
                    this.gvDataBank.DataSource = dt;//HiddenSameData(dt);
                    this.gvDataBank.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvClientBrthDay":
                    if (dt.Rows.Count == 0)
                        return;


                    this.gvClientBrthDay.DataSource = dt;//HiddenSameData(dt);
                    this.gvClientBrthDay.DataBind();

                    for (int i = 0; i < this.gvClientBrthDay.Rows.Count; i++)
                    {


                        DateTime tDate = Convert.ToDateTime(this.txtdate.Text.ToString());
                        DateTime bmday = Convert.ToDateTime(dt.Rows[i]["bmday"].ToString());
                        DateTime Nday = tDate.AddDays(1);

                        DateTime N7day = Nday.AddDays(7);



                        if (bmday == tDate)
                        {
                            this.gvClientBrthDay.Rows[i].BackColor = Color.SkyBlue;
                            this.gvClientBrthDay.Rows[i].ForeColor = Color.Black;

                        }
                        else if (bmday == Nday || bmday < N7day)
                        {
                            this.gvClientBrthDay.Rows[i].BackColor = Color.LightGray;
                            this.gvClientBrthDay.Rows[i].ForeColor = Color.Black;

                        }



                    }



                    break;
                case "gvCliMarrDay":
                    if (dt.Rows.Count == 0)
                        return;

                    this.gvCliMarrDay.DataSource = dt;//HiddenSameData(dt);
                    this.gvCliMarrDay.DataBind();

                    for (int i = 0; i < this.gvCliMarrDay.Rows.Count; i++)
                    {


                        DateTime tDate = Convert.ToDateTime(this.txtdate.Text.ToString());
                        DateTime bmday = Convert.ToDateTime(dt.Rows[i]["bmday"].ToString());

                        DateTime Nday = tDate.AddDays(1);

                        DateTime N7day = Nday.AddDays(7);


                        if (bmday == tDate)
                        //#430000
                        {
                            this.gvCliMarrDay.Rows[i].BackColor = Color.SkyBlue;
                            this.gvCliMarrDay.Rows[i].ForeColor = Color.Black;

                        }
                        else if (bmday == Nday || bmday < N7day)
                        {
                            this.gvCliMarrDay.Rows[i].BackColor = Color.LightGray;
                            this.gvCliMarrDay.Rows[i].ForeColor = Color.Black;

                        }
                        //else if (bmday > Nday && bmday < N7day)
                        //{
                        //    this.gvCliMarrDay.Rows[i].BackColor = Color.Red;
                        //    this.gvCliMarrDay.Rows[i].ForeColor = Color.Black;

                        //}




                    }



                    break;



            }
        }

        protected void gvclient_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                //HyperLink custLink = (HyperLink)e.Row.FindControl("hyplCustomer");
                //HyperLink disLink = (HyperLink)e.Row.FindControl("lgvDiscussion0");
                //string kproscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "proscod")).ToString().Trim();
                //string proscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "proscod")).ToString().Trim();
                //proscod = "9901" + proscod.Substring(4);
                //custLink.NavigateUrl = "~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" + proscod;
                //disLink.NavigateUrl = "~/F_21_Mkt/LandDiscuDetails?Type=Report&prjcode=" + kproscod + "&Date1=" + this.txtdate.Text;




                int index = e.Row.RowIndex;

                Panel Lbtn = (Panel)e.Row.FindControl("pnldis");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer;");

                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkAdddis");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none;");





                Hashtable hst = (Hashtable)Session["tblLogin"];
                string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delempid")).ToString();
                string Empid = hst["empid"].ToString();


                if (dealcode == Empid)
                {
                    Lbtn1.Visible = true;
                }
                else
                {
                    Lbtn1.Visible = false;
                }


            }


        }

        protected void gvSoldUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                HyperLink custLink = (HyperLink)e.Row.FindControl("hyplCustomerS");
                custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";

            }
        }

        protected void gvAppo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                // HyperLink custLink = (HyperLink)e.Row.FindControl("hyplCustomerAppo");
                HyperLink hlnkAdd = (HyperLink)e.Row.FindControl("hlnkAdd");
                HyperLink hyledit = (HyperLink)e.Row.FindControl("hyledit");
                Label cusCode = (Label)e.Row.FindControl("lgbproscod") as Label;
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "cdate")).ToString();
                string napnt = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "napnt")).ToString();

                // custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";
                hlnkAdd.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry?Type=Entry&clientid=" + cusCode.Text + "&followupdate=" + napnt;
                hyledit.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry?Type=Edit&clientid=" + cusCode.Text + "&followupdate=" + date;


            }
        }

        protected void gvDisappon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string comcod = this.GetCompCode();
                //HyperLink custLink = (HyperLink)e.Row.FindControl("hyplrDisAppo");
                //custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";

            }
        }

        protected void gvDissNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string comcod = this.GetCompCode();
                //HyperLink custLink = (HyperLink)e.Row.FindControl("hyplrDisAppo");
                //custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";

            }
        }



        protected void gvDataBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string comcod = this.GetCompCode();
                //HyperLink custLink = (HyperLink)e.Row.FindControl("hyplrDisAppo");
                //custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail?Type=Mgt";

            }
        }

        //Add New client 
        private void GetNewClient()
        {
            ViewState.Remove("tblsameDate");


            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();
            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "CLIENTINFOKPI", proscod, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();


            ViewState["tblsameDate"] = ds1.Tables[0];
            ds1.Dispose();



        }

        private void GetProfession()
        {
            string comcod = this.GetCompCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetProAndLocatio(comcod);
            var lst1 = lst.FindAll(l => l.gcod.Substring(0, 4) == "8601");

            ViewState["tblProfess"] = lst1;


        }



        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper>)
            ViewState["tblProfess"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                TextBox txtgv = (TextBox)e.Row.FindControl("txtgvVal");
                string txtgvname = ((TextBox)e.Row.FindControl("txtgvVal")).Text.ToString();

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01001")
                {

                    txtgv.ReadOnly = true;

                }

                if ((code == "810100103005") || (code == "810100103006"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
                }
                if (code == "810100103007")
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvValMob")).Visible = true;

                }

                if (code == "810100103013")
                {
                    //if (txtgvname != "")
                    //{
                    //    ((DropDownList)e.Row.FindControl("ddlprofession")).SelectedValue = txtgvname;

                    //}

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).Visible = true;

                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataTextField = "gdesc";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataValueField = "gcod";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataSource = lst;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataBind();

                }
                if ((code == "810100103009") || (code == "810100103010"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvCal")).Visible = true;

                }


            }

        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblimages"];
            //if (name=="")
            //    return;

            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);

            if (AsyncFileUpload1.HasFile)
            {

                //string holder = this.ddlimgperson.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/ClientImg/") + comcod + "_" + random + extension);

                Url = "~/Upload/ClientImg/" + comcod + "_" + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
            }
            else
            {
                Url = "~/Upload/ClientImg/NoImage.jpg";
            }

        }


        private void Createtable()
        {
            DataTable tbltemp = new DataTable();
            //tbltemp.Columns.Add("comitemid", Type.GetType("System.String"));

            tbltemp.Columns.Add("comcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("proscod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatat", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatad", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatan", Type.GetType("System.String"));



            ViewState["tbltemp"] = tbltemp;

        }
        private void saveValueClient()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();

            DataTable dt = (DataTable)ViewState["tbltemp"];
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DropDownList ddlprofession = this.gvPersonalInfo.Rows[i].FindControl("ddlprofession") as DropDownList;
                TextBox Gvalue = this.gvPersonalInfo.Rows[i].FindControl("txtgvVal") as TextBox;
                Label Gcode = this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode") as Label;
                Label gtype = this.gvPersonalInfo.Rows[i].FindControl("lgvgval") as Label;
                TextBox Gvalued = this.gvPersonalInfo.Rows[i].FindControl("txtgvCal") as TextBox;
                TextBox GvalueMob = this.gvPersonalInfo.Rows[i].FindControl("txtgvValMob") as TextBox;

                string txtData = "";
                string lblspcode = Gcode.Text;
                string dataType = gtype.Text;




                if (dataType == "N")
                {
                    txtData = ASTUtility.StrPosOrNagative(Gvalue.Text.Trim()).ToString();
                }
                if (dataType == "D")
                {
                    txtData = (Gvalued.Text.Length == 0) ? "01-Jan-1900" : Convert.ToDateTime(Gvalued.Text).ToString("dd-MMM-yyyy");

                    //if (Gcode.Text == "810100103009")
                    //{
                    //    if (txtData == "01-Jan-1900")
                    //    {
                    //        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    //        return;

                    //    }


                    //}
                }
                else
                {

                    if (ASTUtility.Right(Gcode.Text, 3) == "007")
                    {
                        txtData = GvalueMob.Text.Trim().ToString();
                    }
                    else if (ASTUtility.Right(Gcode.Text, 3) == "013")
                    {
                        txtData = ddlprofession.SelectedValue.ToString();
                    }
                    else
                    {
                        txtData = Gvalue.Text.Trim().ToString();
                    }

                }




                DataRow dr1 = dt.NewRow();


                dr1["comcod"] = comcod;
                dr1["proscod"] = proscod;
                dr1["gcod"] = lblspcode;
                dr1["gdatat"] = (dataType == "T" ? txtData : "");
                dr1["gdatan"] = (dataType == "N" ? txtData : "0.00");
                dr1["gdatad"] = (dataType == "D" ? txtData : "01-01-1900");






                dt.Rows.Add(dr1);
            }

            ViewState["tblPersonaldata"] = dt;



        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();
            //string Usircode = this.lblCode.Text.Trim();
            this.saveValueClient();

            //DataTable dt = (DataTable)ViewState["tblPersonaldata"];

            string paddres = string.Empty;
            string mobile = string.Empty;
            string email = string.Empty;
            string active = "1";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                TextBox Mobile = this.gvPersonalInfo.Rows[i].FindControl("txtgvValMob") as TextBox;

                if (((ASTUtility.Right(Gcode, 3) == "001") && (ASTUtility.Right(Gcode, 3) == "001") && Gvalue == ""))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Fill Value Must')", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
                    return;
                }

                if (Gcode == "810100103001")
                {
                    name = Gvalue;
                }



                if (Gcode == "810100103006")
                {
                    paddres = Gvalue;

                }
                if (Gcode == "810100103007")
                {
                    mobile = Mobile.Text.ToString();
                    //mobile = Gvalue;

                }
                if (Gcode == "810100103015")
                {
                    email = Gvalue;

                }
            }







            if (name.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Name  Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            // string Snameaddpandempid = Phone + Email;



            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENT", mobile, email, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dvd = ds2.Tables[0].DefaultView;
                dvd.RowFilter = ("sircode <>'" + proscod + "'");
                DataTable dt = dvd.ToTable();
                if (dt.Rows.Count == 0)
                    ;
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Found Duplicate Phone No');", true);
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Name" + "<br />" + "Sales Team: " + dt.Rows[0]["empname"].ToString();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Found Duplicate Name'" + "<br />" + "'Sales Team: '" + dt.Rows[0]["empname"].ToString()+ ");", true);
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }









            DataTable dtprsonl = (DataTable)ViewState["tblPersonaldata"];

            DataSet ds1 = new DataSet("ds1");
            DataView dv1 = new DataView(dtprsonl);
            ds1.Tables.Add(dv1.ToTable());
            ds1.Tables[0].TableName = "tbl1";


            bool result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "INSORUPCLIENTINFO", proscod, name, paddres, mobile, email, active, "", Url, "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }

            else
            {
                bool CResult = KpiData.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_XML_INFO_KPI", "SHOWCLIENTINFO", ds1, null, null, "", "", "", "", "", "");

                if (CResult == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    string url = "~/F_21_Mkt/MktEmpKpiEntry?Type=Entry&clientid=" + proscod;
                    Response.Redirect(url);
                }
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void txtgvVal_TextChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            string TxtMobile = ((TextBox)this.gvPersonalInfo.Rows[index].FindControl("txtgvValMob")).Text.ToString().Trim();


            DataSet result = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "CHECKDUPLICATEVALUE", TxtMobile, "", "", "", "", "", "");

            string msg = result.Tables[0].Rows[0]["msg"].ToString().Trim();

            string msg1 = msg.ToString() == "1" ? "Exits Client" : "New Client";


            if (msg == "1")
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + msg1 + "')", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
                string TxtMobile1 = ((TextBox)this.gvPersonalInfo.Rows[index].FindControl("txtgvValMob")).Text = "";
                ((TextBox)this.gvPersonalInfo.Rows[index + 1].FindControl("txtgvValMob")).Focus();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

            }



        }
        protected void gvCliMarrDay_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //TextBox txtgv = (TextBox)e.Row.FindControl("txtgvVal");
                //string txtgvname = ((TextBox)e.Row.FindControl("txtgvVal")).Text.ToString();

                //DateTime bmday = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "bday"));

                //DateTime tDate = Convert.ToDateTime(this.txtdate.ToString());

                //if (tDate == bmday)


                //{
                //    return;
                //}
                //if (code == "01001")
                //{

                //    txtgv.ReadOnly = true;

                //}



            }
        }


        protected void lnkAdddis_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblcDate")).Text.ToString().Trim();

            string discussion = ((Label)this.gvclient.Rows[index].FindControl("lgvDiscussion0")).Text.ToString().Trim();
            string disgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvdisgnote")).Text.ToString().Trim();
            string Empid = ((Label)this.gvclient.Rows[index].FindControl("lgvteamcode")).Text.ToString().Trim();
            string Client = ((Label)this.gvclient.Rows[index].FindControl("lgvproscode")).Text.ToString().Trim();

            this.GetClientData(cDate, discussion, disgnote, Empid, Client);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseCommentsModal();", true);

        }

        private void GetClientData(string cDate, string discussion, string disgnote, string Empid, string Client)
        {
            string comcod = this.GetCompCode();
            this.lbldsi.InnerText = "Discussion:";
            this.lbldiscussion.Text = discussion;
            this.lblheader.InnerText = "Add New Comments on discussion for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = Empid;// this.ddlEmpid.SelectedValue.ToString();
            this.lblclient.Text = Client;// this.ddlClient.SelectedValue.ToString();
            this.lbldate.Text = cDate;
            if (disgnote.Length != 0)
                this.txtComm.Text = disgnote;
            //DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPRECOMM", mobile, email, "", "", "", "", "", "", "");


        }
        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            string empid = this.lblEmpid.Text;
            string Client = this.lblclient.Text;
            string cdate = Convert.ToDateTime(this.lbldate.Text).ToString("dd-MMM-yyyy HH:mm:ss");
            string Gvalue = this.lbldiscussion.Text;
            string Comments = this.txtComm.Text;
            string gcod = "";
            string Type = this.lbldsi.InnerText;

            if (Type == "Discussion:")
            {

                gcod = "810100102015";
            }
            else
            {
                gcod = "810100102025";

            }
            bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }
            this.txtComm.Text = "";
            this.GetKpiData();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }




        protected void lnkbtnAssign_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAssign();", true);
        }




        protected void lnkbtnAssignTeam_Click(object sender, EventArgs e)
        {



            this.SaveValue();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
            var lst1 = lst.FindAll(c => c.chk == true);
            string comcod = this.GetCompCode();

            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ASITUtility03.ListToDataTable(lst1);
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            string empid = this.ddlTeam.SelectedValue;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();

            bool result = KpiData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "INSERTASSIGNACCEPTENCE", ds1, null, null, empid, usrid, "", "", "", "", "", "", "",
          "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = KpiData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {
                this.lnkbtnok_Click(null, null);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";


            }

        }
    }
}