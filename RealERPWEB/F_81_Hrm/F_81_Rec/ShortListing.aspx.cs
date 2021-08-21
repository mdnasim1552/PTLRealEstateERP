using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using AjaxControlToolkit;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class ShortListing : System.Web.UI.Page
    {
        ProcessAccess RecData = new ProcessAccess();
        string uploadedFile = string.Empty;
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");


                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // for Mettroo
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "SList") ? "Short Listing Information Input/Edit Screen"
                        : (type == "IResult") ? "Interview Result Information Input/Edit Screen"
                        : "Final Selection Information Input/Edit Screen";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lblmsg1.Visible = false;
                this.lblmsg2.Visible = false;



                Page.Form.Attributes.Add("enctype", "multipart/form-data");



                if (!string.IsNullOrEmpty(this.Request.QueryString["advno"]))
                {
                    this.txtSrchPre.Text = this.Request.QueryString["advno"].ToString();
                    this.txtPostSearch.Text = this.Request.QueryString["postdesc"].ToString();
                    this.ImgbtnFindReq_Click(null, null);
                    this.ImgbtnFindPost_Click(null, null);
                    //this.lbtnOk.Text = "Ok";
                    //this.lbtnOk_Click(null, null);
                }
                else
                {
                    this.lblslnum.Text = "";
                    this.lbtnOk.Text = "New";
                    this.lbtnOk_Click(null, null);

                }

                string qtype = this.Request.QueryString["Type"].ToString();
                if (qtype == "Fselection" || qtype == "IResult")
                {
                    this.pnlcv.Visible = false;
                    this.imgBtnInt.Visible = false;
                    this.Label3.Visible = false;
                    this.ddlInterviewer.Visible = false;
                    this.lbtnSelectRes.Visible = false;

                }
                else
                {
                    this.pnlcv.Visible = true;
                    this.Label3.Visible = true;


                }




            }

        }





        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkSendsmsemail_Click);



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
                this.Pnladv.Visible = false;
                this.txtSrchPre.Visible = true;
                this.lblpreAdv.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                //this.ddlPrevAdvList.Visible = true;
                //this.ddlPrevAdvList.Items.Clear();


                this.txtCurAdvDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

                this.txtCurAdvDate.Enabled = true;

                this.lblmsg1.Text = "";
                this.lblmsg2.Text = "";
                // this.txtPostSearch.Text = "";
                this.ddlInterviewer.Items.Clear();
                this.ddlPOSTList.Items.Clear();
                this.ddlCandidate.Items.Clear();
                this.gvSListInfo.DataSource = null;
                this.gvSListInfo.DataBind();
                this.PanelAddCan.Visible = false;
                //this.PanelAddInt.Visible = false;
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtCurAdvDate.Enabled = true;


                this.ddlPrevAdvList.Visible = true;
                this.lblPreAdvlist.Visible = false;


                this.ddlPOSTList.Visible = true;
                //// this.lblPostList.Visible = false;



                this.ImgbtnFindReq_Click(null, null);
                this.ImgbtnFindPost_Click(null, null);


                this.ddlPrevlist.Visible = true;
                this.Label7.Visible = true;
                this.lbkButtonPrev.Visible = true;
                this.ddlPrevlist.Items.Clear();

                return;
            }

            //this.txtSrchPre.Visible = false;
            //this.lblpreAdv.Visible = false;
            //this.ImgbtnFindReq.Visible = false;
            this.ddlPrevAdvList.Visible = false;
            this.txtCurAdvDate.Enabled = false;
            this.ddlPrevAdvList.Visible = false;
            this.lblPreAdvlist.Visible = true;
            //// this.lblPostList.Visible = true;


            //string mADVNO = "";
            //string mPOST = "";
            if (this.ddlPrevlist.Items.Count > 0)
            {
                //mADVNO = this.ddlPrevlist.SelectedValue.Substring(0, 12).ToString();
                //mPOST = this.ddlPrevlist.SelectedValue.Substring(12, 5).ToString();
                this.lblPreAdvlist.Text = this.ddlPrevlist.SelectedItem.ToString();

            }
            else
            {
                this.lblPreAdvlist.Text = this.ddlPrevAdvList.SelectedItem.ToString();

                //this.lblPostList.Text = this.ddlPOSTList.SelectedItem.ToString();


            }


            //this.lblPostList.Text = this.ddlPOSTList.SelectedItem.ToString();
            //this.lblPreAdvlist.Text = this.ddlPrevlist.SelectedItem.ToString();
            this.ddlPOSTList.Visible = true;
            //// this.lblPostList.Visible = true;

            if (this.Request.QueryString["Type"].ToString() == "SList")
            {
                this.PanelAddCan.Visible = true;
                //this.PanelAddInt.Visible = false;

            }

            this.Pnladv.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_SList_Info();
            this.GetAdvInfo();
            this.GetCanList();


        }

        protected void GetAdvInfo()
        {
            string comcod = this.GetCompCode();
            this.txtCurAdvDate.Enabled = false;

            string mADVNO = "";
            string mPOST = "";
            if (this.ddlPrevlist.Items.Count > 0)
            {
                mADVNO = this.ddlPrevlist.SelectedValue.Substring(0, 12).ToString();
                mPOST = this.ddlPrevlist.SelectedValue.Substring(12, 5).ToString();


                DataTable dt = (DataTable)ViewState["tblddlprevlist"];

                DataView dv = dt.DefaultView;
                dv.RowFilter = ("advno2='" + mADVNO + "'");
                this.ddlPrevAdvList.Items.Clear();
                this.ddlPrevAdvList.DataTextField = "advno1";
                this.ddlPrevAdvList.DataValueField = "advno2";
                this.ddlPrevAdvList.DataSource = dv.ToTable();
                this.ddlPrevAdvList.DataBind();




                this.ddlPOSTList.DataTextField = "postdesc";
                this.ddlPOSTList.DataValueField = "postcode";
                this.ddlPOSTList.DataSource = dv.ToTable(); ;
                this.ddlPOSTList.DataBind();


                this.ddlPrevAdvList.SelectedValue = mADVNO;
                //this.ddlPOSTList.SelectedValue=mPOST;
                //mPOST=  code.Substring(5, 9).ToString();
            }
            else
            {
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
                mPOST = this.ddlPOSTList.SelectedValue.ToString();

            }



            //string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            //string mPOST = this.ddlPOSTList.SelectedValue.ToString();

            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREADVDATABYPOST", mADVNO, mPOST,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count != 0)
            {
                this.lblapp.Text = ds1.Tables[1].Rows[0]["approveby"].ToString(); ;
                this.lblDpt.Text = ds1.Tables[0].Rows[1]["deptdesc"].ToString();
                this.lblPost.Text = ds1.Tables[0].Rows[0]["postdesc"].ToString();// this.ddlPOSTList.SelectedItem.ToString();
                this.lblexp.Text = ds1.Tables[0].Rows[1]["requir"].ToString();
                this.lblsal.Text = ds1.Tables[0].Rows[2]["requir"].ToString();
                this.lblnpos.Text = ds1.Tables[0].Rows[4]["requir"].ToString();
                this.lblQua.Text = ds1.Tables[0].Rows[0]["requir"].ToString();
                //this.ddlPrevAdvList.SelectedValue=
            }


        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        //protected void GetAdvNo()
        //{
        //    string comcod = this.GetCompCode();
        //    string mADVNO = "NEWADV";
        //    if (this.ddlPrevAdvList.Items.Count > 0)
        //        mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();

        //    string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
        //    if (mADVNO == "NEWADV")
        //    {
        //        DataSet ds2 = RecData.GetTransInfo(comcod, "SP_ENTRY_ADVERTISEMENT", "GETREFNO", mADVDAT,
        //               "", "", "", "", "", "", "", "");
        //        if (ds2 == null)
        //            return;
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {

        //            this.ddlPrevAdvList.DataTextField = "advno1";
        //            this.ddlPrevAdvList.DataValueField = "advno";
        //            this.ddlPrevAdvList.DataSource = ds2.Tables[0];
        //            this.ddlPrevAdvList.DataBind();
        //        }
        //    }

        //}
        protected void Get_SList_Info()
        {

            string comcod = this.GetCompCode();
            this.txtCurAdvDate.Enabled = false;

            string mADVNO = "";
            string mPOST = "";
            if (this.ddlPrevlist.Items.Count > 0)
            {
                mADVNO = this.ddlPrevlist.SelectedValue.Substring(0, 12).ToString();
                mPOST = this.ddlPrevlist.SelectedValue.Substring(12, 5).ToString();

            }
            else
            {
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
                mPOST = this.ddlPOSTList.SelectedValue.ToString();

            }



            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "SHOWSHOETLIST", mADVNO, mPOST,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbSList"] = ds1.Tables[0];
            ViewState["tblInterv"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];

            if (ds1.Tables[3].Rows.Count != 0)
            {
                this.txtCurAdvDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["sdate"]).ToString("dd.MM.yyyy");
            }

            this.Data_Bind();

        }
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Int_Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tblInterv"];
            string mIntCode = this.ddlInterviewer.SelectedValue.ToString();

            DataTable tbl3 = (DataTable)ViewState["tblInt"];

            DataRow[] dr3 = tbl1.Select("intcode = '" + mIntCode + "'");
            if (dr3.Length == 0)
            {

                DataRow dr1 = tbl1.NewRow();
                dr1["postcode"] = this.ddlPOSTList.SelectedValue.ToString();
                dr1["postdesc"] = this.ddlPOSTList.SelectedItem.Text.Trim();
                dr1["intcode"] = (((DataTable)ViewState["tblInt"]).Select("intcode='" + mIntCode + "'"))[0]["intcode"].ToString();
                dr1["intdesc"] = (((DataTable)ViewState["tblInt"]).Select("intcode='" + mIntCode + "'"))[0]["intdesc"].ToString();
                dr1["intdat"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr1["remarks"] = "";

                tbl1.Rows.Add(dr1);

            }


            ViewState["tblInterv"] = tbl1;
            this.Data_Bind();

        }
        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    //DataView dv = dt1.DefaultView;
        //    //dv.Sort = "postcode";
        //    //dt1 = dv.ToTable();
        //    string postcode = dt1.Rows[0]["postcode"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["postcode"].ToString() == postcode)
        //        {
        //            postcode = dt1.Rows[j]["postcode"].ToString();
        //            dt1.Rows[j]["postdesc"] = "";
        //        }

        //        else
        //            postcode = dt1.Rows[j]["postcode"].ToString();
        //    }

        //    return dt1;
        //}

        //private DataTable HiddenSameData1(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    //DataView dv = dt1.DefaultView;
        //    //dv.Sort = "postcode";
        //    //dt1 = dv.ToTable();
        //    string postcode = dt1.Rows[0]["postcode"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["postcode"].ToString() == postcode)
        //        {
        //            postcode = dt1.Rows[j]["postcode"].ToString();
        //            dt1.Rows[j]["postdesc"] = "";
        //        }

        //        else
        //            postcode = dt1.Rows[j]["postcode"].ToString();
        //    }

        //    return dt1;
        //}
        protected void lnkPrint_Click(object sender, EventArgs e)
        { }
        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            this.lblmsg1.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();



            this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tbSList"];
            DataTable dt2 = (DataTable)ViewState["tblreqdesc"];
            string mADVNO = "";
            string mPOSTCODE = "";
            if (this.ddlPrevlist.Items.Count > 0)
            {
                mADVNO = this.ddlPrevlist.SelectedValue.Substring(0, 12).ToString();
                mPOSTCODE = this.ddlPrevlist.SelectedValue.Substring(12, 5).ToString();

            }
            else
            {
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
                mPOSTCODE = this.ddlPOSTList.SelectedValue.ToString();

            }


            //string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString().Trim();
            //string mPOSTCODE = this.ddlPOSTList.SelectedValue.ToString().Trim();


            /////--------------------------//////
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string listisu = (tbl1.Rows[i]["listisu"]).ToString();
                string sendflag = (tbl1.Rows[i]["sendflag"]).ToString();


                DataSet ds2 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "CHECKISUNUM", mADVNO, mPOSTCODE, listisu, "", "", "", "", "", "");

                string slnum = (ds2.Tables[0].Rows.Count == 0) ? this.GetSlNum() : listisu;
                //this.lblslnum.Text = slnum;
                int k = 0;
                for (int j = 0; j < dt2.Rows.Count; j++)
                {


                    string Reqcode = (dt2.Rows[k]["reqcode"]).ToString();
                    string Reqdesc = tbl1.Rows[i]["col" + (j + 1).ToString()].ToString();



                    //if(j+1=14)


                    bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATESHRTLIST", "SHORTLISTA",
                            mADVNO, mPOSTCODE, Reqcode, Reqdesc, slnum, sendflag, "", "", "",
                            "", "", "", "", "");
                    if (!result)
                    {
                        this.lblmsg1.Text = RecData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    k++;

                }


            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Short List Entry";
                string eventdesc = "Update Short List";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            this.lblmsg1.Visible = true;


            this.Int_Save_Value();
            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            DataTable tbl2 = (DataTable)ViewState["tblInterv"];
            if (tbl2 == null || tbl2.Rows.Count == 0)
            {
                this.lblmsg1.Text = "please select Interviewer";
                this.lblmsg1.Attributes.Add("style", "color:#fff;font-weight:bold; background:red");
                return;
            }



            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string mINTCODE = tbl2.Rows[i]["intcode"].ToString();
                string mREMARKS = tbl2.Rows[i]["remarks"].ToString();
                string mINTDAT = tbl2.Rows[i]["intdat"].ToString();




                bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATESHRTLIST", "SHORTLISTB",
                            mADVNO, mPOSTCODE, mINTCODE, mADVDAT, mREMARKS, mINTDAT, "Ok", "", "",
                            "", "", "", "", "");
                if (!result)
                {
                    this.lblmsg2.Text = RecData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.txtCurAdvDate.Enabled = false;


            this.lblmsg1.Text = "Update";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Short List Interviewer Entry";
                string eventdesc = "Update Short List Interviewer";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




            this.Get_SList_Info();
            this.GetCanList();




        }
        protected void Data_Bind()
        {
            DataTable dtname = (DataTable)ViewState["tblreqdesc"];
            int j = 4;
            for (int i = 0; i < dtname.Rows.Count; i++)
            {

                this.gvSListInfo.Columns[j].HeaderText = dtname.Rows[i]["reqdesc"].ToString();

                j = (i == 14) ? j + 2 : j + 1;

                //if (i == 12)
                //    j = j + 2;
                //else
                //    j++;


                if (j == 26)
                    break;
            }

            DataTable tbl1 = (DataTable)ViewState["tbSList"];
            this.gvSListInfo.DataSource = tbl1;
            this.gvSListInfo.DataBind();

            for (int i = 0; i < this.gvSListInfo.Rows.Count; i++)
            {
                string postcode = ((Label)gvSListInfo.Rows[i].FindControl("lblgvPostCod")).Text.Trim();
                string slno = ((Label)gvSListInfo.Rows[i].FindControl("lblgvissue")).Text.Trim();
                //string mrno = ((Label)gvSListInfo.Rows[i].FindControl("lgvmrno")).Text.Trim();
                //string cheqno = ((Label)gvSListInfo.Rows[i].FindControl("lgvCheNo")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSListInfo.Rows[i].FindControl("lbSelection");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = postcode + slno;

                ((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;
                ((LinkButton)this.gvSListInfo.Rows[i].FindControl("lbSelection")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;




                // ((LinkButton)this.gvSListInfo.Rows[i].FindControl("lbSelectionSend")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkSMSEmail")).Checked) ? false : true;

                //((FileUpload)this.gvSListInfo.Rows[i].FindControl("FileUpload1")).Visible = ((HyperLink)this.gvSListInfo.Rows[i].FindControl("hyplink")).Text.ToString() == "" ? true : false;


            }



            DataTable tbl2 = (DataTable)ViewState["tblInterv"];
            this.gvIntInfo.DataSource = tbl2;
            this.gvIntInfo.DataBind();

            if (this.Request.QueryString["Type"] == "IResult")
            {
                this.gvSListInfo.Columns[1].Visible = false;
                this.gvIntInfo.Columns[1].Visible = false;

                this.gvSListInfo.Columns[11].Visible = false;

                this.gvSListInfo.Columns[24].Visible = false;
                this.gvSListInfo.Columns[23].Visible = false;
                this.gvSListInfo.Columns[25].Visible = false;
                //this.gvSListInfo.Columns[20].Visible = false;
                this.Label3.Visible = false;
            }

            if (this.Request.QueryString["Type"] == "Fselection")
            {
                // ((LinkButton)this.gvSListInfo.FooterRow.FindControl("lbtnUpdateResReq")).Visible = false;
                //((LinkButton)this.gvIntInfo.FooterRow.FindControl("lnkbtnSave")).Visible = false;
                // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                this.gvSListInfo.Columns[1].Visible = false;
                this.gvIntInfo.Columns[1].Visible = false;
                this.gvSListInfo.Columns[11].Visible = false;

            }

            if (this.Request.QueryString["Type"] == "SList")
            {

                this.gvSListInfo.Columns[11].Visible = false;
                this.gvSListInfo.Columns[16].Visible = false;
                this.gvSListInfo.Columns[17].Visible = false;
                this.gvSListInfo.Columns[18].Visible = false;
                this.gvSListInfo.Columns[19].Visible = false;
                this.gvSListInfo.Columns[20].Visible = false;
                this.gvSListInfo.Columns[21].Visible = false;
                this.gvSListInfo.Columns[22].Visible = false;
                this.gvSListInfo.Columns[23].Visible = false;
                this.gvSListInfo.Columns[24].Visible = false;
                this.gvSListInfo.Columns[25].Visible = false;

            }


            //string Type = this.Request.QueryString["Type"].ToString();
            //this.gvSListInfo.Columns[15].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[16].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[17].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[18].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[19].Visible = (Type == "SList") ? false : true;
            ////this.gvSListInfo.Columns[20].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[21].Visible = (Type == "SList") ? false : true;
            //this.gvSListInfo.Columns[22].Visible = (Type == "SList") ? false : true;
            ////this.gvSListInfo.Columns[23].Visible = (Type == "SList") ? false : true;


        }
        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tbSList"];
            int TblRowIndex2;
            this.lblmsg1.Text = "";
            string gvCol14 = "";
            string gvCol15 = "";
            string gvCol16 = "";
            string gvCol17 = "";
            string gvCol18 = "";
            string gvCol19 = "";
            for (int j = 0; j < this.gvSListInfo.Rows.Count; j++)
            {
                string gvCol1 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol1")).Text.Trim();
                string gvCol2 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol2")).Text.Trim();
                string gvCol3 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol3")).Text.Trim();
                string gvCol4 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol4")).Text.Trim();
                string gvCol5 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol5")).Text.Trim();
                string gvCol6 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol6")).Text.Trim();
                string gvCol7 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol7")).Text.Trim();
                string gvCol8 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol8")).Text.Trim();

                string date = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim();//((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim();

                string gvCol9 = (((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim();//((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol9")).Text.Trim();
                string gvCol10 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol10")).Text.Trim();
                string gvCol11 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol11")).Text.Trim();
                string gvCol12 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol12")).Text.Trim();
                string gvCol13 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol13")).Text.Trim();
                string sendflag = ((CheckBox)this.gvSListInfo.Rows[j].FindControl("chkSMSEmail")).Checked.ToString();
                //string gvCol20 = ((Label)this.gvSListInfo.Rows[j].FindControl("lblcv")).Text.Trim();

                if (this.Request.QueryString["Type"] == "Fselection" || this.Request.QueryString["Type"] == "IResult")
                {
                    gvCol14 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol14")).Text.Trim();
                    gvCol15 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol15")).Text.Trim();
                    gvCol16 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol16")).Text.Trim();
                    gvCol17 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol17")).Text.Trim();
                    gvCol18 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol18")).Text.Trim();
                    gvCol19 = ((TextBox)this.gvSListInfo.Rows[j].FindControl("txtgvCol19")).Text.Trim();

                }
                TblRowIndex2 = (this.gvSListInfo.PageSize) * (this.gvSListInfo.PageIndex) + j;

                tbl1.Rows[TblRowIndex2]["col1"] = gvCol1;
                tbl1.Rows[TblRowIndex2]["col2"] = gvCol2;
                tbl1.Rows[TblRowIndex2]["col3"] = gvCol3;
                tbl1.Rows[TblRowIndex2]["col4"] = gvCol4;
                tbl1.Rows[TblRowIndex2]["col5"] = gvCol5;
                tbl1.Rows[TblRowIndex2]["col6"] = gvCol6;
                tbl1.Rows[TblRowIndex2]["col7"] = gvCol7;
                tbl1.Rows[TblRowIndex2]["col8"] = gvCol8;
                tbl1.Rows[TblRowIndex2]["col9"] = gvCol9;
                tbl1.Rows[TblRowIndex2]["col10"] = gvCol10;
                tbl1.Rows[TblRowIndex2]["col11"] = gvCol11;
                tbl1.Rows[TblRowIndex2]["col12"] = gvCol12;
                tbl1.Rows[TblRowIndex2]["col13"] = gvCol13;

                tbl1.Rows[TblRowIndex2]["tomark"] = Convert.ToDouble("0" + gvCol13) + Convert.ToDouble("0" + gvCol14) + Convert.ToDouble("0" + gvCol15);

                if (this.Request.QueryString["Type"] == "Fselection" || this.Request.QueryString["Type"] == "IResult")
                {
                    tbl1.Rows[TblRowIndex2]["col14"] = gvCol14;
                    tbl1.Rows[TblRowIndex2]["col15"] = gvCol15;
                    tbl1.Rows[TblRowIndex2]["col16"] = gvCol16;
                    tbl1.Rows[TblRowIndex2]["col17"] = gvCol17;
                    tbl1.Rows[TblRowIndex2]["col18"] = gvCol18;
                    tbl1.Rows[TblRowIndex2]["col19"] = gvCol19;
                }
                //tbl1.Rows[TblRowIndex2]["col18"] = gvCol18;
                tbl1.Rows[TblRowIndex2]["sendflag"] = sendflag;


            }
            ViewState["tbSList"] = tbl1;
        }
        private void Int_Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblInterv"];
            int TblRowIndex2;
            this.lblmsg2.Text = "";
            for (int j = 0; j < this.gvIntInfo.Rows.Count; j++)
            {
                string gvIntDesc = ((Label)this.gvIntInfo.Rows[j].FindControl("lblgvIntDesc")).Text.Trim();
                //string gvIntDat = ((TextBox)this.gvIntInfo.Rows[j].FindControl("txtgvIntDat")).Text.Trim();
                string gvIntDat = (((TextBox)this.gvIntInfo.Rows[j].FindControl("txtgvIntDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvIntInfo.Rows[j].FindControl("txtgvIntDat")).Text.Trim();
                string gvRem = ((TextBox)this.gvIntInfo.Rows[j].FindControl("txtgvRem")).Text.Trim();




                TblRowIndex2 = (this.gvIntInfo.PageSize) * (this.gvIntInfo.PageIndex) + j;

                tbl1.Rows[TblRowIndex2]["intdesc"] = gvIntDesc;
                tbl1.Rows[TblRowIndex2]["intdat"] = gvIntDat;
                tbl1.Rows[TblRowIndex2]["remarks"] = gvRem;


            }
            ViewState["tblInterv"] = tbl1;
        }
        private void GetPost()
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
        protected void ImgbtnFindPost_Click(object sender, EventArgs e)
        {

            this.GetPost();
        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string mrfno = "%" + this.txtSrchPre.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string qtype = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETSHORTLIST", CurDate1,
                          mrfno, qtype, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevAdvList.Items.Clear();
            this.ddlPrevAdvList.DataTextField = "advno1";
            this.ddlPrevAdvList.DataValueField = "advno";
            this.ddlPrevAdvList.DataSource = ds1.Tables[0];
            this.ddlPrevAdvList.DataBind();
            this.GetPost();


        }

        protected void ImgbtnReqse_Click(object sender, EventEntry e)
        {

        }
        protected void imgBtnInt_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string mSrchTxt = this.txtIntList.Text.Trim() + "%";
            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETINTERVIWER", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblInt"] = ds1.Tables[0];
            this.ddlInterviewer.DataTextField = "intdesc";
            this.ddlInterviewer.DataValueField = "intcode";
            this.ddlInterviewer.DataSource = ds1.Tables[0];
            this.ddlInterviewer.DataBind();

        }
        protected void gvAdvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSListInfo.PageIndex = e.NewPageIndex;
            this.Save_Value();
            this.Data_Bind();
        }
        private string GetSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["slnum"].ToString();
        }
        private string IncrmentSlNum()
        {
            string slnum = (Convert.ToInt32(this.lblslnum.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + slnum), 9));



        }
        protected void lbtnAddList_Click(object sender, EventArgs e)
        {
            //this.PanelAddInt.Visible = true;
            this.Save_Value();

            DataTable tbl1 = (DataTable)ViewState["tbSList"];
            string mPostCode = this.ddlPOSTList.SelectedValue.ToString();

            string slnum = (this.lblslnum.Text == "") ? this.GetSlNum() : this.IncrmentSlNum();
            this.lblslnum.Text = slnum;

            DataRow dr1 = tbl1.NewRow();
            dr1["advno"] = this.ddlPrevAdvList.SelectedValue.ToString();
            dr1["postcode"] = this.ddlPOSTList.SelectedValue.ToString();
            dr1["postdesc"] = this.ddlPOSTList.SelectedItem.Text.Trim();
            dr1["listisu"] = slnum;
            dr1["col1"] = "";
            dr1["col2"] = "";
            dr1["col3"] = "";
            dr1["col4"] = "";
            dr1["col5"] = "";
            dr1["col6"] = "";
            dr1["col7"] = "";
            dr1["col8"] = "";
            dr1["col9"] = "01-Jan-1900";
            dr1["col10"] = "";
            dr1["col11"] = "";
            dr1["col12"] = "";
            dr1["col13"] = "";
            dr1["col13"] = "";
            dr1["col14"] = "";
            dr1["col15"] = "";
            dr1["col16"] = "";
            dr1["col17"] = "";
            dr1["col18"] = "";
            dr1["col19"] = "";
            //dr1["col20"] = "";
            dr1["tomark"] = 0.00;
            dr1["sendflag"] = "";

            tbl1.Rows.Add(dr1);

            ViewState["tbSList"] = tbl1;
            this.Data_Bind();
        }
        protected void lbtnUpdateInt_Click(object sender, EventArgs e)
        {
            this.lblmsg1.Visible = true;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Int_Save_Value();
            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            DataTable tbl1 = (DataTable)ViewState["tblInterv"];



            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString().Trim();
            string mPOSTCODE = this.ddlPOSTList.SelectedValue.ToString().Trim();

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mINTCODE = tbl1.Rows[i]["intcode"].ToString();
                string mREMARKS = tbl1.Rows[i]["remarks"].ToString();
                string mINTDAT = tbl1.Rows[i]["intdat"].ToString();

                bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATESHRTLIST", "SHORTLISTB",
                            mADVNO, mPOSTCODE, mINTCODE, mADVDAT, mREMARKS, mINTDAT, "", "", "",
                            "", "", "", "", "");
                if (!result)
                {
                    this.lblmsg2.Text = RecData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.txtCurAdvDate.Enabled = false;
            this.lblmsg1.Text = "Update Successfully";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Short List Interviewer Entry";
                string eventdesc = "Update Short List Interviewer";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvSListInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox gvCol1 = (TextBox)e.Row.FindControl("txtgvCol1");
            //    TextBox gvCol2 = (TextBox)e.Row.FindControl("txtgvCol2");
            //    TextBox gvCol3 = (TextBox)e.Row.FindControl("txtgvCol3");
            //    TextBox gvCol4 = (TextBox)e.Row.FindControl("txtgvCol4");
            //    TextBox gvCol5 = (TextBox)e.Row.FindControl("txtgvCol5");
            //    TextBox gvCol6 = (TextBox)e.Row.FindControl("txtgvCol6");
            //    TextBox gvCol7 = (TextBox)e.Row.FindControl("txtgvCol7");
            //    TextBox gvCol8 = (TextBox)e.Row.FindControl("txtgvCol8");
            //    TextBox gvCol9 = (TextBox)e.Row.FindControl("txtgvCol9");
            //    TextBox gvCol10 = (TextBox)e.Row.FindControl("txtgvCol10");
            //    TextBox gvCol11 = (TextBox)e.Row.FindControl("txtgvCol11");
            //    TextBox gvCol12 = (TextBox)e.Row.FindControl("txtgvCol12");
            //    TextBox gvCol13 = (TextBox)e.Row.FindControl("txtgvCol13");
            //    TextBox gvCol14 = (TextBox)e.Row.FindControl("txtgvCol14");
            //    TextBox gvCol15 = (TextBox)e.Row.FindControl("txtgvCol15");
            //    TextBox gvCol16 = (TextBox)e.Row.FindControl("txtgvCol16");
            //    string type = this.Request.QueryString["Type"].ToString();

            //    //string Nlistisu = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nlistisu")).ToString();


            //    if (type == "SList")
            //    {

            //        gvCol9.ReadOnly = true;


            //    }
            //    else if (type == "IResult")
            //    {
            //        gvCol1.ReadOnly = true;
            //        gvCol2.ReadOnly = true;
            //        gvCol3.ReadOnly = true;
            //        gvCol4.ReadOnly = true;
            //        gvCol5.ReadOnly = true;
            //        gvCol6.ReadOnly = true;
            //        gvCol7.ReadOnly = true;
            //        gvCol8.ReadOnly = true;
            //        gvCol10.ReadOnly = true;

            //    }
            //    else
            //    {
            //        gvCol1.ReadOnly = true;
            //        gvCol2.ReadOnly = true;
            //        gvCol3.ReadOnly = true;
            //        gvCol4.ReadOnly = true;
            //        gvCol5.ReadOnly = true;
            //        gvCol6.ReadOnly = true;
            //        gvCol7.ReadOnly = true;
            //        gvCol8.ReadOnly = true;
            //        gvCol9.ReadOnly = true;
            //        gvCol10.ReadOnly = true;

            //    }


            //  }
        }
        protected void gvSListInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbSList"];
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            string postcode = this.ddlPOSTList.SelectedValue.ToString();
            string slnum = ((Label)this.gvSListInfo.Rows[e.RowIndex].FindControl("lblgvissue")).Text.Trim();
            bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "DELETESHORTLIST",
                        mADVNO, postcode, slnum, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvSListInfo.PageSize) * (this.gvSListInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("listisu<>''");
                ViewState["tbSList"] = dv.ToTable();
                this.Data_Bind();
            }
        }
        protected void gvIntInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblInterv"];
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            string postcode = this.ddlPOSTList.SelectedValue.ToString();
            string Intcode = ((Label)this.gvIntInfo.Rows[e.RowIndex].FindControl("lblgvIntCod")).Text.Trim();
            bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "DELETEINTLIST",
                        mADVNO, postcode, Intcode, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvIntInfo.PageSize) * (this.gvIntInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("intcode<>''");
                ViewState["tblInterv"] = dv.ToTable();
                this.Data_Bind();
            }
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)ViewState["tbSList"];
            for (int i = 0; i < this.gvSListInfo.Rows.Count; i++)
            {
                string chkvslno = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? "True" : "False";

                dt.Rows[i]["chkvno"] = chkvslno;


                ((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;
                ((LinkButton)this.gvSListInfo.Rows[i].FindControl("lbSelection")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;


            }
            ViewState["tbSList"] = dt;
        }

        protected void lbSelection_Click(object sender, EventArgs e)
        {

            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string postcode = code.Substring(0, 5).ToString();
            string slnum = code.Substring(5, 9).ToString();
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tbSList"];
            DataRow[] dr = dt.Select("postcode='" + postcode + "' and listisu='" + slnum + "'");


            //string listisu = dr[0]["listisu"].ToString().Trim();
            string Nselid = dr[0]["listisu"].ToString().Trim();
            string Chk = dr[0]["chkvno"].ToString();

            if (Chk == "False")
            {
                this.lblmsg1.Text = "Please Check CheckBox";
                return;
            }

            bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATESELECTION", mADVNO, postcode, Nselid, userid, Postdat, "", "", "", "",
                            "", "", "", "", "", "");

            if (!result)
            {
                this.lblmsg1.Text = RecData.ErrorObject["Msg"].ToString();
                return;
            }

            this.lblmsg1.Text = "Selection Successfully.";

        }
        protected void gvSListInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Save_Value();
            this.gvSListInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void ddlPrevAdvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPost();
        }
        protected void lbkButtonPrev_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblddlprevlist");
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string qtype = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETSHORTLISTPREV", CurDate1,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPOSTList.Items.Clear();
            this.ddlPrevlist.Items.Clear();
            this.ddlPrevlist.DataTextField = "advno1";
            this.ddlPrevlist.DataValueField = "advno";
            this.ddlPrevlist.DataSource = ds1.Tables[0];
            this.ddlPrevlist.DataBind();






            ViewState["tblddlprevlist"] = ds1.Tables[0];

            this.GetPost();
        }


        protected void gvSListInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int ID = Convert.ToInt32(gvSListInfo.DataKeys[e.RowIndex].Values["ID"].ToString());
            //FileUpload flimage = (FileUpload)grduser.Rows[e.RowIndex].FindControl("FileUpload1");
            //string image = Server.MapPath("~/img/") + Guid.NewGuid() + flimage.PostedFile.FileName;
            //flimage.PostedFile.SaveAs(image);
            //string fl = image.Substring(image.LastIndexOf("\\"));
            //string[] split = fl.Split('\\');
            //string newpath = split[1];
            //string imagepath = "~/img/" + newpath;
            //SqlCommand cmd = new SqlCommand("Update LINQ_TABLE set IMAGE= @IMAGE where ID=" + ID, con);
            //cmd.Parameters.AddWithValue("@IMAGE", imagepath);
            //cmd.ExecuteNonQuery();
            //grduser.EditIndex = -1;
            //bindgrid();
        }






        protected void lnkSendsmsemail_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            //DataTable dt = ((DataTable)ViewState["tbSList"]).Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("sendflag = 'False'");
            //dt = dv.ToTable();

            for (int i = 0; i < this.gvSListInfo.Rows.Count; i++)
            {

                string sendflag = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkSMSEmail")).Checked) ? "True" : "False";
                string gvIndate = ((TextBox)this.gvSListInfo.Rows[i].FindControl("txtgvCol8")).Text;
                string gvITime = ((TextBox)this.gvSListInfo.Rows[i].FindControl("txtgvCol9")).Text;
                string gvNumber = ((TextBox)this.gvSListInfo.Rows[i].FindControl("txtgvCol10")).Text;
                string gvEmail = ((TextBox)this.gvSListInfo.Rows[i].FindControl("txtgvCol11")).Text;
                string subject = "Interview Request";
                string customTxt = this.Message.InnerText.ToString();


                string senddisg = "HR";
                string datetime = gvIndate + " at " + gvITime;
                string postName = this.lblPost.Text.Trim();
                string user = "nahid@asit.com.bd";
                string pass = "asit321";
                string senderid = "ASITNAHID";  //Sender

                string SMSText = "DEAR CANDIDATE," + '\n' + customTxt + " " + datetime + " FOR THE POSITION " + postName + ". OUR OFFICE ADDRESS -" + comnam + "," + comadd + ". " + '\n' + "REGARDS" + '\n' + senddisg + '\n' + "www.asit.com.bd";

                if (sendflag == "True" && gvNumber != "")
                {
                    string mobile = "1111880" + ASTUtility.Right(gvNumber, 10).ToString(); //"880" + "1816049616";//this.txtMob.Text.ToString().Trim();

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://login.smsnet24.com/apimanager/sendsms?user_id=" + user + "&user_password=" + pass + "&route_id=" + 3
                  + "&sms_type_id=1" + "&sms_sender=" + senderid + "&sms_receiver=" + mobile + "&sms_text=" + SMSText + "&sms_category_name=General");

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                }

                if (sendflag == "True" && gvEmail != "")
                {

                    string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                    DataSet dssmtpandmail = this.RecData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

                    if (dssmtpandmail.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }

                    //SMTP
                    string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                    int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                    SmtpClient client = new SmtpClient(hostname, portnumber);
                    //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.EnableSsl = true;
                    client.EnableSsl = false;
                    string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
                    string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    ///////////////////////
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(frmemail);

                    msg.To.Add(new MailAddress(gvEmail.ToString()));
                    msg.Subject = subject;
                    msg.IsBodyHtml = true;

                    msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + SMSText + "<br/></pre></body></html>");
                    try
                    {
                        this.lblmsg1.Visible = true;
                        client.Send(msg);

                    }
                    catch (Exception ex)
                    {
                        this.lblmsg1.Visible = true;

                        this.lblmsg1.Text = "Error occured while sending your message." + ex.Message;
                    }

                }



                else
                {
                    this.lblmsg1.Visible = true;

                    this.lblmsg1.Text = "Fail";
                }

            }





        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["tblMFGSales_1"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("sack = 'False'");
            //dt = dv.ToTable();

            int i, index;
            if (((CheckBox)this.gvSListInfo.HeaderRow.FindControl("chkall")).Checked)
            {

                for (i = 0; i < this.gvSListInfo.Rows.Count; i++)
                {
                    string lblgvissue = ((Label)this.gvSListInfo.Rows[i].FindControl("lblgvissue")).Text;
                    string lblgvPostCod = ((Label)this.gvSListInfo.Rows[i].FindControl("lblgvPostCod")).Text;

                    ((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkSMSEmail")).Checked = true;


                }
            }

            else
            {
                for (i = 0; i < this.gvSListInfo.Rows.Count; i++)
                {
                    string lblgvissue = ((Label)this.gvSListInfo.Rows[i].FindControl("lblgvissue")).Text;
                    string lblgvPostCod = ((Label)this.gvSListInfo.Rows[i].FindControl("lblgvPostCod")).Text;

                    ((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkSMSEmail")).Checked = false;

                }

            }



        }


        private void GetCanList()
        {


            DataTable dt = (DataTable)ViewState["tbSList"];
            if (dt == null)
            {

                return;
            }



            DataView dv = dt.DefaultView;
            dv.RowFilter = ("cvname='CV Not Found'");

            DataTable dt1 = dv.ToTable();

            if (dt1.Rows.Count <= 0)
            {
                // this.pnlcv.Visible = false;
                return;
            }




            this.ddlCandidate.DataTextField = "col1";
            this.ddlCandidate.DataValueField = "listisu";
            this.ddlCandidate.DataSource = dt1;
            this.ddlCandidate.DataBind();
        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            try
            {
                string comcod = this.GetCompCode();

                string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString().Trim();
                string mPOSTCODE = this.ddlPOSTList.SelectedValue.ToString().Trim();

                string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                // i = i + 1;


                if (AsyncFileUpload1.HasFile)
                {
                    string candName = this.ddlCandidate.SelectedItem.ToString();
                    string empid = this.ddlCandidate.SelectedValue.ToString();


                    string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                    AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/CV/") + empid + "_" + candName + extension);
                    // AsyncFileUpload1.SaveAs(Server.MapPath("../../Uploads/") + empid + "_" + candName + extension);

                    Url = "~/Upload/CV/" + empid + "_" + candName + extension;
                    //AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/PROJECT/") + prjcode + random + extension);

                    //Url = "~/Upload/PROJECT/" + prjcode + random + extension;

                    bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "UPLOADECANDIDATECV", mADVNO, mPOSTCODE, empid, Url, "", "", "", "", "",
                                "", "", "", "", "", "");
                    if (result == true)
                    {
                        this.lblmsg1.Text = "CV Uploded";


                    }
                    else
                    {
                        this.lblmsg1.Text = "Uploded fail";

                    }

                }
                this.Get_SList_Info();

            }


            catch (Exception ex)
            {

                this.lblmsg1.Text = ex.Message;

            }


        }
        protected void ddlPrevlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.lbkButtonPrev_Click(null, null);

        }
    }
}
