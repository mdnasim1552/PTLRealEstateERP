using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using System.IO;

namespace RealERPWEB.F_09_PImp
{
    public partial class PurLabRequisition : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Entry") ? "Sub-Contractor Bill Requisition"
                    : (this.Request.QueryString["Type"].ToString() == "Edit") ? " Sub-Contractor Bill Requisition Edit" : "Labour Issue Information";



                this.GetProjectList();

                this.GetTrade();
                this.DateForOpeningBill();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.ibtnPreBillList_Click(null, null);
                    this.ddlPrevISSList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    this.lbtnOk_Click(null, null);
                }
                if(this.Request.QueryString["msrno"].ToString().Length > 0)
                {
                    this.getPreviousMSR();
                }
                

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

        protected void GetLabSuEntNo()
        {

            string comcod = this.GetCompCode();
            string mREQNO = "NEWMISS";
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();

            if (mREQNO == "NEWMISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_LAST_LBILL_REQ_NO", mREQDAT,
                        "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxmisuno"].ToString();
                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxmisuno1";
                    this.ddlPrevISSList.DataValueField = "maxmisuno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }
        }




        private void GetProjectList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //string pactcode = "%" + this.txtSrcPro.Text + "%";
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string pactcode = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? ("%16" + this.txtSrcPro.Text.Trim() + "%") : (this.Request.QueryString["prjcode"].ToString() + "%");
            string userid = hst["usrid"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST", pactcode, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();
        }

        private void GetTrade()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETTRADENAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddltrade.DataTextField = "tradedesc";
            this.ddltrade.DataValueField = "tradecod";
            this.ddltrade.DataSource = ds1.Tables[0];
            this.ddltrade.DataBind();

            ds1.Dispose();


        }
        private void DateForOpeningBill()
        {
            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "Opening")
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                this.txtCurISSDate.Enabled = false;

            }





        }


        protected void lnkPrint_Click(object sender, EventArgs e)

        {
            string comcod = this.GetCompCode();

            string lisuno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();

            ////string url ="~/F_99_Allinterface/";
            //string url =  HttpContext.Current.Request.Url.Authority + "//F_99_Allinterface/";
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open(' " + url + "PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode
            //           + "', target='_blank');</script>";


            //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            //string currentptah = "PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode;
            //string totalpath = hostname + currentptah;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            //lnkbtnPrint.NavigateUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode;

            string currentptah = this.ResolveUrl("~/F_99_Allinterface/PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode);
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + currentptah + "', target='_blank');</script>";


        }




        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVLISSUELIST", ProjectCode, CurDate1, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lisuno1";
            this.ddlPrevISSList.DataValueField = "lisuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)         // OK Button
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.ddlprjlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.lblCurISSNo1.Text = "LIS00-";
                this.txtCurISSNo2.Text = "";
                this.txtISSNarr.Text = "";
                this.lblBillno.Text = "";

                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.txtSrcPreBill.Visible = true;
                this.ibtnPreBillList.Visible = true;
                this.txtCurISSDate.Enabled = (this.Request.QueryString["Type"].ToString() == "Opening") ? false : true;
                this.ddlPrevISSList.Items.Clear();


                this.ddlfloorno.Items.Clear();
                DropCheck1.Items.Clear();
                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                this.lblBillno.Text = "";
                this.lblvalvounum.Text = "";
                this.txtSearchLabour.Text = "";
                // this.txtRefno.Text = "";
                //this.lbljavascript.Text = "";

                return;
            }


            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();

            this.ddlprjlist.Visible = false;
            this.lblddlProject.Visible = true;

            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            this.txtSrcPreBill.Visible = false;
            this.ibtnPreBillList.Visible = false;

            this.PnlRes.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.GetFloorCode();
            this.GetCataGory();
            this.Get_Issue_Info();
            //this.PenelVisiblity();


        }

        //private void PenelVisiblity ( )
        //{
        //    string comcod = this.GetCompCode ();
        //    switch (comcod)
        //    {
        //        case "3336":
        //        case "3337":
        //            this.pnlsecurity.Visible = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void GetFloorCode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlfloorno.DataTextField = "flrdes";
            this.ddlfloorno.DataValueField = "flrcod";
            this.ddlfloorno.DataSource = ds1.Tables[0];
            this.ddlfloorno.DataBind();
            this.ddlfloorno_SelectedIndexChanged(null, null);

        }
        protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCataGory();
            // this.GetMaterials();
        }

        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {
            this.GetMaterials();
        }



        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string reqno = "NEWMISS";
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                // this.ddlRA.Enabled = false;
                reqno = this.ddlPrevISSList.SelectedValue.ToString();
            }
            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_PURLAB_REQ_INFO", reqno, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblbillreq"] = ds1.Tables[0];

            if (reqno == "NEWMISS")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_LAST_LBILL_REQ_NO", CurDate1,
                      "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                }


                //string SearchMat = "%";
                //DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, CurDate1, "", SearchMat, "", "", "", "", "");
                //ViewState["tblbillreq"] = ds2.Tables[0];
                //if (ds2 == null)
                //    return;
                //ds2.Dispose();

                //this.grvissue_DataBind();
                return;
            }

            this.lblfloorno.Visible = true;
            this.ddlfloorno.Visible = true;
            // this.txtSearchLabour.Visible = true;
            //this.ibtnSearchMaterisl.Visible = true;
            DropCheck1.Visible = true;
            this.lbtnSelect.Visible = true;
            this.txtRefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["lreqno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["lreqno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd-MMM-yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.txtISSNarr.Text = Convert.ToString(ds1.Tables[1].Rows[0]["rmrks"]);

            ViewState["tbl_contractor"] = ds1.Tables[2];
            this.lblBillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.ddltrade.SelectedValue = ds1.Tables[1].Rows[0]["tradecod"].ToString();




            double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)ViewState["tblbillreq"]).Compute("sum(reqamt)", "")) ? 0.00 : ((DataTable)ViewState["tblbillreq"]).Compute("sum(reqamt)", "")));





            this.grvissue_DataBind();

            // ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = (ds1.Tables[1].Rows[0]["billno"].ToString() == "00000000000000");

        }


        private string ComCalltype()
        {

            string withmat = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335":
                case "3333":

                    // case "3101":
                    withmat = "WithMat";
                    break;

                //case "3101":
                //case"3336":
                //case"3337":
                //case"3344":
                //    withmat = "";
                //    break;


                default:
                    withmat = "";
                    break;




            }

            return withmat;


        }


        private string ComZeroBal()
        {

            string zerobal = "";
            string comcod = this.GetCompCode();
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


        private void GetCataGory()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = "%" + this.txtSearchLabour.Text.Trim() + "%";
            string withmat = this.ComCalltype();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCATAGORY", pactcode, date, flrcode, SearchMat, withmat, "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlcatagory.DataValueField = "rsircode";
            this.ddlcatagory.DataTextField = "rsirdesc";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
            this.GetMaterials();



        }

        private void GetMaterials()
        {
            //DropCheck1.Text = "";
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string SearchMat = "%"+this.txtSearchLabour.Text.Trim() + "%";
            string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%" : this.ddlcatagory.SelectedValue.Substring(0, 4) + "%";
            //string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%" : this.ddlcatagory.SelectedValue.Substring(0, 9) + "%";


            string withmat = this.ComCalltype();
            string zerobal = this.ComZeroBal();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, date, flrcode, SearchMat, withmat, zerobal, "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.DropCheck1.DataTextField = "rsirdesc1";
            this.DropCheck1.DataValueField = "rsircode";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();


        }
        private void GetChargingItem()
        {


            //DropCheck1.Text = "";
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string SearchMat = "%"+this.txtSearchLabour.Text.Trim() + "%";
            string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%" : this.ddlcatagory.SelectedValue.Substring(0, 4) + "%";

            string withmat = this.ComCalltype();
            string zerobal = this.ComZeroBal();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCHARGINGITEM", "", "", "", "", "", "", "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.DropCheck1.DataTextField = "rsirdesc1";
            this.DropCheck1.DataValueField = "rsircode";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();


        }
        protected void grvissue_DataBind()
        {
            string comcod = this.GetCompCode();
            if (this.Request.QueryString["Type"] == "CSApproval")
            {
                this.grvissue.Columns[14].Visible = true;
                this.grvissue.Columns[15].Visible = true;
            }

            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvissue.DataSource = (DataTable)ViewState["tblbillreq"];
            this.grvissue.DataBind();


            this.FooterCalculaton();

        }

        private void FooterCalculaton()
        {

            DataTable dt = (DataTable)ViewState["tblbillreq"];
            if (dt.Rows.Count == 0)
                return;




            ((Label)this.grvissue.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ? 0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); ");



        }


        protected void lbtnSelect_Click(object sender, EventArgs e)         // Select Button
        {


            try
            {
                this.SaveValue();
                string flrcode = this.ddlfloorno.SelectedValue.ToString().Trim();
                string flrdes = this.ddlfloorno.SelectedItem.Text.Trim();

                foreach (ListItem lab1 in DropCheck1.Items)
                {
                    if (lab1.Selected)
                    {
                        string rsircode = lab1.Value;


                        // string rsirdesc = lab1.Substring(13);

                        DataTable dt = (DataTable)ViewState["tblbillreq"];
                        DataRow[] dr = dt.Select(" flrcod='" + flrcode + "' and rsircode='" + rsircode + "'");

                        DataTable dt1 = (DataTable)ViewState["itemlist"];
                        if (dr.Length == 0)
                        {

                            DataRow dr1 = dt.NewRow();
                            dr1["flrcod"] = flrcode;
                            dr1["flrdes"] = flrdes;
                            dr1["rsircode"] = rsircode;
                            dr1["rsirdesc"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirdesc"];
                            //dr1["grp"] = grp;
                            //dr1["grpdesc"] = grpdesc;
                            dr1["rsirunit"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                            dr1["bgdqty"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bgdqty"]).ToString(); ;

                            dr1["balqty"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["balqty"]).ToString();
                            dr1["balamt"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["balamt"]).ToString();
                            dr1["reqqty"] = 0.00;
                            dr1["bgdrat"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bgdrat"]).ToString();
                            dr1["reqrat"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["isurat"]).ToString();
                            dr1["amount"] = 0.00;
                            dr1["reqamt"] = 0.00;
                            dr1["csircode"] = "000000000000";
                            dr1["approve"] = false;
                            dt.Rows.Add(dr1);

                        }
                        ViewState["tblbillreq"] = dt;
                        this.grvissue_DataBind();
                    }
                }
            }

            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }



        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();

        }
        protected void lnkCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double dgvQty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;

                dt.Rows[TblRowIndex]["reqqty"] = dgvQty;
                dt.Rows[TblRowIndex]["reqrat"] = labrate;
                dt.Rows[TblRowIndex]["reqamt"] = labrate * dgvQty;


            }
            ViewState["tblbillreq"] = dt;
            this.grvissue_DataBind();
        }






        protected void lnkupdate_Click(object sender, EventArgs e)      // Update Button
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;





            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);




            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblbillreq"];
            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetLabSuEntNo();
            }
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Refno = this.txtRefno.Text.Trim();



            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mCONCODE = "";
            string mISURNAR = this.txtISSNarr.Text.Trim();

            string trade = this.ddltrade.SelectedValue.ToString();




            if (Refno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fill Ref No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            //string appxml = tbl2.Rows[0]["approval"].ToString();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATE_PUR_LAB_REQUISITION_INFO", "PURLISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mISURNAR, Refno, usrid, Sessionid, trmid, trade);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            foreach (DataRow dr in tbl2.Rows)
            {
                string Flrcod = dr["flrcod"].ToString();
                //  string grp = dr["grp"].ToString();
                string Rsircode = dr["rsircode"].ToString();
                double reqqty = Convert.ToDouble(dr["reqqty"].ToString().Trim());
                string reqamt = dr["reqamt"].ToString().Trim();
                double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());

                string csircode = dr["csircode"].ToString();
                string approve = dr["approve"].ToString();
                if (balqty >= reqqty)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATE_PUR_LAB_REQUISITION_INFO", "PURLISSUEA", mISUNO, Flrcod,
                        Rsircode, reqqty.ToString(), reqamt, csircode, approve);
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                // if (Isuqty > 0)


            }
            this.txtCurISSDate.Enabled = false;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Requistion Information";
                string eventdesc = "Update Labour Bill Requisition QTY & RATE";
                string eventdesc2 = "Req No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void SaveValue()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            int TblRowIndex;
            // double labrate
            double adedamt = 0.00;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {



                string rsircode = ((Label)this.grvissue.Rows[i].FindControl("lblitemcode")).Text.Trim();
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double dgvQty = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                // double balamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalamt")).Text.Trim()));

                double amount = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtgvamount")).Text.Trim());
                string csircode = ((DropDownList)this.grvissue.Rows[i].FindControl("DdlContractor")).SelectedValue.ToString();
                CheckBox approve = (CheckBox)this.grvissue.Rows[i].FindControl("gvCheckBoxAppve");

                bool aprvstatus = false;
                if (approve.Checked)
                {
                    aprvstatus = true;
                }
                else
                {
                    aprvstatus = false;
                }

                string comcod = this.GetCompCode();
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;


                amount = amount > 0 ? amount : dgvQty * labrate;
                labrate = amount > 0 ? amount / dgvQty : labrate;

                dt.Rows[TblRowIndex]["reqqty"] = dgvQty;
                dt.Rows[TblRowIndex]["reqrat"] = labrate;
                dt.Rows[TblRowIndex]["reqamt"] = amount;
                dt.Rows[TblRowIndex]["amount"] = amount;
                dt.Rows[TblRowIndex]["csircode"] = csircode;
                dt.Rows[TblRowIndex]["approve"] = aprvstatus;


            }
            ViewState["tblbillreq"] = dt;
        }


        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Labcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string Flrcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvflrCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "DELETELAB_REQUISITION_ITEM", mISUNO, Flrcode, Labcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblbillreq");
            ViewState["tblbillreq"] = dv.ToTable();
            this.grvissue_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Requistion Information";
                string eventdesc = "Delete Requsition Item";
                string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "REQ No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
                        ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectList();
        }

        protected void ibtnPreBillList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETPREVL_LABBILL_LIST", ProjectCode, CurDate1, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lreqno1";
            this.ddlPrevISSList.DataValueField = "lreqno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "DELETE_ALL_ITEM_LBILL_REQUISITION", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterials();
        }


        protected void grvissue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dlist = (DropDownList)e.Row.FindControl("DdlContractor");
                string contractor = ((Label)e.Row.FindControl("LblGvContractor")).Text.ToString();
                DataTable dt = (DataTable)ViewState["tbl_contractor"];
                dlist.DataTextField = "ssirdesc";
                dlist.DataValueField = "ssircode";
                dlist.DataSource = dt;
                dlist.DataBind();
                dlist.SelectedValue = this.Request.QueryString["recomsup"].ToString()=="" ? contractor : this.Request.QueryString["recomsup"].ToString();

                bool aprovestatus = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "approve"));
                CheckBox approve = (CheckBox)e.Row.FindControl("gvCheckBoxAppve");

                if (aprovestatus == true)
                {
                    approve.Checked = true;
                }
                else
                {
                    approve.Checked = false;
                }


            }
        }

        private void getPreviousMSR()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mMSRNo = this.Request.QueryString["msrno"].ToString() == "" ? "" : this.Request.QueryString["msrno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO1CON", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblt01"] = ds1.Tables[1];
            Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);
            Session["tblterm"] = ds1.Tables[3];


            this.gvMSRInfo_DataBind();
            this.Payterm_DataBind();


        }

        protected void gvMSRInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtrate1 = (TextBox)e.Row.FindControl("txtrate1");
                TextBox txtrate2 = (TextBox)e.Row.FindControl("txtrate2");
                TextBox txtrate3 = (TextBox)e.Row.FindControl("txtrate3");
                TextBox txtrate4 = (TextBox)e.Row.FindControl("txtrate4");
                TextBox txtrate5 = (TextBox)e.Row.FindControl("txtrate5");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(code, 2) == "71")
                {
                    txtrate1.Style.Add("text-align", "Left");
                    txtrate2.Style.Add("text-align", "Left");
                    txtrate3.Style.Add("text-align", "Left");
                    txtrate4.Style.Add("text-align", "Left");
                    txtrate5.Style.Add("text-align", "Left");
                }

            }
        }

        protected void gvMSRInfo2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 5;
                gvrow.Cells.Add(cell0);
                DataTable dt = (DataTable)Session["tblt01"];
                //int j = 5;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TableCell cell = new TableCell();
                    cell.Text = dt.Rows[i]["ssirdesc1"].ToString();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    gvrow.Cells.Add(cell);

                }
                TableCell celll = new TableCell();
                celll.Text = "";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 2;
                gvrow.Cells.Add(celll);

                gvMSRInfo2.Controls[0].Controls.AddAt(0, gvrow);
            }

        }

        protected void gvMSRInfo_DataBind()
        {
            this.gvMSRInfo2.DataSource = (DataTable)Session["tblt02"];
            this.gvMSRInfo2.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblt02"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
          : dt.Compute("Sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
        : dt.Compute("Sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
        : dt.Compute("Sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
        : dt.Compute("Sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00);  ");

            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
        : dt.Compute("Sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00);  ");



        }

        private void Payterm_DataBind()
        {
            this.gvterm.DataSource = (DataTable)Session["tblterm"];
            this.gvterm.DataBind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            DataView dv = dt1.DefaultView;
            dv.Sort = ("rsircode");
            dt1 = dv.ToTable();
            return dt1;
        }

        protected void DdlContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < this.grvissue.Rows.Count; i++)
            //{
            //    string contrator = ((DropDownList)this.grvissue.Rows[i].FindControl("DdlContractor")).SelectedItem.Text.Trim();
            //    string txtlabrate = ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.ToString();
            //    string rsircode = ((Label)this.grvissue.Rows[i].FindControl("lblitemcode")).Text.ToString();

            //    DataTable dt = (DataTable)ViewState["tblbillreq"]; /// gridview main 
            //    DataTable dt2 = (DataTable)Session["tblt02"]; // cs 
                

            //    DataRow[] drstk = dt2.Select(" rsircode='" + rsircode + "' and csircode='" + contrator + "'");

            //    for (int j = 0; j < drstk.Length; j++)
            //    {

            //        DataRow dr2 = dt.NewRow();
                 
            //        dr2["reqrat"] = stkqty * stkrate;
                   
            //        dt.Rows.Add(dr2);

            //    }


            //}
        }
    }
}