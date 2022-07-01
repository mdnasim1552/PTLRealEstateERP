using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_30_Facility
{
    public partial class ComplaintMatReq : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        //private void getReqNo()
        //{
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
        //        this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
        //    }
        //}
    }
}