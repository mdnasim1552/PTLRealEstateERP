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
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurOpenigBill : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                this.GetSupplier();


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Opening Bill";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }


        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnFindSupplier_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
                this.GetSupplier();

        }

        private void GetProjectName()
        {


            string comcod = this.GetCompCode();
            string PactDesc = "%" + this.txtSearchPro.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "GETBILLPROJECT", PactDesc, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }
        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            string Supplier = "%" + this.txtsrchSupplier.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "GETSUPPLIER", Supplier, "", "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();
            ds1.Dispose();

        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
            {

                this.lnkbtnOk.Text = "New";
                this.ddlProjectName.Enabled = false;
                this.ddlSupplier.Enabled = false;
                this.MultiView1.ActiveViewIndex = 0;
                this.ShowOpenBill();
                return;
            }
            this.lnkbtnOk.Text = "Ok";


            this.ddlProjectName.Enabled = true;
            this.ddlSupplier.Enabled = true;
            this.MultiView1.ActiveViewIndex = -1;
            this.gvMoney.DataSource = null;
            this.gvMoney.DataBind();



        }










        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string Receiveno = this.lblReceiveNo.Text.Trim();
            //string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //string UnitName = "%" + this.txtSearchUnit.Text.Trim() + "%";
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "PRINTSALOPENINGINFO", Receiveno, pactcode, UnitName, "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //DataTable dt1= HiddenSameData(ds1.Tables[0]);
            //ReportDocument rrs2 = new SHIPERPRPT.R_22_Sal.RptSalesOpening();
            //TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtProDate = rrs2.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtProDate.Text = "Date: " + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); 

            //TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs2.SetDataSource(dt1);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rrs2.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rrs2;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }





        private void ShowOpenBill()
        {
            Session.Remove("tblopnbill");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string ssircode = this.ddlSupplier.SelectedValue.ToString();
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "SHOWOPENINGBILL", pactcode, ssircode, "", "", "", "", "", "", "");
            Session["tblopnbill"] = ds4.Tables[0];
            this.gv_DataBind();
            ds4.Dispose();




        }


        private string GetBILLNo()
        {

            string comcod = this.GetCompCode();
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "GETOPENBILLNO", "", "", "", "", "", "", "", "", "");
            return (ds3.Tables[0].Rows[0]["billno"].ToString());

        }

        private string IncrmentBillNo(string billno)
        {
            string obillno = ASTUtility.Right(billno, 5);
            string ibillno = (Convert.ToInt32(obillno) + 1).ToString();
            return (ASTUtility.Left(billno, 9) + ASTUtility.Right("00000" + ibillno, 5));
        }



        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {



            DataTable dt = ((DataTable)Session["tblopnbill"]);
            string billno = this.GetBILLNo();

            int invcount = Convert.ToInt32("0" + this.txtnofomrr.Text.Trim());
            for (int i = 0; i < invcount; i++)
            {
                DataRow dr = dt.NewRow();
                dr["billno"] = billno;
                dr["billno1"] = ASTUtility.Right(billno, 5);
                dr["billref"] = "";
                dr["mrno"] = "";
                dr["chlnno"] = "";
                dr["billdate"] = "";
                dr["billamt"] = 0.00;
                dr["rmrks"] = "";



                dt.Rows.Add(dr);
                billno = this.IncrmentBillNo(billno);

            }
            Session["tblopnbill"] = dt;
            this.gv_DataBind();

        }


        private void gv_DataBind()
        {
            DataTable dt = (DataTable)Session["tblopnbill"];
            this.gvMoney.DataSource = dt;
            this.gvMoney.DataBind();
            if (dt.Rows.Count > 1)
                ((Label)this.gvMoney.FooterRow.FindControl("lblgvFBillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 : dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");







        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVisible.Checked == true)
            {

                this.pnlgenMrr.Visible = true;

            }
            else
            {
                this.pnlgenMrr.Visible = false;
            }

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveMoneyValue();
            this.gv_DataBind();
        }


        private void SaveMoneyValue()
        {

            DataTable tbl1 = (DataTable)Session["tblopnbill"];
            for (int i = 0; i < this.gvMoney.Rows.Count; i++)
            {

                tbl1.Rows[i]["billref"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvrefid")).Text.Trim();
                tbl1.Rows[i]["mrno"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvmrno")).Text.Trim();
                tbl1.Rows[i]["chlnno"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvchalanno")).Text.Trim();
                tbl1.Rows[i]["billdate"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvbilldate")).Text.Trim();
                tbl1.Rows[i]["billamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.gvMoney.Rows[i].FindControl("txtgvBillamt")).Text.Trim()).ToString();
                tbl1.Rows[i]["rmrks"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvrmrks")).Text.Trim();
            }
            Session["tblopnbill"] = tbl1;

        }
        protected void gvMoney_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblopnbill"];
            int rowindex = (this.gvMoney.PageSize) * (this.gvMoney.PageIndex) + e.RowIndex;
            string billno = dt.Rows[rowindex]["billno"].ToString();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "DELETEOPNBILLNO", billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {

                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("billno<>''");
                Session["tblopnbill"] = dv.ToTable();
                this.gv_DataBind();
            }
        }
        protected void gvMoney_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvMoney.EditIndex = -1;
            this.gv_DataBind();
        }
        protected void gvMoney_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.SaveMoneyValue();
            this.gvMoney.EditIndex = e.NewEditIndex;
            this.gv_DataBind();


            DataSet ds4 = (DataSet)Session["tblpaytype"];
            string reconbank = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvreconbank")).Text.Trim();
            string PayType = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lbgvpaytype")).Text.Trim();
            string Collfrm = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvColl")).Text.Trim();
            string RecType = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvRecType")).Text.Trim();

            DropDownList ddlgvreconbank = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvreconbank");
            ddlgvreconbank.DataTextField = "cactdesc";
            ddlgvreconbank.DataValueField = "cactcode";
            ddlgvreconbank.DataSource = ds4.Tables[3];
            ddlgvreconbank.DataBind();
            ddlgvreconbank.SelectedValue = (reconbank == "") ? "290200020002" : reconbank;

            DropDownList ddlpaytype = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvpaytype");
            ddlpaytype.DataTextField = "gdesc";
            ddlpaytype.DataValueField = "gcod";
            ddlpaytype.DataSource = ds4.Tables[0];
            ddlpaytype.DataBind();
            ddlpaytype.SelectedValue = PayType;

            DropDownList ddlgvColl = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvColl");
            ddlgvColl.DataTextField = "gdesc";
            ddlgvColl.DataValueField = "gcod";
            ddlgvColl.DataSource = ds4.Tables[1];
            ddlgvColl.DataBind();
            ddlgvColl.SelectedValue = (Collfrm == "") ? "53061001001" : Collfrm;

            DropDownList ddlgvRecType = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvRecType");
            ddlgvRecType.DataTextField = "gdesc";
            ddlgvRecType.DataValueField = "gcod";
            ddlgvRecType.DataSource = ds4.Tables[2];
            ddlgvRecType.DataBind();
            ddlgvRecType.SelectedValue = (RecType == "") ? "54003" : RecType;

            //DataTable tbl1 = (DataTable)Session["tblopnmrr"];
            //int index= e.NewEditIndex;
            //tbl1.Rows[index]["chequeno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvChequeno")).Text.Trim();
            //tbl1.Rows[index]["bankname"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvbankna")).Text.Trim();
            //tbl1.Rows[index]["bbranch"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvBrance")).Text.Trim();
            //tbl1.Rows[index]["paydate"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvpaydate")).Text.Trim();
            //tbl1.Rows[index]["refno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrefid")).Text.Trim();
            //tbl1.Rows[index]["paidamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
            //tbl1.Rows[index]["repchqno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrRpChq")).Text.Trim();
            //tbl1.Rows[index]["rmrks"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvremarks")).Text.Trim();
            //tbl1.Rows[index]["recndt"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrecndate")).Text.Trim();
            //Session["tblopnmrr"] = tbl1;

        }

        protected void lbtnUpdateMoney_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveMoneyValue();
            DataTable dt1 = (DataTable)Session["tblopnbill"];
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string ssircode = this.ddlSupplier.SelectedValue.ToString();

            bool result = true;
            //string  mrno = this.GetMrNo();
            foreach (DataRow dr in dt1.Rows)
            {

                //string Recndate = ASTUtility.DateFormat(dr["recndt"].ToString());

                //DateTime Opndate = Convert.ToDateTime(this.txtOpeningDate.Text);
                //bool dcon = ASITUtility02.TransactionDateOpening(Opndate, Convert.ToDateTime(mrdate));
                //if (!dcon)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MR  Date less than Opening Date');", true);
                //    return;
                //}


                //dcon = ASITUtility02.TransactionDateOpening(Opndate, Convert.ToDateTime(Recndate));
                //if (!dcon)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reconcillation Date is  less  than Opening Date');", true);
                //    return;
                //}



                string billno = dr["billno"].ToString();
                string billdate = ASTUtility.DateFormat(dr["billdate"].ToString());
                string billref = dr["billref"].ToString();
                string mrno = dr["mrno"].ToString();
                string chlnno = dr["chlnno"].ToString();

                string invdate = ASTUtility.DateFormat(dr["billdate"].ToString());
                string rmrks = dr["rmrks"].ToString();


                string billamt = (Convert.ToDouble(dr["billamt"])).ToString();


                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_OPENINGINVABILL", "INSERTORUPOBILLINF", billno, billdate, pactcode, ssircode, billref, mrno, chlnno, billamt, rmrks, "", "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
                //mrno = this.IncrmentMrNo(mrno.Substring(2));
            }

            ((LinkButton)this.gvMoney.FooterRow.FindControl("lbtnUpdateMoney")).Enabled = false;



        }




    }
}