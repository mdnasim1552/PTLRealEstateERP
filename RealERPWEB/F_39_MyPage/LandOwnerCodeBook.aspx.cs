using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using System.IO;
//using  RealERPRPT;
namespace RealERPWEB.F_39_MyPage
{
    public partial class LandOwnerCodeBook : System.Web.UI.Page
    {
        public static string imgpath = "";
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Resource Code";
                this.ShowInformation();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.ConfirmMessage.Visible = false;
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;


            try
            {


                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (!Convert.ToBoolean(dr1[0]["entry"]))
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;
                //}
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string proscod1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
                string proscod2 = ((HyperLink)grvacc.Rows[e.RowIndex].FindControl("hlnkgrcode")).Text.Trim().Replace("-", "");
                // string active = (((CheckBox)this.grvacc.Rows[e.RowIndex].FindControl("chkactive")).Checked) ? "True" : "False";
                string proscod = "";

                bool c = proscod1.Contains(" ");
                proscod = proscod2 + proscod1;
                if (proscod.Length != 12)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Code Lenght Must be 12 degit";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string Address = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvaddress")).Text.Trim();
                string Phone = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvphone")).Text.Trim();
                string Email = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvemail")).Text.Trim();


                //Guid guid = Guid.NewGuid();
                //string random = ASTUtility.Left(guid.ToString(), 4);

                //FileUpload uplFile = (FileUpload)grvacc.Rows[e.RowIndex].FindControl("uplFile");
                //uplFile.Enabled = true;

                //if (uplFile.PostedFile != null && uplFile.PostedFile.ContentLength > 0)
                //{

                //        string dirUrl = "~/Upload/ClientImg";
                //        string dirPath = Server.MapPath(dirUrl);

                //        if (!Directory.Exists(dirPath))
                //        {
                //            Directory.CreateDirectory(dirPath);
                //        }

                //        string fileExtension = Path.GetExtension(uplFile.PostedFile.FileName);
                //        string updatedFilename = random + fileExtension;


                //        string fileName = Path.GetFileName(Desc + "_" + updatedFilename);
                //        string fileUrl = dirUrl + "/" + Path.GetFileName(Desc + "_" + updatedFilename);

                //        //string fileName = Path.GetFileName(actcode1 + "_" + uplFile.PostedFile.FileName);

                //        //string fileUrl = dirUrl + "/" + Path.GetFileName(actcode1 + "_" + uplFile.PostedFile.FileName);
                //        string filePath = Server.MapPath(fileUrl);
                //        uplFile.PostedFile.SaveAs(filePath);

                //        imgpath = dirUrl + "/" + fileName;
                //}


                //else
                //{
                //    imgpath = "~/Upload/ClientImg/NoImage.jpg";
                //}





                DataTable tbl1 = (DataTable)Session["storedata"];
                int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
                tbl1.Rows[Index]["sircode"] = proscod;
                tbl1.Rows[Index]["sirdesc"] = Desc;
                tbl1.Rows[Index]["caddress"] = Address;
                tbl1.Rows[Index]["phone"] = Phone;
                tbl1.Rows[Index]["email"] = Email;
                //tbl1.Rows[Index]["active"] = active;
                //tbl1.Rows[Index]["imgpath"] = imgpath;



                if (Desc.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Name  Should Not Be Empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                // string Snameaddpandempid = Phone + Email;



                //DataSet ds2 = da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENT", Phone, Email, "", "", "", "", "", "", "");
                //if (ds2.Tables[0].Rows.Count == 0)
                //    ;


                //else
                //{

                //    DataView dv1 = ds2.Tables[0].DefaultView;
                //    dv1.RowFilter = ("sircode <>'" + proscod + "'");
                //    DataTable dt = dv1.ToTable();
                //    if (dt.Rows.Count == 0)
                //        ;
                //    else

                //    {
                //       ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Name" + "<br />" + "Sales Team: " + dt.Rows[0]["empname"].ToString();
                //       ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //        //this.ddlPrevReqList.Items.Clear();
                //        return;
                //    }
                //}




                bool result = this.da.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "INSORUPLNOWINFO", proscod, Desc, Address, Phone, Email, "", "", "", "", "", "", "", "", "", "");
                this.grvacc.EditIndex = -1;
                this.ShowInformation();

                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lnkok_Click(null, null);

            //string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //ReportDocument rptAccCode = new  RealERPRPT.R_17_Acc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Sub-CodeBook";
            //    string eventdesc = "Print Sub-CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";    
        }



        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            string wrkcom = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            Session.Remove("storedata");
            string srchoptionmain = "%" + this.txtsrch.Text.Trim() + "%";



            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "GETLANDOWINFO", wrkcom,
                                   srchoptionmain, userid, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }


        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgrcode");
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

                if (Code == "")
                    return;

                hlnkgvdesc.Style.Add("color", "blue");
                string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();

                // hlnkgvdesc.NavigateUrl = "~/F_39_MyPage/LinkClientDetail.aspx?clientcode=" + Code + "&clientdesc=" + sirdesc;
                hlnkgvdesc.NavigateUrl = "~/F_01_LPA/PriLandProposal.aspx?Type=Report&prjcode=" + Code;






            }
        }
        protected void ibtnSrcurClick(object sender, ImageClickEventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //int rowindex = (int)ViewState["gindex"];
            ////string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            //DropDownList ddl2 = (DropDownList)this.gvCodeBook.Rows[rowindex].FindControl("ddlProName");
            //string SearchProject = "%" + ((TextBox)gvCodeBook.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            //DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            //ddl2.DataTextField = "actdesc1";
            //ddl2.DataValueField = "actcode";
            //ddl2.DataSource = ds1;
            //ddl2.DataBind();
            //ddl2.SelectedValue = actcode;
        }

    }
}
