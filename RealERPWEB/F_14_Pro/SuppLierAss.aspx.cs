using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class SuppLierAss : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Assessment";
                this.SupplierList();

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }

        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void SupplierList()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%" + this.txtSupPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSUPPLIER", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlSuplist.DataTextField = "resdesc";
            this.ddlSuplist.DataValueField = "rescode";
            this.ddlSuplist.DataSource = ds1.Tables[0];
            this.ddlSuplist.DataBind();
            ViewState["tblSup"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlSuplist_OnSelectedIndexChanged(null, null);
        }

        //private void GetMaterialList()
        //{
        //    string comcod = this.GetComeCode();
        //    string Srchmaterial = "%" + this.txtSupPro.Text.Trim() + "%";
        //    DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMATERIAL", Srchmaterial, "", "", "", "", "", "", "", "");
        //    this.ddlmatlist.DataTextField = "sirdesc";
        //    this.ddlmatlist.DataValueField = "rsircode";
        //    this.ddlmatlist.DataSource = ds1.Tables[0];
        //    this.ddlmatlist.DataBind();
        //    ds1.Dispose();

        //}

        protected void ddlSuplist_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string Srchmaterial = this.ddlSuplist.SelectedValue;
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMATERIAL", Srchmaterial, "", "", "", "", "", "", "", "");
            this.ddlmatlist.DataTextField = "sirdesc";
            this.ddlmatlist.DataValueField = "rsircode";
            this.ddlmatlist.DataSource = ds1.Tables[0];
            this.ddlmatlist.DataBind();
            ViewState["tblMat"] = ds1.Tables[0];
            ds1.Dispose();

        }
        private void GetAssGenInfo()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSUPASSGENINFO", "", "",
                         "", "", "", "", "", "", "");


            ViewState["tblAss"] = ds1.Tables[0];
            this.Data_DataBind();
        }

        private void Data_DataBind()
        {

            this.gvAssessment.DataSource = (DataTable)ViewState["tblAss"];
            this.gvAssessment.DataBind();
        }

        private void ShowAssInfo()
        {
            ViewState.Remove("tblAss");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mAssNo = "NEWASS";
            if (this.ddlPrevAssNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mAssNo = this.ddlPrevAssNo.SelectedValue.ToString();
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSUPASSINFO", mAssNo, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblAss"] = ds1.Tables[0];



            if (mAssNo == "NEWASS")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTSUPASSINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxsupassno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxsupassno1"].ToString().Substring(6);
                this.GetAssGenInfo();
                return;

            }
            ViewState["tblAss1"] = ds1.Tables[1];

            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["assno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["assno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["assdate"]).ToString("dd-MMM-yyyy");
            this.ddlSuplist.SelectedValue = ds1.Tables[1].Rows[0]["csircod"].ToString();
            this.ddlSuplist_OnSelectedIndexChanged(null, null);
            this.ddlmatlist.SelectedValue = ds1.Tables[1].Rows[0]["matcod"].ToString();
            this.Data_DataBind();
        }

        private void GetPreAssNo()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREVSUPASSNO", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevAssNo.DataTextField = "assno1";
            this.ddlPrevAssNo.DataValueField = "assno";
            this.ddlPrevAssNo.DataSource = ds1.Tables[0];
            this.ddlPrevAssNo.DataBind();
        }
        protected void GetAssNO()
        {
            string comcod = this.GetComeCode();
            string mAssNO = "NEWASS";
            if (this.ddlPrevAssNo.Items.Count > 0)
                mAssNO = this.ddlPrevAssNo.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mAssNO == "NEWASS")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTSUPASSINFO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxsupassno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxsupassno1"].ToString().Substring(6);

                    this.ddlPrevAssNo.DataTextField = "maxsupassno1";
                    this.ddlPrevAssNo.DataValueField = "maxsupassno";
                    this.ddlPrevAssNo.DataSource = ds3.Tables[0];
                    this.ddlPrevAssNo.DataBind();
                }
            }

        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lbtnPrevAssNo.Visible = false;
                this.ddlPrevAssNo.Visible = false;
                this.ddlSuplist.Enabled = false;
                this.ddlmatlist.Enabled = false;
                this.ShowAssInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";


            this.ddlPrevAssNo.Items.Clear();
            this.lbtnPrevAssNo.Visible = true;
            this.ddlPrevAssNo.Visible = true;
            this.txtCurDate.Enabled = true;
            this.ddlSuplist.Enabled = true;
            this.ddlmatlist.Enabled = true;


            this.gvAssessment.DataSource = null;
            this.gvAssessment.DataBind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblAss"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAssessment.Rows.Count; i++)
            {

                string txtgcod = ((TextBox)this.gvAssessment.Rows[i].FindControl("txtasscod")).Text.Trim();
                string txtdesc = ((TextBox)this.gvAssessment.Rows[i].FindControl("txtDescription")).Text.Trim();
                string exc = (((CheckBox)gvAssessment.Rows[i].FindControl("lblexec")).Checked) ? "True" : "False";
                string good = (((CheckBox)gvAssessment.Rows[i].FindControl("lblgood")).Checked) ? "True" : "False";
                string avg = (((CheckBox)gvAssessment.Rows[i].FindControl("lblavrg")).Checked) ? "True" : "False";
                string poor = (((CheckBox)gvAssessment.Rows[i].FindControl("lblpoor")).Checked) ? "True" : "False";
                string nill = (((CheckBox)gvAssessment.Rows[i].FindControl("lblnill")).Checked) ? "True" : "False";


                TblRowIndex = (gvAssessment.PageIndex) * gvAssessment.PageSize + i;
                dt.Rows[TblRowIndex]["asscode"] = txtgcod;
                dt.Rows[TblRowIndex]["assdesc"] = txtdesc;
                dt.Rows[TblRowIndex]["exc"] = exc;
                dt.Rows[TblRowIndex]["good"] = (exc == "True") ? "False" : good;
                dt.Rows[TblRowIndex]["avrg"] = (exc == "True" || good == "True") ? "False" : avg;
                dt.Rows[TblRowIndex]["poor"] = (exc == "True" || good == "True" || avg == "True") ? "False" : poor;
                dt.Rows[TblRowIndex]["nill"] = (exc == "True" || good == "True" || avg == "True" || poor == "True") ? "False" : nill;



            }
            ViewState["tblAss"] = dt;
        }
        protected void lbtnUpPerAppraisal_OnClick(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                if (this.ddlPrevAssNo.Items.Count == 0)
                    this.GetAssNO();

                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblAss"];
                string empid = this.ddlSuplist.SelectedValue.ToString();
                string matcod = this.ddlmatlist.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string assno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string txtref = this.txtassRef.Text.Trim();


                bool result = false;
                result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPASS", "ASSINFB", assno, empid, matcod, curdate, txtref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }


                foreach (DataRow dr1 in dt.Rows)
                {

                    string gcod = dr1["asscode"].ToString();
                    string desc = dr1["assdesc"].ToString();
                    string exc = dr1["exc"].ToString();
                    string good = dr1["good"].ToString();
                    string avg = dr1["avrg"].ToString();
                    string poor = dr1["poor"].ToString();
                    string nill = dr1["nill"].ToString();


                    result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPASS", "ASSINFA", assno, gcod, desc, exc, good,
                        avg, poor, nill, "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        protected void lbtnPrevAssNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreAssNo();
        }


        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.SupplierList();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            ////string hostname = hst["hostname"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string date = txtCurDate.Text.Trim();
            string title = "সরবরাহকারীর মূল্যায়ন প্রতিবেদন";
            DataTable dt1 = (DataTable)ViewState["tblAss"];
            DataTable dt2 = (DataTable)ViewState["tblSup"];
            DataTable dt3 = (DataTable)ViewState["tblMat"];
            string supId = this.ddlSuplist.SelectedValue;
            string supname = dt2.Select("rescode='" + supId + "'")[0]["resdesc"].ToString();
            string matId = this.ddlmatlist.SelectedValue;
            string mat = dt3.Select(("rsircode='" + matId + "'"))[0]["sirdesc"].ToString();
            string mark = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(mark)", "")) ?
                   0.00 : dt1.Compute("Sum(mark)", ""))).ToString("#,##0.00;(#,##0.00); ");
            string per = ((Convert.ToDouble(mark) * 100) / 25).ToString("#,##0.00;(#,##0.00); ");
            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.EmpAssesment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptSuppAss", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtTitle", title));
            Rpt1.SetParameters(new ReportParameter("txtMark", mark));
            Rpt1.SetParameters(new ReportParameter("txtSupname", supname));
            Rpt1.SetParameters(new ReportParameter("txtMat", mat));
            Rpt1.SetParameters(new ReportParameter("txtPer", per));
            Rpt1.SetParameters(new ReportParameter("txtdate", date));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}