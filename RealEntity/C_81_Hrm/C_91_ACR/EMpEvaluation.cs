using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_91_ACR
{
    [Serializable]
   public class EMpEvaluation
    {     
        public string mgcod { get; set; }
        public string gcod { get; set; }
        public string sgcod1 { get; set; }
        public string sgcod2 { get; set; }
        public string sgcod3 { get; set; }
        public string sgcod4 { get; set; }
        public string sgcod5 { get; set; }
        public string sgval1 { get; set; }
        public string sgval2 { get; set; }
        public string sgval3 { get; set; }
        public string sgval4 { get; set; }
        public string sgval5 { get; set; }
        public string mgdesc { get; set; }
        public string gdesc { get; set; }
        public string dgdesc { get; set; }
        public string sgdesc1 { get; set; }
        public string sgdesc2 { get; set; }
        public string sgdesc3 { get; set; }
        public string sgdesc4 { get; set; }
        public string sgdesc5 { get; set; }
        public double n1 { get; set; }
        public double n2 { get; set; }
        public double n3 { get; set; }
        public double n4 { get; set; }
        public double n5 { get; set; }
        public EMpEvaluation() { }
    }

    [Serializable]
    public class Rptnumber
    {
         public string mgcod { get; set; }
         public string mgdesc { get; set; }
         public double amt { get; set; }

        public Rptnumber(){}

        public Rptnumber(string Mgcod, string Mgdesc, double Amt)
        {
            this.mgcod = Mgcod;
            this.mgdesc = Mgdesc;
            this.amt = Amt;
        }
    }
    [Serializable]
    public class RptEmpDetails
    {
        public string empname { get; set; }
        public string empid { get; set; }
        public DateTime empjoindate { get; set; }
        public DateTime confirmdate { get; set; }
        public string empdesig { get; set; }
        public string empsection { get; set; }
        public string sname { get; set; }
        public string sdesig { get; set; }

        public RptEmpDetails ( ) { }

        public RptEmpDetails(string Empname,string Empid,DateTime EmpJoindate,DateTime Empconfirmdate,string Empdesig,string Empsection,string Sname,string Sdesig)
        {
            this.empname = Empname;
            this.empid = Empid;
            this.empjoindate = EmpJoindate;
            this.confirmdate = Empconfirmdate;
            this.empdesig = Empdesig;
            this.empsection = Empsection;
            this.sname = Sname;
            this.sdesig = Sdesig;
        }
    }

}
