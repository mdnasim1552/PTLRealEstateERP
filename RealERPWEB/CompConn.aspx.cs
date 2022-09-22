
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using RealERPLIB;
using System.Collections;

namespace RealERPWEB
{


    public partial class CompConn : System.Web.UI.Page
    {
        DataAccessOLDB da = new DataAccessOLDB();
        ProcessAccess _linkVendorDb = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pnlbillalrt.Visible = false;
            this.pnlTop.Visible = false;
            this.pnlmsg.Visible = false;
            if (!IsPostBack)
            {
                try
                {
                    string sysID = "1";
                    string qs = "ptldbd2021Nahid#$CompbDb*%Process";
                    string pnlType = "sysMsg";

                    //string qs = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["AccessToken"].ToString()));
                    //string sysID = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["sysID"].ToString()));
                    //string pnlType = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.Request.QueryString["sysType"].ToString()));


                    if (qs == "ptldbd2021Nahid#$CompbDb*%Process")
                    {
                        if (pnlType == "sysExp")
                        {
                            GetProcess(sysID);

                            this.pnlbillalrt.Visible = false;
                            this.pnlTop.Visible = true;
                            this.pnlmsg.Visible = false;
                            this.pnlAlertMsg.Visible = false;
                        }
                        else if (pnlType == "sqlExp")
                        {
                            GetProcessSqlInfo(sysID);
                            this.pnlbillalrt.Visible = false;
                            this.pnlTop.Visible = false;
                            this.pnlmsg.Visible = false;
                            this.pnlDtPropertis.Visible = true;
                            this.pnlAlertMsg.Visible = false;
                        }
                        else if (pnlType == "sysMsg")
                        {
                            GetAlertMsgInfo(sysID);
                            this.pnlbillalrt.Visible = false;
                            this.pnlTop.Visible = false;
                            this.pnlmsg.Visible = false;
                            this.pnlDtPropertis.Visible = false;
                            this.pnlAlertMsg.Visible = true;
                        }
                        else
                        {
                            this.pnlbillalrt.Visible = true;
                            this.pnlTop.Visible = false;
                            this.pnlmsg.Visible = false;
                            this.pnlAlertMsg.Visible = false;
                            GetServiceBillAltMsg(sysID);

                        }
                    }
                    else
                    {
                        //Process[] AllProcesses = Process.GetProcesses();
                        //foreach (var process in AllProcesses)
                        //{
                        //    if (process.MainWindowTitle != "")
                        //    {
                        //        string s = process.ProcessName.ToLower();
                        //        if (s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox")
                        //            process.Kill();
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Process[] AllProcesses = Process.GetProcesses();
                    //foreach (var process in AllProcesses)
                    //{
                    //    if (process.MainWindowTitle != "")
                    //    {
                    //        string s = process.ProcessName.ToLower();
                    //        if (s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox")
                    //            process.Kill();
                    //    }
                    //}
                }

            }
        }

        private void GetAlertMsgInfo(string sysID)
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds2 = _linkVendorDb.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMPINFO", "", "", "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                txtCompMsg.Value = ds2.Tables[0].Rows[0]["commsg"].ToString();
                ddlMsgColor.Text = ds2.Tables[0].Rows[0]["commsgcol"].ToString();
                string msgFlg = ds2.Tables[0].Rows[0]["msgflg"].ToString();
                rbtnMsgStatus.SelectedValue = msgFlg;

            }
            catch (Exception ex)
            {
                this.pnlmsg.Visible = true;
                this.msgBox.InnerText = "Error" + ex;
            }
        }
        
        private void GetProcessSqlInfo(string sysID)
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds2 = _linkVendorDb.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETSYSTABLEDTPROPERTISE", "", "", "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                this.txtDtProperties.Text = ds2.Tables[0].Rows[0]["cnumber"].ToString();
                this.lblHL1.Value = ds2.Tables[1].Rows[0]["objectid"].ToString();
                this.lblHL2.Value = ds2.Tables[1].Rows[1]["objectid"].ToString();
                this.lblHL3.Value = ds2.Tables[1].Rows[2]["objectid"].ToString();

                this.txtL1.Text = ds2.Tables[1].Rows[0]["version"].ToString();
                this.txtL2.Text = ds2.Tables[1].Rows[1]["version"].ToString();
                this.txtL3.Text = ds2.Tables[1].Rows[2]["version"].ToString();


            }
            catch (Exception ex)
            {
                this.pnlmsg.Visible = true;
                this.msgBox.InnerText = "Error" + ex;
            }


        }


        private void GetServiceBillAltMsg(string sysID)
        {
            string comcod = GetComCode();
            DataSet ds2 = _linkVendorDb.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETBILLALRTMESSAGE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.txtAltMessage.Value = ds2.Tables[0].Rows[0]["COMMSG"].ToString();
            this.txtColorCode.Value = ds2.Tables[0].Rows[0]["COMMSGCOL"].ToString();
            string msgflg = ds2.Tables[0].Rows[0]["MSGFLG"].ToString();
            this.rbtLsit.SelectedValue = msgflg;

        }

        private void GetProcess(string sysID)
        {
            this.HidnsysID.Value = sysID;
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetHitCounter();
            DataSet ds3 = ulog.ExpDate();
            DataSet ds2 = ulog.GetHitCounterLimit();
            //cntstep, cntval, cntdes
            this.txt_CNUMBER.Text = ds1.Tables[0].Rows[0]["cnumber"].ToString();
            this.txt_NineFive.Text = ds2.Tables[0].Rows[0]["cntval"].ToString();
            this.txt_NineSix.Text = ds2.Tables[0].Rows[1]["cntval"].ToString();
            this.txt_NineSeven.Text = ds2.Tables[0].Rows[2]["cntval"].ToString();
            this.txtExpDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");



        }
        protected void btnUpdateSqlLimit_ServerClick(object sender, EventArgs e)
        {
            UserLogin ulog = new UserLogin();

            try
            {
                double txt_665895 = Convert.ToDouble(txt_NineFive.Text);
                double txt_665896 = Convert.ToDouble(txt_NineSix.Text);
                double txt_665897 = Convert.ToDouble(txt_NineSeven.Text);
                double cnumber = Convert.ToDouble(txt_CNUMBER.Text);
                //  string txtExpDate = Convert.ToDateTime(this.txtExpDate.Text.Trim()).ToString("dd-MM-yyyy");
                DateTime txtExpDate = Convert.ToDateTime(this.txtExpDate.Text.Trim());

                //  Hit counter Update
                ulog.UpdateHitCounter((cnumber).ToString());



                string cmd = "update hcntlmt set CNTVAL='" + txt_665895 + "' where CNTSTEP='01'";
                da.ExecuteCommand(cmd);
                string date1 = "#" + this.txtExpDate.Text + " 12:00:00 AM" + "#";
                // cmd = "update expdinf set date=" + date1 ;
                cmd = "update expdinf set [date]=" + date1;
                da.ExecuteCommand(cmd);


                cmd = "update hcntlmt set CNTVAL='" + txt_665896 + "' where CNTSTEP='02'";
                da.ExecuteCommand(cmd);
                cmd = "update hcntlmt set CNTVAL='" + txt_665897 + "' where CNTSTEP='03'";
                da.ExecuteCommand(cmd);


                string sysid = this.HidnsysID.Value.ToString();

                bool resultb = _linkVendorDb.SysExpiryUpdate(sysid, txtExpDate);
                if (resultb == true)
                {
                    this.pnlTop.Visible = false;
                    this.pnlmsg.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

                }
                else
                {
                    this.pnlTop.Visible = true;
                    this.pnlmsg.Visible = false;

                }
            }
            catch (Exception ex)
            {
                this.pnlmsg.Visible = true;
                this.msgBox.InnerText = "error" + ex;
            }

        }





        protected void btnAlrtMsg_ServerClick(object sender, EventArgs e)
        {
            string comcod = GetComCode();

            string txtMsg = txtAltMessage.Value;
            string txtColor = txtColorCode.Value;

            string msgflg = this.rbtLsit.SelectedValue.ToString();

            bool resultb = _linkVendorDb.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEBILLALRTMESSAGE", txtMsg.ToString(), txtColor, msgflg, "", "", "", "", "");
            if (!resultb)
            {
                this.pnlbillalrt.Visible = true;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = false;
            }
            else
            {
                this.pnlbillalrt.Visible = false;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

            }
        }

        protected void btnDtPropSave_ServerClick(object sender, EventArgs e)
        {
            string comcod = GetComCode();

            string txtDtProper = this.txtDtProperties.Text.ToString();
            string txtL1 = this.txtL1.Text.ToString();
            string txtL2 = this.txtL2.Text.ToString();
            string txtL3 = this.txtL3.Text.ToString();

            string lblHL1 = this.lblHL1.Value;
            string lblHL2 = this.lblHL2.Value;
            string lblHL3 = this.lblHL3.Value;

            bool resultb = _linkVendorDb.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATESYSTABLEDTPROPERTISE", txtDtProper, txtL1, txtL2, txtL3, lblHL1, lblHL2, lblHL3, "", "", "");
            if (!resultb)
            {
                this.pnlbillalrt.Visible = true;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = false;
            }
            else
            {
                this.pnlbillalrt.Visible = false;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

            }
        }

        private string GetComCode()
        {
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            string comcod = ds1.Tables[0].Rows[0]["comcod"].ToString();
            return comcod;
        }

        protected void btnMsgSave_ServerClick(object sender, EventArgs e)
        {
            string comcod = GetComCode();

            string txtCompMsg = this.txtCompMsg.Value.ToString();
            string txtMsgColor = this.ddlMsgColor.Text.ToString();
            string msgStatus = this.rbtnMsgStatus.SelectedValue.ToString();

            bool resultb = _linkVendorDb.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUPDATEMSG", txtCompMsg, txtMsgColor, msgStatus, "", "", "", "", "", "", "");
            if (!resultb)
            {
                this.pnlbillalrt.Visible = false;
                this.pnlTop.Visible = false;
                this.pnlmsg.Visible = false;
                this.pnlAlertMsg.Visible = true;
            }
            else
            {
                this.pnlbillalrt.Visible = false;
                this.pnlTop.Visible = false;
                this.pnlAlertMsg.Visible = true;
                this.pnlmsg.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('logIn.aspx') }, 5000);", true);

            }
        }
    }

}
