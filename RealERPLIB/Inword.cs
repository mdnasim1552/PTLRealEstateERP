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
  public class Inword
    {
      public static string Right(string host, int index)
      {
          return host.Substring(host.Length - index);
      }
        public static string Trans(double XX1, int Index)
        {
            Index = (Index == 0 ? 1 : Index);
            string[] X1 = new string[101];
            string[] Y1 = new string[6];
            string[] Z1 = new string[3];

            X1[0] = "শূন্য ";
            X1[1] = "এক ";
            X1[2] = "দুই ";
            X1[3] = "তিন ";
            X1[4] = "চার ";
            X1[5] = "পাঁচ ";
            X1[6] = "ছয় ";
            X1[7] = "সাত ";
            X1[8] = "আট ";
            X1[9] = "নয় ";
            X1[10] = "দশ ";
            X1[11] = "এগার ";
            X1[12] = "বার ";
            X1[13] = "তের ";
            X1[14] = "চৌদ্দ ";
            X1[15] = "পনের ";
            X1[16] = "ষোল ";
            X1[17] = "সতের ";
            X1[18] = "আঠার ";
            X1[19] = "ঊনিষ ";
            X1[20] = "বিষ ";
            X1[21] = "একুশ ";
            X1[22] = "বাইশ ";
            X1[23] = "তেইশ ";
            X1[24] = "চব্বিশ ";
            X1[25] = "পঁচিশ ";
            X1[26] = "ছাব্বিশ ";
            X1[27] = "সাতাশ ";
            X1[28] = "আটাশ ";
            X1[29] = "ঊনত্রিশ ";
            X1[30] = "ত্রিশ ";
            X1[31] = "একত্রিশ ";
            X1[32] = "বত্রিশ ";
            X1[33] = "তেত্রিশ ";
            X1[34] = "চৌত্রিশ ";
            X1[35] = "পঁয়ত্রিশ ";
            X1[36] = "ছত্রিশ ";
            X1[37] = "সাঁইত্রিশ ";
            X1[38] = "আটত্রিশ ";
            X1[39] = "ঊনচল্লিশ ";
            X1[40] = "চল্লিশ ";
            X1[41] = "একচল্লিশ ";
            X1[42] = "বিয়াল্লিশ ";
            X1[43] = "তেতাল্লিশ ";
            X1[44] = "চুয়াল্লিশ ";
            X1[45] = "পঁয়তাল্লিশ ";
            X1[46] = "ছেচল্লিশ ";
            X1[47] = "সাতচল্লিশ ";
            X1[48] = "আটচল্লিশ ";
            X1[49] = "ঊনপঞ্চাশ ";
            X1[50] = "পঞ্চাশ ";
            X1[51] = "একান্ন  ";
            X1[52] = "বায়ান্ন ";
            X1[53] = "তিপ্পান্ন  ";
            X1[54] = "চুয়ান্ন  ";
            X1[55] = "পঞ্চান্ন ";
            X1[56] = "ছাপ্পান্ন ";       
            X1[57] = "সাতান্ন  ";
            X1[58] = "আটান্ন  ";
            X1[59] = "ঊনষাট ";          
            X1[60] = "ষাট ";
            X1[61] = "একষট্টি ";
            X1[62] = "বাষট্টি ";
            X1[63] = "তেষট্টি ";
            X1[64] = "চৌষট্টি ";
            X1[65] = "পঁয়ষট্টি ";
            X1[66] = "ছেষট্টি ";
            X1[67] = "সাতষট্টি ";
            X1[68] = "আটষট্টি ";
            X1[69] = "ঊনসত্তর ";           
            X1[70] = "সওর ";
            X1[71] = "একাত্তর ";
            X1[72] = "বাহাত্তর ";
            X1[73] = "তিয়াত্তর ";
            X1[74] = "চুয়াত্তর ";
            X1[75] = "পঁচাত্তর ";
            X1[76] = "ছিয়াত্তর ";
            X1[77] = "সাতাত্তর ";
            X1[78] = "আটাত্তর ";
            X1[79] = "ঊনআশি ";
            X1[80] = "আশি ";
            X1[81] = "একাশি ";
            X1[82] = "বিরাশি ";
            X1[83] = "তিরাশি ";
            X1[84] = "চুরাশি ";
            X1[85] = "পঁচাশি ";
            X1[86] = "ছিয়াশি ";
            X1[87] = "সাতাশি ";
            X1[88] = "আটাশি ";
            X1[89] = "ঊননব্বই ";
            X1[90] = "নব্বই ";
            X1[91] = "একানব্বই ";
            X1[92] = "বিরানব্বই ";
            X1[93] = "তিরানব্বই ";
            X1[94] = "চুরানব্বই ";
            X1[95] = "পঁচানব্বই ";
            X1[96] = "ছিয়ানব্বই ";
            X1[97] = "সাতানব্বই ";
            X1[98] = "আটানব্বই ";
            X1[99] = "নিরানব্বই ";

            for (int I1 = 1; I1 <= 99; I1++)
                X1[ I1] =  X1[I1];

           

            Y1[1] = "শত ";
            Y1[2] = " হাজার ";
            Y1[3] = (Index >= 3 ? "লাখ " : "লাখ ");
            Y1[4] = (Index >= 3 ? "কোটি " : "কোটি ");
           //Y1[5] = "Trillion ";
            Z1[1] = "বিয়োগ ";
            Z1[2] = "শূন্য ";
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
            string R = "";
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
                    M[6] = ((Convert.ToInt32(N_2) > 0) ? " পয়সা " : "");
                   // P = M[6] + M[1];
                    P = M[1] + M[6];
                    R =(O.Length) > 0 ? " টাকা " : "";
                    Q_ = "(" + O + R + " " + P + " মাত্র )";
                    break;
                
            }
            return Q_;
        }
    }
}
