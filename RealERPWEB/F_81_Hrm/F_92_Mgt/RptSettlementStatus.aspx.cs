using Microsoft.Reporting.WinForms;
using RealEntity.C_81_Hrm.C_81_Rec;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptSettlementStatus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess feaData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Settlement Top Sheet";

                this.CommonButton();
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                GetWorkStation();
                GetAllOrganogramList();

                this.lnkbtnSerOk_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkBtnAdd_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../../F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");

        }

        private void CommonButton()
        {

            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }
        private void ShowValue()
        {

            string comcod = this.GetComCode();

            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtdateto.Text.ToString();

            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 2) + "%";
            //string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string div = "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "SHOW_SEPERATED_EMP", fdate, tdate, empType, div, Dept, section, "", "", "");

            if (ds2 == null)
            {
                this.gvSettInfo.DataSource = null;
                this.gvSettInfo.DataBind();
                return;
            }

            ViewState["tblSetTopInfo"] = ds2.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();

            this.Data_Bind();

        }
        private void Data_Bind()
        {

            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];

            this.gvSettInfo.DataSource = sttlmntinfo;
            this.gvSettInfo.DataBind();

            this.FooterCal();
            Session["Report1"] = gvSettInfo;
            if (sttlmntinfo.Count > 0)
            {
                ((HyperLink)this.gvSettInfo.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        private void FooterCal()
        {
            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];

            //DataTable dt = (DataTable)ViewState["tblSetTopInfo"];
            //if (dt==null)
            //{
            //    return;
            //}
            //if (dt.Rows.Count == 0)
            //    return;



            //((Label)this.gvSettInfo.FooterRow.FindControl("lblFoterCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cost)", "")) ?
            //   0.00 : dt.Compute("Sum(cost)", ""))).ToString("#,##0;(#,##0); ");

            //((Label)this.gvSettInfo.FooterRow.FindControl("lblFoterRev")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(revenue)", "")) ?
            //   0.00 : dt.Compute("Sum(revenue)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string comcod = this.GetComCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fdate = this.txtDatefrom.Text.ToString();
            //string tdate = this.txtdateto.Text.ToString();
            //string ToFrDate = "(From :" + fdate + " To " + tdate + ")";
            //DataTable dt = (DataTable)ViewState["tblSetTopInfo"];

            //var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.OrderStatus>();

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptOrderStatus", lst, null, null);
            //rpt1.EnableExternalImages = true;

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("ToFrDate", ToFrDate));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Order Status"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            ////rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            ////rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvSettInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");
                //HyperLink lnkPrint = (HyperLink)e.Row.FindControl("HypRDDoPrint");

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).Trim().ToString();
                string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprvstatus"));


                if (apstatus == "False")
                {
                    lnkEdit.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpSettlement?Type=Entry&actcode=" + empid;
                }
                else
                {
                    lnkEdit.Text = "<span class='glyphicon glyphicon-lock'></span>";
                    lnkEdit.CssClass = "btn btn-xs btn-danger";
                    lnkEdit.ToolTip = "Approved";
                }
                //lnkELink.NavigateUrl = "~/F_01_Mer/MerPRCodeBook?BookName=Project";


            }

        }




        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void HypRDDoPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            int index = row.RowIndex;
            string empid = "";
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            empid = ((Label)this.gvSettInfo.Rows[index].FindControl("lblgvempid")).Text.ToString();
            var emplistall = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["tblSetTopInfo"];
            var emplist = emplistall.FindAll(p => p.empid == empid);

            var deptcode = emplist[0].deptcode.Substring(0, 4);
            string comcod = this.GetComCode();
            DataSet ds3 = feaData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_EMP_SETTLEMENT_INFO", empid, deptcode, "", "", "", "", "", "");
            var lst1 = ds3.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            var list1 = lst1.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = lst1.FindAll(p => p.hrgcod.Substring(0, 3) == "352");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            billDate = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(billDate).ToString("dd"))) + "-" + (Convert.ToDateTime(billDate).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(billDate).ToString("yyyy")));

            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            joining = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joining).ToString("dd"))) + "-" + (Convert.ToDateTime(joining).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joining).ToString("yyyy")));
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            sepdate = GetMonthName(GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(sepdate).ToString("dd"))) + "-" + (Convert.ToDateTime(sepdate).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(sepdate).ToString("yyyy")));
            double netamount = Convert.ToDouble("0" + (lst1.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - lst1.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)));
            string servicelength = emplist[0].servleng.ToString();

            double netpay = Convert.ToDouble(netamount);

            string inword = ASTUtility.Trans(netpay, 2).ToString().Trim().Replace(" মাত্র )", ""); ;

            LocalReport rpt1 = new LocalReport();
            var EmpType = emplist[0].deptcode.Substring(0, 4);
            if (EmpType == "9403" || EmpType == "9402")
            {
                comnam = "এডিসন ফুটওয়্যার লিমিটেড:"; //hst["comnam"].ToString();
                comadd = "তালতলী, মির্জাপুর ,গাজীপুর";
                servicelength = emplist[0].servleng.ToString();
                string[] words = servicelength.Split(' ');
                char[] numbers;
                var i = 0;
                foreach (var item in words)
                {
                    if (item != null)
                    {
                        Regex reg = new Regex("[0-9]");

                        if (!reg.IsMatch(item))
                        {
                            switch (item)
                            {

                                case "month":
                                    words[i] = "মাস "; break;
                                case "days":
                                    words[i] = "দিন "; break;
                                case "Year":
                                    words[i] = "বছর "; break;
                            }
                        }
                        else
                        {
                            numbers = item.ToCharArray();
                            var count = 0;
                            foreach (var n in numbers)
                            {
                                switch (n)
                                {

                                    case '1':
                                        numbers[count] = '১'; break;
                                    case '2':
                                        numbers[count] = '২'; break;
                                    case '3':
                                        numbers[count] = '৩'; break;
                                    case '4':
                                        numbers[count] = '৪'; break;
                                    case '5':
                                        numbers[count] = '৫'; break;
                                    case '6':
                                        numbers[count] = '৬'; break;
                                    case '7':
                                        numbers[count] = '৭'; break;
                                    case '8':
                                        numbers[count] = '৮'; break;
                                    case '9':
                                        numbers[count] = '৯'; break;
                                    case '0':
                                        numbers[count] = '০'; break;
                                }
                                count++;
                            }
                            words[i] = "";
                            foreach (var num in numbers)
                            {
                                words[i] += num.ToString();
                            }

                        }
                    }

                    i++;
                }
                servicelength = "";
                foreach (var item in words)
                {
                    servicelength += item + " ";

                }
                rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSattelmentBangla", list1, list2, null);
                rpt1.SetParameters(new ReportParameter("rpttitle", "চূড়ান্ত নিষ্পত্তিকরন বিল"));
            }
            else
            {
                rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSattelment", list1, list2, null);
                rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Final Sattelment"));
            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount.ToString("#,##0.00;(#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name.ToString().Trim()));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", inword));


            Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        public string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        //private void GetDivision()
        //{

        //    string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
        //    string comcod = GetComCode();
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string userid = hst["usrid"].ToString();
        //    List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


        //    var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
        //    RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
        //    lst1.Add(all);

        //    this.ddlDivision.DataTextField = "actdesc";
        //    this.ddlDivision.DataValueField = "actcode";
        //    this.ddlDivision.DataSource = lst1;
        //    this.ddlDivision.DataBind();
        //    this.ddlDivision.SelectedValue = "000000000000";

        //    this.ddlDivision_SelectedIndexChanged(null, null);

        //}

        private void GetDeptList()
        {
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDeptList();
        }

        //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetDeptList();
        //}
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
    }
}