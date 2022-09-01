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
                    btnOKClick.Enabled = false;
                    btnOKClick.Visible = false;
                    pnlMatReq.Visible = true;
                    btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
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
                pnlMatReq.Visible = true;
                btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                getReqNo();
                getComplainUser();
                createMaterialList();
                getReqDesc();
            }
            else
            {
                pnlMatReq.Visible = false;
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
                    this.lblpactcode.Value= row["pactcode"].ToString();
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
            string PostSession = Sessionid ;
            string PostedDat = Date;
            string pactcode = "1561"+ASTUtility.Right(this.lblpactcode.Value,8);
            string flrcod = "000";
            string requsrid = "";
            string mrfno = "Dg-"+Request.QueryString["DgNo"].ToString();
            string reqnar = txtNarration.Text;
            string CRMPostedByid = userid;
            string CRMPosttrmid = Terminal;
            string CRMPostSession = Sessionid;
            string CRMPostedDat = Date;
            string checkbyid= userid;



            bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQB", reqno, reqdate, pactcode, flrcod, requsrid, mrfno, reqnar,
                PostedByid, Posttrmid, PostSession, PostedDat, CRMPostedByid, CRMPosttrmid, CRMPostSession, CRMPostedDat, "", "","","","","");
            
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
                    string mPSTKQTY ="0.00";
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
    }
}