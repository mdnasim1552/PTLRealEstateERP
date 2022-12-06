using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using RealERPLIB;
using System.Net;
using System.IO;
using RestSharp;



namespace RealERPLIB
{

    public class SendSmsProcess : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        private Hashtable _errObj;
       
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        //public bool SendSmms(string text, string userid, string frmname)
        //{
        //    try
        //    {
        //        string comcod = this.GetCompCode();
        //        DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
        //        string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
        //        string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
        //        string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
        //        string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
        //        string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
        //        string SMSText = text; //        
        //        string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
        //        string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
        //        for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
        //        {
        //            string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

        //            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + sender
        //               + "&SMSText=" + SMSText + "&GSM=" + mobile + "&type=longSMS");

        //            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //            string responseString = respStreamReader.ReadToEnd();
        //            respStreamReader.Close();
        //            myResp.Close();
        //        }


        //        return true;
        //    }
        //    catch (Exception exp)
        //    {
        //        this.SetError(exp);
        //        return false;
        //    }// try


        //}



        public bool SendSmms(string text, string userid, string frmname)
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
        public bool SendSmmsPwd(string comcode, string text, string mobilenum)
        {
            try
            {
                string comcod = comcode;
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOFORFORGOTPASS","", "", "", "", "");
                string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
                
                string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
                string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";

                string mobile = "88" + mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                string mobilewccode =  mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                    string apiinfo = "";
                Random rnd1 = new Random(5); //seed value 10
                string cmsid = rnd1.Next().ToString();
                switch (comcod)
                {
                    case "3315"://Assure
                    case "3316"://Assure
                    case "3317"://Assure
                        apiinfo = ApiUrl + "&username=" + user + "&password=" + pass + "&message=" + text + "&msisdn=" + mobilewccode + "&cli=" + sender;
                        break;

                    case "3101"://Assure
                    case "3366"://lanco
                        apiinfo = ApiUrl + "&username=" + user + "&password=" + pass + "&message=" + text + "&msisdn=" + mobilewccode + "&cli=" + sender;
                        break;

                    default:
                        apiinfo = ApiUrl + user + "&password=" + pass + "&sender=" + sender + "&SMSText=" + text + "&GSM=" + mobile + "&type=longSMS";                      
                        break;
                }
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(apiinfo);
                myReq.KeepAlive = false;
                //myReq.Headers=
                myReq.ProtocolVersion = HttpVersion.Version10;
                myReq.ServicePoint.ConnectionLimit = 1;
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
            


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }
        // Nahid 20221205
        public bool SendSms_SSL_Single(string comcode, string text, string mobilenum)
        {
            string comcod = comcode;
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOFORFORGOTPASS", "", "", "", "", "");
            string Single_Sms_Url = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim();
            string Single_Sms_Sid = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender
            string Single_Sms_api_token = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"ASITNAHID";  //Sender
            string mobile = "88" + mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
            Random rnd1 = new Random(9); //seed value 10
            string cmsid = rnd1.Next().ToString();
            var options = new RestClientOptions(Single_Sms_Url)
            {
                ThrowOnAnyError = true,
                Timeout = 1000  // 1 second
            };             
            var client = new RestClient(Single_Sms_Url);
            var request = new RestRequest();

            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");             
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("api_token", Single_Sms_api_token);
            request.AddParameter("sid", Single_Sms_Sid);
            request.AddParameter("msisdn", mobile);
            request.AddParameter("sms", text);
            request.AddParameter("csms_id", cmsid);
            var response = client.Execute(request); 
            return response.IsSuccessful;
        }



        // Create by Md Ibrahim Khalil 

        public bool SendSMSClient(string comcode, string text, string mobilenum)
        {
            try
            {
                string comcod = comcode;
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOFORFORGOTPASS", "", "", "", "", "");
                string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender

                string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
                string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";

                string mobile = "88" + mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                string mobilewccode = mobilenum; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                string apiinfo = "";
                string var_from = "ccl-non-masking";
                switch (comcod)
                {
                    case "3356"://for Intech
                        apiinfo = ApiUrl + user + "&password=" + pass + "&masking=" + sender + "&MsgType=TEXT" + "&receiver=" + mobile + "&message=" + text;               
                        break;
                    default:
                        apiinfo = ApiUrl + user + "&password=" + pass + "&from=" + var_from + "&to=" + mobile + "&message=" + text;
                        break;
                }        
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(apiinfo);

                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            //string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
            //string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
            //for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
            //{
            //    string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
           // http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=Intech&userName=Intech24&password=f61874a0e2550be21aa2fa7171e7c347&MsgType=TEXT&receiver=01860454560&message=hello     
                //}


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }


        public bool SendSmmsEdit(string text, string userid)
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOEDIT", userid, "", "", "", "");
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


        //protected void btnSent_Click(object sender, EventArgs e)
        //{
        //     use the API URL here  
        //    string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=YourUserName:YourPassword&senderID=YourSenderID&    receipientno=1234567890&msgtxt=This is a test from mVaayoo API&state=4";
        //     Create a request object  
        //    WebRequest request = HttpWebRequest.Create(strUrl);
        //     Get the response back  
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream s = (Stream)response.GetResponseStream();
        //    StreamReader readStream = new StreamReader(s);
        //    string dataString = readStream.ReadToEnd();
        //    response.Close();
        //    s.Close();
        //    readStream.Close();
        //}  

        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }

      
    }
}
