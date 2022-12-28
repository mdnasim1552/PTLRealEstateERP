using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_93_AnnInc
{
    public class AnnIncReport
    {

        [Serializable]
        public class AnnualIncrement
        {
            public string comcod { get; set; }
            public string companycod { get; set; }
            public string deptcode { get; set; }
            public string seccode { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public DateTime confirmdate { get; set; }
            public decimal years { get; set; }
            public decimal months { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public decimal grossal { get; set; }
            public decimal inpercnt { get; set; }
            public decimal incamt { get; set; }

            public decimal finincamt { get; set; }
            public decimal revisesal { get; set; }
            public AnnualIncrement() { }





        }



        [Serializable]
        public class AnnualIncrementStatus
        {
            public string comcod { get; set; }
            public string incrno { get; set; }
            public string incrno1 { get; set; }
            public string incrdate1 { get; set; }
            public string companycod { get; set; }
            public string deptcode { get; set; }
            public string idcardno { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public DateTime joindate { get; set; }
            public DateTime confirmdate { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public decimal grossal { get; set; }
            public decimal inpercnt { get; set; }
            public decimal incamt { get; set; }
            public decimal finincamt { get; set; }
            public decimal tosalary { get; set; }
            public decimal bsal { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }
            public decimal pickup { get; set; }
            public decimal entaint { get; set; }
            public string slength { get; set; }
            public string tkinwrd { get; set; }
            public AnnualIncrementStatus() { }


        }






    }
}
