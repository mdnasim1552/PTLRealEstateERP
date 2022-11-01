using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_01_LPA
{
    public partial class RptBusinessClient : System.Web.UI.Page
    {
        ProcessAccess GetData = new ProcessAccess();
        ProcessAccess _processAccessMsgdb = new ProcessAccess("ASTREALERPMSG");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                //this.GetEmployeeName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Landowner Notification";
                this.GetYEARLAND();
                this.GetSMSMAILTempalte();
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetYEARLAND()
        {
            ViewState.Remove("tblempname");
            string comcod = this.GetComeCode();
            DataSet ds3 = GetData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETLANDDDLYEARINFO", "", "", "", "", "", "", "", "", "");
            this.ddlyearland.DataTextField = "sirdesc";
            this.ddlyearland.DataValueField = "sircode";
            this.ddlyearland.DataSource = ds3.Tables[0];
            this.ddlyearland.DataBind();
            this.GetGridData();
        }
        protected void ddlyearland_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetGridData();
        }

        private void GetGridData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string comcod = this.GetComeCode();
            string yearcod = ASTUtility.Left(ddlyearland.SelectedValue.ToString(), 7) + "%";
            DataSet ds3 = GetData.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE_01", "LINFOSUM", null, null, null, yearcod, Empid, "%", "%", "%", "%", "%",
               "%", "%", "%", "%", "%");
            if(ds3 == null || ds3.Tables[0].Rows.Count == 0)
            {
                return;
               
                
            }
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();


            ViewState["tblsummData"] = ds3.Tables[0];
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            DataView dv1 = ds3.Tables[0].Copy().DefaultView;
            dv1.RowFilter = ("active='True'");

            this.gvSummary.DataSource = dv1.ToTable();
            this.gvSummary.DataBind();
            Session["tblBusinessData"] = dv1.ToTable();


        }
        protected void ddlSmsMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSMSMAILTempalte();
        }
        private void GetSMSMAILTempalte()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = this._processAccessMsgdb.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSMAILTEMPLATE", "0131%", "0139%", "", "", "", "", "", "", "");
             if(ds1 == null || ds1.Tables[0].Rows.Count==0)
            {
                return;
            }
            DataTable dt1 = new DataTable();
            DataView view = new DataView();
            view.Table = ds1.Tables[0];
            string contype = this.ddlSmsMail.SelectedValue.ToString();
             
            if (contype == "01")
            {
                view.RowFilter = "active='True'";
                dt1 = view.ToTable();
            }
            else
            {
                view.RowFilter = "mactive='True'";
                dt1 = view.ToTable();
            }
            ViewState["SMSMAILCONTTENT"] = dt1;
            this.SMS_MAIL_BIND();
        }

        private void SMS_MAIL_BIND()
        {
            DataTable dt = (DataTable)ViewState["SMSMAILCONTTENT"];
            this.ddlSMSMAILTEMP.DataTextField = "gdesc";
            this.ddlSMSMAILTEMP.DataValueField = "gcod";
            this.ddlSMSMAILTEMP.DataSource = dt;
            this.ddlSMSMAILTEMP.DataBind();
            ddlSMSMAILTEMP_SelectedIndexChanged(null, null);
        }
        protected void ddlSMSMAILTEMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["SMSMAILCONTTENT"];

            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            string template = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            string contype = this.ddlSmsMail.SelectedValue.ToString();

            if (contype == "01")
            {

                //view.RowFilter = "gcod=" + template;

                view.RowFilter = "gcod='" + template + "'";
                dt1 = view.ToTable();
                if (dt1.Rows.Count > 0)
                {
                    this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
                }

                //this.lblTemMSg.Text = dt1.Rows[0]["smscont"].ToString();
            }

            else
            {
                view.RowFilter = "gcod=" + template;
                dt1 = view.ToTable();

                this.lblTemMSg.Text = dt1.Rows[0]["mailcont"].ToString();
            }

            ViewState["tblsmsmailtempcont"] = dt;
        }
        protected void lnkSend_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            this.SaveCheckedValue();
            string smsstatus = this.ddlSmsMail.SelectedValue.ToString();
            string ntype = this.ddlSMSMAILTEMP.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblBusinessData"];
            dt.TableName = "Tbl1";
            DataTable dtfinal = new DataTable();
            DataView view = new DataView();

            view.Table = dt;
            view.RowFilter = "smstatus=True";
            dtfinal = view.ToTable();
            //var lst = dtfinal.DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();
            //Session["tblUserInfo"] = lst;
            DataTable dt1 = (DataTable)ViewState["tblsmsmailtempcont"];
            string smscontent = "";
            string mailcontent = "";
            string smsst = "";
            string mailst = "";
            if (smsstatus == "01")
            {
                smscontent = this.lblTemMSg.Text.ToString();
                smsst = "Y";
            }
            else
            {
                mailcontent = this.lblTemMSg.Text.ToString();
                mailst = "Y";
            }



            //string compsms = hst["compsms"].ToString();
            //if (compsms == "True")
            //{
            foreach (DataRow dr in dtfinal.Rows)
            {
                string userid = dr["dealcode"].ToString();

                string pactcode = dr["sircode"].ToString();
                string phone = dr["cphone"].ToString();
                string email = dr["cmail"].ToString();
                string tdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string mailattch = "";
                string Usircode = "";

                bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), pactcode, Usircode, userid, tdate, ntype, smsst, smscontent, mailst,
                    mailcontent, mailattch, phone, email);

            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            //}


            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Contact With Administrator";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}
            this.GetGridData();
        }
        private void SaveCheckedValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt1 = (DataTable)Session["tblBusinessData"];


            for (int i = 0; i < this.gvSummary.Rows.Count; i++)
            {
                //comcod, actcode, rescode, refno, trdate, ntype, smsstatus, smscontent, mailstatus, mailcontent, mailattch, postedbyid, postrmid, posteddat, phone, email
                string actcode = ((Label)this.gvSummary.Rows[i].FindControl("lsircode")).Text.ToString();


                //string ntype = ((Label)this.gvAdDetails.Rows[i].FindControl("lblAdno")).Text.ToString(); 


                string phone = ((Label)this.gvSummary.Rows[i].FindControl("lblPhone")).Text.ToString(); ;
                string email = ((Label)this.gvSummary.Rows[i].FindControl("lblMail")).Text.ToString(); ;
                string checkstatus = ((CheckBox)this.gvSummary.Rows[i].FindControl("chkstatus")).Checked == true ? "True" : "False";
                // lblStatus

                string refno = ((Label)this.gvSummary.Rows[i].FindControl("lblDealid")).Text.ToString();/// need 


                DataRow dr1 = dt1.NewRow();


                dr1["dealcode"] = refno;
                dr1["sircode"] = actcode;

                dr1["smstatus"] = checkstatus;
                dr1["cphone"] = phone;
                dr1["cmail"] = email;

                dt1.Rows.Add(dr1);

                Session["tblBusinessData"] = dt1;
            }
        }
    }
}