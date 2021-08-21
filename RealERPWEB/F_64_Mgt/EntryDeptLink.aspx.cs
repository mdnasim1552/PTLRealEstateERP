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
namespace RealERPWEB.F_64_Mgt
{

    public partial class EntryDeptLink : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "department link info";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.GetDeptName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetDeptName()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetCompCode();


            string mSrchTxt = this.txtsrchDept.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETDEPTNAME", mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddldeptlist.DataTextField = "deptdesc";
            this.ddldeptlist.DataValueField = "deptcode";
            this.ddldeptlist.DataSource = ds1.Tables[0];
            this.ddldeptlist.DataBind();
            ds1.Dispose();
        }

        protected void GetHRDeptName()
        {

            string comcod = this.GetCompCode();
            string FindProject = "%" + this.txtsrchhrdept.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETHRDEPTNAME", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlhrdept.DataTextField = "actdesc";
            this.ddlhrdept.DataValueField = "actcode";
            this.ddlhrdept.DataSource = ds1.Tables[0];
            this.ddlhrdept.DataBind();
            ViewState["LinkDept"] = ds1.Tables[0];
            foreach (ListItem myItem in ddlhrdept.Items)
            {
                string item = myItem.Value;

                string Link = (((DataTable)ViewState["LinkDept"]).Select("actcode='" + item + "'"))[0]["link"].ToString();
                if (Link == "2")
                {
                    myItem.Attributes.Add("style", "background-color:#a3ffa3");
                }
            }
            ds1.Dispose();



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddldeptlist.Enabled = true;


                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.txtsrchhrdept.Text = "";

                this.gvProLinkInfo.DataSource = null;
                this.gvProLinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }



            this.ddldeptlist.Enabled = false;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";

            this.GetInfo();
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
            //    rptstk2.SetDataSource((DataTable)Session["tbPreLink"]);
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


        private void GetInfo()
        {

            ViewState.Remove("tbLink");
            string comcod = this.GetCompCode();
            string UserCode = this.ddldeptlist.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SHOWDEPTINFO", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbLink"] = ds1.Tables[0];
            this.gvProLinkInfo_DataBind();
            ds1.Dispose();

        }
        protected void gvProLinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            this.gvProLinkInfo.DataSource = tbl1;
            this.gvProLinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvProLinkInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvProLinkInfo.PageIndex) * this.gvProLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tbLink"] = tbl1;
        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            string ProCode = this.ddlhrdept.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("actcode = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["actcode"] = this.ddlhrdept.SelectedValue.ToString();
                dr1["actdesc"] = this.ddlhrdept.SelectedItem.Text.Trim().Substring(13);
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tbLink"] = tbl1;
            this.gvProLinkInfo_DataBind();
        }


        protected void lbtnSelectall_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbLink"];

            for (int i = 0; i < this.ddlhrdept.Items.Count; i++)
            {
                string actcode = this.ddlhrdept.Items[i].Value;
                DataRow[] dr = dt.Select("actcode='" + actcode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["actcode"] = this.ddlhrdept.Items[i].Value;
                    dr1["actdesc"] = this.ddlhrdept.Items[i].Text.Substring(13);
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);
                }


            }

            ViewState["tbLink"] = dt;
            this.gvProLinkInfo_DataBind();

        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }


            string comcod = this.GetCompCode();
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            string dept = this.ddldeptlist.SelectedValue.ToString();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string pactcode = tbl1.Rows[i]["actcode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTUPDEPTLINKINF", dept, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void ImgbtnFindDept_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void Imgbtnhrdept_Click(object sender, EventArgs e)
        {
            this.GetHRDeptName();
        }


        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbLink"];
            string DeptName = this.ddldeptlist.SelectedValue.ToString();
            string HrDeptCode = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("lblgvBancCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEHRDEPT", DeptName, HrDeptCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbLink");
            ViewState["tbLink"] = dv.ToTable();
            this.gvProLinkInfo_DataBind();


        }


        protected void ddlhrdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem myItem in ddlhrdept.Items)
            {
                string item = myItem.Value;

                string Link = (((DataTable)ViewState["LinkDept"]).Select("actcode='" + item + "'"))[0]["link"].ToString();
                if (Link == "2")
                {
                    myItem.Attributes.Add("style", "background-color:#a3ffa3");
                }
            }
        }
    }
}