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
namespace RealERPWEB.F_22_Sal
{
    public partial class SaleSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Competitive Price Survey";
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.GetProjectInfo();



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

        private void GetProjectInfo()
        {
            try
            {
                ViewState.Remove("tblproinfo");
               ViewState.Remove("tblproginfo");
                string comcod = this.GetCompCode();
                string txtSProject = "%%";
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETCOMSURPROJECTINFO", txtSProject, "", "", "", "", "", "", "", "");
                this.chkProjectName.DataTextField = "pactdesc";
                this.chkProjectName.DataValueField = "pactcode";
                this.chkProjectName.DataSource = ds1.Tables[0];
                this.chkProjectName.DataBind();
                ViewState["tblproinfo"] = ds1.Tables[1];
                ViewState["tblproginfo"] = ds1.Tables[2];


            }

            catch (Exception ex)
            { 
            
            
            
            }




        }

       
        protected void Resource_List(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST1", pmSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {



            if (this.lbtnMSROk.Text == "Ok")
            {
                this.ddlPrevMSRList.Visible = false;
                this.lblprojectname.Visible = true;
                this.chkProjectName.Visible = true;
                this.lbtnSelect.Visible = true;
                this.Panel2.Visible = true;
                this.lbtnMSROk.Text = "New";
                this.Get_Survey_Info();
                return;
            }


            this.ddlPrevMSRList.Visible = true;
            this.ddlPrevMSRList.Items.Clear();
            this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
            this.txtCurMSRDate.Enabled = true;
            this.lblprojectname.Visible = false;
            this.chkProjectName.Visible = false;
            this.lbtnSelect.Visible = false;


            this.gvMSRInfo2.DataSource = null;
            this.gvMSRInfo2.DataBind();
            this.Panel2.Visible = false;
            this.lbtnMSROk.Text = "Ok";





        }

        protected void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            // string date = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).ToString("dd-MMM-yyyy");
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                this.txtCurMSRDate.Enabled = false;
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALESURVEY_ENTRY", "GETCOMSURPROJECTINFO", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblt02"] = ds1.Tables[0];
            //Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);


            //if (mMSRNo == "NEWMSR")
            //{
            //    ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO1", CurDate1, "", "", "", "", "", "", "", "");
            //    if (ds1 == null)
            //        return;
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);

            //    }
            //    return;
            //}
            //this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["msrno1"].ToString().Substring(0, 6);

            //this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");

            //this.txtMSRNarr.Text = ds1.Tables[4].Rows[0]["remarks"].ToString();
            this.gvMSRInfo_DataBind();


        }

        protected void gvMSRInfo_DataBind()
        {


            this.gvMSRInfo2.DataSource = (DataTable)Session["tblt02"];
            this.gvMSRInfo2.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
        //    DataTable dt = (DataTable)Session["tblt02"];
        //    if (dt.Rows.Count == 0)
        //        return;
        //    ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
        //  : dt.Compute("Sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        //    ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
        //: dt.Compute("Sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        //    ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
        //: dt.Compute("Sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00);  ");
        //    ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
        //: dt.Compute("Sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        //    ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
        //: dt.Compute("Sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00);  ");



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }




        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            bool result;
            string comcod = this.GetCompCode();
            this.Session_tblMSR_Update();

            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO1", mMSRDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();
                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);


                    this.ddlPrevMSRList.DataTextField = "maxmsrno1";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
                else
                    return;
            }
            string mRESRATE = "";
            string mResOth = "";
            string mRefno = "";
            //string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            //string mMSRBYDES = this.txtPreparedBy.Text.Trim();
            //string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETRPUR02AB", mMSRNO,
                             "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string mRemarks = this.txtMSRNarr.Text.Trim();
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO1", "PURMSR02A",
                             mMSRNO, mMSRDAT, mRefno, mRemarks, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataTable tbl1 = (DataTable)Session["tblt02"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string spcfcod = tbl1.Rows[i]["spcfcod"].ToString();
                DataTable tbls1 = (DataTable)Session["tblt01"];

                for (int j = 0; j < tbls1.Rows.Count; j++)
                {
                    string mSSIRCODE = tbls1.Rows[j]["ssircode"].ToString();

                    string qty = tbl1.Rows[i]["qty"].ToString();
                    mRESRATE = Convert.ToDouble("0" + tbl1.Rows[i]["resrate" + (j + 1).ToString()]).ToString();



                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO1", "PURMSR02B",
                    mMSRNO, mRSIRCODE, spcfcod, mSSIRCODE, mRESRATE, qty, "", "", "", "", "", "", "", "");
                }

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Update Successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    // return;
                }
            }



            // Term 
            DataTable tblterm = (DataTable)Session["tblterm"];

            foreach (DataRow drr in tblterm.Rows)
            {


                string ssircode = drr["ssircode"].ToString();
                string discount = drr["discount"].ToString();
                string ccharge = drr["ccharge"].ToString();
                string payterm = drr["payterm"].ToString();
                string qutdate = drr["qutdate"].ToString();
                string worktime = drr["worktime"].ToString();
                string notes = drr["notes"].ToString();
                string goodwill = drr["goodwill"].ToString();
                string matavailable = drr["matavailable"].ToString();
                string delcon = drr["delcon"].ToString();
                string ait = drr["ait"].ToString();


                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "INSERTORUPDATEMSURVEY02", mMSRNO, ssircode, discount, ccharge, payterm, qutdate, worktime, notes, goodwill, matavailable, delcon, ait, "", "");
            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "";
                string eventdesc = "Update Survey";
                string eventdesc2 = mMSRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }




        protected void Session_tblMSR_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblt02"];
            int TblRowIndex2;

            for (int j = 0; j < this.gvMSRInfo2.Rows.Count; j++)
            {

                string rsirunit = ((Label)this.gvMSRInfo2.Rows[j].FindControl("lblgvMSRResUnit")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtgvMSRqty")).Text.Trim());
                double resrate1 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate1")).Text.Trim());
                double resrate2 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate2")).Text.Trim());
                double resrate3 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate3")).Text.Trim());
                double resrate4 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate4")).Text.Trim());
                double resrate5 = Convert.ToDouble("0" + ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtrate5")).Text.Trim());


                string aprovrate = (((Label)this.gvMSRInfo2.Rows[j].FindControl("lblaprovrate")).Text.Trim() == "") ? "0.00" : ((Label)this.gvMSRInfo2.Rows[j].FindControl("lblaprovrate")).Text.Trim();

                string dgvMSRRemarks = ((TextBox)this.gvMSRInfo2.Rows[j].FindControl("txtgvMSRRemarks")).Text.Trim();


                TblRowIndex2 = (this.gvMSRInfo2.PageIndex) * this.gvMSRInfo2.PageSize + j;

                tbl1.Rows[TblRowIndex2]["qty"] = qty;
                tbl1.Rows[TblRowIndex2]["rsirunit"] = rsirunit;
                tbl1.Rows[TblRowIndex2]["resrate1"] = resrate1;
                tbl1.Rows[TblRowIndex2]["resrate2"] = resrate2;
                tbl1.Rows[TblRowIndex2]["resrate3"] = resrate3;
                tbl1.Rows[TblRowIndex2]["resrate4"] = resrate4;
                tbl1.Rows[TblRowIndex2]["resrate5"] = resrate5;
                tbl1.Rows[TblRowIndex2]["amt1"] = qty * resrate1;
                tbl1.Rows[TblRowIndex2]["amt2"] = qty * resrate2;
                tbl1.Rows[TblRowIndex2]["amt3"] = qty * resrate3;
                tbl1.Rows[TblRowIndex2]["amt4"] = qty * resrate4;
                tbl1.Rows[TblRowIndex2]["amt5"] = qty * resrate5;

                tbl1.Rows[TblRowIndex2]["aprovrate"] = aprovrate;
                tbl1.Rows[TblRowIndex2]["msrrmrk"] = dgvMSRRemarks;

            }


            Session["tblt02"] = tbl1;
        }


        protected void lnkbtnFindPreMR_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            string prjcode = "";

            foreach (ListItem s1 in chkProjectName.Items)
            {
                if (s1.Selected)
                {
                    prjcode = s1.Value;


                }
            }

        }

        protected void lbtnMSRSelect_Click(object sender, EventArgs e)
        {
            //this.Session_tblMSR_Update();
            //DataTable tbl1 = (DataTable)Session["tblt02"];            
            //string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            //string spcfcod = this.ddlSpecificationms.SelectedValue.ToString();
            //DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and  spcfcod='" + spcfcod + "'");
            //if (dr2.Length == 0)
            //{

            //    DataRow dr1 = tbl1.NewRow();
            //    dr1["rsircode"] = this.ddlMSRRes.SelectedValue.ToString();
            //    dr1["rsirdesc1"] = this.ddlMSRRes.SelectedItem.Text.Trim();
            //    dr1["spcfcod"] = this.ddlSpecificationms.SelectedValue.ToString();
            //    dr1["spcfdesc"] = this.ddlSpecificationms.SelectedItem.Text.Trim();

            //    dr1["qty"] = 0;
            //    dr1["resrate1"] = 0;
            //    dr1["resrate2"] = 0;
            //    dr1["resrate3"] = 0;
            //    dr1["resrate4"] = 0;
            //    dr1["resrate5"] = 0;
            //    dr1["amt1"] = 0;
            //    dr1["amt2"] = 0;
            //    dr1["amt3"] = 0;
            //    dr1["amt4"] = 0;
            //    dr1["amt5"] = 0;

            //    DataTable tbl2 = (DataTable)Session["tblMat"];
            //    DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
            //    dr1["rsirunit"] = dr3[0]["rsirunit"];
            //    dr1["aprovrate"] = dr3[0]["aprovrate"];
            //    dr1["msrrmrk"] = "";
            //    tbl1.Rows.Add(dr1);
            //}
            //Session["tblt02"] = this.HiddenSameData(tbl1);   //tblMSR
            //this.gvMSRInfo_DataBind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "rsircode";
            //dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }


                rsircode = dt1.Rows[j]["rsircode"].ToString();


            }

            DataView dv = dt1.DefaultView;
            dv.Sort = ("rsircode");
            dt1 = dv.ToTable();

            return dt1;
        }






        protected void gvMSRInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtrate1 = (TextBox)e.Row.FindControl("txtrate1");
            //    TextBox txtrate2 = (TextBox)e.Row.FindControl("txtrate2");
            //    TextBox txtrate3 = (TextBox)e.Row.FindControl("txtrate3");
            //    TextBox txtrate4 = (TextBox)e.Row.FindControl("txtrate4");
            //    TextBox txtrate5 = (TextBox)e.Row.FindControl("txtrate5");

            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Left(code, 2) == "71")
            //    {
            //        txtrate1.Style.Add("text-align", "Left");
            //        txtrate2.Style.Add("text-align", "Left");
            //        txtrate3.Style.Add("text-align", "Left");
            //        txtrate4.Style.Add("text-align", "Left");
            //        txtrate5.Style.Add("text-align", "Left");
            //    }

            //}
        }
        protected void ImgbtnFindSpecificationms_Click(object sender, EventArgs e)
        {

        }

        protected void gvMSRInfo2_RowCreated(object sender, GridViewRowEventArgs e)
        {



            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    TableCell cell0 = new TableCell();
            //    cell0.Text = "";
            //    cell0.HorizontalAlign = HorizontalAlign.Center;
            //    cell0.ColumnSpan = 5;
            //    gvrow.Cells.Add(cell0);


            //    DataTable dt = (DataTable)Session["tblt01"];
            //    //int j = 5;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {

            //        TableCell cell = new TableCell();
            //        cell.Text = dt.Rows[i]["ssirdesc1"].ToString();
            //        cell.HorizontalAlign = HorizontalAlign.Center;
            //        cell.ColumnSpan = 2;
            //        cell.Font.Bold = true;
            //        gvrow.Cells.Add(cell);

            //        //if (j == 30)
            //        //    break;


            //    }


            //    TableCell celll = new TableCell();
            //    celll.Text = "";
            //    celll.HorizontalAlign = HorizontalAlign.Center;
            //    celll.ColumnSpan = 2;
            //    gvrow.Cells.Add(celll);



            //    //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
            //    //  i++;


            //    gvMSRInfo2.Controls[0].Controls.AddAt(0, gvrow);
            //}







        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();

            this.gvMSRInfo_DataBind();


        }
        protected void lbtnMrsnodelete_Click(object sender, EventArgs e)
        {


        }

        protected void p2pReqPrintCS()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string comments = this.txtMSRNarr.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string mMSRNo = this.Request.QueryString["msrno"].ToString() == "" ? "" : this.Request.QueryString["msrno"].ToString();

            LocalReport Rpt1 = new LocalReport();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEY02P2P", mMSRNo, CurDate1, "", "", "", "", "", "", "");

            string Projectname = "";
            string Projectlocat = "";
            string Username = "";
            string userdesig = "";
            string rsirdesc = "";

            if (ds1.Tables[3].Rows.Count > 0)
            {
                Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
                Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
                Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
                userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
                rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();


            }


            //string surveyNo = this.lblCurMSRNo1.Text + this.txtCurMSRNo2.Text;
            string surveyNo = mMSRNo;

            DataTable dtdetails = (DataTable)Session["tblt02"];

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();

            if (lst1.Count == 5)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P05", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));


                    i++;
                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));

                Rpt1.SetParameters(new ReportParameter("csinfo", surveyNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }

            else if (lst1.Count == 4)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P02", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));


                    i++;
                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));

                Rpt1.SetParameters(new ReportParameter("csinfo", surveyNo));
                // Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP_2_P", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    i++;

                }
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));

                Rpt1.SetParameters(new ReportParameter("csinfo", surveyNo));
                //Rpt1.SetParameters(new ReportParameter("mMSRNo", mMSRNo));
                //Rpt1.SetParameters(new ReportParameter("SurveyNo", SurveyNo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                //Rpt1.SetParameters(new ReportParameter("surveyNo", surveyNo));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }





       
    }
}