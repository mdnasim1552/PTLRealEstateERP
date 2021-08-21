using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using RealERPLIB;

namespace RealERPWEB
{
    public partial class GetImage : System.Web.UI.Page
    {
        //HttpContext context;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ImgID = Request.QueryString["ImgID"].ToString();
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            DataTable dt = new DataTable();
            switch (ImgID)
            {

                case "ImgUser":
                    DataTable dtuser = ((DataSet)Session["tblusrlog"]).Tables[2];
                    if (dtuser.Rows.Count == 0)
                        return;

                    byte[] i = (byte[])dtuser.Rows[0]["usrimg"];
                    Response.BinaryWrite(i);
                    break;
                case "ImgEmp":
                    dt = (DataTable)Session["tblEmpimg"];
                    if (dt.Rows.Count == 0)
                        return;
                    byte[] img = (byte[])dt.Rows[0]["empimage"];
                    Response.BinaryWrite(img);
                    break;
                case "HREmpSign":
                    dt = (DataTable)Session["tblEmpimg"];
                    if (dt.Rows.Count == 0)
                        return;
                    byte[] sign = (byte[])dt.Rows[0]["empsign"];
                    Response.BinaryWrite(sign);
                    break;
                //case "ProjImg":
                //DataTable dtImg = (DataTable)Session["ProjImg"];
                //string pcode1 = Request.QueryString["pcode"].ToString();
                //DataView dvImg = new DataView(dtImg);
                //dvImg.RowFilter = "pcode='" + pcode1 + "'";
                //DataTable dtModified = dvImg.ToTable();
                //byte[] prjimg = (byte[])dtModified.Rows[0]["prjimg"];
                //Response.BinaryWrite(prjimg);
                //break;

                case "HRIndEmp":
                    DataTable dtallemp = (DataTable)Session["tblEmpstatus"];
                    //string id = Request.QueryString["id"].ToString();
                    DataView dvallEmp = new DataView(dtallemp);
                    string empid = Request.QueryString["empid"].ToString();
                    dvallEmp.RowFilter = "empid='" + empid + "'";
                    DataTable dtEmpModified1 = dvallEmp.ToTable();
                    if (dtEmpModified1 == null)
                        return;
                    if (dtEmpModified1.Rows.Count == 0)
                        return;

                    if (!(dtEmpModified1.Rows[0]["EMPIMAGE"] is DBNull))
                    {
                        byte[] empimgall = (byte[])dtEmpModified1.Rows[0]["EMPIMAGE"];
                        Response.BinaryWrite(empimgall);
                    }


                    break;



                default:
                    DataTable dtempimg = (DataTable)Session["tbAllDoc"];
                    //string id = Request.QueryString["id"].ToString();
                    DataView dvEmpImg = new DataView(dtempimg);
                    dvEmpImg.RowFilter = "id='" + ImgID + "'";
                    DataTable dtEmpModified = dvEmpImg.ToTable();
                    if (dtEmpModified == null)
                        return;
                    if (dtEmpModified.Rows.Count == 0)
                        return;
                    byte[] empimg1 = (byte[])dtEmpModified.Rows[0]["data"];
                    Response.BinaryWrite(empimg1);
                    break;




            }
        }
    }
}
