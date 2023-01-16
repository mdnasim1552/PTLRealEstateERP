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
namespace RealERPWEB.F_13_Cen
{
    public partial class EntryLostSoldADes : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS ISSUE STATUS";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();



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


        private void GetProjectName()
        {


            string comcod = this.GetCompCode();
            string Srchname = "%" + this.txtsrchProject.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();
        }

        private void GetMatList()
        {
            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = this.txtResSearch.Text.Trim() + "%";
            string date = this.txtCurDate.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETMATLIST", mProject, mSrchTxt, date, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlResList.Items.Clear();
                this.ddlResSpcf.Items.Clear();
                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];

            this.ddlResList.DataTextField = "rsirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[1];
            this.ddlResList.DataBind();
            this.GetSpecification();

        }

        private void GetSpecification()
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblMat"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetMatList();
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            this.GetSpecification();

        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();

        }

        protected void GetLSDNo()
        {

            string comcod = GetCompCode();
            string mLSDNo = "NEWLSD";
            if (this.ddlPreList.Items.Count > 0)
                mLSDNo = this.ddlPreList.SelectedValue.ToString();

            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();


            if (mLSDNo == "NEWLSD")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETLSDNO", date,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxlsdno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxlsdno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxlsdno1";
                    this.ddlPreList.DataValueField = "maxlsdno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }

        }

        private void PreLSDList()
        {


            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPRELSDLIST", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPreList.DataTextField = "lsdno1";
            this.ddlPreList.DataValueField = "lsdno";
            this.ddlPreList.DataSource = ds1.Tables[0];
            this.ddlPreList.DataBind();


        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void ImgbtnFindPrevious_Click(object sender, EventArgs e)
        {
            this.PreLSDList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblddlProject.Text = this.ddlProject.SelectedItem.Text;
                this.ddlProject.Visible = false;
                this.lblddlProject.Visible = true;
                this.PanelSub.Visible = true;
                this.lblPreViousList.Visible = false;
                this.txtSrchPrevious.Visible = false;
                this.ImgbtnFindPrevious.Visible = false;
                this.ddlPreList.Visible = false;
                this.GetLSDInfo();

                return;
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblddlProject.Visible = false;
            this.txtCurDate.Enabled = true;
            this.PanelSub.Visible = false;
            this.lblPreViousList.Visible = true;
            this.txtSrchPrevious.Visible = true;
            this.ImgbtnFindPrevious.Visible = true;
            this.ddlPreList.Visible = true;
            this.ddlPreList.Items.Clear();
            this.ddlResList.Items.Clear();
            this.ddlResSpcf.Items.Clear();
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
        }


        private void GetLSDInfo()
        {


            ViewState.Remove("tblmattrns");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mLSDNo = "NEWLSD";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mLSDNo = this.ddlPreList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETLSDINFO", mLSDNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tbllsd"] = ds1.Tables[0];


            if (mLSDNo == "NEWLSD")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETLSDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxlsdno1"].ToString().Trim().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxlsdno1"].ToString().Trim().Substring(6);
                return;
            }



            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text;
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lsddate"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["lsdno1"].ToString().Trim().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["lsdno1"].ToString().Trim().Substring(6);
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            this.grvacc.DataSource = (DataTable)ViewState["tbllsd"];
            this.grvacc.DataBind();
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tbllsd"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(lsdam)", "")) ?
            0.00 : dt1.Compute("sum(lsdam)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }




        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tbllsd"];

            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                double lqty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtgvlqty")).Text.Trim());
                double sqty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtgvsqty")).Text.Trim());
                double dqty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtgvdqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((Label)this.grvacc.Rows[i].FindControl("lblgvstkrate")).Text.Trim());
                int rowindex = ((this.grvacc.PageIndex) * (this.grvacc.PageSize)) + i;
                dt1.Rows[rowindex]["lqty"] = lqty;
                dt1.Rows[rowindex]["sqty"] = sqty;
                dt1.Rows[rowindex]["dqty"] = dqty;
                dt1.Rows[rowindex]["lsdam"] = (lqty + sqty + dqty) * rate;
            }
            ViewState["tbllsd"] = dt1;
        }
        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }



        protected void lnkupdate_Click(object sender, EventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbllsd"]; ;
            string curdate = this.txtCurDate.Text.ToString().Trim();
            if (this.ddlPreList.Items.Count == 0)
                this.GetLSDNo();
            string lsdno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();
            string Refno = this.txtrefno.Text.ToString();
            if (Refno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Ref. No. Should Not Be Empty";
                return;
            }

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "CHECKEDDUPREFNO", Refno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("lsdno <>'" + lsdno + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Ref. No.";
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }


            string pactcode = this.ddlProject.SelectedValue.ToString();
            foreach (DataRow dr in dt.Rows)
            {
                string rsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string lqty = dr["lqty"].ToString().Trim();
                string sqty = dr["sqty"].ToString().Trim();
                string dqty = dr["dqty"].ToString().Trim();
                string rate = dr["stkrate"].ToString().Trim();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "INSORUPLSDINF", lsdno, pactcode, rsircode, spcfcod,
                    curdate, Refno, lqty, sqty, dqty, rate, PostedByid, Posttrmid, PostSession, Posteddat, "");
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.txtCurDate.Enabled = false;


        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {



            //this.Panel2.Visible = true;
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tbllsd"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string Specification = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["stkqty"] = dr3[0]["stkqty"];
                dr1["stkrate"] = dr3[0]["stkrate"]; ;
                dr1["lqty"] = 0;
                dr1["sqty"] = 0;
                dr1["dqty"] = 0;
                dr1["lsdam"] = 0;
                tbl1.Rows.Add(dr1);
            }

            ViewState["tbllsd"] = tbl1;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


    }
}