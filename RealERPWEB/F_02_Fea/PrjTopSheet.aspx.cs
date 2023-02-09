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
using System.Drawing;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_02_Fea
{
    public partial class PrjTopSheet : System.Web.UI.Page
    {

        ProcessAccess Fes = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Top Sheet";

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.Getcatagory();

            }
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void Getcatagory()
        {
            string comcod = this.GetComeCode();
            DataSet dscatg = Fes.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATEGORYALL", "", "", "", "", "", "", "", "", "");
            ddlcatag.DataTextField = "prgdesc";
            ddlcatag.DataValueField = "prgcod";
            ddlcatag.DataSource = dscatg.Tables[0];
            ddlcatag.DataBind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {

            this.LoadTopsheet();

        }

        private void LoadTopsheet()
        {
            string comcod = this.GetComeCode();
            string frmddate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            string catcode = (this.ddlcatag.SelectedValue == "00000" ? "99" : this.ddlcatag.SelectedValue) + "%";

            DataSet ds1 = Fes.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "PROJECTTOPSHEET", frmddate, todate, catcode,
                         "", "", "", "", "", "");
            Session["tblcategory"] = ds1.Tables[0];
            Session["tbllocation"] = ds1.Tables[1];
            this.Data_Bind();

        }


        private void Data_Bind()
        {


            DataTable dt = (DataTable)Session["tblcategory"];
            DataTable dt1 = (DataTable)Session["tbllocation"];


            double l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11, l12, l13, l14, l15, l16, l17, l18, l19, l20, l21, l22, l23, l24, l25, l26, l27, l28, l29, l30, l31, l32, l33, l34, l35, l36, l37, l38, l39, l40, l41, l42, l43, l44, l45, l46, l47, l48, l49, l50;
            double tolamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tolamt)", "")) ? 0.00 : dt.Compute("sum(tolamt)", "")));
            l1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l1)", "")) ? 0.00 : dt.Compute("sum(l1)", "")));
            l2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l2)", "")) ? 0.00 : dt.Compute("sum(l2)", "")));
            l3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l3)", "")) ? 0.00 : dt.Compute("sum(l3)", "")));
            l4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l4)", "")) ? 0.00 : dt.Compute("sum(l4)", "")));
            l5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l5)", "")) ? 0.00 : dt.Compute("sum(l5)", "")));
            l6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l6)", "")) ? 0.00 : dt.Compute("sum(l6)", "")));
            l7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l7)", "")) ? 0.00 : dt.Compute("sum(l7)", "")));
            l8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l8)", "")) ? 0.00 : dt.Compute("sum(l8)", "")));
            l9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l9)", "")) ? 0.00 : dt.Compute("sum(l9)", "")));
            l10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l10)", "")) ? 0.00 : dt.Compute("sum(l10)", "")));
            l11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l11)", "")) ? 0.00 : dt.Compute("sum(l11)", "")));
            l12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l12)", "")) ? 0.00 : dt.Compute("sum(l12)", "")));
            l13 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l13)", "")) ? 0.00 : dt.Compute("sum(l13)", "")));
            l14 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l14)", "")) ? 0.00 : dt.Compute("sum(l14)", "")));
            l15 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l15)", "")) ? 0.00 : dt.Compute("sum(l15)", "")));
            l16 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l16)", "")) ? 0.00 : dt.Compute("sum(l16)", "")));
            l17 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l17)", "")) ? 0.00 : dt.Compute("sum(l17)", "")));
            l18 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l18)", "")) ? 0.00 : dt.Compute("sum(l18)", "")));
            l19 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l19)", "")) ? 0.00 : dt.Compute("sum(l19)", "")));
            l20 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l20)", "")) ? 0.00 : dt.Compute("sum(l20)", "")));
            l21 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l21)", "")) ? 0.00 : dt.Compute("sum(l21)", "")));
            l22 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l22)", "")) ? 0.00 : dt.Compute("sum(l22)", "")));
            l23 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l23)", "")) ? 0.00 : dt.Compute("sum(l23)", "")));
            l24 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l24)", "")) ? 0.00 : dt.Compute("sum(l24)", "")));
            l25 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l25)", "")) ? 0.00 : dt.Compute("sum(l25)", "")));
            l26 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l26)", "")) ? 0.00 : dt.Compute("sum(l26)", "")));
            l27 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l27)", "")) ? 0.00 : dt.Compute("sum(l27)", "")));
            l28 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l28)", "")) ? 0.00 : dt.Compute("sum(l28)", "")));
            l29 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l29)", "")) ? 0.00 : dt.Compute("sum(l29)", "")));
            l30 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l30)", "")) ? 0.00 : dt.Compute("sum(l30)", "")));
            l31 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l31)", "")) ? 0.00 : dt.Compute("sum(l31)", "")));
            l32 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l32)", "")) ? 0.00 : dt.Compute("sum(l32)", "")));
            l33 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l33)", "")) ? 0.00 : dt.Compute("sum(l33)", "")));
            l34 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l34)", "")) ? 0.00 : dt.Compute("sum(l34)", "")));
            l35 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l35)", "")) ? 0.00 : dt.Compute("sum(l35)", "")));
            l36 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l36)", "")) ? 0.00 : dt.Compute("sum(l36)", "")));
            l37 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l37)", "")) ? 0.00 : dt.Compute("sum(l37)", "")));
            l38 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l38)", "")) ? 0.00 : dt.Compute("sum(l38)", "")));
            l39 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l39)", "")) ? 0.00 : dt.Compute("sum(l39)", "")));
            l40 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l40)", "")) ? 0.00 : dt.Compute("sum(l40)", "")));
            l41 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l41)", "")) ? 0.00 : dt.Compute("sum(l41)", "")));
            l42 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l42)", "")) ? 0.00 : dt.Compute("sum(l42)", "")));
            l43 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l43)", "")) ? 0.00 : dt.Compute("sum(l43)", "")));
            l44 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l44)", "")) ? 0.00 : dt.Compute("sum(l44)", "")));
            l45 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l45)", "")) ? 0.00 : dt.Compute("sum(l45)", "")));
            l46 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l46)", "")) ? 0.00 : dt.Compute("sum(l46)", "")));
            l47 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l47)", "")) ? 0.00 : dt.Compute("sum(l47)", "")));
            l48 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l48)", "")) ? 0.00 : dt.Compute("sum(l48)", "")));
            l49 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l49)", "")) ? 0.00 : dt.Compute("sum(l49)", "")));
            l50 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l50)", "")) ? 0.00 : dt.Compute("sum(l50)", "")));

            this.gvComCost.Columns[2].Visible = (l1 != 0);
            this.gvComCost.Columns[3].Visible = (l2 != 0);
            this.gvComCost.Columns[4].Visible = (l3 != 0);
            this.gvComCost.Columns[5].Visible = (l4 != 0);
            this.gvComCost.Columns[6].Visible = (l5 != 0);
            this.gvComCost.Columns[7].Visible = (l6 != 0);
            this.gvComCost.Columns[8].Visible = (l7 != 0);
            this.gvComCost.Columns[9].Visible = (l8 != 0);
            this.gvComCost.Columns[10].Visible = (l9 != 0);
            this.gvComCost.Columns[11].Visible = (l10 != 0);
            this.gvComCost.Columns[12].Visible = (l11 != 0);
            this.gvComCost.Columns[13].Visible = (l12 != 0);
            this.gvComCost.Columns[14].Visible = (l13 != 0);
            this.gvComCost.Columns[15].Visible = (l14 != 0);
            this.gvComCost.Columns[16].Visible = (l15 != 0);
            this.gvComCost.Columns[17].Visible = (l16 != 0);
            this.gvComCost.Columns[18].Visible = (l17 != 0);
            this.gvComCost.Columns[19].Visible = (l18 != 0);
            this.gvComCost.Columns[20].Visible = (l19 != 0);
            this.gvComCost.Columns[21].Visible = (l20 != 0);
            this.gvComCost.Columns[22].Visible = (l21 != 0);
            this.gvComCost.Columns[23].Visible = (l22 != 0);
            this.gvComCost.Columns[24].Visible = (l23 != 0);
            this.gvComCost.Columns[25].Visible = (l24 != 0);
            this.gvComCost.Columns[26].Visible = (l25 != 0);
            this.gvComCost.Columns[27].Visible = (l26 != 0);
            this.gvComCost.Columns[28].Visible = (l27 != 0);
            this.gvComCost.Columns[29].Visible = (l28 != 0);
            this.gvComCost.Columns[30].Visible = (l29 != 0);
            this.gvComCost.Columns[31].Visible = (l30 != 0);
            this.gvComCost.Columns[32].Visible = (l31 != 0);
            this.gvComCost.Columns[33].Visible = (l32 != 0);
            this.gvComCost.Columns[34].Visible = (l33 != 0);
            this.gvComCost.Columns[35].Visible = (l34 != 0);
            this.gvComCost.Columns[36].Visible = (l35 != 0);
            this.gvComCost.Columns[37].Visible = (l36 != 0);
            this.gvComCost.Columns[38].Visible = (l37 != 0);
            this.gvComCost.Columns[39].Visible = (l38 != 0);
            this.gvComCost.Columns[40].Visible = (l39 != 0);
            this.gvComCost.Columns[41].Visible = (l40 != 0);
            this.gvComCost.Columns[42].Visible = (l41 != 0);
            this.gvComCost.Columns[43].Visible = (l42 != 0);
            this.gvComCost.Columns[44].Visible = (l43 != 0);
            this.gvComCost.Columns[45].Visible = (l44 != 0);
            this.gvComCost.Columns[46].Visible = (l45 != 0);
            this.gvComCost.Columns[47].Visible = (l46 != 0);
            this.gvComCost.Columns[48].Visible = (l47 != 0);
            this.gvComCost.Columns[49].Visible = (l48 != 0);
            this.gvComCost.Columns[50].Visible = (l49 != 0);
            this.gvComCost.Columns[51].Visible = (l50 != 0);
            int j = 2;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                this.gvComCost.Columns[j].HeaderText = dt1.Rows[i]["locdesc"].ToString();
                j++;
                if (j == 52)
                    break;
            }
            this.gvComCost.DataSource = dt;
            this.gvComCost.DataBind();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvComCost.FooterRow.FindControl("lgvFtoCost")).Text = tolamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC1")).Text = l1.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC2")).Text = l2.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC3")).Text = l3.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC4")).Text = l4.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC5")).Text = l5.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC6")).Text = l6.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC7")).Text = l7.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC8")).Text = l8.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC9")).Text = l9.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC10")).Text = l10.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC11")).Text = l11.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC12")).Text = l12.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC13")).Text = l13.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC14")).Text = l14.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC15")).Text = l15.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC16")).Text = l16.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC17")).Text = l17.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC18")).Text = l18.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC19")).Text = l19.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC20")).Text = l20.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC21")).Text = l21.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC22")).Text = l22.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC23")).Text = l23.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC24")).Text = l24.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC25")).Text = l25.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC26")).Text = l26.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC27")).Text = l27.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC28")).Text = l28.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC29")).Text = l29.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC30")).Text = l30.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC31")).Text = l31.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC32")).Text = l32.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC33")).Text = l33.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC34")).Text = l34.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC35")).Text = l35.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC36")).Text = l36.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC37")).Text = l37.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC38")).Text = l38.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC39")).Text = l39.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC40")).Text = l40.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC41")).Text = l41.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC42")).Text = l42.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC43")).Text = l43.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC44")).Text = l44.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC45")).Text = l45.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC46")).Text = l46.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC47")).Text = l47.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC48")).Text = l48.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC49")).Text = l49.ToString("#,##0;(#,##0); ");
            ((Label)this.gvComCost.FooterRow.FindControl("lgvFPC50")).Text = l50.ToString("#,##0;(#,##0); ");

            //    Session["Report1"] = gvComCost;
            //    ((HyperLink)this.gvComCost.HeaderRow.FindControl ("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            // Session["tblfeaprjLand"] = ds2.Tables[0];


            //Session["tblcategory"] = ds1.Tables[0];
            //Session["tbllocation"] = ds1.Tables[1];


            DataTable dt = (DataTable)Session["tblcategory"];
            DataTable dt1 = (DataTable)Session["tbllocation"];
            LocalReport Rpt1 = new LocalReport();

            var lst = dt.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectTopSheet01>();
            var lst1 = dt1.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectTopSheet02>();



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_02_Fea.RptProjectTopSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;

            //string lochad ="";

            //for (int i = 0; i <dt1.Rows.Count; i++)
            //{
            //    Rpt1.SetParameters(new ReportParameter("lochad" +(i+1).ToString(), dt1.Rows[i]["locdesc"].ToString()));

            //}


            int i = 1;
            foreach (RealEntity.C_02_Fea.EClasFeasibility.ProjectTopSheet02 lsts in lst1)
            {
                Rpt1.SetParameters(new ReportParameter("lochad" + i.ToString(), lsts.locdesc.ToString()));
                i++;

            }


            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTital", "PROJECT TOP SHEET"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            //Rpt1.SetParameters(new ReportParameter("lochad1", lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad2", lochad));
            ////Rpt1.SetParameters(new ReportParameter("lochad3" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad4" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad5" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad6" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad7" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad8" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad9" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad10" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad12" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad13" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad14" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad15" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad16" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad17" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad18" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad19" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad20" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad21" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad22" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad23" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad24" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad25" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad26" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad27" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad28" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad29" lochad));
            //Rpt1.SetParameters(new ReportParameter("lochad30" lochad));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvComCost_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");

                string catcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "catcode")) + "%";
                string catesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prgdesc"));

                string frmdate = this.txtfromdate.Text;
                string todate = this.txttodate.Text;
                string category = this.ddlcatag.SelectedItem.ToString();
                hlink1.NavigateUrl = "~/F_01_LPA/RptAllProTopSheetHL.aspx?frmdate=" + frmdate + "&todate=" + todate + "&catcode=" + catcode + "&category=" + catesc;

            }
        }


    }
}