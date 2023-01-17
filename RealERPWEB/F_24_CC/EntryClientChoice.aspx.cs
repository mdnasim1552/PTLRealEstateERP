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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class EntryClientChoice : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.GetProjectName();
                this.GetCustomer();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bill Conformation";







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


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            ds1.Dispose();
            this.GetCustomer();
        }
        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtsrchCustomer = "%" + this.txtSrcCustomer.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCUSTOMERNAME", pactcode, txtsrchCustomer, "", "", "", "", "", "", "");
            this.ddlCustomer.DataTextField = "custnam";
            this.ddlCustomer.DataValueField = "custid";
            this.ddlCustomer.DataSource = ds1.Tables[0];
            this.ddlCustomer.DataBind();
            ds1.Dispose();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblCustomer.Text = (this.ddlCustomer.Items.Count == 0) ? "" : this.ddlCustomer.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.ddlCustomer.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblCustomer.Visible = true;
                this.ShowData();
                return;
            }
            this.lbtnOk.Text = "Ok";


            this.ddlProjectName.Visible = true;
            this.ddlCustomer.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.lblCustomer.Visible = false;

            this.gvclientchoice.DataSource = null;
            this.gvclientchoice.DataBind();
        }
        private void ShowData()
        {
            Session.Remove("tblchoice");
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCLINETCHOICE", PactCode, usircode, "", "", "", "", "", "", "");
            //DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCLINETCHOICE",PactCode, "", "", "", "", "", "", "","","","","","","");
            if (ds1 == null)
            {
                this.gvclientchoice.DataSource = null;
                this.gvclientchoice.DataBind();
                return;
            }


            Session["tblchoice"] = this.HiddenSameData(ds1.Tables[0]);           //ds1.Tables[0];

            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string mgcod = dt1.Rows[0]["mgcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgcod"].ToString() == mgcod)
                {

                    dt1.Rows[j]["mgdesc"] = "";

                }

                mgcod = dt1.Rows[j]["mgcod"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {

            try
            {
                DataTable tbl1 = (DataTable)Session["tblchoice"];
                this.gvclientchoice.DataSource = tbl1;
                this.gvclientchoice.DataBind();
            }

            catch (Exception ed)
            {



            }

            //if (tbl1.Rows.Count > 0)
            //{
            //   ((Label)this.gvclientchoice.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amount)", "")) ? 0.00 : tbl1.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); -");

            //}
        }
        protected void ibtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomer();
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
            string projectName = this.lblProjectdesc.Text.Substring(3);//before used substring(4)
            string unitName = this.lblCustomer.Text;
            DataTable dt = (DataTable)Session["tblchoice"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_24_CC.EClassClientChoice>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.rptClientChoice", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", projectName));
            Rpt1.SetParameters(new ReportParameter("txtUnitName", unitName));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S CHOICE"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptletter = new RealERPRPT.R_24_CC.rptClientChoice();
            //TextObject txtComName = rptletter.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //txtComName.Text = comnam;
            //TextObject txtprojectname = rptletter.ReportDefinition.ReportObjects["txtpojectname"] as TextObject;
            //txtprojectname.Text = this.lblProjectdesc.Text.Substring(4);
            //TextObject txtUnitName = rptletter.ReportDefinition.ReportObjects["txtUnitName"] as TextObject;
            //txtUnitName.Text = this.lblCustomer.Text;
            //TextObject txtuserinfo = rptletter.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptletter.SetDataSource(tbl1);
            //Session["Report1"] = rptletter;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }






        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblchoice"];
            for (int i = 0; i < gvclientchoice.Rows.Count; i++)
            {


                //string  qty = Convert.ToDouble("0" + ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvcrate")).Text.Trim()).ToString();


                tbl1.Rows[i]["qty"] = Convert.ToDouble("0" + ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvqty")).Text.Trim()).ToString();
                tbl1.Rows[i]["crate"] = Convert.ToDouble("0" + ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvcrate")).Text.Trim()).ToString();
                tbl1.Rows[i]["clrate"] = Convert.ToDouble("0" + ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvclrate")).Text.Trim()).ToString();
                tbl1.Rows[i]["wdesc"] = ((TextBox)this.gvclientchoice.Rows[i].FindControl("txtgvclientc")).Text.Trim();
                // tbl1.Rows[i]["chk"] = (((CheckBox)gvclientchoice.Rows[i].FindControl("chkgvcc")).Checked) ? "True" : "False"; 


            }



            Session["tblchoice"] = tbl1;

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt = (DataTable)Session["tblchoice"];




                string Pactcode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.ddlCustomer.SelectedValue.ToString();



                foreach (DataRow dr2 in dt.Rows)
                {


                    //string gcod = dr2["gcod"].ToString();
                    //string gdesc = dr2["gdesc"].ToString();
                    string wcode = dr2["gcod"].ToString();
                    double qty = Convert.ToDouble(dr2["qty"].ToString());
                    string crate = dr2["crate"].ToString();
                    string clrate = dr2["clrate"].ToString();
                    string wdesc = dr2["wdesc"].ToString();
                    bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCLINTCHOCEINSERTORUPDATE", Pactcode, Usircode, wcode, qty.ToString(), crate, clrate, wdesc, "", "", "", "", "", "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }



                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);








            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void gvclientchoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //    if (this.chkAll.Checked)
            //    {
            //        this.ShowAll();
            //    }
            //    else
            //    {
            //        this.ShowData();
            //    }

        }
        private void ShowAll()
        {

            Session.Remove("tblchoice");

            string comcod = this.GetCompCode();

            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCLIENTCHOICECHK", PactCode, usircode, "", "", "", "", "", "", "");
            //DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCLINETCHOICE",PactCode, "", "", "", "", "", "", "","","","","","","");
            if (ds1 == null)
            {
                this.gvclientchoice.DataSource = null;
                this.gvclientchoice.DataBind();
                return;
            }


            Session["tblchoice"] = this.HiddenSameData(ds1.Tables[0]);           //ds1.Tables[0];

            this.Data_Bind();


        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomer();
        }
    }
}
