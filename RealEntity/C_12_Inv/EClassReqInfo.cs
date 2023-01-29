using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_12_Inv
{
    class EClassReqInfo
    {
    }

    public class RptMatIssStatus
    {
        public string isuno { get; set; }
        public string dmirfno { get; set; }
        public string isurefno { get; set; }
        public string isuno1 { get; set; }
        public DateTime isudat { get; set; }
        public string isudat1 { get; set; }
        public string pactcode { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public decimal isuqty { get; set; }
        public string actdesc { get; set; }
        public string sirdesc { get; set; }
        public string sirunit { get; set; }
        public string spcfdesc { get; set; }
        public double avragerate { get; set; }
        public double isuamt { get; set; }
        public RptMatIssStatus()
        {

        }




    }


    public class RptMatIssue
    {

        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public string spcfdesc { get; set; }
        public decimal isuqty { get; set; }
        public string useoflocation { get; set; }
        public string remarks { get; set; }

        public RptMatIssue()
        {

        }




    }


    public class rptPurVarAa
    {

        public string rptdesc { get; set; }
        public string rptunit { get; set; }
        public double rcvqty { get; set; }
        public double tqty { get; set; }
        public double acqty { get; set; }
        public double rptqty { get; set; }
        public double bgdstkqty { get; set; }
        public double stkqty { get; set; }



        public rptPurVarAa()
        {

        }




    }





    public class EClassGetID
    {
        public string typecode { get; set; }
        public string typedesc { get; set; }

        public EClassGetID(string typecode, string typedeesc)
        {

            this.typecode = typecode;
            this.typedesc = typedeesc;

        }


    }


    public class EClassGetSupplier
    {
        public string typecode { get; set; }
        public string typedesc { get; set; }

        public EClassGetSupplier(string typecode, string typedeesc)
        {

            this.typecode = typecode;
            this.typedesc = typedeesc;

        }


    }

    public class EClassGetSupplier02
    {
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public string curcode { get; set; }

        public string curdesc { get; set; }

        public EClassGetSupplier02(string sircode, string sirdesc, string curcode, string curdesc)
        {

            this.sircode = sircode;
            this.sirdesc = sirdesc;
            this.curcode = curcode;
            this.curdesc = curdesc;

        }


    }

    [Serializable]
    public class EClassTopSheetCashPurchase
    {
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public string reqdat { get; set; }
        public double reqqty { get; set; }
        public double reqrat { get; set; }
        public double reqamt { get; set; }

        public string rsirdesc { get; set; }
        public string spcfdesc { get; set; }
        public string ssirdesc { get; set; }
        public string pactdesc { get; set; }
        public string rsirunit { get; set; }
        public string flrdes { get; set; }

        public EClassTopSheetCashPurchase()
        {

        }

        public EClassTopSheetCashPurchase(string reqno, string reqno1, string mrfno, string reqdat, double reqqty, double reqrat, double reqamt, string rsirdesc, string spcfdesc, string ssirdesc, string pactdesc, string rsirunit, string flrdes)
        {

            this.reqno = reqno;
            this.reqno1 = reqno1;
            this.mrfno = mrfno;
            this.reqdat = reqdat;
            this.reqqty = reqqty;
            this.reqrat = reqrat;
            this.reqamt = reqamt;
            this.rsirdesc = rsirdesc;
            this.spcfdesc = spcfdesc;
            this.ssirdesc = ssirdesc;
            this.pactdesc = pactdesc;
            this.rsirunit = rsirunit;
            this.flrdes = flrdes;

        }


    }



    // comcod , reqno, reqno1=(case when reqno='' then  '' else left(reqno, 3)+substring(reqno, 8,2)+'-'+right(reqno, 5) end),  
    //mrfno, reqdat=(case when reqdat='01-jan-1900' then ''  else format(reqdat,'dd-MMM-yyyyy') end), rsircode, spcfcod , ssircode , reqqty, reqrat,  reqamt, 
    //rsirdesc=(case   when  rsircode='BBBBAAAAAAAA' then  'Grand Total :' when rsircode='AAAAAAAAAAAA'  then 'Total: '  else isnull(rsirdesc,'')end), 
    //spcfdesc, ssirdesc, pactdesc 

    [Serializable]
    public class ErptStock
    {
        public string bldcod { get; set; }
        public string pactdesc { get; set; }
        public string rptcod { get; set; }
        public decimal issueqty { get; set; }
        public decimal opqty { get; set; }
        public decimal rcvqty { get; set; }
        public decimal trninqty { get; set; }
        public decimal trnoutqty { get; set; }
        public decimal transqty { get; set; }
        public decimal lqty { get; set; }
        public decimal tqty { get; set; }
        public decimal acstock { get; set; }
        public decimal amount { get; set; }
        public decimal ramount { get; set; }
        public string rptdesc1 { get; set; }
        public string rptunit { get; set; }
        public decimal avgrate { get; set; }
        public double bgdqty { get; set; }
        public double diviation { get; set; }
        public double bgdbla { get; set; }


        public ErptStock() { }

    }

    public class MatStockIndPro
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string rsircode { get; set; }
        public string sirdesc { get; set; }
        public double opqty { get; set; }
        public double rcvqty { get; set; }
        public double issueqty { get; set; }
        public double trninqty { get; set; }
        public double trnoutqty { get; set; }
        public double lqty { get; set; }
        public double trcvqty { get; set; }
        public double actstockqty { get; set; }
        public double rate { get; set; }
        public double actstockamt { get; set; }
        public MatStockIndPro() { }
    }


    public class MatStockReportEvaluation
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string pactcode { get; set; }
        public string mrsircode { get; set; }
        public string rsircode { get; set; }
        public string pactdesc { get; set; }
        public string msirdesc { get; set; }
        public string rsirdesc { get; set; }
        public decimal opnamt { get; set; }
        public decimal rcvamt { get; set; }
        public decimal trninamt { get; set; }
        public decimal trnoutamt { get; set; }
        public decimal lsamt { get; set; }
        public decimal netrcvamt { get; set; }
        public decimal issueamt { get; set; }
        public decimal actstock { get; set; }
        public decimal percnt { get; set; }
        public MatStockReportEvaluation() { }
    }





    [Serializable]
    public class ProjStock
    {

        public string rptcod { get; set; }
        public string rptdesc1 { get; set; }
        public string spcfdesc { get; set; }
        public string rptunit { get; set; }
        public double stkqty { get; set; }

        public ProjStock()
        {


        }

    }

    [Serializable]
    public class EMaterialsStock
    {

        public DateTime vdate { get; set; }
        public string vnumber { get; set; }
        public string refno { get; set; }
        public double inqty { get; set; }
        public double outqty { get; set; }
        public double clqty { get; set; }

        public EMaterialsStock()
        {

        }

    }






    #region
    [Serializable]
    public class PurEqisition
    {
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public decimal rsirdesc1 { get; set; }
        public string spcfdesc { get; set; }
        public string rsirunit { get; set; }
        public decimal stkqty { get; set; }
        public decimal bbgdqty { get; set; }
        public decimal preqty { get; set; }
        public decimal expusedt { get; set; }
        public decimal areqty { get; set; }
        public string pursdate { get; set; }
        public decimal bgdrat { get; set; }
        public decimal lpurrate { get; set; }
        public decimal reqrat { get; set; }
        public decimal areqamt { get; set; }
        public string ssirdesc { get; set; }
        public decimal bgdqty { get; set; }
        public decimal treceived { get; set; }
        public decimal bbgdamt { get; set; }
        public decimal tbgdqty { get; set; }
        public decimal tbbgdqty { get; set; }
        public string reqnote { get; set; }
        public string ssircode { get; set; }
        public decimal pstkqty { get; set; }
        public decimal reqsrat { get; set; }
        public PurEqisition()
        {

        }

        #endregion

        #region
        [Serializable]
        public class PurMatReq
        {
            public string sircode { get; set; }
            public string resdesc { get; set; }
            public string sirunit { get; set; }
            public string spcfdesc { get; set; }
            public double qty { get; set; }


            public PurMatReq() { }

        }


        #endregion

        #region
        [Serializable]
        public class PurGetPass
        {
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double getpqty { get; set; }


            public PurGetPass() { }

        }


        #endregion


        #region
        [Serializable]
        public class RptMatTransReq
        {
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string resdesc { get; set; }
            public string spcfdesc { get; set; }
            public string sirunit { get; set; }
            public double mtrfqty { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }


            public RptMatTransReq() { }

        }

        [Serializable]
        public class RptMatTransReqcp
        {
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string resdesc { get; set; }
            public string spcfdesc { get; set; }
            public string getpref { get; set; }
            public string mtrref { get; set; }
            public string sirunit { get; set; }
            public double mtrfqty { get; set; }
            public double balqty { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }


            public RptMatTransReqcp() { }

        }


        #endregion

        #region
        [Serializable]
        public class RptPurMktSurvey
        {
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public string ssirdesc1 { get; set; }
            public double resrate { get; set; }
            public string msrrmrk { get; set; }
            public string cperson { get; set; }
            public string phone { get; set; }
            public double reqqty { get; set; }
            public string mobile { get; set; }
            public double delivery { get; set; }
            public double amount { get; set; }
            public double payment { get; set; }



            public RptPurMktSurvey() { }

        }


        #endregion


        #region
        [Serializable]
        public class RptPurMktSurveySummary
        {

            public string rsirdesc { get; set; }
            public double samount { get; set; }
            public RptPurMktSurveySummary() { }

        }


        #endregion

        #region
        [Serializable]
        public class RptMatWiseSupList
        {

            public string ssirdesc1 { get; set; }
            public string rmrks { get; set; }
            public RptMatWiseSupList() { }

        }


        #endregion


        #region
        [Serializable]
        public class RptSupWiseMatList
        {

            public string rsirdesc1 { get; set; }
            public string spcfdesc { get; set; }
            public string rmrks { get; set; }
            public RptSupWiseMatList() { }

        }


        #endregion
        #region

        [Serializable]
        public class InterComMaterial
        {

            public string vounum1 { get; set; }
            public DateTime voudat { get; set; }
            public string refno { get; set; }
            public string ftcomdesc { get; set; }
            public string ttcomdesc { get; set; }
            public string tfprjdesc { get; set; }
            public string ttprjdesc { get; set; }
            public string matdesc { get; set; }
            public double tqty { get; set; }
            public double rate { get; set; }

            public double tamount { get; set; }

            public InterComMaterial() { }
        }

        #endregion

    }

    [Serializable]
    public class RptMaterialPurchaseRequisition
    {

        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public string rsirdesc1 { get; set; }
        public string rsirdesc2 { get; set; }
        public string spcfdesc { get; set; }
        public string rsirunit { get; set; }
        public double bgdqty { get; set; }
        public double bgdrat { get; set; }
        public double treceived { get; set; }
        public double bbgdqty { get; set; }
        public double bbgdamt { get; set; }
        public double bbgdqty1 { get; set; }
        public double bbgdamt1 { get; set; }
        public double stkqty { get; set; }
        public double preqty { get; set; }
        public double areqty { get; set; }
        public double reqrat { get; set; }
        public double preqamt { get; set; }
        public double areqamt { get; set; }
        public double chqty { get; set; }
        public double pstkqty { get; set; }
        public string expusedt { get; set; }
        public string reqnote { get; set; }
        public string pursdate { get; set; }
        public double lpurrate { get; set; }
        public string storecode { get; set; }
        public string storedesc { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public string orderno { get; set; }
        public double tbgdqty { get; set; }
        public double tbbgdqty { get; set; }
        public double reqsrat { get; set; }
        public string frecid { get; set; }
        public string secrecid { get; set; }
        public string threcid { get; set; }


        public RptMaterialPurchaseRequisition() { }





    }

    [Serializable]
    public class RptMaterialStock
    {


        public string comcod { get; set; }
        public string bldcod { get; set; }
        public string rptcod { get; set; }

        public double opqty { get; set; }
        public double rcvqty { get; set; }
        public double trninqty { get; set; }
        public double trnoutqty { get; set; }
        public double transqty { get; set; }
        public double tqty { get; set; }
        public double lqty { get; set; }
        public double issueqty { get; set; }
        public double acstock { get; set; }
        public double varqty { get; set; }

        public string rptdesc1 { get; set; }
        public string rptunit { get; set; }
        public string actdesc { get; set; }

        public RptMaterialStock() { }

    }



    [Serializable]
    public class RptDayWisePurchase
    {
        public string comcod { get; set; }
        public string mrrno { get; set; }
        public string mrrref { get; set; }

        public string chalanno { get; set; }
        public string mrrno1 { get; set; }
        public DateTime mrrdat { get; set; }
        public string pactcode { get; set; }
        public string ssircode { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public double qty { get; set; }
        public double amt { get; set; }
        public double areqty { get; set; }

        public double balqty { get; set; }
        public double rate { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string orderref { get; set; }
        public DateTime orderdat { get; set; }
        public string reqno { get; set; }

        public string reqno1 { get; set; }
        public string billno { get; set; }
        public string billno1 { get; set; }
        public string mrfno { get; set; }
        public DateTime reqdat { get; set; }
        public string resdesc { get; set; }

        public string resunit { get; set; }
        public string supdesc { get; set; }
        public string pactdesc { get; set; }
        public string usrsname { get; set; }
        public string spcfdesc { get; set; }

        public RptDayWisePurchase() { }

    }


    [Serializable]
    public class RptPurVarA
    {


        public string comcod { get; set; }
        public string bldcod { get; set; }
        public string rptcod { get; set; }

        public double acqty { get; set; }
        public double rcvqty { get; set; }
        public double tqty { get; set; }
        public double stkqty { get; set; }
        public double rptqty { get; set; }
        public double bgdstkqty { get; set; }

        public double varqty { get; set; }

        public string rptdesc1 { get; set; }
        public string rptunit { get; set; }


        public RptPurVarA() { }

    }


    [Serializable]
    public class RptInResource
    {


        public string comcod { get; set; }
        public string flrdes { get; set; }
        public string rptdesc1 { get; set; }
        public double rptbgdqty { get; set; }
        public string rptunit { get; set; }

        public double uresqty { get; set; }
        public double rptqty { get; set; }
        public double rptrat { get; set; }
        public double rptamt { get; set; }
        public double peramt { get; set; }

        public RptInResource() { }


    }






}
