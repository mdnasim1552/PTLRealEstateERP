
using System;
using System.Collections.Generic;
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
using RealEntity;
namespace RealERPWEB.F_04_Bgd
{
    public partial class EntryCustomer : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



            }

        }

        private string GetComcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            string name = "Emdadul Haue";

            //   wcfServices.Service objwcfser = new wcfServices.Service();
            //WcfServices.WCFService objwcfser = new WcfServices.WCFService();
            //  string name = objwcfser.GetData(1); 
        }
    }
}