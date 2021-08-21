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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{

    public partial class AllEmpList : System.Web.UI.Page
    {
        //page create nahid 20160927
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                Session.Remove("tblEmpstatus");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");


                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.cmplist.Visible = true;
                    this.Company();
                    this.ddlComName_OnSelectedIndexChanged(null, null);
                }
                else
                {
                    this.GetCompany();
                    this.GetEmpList();
                    this.LoadSection();
                }



                ((Label)this.Master.FindControl("lblTitle")).Text = "All Employe Details";


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }
        private void GetCompany()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //string comcod = hst["comcod"].ToString();

            //string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            //this.ddlCompany.DataTextField = "actdesc";
            //this.ddlCompany.DataValueField = "actcode";
            //this.ddlCompany.DataSource = ds1.Tables[0];
            //this.ddlCompany.DataBind();
            //ds1.Dispose();
            //this.ddlCompany_SelectedIndexChanged(null, null);


        }

        private void LoadSection()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "EMPSECTION", "", "", "", "", "", "", "", "", "");
            this.ddlsection.DataTextField = "sirdesc";
            this.ddlsection.DataValueField = "sircode";
            this.ddlsection.DataSource = ds1.Tables[0];
            ViewState["sectiondata"] = ds1.Tables[0];
            this.ddlsection.DataBind();

        }
        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }
        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            //string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            //string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTWITHPIC", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {

                return;
            }

            Session["tblEmpstatus"] = (ds4.Tables[0]);
            this.hlnktoEmployee.Text = (ds4.Tables[0].Rows.Count == 0) ? "" : "Total Employee:" + "<span style=color:red>" + (ds4.Tables[0].Rows.Count).ToString("#,##0;(#,##); ") + "</span>";





            DataTable dt2 = ds4.Tables[1];
            if (dt2.Rows.Count == 0)
                return;

            TextBox[] txtarray = { txtdpt1, txtdpt2, txtdpt3, txtdpt4, txtdpt5, txtdpt6, txtdpt7, txtdpt8, txtdpt9, txtdpt10 };

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                txtarray[i].Text = Convert.ToDouble((Convert.IsDBNull(dt2.Rows[i]["nosec"]) ? 0 : (dt2.Rows[i]["nosec"]))).ToString();
            }


            //Section Name

            TextBox[] txtarray2 = { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9, TextBox10 };

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                txtarray2[i].Text = ((Convert.IsDBNull(dt2.Rows[i]["section"]) ? 0 : (dt2.Rows[i]["section"]))).ToString();
            }

            double nosec = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(nosec)", "")) ? 0.00 : dt2.Compute("sum(nosec)", "")));


            this.txtTtlStaff.Text = nosec.ToString("#,##0;(#,##0.00);");

            ds4.Dispose();






        }

        //protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{

        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
        //        HtmlGenericControl divControl = e.Item.FindControl("EmpAll") as HtmlGenericControl;
        //        DataRowView dataRow = ((DataRowView)listViewDataItem.DataItem);
        //        divControl.Attributes.Add("ID", dataRow["idcardno"].ToString());
        //    }

        //}

        protected void ddlComName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string secid = this.ddlsection.SelectedValue == "000000000000" ? "%" : this.ddlsection.SelectedValue;
            ShowData(secid);



        }


        private void ShowData(string Data)
        {

            try
            {
                DataTable dt = (DataTable)Session["tblEmpstatus"];
                DataTable dtsec = (DataTable)ViewState["sectiondata"];
                // DataTable dtnew = new DataTable();
                // dtnew = dt.Clone();
                string innHTML = "";
                int num = (Data == "%") ? dtsec.Rows.Count : 2;
                for (int i = 1; i < num; i++)
                {
                    DataView dv = dt.DefaultView;
                    if (Data == "%")
                    {
                        dv.RowFilter = "secid like '" + dtsec.Rows[i]["sircode"].ToString() + "'";
                    }
                    else
                    {
                        dv.RowFilter = "secid like '" + Data + "'";
                    }
                    DataTable dt1 = dv.ToTable();

                    int j = 0;
                    string topview = "";
                    foreach (DataRow dr in dt1.Rows)
                    {
                        //  dtnew.Rows.Add(dr.ItemArray);



                        string url = "";

                        if (dr["empimage"] != null && dr["empimage"].ToString() != "")
                        {

                            byte[] biempimg = (byte[])dr["empimage"];
                            url = "data:image;base64," + Convert.ToBase64String(biempimg);
                        }
                        else
                        {
                            url = "Content/Theme/images/avatars/human_avatar.png";
                        }



                        if (j != 0)
                        {
                            topview = "<h4><p style='height: 30px;'></p></h4>";
                        }


                        else
                        {
                            topview = "<h4><p style='height: 30px; font-align:center; background-color: #D9E9D3; margin: 0 auto;'>" + dr["section"] + "</p></h4>";
                        }
                        j++;
                        //"<image ID = 'ImgID' runat='server' ImageUrl='~/GetImage.aspx?ImgID=HRIndEmp&empid=" +
                        //                    dr["empid"] + "'" + " Height='70' Width='80' CssClass='img-thumbnail img-responsive' />" +
                        innHTML += @"<div class='col-xs-12 col-sm-6 col-md-3' style='padding: 0 5px; '>" +
                                   "<div id = 'EmpAll' runat = 'server'>" +
                                   topview +
                                        "<div class='well well-sm' style='height: 120px; margin-bottom: 2px;'>" +

                                            "<div class='row'>" +
                                                "<div class='col-sm-6 col-md-4'>" +
                                                    "<a href ='../F_82_App/RptMyInterface.aspx?Type=Report&empid=" + dr["empid"] + "' target='_blank'>" +
                                                       "<img src='" + url + "' Height='70' Width='80' CssClass='img-thumbnail img-responsive' />" +

                                                   " </a></div>" +

                                                "<div class='col-sm-6 col-md-8 pading5px'>" +
                                                    "<h4>" + dr["empname"] + "</h4>" +
                                                    "<p>" + dr["desig"] + "</p>" +
                                                     "<p>" + dr["section"] + "</p>" +
                                                    "<p><i class='glyphicon glyphicon-calendar'></i>" +
                                                         dr["joindate"] + "</p>" +
                                                            "</div></div></div></div></div>";



                    }
                    int sizeA = dt1.Rows.Count % 4;
                    if (sizeA == 0)
                    {

                    }
                    else
                    {
                        for (int k = 0; k < 4 - sizeA; k++)
                        {



                            innHTML += @"<div class='col-xs-12 col-sm-6 col-md-3' style='padding: 0 5px; '>" +
                                 "<div id = 'EmpAll' runat = 'server'>" +
                             "<h4><p style='color:white; height: 30px;'></p></h4>" +
                                      "<div class='well well-sm' style='height: 120px; margin-bottom: 2px;'>" +

                                          "<div class='row'>" +
                                              "<div class='col-sm-6 col-md-4'>" +



                                                 "</div>" +

                                              "<div class='col-sm-6 col-md-8 pading5px'>" +
                                                  "<h4>" + "" + "</h4>" +
                                                  "<p>" + "" + "</p>" +
                                                   "<p>" + "" + "</p>" +
                                                          "</div></div></div></div></div>";




                        }
                    }


                }

                this.datashow.InnerHtml = innHTML;
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }
        }
    }
}