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
namespace RealERPWEB.F_22_Sal
{
    public partial class SaleSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Competitive Price Survey";
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.GetProjectInfo();



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectInfo()
        {
            try
            {
                ViewState.Remove("tblproinfo");
                ViewState.Remove("tblproginfo");
                string comcod = this.GetCompCode();
                string txtSProject = "%%";
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETCOMSURPROJECTINFO", txtSProject, "", "", "", "", "", "", "", "");
                this.chkProjectName.DataTextField = "pactdesc";
                this.chkProjectName.DataValueField = "pactcode";
                this.chkProjectName.DataSource = ds1.Tables[0];
                this.chkProjectName.DataBind();
                ViewState["tblproinfo"] = ds1.Tables[0];
                ViewState["tblproginfo"] = ds1.Tables[2];


            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }




        }


        protected void Resource_List(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST1", pmSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {



            if (this.lbtnMSROk.Text == "Ok")
            {
                this.lblprevious.Visible = false;
                this.ddlPrevMSRList.Visible = false;
                this.lblprojectname.Visible = true;
                this.chkProjectName.Visible = true;
                this.lbtnSelect.Visible = true;

                this.lbtnMSROk.Text = "New";
                this.Get_Survey_Info();
                return;
            }

            this.lblprevious.Visible = true;
            this.ddlPrevMSRList.Visible = true;
            this.ddlPrevMSRList.Items.Clear();
            this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
            this.txtCurMSRDate.Enabled = true;
            this.lblprojectname.Visible = false;
            this.chkProjectName.Visible = false;
            this.lbtnSelect.Visible = false;
            this.gvMSRInfo2.DataSource = null;
            this.gvMSRInfo2.DataBind();

            this.lbtnMSROk.Text = "Ok";





        }

        protected void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            // string date = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).ToString("dd-MMM-yyyy");
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                this.txtCurMSRDate.Enabled = false;
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETCOMSURVEYINFO", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblt02"] = ds1.Tables[0];
            Session["tblsalsurvery"] = ds1.Tables[1];
            //Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);


            if (mMSRNo == "NEWMSR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETLASTSALSURVEYNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxmsrno"].ToString();

                }
                return;
            }
            this.lblCurMSRNo1.Text = ds1.Tables[1].Rows[0]["surveyno"].ToString();
            this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
            this.gvMSRInfo_DataBind();


        }

        protected void gvMSRInfo_DataBind()
        {


            this.gvMSRInfo2.DataSource = (DataTable)Session["tblt02"];
            this.gvMSRInfo2.DataBind();

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
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
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string comments = "";
            string printdate = System.DateTime.Now.ToString("dd/MM/yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string usrname = ((DataTable)Session["tblsalsurvery"]).Rows[0]["username"].ToString();
            string usrdesig = ((DataTable)Session["tblsalsurvery"]).Rows[0]["usrdesig"].ToString();

           // string usernameadesig = usrname + "/" + usrdesig;
           // string mMSRNo = this.Request.QueryString["msrno"].ToString() == "" ? "" : this.Request.QueryString["msrno"].ToString();

            LocalReport Rpt1 = new LocalReport();


            //string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;
            //string surveyNo = mMSRNo;

            DataTable dtdetails = (DataTable)Session["tblt02"];

            var lst = dtdetails.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesSurveyEntry>();
            string developer = "";
            string proaddress = "";

            foreach (var lst1 in lst)
            {
                developer = developer + lst1.companyname + ", ";
                proaddress = proaddress + lst1.location + ", ";



            }
            developer = developer.Length > 0 ? developer.Substring(0, developer.Length - 2) : developer;
            proaddress = proaddress.Length > 0 ? proaddress.Substring(0, proaddress.Length - 2) : proaddress;



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptComSalesServey", lst, null, null);
            Rpt1.EnableExternalImages = true;
            DataTable dt = (DataTable)Session["tblt01"];

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            
            Rpt1.SetParameters(new ReportParameter("SurveyDate", printdate));
            Rpt1.SetParameters(new ReportParameter("PurposeOfSurvey", "Price Survey"));
            Rpt1.SetParameters(new ReportParameter("AreaOfSurvey", proaddress));
            Rpt1.SetParameters(new ReportParameter("TxtCompCompare", developer));
            Rpt1.SetParameters(new ReportParameter("CitySurvey", "Chattogram"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Price Survey"));
            Rpt1.SetParameters(new ReportParameter("comments", comments));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("PrepName", usrname));
            Rpt1.SetParameters(new ReportParameter("PrepDesig", usrdesig));
            
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        private void GetLastSurverNo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETLASTSALSURVEYNO", CurDate1,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {

                this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();
                this.ddlPrevMSRList.DataTextField = "maxmsrno1";
                this.ddlPrevMSRList.DataValueField = "maxmsrno";
                this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                this.ddlPrevMSRList.DataBind();


            }
        }

        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            bool result;
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            this.Session_tblMSR_Update();

            //  string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count == 0)
                this.GetLastSurverNo();
           string mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "INSERTORUPDATESALSURAB", "SALSURB",
                             mMSRNO, mMSRDAT, userid, Terminal, Sessionid, Date, "", "", "", "", "", "", "", "");



            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataTable tbl1 = (DataTable)Session["tblt02"];
            foreach (DataRow dr2 in tbl1.Rows)
            {
                string pactcode = dr2["pactcode"].ToString();
                string comments = dr2["comments"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "INSERTORUPDATESALSURAB", "SALSURA",
                mMSRNO, pactcode, comments, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "";
                string eventdesc = "Update Survey";
                string eventdesc2 = mMSRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        protected void Session_tblMSR_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblt02"];
            int TblRowIndex2;
            int j = 0;
            foreach (GridViewRow gv1 in this.gvMSRInfo2.Rows)
            {
                string comments = ((TextBox)gv1.FindControl("txtgvcomments")).Text.Trim();
                TblRowIndex2 = (this.gvMSRInfo2.PageIndex) * this.gvMSRInfo2.PageSize + j;
                tbl1.Rows[TblRowIndex2]["comments"] = comments;
                j++;
            }


            Session["tblt02"] = tbl1;
        }


        protected void lnkbtnFindPreMR_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETPOREVIOUSMSRNO", "",
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {

                
                this.ddlPrevMSRList.DataTextField = "surveyno1";
                this.ddlPrevMSRList.DataValueField = "surveyno";
                this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                this.ddlPrevMSRList.DataBind();


            }


        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            string pactcode = "";

            DataTable dt = (DataTable)Session["tblt02"];
            DataTable dt1 = (DataTable)ViewState["tblproinfo"];

            foreach (ListItem s1 in chkProjectName.Items)
            {
                if (s1.Selected)
                {
                    pactcode = s1.Value;

                    //select comcod, pactcode, location, aptsize, landarea, storied, aptunit, askingprice, selprice, utlityprice, prkingpirce, hoverdate, procatagory,
                    //  buildtype, pactdesc, companyname



                    DataRow[] dr2 = dt.Select("pactcode = '" + pactcode + "'");
                    if (dr2.Length == 0)
                    {
                        DataRow[] dr1 = dt1.Select("pactcode='" + pactcode + "'");
                        dt.ImportRow(dr1[0]);

                        //DataRow dr1 = dt.NewRow();
                        //dr1["rsircode"] = this.ddlMSRRes.SelectedValue.ToString();
                        //dr1["rsirdesc1"] = this.ddlMSRRes.SelectedItem.Text.Trim();
                        //dr1["spcfcod"] = this.ddlSpecificationms.SelectedValue.ToString();
                        //dr1["spcfdesc"] = this.ddlSpecificationms.SelectedItem.Text.Trim();

                        //dr1["qty"] = 0;
                        //dr1["resrate1"] = 0;
                        //dr1["resrate2"] = 0;
                        //dr1["resrate3"] = 0;
                        //dr1["resrate4"] = 0;
                        //dr1["resrate5"] = 0;
                        //dr1["amt1"] = 0;
                        //dr1["amt2"] = 0;
                        //dr1["amt3"] = 0;
                        //dr1["amt4"] = 0;
                        //dr1["amt5"] = 0;

                        //DataTable tbl2 = (DataTable)Session["tblMat"];
                        //DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                        //dr1["rsirunit"] = dr3[0]["rsirunit"];
                        //dr1["aprovrate"] = dr3[0]["aprovrate"];
                        //dr1["msrrmrk"] = "";
                        //tbl1.Rows.Add(dr1);
                    }







                }
            }

            Session["tblt02"] = dt;
            this.gvMSRInfo_DataBind();




        }













        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            this.gvMSRInfo_DataBind();


        }
      

  






    }
}