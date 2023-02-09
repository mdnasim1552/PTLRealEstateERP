using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccGenAdjJournal : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Requisition Adjustment";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CompanyPost();


            }



        }





        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        //}


        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            Session["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void CompanyPost()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {

                //case "3101":
                case "1103":
                    this.chkpost.Checked = true;
                    break;

                default:
                    this.chkpost.Checked = false;
                    break;
            }


        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string search = this.txtSrchbillno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETADJBILLNO", search, "", "", "", "", "", "", "", "");
            this.ddlbillno.DataTextField = "reqno1";
            this.ddlbillno.DataValueField = "reqno";
            this.ddlbillno.DataSource = ds1.Tables[0];
            this.ddlbillno.DataBind();
            ds1.Dispose();
            // this.GetUnitName();

        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDrAmt")).Text = (accData.ToDramt).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCrAmt")).Text = (accData.ToCramt).ToString("#,##0;(#,##0); - ");




        }


        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblt01"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("billno<>''");
            Session["tblt01"] = dv.ToTable();
            this.Data_Bind();

        }
        //protected void dgv2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{        
        //    DataTable dt =(DataTable) Session["tblt01"];  
        //    int rowindex = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + e.RowIndex;
        //    dt.Rows[rowindex].Delete();
        //    DataView dv = dt.DefaultView;
        //    dv.RowFilter = ("billno<>''");
        //    Session["tblt01"]=dv.ToTable();
        //   this.Data_Bind();


        //}

        protected void ibtnvounu_Click(object sender, EventArgs e)
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetCompCode();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {


            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                this.GetProjectName();
                this.CreateTable();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            Session.Remove("tblt01");


            // this.ddlProject.Items.Clear();
            //this.ddlUnitName.Items.Clear();

            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.lnkFinalUpdate.Enabled = true;
            //this.txtcurrentvou.Enabled = false;
            //this.txtCurrntlast6.Enabled = false;
        }



        protected void imgSearchProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgSearchUnit_Click(object sender, EventArgs e)
        {
            //this.GetUnitName();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetUnitName();
        }


        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            if (accData.ToDramt != accData.ToCramt)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();

            string voudat = this.txtdate.Text.Substring(0, 11);
            this.ibtnvounu_Click(null, null);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";

            //string pounaction = (dtuser.Rows.Count == 0) ? ((this.chkpost.Checked) ? "U" : "") : dtuser.Rows[0]["pounaction"].ToString().Trim();
            string pounaction = (this.chkpost.Checked) ? "U" : "";


            string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE";


            try
            {
                //-----------Update Transaction B Table-----------------//  ACVUPDATE
                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATEUNPOSTED", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                          vounarration2, voutype, vtcode, edit, "", "", "", "", "", "", "", "", "", "", "", "", pounaction, "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("lblgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("lblgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);

                    string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno11")).Text.Trim();
                    string trnremarks = billno;
                    string adj = "adj";

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }


                    //if (ASTUtility.Left(actcode, 2) == "18")
                    //{
                    //    resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INORUPADJJOURNAL",
                    //            billno, vounum, "", "", "", "", "", "", "", "", "", "", "", "");
                    //    if (!resulta)
                    //    {
                    //        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //        return;
                    //    }

                    //}
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Journal";
                    string eventdesc = "Update Journal";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                //this.txtcurrentvou.Enabled = false;
                //this.txtCurrntlast6.Enabled = false;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }
        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            // this.GridColoumnVisible();
            calculation();
            //this.GetNarration();



        }

        protected void lbtnSelec_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            // string Pactcode = this.ddlProject.SelectedValue.ToString();
            //string UnitCode = this.ddlUnitName.SelectedValue.ToString();
            string billno = this.ddlbillno.SelectedValue.ToString();


            // string CallType = (Type == "Consolidate") ? "GETACCSALESJOURNAL" : this.ComSalesJournal();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCADJUSTMENTJOURNAL", billno,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)Session["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                string dgTrnRemarks = dt1.Rows[i]["billno"].ToString();

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);


                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {

                    tblt01.Clear();
                    return;

                }


                else
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dgAccCode;
                    dr1["subcode"] = dgResCode;
                    dr1["spclcode"] = dgSpclCode;
                    dr1["actdesc"] = dgAccDesc;
                    dr1["subdesc"] = dgResDesc;
                    dr1["spcldesc"] = dgSpclDesc;
                    dr1["billno"] = dgTrnRemarks;
                    dr1["trndram"] = dgTrnDrAmt;
                    dr1["trncram"] = dgTrnCrAmt;
                    tblt01.Rows.Add(dr1);
                }
            }





            Session["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();

            this.ibtnvounu.Visible = true;
            this.txtCurrntlast6.ReadOnly = false;
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();
            //string rescode = dt1.Rows[0]["rescode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }
                actcode = dt1.Rows[j]["actcode"].ToString();
            }

            return dt1;
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;

            string billno = dt1.Rows[0]["billno"].ToString();

            for (int j = 0; j < this.dgv2.Rows.Count - 1; j++)
            {

                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("lblgvDrAmt")).Text.Trim()));


                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                dt1.Rows[TblRowIndex2]["trndram"] = dramt;
                //dt1.Rows[TblRowIndex2]["spclcode"] = spclcode;
                //dt1.Rows[TblRowIndex2]["spcldesc"] = spcldesc;
                //    billno = billno1;




            }

            int toprow = dt1.Rows.Count - 1;
            dt1.Rows[toprow]["trncram"] = todramt;
            Session["tblt01"] = dt1;
            this.Data_Bind();



        }



        protected void imgSearchBillno_Click(object sender, ImageClickEventArgs e)
        {
            // this.LoadBillCombo();
        }


    }
}