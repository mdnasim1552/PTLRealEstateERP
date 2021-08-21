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
namespace RealERPWEB.F_07_Ten

{
    public partial class TasPrjInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Information";

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.GetCatagory();
            }

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetCatagory()
        {
            string comcod = this.GetComCode();
            Session.Remove("tblcdesc");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETCATAGORY", "", "", "", "", "", "", "",
                "", "");
            if (ds1 == null)
            {
                //this.ddlcatagory.DataSource = null;
                //this.ddlcatagory.DataBind();
                return;
            }
            Session["tblcdesc"] = ds1.Tables[0];
        }

        private void GetProjectName()
        {
            Session.Remove("tblpro");
            string comcod = this.GetComCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_INFO", "GETTASPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "factdesc";
            this.ddlPrjName.DataValueField = "factcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            Session["tblpro"] = ds1.Tables[0];


        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
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
                this.imgpanel.Visible = true;
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.imgpanel.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Visible = true;

            this.lblProjectdesc.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            this.lblProjectdesc.Visible = false;
            this.gvPrjInfo.DataSource = null;
            this.gvPrjInfo.DataBind();
        }

        private void LoadGrid()
        {


            string comcod = this.GetComCode();
            string fpactcode = this.ddlPrjName.SelectedValue.ToString();
            string pactcode = (((DataTable)Session["tblpro"]).Select("factcode='" + fpactcode + "'"))[0]["actcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", pactcode, fpactcode, "", "", "", "", "", "", "");
            this.gvPrjInfo.DataSource = ds1.Tables[0];
            this.gvPrjInfo.DataBind();
            ViewState["projectEntry"] = ds1.Tables[0];
            ViewState["tblimages"] = ds1.Tables[1];
            ListViewEmpAll.DataSource = ds1.Tables[1];
            ListViewEmpAll.DataBind();
            this.GridTextDDLVisible();
        }

        private void GridTextDDLVisible()
        {

            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["projectEntry"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01003": //Start Date                

                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                    case "01004": //Start Date                   
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                    case "02041": //Location                
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        DropDownList ddlcataloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));

                        DataSet dsloc = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLOCATION", "", "", "", "", "", "", "", "", "");
                        ddlcataloc.DataTextField = "prgdesc";
                        ddlcataloc.DataValueField = "prgcod";
                        ddlcataloc.DataSource = dsloc.Tables[0];
                        ddlcataloc.DataBind();
                        ddlcataloc.SelectedValue = val;
                        break;

                    case "02045": //Category                  
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        DropDownList ddlcatag = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));

                        DataSet dscatg = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        ddlcatag.DataTextField = "prgdesc";
                        ddlcatag.DataValueField = "prgcod";
                        ddlcatag.DataSource = dscatg.Tables[0];
                        ddlcatag.DataBind();
                        ddlcatag.SelectedValue = val;
                        break;

                    case "02050": //Construcation                  
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        DropDownList contype = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));
                        contype.DataTextField = "description";
                        contype.DataValueField = "code";
                        contype.DataSource = (DataTable)Session["tblcdesc"];
                        contype.DataBind();
                        contype.SelectedValue = val;
                        break;

                    default:
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                }
            }
            //string comcod = this.GetComCode();
            //DataTable dt = (DataTable)ViewState["projectEntry"];

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string Gcode = dt.Rows[i]["gcod"].ToString();
            //    string val = dt.Rows[i]["gdesc1"].ToString();
            //    switch (Gcode)
            //    {
            //        case "01003": //Start Date  
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
            //            break;

            //        case "01004": //Start Date                   
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
            //            break;

            //        case "02041": //Location                
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            DropDownList ddlcataloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));

            //            DataSet dsloc = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLOCATION", "", "", "", "", "", "", "", "", "");
            //            ddlcataloc.DataTextField = "prgdesc";
            //            ddlcataloc.DataValueField = "prgcod";
            //            ddlcataloc.DataSource = dsloc.Tables[0];
            //            ddlcataloc.DataBind();
            //            ddlcataloc.SelectedValue = val;
            //            break;

            //        case "02045": //Category                  
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
            //            DropDownList ddlcatag = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));



            //            DataSet dscatg = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
            //            ddlcatag.DataTextField = "prgdesc";
            //            ddlcatag.DataValueField = "prgcod";
            //            ddlcatag.DataSource = dscatg.Tables[0];
            //            ddlcatag.DataBind();
            //            ddlcatag.SelectedValue = val;
            //            break;
            //        case "02050": //Category                  
            //             ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
            //            DropDownList contype = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));
            //            contype.DataTextField = "description";
            //            contype.DataValueField = "code";
            //            contype.DataSource = (DataTable)Session["tblcdesc"] ;
            //            contype.DataBind();
            //            contype.SelectedValue = val;
            //            break;
            //            break;
            //        default:
            //            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
            //            break;
            //    }
            //}
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            try
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
                string fpactcode = this.ddlPrjName.SelectedValue.ToString();


                // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

                //string fpactcode = (((DataTable)Session["tblpro"]).Select ("actcode='" + pactcode + "'"))[0]["factcode"].ToString ();
                string pactcode = (((DataTable)Session["tblpro"]).Select("factcode='" + fpactcode + "'"))[0]["actcode"].ToString();
                //string fpactcode = (((DataTable)Session["tblpro"]).Select ("factcode='" + pactcode + "'"))[0]["actcode"].ToString ();

                for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                    //string Gvalue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    string Gunit = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtResunit")).Text.Trim();


                    string Gvalue1 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    DropDownList ddlloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")) as DropDownList;

                    string Gvalue = "";

                    if (Gcode == "02041" || Gcode == "02045" || Gcode == "02050")
                    {
                        Gvalue = ddlloc.SelectedValue.ToString();
                    }
                    else
                    {
                        Gvalue = Gvalue1;
                    }

                    if (Gcode == "01003" || Gcode == "01004")
                    {
                        Gvalue = (((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit, fpactcode, "", "", "", "", "", "", "", "", "");
                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                // ((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:green; border:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Project Information";
                    string eventdesc = "Update Project Information";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                //((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:red; border:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

            //try
            //{

            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //    if (!Convert.ToBoolean(dr1[0]["entry"]))
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //        return;
            //    }
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string fpactcode = this.ddlPrjName.SelectedValue.ToString();


            //    // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

            //    string pactcode = (((DataTable)Session["tblpro"]).Select("factcode='" + fpactcode + "'"))[0]["actcode"].ToString();

            //    for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            //    {
            //        string Gcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string gtype = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
            //       // string Gvalue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        string Gunit = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtResunit")).Text.Trim();

            //        string Gvalue1 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        DropDownList ddlloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")) as DropDownList;

            //        string Gvalue = "";

            //        if (Gcode == "02041" || Gcode == "02045")
            //        {
            //            Gvalue = ddlloc.SelectedValue.ToString();
            //        }
            //        else
            //        {
            //            Gvalue = Gvalue1;
            //        }


            //        if (Gcode == "01003" || Gcode == "01004")
            //        {

            //            Gvalue = (((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
            //        }


            //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
            //        MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit, fpactcode, "", "", "", "", "", "", "", "", "");

            //    }


            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //    // ((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:green; border:none;";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "Project Information";
            //        string eventdesc = "Update Project Information";
            //        string eventdesc2 = "";
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //}
            //catch (Exception ex)
            //{

            //    ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
            //    //((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:red; border:none;";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //}


        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string prjcode = "";
            if (AsyncFileUpload1.HasFile)
            {
                prjcode = this.ddlPrjName.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/PROJECT/") + prjcode + random + extension);

                Url = "~/Upload/PROJECT/" + prjcode + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, prjcode, Url);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "UPDATECUSTIMAGES", ds1, null, null, prjcode, "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                this.LoadGrid();
            }


        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string actcode = ((Label)this.ListViewEmpAll.Items[j].FindControl("actcode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";

                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "UPDATECUSTIMAGES", ds1, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "");


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

        protected void lbtnEnglish_Click(object sender, EventArgs e)
        {
            string name = this.txtSrcPro.Text.Trim();
            //string bname = ASITUtility02.EngtoBandigit(name);
            string bname1 = ASITUtility02.ToBangla(name);
        }
    }
}