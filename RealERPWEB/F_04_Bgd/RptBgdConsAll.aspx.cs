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
namespace RealERPWEB.F_04_Bgd
{

    public partial class RptBgdConsAll : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtDateFrom.Text = "01" + date.Substring(2);
                this.lbldate.Text = this.Request.QueryString["Date1"];
                this.lblprject.Text = this.Request.QueryString["pactdesc"];
                //((Label)this.Master.FindControl("lblTitle")).Text = "Construction Budget";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.ShowFloorcode();

            }
        }


        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            // comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            int rptindex = this.ddlRptGroup.SelectedIndex;
            switch (rptindex)
            {
                case 0:
                    this.RptWorkBasis();
                    this.paging.Visible = false;
                    this.floorwise.Visible = false;
                    break;
                case 1:
                    this.RptWorkBasis();
                    this.paging.Visible = false;
                    this.floorwise.Visible = false;

                    break;
                case 2:
                    this.ShowWorkVsResource();
                    this.paging.Visible = true;
                    this.floorwise.Visible = true;

                    break;
                default:
                    break;
            }
        }


        private void RptWorkBasis()
        {
            string comcod = this.GetCompCod();
            string PrjCod = this.Request.QueryString["prjcode"];
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            int rptIndex = this.ddlRptGroup.SelectedIndex;
            string CallType = "";
            switch (rptIndex)
            {
                case 0:
                    CallType = "RPTWRKBASIS";
                    break;
                case 1:
                    CallType = "RPTRESBASIS";
                    break;
            }




            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", CallType,
                     PrjCod, "000", "12", "00", "00000000", "00000", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();

                return;

            }

            Session["bgdall"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private void ShowWorkVsResource()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetCompCod();
            string pactcode = this.Request.QueryString["prjcode"];
            string Floorcode = this.ddlFloorList.SelectedValue.ToString();
            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTWORKVSRESOURCE", pactcode, Floorcode, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvWrkVsRes.DataSource = null;
                this.gvWrkVsRes.DataBind();
                return;
            }

            Session["tblbgd"] = HiddenSameData1(ds2.Tables[0]);// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string acgcode = dt1.Rows[0]["acgcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["acgcode"].ToString() == acgcode)
                {
                    dt1.Rows[j]["acgdesc"] = "";
                }
                acgcode = dt1.Rows[j]["acgcode"].ToString();
            }


            return dt1;


        }

        private DataTable HiddenSameData1(DataTable dt1)
        {
            string flrcod = dt1.Rows[0]["flrcod"].ToString();
            string isircode = dt1.Rows[0]["isircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod && dt1.Rows[j]["isircode"].ToString() == isircode)
                {
                    flrcod = dt1.Rows[j]["flrcod"].ToString();
                    isircode = dt1.Rows[j]["isircode"].ToString();
                    dt1.Rows[j]["flrdes"] = "";
                    dt1.Rows[j]["isirdesc"] = "";
                    dt1.Rows[j]["isirunit"] = "";
                    dt1.Rows[j]["itemqty"] = 0.00;
                }

                else
                {
                    if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    {
                        dt1.Rows[j]["flrdes"] = "";
                    }
                    if (dt1.Rows[j]["isircode"].ToString() == isircode)
                    {
                        dt1.Rows[j]["isirdesc"] = "";
                        dt1.Rows[j]["isirunit"] = "";
                        dt1.Rows[j]["itemqty"] = 0.00;
                    }

                    flrcod = dt1.Rows[j]["flrcod"].ToString();
                    isircode = dt1.Rows[j]["isircode"].ToString();
                }

            }
            return dt1;
        }
        private void Data_Bind()
        {

            int rptIndex = this.ddlRptGroup.SelectedIndex;
            switch (rptIndex)
            {
                case 0:
                    this.Multiview1.ActiveViewIndex = 0;
                    this.WorkBasis();
                    break;
                case 1:
                    this.Multiview1.ActiveViewIndex = 0;
                    this.WorkBasis();
                    //this.ResourceBasis();
                    break;
                case 2:
                    this.Multiview1.ActiveViewIndex = 1;
                    this.WorkvsResource();
                    break;
                default:
                    break;
            }



        }


        private void WorkBasis()
        {
            DataTable dt = (DataTable)Session["bgdall"];
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();



            DataView dv = dt.DefaultView;

            dv.RowFilter = ("rptcod   not like '%00000000' and  rptcod   not like '4111AAAAAAAA%' and rptcod not like  '01AAAAAAAAAA%'  and  rptcod not like  '04AAAAAAAAAA%'   and  rptcod not like  '21AAAAAAAAAA%'");

            dt = dv.ToTable();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(peramt)", "")) ? 0.00 : dt.Compute("sum(peramt)", ""))).ToString("#,##0.00;(#,##0.00);") + "%";

            }
            else
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFPercent")).Text = "0.00";
            }


            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0.00 : dt.Compute("sum(rptamt)", "")));
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFTotalCost")).Text = mSUMAM.ToString("#,##0.00;(#,##0.00);");

            string comcod = this.GetCompCod();
            string pactcode = this.Request.QueryString["prjcode"];
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "CONSAREA", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblconarea"] = ds1.Tables[0];

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"]).ToString("#,##0.00;(#,##0.00); ") + " " + ds1.Tables[0].Rows[0]["gunit"].ToString();
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"]) < 0 ? "" : (mSUMAM / Convert.ToDouble(ds1.Tables[0].Rows[0]["conarea"])).ToString("#,##0.00;(#,##0.00); ");
            }
            else
            {
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalConArea")).Text = "";
                ((Label)this.gvRptResBasis.FooterRow.FindControl("lblvalCostPsft")).Text = "";

            }

        }

        protected void ddlpagesizewrkvres_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void WorkvsResource()
        {
            this.gvWrkVsRes.PageSize = Convert.ToInt32(this.ddlpagesizewrkvres.SelectedValue.ToString());
            this.gvWrkVsRes.DataSource = (DataTable)Session["tblbgd"];
            this.gvWrkVsRes.DataBind();
            this.FooterCalculation((DataTable)Session["tblbgd"]);
        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvWrkVsRes.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(resamt)", "")) ?
                                       0 : dt.Compute("sum(resamt)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void gvWrkVsRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWrkVsRes.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvRptResBasis_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink lblgvRptRes1 = (HyperLink)e.Row.FindControl("lblgvRptRes1");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lblgvRptAmt1 = (Label)e.Row.FindControl("lblgvRptAmt1");
                Label lblgvPer = (Label)e.Row.FindControl("lblgvPer");
                HyperLink hlnkres = (HyperLink)e.Row.FindControl("lblgvRptRes1");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();
                string prjcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bldcod")).ToString();

                if (code == "")
                {
                    return;
                }
                string subcod = code.Substring(9, 3);
                if (subcod != "000")
                {
                    int index = this.ddlRptGroup.SelectedIndex;
                    switch (index)
                    {
                        case 0:
                            hlnkres.NavigateUrl = "~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=3&prjcode=" + prjcode + "&sircode=" + code;
                            break;
                        case 1:
                            hlnkres.NavigateUrl = "~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=2&prjcode=" + prjcode + "&sircode=" + code;
                            break;
                        default:
                            break;
                    }
                }

                if (code == "01AAAAAAAAAA" || code == "04AAAAAAAAAA")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;
                    lblgvRptRes1.Style.Add("text-align", "right");
                    e.Row.Attributes["style"] = "background-color:pink;font-size:16px; font-weight:bold;";
                }





                else if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvRptRes1.Font.Bold = true;
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;
                    lblgvRptRes1.Style.Add("text-align", "right");
                }

                else if (ASTUtility.Right(code, 8) == "00000000")
                {
                    lblgvRptRes1.Attributes["style"] = "font-weight:bold; color:green;";
                    lblgvRptAmt1.Font.Bold = true;
                    lblgvPer.Font.Bold = true;


                }



            }
        }

        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCod();
            string pactcode = this.Request.QueryString["prjcode"];
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloorList.DataTextField = "flrdes";
            this.ddlFloorList.DataValueField = "flrcod";
            this.ddlFloorList.DataSource = dt;
            this.ddlFloorList.DataBind();
            this.ddlFloorList.SelectedValue = "AAA";
        }
        protected void lbtnFloorList_Click(object sender, EventArgs e)
        {
            this.ShowFloorcode();
        }

        protected void ddlRptGroup_SelectedIndexChange(object sender, EventArgs e)
        {
            int rptIndex = this.ddlRptGroup.SelectedIndex;
            switch (rptIndex)
            {
                case 0:
                    this.paging.Visible = false;
                    this.floorwise.Visible = false;
                    break;
                case 1:
                    this.paging.Visible = false;
                    this.floorwise.Visible = false;

                    break;
                case 2:
                    this.floorwise.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
}