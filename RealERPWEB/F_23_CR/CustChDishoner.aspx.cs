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
using RealERPLIB;
namespace RealERPWEB.F_23_CR
{
    public partial class CustChDishoner : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.CreateTable();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CLIENT'S DISHONOUR CHEQUE";

                this.GetCDisHonerCheque();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void CreateTable()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("pactcode", Type.GetType("System.String"));
            dttemp.Columns.Add("pactdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("usircode", Type.GetType("System.String"));
            dttemp.Columns.Add("unitacname", Type.GetType("System.String"));
            dttemp.Columns.Add("mrno", Type.GetType("System.String"));
            dttemp.Columns.Add("chqno", Type.GetType("System.String"));
            dttemp.Columns.Add("paydate", Type.GetType("System.String"));
            dttemp.Columns.Add("paidamt", Type.GetType("System.Double"));
            dttemp.Columns.Add("dishdate", Type.GetType("System.String"));
            dttemp.Columns.Add("custphn", Type.GetType("System.String"));
            ViewState["tbldchqno"] = dttemp;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }






        private void GetCDisHonerCheque()
        {
            Session.Remove("tbldischeque");
            string comcod = this.GetCompCode();
            string txtScheqno = "%" + this.txtsrchChequeno.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETDISHONERCHEQUE", txtScheqno, "", "", "", "", "", "", "", "");
            this.ddlChequeNo.DataTextField = "textfield";
            this.ddlChequeNo.DataValueField = "valuefield";
            this.ddlChequeNo.DataSource = ds1.Tables[0];
            this.ddlChequeNo.DataBind();
            Session["tbldischeque"] = ds1.Tables[0];
            ds1.Dispose();


        }







        protected void ibtnFindChequeno_Click(object sender, EventArgs e)
        {
            this.GetCDisHonerCheque();
        }




        private void Data_DataBind()
        {

            this.gvchdishoner.DataSource = (DataTable)ViewState["tbldchqno"];
            this.gvchdishoner.DataBind();
        }

        //private void FooterCalculation(DataTable dt)
        //{
        //    ((Label)this.gvchdishoner.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 :
        //         dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        //}





        protected void lbtnSelectChequeNo_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldchqno"];
            string mrno = this.ddlChequeNo.SelectedValue.Trim().Substring(0, 9);
            string chequeno = this.ddlChequeNo.SelectedValue.Trim().Substring(9);


            DataRow[] dr = dt.Select("mrno='" + mrno + "' and chqno='" + chequeno + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();

                dr1["pactcode"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["pactcode"].ToString();
                dr1["pactdesc"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["pactdesc"].ToString();
                dr1["usircode"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["usircode"].ToString();
                dr1["unitacname"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["unitacname"].ToString(); ;
                dr1["mrno"] = mrno;
                dr1["chqno"] = chequeno;
                dr1["paydate"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["paydate"].ToString();
                dr1["paidamt"] = Convert.ToDouble(((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["paidamt"]).ToString();
                dr1["dishdate"] = "";
                dr1["custphn"] = ((DataTable)Session["tbldischeque"]).Select("mrno='" + mrno + "' and chqno='" + chequeno + "'")[0]["custphn"].ToString(); ;

                dt.Rows.Add(dr1);
            }
            this.SaveValue();
        }


        private void SaveValue()
        {
            DataTable dt1 = (DataTable)ViewState["tbldchqno"];
            for (int i = 0; i < this.gvchdishoner.Rows.Count; i++)
            {
                // TblRowIndex = (gvchdishoner.PageIndex) * gvchdishoner.PageSize + i;
                string DishDate = ((TextBox)this.gvchdishoner.Rows[i].FindControl("txtgvDate")).Text.Trim();
                dt1.Rows[i]["dishdate"] = DishDate;
            }
            ViewState["tbldchqno"] = dt1;
            this.Data_DataBind();


        }

        protected void lbtnTotalAddWork_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbldchqno"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string PactCode = dt.Rows[i]["pactcode"].ToString();
                string Usircode = dt.Rows[i]["usircode"].ToString(); ;
                string mrno = dt.Rows[i]["mrno"].ToString();
                string cheqno = dt.Rows[i]["chqno"].ToString();
                string disdate = (dt.Rows[i]["dishdate"].ToString() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["dishdate"]).ToString("dd-MMM-yyyy");
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INORUPDATESALCDHONER", PactCode, Usircode, mrno, cheqno, disdate, "", "", "", "", "", "", "", "", "", "");

            }



            //--------------------------------------------//---
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            if (compsms == "True")
            {
                DataSet dsSm = CALogRecord.CheckStatus(comcod, "1704");
                if (dsSm.Tables[0].Rows.Count == 0)
                    return;

                if (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True")
                {
                    string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string PactCode = (dt.Rows[0]["pactcode"].ToString());
                    string Usircode = dt.Rows[0]["usircode"].ToString();
                    string mrno = dt.Rows[0]["mrno"].ToString();
                    string chqno = dt.Rows[0]["chqno"].ToString();
                    string phone = dt.Rows[0]["custphn"].ToString();
                    double amt = Convert.ToDouble(dt.Rows[0]["paidamt"].ToString());
                    //double amt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", ""))));
                    string ntype = dsSm.Tables[0].Rows[0]["gcod"].ToString();
                    string smsstatus = (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True") ? "Y" : "N";
                    string smscontent = dsSm.Tables[0].Rows[0]["smscont"].ToString().Replace("XXXXX", amt.ToString());
                    string mailstatus = (dsSm.Tables[0].Rows[0]["mactive"].ToString() == "True") ? "Y" : "N";
                    string mailcontent = dsSm.Tables[0].Rows[0]["mailcont"].ToString();
                    string mailattch = "";
                    bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), PactCode, Usircode, mrno, tdate, ntype, smsstatus, smscontent.Replace("YYYYY", chqno), mailstatus,
                            mailcontent, mailattch, phone, "");
                }

            }
            //------------------------------------//////


            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
    }
}