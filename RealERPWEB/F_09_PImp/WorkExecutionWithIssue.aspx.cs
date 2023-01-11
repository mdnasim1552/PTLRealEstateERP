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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue";
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue - EDIT MODE";
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
            GetProject();
            //Default Panel Visible false
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
                GetWENIssueNo();
                GetWorkWithIssue();
                GetMaterials();
            }
            else
            {
                //New Click
                ddlProject.Enabled = true;
                pnl1.Visible = false;
                btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
                //Remove All Session
                //Grid One and Grid Two Bind Null
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
            dt = dv.ToTable(true, "itemcode", "workitem");


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
        }

        private void GetMaterials()
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(txtEntryDate.Text).ToString("dd-MMM-yyyy");
            string SearchMat = "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMETERIALS", pactcode, date, SearchMat, "", "", "", "", "", "");
            ViewState["itemlistMaterialsBalance"] = ds1.Tables[0];
            ViewState["specification"] = ds1.Tables[2];
        }

        protected void btnGenerateIssue_Click(object sender, EventArgs e)
        {
            GetPerMatIssu();
            Get_Issue_Info();
            LoopForSession();
            DataTable tempforgrid = (DataTable)Session["sessionforgrid"]; // Work Execution grid with Session
            DataTable dt = ((DataTable)ViewState["WorkExeWithIssue"]).Copy(); // Work Execution Issue Standard Analysis
            DataTable dtMatList = (DataTable)ViewState["itemlistMaterialsBalance"];
            DataView dv = dt.DefaultView;
            DataTable dt1 = new DataTable();
            string strColName = "Ratio";
            string strColName1 = "isuqty";
            string strColName2 = "spcfcod";
            string strColName3 = "rsirunit";
            string strColName4 = "balqty";
            string strColName5 = "useoflocation";
            string strColName6 = "remarks";
            DataColumn colNew = new DataColumn(strColName, typeof(double));
            DataColumn colNew1 = new DataColumn(strColName1, typeof(double));
            DataColumn colNew2 = new DataColumn(strColName2, typeof(string));
            DataColumn colNew3 = new DataColumn(strColName3, typeof(string));
            DataColumn colNew4 = new DataColumn(strColName4, typeof(string));
            DataColumn colNew5 = new DataColumn(strColName5, typeof(string));
            DataColumn colNew6 = new DataColumn(strColName6, typeof(string));
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
                            dr["Ratio"] = wrkqty / 100;
                            dr["isuqty"] = Convert.ToDouble(dr["RSTDQTY"].ToString() ?? "0.00") * (wrkqty / 100);
                            dr["rsirunit"] = ((dtMatList).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                            dr["balqty"] = (((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'")).Length == 0) ? "0.00" : Convert.ToDouble(((dtMatList).Select("rsircode='" + rsircode + "' and spcfcod='" + specification + "'"))[0]["bbgdqty"]).ToString();
                            dr["useoflocation"] = "";
                            dr["remarks"] = "";
                        }
                        dt1.Merge(dt);
                    }
                }
                if (flag != "0")
                {
                    ViewState["materialexefinal"] = dt1;
                    GridTwo_DataBind();
                    pnl2.Visible = true;
                    pnl1.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Quantity must be provided" + "');", true);

                }
            }


        }
        private void GridTwo_DataBind()
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["materialexefinal"];
                DataGridTwo.DataSource = HiddenTableTwo(dt1);
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
                string rsircode = ((Label)e.Row.FindControl("lblrsircode")).Text;
                DataView dv = dtspec.DefaultView;
                dv.RowFilter = ("rsircode='" + rsircode + "'");
                ddlSpec.DataSource = dv.ToTable();
                ddlSpec.DataTextField = "spcfdesc";
                ddlSpec.DataValueField = "spcfcod";
                ddlSpec.DataBind();
                ddlSpec.SelectedValue= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod"));
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
                string txtuol =((TextBox)this.DataGridTwo.Rows[i].FindControl("txtuol")).Text.Trim().ToString();
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
            ViewState["materialexefinal"] = dt;
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
            string mBILLNO =txtWRefNo.Text;
            string mMISUNO = this.txtMINoPartOne.Text.Trim().Substring(0, 3) + Convert.ToDateTime(this.txtEntryDate.Text).ToString("yyyy")
                  + this.txtMINoPartOne.Text.Trim().Substring(3, 2) + this.txtMINoPartTwo.Text.Trim();
            string mLISUNO = "00000000000000";

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURISSUEINFO", "PURISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, "", mISUUSRID, mAPPRUSRID, mAPPRDAT, mISUBYDES, mAPPBYDES, mISUREF, mISURNAR, mBILLNO, usrid, sessionid, trmid, mMISUNO, mLISUNO, "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
           
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

            //DataRow[] dr = tbl2.Select("isuqty=0.00");

            //if (dr.Length > 0)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Qtuantity Field ";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}
            string mRef =  this.txtSMCR.Text;
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

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Balance";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }


                        break;



                }
            }
            
            //string mISUDAT = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString("dd-MMM-yyyy");

            // Duplicate 
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
                        ((Label)this.Master.FindControl("lblmsg")).Text = "SMCR No Should Not Be Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //this.ddlPrevISSList.Items.Clear();
                        return;
                    }
                    else if (dmirfno.Length == 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "DMIRF No Should Not Be Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //this.ddlPrevISSList.Items.Clear();
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Found Duplicate SMCR.No');", true);
                            //this.ddlPrevISSList.Items.Clear();
                            return;
                        }
                    }
                    break;
            }


            //string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            //string mISURNAR = this.txtISSNarr.Text.Trim();
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mISURNAR, mRef, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, mSmcr, "", "");
            //if (!result)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}
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

                if (Isuqty > 0)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "PURMISSUEA", mMISUNO,
                        Rsircode, Spcfcod, Isuqty.ToString(), txtlocation, txtremarks, "", "", "", "", "", "", "", "");
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

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Update Successfully" + "');", true);
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
    }
}