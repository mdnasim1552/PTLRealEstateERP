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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_34_Mgt
{
    public partial class ConWrkOrderEdit : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Work Order Edit ";
                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurReqDate_CalendarExtender.EndDate = System.DateTime.Today;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }

        private void GetReqno01()
        {

            Session.Remove("tblreq");
            string comcod = this.GetCompCode();
            string date = this.txtCurReqDate.Text;
            string mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETCONWORKORDER", mrfno, date, "", "", "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SHOWLABREQINFO", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvpurorder.DataSource = null;
                this.gvpurorder.DataBind();
                return;
            }
            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblvoucher"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string grp = dt1.Rows[0]["grp"].ToString();
            string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";

                grp = dt1.Rows[j]["grp"].ToString();

            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            DataTable dt1 = (DataTable)Session["tblvoucher"];


            if (dt.Rows.Count == 0)
            {
                return;
            }
            this.gvpurorder.DataSource = dt;
            this.gvpurorder.DataBind();
            if (dt1.Rows.Count > 0)
            {
                string vounum = dt1.Rows[0]["vounum"].ToString();
                if (vounum != "00000000000000")
                {
                    ((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnqtyTotal")).Visible = false;
                    ((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnUpdate")).Visible = false;
                    this.gvpurorder.Columns[6].Visible = false;
                    this.gvpurorder.Columns[11].Visible = false;
                }
            }


        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            for (int i = 0; i < gvpurorder.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvreqty01")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvAppRate01")).Text.Trim());
                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["rate"] = rate;
                tbl1.Rows[i]["amt"] = qty * rate;
            }
            Session["tblpurchase"] = tbl1;
        }

        private void LogStatus()
        {

            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SHOWREQINFORMATION", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            bool result = this.XmlDataInsert(reqno, ds1);
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblpurchase"];
            string mreqno = this.ddlReqNo01.SelectedValue.ToString();

            this.LogStatus();

            foreach (DataRow dr2 in dt.Rows)
            {
                string reqno = dr2["reqno"].ToString();
                string rsircode = dr2["rsircode"].ToString();
                double qty = Convert.ToDouble(dr2["qty"].ToString());
                double rate = Convert.ToDouble(dr2["rate"].ToString());
                double amt = qty * rate;
                string ssircode = dr2["ssircode"].ToString();
                //string ssircodeo = dr2["ssircodeo"].ToString();

                //string aprovno = dr2["aprovno"].ToString();
                //string mrrno = dr2["mrrno"].ToString();
                string orderno = dr2["orderno"].ToString();

                bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "INSERTUPDATECONWRKORDER", reqno, rsircode, qty.ToString(), rate.ToString(), amt.ToString(), ssircode, orderno, "", "", "", "", "","", "", "", "", "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }


        private bool XmlDataInsert(string reqno, DataSet ds)
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
            dt1.Columns.Add("editbyid", typeof(string));
            dt1.Columns.Add("editseson", typeof(string));
            dt1.Columns.Add("edittrmnid", typeof(string));
            dt1.Columns.Add("editdate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["editbyid"] = usrid;
            dr1["editseson"] = session;
            dr1["edittrmnid"] = trmnid;
            dr1["editdate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXMLORKORDER", ds1, null, null, reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }
       
        protected void lbtnqtyTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvpurorder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvpurorder.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvpurorder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvpurorder.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            // Supplier
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%%";
            string mResCode = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mSupCode = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvResCod1")).Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVALLSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DropDownList ddl1 = (DropDownList)this.gvpurorder.Rows[e.NewEditIndex].FindControl("ddlSupname");
            ddl1.DataTextField = "ssirdesc1";
            ddl1.DataValueField = "ssircode";
            ddl1.DataSource = ds1.Tables[0];
            ddl1.DataBind();
            ddl1.SelectedValue = mSupCode;
        }
        protected void gvpurorder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            string mSSIRCODE = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string mSSIRDesc = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();
            int index = (this.gvpurorder.PageIndex) * this.gvpurorder.PageSize + e.RowIndex;

            double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvreqty01")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvAppRate01")).Text.Trim());

            tbl1.Rows[index]["ssircode"] = mSSIRCODE;
            tbl1.Rows[index]["ssirdesc"] = mSSIRDesc;


            tbl1.Rows[index]["qty"] = qty;
            tbl1.Rows[index]["rate"] = rate;
            tbl1.Rows[index]["amt"] = qty * rate;

            Session["tblpurchase"] = tbl1;
            this.gvpurorder.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvpurorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton btnDelMat = (LinkButton)e.Row.FindControl("btnDelMat");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string grpdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString().Trim();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "genno")).ToString().Trim();
                if (grpdesc == "")
                    return;

                if (grpdesc == "1.Requisition" || grpdesc == "2.Work Order" || grpdesc == "3.Labor Issue" || grpdesc == "4.Contactor Bill")
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";
                }
            }

        }


        protected void btnDelMat_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string reqno = ((DataTable)Session["tblpurchase"]).Rows[RowIndex]["reqno"].ToString();
            string rsircode = ((DataTable)Session["tblpurchase"]).Rows[RowIndex]["rsircode"].ToString();

            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "DELETELABISSUE", reqno, rsircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

                lbtnOk_Click(null, null);
            }


        }

        protected void txtgvreqty01_TextChanged(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text.Trim());
            string grp = ((Label)this.gvpurorder.Rows[index].FindControl("lblgvGroup")).Text.Trim();
            string reqno = ((Label)this.gvpurorder.Rows[index].FindControl("lblgvRqno")).Text.Trim();
            string rsircode = ((Label)this.gvpurorder.Rows[index].FindControl("lblgvResCod")).Text.Trim();
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvAppRate01")).Text.Trim());


            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            DataTable tbl2 = ((DataTable)Session["tblpurchase"]).Copy();

            double reqty = 0;
            double reqty2 = 0;
            double reqty3 = 0;
            DataRow[] dr1;
            DataRow[] dr2;
            DataRow[] dr3;

            switch (grp)
            {
                case "A":
                    dr1 = tbl1.Select("(grp ='B' or grp ='C' or grp ='D') and rsircode='" + rsircode + "' ");
                    if (dr1.Length != 0)
                    {
                        reqty = Convert.ToDouble(dr1[0]["qty"]);
                        if (qty < reqty)
                        {
                            this.RiseError("Requisition Qty Should Large than Further Qty ");
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                    }
                    tbl1.Rows[index]["qty"] = qty;
                    break;

                case "B":
                    dr1 = tbl1.Select("grp ='A' and rsircode='" + rsircode + "' ");
                    if (dr1.Length != 0)
                    {
                        reqty = Convert.ToDouble(dr1[0]["qty"]);
                    }
                    tbl1.Rows[index]["qty"] = qty;
                    dr2 = tbl1.Select("grp ='B' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr2)
                    {
                        reqty2 += Convert.ToDouble(dr["qty"]);
                    }
                    if (reqty2 > reqty)
                    {
                        this.RiseError("Contactor Work Order Qty Cann't Large Req Qty ");
                        tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                        ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                        return;
                    }
                    dr3 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr3)
                    {
                        reqty3 += Convert.ToDouble(dr["qty"]);
                    }
                    if (reqty2 < reqty3)
                    {
                        this.RiseError("Contactor Work Order Qty Should be Larger then Labor Issue Qty");
                        tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                        ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                        return;
                    }
                    break;

                case "C":
                    dr1 = tbl1.Select("grp ='B' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr1)
                    {
                        reqty += Convert.ToDouble(dr["qty"]);
                    }
                    tbl1.Rows[index]["qty"] = qty;
                    dr2 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr2)
                    {
                        reqty2 += Convert.ToDouble(dr["qty"]);
                    }
                    if (reqty2 > reqty)
                    {
                        this.RiseError("Labor Issue Qty Cann't Large Contactor Work Order Qty");
                        tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                        ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                        return;
                    }
                    dr3 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr3)
                    {
                        reqty3 += Convert.ToDouble(dr["qty"]);
                    }
                    if (reqty2 < reqty3)
                    {
                        this.RiseError("Labor Issue Qty Should be Larger then Contactor Bill Qty");
                        tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                        ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                        return;
                    }
                    break;
                case "D":
                    dr1 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' ");
                    foreach (DataRow dr in dr1)
                    {
                        reqty += Convert.ToDouble(dr["qty"]);
                    }
                    tbl1.Rows[index]["qty"] = qty;
                    dr2 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "'");
                    foreach (DataRow dr in dr2)
                    {
                        reqty2 += Convert.ToDouble(dr["qty"]);
                    }
                    if (reqty2 > reqty)
                    {
                        this.RiseError("Contactor Bill Qty Cann't Large Labor Issue Qty");
                        tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                        ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                        return;
                    }
                    break;


                default:
                    break;
            }


            tbl1.Rows[index]["qty"] = qty;
            tbl1.Rows[index]["rate"] = rate;
            tbl1.Rows[index]["amt"] = qty * rate;

            Session["tblpurchase"] = tbl1;
            this.Data_Bind();

        }

        private void RiseError(string msg)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            this.Data_Bind();
        }
    }
}




