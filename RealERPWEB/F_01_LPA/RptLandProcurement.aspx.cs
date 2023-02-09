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
    public partial class RptLandProcurement : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProject();
                this.ViewSection();


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

        private void GetProject()
        {

            string comcod = this.GetCompCode();
            string filter2 = this.txtAccHead.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETLPPROJECT", "", filter2, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlAcHead.DataSource = dt1;
            this.ddlAcHead.DataTextField = "actdesc1";
            this.ddlAcHead.DataValueField = "actcode";
            this.ddlAcHead.DataBind();

        }

        protected void imgBtnAccHead_Click(object sender, EventArgs e)
        {
            this.GetCompCode();
        }

        private void ViewSection()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "LandSt":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "LandStSum":
                    this.lblProject.Visible = false;
                    this.txtAccHead.Visible = false;
                    this.imgBtnAccHead.Visible = false;
                    this.ddlAcHead.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }



        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LandSt":

                    this.GetLandStatus();
                    break;
                case "LandStSum":
                    this.GetLandStatusSum();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Report Budget";
                string eventdesc = "Show Report: " + type; ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        protected void GetLandStatus()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                Session.Remove("BvsE");
                this.lblLandProcurement.Visible = true;
                this.lblPaymentStatus.Visible = true;
                this.lblNotes.Visible = true;
                string date1 = this.txtdate.Text.Substring(0, 11);
                string actcode = this.ddlAcHead.SelectedValue.ToString();
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "RPTLPREGPROCESS", actcode,
                    date1, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                DataTable dt = this.HiddenSameData(ds2.Tables[0]);
                Session["BvsE"] = dt;
                Session["tblpayment"] = ds2.Tables[1];
                Session["tbNote"] = ds2.Tables[2];



                this.Data_Bind();
                this.gvpayment.DataSource = ds2.Tables[1];
                this.gvpayment.DataBind();

                this.gvNote.DataSource = ds2.Tables[2];
                this.gvNote.DataBind();


                if (ds2.Tables[1].Rows.Count == 0)
                    return;
                ((Label)this.gvpayment.FooterRow.FindControl("lgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("sum(billam)", "")) ?
                           0 : ds2.Tables[1].Compute("sum(billam)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvpayment.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("sum(payam)", "")) ?
                           0 : ds2.Tables[1].Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvpayment.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("sum(balam)", "")) ?
                           0 : ds2.Tables[1].Compute("sum(balam)", ""))).ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex) { }

        }

        private void GetLandStatusSum()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                Session.Remove("BvsE");
                string date1 = this.txtdate.Text.Substring(0, 11);
                // string date2 = this.txtDateto.Text.Substring(0, 11);

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_LPROCUREMENT", "RPTLPREGPROCESSSUM", "",
                    date1, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                DataTable dt = this.HiddenSameData(ds2.Tables[0]);
                Session["BvsE"] = dt;
                this.Data_Bind();


            }
            catch (Exception ex) { }


        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "LandSt":
                    string procode = dt1.Rows[0]["procode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["procode"].ToString() == procode)
                            dt1.Rows[j]["prodesc"] = "";
                        procode = dt1.Rows[j]["procode"].ToString();


                    }

                    break;

                case "LandStSum":

                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            dt1.Rows[j]["actdesc"] = "";
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }


                    break;

            }


            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["BvsE"];

            switch (type)
            {
                case "LandSt":
                    this.gvlandSt.DataSource = dt;
                    this.gvlandSt.DataBind();
                    break;

                case "LandStSum":
                    this.gvlandStsum.DataSource = dt;
                    this.gvlandStsum.DataBind();
                    break;

            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "LandSt":
                    this.PrintLandStatus();
                    break;

                case "LandStSum":
                    this.PrintLandStatusSum();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Report Budget";
                string eventdesc = "Print Report: " + type; ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintLandStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["BvsE"];

            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccFinalReportsLand();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["fdate"] as TextObject;
            //txtfdate.Text = this.txtDatefrom.Text.Trim();
            txtfdate.Text = "As on " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "";

            //TextObject txttdate = rptstk.ReportDefinition.ReportObjects["tdate"] as TextObject;
            //txttdate.Text = this.txtDateto.Text.Trim();



            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAcHead.SelectedItem.ToString().Trim().Substring(13, this.ddlAcHead.SelectedItem.ToString().Trim().Length - 13);


            ////////----------------------------Note////////////////////////////
            DataTable dt2 = (DataTable)Session["tbNote"];
            TextObject txtndesc1 = rptstk.ReportDefinition.ReportObjects["txtndesc1"] as TextObject;
            txtndesc1.Text = dt2.Rows[0]["ndesc"].ToString();
            TextObject txtndesc2 = rptstk.ReportDefinition.ReportObjects["txtndesc2"] as TextObject;
            txtndesc2.Text = dt2.Rows[1]["ndesc"].ToString();
            TextObject txtndesc3 = rptstk.ReportDefinition.ReportObjects["txtndesc3"] as TextObject;
            txtndesc3.Text = dt2.Rows[2]["ndesc"].ToString();
            TextObject txtndesc4 = rptstk.ReportDefinition.ReportObjects["txtndesc4"] as TextObject;
            txtndesc4.Text = dt2.Rows[3]["ndesc"].ToString();

            TextObject txtqty1 = rptstk.ReportDefinition.ReportObjects["txtqty1"] as TextObject;
            txtqty1.Text = Convert.ToDouble(dt2.Rows[0]["qty1"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty2 = rptstk.ReportDefinition.ReportObjects["txtqty2"] as TextObject;
            txtqty2.Text = Convert.ToDouble(dt2.Rows[1]["qty1"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty3 = rptstk.ReportDefinition.ReportObjects["txtqty3"] as TextObject;
            txtqty3.Text = Convert.ToDouble(dt2.Rows[2]["qty1"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty7 = rptstk.ReportDefinition.ReportObjects["txtqty7"] as TextObject;
            txtqty7.Text = Convert.ToDouble(dt2.Rows[3]["qty1"]).ToString("#,##0.00;(#,##0.00); - ");

            TextObject txtqty4 = rptstk.ReportDefinition.ReportObjects["txtqty4"] as TextObject;
            txtqty4.Text = Convert.ToDouble(dt2.Rows[0]["qty2"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty5 = rptstk.ReportDefinition.ReportObjects["txtqty5"] as TextObject;
            txtqty5.Text = Convert.ToDouble(dt2.Rows[1]["qty2"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty6 = rptstk.ReportDefinition.ReportObjects["txtqty6"] as TextObject;
            txtqty6.Text = Convert.ToDouble(dt2.Rows[2]["qty2"]).ToString("#,##0.00;(#,##0.00); - ");
            TextObject txtqty8 = rptstk.ReportDefinition.ReportObjects["txtqty8"] as TextObject;
            txtqty8.Text = Convert.ToDouble(dt2.Rows[3]["qty2"]).ToString("#,##0.00;(#,##0.00); - ");



            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLandStatusSum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["BvsE"];

            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_01_LPA.RptLandProcurementAll();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["fdate"] as TextObject;
            txtfdate.Text = "As on " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "";


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvlandSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Label description = (Label)e.Row.FindControl("lblgvdescryptionbe");
            Label bgdqty = (Label)e.Row.FindControl("lbgdqty");
            Label bgdam = (Label)e.Row.FindControl("lblgvbgdam");
            Label actualqty = (Label)e.Row.FindControl("lgvToqty");
            Label actualam = (Label)e.Row.FindControl("lblgvClam");
            Label avqty = (Label)e.Row.FindControl("lgvavqty");
            Label AvAmt = (Label)e.Row.FindControl("lgvAamt");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString().Trim();
            string procode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Left(procode, 4) == "0102")
            {
                description.Style.Add("color", "green");
            }
            if (procode == "0199001")
            {
                description.Style.Add("color", "red");
            }
            if (ASTUtility.Right(code, 4) == "AAAA")
            {
                description.Font.Bold = true;
                bgdqty.Font.Bold = true;
                bgdam.Font.Bold = true;
                actualqty.Font.Bold = true;
                actualam.Font.Bold = true;
                avqty.Font.Bold = true;
                AvAmt.Font.Bold = true;

            }
        }


        protected void gvlandStsum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink gvprocess = (HyperLink)e.Row.FindControl("lkgvgvprocess");
            Label bgdqty = (Label)e.Row.FindControl("lbgdqtysum");
            Label bgdam = (Label)e.Row.FindControl("lblgvbgdamsum");
            Label actualqty = (Label)e.Row.FindControl("lgvToqtysum");
            Label actualam = (Label)e.Row.FindControl("lblgvClamsum");
            Label avqty = (Label)e.Row.FindControl("lgvavqtysum");
            Label AvAmt = (Label)e.Row.FindControl("lgvAamtsum");


            // string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
            string procode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode")).ToString().Trim();

            if (procode == "")
            {
                return;
            }
            if (ASTUtility.Left(procode, 4) == "0102")
            {
                gvprocess.Style.Add("color", "green");
            }
            if (procode == "0199000")
            {
                gvprocess.Style.Add("color", "red");
            }
            if (ASTUtility.Right(procode, 4) == "AAAA")
            {
                gvprocess.Font.Bold = true;
                bgdqty.Font.Bold = true;
                bgdam.Font.Bold = true;
                actualqty.Font.Bold = true;
                actualam.Font.Bold = true;
                avqty.Font.Bold = true;
                AvAmt.Font.Bold = true;
                gvprocess.Style.Add("color", "blue");
                bgdqty.Style.Add("color", "blue");
                bgdam.Style.Add("color", "blue");
                actualqty.Style.Add("color", "blue");
                actualam.Style.Add("color", "blue");
                avqty.Style.Add("color", "blue");
                AvAmt.Style.Add("color", "blue");
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
                string actdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc1")).ToString().Trim();
                gvprocess.NavigateUrl = "~/F_01_LPA/LinkLandProcurement.aspx?Type=LandSt&actcode=" + actcode + "&actdesc=" + actdesc + "&Date=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            }


        }
    }
}