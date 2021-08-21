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

namespace RealERPWEB.F_04_Bgd
{
    public partial class DynamicallyGridView : System.Web.UI.Page
    {

        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.CreateGridView();

            }

        }

        private void CreateGridView()
        {

            TemplateField field = new TemplateField();
            field.HeaderText = "User Code";
            field.ItemStyle.Width = 50;
            field.ItemTemplate = new CreateItemTemplateLabel("ColText", "LabelName"); ;


            //  ((Label)this.gvDyInfo).FindControl lblgv=new Label();
            // Label lblgv = new Label();

            //GridViewRow row = (GridViewRow)lblgv.NamingContainer;
            // lblgv.Text = DataBinder.Eval(row.DataItem, "sirdesc").ToString();
            gvDyInfo.Columns.Add(field);


            TemplateField field1 = new TemplateField();
            field1.HeaderText = "Materials Description";
            field1.ItemStyle.Width = 150;
            //Label lblmatdesc = new Label();
            // lblmatdesc.Text = DataBinder.Eval(((GridViewRow)((Control)sender).NamingContainer).DataItem, "sirdesc").ToString();
            gvDyInfo.Columns.Add(field1);



            TemplateField field2 = new TemplateField();
            field2.HeaderText = "Unit";
            field2.ItemStyle.Width = 50;
            //
            Label lblggsirunit = new Label();
            // lblgv.Text = DataBinder.Eval(((GridViewRow)((Control)sender).NamingContainer).DataItem, "sirunit").ToString();
            gvDyInfo.Columns.Add(field2);

            GridView gd = gvDyInfo;


        }

        public class CreateItemTemplateLabel : ITemplate
        {
            string strColumnText;
            string strLabelName;
            public CreateItemTemplateLabel(string ColText, string LabelName)
            {
                this.strColumnText = ColText;
                this.strLabelName = LabelName;
            }
            public void InstantiateIn(Control objContainer)
            {
                Label lbl = new Label();
                lbl.ID = strLabelName;
                lbl.DataBinding += new EventHandler(lbl_DataBinding);
                objContainer.Controls.Add(lbl);
            }
            private void lbl_DataBinding(object sender, EventArgs e)
            {
                Label lbl = (Label)sender;
                lbl.ID = strLabelName;
                lbl.Text = strColumnText;
            }
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "TESTDYGRID", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvDyInfo.DataSource = null;
                this.gvDyInfo.DataBind();
                return;
            }




            this.gvDyInfo.DataSource = ds2.Tables[0];
            this.gvDyInfo.DataBind();




            //this.gvDyInfo.HeaderRow.Cells[1].Text = "User Code";
            //this.gvDyInfo.HeaderRow.Cells[1].Width = 80;



            //this.gvDyInfo.HeaderRow.Cells[2].Text = "Materials Description";
            //this.gvDyInfo.HeaderRow.Cells[2].Width = 300;
            //this.gvDyInfo.HeaderRow.Cells[3].Text = "Unit";
            //this.gvDyInfo.HeaderRow.Cells[3].Width = 40;

        }


        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < this.gvDyInfo.Rows.Count; i++)
            {

                string sircode = ((Label)this.gvDyInfo.Rows[i].FindControl("LabelName")).Text.Trim();

            }
        }
        protected void gvDyInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvResDescd = (Label)e.Row.FindControl("LabelName");
            }

        }
    }
}