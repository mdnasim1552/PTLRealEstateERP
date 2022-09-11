using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Tickets
{
    public partial class RequestTicketId : System.Web.UI.Page
    {
        ProcessAccess _linkVendorDb = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "User Ticket Request";

                this.TicketRequest();

            }

        }
       
        public void TicketRequest()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userId = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string usrdesig = hst["usrdesig"].ToString();
            
            this.txtUserid.Text = userId;
            this.txtUsername.Text = username;
            this.txtDesig.Text = usrdesig;
            



        }

        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if(hst==null)
            {
                return;
            }
            //[UserName],[UserEmail],[UserPass],[RoleId],[MAC],[FullName],[UserPhone]
            string comcod = hst["comcod"].ToString();
          
            string userId = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string usrdesig = hst["usrdesig"].ToString();
            string UserEmail = "";
            string RoleId = "10";
            string MAC = "10";
            string FullName = hst["userfname"].ToString();
            if (hst != null) 
            {
                DataSet ds = _linkVendorDb.InsertTicketUser(comcod, comcod+'_'+username, UserEmail, RoleId, MAC, FullName);
            }

        }
    }
}