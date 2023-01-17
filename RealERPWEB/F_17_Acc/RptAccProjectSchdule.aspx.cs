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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccProjectSchdule : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, bgdpercent = 0.00, bgdexepercent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Schedule Summary";

                //this.Master.Page.Title = "Project Schedule Summary";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void GetProjectName()
        {
            DropCheck1.Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "pactdesc";
            this.DropCheck1.DataValueField = "pactcode";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();

        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowValue();
        }

        private void ShowValue()
        {
            this.lbljavascript.Text = "";
            this.ShowConProgress();
        }



        private void ShowConProgress()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = "";
            string[] procode = this.DropCheck1.Text.Trim().Split(',');
            if (procode.Length > 0)
            {
                if (procode[0].ToString() == "000000000000" || procode[0].ToString().Trim() == "")
                    pactcode = "";
                else
                    foreach (string s1 in procode)
                        pactcode = pactcode + s1;

            }

            string frmdate = this.txtfrmdate.Text;
            string toDate = this.txttodate.Text;
            string withopening = this.chkboxwithOpening.Checked ? "withopening" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPROSUMMARY", pactcode, frmdate, toDate, withopening, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvproschdule.DataSource = null;
                this.gvproschdule.DataBind();
                return;
            }

            Session["tblConPro"] = ds1.Tables[0];
            ViewState["tblproname"] = ds1.Tables[1];
            this.Data_Bind();


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Floor Wise Construction Progress";
                string eventdesc = "Show Report";
                string eventdesc2 = this.DropCheck1.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblConPro"];
            DataTable dt1 = (DataTable)Session["tblConPro"];

            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34,
                p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67,
                p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;



            DataView dv = new DataView(dt);
            dv.RowFilter = "rescode<>'" + 230000000000 + "'";
            dt = dv.ToTable();

            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(topamt)", "")) ? 0.00 : dt.Compute("sum(topamt)", "")));
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



            this.gvproschdule.Columns[2].Visible = (p1 != 0);
            this.gvproschdule.Columns[3].Visible = (p2 != 0);
            this.gvproschdule.Columns[4].Visible = (p3 != 0);
            this.gvproschdule.Columns[5].Visible = (p4 != 0);
            this.gvproschdule.Columns[6].Visible = (p5 != 0);
            this.gvproschdule.Columns[7].Visible = (p6 != 0);
            this.gvproschdule.Columns[8].Visible = (p7 != 0);
            this.gvproschdule.Columns[9].Visible = (p8 != 0);
            this.gvproschdule.Columns[10].Visible = (p9 != 0);
            this.gvproschdule.Columns[11].Visible = (p10 != 0);
            this.gvproschdule.Columns[12].Visible = (p11 != 0);
            this.gvproschdule.Columns[13].Visible = (p12 != 0);
            this.gvproschdule.Columns[14].Visible = (p13 != 0);
            this.gvproschdule.Columns[15].Visible = (p14 != 0);
            this.gvproschdule.Columns[16].Visible = (p15 != 0);
            this.gvproschdule.Columns[17].Visible = (p16 != 0);
            this.gvproschdule.Columns[18].Visible = (p17 != 0);
            this.gvproschdule.Columns[19].Visible = (p18 != 0);
            this.gvproschdule.Columns[20].Visible = (p19 != 0);
            this.gvproschdule.Columns[21].Visible = (p20 != 0);
            this.gvproschdule.Columns[22].Visible = (p21 != 0);
            this.gvproschdule.Columns[23].Visible = (p22 != 0);
            this.gvproschdule.Columns[24].Visible = (p23 != 0);
            this.gvproschdule.Columns[25].Visible = (p24 != 0);
            this.gvproschdule.Columns[26].Visible = (p25 != 0);
            this.gvproschdule.Columns[27].Visible = (p26 != 0);
            this.gvproschdule.Columns[28].Visible = (p27 != 0);
            this.gvproschdule.Columns[29].Visible = (p28 != 0);
            this.gvproschdule.Columns[30].Visible = (p29 != 0);
            this.gvproschdule.Columns[31].Visible = (p30 != 0);
            this.gvproschdule.Columns[32].Visible = (p31 != 0);
            this.gvproschdule.Columns[33].Visible = (p32 != 0);
            this.gvproschdule.Columns[34].Visible = (p33 != 0);
            this.gvproschdule.Columns[35].Visible = (p34 != 0);
            this.gvproschdule.Columns[36].Visible = (p35 != 0);
            this.gvproschdule.Columns[37].Visible = (p36 != 0);
            this.gvproschdule.Columns[38].Visible = (p37 != 0);
            this.gvproschdule.Columns[39].Visible = (p38 != 0);
            this.gvproschdule.Columns[40].Visible = (p39 != 0);
            this.gvproschdule.Columns[41].Visible = (p40 != 0);
            this.gvproschdule.Columns[42].Visible = (p41 != 0);
            this.gvproschdule.Columns[43].Visible = (p42 != 0);
            this.gvproschdule.Columns[44].Visible = (p43 != 0);
            this.gvproschdule.Columns[45].Visible = (p44 != 0);
            this.gvproschdule.Columns[46].Visible = (p45 != 0);
            this.gvproschdule.Columns[47].Visible = (p46 != 0);
            this.gvproschdule.Columns[48].Visible = (p47 != 0);
            this.gvproschdule.Columns[49].Visible = (p48 != 0);
            this.gvproschdule.Columns[50].Visible = (p49 != 0);
            this.gvproschdule.Columns[51].Visible = (p50 != 0);
            this.gvproschdule.Columns[52].Visible = (p51 != 0);
            this.gvproschdule.Columns[53].Visible = (p52 != 0);
            this.gvproschdule.Columns[54].Visible = (p53 != 0);
            this.gvproschdule.Columns[55].Visible = (p54 != 0);
            this.gvproschdule.Columns[56].Visible = (p55 != 0);
            this.gvproschdule.Columns[57].Visible = (p56 != 0);
            this.gvproschdule.Columns[58].Visible = (p57 != 0);
            this.gvproschdule.Columns[59].Visible = (p58 != 0);
            this.gvproschdule.Columns[60].Visible = (p59 != 0);
            this.gvproschdule.Columns[61].Visible = (p60 != 0);
            this.gvproschdule.Columns[62].Visible = (p61 != 0);
            this.gvproschdule.Columns[63].Visible = (p62 != 0);
            this.gvproschdule.Columns[64].Visible = (p63 != 0);
            this.gvproschdule.Columns[65].Visible = (p64 != 0);
            this.gvproschdule.Columns[66].Visible = (p65 != 0);
            this.gvproschdule.Columns[67].Visible = (p66 != 0);
            this.gvproschdule.Columns[68].Visible = (p67 != 0);
            this.gvproschdule.Columns[69].Visible = (p68 != 0);
            this.gvproschdule.Columns[70].Visible = (p69 != 0);
            this.gvproschdule.Columns[71].Visible = (p70 != 0);
            this.gvproschdule.Columns[72].Visible = (p71 != 0);
            this.gvproschdule.Columns[73].Visible = (p72 != 0);
            this.gvproschdule.Columns[74].Visible = (p73 != 0);
            this.gvproschdule.Columns[75].Visible = (p74 != 0);
            this.gvproschdule.Columns[76].Visible = (p75 != 0);
            this.gvproschdule.Columns[77].Visible = (p76 != 0);
            this.gvproschdule.Columns[78].Visible = (p77 != 0);
            this.gvproschdule.Columns[79].Visible = (p78 != 0);
            this.gvproschdule.Columns[80].Visible = (p79 != 0);
            this.gvproschdule.Columns[81].Visible = (p80 != 0);
            this.gvproschdule.Columns[82].Visible = (p81 != 0);
            this.gvproschdule.Columns[83].Visible = (p82 != 0);
            this.gvproschdule.Columns[84].Visible = (p83 != 0);
            this.gvproschdule.Columns[85].Visible = (p84 != 0);
            this.gvproschdule.Columns[86].Visible = (p85 != 0);
            this.gvproschdule.Columns[87].Visible = (p86 != 0);
            this.gvproschdule.Columns[88].Visible = (p87 != 0);
            this.gvproschdule.Columns[89].Visible = (p88 != 0);
            this.gvproschdule.Columns[90].Visible = (p89 != 0);
            this.gvproschdule.Columns[91].Visible = (p90 != 0);
            this.gvproschdule.Columns[92].Visible = (p91 != 0);
            this.gvproschdule.Columns[93].Visible = (p92 != 0);
            this.gvproschdule.Columns[94].Visible = (p93 != 0);
            this.gvproschdule.Columns[95].Visible = (p94 != 0);
            this.gvproschdule.Columns[96].Visible = (p95 != 0);
            this.gvproschdule.Columns[97].Visible = (p96 != 0);
            this.gvproschdule.Columns[98].Visible = (p97 != 0);
            this.gvproschdule.Columns[99].Visible = (p98 != 0);
            this.gvproschdule.Columns[100].Visible = (p99 != 0);
            this.gvproschdule.Columns[101].Visible = (p100 != 0);

            int j = 2;


            DataTable dtpname = (DataTable)ViewState["tblproname"];
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvproschdule.Columns[j].HeaderText = dtpname.Rows[i]["pactdesc"].ToString();
                j++;
                if (j == 102)
                    break;


            }


            this.gvproschdule.DataSource = dt1;
            this.gvproschdule.DataBind();


            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFtoCost")).Text = toamt.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC1")).Text = p1.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC2")).Text = p2.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC3")).Text = p3.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC4")).Text = p4.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC5")).Text = p5.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC6")).Text = p6.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC7")).Text = p7.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC8")).Text = p8.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC9")).Text = p9.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC10")).Text = p10.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC11")).Text = p11.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC12")).Text = p12.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC13")).Text = p13.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC14")).Text = p14.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC15")).Text = p15.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC16")).Text = p16.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC17")).Text = p17.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC18")).Text = p18.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC19")).Text = p19.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC20")).Text = p20.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC21")).Text = p21.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC22")).Text = p22.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC23")).Text = p23.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC24")).Text = p24.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC25")).Text = p25.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC26")).Text = p26.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC27")).Text = p27.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC28")).Text = p28.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC29")).Text = p29.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC30")).Text = p30.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC31")).Text = p31.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC32")).Text = p32.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC33")).Text = p33.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC34")).Text = p34.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC35")).Text = p35.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC36")).Text = p36.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC37")).Text = p37.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC38")).Text = p38.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC39")).Text = p39.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC40")).Text = p40.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC41")).Text = p41.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC42")).Text = p42.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC43")).Text = p43.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC44")).Text = p44.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC45")).Text = p45.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC46")).Text = p46.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC47")).Text = p47.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC48")).Text = p48.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC49")).Text = p49.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvproschdule.FooterRow.FindControl("lgvFPC50")).Text = p50.ToString("#,##0.00;(#,##0.00); ");
            Session["Report1"] = gvproschdule;
            ((HyperLink)this.gvproschdule.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            ;
        }

        private void FooterCalcul(DataTable dt)
        {


            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = hst["comcod"].ToString();
            // if (dt.Rows.Count == 0)
            //     return;


            //     double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ?0 : dt.Compute("sum(bgdamt)", "")));
            //     double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            //     double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            //     percent =(mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            //     bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            //     bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));
            //     ((Label)this.gvConPro.FooterRow.FindControl("lgvFBgdAmt")).Text = bgdamt.ToString("#,##0.00;(#,##0.00); ");
            //     ((Label)this.gvConPro.FooterRow.FindControl("lgvFMasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplan)", "")) ?
            //                         0 : dt.Compute("sum(mplan)", ""))).ToString("#,##0.00;(#,##0.00); ");            
            //     ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = examt.ToString("#,##0.00;(#,##0.00); ");
            //     ((Label)this.gvConPro.FooterRow.FindControl("lgvFMPlanastoday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ?
            //                         0 : dt.Compute("sum(mplanat)", ""))).ToString("#,##0.00;(#,##0.00); ");

            // ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ?
            //                     0 : dt.Compute("sum(eamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            // ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(leamt)", "")) ?
            //              0 : dt.Compute("sum(leamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            // ((Label)this.gvConPro.FooterRow.FindControl("lgvFWorkP")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(perwork)", "")) ?
            //                     0 : dt.Compute("sum(perwork)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";
            // ((Label)this.gvConPro.FooterRow.FindControl("lgvFPercent")).Text = percent.ToString("#,##0.00;(#,##0.00); ") + "%";



            // string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //// string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13) ;
            // string frmdate = Convert.ToDateTime("01" + this.txtCurDate.Text.Substring(2)).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime( this.txtCurDate.Text).ToString("dd-MMM-yyyy");


            // ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ImpPlan02&comcod="+comcod+"&Pactcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + todate;


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintBgsdVsExe();

        }

        private void PrintBgsdVsExe()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string asdate = "( From " + this.txtfrmdate.Text + " To " + this.txttodate.Text + ")";
            DataTable dt = (DataTable)Session["tblConPro"];
            DataTable dtv2 = (DataTable)Session["tblConPro"];
            DataTable dt1 = (DataTable)ViewState["tblproname"];


            //Added
            DataView dv = new DataView(dt);
            dv.RowFilter = "rescode<>'" + 230000000000 + "'";
            dt = dv.ToTable();


            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34,
                p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67,
                p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;


            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(topamt)", "")) ? 0.00 : dt.Compute("sum(topamt)", "")));
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






            //End





            var list = dtv2.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.FlrWiseConPrg>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptFlrWiseConPrg", list, null, null);

            int j = 1;
            foreach (DataRow dr1 in dt1.Rows)
            {
                //Rpt1.SetParameters (new ReportParameter ("txtp'" + j.ToString () + "'", (dr1["pactdesc"]).ToString ()));

                string pname = "txtp" + j.ToString();
                Rpt1.SetParameters(new ReportParameter(pname, (dr1["pactdesc"]).ToString()));
                if (j == 10)
                    break;
                j++;

            }


            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            //Rpt1.SetParameters (new ReportParameter ("paymentid", paymentid));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("asdate", asdate));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            //Added
            Rpt1.SetParameters(new ReportParameter("toamt", toamt.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p1", p1.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p2", p2.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p3", p3.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p4", p4.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p5", p5.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p6", p6.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p7", p7.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p8", p8.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p9", p9.ToString("#,##0.00;(#,##0.00;) ")));
            Rpt1.SetParameters(new ReportParameter("p10", p10.ToString("#,##0.00;(#,##0.00;) ")));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Session["Report1"] = rptConPro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




        protected void gvConPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    if (e.Row.RowType != DataControlRowType.DataRow)
            //        return;

            //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            //    string mPACTCODE = this.ddlProjectName.SelectedValue.ToString();
            //    string mFlrCode = ((Label)e.Row.FindControl("lblgvflrCode")).Text;
            //    string mTRNDAT1 = this.txtCurDate.Text;

            //    hlink1.NavigateUrl = "~/F_32_Mis/RptLinkImpExeStatus.aspx?Type=BgdAll&comcod=" + comcod + "&pactcode=" + mPACTCODE + "&FlrCode=" + mFlrCode + "&Date1=" + mTRNDAT1;

        }

        protected void gvproschdule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActDescCost = (Label)e.Row.FindControl("lgvActDescCost");
                Label lgvtopamtcost = (Label)e.Row.FindControl("lgvtopamtcost");
                Label lgvpc1 = (Label)e.Row.FindControl("lgvpc1");
                Label lgvpc2 = (Label)e.Row.FindControl("lgvpc2");
                Label lgvpc3 = (Label)e.Row.FindControl("lgvpc3");
                Label lgvpc4 = (Label)e.Row.FindControl("lgvpc4");
                Label lgvpc5 = (Label)e.Row.FindControl("lgvpc5");
                Label lgvpc6 = (Label)e.Row.FindControl("lgvpc6");
                Label lgvpc7 = (Label)e.Row.FindControl("lgvpc7");
                Label lgvpc8 = (Label)e.Row.FindControl("lgvpc8");
                Label lgvpc9 = (Label)e.Row.FindControl("lgvpc9");
                Label lgvpc10 = (Label)e.Row.FindControl("lgvpc10");
                Label lgvpc11 = (Label)e.Row.FindControl("lgvpc11");
                Label lgvpc12 = (Label)e.Row.FindControl("lgvpc12");
                Label lgvpc13 = (Label)e.Row.FindControl("lgvpc13");
                Label lgvpc14 = (Label)e.Row.FindControl("lgvpc14");
                Label lgvpc15 = (Label)e.Row.FindControl("lgvpc15");
                Label lgvpc16 = (Label)e.Row.FindControl("lgvpc16");
                Label lgvpc17 = (Label)e.Row.FindControl("lgvpc17");
                Label lgvpc18 = (Label)e.Row.FindControl("lgvpc18");
                Label lgvpc19 = (Label)e.Row.FindControl("lgvpc19");
                Label lgvpc20 = (Label)e.Row.FindControl("lgvpc20");
                Label lgvpc21 = (Label)e.Row.FindControl("lgvpc21");
                Label lgvpc22 = (Label)e.Row.FindControl("lgvpc22");
                Label lgvpc23 = (Label)e.Row.FindControl("lgvpc23");
                Label lgvpc24 = (Label)e.Row.FindControl("lgvpc24");
                Label lgvpc25 = (Label)e.Row.FindControl("lgvpc25");
                Label lgvpc26 = (Label)e.Row.FindControl("lgvpc26");
                Label lgvpc27 = (Label)e.Row.FindControl("lgvpc27");
                Label lgvpc28 = (Label)e.Row.FindControl("lgvpc28");
                Label lgvpc29 = (Label)e.Row.FindControl("lgvpc29");
                Label lgvpc30 = (Label)e.Row.FindControl("lgvpc30");
                Label lgvpc31 = (Label)e.Row.FindControl("lgvpc31");
                Label lgvpc32 = (Label)e.Row.FindControl("lgvpc32");
                Label lgvpc33 = (Label)e.Row.FindControl("lgvpc33");
                Label lgvpc34 = (Label)e.Row.FindControl("lgvpc34");
                Label lgvpc35 = (Label)e.Row.FindControl("lgvpc35");
                Label lgvpc36 = (Label)e.Row.FindControl("lgvpc36");
                Label lgvpc37 = (Label)e.Row.FindControl("lgvpc37");
                Label lgvpc38 = (Label)e.Row.FindControl("lgvpc38");
                Label lgvpc39 = (Label)e.Row.FindControl("lgvpc39");
                Label lgvpc40 = (Label)e.Row.FindControl("lgvpc40");
                Label lgvpc41 = (Label)e.Row.FindControl("lgvpc41");
                Label lgvpc42 = (Label)e.Row.FindControl("lgvpc42");
                Label lgvpc43 = (Label)e.Row.FindControl("lgvpc43");
                Label lgvpc44 = (Label)e.Row.FindControl("lgvpc44");
                Label lgvpc45 = (Label)e.Row.FindControl("lgvpc45");
                Label lgvpc46 = (Label)e.Row.FindControl("lgvpc46");
                Label lgvpc47 = (Label)e.Row.FindControl("lgvpc47");
                Label lgvpc48 = (Label)e.Row.FindControl("lgvpc48");
                Label lgvpc49 = (Label)e.Row.FindControl("lgvpc49");
                Label lgvpc50 = (Label)e.Row.FindControl("lgvpc50");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "140000000000" || code == "150000000000" || code == "170000000000" || code == "190000000000" || code == "200000000000" || code == "210000000000" || code == "220000000000" || code == "230000000000")
                {
                    lgvActDescCost.Attributes["style"] = "font-size:14px; font-weight:bold; color:maroon;";
                    lgvActDescCost.Style.Add("text-align", "left");
                }
                //if (code == "230000000000")
                //{

                //}

                if (code == "240000000000" || code == "250000000000" || code == "260000000000" || code == "270000000000")
                {
                    lgvActDescCost.Attributes["style"] = "font-size:12px; font-weight:bold; color:blue;";
                    lgvActDescCost.Style.Add("padding-left", "20px");
                    lgvtopamtcost.Attributes["style"] = "font-size:12px; font-weight:bold; color:blue;";
                    lgvpc1.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc2.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc3.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc4.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc5.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc6.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc7.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc8.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc9.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc10.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc11.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc12.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc13.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc14.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc15.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc16.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc17.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc18.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc19.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc20.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc21.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc22.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc23.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc24.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc25.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc26.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc27.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc28.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc29.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc30.Attributes["style"] = "font-weight:bold; color:blue;";

                    lgvpc31.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc32.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc33.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc34.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc35.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc36.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc37.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc38.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc39.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc40.Attributes["style"] = "font-weight:bold; color:blue;";

                    lgvpc41.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc42.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc43.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc44.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc45.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc46.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc47.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc48.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc49.Attributes["style"] = "font-weight:bold; color:blue;";
                    lgvpc50.Attributes["style"] = "font-weight:bold; color:blue;";

                }
                else
                {
                    lgvActDescCost.Attributes["style"] = "font-size:14px; font-weight:bold; color:green;";
                    lgvActDescCost.Style.Add("text-align", "left");
                    lgvtopamtcost.Attributes["style"] = "font-size:14px; font-weight:bold; color:green;";
                    lgvpc1.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc2.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc3.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc4.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc5.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc6.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc7.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc8.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc9.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc10.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc11.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc12.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc13.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc14.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc15.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc16.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc17.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc18.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc19.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc20.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc21.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc22.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc23.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc24.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc25.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc26.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc27.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc28.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc29.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";

                    lgvpc30.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc31.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc32.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc33.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc34.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc35.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc36.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc37.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc38.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc39.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc40.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";

                    lgvpc41.Attributes["style"] = "font-weight:bold; font-size:15px; color:green;";
                    lgvpc42.Attributes["style"] = "font-weight:bold; font-size:15px; color:green;";
                    lgvpc43.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc44.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc45.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc46.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc47.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc48.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc49.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                    lgvpc50.Attributes["style"] = "font-size:15px; font-weight:bold; color:green;";
                }
            }

        }
    }
}

