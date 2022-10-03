using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_07_Ten
{
    public partial class CivilConBOQ : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Civil Construction BOQ";

                DateTime curdate = System.DateTime.Today;
                this.txtfodate.Text = curdate.ToString("dd-MMM-yyyy");

                this.GetProjectsList();
                this.GetWorksGroup();
                this.GetFlorList();
                this.UnitConvr();

                this.CreateTable();
                CommonButton();



            }
        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);




            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();


            tblt01.Columns.Add("itemid", Type.GetType("System.String"));
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));

            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("sdetails", Type.GetType("System.String"));
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            tblt01.Columns.Add("itemcode", Type.GetType("System.String"));
            tblt01.Columns.Add("unit", Type.GetType("System.String"));
            tblt01.Columns.Add("baseUnit", Type.GetType("System.String"));

            tblt01.Columns.Add("flrdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("flrcod", Type.GetType("System.String"));

            tblt01.Columns.Add("convrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            tblt01.Columns.Add("ordam", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("ohamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("ttamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("taxvatamt", Type.GetType("System.Double"));




            tblt01.Columns.Add("costvatoh", Type.GetType("System.Double"));
            tblt01.Columns.Add("actamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("diffamt", Type.GetType("System.Double"));

            tblt01.Columns.Add("sbtrate_per", Type.GetType("System.Double"));
            tblt01.Columns.Add("actamt_per", Type.GetType("System.Double"));
            tblt01.Columns.Add("costvatoh_per", Type.GetType("System.Double"));



            ViewState["tblt01"] = tblt01;
        }
        private void GetProjectsList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "PRJCODELIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "infdesc";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }




        private void GetFlorList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETFLOORINFORMATION", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.DropCheck.DataTextField = "flrdes";
            this.DropCheck.DataValueField = "flrcod";
            this.DropCheck.DataSource = ds1.Tables[0];
            this.DropCheck.DataBind();

            ViewState["tblflrlist"] = ds1.Tables[0];

        }

        private void UnitConvr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETUNITCONVERSION", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblUnitconv"] = ds1.Tables[0];
        }



        private void GetWorksGroup()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKGROUPLIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkGroup.DataTextField = "workdesc";
            this.ddlWorkGroup.DataValueField = "workcode";
            this.ddlWorkGroup.DataSource = ds1.Tables[0];
            this.ddlWorkGroup.DataBind();
        }
        private void GetWorksList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            string prjcode = this.ddlProject.SelectedValue.ToString();
            string workgrp = (this.ddlWorkGroup.SelectedValue.ToString() == "000000000000") ? "%" : ASTUtility.Left(this.ddlWorkGroup.SelectedValue.ToString(), 4) + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKLIST", workgrp, prjcode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //this.ddlWorkList.DataTextField = "workdesc";
            //this.ddlWorkList.DataValueField = "workcode";
            //this.ddlWorkList.DataSource = ds1.Tables[0];
            //this.ddlWorkList.DataBind();


            ViewState["tblworklist"] = ds1.Tables[0];
            this.ddlWorkList.DataTextField = "workdesc";
            this.ddlWorkList.DataValueField = "workcode";
            this.ddlWorkList.DataSource = ds1.Tables[0];
            this.ddlWorkList.DataBind();

        }


        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {
            if (this.ddlProject.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Select Projects" + "');", true);

                return;
            }


            if (this.lnkbtnOK.Text == "Ok")
            {
                ViewState.Remove("tblt01");
                this.lnkbtnOK.Text = "New";
                this.CreateTable();
                this.gvCivilBoq.DataSource = null;
                this.gvCivilBoq.DataBind();
                this.ddlProject.Enabled = false;
                this.divResource.Visible = true;
                this.gvCivilBoq.Visible = true;
                this.txtSbtRate_Per.Enabled = false;
                this.txtACCost_Per.Enabled = false;
                this.txtACCostVatOH_Per.Enabled = false;

                this.ddlPrevReqList.Visible = false; //
                this.txtfodate.Enabled = false;

                this.GetWorksGroup();
                this.GetFlorList();
                this.UnitConvr();
                this.ddlProject_SelectedIndexChanged(null, null);
                return;
            }

            this.lnkbtnOK.Text = "Ok";
            this.divResource.Visible = false;
            this.gvCivilBoq.Visible = false;
            this.txtSbtRate_Per.Enabled = true;
            this.txtACCost_Per.Enabled = true;
            this.txtACCostVatOH_Per.Enabled = true;
            this.ddlProject.Enabled = true;
            this.txtfodate.Enabled = true;
            ViewState.Remove("tblt01");

            this.gvCivilBoq.DataSource = null;
            this.gvCivilBoq.DataBind();
            this.txtSbtRate_Per.Text = "0";
            this.txtACCost_Per.Text = "0";
            this.txtACCostVatOH_Per.Text = "0";


        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            DataTable tblunit = (DataTable)ViewState["tblUnitconv"];
            DataTable tblwlist = (DataTable)ViewState["tblworklist"];
            DataTable tblflr = (DataTable)ViewState["tblflrlist"];

            string actcode = this.ddlProject.SelectedValue.ToString();
            string actdesc = this.ddlProject.SelectedItem.ToString();
            string workcode = this.ddlWorkList.SelectedValue.ToString();
            string subdesc = this.ddlWorkList.SelectedItem.ToString();

            string FLRCOD = this.DropCheck.SelectedValue.ToString();

            string txtsbtrate_per = this.txtSbtRate_Per.Text.Trim().ToString();
            string txtactamt_per = this.txtACCost_Per.Text.Trim().ToString();
            string txtcostvatoh_per = this.txtACCostVatOH_Per.Text.Trim().ToString();
            double convrate = 0.00;
 
            string floredesc = this.DropCheck.SelectedItem.ToString();

            if (workcode != "000000000000")
            {
                foreach (ListItem lab1 in DropCheck.Items)
                {
                    if (lab1.Selected)
                    {
                        string flrcod = lab1.Value;
                        string flrdes = lab1.Text;

                        DataRow[] dr = tblt01.Select(" flrcod='" + flrcod + "' and subcode='" + workcode + "'");
                        if (dr.Length == 0)
                        {
                            DataRow[] drwl = tblwlist.Select("workcode='" + workcode + "'");
                            string baseUnit = drwl[0]["baseUnit"].ToString();
                            string baseUnitDesc = drwl[0]["isirunit"].ToString();
                            string sdetails = drwl[0]["sdetails"].ToString();


                            DataRow[] dr3 = tblunit.Select("bcod='" + baseUnit + "'");
                            if (dr3.Count() == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Unit Not Found" + "');", true);
                                return;
                            }
                            convrate = Convert.ToDouble(dr3[0]["conrat"].ToString());
                            string convUnitDesc = dr3[0]["uconvdesc"].ToString();
                            string convUnitcode = dr3[0]["ccod"].ToString();

                            DataRow dr1 = tblt01.NewRow();
                            dr1["itemid"] = 0;
                            dr1["actcode"] = actcode;
                            dr1["actdesc"] = actdesc;
                            dr1["subcode"] = workcode;
                            dr1["subdesc"] = subdesc;
                            dr1["flrdesc"] = flrdes;
                            dr1["FLRCOD"] = flrcod;
                            dr1["sdetails"] = sdetails;
                            dr1["itemcode"] = "";
                            dr1["qty"] = 0.00;
                            dr1["baseUnit"] = baseUnit;
                            dr1["unit"] = baseUnitDesc;
                            dr1["rate"] = convrate;
                            dr1["convrate"] = convrate;
                            dr1["ordam"] = 0.00;
                            dr1["sbtrate"] = 0.00;
                            dr1["sbtamt"] = 0.00;
                            dr1["ohamt"] = 0.00;
                            dr1["ttamt"] = 0.00;
                            dr1["taxvatamt"] = 0.00;
                            dr1["costvatoh"] = 0.00;
                            dr1["actamt"] = 0.00;
                            dr1["diffamt"] = 0.00;
                            dr1["sbtrate_per"] = Convert.ToDouble("0" + txtsbtrate_per);
                            dr1["actamt_per"] = Convert.ToDouble("0" + txtactamt_per);
                            dr1["costvatoh_per"] = Convert.ToDouble("0" + txtcostvatoh_per);
                            tblt01.Rows.Add(dr1);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Item Exist In The List" + "');", true);
                        }
                    }
                }
 
            }

            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            this.gvCivilBoq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCivilBoq.DataSource = tblt01;
            this.gvCivilBoq.DataBind();
            FooterCalcultion();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;



            
            string subcode;
            subcode = dt1.Rows[0]["subcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                
                

                if ( dt1.Rows[j]["subcode"].ToString() == subcode)
                {
                   
                    
                    dt1.Rows[j]["subdesc"] = "";
                    dt1.Rows[j]["sdetails"] = "";
                }

               
                   
              subcode = dt1.Rows[j]["subcode"].ToString();
                

            }

            return dt1;


        }



        private void FooterCalcultion()
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            if (tblt01.Rows.Count > 0)
            {
                ((Label)this.gvCivilBoq.FooterRow.FindControl("lblgvFAmtInTk")).Text = Convert.ToDouble((Convert.IsDBNull(tblt01.Compute("Sum(ordam)", "")) ? 0.00 : tblt01.Compute("Sum(ordam)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvCivilBoq.FooterRow.FindControl("lblgvFactamt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt01.Compute("Sum(actamt)", "")) ? 0.00 : tblt01.Compute("Sum(actamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvCivilBoq.FooterRow.FindControl("lblgvFdiffamt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt01.Compute("Sum(diffamt)", "")) ? 0.00 : tblt01.Compute("Sum(diffamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetWorksList();
        }

        protected void gvCivilBoq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCivilBoq_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            // ViewState_update();


            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;
            TextBox txtboxqty = (TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty") as TextBox;
            string actcode = lblactcode.Text;
            string workcode = lblWcode.Text;
            Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;
            double qty = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtrate")).Text.Trim());
            double ammount = qty * rate;
            lTotalRate.Text = ammount.ToString();
            txtboxqty.Text = qty.ToString();
            string itemcode = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txItemCode")).Text.ToString();
            string txtSdetials = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtSdetials")).Text.ToString();


            Label txtprft_rate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvProfit") as Label;
            Label txtTtl_amt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblttlamt") as Label;
            Label txtVATTax = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvVatTax") as Label;
            Label txtcostvatoh = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvcostvatoh") as Label;
            Label txtactamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactamt") as Label;
            Label txtdiffamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvdiffamt") as Label;

            double actCost = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtsbamt")).Text.Trim());
            double ordam = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lblordam")).Text.Trim());
            double convrate = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lbconvrate")).Text.Trim());

            double proft_per = Convert.ToDouble("0" + this.txtSbtRate_Per.Text.Trim());
            double ohamt_per = Convert.ToDouble("0" + this.txtACCost_Per.Text.Trim());
            double taxVat_per = Convert.ToDouble("0" + this.txtACCostVatOH_Per.Text.Trim());
            double prft_rate = (proft_per * actCost) / 100;
            double ohamt_rate = (ohamt_per * actCost) / 100;
            double ttamt = actCost + prft_rate + ohamt_rate;
            double taxvatamt = (ttamt * taxVat_per) / 100;
            double actamtvo = ttamt + taxvatamt;
            double actamt = (qty * actamtvo * convrate);
            double diff = ordam - actamt;
            txtprft_rate.Text = prft_rate.ToString();
            txtTtl_amt.Text = ttamt.ToString();
            txtVATTax.Text = taxvatamt.ToString();
            txtcostvatoh.Text = actamtvo.ToString();
            txtactamt.Text = actamt.ToString();
            txtdiffamt.Text = diff.ToString();
            string FLRCOD = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblflorcode")).Text.ToString();
            DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "' and subcode='" + workcode + "'and FLRCOD='" + FLRCOD + "'");


            dr1[0]["qty"] = qty;
            dr1[0]["rate"] = rate;
            dr1[0]["ordam"] = ammount;
            dr1[0]["itemcode"] = itemcode;
            dr1[0]["sdetails"] = txtSdetials;

            dr1[0]["sbtamt"] = actCost;
            dr1[0]["sbtrate"] = prft_rate;
            dr1[0]["ohamt"] = ohamt_rate;
            dr1[0]["ttamt"] = ttamt;
            dr1[0]["taxvatamt"] = taxvatamt;
            dr1[0]["costvatoh"] = actamtvo;
            dr1[0]["actamt"] = actamt;
            dr1[0]["diffamt"] = diff;

            tblt01.AcceptChanges();
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }

        protected void txtrate_TextChanged(object sender, EventArgs e)
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;
            string actcode = lblactcode.Text;
            string workcode = lblWcode.Text;
            Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;
            double qty = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtrate")).Text.Trim());
            double ammount = qty * rate;
            lTotalRate.Text = ammount.ToString();
            string txtSdetials = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtSdetials")).Text.ToString();


            Label txtprft_rate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvProfit") as Label;
            Label txtTtl_amt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblttlamt") as Label;
            Label txtVATTax = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvVatTax") as Label;
            Label txtcostvatoh = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvcostvatoh") as Label;
            Label txtactamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactamt") as Label;
            Label txtdiffamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvdiffamt") as Label;
            string itemcode = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txItemCode")).Text.ToString();

            double actCost = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtsbamt")).Text.Trim());
            double ordam = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lblordam")).Text.Trim());
            double convrate = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lbconvrate")).Text.Trim());

            double proft_per = Convert.ToDouble("0" + this.txtSbtRate_Per.Text.Trim());
            double ohamt_per = Convert.ToDouble("0" + this.txtACCost_Per.Text.Trim());
            double taxVat_per = Convert.ToDouble("0" + this.txtACCostVatOH_Per.Text.Trim());
            double prft_rate = (proft_per * actCost) / 100;
            double ohamt_rate = (ohamt_per * actCost) / 100;
            double ttamt = actCost + prft_rate + ohamt_rate;
            double taxvatamt = (ttamt * taxVat_per) / 100;
            double actamtvo = ttamt + taxvatamt;
            double actamt = (qty * actamtvo * convrate);
            double diff = ordam - actamt;
            txtprft_rate.Text = prft_rate.ToString();
            txtTtl_amt.Text = ttamt.ToString();
            txtVATTax.Text = taxvatamt.ToString();
            txtcostvatoh.Text = actamtvo.ToString();
            txtactamt.Text = actamt.ToString();
            txtdiffamt.Text = diff.ToString();
            string FLRCOD = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblflorcode")).Text.ToString();
            DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "' and subcode='" + workcode + "'and FLRCOD='" + FLRCOD + "'");


            dr1[0]["qty"] = qty;
            dr1[0]["rate"] = rate;
            dr1[0]["ordam"] = ammount;
            dr1[0]["itemcode"] = itemcode;
            dr1[0]["sdetails"] = txtSdetials;

            dr1[0]["sbtamt"] = actCost;
            dr1[0]["sbtrate"] = prft_rate;
            dr1[0]["ohamt"] = ohamt_rate;
            dr1[0]["ttamt"] = ttamt;
            dr1[0]["taxvatamt"] = taxvatamt;
            dr1[0]["costvatoh"] = actamtvo;
            dr1[0]["actamt"] = actamt;
            dr1[0]["diffamt"] = diff;

            tblt01.AcceptChanges();
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();

        }

        protected void txtsbamt_TextChanged(object sender, EventArgs e)
        {

            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;
            string actcode = lblactcode.Text;
            string workcode = lblWcode.Text;
            Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;
            double qty = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtrate")).Text.Trim());
            double ammount = qty * rate;
            lTotalRate.Text = ammount.ToString();
            string itemcode = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txItemCode")).Text.ToString();

            string txtSdetials = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtSdetials")).Text.ToString();

            Label txtprft_rate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvProfit") as Label;
            Label txtTtl_amt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblttlamt") as Label;
            Label txtVATTax = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvVatTax") as Label;
            Label txtcostvatoh = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvcostvatoh") as Label;
            Label txtactamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactamt") as Label;
            Label txtdiffamt = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvdiffamt") as Label;

            double actCost = Convert.ToDouble("0" + ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txtsbamt")).Text.Trim());
            double ordam = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lblordam")).Text.Trim());
            double convrate = Convert.ToDouble("0" + ((Label)this.gvCivilBoq.Rows[index].FindControl("lbconvrate")).Text.Trim());

            double proft_per = Convert.ToDouble("0" + this.txtSbtRate_Per.Text.Trim());
            double ohamt_per = Convert.ToDouble("0" + this.txtACCost_Per.Text.Trim());
            double taxVat_per = Convert.ToDouble("0" + this.txtACCostVatOH_Per.Text.Trim());
            double prft_rate = (proft_per * actCost) / 100;
            double ohamt_rate = (ohamt_per * actCost) / 100;
            double ttamt = actCost + prft_rate + ohamt_rate;
            double taxvatamt = (ttamt * taxVat_per) / 100;
            double actamtvo = ttamt + taxvatamt;
            double actamt = (qty * actamtvo * convrate);
            double diff = ordam - actamt;
            txtprft_rate.Text = prft_rate.ToString();
            txtTtl_amt.Text = ttamt.ToString();
            txtVATTax.Text = taxvatamt.ToString();
            txtcostvatoh.Text = actamtvo.ToString();
            txtactamt.Text = actamt.ToString();
            txtdiffamt.Text = diff.ToString();

            string FLRCOD = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblflorcode")).Text.ToString();
            DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "' and subcode='" + workcode + "'and FLRCOD='" + FLRCOD + "'");


            dr1[0]["qty"] = qty;
            dr1[0]["rate"] = rate;
            dr1[0]["ordam"] = ammount;
            dr1[0]["itemcode"] = itemcode;
            dr1[0]["sdetails"] = txtSdetials;

            dr1[0]["sbtamt"] = actCost;
            dr1[0]["sbtrate"] = prft_rate;
            dr1[0]["ohamt"] = ohamt_rate;
            dr1[0]["ttamt"] = ttamt;
            dr1[0]["taxvatamt"] = taxvatamt;
            dr1[0]["costvatoh"] = actamtvo;
            dr1[0]["actamt"] = actamt;
            dr1[0]["diffamt"] = diff;

            tblt01.AcceptChanges();
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();

                DataTable dt1 = (DataTable)ViewState["tblt01"];
                bool result = true;
                string boqdate = this.txtfodate.Text.Substring(0, 11);

                string actcode = this.ddlProject.SelectedValue.ToString();
                double proft_per = Convert.ToDouble("0" + this.txtSbtRate_Per.Text.Trim());
                double ohamt_per = Convert.ToDouble("0" + this.txtACCost_Per.Text.Trim());
                double taxVat_per = Convert.ToDouble("0" + this.txtACCostVatOH_Per.Text.Trim());



                result = ImpleData.UpdateTransInfo2(comcod, "SP_TANDER_PROCESS", "INSERTTENDERDATA", "", actcode, boqdate, proft_per.ToString(), ohamt_per.ToString(),
                    taxVat_per.ToString(), userid, "", "", "", "", "", "",
                          "", "", "", "", "", "", "TENBOQB", "");


                if (result == true)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {

                        string subcode = dr["subcode"].ToString();
                        string itemcode = dr["itemcode"].ToString();
                        string qty = dr["qty"].ToString();
                        string unit = dr["unit"].ToString();
                        string rate = dr["rate"].ToString();
                        string convrate = dr["convrate"].ToString();
                        string oramt = dr["ordam"].ToString();
                        string sbtrate = dr["sbtrate"].ToString();
                        string sbtamt = dr["sbtamt"].ToString();
                        string ohamt = dr["ohamt"].ToString();
                        string ttamt = dr["ttamt"].ToString();
                        string taxvatamt = dr["taxvatamt"].ToString();
                        string costvatoh = dr["costvatoh"].ToString();
                        string actamt = dr["actamt"].ToString();
                        string diffamt = dr["diffamt"].ToString();
                        string sbtrate_per = dr["sbtrate_per"].ToString();
                        string actamt_per = dr["actamt_per"].ToString();
                        string costvatoh_per = dr["costvatoh_per"].ToString();
                        string unitcode = dr["baseUnit"].ToString();
                        string flrcod = dr["flrcod"].ToString();
                        string sdetails = dr["sdetails"].ToString();

                        result = ImpleData.UpdateTransInfo2(comcod, "SP_TANDER_PROCESS", "INSERTTENDERDATA", "", actcode, itemcode, 
                            subcode, qty, rate, oramt, sbtamt, sbtrate, ohamt, ttamt, taxvatamt, costvatoh,actamt, diffamt, unitcode, 
                            flrcod, sdetails, "", "TENBOQA", "");



                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                            return;
                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }



        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetReqNo()
        {
            //string comcod = this.GetCompCode();
            //string mREQNO = "NEWREQ";
            //if (this.ddlPrevReqList.Items.Count > 0)
            //    mREQNO = this.ddlPrevReqList.SelectedValue.ToString();
            //string boqdate = this.txtfodate.Text.Substring(0, 11);

            //if (mREQNO == "NEWREQ")
            //{
            //    DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETLASTBOQNO", boqdate,
            //           "", "", "", "", "", "", "", "");
            //    if (ds2 == null)
            //        return;
            //    if (ds2.Tables[0].Rows.Count > 0)
            //    {

            //        this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(0, 6);
            //        this.txtCurTndNo.Text = ds2.Tables[0].Rows[0]["maxbgdno1"].ToString().Substring(6, 5);

            //        this.ddlPrevReqList.DataTextField = "maxbgdno1";
            //        this.ddlPrevReqList.DataValueField = "boqid";
            //        this.ddlPrevReqList.DataSource = ds2.Tables[0];
            //        this.ddlPrevReqList.DataBind();
            //    }
            //}

        }


        protected void txItemCode_TextChanged(object sender, EventArgs e)
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;

            Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;

            string actcode = lblactcode.Text;
            string workcode = lblWcode.Text;

            Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;

            string itemcode = ((TextBox)this.gvCivilBoq.Rows[index].FindControl("txItemCode")).Text.ToString();
            string FLRCOD = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblflorcode")).Text.ToString();
            DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "' and subcode='" + workcode + "'and FLRCOD='" + FLRCOD + "'");
            dr1[0]["itemcode"] = itemcode;


            tblt01.AcceptChanges();

            //tblt01.Rows.Add(dr1);

            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }

        protected void lnkDelrow_Click(object sender, EventArgs e)
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string actcode = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode")).Text.ToString();
            string subcode = ((Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode")).Text.ToString();


            bool result = ImpleData.UpdateTransInfo2(comcod, "SP_TANDER_PROCESS", "DELBOQIDBYID",
                             actcode, subcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Data Delete Fail" + "');", true);


                return;
            }

            tblt01.Rows.RemoveAt(index);
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Delete successfully" + "');", true);



        }

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string boqdate = this.txtfodate.Text.Substring(0, 11);


            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GET_PREV_TND_LIST", boqdate,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "boqid1";
            this.ddlPrevReqList.DataValueField = "boqid";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();
        }

        protected void ddlPrevReqList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string prevtndid = this.ddlPrevReqList.SelectedValue.ToString();

            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETTNDDATABYID", prevtndid,
                      "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            ViewState["tblt01"] = ds2.Tables[0];
            this.Data_Bind();
            this.divResource.Visible = true;

        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string prjcode = this.ddlProject.SelectedValue.ToString();


            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETTNDDATABYID", prjcode,
                      "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvCivilBoq.DataSource = null;
                this.gvCivilBoq.DataBind();
                return;
            }
            else
            {
                ViewState["tblt01"] = HiddenSameData(ds2.Tables[0]);
                this.txtSbtRate_Per.Text = ds2.Tables[1].Rows.Count > 0 ? Convert.ToDouble(ds2.Tables[1].Rows[0]["profitper"]).ToString("#,##0.00;(#,##0.00); ") : this.txtSbtRate_Per.Text;
                this.txtACCost_Per.Text = ds2.Tables[1].Rows.Count > 0 ? Convert.ToDouble(ds2.Tables[1].Rows[0]["ohper"]).ToString("#,##0.00;(#,##0.00); ") : this.txtACCost_Per.Text;
                this.txtACCostVatOH_Per.Text = ds2.Tables[1].Rows.Count > 0 ? Convert.ToDouble(ds2.Tables[1].Rows[0]["taxvatper"]).ToString("#,##0.00;(#,##0.00); ") : this.txtACCostVatOH_Per.Text;

                this.Data_Bind();
            }


        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)ViewState["tblt01"];
            var list = dt.DataTableToList<RealEntity.C_07_Ten.RptCivilConBOQ>();

            string projectName = ddlProject.SelectedItem.Text;
            string profitRate = txtSbtRate_Per.Text;
            string overHead = txtACCost_Per.Text;
            string vatTax = txtACCostVatOH_Per.Text;
            string printType = txtPrintId.SelectedValue;
            LocalReport Rpt1 = new LocalReport();
            if (printType == "management")
            {


                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_07_Ten.RptCivilConBOQ", list, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_07_Ten.RptCivilConBOQTender", list, null, null);
                Rpt1.EnableExternalImages = true;
            }
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Civil Construction BOQ"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}