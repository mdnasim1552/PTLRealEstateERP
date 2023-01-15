using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_90_PF
{
    [Serializable]
     
    public class PFAlliance
    {
        public string comcod { get; set; }
        public string empid { get; set; }
        public string idcard { get; set; }
        public string unit { get; set; }
        public string section { get; set; }
        public string desigid { get; set; }
        public string empname { get; set; }
        public string unitdesc { get; set; }
        public DateTime pfsdat { get; set; }
        public string sectionname { get; set; }
        public string desig { get; set; }
        public double opnam { get; set; }
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
        public double toam { get; set; }
        public double contriamt { get; set; }
        public double benamt { get; set; }
        public double netamt { get; set; }
        public PFAlliance() { }
    }
    [Serializable]

    public class ProvidedFund
    {
        public string comcod { get; set; }
        public string refno { get; set; }
        public string empid { get; set; }
        public string gcod { get; set; }
        public string secid { get; set; }
        public string empname { get; set; }
        public string idcard { get; set; }
        public string desig { get; set; }
        public DateTime joindate { get; set; }
        public string refno1 { get; set; }
        public double bsal { get; set; }
        public double gspay { get; set; }
        public double pfund { get; set; }
        public string sectioname { get; set; }
        public string refdesc { get; set; }

        public ProvidedFund() { }
    }
    [Serializable]

    public class IndPfSattlement
    {
        public string comcod { get; set; }
        public string yearid { get; set; }
        public double salary { get; set; }
        public double pfamt { get; set; }
        public double swf { get; set; }
        public double totalamt { get; set; }


        public IndPfSattlement()
        {
        }
    }

    [Serializable]

    public class EmpWiseSWF
    {
        public string comcod { get; set; }
        public string monthid { get; set; }
        public string paydate { get; set; }
        public double salary { get; set; }
        public double pfamt { get; set; }
        public double swf { get; set; }
        public double totalamt { get; set; }


        public EmpWiseSWF()
        {
        }
    }
}
