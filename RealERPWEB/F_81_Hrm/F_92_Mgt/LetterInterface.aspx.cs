using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class LetterInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtdate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
            getLetterCount();
            getAllData();
        }

        private void getAllData()
        {
            Session.Remove("tbldata");
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETALLREC", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvAllRec.DataSource = null;
                this.gvAllRec.DataBind();
                return;
            }


            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = ds.Tables[0];
            string type = this.RadioButtonList1.SelectedValue.ToString();
            //offer letter
            if (type == "10003")
            {
                view.RowFilter = "sendappflag='False' and sendconflag='False' and sendoffflag='False' ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            else if (type == "10002")
            {
                view.RowFilter = "sendappflag='False' and sendconflag='False' and sendoffflag='True' ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }
            else if (type == "10025")
            {
                view.RowFilter = "sendappflag='True' and sendconflag='False' and sendoffflag='True' ";
                view.Sort = "advno desc";
                dt1 = view.ToTable();
            }


            Session["tbldata"] = dt1;

            //DataTable ds = (DataTable)Session["tbldata"];
            this.gvAllRec.DataSource = dt1;
            this.gvAllRec.DataBind();
        }

        //private void loadGrid()
        //{
        //    DataTable ds = (DataTable)Session["tbldata"];
        //    this.gvAllRec.DataSource = ds;
        //    this.gvAllRec.DataBind();
        //}
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void getLetterCount()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "LETTER_COUNT", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                return;
            this.RadioButtonList1.Items[0].Text = "<a> Offer Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["offletter"] + "</span></a>";//
            this.RadioButtonList1.Items[1].Text = "<a> Appointment Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["appletter"] + "</span></a>";//
            this.RadioButtonList1.Items[2].Text = "<a> Confirmation Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["confletter"] + "</span></a>";//


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.RadioButtonList1.SelectedValue.ToString();
            switch (type)
            {
                case "10003":
                    //this.gvAllRec.FindControl["lnkOfferLetter"]
                    getAllData();
                    
                    break;
                case "10002":
                    getAllData();
                    break;
                case "10025":
                    getAllData();
                    break;
            }

        }

        protected void gvAllRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string type = this.RadioButtonList1.SelectedValue.ToString();
                if (type == "10003")
                {
                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = false;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;

                }else if(type== "10002")
                {
                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = false;
                }else if(type== "10025")
                {
                    ((HyperLink)e.Row.FindControl("lnkOfferLetter")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkAppoint")).Visible = true;
                    ((HyperLink)e.Row.FindControl("lnkConfirmation")).Visible = true;
                }

            }
        }
    }
}