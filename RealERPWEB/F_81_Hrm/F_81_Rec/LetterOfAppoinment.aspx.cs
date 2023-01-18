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
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{

    public partial class LetterOfAppoinment : System.Web.UI.Page
    {
        ProcessAccess RecData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // for Mettroo
                //string type = this.Request.QueryString["Type"].ToString();
                //this.lblTitle.Text = (type == "SList") ? "Short Listing Information Input/Edit Screen"
                //        : (type == "IResult") ? "Interview Result Information Input/Edit Screen"
                //        : "Final Selection Information Input/Edit Screen";

                Hashtable hst = (Hashtable)Session["tblLogin"];


                //((Label)this.Master.FindControl("lblTitle")).Text = "Latter of Appoinment";

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                //this.lblmsg2.Visible = false;


                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);
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



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchPre.Visible = true;
                this.lblpreAdv.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevAdvList.Visible = true;
                this.ddlPrevAdvList.Items.Clear();


                this.txtCurAdvDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

                this.txtCurAdvDate.Enabled = true;



                this.txtPostSearch.Text = "";

                this.ddlPOSTList.Items.Clear();
                this.ddlCanList.Items.Clear();



                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtCurAdvDate.Enabled = true;


                this.ddlPrevAdvList.Visible = true;
                this.lblPreAdvlist.Visible = false;


                this.ddlPOSTList.Visible = true;
                this.lblPostList.Visible = false;

                this.ddlCanList.Visible = true;
                this.lblCan.Visible = false;



                this.ImgbtnFindReq_Click(null, null);
                this.ImgbtnFindPost_Click(null, null);
                this.ImgbtnFindCan_Click(null, null);


                return;
            }

            //this.txtSrchPre.Visible = false;
            //this.lblpreAdv.Visible = false;
            //this.ImgbtnFindReq.Visible = false;
            this.ddlPrevAdvList.Visible = false;
            this.txtCurAdvDate.Enabled = false;

            this.ddlPrevAdvList.Visible = false;
            this.lblPreAdvlist.Visible = true;
            this.lblPreAdvlist.Text = this.ddlPrevAdvList.SelectedItem.ToString();

            this.ddlPOSTList.Visible = false;
            this.lblPostList.Visible = true;
            this.lblPostList.Text = this.ddlPOSTList.SelectedItem.ToString();

            this.ddlCanList.Visible = false;
            this.lblCan.Visible = true;
            //  this.lblCan.Text = this.ddlCanList.SelectedItem.ToString();

            if (this.Request.QueryString["Type"].ToString() == "LRpt")
            {
                this.btnUpdate.Visible = false;

            }
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_SList_Info();



        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void Get_SList_Info()
        {

            string comcod = this.GetCompCode();
            this.txtCurAdvDate.Enabled = false;
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            string mCandidate = this.ddlCanList.SelectedValue.ToString();

            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "SHOWAPPLETTER", mADVNO, mCandidate,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbAppLetter"] = ds1.Tables[0];
            //ViewState["tblInterv"] = ds1.Tables[1];
            //ViewState["tblreqdesc"] = ds1.Tables[2];

            //if (ds1.Tables[3].Rows.Count != 0)
            //{
            //    this.txtCurAdvDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["sdate"]).ToString("dd.MM.yyyy");
            //}

            //this.Data_Bind();
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                switch (ds1.Tables[0].Rows[i]["letcode"].ToString())
                {
                    case "0101001": //  
                        this.txt0101001.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0101002": // 
                        this.txt0101002.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;

                    case "0101003": //  
                        this.txt0101003.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;

                    case "0102001": //  
                        this.txt0102001.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102002": //  
                        this.txt0102002.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102003": //  
                        this.txt0102003.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;

                    case "0102004": //  
                        this.txt0102004.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;


                    case "0102005": // 
                        this.txt0102005.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;


                    case "0102006": //  
                        this.txt0102006.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102007": // 
                        this.txt0102007.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102008": //  
                        this.txt0102008.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102009": //  
                        this.txt0102009.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102010": //  
                        this.txt0102010.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102011": //  
                        this.txt0102011.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102012": // 
                        this.txt0102012.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102013": //  
                        this.txt0102013.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0102014": //  
                        this.txt0102014.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                    case "0201001": // 
                        this.txt0201001.Text = ds1.Tables[0].Rows[i]["letdesc"].ToString().Trim();
                        break;
                }


            }





        }

        private void CreateText()
        {
            //this.txt0101001.Text = "";

            DataTable tbl1 = (DataTable)ViewState["tbAppLetter"];


            DataRow dr1 = tbl1.NewRow();
            dr1["letcode"] = "0101001";
            dr1["letdesc"] = this.txt0101001.Text;
            dr1["letcode"] = "0101002";
            dr1["letdesc"] = this.txt0101002.Text;

            dr1["letcode"] = "0101003";
            dr1["letdesc"] = this.txt0101003.Text;

            dr1["letcode"] = "0102001";
            dr1["letdesc"] = this.txt0102001.Text;

            dr1["letcode"] = "0102002";
            dr1["letdesc"] = this.txt0102002.Text;

            dr1["letcode"] = "0102003";
            dr1["letdesc"] = this.txt0102003.Text;


            tbl1.Rows.Add(dr1);

            ViewState["tbAppLetter"] = tbl1;


            //for (int j = 0; j < this.gvSListInfo.Rows.Count; j++)
            //{
            //    string gvCol1 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol1")).Text.Trim();
            //    string gvCol2 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol2")).Text.Trim();
            //    string gvCol3 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol3")).Text.Trim();
            //    string gvCol4 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol4")).Text.Trim();
            //    string gvCol5 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol5")).Text.Trim();
            //    string gvCol6 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol6")).Text.Trim();
            //    string gvCol7 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol7")).Text.Trim();
            //    string gvCol8 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol8")).Text.Trim();
            //    string gvCol9 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim();
            //    string gvCol10 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol10")).Text.Trim();
            //    if (this.Request.QueryString["Type"] == "Fselection")
            //    {
            //        gvCol11 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol11")).Text.Trim();
            //        gvCol12 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol12")).Text.Trim();
            //        gvCol13 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol13")).Text.Trim();
            //    }
            //    TblRowIndex2 = (this.gvSListInfo.PageSize) * (this.gvSListInfo.PageIndex) + j;

            //    tbl1.Rows[TblRowIndex2]["col1"] = gvCol1;
            //    tbl1.Rows[TblRowIndex2]["col2"] = gvCol2;
            //    tbl1.Rows[TblRowIndex2]["col3"] = gvCol3;
            //    tbl1.Rows[TblRowIndex2]["col4"] = gvCol4;
            //    tbl1.Rows[TblRowIndex2]["col5"] = gvCol5;
            //    tbl1.Rows[TblRowIndex2]["col6"] = gvCol6;
            //    tbl1.Rows[TblRowIndex2]["col7"] = gvCol7;
            //    tbl1.Rows[TblRowIndex2]["col8"] = gvCol8;
            //    tbl1.Rows[TblRowIndex2]["col9"] = gvCol9;
            //    tbl1.Rows[TblRowIndex2]["col10"] = gvCol10;
            //    if (this.Request.QueryString["Type"] == "Fselection")
            //    {
            //        tbl1.Rows[TblRowIndex2]["col11"] = gvCol11;
            //        tbl1.Rows[TblRowIndex2]["col12"] = gvCol12;
            //        tbl1.Rows[TblRowIndex2]["col13"] = gvCol13;
            //    }


            //}
            //ViewState["tbSList"] = tbl1;



        }






        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            DataTable dt = (DataTable)ViewState["tbAppLetter"];
            ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_81_Rec.rptAppLetter();
            TextObject txtCompname = rptempservices.ReportDefinition.ReportObjects["txtCompname"] as TextObject;
            txtCompname.Text = comnam;
            //TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString();

            //TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            //rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "Department Name: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            //TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            //txtcomaddress.Text = comadd;
            //TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
            //txtnetsalary.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            //TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
            //txtNetpayable.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netpay"]).ToString("#,##0; (#,##0); ");
            rptempservices.SetDataSource((DataTable)ViewState["tbAppLetter"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }









        protected void ImgbtnFindPost_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlPrevAdvList.SelectedValue.ToString();
            string mSrchTxt = this.txtPostSearch.Text.Trim() + "%";
            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETADVPOST", mProject, mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblJobPost"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];

            this.ddlPOSTList.DataTextField = "postdesc";
            this.ddlPOSTList.DataValueField = "postcode";
            this.ddlPOSTList.DataSource = ds1.Tables[0];
            this.ddlPOSTList.DataBind();



        }

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mrfno = "%" + this.txtSrchPre.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREREF", CurDate1,
                          mrfno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevAdvList.Items.Clear();
            this.ddlPrevAdvList.DataTextField = "advno1";
            this.ddlPrevAdvList.DataValueField = "advno";
            this.ddlPrevAdvList.DataSource = ds1.Tables[0];
            this.ddlPrevAdvList.DataBind();


        }

        protected void ImgbtnReqse_Click(object sender, ImageClickEventArgs e)
        {

        }





        protected void ddlPOSTList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindCan_Click(null, null);
        }
        protected void ImgbtnFindCan_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string advno = this.ddlPrevAdvList.SelectedValue.ToString();
            string postcode = this.ddlPOSTList.SelectedValue.ToString();
            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETCANENAME", advno,
                          postcode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlCanList.Items.Clear();
            this.ddlCanList.DataTextField = "listisu1";
            this.ddlCanList.DataValueField = "listisu";
            this.ddlCanList.DataSource = ds1.Tables[0];
            this.ddlCanList.DataBind();

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tbAppLetter"];
            DataTable dt2 = (DataTable)ViewState["tblreqdesc"];
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString().Trim();
            string mPOSTCODE = this.ddlPOSTList.SelectedValue.ToString().Trim();
            string mISTCOD = this.ddlCanList.SelectedValue.ToString().Trim();



            bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0101001", this.txt0101001.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0101002", this.txt0101002.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0101003", this.txt0101003.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ///////////----------------------------//////////////////////
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102001", this.txt0102001.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102002", this.txt0102002.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102003", this.txt0102003.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102004", this.txt0102004.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102005", this.txt0102005.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102006", this.txt0102006.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102007", this.txt0102007.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102008", this.txt0102008.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102009", this.txt0102009.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102010", this.txt0102010.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102011", this.txt0102011.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102012", this.txt0102012.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102013", this.txt0102013.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0102014", this.txt0102014.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ///////////////------------------------------------------------/////////////////

            result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
                            mADVNO, mISTCOD, "0201001", this.txt0201001.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            /////--------------------------//////
            //for (int i = 0; i < tbl1.Rows.Count; i++)
            //{
            //    //string listisu = (tbl1.Rows[i]["listisu"]).ToString();

            //    //DataSet ds2 = RecData.GetTransInfo(comcod, "SP_ENTRY_ADVERTISEMENT", "CHECKISUNUM", mADVNO, mPOSTCODE, listisu, "", "", "", "", "", "");

            //    //string slnum = (ds2.Tables[0].Rows.Count == 0) ? this.GetSlNum() : listisu;
            //    //this.lblslnum.Text = slnum;

            //    for (int j = 0; j < dt2.Rows.Count; j++)
            //    {
            //        string Reqcode = (dt2.Rows[j]["reqcode"]).ToString();
            //        string Reqdesc = tbl1.Rows[i]["col" + (j + 1).ToString()].ToString();

            //        bool result = RecData.UpdateTransInfo(comcod, "SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTERINFO",
            //                mADVNO, mISTCOD, Reqcode, Reqdesc, "", "", "", "", "", "", "", "", "", "", "");
            //        if (!result)
            //        {
            //           ((Label)this.Master.FindControl("lblmsg")).Text = RecData.ErrorObject["Msg"].ToString();
            //            return;
            //        }
            //    }

            //}







            this.txtCurAdvDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Short List Entry";
                string eventdesc = "Update Short List";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
    }
}
