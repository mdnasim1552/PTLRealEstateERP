using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.ComponentModel;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccBankCheque : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        //this is test
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Book Input";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.GetBankName();
                this.Gengroup.Visible = false;
            }
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
           

                // return (hst["comcod"].ToString());

        }

        private void GetBankName()

        {

            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "BANKLIST", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }



        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }

        protected void lnkOk_Click(object sender, EventArgs e)

        {


            if (this.lnkOk.Text == "Ok")
            {
                this.lnkOk.Text = "New";
                //this.txtAccSearch.Text = this.ddlBankName.SelectedItem.Text.Trim();
                this.ddlBankName.Enabled = false;
                this.Gengroup.Visible = true;
                this.chqBank.Visible = true;
                this.btnGenerate.Visible = false;
                this.txtFirstChq.Visible = false;
                this.txtFirstChq1.Visible = false;
                this.lblChequeno1.Visible = false;
                this.lblChequeno2.Visible = false;
                this.chqBank.Checked = false;

                this.ShowData();

                return;
            }

            else
            {
                this.lnkOk.Text = "Ok";
                this.ddlBankName.Enabled = true;
                this.Gengroup.Visible = false;
                this.txtFirstChq.Visible = false;
                this.txtFirstChq1.Visible = false;
                this.chqBank.Visible = false;
                this.lblChequeno1.Visible = false;
                this.lblChequeno2.Visible = false;
                this.btnGenerate.Visible = true;
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();
            }

        }

        private void ShowData()
        {
            Session.Remove("tblbank");
            string comcod = this.GetComeCode();
            string bankcode = this.ddlBankName.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "SHOWCHEQUE", bankcode, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();
                return;
            }


            Session["tblbank"] = ds1.Tables[0];

            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblbank"];
            this.gvCheque.DataSource = tbl1;
            this.gvCheque.DataBind();
        }




        protected void chqBank_CheckedChanged(object sender, EventArgs e)
        {
            if (chqBank.Checked == true)
            {
                this.txtFirstChq.Visible = true;
                this.txtFirstChq1.Visible = true;
                this.lblChequeno1.Visible = true;
                this.lblChequeno2.Visible = true;
                this.btnGenerate.Visible = true;
                this.txtFirstChq.Text = "";
                this.txtFirstChq1.Text = "";

            }
            else
            {
                this.txtFirstChq.Visible = false;
                this.txtFirstChq1.Visible = false;
                this.lblChequeno1.Visible = false;
                this.lblChequeno2.Visible = false;
                this.gvCheque.Visible = false;
                this.btnGenerate.Visible = false;
            }
        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblbank"];
            for (int i = 0; i < gvCheque.Rows.Count; i++)
            {
                tbl1.Rows[i]["bstatus"] = ((TextBox)this.gvCheque.Rows[i].FindControl("txtStatus")).Text.Trim();

                tbl1.Rows[i]["flag"] = (((CheckBox)gvCheque.Rows[i].FindControl("checkIssued")).Checked) ? "True" : "False";


            }



            Session["tblbank"] = tbl1;

        }
        protected void lUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataTable dt1 = (DataTable)Session["tblbank"];
            this.SaveValue();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            foreach (DataRow dr in dt1.Rows)
            {

                string bankcode = dr["bankcode"].ToString().Trim();
                string chequeno = dr["chequeno"].ToString().Trim();
                string flag = dr["flag"].ToString().Trim();
                string vounum = dr["vounum"].ToString().Trim();
                string bstatus = dr["bstatus"].ToString().Trim();

                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INSERTUPDATE", bankcode, chequeno, flag, vounum, bstatus, "", "", "",
                     "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
            }
        }



        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblbank"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string bankcode = ddlBankName.SelectedValue.ToString();
            int a = Convert.ToInt32(this.txtFirstChq.Text);
            int b = Convert.ToInt32(this.txtFirstChq1.Text);
            int nofcheque = b - a;
            string c = "00";
            for (int i = 0; i <= nofcheque; i++)
            {
                DataRow dr = dt.NewRow();
                dr["comcod"] = comcod;
                dr["bankcode"] = bankcode;
                // dr["chequeno"] =  a + i;

                int chqueno = a + i;
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                string s = (string)converter.ConvertTo(chqueno, typeof(string));


                // dr["chequeno"] = (comcod == "3336" || comcod == "3337" || comcod == "3101") ? (s) : (a + i).ToString();
           
                dr["chequeno"] = (comcod == "3336" || comcod == "3337" || comcod == "3101") ? ASTUtility.Right("0000000" + (s), 7) : (a + i).ToString();
                dr["flag"] = false;
                dr["vounum"] = "";
                dr["bstatus"] = "";
                dt.Rows.Add(dr);

            }

            Session["tblbank"] = dt;

            this.Data_Bind();
            this.gvCheque.Visible = true;

        }
    }
}