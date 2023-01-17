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

namespace RealERPWEB.F_02_Fea
{
    public partial class EntryProjectFesibility05 : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkbtnok_Click(object sender, EventArgs e)
        {

            this.ShowProCost();

        }


        private void ShowProCost()
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string search = "%"; //"%" + this.txtSrcPro.Text.Trim() + "%";
            string CallType = "GETPROJECTNAME";

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", CallType, search, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();

                return;
            }
            ViewState["tblfeaprj"] = ds2.Tables[0];
            ds2.Dispose();
            this.Data_Bind();

        }




        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            this.gvFeaPrjC.DataSource = dt;
            this.gvFeaPrjC.DataBind();
            //this.FooterCalculation();



        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)ViewState["tblfeaprj"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ?
                    0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFpuramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(puramt)", "")) ?
                       0.00 : dt.Compute("Sum(puramt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFcpamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cpamt)", "")) ?
                       0.00 : dt.Compute("Sum(cpamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFutility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utility)", "")) ?
                       0.00 : dt.Compute("Sum(utility)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFbdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bdamt)", "")) ?
                       0.00 : dt.Compute("Sum(bdamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFcommision")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(comamt)", "")) ?
                       0.00 : dt.Compute("Sum(comamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFothamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(othamt)", "")) ?
                       0.00 : dt.Compute("Sum(othamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFtotalaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toamt)", "")) ?
                           0.00 : dt.Compute("Sum(toamt)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblfeaprj"];

            for (int i = 0; i < this.gvFeaPrjC.Rows.Count; i++)
            {
                //string stored = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvstored")).Text.Trim();
                //string face = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvface")).Text.Trim();
               // double usize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvusize")).Text.Trim());
                string udesc = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvfloor")).Text.Trim();

                double usize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvusize")).Text.Trim());
                double uamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvamout")).Text.Trim());
                double txtgvpuramt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvpuramt")).Text.Trim());

                double txtgvPamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvPamt")).Text.Trim());
                double txtgvUtility = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvUtility")).Text.Trim());
                double txtgvactualsal = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvactualsal")).Text.Trim());


                
                string txtgvpurDate = (((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvpurDate")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvpurDate")).Text.Trim();                         
                double commitedval = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvcommision")).Text.Trim());
                string txtgvAgeingDay = (((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAgeingDay")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvAgeingDay")).Text.Trim();

                // double toamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("lblgvtotalaamt")).Text.Trim());
                //double Comamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvcommision")).Text.Trim());
                //double Othamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvothamt")).Text.Trim());
                double rate = (usize == 0) ? 0 : (uamt / usize);


                double tamt = Convert.ToDouble((usize * rate) + txtgvPamt + txtgvUtility );

                //double toamt = 0.00; //  Puramt + Cpamt + Utility + Bdamt + Comamt + Othamt;
                int rowindex = (this.gvFeaPrjC.PageIndex) * (this.gvFeaPrjC.PageSize) + i;
                //dt.Rows[rowindex]["stored"] = stored;
                //dt.Rows[rowindex]["face"] = face;
                dt.Rows[rowindex]["udesc"] = udesc;
                dt.Rows[rowindex]["usize"] = usize;
                dt.Rows[rowindex]["rate"] = rate;

                dt.Rows[rowindex]["uamt"] = uamt;
                dt.Rows[rowindex]["purvalue"] = txtgvpuramt;
                dt.Rows[rowindex]["purdate"] = txtgvpurDate;
                dt.Rows[rowindex]["commitedval"] = commitedval;
                dt.Rows[rowindex]["AgeingDay"] = txtgvAgeingDay;
                dt.Rows[rowindex]["carparking"] = txtgvPamt;
                dt.Rows[rowindex]["utility"] = txtgvUtility;                
                dt.Rows[rowindex]["acsalvalue"] = txtgvactualsal;               
                dt.Rows[rowindex]["tamt"] = tamt;

                //dt.Rows[rowindex]["bdamt"] = Bdamt;
                //dt.Rows[rowindex]["comamt"] = Comamt;
                //dt.Rows[rowindex]["othamt"] = Othamt;
                //dt.Rows[rowindex]["toamt"] = toamt;
            }

            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateCost_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                string Messaged = "";
                DataTable dt1 = (DataTable)ViewState["tblfeaprj"];
                bool result = true;

                foreach (DataRow dr in dt1.Rows)
                {
                    string pactcode = dr["pactcode"].ToString().Trim();
                    

                    double usize = Convert.ToDouble(dr["usize"].ToString());
                    string udesc = dr["udesc"].ToString();
                    string munit = "";// dr["munit"].ToString();
                    double uamt = Convert.ToDouble(dr["uamt"].ToString());
                    double purvalue = Convert.ToDouble(dr["purvalue"].ToString());
                    double commitedval = Convert.ToDouble(dr["commitedval"].ToString());
                    string purdate = Convert.ToDateTime(dr["purdate"].ToString()).ToString(); 
                    string ageingday = Convert.ToDateTime(dr["ageingday"].ToString()).ToString();
                    double carparking = Convert.ToDouble(dr["carparking"].ToString());
                    double utility = Convert.ToDouble(dr["utility"].ToString());
                    double acsalvalue = Convert.ToDouble(dr["acsalvalue"].ToString());

                    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "INSORUPDATEFEAPESTCOST",
                        pactcode, munit, usize.ToString(), udesc, uamt.ToString(), purvalue.ToString(), purdate,
                                                              commitedval.ToString(), ageingday, carparking.ToString(), utility.ToString(), acsalvalue.ToString(), "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        Messaged = "Data Updated Successfully ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

                    }


                   // this.chkAllRes.Checked = false;
                    this.ShowProCost();


                }









            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }



        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowProCost();
        }

        
    }
}
