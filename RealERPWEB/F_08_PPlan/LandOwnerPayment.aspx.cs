using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;
using RealEntity.C_08_PPlan;
using RealERPLIB;
namespace RealERPWEB.F_08_PPlan
{
    public partial class LandOwnerPayment : System.Web.UI.Page
    {
        ProcessAccess _processAccess = new ProcessAccess();
        UserProjPlan objUserproj = new UserProjPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "SCHEDULE";
                this.LoadPaymentDetails();
            }
        }



        protected void lbtnGenerate_OnClick(object sender, EventArgs e)
        {
            this.lbtnAddInstallment.Visible = true;
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst =
                new List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>();


            double toamt = Convert.ToDouble("0" + this.txtToamt.Text.Trim());
            double lnamt = Convert.ToDouble("0" + this.txtinsamt.Text.Trim());
            int dur = Convert.ToInt32(this.ddlMonth.SelectedValue.ToString());
            string date = this.txtstrdate.Text.Trim();
            DateTime lndate;
            for (int i = 0; i < 500; i++)
            {


                if (toamt > 0)
                {
                    lnamt = (toamt > lnamt) ? lnamt : toamt;

                    if (i == 0)
                    {

                        lst.Add(new E_CLassPaymetSch.OwnerInstallment(DateTime.Parse(date), lnamt));
                        toamt = toamt - lnamt;
                        continue;
                    }


                    lndate = Convert.ToDateTime(lst[i - 1].insdate).AddMonths(dur);
                    lst.Add(new E_CLassPaymetSch.OwnerInstallment(lndate, lnamt));
                    toamt = toamt - lnamt;
                }
                else
                {
                    break;

                }
            }

            ViewState["tblins"] = lst;
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            this.gvOwnerPay.DataSource = (List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>)ViewState["tblins"];
            this.gvOwnerPay.DataBind();
            this.FooterCalculation((List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>)ViewState["tblins"]);
        }


        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void FooterCalculation(List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst)
        {
            if (lst.Count == 0)
                return;
            ((Label)this.gvOwnerPay.FooterRow.FindControl("gvlFToamt")).Text =
                (lst.Sum(l => l.insamt)).ToString("#,##0;(#,##0); ");

        }

        private void Save_Value()
        {
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst =
                (List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>)ViewState["tblins"];
            for (int i = 0; i < this.gvOwnerPay.Rows.Count; i++)
            {

                double amount =
                    ASTUtility.StrPosOrNagative(
                        ASTUtility.ExprToValue("0" + ((TextBox)this.gvOwnerPay.Rows[i].FindControl("gvtxtamt")).Text.Trim()));
                string paymentDate =
                    Convert.ToDateTime(((TextBox)this.gvOwnerPay.Rows[i].FindControl("txtgvinstdate")).Text.Trim())
                        .ToString("dd-MMM-yyyy");
                ;


                lst[i].insamt = amount;
                lst[i].insdate = DateTime.Parse(paymentDate);
            }
            ViewState["tblins"] = lst;
        }

        protected void lbtnAddInstallment_OnClick(object sender, EventArgs e)
        {
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst =
                (List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>)ViewState["tblins"];
            double amount = 0;
            DateTime paymentDate = System.DateTime.Today;
            lst.Add(new E_CLassPaymetSch.OwnerInstallment(paymentDate, amount));

            ViewState["tblins"] = lst;
            this.Data_Bind();
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }


        protected void lbtnFinalUpdate_OnClick(object sender, EventArgs e)
        {
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst = (List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment>)ViewState["tblins"];

            string comcod = this.GetCompCode();


            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ASITUtility03.ListToDataTable(lst);
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            string pactcode = this.Request.QueryString["pactcode"];
            string rescode = this.Request.QueryString["sircode"];
            bool result = _processAccess.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INUPPAYMENT", ds1, null, null, pactcode, rescode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = _processAccess.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";


            }

        }

        private void LoadPaymentDetails()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"];
            string rescode = this.Request.QueryString["sircode"];
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst = objUserproj.GetPayment(comcod, pactcode,
                rescode);
            ViewState["tblins"] = lst;
            this.Data_Bind();
        }
    }
}