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
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class MktGrandNoteSheet : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        Common objcom = new Common();

        public static double addamt = 0.00, dedamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string TypeDesc = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Grand Note Sheet";

                Session.Remove("Unit");
               string date= System.DateTime.Today.ToString("dd-MMM-yyyy");
                string finsdate = Convert.ToDateTime(date).AddMonths(1).ToString("dd-MMM-yyyy");
                this.txtBookingdate.Text = date;
                this.txtfirstinsdate.Text = finsdate;
                this.txtcoffBookingdate.Text = date;
                this.txtcoffinsdate.Text = date;
                this.txtrevpBookingdate.Text = date;
                this.txtrevpinsdate.Text = date;
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES WITH PAYMENT  INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.gvSpayment.Columns[0].Visible = false;

               



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
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";            
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                MultiView1.ActiveViewIndex = -1;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.lbtnBack.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

        }

        private void LoadGrid()
        {
            ViewState.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string srchunit = "%" + this.txtsrchunit.Text.Trim() + "%";
            // string musircode = "51";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALESNOTESHEET", "DETAILSIRINFINFORMATION", PactCode, srchunit, "", "", "", "", "", "", "");
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SIRINFINFORMATION", PactCode, srchunit, musircode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
          
        }



        protected void lbtnusize_Click(object sender, EventArgs e)
        {

            this.lbtnBack.Visible = true;

            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            Session.Remove("UsirBasicInformation");

            DataTable dtOrder = (DataTable)ViewState["tblData"];
            DataView dv1 = dtOrder.DefaultView;
            dv1.RowFilter = "usircode like('" + usircode + "')";
            dtOrder = dv1.ToTable();


            this.MultiView1.ActiveViewIndex = 0;
            Session["UsirBasicInformation"] = dtOrder;
            this.gvSpayment.DataSource = dtOrder;
            this.gvSpayment.DataBind();
            this.lblCode.Text = usircode;

            // this.lblAcAmt.Text = Convert.ToDouble(dtOrder.Rows[0]["tamt"]).ToString("#,##0;(#,##0); ");
            this.gvSpayment.Columns[5].Visible = true;
            this.gvSpayment.Columns[6].Visible = true;
            this.ShowData();

            // this.Data_SumBind();
        }

        private void ShowData()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usircode = this.lblCode.Text;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usrid = hst["usrid"].ToString();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "SHOWDUMMYSCHDULEUSERWISE", pactcode, usircode, usrid, date, "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            var lst1 = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            var lst2 = ds1.Tables[6].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            var lst = lst1.Count == 0 ? lst2 : lst1;
            Session["tbldschamt"] = lst;
            Session["tblacamt"] = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>();
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            Session["tblshusel"] = ds1.Tables[2];
            Session["tblactive"] = ds1.Tables[3];
            Session["tblact01"] = ds1.Tables[4];
           // Session["tblintcol"] = ds1.Tables[5];
            Session["tblintcol01"] = ds1.Tables[5];

        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string gcod = dt1.Rows[0]["gcod"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    gcod = dr1["gcod"].ToString();
                    i++;
                    continue;
                }

                if (dr1["gcod"].ToString() == gcod)
                {

                    dr1["gdesc"] = "";
                    dr1["cinsam"] = 0.00;

                }


                gcod = dr1["gcod"].ToString();
            }



            return dt1;

        }



        private void Data_Bind()
        {

            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            //this..DataSource = lstd;
            //this.gvdumpay.DataBind();

            //this.gvacpay.DataSource = lsta;
            //this.gvacpay.DataBind();

            this.FooterCalculation();







        }


        private void FooterCal(DataTable dt)
        {


            //if (dt.Rows.Count > 0)
            //{
            //    Session["Report1"] = gvInterest;
            //    ((HyperLink)this.gvInterest.FooterRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //}

            //if (dt.Rows.Count == 0)
            //    return;


            //((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
            //             0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;-#,##0;");
            //((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
            //                      0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;-#,##0;");
            //((Label)this.gvInterest.FooterRow.FindControl("lgvFdelordis")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(delodis)", "")) ?
            //                          0 : dt.Compute("sum(delodis)", ""))).ToString("#,##0;-#,##0;");


        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            this.LoadGrid();



        }

        private void CustInf()
        {
            Session.Remove("tblcost");
            Session.Remove("tblPay");
            Session.Remove("tpripay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UsirCode = this.lblCode.Text;
            string empid = hst["empid"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALPERSONALINFO", PactCode, UsirCode, "", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[6];
            Session["tpripay"] = ds1.Tables[4];
            Session["tblcost"] = ds1.Tables[1];
        }
        private void Calculation()
        {

            DataTable dtcost = (DataTable)Session["tblcost"];
            DataTable dtpay = (DataTable)Session["tblPay"];
        }

        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

      
        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {



            //DataTable dt = (DataTable)Session["tblPay"];
            //int i, k = 0;
            ////  List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = new List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>();
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            ////Booking and downpayment

            //double bandpamt = 0;
            //string gcod, gdesc; DateTime schDate; DateTime AccBookDate;
            //foreach (GridViewRow gvr1 in gvdumpay.Rows)
            //{
            //    gcod = ((Label)gvr1.FindControl("lblgvgcod")).Text.Trim();
            //    schDate = Convert.ToDateTime(((TextBox)gvr1.FindControl("txtgvScheduledate")).Text.Trim());
            //    double Amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gvr1.FindControl("txtgvdumschamt")).Text.Trim()));
            //    //  Amount = (Amount>0)?Amount:0;
            //    bandpamt += Amount;

            //    if (ASTUtility.Left(gcod, 5) == "81985")
            //    {
            //        var lsts = lstd.FindAll(l => l.gcod == gcod);
            //        if (lsts.Count() > 0)
            //        {


            //            lsts[0].schdate = schDate;
            //            lsts[0].schamt = Amount;
            //        }
            //    }
            //    else
            //    {

            //        lstd[k].schdate = schDate;
            //        lstd[k].schamt = Amount;
            //        k++;

            //    }



            //}




            //double Tamt = Convert.ToDouble("0" + this.txttoamt.Text);
            //double ramt = Tamt - bandpamt;
            //int tin = Convert.ToInt16("0" + this.txtnofins.Text);
            //int dur = Convert.ToInt16(this.ddlMonth.SelectedValue.ToString());
            //double insamt = ramt / tin;
            //// string schDate1 = Convert.ToDateTime(this.txtfinsdate.Text).ToString("dd-MMM-yyyy");
            //DateTime insdate1 = Convert.ToDateTime(this.txtfinsdate.Text);
            //DateTime insdate2;
            //for (int j = k; j < tin + k; j++)
            //{
            //    insdate2 = (j == k) ? insdate1 : Convert.ToDateTime(insdate1).AddMonths(dur);
            //    double schamt = insamt;
            //    lstd[j].schdate = insdate2;
            //    lstd[j].schamt = schamt;
            //    insdate1 = insdate2;
            //}


            //double acbookamt = Convert.ToDouble("0" + this.txtacbooking.Text);
            //var lstbdate = lstd.FindAll(l => l.schamt > 0);
            //AccBookDate = lstbdate[0].schdate;
            //int atin = Convert.ToInt32("0" + this.txtacinstallment.Text);
            //DateTime ainsdate = Convert.ToDateTime(this.txtfinsdate.Text);
            ////lstd.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(insdate, schinsamt));
            //lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(AccBookDate, acbookamt));
            //ramt = Tamt - acbookamt;
            //double acinsamt = ramt / atin;

            //for (i = 0; i < atin; i++)
            //{

            //    lsta.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(ainsdate, acinsamt));
            //    ainsdate = ainsdate.AddMonths(dur);
            //}


            //Session["tbldschamt"] = lstd.FindAll(l => l.schamt > 0);
            //Session["tblacamt"] = lsta;
            //this.Data_Bind();
            //this.chkVisible.Checked = false;
            //this.chkVisible_CheckedChanged(null, null);
        }






        private void FooterCalculation()
        {
            try
            {
                List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                
            }

            catch (Exception e)
            {



            }



        }

        private void FooterCalculationsum()
        {
            try
            {
                List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            }

            catch (Exception e)
            {



            }



        }


        protected void lbtnTotaldumsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];

            //for (int i = 0; i < this.gvdumpay.Rows.Count; i++)
            //{
            //    DateTime schdate = Convert.ToDateTime(((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvScheduledate")).Text.Trim());
            //    double schamt = Convert.ToDouble("0" + ((TextBox)this.gvdumpay.Rows[i].FindControl("txtgvdumschamt")).Text.Trim());

            //    lst[i].schdate = schdate;
            //    lst[i].schamt = schamt;
            //}
            //Session["tbldschamt"] = lst;
            //double Amount = lst.Sum(l => l.schamt);
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            //((Label)this.gvdumpay.FooterRow.FindControl("lgvFdumschamt")).Text = Amount.ToString("#,##0;(#,##0); ");




            //this.Data_Bind();

        }
        protected void lbtnTotalacsch_Click(object sender, EventArgs e)
        {
            //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

            //for (int i = 0; i < this.gvacpay.Rows.Count; i++)
            //{
            //    DateTime schdate = Convert.ToDateTime(((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacScheduledate")).Text.Trim());
            //    double schamt = Convert.ToDouble("0" + ((TextBox)this.gvacpay.Rows[i].FindControl("txtgvacschamt")).Text.Trim());

            //    lst[i].schdate = schdate;
            //    lst[i].schamt = schamt;
            //}
            //Session["tblacamt"] = lst;
            //this.Data_Bind();


        }
        protected void lbtnAdddsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            int lrow = lst.Count;
            string gcod = "";
            string gdesc = "";

            DateTime insdate = lst[lrow - 1].schdate;
            double schinsamt = lst[lrow - 1].schamt;
            lst.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule(gcod, gdesc, insdate, schinsamt));
            this.Data_Bind();
        }

        protected void lbtnDeldsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["tbldschamt"] = lst;
            this.Data_Bind();

        }

        protected void lbtnAddacsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            int lrow = lst.Count;
            DateTime insdate = lst[lrow - 1].schdate;
            double schinsamt = lst[lrow - 1].schamt;
            lst.Add(new RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule(insdate, schinsamt));
            this.Data_Bind();
        }

        protected void lbtnDelacsch_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];
            int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            lst.RemoveAt(rowindex);
            Session["tblacamt"] = lst;
            this.Data_Bind();


        }




        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //string pactcode = this.ddlProjectName.SelectedValue.ToString();

                //string usircode = this.lblCode.Text.Trim();
              

              
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = this.GetCompCode();
                //string usrid = hst["usrid"].ToString();
                //string Postusrid = hst["usrid"].ToString();
                //string trmnid = hst["compname"].ToString();
                //string session = hst["session"].ToString();
                //string PostedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                //string entryben = Convert.ToDouble("0" + this.txtentryben.Text.Trim()).ToString();
                //string delaychrg = Convert.ToDouble("0" + this.txtdelaychrg.Text.Trim()).ToString();

                //string discount = Convert.ToDouble("0" + this.txtdiscount.Text).ToString() ;
                //string Parking = Convert.ToDouble("0" + this.txtParking.Text).ToString();


                //DataSet ds1 = new DataSet("ds1");
                ////Table Schdule
                //DataTable dt1 = new DataTable();
                //dt1.Columns.Add("gcod", typeof(string));
                //dt1.Columns.Add("gdesc", typeof(string));
                //dt1.Columns.Add("schamt", typeof(Double));
                //dt1.Columns.Add("schdate", typeof(DateTime));

                //// Table Actual
                //DataTable dt2 = new DataTable();
                //dt2.Columns.Add("schamt", typeof(Double));
                //dt2.Columns.Add("schdate", typeof(DateTime));



                //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lstd = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
                //List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule> lsta = (List<RealEntity.C_22_Sal.EClassSales_02.EClassAcPaSchdule>)Session["tblacamt"];

                //dt1 = ASITUtility03.ListToDataTable(lstd);
                //dt2 = ASITUtility03.ListToDataTable(lsta);

               


                //ds1.Merge(dt1);
                //ds1.Merge(dt2);
                ////ds1.Merge(dt3);
                ////ds1.Merge(dt3);
                ////ds1.Merge(dt4);
                ////ds1.Merge(dt5);
                //ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";

                ////string xml = ds1.GetXml();
                ////return;
                //bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "UPDATEDUMMYPAYMENTUSERWISE", ds1, null, null, pactcode, usircode,usrid, discount, Parking, entryben, delaychrg, Postusrid, trmnid, session, PostedDate);

                //if (!resulta)
                //{

                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + MktData.ErrorObject["Mesage"].ToString();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;

                //}



                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated SUccessfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //this.ShowData();


            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
     
       
      
      

      
        protected void lbtnAddInstallment_Click(object sender, EventArgs e)
        {
            

        }

        protected void lnkbtnFindProject_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnDelacshall_Click(object sender, EventArgs e)
        {



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string txtsrchisn = this.txtsrchInstallment.Text.Trim() + "%";
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETINSTALLMENT", txtsrchisn, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.ddlInstallment.Items.Clear();
            //    return;

            //}

            //List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassDumPaSchdule>)Session["tbldschamt"];
            //int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //lst.RemoveAt(rowindex);
            //Session["tbldschamt"] = lst;
            //this.Data_Bind();
        }
    }
}

