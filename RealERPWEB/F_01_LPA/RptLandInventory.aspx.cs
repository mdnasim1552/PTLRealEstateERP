using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
namespace RealERPWEB.F_01_LPA
{
    public partial class RptLandInventory : System.Web.UI.Page
    {
        UserManLand objUserMan = new UserManLand();
        protected void Page_Load(object sender, EventArgs e)
        {

          

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Search";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetProjectName();

            }
        }

        protected void GetProjectName()
        {

            try
            {


                string comcod = this.GetCompCode();
                string srchproject = "%";
                List<RealEntity.EClassCommon.EClassProject> lst = new List<RealEntity.EClassCommon.EClassProject>();
                lst = objUserMan.GetProjectName(comcod, srchproject);
                this.ddlProject.DataTextField = "actdesc";
                this.ddlProject.DataValueField = "actcode";
                this.ddlProject.DataSource = lst;
                this.ddlProject.DataBind();
            }

            catch (Exception ex)
            { 
            
            
            
            }








        }


        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            ViewState.Remove("lstproinventory");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo> lst = new List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>();
            lst = objUserMan.GetLandInventory(comcod, pactcode);

            ViewState["lstproinventory"] = this.HiddenSameData(lst);
            this.Data_Bind();

        }


      private List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo> HiddenSameData(List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo> lst)
        {
            if (lst.Count == 0)
                return lst;
            int j = 0;
            string ocsdhagno = lst[0].ocsdhagno;
            foreach (var lst1 in lst)
            {
                if (j == 0)
                {
                    j++;
                    continue;

                }

                else if (lst1.ocsdhagno.ToString() == ocsdhagno)
                {
                   
                    
                    lst1.csdhagno = "";
                    lst1.cslarea = 0.00;
                    lst1.sadhagno = "";
                    lst1.bslarea = 0;

                }



                ocsdhagno = lst1.ocsdhagno;




            }

            return lst;

        }
   


    private void Data_Bind()
        {
            var lst = (List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>)ViewState["lstproinventory"];
            this.gvlandinven.DataSource = lst;
            this.gvlandinven.DataBind();
            this.FooterCalculation();
        
        }





        private void FooterCalculation()
        {
            var lst = (List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>)ViewState["lstproinventory"];

            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFcslarea")).Text =lst.Sum(l=>l.cslarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFbslarea")).Text =lst.Sum(l=>l.bslarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFbsklarea")).Text =lst.Sum(l=>l.bsklarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFjblpurland")).Text =lst.Sum(l=>l.budarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFjbltland")).Text =lst.Sum(l=>l.budarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFrestofland")).Text =lst.Sum(l=>l.restlarea).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvlandinven.FooterRow.FindControl("lblgvFnamzri")).Text =lst.Sum(l=>l.purarea).ToString("#,##0.0000;(#,##0.0000); ");
           







        }


        protected void gvlandinven_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.Attributes["style"] = "font-weight:bold;";
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Dhag No";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.Attributes["style"] = "font-weight:bold; font-size:14px;";
                cell02.ColumnSpan = 4;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Acre of Land";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 2;
                cell03.Attributes["style"] = "font-weight:bold; font-size:14px;";
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "BS";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                cell04.Attributes["style"] = "font-weight:bold; font-size:14px;";
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "JBL Ref. No";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.RowSpan = 2;
                gvrow.Cells.Add(cell05);





                TableCell cell08 = new TableCell();
                cell08.Text = "JBL Purchase of Land";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Attributes["style"] = "font-weight:bold;";
                cell08.RowSpan = 2;
                gvrow.Cells.Add(cell08);



                TableCell cell09a = new TableCell();
                cell09a.Text = "JBL Total Land";
                cell09a.Attributes["style"] = "font-weight:bold;";
                cell09a.HorizontalAlign = HorizontalAlign.Center;
                cell09a.RowSpan = 2;
                gvrow.Cells.Add(cell09a);


                TableCell cell09 = new TableCell();
                cell09.Text = "Rest Of Land(Acre)";
                cell09.Attributes["style"] = "font-weight:bold;";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.RowSpan = 2;
                gvrow.Cells.Add(cell09);

                //TableCell cell09 = new TableCell();
                //cell09.Text = "Process Qty";
                //cell09.HorizontalAlign = HorizontalAlign.Center;
                //cell09.RowSpan = 2;
                //gvrow.Cells.Add(cell09);






                TableCell cell11 = new TableCell();
                cell11.Text = "Namzari";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.RowSpan = 2;
                cell11.Attributes["style"] = "font-weight:bold;";
                gvrow.Cells.Add(cell11);
                gvlandinven.Controls[0].Controls.AddAt(0, gvrow);

            }
        }

        protected void gvlandinven_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
            }
        }
    }
}