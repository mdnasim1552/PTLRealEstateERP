using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_21_MKT
{
    public partial class RptCrmNeedBase : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Need Base";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                GetEmployee();
                GetAllSubdata();
                SelectView();
               
            }


        }
        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            string filter = comcod == "3374" ? "namdesgsec" : "";
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", filter, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ViewState["tblcompany"] = ds2.Tables[3];
            ds2.Dispose();

            // get occution list//
            DataView Dv1 = new DataView();
            DataTable dt = new DataTable();
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '86%'");
             dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlOccupation.DataTextField = "gdesc";
            DdlOccupation.DataValueField = "gcod";
            DdlOccupation.DataSource = dt;
            DdlOccupation.DataBind();
            DdlOccupation.SelectedValue = "0000000";

            // get Location list//
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '89%'");
            dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlLocation.DataTextField = "gdesc";
            DdlLocation.DataValueField = "gcod";
            DdlLocation.DataSource = dt;
            DdlLocation.DataBind();
            DdlLocation.SelectedValue = "0000000";

            // get Category list//
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '32%'");
            dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlCategory.DataTextField = "gdesc";
            DdlCategory.DataValueField = "gcod";
            DdlCategory.DataSource = dt;
            DdlCategory.DataBind();
            DdlCategory.SelectedValue = "0000000";

            // get Apartment Size list//
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '33%'");
            dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlAptSize.DataTextField = "gdesc";
            DdlAptSize.DataValueField = "gcod";
            DdlAptSize.DataSource = dt;
            DdlAptSize.DataBind();
            DdlAptSize.SelectedValue = "0000000";

            // get Main Source list//
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '29%'");
            dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlSource.DataTextField = "gdesc";
            DdlSource.DataValueField = "gcod";
            DdlSource.DataSource = dt;
            DdlSource.DataBind();
            DdlSource.SelectedValue = "0000000";
            this.DdlSource_SelectedIndexChanged(null,null);
            // get Lead Stage list//
            Dv1 = ds2.Tables[0].DefaultView;
            Dv1.RowFilter = ("gcod like '95%'");
            dt = Dv1.ToTable();
            dt.Rows.Add("0000000", "--All--", "");
            DdlStage.DataTextField = "gdesc";
            DdlStage.DataValueField = "gcod";
            DdlStage.DataSource = dt;
            DdlStage.DataBind();
            DdlStage.SelectedValue = "0000000";

            // get current company all project list//
            Dv1 = ds2.Tables[2].DefaultView;
            Dv1.RowFilter = ("comcod='" + comcod + "'");
            dt = Dv1.ToTable();           
         //   dt.Rows.Add("000000000000", "--All--", "");
            DdlProjec.DataTextField = "pactdesc";
            DdlProjec.DataValueField = "pactcode";
            DdlProjec.DataSource = dt;
            DdlProjec.DataBind();
            DdlProjec.SelectedValue = "";


        }
        private void GetEmployee()
        {
            Session.Remove("tblircapro");
            string comcod = this.GetComeCode();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GET_IR_EMPLOYEE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblircapro"] = ds1.Tables[0];
            DataTable dt = ds1.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter= "empid like '93%'";
            dv.Sort = ("empid");

            dt = dv.ToTable();
            DataRow dr1 = dt.NewRow();
            dr1["empid"] = "000000000000";
            dr1["empname"] = "None";
            dt.Rows.Add(dr1);

            DdlEmployee.DataTextField = "empname";
            DdlEmployee.DataValueField = "empid";
            DdlEmployee.DataSource = dt;
            DdlEmployee.DataBind();
            DdlEmployee.SelectedValue = "000000000000";
            ds1.Dispose();


        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void SelectView()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetComeCode();
            switch (Type)
            {

                case "Report":
                    GridSummary();
                    this.Multiview.ActiveViewIndex = 0;
                    switch (comcod)
                    {
                        case "3354"://Edison
                        case "3101"://PTL
                            this.gvSummary.Columns[15].HeaderText = "Sub Source";
                            break;

                        default:
                            break;
                    }
                    break;

                case "RptStd":
                    //this.txtFromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //this.TxtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                  //  GetStdNeedBaseData();
                    this.Multiview.ActiveViewIndex = 1;
                    break;

            }
        }
        private void GridSummary()
        {
            string comcod = this.GetComeCode();
            string Empid = "%";
            string Country = "%";
            string Dist = "%";
            string Zone = "%";
            string PStat = "%";
            string Area = "%";
            string Block = "%";            
            string Pri = "%";
            string Status = "%";
            string Other = "9";
            string TxtVal = "%";
            string srchempid = "%";           
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string mgt = "Management";
           

          

            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                 Pri, Status, Other, TxtVal, todate, srchempid, mgt);


            // DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
            //Pri, Status, Other, TxtVal, todate, srchempid);


            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();


            Session["tblsummData"] = ds3.Tables[0];
            this.dataBindGV();
         
        }


        private void GetStdNeedBaseData()
        {
            string comcod = this.GetComeCode();
            string leadid = this.TxtLeadId.Text.ToString()+"%";
            ((TextBox)this.TxtLeadId).BorderColor = (this.TxtLeadId.Text.Length > 0) ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty;

            string custname = this.TxtCustName.Text.ToString() + "%";       
            ((TextBox)this.TxtCustName).BorderColor = (this.TxtCustName.Text.Length > 0)? System.Drawing.Color.OrangeRed: System.Drawing.Color.Empty;
            
            string mobile = this.TxtMobile.Text.ToString() + "%";
            ((TextBox)this.TxtMobile).BorderColor = (this.TxtMobile.Text.Length > 0) ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; 

            string Email = this.TxtEmail.Text.ToString() + "%";
            ((TextBox)this.TxtEmail).BorderColor = (this.TxtEmail.Text.Length > 0) ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty;

            string org = this.TxtOrg.Text.ToString() + "%"; 
            ((TextBox)this.TxtOrg).BorderColor = (this.TxtOrg.Text.Length > 0) ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty;

            string Block = "%";
            string Pri = "%";
            string Status = "%";
            string Other = "9";
            string TxtVal = "%";
            string srchempid = "%";
            string LeadStatus = (this.DdlStage.SelectedValue.ToString() == "0000000") ? "%" : this.DdlStage.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlStage).BorderColor = (LeadStatus != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string apptsize =(this.DdlAptSize.SelectedValue.ToString()=="0000000")?"%": this.DdlAptSize.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlAptSize).BorderColor = (apptsize != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string category = (this.DdlCategory.SelectedValue.ToString() == "0000000") ? "%" : this.DdlCategory.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlCategory).BorderColor = (category != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string profecode =(this.DdlOccupation.SelectedValue.ToString()=="0000000")?"%": this.DdlOccupation.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlOccupation).BorderColor = (profecode != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string areacode = (this.DdlLocation.SelectedValue.ToString() == "0000000") ? "%" : this.DdlLocation.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlLocation).BorderColor = (areacode != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string projectcod = (this.DdlProjec.SelectedValue.ToString() == "000000000000") ? "%" : this.DdlProjec.SelectedValue.ToString() + "%";
            ((DropDownList)this.DdlProjec).BorderColor = (projectcod != "%") ? System.Drawing.Color.OrangeRed : System.Drawing.Color.Empty; ;

            string fromdate =(this.txtFromdate.Text.Length==0)?"01-Jan-1900": Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = (this.TxtToDate.Text.Length == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.TxtToDate.Text).ToString("dd-MMM-yyyy");
            string mgt = "Management";
            string subsource = (this.DdlSubSource.SelectedValue.ToString() == "0000000") ? "%" : this.DdlSubSource.SelectedValue.ToString() + "%";
            string employee = (this.DdlEmployee.SelectedValue.ToString() == "000000000000") ? "%" : this.DdlEmployee.SelectedValue.ToString() + "%";
            string visitstatus = (this.DdlVisitSource.SelectedValue.ToString().Length == 0) ? "%" : this.DdlVisitSource.SelectedValue.ToString();

            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE02", 
                "GET_CLIENT_NEED_BASE_REPORT", null, null, null, "8301%", leadid, custname, mobile, Email, org, profecode, areacode,
                 category, LeadStatus, apptsize, projectcod, fromdate, todate, subsource, employee, visitstatus);
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvNedBseDetails.DataSource = null;
                this.gvNedBseDetails.DataBind();
                return;
            }

            Session["tblsummData"] = ds3.Tables[0];
            this.dataBindGV();

        }
        private void dataBindGV()
        {
            DataTable dt = (DataTable)Session["tblsummData"];
            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("active='True'");

            string Type = this.Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "Report":
                    this.gvSummary.DataSource = dv1.ToTable();
                    this.gvSummary.DataBind();
                    this.Excel_Bind();
                    break;
                case "RptStd":
                    this.gvNedBseDetails.DataSource = dv1.ToTable();
                    this.gvNedBseDetails.DataBind();
                    
                    break;
            }

        }

        private void Excel_Bind()
        {

            DataTable dt = (DataTable)Session["tblsummData"];
            if (dt.Rows.Count == 0)
                return;

            Session["Report1"] = gvSummary;
            this.hlbtntbCdataExcel.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            

        }



        protected void lnkgvHeader_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = (DataTable)ViewState["tblHeaderCheck"];
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");


            for (int i = 0; i < gvSummary.Rows[0].Cells.Count; i++)
            {
                string headerRowText = gvSummary.HeaderRow.Cells[i].Text;

                if (gvSummary.Columns[i].Visible == false)
                {
                    DataRow dr = dt1.NewRow();
                    if (headerRowText == "Code")
                    {
                    }
                    else
                    {
                        if (headerRowText != "")
                        {
                            dr["gcod"] = i;
                            dr["gvalue"] = headerRowText;
                            dt1.Rows.Add(dr);
                        }

                    }

                }

            }
            this.gvCurrent.DataSource = dt1;
            this.gvCurrent.DataBind();
            this.gvPrev.DataSource = dt2;
            gvPrev.DataBind();


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "OpenGvModal();", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "onchangetrigger();", true);
        }
        protected void lnkgvListShow_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");
            for (int i = 0; i < gvCurrent.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvCurrent.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvCurrent.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvCurrent.Rows[i].FindControl("chkgv")).Checked;
                if (chkbox)
                {
                    this.gvSummary.Columns[index].Visible = true;
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            for (int i = 0; i < gvPrev.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvPrev.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvPrev.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvPrev.Rows[i].FindControl("chkgv")).Checked;
                if (!chkbox)
                {
                    this.gvSummary.Columns[index].Visible = false;
                }
                else
                {
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            this.Excel_Bind();
            ViewState["tblHeaderCheck"] = dt1;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "onchangetrigger();", true);
        }

        protected void LbtnSearch_Click(object sender, EventArgs e)
        {
            this.GetStdNeedBaseData();
        }

        protected void LbtnStageReset_Click(object sender, EventArgs e)
        {
            this.DdlStage.SelectedValue = "0000000";
        }

        protected void LbtnResetCustName_Click(object sender, EventArgs e)
        {
            this.TxtCustName.Text = "";
            ((TextBox)this.TxtCustName).BorderColor = System.Drawing.Color.Empty;

        }

        protected void DdlSource_SelectedIndexChanged(object sender, EventArgs e)
        {

                string source =(this.DdlSource.SelectedValue.ToString()=="0000000")?"%": this.DdlSource.SelectedValue.ToString();
  
                DataView dv5;
                dv5 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                dv5.RowFilter = ("gcod like '31%' and code like '"+ source + "'");
                DataTable dt = dv5.ToTable();
                dt.Rows.Add("0000000", "--All--", "");
                DdlSubSource.DataTextField = "gdesc";
                DdlSubSource.DataValueField = "gcod";
                DdlSubSource.DataSource = dt;
                DdlSubSource.DataBind();
                DdlSubSource.SelectedValue = "0000000";






        }

        protected void lbtnresetsubsource_Click(object sender, EventArgs e)
        {
            this.DdlSubSource.SelectedValue = "0000000";
        }

        protected void LbtnResetSource_Click(object sender, EventArgs e)
        {
            this.DdlSource.SelectedValue = "0000000";

        }

        protected void LbtnResetAptsize_Click(object sender, EventArgs e)
        {
            this.DdlAptSize.SelectedValue = "0000000";

        }

        protected void LbtnResetPrj_Click(object sender, EventArgs e)
        {
            this.DdlProjec.SelectedValue = "";

        }

        protected void LbtnLocation_Click(object sender, EventArgs e)
        {
            this.DdlLocation.SelectedValue = "0000000";

        }

        protected void LbtnResetCate_Click(object sender, EventArgs e)
        {
            this.DdlCategory.SelectedValue = "0000000";

        }

        protected void LbtnResetOrg_Click(object sender, EventArgs e)
        {
            this.TxtOrg.Text = "";
            ((TextBox)this.TxtOrg).BorderColor = System.Drawing.Color.Empty;

        }

        protected void LbtnResetEmail_Click(object sender, EventArgs e)
        {
            this.TxtEmail.Text = "";
            ((TextBox)this.TxtEmail).BorderColor = System.Drawing.Color.Empty;

        }

        protected void LbtnResetMobile_Click(object sender, EventArgs e)
        {
            this.TxtMobile.Text = "";
            ((TextBox)this.TxtMobile).BorderColor = System.Drawing.Color.Empty;
        }

        protected void LbtnResetLeadId_Click(object sender, EventArgs e)
        {
            this.TxtLeadId.Text = "";
            ((TextBox)this.TxtLeadId).BorderColor = System.Drawing.Color.Empty;
        }

        protected void LbtnResetEmployee_Click(object sender, EventArgs e)
        {
            this.DdlEmployee.SelectedValue = "000000000000";

        }

        protected void LbtnResetAll_Click(object sender, EventArgs e)
        {
            this.DdlSubSource.SelectedValue = "0000000";     
            this.DdlSource.SelectedValue = "0000000";        
            this.DdlAptSize.SelectedValue = "0000000";    
            this.DdlProjec.SelectedValue = "";
            this.DdlLocation.SelectedValue = "0000000";
            this.DdlCategory.SelectedValue = "0000000";         
            this.TxtOrg.Text = "";
            ((TextBox)this.TxtOrg).BorderColor = System.Drawing.Color.Empty;         
            this.TxtEmail.Text = "";
            ((TextBox)this.TxtEmail).BorderColor = System.Drawing.Color.Empty;        
            this.TxtMobile.Text = "";
            ((TextBox)this.TxtMobile).BorderColor = System.Drawing.Color.Empty;  
            this.TxtLeadId.Text = "";
            ((TextBox)this.TxtLeadId).BorderColor = System.Drawing.Color.Empty;       
            this.DdlEmployee.SelectedValue = "000000000000";
            this.DdlVisitSource.SelectedValue = "";
            this.DdlOccupation.SelectedValue = "0000000";

        }

        protected void LbtnResetOccup_Click(object sender, EventArgs e)
        {
            this.DdlOccupation.SelectedValue = "0000000";

        }

        protected void lnkgvHeader_Click1(object sender, EventArgs e)
        {
            Session["Report1"] = gvNedBseDetails;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "OpenExcelDownload();", true);

       
        }
    }
}