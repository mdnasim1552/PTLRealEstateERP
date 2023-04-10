﻿using RealERPLIB;
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
using static RealEntity.C_38_AI.AIallPrint;
using static RealEntity.C_38_AI.AIallPrint.RptOngoingProject;

namespace RealERPWEB.F_38_AI
{
    public partial class JobAnalytics : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Job Analytics";
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.ProjectCount();
                this.GetCounting();
            }
        }

        private void ProjectCount()
        {
            try
            {
                string comcod = this.GetComdCode();
                string pid = Request.QueryString["PID"].ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI ", "GETPROJECTDASHBOARD", pid, "", "", "", "", "");
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                    return;


                //dashboard counting (rakib)
                string doninstnace = ds1.Tables[0].Rows[0]["doninstnace"].ToString() ?? "";
                string attinstance = ds1.Tables[0].Rows[0]["attinstance"].ToString() ?? "";
                string qaspent = ds1.Tables[0].Rows[0]["qaspent"].ToString() ?? "";
                string annotspent = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                string adminspnt = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                string ttlskip = ds1.Tables[0].Rows[0]["annotspent"].ToString() ?? "";
                this.doninstnace.InnerText = doninstnace;
                this.attinstance.InnerText = attinstance;
                this.qaspent.InnerText = qaspent;
                this.annotspent.InnerText = annotspent;
                this.adminspnt.InnerText = adminspnt;
                this.ttlskip.InnerText = ttlskip;



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        public string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //return (hst["comcod"].ToString());
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            string comcod = qcomcod.Length > 0 ? qcomcod : hst["comcod"].ToString();
            return (comcod);
        }

        private void GetCounting()
        {
            try
            {
                string comcod = this.GetComdCode();
                string pid = Request.QueryString["PID"].ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI ", "GETANALYTICSYSTEM", pid, "", "", "", "", "");
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                    return;


                double batch = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["batch"].ToString());
                double task = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["task"].ToString());
                double assignqa1 = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["assignqa1"].ToString());
                double assignqa2 = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["assignqa2"].ToString());
                double assignqa3 = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["assignqa3"].ToString());
                double complete = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["complete"].ToString());
                this.lbltotalbatch.Text = batch.ToString("#,##0;(#,##0); ");
                this.lbltotalqa1.Text = assignqa1.ToString("#,##0;(#,##0); ");
                this.lbltotalqa2.Text = assignqa2.ToString("#,##0;(#,##0); ");
                this.lbltotalqa3.Text = assignqa3.ToString("#,##0;(#,##0); ");
                this.lbltotaltask.Text = task.ToString("#,##0;(#,##0); ");
                this.lblcomplete.Text = complete.ToString("#,##0;(#,##0); ");

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetAllData(string comcodi, string projcode)
        {
            try
            {
                ProcessAccess purData = new ProcessAccess();
                string comcod = comcodi;

                
                DataSet ds = purData.GetTransInfo(comcod, "SP_INTERFACE_AI", "GETANALYTICSYSTEM", projcode, "", "", "", "", "", "", "", "");

                if (ds == null) { return ""; };
                var lst = ds.Tables[0].DataTableToList<GrphicalShow>();
                var datalist = new MyAllData(lst);
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(datalist);
                return json;
            }catch(Exception exp)
            {
                return "";
            }
        }
        public class MyAllData
        {
            public List<GrphicalShow> GrphicalShow { get; set; }
            


            public MyAllData()
            {

            }
            public MyAllData(List<GrphicalShow> GrphicalShow)
            {
                this.GrphicalShow = GrphicalShow;
                


            }
        }
    }
}