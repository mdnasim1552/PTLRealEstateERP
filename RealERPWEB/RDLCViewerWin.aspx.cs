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
namespace RealERPWEB
{
    public partial class RDLCViewerWin : System.Web.UI.Page
    {
        ReportDocument rpt1 = new ReportDocument();
        private LocalReport rt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PrintOpt"] == null)
                return;

            string PrtOpt = Request.QueryString["PrintOpt"].ToString();
            switch (PrtOpt)
            {

                //case "HTML":
                //    this.RptHtml();
                //    break;
                case "PDF":
                    this.RptRDLCPDF();
                    break;
                case "WORD":
                    this.RptMSWord();
                    break;
                case "EXCEL":
                    this.RptMSExcel();
                    break;

                    //case "GRIDTOEXCEL":
                    //    this.ExportGridToExcel();
                    //    break;
            }
        }
        protected void LoadReportSceleton()
        {
            rt = new LocalReport();
            rt = (LocalReport)Session["Report1"];
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            if (rpt1 != null)
            {
                rpt1.Close();
                rpt1.Dispose();
                GC.Collect();
            }
        }


        protected void RptRDLCPDF()
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

           // Response.ContentEncoding = System.Text.Encoding.UTF8;  //Uni Code
          //  Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());//Uni Code
            Response.Buffer = true;
            Response.ContentType = "Application/pdf";
            Response.BinaryWrite(bytes);
        }
        protected void RptHtml()
        {
            rpt1 = (ReportDocument)Session["Report1"];
            MemoryStream oStream;
            oStream = (MemoryStream)rpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.HTML40);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/html";
            Response.BinaryWrite(oStream.ToArray());
            Response.End();


        }

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

        protected void RptMSExcel()
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
                //this.form1.Controls.Remove(this.CRViewer1);
                GridView GridView1 = (GridView)Session["Report1"];
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
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




    }
}
