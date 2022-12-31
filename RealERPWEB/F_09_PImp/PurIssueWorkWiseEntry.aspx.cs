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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;

namespace RealERPWEB.F_09_PImp
{
    public partial class PurIssueWorkWiseEntry : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();
        DataTable tempforgrid = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "work execution - work wise";

                this.txtCurISSDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.Load_Project_Combo();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void GetISSuEntNo()
        {

            string comcod = this.GetCompCode();
            string mREQNO = "NEWISS";
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            if (mREQNO == "NEWISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISSUEINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                //ViewState["tblmatissue"] = ds2.Tables[0];
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxisuno"].ToString();
                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxisuno1";
                    this.ddlPrevISSList.DataValueField = "maxisuno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }
        }
        protected void Load_Project_Combo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST01", "%", "", userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)         // Ok Button
        {
            this.pnlgrd.Visible = true;
            if (this.lbtnOk.Text == "New")
            {

                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.ddlPrevISSList.Items.Clear();
                this.ddlprjlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.lblCurISSNo1.Text = "WEN" + "00" + "-";
                this.txtCurISSDate.Enabled = true;

                //this.ddlfloorno.Items.Clear();
                this.ddlitemlist.Items.Clear();
                //this.txtsrchItemName.Text = "";
                this.txtPreparedBy.Text = "000000";
                this.txtApprovedBy.Text = "";
                this.txtISSNarr.Text = "";
                this.txtBillno.Text = "";
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                this.pnlgrd.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;

            this.lblddlProject.Text = (this.ddlprjlist.Items.Count == 0 ? "XXX" : this.ddlprjlist.SelectedItem.Text.Trim());
            this.ddlprjlist.Visible = false;//it will be used
            this.lblddlProject.Visible = true;
            this.txtCurISSNo2.ReadOnly = true;
            ////this.ddlfloorno.Items.Clear();
            this.ddlitemlist.Items.Clear();
            this.pnlgrd.Visible = true;
            this.lbtnOk.Text = "New";
            // this.GetFloorCode();
            this.Get_Issue_Info();
            this.LoadCategory();


        }

        private void GetIssueNo()
        {
            string comcod = this.GetCompCode();
            string isudate = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISSUEINFO", isudate, "", "", "", "", "", "", "", "");
            this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
        }



        protected void Get_Issue_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            string mISSNo = "NEWISS";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mISSNo = this.ddlPrevISSList.SelectedValue.ToString();
            }


            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURISSUEINFO", mISSNo, CurDate1,
                              pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["sessionforgrid"] = ds1.Tables[0];

            if (mISSNo == "NEWISS")
            {
                this.GetIssueNo();

                return;

            }
            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["isuno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["isudat"]).ToString("dd.MM.yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = (this.ddlprjlist.Items.Count == 0 ? "XXX" : this.ddlprjlist.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["isubydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["isunar"].ToString();
            this.txtBillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();

            this.grvissue_DataBind();
        }


        private void LoadCategory()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = "";
            string date = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            string txtsrchItem = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");

            Session["itemlist"] = ds1.Tables[0];
            Session["item"] = ds1.Tables[1];
            if (ds1 == null)
                return;
            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "mitemcode";
            this.ddlcatagory.DataSource = ds1.Tables[2];
            this.ddlcatagory.DataBind();
            this.ddlcatagory_OnSelectedIndexChanged(null, null);
        }
        //private void GetFloorCode()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.ddlitemlist.DataTextField = "flrdes";
        //    this.ddlitemlist.DataValueField = "flrcod";
        //    this.ddlitemlist.DataSource = ds1.Tables[0];
        //    this.ddlitemlist.DataBind();

        //    //this.ddlfloorno.DataTextField = "flrdes";
        //    //this.ddlfloorno.DataValueField = "flrcod";
        //    //this.ddlfloorno.DataSource = ds1.Tables[0];
        //    //this.ddlfloorno.DataBind();
        //   // this.ddlfloorno_SelectedIndexChanged(null, null);

        //}
        //protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    this.GetItemList();
        //}

        //private void GetItemList()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string pactcode = this.ddlprjlist.SelectedValue.ToString();
        //    string flrcode = "";
        //    string date = this.GetStdDate(this.txtCurISSDate.Text.Trim());
        //    string txtsrchItem = this.txtsrchItemName.Text.Trim() + "%";
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");
        //    Session["itemlist"] = ds1.Tables[0];
        //    if (ds1 == null)
        //        return;
        //    //mitemcode
        //    this.ddlitemlist.DataTextField = "workitem";
        //    this.ddlitemlist.DataValueField = "itemcode";
        //    this.ddlitemlist.DataSource = ds1.Tables[1];
        //    this.ddlitemlist.DataBind();
        //    ddlitemlist_SelectedIndexChanged(null, null);
        //}
        //protected void imgbtnSearchItem_Click(object sender, EventArgs e)
        //{
        //    this.GetItemList();
        //}
        protected void lbtnAllLab_Click(object sender, EventArgs e)         // Selecet Data
        {
            this.LoopForSession();
            string floorcode = this.ddlitemlist.SelectedValue.ToString();
            string selecteditem = this.ddlitemlist.SelectedValue.ToString().Trim();
            DataTable itemtable = (DataTable)Session["itemlist"];
            DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            DataRow[] dr1 = tempforgrid.Select("flrcod='" + floorcode + "'  and itemcode='" + selecteditem + "'");
            if (dr1.Length == 0)
            {
                DataRow drforgrid = tempforgrid.NewRow();
                drforgrid["flrcod"] = this.ddlitemlist.SelectedValue.ToString();
                drforgrid["flrdes"] = this.ddlitemlist.SelectedItem.Text.Trim();
                drforgrid["wrkqty"] = 0;

                drforgrid["balqty"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + selecteditem + "'"))[0]["balqty"];
                drforgrid["wrkunit"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + selecteditem + "'"))[0]["wrkunit"];
                drforgrid["itemcode"] = this.ddlitemlist.SelectedValue.ToString();
                drforgrid["workitem"] = this.ddlitemlist.SelectedItem.ToString().Trim();
                tempforgrid.Rows.Add(drforgrid);

            }
            Session["sessionforgrid"] = tempforgrid;
            this.grvissue_DataBind();



        }


        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoopForSession();
            this.grvissue_DataBind();
        }
        protected void grvissue_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvissue.DataSource = tbl1;
            this.grvissue.DataBind();
        }
        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.LoopForSession();
            this.grvissue_DataBind();

        }

        protected void lnkupdate_Click(object sender, EventArgs e)              //UPdate Data
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.LoopForSession();
            DataTable tbl2 = (DataTable)Session["sessionforgrid"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetISSuEntNo();
            }
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(6, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mISUDAT = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mISUUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mISUBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mISUREF = "";
            string mISURNAR = this.txtISSNarr.Text.Trim();
            string mBILLNO = this.txtBillno.Text.Trim();

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURISSUEINFO", "PURISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, "", mISUUSRID, mAPPRUSRID, mAPPRDAT, mISUBYDES, mAPPBYDES, mISUREF, mISURNAR, mBILLNO, usrid, sessionid, trmid, "", "", "", "", "");
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

            this.txtCurISSDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Issue Information";
                string eventdesc = "Update New QTY";
                string eventdesc2 = "Bill No : " + this.txtBillno.Text.Trim() + " - " + mISUNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurISSDate.Text.Trim());
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string genno = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            // string pactcode = this.ddlprjlist.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVISSUELIST", CurDate1, genno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "isuno1";
            this.ddlPrevISSList.DataValueField = "isuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "wrkqty>0";
            // dt = dv1.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var lst = ASITUtility03.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.WorkExecution>((DataTable)dv1.ToTable());
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass1.GetLocalReport("R_09_PIMP.rptPurIssueEntry", lst, null, null);
            rpt1.SetParameters(new ReportParameter("txtCompName", comnam));
            rpt1.SetParameters(new ReportParameter("txtProjectName", "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14)));
            rpt1.SetParameters(new ReportParameter("txtIssueno", "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + this.txtCurISSDate.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("txtBillno", "Bill No : " + this.txtBillno.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("txtnarrationname", this.txtISSNarr.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Issue Information";
                string eventdesc = "Print Report";
                string eventdesc2 = "Bill No : " + this.txtBillno.Text.Trim(); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            dv1.RowFilter = "";




        }

        private void LoopForSession()
        {


            DataTable dt = (DataTable)Session["sessionforgrid"];
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {


                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtwrkqty")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
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



        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Itemcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string Flrcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvflrCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEITEMEISUE", mISUNO,
                         Itemcode, Flrcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("sessionforgrid");
            Session["sessionforgrid"] = dv.ToTable();
            this.grvissue_DataBind();
        }


        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            string flrcod = dt.Rows[0]["flrcod"].ToString();
            string Itemcode = dt.Rows[0]["itemcode"].ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {

                if ((dt.Rows[i]["flrcod"].ToString() == flrcod) && (dt.Rows[i]["itemcode"].ToString() == Itemcode))
                {

                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["itemcode"].ToString();

                    dt.Rows[i]["flrdes"] = "";
                    dt.Rows[i]["workitem"] = "";
                    dt.Rows[i]["wrkunit"] = "";
                    dt.Rows[i]["balqty"] = 0;
                }
                else
                {
                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["itemcode"].ToString();
                }
            }
            return dt;

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoopForSession();
            this.grvissue_DataBind();
        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoopForSession();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }

        protected void ddlitemlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Worklists = this.ddlitemlist.SelectedValue.ToString();

            DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode= " + Worklists);
            dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");

            this.DropCheck1.DataTextField = "flrdes1";
            this.DropCheck1.DataValueField = "flrdes1";
            this.DropCheck1.DataSource = dt;
            this.DropCheck1.DataBind();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["itemlist"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {



                double wrkqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtwrkqty")).Text.Trim()));
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                //double dgvQty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                //double percent = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtpercentge")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;

                dt.Rows[TblRowIndex]["wrkqty"] = wrkqty;

            }
            ViewState["itemlist"] = dt;
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.LoopForSession();
            DataTable itemtable = (DataTable)Session["itemlist"];
            DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            string itemcode = this.ddlitemlist.SelectedValue.ToString().Trim();
            String[] lab = this.DropCheck1.Text.Trim().Split(',');
            String[] lab11 = this.DropCheck1.SelectedValue.ToString().Split(',');
            foreach (string lab1 in lab)
            {
                string flrcode = lab1.Substring(0, 3);
                DataRow[] dr1 = tempforgrid.Select("flrcod='" + flrcode + "'  and itemcode='" + itemcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow drforgrid = tempforgrid.NewRow();
                    drforgrid["flrcod"] = flrcode;
                    drforgrid["flrdes"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["flrdes"];
                    drforgrid["wrkqty"] = 0;

                    drforgrid["balqty"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["balqty"];
                    drforgrid["wrkunit"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["wrkunit"];
                    drforgrid["itemcode"] = this.ddlitemlist.SelectedValue.ToString();
                    drforgrid["workitem"] = this.ddlitemlist.SelectedItem.ToString().Trim();
                    tempforgrid.Rows.Add(drforgrid);

                }

            }
            Session["sessionforgrid"] = tempforgrid;
            this.grvissue_DataBind();
            this.DropCheck1.Text = "";


        }

        protected void ddlcatagory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string itemcode = this.ddlcatagory.SelectedValue.Substring(0, 4).ToString() + "%";

            DataTable dt = ((DataTable)Session["item"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode like '" + itemcode + "' ");
            dt = dv.ToTable(true, "itemcode", "workitem");


            this.ddlitemlist.DataTextField = "workitem";
            this.ddlitemlist.DataValueField = "itemcode";
            this.ddlitemlist.DataSource = dt;
            this.ddlitemlist.DataBind();

        }
    }

}