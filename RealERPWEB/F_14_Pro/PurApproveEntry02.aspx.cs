using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurApproveEntry02 : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

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
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "VenSelect" ? "Vendor Selection" : "Requisition Approval";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetProjectName();

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string srchproject = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "GETPROJECTNAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();




        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.pnlGridPage.Visible = true;
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }


        private void ShowData()
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = " ";
            try
            {
                Session.Remove("tblreq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchChequeno = "%" + this.txtserchmrf.Text.Trim() + "%";

                int startRow = PageNumber * 100;
                int endRow = (PageNumber + 1) * 100;
                string pactcode = ((this.ddlProject.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProject.SelectedValue.ToString()) + "%";

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "SHOWREQUISITON", Date, pactcode, startRow.ToString(), endRow.ToString(), SrchChequeno, "", "", "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }

                Session["tblreq"] = this.HiddenSameDate(ds1.Tables[0]);
                Session["tbltopage"] = ds1.Tables[1];


                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            }

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblreq"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();




            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string reqno = ((Label)dgv1.Rows[i].FindControl("lblgvreqno")).Text.Trim();

                ((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                ((LinkButton)dgv1.Rows[i].FindControl("lbok")).Enabled = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");

                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = reqno;
            }







        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string reqno = dt1.Rows[0]["reqno"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            int j;



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[0]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["mrfno"] = "";
                    dt1.Rows[j]["reqdat"] = "";

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                    dt1.Rows[j]["rsirunit"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    {
                        dt1.Rows[j]["reqno1"] = "";
                        dt1.Rows[j]["mrfno"] = "";
                        dt1.Rows[j]["reqdat"] = "";


                    }

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";



                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";
                        dt1.Rows[j]["rsirunit"] = "";


                    }



                }

                pactcode = dt1.Rows[j]["pactcode"].ToString();
                reqno = dt1.Rows[j]["reqno"].ToString();
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }





            return dt1;


            //grpcode = dt1.Rows[0]["grpcode"].ToString();
            //            string actcode = dt1.Rows[0]["actcode"].ToString();
            //            for (j = 1; j < dt1.Rows.Count; j++)
            //            {
            //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
            //                {
            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();
            //                    dt1.Rows[j]["grpdesc"] = "";
            //                    dt1.Rows[j]["actdesc"] = "";

            //                }

            //                else
            //                {


            //                    if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
            //                    {
            //                        dt1.Rows[j]["grpdesc"] = "";
            //                    }
            //                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //                    {
            //                        dt1.Rows[j]["actdesc"] = "";
            //                    }

            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();

            //                }

            //            }
            //        break;

            //}


            //  return dt1;



        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblreq"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                dt.Rows[i]["chkmv"] = chkmr;

                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblreq"] = dt;
        }


        protected string GetAprovNo()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtdate.Text.Trim();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTAPROVINFO", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return "";

            return (ds1.Tables[0].Rows[0]["maxaprovno"].ToString());
        }


        protected void lbok_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                this.CheckValue();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Reqno = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();


                DataTable dt = (DataTable)Session["tblreq"];
                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("reqno='" + Reqno + "'");
                dt1 = dv.ToTable();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Chk = dt1.Rows[i]["chkmv"].ToString();
                    if (Chk == "False")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
                        return;
                    }
                }




                DataRow[] dr = dt1.Select("aprovqty>0");
                if (dr.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Input Order Qty";
                    return;
                }




                switch (comcod)
                {
                    case "3301":
                    case "1301":
                    case "2301":
                        break;

                    default:
                        foreach (DataRow dr2 in dt1.Rows)
                        {

                            if (Convert.ToDouble(dr2["maprovrate"]) < Convert.ToDouble(dr2["aprovrate"]))
                            {

                                ((Label)this.Master.FindControl("lblmsg")).Text = "Rate Equal or Below Aproved  Rate";
                                return;
                            }

                        }

                        break;

                }
                string mAPROVDAT = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string mAPROVNO = this.GetAprovNo();
                string mAPROVUSRID = "";
                string mAPPRUSRID = "";
                string mAPPRDAT = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy"); ;
                string mAPROVBYDES = "";
                string mAPPBYDES = "";
                string mAPROVNAR = "";

                //log Report
                DataTable dtuser = (DataTable)Session["tblUserAprov"];

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();


                string PostedByid = userid;
                string Posttrmid = Terminal;
                string PostSession = Sessionid;

                string ApprovByid = "";
                string approvdat = "01-Jan-1900";
                string Approvtrmid = "";
                string ApprovSession = "";
                ////For Balace Req Qty
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string mREQNO = dt1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = dt1.Rows[i]["rsircode"].ToString();
                    DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BALREQQTY", mREQNO, mRSIRCODE, "", "", "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "There is no balance qty in Requisition";
                        return;
                    }

                }


                //////////

                bool result = accData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPROVINFO", "PURAPROVB", mAPROVNO, mAPROVDAT, mAPROVUSRID, mAPPRUSRID,
                            mAPPRDAT, mAPROVBYDES, mAPPBYDES, mAPROVNAR, PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }


                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    string mREQNO = dt1.Rows[i]["reqno"].ToString();

                    bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(dt1.Rows[i]["reqdat"].ToString()), Convert.ToDateTime(mAPROVDAT));
                    if (!dcon)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Date is equal or greater Requisition Date');", true);
                        return;
                    }




                    string mRSIRCODE = dt1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = dt1.Rows[i]["spcfcod"].ToString();
                    string mSSIRCODE = dt1.Rows[i]["ssircode"].ToString();
                    double Balqty = Convert.ToDouble(dt1.Rows[i]["aprovqty"]);
                    string mAPROVQTY = Convert.ToDouble("0" + dt1.Rows[i]["aprovqty"]).ToString();
                    double mAPROVQTY1 = Convert.ToDouble("0" + dt1.Rows[i]["aprovqty"]);

                    string mAPROVRATE = dt1.Rows[i]["aprovrate"].ToString();
                    string mPAYTYPE = "";
                    if (Balqty >= mAPROVQTY1)
                    {

                        if (mAPROVQTY1 > 0)
                            result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPROVINFO", "PURAPROVA",
                                        mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mAPROVQTY, mAPROVRATE, mPAYTYPE, "", "", "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }

                    }

                    else
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Order Qty Less then or Equal Balance Qty";
                        return;

                    }

                }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";









                //DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "REQAPPROVED", Reqno, "", "", "", "", "", "", "", "");
                //if (ds4.Tables[0].Rows[0]["approved"].ToString() != "")
                //{

                // ((Label)this.Master.FindControl("lblmsg")).Text = "Requisition Number Already Approved";
                //    return;
                //}



                //bool result = false;
                //foreach (DataRow dr in dt1.Rows)
                //{

                //    string gpsl = dr["gpsl"].ToString();
                //    if (gpsl == "2") continue;
                //    string mRSIRCODE = dr["rsircode"].ToString();
                //    string mSPCFCOD = dr["spcfcod"].ToString();

                //    double mPREQTY = Convert.ToDouble(dr["preqty"]);
                //    double mAREQTY = Convert.ToDouble(dr["areqty"]);

                //    string mREQRAT = dr["reqrat"].ToString();
                //    string mEXPUSEDT = dr["expusedt"].ToString();
                //    string mREQNOTE = dr["reqnote"].ToString();
                //    string PursDate = dr["pursdate"].ToString();
                //    string Lpurrate = dr["lpurrate"].ToString();
                //    string storecode = dr["storecode"].ToString();
                //    string ssircode = dr["ssircode"].ToString();
                //    string orderno = dr["orderno"].ToString();

                //    if (mPREQTY >= mAREQTY)
                //    {

                //        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEPURREQAINF",Reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mEXPUSEDT, mREQNOTE,
                //                    PursDate, Lpurrate, storecode, ssircode, orderno,"","");
                //        if (!result)
                //        {
                //         ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                //            return;
                //        }
                //    }
                //    else
                //    {
                //     ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                //        return;

                //    }

                //}


                //result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQNO", Reqno, ApprovByid, approvdat, Approvtrmid, ApprovSession, approved, "Approved", "", "", "", "", "", "", "", "");


                //  if (!result)
                //        {
                //         ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                //            return;
                //        }

                //  this.lblmsg.Text = "Data Updated successfully";
                Session["tblreq"] = dt;
                this.Data_Bind();
                this.CheckValue();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }

        private string CompanyRequisition()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3101":
                    PrintReq = "PrintReque02";

                    break;

                default:
                    PrintReq = "PrintReque01";
                    break;
            }

            return PrintReq;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string printcomreq = this.CompanyRequisition();

            if (printcomreq == "PrintReque01")
            {
                this.PrintRequisition01();

            }

            else
            {
                this.PrintRequisition02();



            }


        }


        private void PrintRequisition01()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject rpttxtexdeldate = rptstk.ReportDefinition.ReportObjects["eddate"] as TextObject;
            //rpttxtexdeldate.Text = this.txtExpDeliveryDate.Text.Trim();
            //TextObject rpttxtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpttxtadate.Text = this.txtApprovalDate.Text.Trim();
            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtfloornoText = rptstk.ReportDefinition.ReportObjects["floornotext"] as TextObject;
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["floorno"] as TextObject;
            //if (ddlFloor.SelectedValue.ToString().Trim() != "000")
            //{

            //    txtfloornoText.Text = "Floor No:";
            //    txtfloorno.Text = this.ddlFloor.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    txtfloornoText.Text = "";
            //    txtfloorno.Text = " ";
            //}

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);



            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)Session["tblreq"];

            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintRequisition02()
        {



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry02();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(14);

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString();
            //TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            //txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();




            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblreq"]);
            //Session["Report1"] = rptstk;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink survey = (HyperLink)e.Row.FindControl("hlnkgvSurvey");

                //Label sign = (Label)e.Row.FindControl("gvsign");


                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString().Trim();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString().Trim();

                survey.Style.Add("color", "blue");

                if (msrno == "Survey")
                {
                    survey.NavigateUrl = "~/F_12_Inv/LinkMktSurvey.aspx?reqno=" + reqno;
                }
                else
                {
                    survey.NavigateUrl = "~/F_12_Inv/LinkShowMktSurvey.aspx?Type=TarVsAch&msrno=" + msrno;

                }







            }
        }
        protected void imgBtnFirst_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = 0;
            this.ShowData();
            this.lblCurPage.Text = "1";
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            this.imgBtnPerv.Enabled = false;
            this.imgBtnNext.Enabled = true;

        }
        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = PageNumber + 1;

            if (PageNumber == pageCount)
            {
                PageNumber = PageNumber - 1;
                this.imgBtnNext.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnPerv.Enabled = true;
            this.ShowData();
        }

        protected void imgBtnPerv_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = PageNumber - 1;
            if (PageNumber < 0)
            {
                PageNumber = 0;
                this.imgBtnPerv.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.ShowData();
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnNext.Enabled = true;
        }
        protected void imgBtnLast_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = pageCount - 1;
            this.ShowData();
            this.lblCurPage.Text = pageCount.ToString();
            this.lblCurPage.ToolTip = "Page " + (pageCount) + " of " + pageCount;
            this.imgBtnNext.Enabled = false;
            this.imgBtnPerv.Enabled = true;
        }
        protected void imgbtnSearchCheqNO_Click(object sender, ImageClickEventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }

        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgv1.EditIndex = e.NewEditIndex;
            this.Data_Bind();



            string comcod = this.GetCompCode();
            string mSrchTxt = "%";
            string mResCode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mSupCode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvsupliercode")).Text.Trim();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVSUPLIST", mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            if (ds2.Tables[0].Rows.Count == 0)
                return;

            DropDownList ddl2 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlSupname");
            ddl2.DataTextField = "ssirdesc1";
            ddl2.DataValueField = "ssircode";
            ddl2.DataSource = ds2.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = mSupCode;



        }
        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblreq"];

            string ssircode = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string ssirdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();

            double orderqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvNewOrderQty")).Text.Trim());
            double Apprate = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvNewApprovRate")).Text.Trim());

            int index = (this.dgv1.PageIndex) * this.dgv1.PageSize + e.RowIndex;

            tbl1.Rows[index]["ssircode"] = ssircode;
            tbl1.Rows[index]["ssirdesc"] = ssirdesc;
            tbl1.Rows[index]["aprovqty"] = orderqty;
            tbl1.Rows[index]["aprovrate"] = Apprate;
            tbl1.Rows[index]["aprovamt"] = orderqty * Apprate;

            Session["tblreq"] = tbl1;
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.Data_Bind();
        }

        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblreq"];
            int index;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            for (int j = 0; j < this.dgv1.Rows.Count; j++)
            {

                index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + j;
                double orderqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvNewOrderQty")).Text.Trim());
                double Apprate = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[j].FindControl("txtgvNewApprovRate")).Text.Trim());
                tbl1.Rows[index]["aprovqty"] = orderqty;
                tbl1.Rows[index]["aprovrate"] = Apprate;
                tbl1.Rows[index]["aprovamt"] = orderqty * Apprate;
            }
            Session["tblreq"] = tbl1;
        }


        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string EditByid = hst["usrid"].ToString();
                string Edittrmid = hst["compname"].ToString();
                string EditSession = hst["session"].ToString();
                string Editdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");






                DataTable dt1 = (DataTable)Session["tblreq"];
                string Type = this.Request.QueryString["Type"].ToString();

                if (Type == "VenSelect")
                {

                    switch (comcod)
                    {
                        case "3301":
                        case "1301":
                        case "2301":
                            break;

                        default:
                            foreach (DataRow dr2 in dt1.Rows)
                            {

                                if (Convert.ToDouble(dr2["mreqrat"]) < Convert.ToDouble(dr2["reqrat"]))
                                {

                                    ((Label)this.Master.FindControl("lblmsg")).Text = "Rate Equal or Below Aproved  Rate";
                                    return;
                                }

                            }

                            break;

                    }

                }
                bool result = false;




                string Reqno1 = "XXXXXXXXXXXXXX";

                foreach (DataRow dr in dt1.Rows)
                {

                    string gpsl = dr["gpsl"].ToString();
                    if (gpsl == "2") continue;
                    string Reqno = dr["reqno"].ToString();
                    string mRSIRCODE = dr["rsircode"].ToString();
                    string mSPCFCOD = dr["spcfcod"].ToString();
                    double mPREQTY = Convert.ToDouble(dr["preqty"]);
                    double mAREQTY = Convert.ToDouble(dr["areqty"]);
                    string mREQRAT = dr["reqrat"].ToString();
                    string mEXPUSEDT = dr["expusedt"].ToString();
                    string mREQNOTE = dr["reqnote"].ToString();
                    string PursDate = dr["pursdate"].ToString();
                    string Lpurrate = dr["lpurrate"].ToString();
                    string storecode = dr["storecode"].ToString();
                    string ssircode = dr["ssircode"].ToString();
                    string orderno = dr["orderno"].ToString();

                    if (mPREQTY >= mAREQTY)
                    {

                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEPURREQAINF", Reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mEXPUSEDT, mREQNOTE,
                                    PursDate, Lpurrate, storecode, ssircode, orderno, "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                    }




                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                        return;

                    }






                    if (Reqno != Reqno1)
                    {
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQNO", Reqno, EditByid, Editdat, Edittrmid, EditSession, "", "", "", "", "", "", "", "", "", "");


                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }
                    }

                    Reqno1 = Reqno;


                }




         ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }
        protected void ImgbtnFindSechreqno_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnSurvey_Click(object sender, EventArgs e)
        {
            //string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
        }
    }
}


