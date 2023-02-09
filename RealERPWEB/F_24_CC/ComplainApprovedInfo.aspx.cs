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
namespace RealERPWEB.F_24_CC
{

    public partial class ComplainApprovedInfo : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Order Received";

                this.GetComplainNo();
            }



        }


        private void GetComplainNo()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string seachno = "%" + this.txtSrcComp.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "COMPLAINNOINF", seachno, "", "", "", "", "", "", "", "");
            this.ddlComplain.DataTextField = "compdesc";
            this.ddlComplain.DataValueField = "compno";
            this.ddlComplain.DataSource = ds1.Tables[0];
            this.ddlComplain.DataBind();

        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ddlcomp = this.ddlComplain.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCOMPLAININFO", ddlcomp, "", "", "", "", "", "", "", "");
            Session["tblComp"] = ds1.Tables[0];
            this.Data_Bind();


        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblComp"];
            // this.grvMWiseSupBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvcomplain.DataSource = dt;
            this.grvcomplain.DataBind();
            //this.FooterCalculation();
        }

        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblComp"];
            for (int i = 0; i < this.grvcomplain.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.grvcomplain.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                //string Sirialno = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvsirialno")).Text.Trim();
                //string Recdate = (((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim();
                //dt.Rows[i]["sirialno"] = Sirialno;
                //dt.Rows[i]["recondat"] = Recdate;
                dt.Rows[i]["chkmv"] = chkmr;
                ((CheckBox)this.grvcomplain.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.grvcomplain.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.grvcomplain.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.grvcomplain.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblComp"] = dt;
        }

        protected void lbok_Click(object sender, EventArgs e)

        {



            //int rowindex=()((LinkButton)sender).CommandArgument)
            //this.SaveValue();
            this.CheckValue();
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            //string compno = ddlComplain.SelectedValue.ToString().Substring(0, 14);
            //string userid = hst["usrid"].ToString();
            //string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblComp"];
            string compno = dt.Rows[RowIndex]["compno"].ToString();
            string compcod = dt.Rows[RowIndex]["compcod"].ToString();
            //double ppayment = Convert.ToDouble(dt.Rows[RowIndex]["ppayment"].ToString());
            //double taxamt =Convert.ToDouble(dt.Rows[RowIndex]["taxam"].ToString());
            //string  rsircode="970100101001";

            bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "UPDATECOMPLALIN", compno, compcod, "", "", "",
                            "", "", "", "", "", "", "", "", "", "");



            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }



            //if (taxamt != 0.00) 


            //        {
            //            string rsircode = "970100101001";
            //            bool resultt = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INSERTORUPSUPTAX", orderno, rsircode, taxamt.ToString(), "", "",
            //                         "", "", "", "", "", "", "", "", "", "");
            //            if (!resultt)
            //            {
            //                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //                return;
            //            }


            //        }

            //    //this.Data_Bind();
            //this.CheckValue();




        }
    }
}