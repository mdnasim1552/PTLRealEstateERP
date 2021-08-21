using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_32_Mis
{
    public partial class RptMisMasterBgd02 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void lbtnShowColvsExp_Click(object sender, EventArgs e)
        {
            //this.pnlNoteDe.Visible = true;
            //this.lblBankstatus.Visible = true;
            //Session.Remove("tblmasterbgd");
            //string comcod = this.GetComeCode();
            //string txtdate = Convert.ToDateTime(this.txtDateExpens.Text.Trim()).ToString("dd-MMM-yyyy");
            //string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            //string crore = (this.chkCrore.Checked) ? "10000000" : "1";
            //DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTCOLLECTVSEXPENSES", txtdate, consolidate, crore, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvColvsExp.DataSource = null;
            //    this.gvColvsExp.DataBind();
            //    return;
            //}
            //this.PanelNote.Visible = true;
            //this.txtColl.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
            //this.txtNp.Text = Convert.ToDouble(ds1.Tables[1].Rows[1]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
            //this.txtBgd.Text = Convert.ToDouble(ds1.Tables[1].Rows[2]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";

            //DataTable dt = this.HiddenSameData02(ds1.Tables[0]);
            //Session["tblmasterbgd"] = this.HiddenSameData02(ds1.Tables[0]);
            //this.Data_Bind();
            //this.gvCatdesc.DataSource = ds1.Tables[2];
            //this.gvCatdesc.DataBind();
        }
    }
}