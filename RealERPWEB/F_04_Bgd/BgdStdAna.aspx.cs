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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdStdAna : System.Web.UI.Page
    {
        ProcessAccess bgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "STANDARD ANALYSIS";

                this.ImgbtnFindItem_Click(null, null);

                this.lbtnUpdateAna.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                this.gvAnalysis.Columns[1].Visible = (Convert.ToBoolean(dr1[0]["entry"]));
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.GetFloorList();


                //this.lblFloor1.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Phase" : "Floor";
                this.lbtngvP2.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP3.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP4.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP5.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP6.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP7.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
                this.lbtngvP8.Visible = (ASTUtility.Left(comcod, 1) == "3" || ASTUtility.Left(comcod, 1) == "1") ? true : false;
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintAnaLysis_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetFloorList()
        {
            Session.Remove("tblfloor");
            string comcod = this.GetCompCode();
            // string grp = (ASTUtility.Left(comcod, 1) == "2") ? "L" : "R";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "FLOORLIST", "", "", "", "", "", "", "", "", "");
            Session["tblfloor"] = ds1.Tables[0];


            //this.ddlFloor1.DataTextField = "flrdes";
            //this.ddlFloor1.DataValueField = "flrcod";
            //this.ddlFloor1.DataSource = ds1.Tables[0];
            //this.ddlFloor1.DataBind();

        }

        private void GetddlFloorList()
        {

            DataTable dt = ((DataTable)Session["tblfloor"]).Copy();
            DataTable dt1 = (DataTable)Session["tblItmCod"];
            string isircode = this.ddlItem.SelectedValue.ToString();
            string cattype = (dt1.Select("isircode='" + isircode + "'"))[0]["cattype"].ToString();
            DataView dv = dt.DefaultView;
            //dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            //dv.Sort = ("cattype,flrcod");
            //dt = dv.ToTable();
            dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            dv.Sort = ("cattype,flrslno");
            dt = dv.ToTable();

            this.ddlFloor1.DataTextField = "flrdes";
            this.ddlFloor1.DataValueField = "flrcod";
            this.ddlFloor1.DataSource = dt;
            this.ddlFloor1.DataBind();

        }



        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {

            // ** ***Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string mItmcod = this.ddlItem.SelectedValue.ToString();
            string mItmDes = this.ddlItem.SelectedItem.Text.Trim().Substring(15);
            string mFlrCod = this.ddlFloor1.SelectedValue.ToString();
            string mFlrDes = this.ddlFloor1.SelectedItem.Text.Trim().ToLowerInvariant();
            string mBldCoe = "000000000000";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTANASHEET", mBldCoe, mFlrCod, mItmcod, "", "", "", "", "", "");
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
            Rpt1.SetParameters(new ReportParameter("ProjectNam", mItmDes));
            Rpt1.SetParameters(new ReportParameter("Quantity", "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "")));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Standard Analysis"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        public string GetcomItemlock()
        {
            string comlockacme = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3338"://Acme
                case "1205"://P2P engineering
                case "3351"://Wecon Properties
                case "3352"://P2P 360

                    comlockacme = "lock";
                    break;


                default:
                    break;


            }

            return comlockacme;

        }

        protected void ImgbtnFindItem_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string srchTxt = "%" + this.txtItemSearch.Text.Trim() + "%";
            string comlock = GetcomItemlock();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "ITMCODELIST", srchTxt, comlock, userid, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {

                ds1.Tables[0].Rows.Add("XXXXXXXXXXXX", "----Have No Code Permission Please Contact Sys Admin----", "", "", 0, 0, "");

            }
            else
            {
                ds1.Tables[0].Rows.Add("000000000000", "---------------- Select Work List ----------------", "", "", 0, 0, "");

            }

            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "isircode";

            Session["tblItmCod"] = dv.ToTable();// ds1.Tables[0];
            this.ddlItem.Items.Clear();
            this.ddlItem.DataTextField = "isirdesc1";
            this.ddlItem.DataValueField = "isircode";
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

                this.PnlAnalysis.Visible = false;
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                this.lbtnOk2.Enabled = true;
                return;
            }
            this.lbtnOk1.Text = "Other Item";
            this.txtItemSearch.Enabled = false;
            this.ImgbtnFindItem.Enabled = false;
            this.ChkZeroQty.Checked = false;
            this.ddlItem.Visible = false;
            this.ChkCopy.Visible = true;
            this.lblItemDesc.Text = this.ddlItem.SelectedItem.Text.Trim().Substring(14);
            this.lblItemDesc.Width = this.ddlItem.Width;
            this.lblItemDesc.Visible = true;
            string ItmCod = this.ddlItem.SelectedValue.ToString().Trim();

            DataRow[] dr1 = ((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCod + "'");
            this.lblItemDes2.Text = dr1[0]["isirdesc2"].ToString().Trim();
            this.lblUnitFPS.Text = dr1[0]["isirunit"].ToString().Trim();
            this.lblStdQtyF.Text = Convert.ToDouble(dr1[0]["stdqty"]).ToString("#,##0.0000");
            this.GetddlFloorList();
            this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
            this.PnlAnalysis.Visible = true;
            this.ShowColoumGroup(1);
            this.ImgbtnFindResource_Click(null, null);



            bool lockitem = Convert.ToBoolean(((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCod + "'")[0]["lock"]);
            this.lbtnOk2.Enabled = !lockitem;


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Budget Standard Analysis";
                string eventdesc = "Select Items";
                string eventdesc2 = this.ddlItem.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void ShowAnalysisSheet(string itmcod1, string UnitFPS, string StdQtyFPS)
        {
            ViewState.Remove("tblStdAna");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "STDANASHT", itmcod1, UnitFPS, StdQtyFPS, "", "", "", "", "", "");
            ViewState["tblStdAna"] = ds1.Tables[0];
            //this.lblUnitMKS.Text = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            //this.lblStdQtyM.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]).ToString("#,##0.0000");
        }

        protected void ImgbtnFindResource_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtResSearch.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "RESCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblResCod"] = ds1.Tables[0];
            this.ddlResource.Items.Clear();
            this.ddlResource.DataTextField = "rsirdesc1";
            this.ddlResource.DataValueField = "rsircode";
            this.ddlResource.DataSource = (DataTable)Session["tblResCod"];
            this.ddlResource.DataBind();
        }

        protected void lbtnOk2_Click(object sender, EventArgs e)
        {
            if (this.ddlResource.Items.Count == 0)
                return;

            string ResCode = this.ddlResource.SelectedValue.ToString();
            string ResDesc = this.ddlResource.SelectedItem.Text.Trim();
            DataTable tbl1 = (DataTable)ViewState["tblStdAna"];
            DataRow[] dr1 = tbl1.Select("rsircode='" + ResCode + "'");
            if (dr1.Length > 0)
                return;

            this.Refresh_Analysis_Session();

            DataRow[] dr2 = ((DataTable)Session["tblResCod"]).Select("rsircode='" + ResCode + "'");
            string ResUnit = dr2[0]["rsirunit"].ToString();
            DataRow dr3 = tbl1.NewRow();
            dr3["rsircode"] = ResCode;
            dr3["rsirdesc"] = ResDesc;
            dr3["rsirunit"] = ResUnit;
            for (int i = 1; i <= 54; i++)
                dr3["qty" + ASTUtility.Right("00" + i.ToString(), 3)] = 0;

            tbl1.Rows.Add(dr3);
            ViewState["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }

        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.ShowColoumGroup(Convert.ToInt32(((LinkButton)sender).Text));
        }

        private void GridHeeader()
        {


            // Grid Header
            DataTable dt = ((DataTable)Session["tblfloor"]).Copy();
            DataTable dt1 = (DataTable)Session["tblItmCod"];
            string isircode = this.ddlItem.SelectedValue.ToString();
            string cattype = (dt1.Select("isircode='" + isircode + "'"))[0]["cattype"].ToString();
            DataView dv = dt.DefaultView;

            dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            dv.Sort = ("cattype,flrslno");
            dt = dv.ToTable();
            /*
                    dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
                    dv.Sort = ("cattype,flrcod");
                    dt = dv.ToTable();
             */
            int j = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.gvAnalysis.Columns[j].HeaderText = dt.Rows[i]["flrdes"].ToString();
                j++;

            }
        }
        protected void ShowColoumGroup(int i)
        {
            this.Refresh_Analysis_Session();
            this.GridHeeader();
            DataTable dt = (DataTable)ViewState["tblStdAna"];
            this.gvAnalysis.DataSource = (DataTable)ViewState["tblStdAna"];
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
            this.gvAnalysis.Columns[55].Visible = (i == 8);
            this.gvAnalysis.Columns[56].Visible = (i == 8);
            this.gvAnalysis.Columns[57].Visible = (i == 8);
            this.gvAnalysis.Columns[58].Visible = (i == 8);


            this.lblColGroup.Text = Convert.ToString(i);

        }

        protected void Refresh_Analysis_Session()
        {

            DataTable tbl1 = (DataTable)ViewState["tblStdAna"];
            int i = 0;
            foreach (GridViewRow gv1 in gvAnalysis.Rows)
            {
                string gvResCod = ((Label)gv1.FindControl("lblgvResCod")).Text.Trim();
                for (int j = 1; j <= 54; j++)
                {
                    string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    double gvQty2 = Convert.ToDouble("0" + ((TextBox)gv1.FindControl(gvQty1)).Text.Trim().Replace(",", ""));
                    if (this.gvAnalysis.Columns[j + 4].Visible)
                    {
                        if (tbl1.Rows[i]["rsircode"].ToString() == gvResCod)
                            tbl1.Rows[i]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
                    }
                }
                i++;
            }

            //Session["tblStdAna"] = tbl1;

            //DataTable tbl1 = (DataTable)Session["tblStdAna"];
            //for (int i = 0; i < this.gvAnalysis.Rows.Count; i++)
            //{
            //    string gvResCod = ((Label)this.gvAnalysis.Rows[i].FindControl("lblgvResCod")).Text.Trim();
            //    for (int j = 1; j <= 52; j++)
            //    {
            //        string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
            //        double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl(gvQty1)).Text.Trim().Replace(",", ""));
            //        if (this.gvAnalysis.Columns[j + 4].Visible)
            //        {
            //            if (tbl1.Rows[i]["rsircode"].ToString() == gvResCod)
            //                tbl1.Rows[i]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
            //        }
            //    }
            //}
            //Session["tblStdAna"] = tbl1;
        }

        protected void lbtnUpdateAna_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string trmid = hst["compname"].ToString();
            string sessionid = hst["session"].ToString();


            string ItmCod = this.ddlItem.SelectedValue.ToString();
            string ZeroQty = (this.ChkZeroQty.Checked ? "NONZERO" : "INCLZERO");
            DataTable dt1 = (DataTable)ViewState["tblStdAna"];
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));

            DataTable tbl1 = (DataTable)ViewState["tblStdAna"];

            bool lockitem = Convert.ToBoolean(((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCod + "'")[0]["lock"]);
            if (lockitem)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Item is already locked";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }
            //Checked Analysis
            //bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_IRSTDANA", "UPDATEANASHT", ItmCod,
            //               ResCod, FlrSlNo, ResQtyF, ZeroQty, "", "", "", "", "", "", "", "", "", "");

            DataTable dt = ((DataTable)Session["tblfloor"]).Copy();
            string cattype = (((DataTable)Session["tblItmCod"]).Select("isircode='" + ItmCod + "'"))[0]["cattype"].ToString();
            DataView dv = dt.DefaultView;

            dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
            dv.Sort = ("cattype,flrslno");
            dt = dv.ToTable();
            /*
                    dv.RowFilter = ("cattype='" + cattype + "' or cattype='CCC'"); // for common work
                    dv.Sort = ("cattype,flrcod");
                    dt = dv.ToTable();
             */


            int flr = 0, i = 0;
            int tocount = dt.Rows.Count;
            DataTable dtstd = new DataTable();

            if (Session["tblirstdana"] == null)
            {
                this.CreateTable();


            }
            dtstd = ((DataTable)Session["tblirstdana"]).Clone();
          
            string rsircode = "";
            foreach (DataRow dr1 in tbl1.Rows)
            {

                flr = 0;
                 rsircode = dr1["rsircode"].ToString();
                //  int grpst= (ASTUtility.Left(comcod, 1) == "2") ? 51 : 1; 

                for (int j = 1; j <= tocount; j++)
                {
                    string flrcod = dt.Rows[flr++]["flrcod"].ToString();
                    string rstdqty = "0" + dr1["qty" + ASTUtility.Right("00" + j.ToString(), 3)];
                    DataRow drstd = dtstd.NewRow();
                    drstd["rsircode"] = rsircode;
                    drstd["flrcod"] = flrcod;
                    drstd["rstdqty"] = rstdqty;
                    dtstd.Rows.Add(drstd);

                   // result = bgdData.UpdateXmlTransInfo(comcod, "SP_ENTRY_IRSTDANA", "UPDATEANASHT", null, null, null, ItmCod, rsircode, flrcod, rstdqty, usrid, PostedDat, trmid, sessionid, "", "");


                }
            }


            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dtstd);
            ds1.Tables[0].TableName = "tbl1";
            //string xml = ds1.GetXml();
           // return;
            bool result = bgdData.UpdateXmlTransInfo(comcod, "SP_ENTRY_IRSTDANA", "UPDATEANASHT", ds1, null, null, ItmCod, "", "", "", usrid, PostedDat, trmid, sessionid);

            if (!result)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = bgdData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            //bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_IRSTDANA", "UPDATEANASHT", ItmCod,
            //        ResCod, flrcod, ResQtyF, ZeroQty, "", "", "", "", "", "", "", "", "", "");

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ZeroQty == "NONZERO")
            {
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                this.ShowAnalysisSheet(ItmCod, this.lblUnitFPS.Text, this.lblStdQtyF.Text.Replace(",", ""));
                this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));
                this.ChkZeroQty.Checked = false;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Budget Standard Analysis";
                string eventdesc = "Update Analysis";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




            //foreach (DataRow dr1 in  tbl1.Rows)
            //{

            //    flr = 0;
            //    string ResCod = dr1["rsircode"].ToString();
            //    //  int grpst= (ASTUtility.Left(comcod, 1) == "2") ? 51 : 1;

            //    for (int j = 1; j <= tocount; j++)
            //    {
            //        string flrcod = dt.Rows[flr++]["flrcod"].ToString();
            //        // string FlrSlNo = ASTUtility.Right("00" + grpst.ToString(), 3);
            //        string ResQtyF = "0" + dr1["qty" + ASTUtility.Right("00" + j.ToString(), 3)];
            //        bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_IRSTDANA", "UPDATEANASHT", ItmCod,
            //                ResCod, flrcod, ResQtyF, ZeroQty, "", "", "", "", "", "", "", "", "", "");
            //        // grpst++;
            //    }
            //}


            // tbl1 = (DataTable)Session["tblStdAna"];
        }



        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            // tblt01.Columns.Add("isircode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
            tblt01.Columns.Add("rstdqty", Type.GetType("System.Double"));
            // tblt01.Columns.Add("ZeroQty", Type.GetType("System.String"));
            Session["tblirstdana"] = tblt01;




        }


        protected void lbtngvSlNo_Click(object sender, EventArgs e)
        {
            if (!this.gvAnalysis.Columns[4].Visible)
                return;

            int RowIndex = Convert.ToInt32(((LinkButton)sender).Text.Replace(".", "")) - 1;

            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
            DataTable tbl1 = (DataTable)ViewState["tblStdAna"];

            string gvResCod = ((Label)this.gvAnalysis.Rows[RowIndex].FindControl("lblgvResCod")).Text.Trim();
            for (int j = 1; j <= 44; j++)  //may be problem
            {
                string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[RowIndex].FindControl("txtgvQty001")).Text.Trim().Replace(",", ""));
                ((TextBox)this.gvAnalysis.Rows[RowIndex].FindControl(gvQty1)).Text = gvQty2.ToString("#,##0.0000;(#,##0.0000); ");
                if (tbl1.Rows[RowIndex]["rsircode"].ToString() == gvResCod)
                    tbl1.Rows[RowIndex]["qty" + ASTUtility.Right("00" + j.ToString(), 3)] = gvQty2;
            }
            ViewState["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }
        protected void lbtnInputSame_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            if (!this.gvAnalysis.Columns[4].Visible)
                return;

            DataTable tbl1 = (DataTable)ViewState["tblStdAna"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                double gvQty1 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtgvQty001")).Text.Trim().Replace(",", ""));


                switch (comcod)
                {
                    case "3315"://Assure
                    case "3316": //Assure
                                 // case "3101": //Assure
                                 //if (gvQty1 == 0.00)
                                 // continue;
                        break;

                    default:

                        if (gvQty1 == 0.00)
                            continue;


                        break;

                }



                for (int j = 1; j <= 54; j++)
                {

                    //if (gvQty1 == 0.00)  //All Company (First Zero quantity)
                    //          break;

                    string gvQty2 = "qty" + ASTUtility.Right("00" + j.ToString(), 3);
                    tbl1.Rows[i][gvQty2] = gvQty1;
                    string gvQty3 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    ((TextBox)this.gvAnalysis.Rows[i].FindControl(gvQty3)).Text = gvQty1.ToString("#,##0.0000;(#,##0.0000); ");
                }
            }
            ViewState["tblStdAna"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }
        protected void ChkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.lbtnCopyData.Visible = this.ChkCopy.Checked;
            this.ddlItemToCopy.Visible = this.ChkCopy.Checked;
            this.txtItemSearchToCopy.Visible = this.ChkCopy.Checked;
            this.ImgbtnFindItemToCopy.Visible = this.ChkCopy.Checked;
        }
        protected void ImgbtnFindItemToCopy_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string comlock = GetcomItemlock();
            string srchTxt = "%" + this.txtItemSearchToCopy.Text.Trim() + "%";

            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_IRSTDANA", "ITMCODELIST", srchTxt, comlock, usrid, "", "", "", "", "", "");
            this.ddlItemToCopy.Items.Clear();
            this.ddlItemToCopy.DataTextField = "isirdesc1";
            this.ddlItemToCopy.DataValueField = "isircode";
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
            DataTable dt = (DataTable)ViewState["tblStdAna"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Itemcode = this.ddlItem.SelectedValue.ToString();
            string Rescode = ((Label)this.gvAnalysis.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_IRSTDANA", "DELETERESOURCE", Itemcode,
                           Rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result)
            {
                bool result2 = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_IRSTDANA", "DELETERESOURCE02", Itemcode,Rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
                int rowindex = (this.gvAnalysis.PageSize) * (this.gvAnalysis.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            this.gvAnalysis.DataSource = dv.ToTable();
            this.gvAnalysis.DataBind();
            ViewState.Remove("tblStdAna");
            ViewState["tblStdAna"] = dv.ToTable();
            this.ShowColoumGroup(1);
        }
        protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
        {



            DataTable dt = (DataTable)Session["tblResCod"];
            int tindex = dt.Rows.Count;
            if (tindex > 15)
            {
                string rsircode = this.ddlResource.SelectedValue.ToString();
                int sindex = Convert.ToInt16((dt.Select("rsircode='" + rsircode + "'"))[0]["rowid"].ToString());

                DataTable dt2 = dt.Clone();
                int rowid = 1;
                for (int i = sindex - 1; i < tindex; i++)
                {
                    DataRow dr1 = dt2.NewRow();
                    dr1["rowid"] = rowid;
                    dr1["rsircode"] = dt.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc1"] = dt.Rows[i]["rsirdesc1"].ToString();
                    dr1["rsirunit"] = dt.Rows[i]["rsirunit"].ToString();
                    dr1["stdrat"] = dt.Rows[i]["stdrat"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }


                for (int i = 0; i < sindex - 1; i++)
                {
                    DataRow dr1 = dt2.NewRow();
                    dr1["rowid"] = rowid;
                    dr1["rsircode"] = dt.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc1"] = dt.Rows[i]["rsirdesc1"].ToString();
                    dr1["rsirunit"] = dt.Rows[i]["rsirunit"].ToString();
                    dr1["stdrat"] = dt.Rows[i]["stdrat"].ToString();
                    rowid++;
                    dt2.Rows.Add(dr1);

                }
                Session["tblResCod"] = dt2;
                this.ddlBound();
            }


        }


        private void ddlBound()
        {

            this.ddlResource.Items.Clear();
            DataTable dt = (DataTable)Session["tblResCod"];

            this.ddlResource.Items.Clear();
            this.ddlResource.DataTextField = "rsirdesc1";
            this.ddlResource.DataValueField = "rsircode";
            this.ddlResource.DataSource = dt;
            this.ddlResource.DataBind();
        }


    }
}

