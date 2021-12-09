using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using RealERPLIB;

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class CreateLeavRule : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "CREATE LEAVE RULES";
                this.Create_table();
                this.ShowInformation();
                CommonButton();
                
            }
        }
        public void Create_table()
        {
            DataTable dt = new DataTable();
        

            //create colums here.
            dt.Columns.Add("comcod", Type.GetType("System.String"));
            dt.Columns.Add("gcod", Type.GetType("System.String"));
            dt.Columns.Add("year", Type.GetType("System.String"));
            dt.Columns.Add("leave", Type.GetType("System.String"));


            ViewState["tblleavinfo"] = dt;
          
        }

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);




            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = "51"; //Leave code 
            string tempddl2 = "5"; // Details 
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            //Session["storedata"] = ds1.Tables[0];

            DataView view = new DataView();
            view.Table = ds1.Tables[0];
            view.RowFilter = "hrgcod <> '51000'";

            Session["tblleavinfo"] = view.ToTable();
           
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblleavinfo"];
          

            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }

        private void lbtnUpdate_Click(object sender, EventArgs e)
        {
            
            this.GetLeaveData();
            string comcod = this.GetCompCode();
            string year = this.ddlyear.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblleavinfo"];
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {

                string code = dr["gcod"].ToString();
                string leav = dr["leave"].ToString();
               
               bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTCOMPLEAVEINFO", year, code, leav);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
            }
          
        }

        private void GetLeaveData()
        {
           
            DataTable dt = (DataTable)ViewState["tblleavinfo"];
           
            //var descdata = Server.HtmlEncode();
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
               
                Label code = this.grvacc.Rows[i].FindControl("lbgrcod1") as Label;
                TextBox TxtLeav = this.grvacc.Rows[i].FindControl("TxtLeav") as TextBox;
                         
                string lblspcode = code.Text;
                string leave = TxtLeav.Text;
                
                DataRow dr1 = dt.NewRow();
               
                dr1["gcod"] = lblspcode;            
                dr1["leave"] = leave;
                dt.Rows.Add(dr1);
            }

       
            ViewState["tblleavinfo"] = dt;
        }
    }
}