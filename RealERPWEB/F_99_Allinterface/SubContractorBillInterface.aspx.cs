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
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_99_Allinterface
{

    public partial class SubContractorBillInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"])) ;

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SUB CONTRACTOR INTERFACE";//
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();


                txtdate_TextChanged(null, null);
                //  this.RadioButtonList1_SelectedIndexChanged(null, null);
                // this.Countqty();
                this.PannelVisible();
                this.CheckHyperLink();

            }

        }

        private void PannelVisible()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                case "3337":
                    this.pnlAll.Visible = true;
                    this.Panelsuvastu.Visible = true;
                    break;
                case "1103":
                    this.pnltan.Visible = true;
                    this.Panelrpt.Visible = true;
                    break;

                //case "3368":
                //    this.pnltan.Visible = true;
                //    this.Panelrpt.Visible = true;
                //    break;



                case "3370": // cpdl
                case "1205":
                case "3351":
                case "3352":
                    this.pnlAll.Visible = true;
                    this.Panelrpt.Visible = true;
                    //this.txtrefno.ReadOnly = false;
                    break;

                default:
                    this.pnlAll.Visible = true;
                    this.Panelrpt.Visible = true;
                    //this.txtrefno.ReadOnly = true;
                    break;
            }

        }


        private void CheckHyperLink()
        {
            string comcod = this.GetCompCode();
            if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "8306" || comcod == "3370" || comcod == "3101")
            {
                hlnkworkorder.NavigateUrl = "~/F_09_PImp/PurConWrkOrderEntry?Type=Entry&genno=" + "SubConOrder";
            }
            else
            {
                hlnkworkorder.NavigateUrl = "~/F_09_PImp/PurConWrkOrderEntry02?Type=Entry";
            }

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            // txtdate_TextChanged(null, null);


        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            //this.pnlInterf.Visible = true;
            //this.pnlPurchase.Visible = false;
            //this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            //this.pnlInterf.Visible = false;
            //this.pnlPurchase.Visible = true;
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? comcod;
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            // this.Countqty();

            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        //protected void lnkOk_Click(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //}



        private string GettxtBillFinal()
        {
            string billfinal = "";
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "1103":
                    billfinal = "Checked";
                    break;

                default:
                    billfinal = "Bill Finalization";
                    break;
            }
            return billfinal;




        }

        private string GettxtBillApproved()
        {

            string billapprove = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1103":
                    billapprove = "Approved";
                    break;

                case "3368":
                    billapprove = "Final Approval";
                    break;

                default:
                    billapprove = "Bill Confirmed";
                    break;
            }
            return billapprove;




        }

        private string Gettxtfrecon()
        {
            string frecon = "";
            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3368":
                    frecon = "Checked";

                    break;

                default:
                    frecon = "1st Recom.";
                    break;
            }
            return frecon;
        }

        private void SaleRequRpt()
        {
            string comcod = this.GetCompCode();
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mtrrf = "%" + this.txtrefno.Text.Trim().ToString() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "SUBCONTRACTORINTERFACE", Date, mtrrf, "", "", "", "", "", "", "");
            string billfinal = this.GettxtBillFinal();
            string billapprove = this.GettxtBillApproved();
            string frecon = this.Gettxtfrecon();



            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["impcout"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Monthly Plan</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["execount"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Execution</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["allreq"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Req. Status</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["billreq"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Bill CS</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading light-gray counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["billcs"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content light-gray'><div class='circle-tile-description text-faded'>Bill CS APP</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["workorder"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>Work Order</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["redyforbill"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'>Order App.</div></div></div>";

            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["subbillcount"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Sub-Con.Bill</div></div></div>";

            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["billApproval"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Bill Approval</div></div></div>";

            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading  orange counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["billcount"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>" + billfinal + "</div></div></div>";

            this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray  counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["frecom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray  '><div class='circle-tile-description text-faded'>" + frecon + "</div></div></div>";
            this.RadioButtonList1.Items[11].Text = "<div class='circle-tile'><a><div class='circle-tile-heading   dark-blue  counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["secrecom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue '><div class='circle-tile-description text-faded'>2nd Recom.</div></div></div>";
            this.RadioButtonList1.Items[12].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red  counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["threcom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red '><div class='circle-tile-description text-faded'>Forward</div></div></div>";

            this.RadioButtonList1.Items[13].Text = "<div class='circle-tile'><a><div class='circle-tile-heading  purple   counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["billconfirm"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content  purple   '><div class='circle-tile-description text-faded'>" + billapprove + "</div></div></div>";
            this.RadioButtonList1.Items[14].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[8].Rows[0]["paycount"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content  orange '><div class='circle-tile-description text-faded'>Bill Update</div></div></div>";


            //this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["impcout"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Monthly Imp.Plan" + "</span>";
            //this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["execount"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Work Execution" + "</span>";
            //this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["subbillcount"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Sub-Con.Bill" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["billcount"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Bill Finalization" + "</span>";
            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["billconfirm"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Bill Confirmed" + "</span></a>";
            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[6].Rows[0]["paycount"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Bill Update" + "</span></a>";



            DataTable dt = new DataTable();
            DataTable dtb = new DataTable();
            DataView dv = new DataView();

            dt = ((DataTable)ds1.Tables[0]).Copy();
            this.Data_Bind("grvImple", dt);

            //Update Collection
            dt = ((DataTable)ds1.Tables[1]).Copy();
            this.Data_Bind("gvexecution", dt);

            /// all bill requisition

            dt = ((DataTable)ds1.Tables[9]).Copy();
            dv = dt.DefaultView;
            this.Data_Bind("gvAllReq", dv.ToTable());



            /// labour bill requisition
            dt = ((DataTable)ds1.Tables[6]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("msrno =''");
            this.Data_Bind("gvlabbillreq", dv.ToTable());

            /// labour bill requisition CS
            dt = ((DataTable)ds1.Tables[6]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("msrno <>''");
            this.Data_Bind("gvbillcs", dv.ToTable());


            /// labour bill Work Order
            dt = ((DataTable)ds1.Tables[7]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("orderno =''");
            this.Data_Bind("gvWorkOrder", dv.ToTable());

            /// Ready For Bill
            dt = ((DataTable)ds1.Tables[7]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("orderno <>'' and lisueno=''");
            this.Data_Bind("gvReadyForBill", dv.ToTable());


            dtb = ((DataTable)ds1.Tables[2]).Copy();
            dv = dtb.DefaultView;
            //  dv.RowFilter = ("chkid =''");
            this.Data_Bind("gvsubbill", dv.ToTable());






            //dtb = ((DataTable)ds1.Tables[3]).Copy();
            //dv = dtb.DefaultView;
            //dv.RowFilter = ("chkid =''");
            //this.Data_Bind("gvchaver", dv.ToTable());





            dtb = ((DataTable)ds1.Tables[3]).Copy();
            dv = dtb.DefaultView;
            dv.RowFilter = ("frecid =''");
            this.Data_Bind("gvfrec", dv.ToTable());

            dv = dtb.DefaultView;
            dv.RowFilter = (" frecid <>'' and secrecid=''");
            this.Data_Bind("gvsrec", dv.ToTable());


            dv = dtb.DefaultView;
            dv.RowFilter = (" frecid <>'' and secrecid<>'' and threcid=''");
            this.Data_Bind("gvthrec", dv.ToTable());


            //dv = dtb.DefaultView;
            //dv.RowFilter = (" frecid <>'' and secrecid<>'' and threcid<>'' and fiappid=''");
            //this.Data_Bind("gvfiapp", dv.ToTable());





            dv = dtb.DefaultView;
            dv.RowFilter = (" frecid <>'' and secrecid<>'' and threcid<>'' and aprvbyid=''");
            this.Data_Bind("gvfinalapp", dv.ToTable());






            dt = ((DataTable)ds1.Tables[4]).Copy();
            this.Data_Bind("gvConUpdat", dt);


            dt = ((DataTable)ds1.Tables[5]).Copy();
            this.Data_Bind("gvfinal", dt);

            //Purchase gvfinal

            // bill approval
            dtb = ((DataTable)ds1.Tables[10]).Copy();
            dv = dtb.DefaultView;
            this.Data_Bind("gvbillapp", dv.ToTable());






        }

        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;

            string pactcode = dt.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                    dt.Rows[j]["actdesc"] = "";
                }

                else
                    pactcode = dt.Rows[j]["pactcode"].ToString();
            }

            return dt;
        }







        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();

            switch (value)
            {
                case "0":  // M. Plan
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = true;
                    this.pnlAllReq.Visible = false;

                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.pnlSubbill.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";



                    break;

                case "1": // Work Execution
                    this.PanelBillReq.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = true;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;

                case "2": // All Req
                    this.PanelBillReq.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlAllReq.Visible = true;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;

                case "3": // Bill CS
                    this.PanelBillReq.Visible = true;
                    this.pnlAllReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";


                    break;

                case "4": // Bill CS App
                    this.PanelBillReq.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.PanelBillCs.Visible = true;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";

                    break;

                case "5": // Work Order
                    this.PanelWorkOrder.Visible = true;
                    this.pnlAllReq.Visible = false;
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";

                    break;
                case "6": // Read For BIll
                    this.PanelWorkOrder.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelReadyForBil.Visible = true;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";

                    break;

                case "7": // Subcontractor Bill
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = true;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    break;



                case "8": // Bill Finalization
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = true;

                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";

                    break; 

                case "9": // Bill Finalization
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = true;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";

                    break;

                //case "4": // Checked
                //    this.PnlImp.Visible = false;
                //    this.PnlExe.Visible = false;
                //    this.pnlgvupdate.Visible = false;
                //    this.pnlSubbill.Visible = false;
                //    this.Pnlbillfapp.Visible = false;
                //    this.Pnlbillf.Visible = false;
                //    this.pnlchaverify.Visible = true;
                //    this.pnlfrec.Visible = false;
                //    this.pnlsrec.Visible = false;
                //    this.pnlthrec.Visible = false;
                //    this.pnlApproved.Visible = false;
                //    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";


                //    break;


                case "10": // First Recommendation
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlfrec.Visible = true;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[10].Attributes["class"] = "lblactive blink_me";


                    break;

                case "11": // Second Recommendation
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = true;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;
                    this.RadioButtonList1.Items[11].Attributes["class"] = "lblactive blink_me";


                    break;
                case "12": // Third Recommendation
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = true;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;
                    this.RadioButtonList1.Items[12].Attributes["class"] = "lblactive blink_me";


                    break;


                //case "7": // Final Approval 
                //    this.PnlImp.Visible = false;
                //    this.PnlExe.Visible = false;
                //    this.pnlgvupdate.Visible = false;
                //    this.pnlSubbill.Visible = false;
                //    this.Pnlbillfapp.Visible = false;
                //    this.Pnlbillf.Visible = false;
                //    this.pnlchaverify.Visible = false;
                //    this.pnlfrec.Visible = false;
                //    this.pnlsrec.Visible = false;
                //    this.pnlthrec.Visible = false;
                //    this.pnlApproved.Visible = true;
                //    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";

                //    break;


                case "13": // Bill Confirmed
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.pnlAllReq.Visible = false;

                    this.PnlExe.Visible = false;
                    this.pnlgvupdate.Visible = false;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = true;
                    this.Pnlbillf.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;

                    this.RadioButtonList1.Items[13].Attributes["class"] = "lblactive blink_me";




                    break;



                case "14":// Bill Update
                    this.PanelBillReq.Visible = false;
                    this.PanelBillCs.Visible = false;
                    this.PnlImp.Visible = false;
                    this.PanelReadyForBil.Visible = false;
                    this.PnlExe.Visible = false;
                    this.pnlAllReq.Visible = false;
                    this.pnlgvupdate.Visible = true;
                    this.pnlSubbill.Visible = false;
                    this.Pnlbillfapp.Visible = false;
                    this.Pnlbillf.Visible = false;

                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.PanelWorkOrder.Visible = false;
                    this.pnlbillapp.Visible = false;
                    this.RadioButtonList1.Items[14].Attributes["class"] = "lblactive blink_me";

                    break;
            }
           
        }


        private string companytype()
        {
            string comcod = this.GetCompCode();

            string coltype = "";
            switch (comcod)
            {

                //case "3101":
                case "3330":

                    coltype = "TRANSACTION_STATEMENT2";
                    break;

                default:
                    coltype = "TRANSACTIONSTATEMENT1";
                    break;
            }
            return coltype;
        }

        private DataTable CollectCurDate(DataTable dt)
        {
            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");



            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chqdate >= '" + frdate + "' and chqdate<= '" + todate + "'");
            dt = dv.ToTable();
            return dt;

        }






        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
            ////    Hashtable hst = (Hashtable)Session["tblLogin"];
            ////    string comcod = hst["comcod"].ToString();
            ////    string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
            ////    string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

            ////    hlink1.NavigateUrl = "~/F_23_SaM/Print?Type=OrderPrint&comcod=" + comcod + "&centrid=" + centrid + "&orderno=" + orderno;
            ////}
        }
        protected void grvTrnDatWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label CollFrm = (Label)e.Row.FindControl("lgvCollFrm");
                Label Cashamt = (Label)e.Row.FindControl("lgvCaAmt");
                Label chqamt = (Label)e.Row.FindControl("lgvChAmt");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();

                if (grp == "")
                {
                    return;
                }
                if (grp == "F" || grp == "G")
                {

                    CollFrm.Font.Bold = true;
                    Cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                }

                if (mrno == "AAAAAAAAA")
                {
                    CollFrm.Font.Bold = true;
                    Cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                }
            }
        }

        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {

                case "grvImple":
                    this.grvImple.DataSource = HiddenSameData(dt);
                    this.grvImple.DataBind();
                    break;

                case "gvConUpdat":
                    this.gvConUpdat.DataSource = HiddenSameData(dt);
                    this.gvConUpdat.DataBind();
                    break;

                case "gvsubbill":
                    this.gvsubbill.DataSource = HiddenSameData(dt);
                    this.gvsubbill.DataBind();
                    break;


                case "gvfrec":
                    this.gvfrec.DataSource = HiddenSameData(dt);
                    this.gvfrec.DataBind();
                    break;

                case "gvsrec":
                    this.gvsrec.DataSource = HiddenSameData(dt);
                    this.gvsrec.DataBind();
                    break;


                case "gvthrec":
                    this.gvthrec.DataSource = HiddenSameData(dt);
                    this.gvthrec.DataBind();
                    break;


                case "gvexecution":
                    this.gvexecution.DataSource = HiddenSameData(dt);
                    this.gvexecution.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;


                case "gvfinal":
                    this.gvfinal.DataSource = HiddenSameData(dt);
                    this.gvfinal.DataBind();
                    break;


                case "gvfinalapp":
                    this.gvfinalapp.DataSource = HiddenSameData(dt);
                    this.gvfinalapp.DataBind();

                    break;
                case "gvlabbillreq":
                    this.gvlabbillreq.DataSource = HiddenSameData(dt);
                    this.gvlabbillreq.DataBind();

                    break;

                case "gvAllReq":
                    this.gvAllReq.DataSource = HiddenSameData(dt);
                    this.gvAllReq.DataBind();
                    break;


                case "gvbillcs":
                    this.gvbillcs.DataSource = HiddenSameData(dt);
                    this.gvbillcs.DataBind();
                    break;

                case "gvWorkOrder":
                    this.gvWorkOrder.DataSource = HiddenSameData(dt);
                    this.gvWorkOrder.DataBind();
                    break;

                case "gvReadyForBill":
                    this.gvReadyForBill.DataSource = HiddenSameData(dt);
                    this.gvReadyForBill.DataBind();
                    break;

                case "gvbillapp":
                    this.gvbillapp.DataSource = HiddenSameData(dt);
                    this.gvbillapp.DataBind();

                    break;





            }




        }




        protected void grvImple_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvImple.PageIndex = e.NewPageIndex;
            this.Data_Bind("grvImple", null);

        }


        protected void gvfinal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                LinkButton btnDelReqCheck = (LinkButton)e.Row.FindControl("btnDelReqCheck");


                HyperLink lnkbtnEditBilll = (HyperLink)e.Row.FindControl("lnkbtnEditBilll");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string lisuno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();
                string issustatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issustatus")).ToString();


                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                string isudate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "isudat")).ToString("dd-MMM-yyyy");
                if (issustatus == "S")
                {
                    hlink2.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=BillServiceEntry&genno=" + lisuno + "&prjcode=" + pactcode + "&sircode=" + sircode + "&status=" + issustatus;

                }
                else
                {
                    hlink2.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=BillEntry&genno=" + lisuno + "&prjcode=" + pactcode + "&sircode=" + sircode;

                }

                if (comcod == "3340")
                {
                    lnkbtnEditBilll.NavigateUrl = "~/F_09_PImp/PurLabIssue2?Type=Edit&genno=" + lisuno + "&prjcode=" + pactcode + "&sircode=" + sircode;

                }
                else
                {
                    lnkbtnEditBilll.NavigateUrl = "~/F_09_PImp/PurLabIssue?Type=Edit&genno=" + lisuno + "&prjcode=" + pactcode + "&sircode=" + sircode;

                }





                switch (comcod)
                {
                    case "3336":
                    case "3337":
                        btnDelReqCheck.Visible = false;
                        break;

                    default:
                        btnDelReqCheck.Visible = true;
                        break;

                }



            }
        }
        protected void gvsubbill_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string lisuno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                string isudate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "isudat")).ToString("dd-MMM-yyyy");

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ConBillPrint&lisuno=" + lisuno + "&pactcode=" + pactcode;

            }

        }
        protected void gvexecution_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvacamt = (HyperLink)e.Row.FindControl("hlnkgvacamt");
                //string pactcode=this.

                // Label tosaleamt = (Label)e.Row.FindControl("lgvtosaleamt");



                string isuno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isuno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();


                // double per = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "perotsale"));
                if (isuno == "")
                {
                    return;
                }

                else
                {
                    //string pactcode = Request.QueryString["pactcode"].ToString();
                    // string frmdate = Convert.ToDateTime(code.Substring(4,2)+"-"+"01"+"-"+code.Substring(0,4)).ToString("dd-MMM-yyyy");
                    string todate = txtdate.Text; //Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    hlnkgvacamt.Style.Add("color", "blue");
                    hlnkgvacamt.NavigateUrl = "~/F_32_Mis/LinkImpExeStatus?Type=DayWiseExecution&pactcode=" + pactcode + "&Date1=" + "01-jan-2015" + "&Date2=" + todate;




                }






            }

        }

        private bool XmlDataInsertReq(string lisuno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);


            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";



            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, lisuno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }

        protected void btnDelReqCheck_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string lisuno = ((Label)this.gvfinal.Rows[index].FindControl("lbllisuno")).Text.Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", lisuno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(lisuno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETEUPDATEPURLISUUE", lisuno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);



        }

        protected void gvConUpdat_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintBU");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                //string isudate = Convert.ToDateTime (DataBinder.Eval (e.Row.DataItem, "isudat")).ToString ("dd-MMM-yyyy");


                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ConBillFinalization&billno=" + billno;




            }
        }

        protected void gvfinalapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintINapp");
                HyperLink lnkbtnbfinapp = (HyperLink)e.Row.FindControl("lnkbtnbfinapp");

                LinkButton btnDelfinapp = (LinkButton)e.Row.FindControl("btnDelfinapp");



                // HyperLink lnkbtnEditBilll = (HyperLink)e.Row.FindControl("lnkbtnEditBilll");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();

                string billstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billstatus")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ConBillFinalization&billno=" + billno;

                lnkbtnbfinapp.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=BillConfirmed&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode + "&status=" + billstatus;

                // lnkbtnEditBilll.NavigateUrl = "~/F_09_PImp/PurLabIssue?Type=Edit&genno=" + lisuno + "&prjcode=" + pactcode + "&sircode=" + sircode;


                //if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "8306" )
                //{
                //    btnDelfinapp.Visible = false;
                //}





            }
        }
        protected void gvfrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnfrec");


                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                hlink1.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=FirstRecom&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode; ;

            }
        }
        protected void gvsrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnsrec");

                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                hlink1.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=SecRecom&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode; ;

            }
        }
        protected void gvthrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnthrec");

                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                hlink1.NavigateUrl = "~/F_09_PImp/PurSubConBillFinal?Type=ThirdRecom&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode; ;

            }
        }


        protected void gvfiapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnfiapp");

                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                hlink1.NavigateUrl = "~/F_09_PImp/PurLabIssue?Type=Approved&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode; ;

            }

        }


        protected void gvchaver_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnchaver");

                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                hlink1.NavigateUrl = "~/F_09_PImp/PurLabIssue?Type=CheckaVerify&genno=" + billno + "&prjcode=" + pactcode + "&sircode=" + sircode; ;

            }
        }

        protected void btnDelfrec_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvfrec.Rows[index].FindControl("lgvbillnofrec")).Text.Trim();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", billno, "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(billno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEBILLSFREC", billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            this.SaleRequRpt();

        }



        protected void btnDelsrec_OnClick(object sender, EventArgs e)
        {




            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvsrec.Rows[index].FindControl("lgvbillnonosrec")).Text.Trim();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", billno, "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(billno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEBILLSEREC", billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            this.SaleRequRpt();

        }
        protected void btnDelthrec_OnClick(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvthrec.Rows[index].FindControl("lgvbillnothrec")).Text.Trim();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", billno, "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(billno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEBILLTHREC", billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            this.SaleRequRpt();






        }


        private string GetthorFirApproval()
        {
            string comcod = this.GetCompCode();
            string thofiapproval = "";
            switch (comcod)
            {

                //case "3101":
                case "1103":


                    thofiapproval = "thapproval";
                    break;


                default:
                    thofiapproval = "";
                    break;


            }

            return thofiapproval;

        }


        private string GetFristApproval()
        {
            string comcod = this.GetCompCode();
            string firstarpval = "";
            switch (comcod)
            {

                //case "3101":
                case "3368": //Finlay


                    firstarpval = "firstarpval";
                    break;


                default:
                    firstarpval = "";
                    break;


            }

            return firstarpval;

        }


        protected void btnDelfinapp_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvfinalapp.Rows[index].FindControl("lgAPPcordernofiapp")).Text.Trim();
            string thofiapproval = this.GetthorFirApproval();
            string firstarpval = this.GetFristApproval(); //Only frist Approval 

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", billno, "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(billno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEBILLFINALFIAPP", billno, thofiapproval, firstarpval, "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }




            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            this.SaleRequRpt();

        }




        protected void btnDelchaver_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string lisuno = ((Label)this.gvfinal.Rows[index].FindControl("lbllisuno")).Text.Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", lisuno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(lisuno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETEUPDATEPURLISUUE", lisuno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

        }






        protected void btnDelapp_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string lisuno = ((Label)this.gvfinal.Rows[index].FindControl("lbllisuno")).Text.Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", lisuno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(lisuno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETEUPDATEPURLISUUE", lisuno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

        }


        protected void btnDelfiapp_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string lisuno = ((Label)this.gvfinal.Rows[index].FindControl("lbllisuno")).Text.Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", lisuno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(lisuno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETEUPDATEPURLISUUE", lisuno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

        }




        protected void gvlabbillreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkBillCS");
                HyperLink lnkbtnPrint = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEditBilllReq");
                string blreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lreqno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();


                hlink2.NavigateUrl = "~/F_09_PImp/PurLabRequisition?Type=Edit&prjcode=" + pactcode + "&genno=" + blreqno + "&sircode=";
                lnkbtnPrint.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=SubConBillReq&lisuno=" + blreqno + "&pactcode=" + pactcode;

                //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
                //// string currentptah = "PurchasePrint?Type=ConBillPrint&lisuno=" + lisuno + "&pactcode=" + pactcode;
                //string currentptah = "PurchasePrint?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode;
                //string totalpath = hostname + currentptah;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";
                switch (comcod)
                {


                    case "3370":   //cpdl                      
                    case "1205":   //p2p
                    case "3351":   //p2p
                    case "3352":   //p2p

                        hlink1.NavigateUrl = "~/F_14_Pro/PurMktSurveyCont?Type=ConCS&lisuno=" + blreqno + "&pactcode=" + pactcode;

                        break;
                    default:
                        hlink1.NavigateUrl = "~/F_12_Inv/LinkMktSurvey?reqno=" + blreqno;
                        break;
                }
            }
        }

        protected void gvbillcs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkBillCSApp");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEditBilllReq");
                HyperLink lnkbtnPrintCSApp = (HyperLink)e.Row.FindControl("lnkbtnPrintCSApp");

                string blreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lreqno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string recomsup = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recomsup")).ToString() == "" ? "" : Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recomsup")).ToString();
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString() == "" ? "" : Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString();

                hlink1.NavigateUrl = "~/F_09_PImp/PurLabRequisition?Type=CSApproval&prjcode=" + pactcode + "&genno=" + blreqno + "&sircode=" + "" + "&recomsup=" + recomsup + "&msrno=" + msrno;

                lnkbtnPrintCSApp.NavigateUrl = "~/F_14_Pro/PurMktSurveyCont?Type=ConCS&lisuno=" + blreqno + "&pactcode=" + pactcode + "&pType=" + "CSApproval" + "&msrno=" + msrno;

            }
        }

        protected void btnDelReqCSApp_Click(object sender, EventArgs e)
        {
            // Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurLabRequisition?Type=CSApproval";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string msrno = ((Label)this.gvbillcs.Rows[RowIndex].FindControl("lgvSurveyNo")).Text.Trim();
            string refno = ((Label)this.gvbillcs.Rows[RowIndex].FindControl("lblgvissuerefbill")).Text.Trim();
            string reqno = ((Label)this.gvbillcs.Rows[RowIndex].FindControl("lblgvbillreqno")).Text.Trim();




            bool resulbill = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETE_BILLCS_APP", null, null, null, msrno, refno, usrid, trmnid, session, Date, reqno, "", "", "", "", "", "", "", "");

            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            //this.RadioButtonList1_SelectedIndexChanged(null, null);
            this.lbtnOk_Click(null, null);

        }

        protected void gvWorkOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkWorkOrder");

                string lreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lreqno")).ToString();
                string csircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                hlink1.NavigateUrl = "~/F_09_PImp/PurConWrkOrderEntry?Type=Entry&genno=" + lreqno + "&sircode=" + csircode + "&actcode=" + pactcode;

            }
        }

        protected void btnDelWrkodr_Click(object sender, EventArgs e)
        {
            // Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurConWrkOrderEntry?Type=Entry";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string reqno = ((Label)this.gvWorkOrder.Rows[RowIndex].FindControl("lblgvbillreq2")).Text.Trim();
            string csircode = ((Label)this.gvWorkOrder.Rows[RowIndex].FindControl("lblgvcsircode2")).Text.Trim();
            //string msrno = ((Label)this.gvbillcs.Rows[RowIndex].FindControl("lgvSurveyNo")).Text.Trim();


            bool resulbill = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETE_BILLCS_APP_Final", null, null, null, reqno, csircode, "", "", "", "", "", "", "");

            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            //this.RadioButtonList1_SelectedIndexChanged(null, null);
            this.lbtnOk_Click(null, null);
        }

        protected void gvReadyForBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkWorkOrder");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnPrintWorkOrder");

                string lreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lreqno")).ToString();
                string csircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

                hlink1.NavigateUrl = "~/F_09_PImp/PurLabIssue?Type=Current&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + csircode;
                hlink2.NavigateUrl = "~/F_09_PImp/PurConWrkOrderEntry?Type=Entry&genno=" + lreqno + "&sircode=" + csircode + "&actcode=" + pactcode + "&orderno=" + orderno;

            }
        }


        protected void btnDelReadyBill_Click(object sender, EventArgs e)
        {
            // Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurConWrkOrderEntry?Type=Entry";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string oderno = ((Label)this.gvReadyForBill.Rows[RowIndex].FindControl("lgvOrerNo")).Text.Trim();
            string lreqno = ((Label)this.gvReadyForBill.Rows[RowIndex].FindControl("lblgvlreq2")).Text.Trim();

            bool resulbill = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETE_BILLCS_WORKORDER", null, null, null, oderno, lreqno, "", "", "", "", "", "", "");

            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            this.lbtnOk_Click(null, null);
        }

        protected void gvbillapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnbillapp = (HyperLink)e.Row.FindControl("lnkbtnbillapp");

                string lisuno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lisuno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString();

                hlnbillapp.NavigateUrl = "~/F_09_PImp/PurLabIssue2?Type=BillApproval&prjcode=" + pactcode + "&genno=" + lisuno + "&sircode=" + sircode;
                //F_09_PImp/PurLabIssue2?Type=Current&prjcode=&genno=&sircode=
            }
        }
    }
}