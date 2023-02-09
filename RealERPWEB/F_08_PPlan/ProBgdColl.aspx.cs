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
namespace RealERPWEB.F_08_PPlan
{
    public partial class ProBgdColl : System.Web.UI.Page
    {
        ProcessAccess bgdData = new ProcessAccess();
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

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "Prolaovtar") ? "LAND AND OVERHEAD PLANNING" : "SALES & COLLECTION PLANNING";

                this.ImgbtnFindItem_Click(null, null);

                //this.lbtnUpdateAna.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                // this.gvAnalysis.Columns[1].Visible = (Convert.ToBoolean(dr1[0]["entry"]));             
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintAnaLysis_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {


        }

        private void GetGridData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlItem.SelectedValue.ToString();
            string Saltar = this.Request.QueryString["Type"].ToString() == "ProSalestar" ? "sales" : "";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROBGDRESSHOW", pactcode, Saltar, "", "", "", "", "", "", "");

            DateTime startdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prosdate"].ToString());
            DateTime enddate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["penddate"].ToString());
            this.lblStartDate.Text = startdate.ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = enddate.ToString("dd-MMM-yyyy");
            this.lblduration.Text = "Month: " + ds1.Tables[1].Rows[0]["mon"].ToString();
            var s = (enddate.Year * 12 + enddate.Month) - (startdate.Year * 12 + startdate.Month);

            for (int i = 1; i <= s + 1; i++)
            {
                if (i == 1)
                {
                    this.gvAnalysis.Columns[i + 5].Visible = true;
                    this.gvAnalysis.Columns[i + 5].HeaderText = startdate.ToString("MMM-yyyy");
                }
                else
                {
                    this.gvAnalysis.Columns[i + 5].Visible = true;
                    startdate = startdate.AddMonths(1);
                    this.gvAnalysis.Columns[i + 5].HeaderText = startdate.ToString("MMM-yyyy");
                }

            }
            ViewState["tblall"] = ds1.Tables[0];

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j <= s; j++)
                {
                    string gvQty2 = "qty" + ASTUtility.Right("00" + (j + 1).ToString(), 3);
                    double qty = Convert.ToDouble(ds1.Tables[0].Rows[i][gvQty2].ToString());
                    sum = sum + qty;
                }
                ds1.Tables[0].Rows[i]["albgdam"] = (Convert.ToDouble(ds1.Tables[0].Rows[i]["bgdam"].ToString()) - sum).ToString();
            }


            this.Data_Bind();

        }
        private void Data_Bind()
        {

            this.gvAnalysis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAnalysis.DataSource = (DataTable)ViewState["tblall"];
            this.gvAnalysis.DataBind();
            this.FooterCalCulation();

        }

        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)ViewState["tblall"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnalysis.FooterRow.FindControl("lgvFBudget")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdam)", "")) ? 0.00
                : dt.Compute("Sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAnalysis.FooterRow.FindControl("lgvFAlBudget")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(albgdam)", "")) ? 0.00
              : dt.Compute("Sum(albgdam)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void ImgbtnFindItem_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = "%" + this.txtItemSearch.Text.Trim() + "%";
            DataSet ds1 = bgdData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblItmCod"] = ds1.Tables[0];
            this.ddlItem.Items.Clear();
            this.ddlItem.DataTextField = "actdesc";
            this.ddlItem.DataValueField = "actcode";
            this.ddlItem.DataSource = ds1.Tables[0];
            this.ddlItem.DataBind();
            ds1.Dispose();
        }

        protected void lbtnOk1_Click(object sender, EventArgs e)
        {
            if (this.ddlItem.Items.Count == 0)
                return;

            if (this.lbtnOk1.Text == "New")
            {
                this.lbtnOk1.Text = "Ok";
                this.txtItemSearch.Enabled = true;
                this.ImgbtnFindItem.Enabled = true;
                this.ddlItem.Visible = true;
                this.lblItemDesc.Visible = false;
                this.PnlAnalysis.Visible = false;
                this.gvAnalysis.DataSource = null;
                this.gvAnalysis.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                return;
            }
            this.lbtnOk1.Text = "New";
            this.txtItemSearch.Enabled = false;
            this.ImgbtnFindItem.Enabled = false;
            this.ddlItem.Visible = false;
            this.lblItemDesc.Text = this.ddlItem.SelectedItem.Text.Trim();
            this.lblItemDesc.Width = this.ddlItem.Width;
            this.lblItemDesc.Visible = true;
            string ItmCod = this.ddlItem.SelectedValue.ToString().Trim();
            this.PnlAnalysis.Visible = true;
            GetGridData();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Budget Standard Analysis";
                string eventdesc = "Select Items";
                string eventdesc2 = this.ddlItem.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblall"];

            int rowindex;
            for (int i = 0; i < this.gvAnalysis.Rows.Count; i++)
            {

                rowindex = (this.gvAnalysis.PageSize * this.gvAnalysis.PageIndex) + i;

                double qty1 = Convert.ToDouble(ASTUtility.ExprToValue(((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty001")).Text.Trim()));
                tbl1.Rows[rowindex]["qty001"] = qty1;
                tbl1.Rows[rowindex]["qty002"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty002")).Text.Trim());
                tbl1.Rows[rowindex]["qty003"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty003")).Text.Trim());
                tbl1.Rows[rowindex]["qty004"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty004")).Text.Trim());
                tbl1.Rows[rowindex]["qty005"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty005")).Text.Trim());
                tbl1.Rows[rowindex]["qty006"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty006")).Text.Trim());
                tbl1.Rows[rowindex]["qty007"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty007")).Text.Trim());
                tbl1.Rows[rowindex]["qty008"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty008")).Text.Trim());
                tbl1.Rows[rowindex]["qty009"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty009")).Text.Trim());
                tbl1.Rows[rowindex]["qty010"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty010")).Text.Trim());
                tbl1.Rows[rowindex]["qty011"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty011")).Text.Trim());
                tbl1.Rows[rowindex]["qty012"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty012")).Text.Trim());
                tbl1.Rows[rowindex]["qty013"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty013")).Text.Trim());
                tbl1.Rows[rowindex]["qty014"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty014")).Text.Trim());
                tbl1.Rows[rowindex]["qty015"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty015")).Text.Trim());
                tbl1.Rows[rowindex]["qty016"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty016")).Text.Trim());
                tbl1.Rows[rowindex]["qty017"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty017")).Text.Trim());
                tbl1.Rows[rowindex]["qty018"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty018")).Text.Trim());
                tbl1.Rows[rowindex]["qty019"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty019")).Text.Trim());
                tbl1.Rows[rowindex]["qty020"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty020")).Text.Trim());
                tbl1.Rows[rowindex]["qty021"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty021")).Text.Trim());
                tbl1.Rows[rowindex]["qty022"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty022")).Text.Trim());
                tbl1.Rows[rowindex]["qty023"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty023")).Text.Trim());
                tbl1.Rows[rowindex]["qty024"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty024")).Text.Trim());
                tbl1.Rows[rowindex]["qty025"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty025")).Text.Trim());
                tbl1.Rows[rowindex]["qty026"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty026")).Text.Trim());
                tbl1.Rows[rowindex]["qty027"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty027")).Text.Trim());
                tbl1.Rows[rowindex]["qty028"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty028")).Text.Trim());
                tbl1.Rows[rowindex]["qty029"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty029")).Text.Trim());
                tbl1.Rows[rowindex]["qty030"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty030")).Text.Trim());
                tbl1.Rows[rowindex]["qty031"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty031")).Text.Trim());
                tbl1.Rows[rowindex]["qty032"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty032")).Text.Trim());
                tbl1.Rows[rowindex]["qty033"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty033")).Text.Trim());
                tbl1.Rows[rowindex]["qty034"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty034")).Text.Trim());
                tbl1.Rows[rowindex]["qty035"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty035")).Text.Trim());
                tbl1.Rows[rowindex]["qty036"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty036")).Text.Trim());
                tbl1.Rows[rowindex]["qty037"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty037")).Text.Trim());
                tbl1.Rows[rowindex]["qty038"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty038")).Text.Trim());
                tbl1.Rows[rowindex]["qty039"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty039")).Text.Trim());
                tbl1.Rows[rowindex]["qty040"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty040")).Text.Trim());
                tbl1.Rows[rowindex]["qty041"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty041")).Text.Trim());
                tbl1.Rows[rowindex]["qty042"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty042")).Text.Trim());
                tbl1.Rows[rowindex]["qty043"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty043")).Text.Trim());
                tbl1.Rows[rowindex]["qty044"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty044")).Text.Trim());
                tbl1.Rows[rowindex]["qty045"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty045")).Text.Trim());
                tbl1.Rows[rowindex]["qty046"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty046")).Text.Trim());
                tbl1.Rows[rowindex]["qty047"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty047")).Text.Trim());
                tbl1.Rows[rowindex]["qty048"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty048")).Text.Trim());
                tbl1.Rows[rowindex]["qty049"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty049")).Text.Trim());
                tbl1.Rows[rowindex]["qty050"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty050")).Text.Trim());
                tbl1.Rows[rowindex]["qty051"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty051")).Text.Trim());
                tbl1.Rows[rowindex]["qty052"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty052")).Text.Trim());

                tbl1.Rows[rowindex]["qty053"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty053")).Text.Trim());
                tbl1.Rows[rowindex]["qty054"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty054")).Text.Trim());
                tbl1.Rows[rowindex]["qty055"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty055")).Text.Trim());
                tbl1.Rows[rowindex]["qty056"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty056")).Text.Trim());
                tbl1.Rows[rowindex]["qty057"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty057")).Text.Trim());
                tbl1.Rows[rowindex]["qty058"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty058")).Text.Trim());
                tbl1.Rows[rowindex]["qty059"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty059")).Text.Trim());
                tbl1.Rows[rowindex]["qty060"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty060")).Text.Trim());
                tbl1.Rows[rowindex]["qty061"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty061")).Text.Trim());
                tbl1.Rows[rowindex]["qty062"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty062")).Text.Trim());
                tbl1.Rows[rowindex]["qty063"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty063")).Text.Trim());
                tbl1.Rows[rowindex]["qty064"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty064")).Text.Trim());
                tbl1.Rows[rowindex]["qty065"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty065")).Text.Trim());
                tbl1.Rows[rowindex]["qty066"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty066")).Text.Trim());
                tbl1.Rows[rowindex]["qty067"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty067")).Text.Trim());
                tbl1.Rows[rowindex]["qty068"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty068")).Text.Trim());
                tbl1.Rows[rowindex]["qty069"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty069")).Text.Trim());
                tbl1.Rows[rowindex]["qty070"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty070")).Text.Trim());
                tbl1.Rows[rowindex]["qty071"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty071")).Text.Trim());
                tbl1.Rows[rowindex]["qty072"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty072")).Text.Trim());
                tbl1.Rows[rowindex]["qty073"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty073")).Text.Trim());
                tbl1.Rows[rowindex]["qty074"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty074")).Text.Trim());
                tbl1.Rows[rowindex]["qty075"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty075")).Text.Trim());
                tbl1.Rows[rowindex]["qty076"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty076")).Text.Trim());
                tbl1.Rows[rowindex]["qty077"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty077")).Text.Trim());
                tbl1.Rows[rowindex]["qty078"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty078")).Text.Trim());
                tbl1.Rows[rowindex]["qty079"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty079")).Text.Trim());
                tbl1.Rows[rowindex]["qty080"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty080")).Text.Trim());
                tbl1.Rows[rowindex]["qty081"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty081")).Text.Trim());
                tbl1.Rows[rowindex]["qty082"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty082")).Text.Trim());
                tbl1.Rows[rowindex]["qty083"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty083")).Text.Trim());
                tbl1.Rows[rowindex]["qty084"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty084")).Text.Trim());
                tbl1.Rows[rowindex]["qty085"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty085")).Text.Trim());
                tbl1.Rows[rowindex]["qty086"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty086")).Text.Trim());
                tbl1.Rows[rowindex]["qty087"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty087")).Text.Trim());
                tbl1.Rows[rowindex]["qty088"] = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty088")).Text.Trim());



            }
            ViewState["tblall"] = tbl1;
        }


        protected void lbtnUpdateAna_Click(object sender, EventArgs e)
        {
            SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable tbl1 = (DataTable)ViewState["tblall"];
            //DataTable ds1 = (DataTable)ViewState["tblKK"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlItem.SelectedValue.ToString();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                DateTime startdate = Convert.ToDateTime(this.lblStartDate.Text);
                DateTime enddate = Convert.ToDateTime(this.lblEndDate.Text);

                ////DateTime startdate = Convert.ToDateTime(ds1.Rows[0]["prosdate"].ToString());
                ////DateTime enddate = Convert.ToDateTime(ds1.Rows[1]["penddate"].ToString());
                var s = (enddate.Year * 12 + enddate.Month) - (startdate.Year * 12 + startdate.Month);
                string monthid;
                string rescode = tbl1.Rows[i]["sircode"].ToString();
                int rowindex = (this.gvAnalysis.PageSize * this.gvAnalysis.PageIndex) + i;
                for (int j = 0; j <= s; j++)
                {
                    if (j == 0)
                    {
                        monthid = startdate.ToString("yyyyMM");
                    }
                    else
                    {
                        startdate = startdate.AddMonths(1);
                        monthid = startdate.ToString("yyyyMM");
                    }
                    string gvQty2 = "qty" + ASTUtility.Right("00" + (j + 1).ToString(), 3);
                    string qty = tbl1.Rows[i][gvQty2].ToString();
                    if (qty == "0" || qty == "0.00" || qty == "0.000000")
                    { }
                    else
                    {

                        string mstdate = monthid.Substring(4, 2) + "/01/" + monthid.Substring(0, 4);
                        bool result = bgdData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INSERTUPDATEPROBGDCOL", pactcode, monthid, rescode, qty, mstdate, "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successful";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                        }
                    }
                }
            }


        }
        protected void lbtngvSlNo_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnInputSame_Click(object sender, EventArgs e)
        {
            SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblall"];
            for (int i = 0; i < gvAnalysis.Rows.Count; i++)
            {
                int rowindex = (this.gvAnalysis.PageSize * this.gvAnalysis.PageIndex) + i;
                double gvQty1 = Convert.ToDouble("0" + ((TextBox)this.gvAnalysis.Rows[i].FindControl("txtqty001")).Text.Trim().Replace(",", ""));

                for (int j = 1; j <= 88; j++)
                {
                    string gvQty2 = "qty" + ASTUtility.Right("00" + j.ToString(), 3);

                    tbl1.Rows[rowindex][gvQty2] = gvQty1;
                    string gvQty3 = "txtqty" + ASTUtility.Right("00" + j.ToString(), 3);
                    ((TextBox)this.gvAnalysis.Rows[i].FindControl(gvQty3)).Text = gvQty1.ToString("#,##0.0000;(#,##0.0000); ");
                }
            }
            ViewState["tblall"] = tbl1;
            // DataTable ds1 = (DataTable)ViewState["tblKK"];
            DateTime startdate = Convert.ToDateTime(this.lblStartDate.Text);
            DateTime enddate = Convert.ToDateTime(this.lblEndDate.Text);
            this.lblStartDate.Text = startdate.ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = enddate.ToString("dd-MMM-yyyy");
            var s = (enddate.Year * 12 + enddate.Month) - (startdate.Year * 12 + startdate.Month);
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j <= s; j++)
                {
                    string gvQty2 = "qty" + ASTUtility.Right("00" + (j + 1).ToString(), 3);
                    double qty = Convert.ToDouble(tbl1.Rows[i][gvQty2].ToString());
                    sum = sum + qty;
                }
                tbl1.Rows[i]["albgdam"] = (Convert.ToDouble(tbl1.Rows[i]["bgdam"].ToString()) - sum).ToString();
            }
            this.Data_Bind();
            //this.SaveValue();
            //this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvAnalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvAnalysis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblall"];

            DateTime startdate = Convert.ToDateTime(this.lblStartDate.Text);
            DateTime enddate = Convert.ToDateTime(this.lblEndDate.Text);

            var s = (enddate.Year * 12 + enddate.Month) - (startdate.Year * 12 + startdate.Month);
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j <= s; j++)
                {
                    string gvQty2 = "qty" + ASTUtility.Right("00" + (j + 1).ToString(), 3);
                    double qty = Convert.ToDouble(tbl1.Rows[i][gvQty2].ToString());
                    sum = sum + qty;
                }
                tbl1.Rows[i]["albgdam"] = (Convert.ToDouble(tbl1.Rows[i]["bgdam"].ToString()) - sum).ToString();
            }
            this.Data_Bind();
        }

        private void AutoGenerate()
        {

            DataTable tbl1 = (DataTable)ViewState["tblall"];


            double duration = Convert.ToDouble("0" + this.lblduration.Text.Replace("Month: ", ""));
            int rowindex = 0;
            foreach (DataRow dr in tbl1.Rows)
            {

                double bgdamt = Convert.ToDouble(dr["bgdam"].ToString());
                double monbgdamt = Math.Round((bgdamt / duration), 0);
                double unminmon = 0;

                for (int j = 1; j <= duration; j++)
                {
                    string gvQty = "qty" + ASTUtility.Right("00" + j.ToString(), 3);
                    tbl1.Rows[rowindex][gvQty] = (j == duration) ? (bgdamt - unminmon) : monbgdamt;

                    unminmon = unminmon + monbgdamt;

                }
                tbl1.Rows[rowindex]["albgdam"] = 0.00;

                rowindex++;
            }


            ViewState["tblall"] = tbl1;



        }
        protected void lbtnAutoGenerate_Click(object sender, EventArgs e)
        {
            this.AutoGenerate();
            this.Data_Bind();

        }
        protected void gvAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string pactcode = this.ddlItem.SelectedValue;

                if (code == "")
                {
                    return;
                }

                hlink1.NavigateUrl = "~/F_08_PPLan/LandOwnerPayment.aspx?pactcode=" + pactcode + "&sircode=" + code;

            }
        }
    }
}

