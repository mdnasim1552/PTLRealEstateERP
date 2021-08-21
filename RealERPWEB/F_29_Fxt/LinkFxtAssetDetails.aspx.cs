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
namespace RealERPWEB.F_29_Fxt
{
    public partial class LinkFxtAssetDetails : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                // string day = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.lblrsirdesc.Text = this.Request.QueryString["rsirdesc"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Fixed Asset Register";
                this.GetDeparment();
                this.Visibility();
                this.LoadGrid();
                ddlProjectName_SelectedIndexChanged(null, null);

            }
        }

        private void Visibility()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3305":
                    gvProSlInfo.Columns[10].Visible = true;
                    gvProSlInfo.Columns[11].Visible = true;
                    gvProSlInfo.Columns[12].Visible = true;
                    gvProSlInfo.Columns[13].Visible = true;
                    gvProSlInfo.Columns[14].Visible = true;
                    gvProSlInfo.Columns[15].Visible = true;
                    gvProSlInfo.Columns[16].Visible = true;
                    gvProSlInfo.Columns[17].Visible = true;
                    gvProSlInfo.Columns[18].Visible = true;

                    break;



                default:

                    break;





            }





        }
        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "fxtgdesc";
            this.ddlProjectName.DataValueField = "fxtgcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void LoadGrid()
        {
            string pactcod = this.ddlProjectName.SelectedValue.ToString();
            string comcod = this.Request.QueryString["comcod"].ToString();
            string code = this.Request.QueryString["rsircode"].ToString();
            // string qty = this.Request.QueryString["qty"].ToString();
            // string pactcode = this.Request.QueryString["pactcode"].ToString();
            //string Procode = this.Request.QueryString["procode"].ToString();
            //string Qty = this.Request.QueryString["qty"].ToString();

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSETDETAILS", pactcod, code, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblDoChallan"] = ds1.Tables[0];
            this.Data_Bind();
            //this.lblBBLCInf.Text = ds1.Tables[0].Rows[0]["sirdesc"].ToString();
        }

        private void Data_Bind()
        {
            this.gvProSlInfo.DataSource = (DataTable)ViewState["tblDoChallan"];
            this.gvProSlInfo.DataBind();
        }

        private void GetCategory()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "%%";
            DataSet dsone = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETCATEGORY", srchoption, "", "", "", "", "", "", "", "");
            Session["tblcategory"] = dsone.Tables[0];
            dsone.Dispose();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        //private void CheckValue()
        //{
        //    DataTable dt = (DataTable)Session["tblDoChallan"];
        //    for (int i = 0; i < this.gvProSlInfo.Rows.Count; i++)
        //    {

        //        string Codenum = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtgvcodenum")).Text.Trim();
        //        string Colorcode = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtgvcolorcode")).Text.Trim();
        //        string Remarks = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
        //        string chkmr = (((CheckBox)this.gvProSlInfo.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";

        //        dt.Rows[i]["codenum"] = Codenum;
        //        dt.Rows[i]["colorcode"] = Colorcode;
        //        dt.Rows[i]["remarks"] = Remarks;
        //        dt.Rows[i]["chkmv"] = chkmr;

        //        ((CheckBox)this.gvProSlInfo.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.gvProSlInfo.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
        //        //((LinkButton)this.gvProSlInfo.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.gvProSlInfo.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
        //    }
        //    Session["tblDoChallan"] = dt;
        //}

        protected void gvProSlInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataTable dt2 = (DataTable)ViewState["tblDoChallan"];

            this.gvProSlInfo.EditIndex = e.NewEditIndex;
            int rowindex = (gvProSlInfo.PageSize) * (this.gvProSlInfo.PageIndex) + e.NewEditIndex;

            //this.SaveValue();


            string floor = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtfloor")).Text.Trim();
            string model = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblmodel")).Text.Trim();
            //string idcode = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblidcode")).Text.Trim();


            string idcode = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblidcode")).Text.Trim();





            string remarks = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblremarks")).Text.Trim();
            string qty = ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblqy")).Text.Trim();
            string purdate = (((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtgvdate")).Text.Trim());
            string estimate = (((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblestimated")).Text.Trim());
            double warranty = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("lblwarranty")).Text.Trim()); //lbldig lblestimated

            double txtpurprice = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtpurprice")).Text.Trim());
            double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtrate")).Text.Trim().Replace("%", ""));
            double txtAccmulated = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtAccmulated")).Text.Trim());
            string depredate = (((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtgvdepre")).Text.Trim());

            //double txtcurrent = Convert.ToDouble("0"+((Label)this.gvProSlInfo.Rows[i].FindControl("txtcurrent")).Text.Trim());
            //// double rate = (txtcurrent / txtpurprice) * 100;
            // //double current = txtpurprice * (txtrate) / 100;
            double txtadjustment = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("txtadjustment")).Text.Trim());

            // //double wdv = (txtpurprice - (txtAccmulated + current));
            // //double txtwdv = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtwdv")).Text.Trim());
            // //string remarks = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblremarks")).Text.Trim();
            // //string empid = ((DropDownList)this.gvProSlInfo.Rows[i].FindControl("ddlemp")).SelectedValue.ToString();
            //string category = ((DropDownList)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("ddlCategory")).SelectedValue.ToString();

            //// ((Label)this.gvProSlInfo.FindControl("txtcurrent")).Text = txtcurrent.ToString();

            dt2.Rows[rowindex]["floorno"] = floor;
            dt2.Rows[rowindex]["modelno"] = model;
            dt2.Rows[rowindex]["assetidcode"] = idcode;
            //dt2.Rows[rowindex]["category"] = category;
            dt2.Rows[rowindex]["remarks"] = remarks;
            dt2.Rows[rowindex]["qty"] = qty;
            dt2.Rows[rowindex]["purchasedate"] = purdate;
            //dt2.Rows[rowindex]["category"] = category;
            dt2.Rows[rowindex]["warranty"] = warranty;


            //dt2.Rows[rowindex["estimatedlife"] =estimate;

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
            ViewState["tblDoChallan"] = dt2;


            this.Data_Bind();
            // DataTable dt = (DataTable)Session["tblDoChallan"];



            string empid = ((DataTable)ViewState["tblDoChallan"]).Rows[rowindex]["empid"].ToString();
            string categoryid = ((DataTable)ViewState["tblDoChallan"]).Rows[rowindex]["categoryid"].ToString();

            DropDownList ddl2 = (DropDownList)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("ddlemp");
            DropDownList ddlCategory = (DropDownList)this.gvProSlInfo.Rows[e.NewEditIndex].FindControl("ddlCategory");
            // Panel pnlteam = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("pnlTeam");

            //Panel pnl02 = (Panel)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("Panel2");

            //Session.Remove("tblemp");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string deptcode = ((Label)gvProSlInfo.Rows[e.NewEditIndex].FindControl("lgpactcode")).Text.Trim();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", "%", deptcode, "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1.Tables[0];
            ddl2.DataBind();
            ddl2.SelectedValue = empid;
            Session["tblemp"] = ds1.Tables[0];


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string srchoption = "%%";
            //DataSet dsone = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETCATEGORY", srchoption, "", "", "", "", "", "", "", "");
            //Session["tblcategory"] = dsone.Tables[0];

            //ddlCategory.DataTextField = "fxtgdesc";
            //ddlCategory.DataValueField = "fxtgcod";
            //ddlCategory.DataSource = dsone.Tables[0];
            //ddlCategory.DataBind();
            //ddlCategory.SelectedValue = categoryid;
            ////this.SaveValue();
            ////this.Data_Bind();
            //dsone.Dispose();

        }
        protected void gvProSlInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dtemp = (DataTable)Session["tblemp"];
            // DataTable dtcategory = (DataTable)Session["tblcategory"];
            DataTable dt = (DataTable)ViewState["tblDoChallan"];
            string empid = ((DropDownList)this.gvProSlInfo.Rows[e.RowIndex].FindControl("ddlemp")).SelectedValue.ToString();
            string categoryid = ((DropDownList)this.gvProSlInfo.Rows[e.RowIndex].FindControl("ddlCategory")).SelectedValue.ToString();


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
            dt.Rows[index]["section"] = dept;


            //  DataRow[] dr2 = dtcategory.Select("fxtgcod='" + categoryid + "'");


            // dtcategory.Rows[index]["category"] = fxtgdesc;
            // dt.Rows[index]["categoryid"] = dr2[0]["fxtgcod"].ToString();
            // dt.Rows[index]["category"] = dr2[0]["fxtgdesc"].ToString();
            //dtcategory.Rows[index]["category"] = categoryid;
            this.gvProSlInfo.EditIndex = -1;

            ViewState["tblDoChallan"] = dt;
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvProSlInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvProSlInfo.EditIndex = -1;
            this.Data_Bind();

        }



        private void SaveValue()
        {

            DataTable dt2 = (DataTable)ViewState["tblDoChallan"];

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string floor = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtfloor")).Text.Trim();
                string model = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblmodel")).Text.Trim();

                string idcode = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblidcode")).Text.Trim();

                string remarks = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblremarks")).Text.Trim();
                string qty = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblqy")).Text.Trim();
                string purdate = (((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtgvdate")).Text.Trim());
                string estimate = (((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblestimated")).Text.Trim());
                double warranty = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblwarranty")).Text.Trim()); //lbldig lblestimated

                double txtpurprice = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtpurprice")).Text.Trim());
                double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtrate")).Text.Trim().Replace("%", ""));
                double txtAccmulated = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtAccmulated")).Text.Trim());
                string depredate = (((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtgvdepre")).Text.Trim());

                //double txtcurrent = Convert.ToDouble("0"+((Label)this.gvProSlInfo.Rows[i].FindControl("txtcurrent")).Text.Trim());
                // double rate = (txtcurrent / txtpurprice) * 100;
                //double current = txtpurprice * (txtrate) / 100;
                double txtadjustment = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtadjustment")).Text.Trim());

                //double wdv = (txtpurprice - (txtAccmulated + current));
                //double txtwdv = Convert.ToDouble("0" + ((TextBox)this.gvProSlInfo.Rows[i].FindControl("txtwdv")).Text.Trim());
                //string remarks = ((TextBox)this.gvProSlInfo.Rows[i].FindControl("lblremarks")).Text.Trim();
                //string empid = ((DropDownList)this.gvProSlInfo.Rows[i].FindControl("ddlemp")).SelectedValue.ToString();
                // string category = ((DropDownList)this.gvProSlInfo.Rows[i].FindControl("ddlCategory")).SelectedValue.ToString();

                // ((Label)this.gvProSlInfo.FindControl("txtcurrent")).Text = txtcurrent.ToString();

                dt2.Rows[i]["floorno"] = floor;
                dt2.Rows[i]["modelno"] = model;
                dt2.Rows[i]["assetidcode"] = idcode;
                // dt2.Rows[i]["category"] = category;
                dt2.Rows[i]["remarks"] = remarks;
                dt2.Rows[i]["qty"] = qty;
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
            ViewState["tblDoChallan"] = dt2;
        }

        //protected void ibtnSrchemp_Click(object sender, EventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int rowindex = row.RowIndex;
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();

        //    DropDownList ddl2 = (DropDownList)this.gvProSlInfo.Rows[rowindex].FindControl("ddlemp");
        //    //string SearchProject = "%" + ((TextBox)gvProSlInfo.Rows[rowindex].FindControl("txtSrchemp")).Text.Trim() + "%";
        //    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", "%", "", "", "", "", "", "", "", "");
        //    ddl2.DataTextField = "empname";
        //    ddl2.DataValueField = "empid";
        //    ddl2.DataSource = ds1.Tables[0];
        //    ddl2.DataBind();
        //}


        //private void EmpSearch()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    int rowindex = (int)Session["gindex"];
        //    //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
        //    DropDownList ddl2 = (DropDownList)this.gvProSlInfo.Rows[rowindex].FindControl("ddlemp");
        //    string SearchProject = "%" + ((TextBox)gvProSlInfo.Rows[rowindex].FindControl("txtSrchemp")).Text.Trim() + "%";
        //    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
        //    ddl2.DataTextField = "empname";
        //    ddl2.DataValueField = "empid";
        //    ddl2.DataSource = ds1.Tables[0];
        //    ddl2.DataBind();

        //}
        protected void lUpdate_Click1(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = new DataSet();
            DataTable dt1 = ((DataTable)ViewState["tblDoChallan"]).Copy();
            ds1.DataSetName = "ds1";
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";
            string rsircode = this.Request.QueryString["rsircode"].ToString();


            bool result = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTUPDATEXTASSET", ds1, null, null, "", rsircode, "", "", "",
                     "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            }

            this.Data_Bind();

        }
        protected void TblTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string pactcode = this.Request.QueryString["pactcode"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            string rsircode = this.Request.QueryString["rsircode"].ToString();
            string slno1 = "";
            DataTable dt1 = (DataTable)ViewState["tblDoChallan"];
            if (dt1.Rows.Count == 0)
            {
                slno1 = "001";
            }
            else
            {
                int slno = Convert.ToInt16(dt1.Rows[dt1.Rows.Count - 1]["slno"]) + 1;//
                slno1 = ASTUtility.Right(("000" + slno), 3).ToString();
            }
            // DataRow drforgrid = dt1.NewRow();


            //  DataRow row = datatable1.NewRow();

            DataRow row = dt1.NewRow();
            row["comcod"] = comcod;
            row["slno"] = slno1;
            row["pactcode"] = pactcode;
            row["pactdesc"] = this.ddlProjectName.SelectedItem.Text;

            //
            row["rsircode"] = rsircode;
            row["empid"] = "";
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
            // row["currentyear"] = 0.00;
            row["adjustment"] = 0.00;
            // row["wdv"] = 0.00;
            row["categoryid"] = "";
            row["category"] = "";
            row["remarks"] = "";
            row["qty"] = 1;


            dt1.Rows.Add(row);
            this.Data_Bind();

            //comcod,  rsircode,slno,empid,floorno,modelno,assetidcode,purchasedate, estimatedlife ,warranty,category,purchaseprice,ratedepreciation,accudepreciation,currentyear,adjustment, wdv
            //this.Data_Bind();


        }

        //private void TableCreate()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("rsircode", Type.GetType("System.String"));
        //    dt.Columns.Add("spcfcod", Type.GetType("System.String"));
        //    dt.Columns.Add("rsirdesc", Type.GetType("System.String"));
        //    dt.Columns.Add("sirunit", Type.GetType("System.String"));
        //    dt.Columns.Add("spcfdesc", Type.GetType("System.String"));
        //    dt.Columns.Add("qty", Type.GetType("System.Double"));
        //    dt.Columns.Add("rate", Type.GetType("System.Double"));
        //    dt.Columns.Add("amt", Type.GetType("System.Double"));
        //    ViewState["tblpmattrans"] = dt;

        //}

        protected void gvProSlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lgcSlCode");
                string code = this.Request.QueryString["rsircode"].ToString();

                string newstr = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assetidcode")).ToString();


                var assetid = newstr.Replace("&", "^");

                string slno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slno")).ToString();

                if (comcod == "3305")
                {

                }

                else
                {
                    if (assetid.Length > 0)
                    {
                        hlink1.NavigateUrl = "../F_29_Fxt/FxtAstGinf.aspx?Type=Fselection" + "&sircode=" + code + "&genno=" + assetid + "&slno=" + slno;
                        hlink1.Target = "_blank";
                    }
                    else
                    {
                        hlink1.Enabled = false;


                    }

                }






            }
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
            //DataTable dt2 = (DataTable)ViewState["tblDoChallan"];
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
        protected void btnde_click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string slno = ((HyperLink)this.gvProSlInfo.Rows[RowIndex].FindControl("lgcSlCode")).Text.Trim();
            string Pactcod = this.ddlProjectName.SelectedValue.ToString();
            string rsircode = this.Request.QueryString["rsircode"].ToString();

            bool result = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "DELETEALLOCATEDASST", null, null, null, Pactcod, rsircode, slno, "", "",
                 "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            this.LoadGrid();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
    }
}