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
    public partial class BusinessDashboard : System.Web.UI.Page
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
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFdate.Text = "01-" + System.DateTime.Today.ToString("MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "BD Department";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;
                this.GetProfession();
                this.GetKpiData();
                this.Createtable();


            }

        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            GetKpiData();
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            //this.hlnkshowall.NavigateUrl = "~/F_21_Mkt/LandDiscuDetails?Type=Report&prjcode=&Date1=" + this.txtdate.Text;
            GetKpiData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":
                    this.pnlMontDisc.Visible = true;
                    //this.pnlSodlUnit.Visible =false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlDissNew.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "1":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = true;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "2":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = true;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";

                    break;

                case "3":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = true;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "4":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = true;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "5":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = true;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "6":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = true;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[6].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "7":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = true;
                    this.PnlMarrDay.Visible = false;

                    this.RadioButtonList1.Items[7].Attributes["style"] = "background: #430000; display:block; ";

                    break;
                case "8":
                    this.pnlMontDisc.Visible = false;
                    //this.pnlSodlUnit.Visible = false;
                    this.pnlAppoinment.Visible = false;
                    this.pnlDissAppo.Visible = false;
                    this.pnlDissNew.Visible = false;
                    //this.pnlDataBank.Visible = false;
                    this.pnlNextApponi.Visible = false;
                    this.pnlbirthday.Visible = false;
                    this.PnlMarrDay.Visible = true;

                    this.RadioButtonList1.Items[8].Attributes["style"] = "background: #430000; display:block; ";

                    break;


            }
        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
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
        private void GetKpiData()
        {
            // 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = (hst["empid"].ToString() == "") ? "%" : (((hst["usrrmrk"].ToString() == "superadmin") || (hst["usrrmrk"].ToString() == "management")) ? "%" : hst["empid"].ToString());
            DateTime sdate = Convert.ToDateTime(this.txtFdate.Text);
            DateTime date = Convert.ToDateTime(this.txtdate.Text);


            DataSet dskpi = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "GETEMPKPIINTERFACELAND", userid, sdate.ToString("dd-MMM-yyyy"), date.ToString("dd-MMM-yyyy"), "", "", "", "", "", "");
            if (dskpi == null)
                return;
            DataTable dta = dskpi.Tables[3];

            this.hlnkLandowner.Text = Convert.ToDouble(dta.Rows[0]["ttcust"]).ToString("#,##0;(#,##0); ");

            //this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class='lbldata2'>" + "Months Discussion" + "</span>";
            //this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class=lbldata2>" + "Sold Unit" + "</span>";
            //this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pen-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class=lbldata2>" + "Today's Follow Up" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class=lbldata2>" + "Discussion " + "</span>";

            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class=lbldata2>" + "Discussion (New)" + "</span>";

            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class='lbldata2'>" + "Discussion(Data Bank)" + "</span>"; ;
            //this.RadioButtonList1.Items[6].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class='lbldata2'>" + "Tomorrow App." + "</span>"; ;
            //this.RadioButtonList1.Items[7].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class='lbldata2'>" + "Birth Day" + "</span>"; ;
            //this.RadioButtonList1.Items[8].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>"  + "</span>" + "<span class='lbldata2'>" + "Marriage Day" + "</span>"; ;




            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["mnthdis"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Months Discussion" + "</span>";
            //this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["sunit"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Sold Unit" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pen-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["aapnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Today's Follow Up" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disappnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Discussion " + "</span>";

            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disnew"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Discussion (New)" + "</span>";

            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["disdabnk"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Discussion(Data Bank)" + "</span>"; ;
            this.RadioButtonList1.Items[6].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["nextappnt"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Tomorrow App." + "</span>"; ;
            this.RadioButtonList1.Items[7].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["bday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Birth Day" + "</span>"; ;
            this.RadioButtonList1.Items[8].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dta.Rows[0]["mday"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Marriage Day" + "</span>"; ;

            this.gvclient.DataSource = null;
            this.gvclient.DataBind();
            this.gvAppo.DataSource = null;
            this.gvAppo.DataBind();
            this.gvDisappon.DataSource = null;
            this.gvDisappon.DataBind();
            this.gvDissNew.DataSource = null;
            this.gvDissNew.DataBind();
            this.gvNextAppt.DataSource = null;
            this.gvNextAppt.DataBind();
            this.gvClientBrthDay.DataSource = null;
            this.gvClientBrthDay.DataBind();
            this.gvCliMarrDay.DataSource = null;
            this.gvCliMarrDay.DataBind();



            DataTable dt = new DataTable();
            DataView dv = new DataView();
            DataTable dt0 = new DataTable();

            dt0 = ((DataTable)dskpi.Tables[0]).Copy();

            dv = dt0.DefaultView;
            dv.Sort = "cdate asc";
            dt = dv.ToTable();
            this.Data_Bind("gvclient", dt);



            //DataTable dt1 = new DataTable();
            //dt1 = ((DataTable)dskpi.Tables[0]).Copy();
            //dv = dt1.DefaultView;
            //dv.RowFilter = ("pactdesc1='"+"'");       
            //dt = dv.ToTable();
            //this.Data_Bind("gvSoldUnit", dt);


            DataTable dt2 = new DataTable();
            dt2 = ((DataTable)dskpi.Tables[0]).Copy();
            dv = dt2.DefaultView;
            dv.RowFilter = ("napnt1 ='" + date.ToString("dd-MMM-yyyy") + "' and nappcom = ''");
            dt = dv.ToTable();
            this.Data_Bind("gvAppo", dt);


            DataTable dt3 = new DataTable();
            dt3 = ((DataTable)dskpi.Tables[0]).Copy();
            dv = dt3.DefaultView;
            //dv.RowFilter = ("discount <>1 and cdate1 ='" + date.ToString("dd-MMM-yyyy") + "'");
            dv.RowFilter = ("cdate1 ='" + date.ToString("dd-MMM-yyyy") + "' and createdate1<>'" + date.ToString("dd-MMM-yyyy") + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvDisappon", dt);

            DataTable dt4 = new DataTable();
            dt4 = ((DataTable)dskpi.Tables[0]).Copy();
            dv = dt4.DefaultView;
            //dv.RowFilter = ("discount =1 and cdate1 ='" + date.ToString("dd-MMM-yyyy") + "'");
            dv.RowFilter = ("cdate1 ='" + date.ToString("dd-MMM-yyyy") + "' and createdate1='" + date.ToString("dd-MMM-yyyy") + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvDissNew", dt);



            string toapp = Convert.ToDateTime(date).AddDays(1).ToString("dd-MMM-yyyy");

            DataTable dt6 = new DataTable();
            dt6 = ((DataTable)dskpi.Tables[0]).Copy();
            dv = dt6.DefaultView;
            dv.RowFilter = ("napnt >= '" + toapp + "'");
            dt = dv.ToTable();
            this.Data_Bind("gvNextAppt", dt);

            //DataTable dt5 = ((DataTable)dskpi.Tables[2]).Copy();
            //this.Data_Bind("gvDataBank", dt5);




            DataTable dt7 = ((DataTable)dskpi.Tables[1]).Copy();
            //dv = dt7.DefaultView;
            //dv.RowFilter = ("gcod ='810100103009'");
            this.Data_Bind("gvClientBrthDay", dt7);


            DataTable dt8 = ((DataTable)dskpi.Tables[2]).Copy();
            //dv = dt8.DefaultView;
            //dv.RowFilter = ("gcod ='810100103010'");
            this.Data_Bind("gvCliMarrDay", dt8);


        }

        private void Data_Bind(string gv, DataTable dt)
        {

            //comcod, invno, mlccod, styleid,  invdate,   ordrqty, rate, actdesc, buyername
            switch (gv)
            {
                case "gvclient":
                    this.gvclient.DataSource = dt;//HiddenSameData(dt);
                    this.gvclient.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    for (int i = 0; i < this.gvclient.Rows.Count; i++)
                    {
                        string disgnote = ((Label)gvclient.Rows[i].FindControl("lblgvdisgnote")).Text.Trim();
                        ///string subgnote = ((Label)gvclient.Rows[i].FindControl("lblgvsubgnote")).Text.Trim();
                        if (disgnote.Length != 0)
                        {
                            this.gvclient.Rows[i].Cells[4].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                            //gvclient.Columns[9].ItemStyle.BackColor = System.Drawing.Color.FromName("#6EB6C2");
                        }
                        //if (subgnote.Length != 0)
                        //{
                        //    this.gvclient.Rows[i].Cells[9].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                        //}
                    }

                    break;
                //case "gvSoldUnit":
                //    this.gvSoldUnit.DataSource = dt;//HiddenSameData(dt);
                //    this.gvSoldUnit.DataBind();
                //    if (dt.Rows.Count == 0)
                //        return;
                //    break;

                case "gvAppo":
                    this.gvAppo.DataSource = dt;//HiddenSameData(dt);
                    this.gvAppo.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
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
                    if (dt.Rows.Count == 0)
                        return;
                    this.gvNextAppt.DataSource = dt;//HiddenSameData(dt);
                    this.gvNextAppt.DataBind();


                    for (int i = 0; i < this.gvNextAppt.Rows.Count; i++)
                    {
                        string subgnote = ((Label)gvNextAppt.Rows[i].FindControl("lblgvsubgnote")).Text.Trim();
                        if (subgnote.Length != 0)
                        {
                            this.gvNextAppt.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                        }
                    }

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

                //case "gvDataBank":
                //    this.gvDataBank.DataSource = dt;//HiddenSameData(dt);
                //    this.gvDataBank.DataBind();
                //    if (dt.Rows.Count == 0)
                //        return;
                //    break;
                case "gvClientBrthDay":
                    if (dt.Rows.Count == 0)
                        return;


                    this.gvClientBrthDay.DataSource = dt;//HiddenSameData(dt);
                    this.gvClientBrthDay.DataBind();

                    for (int i = 0; i < this.gvClientBrthDay.Rows.Count; i++)
                    {


                        DateTime tDate = Convert.ToDateTime(this.txtdate.Text.ToString());
                        DateTime bmday = Convert.ToDateTime(dt.Rows[i]["cbday"].ToString());
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
                        DateTime bmday = Convert.ToDateTime(dt.Rows[i]["cmday"].ToString());

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





        protected void gvAppo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                HyperLink custLink = (HyperLink)e.Row.FindControl("hyplCustomerAppo");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlink1");

                Label cusCode = (Label)e.Row.FindControl("lgbproscod");
                //Label cusCode = (Label)e.Row.FindControl("lgbproscod") as Label;
                //Label cdate = (Label)e.Row.FindControl("lgvMeetingdatA") as Label;
                string proscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "proscod")).ToString();
                string cdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "cdate")).ToString("dd-MMM-yyyyHH:mm");


                custLink.NavigateUrl = "../F_01_LPA/MktLandOwnerDiscus?Type=Edit&clientid=" + proscod + "&nfollow=" + cdate;

                hlink1.NavigateUrl = "../F_01_LPA/MktLandOwnerDiscus?Type=Entry&clientid=" + proscod + "&nfollow=" + cdate;
                //hyledit.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry?Type=Entry&clientid=" + cusCode.Text;

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
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

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

        protected void lnkAdddissub_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvNextAppt.Rows[index].FindControl("lblcDate")).Text.ToString().Trim();

            string lgvndissub = ((Label)this.gvNextAppt.Rows[index].FindControl("lgvndissub")).Text.ToString().Trim();
            string subgnote = ((Label)this.gvNextAppt.Rows[index].FindControl("lblgvsubgnote")).Text.ToString().Trim();
            string Empid = ((Label)this.gvNextAppt.Rows[index].FindControl("lgvteamcode")).Text.ToString().Trim();
            string Client = ((Label)this.gvNextAppt.Rows[index].FindControl("lgvproscode")).Text.ToString().Trim();

            this.GetClientData2(cDate, lgvndissub, subgnote, Empid, Client);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        private void GetClientData2(string cDate, string lgvndissub, string subgnote, string Empid, string Client)
        {


            this.lbldsi.InnerText = "Subject:";
            this.lbldiscussion.Text = lgvndissub;
            this.lblheader.InnerText = "Add New Comments on Subject for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = Empid;// this.ddlEmpid.SelectedValue.ToString();
            this.lblclient.Text = Client;//this.ddlClient.SelectedValue.ToString();
            this.lbldate.Text = cDate;
            if (subgnote.Length != 0)
                this.txtComm.Text = subgnote;
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

        protected void gvNextAppt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int index = e.Row.RowIndex;

                Panel pnlsub = (Panel)e.Row.FindControl("pnlsub");
                pnlsub.Attributes.Add("onmouseover", "AddButtonsub(" + index + ")");
                pnlsub.Attributes.Add("onmouseout", "HiddenButtonsub(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer;");

                LinkButton Lbtnsub = (LinkButton)e.Row.FindControl("lnkAdddissub");
                Lbtnsub.Attributes.Add("class", "hiddensub" + index);
                Lbtnsub.Attributes.Add("style", "display:none;");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "delempid")).ToString();
                string Empid = hst["empid"].ToString();


                if (dealcode == Empid)
                {
                    Lbtnsub.Visible = true;
                }
                else
                {
                    Lbtnsub.Visible = false;
                }

            }
        }



    }
}