using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_81_Rec
{
    [Serializable]
    public class CreateOffLt
    {

        public string gcod { get; set; }
        public string descp { get; set; }

        public CreateOffLt()
        {
        
        }

        public CreateOffLt(string gcod, string descp)
        {
            this.gcod = gcod;
            this.descp = descp;
        }
    }

    public class SalInfo
    {
        public string cat { get; set; }
        public string grade { get; set; }
        public double gross { get; set; }
        public string desig { get; set; }
        public double bassal { get; set; }
        public double hrent { get; set; }
        public double mallow { get; set; }
        public double tpt { get; set; }
        public double mob { get; set; }
        public double internet { get; set; }
        public string rmks { get; set; }
        
    }
    [Serializable]
    public class EmpAssesment
    {
        public string assdesc { get; set; }
        public bool exc { get; set; }
        public bool good { get; set; }
        public bool avrg { get; set; }
        public bool poor { get; set; }
        public bool nill { get; set; }
    }

    [Serializable]
    public class RptAssYearly
    {
        public string empname { get; set; }
        public string secdesig { get; set; }
        public double mark { get; set; }
        public string position { get; set; }
        public RptAssYearly() { }
    }

    [Serializable]

    public class RptConfmLt
    {
        public string confmno { get; set; }
        public string empname { get; set; }
        public string fname { get; set; }
        public string padd { get; set; }
        public DateTime condate  { get; set; }
        public string refno { get; set; }
    }
    [Serializable]
    public class EmpAllInformation
    {
        public string grpid { get; set; }
        public string empid { get; set; }
        public string gcod { get; set; }
        public string gdesc { get; set; }
        public string gdescbn { get; set; }
        public double amt { get; set; }
        public string gdatat1 { get; set; }
        public string gdatat2 { get; set; }
        public string gdatat3 { get; set; }
        public string gdatat4 { get; set; }
        public double percnt { get; set; }
        public string unit { get; set; }
        public double qty { get; set; }
        public double raate { get; set; }
        public string grpdesc { get; set; }
        public string grpdescbn { get; set; }
        public string tdesc { get; set; }
        public string tdescbn { get; set; }
        public string empimg { get; set; }
        public string empsign { get; set; }

        public EmpAllInformation() { }
    }

    [Serializable]
    public class CodeBookInfo
    {
        public string hrgcod { get; set; }
        public string hrgdesc { get; set; }
        public string sircode { get; set; }
        //public image sirdesc_img { get; set; }
        public string sirdesc { get; set; }
        public string sirdescbn { get; set; }
        public string sirunit { get; set; }
        public double sirval { get; set; }
        public string sirtype { get; set; }
        public string sirtdes { get; set; }
        public string userdesc { get; set; }
        public CodeBookInfo() { }
    }

    [Serializable]
    public class HrgInfo
    {
        //comcod,hrgcod,hrgdesc,percnt
        public string comcod { get; set; }
        public string hrgcod { get; set; }
        public string hrgdesc { get; set; }
        public double percnt { get; set; }

        public HrgInfo() { }
    }
}
