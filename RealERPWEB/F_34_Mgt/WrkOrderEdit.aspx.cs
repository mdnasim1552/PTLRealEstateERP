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
    public partial class WrkOrderEdit : System.Web.UI.Page
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






        private string GetResSupplier()
        {
            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3337":
                case "3336":
                case "3340":
                case "3344":
                case "3348":
                case "1205":
                case "3351":
                case "3352":
                case "3354":
                case "3355":
                case "3315": //Asssure
                case "3316":
                case "3317":
                case "1108":
                case "1109":
                case "3101":
                case "3367":
                case "3368":








                    //case "3101":
                    Calltype = "GETAPROVALLSUPLIST";
                    break;

                default:
                    Calltype = "GETAPROVSUPLIST";
                    break;

            }
            return Calltype;


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
            string pactcode = "000000000000";
            string date = this.txtCurReqDate.Text;
            string Mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SHOWREQINFORMATION", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvpurorder.DataSource = null;
                this.gvpurorder.DataBind();

                return;
            }
            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);

            lblvalvounum.Text = ds1.Tables[1].Rows[0]["billvounum"].ToString();
            lblbillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();

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

            if (dt.Rows.Count == 0)
            {
                return;
            }

            this.gvpurorder.DataSource = dt;
            this.gvpurorder.DataBind();
            string comcod = this.GetCompCode();

            if (comcod == "3339")
            {
                ((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnUpdate")).Visible = (this.lblbillno.Text.Trim() == "00000000000000");

            }



            //((LinkButton)this.gvpurorder.FooterRow.FindControl("lbtnUpdate")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000");
            //this.gvpurorder.Columns[9].Visible= (this.lblvalvounum.Text.Trim() == "00000000000000");

        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            for (int i = 0; i < gvpurorder.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvreqty01")).Text.Trim());
                double srate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvsuprate")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[i].FindControl("txtgvAppRate01")).Text.Trim());
                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["srate"] = srate;
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
                string spcfcod = dr2["spcfcod"].ToString();
                double qty = Convert.ToDouble(dr2["qty"].ToString());
                string rate = Convert.ToDouble(dr2["rate"].ToString()).ToString();
                string srate = Convert.ToDouble(dr2["srate"].ToString()).ToString();
                string ssircode = dr2["ssircode"].ToString();
                string spcfcodo = dr2["spcfcodo"].ToString();
                string ssircodeo = dr2["ssircodeo"].ToString();

                string aprovno = dr2["aprovno"].ToString();
                string mrrno = dr2["mrrno"].ToString();
                string orderno = dr2["orderno"].ToString();

                bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "INSERTUPDATEWRKORDER", reqno, rsircode, spcfcod, qty.ToString(), rate, srate, ssircode, spcfcodo, ssircodeo, mreqno, aprovno, mrrno, orderno, "", "", "", "", "", "", "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


            }



            this.GetRateQtyChangeMsg();

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

        private void GetRateQtyChangeMsg()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3333":
                case "3336":
                    DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("grp='A'");
                    dt = dv.ToTable();
                    string desccrip = "";
                    string crsircode = "XXXXXXXXXXXX";
                    string cspcfcod = "XXXXXXXXXXXX";
                    string resdesc = "";
                    string spcfdesc = "";
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        string rsircode = dr1["rsircode"].ToString();
                        string spcfcod = dr1["spcfcod"].ToString();
                        resdesc = dr1["rsirdesc"].ToString();
                        spcfdesc = dr1["spcfdesc"].ToString();
                        double qty = Convert.ToDouble(dr1["qty"].ToString());
                        double mqty = Convert.ToDouble(dr1["mqty"].ToString());
                        double rate = Convert.ToDouble(dr1["rate"].ToString());
                        double mrate = Convert.ToDouble(dr1["mrate"].ToString());

                        if ((qty != mqty) || (rate != mrate))
                        {

                            if ((rsircode + spcfcod) == (crsircode + cspcfcod))
                            {
                                resdesc = "";
                                spcfdesc = "";

                            }

                            else
                            {
                                if (rsircode == crsircode)
                                    resdesc = "";

                                if (spcfcod == cspcfcod)
                                    spcfdesc = "";



                            }


                            if (qty != mqty)
                            {
                                desccrip += resdesc + ((spcfdesc == "NONE") ? " " : spcfdesc) + " Previous Qty:" + mqty.ToString("#,##0.00;(#,##0.00); ") + " Current Qty:" + qty.ToString("#,##0.00;(#,##0.00); ") + "\n";
                            }


                            if (rate != mrate)
                            {
                                desccrip += resdesc + ((spcfdesc == "NONE") ? " " : spcfdesc) + " Previous Rate:" + mrate.ToString("#,##0.00;(#,##0.00); ") + " Current Rate:" + rate.ToString("#,##0.00;(#,##0.00); ") + "\n";
                            }


                        }

                        crsircode = rsircode;
                        cspcfcod = spcfcod;



                    }




                    Hashtable hst = (Hashtable)Session["tblLogin"];

                    if (desccrip.Length > 0 && hst["compsms"].ToString() == "True")
                    {

                        string mrfno = dt.Rows[0]["refno"].ToString();

                        string desc = desccrip.Length > 0 ? ASTUtility.Left(desccrip, desccrip.Length - 2) : desccrip;
                        SendSmsProcess sms = new SendSmsProcess();

                        string comnam = hst["comnam"].ToString();
                        string Username = hst["username"].ToString();
                        string ApprovByid = hst["usrid"].ToString();
                        string compname = hst["compname"].ToString();
                        string frmname = "PurAprovEntry.aspx?InputType=PurProposal";

                        string SMSText = comnam + "\n" + ASTUtility.Left(desccrip, desccrip.Length - 2) + "\n" + " in MRF No: " + mrfno + "\n" + "Edit By " + Username;
                        bool resultsms = sms.SendSmmsEdit(SMSText, ApprovByid);
                    }
                    break;

                default:
                    break;


            }








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
            string mSpcfCod = ((Label)this.gvpurorder.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();

            string CallType = this.GetResSupplier();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", CallType, mSrchTxt, mResCode, "", "", "", "", "", "", "");
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

            // Specification

            DropDownList ddlspeci = (DropDownList)this.gvpurorder.Rows[e.NewEditIndex].FindControl("ddlspecification");
            ddlspeci.DataTextField = "spcfdesc";
            ddlspeci.DataValueField = "spcfcod";
            ddlspeci.DataSource = ds1.Tables[1];
            ddlspeci.DataBind();
            ddlspeci.SelectedValue = mSpcfCod;
        }
        protected void gvpurorder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)Session["tblpurchase"];

            string spcfcod = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString();
            string spcfdesc = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedItem.Text.Trim();

            string mSSIRCODE = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string mSSIRDesc = ((DropDownList)this.gvpurorder.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();
            // string spcfcod = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString();
            //string spcfdesc = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedItem.Text.Trim();
            // string mAPROVQTY = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewOrderQty")).Text.Trim()).ToString();

            //  string mAPROVRATE = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewApprovRate")).Text.Trim()).ToString();
            // string mAPROVRATE = Convert.ToDouble("0" + ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lgvNewApprovRate")).Text.Trim()).ToString();
            int index = (this.gvpurorder.PageIndex) * this.gvpurorder.PageSize + e.RowIndex;

            double qty = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvreqty01")).Text.Trim());
            double srate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvsuprate")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[e.RowIndex].FindControl("txtgvAppRate01")).Text.Trim());



            tbl1.Rows[index]["spcfcod"] = spcfcod;
            tbl1.Rows[index]["spcfdesc"] = spcfdesc;
            tbl1.Rows[index]["ssircode"] = mSSIRCODE;
            tbl1.Rows[index]["ssirdesc"] = mSSIRDesc;


            tbl1.Rows[index]["qty"] = qty;
            tbl1.Rows[index]["srate"] = srate;
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

                if (genno.Substring(0, 3) != "REQ")
                {

                    btnDelMat.Visible = false;
                }


                if (grpdesc == "")
                    return;

                if (grpdesc == "1. Requisition" || grpdesc == "2. Order Process" || grpdesc == "3. Purchase Order" || grpdesc == "4. Materials Received" || grpdesc == "5. Bill Confirmation")
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";


                }




            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink hlnkgvdesc = (HyperLink)e.Row.FindControl("hlnkgvdesc");



            //    string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

            //    if (Code == "")
            //        return;

            //    if (ASTUtility.Right(Code, 3)!= "000")
            //    {
            //        //hlnkgvdesc.Style.Add("color", "blue");
            //        string sirdesc= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
            //        hlnkgvdesc.NavigateUrl = "~/F_17_Acc/LinkSpecificCodeBook.aspx?sircode=" + Code + "&sirdesc=" + sirdesc;



            //    }




            //}
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
            string spcfcod = ((DataTable)Session["tblpurchase"]).Rows[RowIndex]["spcfcod"].ToString();




            bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "DELETEMATPURCHASE", reqno, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



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
            string spcfcod = ((Label)this.gvpurorder.Rows[index].FindControl("lblgvSpcfCod")).Text.Trim();

            double srate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvsuprate")).Text.Trim());
            double rate = Convert.ToDouble("0" + ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvAppRate01")).Text.Trim());


            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            DataTable tbl2 = ((DataTable)Session["tblpurchase"]).Copy();

            double reqty = 0;
            double reqty2 = 0;
            double reqty3 = 0;
            DataRow[] dr1;
            DataRow[] dr2;
            DataRow[] dr3;

            bool ispscf = this.isSpecification();
            if (ispscf)
            {
                switch (grp)
                {
                    case "A":
                        dr1 = tbl1.Select("(grp ='B' or grp ='C' or grp ='D' or grp ='E') and rsircode='" + rsircode +"' ");
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
                        dr1 = tbl1.Select("grp ='A' and rsircode='" + rsircode +"' ");
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
                            this.RiseError("Order Process Qty Cann't Large Req Qty ");
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
                            this.RiseError("Order Process Qty Should be Larger then Purchase Order Qty ");
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
                            this.RiseError("Purhcase Order Qty Cann't Large OrderProcess Qty ");
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
                            this.RiseError("Purchase Order Qty Should be Larger then Receive Qty");
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
                        dr2 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' ");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Receive Qty Cann't Large Order Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        dr3 = tbl1.Select("grp ='E' and rsircode='" + rsircode + "' ");
                        foreach (DataRow dr in dr3)
                        {
                            reqty3 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 < reqty3)
                        {
                            this.RiseError("Receive Qty Should be Larger then Bill Qty");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    case "E":
                        dr1 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' ");
                        foreach (DataRow dr in dr1)
                        {
                            reqty += Convert.ToDouble(dr["qty"]);
                        }
                        tbl1.Rows[index]["qty"] = qty;
                        dr2 = tbl1.Select("grp ='E' and rsircode='" + rsircode + "'");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Bill Qty Cann't Large Received Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    default:
                        break;
                }

            }
            else
            {
                switch (grp)
                {
                    case "A":
                        dr1 = tbl1.Select("(grp ='B' or grp ='C' or grp ='D' or grp ='E') and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
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

                        dr1 = tbl1.Select("grp ='A' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        if (dr1.Length != 0)
                        {
                            reqty = Convert.ToDouble(dr1[0]["qty"]);
                        }
                        tbl1.Rows[index]["qty"] = qty;
                        dr2 = tbl1.Select("grp ='B' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Order Process Qty Cann't Large Req Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        dr3 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr3)
                        {
                            reqty3 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 < reqty3)
                        {
                            this.RiseError("Order Process Qty Should be Larger then Purchase Order Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    case "C":
                        dr1 = tbl1.Select("grp ='B' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr1)
                        {
                            reqty += Convert.ToDouble(dr["qty"]);
                        }
                        tbl1.Rows[index]["qty"] = qty;
                        dr2 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Purhcase Order Qty Cann't Large OrderProcess Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        dr3 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr3)
                        {
                            reqty3 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 < reqty3)
                        {
                            this.RiseError("Purchase Order Qty Should be Larger then Receive Qty");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    case "D":
                        dr1 = tbl1.Select("grp ='C' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr1)
                        {
                            reqty += Convert.ToDouble(dr["qty"]);
                        }
                        tbl1.Rows[index]["qty"] = qty;
                        dr2 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Receive Qty Cann't Large Order Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        dr3 = tbl1.Select("grp ='E' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr3)
                        {
                            reqty3 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 < reqty3)
                        {
                            this.RiseError("Receive Qty Should be Larger then Bill Qty");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    case "E":
                        dr1 = tbl1.Select("grp ='D' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr1)
                        {
                            reqty += Convert.ToDouble(dr["qty"]);
                        }
                        tbl1.Rows[index]["qty"] = qty;
                        dr2 = tbl1.Select("grp ='E' and rsircode='" + rsircode + "' and spcfcod='" + spcfcod + "' ");
                        foreach (DataRow dr in dr2)
                        {
                            reqty2 += Convert.ToDouble(dr["qty"]);
                        }
                        if (reqty2 > reqty)
                        {
                            this.RiseError("Bill Qty Cann't Large Received Qty ");
                            tbl1.Rows[index]["qty"] = tbl2.Rows[index]["qty"];
                            ((TextBox)this.gvpurorder.Rows[index].FindControl("txtgvreqty01")).Text = Convert.ToDouble(tbl2.Rows[index]["qty"]).ToString("#,##0.00;(#,##0.00); ");
                            return;
                        }
                        break;

                    default:
                        break;
                }

            }


            tbl1.Rows[index]["qty"] = qty;
            tbl1.Rows[index]["srate"] = srate;
            tbl1.Rows[index]["rate"] = rate;
            tbl1.Rows[index]["amt"] = qty * rate;

            Session["tblpurchase"] = tbl1;
            this.Data_Bind();

        }

        private bool isSpecification()
        {
            bool isspcf = false;
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                    isspcf = true;
                    break;
                default:
                    isspcf = false;
                    break;
            }
            return isspcf;

        }

        private void RiseError(string msg)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            this.Data_Bind();
        }
    }
}




