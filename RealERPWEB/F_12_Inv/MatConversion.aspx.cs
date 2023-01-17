using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using RealERPLIB;


using RealERPRPT;

namespace RealERPWEB.F_12_Inv
{
    public partial class MatConversion : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS CONVERSION";
            }

            if (this.ddlprjlistfrom.Items.Count == 0)
            {

                this.Load_Dates_And_Trans_No();
                this.GetProject();
                this.Load_Project_From_Combo();
                this.tableintosession();

                //this.GetSpecification();

                this.LoadMaterial_To_Combo();
                //this.GetSpecification2();

            }





        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_Dates_And_Trans_No()
        {


            string comcod = this.GetCompCode();
            this.txtCurTransDate.Text = GetStdDate(DateTime.Today.ToString("dd.MM.yyyy"));//XXXXXXXXXXXXXX
            this.Last_trn_no();


        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;

        }

        protected void Last_trn_no()
        {

            string comcod = this.GetCompCode();
            string date = this.txtCurTransDate.Text;
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "LASTCONVERSIONNUMBER", date, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            this.lblCurTransNo1.Text = ds.Tables[0].Rows[0]["maxconvrno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds.Tables[0].Rows[0]["maxconvrno1"].ToString().Trim().Substring(6);

        }

        private void GetProject()
        {


            string comcod = this.GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            Session["projectlist"] = ds1.Tables[0];

        }

        protected void Load_Project_From_Combo()
        {

            DataTable dt = (DataTable)Session["projectlist"];

            string srchfrmproject = this.txtSearchRes.Text.Trim();
            //string srchfrmproject = this.txtSrcfrmprojet.Text.Trim();

            if (srchfrmproject.Length > 0)
            {

                EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                         where r.Field<string>("actdesc1").ToUpper().Contains(srchfrmproject.ToUpper())
                                                         select r);
                dt = item.AsDataView().ToTable();


            }




            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = dt;
            this.ddlprjlistfrom.DataBind();
            // this.ddlprjlistfrom_SelectedIndexChanged(null, null);

        }

        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.Load_Project_To_Combo();

        }



        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Load_Project_Res_Combo();
        }

        protected void Load_Project_Res_Combo()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc = this.txtSearchRes.Text.Trim() + "%";
            string curdate = this.txtCurTransDate.Text.ToString().Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GetProjResList", ProjectCode, curdate, FindResDesc, "", "", "", "", "", "");
            Session["projectreslist"] = ds1.Tables[0];
            ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }

            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "rsircode";
            DataTable dt = dv.ToTable(true, "rsircode", "resdesc");
            this.ddlreslistfrom.DataTextField = "resdesc";
            this.ddlreslistfrom.DataValueField = "rsircode";
            this.ddlreslistfrom.DataSource = dt;
            this.ddlreslistfrom.DataBind();
            ds1.Dispose();

            this.GetSpecification();
        }


        protected void ddlreslistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();

            this.LoadMaterial_To_Combo();
            this.GetSpecification2();
        }

        protected void LoadMaterial_To_Combo()
        {
            string comcod = this.GetCompCode();


            DataTable dt = (DataTable)Session["projectreslist"];



            string srchfrmmaterial = this.txtSearchRes2.Text.Trim();



            if (srchfrmmaterial.Length > 0)
            {

                EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                         where r.Field<string>("resdesc").ToUpper().Contains(srchfrmmaterial.ToUpper())
                                                         select r);
                dt = item.AsDataView().ToTable();


            }

            this.ddlreslistto.DataTextField = "resdesc";
            this.ddlreslistto.DataValueField = "rsircode";
            this.ddlreslistto.DataSource = dt;
            this.ddlreslistto.DataBind();

        }



        private void GetSpecification()
        {
            string mResCode = this.ddlreslistfrom.SelectedValue.ToString();

            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = ("mspcfcod = '" + mResCode + "'");
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }



        protected void ddlreslistto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification2();


        }

        private void GetSpecification2()
        {
            string mResCode = this.ddlreslistto.SelectedValue.ToString();

            this.ddlResSpcf2.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = ("mspcfcod = '" + mResCode + "'");
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf2.DataTextField = "spcfdesc";
            this.ddlResSpcf2.DataValueField = "spcfcod";
            this.ddlResSpcf2.DataSource = dt;
            this.ddlResSpcf2.DataBind();


        }

        protected void ImgbtnFindRes2_Click(object sender, EventArgs e)
        {

            this.LoadMaterial_To_Combo();


        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblmatconversion"];
            this.grvconversion.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.grvconversion.DataSource = dt1;
            this.grvconversion.DataBind();


            if (dt1.Rows.Count == 0)
                return;

            this.FooterCalCulation();


        }


        private void Data_BindProduct()
        {
            DataTable dt1 = (DataTable)ViewState["tblmatconversionproduct"];
            this.gvProduct.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
            this.gvProduct.DataSource = dt1;
            this.gvProduct.DataBind();


            if (dt1.Rows.Count == 0)
                return;

            this.FooterCalCulationProduct();


        }




        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblmatconversion"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvconversion.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-");





        }


        private void FooterCalCulationProduct()
        {
            DataTable dt1 = (DataTable)ViewState["tblmatconversionproduct"];

            if (dt1.Rows.Count == 0)
                return;
            //((Label)this.grvconversion.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            //0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-");

            //   ((Label)this.grvconversion.FooterRow.FindControl("lgvTAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            //0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); 



        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.pnlgrd.Visible = true;
                // this.pnlProduct.Visible = true;
                this.lblddlProjectFrom.Visible = true;

                this.ddlprjlistfrom.Visible = false;

                this.lblprious.Visible = false;
                this.ImgbtnFindMCno.Visible = false;
                this.txtSrchConvrNo.Visible = false;

                this.ddlPrevMCList.Visible = false;
                this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;

                this.GetMatConversion();
                this.ImgbtnFindRes_Click(null, null);

            }
            else
            {

                this.ddlprjlistfrom.Visible = true;

                this.lblprious.Visible = true;
                this.txtSrchConvrNo.Visible = true;
                this.ImgbtnFindMCno.Visible = true; ;


                this.ddlPrevMCList.Visible = true;
                this.lblddlProjectFrom.Visible = false;

                this.txtCurTransDate.Enabled = true;
                this.grvconversion.DataSource = null;
                this.grvconversion.DataBind();

                this.gvProduct.DataSource = null;
                this.gvProduct.DataBind();
                this.Last_trn_no();
                this.pnlgrd.Visible = false;
                this.pnlProduct.Visible = false;
                this.txtrefno.Text = "";

                // this.tableintosession();

                //  this.lblConversionNo.Text = "";
                lbtnOk.Text = "Ok";
                this.ddlPrevMCList.Items.Clear();


            }


        }

        protected void ImgbtnFindMCno_Click1(object sender, EventArgs e)
        {
            this.Load_Prev_Trans_List();

        }

        protected void Load_Prev_Trans_List()
        {

            string comcod = this.GetCompCode();
            string fxtast = "%";
            string mtrfno = "%" + this.txtSrchConvrNo.Text.Trim() + "%";
            string prjcode = "%"; // this.ddlprjlistfrom.SelectedValue.ToString();
            string CurDate1 = (this.txtCurTransDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPRIVIOUSCONVERSIONLIST", CurDate1,
                          prjcode, mtrfno, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevMCList.DataTextField = "convrno1";
            this.ddlPrevMCList.DataValueField = "convrno";
            this.ddlPrevMCList.DataSource = ds1.Tables[0];
            this.ddlPrevMCList.DataBind();

        }


        private void GetMatConversion()
        {


            ViewState.Remove("tblmatconversion");
            ViewState.Remove("tblmatconversionproduct");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mCNVRo = "NEWCNVR";
            if (this.ddlPrevMCList.Items.Count > 0)
            {
                this.txtCurTransDate.Enabled = false;
                mCNVRo = this.ddlPrevMCList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PREVCNVRINFO", mCNVRo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmatconversion"] = ds1.Tables[0];

            ViewState["tblmatconversionproduct"] = ds1.Tables[1];


            if (mCNVRo == "NEWCNVR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "LASTCNVRNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurTransNo1.Text = ds1.Tables[0].Rows[0]["maxcnvrno1"].ToString().Trim().Substring(0, 6);
                this.txtCurTransNo2.Text = ds1.Tables[0].Rows[0]["maxcnvrno1"].ToString().Trim().Substring(6);


                //string conversionno = this.Request.QueryString["genno"].ToString();
                //if (conversionno.Length > 0)
                //{
                //    this.ShowConversionInfo();

                //}

                return;
            }


            this.ddlprjlistfrom.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProjectFrom.Text = this.ddlprjlistfrom.SelectedItem.Text;

            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["convrdat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[2].Rows[0]["convrref"].ToString();
            this.txtConvrNarr.Text = ds1.Tables[2].Rows[0]["convrnar"].ToString();
            this.lblCurTransNo1.Text = ds1.Tables[2].Rows[0]["convrno1"].ToString().Trim().Substring(0, 6);
            this.txtCurTransNo2.Text = ds1.Tables[2].Rows[0]["convrno1"].ToString().Trim().Substring(6);

            this.Data_Bind();
            this.Data_BindProduct();
            this.pnlProduct.Visible = true;
        }


        private void ShowConversionInfo()
        {

            string comcod = this.GetCompCode();
            string conversionno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETCONVERSIONINFO", conversionno, "",
                        "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblmatconversion"] = ds1.Tables[0];

            this.Data_Bind();



        }


        protected void lnkconvert_Click(object sender, EventArgs e)
        {
            this.SaveConvertValue();

            string pactcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();


            DataTable dt = (DataTable)ViewState["tblmatconversionproduct"];
            DataTable dt1 = (DataTable)Session["projectreslist"];




            string trsircode = this.ddlreslistto.SelectedValue.ToString().Trim();
            string tspcfcod = this.ddlResSpcf2.SelectedValue.ToString();

            //Existng
            //DataRow[] projectrow1 = dt.Select("frsircode = '" + frsircode + "' and fspcfcod ='" + fspcfcod + "'");
            //DataRow[] projectrow2 = dt.Select("trsircode = '" + trsircode + "' and tspcfcod = '" + tspcfcod + "'");








            DataRow[] projectrow4 = dt1.Select("rsircode = '" + trsircode + "' and spcfcod = '" + tspcfcod + "'");




            //if (projectrow2.Length == 0 )
            //{
            DataRow drforproductgrid = dt.NewRow();
            drforproductgrid["comcod"] = this.GetCompCode();
            drforproductgrid["pactcode"] = this.ddlprjlistfrom.SelectedItem.Text.ToString().Trim();
            drforproductgrid["pactdesc"] = this.ddlprjlistfrom.SelectedItem.Text.ToString().Trim();


            ////////////////////////////////
            drforproductgrid["rsircode"] = projectrow4[0]["rsircode"];
            drforproductgrid["spcfcode"] = tspcfcod;
            drforproductgrid["sirdesc"] = projectrow4[0]["resdesc"];
            drforproductgrid["sirunit"] = projectrow4[0]["sirunit"];
            drforproductgrid["qty"] = projectrow4[0]["qty"];
            drforproductgrid["rate"] = projectrow4[0]["rate"];


            drforproductgrid["amt"] = 0.00;




            dt.Rows.Add(drforproductgrid);
            //}
            ViewState["tblmatconversionproduct"] = dt;
            this.Data_BindProduct();

            if (this.gvProduct.Rows.Count > 0)
            {
                this.lnkconvert.Visible = false;
            }
            else
            {
                this.lnkconvert.Visible = true;
            }



        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {

            this.SaveValue();

            string pactcode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string frsircode = this.ddlreslistfrom.SelectedValue.ToString().Trim();

            DataTable dt = (DataTable)ViewState["tblmatconversion"];
            DataTable dt1 = (DataTable)Session["projectreslist"];


            string fspcfcod = this.ddlResSpcf.SelectedValue.ToString();

            string trsircode = this.ddlreslistto.SelectedValue.ToString().Trim();
            string tspcfcod = this.ddlResSpcf2.SelectedValue.ToString();


            // DataRow[] projectrow1 = dt.Select("rsircode = '" + frsircode + "'");





            DataRow[] projectrow3 = dt1.Select("rsircode = '" + frsircode + "' and spcfcod ='" + fspcfcod + "'");
            DataRow[] projectrow4 = dt1.Select("rsircode = '" + trsircode + "' and spcfcod = '" + tspcfcod + "'");





            DataRow drforgrid = dt.NewRow();
            drforgrid["comcod"] = projectrow3[0]["comcod"];

            drforgrid["rsircode"] = projectrow3[0]["rsircode"];



            drforgrid["spcfcode"] = fspcfcod;




            drforgrid["sirdesc"] = projectrow3[0]["resdesc"];



            drforgrid["sirunit"] = projectrow3[0]["sirunit"];

            drforgrid["qty"] = projectrow3[0]["qty"];


            drforgrid["rate"] = projectrow3[0]["rate"];


            drforgrid["amt"] = projectrow3[0]["amt"];
            drforgrid["balqty"] = projectrow3[0]["balqty"];
            drforgrid["spcfdesc"] = projectrow3[0]["spcfdesc"];

            ////////////////////////////////




            dt.Rows.Add(drforgrid);

            ViewState["tblmatconversion"] = dt;
            this.Data_Bind();

            Load_Project_Res_Combo();


        }

        private void SaveConvertValue()
        {


            DataTable dt1 = (DataTable)ViewState["tblmatconversionproduct"];
            for (int i = 0; i < this.gvProduct.Rows.Count; i++)
            {

                DataTable dt2 = (DataTable)ViewState["tblmatconversion"];

                if (dt1.Rows.Count == 0)
                    return;
                double totalAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ?
                0.00 : dt2.Compute("sum(amt)", "")));



                int rowindex = (this.grvconversion.PageSize * this.grvconversion.PageIndex) + i;
                double tqty = Convert.ToDouble("0" + ((TextBox)this.gvProduct.Rows[i].FindControl("txttqty")).Text.Trim());
                double trate = Convert.ToDouble("0" + ((TextBox)this.gvProduct.Rows[i].FindControl("txttrate")).Text.Trim());
                // double tqty = amt / trate;
                dt1.Rows[rowindex]["qty"] = tqty;
                dt1.Rows[rowindex]["amt"] = totalAmount;
                dt1.Rows[i]["rate"] = totalAmount / tqty;


            }
            ViewState["tblmatconversionproduct"] = dt1;





        }

        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblmatconversion"];
            for (int i = 0; i < this.grvconversion.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvconversion.Rows[i].FindControl("txtfqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.grvconversion.Rows[i].FindControl("txtfrate")).Text.Trim());



                string rsircode = ((Label)this.grvconversion.Rows[i].FindControl("lblgfsircode")).Text;
                string spcfcod = ((Label)this.grvconversion.Rows[i].FindControl("lblgfspcfcode")).Text;

                //DataRow[] dtrow = dt1.Select("rsircode='" + rsircode + "'");
                //foreach (DataRow row in dtrow)
                //{

                //}

                DataTable dt2 = (DataTable)Session["projectreslist"];

                int rowIndex = dt2.Rows.IndexOf(dt2.Select("rsircode='" + rsircode + "' AND spcfcod='" + spcfcod + "'")[0]);
                double balqty = Convert.ToDouble(dt2.Rows[rowIndex]["balqty"]);
                string spcfdesc = Convert.ToString(dt2.Rows[rowIndex]["spcfdesc"]);

                if (qty > balqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Quantity Exceeded";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                }
                else
                {

                    double amt = qty * rate;
                    int rowindex = (this.grvconversion.PageSize * this.grvconversion.PageIndex) + i;
                    dt1.Rows[rowindex]["qty"] = qty;
                    dt1.Rows[rowindex]["amt"] = qty * rate;

                    dt1.Rows[i]["rate"] = rate;

                    dt1.Rows[i]["balqty"] = balqty;
                    dt1.Rows[i]["spcfdesc"] = spcfdesc;

                }


            }
            ViewState["tblmatconversion"] = dt1;
        }

        protected void grvconversion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvconversion.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveConvertValue();
            this.gvProduct.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
            this.pnlProduct.Visible = true;

        }

        protected void lnktotalproduct_Click(object sender, EventArgs e)
        {
            this.SaveConvertValue();
            this.Data_BindProduct();

        }







        protected void tableintosession()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));

            dttemp.Columns.Add("convrno", Type.GetType("System.String"));

            dttemp.Columns.Add("convrdat", Type.GetType("System.DateTime"));

            dttemp.Columns.Add("pactcode", Type.GetType("System.String"));

            dttemp.Columns.Add("rsircode", Type.GetType("System.String"));


            dttemp.Columns.Add("spcfcod", Type.GetType("System.String"));

            dttemp.Columns.Add("qty", Type.GetType("System.Double"));

            dttemp.Columns.Add("amt", Type.GetType("System.Double"));

            dttemp.Columns.Add("convrref", Type.GetType("System.String"));

            dttemp.Columns.Add("convrnar", Type.GetType("System.String"));

            dttemp.Columns.Add("poostedbyid", Type.GetType("System.String"));

            dttemp.Columns.Add("postrmid", Type.GetType("System.String"));

            dttemp.Columns.Add("postseson", Type.GetType("System.String"));

            dttemp.Columns.Add("posteddat", Type.GetType("System.DateTime"));




            ViewState["tblmatconversionproduct"] = dttemp;

        }





        protected void grvconversion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatconversion"];
            string mCONVRNO = this.lblCurTransNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurTransDate.Text.Trim()), 4) + this.lblCurTransNo1.Text.Trim().Substring(3, 2) + this.txtCurTransNo2.Text.Trim();

            string rsircode = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgfsircode")).Text.Trim();
            string spcfcode = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgfspcfcode")).Text.Trim();

            //string trsircode = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgtsircode")).Text.Trim();
            //string tspcfcod = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgtspcfcode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELMTCONVERSIONNO", mCONVRNO, rsircode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvconversion.PageSize) * (this.grvconversion.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmatconversion");
            ViewState["tblmatconversion"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void lnkupdateConversion_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");





            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatconversion"];

            //Product
            DataTable dt2 = (DataTable)ViewState["tblmatconversionproduct"];

            string mtconvrdat = this.txtCurTransDate.Text.ToString().Trim();
            if (ddlPrevMCList.Items.Count == 0)
                this.GetMatConversion();
            string mtconvrno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + mtconvrdat.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
            string mtconvrref = this.txtrefno.Text.ToString();
            string mtrnar = this.txtConvrNarr.Text.ToString();

            string rsircodef = dt2.Rows[0]["rsircode"].ToString();
            string spcfcodef = dt2.Rows[0]["spcfcode"].ToString();



            //string qtyf = Convert.ToDouble(dt2.Rows[0]["qty"]).ToString("#,##0.00;(#,##0.00)");
            //string ratef = Convert.ToDouble(dt2.Rows[0]["rate"]).ToString("#,##0.00;(#,##0.00)");
            //string amtf = Convert.ToDouble(dt2.Rows[0]["amt"]).ToString("#,##0.00;(#,##0.00)");

            string qtyf = Convert.ToDouble(dt2.Rows[0]["qty"]).ToString();
            string ratef = Convert.ToDouble(dt2.Rows[0]["rate"]).ToString();
            string amtf = Convert.ToDouble(dt2.Rows[0]["amt"]).ToString();



            //if (mtconvrref.Length == 0)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Conversion No Should Not Be Empty";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}





            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "CHECKEDDUPMATCONVRREF", mtconvrref, "", "", "", "", "", "", "", "");

            if (ds2.Tables[0].Rows.Count == 0)
                ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("convrref ='" + mtconvrref + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Conversion. No !!!";

                    return;
                }
            }



            string project = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            //string toprj = this.ddlprjlistto.SelectedValue.ToString().Trim();
            //string reqno = this.lblreqno.Text.Trim();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTCONVR", "MATCONVERSIONB", mtconvrno, mtconvrdat, project, mtconvrref, mtrnar, PostedByid, Posttrmid,

                           PostSession, Posteddat, rsircodef, spcfcodef, qtyf, ratef, amtf, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            foreach (DataRow dr in dt.Rows)
            {




                string sircode = dr["rsircode"].ToString();

                string spcfcod = dr["spcfcode"].ToString();



                string qty = dr["qty"].ToString().Trim();

                string rate = dr["rate"].ToString().Trim();

                string amt = dr["amt"].ToString().Trim();



                bool result1 = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_05", "INESERTUPDATEMTCONVR", "MATCONVERSIONA", mtconvrno, sircode,
                   spcfcod, qty, amt, rate, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result1)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                }
            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.txtCurTransDate.Enabled = false;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Conversion";
                string eventdesc = "Material Conversion ";
                string eventdesc2 = "";
                // string eventdesc2 = "Project " + this.ddlprjlistfrom.SelectedItem.ToString() + " Material From  " + this.ddlreslistfrom.SelectedItem.ToString() + " Converted To - " + " Material  " + this.ddlreslistto.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void grvconversion_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = " ";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 1;
                gvrow.Cells.Add(cell01);


                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;
                gvrow.Cells.Add(cell02);



                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 1;
                gvrow.Cells.Add(cell03);



                TableCell cell04 = new TableCell();
                cell04.Text = " Material";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 7;
                gvrow.Cells.Add(cell04);





                grvconversion.Controls[0].Controls.AddAt(0, gvrow);



            }



        }




        protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatconversionproduct"];
            string mCONVRNO = this.lblCurTransNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurTransDate.Text.Trim()), 4) + this.lblCurTransNo1.Text.Trim().Substring(3, 2) + this.txtCurTransNo2.Text.Trim();

            string rsircode = ((Label)this.gvProduct.Rows[e.RowIndex].FindControl("lblgtsircode")).Text.Trim();
            string spcfcode = ((Label)this.gvProduct.Rows[e.RowIndex].FindControl("lblgtspcfcode")).Text.Trim();

            //string trsircode = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgtsircode")).Text.Trim();
            //string tspcfcod = ((Label)this.grvconversion.Rows[e.RowIndex].FindControl("lblgtspcfcode")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "DELMTCONVERSIONPRODUCT", mCONVRNO, rsircode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProduct.PageSize) * (this.gvProduct.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmatconversionproduct");
            ViewState["tblmatconversionproduct"] = dv.ToTable();
            this.Data_BindProduct();

            this.lnkconvert.Visible = true;




        }
    }
}