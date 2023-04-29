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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_34_Mgt
{

    public partial class DataSyncSystem : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        ProcessAccess SecAccData = new ProcessAccess("ASITINTERIOR", "Secondary");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //  ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Voucher 360 <sup>0";
                //this.Master.Page.Title = "Voucher 360 <sup>0</sup>";
                CommonButton();
                this.txtfromdate.Text = Fromdate();
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddDays(7).ToString("dd-MMM-yyyy");
               // this.GetScompany();
                this.lbtnOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc2 = "";
                    string comcod = this.GetCompCode();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }

            }
        }

        //private void GetScompany()
        //{
            
        //        DataSet ds1 = SecAccData.GetTransInfo("", "SP_UTILITY_DATA_SYNC", "GETSECONDAYCOMPANY", "","", "", "", "", "", "", "");
                
        //}

        private  string Fromdate()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_LASTSYNCDATE", "", "", "", "", "", "");
            if (ds1 == null)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                return Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
            }
            else
            {
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    return Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
                }
                else
                {
                    string scomcod = "3368";
                    DataSet ds3 = SecAccData.GetTransInfo(scomcod, "SP_UTILITY_DATA_SYNC", "GET_LASTSYNCDATE", "", "", "", "", "", "");
                    if (ds3 != null)
                    {
                       this.TxtAccMiss.Text= Convert.ToString(Convert.ToInt32(ds3.Tables[1].Rows[0]["totalacccode"]) - Convert.ToInt32(ds1.Tables[1].Rows[0]["totalacccode"]));
                      this.TxtResMiss.Text = Convert.ToString(Convert.ToInt32(ds3.Tables[2].Rows[0]["totalrescode"]) - Convert.ToInt32(ds1.Tables[2].Rows[0]["totalrescode"]));

                    }

                    return Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).AddDays(1).ToString("dd-MMM-yyyy");
                }
            }

            }
         
            
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lbtnDeleteVoucher_Click);

        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = GetCompCode();           
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["AllVoucher"];
            DataTable dtdet = (DataTable)Session["AllVoucherDet"];
            if (dt.Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found to Sync";

                return;
            }
            DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            DateTime todate = Convert.ToDateTime(this.txttodate.Text);

            DataSet ds1 = SecAccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_PERIODIC_ALL_TRANSECTION", frmdate.ToString("dd-MMM-yyyy"), todate.ToString("dd-MMM-yyyy"), "", "", "", "", "", "");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "In the Date range You have some Voucher";

                    return;
                }
            }
            DataSet ds = new DataSet("ds1");
            ds.Merge(dt);
            ds.Tables[0].TableName = "tblvoucher";


            DataSet ds2 = new DataSet("ds2");
            ds2.Merge(dtdet);
            ds2.Tables[0].TableName = "tblvoucherdet";
            var xx = ds.GetXml();
            var xx2 = ds2.GetXml();

            bool result = AccData.UpdateXmlTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "UPDATE_VOUCHER", ds, ds2, null, userid, Terminal, Sessionid, Posteddat);

            if (result==true)
            {
                this.txtfromdate.Text = this.Fromdate();
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddDays(7).ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sync Successfully";

            }



        }

        private string GetCompCode()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = "3368"; //this.GetCompCode();
            DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            DateTime todate = Convert.ToDateTime(this.txttodate.Text);

            if (frmdate.Month == todate.Month && frmdate.Year == todate.Year)
            {
                DataSet ds1 = SecAccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_PERIODIC_ALL_TRANSECTION", frmdate.ToString("dd-MMM-yyyy"), todate.ToString("dd-MMM-yyyy"), "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvAccVoucher.DataSource = null;
                    this.gvAccVoucher.DataBind();
                    return;
                }

                Session["AllVoucher"] = ds1.Tables[0];
                Session["AllVoucherDet"] = ds1.Tables[1];

                this.Data_Bind();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Not Eligible with Selected Date Range";
                return;
            }

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string events = hst["events"].ToString();
            //if (Convert.ToBoolean(events) == true)
            //{
            //    string eventtype = "Show Data " + ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc = "Show Data " + ((Label)this.Master.FindControl("lblTitle")).Text;
            //    string eventdesc2 = "Data Show Date Range : " + "From " + frmdate + "To " + todate + "Voucher Type " + voutype;

            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            //}

        }

        public void CommonButton()
        {
        //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "<span class='fa fa-trash'></span> Remove Selected Voucher";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).CssClass = "btn btn-danger btn-sm";







        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["AllVoucher"];


            this.gvAccVoucher.DataSource = dt;
            this.gvAccVoucher.DataBind();
           



        }

      
        protected void lnkPrint_Click(object sender, EventArgs e)
        {


        }
        protected void gvAccVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

      

        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["AllVoucher"];
          

            for (int i = 0; i < this.gvAccVoucher.Rows.Count; i++)
            {
                if (((CheckBox)this.gvAccVoucher.Rows[i].FindControl("chkCol")).Checked)
                {
                   dt.Rows[i].Delete();                 
                   
                   
                    
                }
            }

            Session["AllVoucher"] = dt;
            this.Data_Bind();

        }

        protected void gvAccVoucher_Sorting(object sender, GridViewSortEventArgs e)
        {

            //    Session["tblunposted"]
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["AllVoucher"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            // Session["SortedView"] = sortedView;
            gvAccVoucher.DataSource = sortedView;
            gvAccVoucher.DataBind();
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void lblvounum_Click(object sender, EventArgs e)
        {
            
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = "3368";
         
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string vounum = ((LinkButton)this.gvAccVoucher.Rows[index].FindControl("lblvounum")).Text.ToString();

            string type1 = vounum.Substring(0, 2);
                string voutype =(type1=="BD")?"PV": (type1 == "CD")?"PV":"";
                string ProceName = (voutype == "PV") ? "SP_ENTRY_ACCOUNTS_PAYMENT" : "SP_REPORT_ACCOUNTS_VOUCHER";
                string ProceNameNar = (voutype == "PV") ? "SP_ENTRY_ACCOUNTS_PAYMENT" : "SP_ENTRY_ACCOUNTS_VOUCHER";
                DataSet _NewDataSet = SecAccData.GetTransInfo(comcod, ProceName, "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
              
                    DataTable dt = this.HiddenSameData(_NewDataSet.Tables[0]);
                    //this.dgv1.DataSource = dt;
                    Session["tblvoucher"] = dt;

            dgv1.DataSource = dt;
            dgv1.DataBind();

            //if (dt.Rows.Count == 0)
            //            return;

            //        this.Data_Bind();
            //        //this.dgv1.DataBind();

            //        Session["UserLog"] = _EditDataSet.Tables[1];
            //        //-------------** Edit **---------------------------//
            //        DataTable dtedit = _EditDataSet.Tables[1];

            //        if (vounum.Substring(0, 2).ToString() != "JV")
            //        {
            //            this.txtScrchConCode.Text = "";
            //            this.txtScrchConCode.Visible = true;
            //            this.ibtnFindConCode.Visible = true;
            //            this.LoadAcccombo();
            //            this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();
            //        }
            //        this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //        this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
            //        this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
            //        this.ddlpayeelist.SelectedValue = dtedit.Rows[0]["payeetype"].ToString().Trim(); //payee Type
            //        this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
            //        this.txtPayto.Text = dtedit.Rows[0]["payto"].ToString();
            //        this.txtBankNam.Text = dtedit.Rows[0]["banknam"].ToString();
            //        this.txtchequedate.Text = (Convert.ToDateTime(dtedit.Rows[0]["chequedat"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(dtedit.Rows[0]["chequedat"]).ToString("dd-MMM-yyyy");

            //        this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
            //        //   this.txtEntryDate.Enabled = false;
            //        //-------------------------------------------------//
            //        this.pnlNarration.Visible = true;
            //        this.lblcurVounum.Text = "Edit Voucher No.";
            //        string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
            //        this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            //        this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
            //        this.txtCurrntlast6.Enabled = false;
            //        this.CalculatrGridTotal();


            //    }
            //    else
            //    {

            //        this.txtEntryDate.Enabled = true;
            //        this.txtCurrntlast6.Enabled = true;


            //        // Previous Nerration
            //        // double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            //        string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            //        //string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" :
            //        //    (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
            //        //string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
            //        string VNo3 = this.ddlvoucher.SelectedValue.ToString().Trim();
            //        string date = this.txtEntryDate.Text;

            //        DataSet ds4 = accData.GetTransInfo(comcod, ProceNameNar, "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");
            //        if (ds4.Tables[0].Rows.Count > 0)
            //            this.txtNarration.Text = this.getLastNarration(ds4);
            //        else
            //            this.txtNarration.Text = "";
            //        //---------------------

            //        this.GetVouCherNumber();

            //        if (VNo3 == "BD" || VNo3 == "CT")
            //        {
            //            this.ChequeNo();


            //        }

            //        this.txtchequedate.Text = (VNo3 == "BD" || VNo3 == "CT" || VNo3 == "BC") ? this.txtEntryDate.Text : "";

            //    }
               
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShowModal();", true);

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();

                }

            }
            return dt1;

        }

        protected void LbtnImport_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            if(Convert.ToInt32(this.TxtAccMiss.Text)==0 && Convert.ToInt32(this.TxtResMiss.Text) == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Nothing for Import";

                return;

            }
            else
            {
                List<AccountsBook> accbooksec = new List<AccountsBook>();
                List<AccountsBook> accbookcur = new List<AccountsBook>();

                List<RescourceBook> resbooksec = new List<RescourceBook>();
                List<RescourceBook> resbookcur = new List<RescourceBook>();

                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "GET_ALL_CODEBOOK", "", "", "", "", "", "");
                if (ds1 != null)
                {
                    accbookcur = ds1.Tables[0].DataTableToList<AccountsBook>();
                    resbookcur = ds1.Tables[1].DataTableToList<RescourceBook>();

                }

                string secomcod = "3368";
                DataSet ds2 = SecAccData.GetTransInfo(secomcod, "SP_UTILITY_DATA_SYNC", "GET_ALL_CODEBOOK", "", "", "", "", "", "");
                if (ds2 != null)
                {
                    accbooksec = ds2.Tables[0].DataTableToList<AccountsBook>();
                    resbooksec = ds2.Tables[1].DataTableToList<RescourceBook>();

                }


                //List<AccountsBook> accimport = accbooksec.Concat(accbookcur).ToList();
                List<AccountsBook>  newresult = accbooksec.Where(k => !accbookcur.Contains(k, new EntityComparer())).ToList();
                List<RescourceBook> newresult1 = resbooksec.Where(k => !resbookcur.Contains(k, new EntityComparerRescode())).ToList();


                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(newresult));
                ds.Tables.Add(ASITUtility03.ListToDataTable(newresult1));
                ds.Tables[0].TableName = "tblacc";
                ds.Tables[1].TableName = "tblres";
                var xx = ds.GetXml();
             
                bool result = AccData.UpdateXmlTransInfo(comcod, "SP_UTILITY_DATA_SYNC", "UPDATE_CODEBOOK", ds,null, null);
                if (result == true)
                {
                    this.txtfromdate.Text = this.Fromdate();
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Codebook Imported Successfully";

                }
                //  list3 = list1.Except(list2);
            }
        }
    }



    public class RescourceBook
    {
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public double sirval { get; set; }
        public string sirtdes { get; set; }


    }

    public class AccountsBook
    {
        public string actcode { get; set; }
        public string actdesc { get; set; }

    }
    class EntityComparer : IEqualityComparer<AccountsBook>
    {
        public bool Equals(AccountsBook x, AccountsBook y)
        {
            return x.actcode == y.actcode;
        }
        //public bool Equals(RescourceBook x, RescourceBook y)
        //{
        //    return x.sircode == y.sircode;
        //}

        public int GetHashCode(AccountsBook obj)
        {
            return 0;
        }
    }
    class EntityComparerRescode : IEqualityComparer<RescourceBook>
    {      
        public bool Equals(RescourceBook x, RescourceBook y)
        {
            return x.sircode == y.sircode;
        }

        public int GetHashCode(RescourceBook obj)
        {
            return 0;
        }
    }

}