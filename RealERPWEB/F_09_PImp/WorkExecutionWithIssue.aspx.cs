using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealEntity.C_09_PIMP.EClassExecution;

namespace RealERPWEB.F_09_PImp
{
    public partial class WorkExecutionWithIssue : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Permission Part
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue";
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue - EDIT MODE";
                }
                InitPage();
            }
        }
        //----SETUP----
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private string GetUserId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            return userid;
        }
        public void InitPage()
        {
            txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            ddlProject.Enabled = true;
            GetProject();
            txtWRefNo.Text = "";
            txtSMCR.Text = "";
            txtDMIRF.Text = "";
            //Default Panel Visible false
            pnl1.Visible = false;
            pnl2.Visible = false;
        }
        //----END SETUP----
        protected void btnOK_Click(object sender, EventArgs e)
        {
            OnOKClick();
        }
        private void OnOKClick()
        {
            if (btnOK.Text == "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK")
            {
                //OK Click
                ddlProject.Enabled = false;
                pnl1.Visible = true;
                pnl2.Visible = false;
                btnOK.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                GetCategory();
                GetItem();
                GetDivision();
                GetWorkWithIssue();
                GetMaterials();
                GetWENIssueNo();
                GridOne_DataBind();
                CreateTable();
                GridTwo_DataBind();
            }
            else
            {
                //New Click
                ddlProject.Enabled = true;
                pnl1.Visible = false;
                pnl2.Visible = false;
                btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
                //Remove All Session
                GetWENIssueNo();
                GridOne_DataBind();
                CreateTable();
                GridTwo_DataBind();
                InitPage();
            }
        }
        private void GetProject()
        {
            string userid = GetUserId();
            string comcod = GetComCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST01", "%", "", userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }
        private void GetCategory()
        {
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string flrcode = "";
            string date = this.txtEntryDate.Text.Trim();
            string txtsrchItem = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");

            Session["itemlist"] = ds1.Tables[0];
            Session["item"] = ds1.Tables[1];
            if (ds1 == null)
                return;
            this.ddlCategory.DataTextField = "sirdesc";
            this.ddlCategory.DataValueField = "mitemcode";
            this.ddlCategory.DataSource = ds1.Tables[2];
            this.ddlCategory.DataBind();
        }

        private void GetItem()
        {
            string itemcode = this.ddlCategory.SelectedValue.Substring(0, 4).ToString() + "%";
            DataTable dt = ((DataTable)Session["item"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode like '" + itemcode + "' ");
            dt = dv.ToTable();
            this.ddlItem.DataTextField = "workitem";
            this.ddlItem.DataValueField = "itemcode";
            this.ddlItem.DataSource = dt;
            this.ddlItem.DataBind();
        }
        public void GetDivision()
        {
            string Worklists = this.ddlItem.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode= " + Worklists);
            dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");
            this.ddlDivision.DataTextField = "flrdes1";
            this.ddlDivision.DataValueField = "flrdes1";
            this.ddlDivision.DataSource = dt;
            this.ddlDivision.DataBind();
        }
        private void GetWENIssueNo()
        {
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string CurDate1 = txtEntryDate.Text;
            string mISSNo = "NEWISS";
            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURISSUEINFO", mISSNo, CurDate1,
                             pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["sessionforgrid"] = ds1.Tables[0];

            GetIssueNo();
        }

        private void GetIssueNo()
        {
            string comcod = this.GetComCode();
            string isudate = this.txtEntryDate.Text.Trim();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISSUEINFO", isudate, "", "", "", "", "", "", "", "");
            this.txtWINoPartOne.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
            this.txtWINoPartTwo.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
            GetDivision();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDivision();
        }
        private void LoopForSession()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            int TblRowIndex;
            for (int i = 0; i < this.DataGridOne.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridOne.Rows[i].FindControl("txtwrkqty")).Text.Trim());
                TblRowIndex = (DataGridOne.PageIndex) * DataGridOne.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);

                if (balqty < txtwrkqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                dt.Rows[TblRowIndex]["wrkqty"] = txtwrkqty;

            }
            Session["sessionforgrid"] = dt;

        }
        protected void btnSelectOne_Click(object sender, EventArgs e)
        {
            OnSelectOneClick();
        }
        private void OnSelectOneClick()
        {
            LoopForSession();
            DataTable itemtable = (DataTable)Session["itemlist"];
            DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            string itemcode = this.ddlItem.SelectedValue.ToString().Trim();
            string gp = this.ddlDivision.SelectedValue.Trim();
            var ItemList = (DataTable)Session["itemlist"];

            if (gp.Length > 0)
            {
                foreach (ListItem s1 in ddlDivision.Items)
                {
                    if (s1.Selected)
                    {
                        string flrcode = s1.Value.Substring(0, 3);
                        DataRow[] dr1 = tempforgrid.Select("flrcod='" + flrcode + "'  and itemcode='" + itemcode + "'");
                        if (dr1.Length == 0)
                        {
                            DataRow drforgrid = tempforgrid.NewRow();
                            drforgrid["flrcod"] = flrcode;
                            drforgrid["flrdes"] = (ItemList.Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["flrdes"];
                            drforgrid["wrkqty"] = 0;
                            drforgrid["stdqty"] = (ItemList.Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["stdqty"];
                            drforgrid["balqty"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["balqty"];
                            drforgrid["wrkunit"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["wrkunit"];
                            drforgrid["itemcode"] = this.ddlItem.SelectedValue.ToString();
                            drforgrid["workitem"] = this.ddlItem.SelectedItem.ToString().Trim();
                            tempforgrid.Rows.Add(drforgrid);

                        }
                    }
                }
            }
            Session["sessionforgrid"] = tempforgrid;
            this.GridOne_DataBind();
            GetDivision();
            //this.ddlDivision.Text = "";

        }

        private void GridOne_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.DataGridOne.DataSource = tbl1;
            this.DataGridOne.DataBind();
        }

        private void GetWorkWithIssue()
        {
            string comcod = this.GetComCode();
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEINFOFROMWORK", "", "", "", "", "", "", "", "", "");
            ViewState["WorkExeWithIssue"] = ds.Tables[0];
            ViewState["WorkExeWithLabour"] = ds.Tables[1];
        }
        private string ComCalltype()
        {

            string withmat = "";
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "3335":
                case "3333":

                    // case "3101":
                    withmat = "WithMat";
                    break;
                default:
                    withmat = "";
                    break;
            }
            return withmat;
        }
        private string ComZeroBal()
        {
            string zerobal = "";
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "3336":
                    zerobal = "Zero";
                    break;

                default:
                    zerobal = "";
                    break;
            }
            return zerobal;
        }
        private void GetMaterials()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(txtEntryDate.Text).ToString("dd-MMM-yyyy");
            string SearchMat = "%";

            string withmat = this.ComCalltype();
            string zerobal = this.ComZeroBal();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMETERIALS", pactcode, date, SearchMat, "", "", "", "", "", "");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, date, "", SearchMat, withmat, zerobal, "", "", "", "");

            ViewState["itemlistMaterialsBalance"] = ds1.Tables[0];
            ViewState["specification"] = ds1.Tables[2];
            ViewState["itemlistLabourBalance"] = ds2.Tables[0];


        }

        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("comcod", Type.GetType("System.String"));
            tblt01.Columns.Add("isircode", Type.GetType("System.String"));
            tblt01.Columns.Add("isirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
            tblt01.Columns.Add("flrdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rstdqty", Type.GetType("System.String"));
            tblt01.Columns.Add("Ratio", Type.GetType("System.Double"));
            tblt01.Columns.Add("isuqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("spcfcod", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirunit", Type.GetType("System.String"));
            tblt01.Columns.Add("balqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("useoflocation", Type.GetType("System.String"));
            tblt01.Columns.Add("remarks", Type.GetType("System.String"));
            ViewState["materialexefinal"] = tblt01;
        }
        private void CreateTableLabour()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("comcod", Type.GetType("System.String"));
            tblt01.Columns.Add("isircode", Type.GetType("System.String"));
            tblt01.Columns.Add("isirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
            tblt01.Columns.Add("flrdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rstdqty", Type.GetType("System.String"));
            tblt01.Columns.Add("Ratio", Type.GetType("System.Double"));
            tblt01.Columns.Add("isuqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("isurat", Type.GetType("System.Double"));
            tblt01.Columns.Add("rsirunit", Type.GetType("System.String"));
            tblt01.Columns.Add("balqty", Type.GetType("System.Double"));
            ViewState["labourexefinal"] = tblt01;
        }
        protected void btnGenerateIssue_Click(object sender, EventArgs e)
        {
            if (txtWRefNo.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Enter Reference No." + "');", true);
                return;
            }
            else
            {
                CreateTable();
                CreateTableLabour();
                GetPerMatIssu();
                Get_Issue_Info();
                Get_IssueLabour_Info();
                GetConList();
                GetTrade();
                LoopForSession();
                DataTable tempforgrid = (DataTable)Session["sessionforgrid"]; // Work Execution grid with Session
                DataTable dt = ((DataTable)ViewState["WorkExeWithIssue"]).Copy(); // Work Execution Issue Standard Analysis
                DataView dv = dt.DefaultView;
                DataTable dtlabour = ((DataTable)ViewState["WorkExeWithLabour"]).Copy();
                DataView dvlabour = dtlabour.DefaultView;

                DataTable dtMatList = (DataTable)ViewState["itemlistMaterialsBalance"];
                DataTable dtLabourList = (DataTable)ViewState["itemlistLabourBalance"];


                DataTable dt1 = (DataTable)ViewState["materialexefinal"];

                DataTable dtlabour1 = (DataTable)ViewState["labourexefinal"];

                string strColName = "Ratio";
                string strColName1 = "isuqty";
                string strColName2 = "spcfcod";
                string strColName3 = "rsirunit";
                string strColName4 = "balqty";
                string strColName5 = "useoflocation";
                string strColName6 = "remarks";
                string strColName7 = "WrkQty";
                string strColName8 = "StdQty";
                string strColName9 = "WrkUnit";
                DataColumn colNew = new DataColumn(strColName, typeof(double));
                DataColumn colNew1 = new DataColumn(strColName1, typeof(double));
                DataColumn colNew2 = new DataColumn(strColName2, typeof(string));
                DataColumn colNew3 = new DataColumn(strColName3, typeof(string));
                DataColumn colNew4 = new DataColumn(strColName4, typeof(double));
                DataColumn colNew5 = new DataColumn(strColName5, typeof(string));
                DataColumn colNew6 = new DataColumn(strColName6, typeof(string));
                DataColumn colNew7 = new DataColumn(strColName7, typeof(double));
                DataColumn colNew8 = new DataColumn(strColName8, typeof(double));
                DataColumn colNew9 = new DataColumn(strColName9, typeof(string));
                colNew.DefaultValue = 0.00;
                colNew1.DefaultValue = 0.00;
                colNew2.DefaultValue = "000000000000";

                if (!dt.Columns.Contains(strColName))
                    dt.Columns.Add(colNew);
                if (!dt.Columns.Contains(strColName1))
                    dt.Columns.Add(colNew1);
                if (!dt.Columns.Contains(strColName2))
                    dt.Columns.Add(colNew2);
                if (!dt.Columns.Contains(strColName3))
                    dt.Columns.Add(colNew3);
                if (!dt.Columns.Contains(strColName4))
                    dt.Columns.Add(colNew4);
                if (!dt.Columns.Contains(strColName5))
                    dt.Columns.Add(colNew5);
                if (!dt.Columns.Contains(strColName6))
                    dt.Columns.Add(colNew6);
                if (!dt.Columns.Contains(strColName7))
                    dt.Columns.Add(colNew7);
                if (!dt.Columns.Contains(strColName8))
                    dt.Columns.Add(colNew8);
                if (!dt.Columns.Contains(strColName9))
                    dt.Columns.Add(colNew9);


                string LstrColName = "Ratio";
                string LstrColName1 = "isuqty";
                string LstrColName2 = "spcfcod";
                string LstrColName3 = "rsirunit";
                string LstrColName4 = "balqty";
                string LstrColName5 = "isurat";
                string LstrColName6 = "isuamt";
                string LstrColName7 = "WrkQty";
                string LstrColName8 = "StdQty";
                string LstrColName9 = "WrkUnit";
                DataColumn LcolNew = new DataColumn(LstrColName, typeof(double));
                DataColumn LcolNew1 = new DataColumn(LstrColName1, typeof(double));
                DataColumn LcolNew2 = new DataColumn(LstrColName2, typeof(string));
                DataColumn LcolNew3 = new DataColumn(LstrColName3, typeof(string));
                DataColumn LcolNew4 = new DataColumn(LstrColName4, typeof(double));
                DataColumn LcolNew5 = new DataColumn(LstrColName5, typeof(double));
                DataColumn LcolNew6 = new DataColumn(LstrColName6, typeof(double));
                DataColumn LcolNew7 = new DataColumn(LstrColName7, typeof(double));
                DataColumn LcolNew8 = new DataColumn(LstrColName8, typeof(double));
                DataColumn LcolNew9 = new DataColumn(LstrColName9, typeof(string));
                LcolNew.DefaultValue = 0.00;
                LcolNew1.DefaultValue = 0.00;
                LcolNew2.DefaultValue = "000000000000";

                if (!dtlabour.Columns.Contains(LstrColName))
                    dtlabour.Columns.Add(LcolNew);
                if (!dtlabour.Columns.Contains(LstrColName1))
                    dtlabour.Columns.Add(LcolNew1);
                if (!dtlabour.Columns.Contains(LstrColName2))
                    dtlabour.Columns.Add(LcolNew2);
                if (!dtlabour.Columns.Contains(LstrColName3))
                    dtlabour.Columns.Add(LcolNew3);
                if (!dtlabour.Columns.Contains(LstrColName4))
                    dtlabour.Columns.Add(LcolNew4);
                if (!dtlabour.Columns.Contains(LstrColName5))
                    dtlabour.Columns.Add(LcolNew5);
                if (!dtlabour.Columns.Contains(LstrColName6))
                    dtlabour.Columns.Add(LcolNew6);
                if (!dtlabour.Columns.Contains(LstrColName7))
                    dtlabour.Columns.Add(LcolNew7);
                if (!dtlabour.Columns.Contains(LstrColName8))
                    dtlabour.Columns.Add(LcolNew8);
                if (!dtlabour.Columns.Contains(LstrColName9))
                    dtlabour.Columns.Add(LcolNew9);

                string flag = "1";
                if (tempforgrid.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Nothing was selected." + "');", true);

                }
                else
                {
                    for (int i = 0; i < tempforgrid.Rows.Count; i++)
                    {

                        string isircode = tempforgrid.Rows[i]["itemcode"].ToString();
                        string flrcode = tempforgrid.Rows[i]["flrcod"].ToString();
                        double wrkqty = Convert.ToDouble(tempforgrid.Rows[i]["wrkqty"].ToString() == "" ? "0.00" : tempforgrid.Rows[i]["wrkqty"].ToString());
                        double stdqty = Convert.ToDouble(tempforgrid.Rows[i]["stdqty"].ToString() == "" ? "0.00" : tempforgrid.Rows[i]["stdqty"].ToString());
                        string wrkunit = tempforgrid.Rows[i]["wrkunit"].ToString();

                        if (wrkqty <= 0)
                        {
                            dt1.Rows.Clear();
                            flag = "0";
                        }
                        else
                        {
                            dv.RowFilter = ("isircode='" + isircode + "'  and flrcod='" + flrcode + "'");
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string rsircode = dr["rsircode"].ToString();
                                string specification = dr["spcfcod"].ToString();

                                if (((dtMatList).Select("rsircode='" + rsircode + "'")).Length > 0)
                                {
                                    dr["WrkUnit"] = wrkunit;
                                    dr["StdQty"] = stdqty;
                                    dr["WrkQty"] = wrkqty;
                                    dr["Ratio"] = stdqty == 0 ? 0.00 : wrkqty / stdqty;
                                    dr["isuqty"] = Convert.ToDouble(dr["RSTDQTY"].ToString() ?? "0.00") * (wrkqty / 100);
                                    dr["rsirunit"] = ((dtMatList).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                                    dr["balqty"] = (((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble(((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["bbgdqty"]).ToString();
                                    dr["useoflocation"] = "";
                                    dr["remarks"] = "";
                                }
                            }

                            if (dt.Select("rsirunit<>''").Length > 0)
                            {
                                dt1.Merge(dt.Select("rsirunit<>''").CopyToDataTable());
                            }


                            dvlabour.RowFilter = ("isircode='" + isircode + "'  and flrcod='" + flrcode + "'");

                            dtlabour = dvlabour.ToTable();
                            foreach (DataRow dr in dtlabour.Rows)
                            {
                                string rsircode = dr["rsircode"].ToString();

                                if (((dtLabourList).Select("rsircode='" + rsircode + "' and flrcod='" + flrcode + "'")).Length > 0)
                                {
                                    dr["WrkUnit"] = wrkunit;
                                    dr["StdQty"] = stdqty;
                                    dr["WrkQty"] = wrkqty;
                                    dr["Ratio"] = stdqty == 0 ? 0.00 : wrkqty / stdqty;
                                    dr["isuqty"] = Convert.ToDouble(dr["RSTDQTY"].ToString() ?? "0.00") * (wrkqty / 100);
                                    dr["rsirunit"] = ((dtLabourList).Select("rsircode='" + rsircode + "' and flrcod='" + flrcode + "'"))[0]["rsirunit"];
                                    dr["balqty"] = (((dtLabourList).Select("rsircode='" + rsircode + "' and flrcod='" + flrcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble(((dtLabourList).Select("rsircode='" + rsircode + "' and flrcod='" + flrcode + "'"))[0]["balqty"]).ToString();
                                    dr["isurat"] = Convert.ToDouble(dtLabourList.Select("rsircode='" + rsircode + "' and flrcod='" + flrcode + "'")[0]["isurat"]);
                                    dr["isuamt"] = Convert.ToDouble(dr["isurat"]) * Convert.ToDouble(dr["isuqty"]);
                                }
                            }
                            if (dtlabour.Select("rsirunit<>''").Length > 0)
                            {
                                dtlabour1.Merge(dtlabour.Select("rsirunit<>''").CopyToDataTable());
                            }

                        }
                    }
                    if (flag != "0")
                    {
                        ViewState["materialexefinal"] = dt1;
                        ViewState["labourexefinal"] = dtlabour1;
                        GridTwo_DataBind();
                        GridThree_DataBind();
                        pnl2.Visible = true;
                        pnl1.Visible = false;
                        pnlMat.Visible = true;
                        pnlLab.Visible = true;
                        if (dt1.Rows.Count == 0)
                        {
                            pnlMat.Visible = false;
                        }
                        if (dtlabour1.Rows.Count == 0)
                        {
                            pnlLab.Visible = false;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Quantity must be provided" + "');", true);

                    }
                }
            }



        }

        private void GridThree_DataBind()
        {
            try
            {

                DataTable dt1 = (DataTable)ViewState["labourexefinal"];
                DataView dv = new DataView(dt1);
                dv.Sort = "isircode ASC,flrcod ASC, rsircode ASC";
                DataGridThree.DataSource = HiddenTableTwo(dv.ToTable());
                DataGridThree.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void GridTwo_DataBind()
        {
            try
            {

                DataTable dt1 = (DataTable)ViewState["materialexefinal"];
                DataView dv = new DataView(dt1);
                dv.Sort = "isircode ASC,flrcod ASC, rsircode ASC";
                DataGridTwo.DataSource = HiddenTableTwo(dv.ToTable());
                DataGridTwo.DataBind();
            }
            catch (Exception ex)
            {

            }

        }


        private DataTable HiddenTableTwo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            string flrcod = dt.Rows[0]["flrcod"].ToString();
            string Itemcode = dt.Rows[0]["isircode"].ToString();
            //((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'")).Length == 0) ? "0.00" :
            //    Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bbgdqty"]).ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i]["flrcod"].ToString() == flrcod) && (dt.Rows[i]["isircode"].ToString() == Itemcode))
                {

                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["isircode"].ToString();
                    dt.Rows[i]["flrdesc"] = "";
                    dt.Rows[i]["isirdesc"] = "";
                    dt.Rows[i]["WrkQty"] = 0.00;
                }
                else
                {
                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["isircode"].ToString();
                }
            }
            return dt;
        }

        protected void DataGridTwo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtspec = (DataTable)ViewState["specification"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSpec = (DropDownList)e.Row.FindControl("ddlSpecification");
                LinkButton lnk = (LinkButton)e.Row.FindControl("LnkbtnSpec");

                string rsircode = ((Label)e.Row.FindControl("lblrsircode")).Text;
                DataView dv = dtspec.DefaultView;
                dv.RowFilter = ("rsircode='" + rsircode + "'");
                ddlSpec.DataSource = dv.ToTable();
                ddlSpec.DataTextField = "spcfdesc";
                ddlSpec.DataValueField = "spcfcod";
                ddlSpec.DataBind();
                if (dv.ToTable().Rows.Count > 1)
                {
                    lnk.Enabled = true;
                }
                else
                {
                    lnk.Enabled = false;
                }
                ddlSpec.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod"));
            }
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridTwoLoopForSession();
            DropDownList dropDownList = sender as DropDownList;
            GridViewRow gridrow = (GridViewRow)dropDownList.NamingContainer;
            int row = gridrow.RowIndex;
            string specification = dropDownList.SelectedValue.ToString();

            DataTable dtMatList = (DataTable)ViewState["itemlistMaterialsBalance"];
            DataTable dt = (DataTable)ViewState["materialexefinal"];
            string rsircode = dt.Rows[row]["rsircode"].ToString();
            dt.Rows[row]["balqty"] = (((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble(((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["bbgdqty"]).ToString();
            dt.Rows[row]["spcfcod"] = specification;
            ViewState["materialexefinal"] = dt;
            GridTwo_DataBind();
            //dropDownList.SelectedValue = specification;
        }

        private void GridTwoLoopForSession()
        {
            DataTable dt = (DataTable)ViewState["materialexefinal"];
            int TblRowIndex;
            for (int i = 0; i < this.DataGridTwo.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtAnaQty")).Text.Trim());
                string txtuol = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtuol")).Text.Trim().ToString();
                string txtremarks = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtremarks")).Text.Trim().ToString();
                TblRowIndex = (DataGridTwo.PageIndex) * DataGridTwo.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);
                if (balqty < txtwrkqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
                dt.Rows[TblRowIndex]["useoflocation"] = txtuol;
                dt.Rows[TblRowIndex]["remarks"] = txtremarks;
            }
            DataView dv = new DataView(dt);
            dv.Sort = "isircode ASC,flrcod ASC, rsircode ASC";
            ViewState["materialexefinal"] = dv.ToTable();
        }

        private void GridThreeLoopForSession()
        {
            DataTable dt = (DataTable)ViewState["labourexefinal"];
            int TblRowIndex;
            for (int i = 0; i < this.DataGridThree.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridThree.Rows[i].FindControl("txtAnaQty")).Text.Trim());
                double txtwrkrate = Convert.ToDouble("0" + ((TextBox)this.DataGridThree.Rows[i].FindControl("txtAnaRate")).Text.Trim());
                ((TextBox)this.DataGridThree.Rows[i].FindControl("txtAnaAmount")).Text = (txtwrkqty * txtwrkrate).ToString();
                string txtuol = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtuol")).Text.Trim().ToString();
                string txtremarks = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtremarks")).Text.Trim().ToString();
                TblRowIndex = (DataGridThree.PageIndex) * DataGridThree.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);
                if (balqty < txtwrkqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
                dt.Rows[TblRowIndex]["isurat"] = txtwrkrate;
                dt.Rows[TblRowIndex]["isuamt"] = txtwrkqty * txtwrkrate;
            }
            DataView dv = new DataView(dt);
            dv.Sort = "isircode ASC,flrcod ASC, rsircode ASC";
            ViewState["labourexefinal"] = dv.ToTable();
        }

        protected void GetPerMatIssu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mREQNO = "NEWMISS";
            string mREQDAT = Convert.ToDateTime(txtEntryDate.Text.Trim()).ToString();
            if (mREQNO == "NEWMISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTMISSUEINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    this.txtMINoPartOne.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtMINoPartTwo.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                }
            }
        }
        private void Get_Issue_Info()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mISSNo = "NEWMISS";
            DataSet ds1 = new DataSet();

            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURMISSUEINFO", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["UserLog"] = ds1.Tables[1];
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnl1.Visible = true;
            pnl2.Visible = false;
        }

        private bool UpdateLabourData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            this.GridThreeLoopForSession();
            DataTable tbl2 = (DataTable)ViewState["labourexefinal"];
            if (tbl2.Rows.Count > 0)
            {
                string comcod = this.GetComCode();
                string mISUNO = this.txtCurNo1.Text.Trim().Substring(0, 3) + this.txtEntryDate.Text.Trim().Substring(7, 4) + this.txtCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
                string Refno = this.txtRefno.Text.Trim();

                switch (comcod)
                {

                    case "3335":
                    case "3336":
                    case "3337":
                        //case "3101":
                        string pactcode = this.ddlProject.SelectedValue.ToString();
                        string csircode = this.ddlSubContractor.SelectedValue.ToString();
                        DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDURANO", Refno, pactcode, csircode, "", "", "", "", "", "");
                        if (ds2.Tables[0].Rows.Count == 0)
                        {

                        }
                        else
                        {

                            DataView dv1 = ds2.Tables[0].DefaultView;
                            dv1.RowFilter = ("lisuno <>'" + mISUNO + "'");
                            DataTable dt = dv1.ToTable();
                            if (dt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate R/A" + "');", true);
                                return false;
                            }
                        }
                        break;
                    default:
                        break;

                }



                string percentage = Convert.ToDouble("0").ToString();
                string sdamt = Convert.ToDouble("0").ToString();
                string dedamt = Convert.ToDouble("0").ToString();
                string Penalty = Convert.ToDouble("0").ToString();
                string advamt = Convert.ToDouble("0").ToString();
                string Reward = Convert.ToDouble("0").ToString();

                string mISUDAT = this.txtEntryDate.Text.Trim();
                string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
                string mCONCODE = this.ddlSubContractor.SelectedValue.ToString().Trim();
                string mISURNAR = this.txtNarration.Text.Trim();

                string trade = "";
                string rano = this.ddlRA.SelectedValue.ToString();

                bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEB",
                     mISUNO, mISUDAT, mPACTCODE, mCONCODE, mISURNAR, Refno, usrid, Sessionid, trmid, trade, rano, percentage, sdamt, dedamt, Penalty, advamt, Reward, "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return false;
                }

                foreach (DataRow dr in tbl2.Rows)
                {
                    string Flrcod = dr["flrcod"].ToString();
                    string grp = "001";//dr["grp"].ToString()
                    string Rsircode = dr["rsircode"].ToString();
                    string prcent = Convert.ToDouble("0").ToString();
                    double Isuqty = Convert.ToDouble(dr["isuqty"].ToString().Trim());
                    double Isuamt = Convert.ToDouble(dr["isuamt"].ToString().Trim());  //dr["isuamt"].ToString().Trim();
                    string wrkqty = dr["wrkqty"].ToString().Trim();
                    double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());
                    string mbbook = "";
                    string above = "0.00";
                    string dedqty = "0.00";
                    string dedunit = "";
                    string idedamt = "0.00";
                    double balamt = Convert.ToDouble("0.00");

                    if (comcod == "3336" || comcod == "3337" || comcod == "3335" || comcod == "3340" || comcod == "1103" || comcod == "3344")
                    {


                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                            Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                            return false;
                        }



                    }


                    else if (comcod == "3339")
                    {
                        if (balamt >= Isuamt)
                        {

                            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                            Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                            if (!result)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                                return false;
                            }


                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Greater than balance amount" + "');", true);
                            return false;
                        }


                    }


                    else
                    {


                        if (balqty >= Isuqty)
                        {

                            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                                Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                            if (!result)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                                return false;
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Balance" + "');", true);
                            return false;
                        }



                    }

                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Labour Issue Information";
                    string eventdesc = "Update Labour QTY & RATE";
                    string eventdesc2 = "Issue No: " + this.txtCurNo1.Text.Trim().Substring(0, 3) +
                            ASTUtility.Right((this.txtEntryDate.Text.Trim()), 4) + this.txtCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();

                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                return true;
            }

            else
            {
                return true;
            }


        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            LoopForSession();

            DataTable tbl2 = (DataTable)Session["sessionforgrid"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string mISUNO = this.txtWINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                  + this.txtWINoPartOne.Text.Trim().Substring(3, 2) + this.txtWINoPartTwo.Text.Trim();
            string mISUDAT = txtEntryDate.Text;
            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mISUUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = txtEntryDate.Text;  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mISUBYDES = "";
            string mAPPBYDES = "";
            string mISUREF = "";
            string mISURNAR = this.txtNarration.Text.Trim();
            string mBILLNO = txtWRefNo.Text;
            string mMISUNO = this.txtMINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                  + this.txtMINoPartOne.Text.Trim().Substring(3, 2) + this.txtMINoPartTwo.Text.Trim();
            string mLISUNO = this.txtCurNo1.Text.Trim().Substring(0, 3) + this.txtEntryDate.Text.Trim().Substring(7, 4) + this.txtCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();

            //------Material issue

            DataTable dtuser = (DataTable)ViewState["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            GridTwoLoopForSession();
            DataTable tbl02 = (DataTable)ViewState["materialexefinal"];
            // Duplicate 
            string mRef = this.txtSMCR.Text;
            string mSmcr = this.txtDMIRF.Text;

            GridThreeLoopForSession();
            DataTable tbl03 = (DataTable)ViewState["labourexefinal"];



            string dmirfno = this.txtDMIRF.Text;
            if (this.Request.QueryString["type"] == "Entry")
            {

                switch (comcod)
                {
                    case "3301":
                    case "1301":
                        //case "3101":
                        break;

                    default:

                        dr1 = tbl02.Select("balqty<isuqty");

                        if (dr1.Length > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Balance" + "');", true);
                            return;
                        }
                        break;
                }
            }
            switch (comcod)
            {
                case "3315": // assure 
                case "3316": // assure
                case "3317": // assure
                case "3367": // epic 
                             //case "3101": // epic 

                    break;

                default:

                    if (mRef.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "SMCR No Should Not Be Empty" + "');", true);
                        return;

                    }
                    else if (dmirfno.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "DMIRF No Should Not Be Empty" + "');", true);

                        return;
                    }

                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPISUMRFNO", mRef, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                    }

                    else
                    {
                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("isuno <>'" + mMISUNO + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                        { }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate SMCR.No" + "');", true);
                            return;
                        }
                    }
                    break;
            }

            //--------------------------------------Update LISUNO---------------------------
            bool result = UpdateLabourData();

            //--------------------------------------Update Pur Material Issue-------------------------

            if (result)
            {

                if (tbl02.Rows.Count > 0)
                {
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEB",
                            mMISUNO, mISUDAT, mPACTCODE, mISURNAR, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, mSmcr, "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    for (int i = 0; i < tbl02.Rows.Count; i++)
                    {
                        string Rsircode = tbl02.Rows[i]["rsircode"].ToString();
                        string Spcfcod = tbl02.Rows[i]["spcfcod"].ToString();
                        double Isuqty = Convert.ToDouble(tbl02.Rows[i]["isuqty"].ToString());
                        string txtlocation = tbl02.Rows[i]["useoflocation"].ToString();
                        string txtremarks = tbl02.Rows[i]["remarks"].ToString();
                        string flrcod = tbl02.Rows[i]["flrcod"].ToString();
                        if (Isuqty > 0)
                        {

                            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEA", mMISUNO,
                                Rsircode, Spcfcod, Isuqty.ToString(), txtlocation, txtremarks, flrcod, "", "", "", "", "", "", "");
                            if (!result)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                                return;
                            }
                        }
                    }

                    string CurDate1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    DataSet dsx = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURMISSUEINFO", mMISUNO, "", "", "", "", "", "", "", "");
                    if (dsx == null)
                        return;
                    this.XmlDataInsert(mMISUNO, dsx);



                    //---------------------------------------End of Pur Material Issue--------------------------


                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURISSUEINFO", "PURISSUEB",
                                     mISUNO, mISUDAT, mPACTCODE, "", mISUUSRID, mAPPRUSRID, mAPPRDAT, mISUBYDES, mAPPBYDES, mISUREF, mISURNAR, mBILLNO, usrid, sessionid, trmid, mMISUNO, mLISUNO, "", "", "");
                    if (!result)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }



                    for (int i = 0; i < tbl2.Rows.Count; i++)
                    {
                        string fisircode = tbl2.Rows[i]["itemcode"].ToString();
                        double wrkqty = Convert.ToDouble(tbl2.Rows[i]["wrkqty"].ToString());
                        string floorcode = tbl2.Rows[i]["flrcod"].ToString();
                        if (wrkqty > 0)
                        {
                            result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURISSUEINFO", "PURISSUEA", mISUNO, floorcode, fisircode, wrkqty.ToString(),
                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!result)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                                return;
                            }
                        }
                    }



                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Update Successfully" + "');", true);
                    InitPage();
                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Materials Issue Information";
                        string eventdesc = "Update New QTY";
                        string eventdesc2 = "Bill No : " + this.txtWRefNo.Text.Trim() + " - " + mISUNO;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
                    if (ConstantInfo.LogStatus == true)
                    {
                        string eventtype = "Materials Issue Information";
                        string eventdesc = "Update Issue QTY";
                        string eventdesc2 = "Issue No: " + mMISUNO;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    }
                }

            }
        }
        private bool XmlDataInsert(string Reqno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("postedbyid", typeof(string));
            dt1.Columns.Add("postedseson", typeof(string));
            dt1.Columns.Add("postedtrmnid", typeof(string));
            dt1.Columns.Add("posteddate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["postedbyid"] = usrid;
            dr1["postedseson"] = session;
            dr1["postedtrmnid"] = trmnid;
            dr1["posteddate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }

        protected void DataGridOne_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            string mISUNO = this.txtWINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                  + this.txtWINoPartOne.Text.Trim().Substring(3, 2) + this.txtWINoPartTwo.Text.Trim();
            string Itemcode = ((Label)this.DataGridOne.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string Flrcode = ((Label)this.DataGridOne.Rows[e.RowIndex].FindControl("lblgvflrCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEITEMEISUE", mISUNO,
                         Itemcode, Flrcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.DataGridOne.PageSize) * (this.DataGridOne.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("sessionforgrid");
            Session["sessionforgrid"] = dv.ToTable();
            this.GridOne_DataBind();
        }

        protected void LnkbtnSpec_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            DataTable dt = (DataTable)ViewState["materialexefinal"];
            string isircode = dt.Rows[RowIndex]["isircode"].ToString();
            string isirdesc = dt.Rows[RowIndex]["isirdesc"].ToString();
            string rsircode = dt.Rows[RowIndex]["rsircode"].ToString();
            string rsirdesc = dt.Rows[RowIndex]["rsirdesc"].ToString();
            string flrcode = dt.Rows[RowIndex]["flrcod"].ToString();
            string flrdesc = dt.Rows[RowIndex]["flrdesc"].ToString();
            double RSTDQTY = Convert.ToDouble(dt.Rows[RowIndex]["rstdqty"].ToString());
            double Ratio = Convert.ToDouble(dt.Rows[RowIndex]["Ratio"].ToString());
            double isuqty = Convert.ToDouble("0.00");
            string spcfcod = dt.Rows[RowIndex]["spcfcod"].ToString();
            string rsirunit = dt.Rows[RowIndex]["rsirunit"].ToString();
            double balqty = Convert.ToDouble(dt.Rows[RowIndex]["balqty"].ToString());
            string useoflocation = dt.Rows[RowIndex]["useoflocation"].ToString();
            string remarks = dt.Rows[RowIndex]["remarks"].ToString();
            double WrkQty = Convert.ToDouble(dt.Rows[RowIndex]["WrkQty"].ToString());
            double StdQty = Convert.ToDouble(dt.Rows[RowIndex]["StdQty"].ToString());
            string WrkUnit = dt.Rows[RowIndex]["WrkUnit"].ToString();


            DataRow row = dt.NewRow();
            row["comcod"] = GetComCode();
            row["isircode"] = isircode;
            row["isirdesc"] = isirdesc;
            row["rsircode"] = rsircode;
            row["rsirdesc"] = rsirdesc;
            row["flrcod"] = flrcode;
            row["flrdesc"] = flrdesc;
            row["rstdqty"] = RSTDQTY;
            row["isuqty"] = isuqty;
            row["Ratio"] = Ratio;
            row["spcfcod"] = spcfcod;
            row["rsirunit"] = rsirunit;
            row["balqty"] = balqty;
            row["useoflocation"] = useoflocation;
            row["remarks"] = remarks;
            row["WrkQty"] = WrkQty;
            row["StdQty"] = StdQty;
            row["WrkUnit"] = WrkUnit;
            dt.Rows.Add(row);

            //DataView dv = new DataView(dt);
            //dv.Sort = "isircode ASC,flrcod ASC, rsircode ASC";
            ViewState["materialexefinal"] = dt;
            GridTwo_DataBind();
            GridTwoLoopForSession();
        }

        private void GetConList()
        {
            string comcod = this.GetComCode();
            //string conlist = "%" + this.txtSrcSub.Text + "%";
            string conlist = "%%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubContractor.DataTextField = "sircode1";
            this.ddlSubContractor.DataValueField = "sircode";
            this.ddlSubContractor.DataSource = ds1.Tables[0];
            this.ddlSubContractor.DataBind();


            //this.ddlgroup.DataTextField = "grpdesc";
            //this.ddlgroup.DataValueField = "grp";
            //this.ddlgroup.DataSource = ds1.Tables[1];
            //this.ddlgroup.DataBind();
        }
        private void GetTrade()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETTRADENAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[1];
            DataView dv;

            switch (comcod)
            {
                case "3335":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("racode<>''");
                    break;
                default:
                    dv = dt.DefaultView;
                    break;
            }
            this.ddlRA.DataTextField = "radesc";
            this.ddlRA.DataValueField = "racode";
            this.ddlRA.DataSource = dv.ToTable();
            this.ddlRA.DataBind();
            ds1.Dispose();
            this.ddlRA_SelectedIndexChanged(null, null);
        }
        protected void lbtnDepost_Click(object sender, EventArgs e)
        {

        }
        protected void ddlRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRefno.Text = this.ddlRA.SelectedItem.Text.Trim();
        }




        private void Get_IssueLabour_Info()
        {

            string comcod = this.GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string mISSNo = "NEWLISS";
            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = this.HiddenSameData(ds1.Tables[0]);

            if (mISSNo == "NEWLISS")
            {

                string Prefix = (this.Request.QueryString["Type"] == "Opening") ? "OPB" : "LIS";

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTLABISSUENO", CurDate1,
                       Prefix, "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.txtCurNo1.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                }
                return;
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";


                grp = dt1.Rows[j]["grp"].ToString();


            }

            return dt1;
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            GridThreeLoopForSession();
            //GridThree_DataBind();
            FooterCalculaton();
        }
        private void FooterCalculaton()
        {
            DataTable dt = (DataTable)ViewState["labourexefinal"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.DataGridThree.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(isuamt)", "")) ? 0.00 : dt.Compute("Sum(isuamt)", ""))).ToString("#,##0.0000;(#,##0.00); ");

        }
    }
}