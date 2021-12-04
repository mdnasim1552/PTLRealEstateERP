﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using RealEntity.C_32_Mis;
using RealERPLIB;
using Label = System.Web.UI.WebControls.Label;
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjectAnalysis : System.Web.UI.Page
    {
        ProcessAccess _access = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Analysis";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectGroup();
            }
        }

        private string GetComecod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }

        protected void lbtnoK_OnClick(object sender, EventArgs e)
        {
            this.ProjectDetails();
        }


        private void GetProjectGroup()
        {
            string comcod = this.GetComecod();
            DataSet ds = _access.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "LOADPROJECTGROUP");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["gcod"] = "00000";
            dr["gdesc"] = "All Group";
            dt.Rows.Add(dr);
            DataView dv = new DataView(dt);
            dv.Sort = "gcod asc";

            this.ddlprjgroup.DataTextField = "gdesc";
            this.ddlprjgroup.DataValueField = "gcod";
            this.ddlprjgroup.DataSource = dv.ToTable();
            this.ddlprjgroup.DataBind();

        }
        private void ProjectDetails()
        {

            string comcod = this.GetComecod();
            string date = this.txtdate.Text;
            string prjgrp = this.ddlprjgroup.SelectedValue == "00000" ? "%%" : "%" + this.ddlprjgroup.SelectedValue + "%";

            DataSet ds = _access.GetTransInfo(comcod, "[SP_ENTRY_SALSMGT02]", "PROJECTANALYSIS", date, prjgrp);

            if (ds == null)
            {
                this.gvprjanalysis.DataSource = null;
                this.gvprjanalysis.DataBind();
                return;
            }

            Session["tblprojanalysis"] = ds.Tables[0];
            Session["tblbprojanalysis"] = ds.Tables[1];

            this.Data_Bind();





        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblbprojanalysis"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode='000000000000'");
            dt = dv.ToTable();

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtsaltg")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tosalval)", "")) ?
              0.00 : dt.Compute("Sum(tosalval)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtsal")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salamt)", "")) ?
              0.00 : dt.Compute("Sum(salamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtsaldue")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsaldue)", "")) ?
              0.00 : dt.Compute("Sum(tsaldue)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtsalcol")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ?
              0.00 : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtcoldue")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcoldue)", "")) ?
              0.00 : dt.Compute("Sum(tcoldue)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFtsalcoldue")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsalcoldue)", "")) ?
              0.00 : dt.Compute("Sum(tsalcoldue)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFconbgd")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbgdamt)", "")) ?
             0.00 : dt.Compute("Sum(cbgdamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFbgdt")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acbgdamt)", "")) ?
             0.00 : dt.Compute("Sum(acbgdamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFactcost")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdamt)", "")) ?
             0.00 : dt.Compute("Sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjanalysis.FooterRow.FindControl("lgvFbgdprft")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdprofit)", "")) ?
             0.00 : dt.Compute("Sum(bgdprofit)", ""))).ToString("#,##0;(#,##0); ");

           

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprojanalysis"];
            this.gvprjanalysis.DataSource = dt;
            this.gvprjanalysis.DataBind();
            Session["Report1"] = gvprjanalysis;
            ((HyperLink)this.gvprjanalysis.FooterRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            this.FooterCalculation();
            this.ShowGraph();

        }

        private void ShowGraph()
        {
            DataTable dt = (DataTable)Session["tblprojanalysis"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode='000000000000'");
            dt = dv.ToTable();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.PrjAnalysis>();
            double tosalval = list.Select(s => s.tosalval).Sum();
            double salamt = list.Select(s => s.salamt).Sum();
            double tsaldue = list.Select(s => s.tsaldue).Sum();
            double collamt = list.Select(s => s.collamt).Sum();
            double tcoldue = list.Select(s => s.tcoldue).Sum();
            double tsalcoldue = list.Select(s => s.tsalcoldue).Sum();
            double core = 10000000;


            List<RealEntity.C_32_Mis.EClassAcc_03.PrjAnalysis1> list1 = new List<RealEntity.C_32_Mis.EClassAcc_03.PrjAnalysis1>();
            list1.Add(new EClassAcc_03.PrjAnalysis1(tosalval / core, salamt / core, tsaldue / core, collamt / core, tcoldue / core, tsalcoldue / core));
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(list1);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "PrjAnalysisGraph('" + json + "')", true);
        }

        protected void lnkgvprjanalysis_OnClick(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string prjgrp = ((DataTable)Session["tblprojanalysis"]).Rows[index]["prjgrp"].ToString();
            string colst = ((DataTable)Session["tblprojanalysis"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblprojanalysis"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("pactcode  like '%000'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("prjgrp='" + prjgrp + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";

            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["prjgrp"] != prjgrp)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("prjgrp='" + prjgrp + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbprojanalysis"]).Copy();

                dv = dtb.DefaultView;
                dv.RowFilter = ("prjgrp='" + prjgrp + "' and pactcode<>'000000000000'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }


            dv = dt.DefaultView;
            dv.Sort = ("prjgrp, pactcode");
            DataTable dt1 = dv.ToTable();
            Session["tblprojanalysis"] = dv.ToTable();
            this.Data_Bind();






            //DataTable dtb = ((DataTable)Session["tblbprojanalysis"]).Copy ();
            //dv = dtb.DefaultView;
            //dv.RowFilter = ("prjgrp='" + prjgrp + "' and pactcode<>'000000000000'");
            //dtb = dv.ToTable ();
            //dt.Merge (dtb);


            //dv = dt.DefaultView;
            //dv.Sort = ("prjgrp, pactcode");
            //DataTable dt1 = dv.ToTable ();
            //Session["tblprojanalysis"] =dv.ToTable ();
            //this.Data_Bind ();
        }

        protected void gvprjanalysis_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string pactcode = ((Label)e.Row.FindControl("lblpactcode")).Text.ToString();
                LinkButton acresdesc = (LinkButton)e.Row.FindControl("lnkgvprjanalysis");
                //Label lbsaltr     = (Label)e.Row.FindControl ("tsaltg");

                //Label lblsal      = (Label)e.Row.FindControl ("tsal");
                Label lbltsaldue = (Label)e.Row.FindControl("tsaldue");
                //Label lbltblco    = (Label)e.Row.FindControl ("tcol");
                Label lblcoldue = (Label)e.Row.FindControl("tcoldues");
                Label lblsalcold = (Label)e.Row.FindControl("tsalcoldues");
                Label lblsaper = (Label)e.Row.FindControl("salperstg");
                Label lblcopersal = (Label)e.Row.FindControl("colpersal");
                Label lblcopersalt = (Label)e.Row.FindControl("scolperstg");
                // Label  lblconsbgd   = (Label)e.Row.FindControl ("consbgd");
                // Label lblbgdtotal  = (Label)e.Row.FindControl ("bgdtotal");
                // Label lblcost     = (Label)e.Row.FindControl ("cost");
                Label lblbgdprofit = (Label)e.Row.FindControl("bgdprofit");
                // Label lblbgdamt = (Label)e.Row.FindControl ("bgdamt");

                HyperLink hlnkbgdamt = (HyperLink)e.Row.FindControl("hlnkbgdamt");
                HyperLink hlnktsal = (HyperLink)e.Row.FindControl("hlnktsal");
                HyperLink hlnktcol = (HyperLink)e.Row.FindControl("hlnktcol");

                HyperLink hlnkconsbgd = (HyperLink)e.Row.FindControl("hlnkconsbgd");
                HyperLink hlnkbgdtotal = (HyperLink)e.Row.FindControl("hlnkbgdtotal");
                HyperLink hlnkcost = (HyperLink)e.Row.FindControl("hlnkcost");
                //string sum = this.chkSum.Checked == true ? "Summary" : "";
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "000000000000")
                {
                    acresdesc.Font.Bold = true;
                    // lbsaltr.Font.Bold = true;
                    hlnktsal.Font.Bold = true;
                    lbltsaldue.Font.Bold = true;
                    hlnktcol.Font.Bold = true;
                    lblcoldue.Font.Bold = true;
                    lblsalcold.Font.Bold = true;
                    lblsaper.Font.Bold = true;
                    lblcopersal.Font.Bold = true;
                    lblcopersalt.Font.Bold = true;
                    hlnkconsbgd.Font.Bold = true;
                    hlnkbgdtotal.Font.Bold = true;
                    hlnkcost.Font.Bold = true;
                    lblbgdprofit.Font.Bold = true;
                    hlnkbgdamt.Font.Bold = true;
                    acresdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                    // lbsaltr.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnktsal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lbltsaldue.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnktcol.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblcoldue.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblsalcold.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblsaper.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblcopersal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblcopersalt.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnkconsbgd.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnkbgdtotal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnkcost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblbgdprofit.Attributes["style"] = "font-weight:bold; color:maroon;";
                    hlnkbgdamt.Attributes["style"] = "font-weight:bold; color:maroon;";
                    acresdesc.Style.Add("text-align", "left");
                }
                if (code == "000000000000")
                {
                    acresdesc.Enabled = true;
                }

                else
                {

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string opndate = Convert.ToDateTime(hst["opndate"].ToString()).ToString("dd-MMM-yyyy");
                    string todate = this.txtdate.Text.Trim();
                    acresdesc.Enabled = false;
                    hlnkbgdamt.NavigateUrl = "~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=" + "18" + pactcode.Substring(2);
                    hlnktsal.NavigateUrl = "~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=" + "18" + pactcode.Substring(2);
                    hlnktcol.NavigateUrl = "~/F_22_Sal/RptTransactionSt.aspx?Type=TransDateWise&comcod=" + comcod + "&prjcode=" + "18" + pactcode.Substring(2) + "&Date1=" + opndate + "&Date2=" + todate;

                    hlnkconsbgd.NavigateUrl = "~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRptALL&prjcode=" + pactcode + "&sircode=";   //&prjcode="+pactcode
                    hlnkbgdtotal.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdGrWise&comcod=" + comcod + "&prjcode=" + pactcode;
                    hlnkcost.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + pactcode;

                }
            }
        }
    }
}