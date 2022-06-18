using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptFacilityInterface : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfrmdate.Text = System.DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                ModuleName();
                getComplainList();
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ModuleName()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETTOPNUMBER", date1, date2, "", "", "", "", "", "", "", "", "");
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + dt.Rows[0][1].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Complains</div></div></div>";
                this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][2].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Engr. Check</div></div></div>";

                this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + dt.Rows[0][3].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Budget" + "</div></div></div>";
                this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][4].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

                this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + dt.Rows[0][5].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Quotation</div></div></div>";

                this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + dt.Rows[0][6].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Material Req.</div></div></div>";

                this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + dt.Rows[0][7].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Status</div></div></div>";
                this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + dt.Rows[0][8].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>QC Pending</div></div></div>";
                this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + dt.Rows[0][9].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Customer Care" + "</div></div></div>";
                this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][10].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + "Work Done" + "</div></div></div>";

            }


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    pnlComplainCount.Visible = true;
                    getComplainList();
                    break;
            }
        }

        private void getComplainList()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETCOMPLAINLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvComplainList.DataSource = ds.Tables[0];
            gvComplainList.DataBind();
        }
    }
}