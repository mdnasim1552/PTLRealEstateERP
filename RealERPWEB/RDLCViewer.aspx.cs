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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using System.Drawing;

namespace RealERPWEB
{
    public partial class RDLCViewer : System.Web.UI.Page
    {
       // ReportDocument rpt1 = new ReportDocument();
        private LocalReport rt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PrintOpt"] == null)
                return;

            string PrtOpt = Request.QueryString["PrintOpt"].ToString();
            string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string rptTitle = this.Request.QueryString["rptTitle"] ?? "Export_Data_"+ date1;
            if (string.IsNullOrEmpty(Page.Title))
            {
                Page.Title = rptTitle;
            }
            else
            {
                Page.Title = rptTitle;

            }

            switch (PrtOpt)
            {

                //case "HTML":
                //    this.RptHtml();
                //    break;
                case "PDF":
                    this.RptRDLCPDF(rptTitle);
                    break;
                case "WORD":
                    this.RptMSWord();
                    break;
                case "EXCEL":
                    this.RptMSExcel(rptTitle);
                    break;

                case "GRIDTOEXCEL":
                    this.ExportGridToExcel();
                   
                    break;
                case "GRIDTOEXCELNEW":
                   
                    this.ExportGridToExcel2();
                    break;
            }
        }
        protected void LoadReportSceleton()
        {
            rt = new LocalReport();
            rt = (LocalReport)Session["Report1"];
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //if (rpt1 != null)
            //{
            //    rpt1.Close();
            //    rpt1.Dispose();
            //    GC.Collect();
            //}
        }


        protected void RptRDLCPDF(string rptTitle)
        {
            LoadReportSceleton();
            string reportType = "PDF";
            string deviceInfo =

                   "<DeviceInfo><EmbedFonts>None</EmbedFonts>"+ 
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "filename=" + rptTitle + "." + filenameExtension);

            Response.BinaryWrite(bytes);
        }
        //protected void RptHtml()
        //{
        //    rpt1 = (ReportDocument)Session["Report1"];
        //    MemoryStream oStream;
        //    oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.HTML40);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "text/html";
        //    Response.BinaryWrite(oStream.ToArray());
        //    Response.End();


        //}

        protected void RptMSWord()
        {
            LoadReportSceleton();
            string reportType = "WORD";
            string deviceInfo =

                   "<DeviceInfo>" +
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/msword";
            Response.BinaryWrite(bytes);

            //rpt1 = (ReportDocument)Session["Report1"];
            //MemoryStream oStream;
            //oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/msword"; //application/msword
            //Response.BinaryWrite(oStream.ToArray());
            //Response.End();


        }

        protected void RptMSExcel(string rptTitle)
        {
            LoadReportSceleton();           

            string reportType = "Excel";
            string deviceInfo =
                   "<DeviceInfo>" +
                   "  <OutputFormat>" + reportType + "</OutputFormat>" +
                   "</DeviceInfo>";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension = string.Empty;
            byte[] bytes = rt.Render(reportType, deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=" + rptTitle + "." + filenameExtension);
            Response.BinaryWrite(bytes);

           

            //FileStream fs = new FileStream("d:\\report1.xls", FileMode.Create);
            ////create Excel file
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();   
        }
        //protected void RptMSExcel() 
        //{

        //    rpt1 = (ReportDocument)Session["Report1"];
        //    MemoryStream oStream;
        //    oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/vnd.ms-excel"; //application/msword
        //    Response.BinaryWrite(oStream.ToArray());
        //    Response.End();




        //}

        protected void ExportGridToExcel()
        {
            try
            {
                string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string fileName = "ExportTable_" + date1; 
                //this.form1.Controls.Remove(this.CRViewer1);
                GridView GridView1 = (GridView)Session["Report1"];
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename="+ fileName + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                this.form1.Controls.Add(GridView1);
                GridView1.RenderControl(hw);


                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception Ex)
            {
                //Label1.Text = Ex.Message;
                return;
            }
        }

        protected void ExportGridToExcel2()
        {
            string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string fileName = "ExportTable_" + date1;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="+ fileName + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView GridView1 = (GridView)Session["Report1"];

                //To Export all pages
                GridView1.AllowPaging = false;
                

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                        List<Control> controls = new List<Control>();

                        //Add controls to be removed to Generic List
                        foreach (Control control in cell.Controls)
                        {
                            controls.Add(control);
                        }

                        //Loop through the controls to be removed and replace then with Literal
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                                case "Label":
                                    cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                    break;
                                case "DropDownList":
                                    cell.Controls.Add(new Literal { Text = (control as DropDownList).SelectedItem.Text.ToString() });
                                    break;


                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void ExportRepeaterToExcel(Repeater name) 
        {
            try
            {
                string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string fileName = "ExportTable_" + date1;
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater rp = name;
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                rp.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception Ex)
            {
                //Label1.Text = Ex.Message;
                return;
            }
        }

    }
}
