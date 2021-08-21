
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
namespace RealERPWEB.F_15_DPayReg
{
    public partial class AccPayLimit : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Payment Limit Permission";

                this.Getuser();
                this.GetPaymentPer();
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void Getuser()
        {
            //if (this.lbtnOk.Text == "New")
            //    return;

            string comcod = this.GetCompCode();
            string mSrchTxt = this.txtUserSearch1.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
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
            //   EntertainmentRPT.R_03_Pro.RptPurMktSurvey rptstk1 = new EntertainmentRPT.R_03_Pro.RptPurMktSurvey() ;
            //    rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //    Session["Report1"] = rptstk1;
            //    rptstk = rptstk1;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            //{
            //     EntertainmentRPT.R_03_Pro.RptMktSurveyMatWiseSupList rptstk2 = new EntertainmentRPT.R_03_Pro.RptMktSurveyMatWiseSupList()  ;
            //    rptstk2.SetDataSource((DataTable)Session["tbPreLink"]);
            //    Session["Report1"] = rptstk2;
            //    rptstk = rptstk2;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            //{
            //    EntertainmentRPT.R_03_Pro.RptMktSurveySupWiseMatList rptstk3 = new EntertainmentRPT.R_03_Pro. RptMktSurveySupWiseMatList();
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


        private void GetPaymentPer()
        {

            ViewState.Remove("tbPayLimit");
            string comcod = this.GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETMARGINLINK", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbPayLimit"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }
        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tbPayLimit"];
            this.gvPayLimit.DataSource = tbl1;
            this.gvPayLimit.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbPayLimit"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvPayLimit.Rows.Count; j++)
            {
                string gvMinamt = Convert.ToDouble("0" + ((TextBox)this.gvPayLimit.Rows[j].FindControl("txtgvMinamt")).Text.Trim()).ToString("#,##0; (#,##0);");
                string gvMaxamt = Convert.ToDouble("0" + ((TextBox)this.gvPayLimit.Rows[j].FindControl("txtgvMaxamt")).Text.Trim()).ToString("#,##0; (#,##0);"); ;

                TblRowIndex2 = (this.gvPayLimit.PageIndex) * this.gvPayLimit.PageSize + j;
                tbl1.Rows[TblRowIndex2]["minamt"] = gvMinamt;
                tbl1.Rows[TblRowIndex2]["maxamt"] = gvMaxamt;
            }
            ViewState["tbPayLimit"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbPayLimit"];
            string Usreid = this.ddlUserList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("userid = '" + Usreid + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                dr1["userdesc"] = this.ddlUserList.SelectedItem.Text.Trim();
                dr1["minamt"] = 0.00;
                dr1["maxamt"] = 0.00;
                tbl1.Rows.Add(dr1);
            }
            ViewState["tbPayLimit"] = tbl1;
            this.Data_Bind();
        }


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbPayLimit"];

            for (int i = 0; i < this.ddlUserList.Items.Count; i++)
            {
                string userid = this.ddlUserList.Items[i].Value;
                DataRow[] dr = dt.Select("userid='" + userid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["userid"] = this.ddlUserList.Items[i].Value;
                    dr1["userdesc"] = this.ddlUserList.Items[i].Text.Trim();
                    dr1["minamt"] = 0.00;
                    dr1["maxamt"] = 0.00;
                    dt.Rows.Add(dr1);


                }


            }

            ViewState["tbPayLimit"] = dt;
            this.Data_Bind();
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            string comcod = this.GetCompCode();
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbPayLimit"];
            //string userid = this.ddlUserList.SelectedValue.ToString();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string userid = tbl1.Rows[i]["userid"].ToString();
                string Minamt = tbl1.Rows[i]["minamt"].ToString();
                string Maxamt = tbl1.Rows[i]["maxamt"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INSERTUPDATEMARLINK", userid, Minamt, Maxamt, "", "", "", "", "", "", "", "", "", "", "", "");

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
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        {
            this.Getuser();
        }


        //protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        //{
        //string comcod = this.GetCompCode();
        ////string UserName = this.ddlUserList.SelectedValue.ToString();       
        //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEALLBANKCODE", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //if (!result)
        //{
        //   ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
        //    return;
        //}

        //this.GetPaymentPer();
        //}
        protected void gvPayLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbPayLimit"];
            //string UserName = this.ddlUserList.SelectedValue.ToString();
            string Userid = ((Label)this.gvPayLimit.Rows[e.RowIndex].FindControl("lblgvUserid")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYCODE", Userid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvPayLimit.PageSize) * (this.gvPayLimit.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbPayLimit");
            ViewState["tbPayLimit"] = dv.ToTable();
            this.Data_Bind();
        }
    }
}