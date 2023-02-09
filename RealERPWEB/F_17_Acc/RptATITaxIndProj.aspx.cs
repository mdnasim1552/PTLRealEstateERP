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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptATITaxIndProj : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "AIT TAX VAT Project Wise ";
                //this.Master.Page.Title = "AIT TAX VAT Project Wise ";
                this.GetResList();
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + ASTUtility.Right(Date, 9);
                this.txtDateto.Text = Date;

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetResList()
        {

            string comcod = this.GetCompCode();
            string filter = "97%";
            string Seacch = "%" + this.txtSrchRes.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESLIST", filter, Seacch, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataBind();
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblaitvatsd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAITAVATPROJECT", frmdate, todate, resource, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvaitvsd.DataSource = null;
                this.gvaitvsd.DataBind();
                return;
            }

            DataTable dt = ds1.Tables[1];
            for (int i = 0; i < dt.Rows.Count; i++)
                this.gvaitvsd.Columns[i + 3].HeaderText = dt.Rows[i]["pactdesc"].ToString();

            Session["tblaitvatsd"] = ds1.Tables[0];
            Session["tblpact"] = ds1.Tables[1];

            this.Data_Bind();


            //DataTable dt = ds1.Tables[0];
            //Session["tblaitvatsd"] = dt;
            //this.Data_Bind();
        }

        private void Data_Bind()
        {
            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22,
                p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44,
                p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66,
                p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88,
                p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;
            DataTable dt = (DataTable)Session["tblaitvatsd"];

            p1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0.00 : dt.Compute("sum(p1)", "")));
            p2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0.00 : dt.Compute("sum(p2)", "")));
            p3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0.00 : dt.Compute("sum(p3)", "")));
            p4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0.00 : dt.Compute("sum(p4)", "")));
            p5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0.00 : dt.Compute("sum(p5)", "")));
            p6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0.00 : dt.Compute("sum(p6)", "")));
            p7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0.00 : dt.Compute("sum(p7)", "")));
            p8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0.00 : dt.Compute("sum(p8)", "")));
            p9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0.00 : dt.Compute("sum(p9)", "")));
            p10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0.00 : dt.Compute("sum(p10)", "")));

            p11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0.00 : dt.Compute("sum(p11)", "")));
            p12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0.00 : dt.Compute("sum(p12)", "")));
            p13 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0.00 : dt.Compute("sum(p13)", "")));
            p14 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0.00 : dt.Compute("sum(p14)", "")));
            p15 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0.00 : dt.Compute("sum(p15)", "")));
            p16 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0.00 : dt.Compute("sum(p16)", "")));
            p17 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0.00 : dt.Compute("sum(p17)", "")));
            p18 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0.00 : dt.Compute("sum(p18)", "")));
            p19 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0.00 : dt.Compute("sum(p19)", "")));
            p20 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0.00 : dt.Compute("sum(p20)", "")));
            p21 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p21)", "")) ? 0.00 : dt.Compute("sum(p21)", "")));
            p22 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p22)", "")) ? 0.00 : dt.Compute("sum(p22)", "")));


            p23 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p23)", "")) ? 0.00 : dt.Compute("sum(p23)", "")));
            p24 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p24)", "")) ? 0.00 : dt.Compute("sum(p24)", "")));
            p25 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p25)", "")) ? 0.00 : dt.Compute("sum(p25)", "")));
            p26 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p26)", "")) ? 0.00 : dt.Compute("sum(p26)", "")));
            p27 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p27)", "")) ? 0.00 : dt.Compute("sum(p27)", "")));
            p28 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p28)", "")) ? 0.00 : dt.Compute("sum(p28)", "")));
            p29 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p29)", "")) ? 0.00 : dt.Compute("sum(p29)", "")));
            p30 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p30)", "")) ? 0.00 : dt.Compute("sum(p30)", "")));

            p31 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p31)", "")) ? 0.00 : dt.Compute("sum(p31)", "")));
            p32 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p32)", "")) ? 0.00 : dt.Compute("sum(p32)", "")));
            p33 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p33)", "")) ? 0.00 : dt.Compute("sum(p33)", "")));
            p34 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p34)", "")) ? 0.00 : dt.Compute("sum(p34)", "")));
            p35 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p35)", "")) ? 0.00 : dt.Compute("sum(p35)", "")));
            p36 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p36)", "")) ? 0.00 : dt.Compute("sum(p36)", "")));
            p37 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p37)", "")) ? 0.00 : dt.Compute("sum(p37)", "")));
            p38 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p38)", "")) ? 0.00 : dt.Compute("sum(p38)", "")));
            p39 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p39)", "")) ? 0.00 : dt.Compute("sum(p39)", "")));
            p40 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p40)", "")) ? 0.00 : dt.Compute("sum(p40)", "")));
            p41 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p41)", "")) ? 0.00 : dt.Compute("sum(p41)", "")));
            p42 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p42)", "")) ? 0.00 : dt.Compute("sum(p42)", "")));

            p43 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p43)", "")) ? 0.00 : dt.Compute("sum(p43)", "")));
            p44 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p44)", "")) ? 0.00 : dt.Compute("sum(p44)", "")));
            p45 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p45)", "")) ? 0.00 : dt.Compute("sum(p45)", "")));
            p46 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p46)", "")) ? 0.00 : dt.Compute("sum(p46)", "")));
            p47 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p47)", "")) ? 0.00 : dt.Compute("sum(p47)", "")));
            p48 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p48)", "")) ? 0.00 : dt.Compute("sum(p48)", "")));
            p49 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p49)", "")) ? 0.00 : dt.Compute("sum(p49)", "")));
            p50 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p50)", "")) ? 0.00 : dt.Compute("sum(p50)", "")));

            p51 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p51)", "")) ? 0.00 : dt.Compute("sum(p51)", "")));
            p52 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p52)", "")) ? 0.00 : dt.Compute("sum(p52)", "")));
            p53 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p53)", "")) ? 0.00 : dt.Compute("sum(p53)", "")));
            p54 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p54)", "")) ? 0.00 : dt.Compute("sum(p54)", "")));
            p55 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p55)", "")) ? 0.00 : dt.Compute("sum(p55)", "")));
            p56 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p56)", "")) ? 0.00 : dt.Compute("sum(p56)", "")));
            p57 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p57)", "")) ? 0.00 : dt.Compute("sum(p57)", "")));
            p58 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p58)", "")) ? 0.00 : dt.Compute("sum(p58)", "")));
            p59 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p59)", "")) ? 0.00 : dt.Compute("sum(p59)", "")));
            p60 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p60)", "")) ? 0.00 : dt.Compute("sum(p60)", "")));
            p61 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p61)", "")) ? 0.00 : dt.Compute("sum(p61)", "")));
            p62 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p62)", "")) ? 0.00 : dt.Compute("sum(p62)", "")));

            p63 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p63)", "")) ? 0.00 : dt.Compute("sum(p63)", "")));
            p64 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p64)", "")) ? 0.00 : dt.Compute("sum(p64)", "")));
            p65 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p65)", "")) ? 0.00 : dt.Compute("sum(p65)", "")));
            p66 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p66)", "")) ? 0.00 : dt.Compute("sum(p66)", "")));
            p67 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p67)", "")) ? 0.00 : dt.Compute("sum(p67)", "")));
            p68 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p68)", "")) ? 0.00 : dt.Compute("sum(p68)", "")));
            p69 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p69)", "")) ? 0.00 : dt.Compute("sum(p69)", "")));
            p70 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p70)", "")) ? 0.00 : dt.Compute("sum(p70)", "")));

            p71 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p71)", "")) ? 0.00 : dt.Compute("sum(p71)", "")));
            p72 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p72)", "")) ? 0.00 : dt.Compute("sum(p72)", "")));
            p73 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p73)", "")) ? 0.00 : dt.Compute("sum(p73)", "")));
            p74 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p74)", "")) ? 0.00 : dt.Compute("sum(p74)", "")));
            p75 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p75)", "")) ? 0.00 : dt.Compute("sum(p75)", "")));
            p76 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p76)", "")) ? 0.00 : dt.Compute("sum(p76)", "")));
            p77 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p77)", "")) ? 0.00 : dt.Compute("sum(p77)", "")));
            p78 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p78)", "")) ? 0.00 : dt.Compute("sum(p78)", "")));
            p79 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p79)", "")) ? 0.00 : dt.Compute("sum(p79)", "")));
            p80 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p80)", "")) ? 0.00 : dt.Compute("sum(p80)", "")));
            p81 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p81)", "")) ? 0.00 : dt.Compute("sum(p81)", "")));
            p82 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p82)", "")) ? 0.00 : dt.Compute("sum(p82)", "")));

            p83 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p83)", "")) ? 0.00 : dt.Compute("sum(p83)", "")));
            p84 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p84)", "")) ? 0.00 : dt.Compute("sum(p84)", "")));
            p85 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p85)", "")) ? 0.00 : dt.Compute("sum(p85)", "")));
            p86 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p86)", "")) ? 0.00 : dt.Compute("sum(p86)", "")));
            p87 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p87)", "")) ? 0.00 : dt.Compute("sum(p87)", "")));
            p88 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p88)", "")) ? 0.00 : dt.Compute("sum(p88)", "")));
            p89 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p89)", "")) ? 0.00 : dt.Compute("sum(p89)", "")));
            p90 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p90)", "")) ? 0.00 : dt.Compute("sum(p90)", "")));

            p91 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p91)", "")) ? 0.00 : dt.Compute("sum(p91)", "")));
            p92 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p92)", "")) ? 0.00 : dt.Compute("sum(p92)", "")));
            p93 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p93)", "")) ? 0.00 : dt.Compute("sum(p93)", "")));
            p94 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p94)", "")) ? 0.00 : dt.Compute("sum(p94)", "")));
            p95 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p95)", "")) ? 0.00 : dt.Compute("sum(p95)", "")));
            p96 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p96)", "")) ? 0.00 : dt.Compute("sum(p96)", "")));
            p97 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p97)", "")) ? 0.00 : dt.Compute("sum(p97)", "")));
            p98 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p98)", "")) ? 0.00 : dt.Compute("sum(p98)", "")));
            p99 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p99)", "")) ? 0.00 : dt.Compute("sum(p99)", "")));
            p100 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p100)", "")) ? 0.00 : dt.Compute("sum(p100)", "")));



            this.gvaitvsd.Columns[3].Visible = (p1 != 0);
            this.gvaitvsd.Columns[4].Visible = (p2 != 0);
            this.gvaitvsd.Columns[5].Visible = (p3 != 0);
            this.gvaitvsd.Columns[6].Visible = (p4 != 0);
            this.gvaitvsd.Columns[7].Visible = (p5 != 0);
            this.gvaitvsd.Columns[8].Visible = (p6 != 0);
            this.gvaitvsd.Columns[9].Visible = (p7 != 0);
            this.gvaitvsd.Columns[10].Visible = (p8 != 0);
            this.gvaitvsd.Columns[11].Visible = (p9 != 0);
            this.gvaitvsd.Columns[12].Visible = (p10 != 0);
            this.gvaitvsd.Columns[13].Visible = (p11 != 0);
            this.gvaitvsd.Columns[14].Visible = (p12 != 0);
            this.gvaitvsd.Columns[15].Visible = (p13 != 0);
            this.gvaitvsd.Columns[16].Visible = (p14 != 0);
            this.gvaitvsd.Columns[17].Visible = (p15 != 0);
            this.gvaitvsd.Columns[18].Visible = (p16 != 0);
            this.gvaitvsd.Columns[19].Visible = (p17 != 0);
            this.gvaitvsd.Columns[20].Visible = (p18 != 0);
            this.gvaitvsd.Columns[21].Visible = (p19 != 0);
            this.gvaitvsd.Columns[22].Visible = (p20 != 0);
            this.gvaitvsd.Columns[23].Visible = (p21 != 0);
            this.gvaitvsd.Columns[24].Visible = (p22 != 0);

            this.gvaitvsd.Columns[25].Visible = (p23 != 0);
            this.gvaitvsd.Columns[26].Visible = (p24 != 0);
            this.gvaitvsd.Columns[27].Visible = (p25 != 0);
            this.gvaitvsd.Columns[28].Visible = (p26 != 0);
            this.gvaitvsd.Columns[29].Visible = (p27 != 0);
            this.gvaitvsd.Columns[30].Visible = (p28 != 0);
            this.gvaitvsd.Columns[31].Visible = (p29 != 0);
            this.gvaitvsd.Columns[32].Visible = (p30 != 0);
            this.gvaitvsd.Columns[33].Visible = (p31 != 0);
            this.gvaitvsd.Columns[34].Visible = (p32 != 0);
            this.gvaitvsd.Columns[35].Visible = (p33 != 0);
            this.gvaitvsd.Columns[36].Visible = (p34 != 0);
            this.gvaitvsd.Columns[37].Visible = (p35 != 0);
            this.gvaitvsd.Columns[38].Visible = (p36 != 0);
            this.gvaitvsd.Columns[39].Visible = (p37 != 0);
            this.gvaitvsd.Columns[40].Visible = (p38 != 0);
            this.gvaitvsd.Columns[41].Visible = (p39 != 0);
            this.gvaitvsd.Columns[42].Visible = (p40 != 0);
            this.gvaitvsd.Columns[43].Visible = (p41 != 0);
            this.gvaitvsd.Columns[44].Visible = (p42 != 0);
            this.gvaitvsd.Columns[45].Visible = (p43 != 0);
            this.gvaitvsd.Columns[46].Visible = (p44 != 0);
            this.gvaitvsd.Columns[47].Visible = (p45 != 0);
            this.gvaitvsd.Columns[48].Visible = (p46 != 0);
            this.gvaitvsd.Columns[49].Visible = (p47 != 0);
            this.gvaitvsd.Columns[50].Visible = (p48 != 0);
            this.gvaitvsd.Columns[51].Visible = (p49 != 0);
            this.gvaitvsd.Columns[52].Visible = (p50 != 0);
            this.gvaitvsd.Columns[53].Visible = (p51 != 0);
            this.gvaitvsd.Columns[54].Visible = (p52 != 0);
            this.gvaitvsd.Columns[55].Visible = (p53 != 0);
            this.gvaitvsd.Columns[56].Visible = (p54 != 0);
            this.gvaitvsd.Columns[57].Visible = (p55 != 0);
            this.gvaitvsd.Columns[58].Visible = (p56 != 0);
            this.gvaitvsd.Columns[59].Visible = (p57 != 0);
            this.gvaitvsd.Columns[60].Visible = (p58 != 0);
            this.gvaitvsd.Columns[61].Visible = (p59 != 0);
            this.gvaitvsd.Columns[62].Visible = (p60 != 0);
            this.gvaitvsd.Columns[63].Visible = (p61 != 0);
            this.gvaitvsd.Columns[64].Visible = (p62 != 0);
            this.gvaitvsd.Columns[65].Visible = (p63 != 0);
            this.gvaitvsd.Columns[66].Visible = (p64 != 0);
            this.gvaitvsd.Columns[67].Visible = (p65 != 0);
            this.gvaitvsd.Columns[68].Visible = (p66 != 0);
            this.gvaitvsd.Columns[69].Visible = (p67 != 0);
            this.gvaitvsd.Columns[70].Visible = (p68 != 0);
            this.gvaitvsd.Columns[71].Visible = (p69 != 0);
            this.gvaitvsd.Columns[72].Visible = (p70 != 0);
            this.gvaitvsd.Columns[73].Visible = (p71 != 0);
            this.gvaitvsd.Columns[74].Visible = (p72 != 0);
            this.gvaitvsd.Columns[75].Visible = (p73 != 0);
            this.gvaitvsd.Columns[76].Visible = (p74 != 0);
            this.gvaitvsd.Columns[77].Visible = (p75 != 0);
            this.gvaitvsd.Columns[78].Visible = (p76 != 0);
            this.gvaitvsd.Columns[79].Visible = (p77 != 0);
            this.gvaitvsd.Columns[80].Visible = (p78 != 0);
            this.gvaitvsd.Columns[81].Visible = (p79 != 0);
            this.gvaitvsd.Columns[82].Visible = (p80 != 0);
            this.gvaitvsd.Columns[83].Visible = (p81 != 0);
            this.gvaitvsd.Columns[84].Visible = (p82 != 0);


            this.gvaitvsd.Columns[85].Visible = (p83 != 0);
            this.gvaitvsd.Columns[86].Visible = (p84 != 0);
            this.gvaitvsd.Columns[87].Visible = (p85 != 0);
            this.gvaitvsd.Columns[88].Visible = (p86 != 0);
            this.gvaitvsd.Columns[89].Visible = (p87 != 0);
            this.gvaitvsd.Columns[90].Visible = (p88 != 0);
            this.gvaitvsd.Columns[91].Visible = (p89 != 0);
            this.gvaitvsd.Columns[92].Visible = (p90 != 0);
            this.gvaitvsd.Columns[93].Visible = (p91 != 0);
            this.gvaitvsd.Columns[94].Visible = (p92 != 0);
            this.gvaitvsd.Columns[95].Visible = (p93 != 0);
            this.gvaitvsd.Columns[96].Visible = (p94 != 0);
            this.gvaitvsd.Columns[97].Visible = (p95 != 0);
            this.gvaitvsd.Columns[98].Visible = (p96 != 0);
            this.gvaitvsd.Columns[99].Visible = (p97 != 0);
            this.gvaitvsd.Columns[100].Visible = (p98 != 0);

            this.gvaitvsd.Columns[101].Visible = (p99 != 0);
            this.gvaitvsd.Columns[102].Visible = (p100 != 0);

            this.gvaitvsd.DataSource = dt;
            this.gvaitvsd.DataBind();
            this.FooterCal();


        }





        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblaitvatsd"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ?
             0 : dt.Compute("sum(tamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ?
                    0 : dt.Compute("sum(p1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ?
                    0 : dt.Compute("sum(p2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ?
                    0 : dt.Compute("sum(p3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ?
                    0 : dt.Compute("sum(p4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ?
                    0 : dt.Compute("sum(p5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ?
                   0 : dt.Compute("sum(p6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lblFprj7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ?
                   0 : dt.Compute("sum(p7)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ?
               0 : dt.Compute("sum(p8)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ?
             0 : dt.Compute("sum(p9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ?
               0 : dt.Compute("sum(p10)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ?
              0 : dt.Compute("sum(p11)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ?
              0 : dt.Compute("sum(p12)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ?
              0 : dt.Compute("sum(p13)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ?
               0 : dt.Compute("sum(p14)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ?
              0 : dt.Compute("sum(p15)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ?
              0 : dt.Compute("sum(p16)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ?
              0 : dt.Compute("sum(p17)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ?
              0 : dt.Compute("sum(p18)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ?
              0 : dt.Compute("sum(p19)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ?
              0 : dt.Compute("sum(p20)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p21)", "")) ?
              0 : dt.Compute("sum(p21)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p22)", "")) ?
              0 : dt.Compute("sum(p22)", ""))).ToString("#,##0.00;(#,##0.00); ");




            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p23)", "")) ?
                  0 : dt.Compute("sum(p23)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p24)", "")) ?
                    0 : dt.Compute("sum(p24)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p25)", "")) ?
                    0 : dt.Compute("sum(p25)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p26)", "")) ?
                   0 : dt.Compute("sum(p26)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lblFprj27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p27)", "")) ?
                   0 : dt.Compute("sum(p27)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p28)", "")) ?
               0 : dt.Compute("sum(p28)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p29)", "")) ?
             0 : dt.Compute("sum(p29)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p30)", "")) ?
               0 : dt.Compute("sum(p30)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p31)", "")) ?
              0 : dt.Compute("sum(p31)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p32)", "")) ?
              0 : dt.Compute("sum(p32)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p33)", "")) ?
              0 : dt.Compute("sum(p33)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p34)", "")) ?
               0 : dt.Compute("sum(p34)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p35)", "")) ?
              0 : dt.Compute("sum(p35)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p36)", "")) ?
              0 : dt.Compute("sum(p36)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p37)", "")) ?
              0 : dt.Compute("sum(p37)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p38)", "")) ?
              0 : dt.Compute("sum(p38)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p39)", "")) ?
              0 : dt.Compute("sum(p39)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p40)", "")) ?
              0 : dt.Compute("sum(p40)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p41)", "")) ?
              0 : dt.Compute("sum(p41)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p42)", "")) ?
              0 : dt.Compute("sum(p42)", ""))).ToString("#,##0.00;(#,##0.00); ");




            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p43)", "")) ?
                  0 : dt.Compute("sum(p43)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p44)", "")) ?
                    0 : dt.Compute("sum(p44)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p45)", "")) ?
                    0 : dt.Compute("sum(p45)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p46)", "")) ?
                   0 : dt.Compute("sum(p46)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lblFprj47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p47)", "")) ?
                   0 : dt.Compute("sum(p47)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p48)", "")) ?
               0 : dt.Compute("sum(p48)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p49)", "")) ?
             0 : dt.Compute("sum(p49)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p50)", "")) ?
               0 : dt.Compute("sum(p50)", ""))).ToString("#,##0.00;(#,##0.00); ");


            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p51)", "")) ?
                   0 : dt.Compute("sum(p51)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p52)", "")) ?
                    0 : dt.Compute("sum(p52)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p53)", "")) ?
                    0 : dt.Compute("sum(p53)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p54)", "")) ?
                    0 : dt.Compute("sum(p54)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p55)", "")) ?
                    0 : dt.Compute("sum(p55)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p56)", "")) ?
                   0 : dt.Compute("sum(p56)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p57)", "")) ?
                   0 : dt.Compute("sum(p57)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p58)", "")) ?
               0 : dt.Compute("sum(p58)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p59)", "")) ?
             0 : dt.Compute("sum(p59)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p60)", "")) ?
               0 : dt.Compute("sum(p60)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj61")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p61)", "")) ?
                   0 : dt.Compute("sum(p61)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj62")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p62)", "")) ?
                    0 : dt.Compute("sum(p62)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj63")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p63)", "")) ?
                    0 : dt.Compute("sum(p63)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj64")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p64)", "")) ?
                    0 : dt.Compute("sum(p64)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj65")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p65)", "")) ?
                    0 : dt.Compute("sum(p65)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj66")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p66)", "")) ?
                   0 : dt.Compute("sum(p66)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lblFprj67")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p67)", "")) ?
                   0 : dt.Compute("sum(p67)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj68")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p68)", "")) ?
               0 : dt.Compute("sum(p68)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj69")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p69)", "")) ?
             0 : dt.Compute("sum(p69)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj70")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p70)", "")) ?
               0 : dt.Compute("sum(p70)", ""))).ToString("#,##0.00;(#,##0.00); ");


            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj71")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p71)", "")) ?
                   0 : dt.Compute("sum(p71)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj72")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p72)", "")) ?
                    0 : dt.Compute("sum(p72)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj73")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p73)", "")) ?
                    0 : dt.Compute("sum(p73)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj74")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p74)", "")) ?
                    0 : dt.Compute("sum(p74)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj75")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p75)", "")) ?
                    0 : dt.Compute("sum(p75)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj76")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p76)", "")) ?
                   0 : dt.Compute("sum(p76)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj77")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p77)", "")) ?
                   0 : dt.Compute("sum(p77)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj78")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p78)", "")) ?
               0 : dt.Compute("sum(p78)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj79")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p79)", "")) ?
             0 : dt.Compute("sum(p79)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj80")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p80)", "")) ?
               0 : dt.Compute("sum(p80)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj81")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p81)", "")) ?
                   0 : dt.Compute("sum(p81)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj82")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p82)", "")) ?
                    0 : dt.Compute("sum(p82)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj83")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p83)", "")) ?
                    0 : dt.Compute("sum(p83)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj84")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p84)", "")) ?
                    0 : dt.Compute("sum(p84)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj85")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p85)", "")) ?
                    0 : dt.Compute("sum(p85)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj86")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p86)", "")) ?
                   0 : dt.Compute("sum(p86)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lblFprj87")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p87)", "")) ?
                   0 : dt.Compute("sum(p87)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj88")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p88)", "")) ?
               0 : dt.Compute("sum(p88)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj89")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p89)", "")) ?
             0 : dt.Compute("sum(p89)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj90")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p90)", "")) ?
               0 : dt.Compute("sum(p90)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj91")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p91)", "")) ?
                   0 : dt.Compute("sum(p91)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFprj92")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p92)", "")) ?
                    0 : dt.Compute("sum(p92)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj93")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p93)", "")) ?
                    0 : dt.Compute("sum(p93)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj94")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p94)", "")) ?
                    0 : dt.Compute("sum(p94)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj95")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p95)", "")) ?
                    0 : dt.Compute("sum(p95)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj96")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p96)", "")) ?
                   0 : dt.Compute("sum(p96)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj97")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p97)", "")) ?
                   0 : dt.Compute("sum(p97)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj98")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p98)", "")) ?
               0 : dt.Compute("sum(p98)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj99")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p99)", "")) ?
             0 : dt.Compute("sum(p99)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPrj100")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p100)", "")) ?
               0 : dt.Compute("sum(p100)", ""))).ToString("#,##0.00;(#,##0.00); ");










        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");


            string title = "Suppier Wise Project Collection";

            DataTable dt1 = (DataTable)Session["tblaitvatsd"];
            DataTable dt = (DataTable)Session["tblpact"];

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptAitTaxVatProjectWise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptAitVatProjWise", lst, null, null);

            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            string date = "Date: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            int j = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                Rpt1.SetParameters(new ReportParameter("p" + j.ToString(), dt.Rows[i]["pactdesc"].ToString()));
                j++;

            }

            for (; j <= 22; j++)
            {
                Rpt1.SetParameters(new ReportParameter("p" + j.ToString(), ""));

            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            // ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptATIVATPrjColl();
            // TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            // rptCname.Text = comnam;
            // TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            // rptftdate.Text = "Date: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            // int j = 1;
            // DataTable dt1 = (DataTable)Session["tblaitvatsd"];
            // DataTable dt = (DataTable)Session["tblpact"];

            // for (int i = 0; i <dt.Rows.Count; i++)
            //// for (int i = 2; i < this.gvaitvsd.Columns.Count; i++)
            // {


            //     TextObject rpttxth = rptstate.ReportDefinition.ReportObjects["textproject" + j.ToString()] as TextObject;
            //     rpttxth.Text = dt.Rows[i]["pactdesc"].ToString();
            //    // rpttxth.Text = this.gvaitvsd.Columns[i].HeaderText.ToString();
            //     j++;


            //    //// rpttxth.Text = dt.Rows[i]["pactdesc"].ToString();
            //    // rpttxth.Text = this.gvaitvsd.Columns[i].HeaderText.ToString();
            //    // j++;

            // }



            // TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            // rptstate.SetDataSource(dt1);
            // Session["Report1"] = rptstate;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}