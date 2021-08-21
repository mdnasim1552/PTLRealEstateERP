using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_07_Ten
{
    public partial class LinkTasAccDetAnalysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Schedule Vs Analysis";
                this.lblProject.Text = this.Request.QueryString["pactdesc"].ToString();
                this.GetAnalysis();

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetAnalysis()
        {
            Session.Remove("tbldelanalysis");
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "GETDETAILSANA", pactcode, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvDetailsAnalysis.DataSource = null;
                this.gvDetailsAnalysis.DataBind();
                return;

            }

            DataTable dtdet = ds1.Tables[0];
            DataTable dt = (DataTable)Session["tblActAna1"];
            DataTable dt2 = (DataTable)Session["tblFlrCod"];
            DataView dv1 = dt2.DefaultView;
            dv1.RowFilter = ("mark1='1'");
            dt2 = dv1.ToTable();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string itmcod = dt.Rows[i]["itmcod"].ToString();

                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    string flrcod = dt2.Rows[j]["flrcod"].ToString();
                    DataRow[] dr2 = dtdet.Select(" itmcod='" + itmcod + "' and flrcod='" + flrcod + "'");
                    // if (dr2.Length > 0)
                    // {

                    //dtdet.Rows[j]["schqty"] = dr2[0]["schqty"];
                    //dtdet.Rows[j]["schrate"] = dr2[0]["schrate"];
                    //dtdet.Rows[j]["schamt"] = dr2[0]["schamt"];
                    //dtdet.Rows[j]["itmslno"] = dr2[0]["itmslno"];
                    //dtdet.Rows[j]["itmschno"] = dr2[0]["itmschno"];
                    // }
                    if (dr2.Length == 0)
                    {
                        DataRow dr3 = dtdet.NewRow();
                        dr3["itmcod"] = dt.Rows[i]["itmcod"];
                        dr3["itmdesc"] = dt.Rows[i]["itmdesc"];
                        dr3["itmunit"] = dt.Rows[i]["itmunit"];
                        dr3["flrcod"] = flrcod;
                        dr3["flrdes"] = dt2.Rows[j]["flrdes"];
                        dr3["schqty"] = dt.Rows[i]["schqty"];
                        dr3["schrate"] = dt.Rows[i]["schrate"];
                        dr3["schamt"] = dt.Rows[i]["schamt"];
                        dr3["itmslno"] = 0.00;
                        dr3["itmschno"] = "";
                        dtdet.Rows.Add(dr3);
                    }
                }



            }


            Session["tbldelanalysis"] = this.HiddenSameData(dtdet);
            ds1.Dispose();
            this.Data_Bind();
            //if()
            // this.cbListFloor.Items.Clear();
            // this.chkFlrShowSelected.Checked = (ds1.Tables[1].Rows.Count > 0);
            // DataTable tbl1 = (DataTable)Session["tblFlrCod"];
            // for (int i = 0; i < tbl1.Rows.Count; i++)
            // {
            //     tbl1.Rows[i]["mark1"] = 0;
            //     string flrcod = tbl1.Rows[i]["flrcod"].ToString();
            //     DataRow[] dr1 = ds1.Tables[1].Select("flrcod='" + flrcod + "'");
            //     if (dr1.Length > 0)
            //         tbl1.Rows[i]["mark1"] = 1;
            // }
            // Session["tblFlrCod"] = tbl1;

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string itmcod = dt1.Rows[0]["itmcod"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["itmcod"].ToString() == itmcod)
                {
                    dt1.Rows[j]["itmdesc"] = "";
                    dt1.Rows[j]["itmunit"] = "";
                }
                itmcod = dt1.Rows[j]["itmcod"].ToString();
            }
            return dt1;

        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tbldelanalysis"];
            this.gvDetailsAnalysis.DataSource = tbl1;
            this.gvDetailsAnalysis.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbldelanalysis"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvDetailsAnalysis.FooterRow.FindControl("lgvFschamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0 : dt.Compute("sum(schamt)", ""))).ToString("#,##0;(#,##0); ");

        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tbldelanalysis"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDetailsAnalysis.Rows.Count; i++)
            {
                double schslno = Convert.ToDouble("0" + ((TextBox)this.gvDetailsAnalysis.Rows[i].FindControl("lblgvitemslno")).Text.Trim());
                string schitemno = ((TextBox)this.gvDetailsAnalysis.Rows[i].FindControl("lblgvschitemno")).Text;
                double schqty = Convert.ToDouble("0" + ((TextBox)this.gvDetailsAnalysis.Rows[i].FindControl("txtgvschqty")).Text.Trim());
                double schrate = Convert.ToDouble("0" + ((TextBox)this.gvDetailsAnalysis.Rows[i].FindControl("txtgvschrate")).Text.Trim());
                TblRowIndex = (gvDetailsAnalysis.PageIndex) * gvDetailsAnalysis.PageSize + i;


                dt.Rows[TblRowIndex]["itmslno"] = schslno;
                dt.Rows[TblRowIndex]["itmschno"] = schitemno;
                dt.Rows[TblRowIndex]["schqty"] = schqty;
                dt.Rows[TblRowIndex]["schrate"] = schrate;
                dt.Rows[TblRowIndex]["schamt"] = schqty * schrate;


            }
            Session["tbldelanalysis"] = dt;
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetCompCode();
            string PrjCod = this.Request.QueryString["pactcode"].ToString();

            DataTable dt = (DataTable)Session["tbldelanalysis"];
            foreach (DataRow dr in dt.Rows)
            {
                string ItmCode = dr["itmcod"].ToString();
                string FlrCode = dr["flrcod"].ToString();
                string Itmqtyf = dr["schqty"].ToString();
                string Qtdratf = dr["schrate"].ToString();
                string SchItmSl = dr["itmslno"].ToString();
                string SchItmNo = dr["itmschno"].ToString();
                bool result1 = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "UPDATEPRJFLOORQTY",
                            PrjCod, ItmCode, FlrCode, Itmqtyf, Qtdratf, SchItmSl, SchItmNo, "", "", "", "", "", "", "", "");

                if (!result1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                    return;

                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);


        }
    }
}