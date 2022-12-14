using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealEntity.C_30_Facility.EClass_Facility_Mgt;

namespace RealERPWEB.F_30_Facility
{
    public partial class ComplaintMatReq : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getProjUnitddl();
                txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                btnOKClick.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Requisition";
                if (Request.QueryString["DgNo"] != null)
                {
                    //btnOKClick.Enabled = false;
                    //btnOKClick.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlTransfer.Visible = false;
                    //btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                    getReqNo();
                    getComplainUser();
                    createMaterialList();
                    getReqDesc();
                }
            }
        }

        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            if (btnOKClick.Text == "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK")
            {
                string type = ddlType.SelectedValue.ToString();
                if (type == "1")
                {
                    getComplainUser();
                    GetToInventoryCode();
                    GetProject();
                    GetMaterial();
                    GetMatTransferCode();
                    pnlMatReq.Visible = false;
                    pnlTransfer.Visible = true;
                }
                else
                {
                    pnlTransfer.Visible = false;
                    pnlMatReq.Visible = true;
                    getReqNo();
                    getComplainUser();
                    createMaterialList();
                    getReqDesc();
                }

                btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";

            }
            else
            {
                pnlMatReq.Visible = false;
                pnlTransfer.Visible = false;
                grvacc.DataSource = null;
                grvacc.DataBind();
                btnOKClick.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void getReqNo()
        {
            string comcod = GetComCode();
            string CurDate1 = txtEntryDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.lblReqNo.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6) + ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                this.lblReqNoFull.Text = ds1.Tables[0].Rows[0]["maxreqno"].ToString();
            }
        }
        private void getProjUnitddl()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGNO", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt = ds.Tables[0];
                ddlDgNo.DataSource = dt;
                ddlDgNo.DataTextField = "dgdesc";
                ddlDgNo.DataValueField = "dgno";
                ddlDgNo.DataBind();
                if (Request.QueryString["DgNo"] != null)
                {
                    ddlDgNo.SelectedValue = Request.QueryString["DgNo"].ToString();
                    ddlDgNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        private void getComplainUser()
        {
            try
            {
                string comcod = GetComCode();
                string dgno = Request.QueryString["DgNo"] ?? ddlDgNo.SelectedValue.ToString();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGINFO", dgno, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt01 = ds.Tables[0];
                DataTable dt02 = ds.Tables[1];
                ViewState["DetailInfo"] = dt02;
                DataTable dt03 = ds.Tables[2];
                if (dt02 != null || dt02.Rows.Count > 0)
                {
                    var row = dt02.Rows[0];
                    lblProjectText.Text = row["pactdesc"].ToString();
                    lblCustomerText.Text = row["custdesc"].ToString();
                    lblUnitText.Text = row["unit"].ToString();
                    lblSiteVisisted.Text = row["sitevisiteddate"].ToString();
                    lblWarranty.Text = row["warrantydesc"].ToString();
                    lblWarrantyCode.Text = row["warranty"].ToString();
                    this.lblpactcode.Value = row["pactcode"].ToString();
                }
                List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
                List<EClass_Complain_List> obj1 = dt03.DataTableToList<EClass_Complain_List>();
                //gvComplainForm.DataSource = obj1;
                //gvComplainForm.DataBind();
                ViewState["ComplainList"] = obj;
                //Bind_Grid(obj);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }
        private void createMaterialList()
        {
            try
            {
                List<EClass_Material_List> obj = new List<EClass_Material_List>();
                string comcod = GetComCode();
                string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETBUDGETINFOMATREQ", dgno, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                if (ds.Tables[1] != null || ds.Tables[1].Rows.Count > 0)
                {
                    var row = ds.Tables[1].Rows[0];
                    lblBgdDate.Text = Convert.ToDateTime(row["bgddate"].ToString()).ToString("dd-MMM-yyyy");
                    lblApprDate.Text = Convert.ToDateTime(row["approvalDate"].ToString()).ToString("dd-MMM-yyyy");
                    lblRemarksAppr.Text = row["notes"].ToString();
                }
                obj = ds.Tables[0].DataTableToList<EClass_Material_List>();
                ViewState["MaterialList"] = obj;
                Bind_Grid_Material();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }
        private void Bind_Grid_Material()
        {
            try
            {
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                gvMaterials.DataSource = obj;
                gvMaterials.DataBind();
                if (obj.Count > 0)
                {
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.amount).ToString("#,##0.00;-#,##0.00;");
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        private void getReqDesc()
        {
            string comcod = GetComCode();
            string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETMATREQDESC", dgno, "", "", "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                return;
            }
            ViewState["tblreqdesc"] = ds.Tables[0];
            gvDescrip.DataSource = ds.Tables[0];
            gvDescrip.DataBind();

        }
        private void SaveReqDesc()
        {

            DataTable dt = (DataTable)ViewState["tblreqdesc"];
            for (int i = 0; i < this.gvDescrip.Rows.Count; i++)
            {
                string trmdesc = ((TextBox)this.gvDescrip.Rows[i].FindControl("txtgvDesc")).Text.Trim();
                string subj = ((TextBox)this.gvDescrip.Rows[i].FindControl("txtgvSubject")).Text.Trim();
                string remarks = ((TextBox)this.gvDescrip.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                dt.Rows[i]["termsdesc"] = trmdesc;
                dt.Rows[i]["termssubj"] = subj;
                dt.Rows[i]["termsrmrk"] = remarks;
            }
            ViewState["tblreqdesc"] = dt;

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = GetComCode();
            string reqno = lblReqNoFull.Text;
            string reqdate = txtEntryDate.Text;
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDat = Date;
            string pactcode = "1561" + ASTUtility.Right(this.lblpactcode.Value, 8);
            string flrcod = "000";
            string requsrid = "";
            string mrfno = "Dg-" + Request.QueryString["DgNo"].ToString();
            string reqnar = txtNarration.Text;
            string CRMPostedByid = userid;
            string CRMPosttrmid = Terminal;
            string CRMPostSession = Sessionid;
            string CRMPostedDat = Date;
            string checkbyid = userid;



            bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQB", reqno, reqdate, pactcode, flrcod, requsrid, mrfno, reqnar,
                PostedByid, Posttrmid, PostSession, PostedDat, CRMPostedByid, CRMPosttrmid, CRMPostSession, CRMPostedDat, "", "", "", "", "", "");

            if (result)
            {
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                int i = 1;
                List<bool> resultCompA = new List<bool>();
                foreach (var item in obj)
                {
                    string rowId = i.ToString();
                    string mRSIRCODE = item.materialId.ToString();
                    string mSPCFCOD = "000000000000";

                    double mPREQTY = Convert.ToDouble(item.quantity);
                    double mAREQTY = Convert.ToDouble("0.00");
                    double mBgdBalQty = Convert.ToDouble("0.00");
                    string mREQRAT = "0.00";      //item.rate.ToString();
                    string mREQSRAT = "0.00";     //item.rate.ToString();
                    string mPSTKQTY = "0.00";
                    string mEXPUSEDT = "";
                    string mREQNOTE = "";
                    string PursDate = "";
                    string Lpurrate = "0.00";
                    string storecode = "";
                    string ssircode = "";
                    string orderno = "";



                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQA", "",
                              reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                              PursDate, Lpurrate, storecode, ssircode, orderno, mREQSRAT, rowId, "", "", "", "", "", "");
                    resultCompA.Add(resultA);
                    i++;
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    SaveReqDesc();
                    DataTable dt1 = (DataTable)ViewState["tblreqdesc"];



                    foreach (DataRow dr in dt1.Rows)
                    {
                        string mTERMSID = dr["termsid"].ToString().Trim();
                        string mTERMSSUBJ = dr["termssubj"].ToString().Trim();
                        string mTERMSDESC = dr["termsdesc"].ToString().Trim();
                        string mTERMSRMRK = dr["termsrmrk"].ToString().Trim();
                        result = _process.UpdateTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQC", "",
                                        reqno, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "",
                                        "", "", "", "", "");

                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                            return;
                        }
                    }
                    bool resultReqNo = _process.UpdateTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEREQNODIAGNOSIS", Request.QueryString["DgNo"].ToString(),
                                        reqno, "", "", "", "", "", "", "", "",
                                        "", "", "", "", "");
                    if (!resultReqNo)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                        return;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"{reqno} is Updated Successful" + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }





        }



        private void GetProject()
        {

            string comcod = this.GetComCode();
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjectFromList", "%", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            Session["projectlist"] = ds1.Tables[0];
            ddlFromInventory.DataSource = ds1.Tables[0];
            ddlFromInventory.DataTextField = "actdesc1";
            ddlFromInventory.DataValueField = "actcode";
            ddlFromInventory.DataBind();
        }
        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetComCode();
            string ProjectCode = this.ddlFromInventory.SelectedValue.ToString().Trim();
            string FindResDesc = "%";
            string curdate = this.txtEntryDate.Text.ToString().Trim();

            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjResList", ProjectCode, curdate, FindResDesc, "", "", "", "", "", "");
            Session["projectreslist"] = ds1.Tables[0];
            ViewState["tblspcf"] = ds1.Tables[1];
            this.GetSpecification();
        }
        private void GetSpecification()
        {
            string mResCode = this.ddlMaterial.SelectedValue.ToString();
            //string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlSpecification.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = ("mspcfcod = '" + mResCode + "'");
            DataTable dt = dv1.ToTable();
            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = dt;
            this.ddlSpecification.DataBind();


        }
        private void GetMaterial()
        {
            string comcod = GetComCode();
            string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETBUDGETINFOMATREQ", dgno, "", "", "", "", "", "", "", "", "", "");
            ViewState["tblBudgetMaterial"] = ds.Tables[0];
            ddlMaterial.DataSource = ds.Tables[0];
            ddlMaterial.DataTextField = "materialdesc";
            ddlMaterial.DataValueField = "materialId";
            ddlMaterial.DataBind();
            Load_Project_Res_Combo();
        }

        protected void lnkTransferSelect_Click(object sender, EventArgs e)
        {
            ddlFromInventory.Enabled = false;
            this.SaveValue();
            string rescode = this.ddlMaterial.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlSpecification.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            DataTable dt2 = (DataTable)ViewState["tblBudgetMaterial"];
            DataRow[] projectrow1 = dt1.Select("rsircode = '" + rescode + "' and spcfcod ='" + spcfcod + "'");
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            DataRow[] projectrow3 = dt2.Select("materialId='" + rescode + "'");
            if (projectrow1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Inventory of this resource found" + "');", true);
                if (grvacc.Rows.Count == 0)
                {
                    ddlFromInventory.Enabled = true;
                }
                return;
            }
            if (projectrow3.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Budget Quantity Found" + "');", true);
                return;
            }
            
            if (projectrow2.Length == 0)
            {
                if (Convert.ToDouble(projectrow1[0]["balqty"]) < Convert.ToDouble(projectrow3[0]["quantity"]))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Balance Quantity Less than Budgeted Quantity" + "');", true);
                    return;
                }
                DataRow drforgrid = dt.NewRow();
                drforgrid["comcod"] = projectrow1[0]["comcod"];
                drforgrid["rsircode"] = projectrow1[0]["rsircode"];
                drforgrid["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                drforgrid["resdesc"] = projectrow1[0]["resdesc"];
                drforgrid["spcfdesc"] = this.ddlSpecification.SelectedItem.Text;
                drforgrid["sirunit"] = projectrow1[0]["sirunit"];
                drforgrid["qty"] = projectrow3[0]["quantity"];
                drforgrid["rate"] = projectrow1[0]["rate"];
                drforgrid["amt"] = projectrow1[0]["amt"];
                drforgrid["balqty"] = projectrow1[0]["balqty"];

                dt.Rows.Add(drforgrid);
            }
            ViewState["tblmattrns"] = dt;
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            this.grvacc.DataSource = dt1;
            this.grvacc.DataBind();

            this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "3370":
                    this.grvacc.Columns[9].Visible = false;
                    this.grvacc.Columns[10].Visible = false;
                    break;
                default:
                    this.grvacc.Columns[9].Visible = true;
                    this.grvacc.Columns[10].Visible = true;
                    break;
            }

            if (dt1.Rows.Count == 0)
                return;
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblmattrns"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;




        }
        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblmattrns"];
            DataTable dt2 = (DataTable)Session["projectreslist"];
            switch (GetComCode())
            {
                case "3370":
                    for (int i = 0; i < this.grvacc.Rows.Count; i++)
                    {
                        string rsircode = ((Label)this.grvacc.Rows[i].FindControl("lblgvMatCode")).Text.ToString();
                        string spcfcod = ((Label)this.grvacc.Rows[i].FindControl("lblgspcfcode")).Text.ToString();
                        double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());

                        DataRow[] dr3 = dt2.Select("rsircode = '" + rsircode + "' and spcfcod = '" + spcfcod + "'");
                        double rate1 = Convert.ToDouble(dr3[0]["rate"]);

                        double rat = Convert.ToDouble("0" + (rate1));
                        int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;
                        dt1.Rows[rowindex]["qty"] = qty;
                        double damt = qty * rat;
                        dt1.Rows[i]["rate"] = rat;
                        dt1.Rows[i]["amt"] = damt;
                    }
                    break;

                default:
                    for (int i = 0; i < this.grvacc.Rows.Count; i++)
                    {
                        double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                        double rat = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                        int rowindex = (this.grvacc.PageSize * this.grvacc.PageIndex) + i;
                        dt1.Rows[rowindex]["qty"] = qty;
                        double damt = qty * rat;
                        dt1.Rows[i]["rate"] = rat;
                        dt1.Rows[i]["amt"] = damt;
                    }
                    break;
            }
            ViewState["tblmattrns"] = dt1;
        }


        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void GetMatTrns()
        {

            string comcod = GetComCode();
            //string mREQNO = "NEWISS";
            string mREQNO;

            string mREQDAT = Convert.ToDateTime(this.txtEntryDate.Text.Trim()).ToString();
            //if (mREQNO == "NEWISS")
            //{
            DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", mREQDAT,
                   "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                mREQNO = ds2.Tables[0].Rows[0]["maxmtrno"].ToString();
                this.lblMTRNo.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim();
                this.lblMTRNoFull.Text = ds2.Tables[0].Rows[0]["maxmtrno"].ToString().Trim();
            }

        }
        private void GetMatTransferCode()
        {

            ViewState.Remove("tblmattrns");
            string comcod = this.GetComCode();
            string CurDate1 = this.txtEntryDate.Text.Trim();
            string mTRNNo = "NEWTRNS";
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mTRNNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmattrns"] = ds1.Tables[0];
            ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTMTRNO", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lblMTRNo.Text = ds1.Tables[0].Rows[0]["maxtrnno1"].ToString().Trim();
            this.lblMTRNoFull.Text = ds1.Tables[0].Rows[0]["maxmtrno"].ToString().Trim();


        }
        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mISUNO = lblMTRNoFull.Text;
            //string FrmPrjCode = this.ddlprjlistfrom.SelectedValue.ToString();
            //string ToPrjCode = this.ddlprjlistto.SelectedValue.ToString();
            string rsircode = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();
            string spcfcod = ((Label)this.grvacc.Rows[e.RowIndex].FindControl("lblgspcfcode")).Text.Trim();

            if (e.RowIndex == 0)
            {
                ddlFromInventory.Enabled = true;
            }
            bool result = _process.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELMTRREQNO", mISUNO, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmattrns");
            ViewState["tblmattrns"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Project_Res_Combo();
            GetSpecification();
        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            mtrReqEntrySave();
        }

        private void GetToInventoryCode()
        {
            DataTable dt = (DataTable)ViewState["DetailInfo"];
            string pactcodecalc = dt.Rows[0]["pactcode"].ToString();
            string pactcode = "16" + ASTUtility.Right(pactcodecalc, 10);
            string comcod = GetComCode();
            string toInventory = pactcode;
            DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GET_AR_To_WIP", toInventory,
                 "", "", "", "", "", "", "", "");
            if (ds2 != null || ds2.Tables[0].Rows.Count>=0)
            {
                lblToInventory.Text = ds2.Tables[0].Rows[0]["actcode"].ToString();
            }
            else
            {
                lblToInventory.Text = "";
            }
        }

        private string createWIP()
        {
            DataTable dt = (DataTable)ViewState["DetailInfo"];
            string pactcode = "";
            string pactdesc = "";
            string comcod = this.GetComCode();
            if (dt != null)
            {
                string pactcodecalc = dt.Rows[0]["pactcode"].ToString();

                pactcode = "16" + ASTUtility.Right(pactcodecalc, 10);
                pactdesc = (dt.Rows[0]["pactdesc"].ToString()).Replace("ARC-", "WIPC-");
                bool result = _process.UpdateTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "INSERT_AR_TO_WIP", pactcode, pactdesc, "2", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    return pactcode;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        private void mtrReqEntrySave()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string msg1 = "";

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
           
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["tblmattrns"];
            string mtreqdat = this.txtEntryDate.Text.ToString().Trim();

            this.GetMatTrns();
            string mtreqno = lblMTRNoFull.Text;
            string mtrref = Request.QueryString["DgNo"] == null ? ddlDgNo.SelectedValue : Request.QueryString["DgNo"].ToString();
            string mtrnar = "";
            string fromprj = this.ddlFromInventory.SelectedValue.ToString().Trim();
            string toprj = lblToInventory.Text=="" ? createWIP(): lblToInventory.Text;
            if (toprj == "")
            {
                msg1 = "Issue with WIP Code Creation";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            dr1 = dt.Select("balqty<qty");
            if ((comcod == "3367") && ASTUtility.Left(fromprj, 2) == "11")
            {
            }
            else
            {
                if (dr1.Length > 0)
                {
                    msg1 = "Not Within the Balance";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                    return;
                }
            }
            if (mtrref.Length == 0)
            {
                msg1 = "MTRF No Should Not Be Empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            DataRow[] dr2 = dt.Select("qty=0.00");
            if (dr2.Length > 0)
            {
                msg1 = "Please Fillup Qtuantity  ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            switch (comcod)
            {
                case "3101":
                case "3340":
                case "3315":
                case "3316":
                case "1108":
                case "1109":
                case "1205":
                case "3351":
                case "3352":
                case "3348":
                    DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "CHECKEDDUPMATREQREF", mtrref, "", "", "", "", "", "", "", "");

                    if (ds2.Tables[0].Rows.Count == 0)
                    {

                    }
                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("mtrref ='" + mtrref + "'");
                        DataTable dt1 = dv1.ToTable();
                        if (dt1.Rows.Count == 0)
                            ;
                        else
                        {
                            msg1 = "Found Duplicate MTRF. No !!!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                            return;
                        }
                    }
                    break;
            }
            string reqno = "";
            string reqApproval = "Approved";

            bool result = _process.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQB", mtreqno, mtreqdat, fromprj, toprj, mtrref, mtrnar, PostedByid, Posttrmid,

                           PostSession, Posteddat, reqno, reqApproval, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                msg1 = _process.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {

                string trsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string tqty = dr["qty"].ToString().Trim();
                string trate = dr["rate"].ToString().Trim();
                string tamt = dr["amt"].ToString().Trim();
                // string reqno = dr["reqno"].ToString().Trim();


                //if (dr["qty"] <= 0)
                //{

                //}

                bool result1 = _process.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTREQ", "PURMTREQA", mtreqno, trsircode, spcfcod, tqty,
                   tamt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result1)
                {
                    msg1 = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);

                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //  string trsircode=dt.Rows[i]["rsircode"].ToString().Trim();
            //  string tunit=dt.Rows[i]["sirunit"].ToString().Trim();
            //  string tqty=dt.Rows[i]["qty"].ToString().Trim();
            //  string trate=dt.Rows[i]["rate"].ToString().Trim();
            //  string tamt = dt.Rows[i]["amt"].ToString().Trim();

            //  bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UpdateTransferInf", transno, fromprj, toprj, trsircode,
            //      tunit, tqty, trate, tamt, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, "");
            //}

            msg1 = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg1 + "');", true);

            this.txtEntryDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Transfer";
                string eventdesc = "Update New QTY & RATE";
                string eventdesc2 = "From " + this.ddlFromInventory.SelectedItem.ToString() + " To " + toprj + " - " + mtreqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ddlFromInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaterial();
        }
    }
}