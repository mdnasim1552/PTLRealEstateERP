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

                // Upload = System.IO.Path.GetFileName (imgFileUpload.PostedFile.FileName);
                //string savelocation = Server.MapPath ("~") + Url;
                //string filepath = savelocation;
                //imgFileUpload.PostedFile.SaveAs (savelocation);

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

        private void GetProjectName()
        {
            try
            {
                string comcod = this.GetCompCode();
                string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
                DataSet ds1 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT_TEST", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
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
                DataSet ds2 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT_TEST", "DETAILSIRINFINFORMATION", pactcode, txtSProject, "", "", "", "", "", "", "");
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


            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT_TEST", "GETBOOKINGAPPLICATION", pactcode, custid, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvProjectInfo.DataSource = null;
                this.gvProjectInfo.DataBind();

                return;
            }



            // DataTable dt = gvInterest_DataBind(ds2);
            Session["tblprjinfo"] = ds2.Tables[0];
            Session["tblperinfo"] = ds2.Tables[1];
            Session["tblcustinfo"] = ds2.Tables[2];
            Session["tblnomineeinfo"] = ds2.Tables[3];
            Session["tblnominee"] = ds2.Tables[4];
            Session["tblnominatedinfo"] = ds2.Tables[5];
            Session["tblnominated"] = ds2.Tables[6];
            Session["tblpricedetail"] = ds2.Tables[7];
            Session["tblprice"] = ds2.Tables[8];



            DataTable dt = ds2.Tables[2];

            this.EmpImg.ImageUrl = (dt.Rows.Count) == 0 ? "" : dt.Rows[0]["custimg"].ToString();
            //  appdate, bookamt, bankname, bbranch, paydate, intavail, paymode
            this.txtdate.Text = (dt.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");
            this.txtbookamt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");
            this.txtCheqNo.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["chequeno"].ToString();
            this.txtbankname.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["bankname"].ToString();
            this.txtbankbranch.Text = (dt.Rows.Count == 0) ? "" : dt.Rows[0]["bbranch"].ToString();
            this.txtbookdate.Text = (dt.Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[0]["paydate"]).ToString("dd-MMM-yyyy");


            this.cblintavailloan.SelectedValue = (dt.Rows.Count == 0) ? "No" : dt.Rows[0]["intavail"].ToString();
            this.cblpaytype.SelectedValue = (dt.Rows.Count == 0) ? "OneTime" : dt.Rows[0]["paymode"].ToString();

            

            //this.txtentryben.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            //this.txtdelaychrg.Text = (dt.Rows.Count < 1) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            //this.Data_Bind();


            ds2.Dispose();
            this.Data_BindPrj();
            this.Data_BindPer();
            this.Data_BindNominee();
            this.Data_BindNominated();
            this.Data_BindPriceDetail();
        }

        private void Data_BindPrj()
        {
            DataTable dt = (DataTable)Session["tblprjinfo"];
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            this.GridTextDDLVisible();
        }
        private void Data_BindPer()
        {

            DataTable dt = (DataTable)Session["tblperinfo"];
            this.gvperinfo.DataSource = dt;
            this.gvperinfo.DataBind();
            this.GridTextDDLVisiblePer();
        }

        private void Data_BindNominee()
        {

            DataTable dt = (DataTable)Session["tblnomineeinfo"];
            this.GridViewNominee.DataSource = dt;
            this.GridViewNominee.DataBind();
            this.GridTextDDLVisibleNominee();
        }

        private void Data_BindNominated()
        {

            DataTable dt = (DataTable)Session["tblnominatedinfo"];
            this.GridViewNominated.DataSource = dt;
            this.GridViewNominated.DataBind();
            //this.GridTextDDLVisibleNominated();
        }

        private void Data_BindPriceDetail()
        {

            DataTable dt = (DataTable)Session["tblpricedetail"];
            this.GridViewPriceDetail.DataSource = dt;
            this.GridViewPriceDetail.DataBind();
            
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

                        //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));
                        //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        //ddlcatag.DataTextField = "prgdesc";
                        //ddlcatag.DataValueField = "prgcod";
                        //ddlcatag.DataSource = dscatg.Tables[0];
                        //ddlcatag.DataBind();
                        //ddlcatag.SelectedValue = val;

                        break;
                    default:
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((CheckBoxList)this.gvProjectInfo.Rows[i].FindControl("cbldesc")).Visible = false;
                        break;

                }
            }

        }

        //protected void AsyncFileUpload1_OnUploadedComplete ( object sender, AsyncFileUploadEventArgs e )
        //{
        //    string comcod = this.GetCompCode();
        //    //DataTable dt = (DataTable)ViewState["tblimages"];
        //    string filename = System.IO.Path.GetFileName (AsyncFileUpload1.FileName);

        //    string pactcode="";
        //    string usircode="";
        //    if (AsyncFileUpload1.HasFile)
        //    {
        //         pactcode = this.ddlProjectName.SelectedValue.ToString ();
        //         usircode = this.ddlCustName.SelectedValue.ToString ();

        //        //string holder = this.ddlimgperson.SelectedValue.ToString ();

        //        string extension = Path.GetExtension (AsyncFileUpload1.PostedFile.FileName);
        //        string random = ASTUtility.RandNumber (1, 99999).ToString ();
        //        AsyncFileUpload1.SaveAs (Server.MapPath ("~/Upload/CUSTOMER/") + pactcode + usircode + random + extension);

        //        Url = "~/Upload/CUSTOMER/" + pactcode +usircode+ random + extension;

        //    }
        //    bool result = SalData.UpdateTransInfo (comcod, "SP_ENTRY_DUMMYSALSMGT", "INSORUPDATECUSTIMG", pactcode, usircode, Url);

        //    if (result == true)
        //    {
        //        this.lblMesg.Text = " Successfully Updated ";

        //    }


        //}

        
        private void GridTextDDLVisiblePriceDetail()
        {

            //string comcod = this.GetCompCode();
            //DataTable dt = ((DataTable)Session["tblpricedetail"]).Copy();

            ((TextBox)this.GridViewPriceDetail.Rows[6].FindControl("txtgvValAmount")).Visible = false;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
                
            //    string Gcode = dt.Rows[i]["code"].ToString();
            //    if (Gcode == "07") {
                    
            //    }
            //}

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

                    case "01109": //Birthdate                 
                        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = true;
                        //((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                        //((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                        break;

                    //case "01010": //Marriage Date                  
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = true;
                    //    //((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                    //    ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                    //    //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                    //    //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                    //    //ddlcatag.DataTextField = "prgdesc";
                    //    //ddlcatag.DataValueField = "prgcod";
                    //    //ddlcatag.DataSource = dscatg.Tables[0];
                    //    //ddlcatag.DataBind();
                    //    //ddlcatag.SelectedValue = val;

                    //    break;

                    //case "01027": //Type                  
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                    //    ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = true;
                    //    ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                    //    CheckBoxList cbl = ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper"));
                    //    cbl.SelectedValue = (val == "") ? "Open" : val;
                    //    //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                    //    //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                    //    //ddlcatag.DataTextField = "prgdesc";
                    //    //ddlcatag.DataValueField = "prgcod";
                    //    //ddlcatag.DataSource = dscatg.Tables[0];
                    //    //ddlcatag.DataBind();
                    //    //ddlcatag.SelectedValue = val;

                    //    break;

                    ////lnkbtnImg Nominee Image

                    //case "01290":
                    //case "01291":
                    //case "01292":
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                    //    ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                    //    ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                    //    ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = true;
                    //    break;

                    default:
                        //((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                        //((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                        //((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;
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
                //switch (Gcode)
                //{

                //    case "01009": //Birthdate                 
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = true;
                //        //((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                //        ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                //        break;

                //    case "01010": //Marriage Date                  
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = true;
                //        //((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                //        ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                //        //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                //        //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                //        //ddlcatag.DataTextField = "prgdesc";
                //        //ddlcatag.DataValueField = "prgcod";
                //        //ddlcatag.DataSource = dscatg.Tables[0];
                //        //ddlcatag.DataBind();
                //        //ddlcatag.SelectedValue = val;

                //        break;

                //    case "01027": //Type                  
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                //        ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = true;
                //        ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                //        CheckBoxList cbl = ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper"));
                //        cbl.SelectedValue = (val == "") ? "Open" : val;
                //        //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                //        //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                //        //ddlcatag.DataTextField = "prgdesc";
                //        //ddlcatag.DataValueField = "prgcod";
                //        //ddlcatag.DataSource = dscatg.Tables[0];
                //        //ddlcatag.DataBind();
                //        //ddlcatag.SelectedValue = val;

                //        break;

                //    //lnkbtnImg Nominee Image

                //    case "01290":
                //    case "01291":
                //    case "01292":
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Visible = false;
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                //        ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                //        ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = true;
                //        break;

                //    default:
                //        ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvdValper")).Visible = false;
                //        ((CheckBoxList)this.GridViewNominee.Rows[i].FindControl("cbldescper")).Visible = false;
                //        ((LinkButton)this.GridViewNominee.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                //        break;

                //}
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

                    case "01009": //Birthdate                 
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = true;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = false;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = false;
                        break;

                    case "01010": //Marriage Date                  
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = true;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = false;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                        //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                        //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        //ddlcatag.DataTextField = "prgdesc";
                        //ddlcatag.DataValueField = "prgcod";
                        //ddlcatag.DataSource = dscatg.Tables[0];
                        //ddlcatag.DataBind();
                        //ddlcatag.SelectedValue = val;

                        break;

                    case "01027": //Type                  
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = false;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = true;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = false;

                        CheckBoxList cbl = ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper"));
                        cbl.SelectedValue = (val == "") ? "Open" : val;
                        //DropDownList ddlcatag = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlcataloc"));


                        //DataSet dscatg = SalData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        //ddlcatag.DataTextField = "prgdesc";
                        //ddlcatag.DataValueField = "prgcod";
                        //ddlcatag.DataSource = dscatg.Tables[0];
                        //ddlcatag.DataBind();
                        //ddlcatag.SelectedValue = val;

                        break;

                    //lnkbtnImg Nominee Image

                    case "01290":
                    case "01291":
                    case "01292":
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvValper")).Visible = false;
                        ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Visible = false;
                        ((CheckBoxList)this.gvperinfo.Rows[i].FindControl("cbldescper")).Visible = false;
                        ((LinkButton)this.gvperinfo.Rows[i].FindControl("lnkbtnImg")).Visible = true;
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

                if (Gcode == "01009" || Gcode == "01010")
                {

                    Gvalue = (((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvperinfo.Rows[i].FindControl("txtgvdValper")).Text.Trim();
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                dtp.Rows[i]["gdesc1"] = Gvalue;
            }

            Session["tblperinfo"] = dtp;




            for (int i = 0; i < this.GridViewNominee.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GridViewNominee.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                //string gtype = ((Label)this.GridViewNominee.Rows[i].FindControl("lgvgvalper")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewNominee.Rows[i].FindControl("txtgvValper")).Text.Trim();
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
                dtn.Rows[i]["gdesc1"] = Gvalue;
            }

            Session["tblnomineeinfo"] = dtn;


            for (int i = 0; i < this.GridViewNominated.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GridViewNominated.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                //string gtype = ((Label)this.GridViewNominated.Rows[i].FindControl("lgvgvalper")).Text.Trim();
                string Gvalue = ((TextBox)this.GridViewNominated.Rows[i].FindControl("txtgvValNominated")).Text.Trim();
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
            //double bookamt = this.txtbookamt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");
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
            //this.txtbookamt.Text = (dt.Rows.Count == 0) ? "" : Convert.ToDouble(dt.Rows[0]["bookamt"]).ToString("#,##0;(#,##0); ");

            string inttoavailloan = this.cblintavailloan.SelectedValue.ToString();
            string modeofpay = this.cblpaytype.SelectedValue.ToString();

            DataTable dt2 = (DataTable)Session["tblcustinfo"];
            DataTable dt3 = (DataTable)Session["tblprice"];
            
            string custimg = new Uri(Server.MapPath(dt2.Rows[0]["custimg"].ToString())).AbsoluteUri;

            double inword = Convert.ToDouble(dt2.Rows[0]["bookamt"]);

            //DataTable dt = ds2.Tables[0];
            DataTable dt = (DataTable)Session["tblcustinfo"];

            //DataTable dt2 = (DataTable)ViewState["tblcustinfo"];




            var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustBookApp2>();
            var list2 = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptNomineeBookApp2>();
            var list3 = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptNominatedBookApp2>();
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


                      //Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
                      //Rpt1.SetParameters(new ReportParameter("UnitName", UnitName));

                      //Rpt1.SetParameters(new ReportParameter("pactcode", dt2.Rows[0]["pactcode"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("usircode", dt2.Rows[0]["usircode"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("appdate", Convert.ToDateTime(dt2.Rows[0]["appdate"]).ToString("dd-MMM-yyyy")));
                      //Rpt1.SetParameters(new ReportParameter("bookamt", dt2.Rows[0]["bookamt"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("bookdate", bookdate));
                      //Rpt1.SetParameters(new ReportParameter("chequeno", dt2.Rows[0]["chequeno"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("bankname", dt2.Rows[0]["bankname"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("bbranch", dt2.Rows[0]["bbranch"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("paydate", dt2.Rows[0]["paydate"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("intavail", dt2.Rows[0]["intavail"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("paymode", dt2.Rows[0]["paymode"].ToString()));

                      //Rpt1.SetParameters(new ReportParameter("bookingType", dt2.Rows[0]["bookingType"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("clientID", dt2.Rows[0]["clientID"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("towerNo", dt2.Rows[0]["towerNo"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("Apartmenttype", dt2.Rows[0]["Apartmenttype"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("ttype", dt2.Rows[0]["ttype"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("floorr", dt2.Rows[0]["floorr"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("side", dt2.Rows[0]["side"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("parkingNo", dt2.Rows[0]["parkingNo"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("parkingLevel", dt2.Rows[0]["parkingLevel"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("locatedAt", dt2.Rows[0]["locatedAt"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("parkingLevel", dt2.Rows[0]["parkingLevel"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("locatedAt", dt2.Rows[0]["locatedAt"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("moneyReceipt", dt2.Rows[0]["moneyReceipt"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("bookingChart", dt2.Rows[0]["bookingChart"].ToString()));

                      //Rpt1.SetParameters(new ReportParameter("fullname", dt2.Rows[0]["fullname"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("spouse", dt2.Rows[0]["spouse"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("fathername", dt2.Rows[0]["fathername"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("mothername", dt2.Rows[0]["mothername"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("presentaddr", dt2.Rows[0]["presentaddr"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("permenentaddr", dt2.Rows[0]["permenentaddr"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("mobilenum", dt2.Rows[0]["mobilenum"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("telephonenum", dt2.Rows[0]["telephonenum"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("birthdate", dt2.Rows[0]["birthdate"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("marriageday", dt2.Rows[0]["marriageday"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nationalid", dt2.Rows[0]["nationalid"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("tinnumber", dt2.Rows[0]["tinnumber"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("mailingaddre", dt2.Rows[0]["mailingaddre"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("occupation", dt2.Rows[0]["occupation"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nationality", dt2.Rows[0]["nationality"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("religion", dt2.Rows[0]["religion"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("drivlicence", dt2.Rows[0]["drivlicence"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("bloodgroup", dt2.Rows[0]["bloodgroup"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("emailaddr", dt2.Rows[0]["emailaddr"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("fax", dt2.Rows[0]["fax"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("ClientStatus", dt2.Rows[0]["ClientStatus"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("namapplicant", dt2.Rows[0]["namapplicant"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("relationApp", dt2.Rows[0]["relationApp"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nationaldr", dt2.Rows[0]["nationaldr"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("applicant", dt2.Rows[0]["applicant"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nomineinfo", dt2.Rows[0]["nomineinfo"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nameofnominee1", dt2.Rows[0]["nameofnominee1"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("relationshipnom1", dt2.Rows[0]["relationshipnom1"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("custnote", dt2.Rows[0]["custnote"].ToString()));
                      //Rpt1.SetParameters(new ReportParameter("nominenumber", dt2.Rows[0]["nominenumber"].ToString()));

                      ////Application Date
                      //Rpt1.SetParameters(new ReportParameter("date1", appdate));
                      //Rpt1.SetParameters(new ReportParameter("bookamt01", Convert.ToDouble("0" + this.txtbookamt.Text).ToString("#,##0.00;(#,##0.00); ")));
                      //Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(inword), 2)));

                      //Rpt1.SetParameters(new ReportParameter("intloan", inttoavailloan));
                      //Rpt1.SetParameters(new ReportParameter("modeofpay", modeofpay));

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

            

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string appdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string bookamt = Convert.ToDouble("0" + this.txtbookamt.Text).ToString();
            string chequeno = this.txtCheqNo.Text.Trim();
            string bankname = this.txtbankname.Text.Trim();
            string branch = this.txtbankbranch.Text.Trim();
            string bookdate = Convert.ToDateTime(this.txtbookdate.Text).ToString("dd-MMM-yyyy");
            string inttoavailloan = this.cblintavailloan.SelectedValue.ToString();
            string modeofpay = this.cblpaytype.SelectedValue.ToString();



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
            ds1.Tables[0].TableName = "tbl1";            
            ds1.Tables[0].TableName = "tbl2";
            ds1.Tables[0].TableName = "tbl3";
            ds1.Tables[0].TableName = "tbl4";
            ds1.Tables[0].TableName = "tbl5";




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








            bool result = SalData.UpdateXmlTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT_TEST", "INSORUPDATECUSTAPPINF", ds1, null, null, pactcode, usircode, appdate, bookamt, bankname, branch, bookdate, inttoavailloan, modeofpay, chequeno);
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
                bool result = SalData.UpdateTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT_TEST", "INSORUPDATECUSTIMG", pactcode,
                    usircode, imgurl);

                if (result == true)
                {
                    //this.lblMesg.Text = " Successfully Updated ";
                    //this.LoadImg();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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
            foreach (GridViewRow gv1 in  GridViewPriceDetail.Rows)
            {
                
                double amount =Convert.ToDouble("0"+ ((TextBox)gv1.FindControl("txtgvValAmount")).Text.Trim());
                dt2.Rows[i]["amount"] = amount;
                i++;
            }


            DataTable dt1 = (DataTable)Session["tblprjinfo"];
           
            double usize = Convert.ToDouble(dt1.Select("gcod='65021'")[0]["gdesc1"]);
            double rate = Convert.ToDouble(dt2.Select("Code='01'")[0]["amount"]);

            double upirce = usize * rate;
            DataRow[] drp = dt2.Select("Code='02'");
            drp[0]["amount"] = upirce;
            double carParkingPrice = Convert.ToDouble(dt2.Select("Code='03'")[0]["amount"]);
            double utility = Convert.ToDouble(dt2.Select("Code='04'")[0]["amount"]);
            double others = Convert.ToDouble(dt2.Select("Code='05'")[0]["amount"]);

            double toamt = upirce + carParkingPrice + utility + others;

            DataRow[] drt = dt2.Select("Code='06'");
            drt[0]["amount"] = toamt;
            //dt2.Rows[0]["amount"] = toamt;

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