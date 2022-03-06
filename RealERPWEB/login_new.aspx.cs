using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB
{
    public partial class login_new : System.Web.UI.Page
    {
        string ind = "x";
        public static int day = 0;
        #region Property
        public DataTable DataSource
        {
            get;
            set;
        }
        public TreeNode SelectedNode { get; set; }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.Initilize();
                this.getComName();
                this.GetHitCounter();
                this.getListModulename();
                listComName_SelectedIndexChanged(null, null);
                Session.Remove("tbllog1");
                //this.notice();
                if ((Hashtable)Session["tblLogin"] == null)
                    return;
                this.txtuserid.Text = ((Hashtable)Session["tblLogin"])["username"].ToString();
                this.txtuserpass.Text = ((Hashtable)Session["tblLogin"])["password"].ToString();
            }
            Session.Remove("tblLogin");

        }
        private void Initilize()
        {

            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GoupCompany();
            if (ds1 == null)
                return; 

        }
        private void getComName()
        {
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.listComName.DataTextField = "comnam";
            this.listComName.DataValueField = "comcod";
            this.listComName.DataSource = ds1.Tables[0];
            this.listComName.DataBind();

            if (this.listComName.Items.Count > 0)
            {
                // DataRow[] dr = ds1.Tables[0].Select("comcod like '3%'");
                DataView dv = ds1.Tables[0].DefaultView;
                dv.RowFilter = ("comcod like '3%'");
                dv.Sort = "slnum";
                DataTable dt1 = dv.ToTable();

                if (dt1.Rows.Count == 0)
                {

                    this.listComName.SelectedIndex = 0;
                }
                else
                {
                    //nahid vs uzzal
                    this.listComName.SelectedValue = (Session["ixComcod"] == null ? dt1.Rows[0]["comcod"].ToString() : Session["ixComcod"].ToString());

                }
            }
            Session["tbllog"] = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
                return;
            string comcod = hst["comcod"].ToString();
            string module = hst["modulenam"].ToString().Trim();
            if (comcod != "")
            {
                this.listComName.SelectedValue = comcod;
            }

        }

        private void GetHitCounter()
        {
            Session.Remove("tblhcntlmt");
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            DataSet ds3 = ulog.ExpDate();
            DataSet ds2 = ulog.GetHitCounterLimit();
            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = (ASTUtility.Left(this.listComName.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGIN", "", "", "", "", "", "", "", "", "");
            Session["tblhcntlmt"] = ds2.Tables[0];


            double cnumber = Convert.ToDouble(ds1.Tables[0].Rows[0]["cnumber"]);
            double cntlim1, cntlim2, cntlim3, dcntlim1, dcntlim2, dcntlim3;
            cntlim1 = Convert.ToDouble(ds2.Tables[0].Rows[0]["cntval"]);
            cntlim2 = Convert.ToDouble(ds2.Tables[0].Rows[1]["cntval"]);
            cntlim3 = Convert.ToDouble(ds2.Tables[0].Rows[2]["cntval"]);
            dcntlim1 = Convert.ToDouble(ds5.Tables[0].Rows[0]["cntval"]);
            dcntlim2 = Convert.ToDouble(ds5.Tables[0].Rows[1]["cntval"]);
            dcntlim3 = Convert.ToDouble(ds5.Tables[0].Rows[2]["cntval"]);
            double dcnumber = Convert.ToDouble(ds5.Tables[1].Rows[0]["cnumber"]);

            DateTime dt1 = Convert.ToDateTime(ds3.Tables[0].Rows[0]["expdate"]);
            DateTime dt2 = System.DateTime.Today;
            day = ASTUtility.Datediffday(dt1, dt2);
            if (ds5.Tables[2].Rows.Count != 0)
            {
                string commsg = ds5.Tables[2].Rows[0]["commsg"].ToString();
                string msgCol = ds5.Tables[2].Rows[0]["commsgcol"].ToString(); //"text-danger";
                if (commsg.Length != 1)
                {
                    //this.pnlmsgbox.Visible = true;
                    //this.lblalrtmsg.InnerText = commsg;
                    //this.lblalrtmsg.Attributes.Add("class", msgCol + " text-justify");
                }
            }
            else
            {
                //this.pnlmsgbox.Visible = false;

            }


            if ((cnumber >= cntlim1 && cnumber < cntlim2) || (day < 10) || (dcnumber >= dcntlim1 && dcnumber < dcntlim2))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

            }
            else if ((cnumber >= cntlim2 && cnumber < cntlim3) || (day <= 5) || (dcnumber >= dcntlim2 && cnumber < dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);
            }

            else if ((cnumber >= cntlim3) || (day <= 0) || (dcnumber >= dcntlim3))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Could Not Loaded MktLIB.dll. Please Repair Selected File.');", true);

            }
        }
        private void getListModulename()
        {

            string comcod = this.listComName.SelectedValue.ToString();
            ProcessAccess ulogin = new ProcessAccess();
            
            DataSet ds51 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", "", "", "", "", "", "", "", "", "");           
            this.ListModulename.DataTextField = "modulename";
            this.ListModulename.DataValueField = "moduleid";
            this.ListModulename.DataSource = ds51.Tables[0];
            this.ListModulename.DataBind();
            ViewState["tblmoduleName"] = ds51.Tables[0];
        }
        protected void listComName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MasComNameaAdd();
        }
        private void MasComNameaAdd()
        { 
            this.Image1.ImageUrl = "~/Image/" + "LOGO" + this.listComName.SelectedValue.ToString() + ".PNG";
        }

    }
}