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
namespace RealERPWEB.F_01_LPA
{
    public partial class EntryLandRegProcess : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Land Managment Interface";
                HiddenTab.Value = "tab1";
                lbtnOk_Click(null, null);

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                //GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                //int RowIndex = gvr.RowIndex;


                //int index = this.grvacc.PageSize * this.grvacc.PageIndex + RowIndex;

                //string sircode = ((DataTable)Session["storedata"]).Rows[index]["sircode"].ToString();
                //string actcode = ((DataTable)Session["storedata"]).Rows[index]["actcode"].ToString();
                //this.lblsircode.Text = sircode;
                //this.txtresourcecode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                //this.Chboxchild.Checked = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                //this.chkbod.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                //this.lblchild.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");

                string Projcode = ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 4);

                string comcod = this.GetCompCode();
                DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETNEWLANDCOD", Projcode, "", "", "", "", "", "", "", "");

                string sircode = ds1.Tables[0].Rows[0]["sircode"].ToString();
                this.lblsircode.Text = sircode;
                this.txtresourcecode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Projcode = this.ddlProjectName.SelectedValue.ToString();
                string isircode = this.lblsircode.Text.Trim();
                string tsircode = this.txtresourcecode.Text.Trim().Replace("-", "");
                string sircode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(isircode, 8) == "00000000") ? (ASTUtility.Left(isircode, 4) + "001" + ASTUtility.Right(isircode, 5))
                    : ((ASTUtility.Right(isircode, 5) == "00000" && ASTUtility.Right(isircode, 8) != "00000000") ? (ASTUtility.Left(isircode, 7) + "01" + ASTUtility.Right(isircode, 3)) : ASTUtility.Left(isircode, 9) + "001"))
                    : ((isircode != tsircode) ? tsircode : isircode);
                string mnumber = (isircode == tsircode) ? "" : "manual";

                string Desc = this.txtresourcehead.Text.Trim();

                string txtsirtype = "";
                string txtsirtdesc = "";
                string txtsirunit = this.txtunit.Text.Trim();
                string txtsirval = Convert.ToDouble("0" + this.txtstdrate.Text.Trim()).ToString();
                string txtQty = Convert.ToDouble("0" + this.txtQty.Text.Trim()).ToString();
                double Amount = Convert.ToDouble(txtsirval) * Convert.ToDouble(txtQty);
                // return;

                if (Desc.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Resource Head is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {


                    DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDRESOUCECODE", sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, "0", userid, "", mnumber);

                    //bool result = this.CustData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDRESOUCECODE",
                    //    sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, "0", userid, "", mnumber,
                    //  "", "", "", "", "");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }
                    string sircodeN = ds1.Tables[0].Rows[0]["sircode"].ToString();
                    bool result2 = this.CustData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATELANDBGD",
                        Projcode, sircodeN, Amount.ToString(), txtQty, userid, Terminal, Sessionid);

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                    //this.Chboxchild.Checked = false;
                    this.txtresourcehead.Text = "";
                    this.txtQty.Text = "";
                    this.txtstdrate.Text = "";

                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "4" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string prjSelectCD = this.ddlProjectName.SelectedValue.ToString();

            string slprjcode = "18" + ASTUtility.Right(prjSelectCD, 10).ToString();

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETLPREGPROCESS", ProjectCode, date, slprjcode, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvBgdSales.DataSource = null;
                this.gvBgdSales.DataBind();
                this.gvRegis.DataSource = null;
                this.gvRegis.DataBind();
                return;
            }
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            Session["tbBgdSal"] = this.HiddenSameDataSale(ds2.Tables[1]);


            this.Data_Bind();

            this.PurchaseMap.NavigateUrl = "~/F_04_Bgd/ProjectMapView2.aspx?prjcode=" + prjSelectCD;

            this.GoogleMap.NavigateUrl = "~/F_04_Bgd/ProjectMapView.aspx?prjcode=" + prjSelectCD;
            this.combinedMap.NavigateUrl = "~/F_04_Bgd/ProjectMapCombined.aspx?prjcode=" + prjSelectCD;
            this.SalesMap.NavigateUrl = "~/F_04_Bgd/ProjectSaleMapView.aspx?prjcode=" + prjSelectCD;

            this.HyperLink4.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=LandPurReg&comcod=" + comcod + "&prjcode=" + prjSelectCD;
            this.HyperLink11.NavigateUrl = "~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain&prjcode=" + prjSelectCD;
            this.HyperLink9.NavigateUrl = "~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain&prjcode=" + prjSelectCD + "&sircode=";
            this.HyperLink7.NavigateUrl = "~/F_04_Bgd/PrjInformation.aspx?Type=Report&prjcode=" + prjSelectCD;
            this.HyperLink8.NavigateUrl = "~/F_22_Sal/MktEntryUnit.aspx";




        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            //string actcode = dt1.Rows[0]["actcode"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //        dt1.Rows[j]["actdesc"] = "";
            //    actcode = dt1.Rows[j]["actcode"].ToString();

            //}




            return dt1;

        }
        private DataTable HiddenSameDataSale(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grpcod = dt1.Rows[0]["grpcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grpcod"].ToString() == grpcod)
                {
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grpcod = dt1.Rows[j]["grpcod"].ToString();
                }

            }

            return dt1;
        }
        private void Data_Bind()
        {

            try
            {
                DataTable dt = (DataTable)Session["tblAccRec"];
                this.gvRegis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvRegis.DataSource = dt;
                this.gvRegis.DataBind();
                this.FooterCalculation();



                DataTable dt2 = (DataTable)Session["tbBgdSal"];
                this.gvBgdSales.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvBgdSales.DataSource = dt2;
                this.gvBgdSales.DataBind();

            }
            catch (Exception ex)
            { }

        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFbudgetqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ?
                0.00 : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgvFbudgetamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdam)", "")) ?
                0.00 : dt.Compute("Sum(bgdam)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvRegis.FooterRow.FindControl("lgvFactualqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnqty)", "")) ?
            0.00 : dt.Compute("Sum(trnqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRegis.FooterRow.FindControl("lgvFactualamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBgdSales.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            this.Data_Bind();
        }



        protected void gvRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRegis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
            HiddenTab.Value = "tab1";

        }






        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }





        private void printDuesCollection()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptDuesCollection();
            //DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            //TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rpttxtCompName.Text = comnam;



            //TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            //txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            //TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            //txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            //TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            //txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            //TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            //TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            //rpttxttoduesupto.Text = "Receivable Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            //TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            //rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            //TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            //rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");






            //TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptRcvList.SetDataSource((DataTable)Session["tblAccRec"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Received List Info";
            //    string eventdesc = "Print Report MR";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptRcvList.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptRcvList;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }


        protected void gvRegis_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvRegis.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvRegis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvRegis.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string procode = ((Label)this.gvRegis.Rows[e.NewEditIndex].FindControl("lgvprocode")).Text.Trim();
            int rowindex = (gvRegis.PageSize) * (this.gvRegis.PageIndex) + e.NewEditIndex;
            DropDownList ddl2 = (DropDownList)this.gvRegis.Rows[e.NewEditIndex].FindControl("ddlpro");
            ViewState["gindex"] = e.NewEditIndex; ;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //Process
            string SearchProject = "%" + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETPROCESSCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "gdesc";
            ddl2.DataValueField = "gcode";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = procode;


            //Broker       
            string brocode = ((Label)this.gvRegis.Rows[e.NewEditIndex].FindControl("lgvbrocode")).Text.Trim();
            DropDownList ddlbro = (DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlbro");
            string SearchBro = "%" + ((TextBox)gvRegis.Rows[rowindex].FindControl("txtSerachbro")).Text.Trim() + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETBORKERCODE", SearchBro, "", "", "", "", "", "", "", "");
            ddlbro.DataTextField = "gdesc";
            ddlbro.DataValueField = "gcode";
            ddlbro.DataSource = ds2.Tables[0];
            ddlbro.DataBind();
            ddlbro.SelectedValue = brocode;


            ds1.Dispose();
            ds2.Dispose();



        }
        protected void gvRegis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblAccRec"];
            int rowindex = (int)ViewState["gindex"];
            string pactcode = ((DataTable)Session["tblAccRec"]).Rows[rowindex]["actcode"].ToString();
            string usircode = ((DataTable)Session["tblAccRec"]).Rows[rowindex]["rsircode"].ToString();
            string procode = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlpro")).SelectedValue.ToString();
            string brocode = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlbro")).SelectedValue.ToString();
            //if (regcode == "0000000")
            //{
            //    this.gvRegis.EditIndex = -1;
            //    this.Data_Bind();
            //    return;
            //}
            string regisdat = this.txtdate.Text.Trim();
            DataRow[] dr2 = dt.Select("actcode = '" + pactcode + "' and rsircode='" + usircode + "'");
            if (dr2.Length > 0)
            {
                dr2[0]["procode"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlpro")).SelectedValue.ToString();
                dr2[0]["prodesc"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlpro")).SelectedItem.Text;

                dr2[0]["brocode"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlbro")).SelectedValue.ToString();
                dr2[0]["brodesc"] = ((DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlbro")).SelectedItem.Text;
            }


            bool resulta = false;
            resulta = CustData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "INSOUPLREGTDINF", pactcode, usircode, procode, brocode, regisdat, "", "", "", "", "", "", "", "", "", "");


            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            this.gvRegis.EditIndex = -1;
            Session["tblAccRec"] = dt;
            this.Data_Bind();

        }

        protected void ibtnSrchpro_Click(object sender, ImageClickEventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlpro");
            string SearchProject = "%" + ((TextBox)gvRegis.Rows[rowindex].FindControl("txtSerachpro")).Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETPROCESSCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "gdesc";
            ddl2.DataValueField = "gcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();


        }
        protected void ibtnSrchbro_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddl2 = (DropDownList)this.gvRegis.Rows[rowindex].FindControl("ddlbro");
            string SearchProject = "%" + ((TextBox)gvRegis.Rows[rowindex].FindControl("txtSerachbro")).Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "GETBORKERCODE", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "gdesc";
            ddl2.DataValueField = "gcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void gvRegis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            //?comcod=2306&patcode=160100010008&sircode=140800000001
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
            string mACTCODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

            //
            HyperLink hlinkDoc = (HyperLink)e.Row.FindControl("HLgvsircodeDoc");
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvsircode");
            HyperLink PayStatus = (HyperLink)e.Row.FindControl("HyLpaystusamount");

            string mCOMCOD = comcod;
            //   string mACTCODE = this.ddlProjectName.SelectedValue.ToString();
            ///string CustCode = ((Label)e.Row.FindControl("lblCode")).Text;


            //  hlink1.Font.Bold = true;
            hlink1.Style.Add("color", "blue");
            hlinkDoc.Style.Add("color", "blue");
            PayStatus.Style.Add("color", "blue");

            //  hlinkDoc.Font.Bold = true;  /.aspx?=&actcode=160100010001&usircode=

            hlink1.NavigateUrl = "../F_51_LBgd/LinkLandPurInfo.aspx?comcod=" + mCOMCOD + "&patcode=" + mACTCODE + "&sircode=" + code + "&type=";
            hlinkDoc.NavigateUrl = "../F_51_LBgd/LinkLandPurInfo.aspx?comcod=" + mCOMCOD + "&patcode=" + mACTCODE + "&sircode=" + code + "&type=DocUpload";
            PayStatus.NavigateUrl = "../F_14_Pro/RptLandOwnerPaySch.aspx?Type=Report" + "&actcode=" + mACTCODE + "&usircode=" + code;

        }


        protected void gvBgdSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string fcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

            string mACTCODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

            Label flrdesc = (Label)e.Row.FindControl("lgcFlDesc");
            Label USize = (Label)e.Row.FindControl("lgvUSize");
            Label UAmt = (Label)e.Row.FindControl("lgvUAmt");
            Label UQty = (Label)e.Row.FindControl("lgvUQty");
            Label PQty = (Label)e.Row.FindControl("lgvPQty");
            Label OChr = (Label)e.Row.FindControl("lgvOChr");
            Label TAmt = (Label)e.Row.FindControl("lgvTAmt");
            HyperLink hypliSalesDocs = (HyperLink)e.Row.FindControl("hypliSalesDocs");
            hypliSalesDocs.Style.Add("color", "blue");
            hypliSalesDocs.NavigateUrl = "../F_51_LBgd/LinkLandPurInfo.aspx?comcod=" + comcod + "&patcode=" + mACTCODE + "&sircode=" + fcode + "&type=SaleDocUpload";


            if (fcode == "")
            {
                return;
            }
            if (ASTUtility.Right(fcode, 5) == "AAAAA")
            {
                flrdesc.Font.Bold = true;
                USize.Font.Bold = true;
                UAmt.Font.Bold = true;
                UQty.Font.Bold = true;
                //PQty.Font.Bold = true;
                OChr.Font.Bold = true;
                TAmt.Font.Bold = true;

                flrdesc.Style.Add("text-align", "right");
            }
        }
        protected void gvBgdSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdSales.PageIndex = e.NewPageIndex;
            this.Data_Bind();
            HiddenTab.Value = "tab2";

            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent();", true);
        }
    }



}












