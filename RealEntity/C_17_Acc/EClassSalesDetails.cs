using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace RealEntity.C_17_Acc
{
    public class EClassSalesDetails
    {


        public string actdesc { set; get; }
        public double advsale { set; get; }
        public double sale { set; get; }
        public double received { set; get; }
        public double recagsal { set; get; }
        public double receivable { set; get; }
        public double costam { set; get; }
        public double margin { set; get; }
        public EClassSalesDetails(string actdesc, double advsale, double sale, double received, double recagsal, double receivable, double costam, double margin)
        {
            this.actdesc = actdesc;
            this.advsale = advsale;
            this.sale = sale;
            this.received = received;
            this.recagsal = recagsal;
            this.receivable = receivable;
            this.costam = costam;
            this.margin = margin;
        }
    }

    [Serializable]
    public class RptSalPerWise
    {
        public string salesperson { get; set; }
        public string pactdesc { get; set; }

        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        public double amt7 { get; set; }
        public double amt8 { get; set; }
        public double amt9 { get; set; }
        public double amt10 { get; set; }
        public double amt11 { get; set; }
        public double amt12 { get; set; }
        public double toamt { get; set; }
    }



    [Serializable]
    public class RptMonWiseColBuyer
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string pactdesc { get; set; }
        public string usircode { get; set; }
        public string usirdesc { get; set; }
        public double amount { get; set; }
    }

    [Serializable]
    public class RptMonWiseCol
    {
        public string pactdesc { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        public double amt7 { get; set; }
        public double amt8 { get; set; }
        public double amt9 { get; set; }
        public double amt10 { get; set; }
        public double amt11 { get; set; }
        public double amt12 { get; set; }
        public double toamt { get; set; }
        public double netamt { get; set; }

        public RptMonWiseCol() { }

        public RptMonWiseCol(double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8,
            double a9, double a10, double a11, double a12)
        {
            this.amt1 = a1;
            this.amt2 = a2;
            this.amt3 = a3;
            this.amt4 = a4;
            this.amt5 = a5;
            this.amt6 = a6;
            this.amt7 = a7;
            this.amt8 = a8;
            this.amt9 = a9;
            this.amt10 = a10;
            this.amt11 = a11;
            this.amt12 = a12;
        }

    }

    [Serializable]
    public class RptMonthValue
    {
        public string monthName { get; set; }
        public double value { get; set; }

        public RptMonthValue() { }

        public RptMonthValue(string name, double data)
        {
            this.monthName = name;
            this.value = data;

        }

    }


    [Serializable]
    public class RptProjectWiseCollectionStatus
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string usirdesc { get; set; }
        public double paidamt { get; set; }//
        public double totalsval { get; set; }//
        public double balance { get; set; }//
        public double cparkam { get; set; }//
        public double utlityam { get; set; }//
        public string udesc { get; set; }
        public double usize { get; set; }
        public string munit { get; set; }
        public string custname { get; set; }
        public string handovrdat { get; set; }
        public string pactdesc { get; set; }
        public string position { get; set; }
        public string regdesc { get; set; }
        public string loan { get; set; }


        public RptProjectWiseCollectionStatus()
        {

        }


    }

    [Serializable]
    public class RptProjectWiseCollectionStatusall
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string custname { get; set; }
        public string udesc { get; set; }
        public double usize { get; set; }
        public double uamt { get; set; }
        public double paidamt { get; set; }
        public double balance { get; set; }
      


        public RptProjectWiseCollectionStatusall()
        {

        }


    }


}
