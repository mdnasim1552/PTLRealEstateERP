using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEntity.C_09_PIMP
{
    public class EClassOrder
    {
        [Serializable]
        #region GetWorkorder
        public class GetWorkOrder
        {
            public int rowid { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string flrcod { get; set; }
            public string flrdes { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spec { get; set; }
            public DateTime comncdat { get; set; }
            public DateTime compltdat { get; set; }
            public string csirdesc { get; set; }
            public string conadd { get; set; }
            public string atten { get; set; }
            public string rmrks { get; set; }
            public double ordrrate { get; set; }
            public string sdetails { get; set; }
            public double ordqty { get; set; }
            public double ordamt { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public GetWorkOrder() { }
            public GetWorkOrder(int rowid, string grp, string grpdesc, string comcod, string flrcod, string flrdes, string rsircode, string rsirdesc,
                string rsirunit, string csirdesc, string conadd, string atten, string rmrks, double ordrrate, string sdetails, double ordqty, double ordamt, string mobile, string spec, string email, DateTime compltdat, DateTime comncdat)
            {
                this.rowid = rowid;
                this.grp = grp;
                this.grpdesc = grpdesc;
                this.comcod = comcod;
                this.flrcod = flrcod;
                this.flrdes = flrdes;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.rsirunit = rsirunit;
                this.csirdesc = csirdesc;
                this.conadd = conadd;
                this.atten = atten;
                this.ordrrate = ordrrate;
                this.rmrks = rmrks;
                this.sdetails = sdetails;
                this.ordqty = ordqty;
                this.ordamt = ordamt;
                this.mobile = mobile;
                this.email = email;
                this.spec = spec;
                this.compltdat = compltdat;
                this.comncdat = comncdat;

            }


        }


        #endregion

        [Serializable]

        #region GetWorkOrder1
        public class GetWorkOrder1
        {
            //a.comcod, a.orderno,a.csircode,  a.orderdat,  a.leterdes,csirdesc= b.sirdesc, a.subject, a.pordref,  term=a.pordnar

            public string comcod { get; set; }
            public string orderno { get; set; }
            public string csircode { get; set; }
            public DateTime orderdat { get; set; }
            public string leterdes { get; set; }
            public string csirdesc { get; set; }
            public string subject { get; set; }
            public string pordref { get; set; }

            public string term { get; set; }
            public string duration { get; set; }
            public string billtype { get; set; }
            public string paddress { get; set; }
            public string pactdesc { get; set; }

            public GetWorkOrder1()

            { }


            public GetWorkOrder1(string comcod, string orderno, string csircode, DateTime orderdat, string leterdes,
             string csirdesc, string subject, string pordref, string term, string duration, string paddress, string pactdesc)
            {
                this.comcod = comcod;
                this.orderno = orderno;
                this.csircode = csircode;
                this.orderdat = orderdat;
                this.leterdes = leterdes;
                this.csirdesc = csirdesc;
                this.subject = subject;
                this.pordref = pordref;
                this.term = term;
                this.duration = duration;
                this.paddress = paddress;
                this.pactdesc = pactdesc;



            }

        }
        #endregion

        //  flrcod,  rsircode, grp, grpdesc, flrdes, rsirdesc, rsirunit,  prcent, isuqty, bgdrat, isurat, wrkrate, isuamt, balqty, wrkqty, peronbgd old
//        flrcod,  rsircode, grp, grpdesc, flrdes, rsirdesc, rsirunit,  prcent, isuqty, bgdrat, isurat, wrkrate, isuamt, balqty,preqty, wrkqty, peronbgd,mbbook, bdedamt, above, amount,
//percntge,sdamt, balamt,dedamt,penamt,advamt,reward,prevrat,prevqty, toqty=isuqty+preqty ,dedqty, dedunit, dedrate, idedamt	

        [Serializable]
        public class Labissue
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string flrcod { get; set; }
            public string rsircode { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string flrdes { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public double prcent { get; set; }
            public double isuqty { get; set; }
            public double bgdrat { get; set; }
            public double isurat { get; set; }
            public double wrkrate { get; set; }
            public double isuamt { get; set; }
            public double preqty { get; set; }
            public double wrkqty { get; set; }
            public double peronbgd { get; set; }
            public string mbbook { get; set; }
            public double adedamt { get; set; }
            public double above { get; set; }
            public double amount { get; set; }
            public double percntge { get; set; }
            public double sdamt { get; set; }
            public double balamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double advamt { get; set; }
            public double reward { get; set; }
            public double prevrat { get; set; }
            public double prevqty { get; set; }
            public double toqty { get; set; }
            public double dedqty { get; set; }
            public string dedunit { get; set; }
            public double dedrate { get; set; }
            public double idedamt { get; set; }
            public double balqty { get; set; }
            public Labissue() { }
        }

        //comcod, billno,  lisuno, pactcode, csircode, billdate,  rmrks, lisurefno, cbillref, percntge, sdamt, 
        //   flrcod, rsircode, billqty, billamt, conqty, conrate,  pactdesc,  csirdesc,  flrdes, rsirdesc, rsirunit, peronbgd, bgdrat, grp, grpdesc 


        [Serializable]
        public class Workorder03
        {
            public string rsircode { get; set; }
            public int serial { get; set; }
            public string rsirdesc { get; set; }
            public string sdetails { get; set; }
            public string spec { get; set; }
            public string rsirunit { get; set; }
            public double bgdqty { get; set; }
            public double ordqty { get; set; }
            public double ordrrate { get; set; }
            public double ordamt { get; set; }
            public string rmrks { get; set; }

            public Workorder03()
            {

            }

        }






        [Serializable]
        public class BillFinalization
        {
            public string comcod { get; set; }
            public string billno { get; set; }
            public string lisuno { get; set; }
            public string lisuno2 { get; set; }
            public string pactcode { get; set; }
            public string csircode { get; set; }
            public DateTime billdate { get; set; }
            public string rmrks { get; set; }
            public string lisurefno { get; set; }
            public string cbillref { get; set; }
            public double percntge { get; set; }
            public double sdamt { get; set; }
            public string flrcod { get; set; }
            public string rsircode { get; set; }
            public double billqty { get; set; }
            public double billamt { get; set; }
            public double conqty { get; set; }
            public double conrate { get; set; }
            public string pactdesc { get; set; }
            public string csirdesc { get; set; }
            public string flrdes { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public double peronbgd { get; set; }
            public double bgdrat { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public double bgdqty { get; set; }
            public double balqty { get; set; }
            public double wrkqty { get; set; }
            public double prcent { get; set; }
            public BillFinalization() { }
        }

        [Serializable]
        public class Billgroup
        {
            public string comcod { get; set; }
            public string resdesc { get; set; }
            public double tramt { get; set; }

            public Billgroup() { }

        }

        [Serializable]
        public class SubConWrkOrder
        {
            public string comcod { get; set; }
            public string orderno { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string csircode { get; set; }
            public string resdesc { get; set; }
            public string sdetails { get; set; }
            public string unit { get; set; }
            public string billdesc { get; set; }
            public string csirdesc { get; set; }
            public DateTime orderdat { get; set; }
            public string pordref { get; set; }
            public double ordqty { get; set; }
            public double ordrrate { get; set; }
            public double ordamt { get; set; }

            public SubConWrkOrder() { }
        }
        [Serializable]
        public class SubConBillStatus
        {

            public string csirdesc { get; set; }
            public double billamt { get; set; }
            public double approvamt { get; set; }
            public double upappovedamt { get; set; }
            public double payment { get; set; }
            public double balanec { get; set; }

            public SubConBillStatus() { }
        }
        [Serializable]
        public class SubConProjWBill
        {


            public string comcod { get; set; }
            public string resdesc { get; set; }
            public double tramt { get; set; }

            public SubConProjWBill() { }
        }
        [Serializable]
        public class SubConGrpWBill
        {


            public string comcod { get; set; }
            public string rsirgrpdesc { get; set; }
            public string rsirdesc { get; set; }

            public double billamt { get; set; }

            public SubConGrpWBill() { }
        }
        [Serializable]
        public class ProjwRptBillQty
        {


            public string comcod { get; set; }
            public string rsirgrpdesc { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public double billqty { get; set; }
            public double daviation { get; set; }

            public ProjwRptBillQty() { }
        }


        [Serializable]

        public class EClassBudVsExe
        {
            public string isircode { get; set; }
            public double bgdam { get; set; }
            public double bgdatam { get; set; }
            public double exeam { get; set; }
            public double bgdper { get; set; }
            public double proper { get; set; }
            public string isirdesc { get; set; }

            public EClassBudVsExe()
            {

            }


            public EClassBudVsExe(string isircode, double bgdam, double bgdatam, double exeam, double bgdper, double proper, string isirdesc)
            {

                this.isircode = isircode;
                this.bgdam = bgdam;
                this.bgdatam = bgdatam;
                this.exeam = exeam;
                this.bgdper = bgdper;
                this.proper = proper;
                this.isirdesc = isirdesc;

            }

        }


                [Serializable]
        public class EClassReciptvspayment
        {
            public string mpaycode { get; set; }
            public string paycode { get; set; }
            public double payam { get; set; }
            public string actdesc { get; set; }
            public string colst { get; set; }
            

            public EClassReciptvspayment()
            {

            }


            public EClassReciptvspayment(string mpaycode, string paycode, double payam, string actdesc, string colst)
            {

                this.mpaycode = mpaycode;
                this.paycode = paycode;
                this.payam = payam;
                this.actdesc = actdesc;
                this.colst = colst;
             

            }

        }
        [Serializable]
                public class EClassCashFlow
                {
                    public string grp { get; set; }
                    public string head { get; set; }
                    public double amount { get; set; }
              


                    public EClassCashFlow()
                    {

                    }


                    public EClassCashFlow(string grp, string head, double amount)
                    {

                        this.grp = grp;
                        this.head = head;
                        this.amount = amount;
                       

                    }

                }

        [Serializable]
        public class EclassFloor {

            public string flrdes { get; set; }

            public EclassFloor() 
            {
            
            }

            public EclassFloor(string flrdes) 
            {
                this.flrdes = flrdes;
            
            }
        
        }
    }

   
    
   
}