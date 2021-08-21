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
namespace RealERPWEB.F_39_MyPage
{
    public partial class EntryMonMarTarget : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Program";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void ImgbtnPrevious_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string SearchMrr = "%%";
            //string SearchMrr = this.txtSrchPreMRR.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETPREVPGNO", CurDate1, SearchMrr, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.ddlPrevList.DataTextField = "pgno1";
            this.ddlPrevList.DataValueField = "pgno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {
                //this.lblPreMRR.Visible = true;
                //this.txtSrchPreMRR.Visible = true;
                this.lbtnOk.Text = "Ok";

                this.lblPrevious.Visible = true;
                this.txtSrchPrevious.Visible = true;
                this.ImgbtnPrevious.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();

                this.lblCurNo1.Text = "MRR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurDate.Enabled = true;
                this.txtRefno.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                this.gvPrjInfo.DataSource = null;
                this.gvPrjInfo.DataBind();

                return;
            }

            //this.lblPreMRR.Visible = false;
            //this.txtSrchPreMRR.Visible = false;

            this.lblPrevious.Visible = false;
            this.txtSrchPrevious.Visible = false;
            this.ImgbtnPrevious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.lbtnOk.Text = "New";

            this.Get_Info();

        }
        protected void GetPGNo()
        {
            string comcod = this.GetComCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mPGNo = "NEWPG";
            if (this.ddlPrevList.Items.Count > 0)
                mPGNo = this.ddlPrevList.SelectedValue.ToString();

            if (mPGNo == "NEWPG")
            {
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETLASTPGNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 5);
                    this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(5, 4);
                    this.ddlPrevList.DataTextField = "maxno1";
                    this.ddlPrevList.DataValueField = "maxno";
                    this.ddlPrevList.DataSource = ds1.Tables[0];
                    this.ddlPrevList.DataBind();
                }

            }






        }

        protected void Get_Info()
        {

            string comcod = this.GetComCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mPGNo = "NEWPG";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPGNo = this.ddlPrevList.SelectedValue.ToString();

            }
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETPGINFO", mPGNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmonpg"] = this.HiddenSameData(ds1.Tables[0]);


            if (mPGNo == "NEWPG")
            {
                ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETLASTPGNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 5);
                    this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(5, 4);
                }
                this.Data_Bind();
                return;
            }




            this.txtRefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["pgdate"]).ToString("dd.MM.yyyy");
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["maxno1"].ToString().Substring(0, 5);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["maxno1"].ToString().Substring(5, 4);
            this.Data_Bind();
        }



        private void Data_Bind()
        {


            this.gvPrjInfo.DataSource = (DataTable)ViewState["tblmonpg"];
            this.gvPrjInfo.DataBind();
            this.FooterCalculation();



        }
        private void FooterCalculation()
        {


            DataTable dt = (DataTable)ViewState["tblmonpg"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(duration)", "")) ?
                                0 : dt.Compute("sum(duration)", ""))).ToString("#,##0;(#,##0); ");



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string deptcod = dt1.Rows[0]["deptcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                    dt1.Rows[j]["deptdesc"] = "";
                deptcod = dt1.Rows[j]["deptcod"].ToString();
            }
            return dt1;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblmonpg"];

            ReportDocument rptResource = new RealERPRPT.R_39_MyPage.rptMonthlyProgram();
            TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rpttxtComName.Text = comnam;


            TextObject txtProgramno = rptResource.ReportDefinition.ReportObjects["txtProgramno"] as TextObject;
            txtProgramno.Text = "Program No: " + this.lblCurNo1.Text + this.txtCurNo2.Text;
            TextObject txtpgdate = rptResource.ReportDefinition.ReportObjects["txtpgdate"] as TextObject;
            txtpgdate.Text = "Date: " + this.txtCurDate.Text;
            TextObject txtRefno = rptResource.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            txtRefno.Text = "Ref No: " + this.txtRefno.Text;
            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptResource.SetDataSource(dt);
            Session["Report1"] = rptResource;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        protected void lbtnCalculaton_Click(object sender, EventArgs e)
        {
            DateTime inidate, preenddate;
            DataTable dt = (DataTable)ViewState["tblmonpg"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {

                int duration = Convert.ToInt32("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvduration")).Text.Trim());

                if (i == 0)
                {
                    inidate = Convert.ToDateTime(this.GetStdDate(this.txtCurDate.Text));

                    if (duration > 0)
                    {

                        dt.Rows[i]["duration"] = duration;
                        dt.Rows[i]["tstdat"] = inidate;
                        dt.Rows[i]["tenddat"] = inidate.AddDays(duration - 1);
                    }
                }


                else
                {
                    //Previous Enddate
                    preenddate = Convert.ToDateTime(dt.Rows[i - 1]["tenddat"].ToString());


                    if (duration > 0)
                    {

                        dt.Rows[i]["duration"] = duration;
                        dt.Rows[i]["tstdat"] = preenddate.AddDays(1);
                        dt.Rows[i]["tenddat"] = preenddate.AddDays(duration);
                    }
                }





            }

            ViewState["tblmonpg"] = dt;
            this.Data_Bind();

        }
        private void SaveValue()
        {



            DateTime tstdat, tenddat, acstdat, acenddat;
            DataTable dt = (DataTable)ViewState["tblmonpg"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {
                double duration = Convert.ToDouble("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvduration")).Text.Trim());

                tstdat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvtStDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvtStDate")).Text.Trim());
                // tenddat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvTEndDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvTEndDate")).Text.Trim());
                tenddat = (tstdat.ToString("dd-MMM-yyyy") == "01-Jan-1900") ? tstdat : tstdat.AddDays(duration - 1);



                acstdat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacStDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacStDate")).Text.Trim());
                acenddat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacEndDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacEndDate")).Text.Trim());

                dt.Rows[i]["duration"] = duration;
                dt.Rows[i]["tstdat"] = tstdat;
                dt.Rows[i]["tenddat"] = tenddat;
                dt.Rows[i]["acstdat"] = acstdat;
                dt.Rows[i]["acenddat"] = acenddat;









            }

            ViewState["tblmonpg"] = dt;
            this.Data_Bind();




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
            Hashtable hst = (Hashtable)Session["tblLogin"];

            this.SaveValue();
            string comcod = hst["comcod"].ToString();

            if (this.ddlPrevList.Items.Count == 0)
                this.GetPGNo();
            DataTable dt = (DataTable)ViewState["tblmonpg"];
            string date = this.GetStdDate(this.txtCurDate.Text);
            string monpgno = this.lblCurNo1.Text.Trim().Substring(0, 2) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(2, 2) + this.txtCurNo2.Text.Trim();
            string refno = this.txtRefno.Text.Trim();
            bool result = false;
            result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSORUPEMPSTDKPI02B", monpgno, date, refno, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }
            foreach (DataRow dr2 in dt.Rows)
            {
                string Actcode = dr2["actcode"].ToString();
                string dur = Convert.ToDouble(dr2["duration"].ToString()).ToString();
                string tstrtdat = Convert.ToDateTime(dr2["tstdat"]).ToString("dd-MMM-yyyy");
                string tenddate = Convert.ToDateTime(dr2["tenddat"]).ToString("dd-MMM-yyyy");
                string acstrtdat = Convert.ToDateTime(dr2["acstdat"]).ToString("dd-MMM-yyyy");
                string acenddate = Convert.ToDateTime(dr2["acenddat"]).ToString("dd-MMM-yyyy");
                string empid = dr2["empid"].ToString();
                //Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSORUPEMPSTDKPI02", monpgno, Actcode, dur, tstrtdat, tenddate, acstrtdat, acenddate, empid, "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvPrjInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            //string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmonpg"];
            //string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            //string MatCode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //string spcfcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUE", mISUNO, MatCode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");


            int rowindex = (this.gvPrjInfo.PageSize) * (this.gvPrjInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmonpg");
            ViewState["tblmonpg"] = dv.ToTable();
            this.Data_Bind();


        }
        protected void gvPrjInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvPrjInfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvPrjInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {


            this.gvPrjInfo.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string comcod = this.GetComCode();
            int rowindex = (this.gvPrjInfo.PageSize) * (this.gvPrjInfo.PageIndex) + e.NewEditIndex;
            string empcode = ((DataTable)ViewState["tblmonpg"]).Rows[rowindex]["empid"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvPrjInfo.Rows[e.NewEditIndex].FindControl("ddlUserName");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = hst["deptcode"].ToString();
            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%" + ((TextBox)gvPrjInfo.Rows[e.NewEditIndex].FindControl("txtSearchUserName")).Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", SearchProject, "", deptcode, "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = empcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

        }

        protected void gvPrjInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int Index = gvPrjInfo.PageSize * gvPrjInfo.PageIndex + e.RowIndex;
            DataTable dt = (DataTable)ViewState["tblmonpg"];
            dt.Rows[Index]["empid"] = ((DropDownList)this.gvPrjInfo.Rows[e.RowIndex].FindControl("ddlUserName")).SelectedValue.ToString();
            dt.Rows[Index]["empname"] = ((DropDownList)this.gvPrjInfo.Rows[e.RowIndex].FindControl("ddlUserName")).SelectedItem.Text;

            this.gvPrjInfo.EditIndex = -1;
            this.Data_Bind();

        }

        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvPrjInfo.Rows[rowindex].FindControl("ddlUserName");
            string SearchProject = "%" + ((TextBox)gvPrjInfo.Rows[rowindex].FindControl("txtSearchUserName")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

    }
}



