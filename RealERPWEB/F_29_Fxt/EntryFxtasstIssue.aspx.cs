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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_29_Fxt
{
    public partial class EntryFxtasstIssue : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
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


                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.lblrsirdesc.Text = this.Request.QueryString["rsirdesc"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Issue";
                this.GetDeparment();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void CreateTable()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("deptno", Type.GetType("System.String"));
            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));
            dttemp.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("issuedate", Type.GetType("System.String"));
            dttemp.Columns.Add("floorno", Type.GetType("System.String"));
            dttemp.Columns.Add("modelno", Type.GetType("System.String"));
            dttemp.Columns.Add("assetidcode", Type.GetType("System.String"));
            dttemp.Columns.Add("purchasedate", Type.GetType("System.String"));
            dttemp.Columns.Add("estimatedlife", Type.GetType("System.String"));
            dttemp.Columns.Add("warranty", Type.GetType("System.String"));
            dttemp.Columns.Add("purchaseprice", Type.GetType("System.String"));
            dttemp.Columns.Add("ratedepreciation", Type.GetType("System.String"));
            dttemp.Columns.Add("accudepreciation", Type.GetType("System.String"));
            dttemp.Columns.Add("depreciationdate", Type.GetType("System.String"));
            dttemp.Columns.Add("adjustment", Type.GetType("System.String"));
            dttemp.Columns.Add("remarks", Type.GetType("System.String"));
            dttemp.Columns.Add("qty", Type.GetType("System.Double"));
            dttemp.Columns.Add("balqty", Type.GetType("System.Double"));
            Session["sessionforgrid"] = dttemp;

        }


        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETHRDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
            this.ddldeptName.DataTextField = "deptdesc";
            this.ddldeptName.DataValueField = "deptno";
            this.ddldeptName.DataSource = ds1.Tables[0];
            this.ddldeptName.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.txtIssueNo.Text = "";
                this.pnlgrd.Visible = false;
                this.ddldeptName.Enabled = true;
                this.gvEmpIssue.DataSource = null;
                this.gvEmpIssue.DataBind();
                Session.Remove("sessionforgrid");

                return;
            }

            this.pnlgrd.Visible = true;
            this.lbtnOk.Text = "New";
            this.ddldeptName.Enabled = false;
            this.GetNewIssueno();
            this.BudgetBalance();
            this.CreateTable();


        }


        private void GetNewIssueno()
        {

            try
            {
                string comcod = this.GetComeCode();
                DataSet ds3 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETNEWISSUENO", "", "", "", "", "", "", "", "", "");
                this.txtIssueNo.Text = ds3.Tables[0].Rows[0]["issueno"].ToString();
                ds3.Dispose();

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }
        }

        protected void BudgetBalance()
        {
            Session.Remove("tblsir");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string deptno = "1%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "FXT_ASST_BALANCE", deptno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlreslist.DataTextField = "rsirdesc";
            this.ddlreslist.DataValueField = "rsircode";
            this.ddlreslist.DataSource = ds1.Tables[0];
            this.ddlreslist.DataBind();
            Session["tblsir"] = ds1.Tables[0];

        }

        private void Session_update()
        {
            DataTable dt1 = (DataTable)Session["sessionforgrid"];
            for (int i = 0; i < this.gvEmpIssue.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtqty")).Text.Trim());
                dt1.Rows[i]["qty"] = qty;
            }
            Session["sessionforgrid"] = dt1;

        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.Session_update();
            string rsircode = this.ddlreslist.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            DataRow[] projectrow2 = dt.Select("rsircode = '" + rsircode + "'");

            if (projectrow2.Length > 0)
            {
                return;

            }


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable dt1 = (DataTable)Session["sessionforgrid"];
            DataRow row = dt1.NewRow();
            row["comcod"] = comcod;
            row["deptno"] = ddldeptName.SelectedValue.ToString();
            row["rsircode"] = rsircode;
            row["rsirdesc"] = ddlreslist.SelectedItem.Text;
            row["empid"] = "";
            row["empname"] = "";
            row["desig"] = "";
            row["floorno"] = "";
            row["modelno"] = "";
            row["assetidcode"] = "";
            row["purchasedate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
            row["estimatedlife"] = "";
            row["warranty"] = 0.00;
            row["purchaseprice"] = 0.00;
            row["ratedepreciation"] = 0.00;
            row["accudepreciation"] = 0.00;
            row["depreciationdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
            row["qty"] = 0;
            row["balqty"] = Convert.ToDouble((((DataTable)Session["tblsir"]).Select("rsircode='" + rsircode + "'"))[0]["balqty"]);
            // row["currentyear"] = 0.00;
            row["adjustment"] = 0.00;
            // row["wdv"] = 0.00;

            row["remarks"] = "";


            dt1.Rows.Add(row);
            this.Data_Bind();



        }


        private void Data_Bind()
        {


            this.gvEmpIssue.DataSource = (DataTable)Session["sessionforgrid"];
            this.gvEmpIssue.DataBind();

        }





        protected void gvEmpIssue_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataTable dt2 = (DataTable)Session["sessionforgrid"];

            this.gvEmpIssue.EditIndex = e.NewEditIndex;
            int rowindex = (gvEmpIssue.PageSize) * (this.gvEmpIssue.PageIndex) + e.NewEditIndex;
            string floor = ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtfloor")).Text.Trim();
            string model = ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("lblmodel")).Text.Trim();
            string idcode = ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("lblidcode")).Text.Trim();
            string remarks = ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("lblremarks")).Text.Trim();
            string purdate = (((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtgvdate")).Text.Trim());
            string estimate = (((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("lblestimated")).Text.Trim());
            double warranty = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("lblwarranty")).Text.Trim()); //lbldig lblestimated

            double txtpurprice = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtpurprice")).Text.Trim());
            double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtrate")).Text.Trim().Replace("%", ""));
            double txtAccmulated = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtAccmulated")).Text.Trim());
            string depredate = (((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtgvdepre")).Text.Trim());
            double txtadjustment = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("txtadjustment")).Text.Trim());



            dt2.Rows[rowindex]["floorno"] = floor;
            dt2.Rows[rowindex]["modelno"] = model;
            dt2.Rows[rowindex]["assetidcode"] = idcode;
            dt2.Rows[rowindex]["remarks"] = remarks;
            dt2.Rows[rowindex]["purchasedate"] = purdate;
            dt2.Rows[rowindex]["warranty"] = warranty;
            dt2.Rows[rowindex]["estimatedlife"] = estimate;
            dt2.Rows[rowindex]["purchaseprice"] = txtpurprice;
            dt2.Rows[rowindex]["ratedepreciation"] = txtrate;
            dt2.Rows[rowindex]["accudepreciation"] = txtAccmulated;
            dt2.Rows[rowindex]["depreciationdate"] = depredate;
            ////dt2.Rows[i]["currentyear"] = current;
            dt2.Rows[rowindex]["adjustment"] = txtadjustment;
            // //dt2.Rows[i]["wdv"] = wdv;
            // //dt2.Rows[i]["section"]=;
            //}
            Session["sessionforgrid"] = dt2;


            this.Data_Bind();
            // DataTable dt = (DataTable)Session["tblDoChallan"];



            string empid = ((DataTable)Session["sessionforgrid"]).Rows[rowindex]["empid"].ToString();

            DropDownList ddl2 = (DropDownList)this.gvEmpIssue.Rows[e.NewEditIndex].FindControl("ddlemp");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SearchProject = "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = empid;
            Session["tblemp"] = ds1.Tables[0];





        }
        protected void gvEmpIssue_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dtemp = (DataTable)Session["tblemp"];

            DataTable dt = (DataTable)Session["sessionforgrid"];
            string empid = ((DropDownList)this.gvEmpIssue.Rows[e.RowIndex].FindControl("ddlemp")).SelectedValue.ToString();


            DataRow[] dr1 = dtemp.Select("empid='" + empid + "'");

            if (dr1.Length == 0)
                return;
            //string desig=dr1[0]["desig"].ToString();
            string empname = dr1[0]["empname"].ToString();
            string desig1 = dr1[0]["desig"].ToString();
            string dept = dr1[0]["department"].ToString();

            int index = e.RowIndex;
            dt.Rows[index]["desig"] = desig1;
            dt.Rows[index]["empid"] = empid;
            dt.Rows[index]["empname"] = empname;
            //  dt.Rows[index]["section"] = dept;



            Session["sessionforgrid"] = dt;

            this.SaveValue();
            this.gvEmpIssue.EditIndex = -1;
            this.Data_Bind();

        }
        protected void gvEmpIssue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.gvEmpIssue.EditIndex = -1;
            this.Data_Bind();

        }



        private void SaveValue()
        {

            DataTable dt2 = (DataTable)Session["sessionforgrid"];

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string floor = ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtfloor")).Text.Trim();
                string model = ((TextBox)this.gvEmpIssue.Rows[i].FindControl("lblmodel")).Text.Trim();
                string idcode = ((TextBox)this.gvEmpIssue.Rows[i].FindControl("lblidcode")).Text.Trim();
                string remarks = ((TextBox)this.gvEmpIssue.Rows[i].FindControl("lblremarks")).Text.Trim();
                string purdate = (((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtgvdate")).Text.Trim());
                string estimate = (((TextBox)this.gvEmpIssue.Rows[i].FindControl("lblestimated")).Text.Trim());
                string warranty = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("lblwarranty")).Text.Trim()).ToString(); //lbldig lblestimated

                double txtpurprice = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtpurprice")).Text.Trim());
                double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtrate")).Text.Trim().Replace("%", ""));
                double txtAccmulated = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtAccmulated")).Text.Trim());
                string depredate = (((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtgvdepre")).Text.Trim());

                //double txtcurrent = Convert.ToDouble("0"+((Label)this.gvProSlInfo.Rows[i].FindControl("txtcurrent")).Text.Trim());
                // double rate = (txtcurrent / txtpurprice) * 100;
                //double current = txtpurprice * (txtrate) / 100;
                double txtadjustment = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtadjustment")).Text.Trim());

                double balqty = Convert.ToDouble("0" + ((Label)this.gvEmpIssue.Rows[i].FindControl("lblbalqty")).Text.Trim());

                double txtqty = Convert.ToDouble("0" + ((TextBox)this.gvEmpIssue.Rows[i].FindControl("txtqty")).Text.Trim());


                if (txtqty > balqty)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Qty must be less then or equal  Balance Qty !!!!');", true);
                    return;
                }

                dt2.Rows[i]["floorno"] = floor;
                dt2.Rows[i]["modelno"] = model;
                dt2.Rows[i]["assetidcode"] = idcode;
                dt2.Rows[i]["balqty"] = balqty;
                dt2.Rows[i]["qty"] = txtqty;

                dt2.Rows[i]["remarks"] = remarks;
                dt2.Rows[i]["purchasedate"] = purdate;
                // dt2.Rows[i]["category"] = category;
                dt2.Rows[i]["warranty"] = warranty;


                dt2.Rows[i]["estimatedlife"] = estimate;
                dt2.Rows[i]["purchaseprice"] = txtpurprice;
                dt2.Rows[i]["ratedepreciation"] = txtrate;
                dt2.Rows[i]["accudepreciation"] = txtAccmulated;
                dt2.Rows[i]["depreciationdate"] = depredate;
                //dt2.Rows[i]["currentyear"] = current;
                dt2.Rows[i]["adjustment"] = txtadjustment;
                //dt2.Rows[i]["wdv"] = wdv;
                //dt2.Rows[i]["section"]=;
            }
            Session["sessionforgrid"] = dt2;
        }




        private void EmpSearch()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)Session["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvEmpIssue.Rows[rowindex].FindControl("ddlemp");
            string SearchProject = "%" + ((TextBox)gvEmpIssue.Rows[rowindex].FindControl("txtSrchemp")).Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();

        }
        protected void lUpdate_Click1(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string deptno = this.ddldeptName.SelectedValue.ToString();
            string Issueno = this.txtIssueNo.Text; ;
            DataTable dt1 = (DataTable)Session["sessionforgrid"];

            //Balance Check
            foreach (DataRow dr2 in dt1.Rows)
            {
                double balqty = Convert.ToDouble(dr2["balqty"].ToString().Trim());
                double qty = Convert.ToDouble(dr2["qty"].ToString().Trim());

                if (qty > balqty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Qty must be less then or equal Balance qty !!!!');", true);
                    return;
                }

            }

            foreach (DataRow dr in dt1.Rows)
            {
                string rsircode = dr["rsircode"].ToString().Trim();
                string empid = dr["empid"].ToString().Trim();
                string date = dr["issuedate"].ToString().Trim();
                string qty = (dr["qty"]).ToString();
                string floorno = dr["floorno"].ToString().Trim();
                string modelno = dr["modelno"].ToString().Trim();
                string assetidcode = dr["assetidcode"].ToString().Trim();
                string purchasedate = dr["purchasedate"].ToString().Trim();
                string warranty = dr["warranty"].ToString().Trim();
                string purchaseprice = dr["purchaseprice"].ToString().Trim();
                string ratedepreciation = dr["ratedepreciation"].ToString().Trim();
                string accudepreciation = dr["accudepreciation"].ToString().Trim();
                string depreciationdate = dr["depreciationdate"].ToString().Trim();
                string adjustment = dr["adjustment"].ToString().Trim();
                string estimatedlife = dr["estimatedlife"].ToString().Trim();
                string remarks = dr["remarks"].ToString().Trim();

                bool result = PurData.UpdateTransInfo3(comcod, "SP_ENTRY_FIXEDASSET_INFO03", "INSERTUPDATISSUEEMP", Issueno, deptno, rsircode, empid, date, qty, floorno,
                    modelno, assetidcode, purchasedate, warranty, purchaseprice, ratedepreciation, accudepreciation, depreciationdate, adjustment,
                    remarks, estimatedlife, "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";

                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";


                }
            }





        }
        protected void TblTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();
        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string asst = this.Request.QueryString["rsirdesc"].ToString();
            //DataTable dt2 = (DataTable)Session["tblDoChallan"];
            //ReportDocument rptsale = new RealERPRPT.R_29_Fxt.RptFixedAssetReg();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtAsset"] as TextObject;
            //rptDate.Text = asst;
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt2);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsale.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ibtnSrchcate_Click(object sender, EventArgs e)
        {

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = row.RowIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DropDownList ddlCategory = (DropDownList)this.gvEmpIssue.Rows[rowindex].FindControl("ddlCategory");

            string srchoption = "%" + ((TextBox)gvEmpIssue.Rows[rowindex].FindControl("txtSrchCate")).Text.Trim() + "%";
            //  string srchoption = "%%";
            DataSet dsone = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETCATEGORY", srchoption, "", "", "", "", "", "", "", "");
            Session["tblcategory"] = dsone.Tables[0];

            ddlCategory.DataTextField = "fxtgdesc";
            ddlCategory.DataValueField = "fxtgcod";
            ddlCategory.DataSource = dsone.Tables[0];
            ddlCategory.DataBind();

        }
    }
}