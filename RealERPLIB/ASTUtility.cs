//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections;
//using System.ComponentModel;
//using System.Xml;
//using System.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;

using System.Security.Cryptography;


namespace RealERPLIB
{
    public static class ASTUtility
    {
        public static object TripleDesProvider { get; private set; }

        public static string FilterString(string SourceString)
        {
            SourceString = SourceString.Trim();
            SourceString = SourceString.Replace("'", "''");
            //SourceString = SourceString.Replace(@"""", "");
            //SourceString = SourceString.Replace("%", "");
            //SourceString = SourceString.Replace("--", "");
            //SourceString = SourceString.Replace(";", "");
            //SourceString = SourceString.Replace("(", "");
            //SourceString = SourceString.Replace(")", "");
            //SourceString = SourceString.Replace("_", "");
            return SourceString;

        }

        public static string Left(string host, int index)
        {
            return host.Substring(0, index);

        }

        public static string Right(string host, int index)
        {
            return host.Substring(host.Length - index);
        }

        public static string Replicate(string number, int repnumber)
        {
            for (int i = 2; i <= repnumber; i++)
            {
                number += number;


            }


            return number;
        }
        public static string ExprToValue(string cExpr)
        {

            if (cExpr.Length == 0)
                return "0";
            string mExpr1 = cExpr.Trim().Replace(",", "");
            mExpr1 = mExpr1.Replace("/", " div ");
            XmlDocument xmlDoc = new XmlDocument();
            XPathNavigator xPathNavigator = xmlDoc.CreateNavigator();
            mExpr1 = xPathNavigator.Evaluate(mExpr1).ToString();
            return mExpr1;
        }

        public static double StrNagative(string Stname)
        {
            double rval = Convert.ToDouble(Stname.ToString().Replace("-", "").Trim());
            double valsign = (Stname.Trim().Substring(0, 1) == "-" ? -1.00 : 1.00);
            return (rval * valsign);
        }



        public static double StrPosOrNagative(string Stname)
        {
            if (Stname.Length == 0)
                return 0.00;
            double rval = Convert.ToDouble(Stname.ToString().Replace("-", "").Trim());
            double valsign = (Stname.Trim().Substring(0, 1) == "-" ? -1.00 : 1.00);
            return (rval * valsign);
        }




        public static string Trans(double XX1, int Index)
        {
            Index = (Index == 0 ? 1 : Index);
            string[] X1 = new string[101];
            string[] Y1 = new string[6];
            string[] Z1 = new string[3];

            X1[0] = "Zero ";
            X1[1] = "One ";
            X1[2] = "Two ";
            X1[3] = "Three ";
            X1[4] = "Four ";
            X1[5] = "Five ";
            X1[6] = "Six ";
            X1[7] = "Seven ";
            X1[8] = "Eight ";
            X1[9] = "Nine ";
            X1[10] = "Ten ";
            X1[11] = "Eleven ";
            X1[12] = "Twelve ";
            X1[13] = "Thirteen ";
            X1[14] = "Fourteen ";
            X1[15] = "Fifteen ";
            X1[16] = "Sixteen ";
            X1[17] = "Seventeen ";
            X1[18] = "Eighteen ";
            X1[19] = "Nineteen ";
            X1[20] = "Twenty ";
            X1[30] = "Thirty ";
            X1[40] = "Forty ";
            X1[50] = "Fifty ";
            X1[60] = "Sixty ";
            X1[70] = "Seventy ";
            X1[80] = "Eighty ";
            X1[90] = "Ninety ";

            for (int J1 = 20; J1 <= 90; J1 = J1 + 10)
                for (int I1 = 1; I1 <= 9; I1++)
                    X1[J1 + I1] = X1[J1] + X1[I1];

            Y1[1] = "Hundred ";
            Y1[2] = "Thousand ";
            Y1[3] = (Index >= 3 ? "Million " : "Lac ");
            Y1[4] = (Index >= 3 ? "Billion " : "Crore ");
            Y1[5] = "Trillion ";
            Z1[1] = "Minnus ";
            Z1[2] = "Zero ";
            long N_1 = System.Convert.ToInt64(Math.Floor(XX1));
            string N_2 = XX1.ToString();
            while (!(N_2.Length == 0))
            {
                if (N_2.Substring(0, 1) == ".")
                    break;
                N_2 = N_2.Substring(1);
            }
            N_2 = (N_2.Length == 0 ? " " : N_2);
            switch (Index)
            {
                case 1:
                case 3:
                    N_2 = ((N_2.Substring(0, 1) == ".") ? ((string)(N_2.Substring(1) + "00000")).Substring(0, 5) : "00000");
                    break;
                case 2:
                case 4:
              
                    N_2 = ((N_2.Substring(0, 1) == ".") ? ((string)(N_2.Substring(1) + "00000")).Substring(0, 2) : "00");
                    break;
            }
            string S_GN = (Math.Sign(N_1) == -1 ? Z1[1] : "");
            string Z1_ER = (N_1 == 0 ? Z1[2] : "");
            string N_O = Right("00000000000000000" + Math.Abs(N_1).ToString(), 17);
            string[] L = new string[100];
            switch (Index)
            {
                case 1:
                case 2:
                    L[0] = "";
                    L[1] = ((Convert.ToInt32(N_O.Substring(0, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(0, 1))] + Y1[1]);
                    L[2] = ((Convert.ToInt32(N_O.Substring(1, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(1, 2))] + Y1[4]);
                    L[3] = ((Convert.ToInt32(N_O.Substring(3, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(3, 2))] + Y1[3]);
                    L[4] = ((Convert.ToInt32(N_O.Substring(5, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(5, 2))] + Y1[2]);
                    L[5] = ((Convert.ToInt32(N_O.Substring(7, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(7, 1))] + Y1[1]);
                    L[6] = ((Convert.ToInt32(N_O.Substring(8, 2)) == 0) ? ((Convert.ToInt32(N_O.Substring(0, 10))) == 0 ? "" : Y1[4]) : X1[Int32.Parse(N_O.Substring(8, 2))] + Y1[4]);
                    L[7] = ((Convert.ToInt32(N_O.Substring(10, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(10, 2))] + Y1[3]);
                    L[8] = ((Convert.ToInt32(N_O.Substring(12, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(12, 2))] + Y1[2]);
                    L[9] = ((Convert.ToInt32(N_O.Substring(14, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(14, 1))] + Y1[1]);
                    L[10] = (Convert.ToInt32(N_O.Substring(15, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(15, 2))];
                    break;

                case 3:
                case 4:
                    L[0] = ((Convert.ToInt32(N_O.Substring(0, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(0, 2))] + Y1[2]);
                    L[1] = ((Convert.ToInt32(N_O.Substring(2, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(2, 1))] + Y1[1]);
                    L[2] = ((Convert.ToInt32(N_O.Substring(3, 2)) == 0) ? ((Convert.ToInt32(N_O.Substring(2, 1)) == 0) ? "" : Y1[5]) : X1[Int32.Parse(N_O.Substring(3, 2))] + Y1[5]);
                    L[3] = ((Convert.ToInt32(N_O.Substring(5, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(5, 1))] + Y1[1]);
                    L[4] = ((Convert.ToInt32(N_O.Substring(6, 2)) == 0) ? ((Convert.ToInt32(N_O.Substring(5, 1)) == 0) ? "" : Y1[4]) : X1[Int32.Parse(N_O.Substring(6, 2))] + Y1[4]);
                    L[5] = ((Convert.ToInt32(N_O.Substring(8, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(8, 1))] + Y1[1]);
                    L[6] = ((Convert.ToInt32(N_O.Substring(9, 2)) == 0) ? ((Convert.ToInt32(N_O.Substring(8, 1)) == 0) ? "" : Y1[3]) : X1[Int32.Parse(N_O.Substring(9, 2))] + Y1[3]);
                    L[7] = ((Convert.ToInt32(N_O.Substring(11, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(11, 1))] + Y1[1]);
                    L[8] = ((Convert.ToInt32(N_O.Substring(12, 2)) == 0) ? ((Convert.ToInt32(N_O.Substring(11, 1)) == 0) ? "" : Y1[2]) : X1[Int32.Parse(N_O.Substring(12, 2))] + Y1[2]);
                    L[9] = ((Convert.ToInt32(N_O.Substring(14, 1)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(14, 1))] + Y1[1]);
                    L[10] = (Convert.ToInt32(N_O.Substring(15, 2)) == 0) ? "" : X1[Int32.Parse(N_O.Substring(15, 2))];
                    break;
            }
            string O = S_GN + Z1_ER + L[0] + L[1] + L[2] + L[3] + L[4] + L[5] + L[6] + L[7] + L[8] + L[9] + L[10];
            string[] M = new string[100];
            string Q_ = "";
            string P = "";

            switch (Index)
            {
                case 1:
                case 3:
                    M[1] = ((Convert.ToInt32(N_2) >= 1) ? X1[Int32.Parse(N_2.Substring(0, 1))] : "");
                    M[2] = ((Convert.ToInt32(N_2) >= 1 & Convert.ToInt32(N_2.Substring(1)) >= 1) ? X1[Int32.Parse(N_2.Substring(1, 1))] : "");
                    M[3] = ((Convert.ToInt32(N_2) >= 1 & Convert.ToInt32(N_2.Substring(2)) >= 1) ? X1[Int32.Parse(N_2.Substring(2, 1))] : "");
                    M[4] = ((Convert.ToInt32(N_2) >= 1 & Convert.ToInt32(N_2.Substring(3 - 1)) >= 1) ? X1[Int32.Parse(N_2.Substring(3, 1))] : "");
                    M[5] = ((Convert.ToInt32(N_2) >= 1 & Convert.ToInt32(N_2.Substring(4)) >= 1) ? X1[Convert.ToInt32(N_2.Substring(4, 1))] : "");
                    M[6] = ((Convert.ToInt32(N_2) > 0) ? "Point " : "");
                    P = M[6] + M[1] + M[2] + M[3] + M[4] + M[5];
                    //Q_ = O + P;
                    Q_ = "( Taka " + O + P + "Only )";
                    break;
                case 2:
                    M[1] = ((Convert.ToInt32(N_2) >= 1) ? X1[Int32.Parse(N_2)] : "");
                    M[6] = ((Convert.ToInt32(N_2) > 0) ? "And Paisa " : "");
                    P = M[6] + M[1];
                    Q_ = "( Taka " + O + P + "Only )";
                    break;

                case 4:
                    M[1] = ((Convert.ToInt32(N_2) >= 1) ? X1[Int32.Parse(N_2)] : "");
                    M[6] = ((Convert.ToInt32(N_2) > 0) ? "And Cent " : "");
                    P = M[6] + M[1];
                    Q_ = "( Dollar " + O + P + "Only )";
                    break;
            }
            return Q_;
        }

        //--------------------------------------------------------------------------------------------------------
        public static string DefComa(double AA) // Bangla Coma
        {
            string[] A = new string[21];
            A[1] = ((Math.Sign(AA) >= 0) ? "" : "-");
            A[2] = Math.Abs(AA).ToString("###0.00");
            A[3] = Math.Abs(AA).ToString("###0.000");
            A[3] = ((double.Parse(A[3]) - (double.Parse(A[2])))).ToString();
            A[2] = A[2] + ((Double.Parse(A[3]) >= 0.005) ? 0.01 : 0);
            A[2] = Left(A[2], A[2].Length - 1);
            A[4] = ((string)(string.Empty.PadLeft(24) + A[2])).Substring(((string)(string.Empty.PadLeft(24) + A[2])).Length - 24);
            A[5] = A[4].Substring(0, 2);
            A[6] = A[4].Substring(2, 2);
            A[7] = A[4].Substring(4, 3);
            A[8] = A[4].Substring(7, 2);
            A[9] = A[4].Substring(9, 2);
            A[10] = A[4].Substring(11, 3);
            A[11] = A[4].Substring(14, 2);
            A[12] = A[4].Substring(16, 2);
            A[13] = A[4].Substring(18, 3);
            A[14] = A[5] + "," + A[6] + "," + A[7] + "," + A[8] + "," + A[9] + "," + A[10] + "," + A[11] + "," + A[12] + "," + A[13];
            A[14] = A[14].Trim();

            while (A[14].Substring(0, 1) == ",")
            {
                A[14] = A[14].Substring(1, A[14].Length - 1);
                A[14] = A[14].Trim();
            }
            A[15] = A[14] + A[4].Substring(21, 3);
            A[16] = ((string)(string.Empty.PadLeft(24) + A[15])).Substring(((string)(string.Empty.PadLeft(24) + A[15])).Length - 24) + " ";
            A[17] = ((A[1] != "") ? "(" : "") + A[16].Trim() + ((A[1] != "") ? ")" : "");
            return A[17];
        }

        //-------------------------------------------------------------------------------------------------------       

        public static string Concat(string compname, string username, string printdate, string Session = "")
        {
            string concat = "";
            concat = concat + "Print Source:" + compname + ", " + username + ", " + Session + ", " + printdate;
            return concat;
        }

        public static string Concat1(string postrmid, string postuser, string postseson, string posteddat, string compname, string username, string printdate, string Session = "")
        {
            string concat = "";
            concat = concat + "Input Source:" + postrmid + ", " + postuser + ", " + postseson + ", " + posteddat + "/" + "Print Source:" + compname + ", " + username + ", " + Session + ", " + printdate;
            return concat;
        }

        public static string Concat2(string postrmid, string postuser, string postseson, string posteddat)
        {
            string concat = "";
            concat = concat + "Input Source:" + postrmid + ", " + postuser + ", " + postseson + ", " + posteddat;
            return concat;
        }

        public static string Cominformation()
        {

            return "Developed By: Pinovation Tech Ltd. , Phone:09611677682, 09611677683";

        }

        public static string ComInfoWithoutNumber()
        {

            return "Developed By: Pinovation Tech Ltd.";

        }

        public static bool PagePermission(string frmname, DataSet ds)
        {
            if (ds.Tables.Count == 7)
            {
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1).Replace(".aspx", "");

                DataTable dt = ds.Tables[1];
                DataRow[] dr1 = dt.Select("(frmname+qrytype1)='" + frmname + "'");
                return (dr1.Length > 0);
            }
            else
            {
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1).Replace(".aspx", "");
                DataTable dt = ds.Tables[1];
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + frmname + "'");
                return (dr1.Length > 0);
            }

        }
        public static DataRow[] PagePermission1(string frmname, DataSet ds)
        {
            if (ds.Tables.Count == 7)
            {
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1).Replace(".aspx", "");
                DataTable dt = ds.Tables[1];
                DataRow[] dr1 = dt.Select("(frmname+qrytype1)='" + frmname + "'");
                return dr1;
            }
            else
            {
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1).Replace(".aspx", "");
                DataTable dt = ds.Tables[1];
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + frmname + "'");
                return dr1;
            }

        }
        public static string ToRoman(int N)
        {
            const string Digits = "IVXLCDM";
            int I = 0;
            int Digit = 0;
            string Temp = null;
            string Temp1 = null;
            int N1 = 0;
            Temp1 = "";
            if (N >= 1000)
            {
                String s = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
                Temp1 = s.Substring(0, N1);
                N1 = N / (1000);
                N = N - N1 * 1000;
            }
            I = 1;
            Temp = "";
            while (N > 0)
            {
                Digit = N % 10;
                N = N / 10;
                switch (Digit)
                {
                    case 1:
                        Temp = Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 2:
                        Temp = Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 3:
                        Temp = Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 4:
                        Temp = Digits.Substring(I - 1, 2) + Temp;
                        break;
                    case 5:
                        Temp = Digits.Substring(I, 1) + Temp;
                        break;
                    case 6:
                        Temp = Digits.Substring(I, 1) + Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 7:
                        Temp = Digits.Substring(I, 1) + Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 8:
                        Temp = Digits.Substring(I, 1) + Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Digits.Substring(I - 1, 1) + Temp;
                        break;
                    case 9:
                        Temp = Digits.Substring(I - 1, 1) + Digits.Substring(I + 2 - 1, 1) + Temp;
                        break;
                }
                I = I + 2;
            }
            return Temp1 + Temp;


        }


        //Code Data export from  Gridview to exel
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition","attachment;filename=DataTable.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";

        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);

        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    //Apply text style to each Row
        //    GridView1.Rows[i].Attributes.Add("class", "textmode");

        //}
        //GridView1.RenderControl(hw);

        ////style to format numbers to string

        //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //Response.Write(style);

        //Response.Output.Write(sw.ToString());
        //Response.Flush();            
        //Response.End();          


        public static string EncodePassword(string originalPassword)
        {

            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        //private static string DecodePassword(string text)
        //{
        //    var hashmd5 = new MD5CryptoServiceProvider();
        //    byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);

        //    byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(text));
        //    hashmd5.Clear();

        //    TripleDesProvider.Key = keyArray;
        //    TripleDesProvider.Mode = CipherMode.ECB;
        //    TripleDesProvider.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = TripleDesProvider.CreateEncryptor();

        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}


        public static int DatediffTotalDays(DateTime dtto, DateTime dtfrm)
        {
            int tcount;
            tcount = (int)(((dtto - dtfrm).TotalDays) + 1);

            return tcount;
        }

        public static int Datediff(DateTime dtto, DateTime dtfrm)
        {

            int year, mon, day;
            year = dtto.Year - dtfrm.Year;
            mon = dtto.Month - dtfrm.Month;
            day = dtto.Day - dtfrm.Day;
            if (day < 0)
            {

                day = day + 30;
                mon = mon - 1;
                if (mon < 0)
                {
                    mon = mon + 12;
                    year = year - 1;
                }
            }

            if (mon < 0)
            {

                mon = mon + 12;
                year = year - 1;
            }

            mon = year * 12 + mon;

            return mon;








        }

        public static int Datediffday(DateTime dtto, DateTime dtfrm)
        {

            int year, mon, day, today;
            year = dtto.Year - dtfrm.Year;
            mon = dtto.Month - dtfrm.Month;
            day = dtto.Day - dtfrm.Day;
            if (day < 0)
            {

                day = day + 30;
                mon = mon - 1;
                if (mon < 0)
                {
                    mon = mon + 12;
                    year = year - 1;
                }
            }

            if (mon < 0)
            {

                mon = mon + 12;
                year = year - 1;
            }

            today = year * 365 + mon * 30 + day;
            return today;
        }

        public static string DateInVal(string date)
        {
            //string dateval = "";

            if (date.Length == 6)
                return (date.Substring(0, 2) + "." + date.Substring(2, 2) + "." + "20" + Right(date, 2));
            else if (date.Contains("."))
                return (date);
            else if (date.Length == 0)
                return "";
            else
                return (date.Substring(0, 2) + "." + date.Substring(2, 2) + "." + Right(date, 4));



        }
        public static string DateFormat(string date)
        {
            int index1 = date.IndexOf(".");
            return ((!date.Contains(".")) ? date : (date.Substring(index1 + 1, 2).Replace(".", "") + "." + date.Substring(0, index1) + "." + Right(date, 4)));
        }

        public static string DateFormat2(string date) 
        {
            int index1 = date.IndexOf("/");
            return ((!date.Contains("/")) ? date : (date.Substring(index1 + 1, 2).Replace("/", "") + "/" + date.Substring(0, index1) + "/" + Right(date, 4)));
        }

        public static string Month3digit(int digit)

        {
            digit = digit - 1;
            string[] Mon = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            return Mon[digit];
        }

        public static int RandNumber(int min, int max)
        {
            Random rnumber = new Random();
            return (rnumber.Next(min, max));
        }

        public static string MonthLastDay(int mon)
        {
            mon = mon - 1;
            string[] days = { "31", "28", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31" };
            return days[mon];
        }

        public static string CustomReqFormat(string refno)  
        {
            string _refno = refno;
            if (refno.Length == 14)
            {
                string part1 = "", part2 = "", part3 = "", part4 = "";
                part1 = refno.Substring(0, 3);
                part2 = refno.Substring(5, 2);
                part3 = refno.Substring(7, 2);
                part4 = refno.Substring(9);
                _refno = part1 + part2 + "-" + part3 + "-" + part4;
            }
            return _refno;
        }
    }

}
