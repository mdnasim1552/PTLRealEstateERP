using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
//using KPIRPT;
namespace RealERPWEB.F_21_MKT
{
    public partial class ToDaysAppointment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "TODAY'S  COMMUNICATON INFORMATION";
                this.txtTo.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetSalesList();
                this.GetClientList();
                this.GetProjectAUnit();
                this.ShowView();

            }
        }


        private void GetSalesList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Sales Person List ---------------//
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchteam = "%" + this.txtSrchSalesTeam.Text;
            string userid = (this.Request.QueryString["UType"] == "Client") ? hst["usrid"].ToString() + "%" : "%";
            DataSet dss = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETMKTTEAM", srchteam, userid, "", "", "", "", "", "", "");
            this.ddlSalesTeam.DataTextField = "teamdesc";
            this.ddlSalesTeam.DataValueField = "teamcode";
            this.ddlSalesTeam.DataSource = dss.Tables[0];
            this.ddlSalesTeam.DataBind();
        }
        private void GetClientList()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            if (this.lnkok.Text == "New")
                return;
            Session.Remove("tblclient");
            string comcod = this.Getcomcod();
            string salesp = this.ddlSalesTeam.SelectedValue.Substring(14).ToString();
            string datefrom = this.txtFrom.Text;
            string dateto = this.txtTo.Text;
            string prTeam = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string txtSerch = "%" + this.txtSrchClient.Text + "%";

            DataSet dset = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETCLIENTCODE", salesp, datefrom, dateto, prTeam, txtSerch, "", "", "", "");
            Session["tblclient"] = dset.Tables[0];
            this.ddlClientList.DataTextField = "prosdesc";
            this.ddlClientList.DataValueField = "proscod";
            this.ddlClientList.DataSource = dset.Tables[0];
            this.ddlClientList.DataBind();
            this.ddlClientList_SelectedIndexChanged(null, null);
        }
        //private void ShowGridHeader() 
        //{

        //    string comcod = this.Getcomcod();
        //    DataSet dsgridheader = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "SHOW_GRIDHEADER", "", "", "", "", "", "", "", "", "");
        //    this.gvPersonalInfo.Columns[3].HeaderText =dsgridheader.Tables[0].Rows[0]["gdesc"].ToString();
        //    this.gvPersonalInfo.Columns[4].HeaderText =dsgridheader.Tables[0].Rows[1]["gdesc"].ToString();
        //    this.gvPersonalInfo.Columns[5].HeaderText =dsgridheader.Tables[0].Rows[8]["gdesc"].ToString();
        //}

        private void GetProjectAUnit()
        {
            ViewState.Remove("tblproaunit");
            string comcod = Getcomcod();
            DataSet dss = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETPROJECTAUNIT", "", "", "", "", "", "", "", "", "");
            ViewState["tblproaunit"] = dss;
            dss.Dispose();
        }
        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "NewClient":
                    this.lblDatefrm.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txtFrom.Visible = false;
                    this.txtTo.Visible = false;
                    break;

                case "PreClient":
                    break;

            }





        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.ddlClientList.Items.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Client!!!";
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
            }
            if (this.lnkok.Text == "Ok")
            {

                ViewState.Remove("tblappmnt");
                string comcod = this.Getcomcod();
                this.ddlSalesTeam.Enabled = false;
                this.ddlClientList.Enabled = false;
                string teamcode = this.ddlSalesTeam.SelectedValue.ToString().Substring(0, 14);
                string clientcod = this.ddlClientList.SelectedValue.ToString();
                string datefrom = Convert.ToDateTime(this.txtFrom.Text).ToString("dd-MMM-yyyy");
                string dateto = Convert.ToDateTime(this.txtTo.Text).ToString("dd-MMM-yyyy");
                DataSet dset = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "CLIENTCOMUCATION", teamcode, clientcod, datefrom, dateto, "", "", "", "", "");

                ViewState["tblappmnt"] = dset.Tables[0];
                IndivClientDataBind();
                // this.lbtnSalesCompleted.Visible = true ;
                this.lnkok.Text = "New";
                this.BindEmployee();
            }
            else
            {
                this.GetClientList();
                this.lnkok.Text = "Ok";
                this.ddlSalesTeam.Enabled = true;
                this.ddlClientList.Enabled = true;
                this.gvAppment.DataSource = null;
                this.gvAppment.DataBind();
                this.txtFrom.Enabled = true;
                this.txtTo.Enabled = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                // this.lbtnSalesCompleted.Visible = false;



            }
        }

        private void BindEmployee()
        {
            //string Comcod = this.Getcomcod();
            //string Proscod = this.ddlClientList.SelectedValue.ToString();
            //string QryStr = "proscod=" + Proscod;
            //string TString = "javascript:window.showModalDialog('MktSalesDescription.aspx?" + QryStr + "', 'Unit Description', 'dialogHeight:600px;dialogWidth:700px;status:no')";
            //this.lbtnSalesCompleted.Attributes.Add("OnClick", TString);


        }

        private void IndivClientDataBind()
        {
            DataTable dt = (DataTable)ViewState["tblappmnt"];
            this.gvAppment.DataSource = dt;
            this.gvAppment.DataBind();


            DataSet ds1 = (DataSet)ViewState["tblproaunit"];

            DropDownList ddlProject, ddlUnit;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Project
                string pactcode = dt.Rows[i]["pactcode"].ToString();
                ddlProject = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlProject"));
                ddlProject.DataTextField = "pactdesc";
                ddlProject.DataValueField = "pactcode";
                ddlProject.DataSource = ds1.Tables[0];
                ddlProject.DataBind();
                ddlProject.SelectedValue = pactcode;


                DataTable dt1 = ds1.Tables[1].Copy();
                DataView dv1 = dt1.DefaultView;
                dv1.RowFilter = ("pactcode='" + pactcode + "'");


                string usircode = dt.Rows[i]["usircode"].ToString();
                ddlUnit = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit"));
                ddlUnit.DataTextField = "udesc";
                ddlUnit.DataValueField = "usircode";
                ddlUnit.DataSource = dv1.ToTable();
                ddlUnit.DataBind();
                ddlUnit.SelectedValue = usircode;

            }









        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblappmnt"];
            for (int i = 0; i < gvAppment.Rows.Count; i++)
            {

                //string cdate = ((TextBox)this.gvAppment.Rows[i].FindControl("txtmtingdate")).Text.Trim();
                //string Pactcode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
                //string Usircode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit")).SelectedValue.ToString();
                double UnitSize = Convert.ToDouble("0" + ((Label)this.gvAppment.Rows[i].FindControl("lgvunitsize")).Text.Trim());
                double Oferedprice = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofprice")).Text.Trim());
                double OferPamt = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofpamt")).Text.Trim());
                double Oferothamt = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofother")).Text.Trim());
                //string destinatin = ((TextBox)this.gvAppment.Rows[i].FindControl("txtdestination")).Text.Trim();
                //string callorvistime = ((TextBox)this.gvAppment.Rows[i].FindControl("txtcallvistime")).Text.Trim();
                //string discussion = ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvVal")).Text.Trim();
                //string nextapnt = ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvna")).Text.Trim();
                //string remarks = ((TextBox)this.gvAppment.Rows[i].FindControl("txtremarks")).Text.Trim();
                double oftuamt = ((UnitSize * Oferedprice) + OferPamt + Oferothamt);
                ((Label)this.gvAppment.Rows[i].FindControl("lgvofftotal")).Text = oftuamt.ToString("#,##0;(#,##0); ");

                //dt.Rows[i]["cdate"] = cdate;
                //dt.Rows[i]["pactcode"] =Pactcode;
                //dt.Rows[i]["usircode"] = Usircode;
                //dt.Rows[i]["rate"] = Oferedprice;
                //dt.Rows[i]["ofpamt"] = OferPamt;
                //dt.Rows[i]["ofothamt"] = Oferothamt;
                //dt.Rows[i]["destintion"] = cdate;
                //dt.Rows[i]["cdate"] = cdate;
                //dt.Rows[i]["cdate"] = cdate;
                //dt.Rows[i]["cdate"] = cdate;
                //dt.Rows[i]["cdate"] = cdate;


            }
        }


        protected void lnkappupdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.Getcomcod();
                string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
                string proscod = this.ddlClientList.SelectedValue.ToString();


                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "DELETECLIENTSCHEDULE", teamcode, proscod, "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + MktData.ErrorObject["Msg"];
                    return;
                }
                for (int i = 0; i < gvAppment.Rows.Count; i++)
                {

                    string cdate = ((TextBox)this.gvAppment.Rows[i].FindControl("txtmtingdate")).Text.Trim();
                    string Pactcode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
                    string Usircode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit")).SelectedValue.ToString();
                    string Oferedprice = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofprice")).Text.Trim()).ToString();
                    string OferPamt = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofpamt")).Text.Trim()).ToString();
                    string Oferothamt = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvofother")).Text.Trim()).ToString();
                    string bookamt = Convert.ToDouble("0" + ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvbookamt")).Text.Trim()).ToString();


                    string destinatin = ((TextBox)this.gvAppment.Rows[i].FindControl("txtdestination")).Text.Trim();
                    string callorvistime = ((TextBox)this.gvAppment.Rows[i].FindControl("txtcallvistime")).Text.Trim();
                    string discussion = ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    string nextapnt = ((TextBox)this.gvAppment.Rows[i].FindControl("txtgvna")).Text.Trim();
                    string remarks = ((TextBox)this.gvAppment.Rows[i].FindControl("txtremarks")).Text.Trim();

                    bool m = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INORUPCLIENTSCHEDULE", teamcode, proscod, cdate, Pactcode, Usircode, destinatin,
                       callorvistime, discussion, nextapnt, remarks, Oferedprice, OferPamt, Oferothamt, bookamt, "");
                    if (m == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + MktData.ErrorObject["Msg"];
                        return;
                    }
                }
                 //}
                 ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Update Info";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.Getcomcod();
            //string clientcod = this.ddlClientList.SelectedValue.ToString();
            //DataSet dset1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTCOMUCATION", clientcod, "", "", "", "", "", "", "", "");
            //DataTable dtab1 = dset1.Tables[0];
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = this.ddlClientList.SelectedItem.Text;
            //rptAppMonitor.SetDataSource(dtab1);
            //Session["Report1"] = rptAppMonitor;
            //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ddlSalesTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetClientList();
        }
        protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblclient"];
            string clientcode = this.ddlClientList.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("proscod='" + clientcode + "'");
            if (dr1.Length > 0)
            {
                this.lblphone.Text = " " + dr1[0]["phone"].ToString();
                return;
            }
            this.lblphone.Text = "";
        }



        protected void imgSearchClient_Click(object sender, EventArgs e)
        {
            this.GetClientList();
        }
        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetSalesList();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblappmnt"];
            DataSet ds1 = (DataSet)ViewState["tblproaunit"];
            DropDownList ddlgval;


            for (int i = 0; i < this.gvAppment.Rows.Count; i++)
            {
                string pactcode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlProject")).Text.Trim();
                string usircode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit")).Text.Trim();

                DataTable dt1 = ds1.Tables[1].Copy();
                DataView dv1;
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
                dt1 = dv1.ToTable();
                //((Label)this.gvAppment.Rows[i].FindControl("lgvacprice")).Text =((usircode=="000000000000")?0: Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["rate"])).ToString("#,##0;(#,##0); ");

                ddlgval = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit"));
                ddlgval.DataTextField = "udesc";
                ddlgval.DataValueField = "usircode";
                ddlgval.DataSource = dv1.ToTable();
                ddlgval.DataBind();
                ddlgval.SelectedValue = usircode;



            }
        }
        protected void imgSearchUnit_Click(object sender, EventArgs e)
        {

        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {

        }
        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblappmnt"];
            DataSet ds1 = (DataSet)ViewState["tblproaunit"];

            for (int i = 0; i < this.gvAppment.Rows.Count; i++)
            {
                string pactcode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlProject")).Text.Trim();
                string usircode = ((DropDownList)this.gvAppment.Rows[i].FindControl("ddlUnit")).Text.Trim();
                DataTable dt1 = ds1.Tables[1].Copy();
                DataView dv1;
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
                dt1 = dv1.ToTable();
                ((Label)this.gvAppment.Rows[i].FindControl("lgvunitsize")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["usize"])).ToString("#,##0;(#,##0); ");
                ((Label)this.gvAppment.Rows[i].FindControl("lgvacrate")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["rate"])).ToString("#,##0;(#,##0); ");
                ((Label)this.gvAppment.Rows[i].FindControl("lgvacparking")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["pamt"])).ToString("#,##0;(#,##0); ");
                ((Label)this.gvAppment.Rows[i].FindControl("lgvacother")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["othamt"])).ToString("#,##0;(#,##0); ");
                ((Label)this.gvAppment.Rows[i].FindControl("lgvactotal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["tuamt"])).ToString("#,##0;(#,##0); ");


            }
        }

        protected void lbtnSalesCompleted_Click(object sender, EventArgs e)
        {

        }
        protected void gvAppment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string proscod = this.ddlClientList.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblappmnt"];
            string cdate = ((TextBox)this.gvAppment.Rows[e.RowIndex].FindControl("txtmtingdate")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "DELETEICLIENTDIS", teamcode, proscod, cdate, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvAppment.PageSize) * (this.gvAppment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblappmnt");
            ViewState["tblappmnt"] = dv.ToTable();
            this.IndivClientDataBind();
        }
    }
}
