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
using RealEntity;
namespace RealERPWEB.F_04_Bgd
{
    public partial class BgdProjectProgress : System.Web.UI.Page
    {

        UserManBudget objUser = new UserManBudget();
        ProcessAccess purData = new ProcessAccess();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PURCHASE INFORMATION EDIT";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProProgress();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetProProgress()
        {

            Session.Remove("tblproprogress");

            List<RealEntity.C_04_Bgd.EClassProProgress> lst = new List<RealEntity.C_04_Bgd.EClassProProgress>();
            lst = objUser.GetProProgress();
            Session["tblproprogress"] = lst;
            //this.DataSet = lst;
            this.Data_Bind();



        }








        private void Data_Bind()
        {
            try
            {
                List<RealEntity.C_04_Bgd.EClassProProgress> lst = (List<RealEntity.C_04_Bgd.EClassProProgress>)Session["tblproprogress"];
                //  List<RealEntity.C_34_Mgt.EClassReqInfo> lst =this.DataSet;
                this.gvProProgress.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue);
                this.gvProProgress.DataSource = lst;
                this.gvProProgress.DataBind();





            }
            catch (Exception ex)
            {


            }


        }
        private void SaveValue()
        {

            List<RealEntity.C_04_Bgd.EClassProProgress> lst = (List<RealEntity.C_04_Bgd.EClassProProgress>)Session["tblproprogress"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProProgress.Rows.Count; j++)
            {


                string catagory = ((TextBox)this.gvProProgress.Rows[j].FindControl("txtgvcatagory")).Text.Trim();
                double conprogress = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvProProgress.Rows[j].FindControl("txtgvconprogress")).Text.Trim()));
                string comdate = ((TextBox)this.gvProProgress.Rows[j].FindControl("txtgvcomdate")).Text.Trim();

                TblRowIndex2 = (this.gvProProgress.PageIndex) * this.gvProProgress.PageSize + j;
                lst[TblRowIndex2].conprogress = conprogress;
                lst[TblRowIndex2].comdate = comdate;
                lst[TblRowIndex2].catagory = catagory;

            }
            Session["tblproprogress"] = lst;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvEditPur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProProgress.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            string comcod = this.GetCompCode();

            List<RealEntity.C_04_Bgd.EClassProProgress> lst = (List<RealEntity.C_04_Bgd.EClassProProgress>)Session["tblproprogress"];
            bool result = true;
            foreach (RealEntity.C_04_Bgd.EClassProProgress c1 in lst)
            {
                string pactcode = c1.pactcode;
                string catagory = c1.catagory;
                string conprogress = Convert.ToDouble(c1.conprogress).ToString();
                string comdate = (c1.comdate == "") ? "" : Convert.ToDateTime(c1.comdate).ToString("dd-MMM-yyyy");




                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, "01004", "D", comdate, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, "01007", "N", conprogress, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, "01009", "T", catagory, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }


            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
    }
}