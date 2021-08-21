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
    public static  class ASITUtility02
    {

        public static bool TransactionDateCon(DateTime bdate, DateTime date)
        {
            DateTime Sysdate = System.DateTime.Now;
           date = Convert.ToDateTime(date.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToString("hh:mm:ss tt"));
           int todays = (int)(date.Date - bdate.Date).TotalDays;
           bool result;
           //bool fresult = (bdate <= date) ? true : false;
           //bool sresult = (date <= Sysdate) ? true : false;
            
            if (todays == 0)
           {
               result = (date <= bdate && date <= Sysdate) ? true : false;

           }
           else
           {
               result = (bdate < date && date <= Sysdate) ? true : false;
           
           }          

          //  bool result = (bdate <date &&  date<=Sysdate) ? true : false;
            return result;

        }

        public static bool TransPostDateCheque(DateTime chqdate, DateTime date) 
        {
            bool result = (date >= chqdate) ? true : false;
            return result;
        }


        public static bool TransReconDate(DateTime recondate, DateTime date)
        {
            bool result = (date <= recondate) ? true : false;
            return result;
        }



        public static bool PurChaseOperation(DateTime date1, DateTime date2)
        {

            bool result = (date1 <=date2) ? true : false;
            return result;

        }



         public static bool TransactionDateOpening(DateTime opening, DateTime date)
        {
           
            bool result = (opening > date) ? true : false;
            return result;

        }

        
   public static  List<string> pasyear(int frmyear, int toyear)


    {

       List<string> passyear = new List<string>();
        while (frmyear <= toyear) 
        {


            passyear.Add(frmyear.ToString());
            frmyear = frmyear + 1;
        }

        return (passyear);
    }


   public static string DecryptValue(string Name) 
   {

      
       string DecryptName = "";
       int a = 5;
       char name;
       for (int i = 0; i < Name.Length; i++)
       {
           name = Convert.ToChar(Name.Substring(i, 1));
           DecryptName =DecryptName+Convert.ToChar(name - a).ToString();
       }

       return DecryptName;
   }

   public static string EncryptValue(string Name)
   {

 
       string EncryptName = "";
       int a = 5;
       char name;
       for (int i = 0; i < Name.Length; i++)
       {
           name = Convert.ToChar(Name.Substring(i, 1));
           EncryptName = EncryptName + Convert.ToChar(name + a).ToString();
       }

       return EncryptName;
   }

      public static  string ToBangla( string enamt)
      {
          // 25369
          string banamt = "";
          char[] eng = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
          char[] ban = { '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯'};
          int len = enamt.Length;
          foreach (var x in enamt)
          {
              string v = x.ToString();
              banamt += (v == "." || v == "," || v == "-" ? v : ban[int.Parse(v)].ToString());
          }

          return banamt;
      }
   public static string EngtoBandigit( string enamt)
   {
       string bamt="";
       //char[] ban = { '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯', '.' };
       //char[] eng = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

        foreach (var i in enamt)
       {

           switch (i) 
           {
           
               case '0':
                   bamt += '০';
                   break;
               case '1':
                   bamt += '১';
                   break;
               case '2':
                   bamt += '২';
                   break;
               case '3':
                   bamt += '৩';
                   break;
               case '4':
                   bamt += '৪';
                   break;
               case '5':
                   bamt += '৫';
                   break;
               case '6':
                   bamt += '৬';
                   break;
               case '7':
                   bamt += '৭';
                   break;
               case '8':
                   bamt += '৮';
                   break;
               case '9':
                   bamt += '৯';
                   break;

               default:
                   bamt += i;
                   break;
           
           }
            
            //int trv=0;

            //while (eng[trv]!='\0')
            //{
            
            //    if(i==eng[trv])
            //    {
                
            //     bamt+=ban[trv];
            //        break;
            //    }
            //    trv++;
            
            //}


           //name = Convert.ToChar(Name.Substring(i, 1));
           //EncryptName = EncryptName + Convert.ToChar(name + a).ToString();
       }

       //char []enamt1=enamt.Split('');





        return bamt;
   
   }


 
  


    }
}
