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
using System.Drawing;

namespace RealERPWEB.F_14_Pro
{
    public partial class PurMktSurveyCont : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");


                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor Comparative Statement";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString().Substring(0, indexofamp);

                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                //  this.TableCreate();
                this.GetProjects();
                this.GetReqNo();
                if (Request.QueryString.AllKeys.Contains("pType"))
                {
                    if (Request.QueryString["pType"].ToString() == "CSApproval")
                    {
                        this.csAprrovalPrint();
                    }
                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void csAprrovalPrint()
        {
            switch (GetCompCode())
            {
                case "3101":
                case "3370":
                    this.printCPDL_cs_approval();
                    break;
                case "1205":
                case "3351":
                case "3352":
                    this.printP2P_cs_approval();
                    break;

                default:
                    break;
            }


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("ssircode", Type.GetType("System.String"));
            tblt01.Columns.Add("ssirdesc1", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }
        protected void Resource_List(string pmSrchTxt)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (this.Request.QueryString["Type"].ToString() == "ConCS")
            {
                this.Resource_ListP2P(pmSrchTxt);
            }
        }

        protected void Resource_ListGen(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST1_CON", pmSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];

        }

        protected void Resource_ListP2P(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string prjcode = (this.Request.QueryString["pactcode"].ToString()).Length == 0 ? "16" : this.Request.QueryString["pactcode"].ToString();
            string lisuno = (this.Request.QueryString["lisuno"].ToString()).Length == 0 ? "000000000000" : this.Request.QueryString["lisuno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLISTP2P", prjcode, lisuno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];

        }


        //protected string GetStdDate(string Date1)
        //{
        //    Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
        //    return Date1;
        //}
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = Convert.ToDateTime(this.txtCurMSRDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mSrchTxt = this.txtPreMSRSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREMSR_CON", date, mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {
                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                this.lblPreMrList.Visible = true;
                this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();
                this.lblCurMSRNo1.Text = "MSC" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;

                this.gvMSRInfo2.DataSource = null;
                this.gvMSRInfo2.DataBind();
                this.gvterm.DataSource = null;
                this.gvterm.DataBind();
                this.pnlSupMat.Visible = false;
                this.Panel2.Visible = false;
                // this.pnlNarration.Visible = false;
                this.lbtnMSROk.Text = "Ok";


                return;
            }
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            this.lblPreMrList.Visible = false;
            this.txtPreMSRSearch.Visible = false;
            this.pnlSupMat.Visible = true;
            this.Panel2.Visible = true;
            // this.pnlNarration.Visible = true;
            this.lbtnMSROk.Text = "New";
            this.ImgbtnFindSup_Click(null, null);
            this.ImgbtnFindMat_Click(null, null);

            this.Get_Survey_Info();


        }

        protected void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            // string date = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).ToString("dd-MMM-yyyy");
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                this.txtCurMSRDate.Enabled = false;
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO1CON", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblt01"] = ds1.Tables[1];
            Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);
            Session["tblterm"] = ds1.Tables[3];


            if (mMSRNo == "NEWMSR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GET_LAST_MSR_CON_INFO1", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["msrno1"].ToString().Substring(0, 6);
            this.txtCurMSRNo2.Text = ds1.Tables[0].Rows[0]["msrno1"].ToString().Substring(6, 5);
            this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");

            this.txtMSRNarr.Text = ds1.Tables[4].Rows[0]["remarks"].ToString();
            this.gvMSRInfo_DataBind();

            this.Payterm_DataBind();
        }

        protected void gvMSRInfo_DataBind()
        {
            DataTable dt = (DataTable)Session["tblt02"];

            dt.DefaultView.Sort = "rsircode ASC, flrcod ASC";
            dt = dt.DefaultView.ToTable();

            this.gvMSRInfo2.DataSource = dt;
            this.gvMSRInfo2.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblt02"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
          : dt.Compute("Sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
        : dt.Compute("Sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
        : dt.Compute("Sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
        : dt.Compute("Sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00);  ");

            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
        : dt.Compute("Sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        }

        protected void printAll_cs()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();

            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            // string mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            //string SurveyNo  = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;
            //  string SupplierList = this.ddlMSRSupl.SelectedItem.Text.Trim().ToString();
            //  string MaterialsList = this.ddlMSRRes.SelectedValue.ToString().Substring(0, 9);
            //   string Specification = this.ddlSpecificationms.SelectedItem.Text.Trim().ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEY02_CON", mMSRNo, "", "", "", "", "", "", "", "");
            string narration = this.txtMSRNarr.Text.Trim();
            string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;
            /// SP_ENTRY_PURCHASE_01 '3101','RPTMARKETSURVEY02','MSR20180200003', '',''
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();

            if (lst1.Count == 5)
            {
                if (comcod == "3353")
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyManama05", lst, lst1, null);
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurvey05", lst, lst1, null);
                }

                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    i++;
                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
                Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("narration", "Comments : " + narration));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else if (lst1.Count == 4)
            {
                if (comcod == "3353")
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyManama02", lst, lst1, null);
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurvey02", lst, lst1, null);
                }

                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    i++;
                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
                Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("narration", "Comments : " + narration));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else
            {
                if (comcod == "3353")
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyManama03", lst, lst1, null);
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurvey03", lst, lst1, null);
                }
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    i++;

                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
                Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("narration", "Comments : " + narration));
                Rpt1.SetParameters(new ReportParameter("surveyNo", surveyNo));

                //Rpt1.SetParameters(new ReportParameter("MaterialsList", MaterialsList));
                // Rpt1.SetParameters(new ReportParameter("Specification", Specification));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void printP2P_cs()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            string comments = this.txtMSRNarr.Text.Trim();
            // string mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEY02P2P", mMSRNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            string Projectname = "";
            string Projectlocat = "";
            string Username = "";
            string userdesig = "";
            string rsirdesc = "";

            if (ds1.Tables[3].Rows.Count > 0)
            {
                Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
                Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
                Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
                userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
                rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();


            }

            //Commented By Nime 
            //string Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
            //string Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
            //string Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
            //string userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
            //string rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();

            string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;

            DataTable dtdetails = (DataTable)Session["tblt02"];

            //DataView dv1 = ds1.Tables[0].DefaultView;
            //dv1.RowFilter = ("spcfcod== 000000000000");
            //dtdetails = dv1.ToTable();

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();


            if (lst1.Count == 5)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P05", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));


                    i++;
                }
            }


            else if (lst1.Count == 4)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P02", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));


                    i++;
                }
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP_2_P", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    i++;

                }
            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
            Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
            Rpt1.SetParameters(new ReportParameter("Username", Username));
            Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
            Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));

            // Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
            //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("comments", comments));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "',  target='_blank');</script>";


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3351":
                case "3352":
                case "1205":
                    //case "3101":
                    this.printP2P_cs_approval();
                    break;
                case "3101": // cpdl
                case "3370": // cpdl
                    this.printCPDL_cs_approval();
                    break;
                default:
                    this.printAll_cs();
                    break;
            }

        }

        protected void printP2P_cs_approval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            string comments = this.txtMSRNarr.Text.Trim();
            // string mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRNo = "";
            if (Request.QueryString.AllKeys.Contains("msrno"))
            {
                mMSRNo = Request.QueryString["msrno"].ToString() == "" ? "NEWMSR" : Request.QueryString["msrno"].ToString();
            }
            else if (this.ddlPrevMSRList.Items.Count > 0)
            {
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }
            else
            {
                //mMSRNo = Request.QueryString["msrno"].ToString() == "" ? "NEWMSR" : Request.QueryString["msrno"].ToString();
                DataTable dt2 = (DataTable)Session["tblmsr01"];
                mMSRNo = dt2.Rows[0]["maxmsrno"].ToString();
            }


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEYP02PCSApproval", mMSRNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string Projectname = "";
            string Projectlocat = "";
            string Username = "";
            string userdesig = "";
            string rsirdesc = "";
            string txtsign1 = "";
            string txtsign2 = "";
            string txtsign3 = "";

            if (ds1.Tables[3].Rows.Count > 0)
            {
                Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
                Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
                Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
                userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
                rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();

                txtsign1 = ds1.Tables[3].Rows[0]["usrname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["userdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
                txtsign2 = ds1.Tables[3].Rows[0]["csname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdat"].ToString();
                txtsign3 = ds1.Tables[3].Rows[0]["aprvname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["aprdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();

            }

            string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;

            DataTable dtdetails = (DataTable)Session["tblt02"];

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();

            string reqinfo = ds1.Tables[3].Rows[0]["reqno"].ToString();
            string csinfo = ds1.Tables[2].Rows[0]["msrno"].ToString() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");


            if (lst1.Count == 5)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P05", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod
                    Rpt1.SetParameters(new ReportParameter("carrying" + i.ToString() + "", lsts.ccharge.ToString()));

                    i++;
                }
            }


            else if (lst1.Count == 4)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P02", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod
                    Rpt1.SetParameters(new ReportParameter("carrying" + i.ToString() + "", lsts.ccharge.ToString()));

                    i++;
                }
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP_2_P", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod
                    Rpt1.SetParameters(new ReportParameter("carrying" + i.ToString() + "", lsts.ccharge.ToString()));

                    i++;

                }

            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
            Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
            Rpt1.SetParameters(new ReportParameter("Username", Username));
            Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
            Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));
            Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
            Rpt1.SetParameters(new ReportParameter("txtsign2", txtsign2));
            Rpt1.SetParameters(new ReportParameter("txtsign3", txtsign3));
            Rpt1.SetParameters(new ReportParameter("reqinfo", reqinfo));
            Rpt1.SetParameters(new ReportParameter("csinfo", csinfo));

            // Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
            //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("comments", comments));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            if (Request.QueryString.AllKeys.Contains("pType"))
            {
                if (Request.QueryString["pType"].ToString() == "CSApproval")
                {
                    Session["Report1"] = Rpt1;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "',  target='_self');</script>";
                }
            }

            else
            {
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "',  target='_blank');</script>";
            }
        }

        protected void printCPDL_cs_approval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd"].ToString().Replace("<br />", "\n");
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            string comments = this.txtMSRNarr.Text.Trim();
            string mMSRNo = "";
            if (Request.QueryString.AllKeys.Contains("msrno"))
            {
                mMSRNo = Request.QueryString["msrno"].ToString() == "" ? "NEWMSR" : Request.QueryString["msrno"].ToString();
            }
            else if (this.ddlPrevMSRList.Items.Count > 0)
            {
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }
            else
            {
                //mMSRNo = Request.QueryString["msrno"].ToString() == "" ? "NEWMSR" : Request.QueryString["msrno"].ToString();
                DataTable dt2 = (DataTable)Session["tblmsr01"];
                mMSRNo = dt2.Rows[0]["maxmsrno"].ToString();
            }


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEYP02PCSApproval", mMSRNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string Projectname = "";
            string Projectlocat = "";
            string Username = "";
            string userdesig = "";
            string rsirdesc = "";
            string txtsign1 = "";
            string txtsign2 = "";
            string txtsign3 = "";
            string recomsup = ds1.Tables[2].Rows[0]["rcmsupdesc"].ToString();

            if (ds1.Tables[3].Rows.Count > 0)
            {
                Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
                Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
                Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
                userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
                rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();
                txtsign1 = ds1.Tables[3].Rows[0]["usrname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["userdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
                txtsign2 = ds1.Tables[3].Rows[0]["csname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdat"].ToString();
                txtsign3 = ds1.Tables[3].Rows[0]["aprvname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["aprdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();

            }

            string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;

            DataTable dtdetails = (DataTable)Session["tblt02"];

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();

            string reqinfo = ASTUtility.CustomReqFormat(ds1.Tables[3].Rows[0]["reqno"].ToString());
            string csinfo = ASTUtility.CustomReqFormat(ds1.Tables[2].Rows[0]["msrno"].ToString()) + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");


            if (lst1.Count == 5)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyCPDL05C", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", Convert.ToDouble(lsts.ccharge).ToString("#,##0.00;(#,##0.00); ")));
                    i++;
                }

            }
            else if (lst1.Count == 4)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyCPDL04C", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", Convert.ToDouble(lsts.ccharge).ToString("#,##0.00;(#,##0.00); ")));

                    i++;
                }
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyCPDL03C", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", Convert.ToDouble(lsts.ccharge).ToString("#,##0.00;(#,##0.00); ")));
                    i++;

                }
            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
            Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
            Rpt1.SetParameters(new ReportParameter("Username", Username));
            Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
            Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));
            Rpt1.SetParameters(new ReportParameter("reqinfo", reqinfo));
            Rpt1.SetParameters(new ReportParameter("csinfo", csinfo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("comments", comments));
            Rpt1.SetParameters(new ReportParameter("recomsup", recomsup));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            if (Request.QueryString.AllKeys.Contains("pType"))
            {
                if (Request.QueryString["pType"].ToString() == "CSApproval")
                {
                    Session["Report1"] = Rpt1;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "',  target='_self');</script>";
                }
            }

            else
            {
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "',  target='_blank');</script>";
            }
        }

        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //    return;
            //}
            this.lbtnTotal_Click(null, null);
            bool result;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            this.Session_tblMSR_Update();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRDAT = this.txtCurMSRDate.Text.Trim();
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO1_CON", mMSRDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();
                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno1";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();

                    Session["tblmsr01"] = ds2.Tables[0];
                }
                else
                    return;
            }
            string mRESRATE = "";
            string mResOth = "";
            string mRefno = "";
            //string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            //string mMSRBYDES = this.txtPreparedBy.Text.Trim();
            //string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETRPUR02AB_CON", mMSRNO,
                             "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string lreqno = this.ddlReqNo.SelectedValue.ToString();
            string mRemarks = this.txtMSRNarr.Text.Trim();
            string prjcode = this.ddlprjlist.SelectedValue.ToString();


            string postedbyid = hst["usrid"].ToString();
            string postrmid = hst["compname"].ToString();
            string postseson = hst["session"].ToString();
            string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string recomsup = this.ddlrecomsup.SelectedValue.ToString() == "000000000000" ? "" : this.ddlrecomsup.SelectedValue.ToString();

            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATE_PUR_MSR_INFO1_CON", "PURMSR02A",
                             mMSRNO, mMSRDAT, mRefno, mRemarks, prjcode, lreqno, postedbyid, postrmid, postseson, posteddat, recomsup, "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataTable tbl1 = (DataTable)Session["tblt02"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                string flrcod = tbl1.Rows[i]["flrcod"].ToString();
                DataTable tbls1 = (DataTable)Session["tblt01"];

                for (int j = 0; j < tbls1.Rows.Count; j++)
                {
                    string mSSIRCODE = tbls1.Rows[j]["ssircode"].ToString();

                    string qty = tbl1.Rows[i]["qty"].ToString();
                    mRESRATE = Convert.ToDouble("0" + tbl1.Rows[i]["resrate" + (j + 1).ToString()]).ToString();

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATE_PUR_MSR_INFO1_CON", "PURMSR02B", mMSRNO, mRSIRCODE, spcfcod, mSSIRCODE, mRESRATE, qty, flrcod, "", "", "", "", "", "", "");
                }

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Update Successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    // return;
                }
            }



            // Term 
            DataTable tblterm = (DataTable)Session["tblterm"];

            foreach (DataRow drr in tblterm.Rows)
            {


                string ssircode = drr["ssircode"].ToString();
                string discount = drr["discount"].ToString();
                string ccharge = drr["ccharge"].ToString();
                string payterm = drr["payterm"].ToString();
                string qutdate = drr["qutdate"].ToString();
                string worktime = drr["worktime"].ToString();
                string notes = drr["notes"].ToString();
                string crperiod = drr["crperiod"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "INSERTORUPDATEMSURVEY02_CON", mMSRNO, ssircode, discount, ccharge, payterm, qutdate, worktime, notes, crperiod, "", "", "", "", "");
            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "";
                string eventdesc = "Update Survey";
                string eventdesc2 = mMSRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            Session.Remove("Supplier");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtMSRSupSearch.Text.Trim() + "%";
            string rtype = (this.Request.QueryString["Type"].ToString() == "ConCS") ? "ConCS" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST01", mSrchTxt, rtype, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["Supplier"] = ds1.Tables[0];
            this.ddlMSRSupl.DataTextField = "ssirdesc1";
            this.ddlMSRSupl.DataValueField = "ssircode";
            this.ddlMSRSupl.DataSource = ds1.Tables[0];
            this.ddlMSRSupl.DataBind();
        }
        protected void ImgbtnFindMat_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtMSRResSearch.Text.Trim() + "%";
            string comcod = this.GetCompCode();

            this.Resource_List(mSrchTxt);


            DataTable dtmat = (DataTable)Session["tblremat"];
            DataTable dtmainMat = (DataTable)Session["tblMat"];


            this.chkMSRRes.DataTextField = "rsirdesc1";
            this.chkMSRRes.DataValueField = "rsircode";
            this.chkMSRRes.DataSource = dtmainMat;
            this.chkMSRRes.DataBind();

            //this.ddlMSRRes.DataTextField = "rsirdesc1";
            //this.ddlMSRRes.DataValueField = "rsircode";
            //this.ddlMSRRes.DataSource = dtmainMat;
            //this.ddlMSRRes.DataBind();


            /*
            if (dtmat == null)
            {
                this.ddlMSRRes.DataTextField = "rsirdesc1";
                this.ddlMSRRes.DataValueField = "rsircode";
                this.ddlMSRRes.DataSource = dtmainMat;
                this.ddlMSRRes.DataBind();
            }
            else
            {
                switch (comcod)
                {
                    case "1205":
                    case "3351":
                    case "3352":

                        this.ddlMSRRes.DataTextField = "rsirdesc1";
                        this.ddlMSRRes.DataValueField = "rsircode";
                        this.ddlMSRRes.DataSource = dtmainMat;
                        this.ddlMSRRes.DataBind();

                        break;

                    default:

                        DataTable dtResult = new DataTable();
                        dtResult.Columns.Add("rsirdesc1", typeof(string));
                        dtResult.Columns.Add("rsircode", typeof(string));
                        var result = from dataRows1 in dtmat.AsEnumerable()
                                     join dataRows2 in dtmainMat.AsEnumerable()
                                     on dataRows1.Field<string>("rsircode") equals ASTUtility.Left(dataRows2.Field<string>("rsircode"), 12).ToString()

                                     select dtResult.LoadDataRow(new object[]
                                     {
                                        dataRows1.Field<string>("rsirdesc1"),
                                        dataRows1.Field<string>("rsircode"),

                                      }, false);
                        result.CopyToDataTable();


                        this.ddlMSRRes.DataTextField = "rsirdesc1";
                        this.ddlMSRRes.DataValueField = "rsircode";
                        this.ddlMSRRes.DataSource = dtResult;
                        this.ddlMSRRes.DataBind();
                        break;
                }


            }

            */




            //this.GetSpecification01();

        }


        //private void GetSpecification01()
        //{

        //    DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
        //    string Resource01 = this.ddlMSRRes.SelectedValue.ToString().Substring(0, 9);
        //    DataView dv = dt.DefaultView;
        //    dv.RowFilter = ("mspcfcod='" + Resource01 + "' or mspcfcod='000000000'");
        //    //dv.Sort = ("wrkcode, rsircode");
        //    dt = dv.ToTable();


        //    this.ddlSpecificationms.DataTextField = "spcfdesc";
        //    this.ddlSpecificationms.DataValueField = "spcfcod";
        //    this.ddlSpecificationms.DataSource = dt;
        //    this.ddlSpecificationms.DataBind();

        //}

        private void GetProjects()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string ddldesc = hst["ddldesc"].ToString();


            string prjcode = (this.Request.QueryString["pactcode"].ToString()).Length == 0 ? "16%" : this.Request.QueryString["pactcode"].ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "PRJCODELIST1", prjcode, "", "", userid, "", "", "", "", "");
            if (ds1 == null)
                return;
            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();

        }
        private void GetReqNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string date = Convert.ToDateTime(this.txtCurMSRDate.Text.Trim()).ToString("dd-MMM-yyyy");


            string lisuno = (this.Request.QueryString["lisuno"].ToString()).Length == 0 ? "%" : this.Request.QueryString["lisuno"].ToString() + "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETWORKERREQLIST", date, lisuno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlReqNo.DataTextField = "lreqno1";
            this.ddlReqNo.DataValueField = "lreqno";
            this.ddlReqNo.DataSource = ds1.Tables[0];
            this.ddlReqNo.DataBind();

            Session["tblremat"] = ds1.Tables[1];
            //Session["tblt02"] = this.HiddenSameData(ds1.Tables[1]);
            Session["tblreq01"] = this.HiddenSameData(ds1.Tables[1]);



        }



        protected void Session_tblMSR_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblt02"];
            int TblRowIndex2;

            for (int j = 0; j < this.gvMSRInfo2.Rows.Count; j++)
            {

                string rsirunit = ((Label)this.gvMSRInfo2.Rows[j].FindControl("lblgvMSRResUnit")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtgvMSRqty")).Text.Trim());
                double resrate1 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate1")).Text.Trim());
                double resrate2 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate2")).Text.Trim());
                double resrate3 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate3")).Text.Trim());
                double resrate4 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate4")).Text.Trim());
                double resrate5 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate5")).Text.Trim());


                string aprovrate = (((Label)this.gvMSRInfo2.Rows[j].FindControl("lblaprovrate")).Text.Trim() == "") ? "0.00" : ((Label)this.gvMSRInfo2.Rows[j].FindControl("lblaprovrate")).Text.Trim();
                string dgvMSRRemarks = ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtgvMSRRemarks")).Text.Trim();
                tbl1.Rows[j]["qty"] = qty;
                tbl1.Rows[j]["rsirunit"] = rsirunit;
                tbl1.Rows[j]["resrate1"] = resrate1;
                tbl1.Rows[j]["resrate2"] = resrate2;
                tbl1.Rows[j]["resrate3"] = resrate3;
                tbl1.Rows[j]["resrate4"] = resrate4;
                tbl1.Rows[j]["resrate5"] = resrate5;
                tbl1.Rows[j]["amt1"] = qty * resrate1;
                tbl1.Rows[j]["amt2"] = qty * resrate2;
                tbl1.Rows[j]["amt3"] = qty * resrate3;
                tbl1.Rows[j]["amt4"] = qty * resrate4;
                tbl1.Rows[j]["amt5"] = qty * resrate5;
                tbl1.Rows[j]["aprovrate"] = aprovrate;
                tbl1.Rows[j]["msrrmrk"] = dgvMSRRemarks;
            }


            Session["tblt02"] = tbl1;
        }

        private void SaveTermValue()
        {

            DataTable tbl1 = (DataTable)Session["tblterm"];
            for (int j = 0; j < this.gvterm.Rows.Count; j++)
            {
                double discount = Convert.ToDouble("0" + ((TextBox)this.gvterm.Rows[j].FindControl("txtgvDiscount")).Text.Trim());
                string ccharge = ((TextBox)this.gvterm.Rows[j].FindControl("txtgvccharge")).Text.Trim();
                string payterm = ((TextBox)this.gvterm.Rows[j].FindControl("txtgvpayterm")).Text.Trim();
                string worktime = ((TextBox)this.gvterm.Rows[j].FindControl("txtworkline")).Text.Trim();
                string notes = ((TextBox)this.gvterm.Rows[j].FindControl("txtNotes")).Text.Trim();
                string crperiod = ((TextBox)this.gvterm.Rows[j].FindControl("txtcrPeriod")).Text.Trim();

                string qtdate = Convert.ToDateTime(((TextBox)this.gvterm.Rows[j].FindControl("txtCurQuTDate")).Text).ToString("dd-MMM-yyyy");

                tbl1.Rows[j]["discount"] = discount;
                tbl1.Rows[j]["ccharge"] = ccharge;
                tbl1.Rows[j]["payterm"] = payterm;
                tbl1.Rows[j]["qutdate"] = qtdate;
                tbl1.Rows[j]["worktime"] = worktime;
                tbl1.Rows[j]["notes"] = notes;
                tbl1.Rows[j]["crperiod"] = crperiod;
            }

            Session["tblterm"] = tbl1;

        }
        protected void lbtnMSRSelect_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            DataTable tbl1 = (DataTable)Session["tblt02"];
            DataTable tblreq = (DataTable)Session["tblreq01"];

            string comcod = this.GetCompCode();

            foreach (ListItem s1 in chkMSRRes.Items)
            {
                if (s1.Selected)
                {
                    string mResCode1 = s1.Value.ToString();
                    //string mResCode1 = "000000000000";//this.ddlMSRRes.SelectedValue.ToString();
                    //string spcfcod1 = "000000000000";// this.ddlSpecificationms.SelectedValue.ToString();

                    string mResCode2 = ASTUtility.Left(mResCode1, 12).ToString();
                    string flrcod = mResCode1.ToString().Length > 12 ? ASTUtility.Right(mResCode1, 3).ToString() : "";

                    DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode2 + "' and flrcod='" + flrcod + "' ");
                    if (dr2.Length == 0)
                    {
                        DataRow dr1 = tbl1.NewRow();
                        dr1["rsircode"] = mResCode2;
                        dr1["rsirdesc1"] = tblreq.Select("rsircode = '" + mResCode2 + "' and flrcod='" + flrcod + "' ")[0]["rsirdesc1"];
                        dr1["spcfcod"] = "";
                        dr1["spcfdesc"] = "";// this.ddlSpecificationms.SelectedItem.Text.Trim();
                        dr1["flrcod"] = flrcod;
                        dr1["flrdesc"] = tblreq.Select("rsircode = '" + mResCode2 + "' and flrcod='" + flrcod + "' ")[0]["flrdesc"];

                        dr1["qty"] = (((DataTable)Session["tblreq01"]).Select("rsircode = '" + mResCode2 + "' and flrcod='" + flrcod + "' "))[0]["qty"];
                        dr1["bgdrat"] = (((DataTable)Session["tblreq01"]).Select("rsircode = '" + mResCode2 + "' and flrcod='" + flrcod + "' "))[0]["bgdrat"];
                        dr1["resrate1"] = 0;
                        dr1["resrate2"] = 0;
                        dr1["resrate3"] = 0;
                        dr1["resrate4"] = 0;
                        dr1["resrate5"] = 0;
                        dr1["amt1"] = 0;
                        dr1["amt2"] = 0;
                        dr1["amt3"] = 0;
                        dr1["amt4"] = 0;
                        dr1["amt5"] = 0;

                        DataTable tbl2 = (DataTable)Session["tblMat"];
                        DataRow[] dr5 = tbl2.Select("rsircode = '" + mResCode1 + "'");
                        dr1["rsirunit"] = dr5[0]["rsirunit"];
                        dr1["aprovrate"] = dr5[0]["aprovrate"];
                        dr1["msrrmrk"] = "";
                        tbl1.Rows.Add(dr1);
                    }
                }
            }
            Session["tblt02"] = (comcod == "3101" || comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "3370") ? tbl1 : this.HiddenSameData(tbl1);   //tblMSR
            this.gvMSRInfo_DataBind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "rsircode";
            //dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    dt1.Rows[j]["rsirdesc1"] = "";
                }
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }
            DataView dv = dt1.DefaultView;
            dv.Sort = ("rsircode");
            dt1 = dv.ToTable();
            return dt1;
        }


        protected void lbtnMSRSup_Click(object sender, EventArgs e)
        {

            DataTable tbl1 = (DataTable)Session["tblt01"];
            DataTable tblt = (DataTable)Session["tblterm"];
            string mSuplCode = this.ddlMSRSupl.SelectedValue.ToString();

            //  Session["Supplier"]
            DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "'");

            if (dr2.Length == 0)
            {
                if (tbl1.Rows.Count < 5)
                {
                    string ssircode = this.ddlMSRSupl.SelectedValue.ToString();
                    DataRow dr1 = tbl1.NewRow();
                    dr1["ssircode"] = this.ddlMSRSupl.SelectedValue.ToString();
                    dr1["ssirdesc1"] = this.ddlMSRSupl.SelectedItem.Text.Trim().Substring(15);
                    tbl1.Rows.Add(dr1);

                    // Term

                    DataRow drt = tblt.NewRow();
                    drt["ssircode"] = this.ddlMSRSupl.SelectedValue.ToString();
                    drt["ssirdesc"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + ssircode + "'"))[0]["ssirdesc"];
                    drt["discount"] = 0.00;
                    drt["ccharge"] = "";
                    drt["payterm"] = "";
                    drt["qutdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    drt["worktime"] = "";
                    drt["notes"] = "";
                    drt["crperiod"] = "";
                    tblt.Rows.Add(drt);

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Suppalyer can't more than 5 !";

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }

            }


            Session["tblt01"] = (DataTable)tbl1;
            Session["tblterm"] = (DataTable)tblt;
            this.Payterm_DataBind();
            this.Recom_Bind();
        }

        private void Recom_Bind()
        {
            DataTable dt1 = (DataTable)Session["tblterm"];
            //dt1.Rows.Add("", "000000000000", 0, " ", " ", "Select Recommeded Supplier") ;
            this.ddlrecomsup.DataTextField = "ssirdesc";
            this.ddlrecomsup.DataValueField = "ssircode";
            this.ddlrecomsup.DataSource = dt1;
            this.ddlrecomsup.DataBind();
            // this.ddlrecomsup.SelectedValue = "000000000000";
            ListItem li = new ListItem();
            li.Text = "Select Recommeded Supplier";
            li.Value = "000000000000";
            ddlrecomsup.Items.Add(li);
            this.ddlrecomsup.SelectedValue = "000000000000";
        }

        private void Payterm_DataBind()
        {
            this.gvterm.DataSource = (DataTable)Session["tblterm"];
            this.gvterm.DataBind();

            switch (GetCompCode())
            {
                case "3101":
                case "3370":
                    this.gvterm.Columns[4].Visible = false;
                    this.gvterm.Columns[6].Visible = false;
                    this.gvterm.Columns[8].Visible = false;
                    this.gvterm.Columns[9].Visible = false;
                    this.gvterm.Columns[5].HeaderText = "VAT & TAX";
                    break;
                default:
                    this.gvterm.Columns[4].Visible = true;
                    this.gvterm.Columns[6].Visible = true;
                    this.gvterm.Columns[8].Visible = true;
                    this.gvterm.Columns[9].Visible = true;
                    break;
            }
        }

        //protected void ddlMSRRes_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    this.GetSpecification01();
        //    //supcount = 1;
        //    //this.lblmsg1.Text = "";
        //}
        protected void gvMSRInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtrate1 = (TextBox)e.Row.FindControl("txtrate1");
                TextBox txtrate2 = (TextBox)e.Row.FindControl("txtrate2");
                TextBox txtrate3 = (TextBox)e.Row.FindControl("txtrate3");
                TextBox txtrate4 = (TextBox)e.Row.FindControl("txtrate4");
                TextBox txtrate5 = (TextBox)e.Row.FindControl("txtrate5");


                Label txtamt1 = (Label)e.Row.FindControl("lblgvAmount1");
                Label txtamt2 = (Label)e.Row.FindControl("lblgvAmount2");
                Label txtamt3 = (Label)e.Row.FindControl("lblgvAmount3");
                Label txtamt4 = (Label)e.Row.FindControl("lblgvAmount4");
                Label txtamt5 = (Label)e.Row.FindControl("lblgvAmount5");

                TextBox txtbgdrat = (TextBox)e.Row.FindControl("txtgvMSRbgdrat");

                double bdgrate = Convert.ToDouble("0" + txtbgdrat.Text.Trim());
                double rate1 = Convert.ToDouble("0" + txtrate1.Text.Trim());
                double rate2 = Convert.ToDouble("0" + txtrate2.Text.Trim());
                double rate3 = Convert.ToDouble("0" + txtrate3.Text.Trim());
                double rate4 = Convert.ToDouble("0" + txtrate4.Text.Trim());
                double rate5 = Convert.ToDouble("0" + txtrate5.Text.Trim());


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(code, 2) == "71")
                {
                    txtrate1.Style.Add("text-align", "Left");
                    txtrate2.Style.Add("text-align", "Left");
                    txtrate3.Style.Add("text-align", "Left");
                    txtrate4.Style.Add("text-align", "Left");
                    txtrate5.Style.Add("text-align", "Left");
                }
                if (rate1 > bdgrate)
                {
                    txtrate1.ForeColor = Color.Red;
                    txtamt1.ForeColor = Color.Red;
                }
                if (rate2 > bdgrate)
                {
                    txtrate2.ForeColor = Color.Red;
                    txtamt2.ForeColor = Color.Red;
                }
                if (rate3 > bdgrate)
                {
                    txtrate3.ForeColor = Color.Red;
                    txtamt3.ForeColor = Color.Red;
                }
                if (rate4 > bdgrate)
                {
                    txtrate4.ForeColor = Color.Red;
                    txtamt4.ForeColor = Color.Red;
                }
                if (rate5 > bdgrate)
                {
                    txtrate5.ForeColor = Color.Red;
                    txtamt5.ForeColor = Color.Red;
                }

            }
        }
        protected void ImgbtnFindSpecificationms_Click(object sender, EventArgs e)
        {

        }

        protected void gvMSRInfo2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 7;
                gvrow.Cells.Add(cell0);


                DataTable dt = (DataTable)Session["tblt01"];
                //int j = 5;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TableCell cell = new TableCell();
                    cell.Text = dt.Rows[i]["ssirdesc1"].ToString();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    gvrow.Cells.Add(cell);

                    //if (j == 30)
                    //    break;


                }


                TableCell celll = new TableCell();
                celll.Text = "";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 2;
                gvrow.Cells.Add(celll);



                //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
                //  i++;


                gvMSRInfo2.Controls[0].Controls.AddAt(0, gvrow);
            }







        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            this.SaveTermValue();
            this.gvMSRInfo_DataBind();
            this.Payterm_DataBind();
        }
        protected void lbtnMrsnodelete_Click(object sender, EventArgs e)
        {

            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblt02"];

            string curdate = Convert.ToDateTime(this.txtCurMSRDate.Text).ToString("dd-MMM-yyyy");
            string mrsno = this.lblCurMSRNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurMSRNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.ToString().Trim();
            string rsircode = ((Label)this.gvMSRInfo2.Rows[rownum].FindControl("lblgvrsircode")).Text.Trim();       //lblgvrsircode  this.ddlProjectName.SelectedValue.ToString();
            string spcfcode = ((Label)this.gvMSRInfo2.Rows[rownum].FindControl("lblgvspcfcode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEMRSNO02_CON",
                       mrsno, rsircode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


            DataView dv = dt.DefaultView;
            Session.Remove("tblt02");
            Session["tblt02"] = dv.ToTable();
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnSupplierdelete_Click(object sender, EventArgs e)
        {
            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblterm"];

            string curdate = Convert.ToDateTime(this.txtCurMSRDate.Text).ToString("dd-MMM-yyyy");
            string mrsno = this.lblCurMSRNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurMSRNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.ToString().Trim();
            string ssircode = ((Label)this.gvterm.Rows[rownum].FindControl("lblgvssircode")).Text.Trim();       //lblgvrsircode  this.ddlProjectName.SelectedValue.ToString();




            bool result;
            DataTable dt02 = (DataTable)Session["tblt02"];

            for (int j = 0; j < dt02.Rows.Count; j++)
            {
                if (rownum == 0)
                {
                    dt02.Rows[j]["resrate1"] = 0.00;
                    dt02.Rows[j]["amt1"] = 0.00;
                }
                else if (rownum == 1)
                {
                    dt02.Rows[j]["resrate2"] = 0.00;
                    dt02.Rows[j]["amt2"] = 0.00;

                }
                else if (rownum == 2)
                {
                    dt02.Rows[j]["resrate3"] = 0.00;
                    dt02.Rows[j]["amt3"] = 0.00;

                }
                else if (rownum == 3)
                {
                    dt02.Rows[j]["resrate4"] = 0.00;
                    dt02.Rows[j]["amt4"] = 0.00;

                }
                else if (rownum == 4)
                {
                    dt02.Rows[j]["resrate5"] = 0.00;
                    dt02.Rows[j]["amt5"] = 0.00;

                }
            }

            DataTable dt01 = (DataTable)Session["tblt01"];

            if (dt01.Rows.Count > 0)
            {
                DataRow[] dtr = dt01.Select("ssircode = '" + ssircode + "'");
                foreach (DataRow dr in dtr)
                {
                    dt01.Rows.Remove(dr);
                }
                dt01.AcceptChanges();
                Session.Remove("tblt01");
                Session["tblt01"] = dt01;
                this.gvMSRInfo_DataBind();
            }

            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEMRS02CN_CON",
                 mrsno, ssircode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
                DataView dv = dt.DefaultView;
                Session.Remove("tblterm");
                Session["tblterm"] = dv.ToTable();
                this.Payterm_DataBind();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void lbtnSameValue_Click(object sender, EventArgs e)
        {
            string Sup1 = "A";
            Session_tblMSR_Update_PutSameValue(Sup1);            
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnSameValueB_Click(object sender, EventArgs e)
        {
            string Sup1 = "B";
            Session_tblMSR_Update_PutSameValue(Sup1);
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnSameValueC_Click(object sender, EventArgs e)
        {
            string Sup1 = "C";
            Session_tblMSR_Update_PutSameValue(Sup1);
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnSameValueD_Click(object sender, EventArgs e)
        {
            string Sup1 = "D";
            Session_tblMSR_Update_PutSameValue(Sup1);
            this.gvMSRInfo_DataBind();
        }
        protected void lbtnSameValueE_Click(object sender, EventArgs e)
        {
            string Sup1 = "E";
            Session_tblMSR_Update_PutSameValue(Sup1);
            this.gvMSRInfo_DataBind();
        }
        protected void Session_tblMSR_Update_PutSameValue(string Sup1)
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tblt02"];
                tbl1.DefaultView.Sort = "rsircode ASC, flrcod ASC";
                tbl1 = tbl1.DefaultView.ToTable();

                string Rescode = "";
                double ResQty = 0;
                double ResRat = 0;
                int RowIndex = 0;
                for (int i = 0; i < this.gvMSRInfo2.Rows.Count; i++)
                {
                 

                    if (Sup1 == "A")
                    {
                        if (i == 0)
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate1")).Text.Trim());
                        }
                        if (Rescode == ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim())
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            
                            tbl1.Rows[i]["resrate1"] = ResRat;
                        }
                        else
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate1")).Text.Trim());
                            
                            tbl1.Rows[i]["resrate1"] = ResRat;
                        }
                    }
                    else if (Sup1 == "B")
                    {
                        if (i == 0)
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate2")).Text.Trim());
                        }
                        if (Rescode == ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim())
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            
                            tbl1.Rows[i]["resrate2"] = ResRat;
                        }
                        else
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate2")).Text.Trim());
                            
                            tbl1.Rows[i]["resrate2"] = ResRat;
                        }
                    }

                    else if (Sup1 == "C")
                    {
                        if (i == 0)
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate3")).Text.Trim());
                        }
                        if (Rescode == ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim())
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                             
                            tbl1.Rows[i]["resrate3"] = ResRat;
                        }
                        else
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate3")).Text.Trim());
                           
                            tbl1.Rows[i]["resrate3"] = ResRat;
                        }
                    }

                    else if (Sup1 == "D")
                    {
                        if (i == 0)
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate4")).Text.Trim());
                        }
                        if (Rescode == ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim())
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            
                            tbl1.Rows[i]["resrate4"] = ResRat;
                        }
                        else
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate4")).Text.Trim());
                            
                            tbl1.Rows[i]["resrate4"] = ResRat;
                        }
                    }
                    else if (Sup1 == "E")
                    {
                        if (i == 0)
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate5")).Text.Trim());
                        }
                        if (Rescode == ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim())
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            
                            tbl1.Rows[i]["resrate5"] = ResRat;
                        }
                        else
                        {
                            Rescode = ((Label)this.gvMSRInfo2.Rows[i].FindControl("lblgvrsircode")).Text.Trim();
                            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[i].FindControl("txtrate5")).Text.Trim());
                            
                            tbl1.Rows[i]["resrate5"] = ResRat;
                        }
                    }



                }

                Session["tblt02"] = tbl1;
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

       
    }
}