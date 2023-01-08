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
using RealERPWEB.Service;
using EASendMail;

namespace RealERPWEB.F_34_Mgt
{
    public partial class OtherReqEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserService userSer = new UserService();
        AutoCompleted AutoData = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


                // this.GetGroup();
                this.GetDeparment();
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.GetBundle();
                this.GetProjectName();
                this.GeResVisibility();
                this.GetAccHead();
                this.GeResVisibilityAdj();
                this.GroupBindadj();
                this.Bankcode();
                this.ViewComponent();
                ((Label)this.Master.FindControl("lblTitle")).Text = "General Requisition";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.lblpaytype.Visible = false;
                this.rblpaytype.Visible = false;
                this.GetComAdvanceorAdjust();
                this.BundleVisiable();


                if ((Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FirstRecom") || (Request.QueryString["Type"].ToString() == "SecRecom") || (Request.QueryString["Type"].ToString() == "ThirdRecom")
                        || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqChecked"))
                {
                    this.lbtnPrevReqList_Click(null, null);
                    this.lbtnOk_Click(null, null);
                    if (Request.QueryString["Type"].ToString() == "OreqPrint")
                    {
                        this.lbtnUpdateResReq.Visible = false;
                        this.RequisitionPrint();
                    }
                    //   this.lbtnCheckedCompShow();
                }



                this.GetRecAndPayto();
                this.RbtnPrint.SelectedIndex = 0;

                this.lbtnOreqChecked.Visible = (Request.QueryString["Type"].ToString() == "OreqChecked");
                this.lbtnUpdateResReq.Visible = !(Request.QueryString["Type"].ToString() == "OreqChecked");

            }
        }

        private void lbtnCheckedCompShow()
        {
            if (Request.QueryString["Type"].ToString() == "OreqChecked")
            {




                //string comcod = this.GetCompCode();
                //switch (comcod)
                //{
                //    case "3101":
                //    case "3370":
                //    case "3368":
                //        this.lbtnOreqChecked.Visible = true;
                //        this.lbtnUpdateResReq.Visible = false;
                //        break;

                //    default:
                //        this.lbtnOreqChecked.Visible = false;
                //        this.lbtnUpdateResReq.Visible = true;
                //        break;
                //}
            }

        }

        private void BundleVisiable()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                case "3337":
                    this.bundle.Visible = true;
                    this.pnltermmpay.Visible = true;
                    this.idattnper.Visible = true;
                    break;

                case "3101":
                case "3368":
                    this.pnltermmpay.Visible = true;
                    this.pnlmodpay.Visible = false;
                    this.idattnper.Visible = false;
                    break;

                default:
                    this.pnltermmpay.Visible = false;
                    this.idattnper.Visible = false;
                    break;

            }
        }

        private void GetComAdvanceorAdjust()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1103"://Tanvir
                            //case "3101":
                    this.chkAdvanced.Checked = true;
                    this.chkAdvanced_CheckedChanged(null, null);
                    this.chkbod.Visible = true;
                    this.chkAdvanced.Visible = true;
                    this.lbladvanced.Visible = true;
                    break;

                default:
                    break;

            }
        }

        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ds1.Tables[0].Rows.Add(comcod, "000000000000", "None");
            //ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = "000000000000";
        }


        private void Bankcode()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string ttsrch = "%" + this.txtserchBankName.Text + "%";
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;


                DataTable dt = ds2.Tables[0];
                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["cactcode"] = "000000000000";
                dr1["actdesc1"] = "None";
                dr1["cactdesc"] = "000000000000-NONE";
                dt.Rows.Add(dr1);



                this.ddlBankName.DataTextField = "cactdesc";
                this.ddlBankName.DataValueField = "cactcode";
                this.ddlBankName.DataSource = dt;
                this.ddlBankName.DataBind();
                this.ddlBankName.SelectedValue = "000000000000";
                this.ChequeNo();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void ChequeNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string flag = "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", bankname, flag, "", "", "", "", "", "", "");
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];
            this.ddlcheque.DataBind();
            if (comcod == "3336")
            {
                this.ddlcheque.SelectedIndex = 0;
            }
            else
            {
                this.ddlcheque.SelectedIndex = 1;
            }

            this.ddlcheque_SelectedIndexChanged(null, null);


        }
        public void GetRecAndPayto()
        {
            Session.Remove("tblrecandPayto");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "OreqAcc"))

            //{
            //    ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            //}

            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void ViewComponent()
        {
            if ((Request.QueryString["Type"].ToString() == "OreqChecked") || (Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqAcc") || (Request.QueryString["Type"].ToString() == "OreqPrint"))
            {
                this.lblMatGroup.Visible = false;
                this.ddlMatGrp.Visible = false;
                this.lblCurNo.Visible = false;
                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
                this.gvOtherReq.Columns[1].Visible = false;
            }
            if ((Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                this.pnlnew.Visible = false;
            }
            if ((Request.QueryString["Type"].ToString() == "OreqEntry" || Request.QueryString["Type"].ToString() == "mgtOreqEntry"))
            {
                this.pnlnew.Visible = true;

                this.gvOtherReq.Columns[13].Visible = false;
                this.rblpaytype.SelectedIndex = 1;

            }

            if (Request.QueryString["Type"].ToString() == "OreqEdit")
            {

                this.lblMatGroup.Visible = true;
                this.ddlMatGrp.Visible = true;
                this.lblCurNo.Visible = true;
                this.lblCurReqNo1.Visible = true;
                this.txtCurReqNo2.Visible = true;
                this.lblmrfno.Visible = true;
                this.txtMRFNo.Visible = true;
                this.pnlnew.Visible = true;

            }

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            Session.Remove("tblproject");
            string type = this.Request.QueryString["Type"].ToString();
            string fac = "%%";
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "PROJECTNAME", fac, type, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlProjectName.DataTextField = TextField;
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            Session["tblproject"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetBundle()
        {
            try
            {

                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETBUNDLE", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.ddlBundle.DataTextField = "bundno";
                this.ddlBundle.DataValueField = "bundno";
                this.ddlBundle.DataSource = ds1.Tables[0];
                this.ddlBundle.DataBind();


            }

            catch (Exception ex)
            {


            }


        }

        private void GetAccHead()
        {


            DataTable dt = ((DataTable)Session["tblproject"]).Copy();
            DataRow dr1 = dt.NewRow();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "000000000000-None";
            dr1["actelev"] = "";
            dr1["actdesc1"] = "000000000000-None";
            dr1["acttype"] = "";
            dt.Rows.Add(dr1);
            DataView dv1 = dt.DefaultView;
            dv1.Sort = ("actcode");
            dt = dv1.ToTable();
            this.ddlactcode.DataTextField = "actdesc";
            this.ddlactcode.DataValueField = "actcode";
            this.ddlactcode.DataSource = dt;
            this.ddlactcode.DataBind();

        }

        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string serch1 = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETSUPPLIERNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "resdesc" : "resdesc1");
            this.ddlSupplier.DataTextField = TextField;
            this.ddlSupplier.DataValueField = "sircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();

        }
        private void GetGroup()
        {
            Session.Remove("tblgroup");
            string comcod = this.GetCompCode();
            string Calltype = (comcod.Substring(0, 1) == "2") ? "LANDOTHERGRP" : "OTHERGRP";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", Calltype, "%", "", "", "", "", "", "", "", "");
            Session["tblgroup"] = ds1.Tables[0];
        }
        private void GroupBind()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string ddldesc = hst["ddldesc"].ToString();
                string comcod = GetCompCode();
                string actcode = this.ddlProjectName.SelectedValue.ToString();
                string filter1 = "%%";
                //string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();
                string SearchInfo = "";

                string search1 = this.ddlProjectName.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)Session["tblproject"];
                DataRow[] dr1 = dt.Select("actcode='" + search1 + "'");
                string rsircode = this.ddlMatGrp.SelectedValue.ToString();

                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {
                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {
                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(rescode," + len + ") between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(rescode," + len + ")" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);

                string TextField = (ddldesc == "True" ? "resdesc" : "resdesc1");
                var lst1 = lst.OrderBy(x => x.rescode).ToList();
                this.ddlMatGrp.DataSource = lst1;
                this.ddlMatGrp.DataTextField = TextField;
                this.ddlMatGrp.DataValueField = "rescode";
                this.ddlMatGrp.DataBind();
                string rsircode1 = lst1[0].rescode.ToString();
                this.ddlMatGrp.SelectedValue = (lst.FindAll(l => l.rescode == rsircode)).Count == 0 ? rsircode1 : rsircode;
                //rsircode
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            //DataTable dt = (DataTable)Session["tblgroup"];
            //this.ddlMatGrp.DataTextField = "sirdesc";
            //this.ddlMatGrp.DataValueField = "sircode";
            //this.ddlMatGrp.DataSource = dt;
            //this.ddlMatGrp.DataBind();
            // this.ddlMatGrp_SelectedIndexChanged(null, null);
        }

        private void GroupBindadj()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string ddldesc = hst["ddldesc"].ToString();
                string comcod = GetCompCode();
                string actcode = this.ddlactcode.SelectedValue.ToString();
                string filter1 = "%%";
                //string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();
                string SearchInfo = "";

                string search1 = this.ddlactcode.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)Session["tblproject"];
                DataRow[] dr1 = dt.Select("actcode='" + search1 + "'");


                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {
                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {
                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(rescode," + len + ") between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(rescode," + len + ")" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);


                string TextField = (ddldesc == "True" ? "resdesc" : "resdesc1");
                var lst1 = lst.OrderBy(x => x.rescode);
                this.ddlSupplier.DataSource = lst1;
                this.ddlSupplier.DataTextField = TextField;
                this.ddlSupplier.DataValueField = "rescode";
                this.ddlSupplier.DataBind();
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            //DataTable dt = (DataTable)Session["tblgroup"];
            //this.ddlMatGrp.DataTextField = "sirdesc";
            //this.ddlMatGrp.DataValueField = "sircode";
            //this.ddlMatGrp.DataSource = dt;
            //this.ddlMatGrp.DataBind();
            // this.ddlMatGrp_SelectedIndexChanged(null, null);
        }


        protected void ddlMatGrp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString().Substring(0, 2);

            if (pactcode == "16")
                this.ProjectData();

            string rescode = this.ddlMatGrp.SelectedValue.ToString().Trim();

            string filter2 = "%%";
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
            DataTable dt = ds.Tables[0];
            this.ddlSpclinf.DataSource = dt;
            this.ddlSpclinf.DataTextField = "spdesc1";
            this.ddlSpclinf.DataValueField = "spcod";
            this.ddlSpclinf.DataBind();
        }

        protected void ddlMatGrp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string project = this.ddlProjectName.SelectedValue.ToString();
            string group = this.ddlMatGrp.SelectedValue.Substring(0, 4);
            string txtfindMat = this.txtResSearch.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETGRPMATERIAL", project, group, CurDate1, txtfindMat, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["getMat"] = ds1.Tables[0];
            this.ddlResList.DataTextField = "sirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                //this.txtSrchMrfNo.Visible = true;
                this.lbtnPrevReqList.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                // this.ddlProjectName.Enabled = true;
                this.lblCurReqNo1.Text = "GBL" + DateTime.Today.ToString("MM") + "-";
                //this.lblCurReqNo1.Text = "REQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";
                //this.lblactcode.Visible = false;
                //this.ddlactcode.Visible = false;
                //this.lblrescodeadj.Visible = false;
                //this.ddlSupplier.Visible = false;
                this.termncon.Text = "";
                this.mofpay.Text = "";
                this.txtResSearch.Text = "";
                this.ddlResList.Items.Clear();
                this.txtReqNarr.Text = "";
                this.gvOtherReq.DataSource = null;
                this.gvOtherReq.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";
                if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
                {
                    this.lblMatGroup.Visible = false;
                    this.ddlMatGrp.Visible = false;
                    this.lblCurNo.Visible = false;
                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                }

                this.lbtnGroupSelect.Visible = false;
                return;
            }

            //this.lblactcode.Visible = false;
            //this.ddlactcode.Visible = false;
            //this.lblrescodeadj.Visible = false;
            //this.ddlSupplier.Visible = false;

            if ((Request.QueryString["Type"].ToString() == "OreqChecked") || (Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                //this.Panel1.Visible=(Request.QueryString["Type"].ToString() =="OreqAcc")?false:true;

                this.lblCurNo.Visible = false;
                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
            }

            this.lblmrfno.Visible = true;
            this.txtMRFNo.Visible = true;
            //this.ddlProjectName.Enabled = false;
            this.lblCurNo.Visible = true;
            this.lblCurReqNo1.Visible = true;
            this.txtCurReqNo2.Visible = true;
            this.txtSrchMrfNo.Visible = false;
            this.lbtnPrevReqList.Visible = false;
            this.ddlPrevReqList.Visible = false;
            this.txtCurReqNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";

            this.ddlMatGrp_SelectedIndexChanged(null, null);
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            this.lbtnGroupSelect.Visible = (pactcode.Substring(0, 2) == "16") ? false : true;
            this.Get_Requisition_Info();
            // this.Get_TrmCon_Info();
        }


        private void Get_TrmCon_Info()
        {
            string comcod = this.GetCompCode();
            //  string csircode = this.ddlContractorlist.SelectedValue.ToString();
            //string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            //DataSet ds1 = new DataSet();
            //if (this.ddlPrevList.Items.Count > 0)
            //{
            //    this.txtCurISSDate.Enabled = false;
            //    mNEWORDNo = this.ddlPrevList.SelectedValue.ToString();
            //}

            string reqno = this.lblCurReqNo1.Text = "GBL" + DateTime.Today.ToString("yyyyMM") + this.txtCurReqNo2.Text.Trim().ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETTRMCON", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbltrm"] = ds1.Tables[0];
            this.termncon.Text = ds1.Tables[0].Rows[0]["termacon"].ToString();
            this.mofpay.Text = ds1.Tables[0].Rows[0]["payofmod"].ToString();
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void GetReqNo()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
                mREQNO = this.ddlPrevReqList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxreqno"].ToString();
                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

        }

        private string CompanyTramncon()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string Tramncon = "";
            switch (comcod)
            {
                case "3336":
                case "3337":
                case "3368":
                    Tramncon = "TramnconSuvato";
                    break;

                default:

                    break;
            }
            return Tramncon;
        }

        protected void lbtnPrevReqList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            // string prjcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string mrfno = ((this.Request.QueryString["genno"]).Length == 0) ? "%%" : this.Request.QueryString["genno"].ToString() + "%";

            string type = this.Request.QueryString["Type"].ToString();
            string Module = (Request.QueryString["Type"].ToString() == "OreqAcc") ? "Acc" : "";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPREVREQLIST", CurDate1,
                          "", Module, mrfno, type, "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();

        }
        protected void Get_Requisition_Info()
        {

            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
                //string actcode = this.ddlProjectName.SelectedValue.ToString();


                string mReqNo = "NEWREQ";
                if (this.ddlPrevReqList.Items.Count > 0)
                {
                    this.txtCurReqDate.Enabled = false;
                    mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
                }
                string Type = this.CompanyTramncon();



                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1, Type, "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                Session["tblReq"] = this.HiddenSameData(ds1.Tables[0]);
                Session["tblUserReq"] = ds1.Tables[1];
                Session["tblUsersign"] = ds1.Tables[2];

                //if (Type == "TramnconSuvato")
                //{

                //}



                this.termncon.Text = ds1.Tables[3].Rows.Count == 0 ? "" : ds1.Tables[3].Rows[0]["termacon"].ToString();
                this.mofpay.Text = ds1.Tables[3].Rows.Count == 0 ? "" : ds1.Tables[3].Rows[0]["payofmod"].ToString();

                DataTable dt = ds1.Tables[1];
                //if (dt.Rows.Count == 0)
                //    return;
                if (dt.Rows.Count > 0)
                {
                    this.txtPayto.Text = dt.Rows[0]["payto"].ToString();
                    this.txtAttn.Text = dt.Rows[0]["attnper"].ToString();

                    string paytype = dt.Rows[0]["paytype"].ToString();
                    //this.ddlSupplier.SelectedValue = dt.Rows[0]["supdesc"].ToString();

                    this.rblpaytype.SelectedIndex = (paytype == "Cheque") ? 1 : 0;

                    //this.rblpaytype.SelectedItem.Text = dt.Rows[0]["paytype"].ToString();
                }


                if (Request.QueryString["Type"].ToString() == "OreqChecked" || Request.QueryString["Type"].ToString() == "OreqApproved" || Request.QueryString["Type"].ToString() == "FinalAppr" || Request.QueryString["Type"].ToString() == "OreqPrint")
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        this.GetApprQty();
                    }
                }




                if (mReqNo == "NEWREQ")
                {
                    ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                        this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                    }

                    // Last Narration 

                    DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "LASTNARRATION", "", "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                        this.txtReqNarr.Text = "";
                    else
                                       
                     this.txtReqNarr.Text = (comcod == "1103"|| comcod=="3368") ? "" : ds4.Tables[0].Rows[0]["vernar"].ToString();

                    return;
                }
                this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
                this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
                this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
                this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");
                this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
                this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
                string adjcod = ds1.Tables[1].Rows[0]["adjcod"].ToString();
                string supcode = ds1.Tables[1].Rows[0]["supcode"].ToString();
                string bankcode = ds1.Tables[1].Rows[0]["bankcode"].ToString();
                // this.ddlBankName.SelectedValue = bankcode;
                this.txtRefNum.Text = ds1.Tables[1].Rows[0]["chequeno"].ToString(); ;

                // this.ddlactcode_OnSelectedIndexChanged(null, null);


                this.ddlactcode.SelectedValue = adjcod;//ds1.Tables[1].Rows[0]["adjcod"].ToString();

                //

                //this.ddlBankName.Items.Clear();
                this.ddlBankName.SelectedValue = bankcode;
                this.ddlBankName.DataTextField = "bankdesc";
                this.ddlBankName.DataValueField = "bankcode";
                this.ddlBankName.DataSource = ds1.Tables[1];
                this.ddlBankName.DataBind();
                //  this.ddlBankName.


                this.pnlbank.Visible = (bankcode.Substring(0, 2) == "19" || bankcode.Substring(0, 2) == "29");
                this.ddlactcode_OnSelectedIndexChanged(null, null);
                // this.GeResVisibilityAdj();
                this.ddlSupplier.SelectedValue = supcode;//ds1.Tables[1].Rows[0]["supcode"].ToString();
                this.ddlBundle.SelectedValue = ds1.Tables[1].Rows[0]["bundno"].ToString();
                this.chkAdvanced.Checked = Convert.ToBoolean(ds1.Tables[1].Rows[0]["advanced"]);
                this.chkAdvanced_CheckedChanged(null, null);
                this.gvOtherReq_DataBind();
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        private void GetApprQty()
        {
            DataTable dt = (DataTable)Session["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double qty = Convert.ToDouble(dt.Rows[i]["qty"]);
                double ppdamt = Convert.ToDouble(dt.Rows[i]["ppdamt"]);

                double proamt = Convert.ToDouble(dt.Rows[i]["proamt"]);
                double appamt = Convert.ToDouble(dt.Rows[i]["appamt"]);
                if (appamt == 0)
                    dt.Rows[i]["appamt"] = proamt;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["ppdamt"] = ppdamt;
            }
            Session["tblReq"] = dt;
        }

        private void GetBudgetAndBal()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rescode = (this.ddlMatGrp.Items.Count == 0) ? "000000000000" : this.ddlMatGrp.SelectedValue.ToString();
            string spcfcod = (this.ddlSpclinf.Items.Count == 0) ? "000000000000" : this.ddlSpclinf.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBALHAOTHERS", pactcode, rescode, CurDate1, spcfcod, "", "", "", "", "");
            Session["tblprobudbal"] = ds2.Tables[0];
        }


        protected void lbtnGroupSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string ddldesc = hst["ddldesc"].ToString();
                this.lblactcode.Visible = true;
                this.ddlactcode.Visible = true;
                this.lblrescodeadj.Visible = true;
                this.ddlSupplier.Visible = true;
                this.Session_tblReq_Update();
                DataTable tbl1 = (DataTable)Session["tblReq"];
                DataTable dt = (DataTable)Session["tblproject"];

                string actcode = this.ddlProjectName.SelectedValue.ToString();
                string actelev = dt.Select("actcode='" + actcode + "'")[0]["actelev"].ToString();
                string Billno = this.txtbillno.Text;

                this.pnlbank.Visible = (actcode.Substring(0, 2) == "19" || actcode.Substring(0, 2) == "29");
                if (actelev == "")
                {

                    this.GetBudgetAndBal();
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = actcode;
                    dr1["rsircode"] = "000000000000";
                    dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text;
                    dr1["sirdesc"] = "";
                    dr1["spcfcod"] = "000000000000";
                    dr1["bgdamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["bgdamt"]).ToString();
                    dr1["trnamt"] = 0.00;// ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["balamt"]).ToString(); ;
                    dr1["balamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["balamt"]).ToString();
                    dr1["proamt"] = 0;
                    dr1["appamt"] = 0;
                    dr1["qty"] = 0;
                    dr1["rate"] = 0;
                    dr1["billno"] = this.txtbillno.Text;
                    dr1["ppdamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["ppdamt"]).ToString();
                    tbl1.Rows.Add(dr1);
                }
                else
                {
                    string rescode = (this.ddlMatGrp.SelectedValue.ToString() == "") ? "000000000000" : this.ddlMatGrp.SelectedValue.ToString();
                    string spcfcod = (this.ddlSpclinf.SelectedValue.ToString() == "") ? "000000000000" : this.ddlSpclinf.SelectedValue.ToString();
                    //DataRow[] dr2 = tbl1.Select("pactcode = '" + actcode + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "' and billno= '" + Billno + "'");
                    DataRow[] dr2;
                    string comcod = this.GetCompCode();
                    switch (comcod)
                    {
                        //case"3101":
                        case "1103":
                            dr2 = tbl1.Select("pactcode = '" + actcode + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "' and billno= '" + Billno + "'");
                            break;
                        default:
                            dr2 = tbl1.Select("pactcode = '" + actcode + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
                            break;
                    }


                    if (dr2.Length == 0)
                    {

                        this.GetBudgetAndBal();
                        DataRow dr1 = tbl1.NewRow();
                        dr1["pactcode"] = actcode;
                        dr1["rsircode"] = rescode;
                        dr1["spcfcod"] = spcfcod;
                        dr1["billno"] = this.txtbillno.Text;
                        dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text;
                        dr1["sirdesc"] = (this.ddlMatGrp.Items.Count == 0) ? "" : (ddldesc == "True" ? this.ddlMatGrp.SelectedItem.Text.Trim() : this.ddlMatGrp.SelectedItem.Text.Trim().Substring(14)) + (spcfcod == "000000000000" ? "" : "[" + this.ddlSpclinf.SelectedItem.Text + "]");
                        dr1["bgdamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["bgdamt"]).ToString();
                        dr1["trnamt"] = 0.00;// ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["balamt"]).ToString(); ;
                        dr1["balamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["balamt"]).ToString();
                        dr1["proamt"] = 0;

                        dr1["appamt"] = 0;
                        dr1["qty"] = 0;
                        dr1["rate"] = 0;
                        dr1["ppdamt"] = ((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["ppdamt"]).ToString(); ;
                        tbl1.Rows.Add(dr1);
                    }
                }
                Session["tblReq"] = this.HiddenSameData(tbl1);
                this.gvOtherReq_DataBind();


            }

            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }



            //this.Session_tblReq_Update();         
            // DataTable tbl1 = (DataTable)Session["tblReq"];
            //string rescode = this.ddlResList.SelectedValue.ToString().Substring(0,4);
            //DataRow[] dr2 = tbl1.Select("rsircode1 = '" + rescode + "'");
            //if (dr2.Length == 0)
            //{

            //    string comcod = this.GetCompCode();  
            //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //    string GroupCode = this.ddlMatGrp.SelectedValue.ToString().Substring(0,4);
            //    string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBAL", pactcode, GroupCode, CurDate1, "", "", "", "", "", "");
            //    DataTable dt=ds2.Tables[0];

            //    if(dt.Rows.Count==0)
            //        return;

            //    for (int i = 0; i < dt.Rows.Count; i++) 
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["rsircode"] =dt.Rows[i]["rsircode"].ToString();
            //        dr1["rsircode1"] =ASTUtility.Left(dt.Rows[i]["rsircode"].ToString(),4);
            //        dr1["sirdesc"] = dt.Rows[i]["sirdesc"].ToString();
            //        dr1["bgdamt"] = Convert.ToDouble(dt.Rows[i]["bgdamt"]);
            //        dr1["trnamt"] = Convert.ToDouble(dt.Rows[i]["trnamt"]);
            //        dr1["balamt"] = Convert.ToDouble(dt.Rows[i]["balamt"]);
            //        dr1["proamt"] = 0;
            //        dr1["appamt"] = 0;
            //        dr1["qty"] = 0;
            //        dr1["ppdamt"] = Convert.ToDouble(dt.Rows[i]["ppdamt"]);

            //        tbl1.Rows.Add(dr1);
            //    }


            //}
            //Session["tblReq"] = tbl1;
            //this.gvOtherReq_DataBind();
        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)Session["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["sirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)Session["getMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["bgdamt"] = dr3[0]["bgdamt"];
                dr1["trnamt"] = dr3[0]["trnamt"];
                dr1["balamt"] = dr3[0]["balamt"];
                dr1["proamt"] = 0;
                dr1["appamt"] = 0;
                dr1["rate"] = 0;
                dr1["qty"] = 0;
                dr1["ppdamt"] = dr3[0]["ppdamt"];


                tbl1.Rows.Add(dr1);
            }
            Session["tblReq"] = tbl1;
            this.gvOtherReq_DataBind();
        }



        private string Comptype()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string otherreq = "";
            switch (comcod)
            {
                //case "3101":
                case "3336":
                case "3337":
                    otherreq = "otherreqsuv";
                    break;
                case "1103":// Tanvir
                    otherreq = "otherreqtan";
                    break;
                default:
                    otherreq = "otherreqgen";
                    break;
            }

            return otherreq;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            string Ptype = this.RbtnPrint.SelectedValue.ToString();

            switch (Ptype)
            {
                case "Requisition":
                    this.RequisitionPrint();
                    break;
                case "Order":
                    this.OrderPrint();
                    break;

                    //default:
                    //    otherreq = "otherreqgen";
                    //    break;
            }

        }

        private void RequisitionPrint()
        {
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "3336":
                case "3337":
                case "1103":// Tanvir
                    this.OtherReqTanvirSuva();
                    break;

                case "1102":// ISBL
                    this.OtherReqPrintISBL();
                    break;

                case "3368":// Finlay
                    this.OtherReqPrintFinlay();
                    break;
                case "3101":
                case "3366":// Lanco
                    this.OtherReqPrintLanco();
                    break;

                default:
                    this.OtherReqPrintGen();
                    break;
            }

        }

        private void OtherReqPrintGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            string payto = "Pay To: " + this.txtPayto.Text.Trim().ToString();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string paytype = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();
            string date = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
            string refno = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
            string reqno = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string narration = "Narration:" + this.txtReqNarr.Text.Trim();
            string title = this.Request.QueryString["Type"].ToString() == "OreqEntry" ? "General Bill Requisition" : "Software Generated Bill";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            DataTable dtsign = ds1.Tables[2];

            string requsinput = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req posted 
            string confirmby = dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqadesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req approved
            string approved = dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["faprovdesig"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString(); // final approved 
            string frapnam = dtsign.Rows[0]["frapnam"].ToString() + "\n" + dtsign.Rows[0]["frapdesig"].ToString() + "\n" + dtsign.Rows[0]["frapdat"].ToString();      // forword
            string secapnam = dtsign.Rows[0]["secapnam"].ToString() + "\n" + dtsign.Rows[0]["secapdesig"].ToString() + "\n" + dtsign.Rows[0]["secapdat"].ToString();   // approval 1
            string thrapnam = dtsign.Rows[0]["thrapnam"].ToString() + "\n" + dtsign.Rows[0]["thrapdesig"].ToString() + "\n" + dtsign.Rows[0]["thrapdat"].ToString();   // approval 1

            LocalReport Rpt1 = new LocalReport();
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rpttitle", title));
            Rpt1.SetParameters(new ReportParameter("paytype", paytype));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("reqno", reqno));
            Rpt1.SetParameters(new ReportParameter("narration", narration));
            Rpt1.SetParameters(new ReportParameter("requsinput", requsinput));
            Rpt1.SetParameters(new ReportParameter("confirmby", confirmby));
            Rpt1.SetParameters(new ReportParameter("approved", approved));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //  Rpt1.SetParameters(new ReportParameter("CurDate", "Order Date: " + CurDate));

            Session["Report1"] = Rpt1;
            if (this.Request.QueryString["Type"].ToString() == "OreqPrint")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void OtherReqPrintLanco()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            string payto = "Pay To: " + this.txtPayto.Text.Trim().ToString();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string paytype = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();
            string date = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
            string refno = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
            //string reqno = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string narration = "Narration:" + this.txtReqNarr.Text.Trim();
            string title = this.Request.QueryString["Type"].ToString() == "OreqEntry" ? "General Bill Requisition" : "Software Generated Bill";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            DataTable dtsign = ds1.Tables[2];
            DataTable dt1 = ds1.Tables[1];

            string reqno = "Requisition No : " + dt1.Rows[0]["reqno"].ToString();

            string requsinput = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req posted 
            string confirmby = dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqadesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req approved
            string approved = dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["faprovdesig"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString(); // final approved 
            string frapnam = dtsign.Rows[0]["frapnam"].ToString() + "\n" + dtsign.Rows[0]["frapdesig"].ToString() + "\n" + dtsign.Rows[0]["frapdat"].ToString();      // forword
            string secapnam = dtsign.Rows[0]["secapnam"].ToString() + "\n" + dtsign.Rows[0]["secapdesig"].ToString() + "\n" + dtsign.Rows[0]["secapdat"].ToString();   // approval 1
            string thrapnam = dtsign.Rows[0]["thrapnam"].ToString() + "\n" + dtsign.Rows[0]["thrapdesig"].ToString() + "\n" + dtsign.Rows[0]["thrapdat"].ToString();   // approval 1

            LocalReport Rpt1 = new LocalReport();
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqStatusLanco", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rpttitle", title));
            Rpt1.SetParameters(new ReportParameter("paytype", paytype));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("reqno", reqno));
            Rpt1.SetParameters(new ReportParameter("narration", narration));
            Rpt1.SetParameters(new ReportParameter("requsinput", requsinput));
            Rpt1.SetParameters(new ReportParameter("confirmby", confirmby));
            Rpt1.SetParameters(new ReportParameter("approved", approved));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //  Rpt1.SetParameters(new ReportParameter("CurDate", "Order Date: " + CurDate));

            Session["Report1"] = Rpt1;
            if (this.Request.QueryString["Type"].ToString() == "OreqPrint")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void OtherReqPrintISBL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            string payto = "Pay To: " + this.txtPayto.Text.Trim().ToString();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string paytype = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();
            string date = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
            string refno = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
            string reqno = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string narration = "Narration:" + this.txtReqNarr.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            DataTable dtsign = ds1.Tables[2];

            string requsinput = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req posted 
            string confirmby = dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqadesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req approved
            string approved = dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["faprovdesig"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString(); // final approved 
            string frapnam = dtsign.Rows[0]["frapnam"].ToString() + "\n" + dtsign.Rows[0]["frapdesig"].ToString() + "\n" + dtsign.Rows[0]["frapdat"].ToString();      // forword
            string secapnam = dtsign.Rows[0]["secapnam"].ToString() + "\n" + dtsign.Rows[0]["secapdesig"].ToString() + "\n" + dtsign.Rows[0]["secapdat"].ToString();   // approval 1
            string thrapnam = dtsign.Rows[0]["thrapnam"].ToString() + "\n" + dtsign.Rows[0]["thrapdesig"].ToString() + "\n" + dtsign.Rows[0]["thrapdat"].ToString();   // approval 1

            LocalReport Rpt1 = new LocalReport();
            var lst1 = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqStatusISBL", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("forward", frapnam));
            Rpt1.SetParameters(new ReportParameter("thrapnam", thrapnam));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("paytype", paytype));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("reqno", reqno));
            Rpt1.SetParameters(new ReportParameter("narration", narration));
            Rpt1.SetParameters(new ReportParameter("requsinput", requsinput));
            Rpt1.SetParameters(new ReportParameter("confirmby", confirmby));
            Rpt1.SetParameters(new ReportParameter("approved", approved));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //  Rpt1.SetParameters(new ReportParameter("CurDate", "Order Date: " + CurDate));

            Session["Report1"] = Rpt1;
            if (this.Request.QueryString["Type"].ToString() == "OreqPrint")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void OtherReqPrintFinlay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            string payto = "Pay To: " + this.txtPayto.Text.Trim().ToString();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string paytype = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();
            string date = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
            string refno = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
            string reqno = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            string narration = "Narration:" + this.txtReqNarr.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            DataTable dtsign = ds1.Tables[2];
            string requsinput = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req posted 
            string checkedby = dtsign.Rows[0]["chkusrnam"].ToString() + "\n" + dtsign.Rows[0]["chkusrdesig"].ToString() + "\n" + dtsign.Rows[0]["checkdat"].ToString();     // req checked
            string confirmby = dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqadesig"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();     // req approved
            string approved = dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["faprovdesig"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString(); // final approved 
            string frapnam = dtsign.Rows[0]["frapnam"].ToString() + "\n" + dtsign.Rows[0]["frapdesig"].ToString() + "\n" + dtsign.Rows[0]["frapdat"].ToString();      // forword
            string secapnam = dtsign.Rows[0]["secapnam"].ToString() + "\n" + dtsign.Rows[0]["secapdesig"].ToString() + "\n" + dtsign.Rows[0]["secapdat"].ToString();   // approval 1
            string thrapnam = dtsign.Rows[0]["thrapnam"].ToString() + "\n" + dtsign.Rows[0]["thrapdesig"].ToString() + "\n" + dtsign.Rows[0]["thrapdat"].ToString();   // approval 1

            LocalReport Rpt1 = new LocalReport();
            var lst1 = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqStatusFinlay", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("paytype", paytype));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("reqno", reqno));
            Rpt1.SetParameters(new ReportParameter("narration", narration));
            Rpt1.SetParameters(new ReportParameter("requsinput", requsinput));
            Rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            Rpt1.SetParameters(new ReportParameter("confirmby", confirmby));
            Rpt1.SetParameters(new ReportParameter("approved", approved));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            if (this.Request.QueryString["Type"].ToString() == "OreqPrint")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void OtherReqTanvirSuva()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                        "", "", "", "", "", "", "");
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["tblReq"];
            DataTable dt2 = new DataTable();
            dt2 = ((DataTable)Session["tblUserReq"]).Copy();
            DataTable dtsign = ds1.Tables[2];
            string type = this.Comptype();


            LocalReport Rpt1 = new LocalReport();
            var list = dt1.DataTableToList<RealEntity.C_34_Mgt.EClassOtherReq>();
            if (type == "otherreqsuv")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.rptOtherReqStatusSuvastu", list, null, null);
                Rpt1.SetParameters(new ReportParameter("txtSupplier", (this.ddlSupplier.Items.Count == 0) ? "" : ("Supplier Name : " + ddlSupplier.SelectedItem.Text.ToString())));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Work Order"));
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.rptOtherReqStatusTanvir", list, null, null);
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Requisition Status"));
            }

            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("payType", "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("payTo", "Pay To: " + this.txtPayto.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("cReqDate", "Date : " + this.txtCurReqDate.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref No : " + this.txtMRFNo.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("cReqNo", "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtReqNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtReq", dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtFirstApp", dtsign.Rows[0]["reqanam"].ToString() + "\n" + dtsign.Rows[0]["reqadat"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtFinalApp", dtsign.Rows[0]["faprovnam"].ToString() + "\n" + dtsign.Rows[0]["fapprvdat"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Print Report";
                string eventdesc2 = "Requisition No: " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void OrderPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETGENBILLREQ", mReqNo, "", "", "", "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_34_Mgt.GenBillReq>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_34_Mgt.GenBillSupdesc>();
            string firmnam = lst1[0].firmname.ToString();
            string Address = lst1[0].conadd.ToString();
            string Attn = lst1[0].conperson.ToString();
            string worknam = lst1[0].natureofwork.ToString();
            string MobileNo = lst1[0].mobile.ToString();
            string prejadd = lst[0].projadds.ToString();
            string trmcon = lst[0].termacon.ToString();
            string payofm = lst[0].payofmod.ToString();
            string reqnum = lst[0].reqno.ToString();
            string reqdat = Convert.ToDateTime(lst[0].reqdat).ToString("dd MMMM, yyyy");
            string projectnam = lst[0].actdesc.ToString();
            string reqnam = ds1.Tables[2].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[2].Rows[0]["reqdat"].ToString();
            string reqanam = ds1.Tables[2].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[2].Rows[0]["reqadat"].ToString();
            string faprovnam = ds1.Tables[2].Rows[0]["faprovnam"].ToString() + "\n" + ds1.Tables[2].Rows[0]["fapprvdat"].ToString();
            string attnper = ds1.Tables[0].Rows[0]["attnper"].ToString();
            string naration = txtReqNarr.Text;

            double tAmt = lst.Select(p => p.proamt).Sum();

            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "3336":
                case "3337":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqPrintSuvasto", lst, lst1, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("firmnam", "M/S: " + payto));
                    Rpt1.SetParameters(new ReportParameter("payto", attnper));
                    break;

                case "3101":
                case "3368":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqPrintFinlay", lst, lst1, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("firmnam", "M/S: " + payto));
                    Rpt1.SetParameters(new ReportParameter("payto", attnper));
                    break;

                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.RptOtherReqPrint", lst, lst1, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("firmnam", "M/S: " + firmnam));
                    Rpt1.SetParameters(new ReportParameter("payto", payto));
                    break;
            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //  Rpt1.SetParameters(new ReportParameter("CurDate", "Order Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("Address", "Address: " + Address));
            Rpt1.SetParameters(new ReportParameter("trmcon", trmcon));
            Rpt1.SetParameters(new ReportParameter("payofm", payofm));
            Rpt1.SetParameters(new ReportParameter("MobileNo", MobileNo));
            Rpt1.SetParameters(new ReportParameter("reqdat", "Order Date: " + reqdat));
            Rpt1.SetParameters(new ReportParameter("reqnum", reqnum));
            Rpt1.SetParameters(new ReportParameter("worknam", " Nature Of Work: " + worknam));
            Rpt1.SetParameters(new ReportParameter("projectnam", projectnam));
            //Rpt1.SetParameters(new ReportParameter("reqnum", reqnum));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "GENERAL REQUISITION"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("prejadd", prejadd));
            Rpt1.SetParameters(new ReportParameter("reqnam", reqnam));
            Rpt1.SetParameters(new ReportParameter("reqanam", reqanam));
            Rpt1.SetParameters(new ReportParameter("faprovnam", faprovnam));
            Rpt1.SetParameters(new ReportParameter("naration", naration));
            Rpt1.SetParameters(new ReportParameter("InWrd", (tAmt == 0 ? "" : "In Words Taka : " + ASTUtility.Trans(Math.Round(tAmt), 2))));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string ComCallType()
        {
            string CallType = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3336":
                case "3337":
                    CallType = "INSERTOTHERREQOMITSTEEP";
                    break;


                default:
                    CallType = "INSERTOTHERREQ";
                    break;
            }
            return CallType;
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
                case "OreqChecked":
                    switch (comcod)
                    {
                        case "3370": //CPDL
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
                                ds1.Tables[0].Rows[0]["threcid"] = usrid;
                                ds1.Tables[0].Rows[0]["threcdat"] = Date;
                                ds1.Tables[0].Rows[0]["threctrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["threcseson"] = session;
                                approval = ds1.GetXml();
                            }
                            break;

                        default:

                            break;
                    }

                    break;



                case "OreqApproved":
                    switch (comcod)
                    {
                        case "1103":
                        case "1102"://IBCEL
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
                    switch (comcod)
                    {
                        case "1102"://IBCEL

                            xmlSR = new System.IO.StringReader(approval);
                            ds1.ReadXml(xmlSR);
                            ds1.Tables[0].TableName = "tbl1";
                            ds1.Tables[0].Rows[0]["secrecid"] = usrid;
                            ds1.Tables[0].Rows[0]["secrecdat"] = Date;
                            ds1.Tables[0].Rows[0]["secrectrmid"] = trmnid;
                            ds1.Tables[0].Rows[0]["secrecseson"] = session;
                            ds1.Tables[0].Rows[0]["threcid"] = usrid;
                            ds1.Tables[0].Rows[0]["threcdat"] = Date;
                            ds1.Tables[0].Rows[0]["threctrmid"] = trmnid;
                            ds1.Tables[0].Rows[0]["threcseson"] = session;
                            approval = ds1.GetXml();
                            break;


                        default:
                            xmlSR = new System.IO.StringReader(approval);
                            ds1.ReadXml(xmlSR);
                            ds1.Tables[0].TableName = "tbl1";
                            ds1.Tables[0].Rows[0]["secrecid"] = usrid;
                            ds1.Tables[0].Rows[0]["secrecdat"] = Date;
                            ds1.Tables[0].Rows[0]["secrectrmid"] = trmnid;
                            ds1.Tables[0].Rows[0]["secrecseson"] = session;
                            approval = ds1.GetXml();
                            break;


                    }




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


        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usrid = hst["usrid"].ToString();
            //string sessionid = hst["session"].ToString();
            //string trmid = hst["compname"].ToString();
            this.Session_tblReq_Update();
            string comcod = this.GetCompCode();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            // Duplicate MRF


            //if (mMRFNO.Length == 0)
            //{
            //   ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //   ((Label)this.Master.FindControl("lblmsg")).Text = "M.R.F No. Should Not Be Empty";
            //    return;
            //}

            //DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");
            //if (ds2.Tables[0].Rows.Count == 0)
            //    ;


            //else
            //{

            //    DataView dv1 = ds2.Tables[0].DefaultView;
            //    dv1.RowFilter = ("reqno <>'" + mREQNO + "'");
            //    DataTable dt = dv1.ToTable();
            //    if (dt.Rows.Count == 0)
            //        ;
            //    else
            //    {
            //       ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //       ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R.F No";
            //        //this.ddlPrevReqList.Items.Clear();
            //        return;
            //    }
            //}




            string type = this.Request.QueryString["Type"].ToString();

            //log Report
            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDate = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");

            string tblApprovByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string tblApprovDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy");
            string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();

            string PostedByid = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;

            string Posttrmid = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;

            string PostSession = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string posteddat = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblPostedDate == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblPostedDate;

            string ApprovByid = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? "" : ((this.Request.QueryString["Type"] == "FinalAppr") ? userid : ((tblApprovByid == "") ? userid : tblApprovByid));
            string approvdat = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? "01-Jan-1900" : ((this.Request.QueryString["Type"] == "FinalAppr") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblApprovDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblApprovDat);
            string Approvtrmid = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? "" : ((this.Request.QueryString["Type"] == "FinalAppr") ? Terminal : (tblApprovtrmid == "") ? Terminal : tblApprovtrmid);
            string ApprovSession = (this.Request.QueryString["Type"] == "OreqEntry" || this.Request.QueryString["Type"] == "mgtOreqEntry") ? "" : ((this.Request.QueryString["Type"] == "FinalAppr") ? Sessionid : (tblApprovSession == "") ? Sessionid : tblApprovSession);


            /////log end

            string nARRATION = txtReqNarr.Text.Trim();

            string paytype = this.rblpaytype.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();
            string supcode = ddlSupplier.SelectedValue.ToString();
            string termncon = this.termncon.Text.ToString();
            string payofmod = this.mofpay.Text.ToString();
            string deptcode = this.ddlDeptCode.SelectedValue.ToString();
            supcode = (supcode.Trim() == "" ? "000000000000" : supcode);
            supcode = (type == "OreqChecked" || type == "OreqApproved" || type == "FinalAppr" ? dtuser.Rows[0]["supcode"].ToString() : supcode);

            DataTable tbl1 = (DataTable)Session["tblReq"];
            bool result = true;
            string adjcod = this.ddlactcode.SelectedValue.ToString();
            string bundleno = this.ddlBundle.SelectedValue.ToString().Trim();

            // Mandatory Field 
            switch (comcod)
            {
                case "3336":
                case "3337":
                    if (payto.Trim().Length == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Pay to Field";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    break;

                case "1103":
                    //case "3101":

                    for (int i = 0; i < tbl1.Rows.Count; i++)
                    {
                        string billno = tbl1.Rows[i]["billno"].ToString();

                        if (billno.Trim().Length == 0)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Bill No/Purpose Field !!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }
                    break;
                default:
                    break;


            }
            // todo for other req checked 
            string chckid = "", checkdat = "";
            switch (comcod)
            {  //Checked defaul all company without finlay and cpdl
                case "3368"://Finlay
                case "3370"://CPDL
                    chckid = "";
                    checkdat = "01-Jan-1900";
                    break;

                default:
                    chckid = PostedByid;
                    checkdat = posteddat;
                    break;
            }


            //if (comcod == "3336")
            //{
            //    DataSet ds1 = new DataSet();
            //    DataTable dt1 = new DataTable("TblA");
            //    dt1.Columns.Add("termacon");
            //    dt1.Columns.Add("payofmod");
            //    DataRow dr = dt1.NewRow();
            //    dr["termacon"] = this.termncon.Text;
            //    dr["payofmod"] = this.mofpay.Text;
            //    dt1.Rows.Add(dr);
            //    ds1.Tables.Add(dt1);
            //    result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQEXT", ds1, null, null, mREQNO);
            //}
            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string refnum = this.txtRefNum.Text.Trim();

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mPACTCODE = tbl1.Rows[i]["pactcode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                string billno = tbl1.Rows[i]["billno"].ToString();
                double mProAMT = Convert.ToDouble(tbl1.Rows[i]["proamt"]);
                double mAPPAMT = Convert.ToDouble(tbl1.Rows[i]["appamt"]);
                double qty = Convert.ToDouble(tbl1.Rows[i]["qty"]);
                double rate = Convert.ToDouble(tbl1.Rows[i]["rate"]);
                double ppdamt = Convert.ToDouble(tbl1.Rows[i]["ppdamt"]);
                string appxml = tbl1.Rows[i]["approval"].ToString();
                string Approval = this.GetReqApproval(appxml);
                string advanced = this.chkAdvanced.Checked ? "1" : "0";
                string attnper = this.txtAttn.Text.ToString();


                if (mProAMT > 0)
                {
                    result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ",
                             mREQNO, mPACTCODE, mRSIRCODE, mREQDAT, mMRFNO, mProAMT.ToString(), mAPPAMT.ToString(), nARRATION,
                             PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, qty.ToString(), paytype, payto,
                             ppdamt.ToString(), posteddat, supcode, spcfcod, adjcod, type, termncon, payofmod, bundleno, billno, bankcode, refnum, Approval,
                             advanced, attnper, deptcode, chckid, checkdat);
                }
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }



            if (type == "FinalAppr")
            {
                switch (comcod)
                {
                    // case "3101":
                    case "1103":
                        //if (bankcode != "000000000000")
                        //{

                        //    result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATECONTRAVOUCHER",
                        //            mREQNO, "", "", mREQDAT, mMRFNO, "", "", nARRATION,
                        //            PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", paytype, payto, "", posteddat, supcode, termncon, payofmod, "", "", "");

                        //    if (!result)
                        //    {
                        //        ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                        //        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //        return;
                        //    }

                        //}
                        bool advanced = this.chkAdvanced.Checked ? true : false;
                        if (adjcod != "000000000000" && advanced)
                        {

                            result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "NONADJUSTMENT",
                                        mREQNO, "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                            if (!result)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;


                            }

                        }



                        break;

                    default:
                        //if (adjcod != "000000000000")
                        //{
                        //    result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEJOURNAL",
                        //         mREQNO, "", "", mREQDAT, mMRFNO, "", "", nARRATION,
                        //         PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", paytype, payto, "", posteddat, supcode, termncon, payofmod, "", "", "");

                        //    if (!result)
                        //    {
                        //        ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                        //        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //        return;
                        //    }
                        //}


                        //if (adjcod != "000000000000")
                        //{
                        //    result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEJOURNAL",
                        //         mREQNO, "", "", mREQDAT, mMRFNO, "", "", nARRATION,
                        //         PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", paytype, payto, "", posteddat, supcode, termncon, payofmod, "", "", "");

                        //    if (!result)
                        //    {
                        //        ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                        //        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //        return;
                        //    }
                        //}

                        if (bankcode != "000000000000")
                        {

                            result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATECONTRAVOUCHER",
                                    mREQNO, "", "", mREQDAT, mMRFNO, "", "", nARRATION,
                                    PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", paytype, payto, "", posteddat, supcode, termncon, payofmod, "", "", "");

                            if (!result)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }

                        }

                        break;


                }



                switch (comcod)
                {

                    case "3336":
                    case "3337":
                        // case "3101":

                        result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTORUPONLINEPAY",
                                    mREQNO, "", "", "", "", "", "", "",
                                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                            ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        break;


                    case "1103": // Tanvir

                        result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTORUPONLINEPAYTAN",
                            mREQNO, "", "", "", "", "", "", "",
                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                            ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        break;



                    case "3370"://CPDL 
                                //case "3101"://CPDL 

                        if (adjcod == "000000000000")
                        {
                            result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTORUPONLINEPAYTAN",
                                mREQNO, "", "", "", "", "", "", "",
                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                            if (!result)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                        }
                        break;



                    default:
                        break;

                }


            }




            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (hst["compsms"].ToString() == "True")
            {


                if (this.Request.QueryString["Type"] == "OreqEntry")
                {

                    switch (comcod)
                    {
                        case "3333":
                            break;

                        default:

                            string SMSHead = "Ready for First Approval";
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "OtherReqEntry?Type=OreqApproved";
                            DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWGENBILLAPIINFO", userid, frmname, "", "", "");
                            SendSmsProcess sms = new SendSmsProcess();
                            DataTable dt = ds3.Tables[0];
                            foreach (DataRow drs in dt.Rows)
                            {
                                string mobile = drs["phno"].ToString();
                                string SMSText = comnam + ":\n" + SMSHead + "\n" + "MRF No: " + txtMRFNo.Text + "\n" + "Thanks";
                                bool resultsms = sms.SendSmmsPwd(comcod, SMSText, mobile);
                            }
                            break;
                    }
                }

                if (this.Request.QueryString["Type"] == "OreqApproved")
                {


                    DataSet dsruaauser = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "SHOWREQAAPROVEUSER", mREQNO, "", "", "", "");


                    string rusername = dsruaauser.Tables[0].Rows[0]["rusername"].ToString();
                    string fausername = dsruaauser.Tables[0].Rows[0]["fausername"].ToString();
                    SendSmsProcess sms = new SendSmsProcess();
                    string comnam = hst["comnam"].ToString();
                    string compname = hst["compname"].ToString();
                    string frmname = "OtherReqEntry?Type=FinalAppr";

                    string SMSHead = "Ready for Final Approval(General Requisition)";


                    string SMSText = comnam + ":\n" + SMSHead + "\n" + "\n" + "MRF No: " + txtMRFNo.Text + "\n" + "Req. Entry: " + rusername + "\n" + "First Approved: " + fausername + "\n" + "Thanks";
                    bool resultsms = sms.SendSmms(SMSText, userid, frmname);
                }
            }

            if (hst["compmail"].ToString() == "True")
            {

                switch (comcod)
                {

                    case "1102"://IBCEL
                        if (this.Request.QueryString["Type"] == "OreqApproved" || this.Request.QueryString["Type"] == "FirstRecom" || this.Request.QueryString["Type"] == "SecRecom")
                        {


                            DataSet dsruaauser = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "SHOWREQAAPROVEUSERMAIL", mREQNO, "", "", "", "");


                            string rusername = dsruaauser.Tables[0].Rows[0]["rusername"].ToString();
                            string fausername = dsruaauser.Tables[0].Rows[0]["fausername"].ToString();
                            string secapname = dsruaauser.Tables[0].Rows[0]["secapname"].ToString();
                            string thrapname = dsruaauser.Tables[0].Rows[0]["thrapname"].ToString();
                            SendMailProcess objsendmail = new SendMailProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = this.Request.QueryString["Type"] == "OreqApproved" ? "OtherReqEntry?Type=FirstRecom"
                                    : this.Request.QueryString["Type"] == "FirstRecom" ? "OtherReqEntry?Type=SecRecom" : "OtherReqEntry?Type=FinalAppr";

                            string subject = this.Request.QueryString["Type"] == "OreqApproved" ? "Ready for Forwared"
                                    : this.Request.QueryString["Type"] == "FirstRecom" ? "Ready for Approval" : "Ready for Final Approval";

                            string SMSHead = this.Request.QueryString["Type"] == "OreqApproved" ? "Ready for Forwared(General Requisition)"
                                    : this.Request.QueryString["Type"] == "FirstRecom" ? "Ready for Approval(General Requisition)" : "Ready for Final Approval(General Requisition)";
                            // string subject = "Ready for Final Approval";
                            //string SMSHead = "Ready for Final Approval(General Requisition)";


                            string reqno = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text;
                            string SMSText = comnam + "\n" + SMSHead + "\n" + "\n" + "Req No: " + reqno + "\n" + "Req. Entry: " + rusername
                                + (fausername.Length == 0 ? "" : "\n") + (fausername.Length == 0 ? "" : ("First Approved: " + fausername)) + (secapname.Length == 0 ? "" : "\n")
                                  + (secapname.Length == 0 ? "" : ("Second Approved: " + secapname)) + (thrapname.Length == 0 ? "" : "\n") + (thrapname.Length == 0 ? "" : ("Third Approved: " + thrapname));



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
                    default:
                        break;


                }


            }

            //Compmany Mail


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Update Req";
                string eventdesc2 = mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void lbtnOreqChecked_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            this.Session_tblReq_Update();
            string comcod = this.GetCompCode();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.Request.QueryString["genno"].ToString();
            string type = this.Request.QueryString["Type"].ToString();

            //log Report



            DataTable dtuser = (DataTable)Session["tblUserReq"];

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = "";
            string Posttrmid = "";
            string PostSession = "";
            string posteddat = "";
            string ApprovByid, approvdat, Approvtrmid, ApprovSession;
            //Skip Approval

            switch (comcod)
            {
                case "3370":  //CPDL
                case "3101":
                    ApprovByid = userid;
                    approvdat = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    Approvtrmid = Terminal;
                    ApprovSession = Sessionid;
                    break;

                default:
                    ApprovByid = "";
                    approvdat = "01-Jan-1900";
                    Approvtrmid = "";
                    ApprovSession = "";
                    break;


            }




            /////log end

            string nARRATION = txtReqNarr.Text.Trim();

            string paytype = this.rblpaytype.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();
            string supcode = ddlSupplier.SelectedValue.ToString();
            string termncon = this.termncon.Text.ToString();
            string payofmod = this.mofpay.Text.ToString();
            string deptcode = this.ddlDeptCode.SelectedValue.ToString();
            supcode = (supcode.Trim() == "" ? "000000000000" : supcode);
            supcode = dtuser.Rows[0]["supcode"].ToString();

            DataTable tbl1 = (DataTable)Session["tblReq"];
            bool result = false;
            string adjcod = this.ddlactcode.SelectedValue.ToString();
            string bundleno = this.ddlBundle.SelectedValue.ToString().Trim();

            // todo for other req checked 
            string chckid = userid;
            string checkdat = System.DateTime.Today.ToString("dd-MMM-yyyy");


            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string refnum = this.txtRefNum.Text.Trim();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mPACTCODE = tbl1.Rows[i]["pactcode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                string billno = tbl1.Rows[i]["billno"].ToString();
                double mProAMT = Convert.ToDouble(tbl1.Rows[i]["proamt"]);
                double mAPPAMT = Convert.ToDouble(tbl1.Rows[i]["appamt"]);
                double qty = Convert.ToDouble(tbl1.Rows[i]["qty"]);
                double rate = Convert.ToDouble(tbl1.Rows[i]["rate"]);
                double ppdamt = Convert.ToDouble(tbl1.Rows[i]["ppdamt"]);
                string appxml = tbl1.Rows[i]["approval"].ToString();
                string Approval = this.GetReqApproval(appxml);
                string advanced = this.chkAdvanced.Checked ? "1" : "0";
                string attnper = this.txtAttn.Text.ToString();

                if (mProAMT > 0)
                {
                    result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "OTHERREQCHECKED",
                             mREQNO, mPACTCODE, mRSIRCODE, mREQDAT, mMRFNO, mProAMT.ToString(), mAPPAMT.ToString(), nARRATION,
                             PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, qty.ToString(), paytype, payto,
                             ppdamt.ToString(), posteddat, supcode, spcfcod, adjcod, type, termncon, payofmod, bundleno, billno, bankcode, refnum, Approval,
                             advanced, attnper, deptcode, chckid, checkdat);
                }
                if (!result)
                {
                    string message = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + message + "');", true);
                    return;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Updated successfully" + "');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Update Req";
                string eventdesc2 = mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private bool SendSSLMail(string subject, string SMSText, string userid, string frmname)
        {
            try
            {

                string comcod = this.GetCompCode();
                DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", userid, "", "", "", "", "", "", "", "");
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWMAILAPIINFO", userid, frmname, "", "", "");

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
                    string currentptah = "RptEngInterface?Type=Report&comcod=" + comcod + "&usrid=" + usrid;
                    string totalpath = uhostname + currentptah;


                    string body = "<pre>";

                    body += "Dear Sir,";
                    body += "\n" + SMSText + "\n" +
                    "<div style='float:left;  padding:10px; background:Lavender; width:150px; height:40px; text-align:center '>" +
                    "<a href='" + totalpath + "' style='float:left; align:center; padding:10px; padding-left:40px; padding-right:45px;background:darkorange; color:white;text-decoration:none; text-align:center''> Click </a></div>";
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

        private void UpdateAutoJrnl()
        {
            string comcod = this.GetCompCode();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            bool result = true;


            result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ", mMRFNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

        }


        protected void gvOtherReq_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            this.gvOtherReq.DataSource = tbl1;
            this.gvOtherReq.DataBind();
            this.FooterVallue();
        }


        protected void FooterVallue()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            if (tbl1.Rows.Count == 0)
                return;


            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(bgdamt)", "")) ?
                0.00 : tbl1.Compute("Sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFPaidAmtxx")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(ppdamt)", "")) ?
                0.00 : tbl1.Compute("Sum(ppdamt)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(trnamt)", "")) ?
                0.00 : tbl1.Compute("Sum(trnamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBalamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(balamt)", "")) ?
                0.00 : tbl1.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFProposedamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(appamt)", "")) ?
                0.00 : tbl1.Compute("Sum(proamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFAppamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(appamt)", "")) ?
             0.00 : tbl1.Compute("Sum(appamt)", ""))).ToString("#,##0;(#,##0); ");





        }


        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            // int TblRowIndex2;
            string type = this.Request.QueryString["Type"].ToString();

            for (int i = 0; i < this.gvOtherReq.Rows.Count; i++)
            {
                //TblRowIndex2 = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + i;

                double qty = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvQtamt")).Text.Trim());
                double rate = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvRate")).Text.Trim());

                double Proamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvProposedamt")).Text.Trim());
                double appamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvApamt")).Text.Trim());
                string billno = ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvBillno")).Text.Trim();

                string comcod = this.GetCompCode();


                rate = rate > 0 ? rate : (qty > 0 ? (Proamt / qty) : 0.00);
                Proamt = (type == "OreqEntry" || type == "mgtOreqEntry") ? (rate > 0 ? qty * rate : Proamt) : Proamt;
                appamt = (type == "OreqEntry" || type == "mgtOreqEntry") ? 0.00 : (rate > 0 ? qty * rate : appamt);

                tbl1.Rows[i]["proamt"] = Proamt;// qty* rate; proamt
                tbl1.Rows[i]["appamt"] = appamt; //qty * rate;//appamt;
                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["rate"] = rate;
                tbl1.Rows[i]["billno"] = billno;

            }
            Session["tblReq"] = tbl1;
        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvOtherReq_DataBind();

        }

        private void GeResVisibility()
        {
            DataTable dtp = (DataTable)Session["tblproject"];
            string actcode = this.ddlProjectName.SelectedValue.ToString();
            string actelev = dtp.Select("actcode='" + actcode + "'")[0]["actelev"].ToString().Trim();

            if (actelev == "2")
            {
                this.GroupBind();
                this.lblMatGroup.Visible = true;
                this.ddlMatGrp.Visible = true;
            }
            else
            {
                this.ddlMatGrp.Items.Clear();
                this.lblMatGroup.Visible = false;
                this.ddlMatGrp.Visible = false;
            }
        }

        private void ProjectData()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            if (pactcode.Substring(0, 2) == "16")
            {

                string GroupCode = this.ddlMatGrp.SelectedValue.ToString();

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBAL", pactcode, GroupCode, CurDate1, "", "", "", "", "", "");

                DataTable dt = ds2.Tables[0];
                DataTable dt1 = (DataTable)Session["tblReq"];

                foreach (DataRow dr1 in dt.Rows)
                {
                    string actcode = dr1["pactcode"].ToString();
                    string rescode = dr1["rsircode"].ToString();
                    string spcfcod = dr1["spcfcod"].ToString();
                    DataRow[] dr2 = dt1.Select("pactcode = '" + actcode + "' and rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
                    if (dr2.Length == 0)
                        dt1.ImportRow(dr1);
                }


                Session["tblReq"] = this.HiddenSameData(dt1);
                if (ds2 == null)
                    return;
                ds2.Dispose();
                this.gvOtherReq_DataBind();


            }

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GeResVisibility();
            string comcod = this.GetCompCode();
            if (comcod == "3338" || comcod == "3101" || comcod == "3348" || comcod=="3368")
                return;

            this.ProjectData();


        }

        protected void gvOtherReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblReq"];
            string comcod = this.GetCompCode();
            int rowindex = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.RowIndex;
            string pactcode = dt.Rows[rowindex]["pactcode"].ToString();
            // string reqno = "REQ" + this.txtCurReqDate.Text.Substring(6, 4) + this.lblCurReqNo1.Text.Substring(3, 2) + this.txtCurReqNo2.Text.ToString();//((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvreqf")).Text.Trim();

            string mREQDAT = this.txtCurReqDate.Text.Trim();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            string rsircode = ((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEREQRES", mREQNO, pactcode, rsircode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //this.Get_Requisition_Info();
                //int rowindex = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            Session.Remove("tblReq");
            Session["tblReq"] = dv.ToTable();
            this.gvOtherReq_DataBind();
        }


        protected void ImgbtnFindGroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        protected void gvOtherReq_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvOtherReq.EditIndex = -1;
            this.gvOtherReq_DataBind();

        }
        protected void gvOtherReq_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvOtherReq.EditIndex = e.NewEditIndex;
            this.gvOtherReq_DataBind();


            string comcod = this.GetCompCode();
            int rowindex = (gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)Session["tblReq"]).Rows[rowindex]["pactcode"].ToString();
            string subcode = ((DataTable)Session["tblReq"]).Rows[rowindex]["rsircode"].ToString();
            //double txtgvQty = Convert.ToDouble("0"+((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvQty")).Text.Trim());


            //double txtgvRate =Convert.ToDouble("0"+ ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvRate")).Text.Trim());
            //double txtgvCrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvCrAmt")).Text.Trim());

            //double txtgvDrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvDrAmt")).Text.Trim());

            DropDownList ddlgrdacccode = (DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlgrdacccode");
            ViewState["gindex"] = e.NewEditIndex;




            DataTable dt2 = (DataTable)Session["tblproject"];


            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            ddlgrdacccode.SelectedValue = actcode;




            //ddlgrdresouce.SelectedValue = actcode; 
            DataTable dt01 = (DataTable)Session["tblproject"];
            string search1 = ddlgrdacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode");

            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((Label)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = true;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = true;

                //string SearchResourche = "%"; // +((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Text.Trim() + "%";

                //DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, SearchResourche, "", "", "", "", "", "", "");
                //DataTable dt3 = ds3.Tables[0];
                //Session["HeadRsc1"] = ds3.Tables[0];


                //List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                //lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);
                //Session["HeadRsc1"] = ds3.Tables[0];

                //var lst1 = lst.OrderBy(x => x.rescode);
                //this.ddlMatGrp.DataSource = lst1;
                //this.ddlMatGrp.DataTextField = "resdesc1";
                //this.ddlMatGrp.DataValueField = "rescode";
                //this.ddlMatGrp.DataBind();







                string filter1 = "%%";
                string SearchInfo = "";


                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {
                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {
                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(rescode," + len + ") between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(rescode," + len + ")" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);


                var lst1 = lst.OrderBy(x => x.rescode);
                ddlgrdresouce.DataSource = lst1;
                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataBind();
                ddlgrdresouce.SelectedValue = subcode;
                ddlgrdresouce.Focus();





                //SP_ENTRY_ACCOUNTS_BUDGET", "OTHERGRP",





            }
            else
            {

                ((Label)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = false;
                //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Visible = false;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = false;


            }
            //---------------------------------------------//
            //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Focus();
        }
        protected void gvOtherReq_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblReq"];
            int rowindex = (int)ViewState["gindex"];
            string pactcode = ((DataTable)Session["tblReq"]).Rows[rowindex]["pactcode"].ToString();
            string rsircode = ((DataTable)Session["tblReq"]).Rows[rowindex]["rsircode"].ToString();



            string type = this.Request.QueryString["Type"].ToString();
            DataRow[] dr2 = dt.Select("pactcode = '" + pactcode + "' and rsircode='" + rsircode + "'");
            string ResCode = "";
            if (dr2.Length > 0)
            {

                double qty = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvQtamt")).Text.Trim());
                double rate = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvRate")).Text.Trim());
                double Proamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvProposedamt")).Text.Trim());
                double appamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvApamt")).Text.Trim());

                rate = rate > 0 ? rate : (qty > 0 ? (Proamt / qty) : 0.00);
                Proamt = (type == "OreqEntry" || type == "mgtOreqEntry") ? (rate > 0 ? qty * rate : Proamt) : Proamt;
                appamt = (type == "OreqEntry" || type == "mgtOreqEntry") ? 0.00 : (rate > 0 ? qty * rate : appamt);


                dr2[0]["pactcode"] = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
                ResCode = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                dr2[0]["rsircode"] = ResCode;
                dr2[0]["pactdesc"] = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedItem.Text;
                dr2[0]["sirdesc"] = ResCode == "000000000000" ? "" : ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedItem.Text;
                dr2[0]["proamt"] = Proamt;// qty* rate; proamt
                dr2[0]["appamt"] = appamt; //qty * rate;//appamt;
                dr2[0]["qty"] = qty;
                dr2[0]["rate"] = rate;


            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string actcodeold = pactcode;
            string rescodeold = rsircode;
            string mREQDAT = this.txtCurReqDate.Text.Trim();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            string actcode1 = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string subcode1 = ResCode;

            string mqty = dr2[0]["qty"].ToString();
            string mProAMT = dr2[0]["proamt"].ToString();
            string mAPPAMT = dr2[0]["appamt"].ToString();
            string nARRATION = txtReqNarr.Text.Trim();

            string paytype = this.rblpaytype.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();
            string supllcode = ddlSupplier.SelectedValue.ToString().Trim() == "" ? "000000000000" : this.ddlSupplier.SelectedValue.ToString();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "EDITOTHERREQSITION",
                        mREQNO, mMRFNO, actcodeold, rescodeold, actcode1, subcode1, mqty, mProAMT, mAPPAMT, paytype, payto, supllcode, nARRATION, userid, Terminal, userdate, "", "", "", "", "", "", "");










            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            this.gvOtherReq.EditIndex = -1;


            Session["tblReq"] = HiddenSameData(dt);
            DataView dv = dt.DefaultView;
            dv.Sort = "pactcode,rsircode";
            dt = dv.ToTable();

            this.Get_Requisition_Info();
            // this.gvOtherReq_DataBind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            //dv.Sort = "pactcode,rsircode";
            dv.Sort = "pactcode";
            dt1 = dv.ToTable();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {

                    dt1.Rows[j]["pactdesc"] = "";
                }



                pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }

        protected void ddlgrdacccode_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            DataTable dt01 = (DataTable)Session["tblproject"];
            string search1 = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).Text;
            DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode");
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                ((Label)this.gvOtherReq.Rows[rowindex].FindControl("lblgvreshead")).Visible = true;
                ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                this.GetgrdResource();

            }

            else
            {
                ((Label)this.gvOtherReq.Rows[rowindex].FindControl("lblgvreshead")).Visible = false;
                ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                ddlgrdresouce.Items.Clear();


            }

        }

        private void GetgrdResource()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();

                int rowindex = (int)ViewState["gindex"];
                DropDownList ddlactcode = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode");
                DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode");

                string actcode = ddlactcode.SelectedValue.ToString();
                string filter1 = "%";

                string oldRescode = (ddlgrdresouce.Items.Count == 0) ? "" : ddlgrdresouce.SelectedValue.ToString();

                string SearchInfo = "";

                DataTable dt = (DataTable)Session["tblproject"];
                DataRow[] dr1 = dt.Select("actcode='" + actcode + "'");
                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {


                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);
                Session["HeadRsc1"] = lst;
                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataSource = lst;
                ddlgrdresouce.DataBind();
                ddlgrdresouce.Focus();

                List<RealEntity.C_17_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    ddlgrdresouce.SelectedValue = oldRescode;

                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void GeResVisibilityAdj()
        {
            DataTable dtp = (DataTable)Session["tblproject"];
            string actcode = this.ddlactcode.SelectedValue.ToString();
            string actelev = (dtp.Select("actcode='" + actcode + "'").Length == 0) ? "" : dtp.Select("actcode='" + actcode + "'")[0]["actelev"].ToString().Trim();

            if (actelev == "2")
            {
                this.GroupBindadj();
                this.lblrescodeadj.Visible = true;
                this.ddlSupplier.Visible = true;
            }
            else
            {
                this.ddlSupplier.Items.Clear();
                this.lblrescodeadj.Visible = false;
                this.ddlSupplier.Visible = false;
            }
        }
        protected void ddlactcode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GeResVisibilityAdj();

        }
        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {

        }
        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChequeNo();
        }
        protected void ddlcheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlcheque.Items.Count == 0)
                return;
            this.txtRefNum.Text = this.ddlcheque.SelectedItem.Text;
        }
        protected void chkAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdvanced.Checked == true)
            {
                this.lbladvanced.Text = "Advanced";
            }
            else
            {
                this.lbladvanced.Text = "Adjusted";
            }

        }

        protected void lbtpath_Click(object sender, EventArgs e)
        {
           string auth = HttpContext.Current.Request.Url.Authority;


          
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string portAdd = hst["portnum"].ToString().Length==0?"": (":" + hst["portnum"].ToString());

            //string fullurl= HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            //string application = HttpContext.Current.Request.ApplicationPath;
            //fullurl = fullurl.Replace("//", "/");
            //string []array = fullurl.Split('/');
            //string ipwithport = array[1];
            //int indexoffapp = fullurl.IndexOf('/');
            ////string ipwithport = array[1];
            ////  int portStart = ipString.LastIndexOf(':');

            //string add="";
            //foreach (string item in array)
            //{
            //    add= add+","+ item.ToString();


            //}

            string HostAdd = HttpContext.Current.Request.Url.Host;
          
            string uhostname = "http://" + HostAdd+ portAdd + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";           
            string currentptah = "RptEngInterface?Type=Report";
            string totalpath = uhostname + currentptah;
            string autvshanport = "author:" + auth+"host add:"+ portAdd + " End"+" ";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + autvshanport + "');", true);

        }
    }
}
