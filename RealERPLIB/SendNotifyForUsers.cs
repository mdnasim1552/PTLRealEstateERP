using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealERPLIB
{
    public class SendNotifyForUsers : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        private Hashtable _errObj;

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        public bool SendNotification(string ntitle, string ndetails, string recvid)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["userfname"].ToString();
                string ncreatedby = hst["usrid"].ToString();
                string ncreated = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                ndetails = ndetails + "Created by :"+ username;

                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "INSERT_NOTIFICAIOTN_USER_WISE", ntitle, ndetails, ncreated, ncreatedby, recvid, "", "");
               


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
