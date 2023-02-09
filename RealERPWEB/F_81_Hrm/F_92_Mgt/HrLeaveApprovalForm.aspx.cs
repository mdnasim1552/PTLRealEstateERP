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
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{

    public partial class HrLeaveApprovalForm : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

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
                //((Label)this.Master.FindControl("lblTitle")).Text = "HR Approval Setup";

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.GetDepartment();
                this.viewType.SelectedIndex = 0;

                if (this.viewType.SelectedIndex == 0)
                {
                    this.pnlLeave.Visible = true;
                    this.pnlLoan.Visible = false;
                }
                else
                {
                    this.pnlLeave.Visible = false;
                    this.pnlLoan.Visible = true;
                }
                getAllLoanPerm();
                pnlLoanUser();
                getLoanStep();
                //this.GetEmployeeName();\
                this.viewType_SelectedIndexChanged(null, null);

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public void LoadOrderDapp()
        {
            string comcod = GetCompCode();
            string dptName = (this.ddldpt.SelectedValue.ToString() == "000000000000") ? "%%" : this.ddldpt.SelectedValue.ToString();

            string typrole = this.ddlTypeRole.SelectedValue.ToString();


            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SELECTLEAVEAPP", dptName, typrole, "", "", "", "", "", "", "");
            DataTable UserInfoTable = ds.Tables[0];
            ViewState["UserInfoTable"] = UserInfoTable;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetDepartment()
        {

            string comcod = GetCompCode();
            string txtCompanyname = "94%";
            string txtSearchDept = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETALLDEPT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddldpt.DataTextField = "actdesc";
            this.ddldpt.DataValueField = "actcode";
            this.ddldpt.DataSource = ds1.Tables[0];
            this.ddldpt.DataBind();
            this.ddldpt.Items.Insert(0, new ListItem("--Set for all Department--", "000000000000"));

            ViewState["tblAllDpt"] = ds1.Tables[0];


            ds1.Dispose();
        }

        private void Getuser()
        {

            string comcod = GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERNAMELIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }






        protected void Select_Click(object sender, EventArgs e)
        {
            string userName = ddlUserList.SelectedItem.Text;
            string usrid = ddlUserList.SelectedItem.Value;
            string comcod = GetCompCode();
            string roletype = this.ddlTypeRole.SelectedValue.ToString();

            string centrid = this.ddldpt.SelectedValue.ToString();
            string actdesc = this.ddldpt.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            if (centrid != "000000000000")
            {
                DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = UserInfoTable.NewRow();
                    dr1["usrid"] = usrid;
                    dr1["usrsname"] = userName;
                    dr1["usrsname"] = userName;
                    dr1["centrid"] = centrid;
                    dr1["actdesc"] = actdesc;
                    dr1["roletype"] = roletype;
                    dr1["comcod"] = comcod;
                    dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                    UserInfoTable.Rows.Add(dr1);
                }
                else
                {
                    string Messaged = "Already Added !!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

                    return;
                }
            }
            else
            {
                DataTable dt = (DataTable)ViewState["tblAllDpt"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    centrid = dt.Rows[i]["actcode"].ToString();

                    DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "' and centrid='" + centrid + "'");
                    if (dr.Length == 0)
                    {
                        DataRow dr1 = UserInfoTable.NewRow();
                        dr1["usrid"] = usrid;
                        dr1["usrsname"] = userName;
                        dr1["centrid"] = dt.Rows[i]["actcode"];
                        dr1["actdesc"] = dt.Rows[i]["actdesc"];
                        dr1["comcod"] = comcod;
                        dr1["roletype"] = roletype;
                        dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                        UserInfoTable.Rows.Add(dr1);
                    }
                }

            }


            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }
        protected void SelectAll_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode(); ;
            string centrid = this.ddldpt.SelectedValue.ToString();
            string actdesc = this.ddldpt.SelectedItem.Text;
            string roletype = this.ddlTypeRole.SelectedValue.ToString();

            if (centrid == "000000000000")
            {
                string Messaged = "Plese Select Department !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

                return;
            }

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            for (int i = 0; i < this.ddlUserList.Items.Count; i++)
            {
                string usrid = this.ddlUserList.Items[i].Value;
                string userName = this.ddlUserList.Items[i].Text;
                DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = UserInfoTable.NewRow();
                    dr1["usrid"] = usrid;
                    dr1["usrsname"] = userName;
                    dr1["centrid"] = centrid;
                    dr1["actdesc"] = actdesc;
                    dr1["comcod"] = comcod;
                    dr1["roletype"] = roletype;

                    dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                    UserInfoTable.Rows.Add(dr1);
                }
            }


            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }





        protected void gvProLinkInfo_DataBind()
        {
            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            this.gvProLinkInfo.DataSource = UserInfoTable;
            this.gvProLinkInfo.DataBind();

        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            string Messaged = "";
            string typrole = this.ddlTypeRole.SelectedValue.ToString();
            string dpt = this.ddldpt.SelectedValue.ToString();
            string comcod = GetCompCode(); ;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            if (dpt != "000000000000")
            {
                if (!IsValid(UserInfoTable))
                {
                    Messaged = "Invalid Serial No !!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }

                if (HasDup(UserInfoTable))
                {
                    Messaged = "Duplicate Serial No !!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }

                bool result = false;
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", dpt, typrole, "", "", "", "", "", "", "", "", "", "", "");

                for (int i = 0; i < UserInfoTable.Rows.Count; i++)
                {

                    //string userid = UserInfoTable.Rows[i]["usrid"].ToString();
                    //dpt = ((Label)gvProLinkInfo.Rows[i].FindControl("lblcentrid")).Text;
                    //typrole = ((Label)gvProLinkInfo.Rows[i].FindControl("lbltyprole")).Text;
                    string userid = ((Label)gvProLinkInfo.Rows[i].FindControl("userid")).Text;
                    string slNum = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTLEAVEAPP", slNum, userid, dpt, typrole, "", "", "", "", "", "", "", "", "", "", "");


                }
                if (!result)
                {
                    Messaged = "Update Failed !!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }
            }
            else
            {
                bool result = false;
                // result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", dpt, typrole, "", "", "", "", "", "", "", "", "", "", "");

                for (int i = 0; i < UserInfoTable.Rows.Count; i++)
                {

                    //string userid = UserInfoTable.Rows[i]["usrid"].ToString();
                    dpt = ((Label)gvProLinkInfo.Rows[i].FindControl("lblcentrid")).Text;
                    typrole = ((Label)gvProLinkInfo.Rows[i].FindControl("lbltyprole")).Text;
                    string userid = ((Label)gvProLinkInfo.Rows[i].FindControl("userid")).Text;
                    string slNum = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTLEAVEAPP", slNum, userid, dpt, typrole, "", "", "", "", "", "", "", "", "", "", "");


                }
                if (!result)
                {
                    Messaged = "Update Failed !!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

                    return;
                }
            }



            Messaged = "Data Updated successfully  !!";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);



        }
        public bool IsValid(DataTable UserInfoTable)
        {
            int slNum = 0;
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                try
                {
                    slNum = Convert.ToInt32(((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text);
                }
                catch (Exception)
                {
                    return false;
                }


            }

            return true;
        }
        public bool IsUnique(DataTable UserInfoTable, string str, int index)
        {

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (str == temp && index != i)
                    return false;
            }

            return true;
        }

        public bool HasDup(DataTable UserInfoTable)
        {
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (!IsUnique(UserInfoTable, temp, i))
                    return true;
            }
            return false;
        }


        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            //string centrid = this.ddldpt.SelectedValue.ToString();
            string typrole = this.ddlTypeRole.SelectedValue.ToString();

            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string userid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("userid")).Text.Trim();
            string centrid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("lblcentrid")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELLEAVEAPP", userid, centrid, typrole, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ViewState["UserInfoTable"] = dt;
                gvProLinkInfo_DataBind();
            }
            BindGrid();
        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string centrid = this.ddldpt.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddldpt.SelectedValue.ToString();
            string typrole = this.ddlTypeRole.SelectedValue.ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", centrid, typrole, "", "", "", "", "", "", "", "", "", "", "", "", "");
            BindGrid();
        }


        //LinkButton4

        protected void GetcntresByNam(object sender, EventArgs e)
        {
            //GetCentres();
            lbtnOkOrNew.Visible = true;


        }
        protected void GetUserInfo(object sender, EventArgs e)
        {
            if (lbtnOkOrNew.Text == "Ok")
            {
                if (ddldpt.Items.Count > 0)
                {
                    this.Panel2.Visible = true;
                    lbtnOkOrNew.Text = "New";
                    this.LoadOrderDapp();
                    ddldpt.Enabled = false;
                    ddlTypeRole.Enabled = false;
                    //ddlEmploye.Enabled = false;

                    BindGrid();
                    this.Getuser();

                }

                else
                {
                    ddlTypeRole.Enabled = true;

                    ddldpt.Enabled = true;
                    //ddlEmploye.Enabled = true;
                    this.Panel2.Visible = false;
                }
                return;
            }
            if (lbtnOkOrNew.Text == "New")
            {
                ddldpt.Enabled = true;
                ddlTypeRole.Enabled = true;

                //ddlEmploye.Enabled = true;

                lbtnOkOrNew.Text = "Ok";
                this.Panel2.Visible = false;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
        }
        protected void BindGrid()
        {
            LoadOrderDapp();
            gvProLinkInfo_DataBind();
        }




        //panel loan 
        private void getAllLoanPerm()
        {
            string comcod = GetCompCode();


            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETPERMISSIONSTEP", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                this.gvLoanStep.DataSource = null;
                this.gvLoanStep.DataBind();
                this.DataBind();
                return;
            }
            else
            {
                this.gvLoanStep.DataSource = ds1.Tables[0];
                this.gvLoanStep.DataBind();
                this.DataBind();
                return;
            }
                
    
        }



        protected void saveBtnLn_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            string stepid= this.ddlLoanStep.SelectedValue.ToString();
           string userid = this.ddlLoanUser.SelectedValue.ToString();
            string Messaged = "";
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOANSTEP", stepid, userid, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                Messaged = "Data Inserted successfully  !!";
                this.getAllLoanPerm();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
            }
            else
            {
                Messaged = "Failed !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }


        }

        private void pnlLoanUser()
        {

            string comcod = GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERNAMELIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;

            this.ddlLoanUser.DataTextField = "usrsname";
            this.ddlLoanUser.DataValueField = "usrid";
            this.ddlLoanUser.DataSource = ds1.Tables[0];
            this.ddlLoanUser.DataBind();
            ds1.Dispose();
        }


        private void getLoanStep()
        {
            string comcod = GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANSTEP", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                return;

            this.ddlLoanStep.DataTextField = "stepname";
            this.ddlLoanStep.DataValueField = "id";
            this.ddlLoanStep.DataSource = ds1.Tables[0];
            this.ddlLoanStep.DataBind();
            ds1.Dispose();
        }

        protected void viewType_SelectedIndexChanged(object sender, EventArgs e)
        {
          int index=  this.viewType.SelectedIndex;
        
            switch (index)
            {
                case 0:
                    this.pnlLeave.Visible = true;
                    this.pnlLoan.Visible = false;
                    break;
                case 1:
                    this.pnlLeave.Visible = false;
                    this.pnlLoan.Visible = true;
                    break;

            }
           
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string pid = ((Label)this.gvLoanStep.Rows[index].FindControl("lblLid")).Text.ToString();
            string comcod = GetCompCode();
            string Messaged = "";

            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "DELETELOANSTEP", pid, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                Messaged = "Data Deleted successfully  !!";
                this.getAllLoanPerm();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
            }
            else
            {
                Messaged = "Failed !!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }
        }
    }
}