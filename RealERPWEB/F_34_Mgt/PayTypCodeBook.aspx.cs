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
namespace RealERPWEB.F_34_Mgt
{

    public partial class PayTypCodeBook : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.Getuser();
                this.Get_Receive_Info();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Signature Margin";
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void Getuser()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtUserSearch1.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //if (this.lbtnOk.Text == "New")
            //{

            //    this.ddlUserList.Enabled = true;

            //    this.ddlProjectName.Enabled = true;
            //   ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //    this.txtProSearch.Text = "";
            //    this.ddlProjectName.Items.Clear();
            //    this.gvProLinkInfo.DataSource = null;
            //    this.gvProLinkInfo.DataBind();
            //    this.Panel2.Visible = false;
            //    this.lbtnOk.Text = "Ok";
            //    return;
            //}



            //this.ddlUserList.Enabled = false;

            //this.Panel2.Visible = true;
            //this.lbtnOk.Text = "New";

            //this.Get_Receive_Info();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //ReportClass rptstk = null;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "1")
            //{
            //    DataTable dt = (DataTable)Session["tblMSR"];
            //   RealERPRPT.R_03_Pro.RptPurMktSurvey rptstk1 = new RealERPRPT.R_03_Pro.RptPurMktSurvey() ;
            //    rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //    Session["Report1"] = rptstk1;
            //    rptstk = rptstk1;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            //{
            //     RealERPRPT.R_03_Pro.RptMktSurveyMatWiseSupList rptstk2 = new RealERPRPT.R_03_Pro.RptMktSurveyMatWiseSupList()  ;
            //    rptstk2.SetDataSource((DataTable)Session["tbMargin"]);
            //    Session["Report1"] = rptstk2;
            //    rptstk = rptstk2;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            //{
            //    RealERPRPT.R_03_Pro.RptMktSurveySupWiseMatList rptstk3 = new RealERPRPT.R_03_Pro. RptMktSurveySupWiseMatList();
            //    rptstk3.SetDataSource((DataTable)Session["SuplRes"]);
            //    Session["Report1"] = rptstk3;
            //    rptstk = rptstk3;
            //}


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text =comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["companyaddress"] as TextObject;
            ////txtCompanyAddress.Text = ConstantInfo.ComAdd;
            //TextObject txtsurveynoname = rptstk.ReportDefinition.ReportObjects["surveynoname"] as TextObject;
            //txtsurveynoname.Text =this.lblCurMSRNo1.Text.Trim()+ this.txtCurMSRNo2.Text.ToString().Trim();
            //TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
            //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //txtnarrationname.Text = this.txtMSRNarr.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.Label1.Text;
            //    string eventdesc = "Print Report Survey";
            //    string eventdesc2 = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim(); 
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //this.lblprintstk.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void Get_Receive_Info()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "GETMARGINLINK", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbMargin"] = ds1.Tables[0];

            this.gvProLinkInfo_DataBind();

        }
        protected void gvProLinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbMargin"];
            this.gvMarLinkInfo.DataSource = tbl1;
            this.gvMarLinkInfo.DataBind();

        }

        private void Session_tbltbMargin_Update()
        {
            DataTable tbl1 = (DataTable)Session["tbMargin"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvMarLinkInfo.Rows.Count; j++)
            {
                double Minamt = Convert.ToDouble("0" + ((TextBox)this.gvMarLinkInfo.Rows[j].FindControl("gvMinamt")).Text.Trim());
                double Maxamt = Convert.ToDouble("0" + ((TextBox)this.gvMarLinkInfo.Rows[j].FindControl("gvMaxamt")).Text.Trim());

                TblRowIndex2 = (this.gvMarLinkInfo.PageIndex) * this.gvMarLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["minamt"] = Minamt.ToString();
                tbl1.Rows[TblRowIndex2]["maxamt"] = Maxamt.ToString();
            }
            Session["tbMargin"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbMargin_Update();
            DataTable tbl1 = (DataTable)Session["tbMargin"];
            string userid = this.ddlUserList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("userid = '" + userid + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                dr1["userdesc"] = this.ddlUserList.SelectedValue.ToString() + " - " + this.ddlUserList.SelectedItem.Text.Trim();
                dr1["minamt"] = 0.00;
                dr1["maxamt"] = 0.00;
                tbl1.Rows.Add(dr1);
            }
            Session["tbMargin"] = tbl1;
            int RowNo = 1;
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    if (tbl1.Rows[i]["pactcode"].ToString() == ProCode)
            //    {
            //        RowNo = i + 1;
            //        break;
            //    }
            //}
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvMarLinkInfo.PageSize);
            this.gvMarLinkInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvProLinkInfo_DataBind();
        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tbltbMargin_Update();
            DataTable tbl1 = (DataTable)Session["tbMargin"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string userid = tbl1.Rows[i]["userid"].ToString();
                string minamt = tbl1.Rows[i]["minamt"].ToString();
                string maxamt = tbl1.Rows[i]["maxamt"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYPROSAL", "INSERTUPDATEMARLINK",
                              userid, minamt, maxamt, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Margin user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        {
            this.Getuser();
        }


    }
}
