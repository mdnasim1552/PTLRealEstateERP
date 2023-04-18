using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using RealERPLIB;


namespace RealERPWEB.F_34_Mgt
{
    public partial class PrintChequeStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            }


            if (this.ddlChequeno.Items.Count == 0)
            {
                this.GetPrintCheque();

            }



        }


        private void GetPrintCheque()

        {
            Session.Remove("Printcheque");

            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text + "%";

            string txtSProject = "%" + this.txtSrcPro.Text + "%";


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "GETPRINTCHQSTATUS", txtSProject, "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlChequeno.DataTextField = "chequeno1";
            this.ddlChequeno.DataValueField = "chequeno";
            this.ddlChequeno.DataSource = ds1.Tables[0];
            this.ddlChequeno.DataBind();
            Session["Printcheque"] = ds1.Tables[0];
  

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetPrintCheque();
        }

        protected void lblShow_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = " ";
            string comcod = this.GetCompCode();
            string chequeno = this.ddlChequeno.SelectedValue.ToString();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "SHOWCHQSTATUSUDATA", chequeno, "", "", "", "", "", "", "", "");
            
            ViewState["dtstatus"] = ds2.Tables[0];

            if (ds2.Tables[0] == null)
                return;

            this.grvacc.DataSource = ds2.Tables[0];
            this.grvacc.DataBind();




        }

        private void Session_update()
        {
            DataTable dt = (DataTable)ViewState["dtstatus"];
            int index;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                string chqstatus = (((CheckBox)grvacc.Rows[i].FindControl("chqprint")).Checked) ? "True" : "False";
                index = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + i;
                dt.Rows[index]["chqstatus"] = chqstatus;

            }

            ViewState["dtstatus"] = dt;

        }


        protected void lbtnChqUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            this.Session_update();

            DataTable tbl2 = (DataTable)ViewState["dtstatus"];
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string chqstatus = tbl2.Rows[i]["chqstatus"].ToString();
                string chequeno = tbl2.Rows[i]["chequeno"].ToString();
                bool result = MktData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "UPDATECHQSTATUS", chequeno, chqstatus,
                        "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail !!!";

                    return;
                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully !!!";

                }

            }


        }
    }

}