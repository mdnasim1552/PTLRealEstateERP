using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_24_CC
{
    [Serializable]
    public class RptCstRegstration
    {
        public string regdesc { get; set; }
        public double totalvalue { get; set; }
        public double recamt { get; set; }
        public double Acamt { get; set; }
        public double rcvamtp { get; set; }
        public double acamtp { get; set; }
    }
    [Serializable]
    public class RptLandInfo
    {
        public string udesc { get; set; }
        public string munit { get; set; }
        public double usize { get; set; }
        public double fsize { get; set; }
        public string customer { get; set; }
        public double noflat { get; set; }
        public double totaland { get; set; }
        public string gunit { get; set; }
    }

    [Serializable]
    public class EClassCustRegistrationStatus
    {

        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string pactdesc { get; set; }
        public string custname { get; set; }
        public string unitdesc { get; set; }
        public string rtlegdept { get; set; }
        public string rclegdept { get; set; }
        public string ptclient { get; set; }
        public string pcclient { get; set; }


        public EClassCustRegistrationStatus() { }



    }

    [Serializable]
    public class EClassRegistrationStatusAllPro
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string regcode { get; set; }
        public string pactdesc { get; set; }
        public string custname { get; set; }
        public string udesc { get; set; }
        public string regdesc { get; set; }
        public double suamt { get; set; }
        public double reconamt { get; set; }
        public double inproamt { get; set; }
        public double recamt { get; set; }
        public double bamt { get; set; }
        public double cdelay { get; set; }
        public double discharge { get; set; }
        public double delayadis { get; set; }
        public double peronsal { get; set; }

        public EClassRegistrationStatusAllPro() { }


    }

    [Serializable]
    public class EClassClientLetterInfo
    {
        public string comcod { get; set; }
        public string chk { get; set; }
        public string usircode { get; set; }
        public string name { get; set; }
        public string paddress { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public EClassClientLetterInfo() { }

    }

    [Serializable]
    public class EClassClientChoice
    {
        public string hcode { get; set; }
        public string mgcod { get; set; }
        public string gcod { get; set; }
        public string gdesc { get; set; }
        public string hdesc { get; set; }
        public string wdesc { get; set; }
        public string mgdesc { get; set; }
        public string unit { get; set; }
        public double rate { get; set; }
        public double qty { get; set; }
        public double crate { get; set; }
        public double clrate { get; set; }
        public string chk { get; set; }
        public string chkin { get; set; }
        public EClassClientChoice() { }

    }

}
