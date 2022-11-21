using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.Net;
using System.IO;
using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using EASendMail;

namespace RealERPWEB.F_12_Inv

{
    public partial class PurReqApproval : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        public static int PageNumber = 0;

        public static int i, j;
        public static string Url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate_CalendarExtender.EndDate = System.DateTime.Today;


                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string reqapproval = this.GetReqApproval();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "RateInput") ? "Rate Proposal" : reqapproval;
                this.Master.Page.Title = (Request.QueryString["Type"].ToString() == "RateInput") ? "Rate Proposal" : reqapproval;




                //: (Request.QueryString["Type"].ToString() == "Approval") ? "Requisition Approval" 

                // ((Label)this.Master.FindControl("lblTitle")).Text = "Rate Proposal";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.VisibleGrid();
                this.GetProjectName();
                this.GetStoreName();
                this.GetPayType();
                this.createTable();

                this.lnkOk_Click(null, null);

            }

        }


        private string GetReqApproval()
        {
            string comcod = this.GetCompCode();
            string reqcheck = "";

            switch (comcod)
            {
                case "3336":
                case "3340":
                    reqcheck = "Rate Approval";

                    break;
                default:
                    reqcheck = "Requisition Approval";
                    break;
            }
            return reqcheck;

        }

        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void VisibleGrid()
        {

            string Type = Request.QueryString["Type"].ToString();
            this.lblmrfno.Text = this.ReadCookie();
            this.dgv1.Columns[7].HeaderText = this.ReadCookie();
            switch (Type)
            {
                case "Approval":
                    this.dgv1.Columns[12].Visible = true;
                    this.dgv1.Columns[14].Visible = true;
                    //this.dgv1.Columns[15].Visible = true;// boq
                    this.dgv1.Columns[19].Visible = true;
                    this.dgv1.Columns[20].Visible = true;
                    this.dgv1.Columns[21].Visible = true;
                    this.dgv1.Columns[22].Visible = true;

                    break;

                case "VenSelect":

                    this.dgv1.Columns[12].Visible = true;
                    this.dgv1.Columns[14].Visible = true;
                    this.dgv1.Columns[20].Visible = true;
                    // this.dgv1.Columns[20].Visible = true;
                    //this.dgv1.Columns[21].Visible = true;
                    break;
            }

        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetStoreName()
        {
            ViewState.Remove("tblstorename");
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSTORENAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblstorename"] = ds1.Tables[0];

        }
        private void GetPayType()
        {

            ViewState.Remove("tblpaytype");
            string comcod = this.GetCompCode();
            DataSet dsP = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETPAYTYPE", "", "", "", "", "", "", "", "", "");
            if (dsP == null)
                return;
            ViewState["tblpaytype"] = dsP.Tables[0];

        }
        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            string srchproject = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtProjectSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETPROJECTNAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();
            if (this.Request.QueryString["prjcode"].ToString().Length > 0)
                this.ddlProject.SelectedValue = this.Request.QueryString["prjcode"].ToString();




        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.pnlGridPage.Visible = true;
            this.pnlApproval.Visible = (this.Request.QueryString["Type"] == "Approval") ? true : false;
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            if (dt == null)
            {
                return;
            }
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            ddlBestSupplierinfo();
        }

        private string RateorApproved()
        {
            string comcod = this.GetCompCode();
            string Approval = this.Request.QueryString["Type"].ToString();
            switch (comcod)
            {

                case "1301":
                case "2301":
                case "3301":
                    Approval = (Approval == "Approval") ? "RateInput" : Approval;

                    break;
                default:
                    break;
            }

            return Approval;



        }


        private void ShowData()
        {


            try
            {
                Session.Remove("tblreq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchChequeno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtserchmrf.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";

                int startRow = PageNumber * 100;
                int endRow = (PageNumber + 1) * 100;
                string pactcode = ((this.ddlProject.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProject.SelectedValue.ToString()) + "%";
                string Approval = this.RateorApproved();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "SHOWREQUISITON", Date, SrchChequeno, startRow.ToString(), endRow.ToString(), pactcode, Approval, "", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }

                this.lblreqnaration.Text = "Req Narration : " + ds1.Tables[0].Rows[0]["narration"].ToString();

                //string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                //DataSet ds1a = new DataSet("ds1");
                //System.IO.StringReader xmlSR = new System.IO.StringReader(xmlDS);                         
                //ds1a.ReadXml(xmlSR);
                //ds1a.Tables[0].TableName = "tbl1";


                // DataTaowsble dt = (DataTable) ds1.Tables.r[0]["approval"];


                Session["tblreq"] = this.HiddenSameDate(ds1.Tables[0]);
                Session["tbltopage"] = ds1.Tables[1];

                if (Request.QueryString["Type"].ToString() == "Approval")
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        this.GetApprQty();
                    }
                }

                string msrno = ((DataTable)Session["tblReq"]).Rows[0]["msrno"].ToString();

                if (msrno != "Survey")
                {
                    this.ShowMarketSurvey(msrno);

                }
                this.Data_Bind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        private string CompanyLengh()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {

                case "1205": //p2p
                case "3351": //p2p
                case "3352": //p2p
                case "3101":
                case "3353": //manama
                case "3370": //cpdl
                    length = "length";

                    break;
                default:
                    break;
            }

            return length;



        }
        private void ShowMarketSurvey(string msrno)
        {
            this.lblsurveyby.Visible = true;
            string comcod = this.GetCompCode();
            string length = this.CompanyLengh();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", msrno, "", length,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = HiddenSameData1(ds1.Tables[0]);

            if (this.Request.QueryString["Type"].ToString() == "Approval")
            {
                this.gvMSRInfo.Visible = true;
            }
            this.gvMSRInfo.DataSource = dt1;
            this.gvMSRInfo.DataBind();
            this.lblsurveyby.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : "Survey Completed By: " + ds1.Tables[1].Rows[0]["username"].ToString();


        }


        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }



        private void GetApprQty()
        {
            DataTable dt = (DataTable)Session["tblreq"];
            double apramt = 0.00, boqamt = 0.00;
            double aprqty = 0.00, reqrat = 0.00, bgdrat = 0.00;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gpsl = dt.Rows[i]["gpsl"].ToString();

                if (gpsl == "2")
                {
                    dt.Rows[i]["areqamt"] = apramt;
                    dt.Rows[i]["bgdreqamt"] = boqamt;
                    apramt = 0.00;
                    boqamt = 0.00;
                    continue;
                }
                aprqty = Convert.ToDouble(dt.Rows[i]["preqty"]);
                reqrat = Convert.ToDouble(dt.Rows[i]["reqrat"]);
                bgdrat = Convert.ToDouble(dt.Rows[i]["bgdrat"]);
                dt.Rows[i]["areqty"] = aprqty;
                dt.Rows[i]["areqamt"] = aprqty * reqrat;
                dt.Rows[i]["bgdreqamt"] = aprqty * bgdrat;
                apramt += aprqty * reqrat;
                boqamt += aprqty * bgdrat;


            }
            Session["tblreq"] = dt;


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblreq"];

            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            if (dt.Rows.Count > 0)
            {

                ((TextBox)this.dgv1.Rows[0].FindControl("txtgvsupRat")).Focus();
                ((LinkButton)this.dgv1.FooterRow.FindControl("lbtnFinalUpdate")).Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "VenSelect")
                                                                                           || (this.Request.QueryString["Type"].ToString().Trim() == "RateInput") || (this.Request.QueryString["Type"].ToString().Trim() == "FirstRecom") || (this.Request.QueryString["Type"].ToString().Trim() == "SecRecom") || (this.Request.QueryString["Type"].ToString().Trim() == "ThirdRecom"));
            }
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string reqno = ((Label)dgv1.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                string rsircode = ((Label)dgv1.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                string spcfcod = ((Label)dgv1.Rows[i].FindControl("lblgvSpcfCod")).Text.Trim();
                string gpsl = ((Label)dgv1.Rows[i].FindControl("lblgvgpsl")).Text.Trim();
                //double rate =this.dgv1.Rows[i].FindControl("txtgvsupRat").Focus();


                ((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                ((LinkButton)dgv1.Rows[i].FindControl("lbok")).Enabled = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                ((TextBox)dgv1.Rows[i].FindControl("txtgvappQty")).Visible = (gpsl == "1");
                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                //LinkButton lbtn02 = (LinkButton)dgv1.Rows[i].FindControl("lbtnSurvey");
                //string msrno = ((LinkButton)dgv1.Rows[i].FindControl("lbtnSurvey")).Text;



                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = reqno + rsircode + spcfcod;


                //if (lbtn02 != null)
                //    if (lbtn02.Text.Trim().Length > 0)
                //        lbtn02.CommandArgument = msrno;
            }

            switch (this.GetCompCode())
            {
                case "1205":
                case "3351":
                case "3352":
                case "3101":
                case "3353"://Manama
                case "3354"://Edison
                    string reqno = this.Request.QueryString["genno"].ToString();
                    string rescode = String.Empty;
                    DataTable dtreq = (DataTable)Session["tblreq"];
                    DataTable dtn = dtreq.Copy();
                    DataView dv = dtn.DefaultView;
                    dv.RowFilter = "reqsrat>0";
                    dtn = dv.ToTable();
                    foreach (DataRow dr in dtn.Rows)
                    {
                        rescode += dr["rsircode"].ToString() + dr["spcfcod"].ToString();
                    }
                    ((HyperLink)dgv1.FooterRow.FindControl("HypMakeSurvey")).Visible = true;
                    ((HyperLink)dgv1.FooterRow.FindControl("HypMakeSurvey")).NavigateUrl = "~/F_12_Inv/LinkMktSurvey.aspx?reqno=" + reqno + rescode;
                    break;
                default:
                    break;
            }

            if (this.Request.QueryString["Type"].ToString() == "RateInput")
            {
                switch (this.GetCompCode())
                {

                    case "1205":
                    case "3351":
                    case "3352":
                    case "3101":

                        this.dgv1.Columns[14].Visible = true;
                        this.dgv1.Columns[19].Visible = true;
                        break;

                    default:
                        this.dgv1.Columns[14].Visible = false;
                        this.dgv1.Columns[19].Visible = false;
                        break;
                }
            }

        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string reqno = dt1.Rows[0]["reqno"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            int j;



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[0]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["mrfno"] = "";
                    dt1.Rows[j]["reqdat"] = "";

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                    dt1.Rows[j]["rsirunit"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    {
                        dt1.Rows[j]["reqno1"] = "";
                        dt1.Rows[j]["mrfno"] = "";
                        dt1.Rows[j]["reqdat"] = "";


                    }

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";



                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";
                        dt1.Rows[j]["rsirunit"] = "";


                    }



                }

                pactcode = dt1.Rows[j]["pactcode"].ToString();
                reqno = dt1.Rows[j]["reqno"].ToString();
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }





            return dt1;


            //grpcode = dt1.Rows[0]["grpcode"].ToString();
            //            string actcode = dt1.Rows[0]["actcode"].ToString();
            //            for (j = 1; j < dt1.Rows.Count; j++)
            //            {
            //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
            //                {
            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();
            //                    dt1.Rows[j]["grpdesc"] = "";
            //                    dt1.Rows[j]["actdesc"] = "";

            //                }

            //                else
            //                {


            //                    if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
            //                    {
            //                        dt1.Rows[j]["grpdesc"] = "";
            //                    }
            //                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //                    {
            //                        dt1.Rows[j]["actdesc"] = "";
            //                    }

            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();

            //                }

            //            }
            //        break;

            //}


            //  return dt1;



        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblreq"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                dt.Rows[i]["chkmv"] = chkmr;

                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblreq"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {
            try
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                this.CheckValue();
                // for Update
                this.Session_tblReq_Update();
                // this.Data_Bind();


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ApprovByid = hst["usrid"].ToString();
                string Approvtrmid = hst["compname"].ToString();
                string ApprovSession = hst["session"].ToString();
                string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string reqresaspcfcod = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
                string Reqno = reqresaspcfcod.Substring(0, 14);
                string rsircode = reqresaspcfcod.Substring(14, 12);
                string spcfcod = reqresaspcfcod.Substring(26);
                string approved = "Ok";

                //Individual



                DataTable dt = (DataTable)Session["tblreq"];
                DataTable dt1 = dt.Copy();
                DataTable dti;

                DataView dvi = dt1.DefaultView;
                dvi.RowFilter = ("reqno='" + Reqno + "' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "'");
                dti = dvi.ToTable();
                // DataRow []drind=dt1.Select(("reqno='" + Reqno + "' and rsircode='"+rsircode+"' and spcfcod='"+spcfcod+"'"));

                if (dti.Rows[0]["chkmv"].ToString() == "False")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
                    return;

                }

                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("reqno='" + Reqno + "' and  chkmv=True");
                dt1 = dv.ToTable();
                string mrfno = dt1.Rows[0]["mrfno"].ToString();


                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string ptype = dt1.Rows[i]["ptype"].ToString();
                    string ssircode = dt1.Rows[i]["ssircode"].ToString().Trim();

                    if (ptype == "002" && ssircode == "")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Supplier Name');", true);
                        return;

                    }
                }




                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "REQAPPROVED", Reqno, rsircode, spcfcod, "", "", "", "", "", "");

                if (ds4.Tables[0].Rows.Count > 0)
                    if (ds4.Tables[0].Rows[0]["approved"].ToString() != "")
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Requisition Number Already Approved";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }




                bool result = false;
                // string ptype = dti.Select("reqno='" + Reqno + "'")[0]["ptype"].ToString();
                string carring = Convert.ToDouble("0" + this.txtCarring.Text.Trim()).ToString();

                //update paytype

                result = accData.UpdateTransInfo3(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQAPPROVEDPAYTYPE", Reqno, "", carring, "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                foreach (DataRow dr in dt1.Rows)
                {

                    string gpsl = dr["gpsl"].ToString();
                    if (gpsl == "2") continue;
                    string mRSIRCODE = dr["rsircode"].ToString();
                    string mSPCFCOD = dr["spcfcod"].ToString();
                    string mSPCFCODold = dr["spcfcodo"].ToString();

                    double mPREQTY = Convert.ToDouble(dr["preqty"]);
                    double mAREQTY = Convert.ToDouble(dr["areqty"]);

                    string mREQRAT = dr["reqrat"].ToString();
                    string mEXPUSEDT = dr["expusedt"].ToString();
                    string mREQNOTE = dr["reqnote"].ToString();
                    string PursDate = dr["pursdate"].ToString();
                    string Lpurrate = dr["lpurrate"].ToString();
                    string storecode = dr["storecode"].ToString();
                    string ssircode = dr["ssircode"].ToString();
                    string orderno = dr["orderno"].ToString();

                    string reqsrat = dr["reqsrat"].ToString();
                    string dispercnt = dr["dispercnt"].ToString();
                    string ptype = (dr["ptype"].ToString().Trim() == "") ? "001" : dr["ptype"].ToString();
                    if (mPREQTY >= mAREQTY)
                    {

                        //if (Reqno != Reqno1)
                        //{
                        //    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "DELETEREQNO", Reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                        //    if (!result)
                        //    {
                        //     ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        //        return;
                        //    }


                        //}



                        result = accData.UpdateTransInfo3(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEPURREQAINF", Reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mEXPUSEDT, mREQNOTE,
                                    PursDate, Lpurrate, storecode, ssircode, orderno, ApprovByid, approvdat, Approvtrmid, ApprovSession, approved, reqsrat, dispercnt, ptype, mSPCFCODold, "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }
                    //Reqno1 = Reqno;
                }

                //Auto Purchase Program and Order 


                DataRow[] drt = dt1.Select("ptype='002'");
                if (drt.Length > 0)
                {

                    switch (comcod)
                    {


                        case "3339": // Cash purcahse but check purcahse program and Work order for Tropical
                            break;

                        default:

                            string reqapdate = this.txtdate.Text.Trim();
                            result = accData.UpdateTransInfo3(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "AUTOUPDATEPPROGAORD", Reqno, ApprovByid, approvdat, Approvtrmid, ApprovSession, carring, reqapdate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!result)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                            break;

                    }









                }





                //result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQNO", Reqno, ApprovByid, approvdat, Approvtrmid, ApprovSession, approved, "Approved", "", "", "", "", "", "", "", "");


                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                string reqno = Request.QueryString["genno"].ToString();

                //if (ConstantInfo.LogStatus == true)
                //{

                //    string text = this.ddlProject.SelectedItem.Text + "(" + reqno + ")" + " forwarded to Order Process";
                //    sendSmsFromAPI(text);

                //}


                Session["tblreq"] = dt;
                this.Data_Bind();
                this.CheckValue();


                if (comcod == "3315" || comcod == "3316")
                {

                }


                else
                {
                    if (hst["compsms"].ToString() == "True")
                    {

                        switch (comcod)
                        {
                            case "3333"://


                                break;
                            default:
                                SendSmsProcess sms = new SendSmsProcess();
                                string comnam = hst["comnam"].ToString();
                                string compname = hst["compname"].ToString();
                                string frmname = "PurAprovEntry.aspx?InputType=PurProposal";
                                string SMSHead = "Ready To Order Process, ";
                                string SMSText = comnam + ":\n" + SMSHead + "\n" + "MRF No: " + mrfno;
                                bool resultsms = sms.SendSmms(SMSText, ApprovByid, frmname);
                                break;
                        }



                    }

                }





            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }
        //private void sendSmsFromAPI(string text)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Sesion = hst["session"].ToString();
        //    string userid = hst["usrid"].ToString();
        //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        //    string frmname = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
        //    frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";

        //    DataSet ds3 = accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
        //    if (ds3.Tables[0].Rows.Count == 0)
        //        return;
        //    string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
        //    string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
        //    string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
        //    string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
        //    string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender


        //    string SMSText = text; //        
        //    string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
        //    string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"
        //    for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
        //    {
        //        string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

        //        //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + mobile + "&sms[0][1]=" + System.Web.HttpUtility.UrlEncode(SMSText) + "&sms[0][2]=" + "1234567890" + "&sid=" + sender;
        //        //using (WebClient wc = new WebClient())
        //        //{
        //        //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        //    string HtmlResult = wc.UploadString(ApiUrl, myParameters); Console.Write(HtmlResult);
        //        //}
        //        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&user_password=" + pass + "&route_id=" + routeid
        //           + "&sms_type_id=" + typeid + "&sms_sender=" + sender + "&sms_receiver=" + mobile + "&sms_text=" + SMSText + "&sms_category_name=" + catname);

        //        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //        string responseString = respStreamReader.ReadToEnd();
        //        respStreamReader.Close();
        //        myResp.Close();
        //    }


        //}
        private string CompanyRequisition()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                    // case "3101":
                    PrintReq = "PrintReque02";

                    break;


                case "3336":
                case "3325":
                case "2325":

                    PrintReq = "PrintReque03";

                    break;

                default:
                    PrintReq = "PrintReque01";
                    break;
            }

            return PrintReq;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string printcomreq = this.CompanyRequisition();

            if (printcomreq == "PrintReque01")
            {
                this.PrintRequisition01();

            }

            else if (printcomreq == "PrintReque03")
            {
                this.PrintRequisition03();

            }

            else
            {
                this.PrintRequisition02();



            }


        }


        private void PrintRequisition03()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            // DataTable dt = (DataTable)Session["tblreq"];
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            string search = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtserchmrf.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";

            //string search = this. "%" + txtserchmrf.Text.ToString() + "%";


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETPRINTAPPROVAL", search, "",
                    "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblReq"] = ds1.Tables[0];

            Session["tblUserReq"] = ds1.Tables[1];
            Session["tblreqdesc"] = ds1.Tables[2];


            DataTable dt1 = ds1.Tables[1];


            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;


            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";

            if (comcod == "3330")
            {

                txtSign1 = "Store In-charge";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM (Operation)";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Approved By";
            }


            else
            {

                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM/AGM/DGM";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Managing Director";
            }


            var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry03();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = ds1.Tables[1].Rows[0]["reqno"].ToString();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]

            //DataTable dt = (DataTable)Session["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();

            //DataTable dtr = (DataTable)Session["tblReq"];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;
            //double bgdamt1 = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(bgdamt1)", "")) ? 0.00 : dtr.Compute("Sum(bgdamt1)", "")));
            //double bgdamt2 = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(bgdamt)", "")) ? 0.00 : dtr.Compute("Sum(bgdamt)", "")));

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");




            //TextObject txtbgdamt1 = rptstk.ReportDefinition.ReportObjects["txtbgdamt1"] as TextObject;
            //txtbgdamt1.Text = Convert.ToDouble(bgdamt1).ToString("#,##0.00;(#,##0.00); ");

            //TextObject txtbgdamt2 = rptstk.ReportDefinition.ReportObjects["txtbgdamt2"] as TextObject;
            //txtbgdamt2.Text = Convert.ToDouble(bgdamt2).ToString("#,##0.00;(#,##0.00); ");


            ////TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            ////txttoamt02.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();


            //if (comcod == "3330")
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "Store In-charge";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM (Operation)";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "Approved By";
            //}
            //else
            //{
            //    TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //    txtSign1.Text = "S.K";
            //    TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //    txtSign2.Text = "Project Incharge";
            //    TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //    txtSign3.Text = "DPM/PM/AGM/DGM";
            //    TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //    txtSign4.Text = "Procurement";
            //    TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //    txtSign5.Text = "Cost & Budget";
            //    TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //    txtSign6.Text = "Head Of Construction";
            //    TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //    txtSign7.Text = "DMD";
            //    TextObject txtSign8 = rptstk.ReportDefinition.ReportObjects["txtSign8"] as TextObject;
            //    txtSign8.Text = "Chairman";
            //}


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintRequisition01()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject rpttxtexdeldate = rptstk.ReportDefinition.ReportObjects["eddate"] as TextObject;
            //rpttxtexdeldate.Text = this.txtExpDeliveryDate.Text.Trim();
            //TextObject rpttxtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpttxtadate.Text = this.txtApprovalDate.Text.Trim();
            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtfloornoText = rptstk.ReportDefinition.ReportObjects["floornotext"] as TextObject;
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["floorno"] as TextObject;
            //if (ddlFloor.SelectedValue.ToString().Trim() != "000")
            //{

            //    txtfloornoText.Text = "Floor No:";
            //    txtfloorno.Text = this.ddlFloor.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    txtfloornoText.Text = "";
            //    txtfloorno.Text = " ";
            //}

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);



            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)Session["tblreq"];

            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintRequisition02()
        {



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry02();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(14);

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString();
            //TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            //txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();




            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblreq"]);
            //Session["Report1"] = rptstk;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private string GetResSupplier()
        {
            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3330": //Bridge
                case "3333": //Alliance            
                case "3339": //Tropical            
                    Calltype = "GETAPROVSUPLIST";
                    break;



                default:
                    Calltype = "GETAPROVALLSUPLIST";

                    break;

            }
            return Calltype;


        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lblgvproject");
                Label amt = (Label)e.Row.FindControl("lblgvTAprAmt");
                HyperLink survey = (HyperLink)e.Row.FindControl("hlnkgvSurvey");
                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlStorename");
                DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlSupname");
                DropDownList ddl3 = (DropDownList)e.Row.FindControl("ddlspcfdesc");
                DropDownList ddl4 = (DropDownList)e.Row.FindControl("ddlptype");
                HyperLink resourceLink = (HyperLink)e.Row.FindControl("lblgvResDesc");
                TextBox supRat = (TextBox)e.Row.FindControl("txtgvsupRat");

                LinkButton lbtnok = (LinkButton)e.Row.FindControl("lbok"); 


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gpsl")).ToString().Trim();
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString().Trim();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString().Trim();
                string Storecode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storecode")).ToString().Trim();
                string mResCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString().Trim();
                string mSupCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString().Trim(); ;
                string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString().Trim();
                string ptype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ptype")).ToString().Trim();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString().Trim();


                //Store 
                DataTable dts = (DataTable)ViewState["tblstorename"];
                if (dts.Rows.Count == 0)
                    return;
                ddl1.DataTextField = "actdesc";
                ddl1.DataValueField = "actcode";
                ddl1.DataSource = dts;
                ddl1.DataBind();
                ddl1.SelectedValue = Storecode;

                string comcod = this.GetCompCode();
                string mSrchTxt = "%";

                //Material Stock Information(Work Wise Link)
                if (mResCode != "AAAAAAAAAAAA")
                {
                    resourceLink.NavigateUrl = "~/F_12_Inv/RptMaterialStock.aspx?Type=inv&prjcode=" + pactcode + "&sircode=" + mResCode;
                    // Supplier & Specification

                    string Calltype = this.GetResSupplier();
                    DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", Calltype, mSrchTxt, mResCode, "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    if (ds2.Tables[0].Rows.Count == 0)
                        return;
                    ddl2.DataTextField = "ssirdesc1";
                    ddl2.DataValueField = "ssircode";
                    ddl2.DataSource = ds2.Tables[0];
                    ddl2.DataBind();
                    ddl2.SelectedValue = mSupCode;



                    ddl3.DataTextField = "spcfdesc";
                    ddl3.DataValueField = "spcfcod";
                    ddl3.DataSource = ds2.Tables[1];
                    ddl3.DataBind();
                    ddl3.SelectedValue = spcfcod;


                    // Paytype
                    DataTable dtp = (DataTable)ViewState["tblpaytype"];
                    ddl4.DataTextField = "codedesc";
                    ddl4.DataValueField = "code";
                    ddl4.DataSource = dtp;
                    ddl4.DataBind();
                    ddl4.SelectedValue = ptype;
                    lbtnok.Visible = true;
                    lbtnok.Style.Add("color", "blue");
                }
                else
                {
                    ddl1.Visible = false;
                    ddl2.Visible = false;
                    ddl3.Visible = false;
                    ddl4.Visible = false;
                    supRat.Visible = false;
                    lbtnok.Visible = false;

                }


                if (code == "")
                {
                    return;
                }

                else if (code == "2")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right") ;

                }
                survey.Style.Add("color", "blue");

                if (msrno == "Survey")
                {
                    switch (comcod)
                    {
                        case "1205":
                        case "3351":
                        case "3352":
                        case "3101":
                        case "3353"://Manama
                        case "3354"://Edison Real Estate
                        case "3364"://Edison Real Estate

                            survey.Visible = false;
                            break;
                        default:
                            survey.NavigateUrl = "~/F_12_Inv/LinkMktSurvey.aspx?reqno=" + reqno;
                            break;

                    }
                }
                else
                {
                    survey.NavigateUrl = "~/F_12_Inv/LinkMktSurvey.aspx?reqno=" + reqno + "&msrno=" + msrno;
                    // survey.NavigateUrl = "~/F_12_Inv/LinkShowMktSurvey.aspx?Type=TarVsAch&msrno=" + msrno;

                }



                string Type = Request.QueryString["Type"].ToString();
                if (Type == "VenSelect")
                {
                    ((TextBox)e.Row.FindControl("txtgvappQty")).ReadOnly = true;

                }

                //if(Type== "RateInput")
                //{
                //    switch (comcod)
                //    {
                //        case "1205":
                //        case "3351":
                //        case "3352":
                //        case "3101":

                //            boqrate.Visible = true;
                //            break;
                //        default:
                //            boqrate.Visible = false;
                //            break;

                //    }
                //}



            }
        }
        protected void imgBtnFirst_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = 0;
            this.ShowData();
            this.lblCurPage.Text = "1";
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            this.imgBtnPerv.Enabled = false;
            this.imgBtnNext.Enabled = true;

        }
        protected void imgBtnNext_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = PageNumber + 1;

            if (PageNumber == pageCount)
            {
                PageNumber = PageNumber - 1;
                this.imgBtnNext.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnPerv.Enabled = true;
            this.ShowData();
        }

        protected void imgBtnPerv_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = PageNumber - 1;
            if (PageNumber < 0)
            {
                PageNumber = 0;
                this.imgBtnPerv.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.ShowData();
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnNext.Enabled = true;
        }
        protected void imgBtnLast_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = pageCount - 1;
            this.ShowData();
            this.lblCurPage.Text = pageCount.ToString();
            this.lblCurPage.ToolTip = "Page " + (pageCount) + " of " + pageCount;
            this.imgBtnNext.Enabled = false;
            this.imgBtnPerv.Enabled = true;
        }
        protected void imgbtnSearchCheqNO_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }

        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // this.dgv1.EditIndex = e.NewEditIndex;
            // this.Data_Bind();



            // string comcod = this.GetCompCode();
            // int index = (this.dgv1.PageIndex) * this.dgv1.PageSize + e.NewEditIndex;
            // string Storecode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvstorecode")).Text.Trim();
            // DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSTORENAME", "", "", "", "", "", "", "", "", "");
            // if (ds1 == null)
            //     return;

            // if (ds1.Tables[0].Rows.Count == 0)
            //     return;

            // DropDownList ddl1 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlStorename");
            // ddl1.DataTextField = "actdesc";
            // ddl1.DataValueField = "actcode";
            // ddl1.DataSource = ds1.Tables[0];
            // ddl1.DataBind();
            // ddl1.SelectedValue = Storecode;







            // string mSrchTxt = "%";
            // string mResCode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            // string mSupCode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvsupliercode")).Text.Trim();
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            // if (ds2 == null)
            //     return;

            // if (ds2.Tables[0].Rows.Count == 0)
            //     return;

            // DropDownList ddl2 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlSupname");
            // ddl2.DataTextField = "ssirdesc1";
            // ddl2.DataValueField = "ssircode";
            // ddl2.DataSource = ds2.Tables[0];
            // ddl2.DataBind();
            // ddl2.SelectedValue = mSupCode;





            // string spcfcod = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();
            // DropDownList ddl3 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlspcfdesc");
            // ddl3.DataTextField = "spcfdesc";
            // ddl3.DataValueField = "spcfcod";
            // ddl3.DataSource = ds2.Tables[1];
            // ddl3.DataBind();
            // ddl3.SelectedValue = spcfcod;



            // string ptype = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvptype")).Text.Trim();
            // DropDownList ddl4 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlptype");
            // DataSet dsP = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETPAYTYPE", "", "", "", "", "", "", "", "", "");
            // if (ds2 == null)
            //     return;



            //ddl4.DataTextField = "codedesc";
            //ddl4.DataValueField = "code";
            //ddl4.DataSource = dsP.Tables[0];
            //ddl4.DataBind();
            //ddl4.SelectedValue = ptype;




        }
        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblreq"];
            string mResCode = ((Label)this.dgv1.Rows[j].FindControl("lblgvrsircode")).Text.ToString().Trim();
            if (mResCode != "AAAAAAAAAAAA")
            {
                string spcfcod = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlspcfdesc")).SelectedValue.ToString();
                string spcfdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlspcfdesc")).SelectedItem.Text.Trim();

                string Storecode = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedValue.ToString();
                string StoreDesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedItem.Text.Trim();

                string ssircode = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
                string ssirdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();
                string ptype = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlptype")).SelectedValue.ToString();
                string pdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlptype")).SelectedItem.Text.ToString();
                double dgvReqRat = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvResRat")).Text.Trim());

                int index = (this.dgv1.PageIndex) * this.dgv1.PageSize + e.RowIndex;
                tbl1.Rows[index]["spcfcod"] = spcfcod;
                tbl1.Rows[index]["spcfdesc"] = spcfdesc;
                tbl1.Rows[index]["storecode"] = Storecode;
                tbl1.Rows[index]["storedesc"] = StoreDesc;
                tbl1.Rows[index]["ssircode"] = ssircode;
                tbl1.Rows[index]["ssirdesc"] = ssirdesc;
                tbl1.Rows[index]["reqrat"] = dgvReqRat;
                tbl1.Rows[index]["ptype"] = ptype;
                tbl1.Rows[index]["pdesc"] = pdesc;
            }

            Session["tblreq"] = tbl1;
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            //this.GetApprQty();
            this.Data_Bind();
            this.ddlBestSupplierinfo();
        }

        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblreq"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv1.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + j;

                string mResCode = ((Label)this.dgv1.Rows[j].FindControl("lblgvrsircode")).Text.ToString().Trim();
                if (mResCode != "AAAAAAAAAAAA")
                {
                    string spcfcod = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlspcfdesc")).SelectedValue.ToString();
                    string spcfdesc = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlspcfdesc")).SelectedItem.Text.Trim();
                    string Storecode = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlStorename")).SelectedValue.ToString();
                    string StoreDesc = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlStorename")).SelectedItem.Text.Trim();
                    string ssircode = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlSupname")).SelectedValue.ToString();
                    string ssirdesc = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlSupname")).SelectedItem.Text.Trim();
                    string ptype = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlptype")).SelectedValue.ToString();
                    string pdesc = ((DropDownList)this.dgv1.Rows[j].FindControl("ddlptype")).SelectedItem.Text.ToString();
                    double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.dgv1.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));
                    double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
                    double dgvsupRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvsupRat")).Text.Trim()));
                    double dgvdispercnt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvdispercnt")).Text.Trim().Replace("%", "")));
                    double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
                    double dgvBoqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.dgv1.Rows[j].FindControl("lblgvboqRate")).Text.Trim()));

                    dgvdispercnt = (dgvReqRat > 0) ? ((dgvsupRat - dgvReqRat) * 100) / dgvsupRat : dgvdispercnt;
                    dgvReqRat = (dgvReqRat > 0) ? dgvReqRat : (dgvsupRat - dgvsupRat * .01 * dgvdispercnt);
                    string dgvUseDat = ((TextBox)this.dgv1.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
                    string dgvSupDat = ((TextBox)this.dgv1.Rows[j].FindControl("txtgvpursupDat")).Text.Trim();
                    string dgvReqNote = ((TextBox)this.dgv1.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                    double dgvReqAmt = dgvReqQty * dgvReqRat;
                    double dgvApprAmt = dgvApprQty * dgvReqRat;
                    double dgvbgdreqamt = dgvApprQty * dgvBoqRat;
                    ((Label)this.dgv1.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.000;(#,##0.000); ");
                    ((TextBox)this.dgv1.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.000;(#,##0.000); ");
                    ((TextBox)this.dgv1.Rows[j].FindControl("txtgvsupRat")).Text = dgvsupRat.ToString("#,##0.0000;(#,##0.0000); ");
                    ((TextBox)this.dgv1.Rows[j].FindControl("txtgvdispercnt")).Text = dgvdispercnt.ToString("#,##0.00;(#,##0.00); ");
                    ((TextBox)this.dgv1.Rows[j].FindControl("txtgvResRat")).Text = dgvReqRat.ToString("#,##0.0000;(#,##0.0000); ");
                    //((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.000;(#,##0.000); ");
                    ((Label)this.dgv1.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvApprAmt.ToString("#,##0.000;(#,##0.000); ");
                    ((Label)this.dgv1.Rows[j].FindControl("lblgvbgdreqamt")).Text = dgvbgdreqamt.ToString("#,##0.000;(#,##0.000); ");


                    if (dgvsupRat < dgvReqRat)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier rate must be greater then Actual Rate";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    tbl1.Rows[TblRowIndex2]["spcfcod"] = spcfcod;
                    tbl1.Rows[TblRowIndex2]["spcfdesc"] = spcfdesc;
                    tbl1.Rows[TblRowIndex2]["storecode"] = Storecode;
                    tbl1.Rows[TblRowIndex2]["storedesc"] = StoreDesc;
                    tbl1.Rows[TblRowIndex2]["ssircode"] = ssircode;
                    tbl1.Rows[TblRowIndex2]["ssirdesc"] = ssirdesc;
                    tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
                    tbl1.Rows[TblRowIndex2]["ptype"] = ptype;
                    tbl1.Rows[TblRowIndex2]["pdesc"] = pdesc;
                    tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
                    tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
                    tbl1.Rows[TblRowIndex2]["reqsrat"] = dgvsupRat;
                    tbl1.Rows[TblRowIndex2]["dispercnt"] = dgvdispercnt;
                    tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
                    tbl1.Rows[TblRowIndex2]["preqamt"] = dgvReqAmt;
                    tbl1.Rows[TblRowIndex2]["areqamt"] = dgvApprAmt;
                    tbl1.Rows[TblRowIndex2]["expusedt"] = dgvUseDat;
                    tbl1.Rows[TblRowIndex2]["pursdate"] = dgvSupDat;
                    tbl1.Rows[TblRowIndex2]["reqnote"] = dgvReqNote;
                    tbl1.Rows[TblRowIndex2]["bgdreqamt"] = dgvbgdreqamt;
                }


            }

            DataView dv = tbl1.Copy().DefaultView;
            dv.RowFilter = ("gpsl=1");
            DataTable dt1 = dv.ToTable();
            double preamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(preqamt)", "")) ? 0.00 : dt1.Compute("Sum(preqamt)", "")));
            double areqamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(areqamt)", "")) ? 0.00 : dt1.Compute("Sum(areqamt)", "")));
            DataRow[] dr1 = tbl1.Select("gpsl=2");
            dr1[0]["preqamt"] = preamt;
            dr1[0]["areqamt"] = areqamt;
            Session["tblreq"] = tbl1;
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("frecid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("frectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("secrectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("threcid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcdat", Type.GetType("System.String"));
            tblt01.Columns.Add("threctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }


        private string GetReqApproval(string approval)
        {


            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "RateInput":
                    switch (comcod)
                    {
                        // case "3101":
                        case "1103":
                            break;

                        case "3335": // Edison
                                     // case "3101":
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();
                                dr1["frecid"] = usrid;
                                dr1["frecdat"] = Date;
                                dr1["frectrmid"] = trmnid;
                                dr1["frecseson"] = session;
                                dr1["secrecid"] = usrid;
                                dr1["secrecdat"] = Date;
                                dr1["secrectrmid"] = trmnid;
                                dr1["secrecseson"] = session;
                                dr1["threcid"] = "";
                                dr1["threcdat"] = "";
                                dr1["threctrmid"] = "";
                                dr1["threcseson"] = "";
                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();

                            }


                            else
                            {

                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["frecid"] = usrid;
                                ds1.Tables[0].Rows[0]["frecdat"] = Date;
                                ds1.Tables[0].Rows[0]["frectrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["frecseson"] = session;
                                ds1.Tables[0].Rows[0]["secrecid"] = usrid;
                                ds1.Tables[0].Rows[0]["secrecdat"] = Date;
                                ds1.Tables[0].Rows[0]["secrectrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["secrecseson"] = session;
                                ds1.Tables[0].Rows[0]["threcid"] = "";
                                ds1.Tables[0].Rows[0]["threcdat"] = "";
                                ds1.Tables[0].Rows[0]["threctrmid"] = "";
                                ds1.Tables[0].Rows[0]["threcseson"] = "";
                                approval = ds1.GetXml();

                            }
                            break;


                        default:
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();
                                dr1["frecid"] = usrid;
                                dr1["frecdat"] = Date;
                                dr1["frectrmid"] = trmnid;
                                dr1["frecseson"] = session;
                                dr1["secrecid"] = usrid;
                                dr1["secrecdat"] = Date;
                                dr1["secrectrmid"] = trmnid;
                                dr1["secrecseson"] = session;
                                dr1["threcid"] = usrid;
                                dr1["threcdat"] = Date;
                                dr1["threctrmid"] = trmnid;
                                dr1["threcseson"] = session;
                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            break;

                    }

                    break;


                case "FirstRecom":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (approval == "")
                    {


                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();

                        dr1["frecid"] = usrid;
                        dr1["frecdat"] = Date;
                        dr1["frectrmid"] = trmnid;
                        dr1["frecseson"] = session;
                        dr1["secrecid"] = "";
                        dr1["secrecdat"] = "";
                        dr1["secrectrmid"] = "";
                        dr1["secrecseson"] = "";
                        dr1["threcid"] = "";
                        dr1["threcdat"] = "";
                        dr1["threctrmid"] = "";
                        dr1["threcseson"] = "";
                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        approval = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["frecid"] = usrid;
                        ds1.Tables[0].Rows[0]["frecdat"] = Date;
                        ds1.Tables[0].Rows[0]["frectrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["frecseson"] = session;
                        ds1.Tables[0].Rows[0]["secrecid"] = "";
                        ds1.Tables[0].Rows[0]["secrecdat"] = "";
                        ds1.Tables[0].Rows[0]["secrectrmid"] = "";
                        ds1.Tables[0].Rows[0]["secrecseson"] = "";
                        ds1.Tables[0].Rows[0]["threcid"] = "";
                        ds1.Tables[0].Rows[0]["threcdat"] = "";
                        ds1.Tables[0].Rows[0]["threctrmid"] = "";
                        ds1.Tables[0].Rows[0]["threcseson"] = "";
                        approval = ds1.GetXml();

                    }

                    break;




                case "SecRecom":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["secrecid"] = usrid;
                    ds1.Tables[0].Rows[0]["secrecdat"] = Date;
                    ds1.Tables[0].Rows[0]["secrectrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["secrecseson"] = session;
                    approval = ds1.GetXml();

                    break;

                case "ThirdRecom":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["threcid"] = usrid;
                    ds1.Tables[0].Rows[0]["threcdat"] = Date;
                    ds1.Tables[0].Rows[0]["threctrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["threcseson"] = session;
                    approval = ds1.GetXml();
                    break;





            }


            return approval;

        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string EditByid = hst["usrid"].ToString();
                string Edittrmid = hst["compname"].ToString();
                string EditSession = hst["session"].ToString();
                string Editdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string ApprovByid = hst["usrid"].ToString();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;





                DataTable dt1 = (DataTable)Session["tblreq"];
                string mrfno = dt1.Rows[0]["mrfno"].ToString();
                string Type = this.Request.QueryString["Type"].ToString();
                string projcod = this.Request.QueryString["prjcode"] ?? "";

                if (Type == "VenSelect")
                {

                    switch (comcod)
                    {
                        case "3301":
                        case "1301":
                        case "2301":
                            break;

                        default:
                            foreach (DataRow dr2 in dt1.Rows)
                            {

                                if (Convert.ToDouble(dr2["mreqrat"]) < Convert.ToDouble(dr2["reqrat"]))
                                {

                                    ((Label)this.Master.FindControl("lblmsg")).Text = "Rate Equal or Below Aproved  Rate";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                    return;
                                }

                            }

                            break;

                    }

                }
                bool result = false;

                //string Reqno1 = "XXXXXXXXXXXXXX";
                string rateidate = this.txtdate.Text.Trim();

                foreach (DataRow dr in dt1.Rows)
                {

                    string gpsl = dr["gpsl"].ToString();
                    if (gpsl == "2") continue;
                    string Reqno = dr["reqno"].ToString();
                    string mRSIRCODE = dr["rsircode"].ToString();
                    string mSPCFCOD = dr["spcfcod"].ToString();
                    string mSPCFCODOld = dr["spcfcodo"].ToString();

                    double mPREQTY = Convert.ToDouble(dr["preqty"]);
                    double mAREQTY = Convert.ToDouble(dr["areqty"]);
                    string mREQRAT = dr["reqrat"].ToString();
                    string mEXPUSEDT = dr["expusedt"].ToString();
                    string mREQNOTE = dr["reqnote"].ToString();
                    string PursDate = dr["pursdate"].ToString();
                    string Lpurrate = dr["lpurrate"].ToString();
                    string storecode = dr["storecode"].ToString();
                    string ssircode = dr["ssircode"].ToString();
                    string orderno = dr["orderno"].ToString();


                    double reqsrat1 = Convert.ToDouble(dr["reqsrat"].ToString());
                    string reqsrat = dr["reqsrat"].ToString();

                    string dispercnt = dr["dispercnt"].ToString();
                    string ptype = dr["ptype"].ToString();
                    string appxml = dr["approval"].ToString();
                    string Approval = this.GetReqApproval(appxml);
                    // string msrno = dr["msrno"].ToString();

                    if (mPREQTY >= mAREQTY)
                    {
                        //if (Reqno != Reqno1)
                        //{
                        //    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "DELETEREQNO", Reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                        //    if (!result)
                        //    {
                        //     ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        //        return;
                        //    }


                        //}


                        result = accData.UpdateTransInfo3(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEPURREQARATEINF", Reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mEXPUSEDT, mREQNOTE,
                                    PursDate, Lpurrate, storecode, ssircode, orderno, reqsrat, dispercnt, ptype, mSPCFCODOld, Approval, "", "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }




                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }




                    string type = this.Request.QueryString["Type"];

                    switch (type)
                    {
                        case "RateInput":
                            result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQNO", Reqno, EditByid, Editdat, Edittrmid, EditSession, rateidate, "", "", "", "", "", "", "", "", "");

                            break;
                        default:
                            break;

                    }






                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }


                    DataTable dttemp = (DataTable)Session["tblAttDocs"];

                    DataSet ds1 = new DataSet("ds1");
                    DataView dv1 = new DataView(dttemp);
                    ds1.Tables.Add(dv1.ToTable());
                    ds1.Tables[0].TableName = "tbl1";

                    bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "REQOTHERATTACHEDDOCUMENT", ds1, null, null, Reqno);

                    if (!resulta)
                    {

                        //return;
                    }
                    else
                    {
                        this.btnShowimg_Click(null, null);

                    }


                    // }

                    // Reqno1 = Reqno;


                }

                string reqno = Request.QueryString["genno"].ToString();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                //string Type = this.Request.QueryString["Type"];

                if (comcod == "3315" || comcod == "3316")
                {
                }

                else
                {

                    if (hst["compsms"].ToString() == "True")
                    {

                        if (Type == "RateInput")
                        {


                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "PurReqApproval.aspx?Type=Approval";
                            string SMSHead = "Ready To Requisiton Approval(Purchase Requisition)";
                            string SMSText = comnam + ":\n" + SMSHead + "\n" + "MRF No: " + mrfno;
                            bool resultsms = sms.SendSmms(SMSText, ApprovByid, frmname);
                            if (!resultsms)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Message Send Fail.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Message Send Successfully.');", true);
                            }

                        }


                    }

                }


                if (hst["compmail"].ToString() == "True")
                {

                    switch (comcod)
                    {

                        case "1102"://IBCEL
                            if (this.Request.QueryString["Type"] == "RateInput")
                            {


                                DataSet dsruaauser = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "SHOWREQAAPROVEUSERMAIL", reqno, "", "", "", "");


                                string rusername = dsruaauser.Tables[0].Rows[0]["rusername"].ToString();
                                string chkusername = dsruaauser.Tables[0].Rows[0]["chkusername"].ToString();
                                string ratepusername = dsruaauser.Tables[0].Rows[0]["ratepusername"].ToString();
                                SendMailProcess objsendmail = new SendMailProcess();
                                string comnam = hst["comnam"].ToString();
                                string compname = hst["compname"].ToString();
                                string frmname = "PurReqApproval?Type=Approval";

                                string subject = "Ready for Requisition Approval";
                                string SMSHead = "Ready for Requisition Approval(Purchase)";
                                // string subject = "Ready for Final Approval";
                                //string SMSHead = "Ready for Final Approval(General Requisition)";



                                string SMSText = comnam + "\n" + SMSHead + "\n" + "\n" + "MRF No: " + mrfno + "\n" + "Req. Entry: " + rusername
                                    + "\n" + "Checked By: " + chkusername + "\n" + "Rate Proposed By: " + ratepusername;



                                bool ssl = Convert.ToBoolean(((Hashtable)Session["tblLogin"])["ssl"].ToString());
                                switch (ssl)
                                {
                                    case true:
                                        bool resultmail = SendSSLMail(subject, SMSText, userid, frmname);

                                        break;

                                    case false:
                                        bool resulnmail = objsendmail.SendMail(subject, SMSText, userid, frmname);
                                        break;

                                }




                            }
                            break;

                        case "3101":
                        case "3368":


                            string pactcode = projcod.Substring(0, 4);
                            if (pactcode == "1102")
                            {
                                try
                                {
                                    DataSet dsruaauser = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "SHOWREQAAPROVEUSERMAIL", reqno, "", "", "", "");


                                    string rusername = dsruaauser.Tables[0].Rows[0]["rusername"].ToString();
                                    string chkusername = dsruaauser.Tables[0].Rows[0]["chkusername"].ToString();
                                    string ratepusername = dsruaauser.Tables[0].Rows[0]["ratepusername"].ToString();
                                    //string frmname = "PurReqApproval?Type=Approval";

                                    string empid = "930100101086"; // MD Sir Employee ID
                                                                   // string empid = "930100101005";
                                    var ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

                                    if (ds1 == null)
                                        return;

                                    string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                                    string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                                    string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];

                                    string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
                                    string currentptah = "/F_12_Inv/PurReqApproval?Type=Approval&prjcode=" + projcod + "&genno=" + reqno + "&comcod=" + comcod + "&usrid=" + suserid;
                                    string totalpath = uhostname + currentptah;

                                    string maildescription = "Dear Sir, Please check details information <br>"
                                        + "<br> Ready for Requisition Approval(Purchase) MRF No : " + mrfno + ",<br>" + "Req. Entry:" + rusername + ",<br>" + "Rate proposed by : " + ratepusername + "." + "<br>" +
                                             " <br> <br> <br> N.B: This email is system generated. ";
                                    maildescription += "<br> <br><div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Interface</div>" + "<br/>";
                                    ///GET SMTP AND SMS API INFORMATION
                                    #region
                                    string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                                    DataSet dssmtpandmail = accData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                                    if (dssmtpandmail == null)
                                        return;
                                    //SMTP
                                    string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                                    int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                                    string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                                    string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                                    bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());
                                    #endregion


                                    #region
                                    string subj = "Ready for Requisition Approval";
                                    string msgbody = maildescription;

                                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, "", "", "", "", tomail, msgbody, isSSL);


                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    string Messagesd = "Mail Not Send " + ex.Message;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                                }
                            }


                            break;

                        default:
                            break;


                    }


                }





                //if (hst["compsms"].ToString() == "True")
                //{

                //    SendSmsProcess sms = new SendSmsProcess();
                //    string comnam = hst["comnam"].ToString();
                //    string compname = hst["compname"].ToString();
                //    string frmname = "PurReqApproval.aspx?Type=Approval";

                //    string SMSHead = "Ready To Requisition Approval -";
                //    string SMSText = comnam + ":\n" + SMSHead + ", MRF No:" + mrfno + "\n Thanks";
                //    bool resultsms = sms.SendSmms(SMSText, EditByid, frmname);
                //}






                //if (ConstantInfo.LogStatus == true)
                //{
                //    string reqno = Request.QueryString["genno"].ToString();
                //    string text = this.ddlProject.SelectedItem.Text + "(" + reqno + ")" + " forwarded to Requisition Approval";
                //    sendSmsFromAPI(text);

                //}


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }


        private bool SendSSLMail(string subject, string SMSText, string userid, string frmname)
        {
            try
            {

                string comcod = this.GetCompCode();
                DataSet dssmtpandmail = this.accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", userid, "", "", "", "", "", "", "", "");
                DataSet ds3 = accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWMAILAPIINFO", userid, frmname, "", "", "");

                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string mailtousr = "";




                for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
                {
                    mailtousr = ds3.Tables[1].Rows[i]["email"].ToString();
                    EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");
                    //Connection Details 
                    SmtpServer oServer = new SmtpServer(hostname);
                    oServer.User = frmemail;
                    oServer.Password = psssword;
                    oServer.Port = portnumber;
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


                    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                    oMail.From = frmemail;
                    oMail.To = mailtousr;
                    oMail.Cc = frmemail;
                    oMail.Subject = subject;


                    // oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + SMSText + "</pre></body></html>";

                    string usrid = ds3.Tables[1].Rows[i]["usrid"].ToString();

                    string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
                    string currentptah = "RptPurInterface?Type=Report&comcod=" + comcod + "&usrid=" + usrid;
                    string totalpath = uhostname + currentptah;


                    string body = "<pre>";

                    body += "Dear Sir,";
                    body += "\n" + SMSText + "\n" +
                    "<div style='float:left;  padding:10px; background:Lavender; width:150px; height:40px; text-align:center '><a href='" + totalpath + "' style='float:left; align:center; padding:10px; padding-left:40px; padding-right:45px;background:#05A6FF; color:white;text-decoration:none; text-align:center''> Click </a></div>";
                    body += "\n" + "\n" + "\n" + "<div style='float:left;clear:both;margin-top:40px;'>Best Regards" + "<div></pre>";
                    oMail.HtmlBody = body;
                    //return false;
                    //oMail.HtmlBody = true;


                    try
                    {
                        oSmtp.SendMail(oServer, oMail);

                    }
                    catch (Exception ex)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;

                    }




                }


                return true;
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                return false;
            }// try








        }
        protected void ImgbtnFindSechreqno_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnSurvey_Click(object sender, EventArgs e)
        {
            //string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
        }




        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            int i, index;
            DataTable dt = (DataTable)Session["tblreq"];

            if (((CheckBox)this.dgv1.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked = true;
                    //  ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked = false;
                    // ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + i;
                    dt.Rows[index]["chkmv"] = "False";


                }

            }

            Session["tblreq"] = dt;
        }


        public void createTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("comcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("reqno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("sircode", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemsurl", Type.GetType("System.String"));
            Session["tblAttDocs"] = mnuTbl1;


        }
        private void ddlBestSupplierinfo()
        {

            DataTable dt = (DataTable)Session["tblreq"];
            if (dt == null)
                return;
            DataView dv = dt.DefaultView;

            DataTable tbSupList = dv.ToTable(true, "ssircode", "ssirdesc");
            this.ddlBestSupplier.DataTextField = "ssirdesc";
            this.ddlBestSupplier.DataValueField = "ssircode";
            this.ddlBestSupplier.DataSource = tbSupList;
            this.ddlBestSupplier.DataBind();
        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            DataTable dt = (DataTable)Session["tblAttDocs"];
            string comcod = this.GetCompCode();
            string mREQNO = this.Request.QueryString["genno"].ToString();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string sircode = "";

            if (AsyncFileUpload1.HasFile)
            {
                sircode = this.ddlBestSupplier.SelectedValue.ToString();

                Random r = new Random();
                int next = r.Next(0, 9999999);
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Purchase/") + next + extension);
                Url = next + extension;

                DataRow[] dr2 = dt.Select("sircode = '" + sircode + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["comcod"] = comcod;
                    dr1["reqno"] = mREQNO;
                    dr1["sircode"] = sircode;
                    dr1["itemsurl"] = Url;
                    dt.Rows.Add(dr1);
                }
            }
            DataView dv = dt.DefaultView;
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";
            Session["tblAttDocs"] = dt;

            bool result = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "INSERTREQAPPROVALATTACHEDDOC", ds1, null, null, mREQNO);
            //  this.btnShowimg_Click(null, null);

        }

        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string mREQNO = this.Request.QueryString["genno"].ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "SHOWXMLINFORAMTIONREQIMG", mREQNO, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];
            ListViewEmpAll.DataSource = tbl1;
            ListViewEmpAll.DataBind();
            Session["tblAttDocs"] = tbl1;


        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }


        //update nahid 20211013- for same supplier selected
        protected void chkSameSupplier_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblreq"];
            string ssircode = ((DropDownList)dgv1.Rows[0].FindControl("ddlSupname")).SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ssircode"] = ssircode;
            }
            Session["tblreq"] = dt;
            this.Data_Bind();
        }

        protected void lbtnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;
                int index = this.dgv1.PageSize * this.dgv1.PageIndex + RowIndex;

                string rsircode = ((Label)dgv1.Rows[0].FindControl("lblgvrsircode")).Text.ToString();
                string spcfcod = ((Label)dgv1.Rows[0].FindControl("lblgvSpcfCod")).Text.ToString();
                string rsirdesc = ((HyperLink)dgv1.Rows[0].FindControl("lblgvResDesc")).Text.ToString();
                string prjcode = this.ddlProject.SelectedValue.ToString();

                spanMatName.InnerText = rsirdesc.ToString();
                string comcod = this.GetCompCode();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETMATHISTORYVIEW", prjcode, rsircode, spcfcod, "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }

                this.gvsupres.DataSource = HiddenSameDate2(ds1.Tables[0]);
                this.gvsupres.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openSupModal();", true);
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }
        }

        private DataTable HiddenSameDate2(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }
                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();

                }
            }

            return dt1;


        }

        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAttDocs"];

            string comcod = this.GetCompCode();
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string Reqno = ((Label)this.ListViewEmpAll.Items[j].FindControl("reqno")).Text.ToString();
                string sircode = ((Label)this.ListViewEmpAll.Items[j].FindControl("sircode")).Text.ToString();
                string filesname = "Upload/Purchase/" + ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();

                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {

                    DataSet ds1 = new DataSet("ds1");
                    DataView dv1 = new DataView(dt);

                    dv1.RowFilter = ("sircode<>" + sircode);
                    ds1.Tables.Add(dv1.ToTable());
                    ds1.Tables[0].TableName = "tbl1";

                    bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "SHOWXMLINFORAMTIONREQIMG", ds1, null, null, Reqno);

                    if (!resulta)
                    {
                        //return;
                    }
                    else
                    {
                        //string filePath = Server.MapPath("~/InteriorWEB/");
                        //System.IO.File.Delete(filePath + filesname);

                        this.lblMesg.Text = " Files Removed ";
                        this.btnShowimg_Click(null, null);
                    }



                    //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "REMOVESURVEYIMG", mrsno, sircode, "", "", "", "", "", "", "", "", "", "", "");

                    //if (result == true)
                    //{

                    //}

                }
            }


        }

    }
}


