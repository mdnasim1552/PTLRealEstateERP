using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_22_Sal
{
   public  class EClassSales
    {
       [Serializable]
       public class SalesInventory 
       
       {

           public string grp{get;set;}
           public string grpdesc{get;set;}
           public string pactcode{get;set;}
         
           public string grp1{get;set;}
           public string prodesc{get;set;}
           public string loc{get;set;}
           public double uqty{get;set;}
           public double soldqty{get;set;}
           public double mgtbook{get;set;}
           public double usoldqty{get;set;}
           public string hdate{get;set;}

           public SalesInventory()
           {
           }
       
       }


       [Serializable]
       public class SalesInterest
       {

           public string comcod { get; set; }
           public string grp { get; set; }
           public string grpdesc { get; set; }
           public string rowid { get; set; }
           public string inscod { get; set; }
           public string insdes { get; set; }
           public string mrno { get; set; }
           public string chqno { get; set; }
           public DateTime cinsdat { get; set; }
           public DateTime trdat { get; set; }
           public DateTime paydate { get; set; }
           public DateTime recndat { get; set; }
           public string day { get; set; }
           public double cinsam { get; set; }
           public double pamount { get; set; }
           public double cumdue { get; set; }
           public double cumpaid { get; set; }
           public double cumbalance { get; set; }
           public double interest { get; set; }
           public double cuminterest { get; set; }
           public double dueamt { get; set; }
           public double discharge { get; set; }
           public double delcharge { get; set; }
           public double benifit { get; set; }

           public SalesInterest()
           {
           }

       }


    }
}
