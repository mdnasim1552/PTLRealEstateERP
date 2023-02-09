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
namespace RealERPWEB.F_24_CC
{
    public partial class CustRevenue : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = "Revenue(Utility & Others)";

                Session.Remove("Unit");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.Page.Title = "Revenue(Utility & Others)";

                this.gvSpayment.Columns[0].Visible = false;



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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            string userid = hst["usrid"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                //this.lblProjectmDesc.Text = ddldesc == "True" ? this.ddlProjectName.SelectedItem.Text.Trim() : this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.ToString();
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                //this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";

                this.lbtnBack.Visible = false;
                this.ClearScreen();
            }
        }



        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";
            string musircode = "51";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "DETAILSIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnusize_Click(object sender, EventArgs e)
        {
            this.gvSpayment.Columns[0].Visible = true;
            this.lbtnBack.Visible = true;
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();
            //if ((Convert.ToBoolean(dtOrder.Rows[0]["mgtbook"])) == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Booked from Management');", true);
            //    return;
            //}
            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;
            this.lblAcAmt.Text = Convert.ToDouble(dtOrder.Rows[0]["tamt"]).ToString("#,##0;(#,##0); ");
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;
            this.CustInf();

        }




        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lbtnBack.Visible = false;
            this.LoadGrid();



        }

        private void CustInf()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string empid = hst["empid"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETREVENUEINFO", PactCode, UsirCode, "", "", "", "", "", "", "");




            this.gvCost.DataSource = ds1.Tables[0];
            this.gvCost.DataBind();
            Session["tblcost"] = ds1.Tables[0];
            this.FooterCalculation();

            //System.DateTime.Today.AddYears(-2).ToString("dd-MMM-yyyy")
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblcost"];

            ((Label)this.gvCost.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ? 0.00 :
                   dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnTotalCost_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblcost"];
            // double PaidAmt = 0;
            int i = 0;
            foreach (GridViewRow gvr in gvCost.Rows)
            {
                double dAmt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gvr.FindControl("txtgvuamt")).Text.Trim()));
                ((TextBox)gvr.FindControl("txtgvuamt")).Text = dAmt.ToString("#,##0; -#,##0; ");
                dt.Rows[i++]["uamt"] = dAmt;
            }

            Session["tblcost"] = dt;
            this.FooterCalculation();




        }

        protected void lFinalUpdateCost_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();
            DataTable dt = (DataTable)Session["tblcost"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";

            bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESALGIN2", ds1, null, null, PactCode, Usircode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                return;

            }


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Revenue Info";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();

        }
        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl2 = (DropDownList)this.gvSpayment.Rows[0].FindControl("ddlClientName");

            string clientcode = ddl2.SelectedValue.ToString();
            if (ddl2 == null)
                return;


            if (clientcode == "000000000000")
            {
                ddl2.Visible = false;
                ((TextBox)this.gvSpayment.Rows[0].FindControl("txtCustname")).Visible = true;
            }

        }


    }
}

