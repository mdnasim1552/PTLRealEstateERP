using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_22_Sal
{
    public partial class EntryDuesFProject : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtschstddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtschenddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }

            if (this.ddlProject.Items.Count == 0)
            {
                this.GetProjectName();

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string srchTxt = this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETFINROJECTNAME", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
            ViewState["tblproject"] = ds1.Tables[0];
        }
        protected void ImgbtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.ddlProject.Visible = false;
                this.lblProjectDesc.Visible = true;
                this.PnlColoumn.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.GeProSandEndate();
                this.ShowDuesFProject();

                this.ShowColoumGroup(1);
                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblProjectDesc.Visible = false;
            this.PnlColoumn.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.txtschstddate.Enabled = true;
            this.lbtngvP2.Enabled = true;
            this.lbtngvP3.Enabled = true;
            this.lbtngvP4.Enabled = true;
            this.lbtngvP5.Enabled = true;
            this.lbtngvP6.Enabled = true;
            this.lbtngvP7.Enabled = true;
            this.lbtngvP8.Enabled = true;
            this.lbtngvP9.Enabled = true;
            this.gvDuesFProject.DataSource = null;
            this.gvDuesFProject.DataBind();


        }
        private void GeProSandEndate()
        {

            string actcode = this.ddlProject.SelectedValue.ToString();

            DataTable dt = (DataTable)ViewState["tblproject"];
            DateTime dt1; DateTime dt2;
            dt1 = Convert.ToDateTime((((DataTable)ViewState["tblproject"]).Select("actcode='" + actcode + "'"))[0]["schstartdate"]);
            dt2 = Convert.ToDateTime((((DataTable)ViewState["tblproject"]).Select("actcode='" + actcode + "'"))[0]["schenddate"]);
            this.txtschstddate.Text = (dt1.ToString("dd-MMM-yyyy") == "01-Jan-1900") ? this.txtschstddate.Text : dt1.ToString("dd-MMM-yyyy");
            this.txtschenddate.Text = (dt2.ToString("dd-MMM-yyyy") == "01-Jan-1900") ? this.txtschstddate.Text : dt2.ToString("dd-MMM-yyyy");
            this.txtschstddate.Enabled = (dt2.ToString("dd-MMM-yyyy") == "01-Jan-1900") ? true : false;

        }

        private void ShowDuesFProject()
        {




            ViewState.Remove("tblptarget");
            ViewState.Remove("tblpymon");

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string strtdate = Convert.ToDateTime(this.txtschstddate.Text).ToString("dd-MMM-yyyy");
            string enddate = Convert.ToDateTime(this.txtschenddate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "FPRODUESANDSCHDULE", pactcode, strtdate, enddate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDuesFProject.DataSource = null;
                this.gvDuesFProject.DataBind();
                return;

            }
            ViewState["tblptarget"] = ds1.Tables[0];
            //  Session["tblpymon"] = ds1.Tables[1];
            //  DataTable dt = ds1.Tables[1];
            //int j = 6;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    this.gvDuesFProject.Columns[j].HeaderText = dt.Rows[i]["monyear"].ToString();
            //    j++;
            //}






            this.txtschstddate.Text = (ds1.Tables[1].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[1].Rows[0]["schstartdate"]).ToString("dd-MMM-yyyy");
            this.txtschenddate.Text = (ds1.Tables[1].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[1].Rows[0]["schenddate"]).ToString("dd-MMM-yyyy");
            int duration = (ds1.Tables[1].Rows.Count == 0) ? 0 : Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]);


            // this.lbtnGenerate.Visible = (ds1.Tables[1].Rows.Count == 0) ? true : false;
            this.EnableButton(duration);
            DateTime datefrm = Convert.ToDateTime(this.txtschstddate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txtschenddate.Text.Trim());
            for (int i = 7; i < 66; i++)
            {
                if (datefrm > dateto)
                    break;

                this.gvDuesFProject.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
            }


            ds1.Dispose();

        }

        private void EnableButton(int duration)
        {
            switch (duration)
            {

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    this.lbtngvP2.Enabled = false;
                    this.lbtngvP3.Enabled = false;
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    this.lbtngvP3.Enabled = false;
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;



                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    this.lbtngvP4.Enabled = false;
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;


                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    this.lbtngvP5.Enabled = false;
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    this.lbtngvP6.Enabled = false;
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;


                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                    this.lbtngvP7.Enabled = false;
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    this.lbtngvP8.Enabled = false;
                    this.lbtngvP9.Enabled = false;
                    break;

                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    this.lbtngvP9.Enabled = false;
                    break;

                default:
                    break;


            }


            //  int Duration=this.lblDuration.Text.IndexOf(

        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {


            DateTime datefrm = Convert.ToDateTime(this.txtschstddate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txtschenddate.Text.Trim());
            for (int i = 7; i < 66; i++)
            {
                if (datefrm > dateto)
                    break;

                this.gvDuesFProject.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
            }

            int duration = ASTUtility.Datediff(Convert.ToDateTime(this.txtschenddate.Text.Trim()), Convert.ToDateTime(this.txtschstddate.Text.Trim()));
            this.lbtnGenerate.Visible = false;
            this.EnableButton(duration);
            ShowColoumGroup(1);





        }

        protected void lbtngvP_Click(object sender, EventArgs e)
        {
            this.Refresh_Analysis_Session();
            this.ShowColoumGroup(Convert.ToInt32(((LinkButton)sender).Text));
        }

        protected void ShowColoumGroup(int i)
        {
            // this.Refresh_Analysis_Session();
            this.Data_Bind();
            i = (i > 9 ? 1 : (i < 1 ? 9 : i));
            this.gvDuesFProject.Columns[7].Visible = (i == 1);
            this.gvDuesFProject.Columns[8].Visible = (i == 1);
            this.gvDuesFProject.Columns[9].Visible = (i == 1);
            this.gvDuesFProject.Columns[10].Visible = (i == 1);
            this.gvDuesFProject.Columns[11].Visible = (i == 1);
            this.gvDuesFProject.Columns[12].Visible = (i == 1);
            this.gvDuesFProject.Columns[13].Visible = (i == 1);

            this.gvDuesFProject.Columns[14].Visible = (i == 2);
            this.gvDuesFProject.Columns[15].Visible = (i == 2);
            this.gvDuesFProject.Columns[16].Visible = (i == 2);
            this.gvDuesFProject.Columns[17].Visible = (i == 2);
            this.gvDuesFProject.Columns[18].Visible = (i == 2);
            this.gvDuesFProject.Columns[19].Visible = (i == 2);
            this.gvDuesFProject.Columns[20].Visible = (i == 2);

            this.gvDuesFProject.Columns[21].Visible = (i == 3);
            this.gvDuesFProject.Columns[22].Visible = (i == 3);
            this.gvDuesFProject.Columns[23].Visible = (i == 3);
            this.gvDuesFProject.Columns[24].Visible = (i == 3);
            this.gvDuesFProject.Columns[25].Visible = (i == 3);
            this.gvDuesFProject.Columns[26].Visible = (i == 3);
            this.gvDuesFProject.Columns[27].Visible = (i == 3);

            this.gvDuesFProject.Columns[28].Visible = (i == 4);
            this.gvDuesFProject.Columns[29].Visible = (i == 4);
            this.gvDuesFProject.Columns[30].Visible = (i == 4);
            this.gvDuesFProject.Columns[31].Visible = (i == 4);
            this.gvDuesFProject.Columns[32].Visible = (i == 4);
            this.gvDuesFProject.Columns[33].Visible = (i == 4);
            this.gvDuesFProject.Columns[34].Visible = (i == 4);

            this.gvDuesFProject.Columns[35].Visible = (i == 5);
            this.gvDuesFProject.Columns[36].Visible = (i == 5);
            this.gvDuesFProject.Columns[37].Visible = (i == 5);
            this.gvDuesFProject.Columns[38].Visible = (i == 5);
            this.gvDuesFProject.Columns[39].Visible = (i == 5);
            this.gvDuesFProject.Columns[40].Visible = (i == 5);
            this.gvDuesFProject.Columns[41].Visible = (i == 5);

            this.gvDuesFProject.Columns[42].Visible = (i == 6);
            this.gvDuesFProject.Columns[43].Visible = (i == 6);
            this.gvDuesFProject.Columns[44].Visible = (i == 6);
            this.gvDuesFProject.Columns[45].Visible = (i == 6);
            this.gvDuesFProject.Columns[46].Visible = (i == 6);
            this.gvDuesFProject.Columns[47].Visible = (i == 6);
            this.gvDuesFProject.Columns[48].Visible = (i == 6);

            this.gvDuesFProject.Columns[49].Visible = (i == 7);
            this.gvDuesFProject.Columns[50].Visible = (i == 7);
            this.gvDuesFProject.Columns[51].Visible = (i == 7);
            this.gvDuesFProject.Columns[52].Visible = (i == 7);
            this.gvDuesFProject.Columns[53].Visible = (i == 7);
            this.gvDuesFProject.Columns[54].Visible = (i == 7);
            this.gvDuesFProject.Columns[55].Visible = (i == 7);

            this.gvDuesFProject.Columns[56].Visible = (i == 8);
            this.gvDuesFProject.Columns[57].Visible = (i == 8);
            this.gvDuesFProject.Columns[58].Visible = (i == 8);
            this.gvDuesFProject.Columns[59].Visible = (i == 8);
            this.gvDuesFProject.Columns[60].Visible = (i == 8);
            this.gvDuesFProject.Columns[61].Visible = (i == 8);
            this.gvDuesFProject.Columns[62].Visible = (i == 8);

            this.gvDuesFProject.Columns[63].Visible = (i == 9);
            this.gvDuesFProject.Columns[64].Visible = (i == 9);
            this.gvDuesFProject.Columns[65].Visible = (i == 9);
            this.gvDuesFProject.Columns[66].Visible = (i == 9);
            this.lblColGroup.Text = Convert.ToString(i);
            this.FooterQty((DataTable)ViewState["tblptarget"]);

        }

        private void Data_Bind()
        {
            this.gvDuesFProject.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDuesFProject.DataSource = (DataTable)ViewState["tblptarget"];
            this.gvDuesFProject.DataBind();
        }

        protected void Refresh_Analysis_Session()
        {
            DataTable tbl1 = (DataTable)ViewState["tblptarget"];
            int rowindex;
            for (int i = 0; i < this.gvDuesFProject.Rows.Count; i++)
            {

                double Overdue = Convert.ToDouble("0" + ((TextBox)this.gvDuesFProject.Rows[i].FindControl("txtgvoverdues")).Text.Trim());

                for (int j = 1; j <= 60; j++)
                {
                    string gvQty1 = "txtgvQty" + ASTUtility.Right("00" + j.ToString(), 3);
                    double gvQty2 = Convert.ToDouble("0" + ((TextBox)this.gvDuesFProject.Rows[i].FindControl(gvQty1)).Text.Trim());
                    if (this.gvDuesFProject.Columns[j + 6].Visible)
                    {
                        rowindex = (this.gvDuesFProject.PageSize * this.gvDuesFProject.PageIndex) + i;
                        tbl1.Rows[rowindex]["ym" + j.ToString()] = gvQty2;
                    }
                }
                rowindex = (this.gvDuesFProject.PageSize * this.gvDuesFProject.PageIndex) + i;
                tbl1.Rows[rowindex]["overdue"] = Overdue;


            }
            ViewState["tblptarget"] = tbl1;
        }

        private void FooterQty(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFsalam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salam)", "")) ? 0.00
                : dt.Compute("Sum(salam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ? 0.00
                : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFreceivable")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(receivable)", "")) ? 0.00
                : dt.Compute("Sum(receivable)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblFgvoverdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(overdue)", "")) ? 0.00
             : dt.Compute("Sum(overdue)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFtoschdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toscham)", "")) ? 0.00
               : dt.Compute("Sum(toscham)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym1qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym1)", "")) ? 0.00
                : dt.Compute("Sum(ym1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym2qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
              : dt.Compute("Sum(ym2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym3qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
              : dt.Compute("Sum(ym3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym4qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
              : dt.Compute("Sum(ym4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym5qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
              : dt.Compute("Sum(ym5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym6qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
              : dt.Compute("Sum(ym6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym7qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
              : dt.Compute("Sum(ym7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym8qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
              : dt.Compute("Sum(ym8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym9qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
              : dt.Compute("Sum(ym9)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym10qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
              : dt.Compute("Sum(ym10)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym11qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
                : dt.Compute("Sum(ym11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym12qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
              : dt.Compute("Sum(ym12)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym13qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
              : dt.Compute("Sum(ym13)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym14qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
              : dt.Compute("Sum(ym14)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym15qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
              : dt.Compute("Sum(ym15)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym16qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
              : dt.Compute("Sum(ym16)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym17qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
              : dt.Compute("Sum(ym17)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym18qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
              : dt.Compute("Sum(ym18)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym19qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
              : dt.Compute("Sum(ym19)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym20qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
              : dt.Compute("Sum(ym20)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym21qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
                : dt.Compute("Sum(ym21)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym22qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
              : dt.Compute("Sum(ym22)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym23qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
              : dt.Compute("Sum(ym23)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym24qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
              : dt.Compute("Sum(ym24)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym25qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
              : dt.Compute("Sum(ym25)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym26qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
              : dt.Compute("Sum(ym26)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym27qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
              : dt.Compute("Sum(ym27)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym28qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
              : dt.Compute("Sum(ym28)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym29qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
              : dt.Compute("Sum(ym29)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym30qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
              : dt.Compute("Sum(ym30)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym31qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
                : dt.Compute("Sum(ym31)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym32qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
              : dt.Compute("Sum(ym32)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym33qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
              : dt.Compute("Sum(ym33)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym34qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
              : dt.Compute("Sum(ym34)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym35qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
              : dt.Compute("Sum(ym35)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym36qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
              : dt.Compute("Sum(ym36)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym37qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
              : dt.Compute("Sum(ym37)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym38qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
              : dt.Compute("Sum(ym38)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym39qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
              : dt.Compute("Sum(ym39)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym40qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
              : dt.Compute("Sum(ym40)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym41qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
                : dt.Compute("Sum(ym41)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym42qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
              : dt.Compute("Sum(ym42)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym43qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
              : dt.Compute("Sum(ym43)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym44qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
              : dt.Compute("Sum(ym44)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym45qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
              : dt.Compute("Sum(ym45)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym46qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
              : dt.Compute("Sum(ym46)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym47qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
              : dt.Compute("Sum(ym47)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym48qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
              : dt.Compute("Sum(ym48)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym49qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
              : dt.Compute("Sum(ym49)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym50qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
              : dt.Compute("Sum(ym50)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym51qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
                : dt.Compute("Sum(ym51)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym52qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
              : dt.Compute("Sum(ym52)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym53qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
              : dt.Compute("Sum(ym53)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym54qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
              : dt.Compute("Sum(ym54)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym55qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
              : dt.Compute("Sum(ym55)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym56qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
              : dt.Compute("Sum(ym56)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym57qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
              : dt.Compute("Sum(ym57)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym58qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
              : dt.Compute("Sum(ym58)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym59qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
              : dt.Compute("Sum(ym59)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym60qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
              : dt.Compute("Sum(ym60)", ""))).ToString("#,##0;(#,##0); ");





            //((Label)this.gvDuesFProject.FooterRow.FindControl("lblgvFym2qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym2)", "")) ? 0.00
            //    : dt.Compute("Sum(ym2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym3qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym3)", "")) ? 0.00
            //    : dt.Compute("Sum(ym3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym4qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym4)", "")) ? 0.00
            //    : dt.Compute("Sum(ym4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym5qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym5)", "")) ? 0.00
            //    : dt.Compute("Sum(ym5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym6qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym6)", "")) ? 0.00
            //    : dt.Compute("Sum(ym6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym7qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym7)", "")) ? 0.00
            //    : dt.Compute("Sum(ym7)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym8qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym8)", "")) ? 0.00
            //    : dt.Compute("Sum(ym8)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym9qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym9)", "")) ? 0.00
            //    : dt.Compute("Sum(ym9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym10qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym10)", "")) ? 0.00
            //    : dt.Compute("Sum(ym10)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym11qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym11)", "")) ? 0.00
            //   : dt.Compute("Sum(ym11)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym12qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym12)", "")) ? 0.00
            //    : dt.Compute("Sum(ym12)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym13qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym13)", "")) ? 0.00
            //    : dt.Compute("Sum(ym13)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym14qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym14)", "")) ? 0.00
            //    : dt.Compute("Sum(ym14)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym15qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym15)", "")) ? 0.00
            //    : dt.Compute("Sum(ym15)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym16qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym16)", "")) ? 0.00
            //    : dt.Compute("Sum(ym16)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym17qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym17)", "")) ? 0.00
            //    : dt.Compute("Sum(ym17)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym18qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym18)", "")) ? 0.00
            //    : dt.Compute("Sum(ym18)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym19qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym19)", "")) ? 0.00
            //    : dt.Compute("Sum(ym19)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym20qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym20)", "")) ? 0.00
            //    : dt.Compute("Sum(ym20)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym21qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym21)", "")) ? 0.00
            //   : dt.Compute("Sum(ym21)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym22qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym22)", "")) ? 0.00
            //    : dt.Compute("Sum(ym22)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym23qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym23)", "")) ? 0.00
            //    : dt.Compute("Sum(ym23)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym24qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym24)", "")) ? 0.00
            //    : dt.Compute("Sum(ym24)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym25qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym25)", "")) ? 0.00
            //    : dt.Compute("Sum(ym25)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym26qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym26)", "")) ? 0.00
            //    : dt.Compute("Sum(ym26)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym27qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym27)", "")) ? 0.00
            //    : dt.Compute("Sum(ym27)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym28qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym28)", "")) ? 0.00
            //    : dt.Compute("Sum(ym28)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym29qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym29)", "")) ? 0.00
            //    : dt.Compute("Sum(ym29)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym30qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym30)", "")) ? 0.00
            //    : dt.Compute("Sum(ym30)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym31qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym31)", "")) ? 0.00
            //   : dt.Compute("Sum(ym31)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym32qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym32)", "")) ? 0.00
            //    : dt.Compute("Sum(ym32)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym33qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym33)", "")) ? 0.00
            //    : dt.Compute("Sum(ym33)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym34qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym34)", "")) ? 0.00
            //    : dt.Compute("Sum(ym34)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym35qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym35)", "")) ? 0.00
            //    : dt.Compute("Sum(ym35)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym36qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym36)", "")) ? 0.00
            //    : dt.Compute("Sum(ym36)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym37qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym37)", "")) ? 0.00
            //    : dt.Compute("Sum(ym37)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym38qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym38)", "")) ? 0.00
            //    : dt.Compute("Sum(ym38)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym39qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym39)", "")) ? 0.00
            //    : dt.Compute("Sum(ym39)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym40qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym40)", "")) ? 0.00
            //    : dt.Compute("Sum(ym40)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym41qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym41)", "")) ? 0.00
            //   : dt.Compute("Sum(ym41)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym42qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym42)", "")) ? 0.00
            //    : dt.Compute("Sum(ym42)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym43qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym43)", "")) ? 0.00
            //    : dt.Compute("Sum(ym43)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym44qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym44)", "")) ? 0.00
            //    : dt.Compute("Sum(ym44)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym45qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym45)", "")) ? 0.00
            //    : dt.Compute("Sum(ym45)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym46qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym46)", "")) ? 0.00
            //    : dt.Compute("Sum(ym46)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym47qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym47)", "")) ? 0.00
            //    : dt.Compute("Sum(ym47)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym48qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym48)", "")) ? 0.00
            //    : dt.Compute("Sum(ym48)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym49qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym49)", "")) ? 0.00
            //    : dt.Compute("Sum(ym49)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym50qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym50)", "")) ? 0.00
            //    : dt.Compute("Sum(ym50)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym51qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym51)", "")) ? 0.00
            //   : dt.Compute("Sum(ym51)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym52qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym52)", "")) ? 0.00
            //    : dt.Compute("Sum(ym52)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym53qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym53)", "")) ? 0.00
            //    : dt.Compute("Sum(ym53)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym54qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym54)", "")) ? 0.00
            //    : dt.Compute("Sum(ym54)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym55qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym55)", "")) ? 0.00
            //    : dt.Compute("Sum(ym55)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym56qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym56)", "")) ? 0.00
            //    : dt.Compute("Sum(ym56)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym57qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym57)", "")) ? 0.00
            //    : dt.Compute("Sum(ym57)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym58qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym58)", "")) ? 0.00
            //    : dt.Compute("Sum(ym58)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym59qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym59)", "")) ? 0.00
            //    : dt.Compute("Sum(ym59)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvProTarget.FooterRow.FindControl("lblgvFym60qty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ym60)", "")) ? 0.00
            //    : dt.Compute("Sum(ym60)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Refresh_Analysis_Session();
            DataTable tbl1 = (DataTable)ViewState["tblptarget"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                double toschdues = Convert.ToDouble(tbl1.Rows[i]["overdue"]);
                for (int j = 1; j <= 60; j++)
                    toschdues = toschdues + Convert.ToDouble(tbl1.Rows[i]["ym" + j.ToString()]);


                tbl1.Rows[i]["toscham"] = toschdues;

            }
            ViewState["tblptarget"] = tbl1;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text.Trim()));

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            this.Refresh_Analysis_Session();
            //this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
            DataTable tbl1 = (DataTable)ViewState["tblptarget"];
            // DataTable tblym = ((DataTable)Session["tblpymon"]);
            string txtfrmdate = Convert.ToDateTime(this.txtschstddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txtschenddate.Text.Trim()).ToString("dd-MMM-yyyy");
            bool result = false;
            result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPFPRODUEB", pactcode, txtfrmdate, txttodate, "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                return;

            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string usircode = tbl1.Rows[i]["usircode"].ToString();
                DateTime datefrm = Convert.ToDateTime(this.txtschstddate.Text.Trim());
                DateTime dateto = Convert.ToDateTime(this.txtschenddate.Text.Trim());
                int count = ASTUtility.Datediff(Convert.ToDateTime(this.txtschenddate.Text.Trim()), Convert.ToDateTime(this.txtschstddate.Text.Trim()));
                string Tosaleam = Convert.ToDouble(tbl1.Rows[i]["salam"]).ToString();
                string recamt = Convert.ToDouble(tbl1.Rows[i]["recam"]).ToString();
                string receivable = Convert.ToDouble(tbl1.Rows[i]["receivable"]).ToString();
                string overdue = Convert.ToDouble(tbl1.Rows[i]["overdue"]).ToString();


                for (int j = 0; j <= count; j++)
                {

                    string yearmon = datefrm.ToString("yyyyMM");
                    double scham = Convert.ToDouble(tbl1.Rows[i]["ym" + (j + 1).ToString()]);
                    //if (scham > 0)
                    result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPFPRODUEA", pactcode, usircode, yearmon, Tosaleam, recamt, receivable, overdue, scham.ToString(), "", "", "", "", "", "", "");
                    datefrm = datefrm.AddMonths(1);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "PROJECT COMPLETION INFORMATION";
                string eventdesc = "Update";
                string eventdesc2 = this.ddlProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh_Analysis_Session();
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }
        protected void gvDuesFProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Refresh_Analysis_Session();
            this.gvDuesFProject.PageIndex = e.NewPageIndex;
            this.ShowColoumGroup(Convert.ToInt32(this.lblColGroup.Text));
        }



    }
}