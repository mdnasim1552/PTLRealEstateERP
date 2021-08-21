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
//using  RealERPRPT;
using RealEntity.C_47_Kpi;
namespace RealERPWEB.F_39_MyPage
{
    public partial class EmpKpiEntry04Det : System.Web.UI.Page
    {

        BL_EntryKpi objUser = new BL_EntryKpi();
        ProcessAccess KpiData = new ProcessAccess();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "KPI ENTRY";
                string DayID = this.Request.QueryString["dayid"].ToString();
                int year = Convert.ToInt16(ASTUtility.Left(DayID, 4));
                int month = Convert.ToInt16(DayID.Substring(4, 2));
                int day = Convert.ToInt16(ASTUtility.Right(DayID, 2));
                DateTime date = new DateTime(year, month, day);
                this.txtdate.Text = date.AddDays(-2).ToString("dd-MMM-yyyy");
                this.showGvEmpwlist();
            }
        }
        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void showGvEmpwlist()
        {
            string comcod = Getcomcod();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = this.Request.QueryString["empid"].ToString();
            string DayID = this.Request.QueryString["dayid"].ToString();
            string DptCode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EntryKpi> lst3 = objUser.EntryKPIWorkList(DptCode, DayID, Empid);
            ViewState["tblEntry"] = lst3;
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string DayID = this.Request.QueryString["dayid"].ToString();
            int year = Convert.ToInt16(ASTUtility.Left(DayID, 4));
            int month = Convert.ToInt16(DayID.Substring(4, 2));
            int day = Convert.ToInt16(ASTUtility.Right(DayID, 2));
            DateTime oldDate = new DateTime(year, month, day);

            this.gvEmpWlistEntry.Columns[2].HeaderText = "Work List of <span class='red'>" + oldDate.ToString("dd-MMM-yyyy dddd") + "</span>";
            List<RealEntity.C_47_Kpi.EntryKpi> lst = (List<RealEntity.C_47_Kpi.EntryKpi>)ViewState["tblEntry"];
            this.gvEmpWlistEntry.DataSource = lst;
            this.gvEmpWlistEntry.DataBind();

        }
        private List<RealEntity.C_47_Kpi.EntryKpi> HiddenSameData(List<RealEntity.C_47_Kpi.EntryKpi> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string actcode1 = "";
            foreach (RealEntity.C_47_Kpi.EntryKpi c1 in lst3)
            {
                if (i == 0)
                {
                    actcode1 = c1.actcode1;
                    i++;
                    continue;

                }
                else if (c1.actcode1 == actcode1)
                {
                    c1.actdesc1 = "";
                }
                actcode1 = c1.actcode1;

            }

            return lst3;

        }

        protected void Save_Value()
        {
            try
            {

                List<RealEntity.C_47_Kpi.EntryKpi> lst = (List<RealEntity.C_47_Kpi.EntryKpi>)ViewState["tblEntry"];

                int index;
                for (int j = 0; j < this.gvEmpWlistEntry.Rows.Count; j++)
                {
                    //double Targetwork = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvEmpWlistEntry.Rows[j].FindControl("txtGvtarget")).Text.Trim()));
                    double Actqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvEmpWlistEntry.Rows[j].FindControl("txtQtyWork")).Text.Trim()));
                    string Note = ((TextBox)this.gvEmpWlistEntry.Rows[j].FindControl("txtgbnote")).Text.Trim();
                    string Remarks = ((TextBox)this.gvEmpWlistEntry.Rows[j].FindControl("txtRemarks")).Text.Trim();
                    index = (this.gvEmpWlistEntry.PageIndex) * this.gvEmpWlistEntry.PageSize + j;
                    // lst[index].wQty = Targetwork;
                    lst[index].acqty = Actqty;
                    lst[index].note = Note;
                    lst[index].remarks = Remarks;
                }


                ViewState["tblEntry"] = lst;
            }

            catch (Exception ex)
            {

            }


        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            //this.lblmsg.Visible = true;
            ((Label)this.gvEmpWlistEntry.FooterRow.FindControl("lblmsg")).Visible = true;

            string comcod = this.Getcomcod();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            this.Save_Value();
            List<RealEntity.C_47_Kpi.EntryKpi> lst = (List<RealEntity.C_47_Kpi.EntryKpi>)ViewState["tblEntry"];
            string Empid = this.Request.QueryString["empid"].ToString();
            string DayID = this.Request.QueryString["dayid"].ToString();
            string DptCode = hst["deptcode"].ToString();
            bool result;

            foreach (RealEntity.C_47_Kpi.EntryKpi c1 in lst)
            {

                string actcode = c1.actcode;
                double actQty = Convert.ToDouble(c1.acqty);
                string Note = c1.note;
                string Remarks = c1.remarks;

                if (actQty != 0 || Note.Length != 0 || Remarks.Length != 0)
                {

                    result = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY_02", "INSERTUPDATEWORKLIST", DayID, DptCode, Empid, actcode, DayID, actQty.ToString(), Note, Remarks, "", "", "", "", "", "", "");

                    if (!result)
                    {
                        ((Label)this.gvEmpWlistEntry.FooterRow.FindControl("lblmsg")).Text = "Updated Fail";
                        return;
                    }
                }
            }
            ((Label)this.gvEmpWlistEntry.FooterRow.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "KPI Information";
                string eventdesc = "Update KPI Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvEmpWlistEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label mdesc = (Label)e.Row.FindControl("lblppercent");

                double ppercent = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ppercent"));

                if (ppercent == 0)
                {
                    return;
                }

                if (ppercent < 50)
                {
                    mdesc.Style.Add("color", "red");
                }


            }
        }
        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {

            this.pnlCopy.Visible = (this.chkcopy.Checked);
        }
        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblEntry");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string deptcode = hst["deptcode"].ToString();
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string Empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EntryKpi> lst3 = objUser.EntryKPIWorkList(deptcode, dayid, Empid);
            ViewState["tblEntry"] = lst3;
            this.Data_Bind();
            this.chkcopy.Checked = false;
            this.chkcopy_CheckedChanged(null, null);


        }
    }
}