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
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{

    public partial class RptShortListing : System.Web.UI.Page
    {
        ProcessAccess RecData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string type = this.Request.QueryString["Type"].ToString();
                this.lblTitle.Text = (type == "SList") ? "Short Listing Information Input/Edit Screen"
                        : (type == "IResult") ? "Interview Result Information Input/Edit Screen" : "Final Selection Information Input/Edit Screen";




            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.Get_SList_Info();
        }





        protected void Get_SList_Info()
        {

            string comcod = this.GetCompCode();
            string mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            string mPOST = this.ddlPOSTList.SelectedValue.ToString();

            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "SHOWSHOETLIST", mADVNO, mPOST,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbSList"] = ds1.Tables[0];
            ViewState["tblInterv"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];
            this.Data_Bind();

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }

        protected void Data_Bind()
        {
            DataTable dtname = (DataTable)ViewState["tblreqdesc"];
            int j = 4;
            for (int i = 0; i < dtname.Rows.Count; i++)
            {

                this.gvSListInfo.Columns[j].HeaderText = dtname.Rows[i]["reqdesc"].ToString();
                j++;
                if (j == 17)
                    break;
            }

            DataTable tbl1 = (DataTable)ViewState["tbSList"];
            this.gvSListInfo.DataSource = tbl1;
            this.gvSListInfo.DataBind();

            for (int i = 0; i < this.gvSListInfo.Rows.Count; i++)
            {
                string postcode = ((Label)gvSListInfo.Rows[i].FindControl("lblgvPostCod")).Text.Trim();
                string slno = ((Label)gvSListInfo.Rows[i].FindControl("lblgvissue")).Text.Trim();
                //string mrno = ((Label)gvSListInfo.Rows[i].FindControl("lgvmrno")).Text.Trim();
                //string cheqno = ((Label)gvSListInfo.Rows[i].FindControl("lgvCheNo")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSListInfo.Rows[i].FindControl("lbSelection");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = postcode + slno;

                ((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;
                ((LinkButton)this.gvSListInfo.Rows[i].FindControl("lbSelection")).Enabled = (((CheckBox)this.gvSListInfo.Rows[i].FindControl("chkvslno")).Checked) ? false : true;



            }



            DataTable tbl2 = (DataTable)ViewState["tblInterv"];
            this.gvIntInfo.DataSource = tbl2;
            this.gvIntInfo.DataBind();

            if (this.Request.QueryString["Type"] == "IResult")
            {
                this.gvSListInfo.Columns[1].Visible = false;
                this.gvIntInfo.Columns[1].Visible = false;
            }
            if (this.Request.QueryString["Type"] != "Fselection")
            {
                this.gvSListInfo.Columns[14].Visible = false;
                this.gvSListInfo.Columns[15].Visible = false;
                this.gvSListInfo.Columns[16].Visible = false;
                this.gvSListInfo.Columns[17].Visible = false;
                this.gvSListInfo.Columns[18].Visible = false;
            }
            if (this.Request.QueryString["Type"] == "Fselection")
            {
                //((LinkButton)this.gvSListInfo.FooterRow.FindControl("lbtnUpdateResReq")).Visible = false;
                ((LinkButton)this.gvIntInfo.FooterRow.FindControl("lbtnUpdateInt")).Visible = false;
                this.gvSListInfo.Columns[1].Visible = false;
                this.gvIntInfo.Columns[1].Visible = false;


            }

        }






        protected void ImgbtnFindPost_Click(object sender, ImageClickEventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlPrevAdvList.SelectedValue.ToString();
            string mSrchTxt = this.txtPostSearch.Text.Trim() + "%";
            DataSet ds1 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETADVPOST", mProject, mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblJobPost"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[1];

            this.ddlPOSTList.DataTextField = "postdesc";
            this.ddlPOSTList.DataValueField = "postcode";
            this.ddlPOSTList.DataSource = ds1.Tables[0];
            this.ddlPOSTList.DataBind();



        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImgbtnFindAdv_Click(object sender, ImageClickEventArgs e)
        {
            //string comcod = this.GetCompCode();


            //string mrfno = "%" + this.txtSrchPre.Text.Trim() + "%";
            //string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            //DataSet ds1 = RecData.GetTransInfo(comcod, "SP_ENTRY_ADVERTISEMENT", "GETPREREF", CurDate1,
            //              mrfno, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //this.ddlPrevAdvList.Items.Clear();
            //this.ddlPrevAdvList.DataTextField = "advno1";
            //this.ddlPrevAdvList.DataValueField = "advno";
            //this.ddlPrevAdvList.DataSource = ds1.Tables[0];
            //this.ddlPrevAdvList.DataBind();
        }



        protected void ImgbtnReqse_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void gvAdvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSListInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }






        protected void gvSListInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox gvCol1 = (TextBox)e.Row.FindControl("txtgvCol1");
            //    TextBox gvCol2 = (TextBox)e.Row.FindControl("txtgvCol2");
            //    TextBox gvCol3 = (TextBox)e.Row.FindControl("txtgvCol3");
            //    TextBox gvCol4 = (TextBox)e.Row.FindControl("txtgvCol4");
            //    TextBox gvCol5 = (TextBox)e.Row.FindControl("txtgvCol5");
            //    TextBox gvCol6 = (TextBox)e.Row.FindControl("txtgvCol6");
            //    TextBox gvCol7 = (TextBox)e.Row.FindControl("txtgvCol7");
            //    TextBox gvCol8 = (TextBox)e.Row.FindControl("txtgvCol8");
            //    TextBox gvCol9 = (TextBox)e.Row.FindControl("txtgvCol9");
            //    TextBox gvCol10 = (TextBox)e.Row.FindControl("txtgvCol10");
            //    TextBox gvCol11 = (TextBox)e.Row.FindControl("txtgvCol11");
            //    TextBox gvCol12 = (TextBox)e.Row.FindControl("txtgvCol12");
            //    TextBox gvCol13 = (TextBox)e.Row.FindControl("txtgvCol13");          

            //    string type = this.Request.QueryString["Type"].ToString();




            //    if (type == "SList")
            //    {

            //        gvCol9.ReadOnly = true;


            //    }
            //    else if (type == "IResult")
            //    {
            //        gvCol1.ReadOnly = true;
            //        gvCol2.ReadOnly = true;
            //        gvCol3.ReadOnly = true;
            //        gvCol4.ReadOnly = true;
            //        gvCol5.ReadOnly = true;
            //        gvCol6.ReadOnly = true;
            //        gvCol7.ReadOnly = true;
            //        gvCol8.ReadOnly = true;
            //        gvCol10.ReadOnly = true;
            //    }
            //    else
            //    {
            //        gvCol1.ReadOnly = true;
            //        gvCol2.ReadOnly = true;
            //        gvCol3.ReadOnly = true;
            //        gvCol4.ReadOnly = true;
            //        gvCol5.ReadOnly = true;
            //        gvCol6.ReadOnly = true;
            //        gvCol7.ReadOnly = true;
            //        gvCol8.ReadOnly = true;
            //        gvCol9.ReadOnly = true;
            //        gvCol10.ReadOnly = true;
            //        //gvCol11.Style.Add("color","red");
            //        //gvCol12.Font.Bold = true;
            //        //gvCol13.Font.Bold = true;
            //    }
            //    //if (Nlistisu != "")
            //    //{
            //    //    chkvslno.Enabled = false;
            //    //    lbSelection.Enabled = false;
            //    //}

            //}
        }






    }
}
