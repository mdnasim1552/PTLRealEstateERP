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
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class EntryComplain : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Entry Complain Form";

                this.txtCurCompDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();


            }


        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblComplain"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptComplain", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Complain Infromation"));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(17)));
            Rpt1.SetParameters(new ReportParameter("txtUnit", this.ddlUnit.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compNo", "Complain No : " + this.lblCurCompNo1.Text.ToString() + this.txtCurCompNo2.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref No : " + this.txtComRef.Text));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date : " + Convert.ToDateTime(this.txtCurCompDate.Text.Trim()).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtsrchproject.Text + "%";
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetUnit();

        }

        private void GetUnit()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtschUnit = "%" + this.txtunit.Text + "%";
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETUNITINF", pactcode, txtschUnit, "", "", "", "", "", "", "");
            this.ddlUnit.DataTextField = "udesc";
            this.ddlUnit.DataValueField = "usircode";
            this.ddlUnit.DataSource = ds1.Tables[0];
            this.ddlUnit.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.lbtnPrevCompList.Visible = true;

                this.ddlPrevCompList.Visible = true;
                this.ddlPrevCompList.Items.Clear();

                this.ddlProjectName.Visible = true;
                this.lblddlProject.Visible = false;
                this.txtCurCompDate.Enabled = true;
                this.ddlUnit.Enabled = true;
                this.lblCurCompNo1.Text = "COM" + DateTime.Today.ToString("MM") + "-";
                this.txtCurCompNo2.Text = "";
                //this.ddlUnit.Items.Clear();


                this.lblmsg1.Text = "";
                this.txtComRef.Text = "";
                this.gvComplain.DataSource = null;
                this.gvComplain.DataBind();
                return;
            }
            this.lbtnPrevCompList.Visible = false;
            this.ddlPrevCompList.Visible = false;

            this.lblddlProject.Text = this.ddlProjectName.SelectedItem.Text.Trim();
            this.lblddlunit.Text = this.ddlUnit.SelectedItem.Text.Trim();
            this.ddlProjectName.Visible = false;//it will be used
            this.lblddlProject.Visible = true;
            this.ddlUnit.Enabled = false;
            //this.lblddlunit.Visible = true;

            this.lbtnOk.Text = "New";
            this.Get_Comp_Info();
            //this.ibtnSearchMaterisl_Click(null, null);




        }

        private void Get_Comp_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlUnit.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurCompDate.Text.Trim()).ToString();
            string compno = "NEWCOMP";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevCompList.Items.Count > 0)
            {
                this.txtCurCompDate.Enabled = false;
                compno = this.ddlPrevCompList.SelectedValue.ToString();
            }
            ds1 = da.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCOMPLAINDATA", compno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblComplain"] = ds1.Tables[0];
            Session["UserLog"] = ds1.Tables[1];
            DataTable dt = (DataTable)Session["UserLog"];


            if (compno == "NEWCOMP")
            {
                ds1 = da.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETLASCOMPINFO", CurDate1,
                       "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurCompNo1.Text = ds1.Tables[0].Rows[0]["maxcompno1"].ToString().Substring(0, 6);
                    this.txtCurCompNo2.Text = ds1.Tables[0].Rows[0]["maxcompno1"].ToString().Substring(6, 5);

                }
                this.Data_Bind();

                return;
            }


            if (ddlPrevCompList.Items.Count > 0)
            {

                this.lblCurCompNo1.Text = ds1.Tables[1].Rows[0]["compno1"].ToString().Substring(0, 6);
                this.txtCurCompNo2.Text = ds1.Tables[1].Rows[0]["compno1"].ToString().Substring(6, 5);
                this.txtCurCompDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["compdat"]).ToString("dd-MMM-yyyy");
                this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
                this.ddlProjectName_SelectedIndexChanged(null, null);
                this.lblddlProject.Text = this.ddlProjectName.SelectedItem.Text.Trim();
                this.ddlUnit.SelectedValue = ds1.Tables[1].Rows[0]["usircode"].ToString();
                this.lblddlunit.Text = this.ddlUnit.SelectedItem.Text.Trim();

                //this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
                this.txtComRef.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                //txtComRef.this.txtsmcr.Text = ds1.Tables[1].Rows[0]["smcrno"].ToString();



            }




            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblComplain"];
            this.gvComplain.DataSource = tbl1;
            this.gvComplain.DataBind();
        }



        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblComplain"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string txtgvcomdat = ((TextBox)this.gvComplain.Rows[i].FindControl("txtgvcomdat")).Text.Trim();
                string txtComplain = ((TextBox)this.gvComplain.Rows[i].FindControl("txtComplain")).Text.Trim();

                // dt.Rows[i]["compdat"] = txtgvcomdat;
                dt.Rows[i]["comdesc"] = txtComplain;

            }
            Session["tblComplain"] = dt;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            this.lblmsg1.Visible = true;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");


            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = tblPostedByid == "" ? userid : tblPostedByid;
            string Posttrmid = tblPostedtrmid == "" ? Terminal : tblPostedtrmid;
            string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = userid;
            string Editdat = System.DateTime.Today.ToString("dd-MMM-yyyy");

            //this.SaveValue();

            this.SaveValue();
            DataTable tbl2 = (DataTable)Session["tblComplain"];


            string comcod = this.GetCompCode();
            if (ddlPrevCompList.Items.Count == 0)
            {
                // this.GetPerMatIssu();
            }

            string compno = this.lblCurCompNo1.Text.Trim().Substring(0, 3) + this.txtCurCompDate.Text.Trim().Substring(7, 4) + this.lblCurCompNo1.Text.Trim().Substring(3, 2) + this.txtCurCompNo2.Text.Trim();
            string comdat = Convert.ToDateTime(this.txtCurCompDate.Text.Trim()).ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString().Trim();
            string usircode = this.ddlUnit.SelectedValue.ToString();
            string compRef = this.txtComRef.Text;

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "INSERTUPDATECOMP", "COMPLAINB",
                             compno, comdat, pactcode, usircode, compRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "", "");
            if (!result)
            {
                this.lblmsg1.Text = "Update Fail !!!";
                return;
            }





            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                //string compno1 = tbl2.Rows[i]["compno"].ToString();
                string compcod = tbl2.Rows[i]["compcod"].ToString();



                string comdesc = tbl2.Rows[i]["comdesc"].ToString();






                if (comdesc.Length > 0)
                {
                    result = da.UpdateTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "INSERTUPDATECOMP", "COMPLAINA", compno, compcod,
                             comdesc, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        this.lblmsg1.Text = "Update Fail !!";
                        return;
                    }

                    else
                    {
                        this.lblmsg1.Text = "Update Successfull";
                    }

                }



            }

        }


        protected void lbtnFindProject_Click(object sender, EventArgs e)
        {
            GetProjectName();
        }
        protected void btnunitseach_Click(object sender, EventArgs e)
        {
            this.GetUnit();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnit();
        }
        protected void lbtnPrevCompList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurCompDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "PREVIOUSCOMPLIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevCompList.Items.Clear();
            this.ddlPrevCompList.DataTextField = "compno1";
            this.ddlPrevCompList.DataValueField = "compno";
            this.ddlPrevCompList.DataSource = ds1.Tables[0];
            this.ddlPrevCompList.DataBind();


        }



    }
}