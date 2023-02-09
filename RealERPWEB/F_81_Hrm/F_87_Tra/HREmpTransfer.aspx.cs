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

namespace RealERPWEB.F_81_Hrm.F_87_Tra
{
    public partial class HREmpTransfer1 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtpatplacedate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE TRANSFER INFORMATION";
                this.Get_Trnsno();
                this.tableintosession();
                this.GetCompany();
                this.GetToCompany();

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
        protected void GetPrvNm()
        {
            string comcod = GetCompCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", mREQDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxtrnno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }
        private void Get_Trnsno()
        {
            string comcod = this.GetCompCode();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);
        }
        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("desigid", Type.GetType("System.String"));
            dttemp.Columns.Add("tfcompany", Type.GetType("System.String"));
            dttemp.Columns.Add("tfprjcode", Type.GetType("System.String"));
            dttemp.Columns.Add("ttcompany", Type.GetType("System.String"));
            dttemp.Columns.Add("ttprjcode", Type.GetType("System.String"));

            dttemp.Columns.Add("tfcomdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("tfprjdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("ttcomdesc", Type.GetType("System.String"));

            dttemp.Columns.Add("ttprjdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("pplacedate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("rmrks", Type.GetType("System.String"));
            dttemp.Columns.Add("infor", Type.GetType("System.String"));
            dttemp.Columns.Add("spnote", Type.GetType("System.String"));
            dttemp.Columns.Add("address", Type.GetType("System.String"));
            Session["sessionforgrid"] = dttemp;
        }
        protected void GetSection()
        {
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtsrchSection = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", Company, txtsrchSection, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlistfrom.DataTextField = "actdesc";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);
        }
        private void GetToSection()
        {
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlToCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlToCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txttoSection = "%%";

            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", Company, txttoSection, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlistto.DataTextField = "actdesc";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = ds1.Tables[0];
            this.ddlprjlistto.DataBind();
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string company = this.ddlCompany.SelectedItem.Text.Trim();
            DataTable dt1 = (DataTable)Session["sessionforgrid"];

            List<RealEntity.C_81_Hrm.C_87_Tra.EmployeeTransInfo01> list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_87_Tra.EmployeeTransInfo01>();
            LocalReport Rpt1 = new LocalReport();
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string transferno = this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
            if (this.rbtTrnstype.SelectedIndex == 0)
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_87_Tra.RptEmployeeTransfer", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("trnsno", transferno));
                Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("finanmatter", this.txtfmaters.Text.Trim()));
                Rpt1.SetParameters(new ReportParameter("specailnote", this.txtspnote.Text.Trim()));
                Rpt1.SetParameters(new ReportParameter("footer", printFooter));


            }
            else
            {
                string description = "Management has decided to transfer you in the following department / project.You  are requested  to confirm your reporting in the following place on or before " + list[0].pplacedate.ToString("dd-MMM-yyyy");
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_87_Tra.RptEmployeeTransfer02", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("date", Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("trnsno", transferno));
                Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("comname", comnam));
                Rpt1.SetParameters(new ReportParameter("description", description));
                Rpt1.SetParameters(new ReportParameter("footer", printFooter));




            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void Load_Prev_Trans_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlCompany.Visible = true;
                this.pnlToCompany.Visible = true;
                this.pnlremarks.Visible = true;
                this.txtCurTransDate.Enabled = true;
                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    string comcod = this.GetCompCode();
                    DataSet ds = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    Session["sessionforgrid"] = ds.Tables[0];
                    this.txtfmaters.Text = ds.Tables[0].Rows[0]["infor"].ToString();
                    this.txtspnote.Text = ds.Tables[0].Rows[0]["spnote"].ToString();
                    this.grvacc_DataBind();
                    this.Load_Cur_Trans_NO();
                }
                else
                {
                    this.Get_Trnsno();
                }
                this.lbtnPrevTransList.Visible = false;
                this.ddlPrevISSList.Visible = false;
            }
            else
            {
                this.txtfmaters.Text = "";
                this.txtspnote.Text = "";
                this.ddlPrevISSList.Items.Clear();
                this.txtCurTransDate.Enabled = true;
                this.lbtnPrevTransList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                //Session("sessionforgrid").remove;
                Session.Remove("sessionforgrid");
                this.tableintosession();
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.pnlCompany.Visible = false;
                this.pnlToCompany.Visible = false;
                this.pnlremarks.Visible = false;
                lbtnOk.Text = "Ok";
            }
        }

        protected void grvacc_DataBind()
        {
            this.grvacc.DataSource = (DataTable)Session["sessionforgrid"];
            this.grvacc.DataBind();
        }
        protected void Employee_List()
        {
            Session.Remove("tblemp");
            string comcod = this.GetCompCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" :
                    this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode =  "%";//this.ddlprjlistfrom.SelectedValue.ToString() + "%";
            string emplist = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", pactcode, emplist, company, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            Session["tblemp"] = ds1.Tables[0];

            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            DataTable dt = ds1.Tables[0];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlprjlistfrom.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
                this.ddlCompany.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.txtEmpDesignation.Text = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
            }
        }

        private void GetComASecSelected()
        {
            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            if (empid == "000000000000" || empid == "")
                return;
            DataTable dt = (DataTable)Session["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompany.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                //this.ddlDepartment.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
                // this.ddlProjectName_SelectedIndexChanged(null,null);
                this.ddlprjlistfrom.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
                this.txtEmpDesignation.Text = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
            }
        }
        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");

            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }

        protected void imgbtnSection_Click(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.Employee_List();
        }

        protected void imgbtntoCompany_Click(object sender, EventArgs e)
        {
            this.GetToCompany();
        }

        protected void imgbtnToSection_Click(object sender, EventArgs e)
        {
            this.GetToSection();
        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string empid = this.ddlEmpList.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] projectrow2 = dt.Select("empid = '" + empid + "'");
            if (projectrow2.Length > 0)
            {
                return;

            }
            DataRow drforgrid = dt.NewRow();
            drforgrid["empid"] = empid;
            drforgrid["idcardno"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["idcardno"];
            drforgrid["empname"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["empname"].ToString().Substring(7);
            drforgrid["desigid"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desigid"];
            drforgrid["desig"] = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["desig"];

            drforgrid["tfcompany"] = this.ddlCompany.SelectedValue.ToString();
            drforgrid["tfprjcode"] = this.ddlprjlistfrom.SelectedValue.ToString();
            drforgrid["tfcomdesc"] = this.ddlCompany.SelectedItem.Text.ToString();
            drforgrid["tfprjdesc"] = this.ddlprjlistfrom.SelectedItem.Text.Substring(13);

            drforgrid["ttcompany"] = this.ddlToCompany.SelectedValue.ToString();
            drforgrid["ttprjcode"] = this.ddlprjlistto.SelectedValue.ToString();
            drforgrid["ttcomdesc"] = this.ddlToCompany.SelectedItem.Text.ToString();
            drforgrid["ttprjdesc"] = this.ddlprjlistto.SelectedItem.Text.Substring(13);
            drforgrid["pplacedate"] = this.GetStdDate(this.txtpatplacedate.Text.Trim());
            //drforgrid["Comname"] = this.ddlCompany.SelectedItem.Text.Trim();
            dt.Rows.Add(drforgrid);
            Session["sessionforgrid"] = dt;
            this.grvacc_DataBind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            int TblRowIndex;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                string txtremarks = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvremarks")).Text;
                TblRowIndex = (grvacc.PageIndex) * grvacc.PageSize + i;
                dt.Rows[TblRowIndex]["rmrks"] = txtremarks;
            }
            Session["sessionforgrid"] = dt;


        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Employee_List();
        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
                string sessionid = hst["session"].ToString();
                string trmid = hst["compname"].ToString();
                this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["sessionforgrid"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPrvNm();
                }
                //string curdate = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                string information = this.txtfmaters.Text.Trim();
                string spnote = this.txtspnote.Text.Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string fromprj = dt.Rows[i]["tfprjcode"].ToString();
                    string toprj = dt.Rows[i]["ttprjcode"].ToString();
                    string remarks = dt.Rows[i]["rmrks"].ToString();
                    string date = Convert.ToDateTime(dt.Rows[i]["pplacedate"]).ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();
                    bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPHREMPTNSINF", tansno, fromprj, toprj, empid,
                         curdate, remarks, information, spnote, date, desigid, userid, sessionid, trmid, postDat, "");
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }
        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%%";
            DataSet ds5 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds5.Tables[0];
            ds5.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }

        private void GetToCompany()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string userid = hst["usrid"].ToString();
            //string txtCompany = "%" + this.txtSrctoCompany.Text.Trim() + "%";
            //DataSet ds5 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETTRNSCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            DataTable dt = (DataTable)Session["tblcompany"];

            this.ddlToCompany.DataTextField = "actdesc";
            this.ddlToCompany.DataValueField = "actcode";
            this.ddlToCompany.DataSource = dt;
            this.ddlToCompany.DataBind();
            this.ddlToCompany_SelectedIndexChanged(null, null);
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptment();
        }

        protected void ddlToCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToDeptment();
        }

        private void GetToDeptment()
        {
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlTodept.DataTextField = "actdesc";
            this.ddlTodept.DataValueField = "actcode";
            this.ddlTodept.DataSource = ds1.Tables[0];
            this.ddlTodept.DataBind();

            ddlTodept_SelectedIndexChanged(null,null);
        }

        private void GetDeptment()
        {
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }
        

        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblemp"];
            //string empid = this.ddlEmpList.SelectedValue.ToString();
            //DataRow[] dr = dt.Select("empid='" + empid + "'");
            //if (dr.Length == 0)
            //{
            //    this.txtEmpDesignation.Text = "";
            //    return;

            //}
            //this.txtEmpDesignation.Text = dr[0]["desig"].ToString();
            this.GetComASecSelected();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void ddlTodept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToSection();
        }

       
    }
}