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
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{
    public partial class LinkClientDetail : System.Web.UI.Page
    {
        UserManMkt objuserman = new UserManMkt();
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Client Datails Information";
                this.lblclintnamedesc.Text = this.Request.QueryString["clientdesc"].ToString();
                this.GetProfession();
                this.Createtable();
                this.LoadGrid();



            }
        }

        private void Createtable()
        {
            DataTable tbltemp = new DataTable();
            //tbltemp.Columns.Add("comitemid", Type.GetType("System.String"));

            tbltemp.Columns.Add("comcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("proscod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gcod", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatat", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatad", Type.GetType("System.String"));
            tbltemp.Columns.Add("gdatan", Type.GetType("System.String"));



            ViewState["tbltemp"] = tbltemp;

        }




        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }








        private void LoadGrid()
        {


            string comcod = this.GetComdCode();
            string proscod = this.Request.QueryString["clientcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "CLIENTINFO", proscod, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();

            // ViewState["tblsameDate"] = ds1.Tables[1];


            ds1.Dispose();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void saveValueClient()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComdCode();
            string proscod = this.Request.QueryString["clientcode"].ToString();

            DataTable dt = (DataTable)ViewState["tbltemp"];
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DropDownList ddlprofession = this.gvPersonalInfo.Rows[i].FindControl("ddlprofession") as DropDownList;
                TextBox Gvalue = this.gvPersonalInfo.Rows[i].FindControl("txtgvVal") as TextBox;
                Label Gcode = this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode") as Label;
                Label gtype = this.gvPersonalInfo.Rows[i].FindControl("lgvgval") as Label;
                TextBox Gvalued = this.gvPersonalInfo.Rows[i].FindControl("txtgvCal") as TextBox;
                TextBox GvalueMob = this.gvPersonalInfo.Rows[i].FindControl("txtgvValMob") as TextBox;

                string txtData = "";
                string lblspcode = Gcode.Text;
                string dataType = gtype.Text;




                if (dataType == "N")
                {
                    txtData = ASTUtility.StrPosOrNagative(Gvalue.Text.Trim()).ToString();
                }
                if (dataType == "D")
                {
                    txtData = (Gvalued.Text.Length == 0) ? "01-Jan-1900" : Convert.ToDateTime(Gvalued.Text).ToString("dd-MMM-yyyy");

                    //if (Gcode.Text == "810100103009")
                    //{
                    //    if (txtData == "01-Jan-1900")
                    //    {
                    //        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    //        return;

                    //    }


                    //}
                }
                else
                {

                    if (ASTUtility.Right(Gcode.Text, 3) == "007")
                    {
                        txtData = GvalueMob.Text.Trim().ToString();
                    }
                    else if (ASTUtility.Right(Gcode.Text, 3) == "013")
                    {
                        txtData = ddlprofession.SelectedValue.ToString();
                    }
                    else
                    {
                        txtData = Gvalue.Text.Trim().ToString();
                    }

                }




                DataRow dr1 = dt.NewRow();


                dr1["comcod"] = comcod;
                dr1["proscod"] = proscod;
                dr1["gcod"] = lblspcode;
                dr1["gdatat"] = (dataType == "T" ? txtData : "");
                dr1["gdatan"] = (dataType == "N" ? txtData : "0.00");
                dr1["gdatad"] = (dataType == "D" ? txtData : "01-01-1900");






                dt.Rows.Add(dr1);
            }

            ViewState["tblPersonaldata"] = dt;



        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComdCode();
            string proscod = this.Request.QueryString["clientcode"].ToString();
            this.saveValueClient();

            DataTable dtprsonl = (DataTable)ViewState["tblPersonaldata"];

            DataSet ds1 = new DataSet("ds1");
            DataView dv1 = new DataView(dtprsonl);
            ds1.Tables.Add(dv1.ToTable());
            ds1.Tables[0].TableName = "tbl1";

            bool CResult = MktData.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_XML_INFO_KPI", "SHOWCLIENTINFO", ds1, null, null, "", "", "", "", "", "");

            if (CResult == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //string comcod = this.GetComdCode();
            //string proscod = this.Request.QueryString["clientcode"].ToString();



            //DataTable dt = (DataTable)ViewState["tblsameDate"];

            //for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //    string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
            //    string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

            //    if (Gcode=="810100103007")
            //    {
            //        //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //        //Gvalue = dt.Rows[0]["phone"].ToString();
            //        //((Label)this.Master.FindControl("lblmsg")).Text = "Phone Number Already Exists";
            //        //return;
            //    }

            //    if (Gcode == "810100103015")
            //    {
            //        //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //        //Gvalue = dt.Rows[0]["email"].ToString();
            //        //((Label)this.Master.FindControl("lblmsg")).Text = "Email Number Already Exists";
            //        //return;
            //    }


            //    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
            //    bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "INSERTORUPDATECUSTINF", proscod, "", Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");
            //    if (!result)
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
            //        return;
            //    }
            //}
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Client Info";
            //    string eventdesc = "Update Info";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


        }



        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = (List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper>)
            ViewState["tblProfess"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                TextBox txtgv = (TextBox)e.Row.FindControl("txtgvVal");
                string txtgvname = ((TextBox)e.Row.FindControl("txtgvVal")).Text.ToString();

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01001")
                {

                    txtgv.ReadOnly = true;

                }

                if ((code == "810100103005") || (code == "810100103006"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
                }
                if (code == "810100103007")
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvValMob")).Visible = true;

                }

                if (code == "810100103013")
                {
                    //if (txtgvname != "")
                    //{
                    //    ((DropDownList)e.Row.FindControl("ddlprofession")).SelectedValue = txtgvname;

                    //}

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).Visible = true;

                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataTextField = "gdesc";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataValueField = "gcod";
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataSource = lst;
                    ((DropDownList)e.Row.FindControl("ddlprofession")).DataBind();

                }
                if ((code == "810100103009") || (code == "810100103010"))
                {

                    ((TextBox)e.Row.FindControl("txtgvVal")).Visible = false;
                    ((TextBox)e.Row.FindControl("txtgvCal")).Visible = true;

                }


            }

        }

        private void GetProfession()
        {
            string comcod = this.GetComdCode();

            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = objuserman.GetProAndLocatio(comcod);
            var lst1 = lst.FindAll(l => l.gcod.Substring(0, 4) == "8601");

            ViewState["tblProfess"] = lst1;


        }
    }
}



