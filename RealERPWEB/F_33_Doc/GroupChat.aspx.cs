using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_33_Doc
{

    public partial class GroupChat : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
            this.UserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
            this.GetAllUser();
            this.ParentDir.Text = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            this.GetProject();
            this.GetAsindege();
            this.GetTasks();
            //  this.lblLoginInfo.Text = "User: " + ((DataTable)Session["tbllog1"]).Rows[0]["usrsname"].ToString()
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetAllUser()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSER", "%", "", "", "", "", "", "", "", "");
            this.ddlUser.DataTextField = "usrsname";
            this.ddlUser.DataValueField = "usrid";
            this.ddlUser.DataSource = ds1.Tables[0];
            this.ddlUser.DataBind();

        }
        private void GetProject()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "PRJCODELIST", "16%", "", "", "", "", "", "", "", "");
            this.ddlproject.DataTextField = "actdesc1";
            this.ddlproject.DataValueField = "actcode";
            this.ddlproject.DataSource = ds1.Tables[0];
            this.ddlproject.DataBind();
        }
        private void GetTasks()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_CHAT_MGT", "GET_DISCUSSION_TASK", "", "", "", "", "", "", "", "", "");

            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod;
            dr1["gcod"] = "0000";
            dr1["gdesc"] = "0000-None";
            ds1.Tables[0].Rows.Add(dr1);
            this.ddltask.DataTextField = "gdesc";
            this.ddltask.DataValueField = "gcod";
            this.ddltask.DataSource = ds1.Tables[0];
            this.ddltask.DataBind();


        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]

        public static string SaveGroupChat(string userid, string chatname, string actcode, string message, string probdate, string taskcod, string asinuser)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            string posteduser = ObjCommon.GetUserCode();
            string terminal = ObjCommon.Terminal();
            string session = ObjCommon.Sessionid();
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyy hh:mm:ss");
            ProcessAccess _DataEntry = new ProcessAccess();
            string userid1 = "";
            var usr = userid.Split(',');
            foreach (var data in usr)
            {
                userid1 = userid1 + data;
            }
            bool result = _DataEntry.UpdateTransInfo3(comcod, "SP_CHAT_MGT", "SAVE_AND_GET_CHAT", userid1, chatname, posteduser, posteddat, terminal, session, actcode, message, probdate, taskcod, asinuser);

            return "True";
        }
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetGroupChat()

        {

            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess _DataEntry = new ProcessAccess();
            string userid = ObjCommon.GetUserCode();
            DataSet result = _DataEntry.GetTransInfo(comcod, "SP_CHAT_MGT", "GET_GROUP_CHAT", userid, "", "");
            List<RealEntity.C_34_Mgt.EclassGroupChat> list2 = result.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EclassGroupChat>();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list2);
            return json;
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetChatMsg(string chatno)

        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            ProcessAccess _DataEntry = new ProcessAccess();
            string userid = ObjCommon.GetUserCode();

            DataSet result = _DataEntry.GetTransInfo(comcod, "SP_CHAT_MGT", "GET_CHAT_MSG", chatno, userid, "");
            List<RealEntity.C_34_Mgt.EclassChatMSG> list2 = result.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EclassChatMSG>();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list2);
            return json;
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]

        public static string SaveChatMsg(string chatno, string message)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            string posteduser = ObjCommon.GetUserCode();
            string terminal = ObjCommon.Terminal();
            string session = ObjCommon.Sessionid();
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyy hh:mm:ss");
            ProcessAccess _DataEntry = new ProcessAccess();

            DataSet result = _DataEntry.GetTransInfo(comcod, "SP_CHAT_MGT", "SAVE_CHAT_MSG", chatno, message, posteduser, posteddat, terminal, session, "False");

            return "True";
        }
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]

        public static string CLoseGroupChat(string chatno)
        {
            Common ObjCommon = new Common();
            string comcod = ObjCommon.GetCompCode();
            string posteduser = ObjCommon.GetUserCode();
            string terminal = ObjCommon.Terminal();
            string session = ObjCommon.Sessionid();
            string posteddat = System.DateTime.Today.ToString("dd-MMM-yyy hh:mm:ss");
            ProcessAccess _DataEntry = new ProcessAccess();

            DataSet result = _DataEntry.GetTransInfo(comcod, "SP_CHAT_MGT", "CLOSE_GROUP_CHAT", chatno, posteduser, posteddat, terminal, session);

            return "True";
        }

        private void GetAsindege()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_REPORT_CHAT_MGT", "GETCHATASIN", "%", "", "", "", "", "", "", "", "");
            this.ddlasin.DataTextField = "hrgdesc";
            this.ddlasin.DataValueField = "hrgcod";
            this.ddlasin.DataSource = ds1.Tables[0];
            this.ddlasin.DataBind();

        }



    }
}