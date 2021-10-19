using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Excel;
using RealERPLIB;
using Label = System.Web.UI.WebControls.Label;
namespace RealERPWEB
{

    public partial class DeafultMenu : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;

                //this.pnlHousing.Visible = (this.Request.QueryString["Type"] == "4105");
                //this.Panel1.Visible = (this.Request.QueryString["Type"] == "4106");
                //this.Panel2.Visible = (this.Request.QueryString["Type"] == "4107");
                //this.Panel3.Visible = (this.Request.QueryString["Type"] == "4108");
                //this.Panel4.Visible = (this.Request.QueryString["Type"] == "4109");
                //this.Panel5.Visible = (this.Request.QueryString["Type"] == "22");
                //this.Panel6.Visible = (this.Request.QueryString["Type"] == "17");
                //this.Panel7.Visible = (this.Request.QueryString["Type"] == "4110");
                this.pnlAdminPermission.Visible = (this.Request.QueryString["Type"] == "4112");
                this.Panel8.Visible = (this.Request.QueryString["Type"] == "5000");///////DashBoard

                // this.PanelAllins.Visible = (this.Request.QueryString["Type"] == "3333");

                this.PanelAllinsNew.Visible = (this.Request.QueryString["Type"] == "3333");





                //this.Panel9.Visible = (this.Request.QueryString["Type"] == "6000");
                //this.pnlHR.Visible = (this.Request.QueryString["Type"] == "4101");
                this.PanelHR.Visible = (this.Request.QueryString["Type"] == "7000");
                //this.PanelLand.Visible = (this.Request.QueryString["Type"] == "2101");

                //==================================all module flow menu==================================//

                //this.landpanal.Visible = (this.Request.QueryString["Type"] == "landpro");
                //this.projefis.Visible = (this.Request.QueryString["Type"] == "pfpanal");
                //this.bugetepaal.Visible = (this.Request.QueryString["Type"] == "bgpanal");
                //this.projepan.Visible = (this.Request.QueryString["Type"] == "pppanal");
                //this.construpna.Visible = (this.Request.QueryString["Type"] == "construpn");
                //this.Procurement.Visible = (this.Request.QueryString["Type"] == "Procurpanal");
                //this.Materialsst.Visible = (this.Request.QueryString["Type"] == "mspanal");
                //this.Marketingp.Visible = (this.Request.QueryString["Type"] == "Marketpanal");
                //this.salapna.Visible = (this.Request.QueryString["Type"] == "Salespanal");
                //this.criteor.Visible = (this.Request.QueryString["Type"] == "crpanal");
                //this.cusnarpan.Visible = (this.Request.QueryString["Type"] == "ccpanal");

                //this.regpals.Visible = (this.Request.QueryString["Type"] == "regpanal");

                //this.accountspn.Visible = (this.Request.QueryString["Type"] == "Accounts");

                //this.financepn.Visible = (this.Request.QueryString["Type"] == "Finance");

                //this.annybspp.Visible = (this.Request.QueryString["Type"] == "abppnal");

                //this.stepofop.Visible = (this.Request.QueryString["Type"] == "sofop");
                this.Company();
                this.PnlGrp.Visible = (this.Request.QueryString["Type"] == "9000");
                this.PnlGrpDetails.Visible = (this.Request.QueryString["Type"] == "37");
                //if (this.Request.QueryString["Type"] == "9000")
                //{
                //    this.CallCompanyList();

                //}
                this.pnlkMIS.Visible = (this.Request.QueryString["Type"] == "5004");

                if (ASTUtility.Left(this.GetCompCode(), 1) == "1")
                {
                    this.pnlflochartCon.Visible = (this.Request.QueryString["type"] == "8010");/////////Constraction
                }
                else if(ASTUtility.Left(this.GetCompCode(), 1) == "3")
                {
                    this.pnlflochart.Visible = (this.Request.QueryString["type"] == "8010");/////////Real Estate
                }
                else 
                {
                    this.pnlflochartLand.Visible = (this.Request.QueryString["type"] == "8010");/////////Real Estate
                }

                //((LinkButton)this.Master.FindControl("id")).Visible = false;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {


        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CallCompanyList(string comcod)
        {
            ProcessAccess GrpData = new ProcessAccess();
            //string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            var lst = ds1.Tables[0].DataTableToList<RealEntity.CompGroupMenu>();
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(lst);
            return json;

        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }
        public string GetCompCodeS()
        {
            return "3101";
        }
        public string GetCompCodeS1()
        {
            return "";
        }
        protected void btnSales_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        //protected void btnPur_Click(object sender, EventArgs e)
        //{
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
        //    string comcod = this.GetCompCodeS();
        //    string comsnam = this.GetCompCodeS1();
        //    if (ASTUtility.Left(comcod,1) == "2")
        //    {
        //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpRptDashBoardLandPro.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //    }
        //    else
        //    {
        //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpPurInformation.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
        //    }




        //}
        protected void btnPro_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpConstructionInfo.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnAcc_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpAccDashBoard.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";

        }
        protected void btnOver_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCodeS();
            string comsnam = this.GetCompCodeS1();

            if (ASTUtility.Left(comcod, 1) == "2")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpDashBoardAllLP.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('F_46_GrMgtInter/GrpDashBoardAll.aspx?comcod=" + comcod + "&Desc=" + comsnam + "', target='_blank');</script>";
            }



        }






        protected void lnkbtnAbp_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "05";
            ds1.Tables[0].Rows[0]["moduleid2"] = "05";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "01";
            ds1.Tables[0].Rows[0]["moduleid2"] = "01";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMatr_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "04";
            ds1.Tables[0].Rows[0]["moduleid2"] = "04";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnPlaning_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "08";
            ds1.Tables[0].Rows[0]["moduleid2"] = "08";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void btnImp_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "09";
            ds1.Tables[0].Rows[0]["moduleid2"] = "09";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnGoodsInv_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "12";
            ds1.Tables[0].Rows[0]["moduleid2"] = "12";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void btnPur_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "14";
            ds1.Tables[0].Rows[0]["moduleid2"] = "14";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnACC_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "17";
            ds1.Tables[0].Rows[0]["moduleid2"] = "17";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMKT_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "21";
            ds1.Tables[0].Rows[0]["moduleid2"] = "21";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnSale_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "22";
            ds1.Tables[0].Rows[0]["moduleid2"] = "22";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnCR_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "23";
            ds1.Tables[0].Rows[0]["moduleid2"] = "23";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnCC_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "24";
            ds1.Tables[0].Rows[0]["moduleid2"] = "24";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }
        protected void lnkbtnMIS_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "32";
            ds1.Tables[0].Rows[0]["moduleid2"] = "32";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnMM_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "35";
            ds1.Tables[0].Rows[0]["moduleid2"] = "35";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnHR_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "81";
            ds1.Tables[0].Rows[0]["moduleid2"] = "81";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnKPI_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "79";
            ds1.Tables[0].Rows[0]["moduleid2"] = "79";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        protected void lnkbtnAssets_Click(object sender, EventArgs e)
        {

            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "29";
            ds1.Tables[0].Rows[0]["moduleid2"] = "29";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }



        protected void inkbtnCW_Click(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblusrlog"];
            ds1.Tables[0].Rows[0]["moduleid"] = "13";
            ds1.Tables[0].Rows[0]["moduleid2"] = "13";
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('StepofOperation.aspx', target='_self');</script>";
            Session["tblusrlog"] = ds1;
        }

        //protected void ddlComName_OnSelectedIndexChanged ( object sender, EventArgs e )
        //{
        //    string comcod=this.ddlco
        //    ((Label)this.Master.FindControl ("lblprintstk1")).Text = @"<script>window.open('CompanyOverAllReport.aspx?comcod='"++"'', target='_self');</script>";
        //}
    }
}