
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class LetterInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtdate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
            ((Label)this.Master.FindControl("lblTitle")).Text = "Letter Interface";
            getLetterCount();
            getAllData();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void getAllData()
        {
            Session.Remove("tbldata");
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETALLREC", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvAllRec.DataSource = null;
                this.gvAllRec.DataBind();
                return;
            }


            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = ds.Tables[0];
            string type = this.RadioButtonList1.SelectedValue.ToString();
            //offer letter
            if (type == "10003")
            {
                view.RowFilter = "sendappflag='False' and sendconflag='False'  and isreject='false' and isoffaccept='False' ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            //appointment letter sendappflag=0 and   isreject=0 and isappaccept=1
            else if (type == "10002")
            {
                view.RowFilter = " isreject='False' and isoffaccept='True' and isappaccept='False'  ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            //confirmation letter
            else if (type == "10025")
            {
                view.RowFilter = "sendappflag='True' and sendconflag='False' and sendoffflag='True'  and isreject='False'";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            //rejected letter
            else if (type == "reject")
            {
                view.RowFilter = "isreject='True'";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            //accepted offer letter letter
            else if (type == "acceptoffletter")
            {
                view.RowFilter = "isoffaccept='True' and isappaccept='False' and isreject='False' and sendappflag='False' ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }else if(type== "acceptappletter")
            {
                view.RowFilter = "isappaccept='True' and isoffaccept='True' and  isreject='False'";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }


            Session["tbldata"] = dt1;

            //DataTable ds = (DataTable)Session["tbldata"];
            this.gvAllRec.DataSource = dt1;
            this.gvAllRec.DataBind();
        }

        //private void loadGrid()
        //{
        //    DataTable ds = (DataTable)Session["tbldata"];
        //    this.gvAllRec.DataSource = ds;
        //    this.gvAllRec.DataBind();
        //}
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            return (hst["comcod"].ToString());
        }

        private void getLetterCount()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "LETTER_COUNT", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                return;
            this.RadioButtonList1.Items[0].Text = "<a>Eligible for offer letter" + "<span class=lbldata>" + ds1.Tables[0].Rows[0]["offletter"].ToString() + "</span></a>";
            this.RadioButtonList1.Items[1].Text = "<a> Accepted Offer Letter" + "<span class=lbldata>" + ds1.Tables[0].Rows[0]["acceptedOffLetter"].ToString() + "</span></a>";
            this.RadioButtonList1.Items[2].Text = "<a> Rejected Letter" + "<span class=lbldata>" + ds1.Tables[0].Rows[0]["rejectedLetter"].ToString() + "</span></a>";
            this.RadioButtonList1.Items[3].Text = "<a>Eligible for appointment letter" + "<span class=lbldata>" + ds1.Tables[0].Rows[0]["appletter"].ToString() + "</span></a>";
            this.RadioButtonList1.Items[4].Text = "<a> Accepted Appointment Letter" + "<span class=lbldata>" + ds1.Tables[0].Rows[0]["acceptedAppLetter"].ToString() + "</span></a>";

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.RadioButtonList1.SelectedValue.ToString();
            switch (type)
            {
                case "10003":
                    //this.gvAllRec.FindControl["lnkOfferLetter"]
                    getAllData();
                    
                    break;
                case "10002":
                    getAllData();
                    break;
                case "reject":
                    getAllData();
                    break;
                case "acceptoffletter":
                    getAllData();
                    break;
                case "acceptappletter":
                    getAllData();
                    break;
            }

        }

        protected void gvAllRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string type = this.RadioButtonList1.SelectedValue.ToString();

                string sendofflag = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sendoffflag")).ToString();
                string sendappflag = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sendappflag")).ToString();

                string offaccept = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isoffaccept")).ToString();
                string appaccept = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isappaccept")).ToString();
                string isreject = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isreject")).ToString();


                Label lblstatus = e.Row.FindControl("lblstatus") as Label;


                if (type == "10003")
                {
                    if(sendofflag=="True" && offaccept == "False" && sendappflag=="False")
                    {
                        lblstatus.Text = "Not accepted yet";
                        ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = true;
                        ((LinkButton)e.Row.FindControl("RejectLetter")).Visible = true;
                    }
                    else
                    {
                        lblstatus.Text = "Not send yet";
                    }

                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = false;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;

                }else if(type== "10002")
                {
                    if (sendappflag == "True" && appaccept == "False" && sendappflag=="True" && offaccept=="True")
                    {
                        lblstatus.Text = "Not accepted yet";
                        ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = true;
                        ((LinkButton)e.Row.FindControl("RejectLetter")).Visible = true;


                    }
                    else
                    {
                        lblstatus.Text = "Not send yet";

                    }

                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
                }
                else if (type == "reject")
                {
                    if (isreject == "True" && sendofflag == "True" && offaccept=="False")
                    {
                        lblstatus.Text = "Offer Letter";
                        ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = true;

                    }
                    else if (isreject == "True" && sendofflag == "True" && offaccept == "True"  && appaccept =="False" )
                    {
                        lblstatus.Text = "Appointment";
                        ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = true;

                    }
                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = false;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = false;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = true;

                }
                else if (type == "acceptoffletter")
                {
                    lblstatus.Text = "Accepted";
                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = false;


                }
                else if (type == "acceptappletter")
                {
                    lblstatus.Text = "Accepted";

                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkAccept")).Visible = false;

                }

            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string btnvalue = this.RadioButtonList1.SelectedValue.ToString();

            switch (btnvalue)
            {
                case "10003":
                    this.PrintEmpOfferLetter();
                    break;

                case "10002":
                    this.PrintEmpApointmentLetter();
                    break;

            }


        }

        private void PrintEmpOfferLetter()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldata"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpOfferLetter>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpOfferLetter", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Ready To Send Offer Letter"));
            //Rpt1.SetParameters(new ReportParameter("txtProjName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString()));
            //Rpt1.SetParameters(new ReportParameter("txtDate", "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "  To:  " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpApointmentLetter()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldata"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpOfferLetter>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpApointmentLetter", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Ready To Send Apointment Letter"));
            //Rpt1.SetParameters(new ReportParameter("txtProjName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString()));
            //Rpt1.SetParameters(new ReportParameter("txtDate", "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "  To:  " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void RejectLetter_Click(object sender, EventArgs e)
        {
         string comcod=   this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string advno = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();
            bool result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "CHANGELETTERSTATUS", advno, "reject", "True", "", "", "", "", "", "");
            if (result)
            {
               string Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                getLetterCount();
                getAllData();
               
            }

        }

        protected void lnkAccept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string advno = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();
            //string isofflsend = ((Label)this.gvAllRec.Rows[index].FindControl("sendoffflag")).Text.ToString();
            //string isappsend = ((Label)this.gvAllRec.Rows[index].FindControl("lbladvno")).Text.ToString();
           // int selectIndex = this.RadioButtonList1.SelectedIndex;
            //if (selectIndex == 0 && isofflsend == "True")
            //{

            //}
            //else if (selectIndex == 1)
            //{

            //}
            bool result = false;

            string type = this.RadioButtonList1.SelectedValue.ToString();
            if(type== "10003")
            {
                 result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "CHANGELETTERSTATUS", advno, "acceptoffletter", "True", "False", "", "", "", "", "");
  
            }
            else if (type == "reject")
            {
                result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "CHANGELETTERSTATUS", advno, "reject", "False", "", "", "", "", "", "");

            }
            else if(type=="10002")
            {
                 result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "CHANGELETTERSTATUS", advno, "acceptappletter", "True", "False", "", "", "", "", "");
  
            }
            if (result)
            {
                string Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                getLetterCount();
                getAllData();
               
            }


        }
    }
}