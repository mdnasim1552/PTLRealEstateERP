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
namespace RealERPWEB.F_03_Fin
{
    public partial class EntryYearlySalAndColl : System.Web.UI.Page
    {
        ProcessAccess SaleData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void GetPreviousList()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtfromdate.Text.Trim();
            DataSet ds1 = SaleData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "GETPREBGDLIST", CurDate1,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "bgdnum1";
            this.ddlPrevList.DataValueField = "bgdnum";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
            ViewState["tblbgdnum"] = ds1.Tables[0];
            ds1.Dispose();




        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            this.GetPreviousList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.txtfromdate.Enabled = false;
                this.txttodate.Enabled = false;

                this.lblPrevList.Visible = false;
                this.txtSrcPrelist.Visible = false;
                this.ibtnPreList.Visible = false;
                this.ddlPrevList.Visible = false;
                this.ShowSalesACollTar();
                return;


            }

            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.ddlPrevList.Items.Clear();
            this.txtfromdate.Enabled = true;
            this.txttodate.Enabled = true;
            this.lblPrevList.Visible = true;
            this.txtSrcPrelist.Visible = true;
            this.ibtnPreList.Visible = true;
            this.ddlPrevList.Visible = true;
            this.gvSalAndColl.DataSource = null;
            this.gvSalAndColl.DataBind();




        }


        protected void GetMBGD()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWMBD";
            if (this.ddlPrevList.Items.Count > 0)
                mREQNO = this.ddlPrevList.SelectedValue.ToString();

            string mREQDAT = this.txtfromdate.Text.Trim();
            if (mREQNO == "NEWMBD")
            {
                DataSet ds2 = SaleData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "LASTMBGDNO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurINo1.Text = ds2.Tables[0].Rows[0]["maxmbdno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxmbdno1"].ToString().Substring(6, 5);

                    this.ddlPrevList.DataTextField = "maxmbdno1";
                    this.ddlPrevList.DataValueField = "maxmbdno1";
                    this.ddlPrevList.DataSource = ds2.Tables[0];
                    this.ddlPrevList.DataBind();
                }
            }

        }


        private void GetLastMBGD()
        {


        }

        private void ShowSalesACollTar()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;
            }



            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = new DataSet();
            if (this.ddlPrevList.Items.Count > 0)
            {
                string bgdnum = this.ddlPrevList.SelectedValue.ToString();
                string minmon = (((DataTable)ViewState["tblbgdnum"]).Select("bgdnum='" + bgdnum + "'"))[0]["minmon"].ToString();
                string bgdnum1 = (((DataTable)ViewState["tblbgdnum"]).Select("bgdnum='" + bgdnum + "'"))[0]["bgdnum1"].ToString();
                this.txtfromdate.Text = Convert.ToDateTime(minmon.Substring(4, 2) + "-01-" + minmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.lblCurINo1.Text = bgdnum1.Substring(0, 6);
                this.lblCurNo2.Text = bgdnum1.Substring(6, 5);
                ds1 = SaleData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "GETPREEXSALCOLL", bgdnum, this.txtfromdate.Text, this.txttodate.Text, "", "", "", "", "", "");

            }


            else ds1 = SaleData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "RPTEXSALCOLL", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalAndColl.DataSource = null;
                this.gvSalAndColl.DataBind();
                return;
            }


            ViewState["tblysalacoll"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;


            string grp = dt1.Rows[0]["grp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";

                grp = dt1.Rows[j]["grp"].ToString();
            }

            return dt1;

        }





        private void Data_Bind()
        {

            DateTime datefrm, dateto;
            datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 5; i < 17; i++)
            {
                if (datefrm > dateto)
                    break;
                this.gvSalAndColl.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            this.gvSalAndColl.DataSource = (DataTable)ViewState["tblysalacoll"];
            this.gvSalAndColl.DataBind();

        }







        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblysalacoll"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_03_Fin.RptExpYSalAColl();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";
            TextObject txtmbno = rptstk.ReportDefinition.ReportObjects["txtmbno"] as TextObject;
            txtmbno.Text = "No: " + this.lblCurINo1.Text.Trim() + this.lblCurNo2.Text.Trim();


            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblysalacoll"];
            for (int i = 0; i < this.gvSalAndColl.Rows.Count; i++)
            {
                dt.Rows[i]["amt1"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt1")).Text.Trim()).ToString();
                dt.Rows[i]["amt2"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt2")).Text.Trim()).ToString();
                dt.Rows[i]["amt3"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt3")).Text.Trim()).ToString();
                dt.Rows[i]["amt4"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt4")).Text.Trim()).ToString();

                dt.Rows[i]["amt5"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt5")).Text.Trim()).ToString();
                dt.Rows[i]["amt6"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt6")).Text.Trim()).ToString();
                dt.Rows[i]["amt7"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt7")).Text.Trim()).ToString();
                dt.Rows[i]["amt8"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt8")).Text.Trim()).ToString();

                dt.Rows[i]["amt9"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt9")).Text.Trim()).ToString();
                dt.Rows[i]["amt10"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt10")).Text.Trim()).ToString();
                dt.Rows[i]["amt11"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt11")).Text.Trim()).ToString();
                dt.Rows[i]["amt12"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt12")).Text.Trim()).ToString();

                dt.Rows[i]["toamt"] = Convert.ToDouble(dt.Rows[i]["amt1"]) + Convert.ToDouble(dt.Rows[i]["amt2"]) + Convert.ToDouble(dt.Rows[i]["amt3"])
                       + Convert.ToDouble(dt.Rows[i]["amt4"]) + Convert.ToDouble(dt.Rows[i]["amt5"]) + Convert.ToDouble(dt.Rows[i]["amt6"]) + Convert.ToDouble(dt.Rows[i]["amt7"])
                       + Convert.ToDouble(dt.Rows[i]["amt8"]) + Convert.ToDouble(dt.Rows[i]["amt9"]) + Convert.ToDouble(dt.Rows[i]["amt10"]) + Convert.ToDouble(dt.Rows[i]["amt11"])
                       + Convert.ToDouble(dt.Rows[i]["amt12"]);
            }

            ViewState["tblysalacoll"] = dt;
            this.Data_Bind();


        }


        protected void gvSalAndColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvCodeDesc = (Label)e.Row.FindControl("lgvCodeDesc");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamt");
                TextBox amt1 = (TextBox)e.Row.FindControl("txtgvamt1");
                TextBox amt2 = (TextBox)e.Row.FindControl("txtgvamt2");
                TextBox amt3 = (TextBox)e.Row.FindControl("txtgvamt3");
                TextBox amt4 = (TextBox)e.Row.FindControl("txtgvamt4");
                TextBox amt5 = (TextBox)e.Row.FindControl("txtgvamt5");
                TextBox amt6 = (TextBox)e.Row.FindControl("txtgvamt6");
                TextBox amt7 = (TextBox)e.Row.FindControl("txtgvamt7");
                TextBox amt8 = (TextBox)e.Row.FindControl("txtgvamt8");
                TextBox amt9 = (TextBox)e.Row.FindControl("txtgvamt9");
                TextBox amt10 = (TextBox)e.Row.FindControl("txtgvamt10");
                TextBox amt11 = (TextBox)e.Row.FindControl("txtgvamt11");
                TextBox amt12 = (TextBox)e.Row.FindControl("txtgvamt12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {

                    lgvCodeDesc.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    amt1.Font.Bold = true;
                    amt2.Font.Bold = true;
                    amt3.Font.Bold = true;
                    amt4.Font.Bold = true;
                    amt5.Font.Bold = true;
                    amt6.Font.Bold = true;
                    amt7.Font.Bold = true;
                    amt8.Font.Bold = true;
                    amt9.Font.Bold = true;
                    amt10.Font.Bold = true;
                    amt11.Font.Bold = true;
                    amt12.Font.Bold = true;
                    lgvCodeDesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void lbtnCalCulation_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblysalacoll"];
            int j = 0;
            for (int i = 0; i < this.gvSalAndColl.Rows.Count; i++)
            {

                double toamt = Convert.ToDouble("0" + ((Label)this.gvSalAndColl.Rows[i].FindControl("lgvtoamt")).Text.Trim());
                string unit = ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtunit")).Text.Trim();
                double assumtion = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtassumtion")).Text.Trim());
                dt.Rows[i]["unval"] = assumtion;
                dt.Rows[i]["unit"] = unit;
                if (unit == "%")
                    for (j = 0; j < 12; j++)
                        dt.Rows[i]["amt" + (j + 1).ToString()] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt" + (j + 1).ToString())).Text.Trim()) * 0.01 * assumtion;




                else if (unit == "")
                    ;
                else
                    for (j = 0; j < assumtion; j++)
                        dt.Rows[i]["amt" + (j + 1).ToString()] = toamt / assumtion;
            }

            ViewState["tblysalacoll"] = dt;
            this.Data_Bind();
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblysalacoll"];
                //DataRow[] dr = new DataRow();
                if (this.ddlPrevList.Items.Count == 0)
                    this.GetMBGD();
                bool result = true;
                string mBgdnum = this.lblCurINo1.Text.Trim().Substring(0, 3) + this.txtfromdate.Text.Trim().Substring(7, 4) + this.lblCurINo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();


                string date = this.txtfromdate.Text.Trim();
                DateTime datefrm, dateto;
                datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                dateto = Convert.ToDateTime(this.txttodate.Text.Trim());



                for (int i = 0; i < 12; i++)
                {
                    if (datefrm > dateto)
                        break;

                    string yearmon = datefrm.ToString("yyyyMM");


                    foreach (DataRow datarow in dt.Rows)
                    {


                        string grp = datarow["grp"].ToString();
                        string code = datarow["code"].ToString();
                        if (code.Substring(2) == "AAA")
                            continue;

                        string unval = datarow["unval"].ToString();
                        string unit = datarow["unit"].ToString();
                        string amt = datarow["amt" + (i + 1).ToString()].ToString();
                        result = SaleData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT04", "INSORUPYCSACOLLTAR", mBgdnum, grp, code, yearmon, date, unval, unit, amt, "", "", "", "", "", "", "");


                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                            return;
                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        }

                        //   }

                    }
                    datefrm = datefrm.AddMonths(1);

                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }
        }


    }
}
