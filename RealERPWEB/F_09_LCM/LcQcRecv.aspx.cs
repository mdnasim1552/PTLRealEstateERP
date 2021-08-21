
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_09_LCM
{
    public partial class LcQcRecv : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Purdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {

                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LC Qc Form";
                this.txtreceivedate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLcNumber();
                this.CommonButton();
            }

        }
        private void CommonButton()
        {
            // ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //   ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            //  ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;




        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Rec_Value();
            this.Data_Bind();
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "Entry") ? userid : "";
            string Posttrmid = (this.Request.QueryString["Type"] == "Entry") ? Terminal : "";
            string PostSession = (this.Request.QueryString["Type"] == "Entry") ? Sessionid : "";
            string Posteddat = (this.Request.QueryString["Type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : "01-Jan-1900";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            string rcvdate = this.txtreceivedate.Text.Substring(0, 11).ToString();
            this.Save_Rec_Value();

            if (this.ddlPreGrn.Items.Count == 0)
                LoadGRRNo();
            string grrno = this.txtgrrno.Text.Trim().ToString();
            if (ViewState["TblReceive"] != null)
            {
                var lst = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
                DataTable tbl4 = ASITUtility03.ListToDataTable(lst); //((DataTable)ViewState["TblReceive"]).Copy();           

                DataSet ds1 = new DataSet("ds1");

                ds1.Tables.Add(tbl4);
                //ds1.Tables.Add(dt);
                ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";

                DataSet ds112 = Purdata.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "UPDATE_LC_QC", ds1, null, null, rcvdate, PostedByid, PostSession, Posttrmid, Posteddat, grrno, "", "", "", "", "");
                if (ds112.Tables[0].Rows.Count != 0)
                {
                    this.txtgrrno.Text = ds112.Tables[0].Rows[0]["memonum"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
                    //((LinkButton)this.dgvReceive.FooterRow.FindControl("lnkFinalUpdateR")).Enabled = false;

                }


            }

        }
        private void LoadGRRNo()
        {
            string comcod = this.GetCompCode();
            string storid = this.Request.QueryString["centrid"].ToString();
            string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
            DataSet ds = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GETGRRNO", grrdat, storid, "", "", "", "", "", "", "");
            this.ddlPreGrn.DataTextField = "grrno";
            this.ddlPreGrn.DataValueField = "grrno";
            this.ddlPreGrn.DataSource = ds.Tables[0];
            this.ddlPreGrn.DataBind();
            this.txtgrrno.Text = ds.Tables[0].Rows[0]["grrno"].ToString();
        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }
        public void GetLcNumber()
        {
            string comcod = this.GetCompCode();
            string SlcNO = "%%";
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "ACINF", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlLcCode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlLcCode_SelectedIndexChanged(null, null);
                this.LbtnOk_Click(null, null);
            }

        }


        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.ddlLcCode.Enabled = false;
            this.ddlrcvno.Enabled = false;
            this.Get_info();

        }
        protected void imgbtnPreGrn_Click(object sender, EventArgs e)
        {

            this.PreGrn();
        }
        private void PreGrn()
        {
            string comcod = this.GetCompCode();
            string store = "";
            if (this.Request.QueryString["centrid"].Length > 0)
            {
                store = this.Request.QueryString["centrid"].ToString();
            }
            string filter2 = "%" + this.txtgrrno.Text.Trim() + "%";
            DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_LC_INFO", "GETPREGRN", store, filter2, "", "", "", "", "", "", ""); //table Desc 2

            if (ds5.Tables[0].Rows.Count == 0)
                return;
            this.ddlPreGrn.DataTextField = "grrno1";
            this.ddlPreGrn.DataValueField = "grrno";
            this.ddlPreGrn.DataSource = ds5.Tables[0];
            this.ddlPreGrn.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreGrn.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
        }

        private void Get_info()
        {


            ViewState.Remove("TblReceive");
            string comcod = this.GetCompCode();
            string storid = this.Request.QueryString["centrid"].ToString();
            string grrno = "NEWGRN";
            if (this.ddlPreGrn.Items.Count > 0)
            {
                this.txtreceivedate.Enabled = false;
                grrno = this.ddlPreGrn.SelectedValue.ToString();

            }
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_LCQC_INFO", storid, grrno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["TblReceive"] = ds1.Tables[0].DataTableToList<RealEntity.C_09_LCM.EClassLCMGT>();


            if (grrno == "NEWGRN")
            {

                string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
                DataSet ds = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GETGRRNO", grrdat, storid, "", "", "", "", "", "", "");
                //this.ddlPreGrn.DataTextField = "grrno";
                //this.ddlPreGrn.DataValueField = "grrno";
                //this.ddlPreGrn.DataSource = ds.Tables[0];
                //this.ddlPreGrn.DataBind();
                this.txtgrrno.Text = ds.Tables[0].Rows[0]["grrno"].ToString();
                this.GetRecv_info();
                return;
            }



            this.ddlrcvno.SelectedValue = ds1.Tables[0].Rows[0]["rcvno"].ToString();
            this.txtreceivedate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["rcvdate"]).ToString("dd-MMM-yyyy");
            this.txtgrrno.Text = ds1.Tables[0].Rows[0]["grrno"].ToString();

            this.Data_Bind();
        }
        private void GetRecv_info()
        {

            string comcod = this.GetCompCode();
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string rcvno = this.ddlrcvno.SelectedValue.ToString();

            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_LC_RECEIVED", lcno2, rcvno, "", "", "", "", "", "", "");
            var lst = ds6.Tables[0].DataTableToList<RealEntity.C_09_LCM.EClassLCMGT>();
            ViewState["TblReceive"] = lst;
            Data_Bind();

        }

        private void Data_Bind()
        {
            try
            {
                var rcvdata = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];

                this.dgvReceive.DataSource = rcvdata;
                this.dgvReceive.DataBind();

                this.Rcv_Footcal();


            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }
        private void Rcv_Footcal()
        {
            try
            {

                var lst = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
                if (lst.Count == 0)
                    return;
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = (lst.Select(p => p.rcvqty).Sum() == 0.00) ? "0" : lst.Select(p => p.rcvqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFreuptlast")).Text = (lst.Select(p => p.preqcqty).Sum() == 0.00) ? "0" : lst.Select(p => p.preqcqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrmainord")).Text = (lst.Select(p => p.remqty).Sum() == 0.00) ? "0" : lst.Select(p => p.remqty).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrcvQty")).Text = (lst.Select(p => p.qcqty).Sum() == 0.00) ? "0" : lst.Select(p => p.qcqty).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }

        private void Save_Rec_Value()
        {
            var lst = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
            int RowIndex = 0;

            //  double tocost = Convert.ToDouble("0" + ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text);
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvrcvQty")).Text.Trim());
                double remqty = Convert.ToDouble("0" + ((Label)this.dgvReceive.Rows[i].FindControl("lblgvrmainord")).Text.Trim());
                string expdate = Convert.ToDateTime(((TextBox)this.dgvReceive.Rows[i].FindControl("txtexpeirdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                RowIndex = this.dgvReceive.PageIndex * this.dgvReceive.PageSize + i;
                if (Qty > remqty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check Your Qc Quantity');", true);
                    return;
                }
                if (Qty == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check Your Qc Quantity');", true);
                    return;
                }
                lst[RowIndex].qcqty = Qty;
                lst[RowIndex].expdate = Convert.ToDateTime(expdate);
                lst[RowIndex].remarks = ((TextBox)this.dgvReceive.Rows[i].FindControl("txtremarks")).Text.Trim();

            }

            ViewState["TblReceive"] = lst;
        }
        protected void dgvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox gvrcvQty = (TextBox)e.Row.FindControl("txtgvrcvQty");

                double balqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "qcqty"));


                if (balqty == 0.00)
                {

                    //gvrcvQty.Enabled = false;
                    gvrcvQty.ToolTip = "Balance Qty Zero";
                }
                else
                {

                }
            }
        }


        protected void ddlLcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_LC_RCVNO", actcode, "", "", "", "", "", ""); // table Desc 3
            this.ddlrcvno.DataTextField = "rcvno1";
            this.ddlrcvno.DataValueField = "rcvno";
            this.ddlrcvno.DataSource = ds1.Tables[0];
            this.ddlrcvno.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "Entry")
            {
                if (this.Request.QueryString["genno"].Length > 0)
                {
                    this.ddlrcvno.SelectedValue = this.Request.QueryString["genno"].ToString();
                }
            }
            else
            {
                this.PreGrn();
            }

        }
    }
}