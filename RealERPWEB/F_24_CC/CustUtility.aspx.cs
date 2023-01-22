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
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class CustUtility : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                this.GetUnitName();
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CLIENT'S UTILITIES";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                string type = this.Request.QueryString["Type"];
                if (type == "Check" || type == "Audit" || type == "Approv")
                {
                    PreviousAddNumber();
                    lbtnOk_Click(null, null);
                }
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

            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPNAMEMAINTENANCE", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        private void GetUnitName()
        {

            Session.Remove("tblunit");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSUnit = this.txtsrchUnitName.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETUNITNAME", pactcode, txtSUnit, "", "", "", "", "", "", "");
            this.ddlUnitName.DataTextField = "udesc1";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds1.Tables[0];
            this.ddlUnitName.DataBind();
            Session["tblunit"] = ds1.Tables[0];

        }









        private void GetItemName()
        {
            ViewState.Remove("tblwrk");
            string comcod = this.GetCompCode();
            string code = "14";
            string txtsrchitem = this.txtsrchItem.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETITEMNAME", code, txtsrchitem, "", "", "", "", "", "", "");
            this.ddlItemName.DataTextField = "gdesc";
            this.ddlItemName.DataValueField = "gcod";
            this.ddlItemName.DataSource = ds1.Tables[0];
            this.ddlItemName.DataBind();
            ViewState["tblwrk"] = ds1.Tables[0];
            ds1.Dispose();
        }
        private void PreviousAddNumber()
        {
            string comcod = this.GetCompCode();
            string curdate = this.Request.QueryString["Date1"].Length > 0 ? this.Request.QueryString["Date1"] : Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
            string adno = this.Request.QueryString["genno"].Length > 0 ? ("%" + this.Request.QueryString["genno"] + "%") : "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREADWORK", curdate, adno, "", "", "", "", "", "", "");
            this.ddlPrevADNumber.DataTextField = "adno1";
            this.ddlPrevADNumber.DataValueField = "adno";
            this.ddlPrevADNumber.DataSource = ds1.Tables[0];
            this.ddlPrevADNumber.DataBind();
            ds1.Dispose();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlUnitName_SelectedIndexChanged(null, null);
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblUnitName.Text = this.ddlUnitName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.ddlUnitName.Visible = false;
                this.lblUnitName.Visible = true;
                this.ibtnPreAdNo.Visible = false;
                this.ddlPrevADNumber.Visible = false;
                this.PanelItem.Visible = true;
                this.PnlNarration.Visible = true;
                this.GetItemName();
                this.ShowAdWork();
                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.ddlUnitName.Visible = true;
            this.lblUnitName.Text = "";
            this.lblUnitName.Visible = false;
            this.ddlPrevADNumber.Items.Clear();
            this.ibtnPreAdNo.Visible = true;
            this.ddlPrevADNumber.Visible = true;
            this.PanelItem.Visible = false;
            this.gvAddWork.DataSource = null;
            this.gvAddWork.DataBind();
            this.ddlItemName.Items.Clear();

            this.PnlNarration.Visible = false;
            this.lblSchCode.Text = "";

        }



        protected void GetAddDedNumber()
        {
            string comcod = this.GetCompCode();
            string mAddNUmber = "NEWADD";
            if (this.ddlPrevADNumber.Items.Count > 0)
                mAddNUmber = this.ddlPrevADNumber.SelectedValue.ToString();

            string CurDate1 = this.txtCurTransDate.Text.Trim();
            if (mAddNUmber == "NEWADD")
            {
                DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTADDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(6, 5);

                    this.ddlPrevADNumber.DataTextField = "maxaddno1";
                    this.ddlPrevADNumber.DataValueField = "maxaddno";
                    this.ddlPrevADNumber.DataSource = ds2.Tables[0];
                    this.ddlPrevADNumber.DataBind();
                }

                ds2.Dispose();
            }

        }

        private void ShowAdWork()
        {

            Session.Remove("tbladwork");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mAdNo = "NEWAD";
            if (this.ddlPrevADNumber.Items.Count > 0)
                mAdNo = this.ddlPrevADNumber.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tbladwork"] = ds1.Tables[0];


            if (mAdNo == "NEWAD")
            {


                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTADDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(6);
                return;
            }


            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(6, 5);
            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["addate"]).ToString("dd-MMM-yyyy");
            this.txtNarr.Text = ds1.Tables[1].Rows[0]["narration"].ToString();
            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            this.ddlUnitName.SelectedValue = ds1.Tables[1].Rows[0]["usircode"].ToString();
            this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
            this.lblUnitName.Text = this.ddlUnitName.SelectedItem.Text;
            this.lblSchCode.Text = ds1.Tables[0].Rows[0]["shcod"].ToString();
            this.Data_DataBind();



        }
        private void Data_DataBind()
        {

            this.gvAddWork.DataSource = (DataTable)Session["tbladwork"];
            this.gvAddWork.DataBind();
            this.FooterCalculation((DataTable)Session["tbladwork"]);

        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvAddWork.FooterRow.FindControl("lblgvFclAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clamt)", "")) ? 0.00 :
                 dt.Compute("sum(clamt)", ""))).ToString("#,##0.00;-#,##0.00; ");




        }
        private string ComPrintAddWork()
        {
            string comcod = this.GetCompCode();
            string CompAddWork = "";

            switch (comcod)
            {
                case "3301":
                case "1301":
                case "2301":
                    CompAddWork = "PrintAddWorkSan";
                    break;
                case "3336":
                case "3337":
                    CompAddWork = "PrintAddWorkSanSuvastu";
                    break;
                case "3101":
                    CompAddWork = "PrintAddWork";
                    break;


            }
            return CompAddWork;



        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



            string CompAddWork = this.ComPrintAddWork();

            if (CompAddWork == "PrintAddWorkSan")
            {
                this.PrintAddWorkSan();


            }
            else if (CompAddWork == "PrintAddWorkSanSuvastu")
            {
                this.PrintAddWorkSanSuvastu();
            }
            else
            {
                this.PrintAddWork();


            }


        }

        private void PrintAddWorkSanSuvastu()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string UnitName = this.ddlUnitName.SelectedItem.Text;
            LocalReport Rpt1 = new LocalReport();



            string mAdNo = this.ddlPrevADNumber.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, "",
                          "", "", "", "", "", "", "");


            Session["tbladwork"] = ds1.Tables[0];

            DataTable dt = (DataTable)Session["tbladwork"];
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_24_CC.RptAddWorkSuvastu", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Statement on Optional Works"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("UnitName", UnitName));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintAddWork()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string unitName = this.ddlUnitName.SelectedItem.Text.Trim();

            DataTable dt = (DataTable)Session["tbladwork"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrk", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtNarr.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintAddWorkSan()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);

            string unitName = this.ddlUnitName.SelectedItem.Text.Trim();

            DataTable dt = (DataTable)Session["tbladwork"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrkSan", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtNarr.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ibtnFindUnitName_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string Unitcode=this.ddlUnitName.SelectedValue.ToString();
            //DataRow[] dr = ((DataTable)Session["tblunit"]).Select("usircode='" + Unitcode + "'");
            //if (dr.Length > 0) 
            //{
            //    this.lblCustomerName.Text = dr[0]["custname"].ToString();
            //    return;
            //}
            //this.lblCustomerName.Text = "";
        }
        protected void ibtnPreAdNo_Click(object sender, EventArgs e)
        {
            this.PreviousAddNumber();
        }
        protected void ibtnFindAdWork_Click(object sender, EventArgs e)
        {
            this.GetItemName();
        }
        protected void lbtnAddWork_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladwork"];
            string gcod = this.ddlItemName.SelectedValue.ToString();
            DataRow[] dr = dt.Select("gcod='" + gcod + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["gcod"] = gcod;
                dr1["gdesc"] = this.ddlItemName.SelectedItem.Text.Trim();
                dr1["unit"] = (((DataTable)ViewState["tblwrk"]).Select("gcod='" + gcod + "'"))[0]["unit"];
                dr1["wrkdesc"] = "";
                dr1["qty"] = 0.00;
                dr1["comqty"] = 0.00;
                dr1["comamt"] = 0.00;
                // dr1["clamt"] = 0.00;
                dr1["rate"] = 0.00;
                dr1["crate"] = 0.00;
                dr1["comlrate"] = 0.00;
                dr1["clrate"] = 0.00;
                dr1["cllrate"] = 0.00;
                dr1["amt"] = 0.00;
                dr1["disamt"] = 0.00;
                dr1["netamt"] = 0.00;
                dr1["comamt"] = 0.00;
                dr1["clamt"] = 0.00;
                dr1["nrefund"] = 0.00;
                dr1["ndemand"] = 0.00;
                dr1["location"] = "";
                dt.Rows.Add(dr1);
            }
            this.SaveValue();


        }
        private void SaveValue()
        {
            DataTable dt1 = (DataTable)Session["tbladwork"];
            int TblRowIndex;
            double comamt, clamt;
            for (int i = 0; i < this.gvAddWork.Rows.Count; i++)
            {
                TblRowIndex = (gvAddWork.PageIndex) * gvAddWork.PageSize + i;

                //string wrkdesc = ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvdesclchoice")).Text.Trim();
                //string txtgvlocateion = ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvlocateion")).Text.Trim();
                //double qty = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvqty")).Text.Trim());
                //double crate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcommRate")).Text.Trim());
                //double comlrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcomlRate")).Text.Trim());

                //double clrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvclmRate")).Text.Trim());
                //double cllrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcllRate")).Text.Trim());
                clamt = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvclAmt")).Text.Trim());



                //double rate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvRate")).Text.Trim());
                //double disamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvdiscount")).Text.Trim()));
                //double comqty = Convert.ToDouble ("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl ("txtgvcomqty")).Text.Trim ());
                //double amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvamt")).Text.Trim()));
                //double rate = ((clrate + cllrate) - (crate + comlrate));
                // comamt = comqty * (crate + comlrate);
                // clamt = qty * (clrate + cllrate);
                // amt =  clamt - comamt;


                //. amt = (this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17") ? amt*-1:amt;             

                //if (rate != 0)
                //    amt = qty * rate;
                //else if (amt > 0)
                //{
                //    rate = qty > 0 ? Convert.ToDouble(amt / qty) : 0;

                //}


                //if (rate != 0)
                //    amt = qty * rate;
                //else if (amt > 0)
                //{
                //    rate = qty > 0 ? Convert.ToDouble(amt / qty) : 0;

                //}



                //}

                //dt1.Rows[TblRowIndex]["wrkdesc"] = wrkdesc;
                //dt1.Rows[TblRowIndex]["qty"] = 0.00;
                //dt1.Rows[TblRowIndex]["crate"] = 0.00;
                //dt1.Rows[TblRowIndex]["comlrate"] = 0.00;
                //dt1.Rows[TblRowIndex]["clrate"] = 0.00;
                //dt1.Rows[TblRowIndex]["cllrate"] = cllrate;
                //dt1.Rows[TblRowIndex]["rate"] = rate;

                dt1.Rows[TblRowIndex]["amt"] = clamt;
                dt1.Rows[TblRowIndex]["disamt"] = 0.00;
                dt1.Rows[TblRowIndex]["netamt"] = clamt;
                dt1.Rows[TblRowIndex]["comqty"] = 0.00;
                dt1.Rows[TblRowIndex]["comamt"] = 0.00;
                dt1.Rows[TblRowIndex]["clamt"] = clamt;
                //   dt1.Rows[TblRowIndex]["location"] = txtgvlocateion;

            }
            Session["tbladwork"] = dt1;
            this.Data_DataBind();
        }
        private string GetSchCode()
        {

            string instype = "14";
            string SchCode = "";

            switch (instype)
            {
                case "11":
                    SchCode = "819880";
                    break;
                case "12":
                    SchCode = "819890";
                    break;
                case "13":
                    SchCode = "819900";
                    break;
                case "14":
                    SchCode = "819910";
                    break;

                case "15":
                    SchCode = "819920";
                    break;

                case "16":
                    SchCode = "819930";
                    break;
                case "17":
                    SchCode = "819940";
                    break;


                case "30":
                    SchCode = "819980";
                    break;


                case "32":
                    SchCode = "819990";
                    break;
                case "33":
                    SchCode = "819970";
                    break;
            }

            if (SchCode == "")
                return "";

            else
            {

                //  //if net amount is negative then  refundable
                //DataTable dt = (DataTable)Session["tbladwork"];
                //double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 :
                //  dt.Compute("sum(amt)", "")));
                //double disamt=Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 :
                //  dt.Compute("sum(amt)", "")));
                //double netamt = amt - disamt;

                //SchCode = (SchCode == "819880" && netamt < 0) ? "819890" : SchCode;
                string comcod = this.GetCompCode();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.ddlUnitName.Text.Trim();

                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPAYSCHCODE", PactCode, Usircode, SchCode, "", "", "", "", "", "");
                SchCode = ds3.Tables[0].Rows[0]["schcode"].ToString();
                this.lblSchCode.Text = ds3.Tables[0].Rows[0]["schcode"].ToString();
            }

            return SchCode;


        }

        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("chkbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("chktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("chksession", Type.GetType("System.String"));
            tblt01.Columns.Add("auditid", Type.GetType("System.String"));
            tblt01.Columns.Add("audittrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("auditsession", Type.GetType("System.String"));
            tblt01.Columns.Add("approvbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("approvtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("approvsession", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }

        private string GetReqApproval(string approval)
        {


            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "Entry":
                    switch (comcod)
                    {
                        case "3101":

                            break;

                        default:
                            if (approval == "")
                            {

                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["chkbyid"] = usrid;
                                dr1["chktrmid"] = trmnid;
                                dr1["chksession"] = session;
                                dr1["auditid"] = usrid;
                                dr1["audittrmid"] = trmnid;
                                dr1["auditsession"] = session;
                                dr1["approvbyid"] = usrid;
                                dr1["approvtrmid"] = trmnid;
                                dr1["approvsession"] = session;

                                dt.Rows.Add(dr1);

                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();

                            }

                            break;
                    }

                    break;

                case "Check":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (approval == "")
                    {
                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();
                        dr1["chkbyid"] = usrid;
                        dr1["chktrmid"] = trmnid;
                        dr1["chksession"] = session;
                        dr1["auditid"] = "";
                        dr1["audittrmid"] = "";
                        dr1["auditsession"] = "";
                        dr1["approvbyid"] = "";
                        dr1["approvtrmid"] = "";
                        dr1["approvsession"] = "";
                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        approval = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["chkbyid"] = usrid;
                        ds1.Tables[0].Rows[0]["chktrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["chksession"] = session;
                        ds1.Tables[0].Rows[0]["auditid"] = "";
                        ds1.Tables[0].Rows[0]["audittrmid"] = "";
                        ds1.Tables[0].Rows[0]["auditsession"] = "";
                        ds1.Tables[0].Rows[0]["approvbyid"] = "";
                        ds1.Tables[0].Rows[0]["approvtrmid"] = "";
                        ds1.Tables[0].Rows[0]["approvsession"] = "";

                        approval = ds1.GetXml();

                    }

                    break;




                case "Audit":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";

                    ds1.Tables[0].Rows[0]["auditid"] = usrid;
                    ds1.Tables[0].Rows[0]["audittrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["auditsession"] = session;

                    approval = ds1.GetXml();

                    break;

                case "Approv":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["approvbyid"] = usrid;
                    ds1.Tables[0].Rows[0]["approvtrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["approvsession"] = session;

                    approval = ds1.GetXml();
                    break;





            }


            return approval;

        }

        protected void lFinalUpdateAdWork_Click(object sender, EventArgs e)
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
            string Gcode = "";
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlUnitName.Text.Trim();
            string curdate = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
            string paysch = (this.lblSchCode.Text.Trim() == "") ? this.GetSchCode() : this.lblSchCode.Text.Trim();

            this.SaveValue();
            DataTable dt = (DataTable)Session["tbladwork"];
            double schamt = 0;
            //bool result;
            string SchCode1 = "";
            if (this.ddlPrevADNumber.Items.Count == 0)
                this.GetAddDedNumber();

            string addno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string narration = this.txtNarr.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Gcode = dt.Rows[i]["gcod"].ToString();
                string wrkdesc = dt.Rows[i]["wrkdesc"].ToString();
                string location = dt.Rows[i]["location"].ToString();
                string qty = Convert.ToDouble(dt.Rows[i]["qty"].ToString()).ToString();
                string clamt = Convert.ToDouble(dt.Rows[i]["clamt"].ToString()).ToString();
                string comqty = Convert.ToDouble(dt.Rows[i]["comqty"].ToString()).ToString();
                string comamt = Convert.ToDouble(dt.Rows[i]["comamt"].ToString()).ToString();
                string appxml = dt.Rows[i]["approval"].ToString();
                string Approval = this.GetReqApproval(appxml);
                double disamt = Convert.ToDouble(dt.Rows[i]["disamt"]);

                double amt = Convert.ToDouble(dt.Rows[i]["amt"].ToString());
                //double amt = (this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17") ? (Convert.ToDouble(dt.Rows[i]["amt"].ToString()) * -1)
                //    : Convert.ToDouble(dt.Rows[i]["amt"].ToString());

                schamt = schamt + amt - disamt;

                string crate = Convert.ToDouble(dt.Rows[i]["crate"].ToString()).ToString();
                string comlrate = Convert.ToDouble(dt.Rows[i]["comlrate"].ToString()).ToString();
                string clrate = Convert.ToDouble(dt.Rows[i]["clrate"].ToString()).ToString();
                string cllrate = Convert.ToDouble(dt.Rows[i]["cllrate"].ToString()).ToString();

                //if (amt > 0)
                MktData.UpdateTransInfo3(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEADWORK", addno, PactCode, Usircode, Gcode, curdate, qty, amt.ToString(), paysch, comamt, wrkdesc, narration, disamt.ToString(), comqty, clamt, location, crate, comlrate, clrate, cllrate, Approval);
            }

            if (Gcode != "")
            {
                SchCode1 = Gcode.Substring(0, 2);
                string gcode = "02003";
                //if net amount is negative then  refundable 
                //  gcode = (gcode == "02090" && schamt < 0) ? "02091" : gcode;
                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INORUPSALGINF1CUMOD", PactCode, Usircode, gcode, SchCode1, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed in Revenue Information ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

            result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, paysch, curdate, schamt.ToString(), "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed in Payment Information ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Modification";
                string eventdesc = "Update Info";
                string eventdesc2 = addno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lbtnTotalAddWork_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }




    }
}