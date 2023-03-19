using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEntity;

namespace RealERPWEB.F_21_MKT
{
    public partial class DeviceIPSetup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.GetIpSetup();
            }
        }
        private void GetIpSetup()
        {
            string comcod = GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_COMWISE_MACH_IP");

            Session["IpSetup"] = ds1.Tables[0].DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf>().ToList();

            ds1.Dispose();

            this.Data_Bind();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void Data_Bind()
        {
            var IpSetupInf = Session["IpSetup"];
            this.grvIpSetup.PageSize = 10;
            this.grvIpSetup.DataSource = IpSetupInf;
            this.grvIpSetup.DataBind();
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf> dl = (List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf>)Session["IpSetup"];

                int index = dl.Count-1;

                string machno = dl[index].machno;

                this.txtMachineNo.Text = Convert.ToString(Convert.ToInt32(machno)+1);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }
            catch (Exception ex)
            {

            }
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvIpSetup.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string machno = this.txtMachineNo.Text;
            string ipaddress = this.txtIpAddress.Text;
            string alias = this.txtAlias.Text;
            string port = this.txtPort.Text;

            bool addDone = this.MktData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "ADD_COMWISE_MACH_IP", machno, ipaddress, alias, port);

            if (addDone)
            {
                string msg = "New IP Address Added Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                this.clearDataField();
                this.GetIpSetup();
            }
        }
        private void clearDataField()
        {
            this.txtMachineNo.Text = "";
            this.txtIpAddress.Text = "";
            this.txtAlias.Text = "";
            this.txtPort.Text = "";
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpPer_Click);
        }
        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = GetComCode();
            List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf> dt = (List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf>)ViewState["tblIpAddress"];

            for(int i=0; i<dt.Count; i++)
            {
                string machno = dt[i].machno.ToString();
                string ipaddress = dt[i].ipaddress.ToString();
                string machinealias = dt[i].machinealias.ToString();
                string port = dt[i].port.ToString();

                MktData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATE_COMWISE_MACH_IP", machno, ipaddress, machinealias, port);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            this.GetIpSetup();
        }
        private void SaveValue()
        {
            int rowindex;
            List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf> tblt02 = (List<RealEntity.C_21_Mkt.ECRMClientInfo.IPSetupInf>)Session["IpSetup"];
            
            for (int i = 0; i < this.grvIpSetup.Rows.Count; i++)
            {
                string machno = ((Label)this.grvIpSetup.Rows[i].FindControl("lblMachNo")).Text.ToString().Trim();
                string ipaddress = ((TextBox)this.grvIpSetup.Rows[i].FindControl("txtIpAddress")).Text.ToString().Trim();
                string machinealias = ((TextBox)this.grvIpSetup.Rows[i].FindControl("txtAlias")).Text.ToString().Trim();
                string port = ((TextBox)this.grvIpSetup.Rows[i].FindControl("txtPort")).Text.ToString().Trim();

                rowindex = (this.grvIpSetup.PageSize * this.grvIpSetup.PageIndex) + i;

                tblt02[rowindex].machno = machno;
                tblt02[rowindex].ipaddress = ipaddress;
                tblt02[rowindex].machinealias = machinealias;
                tblt02[rowindex].port = port;
            }
            ViewState["tblIpAddress"] = tblt02;
        }
    }
}