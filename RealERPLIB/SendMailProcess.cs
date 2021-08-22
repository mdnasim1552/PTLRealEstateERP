using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using RealERPLIB;
using System.Net;

namespace RealERPLIB
{
   public class SendMailProcess : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        private Hashtable _errObj;

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

<<<<<<< HEAD

        public bool SendMail(string subject, string text, string userid, string frmname)
=======
       
        public bool SendMail( string subject, string text, string userid, string frmname)
>>>>>>> b300eea84dbe67331754c7fb87965eac549b6019
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
                string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
                string SMSText = text; //  

                string var_from = "ccl-non-masking";


                string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
                string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
                for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
                {
                    string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from + "&to=" + mobile + "&message=" + SMSText);

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                }


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }

        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }
    }
}
