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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_16_Bill
{
    public partial class BillingRateEntry02 : System.Web.UI.Page
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

                this.ImgbtnFindProject_OnClick(null, null);
                this.GetItemCode();


                ((Label)this.Master.FindControl("lblTitle")).Text = "Budget-Sales (Const.)";
                Session.Remove("tblBill");
                if (this.Request.QueryString["prjcode"].ToString().Length > 0)
                {
                    this.lbtnOk1_Click(null, null);

                }
            }

        }


        private void GetItemCode()
        {

            ViewState.Remove("tblItem");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETITEMCODE", "", "", "", "", "", "", "", "", "");

            ViewState["tblItem"] = ds1.Tables[0];

            this.ddlGroup.DataTextField = "itemdesc";
            this.ddlGroup.DataValueField = "itemcode";
            this.ddlGroup.DataSource = ds1.Tables[1];
            this.ddlGroup.DataBind();
            this.GetItemSelect();


        }

        private void GetItemSelect()
        {
            DataTable dt = ((DataTable)ViewState["tblItem"]).Copy();
            string grpcode = this.ddlGroup.SelectedValue.ToString().Substring(0, 9);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode like '" + grpcode + "%'");

            this.ddlitemlist.DataTextField = "itemdesc";
            this.ddlitemlist.DataValueField = "itemcode";
            this.ddlitemlist.DataSource = dv.ToTable();
            this.ddlitemlist.DataBind();

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }


        protected void lbtnOk1_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk1.Text == "New")
            {
                Session.Remove("tblBill");
                this.lbtnOk1.Text = "Ok";
                this.ddlProject.Visible = true;
                this.lblProjectDesc.Visible = false;

                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();

                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lblpage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.pnlItem.Visible = false;

                return;
            }

            this.lbtnOk1.Text = "New";
            this.ddlProject.Visible = false;
            this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblProjectDesc.Visible = true;
            this.lblpage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.pnlItem.Visible = true;
            this.ShowProWSalRate();
        }


        private void ShowProWSalRate()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = new DataSet();

            string PrjCod = this.ddlProject.SelectedValue.ToString().Trim();

            ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT", "GETWORKBILLRATE02", PrjCod, "", "", "", "", "", "", "", "");
            Session["tblBill"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string misircode = dt1.Rows[0]["misircode"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    misircode = dr1["misircode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["misircode"].ToString() == misircode)
                {

                    dr1["misirdesc"] = "";

                }


                misircode = dr1["misircode"].ToString();
            }



            return dt1;

        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string Project = this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string mBILLNo = this.ddlPrevList.SelectedValue.ToString();
            //string mbillno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string PrjCod = this.ddlProject.SelectedItem.Text.Trim().Substring(13);
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            // DataTable dt = (DataTable)Session["tblBill"];

            DataTable dt = (DataTable)Session["tblBill"];
            var lst = dt.DataTableToList<RealEntity.C_16_Bill.BO_BillEntry.BillingRateEntry>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptBillRateEntry", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Project", "Project/Unit: " + PrjCod));
            Rpt1.SetParameters(new ReportParameter("rptname", "SALES BUDGET"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void Data_Bind()
        {
            DataTable TptTable = (DataTable)Session["tblBill"];

            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = TptTable;
            this.gvRptResBasis.DataBind();
            this.FooterCalCulation();
        }

        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)Session["tblBill"];
            double ordam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordam)", "")) ? 0.00 : dt.Compute("Sum(ordam)", "")));



            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lblgvFordam")).Text = ordam.ToString("#,##0.00;(#,##0.00); ");



        }

        protected void ImgbtnFindProject_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchTxt = this.Request.QueryString["prjcode"].ToString().Length > 0 ? (this.Request.QueryString["prjcode"].ToString() + "%") : "%%";
            string calltype;
            if (this.Request.QueryString["Type"] == "Entry")
            {
                calltype = "PRJCODELIST02";
            }
            else
            {
                calltype = "TASPRJLIST";

            }

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", calltype, srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "prjdesc1";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }



        private void SaveValue()
        {
            DataTable dt1 = (DataTable)Session["tblBill"];
            int TblRowIndex;
            double peronbgd, ordam, bgdam;
            for (int i = 0; i < gvRptResBasis.Rows.Count; i++)
            {
                TblRowIndex = (gvRptResBasis.PageIndex) * gvRptResBasis.PageSize + i;
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtbgdqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtbillrate")).Text.Trim());
                double percnt = Convert.ToDouble("0" + ((TextBox)this.gvRptResBasis.Rows[i].FindControl("txtgvpercnt")).Text.Trim());

                ordam = qty * rate;
                dt1.Rows[TblRowIndex]["bgdqty"] = qty;
                dt1.Rows[TblRowIndex]["rate"] = rate;
                dt1.Rows[TblRowIndex]["ordam"] = ordam;
                dt1.Rows[TblRowIndex]["percnt"] = percnt;


            }
            Session["tblBill"] = dt1;
            this.Data_Bind();
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }


        protected void lnkfinalup_Click(object sender, EventArgs e)
        {

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.SaveValue();
            DataTable dt1 = (DataTable)Session["tblBill"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            dt1.Columns.Remove("isirdesc");
            dt1.Columns.Remove("sdetails");
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";

            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProject.SelectedValue.ToString();

            bool result = ImpleData.UpdateXmlTransInfo(comcod, "SP_ENTRY_BILLMGT", "INSERTORUPDATETWRATE", ds1, null, null,
                   pactcode, "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ImpleData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }





             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }



        protected void gvRptResBasis_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;


                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 1;


                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;

                TableCell cell07 = new TableCell();
                cell07.Text = "Quotation";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 2;
                cell07.Font.Bold = true;

                TableCell cell08 = new TableCell();
                cell08.Text = "";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvRptResBasis.Controls[0].Controls.AddAt(0, gvrow);
            }

        }




        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //Hashtable hst = (Hashtable)Session["tblLogin"];
        //string comnam = hst["comnam"].ToString();
        //string comadd = hst["comadd1"].ToString();
        //string compname = hst["compname"].ToString();
        //string username = hst["username"].ToString();
        //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");        
        //RptImplementPlan rptstk = new RptImplementPlan();
        //DataTable dt1 = new DataTable();
        //dt1 = (DataTable)Session["tblImplemt"];
        ////DataTable dt2 = new DataTable();
        ////dt2 = (DataTable)Session["tblImplemtn"];
        //DataView dv1 = dt1.DefaultView;
        //dv1.RowFilter = "qty>0";
        //rptstk.SetDataSource(dv1);
        // TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //txtCompanyName.Text = comnam;
        //TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
        //txtCompanyAddress.Text = comadd;
        //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
        //txtprojectname.Text =this.ddlProject.SelectedItem.Text.Substring(14);
        //TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //rpttxtDate.Text ="Date: " +Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
        //TextObject rpttxtVou = rptstk.ReportDefinition.ReportObjects["txtVou"] as TextObject;
        //rpttxtVou.Text ="Voucher: "+ this.lblCurVOUNo1.Text.Trim()+this.txtCurVOUNo2.Text.Trim();
        //Session["Report1"] = rptstk;
        //this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";
        //dv1.RowFilter = "";
        //}

        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lnktotal_Click(object sender, EventArgs e)
        {


            this.SaveValue();

        }


        protected void lbtnWork_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblBill"];
            string misircode = this.ddlGroup.SelectedValue.ToString();
            string misirdesc = this.ddlGroup.SelectedItem.Text;
            string isircode = this.ddlitemlist.SelectedValue.ToString();
            string isirdesc = this.ddlitemlist.SelectedItem.Text;
            string flrcod = "000";
            DataRow[] dr1 = dt.Select("isircode='" + isircode + "'");
            if (dr1.Length == 0)
            {
                DataRow dr2 = dt.NewRow();

                dr2["misircode"] = misircode;
                dr2["misirdesc"] = misirdesc;
                dr2["isircode"] = isircode;
                dr2["isirdesc"] = isirdesc;
                dr2["unit"] = ((DataTable)ViewState["tblItem"]).Select("itemcode='" + isircode + "'")[0]["itemunit"].ToString(); ;
                dr2["flrcod"] = flrcod;
                dr2["bgdqty"] = 0.00;
                dr2["rate"] = 0.00;
                dr2["ordam"] = 0.00;
                dr2["percnt"] = 0.00;
                dr2["sdetails"] = ((DataTable)ViewState["tblItem"]).Select("itemcode='" + isircode + "'")[0]["sdetails"].ToString();
                dt.Rows.Add(dr2);

            }







            Session["tblBill"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }
        protected void lbtnAllWork_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblBill"];
            string flrcod = "000";
            string isircode = "";
            string isirdesc = "";
            double qty, rate = 0;
            string misircode = this.ddlGroup.SelectedValue.ToString();
            string misirdesc = this.ddlGroup.SelectedItem.Text;

            foreach (ListItem litemn in ddlitemlist.Items)
            {

                DataRow dr1 = dt.NewRow();

                isircode = litemn.Value;
                isirdesc = litemn.Text;
                rate = Convert.ToDouble(((DataTable)ViewState["tblItem"]).Select("itemcode='" + isircode + "'")[0]["rate"].ToString());
                qty = 0.00;
                dr1["misircode"] = misircode;
                dr1["misirdesc"] = misirdesc;
                dr1["isircode"] = isircode;
                dr1["isirdesc"] = isirdesc;
                dr1["unit"] = ((DataTable)ViewState["tblItem"]).Select("itemcode='" + isircode + "'")[0]["itemunit"].ToString(); ;
                dr1["flrcod"] = flrcod;
                dr1["bgdqty"] = qty;
                dr1["rate"] = rate;
                dr1["percnt"] = 0.00;
                dr1["ordam"] = qty * rate;
                dr1["sdetails"] = ((DataTable)ViewState["tblItem"]).Select("itemcode='" + isircode + "'")[0]["sdetails"].ToString();
                dt.Rows.Add(dr1);

            }

            Session["tblBill"] = this.HiddenSameData(dt);
            this.Data_Bind();






        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetItemSelect();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int row = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            DataTable dt = (DataTable)Session["tblBill"];
            int rowindex = (this.gvRptResBasis.PageSize) * (this.gvRptResBasis.PageIndex) + row;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            Session.Remove("tblBill");
            Session["tblBill"] = dv.ToTable();
            this.Data_Bind();

        }
    }
}
