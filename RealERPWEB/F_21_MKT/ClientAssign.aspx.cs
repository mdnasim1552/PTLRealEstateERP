using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;

using RealEntity;
using AjaxControlToolkit;
namespace RealERPWEB.F_21_MKT
{
    public partial class ClientAssign : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        public static string Url = "";
        public static string name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string qtype = this.Request.QueryString["Type"].ToString();
                if (qtype == "MktClAss")
                {
                    pnlAssign.Visible = true;
                    LoadddlEmp();
                    this.GetProspectClient();

                    //((Label)this.Master.FindControl("lblTitle")).Text = "Client Assign";
                }
                else
                {

                    pnlAssign.Visible = false;
                    GetProfession();
                    this.GetProspectClient();
                    this.Createtable();

                    //((Label)this.Master.FindControl("lblTitle")).Text = "Accept Prospective Client List";
                }


            }
        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadddlEmp()
        {
            string comcod = this.GetCompCode();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EempList> lst = objuserman.GetEmpList(comcod);

            this.ddlEmplist.DataTextField = "emplist";
            this.ddlEmplist.DataValueField = "empid";
            this.ddlEmplist.DataSource = lst;
            this.ddlEmplist.DataBind();
        }


        private void GetProspectClient()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string qtype = this.Request.QueryString["Type"].ToString() == "MktClAss" ? "" : usrid;
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = objuserman.GetAllEmp(comcod, qtype);

            Session["tblClientInfo"] = lst;
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst =
                (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
            this.gvAdDetails.DataSource = lst;
            this.gvAdDetails.DataBind();


            string qtype = this.Request.QueryString["Type"].ToString();

            if (qtype == "MktAcceptClient")
            {
                //((LinkButton)this.gvAdDetails.FooterRow.FindControl("lUpdate")).Visible = false;


            }



        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.GetProspectClient();

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.gvAdDetails.DataSource = null;
            this.gvAdDetails.DataBind();

        }

        private void SaveValue()
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
            for (int i = 0; i < this.gvAdDetails.Rows.Count; i++)
            {
                bool chkid = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkempid")).Checked;
                if (chkid == true)
                {
                    string rmks = ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtclrmks")).Text.Trim();
                    bool chks = ((CheckBox)this.gvAdDetails.Rows[i].FindControl("chkempid")).Checked ? true : false;

                    lst[i].rmks = rmks;
                    lst[i].chk = chks;
                }

            }
            Session["tblClientInfo"] = lst;


        }

        protected void lUpdate_OnClick(object sender, EventArgs e)
        {
            this.SaveValue();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)Session["tblClientInfo"];
            var lst1 = lst.FindAll(c => c.chk == true);
            string comcod = this.GetCompCode();

            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ASITUtility03.ListToDataTable(lst1);
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            string empid = this.ddlEmplist.SelectedValue;
            bool result = _processAccess.UpdateXmlTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "INSERTCLIENTID", ds1, null, null, empid, "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";


            }

        }

        protected void gvAdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string qtype = this.Request.QueryString["Type"].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkempid") as CheckBox;
                if (qtype == "MktAcceptClient")
                {

                    if (chk.Checked == false)
                    {
                        ((LinkButton)e.Row.FindControl("lnkAddclient")).Visible = true;
                        ((CheckBox)e.Row.FindControl("chkempid")).Visible = false;
                    }
                    else
                    {
                        ((LinkButton)e.Row.FindControl("lnkAddclient")).Visible = false;
                        ((CheckBox)e.Row.FindControl("chkempid")).Visible = true;
                    }
                }

            }
        }

        protected void lnkAddclient_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string TxtMobile = ((Label)this.gvAdDetails.Rows[index].FindControl("txtclmob")).Text.ToString().Trim();

            this.GetClientData(TxtMobile);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        private void GetClientData(string TxtMobile)
        {
            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_INTERFACE", "CLIENTINFOKPI", proscod, TxtMobile, "", "", "", "", "", "", "");


            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();


            ViewState["tblsameDate"] = ds1.Tables[0];
            ds1.Dispose();



        }
        private string GetLastclId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();

            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "GETCLIENTSINFO", "",
                                  "%%", userid, "", "", "", "", "");
            string lastid = ds1.Tables[1].Rows[0]["sircode"].ToString();
            return (lastid);


        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();




            //string Usircode = this.lblCode.Text.Trim();
            this.saveValueClient();

            //DataTable dt = (DataTable)ViewState["tblPersonaldata"];

            string paddres = string.Empty;
            string mobile = string.Empty;
            string email = string.Empty;
            string active = "1";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                TextBox Mobile = this.gvPersonalInfo.Rows[i].FindControl("txtgvValMob") as TextBox;


                if (Gcode == "810100103001")
                {
                    name = Gvalue;
                }

                if (Gcode == "810100103006")
                {
                    paddres = Gvalue;

                }
                if (Gcode == "810100103007")
                {
                    mobile = Mobile.Text.ToString();
                    //mobile = Gvalue;

                }
                if (Gcode == "810100103015")
                {
                    email = Gvalue;

                }
            }

            DataTable dtprsonl = (DataTable)ViewState["tblPersonaldata"];

            DataSet ds1 = new DataSet("ds1");
            DataView dv1 = new DataView(dtprsonl);
            ds1.Tables.Add(dv1.ToTable());
            ds1.Tables[0].TableName = "tbl1";


            bool result = _processAccess.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "INSORUPCLIENTINFO", proscod, name, paddres, mobile, email, active, "", Url, "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }

            else
            {
                bool CResult = _processAccess.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_XML_INFO_KPI", "SHOWCLIENTINFO", ds1, null, null, "", "", "", "", "", "");

                if (CResult == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    string page = "MktEmpKpiEntry.aspx?Type=Entry&clientid=" + proscod;
                    ScriptManager.RegisterStartupScript(this, GetType(), "target", "ClientDisPage('" + page + "');", true);
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('MktEmpKpiEntry.aspx?Type=Entry&clientid=" + proscod
                    //+ "', target='_blank');</script>";



                    //string url = "~/F_21_Mkt/MktEmpKpiEntry.aspx?Type=Entry&clientid=" + proscod;
                    //Response.Redirect(url);
                }
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper>)
            ViewState["tblProfess"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                TextBox txtgv = (TextBox)e.Row.FindControl("txtgvVal");
                string txtgvname = ((TextBox)e.Row.FindControl("txtgvVal")).Text.ToString();

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01001")
                {

                    txtgv.ReadOnly = true;

                }

                if ((code == "810100103005") || (code == "810100103006"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
                }
                if (code == "810100103007")
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvValMob")).Visible = true;

                }

                if (code == "810100103013")
                {
                    //if (txtgvname != "")
                    //{
                    //    ((DropDownList)e.Row.FindControl("ddlprofession")).SelectedValue = txtgvname;

                    //}

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).Visible = true;

                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataTextField = "gdesc";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataValueField = "gcod";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataSource = lst;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataBind();

                }
                if ((code == "810100103009") || (code == "810100103010"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvCal")).Visible = true;

                }


            }

        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblimages"];
            //if (name=="")
            //    return;

            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);

            if (AsyncFileUpload1.HasFile)
            {

                //string holder = this.ddlimgperson.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/ClientImg/") + comcod + "_" + random + extension);

                Url = "~/Upload/ClientImg/" + comcod + "_" + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
            }
            else
            {
                Url = "~/Upload/ClientImg/NoImage.jpg";
            }

        }


        private void Createtable()
        {
            DataTable tbltemp = new DataTable();
            //tbltemp.Columns.Add("comitemid", Type.GetType("System.String"));

            tbltemp.Columns.Add("comcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("proscod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatat", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatad", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatan", Type.GetType("System.String"));



            ViewState["tbltemp"] = tbltemp;

        }
        private void saveValueClient()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string proscod = this.GetLastclId();

            DataTable dt = (DataTable)ViewState["tbltemp"];
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DropDownList ddlprofession = this.gvPersonalInfo.Rows[i].FindControl("ddlprofession") as DropDownList;
                TextBox Gvalue = this.gvPersonalInfo.Rows[i].FindControl("txtgvVal") as TextBox;
                Label Gcode = this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode") as Label;
                Label gtype = this.gvPersonalInfo.Rows[i].FindControl("lgvgval") as Label;
                TextBox Gvalued = this.gvPersonalInfo.Rows[i].FindControl("txtgvCal") as TextBox;

                string txtData = "";
                string lblspcode = Gcode.Text;
                string dataType = gtype.Text;


                if (dataType == "N")
                {
                    txtData = ASTUtility.StrPosOrNagative(Gvalue.Text.Trim()).ToString();
                }
                if (dataType == "D")
                {
                    txtData = (Gvalued.Text.Length == 0) ? "01-Jan-1900" : Convert.ToDateTime(Gvalued.Text).ToString("dd-MMM-yyyy");
                }
                else
                {
                    if (ASTUtility.Right(Gcode.Text, 3) == "013")
                    {
                        txtData = ddlprofession.SelectedValue.ToString();
                    }
                    else
                    {
                        txtData = Gvalue.Text.Trim().ToString();
                    }

                }




                DataRow dr1 = dt.NewRow();


                dr1["comcod"] = comcod;
                dr1["proscod"] = proscod;
                dr1["gcod"] = lblspcode;
                dr1["gdatat"] = (dataType == "T" ? txtData : "");
                dr1["gdatan"] = (dataType == "N" ? txtData : "0.00");
                dr1["gdatad"] = (dataType == "D" ? txtData : "01-01-1900");






                dt.Rows.Add(dr1);
            }

            ViewState["tblPersonaldata"] = dt;



        }
        private void GetProfession()
        {
            string comcod = this.GetCompCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetProAndLocatio(comcod);
            var lst1 = lst.FindAll(l => l.gcod.Substring(0, 4) == "8601");

            ViewState["tblProfess"] = lst1;


        }
    }
}