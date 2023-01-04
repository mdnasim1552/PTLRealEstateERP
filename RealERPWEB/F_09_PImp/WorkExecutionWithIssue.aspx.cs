using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealEntity.C_09_PIMP.EClassExecution;

namespace RealERPWEB.F_09_PImp
{
    public partial class WorkExecutionWithIssue : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Permission Part
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue";
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue - EDIT MODE";
                }
                InitPage();
            }
        }
        //----SETUP----
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private string GetUserId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            return userid;
        }
        public void InitPage()
        {
            txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            GetProject();            
            //Default Panel Visible false
        }
        //----END SETUP----
        protected void btnOK_Click(object sender, EventArgs e)
        {
            OnOKClick();
        }
        private void OnOKClick()
        {
            if (btnOK.Text == "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK")
            {
                //OK Click
                ddlProject.Enabled = false;
                pnl1.Visible = true;
                pnl2.Visible = false;
                btnOK.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                GetCategory();
                GetItem();
                GetDivision();
                GetWENIssueNo();
                GetWorkWithIssue();
            }
            else
            {
                //New Click
                ddlProject.Enabled = true;
                pnl1.Visible = false;
               
                btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            }
        }
        private void GetProject()
        {
            string userid = GetUserId();
            string comcod = GetComCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST01", "%", "", userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }
        private void GetCategory()
        {
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string flrcode = "";
            string date = this.txtEntryDate.Text.Trim();
            string txtsrchItem = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");

            Session["itemlist"] = ds1.Tables[0];
            Session["item"] = ds1.Tables[1];
            if (ds1 == null)
                return;

            this.ddlCategory.DataTextField = "sirdesc";
            this.ddlCategory.DataValueField = "mitemcode";
            this.ddlCategory.DataSource = ds1.Tables[2];
            this.ddlCategory.DataBind();
        }
        private void GetItem()
        {
            string itemcode = this.ddlCategory.SelectedValue.Substring(0, 4).ToString() + "%";
            DataTable dt = ((DataTable)Session["item"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode like '" + itemcode + "' ");
            dt = dv.ToTable(true, "itemcode", "workitem");


            this.ddlItem.DataTextField = "workitem";
            this.ddlItem.DataValueField = "itemcode";
            this.ddlItem.DataSource = dt;
            this.ddlItem.DataBind();
        }
        public void GetDivision()
        {
            string Worklists = this.ddlItem.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode= " + Worklists);
            dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");
            this.ddlDivision.DataTextField = "flrdes1";
            this.ddlDivision.DataValueField = "flrdes1";
            this.ddlDivision.DataSource = dt;
            this.ddlDivision.DataBind();
        }
        private void GetWENIssueNo()
        {
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string CurDate1 = txtEntryDate.Text;
            string mISSNo = "NEWISS";
            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURISSUEINFO", mISSNo, CurDate1,
                             pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["sessionforgrid"] = ds1.Tables[0];
            GetIssueNo();
        }

        private void GetIssueNo()
        {
            string comcod = this.GetComCode();
            string isudate = this.txtEntryDate.Text.Trim();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISSUEINFO", isudate, "", "", "", "", "", "", "", "");
            this.txtWINoPartOne.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
            this.txtWINoPartTwo.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
            GetDivision();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDivision();
        }
        private void LoopForSession()
        {
            DataTable dt = (DataTable)Session["sessionforgrid"];
            int TblRowIndex;
            for (int i = 0; i < this.DataGridOne.Rows.Count; i++)
            {
                double txtwrkqty = Convert.ToDouble("0" + ((TextBox)this.DataGridOne.Rows[i].FindControl("txtwrkqty")).Text.Trim());
                TblRowIndex = (DataGridOne.PageIndex) * DataGridOne.PageSize + i;
                double balqty = Convert.ToDouble(dt.Rows[TblRowIndex]["balqty"]);

                if (balqty < txtwrkqty)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                dt.Rows[TblRowIndex]["wrkqty"] = txtwrkqty;

            }
            Session["sessionforgrid"] = dt;

        }
        protected void btnSelectOne_Click(object sender, EventArgs e)
        {
            OnSelectOneClick();
        }
        private void OnSelectOneClick()
        {
            LoopForSession();
            DataTable itemtable = (DataTable)Session["itemlist"];
            DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            string itemcode = this.ddlItem.SelectedValue.ToString().Trim();
            string gp = this.ddlDivision.SelectedValue.Trim();            
            var ItemList = (DataTable)Session["itemlist"];
            
            if (gp.Length > 0)
            {
                foreach (ListItem s1 in ddlDivision.Items)
                {
                    if (s1.Selected)
                    {
                        string flrcode = s1.Value.Substring(0, 3);
                        DataRow[] dr1 = tempforgrid.Select("flrcod='" + flrcode + "'  and itemcode='" + itemcode + "'");
                        if (dr1.Length == 0)
                        {
                            DataRow drforgrid = tempforgrid.NewRow();
                            drforgrid["flrcod"] = flrcode;
                            drforgrid["flrdes"] = (ItemList.Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["flrdes"];
                            drforgrid["wrkqty"] = 0;

                            drforgrid["balqty"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["balqty"];
                            drforgrid["wrkunit"] = (((DataTable)Session["itemlist"]).Select("itemcode='" + itemcode + "' and flrcod= '" + flrcode + "'"))[0]["wrkunit"];
                            drforgrid["itemcode"] = this.ddlItem.SelectedValue.ToString();
                            drforgrid["workitem"] = this.ddlItem.SelectedItem.ToString().Trim();
                            tempforgrid.Rows.Add(drforgrid);
                            
                        }
                    }
                }
            }
            Session["sessionforgrid"] = tempforgrid;
            this.GridOne_DataBind();
            GetDivision();
            //this.ddlDivision.Text = "";

        }

        private void GridOne_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.DataGridOne.PageSize = Convert.ToInt32(this.ddlPage.SelectedValue.ToString());
            this.DataGridOne.DataSource = tbl1;
            this.DataGridOne.DataBind();
        }

        private void GetWorkWithIssue()
        {
            string comcod = this.GetComCode();
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEINFOFROMWORK", "", "", "", "", "", "", "", "", "");
            ViewState["WorkExeWithIssue"] = ds.Tables[0];
        }

        private void GetMaterials()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = this.txtSearchMaterials.Text.Trim() + "%";
            string balcon = this.CompBalConMat();
            //string CallType = this.CompReceived();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMETERIALS", pactcode, date, SearchMat, balcon, "", "", "", "", "");
            Session["itemlistMaterials"] = ds1.Tables[0];
            Session["specification"] = ds1.Tables[2];           
        }

        protected void btnGenerateIssue_Click(object sender, EventArgs e)
        {
            LoopForSession();


            DataTable tempforgrid = (DataTable)Session["sessionforgrid"];
            DataTable dt = ((DataTable)ViewState["WorkExeWithIssue"]).Copy();
            DataView dv = dt.DefaultView;
            DataTable dt1 = new DataTable();
            for (int i=0; i< tempforgrid.Rows.Count; i++)
            {
                string isircode = tempforgrid.Rows[i]["itemcode"].ToString();     
                string flrcode= tempforgrid.Rows[i]["flrcod"].ToString();
                dv.RowFilter = ("isircode='" + isircode + "'  and flrcod='" + flrcode + "'");
                dt = dv.ToTable();
                dt1.Merge(dt);
            }
            DataGridTwo.DataSource = HiddenTableTwo(dt1);
            DataGridTwo.DataBind();
            pnl2.Visible = true;
        }

        private DataTable HiddenTableTwo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            string flrcod = dt.Rows[0]["flrcod"].ToString();
            string Itemcode = dt.Rows[0]["isircode"].ToString();
            //((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'")).Length == 0) ? "0.00" :
            //    Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bbgdqty"]).ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i]["flrcod"].ToString() == flrcod) && (dt.Rows[i]["isircode"].ToString() == Itemcode))
                {

                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["isircode"].ToString();

                    dt.Rows[i]["flrdesc"] = "";
                    dt.Rows[i]["isirdesc"] = "";                   
                }
                else
                {
                    flrcod = dt.Rows[i]["flrcod"].ToString();
                    Itemcode = dt.Rows[i]["isircode"].ToString();
                }
            }
            return dt;
        }



    }
}