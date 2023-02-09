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
namespace RealERPWEB.F_33_Doc
{
    public partial class DocUpload : System.Web.UI.Page
    {
        ProcessAccess ImgData = new ProcessAccess("ASITDOC");
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetIDNo();
                this.GetCode();

                string comcod = this.GetCompCode();
                if (comcod == "3101")
                {
                   string gcode = this.ddlType.SelectedValue.ToString().Substring(0, 7);
      
                    switch (gcode)
                    {
                        case "8100001":
                            this.lbltitle.Text = this.ddlType.SelectedValue.ToString();
                            this.txtsName.Visible = true;

                         break;
                            DataSet ds1 = new DataSet();

                        case "8100005":
                            this.lbltitle.Text = "Department";
                            this.ddlMonth.Visible = false;
                            this.txtsName.Visible = false;


                            //string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
                            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                            string txtCompanyname = "%";


                            string txtSearchDept ="%";
                             ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
                            this.ddlDepartment.DataTextField = "actdesc";
                            this.ddlDepartment.DataValueField = "actcode";
                            this.ddlDepartment.DataSource = ds1.Tables[0];
                            this.ddlDepartment.DataBind();

                            break;

                        case "8100008" :
                            this.lbltitle.Text = "Month";
                            this.txtsName.Visible = false;
                            this.ddlMonth.Visible = true;
                            this.ddlDepartment.Visible = false;

                             ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");

                            if (ds1 == null)
                                return;
                            this.ddlMonth.DataTextField = "yearmon";
                            this.ddlMonth.DataValueField = "ymon";
                            this.ddlMonth.DataSource = ds1.Tables[0];
                            this.ddlMonth.DataBind();
                            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
                            ds1.Dispose();

                            break;
                    }
                
                 
                }


            }

            if (imgFileUpload.HasFile)
            {


                Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgFileUpload.PostedFile.SaveAs(savelocation);
                EmpImg.ImageUrl = "~/Image1/" + Upload;
                // Session["x"] = "~/Image1/" + Upload;
                image_file = imgFileUpload.PostedFile.InputStream;
                size = imgFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;
                // image_file.Close();

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";

            DataSet ds1 = new DataSet();
            switch (comcod)
            {
                case "3301":
                    txtSProject = "%81%";
                     ds1 = ImgData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETINFOCODEDET", txtSProject, "", "", "", "", "", "", "", "");
                    break;
                default:
                     ds1 = ImgData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETINFOCODEDET", txtSProject, "", "", "", "", "", "", "", "");
                    break;
            }



            this.ddlType.DataTextField = "gdesc";
            this.ddlType.DataValueField = "gcod";
            this.ddlType.DataSource = ds1.Tables[0];
            this.ddlType.DataBind();
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private void GetIDNo()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtDate.Text.Trim();
            DataSet ds1 = ImgData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETLASTID", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxidno"].ToString();

            }

        }
        private void StartUpLoad()
        {

            //get the file name of the posted image

            string imgName = imgFileUpload.FileName;

            //sets the image path

            string imgPath = "ImageStorage/" + imgName;

            //get the size in bytes that



            int imgSize = imgFileUpload.PostedFile.ContentLength;



            //validates the posted file before saving

            if (imgFileUpload.PostedFile != null && imgFileUpload.PostedFile.FileName != "")
            {

                // 10240 KB means 10MB, You can change the value based on your requirement

                //if (imgFileUpload.PostedFile.ContentLength > 10240)
                //{

                //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);

                //}

                //else
                //{

                //then save it to the Folder

                imgFileUpload.SaveAs(Server.MapPath(imgPath));

                this.EmpImg.ImageUrl = "~/" + imgPath;

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);

                //  }


            }
        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetCompCode();
                string savelocation = Server.MapPath("~") + "\\Image1";
                string[] filePaths = Directory.GetFiles(savelocation);

                //foreach (string filePath in filePaths)

                //{
                int lensavelocation = (savelocation.Length) + 1;
                string filename = filePaths[0].Substring(lensavelocation);
                File.Delete(filePaths[0]);
                //}

                byte[] photo = new byte[0];
                //byte[] signature = new byte[0];



                image_file = (Stream)Session["i"];
                size = Convert.ToInt32(Session["s"]);
                BinaryReader br = new BinaryReader(image_file);
                photo = br.ReadBytes(size);

                this.GetIDNo();
                string strname = this.txtsName.Text;
                string details = this.txtDetails1.Text;
                string imgdat = this.txtDate.Text;

                string imgid = this.lblCurMSRNo1.Text;
                bool updatPhoto;
                string code = this.ddlType.SelectedValue.ToString();
                //DataSet ds3 = ImgData.GetTransInfo(comcod, "SP_ENTRY_DOC", "IMGID", imgid, "", "", "", "", "", "", "", "");
                //
                //if (ds3.Tables[0].Rows.Count == 0)
                //{
                //updatPhoto = ImgData.InsertUserDoc(comcod, imgid, filename, photo);
                switch (comcod)
                {
                    case "3101":

                        string gcode = this.ddlType.SelectedValue.ToString().Substring(0, 7);
                        switch (gcode)
                        {
                            case "8100001":
                                strname = this.txtsName.Text;

                                break;

                            case "8100005":
                                strname = this.ddlDepartment.SelectedValue.ToString();
                                break;
                            case "8100008":
                                strname = this.ddlMonth.SelectedValue.ToString();


                                break;
                        }
                        updatPhoto = ImgData.UpdateTransInfoDoc(comcod, "SP_ENTRY_DOC", "UPDATEIMG", "docinfb", imgid, strname, details, imgdat, code, "", "", "", null, "", "", "", "", "");
                        updatPhoto = ImgData.UpdateXmlTransInfo(comCode: comcod, SQLprocName: "SP_ENTRY_DOC", CallType: "UPDATEIMG",
                            parmbyte: photo, mDesc1: "docinfa", mDesc2: imgid, mDesc3: filename);
                        break;

                    default:
                        updatPhoto = ImgData.UpdateTransInfoDoc(comcod, "SP_ENTRY_DOC", "UPDATEIMG", "docinfb", imgid, strname, details, imgdat, code, "", "", "", null, "", "", "", "", "");
                        updatPhoto = ImgData.UpdateXmlTransInfo(comCode: comcod, SQLprocName: "SP_ENTRY_DOC", CallType: "UPDATEIMG",
                            parmbyte: photo, mDesc1: "docinfa", mDesc2: imgid, mDesc3: filename);
                        break;
                }

                if (updatPhoto)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                }


                this.txtDetails1.Text = "";
  
                
                //this.lblmsg.Text = "";









            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
            }

        }
        /*
        public ASITFunParams.ProcessAccessParams SetParamForHCDocInfoUpdate 
         ( string CompCode, string _docid1a = "XXXXXXXXXXXXXXX", string _docnam1a = "NONAME", string _docext1a = ".TXT",
              string _docsize1a = "0KB", string _docdes1a = "", string _docsorc1a = "", string _docrmrk1a = "", string _docref1a = "", byte[] _docbin1a = null )
        {
            // comcod, docid, docref, docinf, docbin, rowtime
            DataSet ds1 = new DataSet ("dsdoc");
            DataTable tbl1b = new DataTable ("tbldoc");
            tbl1b.Columns.Add ("comcod", typeof (String));
            tbl1b.Columns.Add ("docid", typeof (String));
            tbl1b.Columns.Add ("docnam", typeof (String));
            tbl1b.Columns.Add ("docext", typeof (String));
            tbl1b.Columns.Add ("docsize", typeof (String));
            tbl1b.Columns.Add ("docdes", typeof (String));
            tbl1b.Columns.Add ("docsorc", typeof (String));
            tbl1b.Columns.Add ("docrmrk", typeof (String));
            tbl1b.Columns.Add ("docref", typeof (String));
            tbl1b.Columns.Add ("docbin", typeof (byte[]));
            tbl1b.Columns["docbin"].AllowDBNull = true;

            DataRow dr1b = tbl1b.NewRow ();
            dr1b["comcod"] = CompCode;
            dr1b["docid"] = _docid1a;
            dr1b["docnam"] = _docnam1a;
            dr1b["docext"] = _docext1a;
            dr1b["docsize"] = _docsize1a;
            dr1b["docdes"] = _docdes1a;
            dr1b["docsorc"] = _docsorc1a;
            dr1b["docrmrk"] = _docrmrk1a;
            dr1b["docref"] = _docref1a;
            dr1b["docbin"] = _docbin1a;
            tbl1b.Rows.Add (dr1b);
            ds1.Tables.Add (tbl1b);

            var pap1 = new ASITFunParams.ProcessAccessParams ();
            pap1.comCod = CompCode;
            pap1.ProcName = "dbo_hcm.SP_ENTRY_HCM_TRANS_01";
            pap1.ProcID = "UPDATE_HCDOCINFO01";
            pap1.parmXml01 = ds1;
            pap1.parm01 = _docid1a;
            return pap1;
        }
          */

        /*
         --****************************************************************
    UPDATE_ALL_TYPES_HUMAN_CAPITAL_DOCUMANT_INFO_01:
    begin
        begin try
        if len(@Desc01) < 15
        begin
            set @Desc20 = char(13) + 'Could not update information. Please input information in proper way.';
            raiserror(@Desc20, 16, 1);
            return;
        end;

        select ref.value('comcod[1]', 'nchar(4)') as comcod,
            ref.value('docid[1]', 'nchar(18)') as docid,
            ref.value('docnam[1]', 'nvarchar(120)') as docnam,
            ref.value('docext[1]', 'nvarchar(20)') as docext,
            ref.value('docsize[1]', 'nvarchar(50)') as docsize,  
            ref.value('docdes[1]', 'nvarchar(1000)') as docdes,
            ref.value('docsorc[1]', 'nvarchar(250)') as docsorc,
            ref.value('docrmrk[1]', 'nvarchar(250)') as docrmrk,  
            ref.value('docref[1]', 'nvarchar(250)') as docref,  
            ref.value('docbin[1]', 'varbinary(MAX)') as docbin  
            into #tbldoc01 from @Dxml01.nodes('/dsdoc/tbldoc') xmlData(ref);

        declare @docinf1 xml, @docref1 nvarchar(250), @docbin1 varbinary(MAX) ;
         * 
        set @docinf1 = (select (select nam = docnam, ext = docext, siz = docsize, dsc = docdes, src = docsorc, rmk = docrmrk from #tbldoc01 
          where comcod = @ComCod and docid = @Desc01 for xml path('tbli'), type) for xml path('dsi'));

        select top 1 @docref1 = docref, @docbin1 = docbin from #tbldoc01 where comcod = @ComCod and docid = @Desc01;

        begin transaction t2hpd;   
            -- select comcod, docid, docref, docinf, docbin, rowid, rowtime from  vw_docginf
            if len(@Desc01) = 18
            begin
                if isnull((select count(*) from dbo.vw_docginf where comcod = @PComCod and docid = @Desc01), 0) > 0
                    update dbo.vw_docginf set docref = @docref1, docinf = @docinf1, docbin = @docbin1 where comcod = @PComCod and docid = @Desc01;
            end
            else
            if len(@Desc01) < 18
            begin
                set @Desc11 = isnull((select max(right(docid, 3)) from dbo.vw_docginf where comcod = @PComCod and left(docid, 15) = left(@Desc01, 15)), '000');
                set @Desc01 = left(@Desc01, 15) + format(convert(int, @Desc11) + 1, '000');
                insert into dbo.vw_docginf (comcod, docid, docref, docinf, docbin, rowtime)
                    select @PComCod, @Desc01, @docref1, @docinf1, @docbin1, getdate();
            end;
            select comcod, docid, docref from dbo.vw_docginf where comcod = @PComCod and docid = @Desc01;
        commit transaction t2hpd;   
        end try
        begin catch
            execute dbo.sp_common_utility_info 'XXX', 'GETERRORINFO01', @Desc01=@ProcID;

            if (xact_state()) = -1	-- 'The transaction is in an uncommittable state. Rolling back transaction.'
                rollback transaction t2hpd;

            if (xact_state()) = 1	-- 'The transaction is committable. Committing transaction.'         
                commit transaction t2hpd;   
        end catch; 
        return;
    end; ----UPDATE_ALL_TYPES_HUMAN_CAPITAL_DOCUMANT_INFO_01
    --****************************************************************
         */
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void imgbtnFindType_Click(object sender, ImageClickEventArgs e)
        {
            this.GetCode();
        }
    }
}