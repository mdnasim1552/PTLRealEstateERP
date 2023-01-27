using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;

namespace RealERPWEB.F_21_MKT
{

    public partial class ClientTransfer : System.Web.UI.Page
    {
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //  this.txtcurdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurTransNo1.Enabled = false;


                // this.LoadddlPaper();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Transfer List";
                this.GetFromTeam();
                this.Get_Trnsno();
                this.tableintosession();
            }
        }
        public string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void Get_Trnsno()
        {

            string comcod = this.GetCompCode();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetFromTeam()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETTEAMINFO", "", "", "", "", "", "", "", "", "");

            //For From Team 
            this.ddlFrmTeam.DataTextField = "empname";
            this.ddlFrmTeam.DataValueField = "empid";
            this.ddlFrmTeam.DataSource = ds1.Tables[0];
            this.ddlFrmTeam.DataBind();

            this.ddlToTeam.DataTextField = "empname";
            this.ddlToTeam.DataValueField = "empid";
            this.ddlToTeam.DataSource = ds1.Tables[0];
            this.ddlToTeam.DataBind();
            ViewState["tblteaminfo"] = ds1.Tables[0];

            ddlFrmTeam_SelectedIndexChanged(null, null);
        }
        protected void ddlFrmTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlFrmTeam.SelectedValue.ToString();
            this.GetClientName(empid);
        }
        private void GetClientName(string empid)
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = this._processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETCLIENTINFO", empid, "", "", "", "", "", "", "", "");

            this.ddlClientNam.DataTextField = "clientname";
            this.ddlClientNam.DataValueField = "clientid";
            this.ddlClientNam.DataSource = ds1.Tables[0];
            this.ddlClientNam.DataBind();


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.dvclintpanel.Visible = true;
                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    string comcod = this.GetCompCode();
                    DataSet ds = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    Session["sessionforgrid"] = ds.Tables[0];
                    //this.txtfmaters.Text = ds.Tables[0].Rows[0]["infor"].ToString();
                    //this.txtspnote.Text = ds.Tables[0].Rows[0]["spnote"].ToString();
                    this.Data_Bind();
                    this.Load_Cur_Trans_NO();
                }
                else
                {
                    this.Get_Trnsno();
                }
            }
            else
            {
                this.dvclintpanel.Visible = false;
                lbtnOk.Text = "Ok";

                this.GvClientTrans.DataSource = null;
                this.GvClientTrans.DataBind();
            }
        }
        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            string curdate = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");

            if (curdate.Substring(2, 1).ToString().Trim() == "-")
            {
                this.txtCurTransDate.Text = "0" + curdate.Trim();
            }
            else
                this.txtCurTransDate.Text = curdate;

        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            string fteamid = this.ddlFrmTeam.SelectedValue.ToString();
            string tteamid = this.ddlToTeam.SelectedValue.ToString();
            string clientid = this.ddlClientNam.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] clientidrow = dt.Select("clientid = '" + clientid + "'");
            if (clientidrow.Length > 0)
            {
                return;

            }
            DataRow drforgrid = dt.NewRow();
            drforgrid["clientid"] = clientid;
            drforgrid["clientname"] = this.ddlClientNam.SelectedItem.Text.ToString();

            drforgrid["fteam"] = this.ddlFrmTeam.SelectedValue.ToString();
            drforgrid["fteamname"] = this.ddlFrmTeam.SelectedItem.Text.ToString();

            drforgrid["tteam"] = this.ddlToTeam.SelectedValue.ToString();
            drforgrid["ttemname"] = this.ddlToTeam.SelectedItem.Text.ToString();

            dt.Rows.Add(drforgrid);
            Session["sessionforgrid"] = dt;
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            this.GvClientTrans.DataSource = dt;
            this.GvClientTrans.DataBind();

        }
        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();

            dttemp.Columns.Add("fteamname", Type.GetType("System.String"));
            dttemp.Columns.Add("ttemname", Type.GetType("System.String"));

            dttemp.Columns.Add("fteam", Type.GetType("System.String"));
            dttemp.Columns.Add("tteam", Type.GetType("System.String"));

            dttemp.Columns.Add("clientid", Type.GetType("System.String"));
            dttemp.Columns.Add("clientname", Type.GetType("System.String"));
            Session["sessionforgrid"] = dttemp;
        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                //this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["sessionforgrid"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPrvNm();
                }
                //string curdate = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string clientid = dt.Rows[i]["clientid"].ToString();
                    string fteam = dt.Rows[i]["fteam"].ToString();
                    string toteam = dt.Rows[i]["tteam"].ToString();

                    bool result = _processAccess.UpdateTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "INSERTUPDATECLIENTTRANSFER", tansno, clientid, fteam, toteam,
                         curdate);
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        protected void GetPrvNm()
        {

            string comcod = GetCompCode();
            //string mREQNO = "NEWISS";
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "LASTTRANSFETNO", mREQDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxtrnno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();
            }

        }
        protected void lbtnPrevTransList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void Load_Prev_Trans_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
    }
}