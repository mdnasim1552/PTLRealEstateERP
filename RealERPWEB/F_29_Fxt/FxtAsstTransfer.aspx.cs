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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_29_Fxt
{
    public partial class FxtAsstTransfer : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        DataTable dttemp = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Transfer";

            if (this.ddlprjlistfrom.Items.Count == 0)
            {
                this.Load_Project_From_Combo();
                this.tableintosession();
                this.Load_Dates_And_Trans_No();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }


        protected void GetPreTrnNm()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mREQNO;
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

            string mTrnsDAT = this.GetStdDate(this.txtCurTransDate.Text.Trim());

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "LASTTRANSFETNO", mTrnsDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
                this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 6);
                this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
                this.ddlPrevISSList.DataTextField = "maxtrnno1";
                this.ddlPrevISSList.DataValueField = "maxtrnno";
                this.ddlPrevISSList.DataSource = ds2.Tables[0];
                this.ddlPrevISSList.DataBind();

            }
        }
        protected void Load_Dates_And_Trans_No()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");//XXXXXXXXXXXXXX
            this.Get_Trnsno();

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void tableintosession()
        {
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("sirunit", Type.GetType("System.String"));
            dttemp.Columns.Add("qty", Type.GetType("System.Double"));
            dttemp.Columns.Add("balqty", Type.GetType("System.Double"));
            Session["sessionforgrid"] = dttemp;

        }



        protected void Load_Project_From_Combo()
        {
            Session.Remove("prlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fproject = "%" + this.txtProjectSearchF.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETASSETINFO", fproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlprjlistfrom.DataTextField = "actdesc";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            Session["prlist"] = ds1.Tables[0];
            this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["prlist"];
            string actcode = this.ddlprjlistfrom.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode not in ('" + actcode + "')";
            this.ddlprjlistto.DataTextField = "actdesc";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = dv1.ToTable();
            this.ddlprjlistto.DataBind();

        }

        protected void Load_Project_Res_Combo()
        {
            Session.Remove("tblsir");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string prjfrom = this.ddlprjlistfrom.SelectedItem.Text;
            string prjcode = this.ddlprjlistfrom.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "RESLIST", prjcode, "%%", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlreslist.DataTextField = "sirdesc";
            this.ddlreslist.DataValueField = "sircode";
            this.ddlreslist.DataSource = ds1.Tables[0];
            this.ddlreslist.DataBind();
            Session["tblsir"] = ds1.Tables[0];

        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.Session_update();
            string rsircode = this.ddlreslist.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rsircode + "'");

            if (projectrow2.Length > 0)
            {
                return;

            }

            DataRow drforgrid = dt.NewRow();
            drforgrid["rsircode"] = rsircode;
            drforgrid["rsirdesc"] = this.ddlreslist.SelectedItem.Text.Trim();
            drforgrid["sirunit"] = (((DataTable)Session["tblsir"]).Select("sircode='" + rsircode + "'"))[0]["sirunit"].ToString();
            drforgrid["qty"] = 0;
            drforgrid["balqty"] = Convert.ToDouble((((DataTable)Session["tblsir"]).Select("sircode='" + rsircode + "'"))[0]["balqty"]);
            dt.Rows.Add(drforgrid);
            Session["sessionforgrid"] = dt;
            this.grvacc_DataBind();
            CommonButton();
        }

        private void Session_update()
        {
            DataTable dt1 = (DataTable)Session["sessionforgrid"];
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                dt1.Rows[i]["qty"] = qty;
            }
            Session["sessionforgrid"] = dt1;

        }


        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
        }





        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_update();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                ProcessAccess pa = new ProcessAccess();
                DataTable dt = (DataTable)Session["sessionforgrid"];
                if (ddlPrevISSList.Items.Count == 0)
                {
                    this.GetPreTrnNm();
                }
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                string fromprj = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
                string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string trsircode = dt.Rows[i]["rsircode"].ToString().Trim();
                    string tunit = dt.Rows[i]["sirunit"].ToString().Trim();
                    string tqty = dt.Rows[i]["qty"].ToString().Trim();
                    bool result = pa.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTORUPDATEASSTTRASNSINFO", tansno, fromprj, toprj, trsircode,
                         tqty, curdate, "", "", "", "", "", "", "", "", "");
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
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.divresource.Visible = true;
                this.pnlgrd.Visible = true;
                this.ddlprjlistfrom.Enabled = true;
                this.ddlprjlistto.Enabled = false;
                this.ddlPrevISSList.Visible = false;
                this.divPrevTrans.Visible = false;
                this.txtPreTrnsSearch.Visible = false;
                this.lblPreList.Visible = false;
                this.lbtnPrevVOUList.Visible = false;
                if (this.ddlPrevISSList.Items.Count > 0)
                {
                    this.txtCurTransDate.Enabled = false;
                    string trnno = this.ddlPrevISSList.SelectedValue.ToString().Trim();
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();

                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
                    DataTable dt1 = ds.Tables[0];
                    this.ddlprjlistfrom.SelectedValue = ds.Tables[1].Rows[0]["tfpactcode"].ToString();
                    this.ddlprjlistfrom_SelectedIndexChanged(null, null);
                    this.ddlprjlistto.SelectedValue = ds.Tables[1].Rows[0]["ttpactcode"].ToString();
                    Session["sessionforgrid"] = dt1;
                    this.grvacc_DataBind();
                    this.Load_Cur_Trans_NO();

                }
                else
                {
                    this.Get_Trnsno();

                }
                this.Load_Project_Res_Combo();


            }
            else
            {
                ///Session.Remove("sessionforgrid");
                Session["sessionforgrid"] = null;
                this.tableintosession();

                this.ddlprjlistfrom.Enabled = true;
                this.ddlprjlistto.Enabled = true;
                this.ddlPrevISSList.Visible = true;
                this.lbtnPrevVOUList.Visible = true;
                this.divPrevTrans.Visible = true;
                this.txtPreTrnsSearch.Visible = false;
                this.lblPreList.Visible = true;
                this.grvacc.DataSource = null;
                this.txtCurTransDate.Enabled = true;
                this.grvacc.DataBind();
                this.Get_Trnsno();
                this.divresource.Visible = false;
                this.pnlgrd.Visible = false;
                lbtnOk.Text = "Ok";
                this.ddlPrevISSList.Items.Clear();
            }

        }

        private void Get_Trnsno()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);


        }

        protected void Load_Cur_Trans_NO()
        {
            this.lblCurTransNo1.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(0, 5);
            this.txtCurTransNo2.Text = this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(6, 5);
            this.txtCurTransDate.Text = Convert.ToDateTime(this.ddlPrevISSList.SelectedItem.ToString().Trim().Substring(12, this.ddlPrevISSList.SelectedItem.ToString().Trim().Length - 12)).ToString("dd.MM.yyyy");


        }

        //protected void Last_trn_no()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string date = this.GetStdDate(this.txtCurTransDate.Text);
        //    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
        //    DataTable dt1 = ds.Tables[0];
        //    string trnno = (Convert.ToInt32(dt1.Rows[0]["maxtrnno1"].ToString().Trim().Substring(6, 5)) + 1).ToString();
        //    this.lblLastTransNo.Text = dt1.Rows[0]["maxtrnno1"].ToString().Trim();

        //}

        protected void Load_Prev_Trans_List()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GetPrevTrnsList", curdate, "", "", "", "", "", "", "", "");
            Session["prevtranslist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlPrevISSList.DataTextField = "trnno1";
            this.ddlPrevISSList.DataValueField = "trnno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk_Click(null, null);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["sessionforgrid"];

            string prjfrm = "Transfer From: " + this.ddlprjlistfrom.SelectedItem.Text.Trim().Substring(13);
            string prjto = "Transfer To: " + this.ddlprjlistto.SelectedItem.Text.Trim().Substring(13);
            string date = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtCurTransDate.Text)).ToString("MMMM dd, yyyy");
            string transferNo = "Transfer No: " + this.lblCurTransNo1.Text.Trim() + "-" + this.txtCurTransNo2.Text.Trim();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            var lst = dt1.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetTransfer>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptFixedAssetTransfer", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Asset Transfer Permission"));
            Rpt1.SetParameters(new ReportParameter("prjfrm", prjfrm));
            Rpt1.SetParameters(new ReportParameter("prjto", prjto));
            Rpt1.SetParameters(new ReportParameter("transferNo", transferNo));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void lbtnPrevVOUList_Click(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();
        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.Load_Project_From_Combo();
        }
        protected void ImgbtnFindProject0_Click(object sender, EventArgs e)
        {

        }
    }
}
