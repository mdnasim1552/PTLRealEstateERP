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
namespace RealERPWEB.F_32_Mis
{
    public partial class RptMisMasterBgd03 : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();
        AutoCompleted Data = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("tblmasterbgd");
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "AllCost") ? "Cost-All Project"

                //    : "COST OF FUND OF PROJECTS";
                this.SectionView();
                // this.pnlMenu.Visible = true;


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];
            //string str =ASTUtility.Left((hst["comcod"].ToString()),1);
            return (hst["comcod"].ToString());
        }
        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "AllCost":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;




            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                // string eventtype = this.lblHtitle.Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "AllCost":
                    this.ShowAllCost();
                    break;




            }
        }


        private void ShowAllCost()
        {
            Session.Remove("tblmasterbgd");
            string comcod = this.GetCompCode();

            string txtdatefrm = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string level = this.ddlReportLevelCom.SelectedValue.ToString();

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTCOSTALLACCHEAD", txtdatefrm, txtdateto, level, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvComCost.DataSource = null;
                this.gvComCost.DataBind();
                return;
            }


            Session["tblmasterbgd"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tblproname"] = ds1.Tables[1];
            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            string grp = dt1.Rows[0]["grp"].ToString();
            string mrescode = dt1.Rows[0]["mrescode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["mrescode"].ToString() == mrescode)
                {

                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["mresdesc"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                        dt1.Rows[j]["grpdesc"] = "";

                    if (dt1.Rows[j]["mrescode"].ToString() == mrescode)
                        dt1.Rows[j]["mresdesc"] = "";



                }


                grp = dt1.Rows[j]["grp"].ToString();
                mrescode = dt1.Rows[j]["mrescode"].ToString();
            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt, dt1; DataView dv;
            dt1 = ((DataTable)Session["tblmasterbgd"]).Copy();
            dv = ((DataTable)Session["tblmasterbgd"]).DefaultView;

            int i, j;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34,
                p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67,
                p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;


            switch (Type)
            {

                case "AllCost":
                    dv.RowFilter = ("grp= 'AA'");
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



                    this.gvComCost.Columns[4].Visible = (p1 != 0);
                    this.gvComCost.Columns[5].Visible = (p2 != 0);
                    this.gvComCost.Columns[6].Visible = (p3 != 0);
                    this.gvComCost.Columns[7].Visible = (p4 != 0);
                    this.gvComCost.Columns[8].Visible = (p5 != 0);
                    this.gvComCost.Columns[9].Visible = (p6 != 0);
                    this.gvComCost.Columns[10].Visible = (p7 != 0);
                    this.gvComCost.Columns[11].Visible = (p8 != 0);
                    this.gvComCost.Columns[12].Visible = (p9 != 0);
                    this.gvComCost.Columns[13].Visible = (p10 != 0);
                    this.gvComCost.Columns[14].Visible = (p11 != 0);
                    this.gvComCost.Columns[15].Visible = (p12 != 0);
                    this.gvComCost.Columns[16].Visible = (p13 != 0);
                    this.gvComCost.Columns[17].Visible = (p14 != 0);
                    this.gvComCost.Columns[18].Visible = (p15 != 0);
                    this.gvComCost.Columns[19].Visible = (p16 != 0);
                    this.gvComCost.Columns[20].Visible = (p17 != 0);
                    this.gvComCost.Columns[21].Visible = (p18 != 0);
                    this.gvComCost.Columns[22].Visible = (p19 != 0);
                    this.gvComCost.Columns[23].Visible = (p20 != 0);
                    this.gvComCost.Columns[24].Visible = (p21 != 0);
                    this.gvComCost.Columns[25].Visible = (p22 != 0);
                    this.gvComCost.Columns[26].Visible = (p23 != 0);
                    this.gvComCost.Columns[27].Visible = (p24 != 0);
                    this.gvComCost.Columns[28].Visible = (p25 != 0);
                    this.gvComCost.Columns[29].Visible = (p26 != 0);
                    this.gvComCost.Columns[30].Visible = (p27 != 0);
                    this.gvComCost.Columns[31].Visible = (p28 != 0);
                    this.gvComCost.Columns[32].Visible = (p29 != 0);
                    this.gvComCost.Columns[33].Visible = (p30 != 0);
                    this.gvComCost.Columns[34].Visible = (p31 != 0);
                    this.gvComCost.Columns[35].Visible = (p32 != 0);
                    this.gvComCost.Columns[36].Visible = (p33 != 0);
                    this.gvComCost.Columns[37].Visible = (p34 != 0);
                    this.gvComCost.Columns[38].Visible = (p35 != 0);
                    this.gvComCost.Columns[39].Visible = (p36 != 0);
                    this.gvComCost.Columns[40].Visible = (p37 != 0);
                    this.gvComCost.Columns[41].Visible = (p38 != 0);
                    this.gvComCost.Columns[42].Visible = (p39 != 0);
                    this.gvComCost.Columns[43].Visible = (p40 != 0);
                    this.gvComCost.Columns[44].Visible = (p41 != 0);
                    this.gvComCost.Columns[45].Visible = (p42 != 0);
                    this.gvComCost.Columns[46].Visible = (p43 != 0);
                    this.gvComCost.Columns[47].Visible = (p44 != 0);
                    this.gvComCost.Columns[48].Visible = (p45 != 0);
                    this.gvComCost.Columns[49].Visible = (p46 != 0);
                    this.gvComCost.Columns[50].Visible = (p47 != 0);

                    this.gvComCost.Columns[51].Visible = (p48 != 0);
                    this.gvComCost.Columns[52].Visible = (p49 != 0);
                    this.gvComCost.Columns[53].Visible = (p50 != 0);
                    this.gvComCost.Columns[54].Visible = (p51 != 0);
                    this.gvComCost.Columns[55].Visible = (p52 != 0);
                    this.gvComCost.Columns[56].Visible = (p53 != 0);
                    this.gvComCost.Columns[57].Visible = (p54 != 0);
                    this.gvComCost.Columns[58].Visible = (p55 != 0);
                    this.gvComCost.Columns[59].Visible = (p56 != 0);
                    this.gvComCost.Columns[60].Visible = (p57 != 0);
                    this.gvComCost.Columns[61].Visible = (p58 != 0);
                    this.gvComCost.Columns[62].Visible = (p59 != 0);
                    this.gvComCost.Columns[63].Visible = (p60 != 0);
                    this.gvComCost.Columns[64].Visible = (p61 != 0);
                    this.gvComCost.Columns[65].Visible = (p62 != 0);
                    this.gvComCost.Columns[66].Visible = (p63 != 0);
                    this.gvComCost.Columns[67].Visible = (p64 != 0);
                    this.gvComCost.Columns[68].Visible = (p65 != 0);
                    this.gvComCost.Columns[69].Visible = (p66 != 0);
                    this.gvComCost.Columns[70].Visible = (p67 != 0);
                    this.gvComCost.Columns[71].Visible = (p68 != 0);
                    this.gvComCost.Columns[72].Visible = (p69 != 0);
                    this.gvComCost.Columns[73].Visible = (p70 != 0);
                    this.gvComCost.Columns[74].Visible = (p71 != 0);
                    this.gvComCost.Columns[75].Visible = (p72 != 0);
                    this.gvComCost.Columns[76].Visible = (p73 != 0);
                    this.gvComCost.Columns[77].Visible = (p74 != 0);
                    this.gvComCost.Columns[78].Visible = (p75 != 0);
                    this.gvComCost.Columns[79].Visible = (p76 != 0);
                    this.gvComCost.Columns[80].Visible = (p77 != 0);
                    this.gvComCost.Columns[81].Visible = (p78 != 0);
                    this.gvComCost.Columns[82].Visible = (p79 != 0);
                    this.gvComCost.Columns[83].Visible = (p80 != 0);
                    this.gvComCost.Columns[84].Visible = (p81 != 0);
                    this.gvComCost.Columns[85].Visible = (p82 != 0);
                    this.gvComCost.Columns[86].Visible = (p83 != 0);
                    this.gvComCost.Columns[87].Visible = (p84 != 0);
                    this.gvComCost.Columns[88].Visible = (p85 != 0);
                    this.gvComCost.Columns[89].Visible = (p86 != 0);
                    this.gvComCost.Columns[90].Visible = (p87 != 0);
                    this.gvComCost.Columns[91].Visible = (p88 != 0);
                    this.gvComCost.Columns[92].Visible = (p89 != 0);
                    this.gvComCost.Columns[93].Visible = (p90 != 0);
                    this.gvComCost.Columns[94].Visible = (p91 != 0);
                    this.gvComCost.Columns[95].Visible = (p92 != 0);
                    this.gvComCost.Columns[96].Visible = (p93 != 0);
                    this.gvComCost.Columns[97].Visible = (p94 != 0);
                    this.gvComCost.Columns[98].Visible = (p95 != 0);
                    this.gvComCost.Columns[99].Visible = (p96 != 0);
                    this.gvComCost.Columns[100].Visible = (p97 != 0);
                    this.gvComCost.Columns[101].Visible = (p98 != 0);
                    this.gvComCost.Columns[102].Visible = (p99 != 0);
                    this.gvComCost.Columns[103].Visible = (p100 != 0);
                    j = 4;


                    DataTable dtpname = (DataTable)ViewState["tblproname"];
                    for (i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvComCost.Columns[j].HeaderText = dtpname.Rows[i]["pactdesc"].ToString();
                        j++;
                        if (j == 104)
                            break;


                    }

                    this.gvComCost.DataSource = dt1;
                    this.gvComCost.DataBind();


                    if (dt1.Rows.Count == 0)
                        return;

                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFtoCost")).Text = toamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC1")).Text = p1.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC2")).Text = p2.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC3")).Text = p3.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC4")).Text = p4.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC5")).Text = p5.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC6")).Text = p6.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC7")).Text = p7.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC8")).Text = p8.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC9")).Text = p9.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC10")).Text = p10.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC11")).Text = p11.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC12")).Text = p12.ToString("#,##0;(#,##0); ");

                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC13")).Text = p13.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC14")).Text = p14.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC15")).Text = p15.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC16")).Text = p16.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC17")).Text = p17.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC18")).Text = p18.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC19")).Text = p19.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC20")).Text = p20.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC21")).Text = p21.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC22")).Text = p22.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC23")).Text = p23.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC24")).Text = p24.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC25")).Text = p25.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC26")).Text = p26.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC27")).Text = p27.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC28")).Text = p28.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC29")).Text = p29.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC30")).Text = p30.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC31")).Text = p31.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC32")).Text = p32.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC33")).Text = p33.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC34")).Text = p34.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC35")).Text = p35.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC36")).Text = p36.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC37")).Text = p37.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC38")).Text = p38.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC39")).Text = p39.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC40")).Text = p40.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC41")).Text = p41.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC42")).Text = p42.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC43")).Text = p43.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC44")).Text = p44.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC45")).Text = p45.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC46")).Text = p46.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC47")).Text = p47.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC48")).Text = p48.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC49")).Text = p49.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC50")).Text = p50.ToString("#,##0;(#,##0); ");
                    Session["Report1"] = gvComCost;
                    ((HyperLink)this.gvComCost.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;



            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "AllCost":
                    this.PrintAllCost();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                //string eventtype = this.lblHtitle.Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                //  bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }


        private void PrintAllCost()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblmasterbgd"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptCostAccHead();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //int j = 1;


            //DataTable dtpname = (DataTable)ViewState["tblproname"];
            //for (int i = 0; i < dtpname.Rows.Count; i++)
            //{

            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
            //    rpttxth.Text = dtpname.Rows[i]["pactdesc"].ToString();
            //    j++;
            //    if (j == 13)
            //        break;
            //}

            //TextObject DateFrmTo = rptstk.ReportDefinition.ReportObjects["DateFrmTo"] as TextObject;
            //DateFrmTo.Text = "From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd.MM.yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd.MM.yyyy");

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO1.jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvComCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgvActDescCost");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtopamtcost");

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
                Label lgvpc51 = (Label)e.Row.FindControl("lgvpc51");
                Label lgvpc52 = (Label)e.Row.FindControl("lgvpc52");
                Label lgvpc53 = (Label)e.Row.FindControl("lgvpc53");
                Label lgvpc54 = (Label)e.Row.FindControl("lgvpc54");
                Label lgvpc55 = (Label)e.Row.FindControl("lgvpc55");
                Label lgvpc56 = (Label)e.Row.FindControl("lgvpc56");
                Label lgvpc57 = (Label)e.Row.FindControl("lgvpc57");
                Label lgvpc58 = (Label)e.Row.FindControl("lgvpc58");
                Label lgvpc59 = (Label)e.Row.FindControl("lgvpc59");
                Label lgvpc60 = (Label)e.Row.FindControl("lgvpc60");
                Label lgvpc61 = (Label)e.Row.FindControl("lgvpc61");
                Label lgvpc62 = (Label)e.Row.FindControl("lgvpc62");
                Label lgvpc63 = (Label)e.Row.FindControl("lgvpc63");
                Label lgvpc64 = (Label)e.Row.FindControl("lgvpc64");
                Label lgvpc65 = (Label)e.Row.FindControl("lgvpc65");
                Label lgvpc66 = (Label)e.Row.FindControl("lgvpc66");
                Label lgvpc67 = (Label)e.Row.FindControl("lgvpc67");
                Label lgvpc68 = (Label)e.Row.FindControl("lgvpc68");
                Label lgvpc69 = (Label)e.Row.FindControl("lgvpc69");
                Label lgvpc70 = (Label)e.Row.FindControl("lgvpc70");
                Label lgvpc71 = (Label)e.Row.FindControl("lgvpc71");
                Label lgvpc72 = (Label)e.Row.FindControl("lgvpc72");
                Label lgvpc73 = (Label)e.Row.FindControl("lgvpc73");
                Label lgvpc74 = (Label)e.Row.FindControl("lgvpc74");
                Label lgvpc75 = (Label)e.Row.FindControl("lgvpc75");
                Label lgvpc76 = (Label)e.Row.FindControl("lgvpc76");
                Label lgvpc77 = (Label)e.Row.FindControl("lgvpc77");
                Label lgvpc78 = (Label)e.Row.FindControl("lgvpc78");
                Label lgvpc79 = (Label)e.Row.FindControl("lgvpc79");
                Label lgvpc80 = (Label)e.Row.FindControl("lgvpc80");
                Label lgvpc81 = (Label)e.Row.FindControl("lgvpc81");
                Label lgvpc82 = (Label)e.Row.FindControl("lgvpc82");
                Label lgvpc83 = (Label)e.Row.FindControl("lgvpc83");
                Label lgvpc84 = (Label)e.Row.FindControl("lgvpc84");
                Label lgvpc85 = (Label)e.Row.FindControl("lgvpc85");
                Label lgvpc86 = (Label)e.Row.FindControl("lgvpc86");
                Label lgvpc87 = (Label)e.Row.FindControl("lgvpc87");
                Label lgvpc88 = (Label)e.Row.FindControl("lgvpc88");
                Label lgvpc89 = (Label)e.Row.FindControl("lgvpc89");
                Label lgvpc90 = (Label)e.Row.FindControl("lgvpc90");
                Label lgvpc91 = (Label)e.Row.FindControl("lgvpc91");
                Label lgvpc92 = (Label)e.Row.FindControl("lgvpc92");
                Label lgvpc93 = (Label)e.Row.FindControl("lgvpc93");
                Label lgvpc94 = (Label)e.Row.FindControl("lgvpc94");
                Label lgvpc95 = (Label)e.Row.FindControl("lgvpc95");
                Label lgvpc96 = (Label)e.Row.FindControl("lgvpc96");
                Label lgvpc97 = (Label)e.Row.FindControl("lgvpc97");
                Label lgvpc98 = (Label)e.Row.FindControl("lgvpc98");
                Label lgvpc99 = (Label)e.Row.FindControl("lgvpc99");
                Label lgvpc100 = (Label)e.Row.FindControl("lgvpc100");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvpc1.Font.Bold = true;
                    lgvpc2.Font.Bold = true;
                    lgvpc3.Font.Bold = true;
                    lgvpc4.Font.Bold = true;
                    lgvpc5.Font.Bold = true;
                    lgvpc6.Font.Bold = true;
                    lgvpc7.Font.Bold = true;
                    lgvpc8.Font.Bold = true;
                    lgvpc9.Font.Bold = true;
                    lgvpc10.Font.Bold = true;
                    lgvpc11.Font.Bold = true;
                    lgvpc12.Font.Bold = true;
                    lgvpc13.Font.Bold = true;
                    lgvpc14.Font.Bold = true;
                    lgvpc15.Font.Bold = true;
                    lgvpc16.Font.Bold = true;
                    lgvpc17.Font.Bold = true;
                    lgvpc18.Font.Bold = true;
                    lgvpc19.Font.Bold = true;
                    lgvpc20.Font.Bold = true;
                    lgvpc21.Font.Bold = true;
                    lgvpc22.Font.Bold = true;
                    lgvpc23.Font.Bold = true;
                    lgvpc24.Font.Bold = true;
                    lgvpc25.Font.Bold = true;
                    lgvpc26.Font.Bold = true;
                    lgvpc27.Font.Bold = true;
                    lgvpc28.Font.Bold = true;
                    lgvpc29.Font.Bold = true;
                    lgvpc30.Font.Bold = true;
                    lgvpc31.Font.Bold = true;
                    lgvpc32.Font.Bold = true;
                    lgvpc33.Font.Bold = true;
                    lgvpc34.Font.Bold = true;
                    lgvpc35.Font.Bold = true;
                    lgvpc36.Font.Bold = true;
                    lgvpc37.Font.Bold = true;
                    lgvpc38.Font.Bold = true;
                    lgvpc39.Font.Bold = true;
                    lgvpc40.Font.Bold = true;
                    lgvpc41.Font.Bold = true;
                    lgvpc42.Font.Bold = true;
                    lgvpc43.Font.Bold = true;
                    lgvpc44.Font.Bold = true;
                    lgvpc45.Font.Bold = true;
                    lgvpc46.Font.Bold = true;
                    lgvpc47.Font.Bold = true;
                    lgvpc48.Font.Bold = true;
                    lgvpc49.Font.Bold = true;
                    lgvpc50.Font.Bold = true;
                    lgvpc51.Font.Bold = true;
                    lgvpc52.Font.Bold = true;
                    lgvpc53.Font.Bold = true;
                    lgvpc54.Font.Bold = true;
                    lgvpc55.Font.Bold = true;
                    lgvpc56.Font.Bold = true;
                    lgvpc57.Font.Bold = true;
                    lgvpc58.Font.Bold = true;
                    lgvpc59.Font.Bold = true;
                    lgvpc60.Font.Bold = true;
                    lgvpc61.Font.Bold = true;
                    lgvpc62.Font.Bold = true;
                    lgvpc63.Font.Bold = true;
                    lgvpc64.Font.Bold = true;
                    lgvpc65.Font.Bold = true;
                    lgvpc66.Font.Bold = true;
                    lgvpc67.Font.Bold = true;
                    lgvpc68.Font.Bold = true;
                    lgvpc69.Font.Bold = true;
                    lgvpc70.Font.Bold = true;
                    lgvpc71.Font.Bold = true;
                    lgvpc72.Font.Bold = true;
                    lgvpc73.Font.Bold = true;
                    lgvpc74.Font.Bold = true;
                    lgvpc75.Font.Bold = true;
                    lgvpc76.Font.Bold = true;
                    lgvpc77.Font.Bold = true;
                    lgvpc78.Font.Bold = true;
                    lgvpc79.Font.Bold = true;
                    lgvpc80.Font.Bold = true;
                    lgvpc81.Font.Bold = true;
                    lgvpc82.Font.Bold = true;
                    lgvpc83.Font.Bold = true;
                    lgvpc84.Font.Bold = true;
                    lgvpc85.Font.Bold = true;
                    lgvpc86.Font.Bold = true;
                    lgvpc87.Font.Bold = true;
                    lgvpc88.Font.Bold = true;
                    lgvpc89.Font.Bold = true;
                    lgvpc90.Font.Bold = true;
                    lgvpc91.Font.Bold = true;
                    lgvpc92.Font.Bold = true;
                    lgvpc93.Font.Bold = true;
                    lgvpc94.Font.Bold = true;
                    lgvpc95.Font.Bold = true;
                    lgvpc96.Font.Bold = true;
                    lgvpc97.Font.Bold = true;
                    lgvpc98.Font.Bold = true;
                    lgvpc99.Font.Bold = true;
                    lgvpc10.Font.Bold = true;



                    acresdesc.Style.Add("text-align", "right");
                }

            }



        }
    }
}