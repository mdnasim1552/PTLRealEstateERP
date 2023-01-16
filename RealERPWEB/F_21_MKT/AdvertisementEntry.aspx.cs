using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using RealEntity;
using RealEntity.C_21_Mkt;
namespace RealERPWEB.F_21_MKT
{
    public partial class AdvertisementEntry : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Advertisement Entry";
                this.GetPaperAdDes();
                string genno = this.Request.QueryString["genno"];

                if (genno.Length > 0)
                {
                    this.GetPreOffNo();
                    this.lbtnOk_OnClick(null, null);
                }

            }



        }


        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.lbtnPrevAdNo.Visible = false;
                this.ddlPrevAdNo.Visible = false;
                this.adDetails.Visible = true;

                this.ShowAddInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.adDetails.Visible = false;

            this.ddlPrevAdNo.Items.Clear();
            this.lbtnPrevAdNo.Visible = true;
            this.ddlPrevAdNo.Visible = true;
            this.txtCurDate.Enabled = true;
            this.gvAdDetails.DataSource = null;
            this.gvAdDetails.DataBind();
        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetPaperAdDes()
        {
            string comcod = this.GetCompCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetPaperAndDesCode(comcod);
            var lst1 = lst.FindAll(l => l.gcod.Substring(0, 4) == "0901");
            var lst2 = lst.FindAll(l => l.gcod.Substring(0, 4) == "0999");

            this.ddlPaperName.DataTextField = "gdesc";
            this.ddlPaperName.DataValueField = "gcod";
            this.ddlPaperName.DataSource = lst1;
            this.ddlPaperName.DataBind();

            this.ddlPaperDesc.DataTextField = "gdesc";
            this.ddlPaperDesc.DataValueField = "gcod";
            this.ddlPaperDesc.DataSource = lst2;
            this.ddlPaperDesc.DataBind();

        }

        private void GetLastAddNo()
        {


            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string madNo = "NEWAD";
            if (this.ddlPrevAdNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;

                madNo = this.ddlPrevAdNo.SelectedValue.ToString();


            }

            if (madNo == "NEWAD")
            {
                List<RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo> lst = objuserman.GetLastAdNo(comcod, CurDate1);
                if (lst.Count == 0)
                    return;
                if (lst.Count > 0)
                {
                    this.lblCurNo1.Text = lst[0].maxadno1.Substring(0, 6); //.Substring (0, 6);
                    this.lblCurNo2.Text = lst[0].maxadno1.Substring(6);
                    this.ddlPrevAdNo.DataTextField = "maxadno1";
                    this.ddlPrevAdNo.DataValueField = "maxadno";
                    this.ddlPrevAdNo.DataSource = lst;
                    this.ddlPrevAdNo.DataBind();
                }

            }




        }

        private void ShowAddInfo()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string madNo = "NEWAD";

            if (this.ddlPrevAdNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;

                madNo = this.ddlPrevAdNo.SelectedValue.ToString();


            }
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETADINFO", "", madNo, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            /////Here was
            ViewState["tblln"] = ds1.Tables[0].DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>();
            ViewState["UserLog"] = ds1.Tables[0].DataTableToList<RealEntity.C_21_Mkt.EClassAdvertisement.EClassAddUser>();



            if (madNo == "NEWAD")
            {
                List<RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo> lst = objuserman.GetLastAdNo(comcod, CurDate1);
                if (lst == null)
                    return;

                this.lblCurNo1.Text = lst[0].maxadno1.Substring(0, 6); //.Substring (0, 6);
                this.lblCurNo2.Text = lst[0].maxadno1.Substring(6);

                return;

            }


            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["addate"]).ToString("dd-MMM-yyyy");
            this.Data_DataBind();

        }


        private void Data_DataBind()
        {

            this.gvAdDetails.DataSource = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>)ViewState["tblln"];
            this.gvAdDetails.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>)ViewState["tblln"];
            if (lst.Count == 0)
                return;
            double toamt = lst.Sum(l => l.amount);
            ((Label)this.gvAdDetails.FooterRow.FindControl("lgvFAmount")).Text = toamt.ToString("#,##0.00;(#,##0.00); ");

        }

        protected void lbtnAdd_OnClick(object sender, EventArgs e)
        {


            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>)ViewState["tblln"];
            string papcod = this.ddlPaperName.SelectedValue.ToString();
            string descode = this.ddlPaperDesc.SelectedValue.ToString();

            string papdesc = this.ddlPaperName.SelectedItem.Text;
            string addesc = this.ddlPaperDesc.SelectedItem.Text;
            double amt = Convert.ToDouble("0" + this.txtAmount.Text);
            var lst1 = lst.FindAll(l => l.papcod == papcod && l.descode == descode);
            if (lst1.Count == 0)
            {
                RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes objadd = new EClassAdvertisement.EPaperadDes(papcod, descode, papdesc, addesc, amt);
                lst.Add(objadd);

            }

            ViewState["tblln"] = lst;
            this.Data_DataBind();



        }

        private void SaveValue()
        {

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>)ViewState["tblln"];
            for (int i = 0; i < this.gvAdDetails.Rows.Count; i++)
            {

                double amount = ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAdDetails.Rows[i].FindControl("txtamount")).Text.Trim()));

                lst[i].amount = amount;



            }
            ViewState["tblln"] = lst;


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();

        }
        protected void lUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string adno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string comcod = this.GetCompCode();
            string refno = this.txtAdRef.Text.Trim();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            var dtuser = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassAddUser>)ViewState["UserLog"];

            string tblPostedByid = (dtuser.Count == 0) ? "" : dtuser[0].postedbyid;
            string tblPostedtrmid = (dtuser.Count == 0) ? "" : dtuser[0].postrmid;
            string tblPostedSession = (dtuser.Count == 0) ? "" : dtuser[0].postseson;
            string tblPosteddat = (dtuser.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser[0].posteddat).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string tblEdittrmid = (dtuser.Count == 0) ? "" : dtuser[0].edittrmid;
            string tblEditseson = (dtuser.Count == 0) ? "" : dtuser[0].editseson;


            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "MktAd") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["Type"] == "MktAd") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["Type"] == "MktAd") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["Type"] == "MktAd") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["Type"] == "MktAd") ? "" : userid;

            string Editdat = (this.Request.QueryString["Type"] == "MktAd") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Editseson = (this.Request.QueryString["Type"] == "MktAd") ? Sessionid : (tblEditseson == "") ? Sessionid : tblEditseson;
            String Edittrmid = (this.Request.QueryString["Type"] == "MktAd") ? Terminal : (tblEdittrmid == "") ? Terminal : tblEdittrmid;



            this.SaveValue();
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes>)ViewState["tblln"];

            if (this.ddlPrevAdNo.Items.Count == 0)
                this.GetLastAddNo();






            bool result = _processAccess.UpdateTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "ADVERINUP", "ADVERB",
                             adno, curdate, refno, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Edittrmid, Editseson, "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }





            foreach (RealEntity.C_21_Mkt.EClassAdvertisement.EPaperadDes lst1 in lst)
            {

                string papcod = lst1.papcod;
                string descod = lst1.descode;
                string amt = lst1.amount.ToString();




                result = _processAccess.UpdateTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "ADVERINUP", "ADVERA", adno,
                    papcod, descod, amt, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

        }

        private void GetPreOffNo()
        {


            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.Trim();

            string advno = (this.Request.QueryString["genno"].Length > 0 ? this.Request.QueryString["genno"] : "") + "%";
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETPREVADNO", curdate, advno, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevAdNo.DataTextField = "adno1";
            this.ddlPrevAdNo.DataValueField = "adno";
            this.ddlPrevAdNo.DataSource = ds1.Tables[0];
            this.ddlPrevAdNo.DataBind();
        }

        protected void lbtnPrevAdNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreOffNo();
        }


    }
}
