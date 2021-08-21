using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_32_Mis
{
    public partial class PrjDirectCost : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtDatefromCom.Text = System.DateTime.Today.AddMonths (-1).ToString ("dd-MMM-yyyy");
                this.txtDatefromCom.Text = "01" + date.Substring(2);
                //this.txtDatetoCom.Text =
                //    Convert.ToDateTime(this.txtDatetoCom.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.txtDatetoCom.Text = Convert.ToDateTime(this.txtDatefromCom.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowData()
        {
            string comcod = this.GetComeCode();
            string txtdatefrm = this.txtDatefromCom.Text;
            string txtdateto = this.txtDatetoCom.Text;
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "GETYSALBGDBREAK", txtdatefrm, txtdateto);
            if (ds1 == null)
            {
                this.gvComCost.DataSource = null;
                this.gvComCost.DataBind();
                return;
            }


            Session["tblmasterbgd"] = ds1.Tables[0];
            ViewState["tblproname"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt, dt1; DataView dv;
            dt1 = ((DataTable)Session["tblmasterbgd"]);
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("rescode like '%0000000000'");
            dt = dv.ToTable();

            //double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34,
                p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67,
                p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99, p100;
            // DateTime datefrm, dateto;
            int i, j;
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



            this.gvComCost.Columns[2].Visible = (p1 != 0);
            this.gvComCost.Columns[3].Visible = (p2 != 0);
            this.gvComCost.Columns[4].Visible = (p3 != 0);
            this.gvComCost.Columns[5].Visible = (p4 != 0);
            this.gvComCost.Columns[6].Visible = (p5 != 0);
            this.gvComCost.Columns[7].Visible = (p6 != 0);
            this.gvComCost.Columns[8].Visible = (p7 != 0);
            this.gvComCost.Columns[9].Visible = (p8 != 0);
            this.gvComCost.Columns[10].Visible = (p9 != 0);
            this.gvComCost.Columns[11].Visible = (p10 != 0);
            this.gvComCost.Columns[12].Visible = (p11 != 0);
            this.gvComCost.Columns[13].Visible = (p12 != 0);
            this.gvComCost.Columns[14].Visible = (p13 != 0);
            this.gvComCost.Columns[15].Visible = (p14 != 0);
            this.gvComCost.Columns[16].Visible = (p15 != 0);
            this.gvComCost.Columns[17].Visible = (p16 != 0);
            this.gvComCost.Columns[18].Visible = (p17 != 0);
            this.gvComCost.Columns[19].Visible = (p18 != 0);
            this.gvComCost.Columns[20].Visible = (p19 != 0);
            this.gvComCost.Columns[21].Visible = (p20 != 0);
            this.gvComCost.Columns[22].Visible = (p21 != 0);
            this.gvComCost.Columns[23].Visible = (p22 != 0);
            this.gvComCost.Columns[24].Visible = (p23 != 0);
            this.gvComCost.Columns[25].Visible = (p24 != 0);
            this.gvComCost.Columns[26].Visible = (p25 != 0);
            this.gvComCost.Columns[27].Visible = (p26 != 0);
            this.gvComCost.Columns[28].Visible = (p27 != 0);
            this.gvComCost.Columns[29].Visible = (p28 != 0);
            this.gvComCost.Columns[30].Visible = (p29 != 0);
            this.gvComCost.Columns[31].Visible = (p30 != 0);
            this.gvComCost.Columns[32].Visible = (p31 != 0);
            this.gvComCost.Columns[33].Visible = (p32 != 0);
            this.gvComCost.Columns[34].Visible = (p33 != 0);
            this.gvComCost.Columns[35].Visible = (p34 != 0);
            this.gvComCost.Columns[36].Visible = (p35 != 0);
            this.gvComCost.Columns[37].Visible = (p36 != 0);
            this.gvComCost.Columns[38].Visible = (p37 != 0);
            this.gvComCost.Columns[39].Visible = (p38 != 0);
            this.gvComCost.Columns[40].Visible = (p39 != 0);
            this.gvComCost.Columns[41].Visible = (p40 != 0);
            this.gvComCost.Columns[42].Visible = (p41 != 0);
            this.gvComCost.Columns[43].Visible = (p42 != 0);
            this.gvComCost.Columns[44].Visible = (p43 != 0);
            this.gvComCost.Columns[45].Visible = (p44 != 0);
            this.gvComCost.Columns[46].Visible = (p45 != 0);
            this.gvComCost.Columns[47].Visible = (p46 != 0);
            this.gvComCost.Columns[48].Visible = (p47 != 0);
            this.gvComCost.Columns[49].Visible = (p48 != 0);
            this.gvComCost.Columns[50].Visible = (p49 != 0);
            this.gvComCost.Columns[51].Visible = (p50 != 0);

            this.gvComCost.Columns[52].Visible = (p51 != 0);
            this.gvComCost.Columns[53].Visible = (p52 != 0);
            this.gvComCost.Columns[54].Visible = (p53 != 0);
            this.gvComCost.Columns[55].Visible = (p54 != 0);
            this.gvComCost.Columns[56].Visible = (p55 != 0);
            this.gvComCost.Columns[57].Visible = (p56 != 0);
            this.gvComCost.Columns[58].Visible = (p57 != 0);
            this.gvComCost.Columns[59].Visible = (p58 != 0);
            this.gvComCost.Columns[60].Visible = (p59 != 0);
            this.gvComCost.Columns[61].Visible = (p60 != 0);
            this.gvComCost.Columns[62].Visible = (p61 != 0);
            this.gvComCost.Columns[63].Visible = (p62 != 0);
            this.gvComCost.Columns[64].Visible = (p63 != 0);
            this.gvComCost.Columns[65].Visible = (p64 != 0);
            this.gvComCost.Columns[66].Visible = (p65 != 0);
            this.gvComCost.Columns[67].Visible = (p66 != 0);
            this.gvComCost.Columns[68].Visible = (p67 != 0);
            this.gvComCost.Columns[69].Visible = (p68 != 0);
            this.gvComCost.Columns[70].Visible = (p69 != 0);
            this.gvComCost.Columns[71].Visible = (p70 != 0);
            this.gvComCost.Columns[72].Visible = (p71 != 0);
            this.gvComCost.Columns[73].Visible = (p72 != 0);
            this.gvComCost.Columns[74].Visible = (p73 != 0);
            this.gvComCost.Columns[75].Visible = (p74 != 0);
            this.gvComCost.Columns[76].Visible = (p75 != 0);
            this.gvComCost.Columns[77].Visible = (p76 != 0);
            this.gvComCost.Columns[78].Visible = (p77 != 0);
            this.gvComCost.Columns[79].Visible = (p78 != 0);
            this.gvComCost.Columns[80].Visible = (p79 != 0);
            this.gvComCost.Columns[81].Visible = (p80 != 0);
            this.gvComCost.Columns[82].Visible = (p81 != 0);
            this.gvComCost.Columns[83].Visible = (p82 != 0);
            this.gvComCost.Columns[84].Visible = (p83 != 0);
            this.gvComCost.Columns[85].Visible = (p84 != 0);
            this.gvComCost.Columns[86].Visible = (p85 != 0);
            this.gvComCost.Columns[87].Visible = (p86 != 0);
            this.gvComCost.Columns[88].Visible = (p87 != 0);
            this.gvComCost.Columns[89].Visible = (p88 != 0);
            this.gvComCost.Columns[90].Visible = (p89 != 0);
            this.gvComCost.Columns[91].Visible = (p90 != 0);
            this.gvComCost.Columns[92].Visible = (p91 != 0);
            this.gvComCost.Columns[93].Visible = (p92 != 0);
            this.gvComCost.Columns[94].Visible = (p93 != 0);
            this.gvComCost.Columns[95].Visible = (p94 != 0);
            this.gvComCost.Columns[96].Visible = (p95 != 0);
            this.gvComCost.Columns[97].Visible = (p96 != 0);
            this.gvComCost.Columns[98].Visible = (p97 != 0);
            this.gvComCost.Columns[99].Visible = (p98 != 0);
            this.gvComCost.Columns[100].Visible = (p99 != 0);
            this.gvComCost.Columns[101].Visible = (p100 != 0);
            j = 2;


            DataTable dtpname = (DataTable)ViewState["tblproname"];
            for (i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvComCost.Columns[j].HeaderText = dtpname.Rows[i]["pactdesc"].ToString();
                j++;
                if (j == 102)
                    break;


            }

            this.gvComCost.DataSource = dt1;
            this.gvComCost.DataBind();


            if (dt.Rows.Count == 0)
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
        }

        protected void lbtnShowComCost_OnClick(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void gvComCost_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvRptRes1 = (Label)e.Row.FindControl("lgvActDescCost");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblgvRptAmt1 = (Label)e.Row.FindControl("lgvtopamtcost");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }



                if (ASTUtility.Right(code, 10) == "0000000000")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    e.Row.Attributes["style"] = "background-color:green;color:white;font-size:12px; font-weight:bold;";
                    //lblgvRptAmt1.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                    //lblgvPer.Attributes["style"] = "background-color:green;font-size:16px; font-weight:bold;";
                }


            }
        }
    }
}