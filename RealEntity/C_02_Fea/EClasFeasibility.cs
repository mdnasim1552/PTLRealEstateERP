using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_02_Fea
{
    public class EClasFeasibility
    {
        [Serializable]
        public class EClassProFeasibility
        {
            public string infcod { get; set; }
            public string unit { get; set; }
            public double rsize { get; set; }
            public double number { get; set; }
            public double tsize { get; set; }
            public double salrate { get; set; }
            public double amt { get; set; }
            public string grpdesc { get; set; }
            public string infdesc { get; set; }
            public double pecent { get; set; }
            public double ccostosarea { get; set; }
            public double ccostocarea { get; set; }


          
            public EClassProFeasibility()
            {

            }

            public EClassProFeasibility(string infcod, string unit, double rsize, double number, double tsize, double salreate, double amt, string grpdesc,
                    string infdesc, double pecent, double ccostosarea, double ccostocarea)
            {

                this.infcod = infcod;
                this.unit = unit;
                this.rsize = rsize;
                this.number = number;
                this.tsize = tsize;
                this.salrate = salreate;
                this.grpdesc = grpdesc;
                this.infdesc = infdesc;
                this.pecent = pecent;
                this.ccostosarea = ccostosarea;
                this.ccostocarea = ccostocarea;



            }

        }

        [Serializable]
        public class ProjectTopSheet01
        {
            public string comcod { get; set; }
            public string catcode { get; set; }
            public double tolamt { get; set; }
            public string prgdesc { get; set; }
            public double l1 { get; set; }
            public double l2 { get; set; }
            public double l3 { get; set; }
            public double l4 { get; set; }
            public double l5 { get; set; }
            public double l6 { get; set; }
            public double l7 { get; set; }
            public double l8 { get; set; }
            public double l9 { get; set; }
            public double l10 { get; set; }
            public double l11 { get; set; }
            public double l12 { get; set; }
            public double l13 { get; set; }
            public double l14 { get; set; }
            public double l15 { get; set; }
            public double l16 { get; set; }
            public double l17 { get; set; }
            public double l18 { get; set; }
            public double l19 { get; set; }
            public double l20 { get; set; }
            public double l21 { get; set; }
            public double l22 { get; set; }
            public double l23 { get; set; }
            public double l24 { get; set; }
            public double l25 { get; set; }
            public double l26 { get; set; }
            public double l27 { get; set; }
            public double l28 { get; set; }
            public double l29 { get; set; }
            public double l30 { get; set; }
            public double l31 { get; set; }
            public double l32 { get; set; }
            public double l33 { get; set; }
            public double l34 { get; set; }
            public double l35 { get; set; }
            public double l36 { get; set; }
            public double l37 { get; set; }
            public double l38 { get; set; }
            public double l39 { get; set; }
            public double l40 { get; set; }
            public double l41 { get; set; }
            public double l42 { get; set; }
            public double l43 { get; set; }
            public double l44 { get; set; }
            public double l45 { get; set; }
            public double l46 { get; set; }
            public double l47 { get; set; }
            public double l48 { get; set; }
            public double l49 { get; set; }
            public double l50 { get; set; }
            public ProjectTopSheet01() { }
        }

        [Serializable]
        public class ProjectTopSheet02
        {
            public string comcod { get; set; }
            public string loccode { get; set; }
            public string locdesc { get; set; }
            public ProjectTopSheet02() { }
        }
        [Serializable]
        public class EClassProjectFeasibility
        {
             public string comcod { get; set; }
             public string grp { get; set; }
             public string prgcod { get; set; }
             public string prgdesc { get; set; }
             public string prgdesc1 { get; set; }
             public string percnt { get; set; }
             public string infcod { get; set; }
             public string infdesc2 { get; set; }
             public string unit { get; set; }
             public double rsize { get; set; }
             public double number { get; set; }
             public double tsize { get; set; }
             public double salrate { get; set; }
             public double amt { get; set; }
             public string grpdesc { get; set; }
             public string infdesc { get; set; }
             public string subgrp { get; set; }
             public string subgrpdesc { get; set; }
             public double pecent { get; set; }
             public double ratio { get; set; }
             public double total { get; set; }

             public EClassProjectFeasibility() { }




        }

        [Serializable]
        public class ProjectFeasibility
        {
            public string comcod { get; set; }
            public string rowid { get; set; }
            public string grp { get; set; }
            public string prgcod { get; set; }
            public string prgdesc { get; set; }
            public string prgdesc1 { get; set; }
            public string unit { get; set; }
            public double buildarea { get; set; }
            public double ratio { get; set; }
            public double total { get; set; }
            public double irate { get; set; }
            public double itwlemont { get; set; }
            public double imonth { get; set; }
            public double intamt { get; set; }
            public double conspersft { get; set; }
            public double salablepersft { get; set; }
            public double brkpointpersft { get; set; }
            public string percnt { get; set; }
            public ProjectFeasibility() { }
        }

        [Serializable]

        public class ProfitAndLoss
        {
            public string grp { get; set; }
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string estgcod { get; set; }
            public string estgdesc { get; set; }
            public string prgval { get; set; }
            public double estcost { get; set; }
            public double actual { get; set; }
            public double balamt { get; set; }
            public double fundamt { get; set; }
            public double percnt { get; set; }
            public DateTime paymentdate { get; set; }

            public ProfitAndLoss() { }
        }
        [Serializable]
        public class AgeingDays
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string gdesc { get; set; }
            public string aginday { get; set; }
            public DateTime efectivedate { get; set; }
            public AgeingDays() { }
        }


        [Serializable]
        public class ProdCostAnalysis
        {
           // comcod,pactcode,pactdesc,munit,udesc ,usize,uamt,purvalue,purinstive,tpurcost,fxtcost,othmktexp, salincentive, adminovh, tcost,
           //commitedval,margin=commitedval-tcost,accosttill ,ageingday,validity,ageing,remainday,purdate
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string munit { get; set; }
            public string udesc { get; set; }
            public double usize { get; set; }
            public double uamt { get; set; }
            public double purvalue { get; set; }
            public double purinstive { get; set; }
            public double tpurcost { get; set; }
            public double fxtcost { get; set; }
            public double othmktexp { get; set; }
            public double salincentive { get; set; }
            public double adminovh { get; set; }
            public double tcost { get; set; }
            public double commitedval { get; set; }
            public double margin { get; set; }
            public double accosttill { get; set; }
            public string ageingday { get; set; }
            public string validity { get; set; }
            public string ageing { get; set; }
            public string remainday { get; set; }
            public DateTime purdate { get; set; }
            public ProdCostAnalysis() { }
        }


    }
}
