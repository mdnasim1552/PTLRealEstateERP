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
using AjaxControlToolkit;
using System.IO;
using System.Security.Policy;
using Microsoft.Reporting.WinForms;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{
    public partial class CustomerDetail : System.Web.UI.Page
    {
        UserManSales objUser = new UserManSales();
        ProcessAccess MktData = new ProcessAccess();
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Datails Information";
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Details Information";
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetClientName();
                }

                this.GetClientID();
                this.LoadGrid();
                this.ComponentVisibility();

            }
        }

        private void ComponentVisibility()
        {
            string comcod = this.GetComdCode();
            switch (comcod)
            {
                case "3101":
                case "3368":
                    divAttachedfiles.Visible = false;
                    break;
                   
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetClientName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComdCode();
            //string wcomcod = this.GetWComCode();

            string userid = hst["usrid"].ToString();
            string txtProsCode = "%" + this.txtSrcPro.Text.Trim() + "%";
            string Calltype = (this.Request.QueryString["Type"] == "Client") ? "GETCLIENTINFO" : "GETALLCLIENTINFO";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", Calltype, txtProsCode, userid, "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "prosdesc";
            this.ddlPrjName.DataValueField = "proscod";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetClientName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlPrjName.SelectedItem.Text;
                this.ddlPrjName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Visible = true;

            this.lblProjectdesc.Text = "";

            this.lblProjectdesc.Visible = false;
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
        }

        private void LoadGrid()
        {
            string comcod = this.GetComdCode();
            string UsirCode = this.Request.QueryString["genno"].ToString().Trim();
            string PactCode = this.Request.QueryString["prjcode"].ToString().Trim();
            string pactdesc = this.Request.QueryString["pactdesc"].ToString().Trim();
            ViewState.Remove("tblimages");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");

            ViewState["tblimages"] = ds1.Tables[9];


            DataTable dt = ds1.Tables[0];
            string gcod = "01020";
            DataRow[] dr1 = dt.Select("gcod='" + gcod + "'");
            if (dr1.Length > 0)
            {

                if (dr1[0]["gdesc1"].ToString().Length == 0)
                {
                    dr1[0]["gdesc1"] = this.lblcustomerid.Text.Trim();

                }
            }


            ViewState["tblcustinf"] = dt;
            ViewState["tblFileNo"] = ds1.Tables[10];
            this.gvPersonalInfo.DataSource = dt;
            this.gvPersonalInfo.DataBind();
            this.GridTextDDLVisible();
            this.ddlimgperson_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetClientID()
        {
            string comcod = this.GetComdCode();
            // string UsirCode = this.Request.QueryString["genno"].ToString().Trim();
            string PactCode = this.Request.QueryString["prjcode"].ToString().Trim();
            var lst = objUser.GetCustomerID(comcod, PactCode);
            string customerid = lst[0].custcode.ToString();
            this.lblcustomerid.Text = customerid;
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCLIENTID", PactCode, UsirCode, "", "", "", "", "", "", "");

        }


        private void GridTextDDLVisible()
        {

            DataTable dt = ((DataTable)ViewState["tblcustinf"]).Copy();
            DataTable dtFile = (DataTable)ViewState["tblFileNo"];
            DropDownList ddlgvFile;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01009": //BirthDay
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        break;
                    case "01010": //MarriageDay
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        //((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = "";
                        break;

                    case "01021": //File No
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = true;
                        ddlgvFile = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo"));
                        ddlgvFile.DataTextField = "fileno";
                        ddlgvFile.DataValueField = "id";
                        ddlgvFile.DataSource = dtFile;
                        ddlgvFile.DataBind();
                        ddlgvFile.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = true;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).Visible = false;
                        break;

                }
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblcustinf"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string pactdesc = this.Request.QueryString["pactdesc"].ToString();

            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.Customerinf>();

            DataTable dti = (DataTable)ViewState["tblimages"];
            var lsti = new List<RealEntity.C_22_Sal.EClassSales_02.CustomerImagePath>();
            foreach (DataRow dr1 in dti.Rows)

            {
                string images = (dr1["imgpath"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["imgpath"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/Upload/CUSTOMER/noimage.jpg")).AbsoluteUri;
                lsti.Add(new RealEntity.C_22_Sal.EClassSales_02.CustomerImagePath(images));
            }


            string images1 = dti.Rows.Count == 0 ? "" : new Uri(Server.MapPath(dti.Rows[0]["imgpath"].ToString())).AbsoluteUri;
            string images2 = dti.Rows.Count < 2 ? "" : new Uri(Server.MapPath(dti.Rows[1]["imgpath"].ToString())).AbsoluteUri;

            //obj1.images = (dr1["images"].ToString().Length > 0) ? new Uri(Server.MapPath(dr1["images"].ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RDLCAccountSetup.GetLocalReport("R_22_Sal.RptCustomerinf", lst, lsti, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtimg1", images1));
            Rpt1.SetParameters(new ReportParameter("txtimg2", images2));
            Rpt1.SetParameters(new ReportParameter("projname", "Project Name : " + pactdesc));
            Rpt1.SetParameters(new ReportParameter("custinf", "Customer Information"));
            Rpt1.SetParameters(new ReportParameter("footer", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.Request.QueryString["prjcode"].ToString().Trim();
            string Usircode = this.Request.QueryString["genno"].ToString().Trim();
            string msg = "";


            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();





                string Gvalue = "";

                if (Gcode == "01009")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "01010")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                }
                else if (Gcode == "01021")
                {
                    Gvalue = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlFileNo")).SelectedValue;
                }
                else
                {
                    Gvalue = Gvalue1;
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", PactCode, Usircode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    msg = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            msg = "Update Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales With Payment Schedule";
                string eventdesc = "Update Personal Info";
                string eventdesc2 = "Project Name: " + PactCode + " - " + Usircode;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "01001" || code == "01020")
                {
                    txtgvname.ReadOnly = true;
                }

            }

        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComdCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string prjcode = "";
            string custcode = "";
            if (AsyncFileUpload1.HasFile)
            {
                prjcode = this.Request.QueryString["prjcode"].ToString();
                custcode = this.Request.QueryString["genno"].ToString();
                string holder = this.ddlimgperson.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/CUSTOMER/") + prjcode + custcode + random + extension);

                Url = "~/Upload/CUSTOMER/" + prjcode + custcode + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, prjcode, custcode, Url, holder);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";


            bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATECUSTIMAGES", ds1, null, null, prjcode, custcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                this.LoadGrid();
            }


        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComdCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string prjcode = ((Label)this.ListViewEmpAll.Items[j].FindControl("pactcode")).Text.ToString();
                string custcode = ((Label)this.ListViewEmpAll.Items[j].FindControl("usricode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";

                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATECUSTIMAGES", ds1, null, null, prjcode, custcode, "", "", "", "", "", "", "", "", "", "", "", "");


                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname);
                        this.lblMesg.Text = " Files Removed ";
                        this.LoadGrid();
                    }


                }




            }

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


                }

            }

        }
        protected void ddlimgperson_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblimages"];
            string holder = this.ddlimgperson.SelectedItem.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "holder='" + holder + "'";
            dt = dv.ToTable();
            if (dt == null)
                return;
            ListViewEmpAll.DataSource = dt;
            ListViewEmpAll.DataBind();

        }
    }
}



