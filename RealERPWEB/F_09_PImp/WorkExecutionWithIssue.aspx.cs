﻿using RealERPLIB;
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
        private bool isBudget = false;
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

                GetCategory();
                GetItem();
                GetDivision();
                GetWENIssueNo();
                GridOne_DataBind();
                pnl1.Visible = true;
                pnl2.Visible = false;
                btnOK.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
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
            if (ddlCategory.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Category was not found for this Project" + "');", true);
                return;
            }
            else
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
        }
        public void GetDivision()
        {
            if (ddlItem.SelectedValue == "")
            {

            }
            else
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
                    isBudget = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
                    return;
                }
                else
                {
                    isBudget = true;
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
        private void GetSpecification()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(txtEntryDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETSPECIFICATION", pactcode, date, "", "", "", "", "", "", "");
            ViewState["specification"] = ds1.Tables[0];
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("comcod", Type.GetType("System.String"));
            tblt01.Columns.Add("isircode", Type.GetType("System.String"));
            tblt01.Columns.Add("isirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsircode", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rsirunit", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
            tblt01.Columns.Add("flrdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("wrkunit", Type.GetType("System.String"));
            tblt01.Columns.Add("rstdqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("stdqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("wrkqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("ratio", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("balqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("isuqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("spcfcod", Type.GetType("System.String"));

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
            tblt01.Columns.Add("rsirunit", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));
            tblt01.Columns.Add("flrdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("wrkunit", Type.GetType("System.String"));
            tblt01.Columns.Add("rstdqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("stdqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("wrkqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("ratio", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("balqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("isuqty", Type.GetType("System.Decimal"));
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
                string comcod = GetComCode();
                CreateTable();
                CreateTableLabour();
                GetSpecification();
                LoopForSession();

                DataTable tempforgrid = (DataTable)Session["sessionforgrid"]; // Work Execution grid with Session

                DataTable dt1 = (DataTable)ViewState["materialexefinal"];
                DataTable dtlabour1 = (DataTable)ViewState["labourexefinal"];
                string flag = "1";
                if (tempforgrid.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Nothing was selected." + "');", true);

                }
                else
                {
                    for (int i = 0; i < tempforgrid.Rows.Count; i++)
                    {
                        string pactcode = ddlProject.SelectedValue.ToString();
                        string isircode = tempforgrid.Rows[i]["itemcode"].ToString();
                        string flrcode = tempforgrid.Rows[i]["flrcod"].ToString();
                        double wrkqty = Convert.ToDouble(tempforgrid.Rows[i]["wrkqty"].ToString() == "" ? "0.00" : tempforgrid.Rows[i]["wrkqty"].ToString());
                        double stdqty = Convert.ToDouble(tempforgrid.Rows[i]["stdqty"].ToString() == "" ? "0.00" : tempforgrid.Rows[i]["stdqty"].ToString());
                        string wrkunit = tempforgrid.Rows[i]["wrkunit"].ToString();
                        string EntryDate = this.txtEntryDate.Text;
                        if (wrkqty <= 0)
                        {
                            dt1.Rows.Clear();
                            flag = "0";
                        }
                        else
                        {

                            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEINFOFROMWORKDETAILS", isircode, flrcode, wrkqty.ToString(), pactcode, EntryDate, "", "", "", "");

                            dt1.Merge(ds1.Tables[0]);

                            dtlabour1.Merge(ds1.Tables[1]);
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
                ViewState["labourexefinal"]= dv.ToTable();
                DataGridThree.DataSource = HiddenTableTwo(dv.ToTable());
                DataGridThree.DataBind();
                //FooterCalculaton();
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
                ViewState["materialexefinal"] = dv.ToTable();
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
                    //lnk.Visible = false;
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

            DataTable dtMatList = (DataTable)ViewState["specification"];
            DataTable dt = (DataTable)ViewState["materialexefinal"];
            string rsircode = dt.Rows[row]["rsircode"].ToString();
            dt.Rows[row]["balqty"] = (((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble(((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["balqty"]).ToString();
            dt.Rows[row]["spcfcod"] = specification;
            ViewState["materialexefinal"] = dt;
            GridTwo_DataBind();
            //dropDownList.SelectedValue = specification;
        }

        private void GridTwoLoopForSession()
        {
            DataTable dt = (DataTable)ViewState["materialexefinal"];
            int TblRowIndex;
            isBudget = true;
            for (int i = 0; i < this.DataGridTwo.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtAnaQty")).Text.Trim());
                string txtuol = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtuol")).Text.Trim().ToString();
                string txtremarks = ((TextBox)this.DataGridTwo.Rows[i].FindControl("txtremarks")).Text.Trim().ToString();
                TblRowIndex = (DataGridTwo.PageIndex) * DataGridTwo.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);
                if (balqty < txtwrkqty)
                {
                    isBudget = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
                    return;
                }
                else
                {
                    isBudget = true;
                }
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
                dt.Rows[TblRowIndex]["useoflocation"] = txtuol;
                dt.Rows[TblRowIndex]["remarks"] = txtremarks;
            }
            ViewState["materialexefinal"] = dt;
        }

        private void GridThreeLoopForSession()
        {
            DataTable dt = (DataTable)ViewState["labourexefinal"];
            int TblRowIndex;
            isBudget = true;
            for (int i = 0; i < this.DataGridThree.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridThree.Rows[i].FindControl("txtAnaQty")).Text.Trim());

                TblRowIndex = (DataGridThree.PageIndex) * DataGridThree.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);
                if (balqty < txtwrkqty)
                {
                    isBudget = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Budget" + "');", true);
                    return;
                }
                else
                {
                    isBudget = true;
                }
                dt.Rows[TblRowIndex]["isuqty"] = txtwrkqty;
            }
            ViewState["labourexefinal"] = dt;
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

        private bool UpdateLabourData(string mISUNO)
        {
            DataTable tbl2 = (DataTable)ViewState["labourexefinal"];
            string comcod = GetComCode();
            string pactcode = ddlProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(txtEntryDate.Text).ToString("dd-MMM-yyyy");
            List<bool> arrResult = new List<bool>();
            foreach (DataRow dr in tbl2.Rows)
            {
                string ISircode = dr["isircode"].ToString();
                string Flrcod = dr["flrcod"].ToString();
                string grp = "001";
                string Rsircode = dr["rsircode"].ToString();
                double LWorkqty = Convert.ToDouble(dr["wrkqty"].ToString().Trim());
                double Isuqty = Convert.ToDouble(dr["isuqty"].ToString().Trim());
                double Isuamt = Convert.ToDouble("0.00");
                double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLISSUEF", mISUNO, Flrcod, grp, Rsircode, date,
                    LWorkqty.ToString(), Isuqty.ToString(), Isuamt.ToString(), pactcode, ISircode);
                arrResult.Add(result);
            }
            if (arrResult.Contains(false))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEPURLISSUEF", mISUNO, pactcode);
                return false;
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
            List<bool> IsWithinBudget = new List<bool>();
            LoopForSession();
            IsWithinBudget.Add(isBudget);
            GridTwoLoopForSession();
            IsWithinBudget.Add(isBudget);
            GridThreeLoopForSession();
            IsWithinBudget.Add(isBudget);
            if (!IsWithinBudget.Contains(false))
            {

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

                GetPerMatIssu();
                string mMISUNO = this.txtMINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                      + this.txtMINoPartOne.Text.Trim().Substring(3, 2) + this.txtMINoPartTwo.Text.Trim();
                //string mLISUNO = this.txtCurNo1.Text.Trim().Substring(0, 3) + this.txtEntryDate.Text.Trim().Substring(7, 4) + this.txtCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();

                //------Material issue
                Get_Issue_Info();
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
                
                DataTable tbl02 = (DataTable)ViewState["materialexefinal"];
                // Duplicate 
                string mRef = this.txtSMCR.Text;
                string mSmcr = this.txtDMIRF.Text;

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
                GetIssueNo();
                string mWENISUNO = this.txtWINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                     + this.txtWINoPartOne.Text.Trim().Substring(3, 2) + this.txtWINoPartTwo.Text.Trim();
                //--------------------------------------Update LISUNO---------------------------
                bool result = UpdateLabourData(mWENISUNO);

                //--------------------------------------Update Pur Material Issue-------------------------

                if (result)
                {
                    if (tbl02.Rows.Count > 0)
                    {
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEB",
                                mMISUNO, mISUDAT, mPACTCODE, mISURNAR, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, mSmcr, mWENISUNO, "");

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
                                         mISUNO, mISUDAT, mPACTCODE, "", mISUUSRID, mAPPRUSRID, mAPPRDAT, mISUBYDES, mAPPBYDES, mISUREF, mISURNAR, mBILLNO, usrid, sessionid, trmid, "", "", "", "", "");
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
                    }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within Budget" + "');", true);

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


            ViewState["materialexefinal"] = dt;
            GridTwo_DataBind();
            GridTwoLoopForSession();
        }


    }
}