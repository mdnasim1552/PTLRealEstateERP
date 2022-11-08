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
namespace RealERPWEB.F_12_Inv
{
    public partial class LinkMktSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Market Servey";
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                if (this.Request.QueryString.AllKeys.Contains("msrno") && (this.Request.QueryString["reqno"].Substring(0, 3) == "REQ"))
                {
                    this.ImgbtnFindPreMR_Click(null,null);
                    this.ddlPrevMSRList.SelectedValue = this.Request.QueryString["msrno"].ToString();
                    this.lbtnOk_Click(null, null);
                    this.lbtnMSRUpdate.Visible = false;
                }

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string premrlist = "%" + this.txtPreMSRSearch.Text + "%";
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            string calltype = (this.Request.QueryString["reqno"].Length > 14) ? "GETPREMSR" : (this.Request.QueryString["reqno"].Substring(0, 3) == "LRQ") ? "GETPREMSR" : "GETPREVMSRLIST";

            string msrType = this.getMsrType();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", calltype, CurDate1, premrlist, msrType, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevMSRList.Items.Clear();
            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }

        private string getMsrType()
        {
            string msrType = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "1205":
                case "3351":
                case "3352":
                case "3364": //JBS
                case "3370": //cpdl
                    msrType = "MSR02";
                    break;

                default:
                    msrType = "";
                    break;
            }
            return msrType;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            switch (this.GetCompCode())
            {
                case "1205":
                case "3351":
                case "3352":
                case "3101":
                case "3353"://Manama
                case "3364": //JBS
                case "3370": //cpdl
                    this.Multiview1.ActiveViewIndex = 1;
                    this.Get_Survey_Info();
                    break;
                default:
                    this.Multiview1.ActiveViewIndex = 0;
                    this.ShowSurvey();
                    break;
            }

        }


        protected void ShowSurvey()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            string mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblMSR"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            this.txtMSRNarr.Text = ds1.Tables[1].Rows[0]["msrnar"].ToString();

        }


        protected void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurMSRDate.Text.Trim();
            // string date = Convert.ToDateTime(this.GetStdDate(this.txtCurMSRDate.Text.Trim())).ToString("dd-MMM-yyyy");
            string mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO1", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblt01"] = ds1.Tables[1];
            Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);
            Session["tblterm"] = ds1.Tables[3];


            this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");

            this.txtMSRNarr.Text = ds1.Tables[4].Rows[0]["remarks"].ToString();
            this.Data_Bind();

            //  this.Payterm_DataBind();
        }

        protected void Data_Bind()
        {
            switch (this.GetCompCode())
            {
                case "1205":
                case "3351":
                case "3352":
                case "3101":
                case "3353"://Manama
                case "3364": //JBS
                case "3370": //cpdl

                    this.gvMSRInfo2.DataSource = (DataTable)Session["tblt02"];
                    this.gvMSRInfo2.DataBind();

                    Payterm_DataBind();
                    break;
                default:
                    this.gvMSRInfo.DataSource = (DataTable)Session["tblMSR"];
                    this.gvMSRInfo.DataBind();
                    break;
            }
        }
        private void Payterm_DataBind()
        {
            this.gvterm.DataSource = (DataTable)Session["tblterm"];
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3101":
                case "3368":
                    this.gvterm.Columns[4].Visible = false;
                    this.gvterm.Columns[5].Visible = false;
                    this.gvterm.Columns[7].Visible = true;
                    this.gvterm.Columns[9].Visible = true;
                    this.gvterm.Columns[10].Visible = true;
                    this.gvterm.Columns[11].Visible = true;
                    break;

                case "3370": //cpdl
                    this.gvterm.Columns[4].Visible = true;
                    this.gvterm.Columns[7].Visible = true;
                    this.gvterm.Columns[9].Visible = true;
                    this.gvterm.Columns[10].Visible = true;
                    this.gvterm.Columns[11].Visible = true;

                    this.gvterm.Columns[5].Visible = false;
                    //this.gvterm.Columns[6].Visible = false;


                    //testGV.HeaderRow.Cells[0].Text = "Date"
                    this.gvterm.Columns[7].HeaderText = "Brand"; // goodwill as brand
                    this.gvterm.Columns[8].HeaderText = "Credit Period";
                    this.gvterm.Columns[11].HeaderText = "VAT & AIT";
                    break;

                default:
                    this.gvterm.Columns[4].Visible = true;
                    this.gvterm.Columns[5].Visible = true;
                    this.gvterm.Columns[7].Visible = false;
                    this.gvterm.Columns[9].Visible = false;
                    this.gvterm.Columns[10].Visible = false;
                    this.gvterm.Columns[11].Visible = false;
                    break;
            }

            this.gvterm.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescod = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                {
                    rescod = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                rescod = dt1.Rows[j]["rsircode"].ToString();
            }
            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                //string date = Convert.ToDateTime(txtCurMSRDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string date = txtCurMSRDate.Text.Trim();
                string comcod = this.GetCompCode();
                string reqno = this.Request.QueryString["reqno"].Substring(0, 14).ToString();
                string surveynpo = this.ddlPrevMSRList.SelectedValue.ToString();
                string rescode = String.Empty, spcfcod = String.Empty;
                bool result = false;
                if (this.Request.QueryString["reqno"].Length > 14)
                {
                    string resouces = this.Request.QueryString["reqno"].Substring(14).ToString();
                    int i = 0;
                    int counter = resouces.Length;
                    while (i < resouces.Length)
                    {
                        rescode = resouces.Substring(i, 12).ToString();
                        spcfcod = resouces.Substring(i + 12, 12).ToString();
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEMSERVAYNUM", reqno, surveynpo, rescode, spcfcod, "", "",
                      "", "", "", "", "", "", "", "", "");
                        i = i + 24;
                        rescode = string.Empty;
                        spcfcod = string.Empty;
                    }




                }
                else if (this.Request.QueryString["reqno"].ToString().Substring(0, 3) == "LRQ")
                {
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATE_LAB_BILL_CS", reqno, surveynpo, rescode, spcfcod, "", "",
                     "", "", "", "", "", "", "", "", "");
                }
                else
                {
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEMSERVAYNUM", reqno, surveynpo, date, "",
                      "", "", "", "", "", "", "", "", "");
                }

                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated  Fail');", true);
                    return;
                }

                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);


            }
            catch (Exception ex)
            {

            }
        }
        protected void gvMSRInfo2_RowCreated(object sender, GridViewRowEventArgs e)
        {



            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 4;
                gvrow.Cells.Add(cell0);


                DataTable dt = (DataTable)Session["tblt01"];
                //int j = 5;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TableCell cell = new TableCell();
                    cell.Text = dt.Rows[i]["ssirdesc1"].ToString();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    gvrow.Cells.Add(cell);

                    //if (j == 30)
                    //    break;


                }


                TableCell celll = new TableCell();
                celll.Text = "";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 2;
                gvrow.Cells.Add(celll);



                //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
                //  i++;


                gvMSRInfo2.Controls[0].Controls.AddAt(0, gvrow);
            }







        }

        protected void gvMSRInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtrate1 = (TextBox)e.Row.FindControl("txtrate1");
                TextBox txtrate2 = (TextBox)e.Row.FindControl("txtrate2");
                TextBox txtrate3 = (TextBox)e.Row.FindControl("txtrate3");
                TextBox txtrate4 = (TextBox)e.Row.FindControl("txtrate4");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(code, 2) == "71")
                {
                    txtrate1.Style.Add("text-align", "Left");
                    txtrate2.Style.Add("text-align", "Left");
                    txtrate3.Style.Add("text-align", "Left");
                    txtrate4.Style.Add("text-align", "Left");
                }

            }
        }
    }
}