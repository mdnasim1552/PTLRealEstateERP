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

    public partial class TASStdAnalysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrintAnaLysis.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ImgbtnFindItem_OnClick(null, null);

            }
            if (this.ddlFloor1.Items.Count == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "FLOORLIST", "", "", "", "", "", "", "", "", "");
                this.ddlFloor1.DataTextField = "flrdes";
                this.ddlFloor1.DataValueField = "flrcod";
                this.ddlFloor1.DataSource = ds1.Tables[0];
                this.ddlFloor1.DataBind();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintAnaLysis_Click);
        }
        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mItmcod = this.ddlItem.SelectedValue.ToString();
            string mItmDes = this.ddlItem.SelectedItem.Text.Trim().Substring(12);
            string mFlrCod = this.ddlFloor1.SelectedValue.ToString();
            string mFlrDes = this.ddlFloor1.SelectedItem.Text.Trim().ToLowerInvariant();
            string mBldCoe = "000000000000";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RPTANASHEET", mBldCoe, mFlrCod, mItmcod, "", "", "", "", "", "");

            ReportDocument rptAnaSheet = new RealERPRPT.R_07_Ten.rptTASAnaSheet();
            rptAnaSheet.SetDataSource(ds1.Tables[0]);

            TextObject TxtRptTitle1 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle1"] as TextObject;
            TxtRptTitle1.Text = hst["comsnam"].ToString();

            TextObject TxtRptTitle2 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            TxtRptTitle2.Text = "";

            TextObject TxtRptTitle3 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            TxtRptTitle3.Text = ""; // Sch. Item No

            TextObject TxtRptTitle4 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            TxtRptTitle4.Text = ds1.Tables[1].Rows[0]["Itmdesc"].ToString();

            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);

            TextObject TxtRptTitle5 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "");

            Session["Report1"] = rptAnaSheet;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ImgbtnFindItem_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtItemSearch.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "ITMCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblItmCod"] = ds1.Tables[0];
            this.ddlItem.Items.Clear();
            this.ddlItem.DataTextField = "infdesc1";
            this.ddlItem.DataValueField = "infcod";
            this.ddlItem.DataSource = (DataTable)Session["tblItmCod"];
            this.ddlItem.DataBind();
        }

        protected void lbtnOk1_Click(object sender, EventArgs e)
        {
            if (this.ddlItem.Items.Count == 0)
                return;

            if (this.lbtnOk1.Text == "Other Item")
            {
                this.lbtnOk1.Text = "Select Item";
                this.txtItemSearch.Enabled = true;
                this.ImgbtnFindItem.Enabled = true;
                this.ddlItem.Visible = true;
                this.ChkCopy.Visible = false;
                this.ChkCopy.Checked = false;
                this.ChkCopy_CheckedChanged(null, null);
                this.lblItemDesc.Visible = false;
                this.lblItemDes2.Text = "";
                this.lblUnitFPS.Text = "";
                this.lblStdQtyF.Text = "";
                //this.lblUnitMKS.Text = "";
                //this.lblStdQtyM.Text = "";

                this.PnlAnalysis.Visible = false;
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                return;
            }
            this.lbtnOk1.Text = "Other Item";
            this.txtItemSearch.Enabled = false;
            this.ImgbtnFindItem.Enabled = false;
            this.ChkZeroQty.Checked = false;
            this.ddlItem.Visible = false;
            this.ChkCopy.Visible = true;
            this.lblItemDesc.Text = this.ddlItem.SelectedItem.Text.Trim();
            this.lblItemDesc.Width = this.ddlItem.Width;
            this.lblItemDesc.Visible = true;
            string ItmCod = this.ddlItem.SelectedValue.ToString().Trim();
            this.ImgbtnFindResource_OnClick(null, null);
            DataRow[] dr1 = ((DataTable)Session["tblItmCod"]).Select("infcod='" + ItmCod + "'");
            this.lblItemDes2.Text = this.ddlItem.SelectedItem.Text.Trim().Substring(14);
            this.lblUnitFPS.Text = dr1[0]["unitfps"].ToString().Trim();
            this.lblStdQtyF.Text = Convert.ToDouble(dr1[0]["stdqtyf"]).ToString("#,##0.0000");
            //this.lblUnitMKS.Text = dr1[0]["unitmks"].ToString().Trim();
            //this.lblStdQtyM.Text = Convert.ToDouble(dr1[0]["mksqty"]).ToString("#,##0.0000");
            this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
            this.PnlAnalysis.Visible = true;
            this.ShowColoumGroup(1);

        }

        protected void ShowAnalysisSheet(string itmcod1, string UnitFPS, string StdQtyFPS)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "STDANASHT", itmcod1, UnitFPS, StdQtyFPS, "", "", "", "", "", "");
            Session["tblStdAna"] = ds1.Tables[0];
            //this.lblUnitMKS.Text = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            // this.lblStdQtyM.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]).ToString("#,##0.0000");
        }

        protected void ImgbtnFindResource_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtResSearch.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "RESCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblResCod"] = ds1.Tables[0];
            this.ddlResource.Items.Clear();
            this.ddlResource.DataTextField = "rinfdesc1";
            this.ddlResource.DataValueField = "rinfcod";
            this.ddlResource.DataSource = (DataTable)Session["tblResCod"];
            this.ddlResource.DataBind();
        }
        //protected void ImgbtnFindResource_Click(object sender, ImageClickEventArgs e)
        //{

        //}

        protected void lbtnOk2_Click(object sender, EventArgs e)
        {
            if (this.ddlResource.Items.Count == 0)
                return;

            string ResCode = this.ddlResource.SelectedValue.ToString();
            string ResDesc = this.ddlResource.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)Session["tblStdAna"];
            DataRow[] dr1 = tbl1.Select("rescod='" + ResCode + "'");
            //if (dr1.Length > 0)
            //    return;

            this.Refresh_Analysis_Session();

            DataRow[] dr2 = ((DataTable)Session["tblResCod"]).Select("rinfcod='" + ResCode + "'");
            string ResUnit = dr2[0]["unitfps"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["rescod"] = ResCode;
            dr3["resdesc"] = ResDesc;
            dr3["resunit"] = ResUnit;
            for (int i = 1; i <= 50; i++)
                dr3["qty" + ASTUtility.Right("00" + i.ToString(), 3)] = 0;

            tbl1.Rows.Add(dr3);
            Session["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }

        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.ShowColoumGroup(Convert.ToInt32(((LinkButton)sender).Text));
        }

        protected void ShowColoumGroup(int i)
        {
            this.Refresh_Analysis_Session();
            this.gvAnalysis.DataSource = (DataTable)Session["tblStdAna"];
            this.gvAnalysis.DataBind();
            i = (i > 8 ? 1 : (i < 1 ? 8 : i));
            this.gvAnalysis.Columns[4].Visible = (i == 1);
            this.gvAnalysis.Columns[5].Visible = (i == 1);
            this.gvAnalysis.Columns[6].Visible = (i == 1);
            this.gvAnalysis.Columns[7].Visible = (i == 1);
            this.gvAnalysis.Columns[8].Visible = (i == 1);
            this.gvAnalysis.Columns[9].Visible = (i == 1);
            this.gvAnalysis.Columns[10].Visible = (i == 1);

            this.gvAnalysis.Columns[11].Visible = (i == 2);
            this.gvAnalysis.Columns[12].Visible = (i == 2);
            this.gvAnalysis.Columns[13].Visible = (i == 2);
            this.gvAnalysis.Columns[14].Visible = (i == 2);
            this.gvAnalysis.Columns[15].Visible = (i == 2);
            this.gvAnalysis.Columns[16].Visible = (i == 2);
            this.gvAnalysis.Columns[17].Visible = (i == 2);

            this.gvAnalysis.Columns[18].Visible = (i == 3);
            this.gvAnalysis.Columns[19].Visible = (i == 3);
            this.gvAnalysis.Columns[20].Visible = (i == 3);
            this.gvAnalysis.Columns[21].Visible = (i == 3);
            this.gvAnalysis.Columns[22].Visible = (i == 3);
            this.gvAnalysis.Columns[23].Visible = (i == 3);
            this.gvAnalysis.Columns[24].Visible = (i == 3);

            this.gvAnalysis.Columns[25].Visible = (i == 4);
            this.gvAnalysis.Columns[26].Visible = (i == 4);
            this.gvAnalysis.Columns[27].Visible = (i == 4);
            this.gvAnalysis.Columns[28].Visible = (i == 4);
            this.gvAnalysis.Columns[29].Visible = (i == 4);
            this.gvAnalysis.Columns[30].Visible = (i == 4);
            this.gvAnalysis.Columns[31].Visible = (i == 4);

            this.gvAnalysis.Columns[32].Visible = (i == 5);
            this.gvAnalysis.Columns[33].Visible = (i == 5);
            this.gvAnalysis.Columns[34].Visible = (i == 5);
            this.gvAnalysis.Columns[35].Visible = (i == 5);
            this.gvAnalysis.Columns[36].Visible = (i == 5);
            this.gvAnalysis.Columns[37].Visible = (i == 5);
            this.gvAnalysis.Columns[38].Visible = (i == 5);

            this.gvAnalysis.Columns[39].Visible = (i == 6);
            this.gvAnalysis.Columns[40].Visible = (i == 6);
            this.gvAnalysis.Columns[41].Visible = (i == 6);
            this.gvAnalysis.Columns[42].Visible = (i == 6);
            this.gvAnalysis.Columns[43].Visible = (i == 6);
            this.gvAnalysis.Columns[44].Visible = (i == 6);
            this.gvAnalysis.Columns[45].Visible = (i == 6);

            this.gvAnalysis.Columns[46].Visible = (i == 7);
            this.gvAnalysis.Columns[47].Visible = (i == 7);
            this.gvAnalysis.Columns[48].Visible = (i == 7);
            this.gvAnalysis.Columns[49].Visible = (i == 7);
            this.gvAnalysis.Columns[50].Visible = (i == 7);
            this.gvAnalysis.Columns[51].Visible = (i == 7);
            this.gvAnalysis.Columns[52].Visible = (i == 7);

            this.gvAnalysis.Columns[53].Visible = (i == 8);
            this.gvAnalysis.Columns[54].Visible = (i == 8);

            this.lblColGroup.Text = Convert.ToString(i);

        }

        protected void Refresh_Analysis_Session()
        {
            DataTable tbl1 = (DataTable)Session["tblStdAna"];
            for (int i = 0; i < this.gvAnalysis.Rows.Count; i++)
            {
                string gvResCod = ((Label)this.gvAnalysis.Rows[i].FindControl("lblgvResCod")).Text.Trim();
                for (int j = 1; j <= 50; j++)
                {
                    string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl(gvQty1)).Text.Trim().Replace(",", ""));
                    if (this.gvAnalysis.Columns[j + 4].Visible)
                    {
                        if (tbl1.Rows[i]["rescod"].ToString() == gvResCod)
                            tbl1.Rows[i]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
                    }
                }
            }
            Session["tblStdAna"] = tbl1;



        }

        protected void lbtnUpdateAna_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ItmCod = this.ddlItem.SelectedValue.ToString();
            string ZeroQty = (this.ChkZeroQty.Checked ? "NONZERO" : "INCLZERO");
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));

            DataTable tbl1 = (DataTable)Session["tblStdAna"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string BldCod = "000000000000";
                string ResCod = tbl1.Rows[i]["rescod"].ToString();
                for (int j = 1; j <= 50; j++)
                {
                    string FlrSlno = ASTUtility.Right("00" + j.ToString(), 3);
                    string ResQtyF = "0" + tbl1.Rows[i]["qty" + FlrSlno].ToString();
                    bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "UPDATEANASHT", BldCod, ItmCod,
                            ResCod, FlrSlno, ResQtyF, ZeroQty, "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }
            }
            if (ZeroQty == "NONZERO")
            {
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
                this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));
                this.ChkZeroQty.Checked = false;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            tbl1 = (DataTable)Session["tblStdAna"];

        }
        protected void lbtngvSlNo_Click(object sender, EventArgs e)
        {
            if (!this.gvAnalysis.Columns[4].Visible)
                return;

            int RowIndex = Convert.ToInt32(((LinkButton)sender).Text.Replace(".", "")) - 1;

            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
            DataTable tbl1 = (DataTable)Session["tblStdAna"];

            string gvResCod = ((Label)this.gvAnalysis.Rows[RowIndex].FindControl("lblgvResCod")).Text.Trim();
            for (int j = 1; j <= 42; j++)
            {
                string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[RowIndex].FindControl("txtgvQty001")).Text.Trim().Replace(",", ""));
                ((TextBox)this.gvAnalysis.Rows[RowIndex].FindControl(gvQty1)).Text = gvQty2.ToString("#,##0.0000;(#,##0.0000); ");
                if (tbl1.Rows[RowIndex]["rescod"].ToString() == gvResCod)
                    tbl1.Rows[RowIndex]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
            }
            Session["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }
        protected void lbtnInputSame_Click(object sender, EventArgs e)
        {
            if (!this.gvAnalysis.Columns[4].Visible)
                return;

            DataTable tbl1 = (DataTable)Session["tblStdAna"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                double gvQty1 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtgvQty001")).Text.Trim().Replace(",", ""));
                for (int j = 1; j <= 50; j++)
                {
                    string gvQty2 = "qty" + ASTUtility.Right("00" + j.ToString(), 3);
                    tbl1.Rows[i][gvQty2] = gvQty1;
                    string gvQty3 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    ((TextBox)this.gvAnalysis.Rows[i].FindControl(gvQty3)).Text = gvQty1.ToString("#,##0.0000;(#,##0.0000); ");
                }

            }
            Session["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }
        protected void ChkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.lbtnCopyData.Visible = this.ChkCopy.Checked;
            this.ddlItemToCopy.Visible = this.ChkCopy.Checked;
            this.txtItemSearchToCopy.Visible = this.ChkCopy.Checked;
            this.ImgbtnFindItemToCopy.Visible = this.ChkCopy.Checked;
        }
        protected void ImgbtnFindItemToCopy_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtItemSearch.Text.Trim() + "%";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "ITMCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlItemToCopy.Items.Clear();
            this.ddlItemToCopy.DataTextField = "infdesc1";
            this.ddlItemToCopy.DataValueField = "infcod";
            this.ddlItemToCopy.DataSource = ds1.Tables[0];
            this.ddlItemToCopy.DataBind();
        }
        protected void lbtnCopyData_Click(object sender, EventArgs e)
        {
            this.gvAnalysis.DataSource = null;
            this.gvAnalysis.DataBind();
            string ItmCod = this.ddlItemToCopy.SelectedValue.ToString().Trim();
            this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
            this.ShowColoumGroup(1);
        }
        protected void gvAnalysis_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblStdAna"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Itemcode = this.ddlItem.SelectedValue.ToString();
            string Rescode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "DELETERESOURCE", Itemcode,
                           Rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvAnalysis.PageSize) * (this.gvAnalysis.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            this.gvAnalysis.DataSource = dv.ToTable();
            this.gvAnalysis.DataBind();
            Session.Remove("tblStdAna");
            Session["tblStdAna"] = dv.ToTable();
            this.ShowColoumGroup(1);
        }



    }
}
