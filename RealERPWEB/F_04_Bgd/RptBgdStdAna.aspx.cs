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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptBgdStdAna : System.Web.UI.Page
    {
        ProcessAccess bgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                if (this.lblRptType.Text.Length == 0)
                {
                    string mRptType = Request.QueryString["rpttype"].ToString().Trim();
                    string ItemCode = Request.QueryString["isircode"].ToString().Trim();
                    string Isirdesc = Request.QueryString["Isirdesc"].ToString().Trim();
                    this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
                    switch (mRptType)
                    {
                        case "RptAnaly":
                            lbItemCode.Text = ItemCode;
                            lblItemDesc.Text = Isirdesc;
                            //this.ShowColoumGroup(1);
                            this.ShowInfo();
                            break;
                    }
                }

            if (this.ddlFloor1.Items.Count == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "FLOORLIST", "", "", "", "", "", "", "", "", "");
                this.ddlFloor1.DataTextField = "flrdes";
                this.ddlFloor1.DataValueField = "flrcod";
                this.ddlFloor1.DataSource = ds1.Tables[0];
                this.ddlFloor1.DataBind();
                // Grid Header
                DataTable dt = ds1.Tables[0];

                int j = 5;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (i == 3 || i == 4 || i == 5)
                    //{
                    //    j++;
                    //    continue;

                    //}
                    this.gvAnalysis.Columns[j].HeaderText = dt.Rows[i]["flrdes"].ToString();
                    j++;

                }
            }





        }

        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ItemCode = Request.QueryString["isircode"].ToString().Trim();
            //string mItmcod = this.ddlItem.SelectedValue.ToString();
            //string mItmDes = this.ddlItem.SelectedItem.Text.Trim().Substring(15);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mFlrCod = this.ddlFloor1.SelectedValue.ToString();
            string mFlrDes = this.ddlFloor1.SelectedItem.Text.Trim().ToLowerInvariant();
            string mBldCoe = "000000000000";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTANASHEET", mBldCoe, mFlrCod, ItemCode, "", "", "", "", "", "");


            LocalReport Rpt1 = new LocalReport();
            //  DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugdAna>();
            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptStdAnaSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Item Name: " + ds1.Tables[1].Rows[0]["Itmdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Quantity", "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "")));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Standard Analysis"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptStdAnaSheet = new RealERPRPT.R_04_Bgd.rptStdAnaSheet();
            //TextObject TxtRptTitle2 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            //TxtRptTitle2.Text = "Standard Analysis";

            ////TextObject TxtRptTitle3 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            ////TxtRptTitle3.Text = "Item No:"; // Sch. Item No

            //TextObject TxtRptTitle4 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            //TxtRptTitle4.Text = ds1.Tables[1].Rows[0]["Itmdesc"].ToString();

            //string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            //string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            //double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            //double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);

            //TextObject TxtRptTitle5 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            //TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
            //    (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "");
            //TextObject txtuserinfo = rptStdAnaSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "STANDARD ANALYSIS INFORMATION";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptStdAnaSheet.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptStdAnaSheet.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStdAnaSheet;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 

        }

        protected void ShowInfo()
        {
            this.lblUnitFPS.Text = "";
            this.lblStdQtyF.Text = "";
            string ItmCod = Request.QueryString["isircode"].ToString().Trim();
            string isirunit = Request.QueryString["isirunit"].ToString().Trim();
            string stdqty = Request.QueryString["stdqty"].ToString().Trim();
            this.lblUnitFPS.Text = isirunit;
            this.lblStdQtyF.Text = Convert.ToDouble(stdqty).ToString("#,##0.0000");
            this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
            this.PnlAnalysis.Visible = true;
            this.ShowColoumGroup(1);

        }


        protected void lbtnOk1_Click(object sender, EventArgs e)
        {
            this.ShowInfo();
        }

        protected void ShowAnalysisSheet(string itmcod1, string UnitFPS, string StdQtyFPS)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "STDANASHT", itmcod1, UnitFPS, StdQtyFPS, "", "", "", "", "", "");
            Session["tblStdAna"] = ds1.Tables[0];
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
                        if (tbl1.Rows[i]["rsircode"].ToString() == gvResCod)
                            tbl1.Rows[i]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
                    }
                }
            }
            Session["tblStdAna"] = tbl1;
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
                if (tbl1.Rows[RowIndex]["rsircode"].ToString() == gvResCod)
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

    }
}
