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
    public partial class FeaProjectCost : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

            this.ShowProCost();

        }


        private void ShowProCost()
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            ViewState.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string search = "%" + this.txtSrcPro.Text.Trim() + "%";
            string CallType = (this.chkAllRes.Checked) ? "SHOWALLPROJECTSOT" : "SHOWPROJECTSOT";

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
            this.FooterCalculation();



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
                string stored = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvstored")).Text.Trim();
                string face = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvface")).Text.Trim();
                double usize = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvusize")).Text.Trim());
                string floor = ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvfloor")).Text.Trim();

                double Cparking = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvcparking")).Text.Trim());
                double Puramt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvpuramt")).Text.Trim());
                double Cpamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvcpamt")).Text.Trim());
                double Utility = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvutility")).Text.Trim());
                double Bdamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvbdamt")).Text.Trim());
                double Comamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvcommision")).Text.Trim());
                double Othamt = Convert.ToDouble("0" + ((TextBox)this.gvFeaPrjC.Rows[i].FindControl("txtgvothamt")).Text.Trim());
                double purrate = (usize == 0) ? 0 : (Puramt / usize);
                double toamt = Puramt + Cpamt + Utility + Bdamt + Comamt + Othamt;
                int rowindex = (this.gvFeaPrjC.PageIndex) * (this.gvFeaPrjC.PageSize) + i;
                dt.Rows[rowindex]["stored"] = stored;
                dt.Rows[rowindex]["face"] = face;
                dt.Rows[rowindex]["flr"] = floor;
                dt.Rows[rowindex]["usize"] = usize;
                dt.Rows[rowindex]["cparking"] = Cparking;
                dt.Rows[rowindex]["purrate"] = purrate;
                dt.Rows[rowindex]["puramt"] = Puramt;

                dt.Rows[rowindex]["cpamt"] = Cpamt;
                dt.Rows[rowindex]["utility"] = Utility;
                dt.Rows[rowindex]["bdamt"] = Bdamt;
                dt.Rows[rowindex]["comamt"] = Comamt;
                dt.Rows[rowindex]["othamt"] = Othamt;
                dt.Rows[rowindex]["toamt"] = toamt;
            }

            ViewState["tblfeaprj"] = dt;
            this.Data_Bind();
        }
        protected void lbtnfUpdateCost_Click(object sender, EventArgs e)
        {

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

                DataTable dt1 = (DataTable)ViewState["tblfeaprj"];
                bool result = true;

                foreach (DataRow dr in dt1.Rows)
                {
                    string pactcode = dr["pactcode"].ToString().Trim();
                    string stored = dr["stored"].ToString().Trim();
                    string face = dr["face"].ToString();
                    string Usize = Convert.ToDouble(dr["usize"].ToString()).ToString();
                    string Floor = dr["flr"].ToString();
                    string cparking = Convert.ToDouble(dr["cparking"].ToString()).ToString();
                    string puramt = Convert.ToDouble(dr["puramt"].ToString()).ToString();
                    string cpamt = Convert.ToDouble(dr["cpamt"].ToString()).ToString();
                    string utility = Convert.ToDouble(dr["utility"].ToString()).ToString();
                    string bdamt = Convert.ToDouble(dr["bdamt"].ToString()).ToString();
                    string comamt = Convert.ToDouble(dr["comamt"].ToString()).ToString();
                    string othamt = Convert.ToDouble(dr["othamt"].ToString()).ToString();
                    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY_03", "INSORUPDATEFEAPCOST", pactcode, stored, face, Usize, Floor, cparking, puramt,
                                                              cpamt, utility, bdamt, comamt, othamt, "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }


                    this.chkAllRes.Checked = false;
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