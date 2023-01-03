using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_09_PIMP
{
    public class EClassExecution
    {
        #region
        [Serializable]
        public class EmplemanPlan
        {

            public string comcod { get; set; }
            public string flrdes { get; set; }
            public string rptdesc1 { get; set; }
            public string rptunit { get; set; }
            public double rptrat { get; set; }
            public double preqty { get; set; }
            public double preamt { get; set; }
            public double curqty { get; set; }
            public double curamt { get; set; }
            public double toqty { get; set; }
            public double toamt { get; set; }

            public EmplemanPlan() { }


        }
        #endregion
        #region
        [Serializable]
        public class SubConPrjBillDetails
        {

            public string comcod { get; set; }
            public string pactdesc { get; set; }
            public double billamt { get; set; }
            public double payment { get; set; }
            public double netpayable { get; set; }

            public SubConPrjBillDetails() { }


        }


        #endregion


        #region
        [Serializable]
        public class MonthlyPlan
        {

            public string comcod { get; set; }
            public string flrcod { get; set; }
            public string rptcod { get; set; }
            public string flrdes { get; set; }
            public string rptunit { get; set; }
            public string rptdesc { get; set; }
            public double rptqty { get; set; }
            public double comqty { get; set; }
            public double balqty { get; set; }
            public double rptrat { get; set; }
            public double trqty { get; set; }
            public double qty { get; set; }
            public double rptamt { get; set; }
            public MonthlyPlan() { }


        }


        #region
        [Serializable]
        public class MonthlyPlanVSExecution
        {

            public string comcod { get; set; }
            public string flrdes { get; set; }
            public string rptdesc1 { get; set; }
            public string rptunit { get; set; }
            public double ppreqty { get; set; }
            public double ppreamt { get; set; }
            public double pcurqty { get; set; }
            public double pcuramt { get; set; }
            public double ptoqty { get; set; }
            public double ptoamt { get; set; }
            public double epreqty { get; set; }
            public double epreamt { get; set; }
            public double ecurqty { get; set; }
            public double ecuramt { get; set; }
            public double etoqty { get; set; }
            public double etoamt { get; set; }
            public double vtoqty { get; set; }
            public double vtoamt { get; set; }


            public MonthlyPlanVSExecution() { }

            #endregion
        }














        #endregion

        #region
        [Serializable]
        public class RptMaPlanVsPlanVsEx
        {

            public string comcod { get; set; }
            public string flrdes { get; set; }
            public string sirdesc { get; set; }
            public string sirunit { get; set; }
            public double rptrat { get; set; }
            public double mapqty { get; set; }
            public double pqty { get; set; }
            public double eqty { get; set; }
            public double mpamt { get; set; }
            public double mamt { get; set; }
            public double examt { get; set; }
            public double experc { get; set; }


            public RptMaPlanVsPlanVsEx() { }

            #endregion
        }

        [Serializable]
        public class WorkExecution
        {

            public string flrcod { get; set; }
            public string itemcode { get; set; }
            public string flrdes { get; set; }
            public string workitem { get; set; }
            public double wrkqty { get; set; }
            public string wrkunit { get; set; }
            public double balqty { get; set; }
            public string isuno { get; set; }
            public string billno { get; set; }

            public WorkExecution()
            {


            }

        }



        [Serializable]
        public class BudgetVsExecution
        {

            public string bldcod { get; set; }
            public string rptcod { get; set; }
            public double bgdrat { get; set; }
            public double bgdqty { get; set; }
            public double bgdamt { get; set; }
            public double eqty { get; set; }
            public double eamt { get; set; }
            public double varqty { get; set; }
            public double varamt { get; set; }
            public double prcent { get; set; }
            public string flrdes { get; set; }
            public string rptdesc1 { get; set; }
            public string rptunit { get; set; }

            public BudgetVsExecution()
            {


            }


        }





    }



}
