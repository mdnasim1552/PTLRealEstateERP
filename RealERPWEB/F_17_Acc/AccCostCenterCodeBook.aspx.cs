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
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccCostCenterCodeBook : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = "Cost Center Code";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                if (this.ddlCostCodeBook.Items.Count == 0)
                    this.LoadCostCenterCodeBook();

                CommonButton();
            }



        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void LoadCostCenterCodeBook()
        {

            try
            {







                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string coderange = "sectcod like '%'";
                string AllRes = "ALL";
                DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETCOSTCENTERCODE", coderange, AllRes, "", "", "", "", "", "", "");
                this.ddlCostCodeBook.DataTextField = "sectcod";
                this.ddlCostCodeBook.DataValueField = "sectcod1";
                this.ddlCostCodeBook.DataSource = dsone.Tables[0];
                this.ddlCostCodeBook.DataBind();

            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Error:" + ex.Message;
            }
        }


        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.ConfirmMessage.Visible = false;
                    this.LblBookName1.Text = "Cost Code Book:";
                    this.ddlCostCodeBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lbalterofddl.Text = this.ddlCostCodeBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    string tempddl1 = (this.ddlCostCodeBook.SelectedValue.ToString()).Substring(0, 2);
                    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();


                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    this.ConfirmMessage.Visible = false;
                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ddlCostCodeBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }

            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Information not found!!!!";
            }
        }

        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ViewState.Remove("storedata");
            string srchoptionmain = this.txtsrch.Text.Trim();
            //string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {

                int index, index01;
                if (srchoption.Contains(","))
                {
                    index = srchoption.IndexOf(",");
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sectcod like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";
                    srchoption = srchoption + " or " + "sectcod like '" + srchoptionmain.Substring(index + 1) + "%'";
                }
                else
                {
                    index01 = srchoption.IndexOf("-");
                    srchoption = "sectcod like '" + srchoptionmain.Substring(0, 1) + "[" + srchoptionmain.Substring(1, 1) + "-" + srchoptionmain.Substring(index01 + 2, 1) + "]%'";

                }

            }

            string tempddl1 = (this.ddlCostCodeBook.SelectedValue.ToString()).Substring(0, 2);

            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;
            string Calltype = (srchoptionmain.Contains("-")) ? "OACCOUNTCOSTBTWNCINFO" : "OACCOUNTCOSTINFO";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1,
                                  tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            ViewState["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)ViewState["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());



                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {

            }
        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }



        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.ConfirmMessage.Visible = false;
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();


        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string sectcod1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sectcod2 = ((HyperLink)grvacc.Rows[e.RowIndex].FindControl("HLgvcode")).Text.Trim();
                string sectcod = "";
                bool updateallow = true;

                if (sectcod1.Length == 13 && sectcod1.Substring(2, 1) == "-" && sectcod1.Substring(6, 1) == "-" && sectcod1.Substring(9, 1) == "-" && !sectcod1.Contains(" "))
                {
                    sectcod = sectcod2.Substring(0, 2) + sectcod1.Substring(0, 2) + sectcod1.Substring(3, 3) + sectcod1.Substring(7, 2) + sectcod1.Substring(10, 3);
                }
                else
                {
                    //this.ConfirmMessage.Text = "Invalid code!";
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    updateallow = false;
                }

                //  int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string psectcod1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string Name = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvcentername")).Text.Trim();


                DataTable tbl1 = (DataTable)ViewState["storedata"];
                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psectcod1 != sectcod.Substring(2, 10) && sectcod1.Length == 13 && sectcod1.Substring(2, 1) == "-" && sectcod1.Substring(6, 1) == "-" && sectcod1.Substring(9, 1) == "-" && !sectcod1.Contains(" "))
                {
                    if (sectcod1.Substring(3, 3) != psectcod1.Substring(2, 3))
                    {
                        //this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sectcod1.Substring(7, 2) != psectcod1.Substring(5, 2))
                    {
                        //this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sectcod1.Substring(10, 3) != psectcod1.Substring(7, 3))
                    {
                        // this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psectcod1 != sectcod.Substring(2, 10) && sectcod1.Length == 13 && sectcod1.Substring(2, 1) == "-" && sectcod1.Substring(6, 1) == "-" && sectcod1.Substring(9, 1) == "-" && !sectcod1.Contains(" "))
                {
                    if (sectcod1.Substring(7, 2) != psectcod1.Substring(5, 2))
                    {
                        // this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sectcod1.Substring(10, 3) != psectcod1.Substring(7, 3))
                    {
                        // this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psectcod1 != sectcod.Substring(2, 10) && sectcod1.Length == 13 && sectcod1.Substring(2, 1) == "-" && sectcod1.Substring(6, 1) == "-" && sectcod1.Substring(9, 1) == "-" && !sectcod1.Contains(" "))
                {

                    if (sectcod1.Substring(10, 3) != psectcod1.Substring(7, 3) || sectcod1.Substring(3, 3) == "000")
                    {
                        // this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psectcod1 != sectcod.Substring(2, 10) && sectcod1.Length == 13 && sectcod1.Substring(2, 1) == "-" && sectcod1.Substring(6, 1) == "-" && sectcod1.Substring(9, 1) == "-" && !sectcod1.Contains(" "))
                {
                    if (sectcod1.Substring(3, 3) == "000" && sectcod1.Substring(7, 2) != "00" || sectcod1.Substring(7, 2) == "00" && sectcod1.Substring(10, 3) != "000")
                    {
                        //this.ConfirmMessage.Text = "Update Not Allowed";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        updateallow = false;
                    }
                }

                if (updateallow)
                {
                    //Hashtable hst = BDAccSession.Current.tblLogin;//BDAccSession.Current.tblLogin;
                    //string userid = hst["usrid"].ToString();

                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                    //tbl1.Rows[Index]["SECTCOD"] = sectcod;
                    //tbl1.Rows[Index]["SECTDESC"] = Desc;
                    //tbl1.Rows[Index]["SECTNAME"] = Name;
                    bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCOSTUPDATE", sectcod2.Substring(0, 2), sectcod, Name, Desc);
                    this.ShowInformation();

                    if (result)
                    {
                        //this.ConfirmMessage.Text = "Update Successfully";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                    }
                    else
                    {
                        // this.ConfirmMessage.Text = "Parent Code Not Found!!!";
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    }

                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();
                }



            }
            catch (Exception ex)
            {

                this.ConfirmMessage.Text = "Error:" + ex.Message;


            }

        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            HyperLink mdesc = (HyperLink)e.Row.FindControl("HLgvcode");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sectcod")).ToString();
                string desc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sectdesc")).ToString();


                if (ASTUtility.Right(code, 3) != "000")
                {
                    mdesc.Font.Bold = true;
                    mdesc.Style.Add("color", "blue");

                }
            }




        }


    }
}