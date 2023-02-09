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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_08_PPlan
{
    public partial class RptPrjCompArchiaInterior : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess MktData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                string qType = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Architectural Report";
                this.GetProjectName();

            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PROJECT_DESIGN", "GETEXPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            this.ddlPrjName.SelectedValue = this.Request.QueryString["prjcode"];

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlPrjName.Enabled = false;
                this.LoadGrid();

            }
            else
            {

                this.lbtnOk.Text = "Ok";
                this.ddlPrjName.Enabled = true;
                this.gvPrjInfo.DataSource = null;
                this.gvPrjInfo.DataBind();
            }
        }


        private void LoadGrid()
        {
            Session.Remove("tbljob");
            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PROJECT_DESIGN", "SHOWPROJECTDESIGNINFO", ProjectCode, "08%", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            Session["tblprocom"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tbljob"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    actcode = dr1["actcode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["actcode"].ToString() == actcode)
                {

                    dr1["actdesc"] = "";
                    dr1["proesam"] = 0.00;
                    dr1["proadam"] = 0.00;
                    dr1["dueam"] = 0.00;

                }


                actcode = dr1["actcode"].ToString();
            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprocom"];
            DataTable dtj = (DataTable)Session["tbljob"];
            DataTable dtusr = (DataTable)Session["tbluser"];

            int i, j = 4, k;
            for (i = 4; i < this.gvPrjInfo.Columns.Count; i++)
                this.gvPrjInfo.Columns[i].Visible = false;

            foreach (DataRow dr1 in dtj.Rows)
            {

                this.gvPrjInfo.Columns[j].Visible = true;
                this.gvPrjInfo.Columns[j].HeaderText = dr1["jobdesc"].ToString();
                j++;


            }


            this.gvPrjInfo.DataSource = dt;
            this.gvPrjInfo.DataBind();

            i = 0;
            foreach (DataRow dr1 in dt.Rows)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {



                    case "1001001": //Percentage 
                    case "1001002": //Amount  


                        for (k = 1; k <= 14; k++)
                        {

                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = true;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;

                        }

                        break;

                    case "1001003": //Concern Person, 
                    case "1001005": //Target Start Date, 
                    case "1001007": //Target End Date, 
                    case "1001009": //Actual Start Date, 
                    case "1001011": //Actual End Date, 

                        for (k = 1; k <= 14; k++)
                        {
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = true;
                        }
                        break;


                    case "1001004": //Dropdown Concern Person,    
                        for (k = 1; k <= 14; k++)
                        {
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvassignuser" + k.ToString())).Visible = true;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;

                            //ddlassignuser = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString()));
                            //ddlassignuser.DataTextField = "usrname";
                            //ddlassignuser.DataValueField = "usrid";
                            //ddlassignuser.DataSource = dtusr;
                            //ddlassignuser.DataBind();
                            //ddlassignuser.SelectedValue = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Text.Trim();
                        }


                        break;


                    case "1001006": //Target Start Date Value, 
                    case "1001008": //Target End Date Value, 
                    case "1001010": //Actual Start Date Value, 
                    case "1001012": //Actual End Date Value, 
                        for (k = 1; k <= 14; k++)
                        {

                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = true;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;
                        }
                        break;

                    default:

                        for (k = 1; k <= 14; k++)
                        {

                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("txtgvassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = true;
                        }


                        break;





                }

                i++;

            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printdate1 = System.DateTime.Now.ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblprocom"];

            var prjList = dt.DataTableToList<RealEntity.C_08_PPlan.BO_Class_Con.ProjectDesign>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_08_PPlan.RptProjectDesign", prjList, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Project Design Report"));
            Rpt1.SetParameters(new ReportParameter("date1", "Date: " + printdate1));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, comnam, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd"); 



                string gcod = DataBinder.Eval(e.Row.DataItem, "gcod").ToString();

                //if (gcod == "1001002")
                //{
                //    TextBox txtgvjob1 = (TextBox)e.Row.FindControl("txtgvjob1");
                //    TextBox txtgvjob2 = (TextBox)e.Row.FindControl("txtgvjob2");
                //    TextBox txtgvjob3 = (TextBox)e.Row.FindControl("txtgvjob3");
                //    TextBox txtgvjob4 = (TextBox)e.Row.FindControl("txtgvjob4");
                //    TextBox txtgvjob5 = (TextBox)e.Row.FindControl("txtgvjob5");
                //    TextBox txtgvjob6 = (TextBox)e.Row.FindControl("txtgvjob6");
                //    TextBox txtgvjob7 = (TextBox)e.Row.FindControl("txtgvjob7");
                //    TextBox txtgvjob8 = (TextBox)e.Row.FindControl("txtgvjob8");
                //    TextBox txtgvjob9 = (TextBox)e.Row.FindControl("txtgvjob9");
                //    TextBox txtgvjob10 = (TextBox)e.Row.FindControl("txtgvjob10");
                //    TextBox txtgvjob11 = (TextBox)e.Row.FindControl("txtgvjob11");
                //    TextBox txtgvjob12 = (TextBox)e.Row.FindControl("txtgvjob12");
                //    TextBox txtgvjob13 = (TextBox)e.Row.FindControl("txtgvjob13");
                //    TextBox txtgvjob14 = (TextBox)e.Row.FindControl("txtgvjob14");   

                //    txtgvjob1.Attributes["style"] = "text-align:right;";
                //    txtgvjob2.Attributes["style"] =  "text-align:right;";
                //    txtgvjob3.Attributes["style"] =  "text-align:right;";
                //    txtgvjob4.Attributes["style"] =  "text-align:right;";
                //    txtgvjob5.Attributes["style"] =  "text-align:right;";
                //    txtgvjob6.Attributes["style"] =  "text-align:right;";
                //    txtgvjob7.Attributes["style"] =  "text-align:right;";
                //    txtgvjob8.Attributes["style"] =  "text-align:right;";
                //    txtgvjob9.Attributes["style"] =  "text-align:right;";
                //    txtgvjob10.Attributes["style"] = "text-align:right;";
                //    txtgvjob11.Attributes["style"] = "text-align:right;";
                //    txtgvjob12.Attributes["style"] = "text-align:right;";
                //    txtgvjob13.Attributes["style"] = "text-align:right;";
                //    txtgvjob14.Attributes["style"] = "text-align:right;";

                //}

                if (gcod == "1001003" || gcod == "1001005" || gcod == "1001007" || gcod == "1001009" || gcod == "1001011")
                {
                    Label lblgvjob1 = (Label)e.Row.FindControl("lblgvjob1");
                    Label lblgvjob2 = (Label)e.Row.FindControl("lblgvjob2");
                    Label lblgvjob3 = (Label)e.Row.FindControl("lblgvjob3");
                    Label lblgvjob4 = (Label)e.Row.FindControl("lblgvjob4");
                    Label lblgvjob5 = (Label)e.Row.FindControl("lblgvjob5");
                    Label lblgvjob6 = (Label)e.Row.FindControl("lblgvjob6");
                    Label lblgvjob7 = (Label)e.Row.FindControl("lblgvjob7");
                    Label lblgvjob8 = (Label)e.Row.FindControl("lblgvjob8");
                    Label lblgvjob9 = (Label)e.Row.FindControl("lblgvjob9");
                    Label lblgvjob10 = (Label)e.Row.FindControl("lblgvjob10");
                    Label lblgvjob11 = (Label)e.Row.FindControl("lblgvjob11");
                    Label lblgvjob12 = (Label)e.Row.FindControl("lblgvjob12");
                    Label lblgvjob13 = (Label)e.Row.FindControl("lblgvjob13");
                    Label lblgvjob14 = (Label)e.Row.FindControl("lblgvjob14");


                    // e.Row.Attributes["style"] = "background:gray;";
                    lblgvjob1.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold; text-align:center;";
                    lblgvjob2.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob3.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob4.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob5.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob6.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob7.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob8.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob9.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob10.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob11.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob12.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob13.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob14.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";


                }





            }

        }


        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbljob"];
            this.Data_Bind();

        }




        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }



    }
}



