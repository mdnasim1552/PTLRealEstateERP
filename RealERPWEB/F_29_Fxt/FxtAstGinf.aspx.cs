using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using AjaxControlToolkit;
using System.IO;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_29_Fxt
{

    public partial class FxtAstGinf : System.Web.UI.Page
    {
        ProcessAccess FAstData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "FIXED ASSET INFORMATION VIEW/EDIT";
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);



                var type = this.Request.QueryString["Type"].ToString().Trim();
                if (type == "Fselection")
                {
                    var serial1 = this.Request.QueryString["slno"].ToString().Trim();
                    this.lblserial.Text = serial1;
                    this.imgpannel.Visible = true;
                    this.GetFixedAssetName();
                    this.LoadGrid();
                    this.LoadImg();
                }

            }

            if (this.ddlFAstName.Items.Count == 0)
            {
                this.GetFixedAssetName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void GetFixedAssetName()
        {
            var type = this.Request.QueryString["Type"].ToString().Trim();
            var rcode = this.Request.QueryString["sircode"].ToString().Trim();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = type == "Fselection" ? rcode : "%" + this.txtSrcFAst.Text + "%";
            DataSet ds1 = FAstData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXEDASSETNAME", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlFAstName.DataTextField = "sirdesc";
            this.ddlFAstName.DataValueField = "sircode";
            this.ddlFAstName.DataSource = ds1.Tables[0];
            this.ddlFAstName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetFixedAssetName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.imgpannel.Visible = true;

            this.LoadGrid();
            this.LoadImg();
            //this.ClearScreen();
        }

        private void ClearScreen()
        {
            this.ddlFAstName.Visible = true;

            this.gvFAstInf.DataSource = null;
            this.gvFAstInf.DataBind();
        }

        private void LoadGrid()
        {

            //var type = this.Request.QueryString["Type"].ToString().Trim();
            //var rcode = this.Request.QueryString["sircode"].ToString().Trim();
            var assetcode = this.Request.QueryString["genno"].ToString().Trim();

            Session.Remove("tblcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //  string FAstCode = type == "Fselection" ? rcode : this.ddlFAstName.SelectedValue.ToString();
            string FAstCode = this.ddlFAstName.SelectedValue.ToString();
            DataSet ds1 = FAstData.GetTransInfoNew(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXEDASSETINF", null, null, null, FAstCode, assetcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvFAstInf.DataSource = null;
                this.gvFAstInf.DataBind();
                return;
            }
            this.gvFAstInf.DataSource = this.HiddenSameData(ds1.Tables[0]);

            ViewState["GenInfo"] = ds1.Tables[0];
            this.gvFAstInf.DataBind();
            this.DateVisible();

            //this.gvFAstInf.DataBind();
            // ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;


        }

        private void DateVisible()
        {
            DataTable dt = (DataTable)ViewState["GenInfo"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gval = dt.Rows[i]["fxtgval"].ToString();

                if (Gval == "T")
                {
                    ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    // ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvdVal")).Visible = false;
                }

                else if (Gval == "N")
                {

                    ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvdVal")).Visible = false;

                }
                else
                {
                    ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvVal")).Visible = false;
                }



            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //string AssetImg="";
            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string astname = this.ddlFAstName.SelectedItem.ToString();

            //DataTable dt = (DataTable)ViewState["GenInfo"];
            //DataTable dti = (DataTable)ViewState["fiximg"];
            //if (dti.Rows.Count != 0)
            //    AssetImg = new Uri(Server.MapPath(@"~/Uploads/" + dti.Rows[0]["imginf"].ToString())).AbsoluteUri;

            //var lst1 = dt.DataTableToList<MFGOBJ.C_25_Fxt.FxtGenInfo>();


            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass1.GetLocalReport("RD_25_Fxt.RptFixAssGenInfo", lst1, null, null);
            //rpt1.EnableExternalImages = true;


            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Asset Details"));
            //rpt1.SetParameters(new ReportParameter("AssetName", astname.Substring(13)));
            //rpt1.SetParameters(new ReportParameter("AssetImg", AssetImg));
            //rpt1.SetParameters(new ReportParameter("footer", ""));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            var newstr = this.Request.QueryString["genno"].ToString().Trim();


            var assetcod = newstr.Replace("^", "&");


            //var type = this.Request.QueryString["Type"].ToString().Trim();
            //var rcode = this.Request.QueryString["sircode"].ToString().Trim();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ssirCode = this.ddlFAstName.SelectedValue.ToString();
            // string ssirCode = type == "Fselection" ? rcode : this.ddlFAstName.SelectedValue.ToString();

            for (int i = 0; i < this.gvFAstInf.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvFAstInf.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvFAstInf.Rows[i].FindControl("lgvgval")).Text.Trim();

                string Gvalue = gtype == "D" ? ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvdVal")).Text.Trim() : ((TextBox)this.gvFAstInf.Rows[i].FindControl("txtgvVal")).Text.Trim();
                if (Gcode == "01003")
                {
                    if (Gvalue == "")
                    {
                        Gvalue = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    }
                }


                FAstData.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTORUPDATEFIXEDASSETINF", ssirCode, Gcode, gtype, Gvalue, assetcod, "", "", "", "", "", "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Updated Info";
                string eventdesc2 = this.ddlFAstName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string advno = dt1.Rows[0]["gcod"].ToString();
            //string deptcode = dt1.Rows[0]["deptcode"].ToString();
            //string postcode = dt1.Rows[0]["postcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == advno)
                {
                    dt1.Rows[j]["gdesc"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["gcod"].ToString() == advno)
                    {
                        dt1.Rows[j]["gdesc"] = "";
                    }
                    advno = dt1.Rows[j]["gcod"].ToString();
                }
            }

            return dt1;
        }


        protected void LoadImg()
        {
            //var type = this.Request.QueryString["Type"].ToString().Trim();
            //var rcode = this.Request.QueryString["sircode"].ToString().Trim();
            var assetcod = this.Request.QueryString["genno"].ToString().Trim();

            imgFix.ImageUrl = "";
            string comcod = this.GetCompCode();
            string ssircode = this.ddlFAstName.SelectedValue.ToString();
            // string ssircode = type == "Fselection" ? rcode : this.ddlFAstName.SelectedValue.ToString();

            DataSet ds = FAstData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXAIMAGESHOW", ssircode, assetcod, "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;

            imgFix.ImageUrl = "~/Uploads/" + ds.Tables[0].Rows[0]["imginf"].ToString();

            ViewState["fiximg"] = ds.Tables[0];
        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            var assetcod = this.Request.QueryString["genno"].ToString().Trim();

            string comcod = this.GetCompCode();
            string Url = "";
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            // i = i + 1;
            string ssircode = "";

            if (AsyncFileUpload1.HasFile)
            {
                ssircode = this.ddlFAstName.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/") + comcod + ssircode + extension);

                Url = comcod + ssircode + extension;
            }
            //Url = Url.Substring(0,(Url.Length-1));

            bool result = FAstData.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXAIMAGEUPLOAD", ssircode, Url, assetcod, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
    }
}