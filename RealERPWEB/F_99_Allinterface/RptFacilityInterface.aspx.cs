using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptFacilityInterface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ModuleName();

            }
        }

        private void ModuleName()
        {
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + 1000 + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Complains</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + 1000 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Engr. Check</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + 1000 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Budget" + "</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + 1000 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + 1000 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Quotation</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + 1000 + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Material Req.</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + 1000 + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + 1000 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>QC Pending</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + 1000 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Customer Care" + "</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + 1000 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + "Work Done" + "</div></div></div>";
           
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}