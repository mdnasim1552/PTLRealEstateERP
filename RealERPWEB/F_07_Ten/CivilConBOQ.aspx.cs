using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_07_Ten
{
    public partial class CivilConBOQ : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Civil Construction BOQ";

                DateTime curdate = System.DateTime.Today;
                this.txtfodate.Text = curdate.ToString("dd-MMM-yyyy");

                this.GetProjectsList();
                this.GetWorksGroup();
                this.UnitConvr();
                this.CreateTable();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));

            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("sdetails", Type.GetType("System.String"));
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            tblt01.Columns.Add("unit", Type.GetType("System.String"));
            tblt01.Columns.Add("convrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            tblt01.Columns.Add("ordam", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("costvatoh", Type.GetType("System.Double"));
            tblt01.Columns.Add("actamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("diffamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("rmrks", Type.GetType("System.String"));

            tblt01.Columns.Add("sbtrate_per", Type.GetType("System.Double"));
            tblt01.Columns.Add("actamt_per", Type.GetType("System.Double"));
            tblt01.Columns.Add("costvatoh_per", Type.GetType("System.Double"));



            ViewState["tblt01"] = tblt01;
        }
        private void GetProjectsList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "prjdesc2";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }

        private void UnitConvr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETUNITCONVERSION", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblUnitconv"] = ds1.Tables[0];
        }



        private void GetWorksGroup()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKGROUPLIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkGroup.DataTextField = "workdesc";
            this.ddlWorkGroup.DataValueField = "workcode";
            this.ddlWorkGroup.DataSource = ds1.Tables[0];
            this.ddlWorkGroup.DataBind();
        }
        private void GetWorksList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string workgrp = (this.ddlWorkGroup.SelectedValue.ToString() == "000000000000") ? "%" : ASTUtility.Left(this.ddlWorkGroup.SelectedValue.ToString(), 4) + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKLIST", workgrp, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkList.DataTextField = "workdesc";
            this.ddlWorkList.DataValueField = "workcode";
            this.ddlWorkList.DataSource = ds1.Tables[0];
            this.ddlWorkList.DataBind();


            ViewState["tblworklist"] = ds1.Tables[0];
            //this.DropCheck1.DataTextField = "workdesc";
            //this.DropCheck1.DataValueField = "workcode";
            //this.DropCheck1.DataSource = ds1.Tables[0];
            //this.DropCheck1.DataBind();

        }


        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOK.Text == "Ok")
            {
                this.lnkbtnOK.Text = "New";
                this.ddlProject.Enabled = false;
                this.divResource.Visible = true;
                this.gvCivilBoq.Visible = true;
                this.txtSbtRate_Per.Enabled = false;
                this.txtACCost_Per.Enabled = false;
                this.txtACCostVatOH_Per.Enabled = false;
                return;
            }

            this.lnkbtnOK.Text = "Ok";
            this.divResource.Visible = false;
            this.gvCivilBoq.Visible = false;
            this.txtSbtRate_Per.Enabled = true;
            this.txtACCost_Per.Enabled = true;
            this.txtACCostVatOH_Per.Enabled = true;
            this.ddlProject.Enabled = true;



        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable tblt01 = (DataTable)ViewState["tblt01"];
            DataTable tblunit = (DataTable)ViewState["tblUnitconv"];
            DataTable tblwlist = (DataTable)ViewState["tblworklist"];
            
            string actcode = this.ddlProject.SelectedValue.ToString();
            string actdesc = this.ddlProject.SelectedItem.ToString();
            string workcode = this.ddlWorkList.SelectedValue.ToString();
            string subdesc = this.ddlWorkList.SelectedItem.ToString();

            string txtsbtrate_per = this.txtSbtRate_Per.Text.Trim().ToString();
            string txtactamt_per = this.txtACCost_Per.Text.Trim().ToString();
            string txtcostvatoh_per = this.txtACCostVatOH_Per.Text.Trim().ToString();

            DataRow[] dr2 = tblt01.Select("actcode='" + actcode + "'  and subcode='" + workcode + "'");
            if (dr2.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Item Exist In The List" + "');", true);
                return;
            }


           
               
            double convrate = 0.00;
            

            DataRow[] drwork = tblwlist.Select("workcode='" + workcode + "'");
            if (drwork.Length < 0)
                return;
            string baseUnit = drwork[0]["baseUnit"].ToString();
            string baseUnitDesc = drwork[0]["isirunit"].ToString();
            string sdetails = drwork[0]["sdetails"].ToString();
            
            DataRow[] dr3 = tblunit.Select("bcod='" + baseUnit + "'");
            convrate = Convert.ToDouble(dr3[0]["conrat"].ToString());
            string convUnitDesc = dr3[0]["uconvdesc"].ToString();
            string convUnitcode = dr3[0]["ccod"].ToString();




            


            DataRow dr1 = tblt01.NewRow();
            dr1["actcode"] = actcode;
            dr1["actdesc"] = actdesc;
            dr1["subcode"] = workcode;
            dr1["subdesc"] = subdesc;
            dr1["sdetails"] = sdetails;

            dr1["qty"] = 0.00;
            dr1["unit"] = baseUnitDesc;
            dr1["rate"] = 0.00;
            dr1["convrate"] = convrate; 
            
            dr1["ordam"] = 0.00;
            dr1["sbtrate"] = 0.00;
            dr1["sbtamt"] = 0.00;
            dr1["costvatoh"] = 0.00;
            dr1["actamt"] = 0.00;
            dr1["diffamt"] = 0.00;
            dr1["sbtrate_per"] = Convert.ToDouble("0" + txtsbtrate_per);
            dr1["actamt_per"] = Convert.ToDouble("0" + txtactamt_per);
            dr1["costvatoh_per"] = Convert.ToDouble("0" + txtcostvatoh_per);
            dr1["rmrks"] = "";
            tblt01.Rows.Add(dr1);

            ViewState["tblt01"] = tblt01;
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];


            this.gvCivilBoq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCivilBoq.DataSource = tblt01;
            this.gvCivilBoq.DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetWorksList();
        }

        protected void gvCivilBoq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCivilBoq_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
           // ViewState_update();

            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;

            Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;

            string actcode = lblactcode.Text;
            string workcode = lblWcode.Text;

            TextBox Quntity = (TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty") as TextBox;
            TextBox lRate = (TextBox)this.gvCivilBoq.Rows[index].FindControl("txtrate") as TextBox;
            Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;
            double qty = Convert.ToDouble("0"+Quntity.Text.ToString());
            double rate = Convert.ToDouble("0" + lRate.Text.ToString());
            double ammount = qty * rate;
            lTotalRate.Text = ammount.ToString();



            DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "'  and subcode='" + workcode + "'");

            dr1[index]["qty"] = qty;
            dr1[index]["rate"] = rate;
            dr1[index]["ordam"] = ammount;


            //var lst = (List<EComRpEntity.Both.EclassBoth_BO.GetOrderPlace>)ViewState["tblOrderData"];
            //lst[index].count = qty;
            //lst[index].cstotalam = ammount;



            tblt01.Rows.Add(dr1);

             ViewState["tblt01"] = tblt01;
             this.Data_Bind();
        }

        protected void txtrate_TextChanged(object sender, EventArgs e)
        {
            ViewState_update();


            //DataTable tblt01 = (DataTable)ViewState["tblt01"];

            //int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;

            //Label lblactcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblgvactcode") as Label;
            //Label lblWcode = (Label)this.gvCivilBoq.Rows[index].FindControl("lblGvworkcode") as Label;

            //string actcode = lblactcode.Text;
            //string workcode = lblWcode.Text;

            //TextBox Quntity = (TextBox)this.gvCivilBoq.Rows[index].FindControl("txtqty") as TextBox;
            //Label lRate = (Label)this.gvCivilBoq.Rows[index].FindControl("txtrate") as Label;
            //Label lTotalRate = (Label)this.gvCivilBoq.Rows[index].FindControl("lblordam") as Label;
            //double qty = Convert.ToDouble(Quntity.Text.ToString());
            //double rate = Convert.ToDouble(lRate.Text.ToString());
            //double ammount = qty * rate;
            //lTotalRate.Text = ammount.ToString();



            //   DataRow[] dr1 = tblt01.Select("actcode='" + actcode + "'  and subcode='" + workcode + "'");




            //var lst = (List<EComRpEntity.Both.EclassBoth_BO.GetOrderPlace>)ViewState["tblOrderData"];
            //lst[index].count = qty;
            //lst[index].cstotalam = ammount;





            //ViewState["tblt01"] = tblt01;
            //this.Data_Bind();

        }

        protected void txtsbamt_TextChanged(object sender, EventArgs e)
        {

            ViewState_update();
        }

        private void ViewState_update()
        {
            DataTable dt = (DataTable)ViewState["tblt01"];
            int index;
            for (int i = 0; i < this.gvCivilBoq.Rows.Count; i++)
            {
                TextBox Quntity = (TextBox)this.gvCivilBoq.Rows[i].FindControl("txtqty") as TextBox;
                TextBox lRate = (TextBox)this.gvCivilBoq.Rows[i].FindControl("txtrate") as TextBox;
                Label lTotalRate = (Label)this.gvCivilBoq.Rows[i].FindControl("lblordam") as Label;
                double qty = Convert.ToDouble(Quntity.Text.ToString());
                double rate = Convert.ToDouble(lRate.Text.ToString());
                double ammount = qty * rate;
                lTotalRate.Text = ammount.ToString();
                string txtqty = Quntity.Text;
                string txtrate = lRate.Text;                
                string txtamt = lTotalRate.Text;

                index = (this.gvCivilBoq.PageSize) * (this.gvCivilBoq.PageIndex) + i;
                 
                dt.Rows[index]["qty"] = txtqty;
                dt.Rows[index]["rate"] = txtrate;
                dt.Rows[index]["ordam"] = txtamt;


                //tblt01.Columns.Add("qty", Type.GetType("System.Double"));
                //tblt01.Columns.Add("unit", Type.GetType("System.String"));
                //tblt01.Columns.Add("convrate", Type.GetType("System.Double"));
                //tblt01.Columns.Add("rate", Type.GetType("System.Double"));
                //tblt01.Columns.Add("ordam", Type.GetType("System.Double"));
                //tblt01.Columns.Add("sbtrate", Type.GetType("System.Double"));
                //tblt01.Columns.Add("sbtamt", Type.GetType("System.Double"));
                //tblt01.Columns.Add("costvatoh", Type.GetType("System.Double"));
                //tblt01.Columns.Add("actamt", Type.GetType("System.Double"));
                //tblt01.Columns.Add("diffamt", Type.GetType("System.Double"));

            }

            ViewState["tblt01"] = dt;
            this.DataBind();



        }


    }
}