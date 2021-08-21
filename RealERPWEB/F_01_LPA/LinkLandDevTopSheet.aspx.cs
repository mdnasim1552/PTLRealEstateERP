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
using System.Drawing;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_01_LPA
{
    public partial class LinkLandDevTopSheet : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblProjectName.Text = this.Request.QueryString["pactdesc"].ToString();
                this.ShowReport();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Summary Sheet of Land Proposal";
            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.Request.QueryString["pactdesc"].ToString();
            ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptFeaProject();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = prjname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rpcp.SetDataSource((DataTable)Session["tblfeaprj"]);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void ShowReport()
        {
            Session.Remove("tblfeaprj");
            string comcod = this.GetComCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString(); ;
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITY", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrjRep.DataSource = null;
                this.gvFeaPrjRep.DataBind();
                return;
            }
            Session["tblfeaprj"] = this.HiddenSameData(ds2.Tables[0]);
            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            string grp = dt1.Rows[0]["grp"].ToString();
            string subgrp = dt1.Rows[0]["subgrp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["subgrp"].ToString() == subgrp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                    dt1.Rows[j]["subgrpdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["subgrp"].ToString() == subgrp)
                    {
                        dt1.Rows[j]["subgrpdesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    grp = dt1.Rows[j]["grp"].ToString();
                    subgrp = dt1.Rows[j]["subgrp"].ToString();

                }

            }
            return dt1;

        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfeaprj"];
            this.gvFeaPrjRep.DataSource = dt;
            this.gvFeaPrjRep.DataBind();

        }

        protected void gvFeaPrjRep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lgvgroupdesc");
                Label ToSize = (Label)e.Row.FindControl("lgtsizecRep");
                Label RatepSft = (Label)e.Row.FindControl("lgsalraterep");
                Label amt = (Label)e.Row.FindControl("lgvAmtrep");
                Label per = (Label)e.Row.FindControl("lgvper");
                Label lgvgroupdesc = (Label)e.Row.FindControl("lgvgroupdesc");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 5) == "AAAAA")
                {
                    groupdesc.Font.Bold = true;
                    groupdesc.ForeColor = Color.Green;
                    ToSize.ForeColor = Color.Green;
                    RatepSft.ForeColor = Color.Green;
                    amt.ForeColor = Color.Green;
                    per.ForeColor = Color.Green;

                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    per.Font.Bold = true;

                    groupdesc.Style.Add("text-align", "right");
                }


                if (ASTUtility.Right(code, 5) == "00000")
                {

                    lgvgroupdesc.Attributes["style"] = "color:blue; font-weight:bold;";
                    ToSize.ForeColor = Color.Blue;
                    RatepSft.ForeColor = Color.Blue;
                    amt.ForeColor = Color.Blue;
                    per.ForeColor = Color.Blue;

                    ToSize.Font.Bold = true;
                    RatepSft.Font.Bold = true;
                    amt.Font.Bold = true;
                    per.Font.Bold = true;

                    e.Row.Attributes["style"] = "background-color:#CCECC3; font-weight:bold;";

                }
                else
                {

                    //
                }

            }

















        }


    }
}