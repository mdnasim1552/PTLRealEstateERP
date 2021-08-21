﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.Tickets
{
    public partial class CreateTicket : System.Web.UI.Page
    {
        ProcessAccess _linkVendorDb = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();

            string ticketType = this.ddlTicketType.SelectedValue.ToString();
            string txtTdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string txtTicketDesc = this.txtTicketDesc.Text.ToString();
            //string txtRemarks = this.txtRemarks.Text.ToString();
            string priority = this.ddlPriority.SelectedValue.ToString();

            bool resultb = _linkVendorDb.InsertTicket("47", txtTicketDesc, ticketType, "99200", priority, txtTdate, "18", "", "", "3101001");
            if (!resultb)
            {

            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Ticket Created Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Image/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            imageUpload.SaveAs(folderPath + Path.GetFileName(imageUpload.FileName));

            //Display the Picture in Image control.
            imageShow.ImageUrl = "~/Image/" + Path.GetFileName(imageUpload.FileName);
        }

    }
}