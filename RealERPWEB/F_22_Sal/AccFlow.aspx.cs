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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_22_Sal
{
    public partial class AccFlow : System.Web.UI.Page
    {

        ProcessAccess CustData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        SalesInvoice_BL GetCompinf = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //   if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //     Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //string qType = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (qType == "Architecture") ? "Project Design" : (qType == "Legal") ? "Project Interior"
                //    : (qType == "Logistic") ? "Project Pre-Planning-Logistic" : (qType == "Design") ? "Project Pre-Planning-Design" : "Project Pre-Planning-Brand";

                this.GetCompcodeERP();
                this.GetCompcodeACC();
                this.GetProjectNameERP();
                this.GetProjectNameACC();
                this.GetPrjCustomerAcc();


                this.tableintosession();

                this.GetExistingDataItem();

            }
        }

        private void GetCompcodeERP()
        {
            try
            {
                string comcod = this.GetCompCode();
                string calltype = "GETERPCOMCODERP";

                var lst = GetCompinf.GetAllCompData(comcod, calltype);
                this.ddlcomplist.DataTextField = "compname";
                this.ddlcomplist.DataValueField = "comcod";
                this.ddlcomplist.DataSource = lst;
                this.ddlcomplist.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private void GetCompcodeACC()
        {


            try
            {
                string comcod = this.GetCompCode();
                string calltype = "GETERPCOMCODACCOUNTS";

                var lst = GetCompinf.GetAllCompData(comcod, calltype);
                this.ddlAccComplist.DataTextField = "compname";
                this.ddlAccComplist.DataValueField = "comcod";
                this.ddlAccComplist.DataSource = lst;
                this.ddlAccComplist.DataBind();
            }
            catch (Exception)
            {
            }



        }





        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetProjectNameERP()
        {
            try
            {
                string comcod = this.ddlcomplist.SelectedValue.ToString();

                string calltype = "GETERPPRJLIST";

                var lst = GetCompinf.GetProjectList(comcod, calltype);


                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = lst;
                this.ddlProjectName.DataBind();
            }
            catch (Exception)
            {
            }

        }


        private void GetProjectNameACC()
        {
            try
            {
                string comcod = this.ddlAccComplist.SelectedValue.ToString();

                string calltype = "GETACCPRJLIST";

                var lst = GetCompinf.GetProjectList(comcod, calltype);


                this.ddlAccPrjlist.DataTextField = "actdesc";
                this.ddlAccPrjlist.DataValueField = "actcode";
                this.ddlAccPrjlist.DataSource = lst;
                this.ddlAccPrjlist.DataBind();
            }
            catch (Exception)
            {
            }

        }




        private void GetPrjCustomerERP()
        {
            try
            {
                string comcod = this.ddlcomplist.SelectedValue.ToString();
                string prjcod = this.ddlProjectName.SelectedValue.ToString();
                string calltype = "GETERPCUSTLIST";
                var lst = GetCompinf.GetERPCustList(comcod, prjcod, calltype);

                this.ddlcustomerlist.DataTextField = "custdesc";
                this.ddlcustomerlist.DataValueField = "usircode";
                this.ddlcustomerlist.DataSource = lst;
                this.ddlcustomerlist.DataBind();
            }
            catch (Exception)
            {
            }

        }


        private void GetPrjCustomerAcc()
        {
            try
            {
                string accomcod = this.ddlAccComplist.SelectedValue.ToString();
                string prjcod = this.ddlAccPrjlist.SelectedValue.ToString();
                string calltype = "GETACCCUSTLIST";
                if (accomcod == "3349")
                {
                    calltype = "GETACCCUSTLIST_CRD";
                }
                else
                {
                    calltype = "GETACCCUSTLIST";

                }



                var lst = GetCompinf.GetERPCustList(accomcod, prjcod, calltype);

                this.ddlAccCustomer.DataTextField = "custdesc";
                this.ddlAccCustomer.DataValueField = "usircode";
                this.ddlAccCustomer.DataSource = lst;
                this.ddlAccCustomer.DataBind();
            }
            catch (Exception)
            {
            }

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPrjCustomerERP();
            GetExistingDataItem();
        }
        protected void ddlcomplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectNameERP();

        }
        protected void ddlAccComplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectNameACC();

        }
        protected void ddlAccPrjlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPrjCustomerAcc();

        }
        protected void ddlcustomerlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.ddlcomplist.SelectedValue.ToString();
            string custid = this.ddlcustomerlist.SelectedValue.ToString();
            string calltype = "GETCUSTOMERDETAILSERP";
            var lst = GetCompinf.GetERPCustList(comcod, custid, calltype);

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string erp_prj = this.ddlProjectName.SelectedValue.ToString();
            string erp_cust = this.ddlcustomerlist.SelectedValue.ToString();
            string acc_prj = this.ddlAccPrjlist.SelectedValue.ToString();
            string acc_cust = this.ddlAccCustomer.SelectedValue.ToString();

            if (erp_prj.Equals("") || erp_cust.Equals(""))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Customer Info Required  ... !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else if (acc_prj.Equals("") || acc_cust.Equals(""))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Account ERP Info Required  ... !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            string erp_comcod = this.ddlcomplist.SelectedValue.ToString();
            string erp_comcod_desc = this.ddlcomplist.SelectedItem.ToString();
            string erp_prj_desc = this.ddlProjectName.SelectedItem.ToString();
            string erp_cust_desc = this.ddlcustomerlist.SelectedItem.ToString();
            string acc_comcod = this.ddlAccComplist.SelectedValue.ToString();
            string acc_comcod_desc = this.ddlAccComplist.SelectedItem.ToString();
            string acc_prj_desc = this.ddlAccPrjlist.SelectedItem.ToString();
            string acc_cust_desc = this.ddlAccCustomer.SelectedItem.ToString();


            if (isCustomerExit(erp_cust, acc_cust))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Data Already Exit ... !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataTable dt = (DataTable)Session["tblsession"];
            DataRow dr = dt.NewRow();
            dr["erp_comcod"] = erp_comcod;
            dr["erp_comcod_desc"] = erp_comcod_desc;
            dr["erp_prj"] = erp_prj;
            dr["erp_prj_desc"] = erp_prj_desc;
            dr["erp_cust"] = erp_cust;
            dr["erp_cust_desc"] = erp_cust_desc;

            dr["acc_comcod"] = acc_comcod;
            dr["acc_prj"] = acc_prj;
            dr["acc_cust"] = acc_cust;
            dr["acc_comcod_desc"] = acc_comcod_desc;
            dr["acc_prj_desc"] = acc_prj_desc;
            dr["acc_cust_desc"] = acc_cust_desc;

            //dr["inflowid"] = "0";
            dt.Rows.Add(dr);
            Session["tblsession"] = dt;
            this.grvacc_DataBind();
        }


        //private bool isValidDataErp()
        //{
        //    string erp_prj = this.ddlProjectName.SelectedValue.ToString();
        //    string erp_cust = this.ddlcustomerlist.SelectedValue.ToString();

        //    string acc_prj = this.ddlAccPrjlist.SelectedValue.ToString();
        //    string acc_cust = this.ddlAccCustomer.SelectedValue.ToString();
        //    if (erp_prj.Equals("") || erp_cust.Equals("") || acc_prj.Equals("") || acc_cust.Equals(""))
        //    {
        //        return true;
        //    }
        //    return false;
        //}


        private bool isCustomerExit(string erp_cust, string acc_cust)
        {
            DataTable dt = (DataTable)Session["tblsession"];
            foreach (DataRow row in dt.Rows)
            {
                var erpcust = row["erp_cust"].ToString();
                var acccust = row["acc_cust"].ToString();
                if (erpcust == erp_cust && acccust == acc_cust)
                {
                    return true;
                }
            }
            return false;
        }

        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();


            //  dttemp.Columns.Add("inflowid", Type.GetType("System.int"));
            dttemp.Columns.Add("erp_comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("erp_comcod_desc", Type.GetType("System.String"));
            dttemp.Columns.Add("erp_prj", Type.GetType("System.String"));
            dttemp.Columns.Add("erp_prj_desc", Type.GetType("System.String"));
            dttemp.Columns.Add("erp_cust", Type.GetType("System.String"));

            dttemp.Columns.Add("erp_cust_desc", Type.GetType("System.String"));
            dttemp.Columns.Add("acc_comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("acc_prj", Type.GetType("System.String"));
            dttemp.Columns.Add("acc_cust", Type.GetType("System.String"));

            dttemp.Columns.Add("acc_comcod_desc", Type.GetType("System.String"));
            dttemp.Columns.Add("acc_prj_desc", Type.GetType("System.String"));
            dttemp.Columns.Add("acc_cust_desc", Type.GetType("System.String"));

            Session["tblsession"] = dttemp;
        }

        private void GetExistingDataItem()
        {
            string comcod = this.ddlcomplist.SelectedValue.ToString();
            string prjcod =  this.ddlProjectName.SelectedValue.ToString() + "%";
            string acccomcod = this.ddlAccComplist.SelectedValue.ToString();
            string qtype = "";
            if (comcod == "3348")
            {
                qtype = "GETEXISTSDATA_CRD";
            }
            else
            {
                qtype = "GETEXISTSDATA";

            }


            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", qtype, prjcod, acccomcod, "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                return;
            }
            Session["tblsession"] = ds2.Tables[0];
            this.grvacc.DataSource = (DataTable)Session["tblsession"];
            this.grvacc.DataBind();
        }
        protected void grvacc_DataBind()
        {

            this.grvacc.DataSource = (DataTable)Session["tblsession"];
            this.grvacc.DataBind();
        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblsession"];


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string erp_comcod = dt.Rows[i]["erp_comcod"].ToString();
                    string erp_prj = dt.Rows[i]["erp_prj"].ToString();
                    string erp_cust = dt.Rows[i]["erp_cust"].ToString();
                    string acc_comcod = dt.Rows[i]["acc_comcod"].ToString();
                    string acc_prj = dt.Rows[i]["acc_prj"].ToString();
                    string acc_cust = dt.Rows[i]["acc_cust"].ToString();


                    bool result = CustData.UpdateTransInfo(erp_comcod, "SP_REPORT_ACCOUNTSMAP", "INSERTCOMPMAPDATA", erp_prj, erp_cust, acc_comcod,
                         acc_prj, acc_cust, "", "", "", "", "", "", "", "", "");
                    if (result == true)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                    }


                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsession"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();

            int inflowid = Convert.ToUInt16(dt.Rows[RowIndex]["inflowid"]);
            if (inflowid > 0)
            {
                bool result = CustData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTSMAP", "DELETEEMPINFOW", inflowid.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == true)
                {
                    dt.Rows[RowIndex].Delete();
                    DataView dv = dt.DefaultView;
                    Session.Remove("tblsession");
                    Session["tblsession"] = dv.ToTable();
                    this.grvacc_DataBind();

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }

            }
            else
            {
                dt.Rows[RowIndex].Delete();
                DataView dv = dt.DefaultView;
                Session.Remove("tblsession");
                Session["tblsession"] = dv.ToTable();
                this.grvacc_DataBind();

                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Deleted";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

        }

    }
}