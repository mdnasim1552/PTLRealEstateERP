using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
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
namespace RealERPWEB.F_22_Sal
{
    public partial class RptClientDateFile : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Budget";


                this.GetProjectName();

                GetEnvType();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCLIENTPRJNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        private void GetEnvType()
        {

            string comcod = this.GetCompCode();
          
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETENVTYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                return;
            this.ddlTypeHeader.DataTextField = "title";
            this.ddlTypeHeader.DataValueField = "gcod";
            this.ddlTypeHeader.DataSource = ds1.Tables[0];
            this.ddlTypeHeader.DataBind();
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowDetails();


        }
        private void ShowDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "CLIENTINFODETAILS", PactCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblfiledetails"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }
            }
            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfiledetails"];
            this.gvFileData.DataSource = dt;
            this.gvFileData.DataBind();

            Session["Report1"] = gvFileData;
            ((HyperLink)this.gvFileData.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {



            //DataTable dt = (DataTable)Session["tblSupbill"];

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //var lst = dt.DataTableToList<RealEntity.C_14_Pro.suppbgd>();
            //LocalReport Rpt1 = new LocalReport();

            //Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_14_Pro.RptSuplierBgd", lst, null, null);

            //Rpt1.SetParameters(new ReportParameter("title", "Supplier Budget"));

            //Rpt1.SetParameters(new ReportParameter("txtcomname", comnam));


            //// Rpt1.SetParameters(new ReportParameter("footer", printFooter));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        //protected void gvFileData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string name = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "name")).ToString();
        //        string address1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "address1")).ToString();
        //        string address2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "address2")).ToString();
        //        string address3 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "address3")).ToString();
        //        string address4 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "address4")).ToString();
        //        string address5 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "address5")).ToString();
        //        //hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry.aspx?InputType=ReqFirstApproved&prjcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod1;

        //    }
        //}
        protected void lnkbtnEnvelop_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

                int index = row.RowIndex;
                string name = ((Label)row.FindControl("lgvCusName")).Text.ToString();
                string address1 = ((Label)row.FindControl("lgvAddress1")).Text.ToString();
                string address2 = ((Label)row.FindControl("lgvAddress2")).Text.ToString();
                string address3 = ((Label)row.FindControl("lgvAddress3")).Text.ToString();
                string address4 = ((Label)row.FindControl("lgvAddress4")).Text.ToString();
                string address5 = ((Label)row.FindControl("lgvAddress5")).Text.ToString();
                string peradd = ((Label)row.FindControl("lblperadd")).Text.ToString();

                string typeheader = ddlTypeHeader.SelectedItem.Text.ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();

                string session = hst["session"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string envtype = this.ddlTypeHeader.SelectedValue.ToString();

                var list = new List<RealEntity.C_22_Sal.EnvelopModel>();
                var obj = new RealEntity.C_22_Sal.EnvelopModel();
                if (comcod == "3368")
                {
                     obj = new RealEntity.C_22_Sal.EnvelopModel()
                    {
                        Name = name,
                        Address1 = peradd,
                        Address2 = address2,
                        Address3 = address3,
                        Address4 = address4,
                        Address5 = address5
                    };
                }
                else
                {
                     obj = new RealEntity.C_22_Sal.EnvelopModel()
                    {
                        Name = name,
                        Address1 = address1,
                        Address2 = address2,
                        Address3 = address3,
                        Address4 = address4,
                        Address5 = address5
                    };
                }
                    list.Add(obj);
                LocalReport Rpt1 = new LocalReport();

                if (comcod == "3368")
                {
                    switch (envtype)
                    {
                        case "7200001":
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                        case "7200002":
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                        case "7200003":
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                        case "7200004":
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                        case "7200005":
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                        default:
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopOffice", list, null, null);
                            break;
                    }
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptEnvelopNew", list, null, null);
               
                }
               
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("toheader", typeheader));

                Session["Report1"] = Rpt1;
                string type = "PDF";
                ScriptManager.RegisterStartupScript(this, GetType(), "target", "printEnvelop('" + type + "');", true);
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }


        }


    }
}