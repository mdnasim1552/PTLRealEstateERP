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
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdEstStdAna : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "ESTIMATED STANDARD ANALYSIS";

                this.GetWorkName();
                this.GetResource();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetWorkName()
        {

            string comcod = this.GetComCode();
            string Srchwrk = "%" + this.txtSrcWrk.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ESTSTDANA", "GETWRKMAINCODE", Srchwrk, "", "", "", "", "", "", "", "");
            this.ddlWork.DataTextField = "wrkdesc";
            this.ddlWork.DataValueField = "wrkcode";
            this.ddlWork.DataSource = ds1.Tables[0];
            this.ddlWork.DataBind();
            ds1.Dispose();

        }


        private void GetItemCode()
        {

            string comcod = this.GetComCode();
            string WrkCode = this.ddlWork.SelectedValue.ToString().Substring(0, 3);
            string Srchwrk = "%" + this.txtSrcItem.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ESTSTDANA", "GETWRKDETAILSCODE", WrkCode, Srchwrk, "", "", "", "", "", "", "");
            this.ddlItem.DataTextField = "wrkdesc";
            this.ddlItem.DataValueField = "wrkcode";
            this.ddlItem.DataSource = ds1.Tables[0];
            this.ddlItem.DataBind();
            ds1.Dispose();


        }



        private void GetResource()
        {
            ViewState.Remove("tblResouce");
            string comcod = this.GetComCode();
            string SrchResource = "%" + this.txtSrcResouce.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ESTSTDANA", "GETRESCODE", SrchResource, "", "", "", "", "", "", "", "");
            this.ddlResource.DataTextField = "rsirdesc";
            this.ddlResource.DataValueField = "rsircode";
            this.ddlResource.DataSource = ds1.Tables[0];
            this.ddlResource.DataBind();
            ViewState["tblResouce"] = ds1.Tables[0];

        }
        protected void ibtnFindWork_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetWorkName();
        }

        protected void ibtnFindItem_Click(object sender, EventArgs e)
        {
            this.GetItemCode();
        }
        protected void ibtnFindResource_Click(object sender, EventArgs e)
        {
            this.GetResource();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblWrkdesc.Text = this.ddlWork.SelectedItem.Text;
                this.ddlWork.Visible = false;
                this.lblWrkdesc.Visible = true;
                this.PanelSub.Visible = true;
                this.GetItemCode();
                this.ShowData();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlWork.Visible = true;
            this.lblWrkdesc.Text = "";

            this.lblWrkdesc.Visible = false;
            this.PanelSub.Visible = false;
            this.gvEstAna.DataSource = null;
            this.gvEstAna.DataBind();
        }




        private void ShowData()
        {
            ViewState.Remove("tblestana");
            string comcod = this.GetComCode();
            string WorkName = this.ddlWork.SelectedValue.ToString().Substring(0, 3) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ESTSTDANA", "GETSTDANALYSIS", WorkName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvEstAna.DataSource = null;
                this.gvEstAna.DataBind();

            }
            ViewState["tblestana"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void Data_Bind()
        {

            this.gvEstAna.DataSource = (DataTable)ViewState["tblestana"];
            this.gvEstAna.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblestana"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFtoWeight")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toweight)", "")) ?
                       0 : dt.Compute("sum(toweight)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFtotalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toqty)", "")) ?
                                0 : dt.Compute("sum(toqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEstAna.FooterRow.FindControl("lgvFgtotalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gtqty)", "")) ?
                         0 : dt.Compute("sum(gtqty)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblestana"];
            bool result = false;
            foreach (DataRow dr in dt.Rows)
            {
                string wrkcode = dr["wrkcode"].ToString();
                string rsircode = dr["rsircode"].ToString();
                string length = dr["lnght"].ToString();
                string qty = dr["qty"].ToString();
                string weight = dr["weight"].ToString();
                string tobase = dr["tbase"].ToString();
                string wastage = dr["wastage"].ToString();
                string bnumber = dr["bnumber"].ToString();


                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_ESTSTDANA", "INSORUPDATEESTSTDANA", wrkcode,
                        rsircode, length, qty, weight, tobase, wastage, bnumber, "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




        }




        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string WrkCode = this.ddlItem.SelectedValue.ToString().Trim();
            string RsirCode = this.ddlResource.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblestana"];
            DataRow[] dr = dt.Select("wrkcode='" + WrkCode + "' and rsircode='" + RsirCode + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["wrkcode"] = WrkCode;
                dr1["wrkdesc"] = this.ddlItem.SelectedItem.Text;
                dr1["rsircode"] = RsirCode;
                dr1["rsirdesc"] = this.ddlResource.SelectedItem.Text;
                dr1["rsirunit"] = (((DataTable)ViewState["tblResouce"]).Select("rsircode='" + RsirCode + "'"))[0]["rsirunit"];
                dr1["bnumber"] = "";
                dr1["lnght"] = 0.00;
                dr1["qty"] = 0.00;
                dr1["weight"] = 0.00;
                dr1["toweight"] = 0.00;
                dr1["tbase"] = 0.00;
                dr1["toqty"] = 0.00;
                dr1["wastage"] = 0.00;
                dr1["gtqty"] = 0.00;
                dt.Rows.Add(dr1);

            }

            DataView dv = dt.DefaultView;
            dv.Sort = ("wrkcode, rsircode");
            dt = dv.ToTable();
            ViewState["tblestana"] = this.HiddenSameData(dt);
            this.Data_Bind();
        }





        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string wrkcode = dt1.Rows[0]["wrkcode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["wrkcode"].ToString() == wrkcode)
                    dt1.Rows[j]["wrkdesc"] = "";
                wrkcode = dt1.Rows[j]["wrkcode"].ToString();

            }



            return dt1;

        }



        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblestana"];
            int rowindex;
            for (int i = 0; i < this.gvEstAna.Rows.Count; i++)
            {
                string bnumber = ((TextBox)this.gvEstAna.Rows[i].FindControl("txtbasenumber")).Text.Trim();
                double Length = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvlength")).Text.Trim());
                double Qty = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvqty")).Text.Trim());
                double weight = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvweight")).Text.Trim());
                double tobase = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvtobase")).Text.Trim());
                double Wastage = Convert.ToDouble("0" + ((TextBox)this.gvEstAna.Rows[i].FindControl("txtgvwastage")).Text.Trim());
                double toweight = Length * Qty * weight;
                double toqty = Length * Qty * weight * tobase;
                double wasqty = Length * Qty * weight * tobase * Wastage * 0.01;
                double gqty = toqty + wasqty;

                rowindex = (gvEstAna.PageIndex) * gvEstAna.PageSize + i;
                dt.Rows[rowindex]["bnumber"] = bnumber;
                dt.Rows[rowindex]["lnght"] = Length;
                dt.Rows[rowindex]["qty"] = Qty;
                dt.Rows[rowindex]["weight"] = weight;
                dt.Rows[rowindex]["toweight"] = toweight;
                dt.Rows[rowindex]["tbase"] = tobase;
                dt.Rows[rowindex]["toqty"] = toqty;
                dt.Rows[rowindex]["wastage"] = Wastage;
                dt.Rows[rowindex]["gtqty"] = gqty;
            }


            ViewState["tblestana"] = dt;

        }

        protected void lnkbtnTotql_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
    }
}



