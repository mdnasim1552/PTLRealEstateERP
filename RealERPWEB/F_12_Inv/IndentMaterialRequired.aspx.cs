using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_12_Inv
{
    public partial class IndentMaterialRequired : System.Web.UI.Page
    {
        ProcessAccess dbaccess = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetProjectName();
                this.GetDeparment();
                this.CommonButton();

                // ddlResSpcf_SelectedIndexChanged(null, null);
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    //this.PreList();
                }
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click); 
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            ////
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ////((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            //if (this.Request.QueryString["Type"] == "Approve")
            //{
            //    ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            //    ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
            //    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
            //    ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you want to Approve?');";
            //    //     ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            //    //   ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //}


        }

        private void GetProjectName()
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = (hst["usrid"].ToString());
                string comcod = this.GetCompCode();

                // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
                DataSet ds2 = dbaccess.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "11020099%", "FxtAst", "", userid, "", "", "", "", "");
                if (ds2 == null)
                    return;
                ViewState["tblStoreType"] = ds2.Tables[0];
                this.ddlStoreList.DataTextField = "actdesc1";
                this.ddlStoreList.DataValueField = "actcode";
                this.ddlStoreList.DataSource = ds2.Tables[0];
                this.ddlStoreList.DataBind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }
        protected void GetDeparment()
        {
            try
            {
                string comcod = this.GetCompCode();
                //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
                DataSet ds1 = dbaccess.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
                ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");
                this.ddlDeptCode.DataTextField = "fxtgdesc";
                this.ddlDeptCode.DataValueField = "fxtgcod";
                this.ddlDeptCode.DataSource = ds1.Tables[0];
                this.ddlDeptCode.DataBind();
                this.ddlDeptCode.SelectedValue = "AAAAAAAAAAAA";
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlDeptCode.SelectedValue.ToString() == "AAAAAAAAAAAA")
                {
                    string Msg = "Please Select Department";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                    return;
                }
                if (this.lbtnOk.Text == "Ok")
                {
                    this.lbtnOk.Text = "New";
                    this.ddlDeptCode.Enabled = false;
                    this.GetMatList();
                    this.GetIssuenfo();
                    this.divMatrial.Visible = true;
                    this.divMatrialSpec.Visible = true;
                    this.divBtn.Visible = true;

                    if (this.Request.QueryString["Type"] == "Entry")
                    {
                        //if (this.ddlPreList.Items.Count == 0)
                        //{
                        //    if (this.ddlDeptCode.SelectedValue.ToString() == "AAAAAAAAAAAA")
                        //    {
                        //        this.lblmsg1.Visible = true;
                        //        this.lblmsg1.Text = "Please Select Department";
                        //        return;
                        //    }
                        //}

                    }
                    return;
                }

                this.lbtnOk.Text = "Ok";

                this.divMatrial.Visible = false;
                this.divBtn.Visible = false;
                this.divMatrialSpec.Visible = false;
                this.ddlDeptCode.Enabled = true;
                // this.ddlEmpList.Enabled = true;

                this.txtaplydate.Enabled = true;

                //this.ImgbtnFindPrevious.Visible = true;
                //this.ddlPreList.Visible = true;
                //this.ddlPreList.Items.Clear();
                this.ddlMaterials.Items.Clear();
                //this.ddlResSpcf.Items.Clear();
                //this.gvIssue.DataSource = null;
                //this.gvIssue.DataBind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void GetIssuenfo()
        {
            try
            {

                ViewState.Remove("tblIssue");
                string comcod = this.GetCompCode();
                string CurDate1 = this.txtaplydate.Text.Trim();
                string mISUNo = "NEWISU";
                //if (this.ddlPreList.Items.Count > 0)
                //{
                //    this.txtCurDate.Enabled = false;
                //    mISUNo = this.ddlPreList.SelectedValue.ToString();

                //}
                string pactcode = this.ddlStoreList.SelectedValue.ToString();

                DataSet ds1 = dbaccess.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "GETMATREQUIEDINFO", mISUNo, CurDate1, "");
                if (ds1 == null)
                    return;

                ViewState["tblIssue"] = ds1.Tables[0];


                if (mISUNo == "NEWISU")
                {
                    ds1 = dbaccess.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "GETIMATREQNO", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(0, 6);
                    this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(6);
                    return;
                }



                this.ddlStoreList.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
                this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
                //   this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();

                this.txtaplydate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["issuedat"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(6);
                // this.Data_Bind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void GetMatList()
        {
            try
            {
                string comcod = this.GetCompCode();
                string mProject = this.ddlStoreList.SelectedValue.ToString();
                string mSrchTxt = "%";
                string date = this.txtaplydate.Text.Trim();
                DataTable dt = (DataTable)ViewState["tblStoreType"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("actcode='" + mProject + "'");
                dt = dv.ToTable();
                string Codetype = dt.Rows[0]["acttype"].ToString();
                string SearchInfo = "";
                if (Codetype.Length > 0)
                {

                    int len;
                    string[] ar = Codetype.Split('/');
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
                string reqno = "";

                DataSet ds1 = dbaccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INDENTGETMATLIST", mProject, mSrchTxt, date, reqno, "", "", "", "", "");

                if (ds1 == null)
                {
                    this.ddlMaterials.Items.Clear();
                    // this.ddlResSpcf.Items.Clear();
                    return;
                }
                ViewState["tblMat"] = ds1.Tables[0];

                ViewState["tblSpcf"] = ds1.Tables[2];
                this.ddlMaterials.DataTextField = "rsirdesc";
                this.ddlMaterials.DataValueField = "rsircode";
                this.ddlMaterials.DataSource = ds1.Tables[1];
                this.ddlMaterials.DataBind();
                this.ddlResSpcf_SelectedIndexChanged(null, null);
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }

        }

        protected void ddlStoreList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMatList();
        }

        protected void ddlResSpcf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlMaterials.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Panel2.Visible = true;
                this.SaveValue();
                DataTable tbl1 = (DataTable)ViewState["tblIssue"];
                string mResCode = this.ddlMaterials.SelectedValue.ToString();
                // string Specification = this.ddlResSpcf.SelectedValue.ToString();
                string Dmpcode = this.ddlDeptCode.SelectedValue.ToString();
                string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
                DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();

                    dr1["comcod"] = this.GetCompCode(); 
                    dr1["rsircode"] = this.ddlMaterials.SelectedValue.ToString();
                    dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                    dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                    dr1["rsirdesc"] = this.ddlMaterials.SelectedItem.Text.Trim();
                    dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                    dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                    dr1["empid"] = "";
                    dr1["remarks"] = "";
                    DataTable tbl2 = (DataTable)ViewState["tblMat"];
                    DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
                    dr1["rsirunit"] = dr3[0]["rsirunit"];
                    dr1["stkqty"] = "0";
                    dr1["stkrate"] = "0";
                    dr1["issueqty"] = dr3[0]["issueqty"];
                    dr1["issueamt"] = 0;
                    
                    tbl1.Rows.Add(dr1);
                }

                ViewState["tblIssue"] = tbl1;
                this.Data_Bind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }

        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            ////this.Panel2.Visible = true;
            //this.SaveValue();
            //DataTable tbl1 = (DataTable)ViewState["tblIssue"];
            //string mResCode = this.ddlResList.SelectedValue.ToString();
            //// string Specification = this.ddlResSpcf.SelectedValue.ToString();
            //string Empcode = this.ddlDeptCode.SelectedValue.ToString();
            //string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            //DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
            //if (dr2.Length == 0)
            //{
            //    DataRow dr1 = tbl1.NewRow();

            //    dr1["comcod"] = this.GetCompCode(); ;
            //    dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
            //    dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
            //    dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
            //    dr1["rsirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
            //    dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
            //    dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
            //    dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();
            //    DataTable tbl2 = (DataTable)ViewState["tblMat"];
            //    DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
            //    dr1["rsirunit"] = dr3[0]["rsirunit"];
            //    dr1["stkqty"] = dr3[0]["stkqty"];
            //    dr1["stkrate"] = dr3[0]["stkrate"];
            //    dr1["issueqty"] = dr3[0]["issueqty"];
            //    dr1["issueamt"] = 0;
            //    dr1["remarks"] = "";
            //    tbl1.Rows.Add(dr1);
            //}

            //ViewState["tblIssue"] = tbl1;
            //this.Data_Bind();

        }

        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblIssue"];

            for (int i = 0; i < this.gvIssue.Rows.Count; i++)
            {


                double issueqty = Convert.ToDouble("0" + ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvissueqty")).Text.Trim());
                string Remarks = ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvremarks")).Text.Trim();
                int rowindex = ((this.gvIssue.PageIndex) * (this.gvIssue.PageSize)) + i;

                dt1.Rows[rowindex]["issueqty"] = issueqty;
                dt1.Rows[rowindex]["remarks"] = Remarks;

            }
            ViewState["tblIssue"] = dt1;
        }

        private void Data_Bind()
        {

            this.gvIssue.DataSource = (DataTable)ViewState["tblIssue"];
            this.gvIssue.DataBind();
            //this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            //DataTable dt1 = (DataTable)ViewState["tblIssue"];

            //if (dt1.Rows.Count == 0)
            //    return;
            //if (this.GetCompCode() == "7305")
            //{
            //    ((Label)this.gvIssue.FooterRow.FindControl("lblFgvissueqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(issueqty)", "")) ?
            //0.00 : dt1.Compute("sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //}


        }

    }
}