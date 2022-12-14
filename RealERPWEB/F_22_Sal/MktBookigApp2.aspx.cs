using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
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
using System.Drawing;
using System.IO;
using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class MktBookigApp2 : System.Web.UI.Page
    {
        decimal cinsamount = 0;
        decimal payamount = 0;
        ProcessAccess SalData = new ProcessAccess();
        public static string Url = "";
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "BOOKING APPLICATION";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtbookdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetProjectName();
                this.GetMaxCustNumber();
                this.GetSalesName();
            }


            if (imgFileUpload.HasFile)
            {
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                string extension = Path.GetExtension(imgFileUpload.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                imgFileUpload.SaveAs(Server.MapPath("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;
                EmpImg.ImageUrl = Url;
                Session["imgUrl"] = Url;
            }
            if (imgFileUploadN.HasFile)
            {
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string gcode = this.lblgcode.Text.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                string extension = Path.GetExtension(imgFileUploadN.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                imgFileUploadN.SaveAs(Server.MapPath("~/Upload/CUSTOMER/") + pactcode + usircode + gcode + random + extension);

                Url = "~/Upload/CUSTOMER/" + pactcode + usircode + gcode + random + extension;
                Session["imgUrl2"] = Url;
                this.SaveValueImage(gcode, Url);
            }

            if (imgUploadNominee.HasFile)
            {
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                string extension = Path.GetExtension(imgUploadNominee.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                imgUploadNominee.SaveAs(Server.MapPath("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;
                ImageNominee.ImageUrl = Url;
                Session["imgNomineeUrl"] = Url;
            }

            if (imgUploadCorrespondent.HasFile)
            {
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                string extension = Path.GetExtension(imgUploadCorrespondent.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                imgUploadCorrespondent.SaveAs(Server.MapPath("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;
                ImageCorrespondent.ImageUrl = Url;
                Session["imgCorrespondentUrl"] = Url;
            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void GetSalesName()
        {
            try
            {
                string comcod = this.GetCompCode();

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string custid = this.ddlCustName.SelectedValue.ToString();

                DataSet ds = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "GETSALESNAME", pactcode, custid, "", "", "", "", "", "", "");
                if (ds == null)
                {
                    return;
                }
                Session["salesname"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        private void GetMaxCustNumber()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable dt1 = (DataTable)Session["tblcustinfo"];
                if (dt1 == null)
                    return;
                string applicationDate = (dt1.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt1.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");

                DataSet ds = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "MAXCUTOMERNUMBER", applicationDate, "", "", "", "", "", "", "", "");
                DataTable dt = ds.Tables[0];
                this.txtCustmerNumber.Text = (dt.Rows.Count) == 0 ? "" : dt.Rows[0]["customerno"].ToString();
                this.txtCustmerNumber.Enabled = false;
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void GetProjectName()
        {
            try
            {
                string comcod = this.GetCompCode();
                string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
                DataSet ds1 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = ds1.Tables[0];
                this.ddlProjectName.DataBind();
                ds1.Dispose();
                this.ddlProjectName_SelectedIndexChanged(null, null);
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void GetCustomerName()
        {
            try
            {
                string comcod = this.GetCompCode();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
                DataSet ds2 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "DETAILSIRINFINFORMATION", pactcode, txtSProject, "", "", "", "", "", "", "");
                this.ddlCustName.DataTextField = "udesc";
                this.ddlCustName.DataValueField = "usircode";
                this.ddlCustName.DataSource = ds2.Tables[0];
                this.ddlCustName.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.GetCustomerName();
            //this.lbtnOk_Click(null, null);
        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lbtnOk.Text == "Ok")
                {
                    this.lbtnOk.Text = "New";
                    this.ddlProjectName.Enabled = false;
                    this.ddlCustName.Enabled = false;
                    this.MultiView1.ActiveViewIndex = 0;

                    this.GetMaxCustNumber();
                    this.ShowData();
                    return;
                }
                this.lbtnOk.Text = "Ok";
                this.ddlProjectName.Enabled = true;
                this.ddlCustName.Enabled = true;
                this.MultiView1.ActiveViewIndex = -1;
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void ShowData()
        {

            Session.Remove("tblprjinfo");
            Session.Remove("tblperinfo");
            Session.Remove("tblnomineeinfo");
            Session.Remove("tblnominatedinfo");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();

            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "GETBOOKINGAPPLICATION", pactcode, custid, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();
                return;
            }

            Session["tblprjinfo"] = ds2.Tables[0];
            Session["tblperinfo"] = ds2.Tables[1];
            Session["tblcustinfo"] = ds2.Tables[2];
            Session["tblnomineeinfo"] = ds2.Tables[3];
            Session["tblnominee"] = ds2.Tables[4];
            Session["tblnominatedinfo"] = ds2.Tables[5];
            Session["tblnominated"] = ds2.Tables[6];
            Session["tblpricedetail"] = ds2.Tables[7];
            Session["tblprice"] = ds2.Tables[8];
            Session["tblrmrkdetail"] = ds2.Tables[9];
            Session["tblrmrk"] = ds2.Tables[10];


            Session["tblcustinfo"] = ds2.Tables[2];
            DataTable dt = ds2.Tables[2];

            this.EmpImg.ImageUrl = (dt.Rows.Count) == 0 ? "" : dt.Rows[0]["custimg"].ToString();
            this.ImageNominee.ImageUrl = (dt.Rows.Count) == 0 ? "" : dt.Rows[0]["nomineeimg"].ToString();
            this.ImageCorrespondent.ImageUrl = (dt.Rows.Count) == 0 ? "" : dt.Rows[0]["correspondentimg"].ToString();

            //  appdate, bookamt, bankname, bbranch, paydate, intavail, paymode
            this.txtdate.Text = (dt.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");
            this.TextBookingAmt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");
            this.txtCheqNo.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["chequeno"].ToString();
            this.txtbankname.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["bankname"].ToString();
            this.txtbankbranch.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["bbranch"].ToString();
            this.txtbookdate.Text = (dt.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[0]["paydate"]).ToString("dd-MMM-yyyy");
            this.Textinsamt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["insamptpermonth"]).ToString("#,##0;(#,##0); ");
            this.TxtNoTInstall.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["totalinstallment"]).ToString("#,##0;(#,##0); ");
            this.txtrcvbookingam.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["rcvbookingamt"]).ToString("#,##0;(#,##0); ");

            this.cblintavailloan.SelectedValue = (dt.Rows.Count == 0) ? "No" : dt.Rows[0]["intavail"].ToString();
            this.cblpaytype.SelectedValue = (dt.Rows.Count == 0) ? "OneTime" : dt.Rows[0]["paymode"].ToString();

            if (dt.Rows.Count > 0 && dt.Rows[0]["customerno"].ToString().Length > 0)
            {
                this.txtCustmerNumber.Text = dt.Rows[0]["customerno"].ToString();
            }


            ds2.Dispose();
            this.Data_BindPrj();
            this.Data_BindPer();
            this.Data_BindNominee();
            this.Data_BindNominated();
            this.Data_BindPriceDetail();
            this.Data_BindRmrkDetail();
        }

        private void Data_BindPrj()
        {
            DataTable dt = (DataTable)Session["tblprjinfo"];
            if (dt.Rows.Count == 0)
                return;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            this.GridTextDDLVisible();
        }
        private void Data_BindPer()
        {
            DataTable dt = (DataTable)Session["tblperinfo"];
            if (dt.Rows.Count == 0)
                return;
            this.gvperinfo.DataSource = dt;
            this.gvperinfo.DataBind();
            this.GridTextDDLVisiblePer();
        }

        private void Data_BindNominee()
        {
            DataTable dt = (DataTable)Session["tblnomineeinfo"];
            if (dt.Rows.Count == 0)
                return;
            this.GridViewNominee.DataSource = dt;
            this.GridViewNominee.DataBind();
            this.GridTextDDLVisibleNominee();
        }

        private void Data_BindNominated()
        {

            DataTable dt = (DataTable)Session["tblnominatedinfo"];
            if (dt.Rows.Count == 0)
                return;
            this.GridViewNominated.DataSource = dt;
            this.GridViewNominated.DataBind();
            this.GridTextDDLVisibleNominated();
        }

        private void Data_BindPriceDetail()
        {

            DataTable dt = (DataTable)Session["tblpricedetail"];
            if (dt.Rows.Count == 0)
                return;
            this.GridViewPriceDetail.DataSource = dt;
            this.GridViewPriceDetail.DataBind();
        }

        private void Data_BindRmrkDetail()
        {
            DataTable dt = (DataTable)Session["tblrmrkdetail"];
            if (dt.Rows.Count == 0)
                return;
            this.GridViewRemarks.DataSource = dt;
            this.GridViewRemarks.DataBind();
            //this.GridTextDDLVisibleRmrk();
        }

        private void GridTextDDLVisible()
        {
            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tblprjinfo"]).Copy();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "65001": //Type                  
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((CheckBoxList)this.gvProjectInfo.Rows[i].FindControl("cbldesc")).Visible = true;
                        CheckBoxList cbl = ((CheckBoxList)this.gvProjectInfo.Rows[i].FindControl("cbldesc"));
                        cbl.SelectedValue = (val == "") ? "Apartment" : val;
                        break;
                    default:
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((CheckBoxList)this.gvProjectInfo.Rows[i].FindControl("cbldesc")).Visible = false;
                        break;
                }
            }
        }

        private void GridTextDDLVisiblePriceDetail()
        {
            ((TextBox)this.GridViewPriceDetail.Rows[6].FindControl("txtgvValAmount")).Visible = false;
        }
        private void GridTextDDLVisibleNominee()
        {

            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tblnomineeinfo"]).Copy();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01118":
                    case "01109": //Birthdate                 
                        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtValNominee")).Visible = false;
                        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValNominee")).Visible = true;
                        break;

                    default:
                        break;

                }
            }
        }


        private void GridTextDDLVisibleNominated()
        {

            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tblnominatedinfo"]).Copy();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {

                    case "01307":
                        ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvValNominated")).Visible = false;
                        ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvdValNominated")).Visible = true;
                        break;

                    default:

                        break;
                }
            }




        }
        private void GridTextDDLVisiblePer()
        {

            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tblperinfo"]).Copy();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01031":
                    case "01009": //Birthdate                 
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = true;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = false;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                        break;
                    default:
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = false;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = false;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                        break;
                }
            }
        }
        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblprjinfo"];
            DataTable dtp = (DataTable)Session["tblperinfo"];
            DataTable dtn = (DataTable)Session["tblnomineeinfo"];
            DataTable dtntd = (DataTable)Session["tblnominatedinfo"];
            DataTable dtpdt = (DataTable)Session["tblpricedetail"];
            DataTable dtRMRK = (DataTable)Session["tblrmrkdetail"];





            for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();



                if (Gcode == "65001")
                {
                    CheckBoxList cbldesc = (CheckBoxList)this.gvProjectInfo.Rows[i].FindControl("cbldesc");

                    for (int j = 0; j < cbldesc.Items.Count; j++)
                        if (cbldesc.Items[j].Selected)
                        {
                            Gvalue = cbldesc.Items[j].Value.ToString();
                            break;
                        }

                }
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                dt.Rows[i]["gdesc1"] = Gvalue;
            }

            Session["tblprjinfo"] = dt;


            for (int i = 0; i < this.gvperinfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvperinfo.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string gtype = ((Label)this.gvperinfo.Rows[i].FindControl("lgvgvalper")).Text.Trim();
                string Gvalue = ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Text.Trim();
                if (Gcode == "01027")
                {
                    CheckBoxList cbldesc = (CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper");

                    for (int j = 0; j < cbldesc.Items.Count; j++)
                        if (cbldesc.Items[j].Selected)
                        {
                            Gvalue = cbldesc.Items[j].Value.ToString();
                            break;
                        }
                }

                if (Gcode == "01009" || Gcode == "01031")
                {

                    Gvalue = (((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim();
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                dtp.Rows[i]["gdesc1"] = Gvalue;
            }

            Session["tblperinfo"] = dtp;




            for (int i = 0; i < this.GridViewNominee.Rows.Count; i++)
            {
                //string Gcode = ((Label)this.GridViewNominee.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                ////string gtype = ((Label)this.GridViewNominee.Rows[i].FindControl("lgvgvalper")).Text.Trim();
                //string Gvalue = ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtValNominee")).Text.Trim();
                //dtn.Rows[i]["gdesc1"] = Gvalue;



                string Gcode = ((Label)this.GridViewNominee.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string gtype = ((Label)this.GridViewNominee.Rows[i].FindControl("lgvgvalNominee")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtValNominee")).Text.Trim();



                if (Gcode == "01109" || Gcode == "01118")
                {

                    Gvalue = (((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValNominee")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValNominee")).Text.Trim();
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                dtn.Rows[i]["gdesc1"] = Gvalue;

            }

            Session["tblnomineeinfo"] = dtn;


            for (int i = 0; i < this.GridViewNominated.Rows.Count; i++)
            {
                //string Gcode = ((Label)this.GridViewNominated.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                //string Gvalue = ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvValNominated")).Text.Trim();



                string Gcode = ((Label)this.GridViewNominated.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string gtype = ((Label)this.GridViewNominated.Rows[i].FindControl("lgvgvalNominated")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvValNominated")).Text.Trim();


                if (Gcode == "01307")
                {

                    Gvalue = (((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvdValNominated")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvdValNominated")).Text.Trim();
                }
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;

                dtntd.Rows[i]["gdesc1"] = Gvalue;
            }
            Session["tblnominatedinfo"] = dtntd;




            for (int i = 0; i < this.GridViewPriceDetail.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GridViewPriceDetail.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewPriceDetail.Rows[i].FindControl("txtgvValAmount")).Text.Trim();
                //if (Gcode == "01027")
                //{
                //    CheckBoxList cbldesc = (CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper");

                //    for (int j = 0; j < cbldesc.Items.Count; j++)
                //        if (cbldesc.Items[j].Selected)
                //        {
                //            Gvalue = cbldesc.Items[j].Value.ToString();
                //            break;
                //        }
                //}

                //if (Gcode == "01009" || Gcode == "01010")
                //{
                //    Gvalue = (((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim();
                //}

                //Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                dtpdt.Rows[i]["amount"] = Convert.ToDouble("0" + Gvalue).ToString();
            }

            Session["tblpricedetail"] = dtpdt;





            for (int i = 0; i < this.GridViewRemarks.Rows.Count; i++)
            {
                //string Gcode = ((Label)this.GridViewNominated.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                //string Gvalue = ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvValNominated")).Text.Trim();



                string Gcode = ((Label)this.GridViewRemarks.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string gtype = ((Label)this.GridViewRemarks.Rows[i].FindControl("lgvgvalRmrk")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewRemarks.Rows[i].FindControl("txtgvValRmrk")).Text.Trim();


                //if (Gcode == "01307")
                //{

                //    Gvalue = (((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvdValNominated")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvdValNominated")).Text.Trim();
                //}
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;

                dtRMRK.Rows[i]["gdesc1"] = Gvalue;
            }
            Session["tblrmrkdetail"] = dtRMRK;

        }

        private void SaveValueImage(string _gcode, string _url)
        {

            DataTable dtp = (DataTable)Session["tblperinfo"];

            for (int i = 0; i < this.gvperinfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvperinfo.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                string gtype = ((Label)this.gvperinfo.Rows[i].FindControl("lgvgvalper")).Text.Trim();
                string Gvalue = ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Text.Trim();
                if (Gcode == _gcode)
                {
                    Gvalue = _url;
                }
                dtp.Rows[i]["gdesc1"] = Gvalue;
            }
            Session["tblperinfo"] = dtp;
            this.Data_BindPer();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            if (this.saleDeclaration.Checked)
            {
                this.PrintSaleDeclaration();
            }
            else
            {
                this.PrintBookingApplication();
            }
        }

        private void PrintSaleDeclaration()
        {
            this.ShowData();
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comfadd = hst["comadd"].ToString().Replace("<br />", "\n");
            string comadd1 = "81, S. S. Khaled Road, Jamal Khan, Chattogram. Phone: +8802333354442, 02333354443, 02333351443";
            string contactCommunication = "Mobile: +8801755663636. E-mail: mail@cpdl.com.bd, Web: www.cpdl.com.bd";
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string modeofpay = this.cblpaytype.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text;
            DataTable dt2 = (DataTable)Session["tblcustinfo"];
            DataTable dt3 = (DataTable)Session["tblprice"];
            DataTable dt10 = (DataTable)Session["tblrmrk"];
            DataTable dtSalname = (DataTable)Session["salesname"];

            string customerno = dt2.Rows[0]["customerno"].ToString() == "" ? dt2.Rows[0]["usircode"].ToString() : dt2.Rows[0]["custno"].ToString();

            var list = dt2.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustBookApp2>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_22_Sal.RptSaleDeclaration", list, "", "");
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd1", comadd1));
            Rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("comfadd", comfadd));
            Rpt1.SetParameters(new ReportParameter("contactCommunication", contactCommunication));
            Rpt1.SetParameters(new ReportParameter("enrolmentdate", Convert.ToDateTime(dt2.Rows[0]["appdate"]).ToString("ddMMyyyy")));
            Rpt1.SetParameters(new ReportParameter("customerno", customerno));
            Rpt1.SetParameters(new ReportParameter("usircode", dt2.Rows[0]["usircode"].ToString()));
            Rpt1.SetParameters(new ReportParameter("bookingno", dt2.Rows[0]["bookingno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("customername", dt2.Rows[0]["fullname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("contactno", dt2.Rows[0]["mobilenum"].ToString()));
            Rpt1.SetParameters(new ReportParameter("address", dt2.Rows[0]["presentaddr"].ToString()));
            Rpt1.SetParameters(new ReportParameter("propertyname", projectName));
            Rpt1.SetParameters(new ReportParameter("floor", dt2.Rows[0]["floorr"].ToString()));
            Rpt1.SetParameters(new ReportParameter("unit", dt2.Rows[0]["parkingLevel"].ToString()));
            Rpt1.SetParameters(new ReportParameter("size", dt2.Rows[0]["size"].ToString()));
            Rpt1.SetParameters(new ReportParameter("propertyaddress", dt3.Rows[0]["propertyAddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("remarks", dt10.Rows[0]["remarks"].ToString()));
            Rpt1.SetParameters(new ReportParameter("salesname", dtSalname.Rows.Count > 0 ? dtSalname.Rows[0]["salesname"].ToString() : ""));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintBookingApplication()
        {
            this.ShowData();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTINFORMATION", pactcode, custid, "", "", "", "", "", "", "");

            string ProjectName = this.ddlProjectName.SelectedItem.Text.Trim().ToString().Substring(13);
            string UnitName = this.ddlCustName.SelectedItem.Text.Trim().ToString();
            string appdate = Convert.ToDateTime(this.txtdate.Text).ToString("ddMMyyyy");
            //double bookamt = this.TextBookingAmt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["
            //"]).ToString("#,##0;(#,##0); ");
            //string chequeno = this.txtCheqNo.Text.Trim();
            string bankname = this.txtbankname.Text.Trim();
            //string branch = this.txtbankbranch.Text.Trim();
            string bookdate = Convert.ToDateTime(this.txtbookdate.Text).ToString("dd-MMM-yyyy");
            //string inttoavailloan = this.cblintavailloan.SelectedValue.ToString();
            //string modeofpay = this.cblpaytype.SelectedValue.ToString();


            //ViewState["tblperinfo"] = ds2.Tables[1];
            //ViewState["tblcustinfo"] = ds2.Tables[2];
            //DataTable dt = ds2.Tables[2];
            ////  appdate, bookamt, bankname, bbranch, paydate, intavail, paymode
            //this.txtdate.Text = (dt.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");
            //this.TextBookingAmt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");

            string inttoavailloan = this.cblintavailloan.SelectedValue.ToString();
            string modeofpay = this.cblpaytype.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text;
            DataTable dt1 = (DataTable)Session["tblpricedetail"];
            DataTable dt2 = (DataTable)Session["tblcustinfo"];
            DataTable dt3 = (DataTable)Session["tblprice"];
            DataTable dt4 = (DataTable)Session["tblnominee"];
            DataTable dt5 = (DataTable)Session["tblnominated"];
            DataTable dt10 = (DataTable)Session["tblrmrk"];








            string custimg = new Uri(Server.MapPath(dt2.Rows[0]["custimg"].ToString())).AbsoluteUri;
            string nomineeimg = new Uri(Server.MapPath(dt2.Rows[0]["nomineeimg"].ToString())).AbsoluteUri;
            string correspondentimg = new Uri(Server.MapPath(dt2.Rows[0]["correspondentimg"].ToString())).AbsoluteUri;

            double inword = Convert.ToDouble(dt2.Rows[0]["bookamt"]);
            double percntamt = Convert.ToDouble(dt1.Select("Code='07'")[0]["amount"]);

            //DataTable dt = ds2.Tables[0];
            //DataTable dt = (DataTable)Session["tblcustinfo"];

            //DataTable dt2 = (DataTable)ViewState["tblcustinfo"];




            var list = dt2.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustBookApp2>();
            var list2 = dt4.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptNomineeBookApp2>();
            var list3 = dt5.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptNominatedBookApp2>();
            LocalReport Rpt1 = new LocalReport();



            Rpt1 = RDLCAccountSetup.GetLocalReport("R_22_Sal.RptBookingApp2", list, list2, list3);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comlogo", comlogo));

            //Rpt1.SetParameters(new ReportParameter("custimg", custimg));
            //Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));

            Rpt1.SetParameters(new ReportParameter("rate", dt3.Rows[0]["rate"].ToString()));
            Rpt1.SetParameters(new ReportParameter("propertyprice", dt3.Rows[0]["propertyprice"].ToString()));
            Rpt1.SetParameters(new ReportParameter("carparkingprice", dt3.Rows[0]["carparkingprice"].ToString()));
            Rpt1.SetParameters(new ReportParameter("utility", dt3.Rows[0]["utility"].ToString()));
            Rpt1.SetParameters(new ReportParameter("others", dt3.Rows[0]["others"].ToString()));
            Rpt1.SetParameters(new ReportParameter("total", dt3.Rows[0]["total"].ToString()));
            Rpt1.SetParameters(new ReportParameter("unitdescriptio", dt3.Rows[0]["unitdescriptio"].ToString()));
            Rpt1.SetParameters(new ReportParameter("nomineenationalid", dt4.Rows[0]["nationalid"].ToString()));
            Rpt1.SetParameters(new ReportParameter("enrolmentdate", Convert.ToDateTime(dt2.Rows[0]["appdate"]).ToString("ddMMyyyy")));
            Rpt1.SetParameters(new ReportParameter("customerno", dt2.Rows[0]["customerno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("installmentamtpermonth", dt2.Rows[0]["insamptpermonth"].ToString()));
            Rpt1.SetParameters(new ReportParameter("nooftotalinstallment", dt2.Rows[0]["totalinstallment"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rcvbookingamt", dt2.Rows[0]["rcvbookingamt"].ToString()));
            Rpt1.SetParameters(new ReportParameter("bookingno", dt2.Rows[0]["bookingno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("custimg", custimg));
            Rpt1.SetParameters(new ReportParameter("nomineeimg", nomineeimg));
            Rpt1.SetParameters(new ReportParameter("correspondentimg", correspondentimg));
            Rpt1.SetParameters(new ReportParameter("propertyaddress", dt3.Rows[0]["propertyAddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("dateforapplicant", Convert.ToDateTime(dt2.Rows[0]["dateforapplicant"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("datefornominee", Convert.ToDateTime(dt4.Rows[0]["datefornominee"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("datefornominated", Convert.ToDateTime(dt5.Rows[0]["datefornominated"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("percntamt", percntamt.ToString()));
            Rpt1.SetParameters(new ReportParameter("remarks", dt10.Rows[0]["remarks"].ToString()));


            string bookingmoney = (dt2.Rows.Count == 0) ? "" : Convert.ToDouble(dt2.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");
            Rpt1.SetParameters(new ReportParameter("bookingmoney", bookingmoney));

            string checkno = (dt2.Rows.Count == 0) ? "" : dt2.Rows[0]["chequeno"].ToString();
            Rpt1.SetParameters(new ReportParameter("checkno", checkno));

            string bank = (dt2.Rows.Count == 0) ? "" : dt2.Rows[0]["bankname"].ToString();
            Rpt1.SetParameters(new ReportParameter("bankname", bankname));

            string branch = (dt2.Rows.Count == 0) ? "" : dt2.Rows[0]["bbranch"].ToString();
            Rpt1.SetParameters(new ReportParameter("branch", branch));

            string paydate = (dt2.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt2.Rows[0]["paydate"]).ToString("dd-MMM-yyyy");
            Rpt1.SetParameters(new ReportParameter("installmentdate", paydate));

            string paymentmode = this.cblpaytype.SelectedValue.ToString();
            Rpt1.SetParameters(new ReportParameter("modeofpay", paymentmode));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblprjinfo"];
            DataTable dtp = (DataTable)Session["tblperinfo"];
            DataTable dtn = (DataTable)Session["tblnomineeinfo"];
            DataTable dtntd = (DataTable)Session["tblnominatedinfo"];
            DataTable dtpdt = (DataTable)Session["tblpricedetail"];
            DataTable dtrmrk = (DataTable)Session["tblrmrkdetail"];



            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string appdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string chequeno = this.txtCheqNo.Text.Trim();
            string bankname = this.txtbankname.Text.Trim();
            string branch = this.txtbankbranch.Text.Trim();
            string InstallAmtPerMonth = Convert.ToDouble("0" + this.Textinsamt.Text.Trim()).ToString();
            string NoofTotalInstall = Convert.ToDouble("0" + this.TxtNoTInstall.Text.Trim()).ToString();
            string Rcvbookingam = Convert.ToDouble("0" + this.txtrcvbookingam.Text.Trim()).ToString();

            string bookingamt = Convert.ToDouble("0" + this.TextBookingAmt.Text.Trim()).ToString();
            string bookdate = Convert.ToDateTime(this.txtbookdate.Text).ToString("dd-MMM-yyyy");
            string inttoavailloan = this.cblintavailloan.SelectedValue.ToString();
            string modeofpay = this.cblpaytype.SelectedValue.ToString();
            string customerMaxNo = this.txtCustmerNumber.Text.Trim();




            //DataSet ds1 = new DataSet("ds1");
            //ds1.Merge(dt);
            //ds1.Tables[0].TableName = "tbl1";

            //DataSet ds2 = new DataSet("ds1");
            //ds2.Merge(dtp);
            //ds2.Tables[0].TableName = "tbl1";


            //DataSet ds3 = new DataSet("ds1");
            //ds3.Merge(dtn);
            //ds3.Tables[0].TableName = "tbl1";




            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Merge(dtp);
            ds1.Merge(dtn);
            ds1.Merge(dtntd);
            ds1.Merge(dtpdt);
            ds1.Merge(dtrmrk);


            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[0].TableName = "tbl2";
            ds1.Tables[0].TableName = "tbl3";
            ds1.Tables[0].TableName = "tbl4";
            ds1.Tables[0].TableName = "tbl5";
            ds1.Tables[0].TableName = "tbl6";





            //DataSet ds2 = new DataSet("ds1");
            //ds2.Merge(dtp);
            //ds2.Tables[0].TableName = "tbl1";


            //DataSet ds3 = new DataSet("ds1");
            //ds3.Merge(dtn);
            //ds3.Tables[0].TableName = "tbl1";



            //Uploading Image
            //string savelocation = Server.MapPath ("~") + "\\Image1";
            //string[] filePaths = Directory.GetFiles (savelocation);
            //foreach (string filePath in filePaths)
            //    File.Delete (filePath);

            //byte[] photo = new byte[0];


            //// Image
            //if (Session["i"] != null)
            //{
            //    image_file = (Stream)Session["i"];
            //    size = Convert.ToInt32 (Session["s"]);
            //    BinaryReader br = new BinaryReader (image_file);
            //    photo = br.ReadBytes (size);
            //}






            bool result = SalData.UpdateXmlTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "INSORUPDATECUSTAPPINF", ds1, null, null, pactcode, usircode, appdate, bookingamt, bankname, branch, bookdate, inttoavailloan, modeofpay, chequeno, customerMaxNo, InstallAmtPerMonth, NoofTotalInstall, Rcvbookingam);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = SalData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                //DataTable dt = (DataTable)ViewState["tblimages"];
                //string filename = System.IO.Path.GetFileName (imgFileUpload.FileName);

                //string pactcode = "";
                //string usircode = "";
                //if (imgFileUpload.HasFile)
                //{
                //    pactcode = this.ddlProjectName.SelectedValue.ToString ();
                //    usircode = this.ddlCustName.SelectedValue.ToString ();

                //    //string holder = this.ddlimgperson.SelectedValue.ToString ();

                //    string extension = Path.GetExtension (imgFileUpload.PostedFile.FileName);
                //    string random = ASTUtility.RandNumber (1, 99999).ToString ();
                //    imgFileUpload.SaveAs (Server.MapPath ("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                //    Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;

                //}

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                //Extract Image File Name.
                //string fileName = Path.GetFileName(imgFileUpload.PostedFile.FileName);

                ////Set the Image File Path.
                //string filePath = "~/Upload/CUSTOMER/" + fileName;

                ////Save the Image File in Folder.
                //imgFileUpload.PostedFile.SaveAs(Server.MapPath(filePath));
                string imgurl = Session["imgUrl"].ToString();

                bool result = SalData.UpdateTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "INSORUPDATECUSTIMG", pactcode,
                    usircode, imgurl);

                if (result == true)
                {
                    //this.lblMesg.Text = " Successfully Updated ";
                    //this.LoadImg();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Uploaded Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }
            }
            catch (Exception ex)
            {

            }

        }


        protected void btnUploadNominee_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                //DataTable dt = (DataTable)ViewState["tblimages"];
                //string filename = System.IO.Path.GetFileName (imgFileUpload.FileName);

                //string pactcode = "";
                //string usircode = "";
                //if (imgFileUpload.HasFile)
                //{
                //    pactcode = this.ddlProjectName.SelectedValue.ToString ();
                //    usircode = this.ddlCustName.SelectedValue.ToString ();

                //    //string holder = this.ddlimgperson.SelectedValue.ToString ();

                //    string extension = Path.GetExtension (imgFileUpload.PostedFile.FileName);
                //    string random = ASTUtility.RandNumber (1, 99999).ToString ();
                //    imgFileUpload.SaveAs (Server.MapPath ("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                //    Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;

                //}

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                //Extract Image File Name.
                //string fileName = Path.GetFileName(imgFileUpload.PostedFile.FileName);

                ////Set the Image File Path.
                //string filePath = "~/Upload/CUSTOMER/" + fileName;

                ////Save the Image File in Folder.
                //imgFileUpload.PostedFile.SaveAs(Server.MapPath(filePath));
                string imgurl = Session["imgNomineeUrl"].ToString();
                bool result = SalData.UpdateTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "INSORUPDATENOMINEEIMG", pactcode,
                    usircode, imgurl);

                if (result == true)
                {
                    //this.lblMesg.Text = " Successfully Updated ";
                    //this.LoadImg();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Uploaded Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }
            }
            catch (Exception ex)
            {

            }

        }


        protected void btnUploadCorrespondent_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                //DataTable dt = (DataTable)ViewState["tblimages"];
                //string filename = System.IO.Path.GetFileName (imgFileUpload.FileName);

                //string pactcode = "";
                //string usircode = "";
                //if (imgFileUpload.HasFile)
                //{
                //    pactcode = this.ddlProjectName.SelectedValue.ToString ();
                //    usircode = this.ddlCustName.SelectedValue.ToString ();

                //    //string holder = this.ddlimgperson.SelectedValue.ToString ();

                //    string extension = Path.GetExtension (imgFileUpload.PostedFile.FileName);
                //    string random = ASTUtility.RandNumber (1, 99999).ToString ();
                //    imgFileUpload.SaveAs (Server.MapPath ("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

                //    Url = "~/Upload/CUSTOMER/" + pactcode + usircode + random + extension;

                //}

                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string usircode = this.ddlCustName.SelectedValue.ToString();
                //Extract Image File Name.
                //string fileName = Path.GetFileName(imgFileUpload.PostedFile.FileName);

                ////Set the Image File Path.
                //string filePath = "~/Upload/CUSTOMER/" + fileName;

                ////Save the Image File in Folder.
                //imgFileUpload.PostedFile.SaveAs(Server.MapPath(filePath));
                string imgurl = Session["imgCorrespondentUrl"].ToString();
                bool result = SalData.UpdateTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "INSORUPDATECORRESPONDENTIMG", pactcode,
                    usircode, imgurl);

                if (result == true)
                {
                    //this.lblMesg.Text = " Successfully Updated ";
                    //this.LoadImg();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Uploaded Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
            }
            catch (Exception ex)
            {

            }
        }




        protected void lnkbtnImg_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;
                int index = this.gvperinfo.PageSize * this.gvperinfo.PageIndex + RowIndex;

                string gcod = ((DataTable)Session["tblperinfo"]).Rows[index]["gcod"].ToString();
                this.lblgcode.Text = gcod.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        //protected void txtgvValAmount_TextChanged(object sender, EventArgs e)
        //{
        //    DataTable dt1 = (DataTable)Session["tblprjinfo"];
        //    DataTable dt2 = (DataTable)Session["tblpricedetail"];
        //    double usize = Convert.ToDouble(dt1.Select("gcod='65021'")[0]["gdesc1"]);
        //    double rate = Convert.ToDouble(dt2.Select("Code='01'")[0]["amount"]);

        //    double upirce = usize * rate;
        //    DataRow[] drp = dt2.Select("Code='02'");
        //    drp[0]["amount"] = upirce;
        //    double carParkingPrice = Convert.ToDouble(dt2.Select("Code='03'")[0]["amount"]);
        //    double utility = Convert.ToDouble(dt2.Select("Code='04'")[0]["amount"]);
        //    double others = Convert.ToDouble(dt2.Select("Code='05'")[0]["amount"]);

        //    double toamt = upirce + carParkingPrice + utility + others;

        //    DataRow[] drt = dt2.Select("Code='06'");
        //    drt[0]["amount"] = toamt;
        //    //dt2.Rows[0]["amount"] = toamt;

        //    Session["tblprjinfo"] = dt1;
        //    Session["tblpricedetail"] = dt2;

        //    this.Data_BindPriceDetail();
        //}


        protected void llbtnCalculation_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dt2 = (DataTable)Session["tblpricedetail"];
            foreach (GridViewRow gv1 in GridViewPriceDetail.Rows)
            {

                double amount = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvValAmount")).Text.Trim());
                dt2.Rows[i]["amount"] = amount;
                i++;
            }

            DataTable dt1 = (DataTable)Session["tblprjinfo"];
            double usize = Convert.ToDouble(dt1.Select("gcod='65021'")[0]["gdesc1"]);
            double rate = Convert.ToDouble(dt2.Select("Code='01'")[0]["amount"]);



            double upirce = usize * rate;
            DataRow[] drp = dt2.Select("Code='02'");



            //double discount = Convert.ToDouble(dt2.Select("Code='08'")[0]["amount"]);

            //if (discount > 0)
            //{
            //    //DataRow[] drd = dt2.Select("Code='02'");
            //    //drd[0]["amount"] = ppamtafterdiscount;
            //    upirce = upirce - discount;

            //    DataRow[] drpp = dt2.Select("Code='02'");
            //    drpp[0]["amount"] = upirce;
            //}


            drp[0]["amount"] = upirce;

            double carParkingPrice = Convert.ToDouble(dt2.Select("Code='03'")[0]["amount"]);
            double utility = Convert.ToDouble(dt2.Select("Code='04'")[0]["amount"]);
            double others = Convert.ToDouble(dt2.Select("Code='05'")[0]["amount"]);

            double toamt = upirce + carParkingPrice + utility + others;

            DataRow[] drt = dt2.Select("Code='06'");
            drt[0]["amount"] = toamt;
            //dt2.Rows[0]["amount"] = toamt;



            double discount = Convert.ToDouble(dt2.Select("Code='08'")[0]["amount"]);

            double toamtafterdiscount = toamt - discount;

            DataRow[] drgt = dt2.Select("Code='09'");
            drgt[0]["amount"] = toamtafterdiscount.ToString("#,##0;(#,##0); ");


            double amtpercnt = Convert.ToDouble(dt2.Select("Code='07'")[0]["amount"]);
            double payAmount = ((toamtafterdiscount * amtpercnt) / 100);
            this.TextBookingAmt.Text = payAmount.ToString("#,##0;(#,##0); ");

            //if (discount > 0)
            //{
            //    DataRow[] drd = dt2.Select("Code='06'");
            //    drd[0]["amount"] = toamtafterdiscount;

            //}



            Session["tblprjinfo"] = dt1;
            Session["tblpricedetail"] = dt2;
            this.Data_BindPriceDetail();
        }


        //private void LoadImg()
        //{
        //    string comcod = this.GetCompCode ();
        //    string pactcode = this.ddlProjectName.SelectedValue.ToString ();
        //    string usircode = this.ddlCustName.SelectedValue.ToString ();

        //    DataSet dt = SalData.GetTransInfo (comcod, "SP_ENTRY_DUMMYSALSMGT", "GETCUSIMG", pactcode, usircode);
        //}
    }
}