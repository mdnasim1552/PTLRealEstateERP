using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RealEntity.C_32_Mis
{
    public class EClassAcc_03
    {
        [Serializable]
        public class Customer_ProjectSumm
        {
            //Iqbal Nayan
            public string pactdesc { set; get; }
            public double bgdamt { set; get; }
            public double toacamt { set; get; }
            public double perontw { set; get; }
            public double peronac { set; get; }
            public double collamt { set; get; }
            public double collbgd { set; get; }
            public double salamt { set; get; }
            public double tosalval { set; get; }
            public double cactamt { set; get; }
            public double examt { set; get; }
            public double mplanat { set; get; }
            public double cbgdamt { set; get; }
            public string pactcode { set; get; }
            public string comcod { set; get; }
            public double adamt { set; get; }
            public double tbgdamt { set; get; }
            public double princur { set; get; }
            public double fuincur { set; get; }
            public double peronbgd { set; get; }
            public double peronsal { set; get; }
            public double peroncoll { set; get; }
            public double texwprincur { set; get; }
            public Customer_ProjectSumm() { }

            public Customer_ProjectSumm( string pactdesc, double bgdamt, double toacamt, double perontw, double peronac, double collamt, double collbgd, double salamt,
                double tosalval, double cactamt, double examt, double mplanat, double cbgdamt, string pactcode, string comcod, double adamt, double tbgdamt, double princur, double fuincur, double peronbgd, double peronsal, double peroncoll, double texwprincur)
            {

                this.pactdesc = pactdesc;
                this.bgdamt = bgdamt;
                this.toacamt = toacamt;
                this.perontw = perontw;
                this.peronac = peronac;
                this.collamt = collamt;
                this.collbgd = collbgd;
                this.salamt = salamt;
                this.tosalval = tosalval;
                this.cactamt = cactamt;
                this.examt = examt;
                this.mplanat = mplanat;
                this.cbgdamt = cbgdamt;
                this.pactcode = pactcode;
                this.comcod = comcod;
                this.adamt = adamt;
                this.tbgdamt = tbgdamt;
                this.princur = princur;
                this.fuincur = fuincur;
                this.peronbgd = peronbgd;
                this.peronsal = peronsal;
                this.peroncoll = peroncoll;
                this.texwprincur = texwprincur;
            }

        }
        [Serializable]
        public class ProjectWisRes
        {
         // Iqbal Nayan
            public string comcod { get; set; }
            public string catmcode { get; set; }
            public string catcode { get; set; }
            public string pactcode { get; set; }
            public double iopnam { get; set; }
            public double iclsam { get; set; }
            public double iaddam { get; set; }
            public double iadjam { get; set; }
            public double lopnam { get; set; }
            public double laddam { get; set; }
            public double ladjam { get; set; }
            public double lclsam { get; set; }
            public double mopnam { get; set; }
            public double maddam { get; set; }
            public double madjam { get; set; }
            public double mclsam { get; set; }
            public double sopnam { get; set; }
            public double saddam { get; set; }
            public double sadjam { get; set; }
            public double sclsam { get; set; }
            public double topnam { get; set; }
            public double taddam { get; set; }
            public double tadjam { get; set; }
            public double tclsam { get; set; }
            public string catdesc { get; set; }
            public string catmdesc { get; set; }
            public string pactdesc { get; set; }
            public ProjectWisRes() { }
        }    

        [Serializable]
        public class TrailsBal3
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double cramt { get; set; }
            public double clsam { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public TrailsBal3() { }

        }
        [Serializable]
        public class EclassBalSheetSum
        {
            //Iqbal Nayan
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public double noncuram { get; set; }
            public double curam { get; set; }
            public double equityam { get; set; }
            public double noncurlia { get; set; }
            public double curlia { get; set; }
            public double toasset { get; set; }
            public double tolib { get; set; }
            public EclassBalSheetSum() { }
        }


        [Serializable]
        public class PrjAnalysis
        {
            //Iqbal Nayan
            public double tosalval { get; set; }
            public double salamt { get; set; }
            public double tsaldue { get; set; }
            public double collamt { get; set; }
            public double tcoldue { get; set; }
            public double tsalcoldue { get; set; }
            public PrjAnalysis ( ) { }

            public PrjAnalysis ( double _tsalval, double _salamt, double _tsaldue, double _collamt, double _tcoldue, double _tsalcoldue )
            {
                this.tosalval = _tsalval;
                this.salamt = _salamt;
                this.tsaldue = _tsaldue;
                this.collamt = _collamt;
                this.tcoldue = _tcoldue;
                this.tsalcoldue = _tsalcoldue;
            }

        }


        [Serializable]
        public class PrjAnalysis1
        {
            //Iqbal Nayan
            public double tosalval { get; set; }
            public double salamt { get; set; }
            public double tsaldue { get; set; }
            public double collamt { get; set; }
            public double tcoldue { get; set; }
            public double tsalcoldue { get; set; }

            public PrjAnalysis1 ( ) { }

            public PrjAnalysis1 ( double _tsalval, double _salamt, double _tsaldue, double _collamt, double _tcoldue, double _tsalcoldue )
            {
                this.tosalval = _tsalval;
                this.salamt = _salamt;
                this.tsaldue = _tsaldue;
                this.collamt = _collamt;
                this.tcoldue = _tcoldue;
                this.tsalcoldue = _tsalcoldue;
            }

        }
             [Serializable]

        public class EClassInflation
        {
        
            //Iqbal nayan

             public double bgdam{get;set;}
             public double puram { get; set; }
             public double rbgdam { get; set; }
             public double reqbgd { get; set; }
             public double costincur { get; set; }
             public double costsave { get; set; }
             public double fuinfla { get; set; }
             public double fusave { get; set; }
             public double infla { get; set; }


             public EClassInflation()
             {
             }

             public EClassInflation(double bgdam,double puram,double rbgdam,double reqbgd,double costincur,double costsave,double fuinfla,double fusave,double infla)
        
             {
             this.bgdam=bgdam;
             this.puram=puram;
             this.rbgdam=rbgdam;
             this.reqbgd=reqbgd;
             this.costincur=costincur;
             this.costsave=costsave;
             this.fuinfla=fuinfla;
             this.fusave=fusave;
             this.infla=infla;
            
             
             
             }
        }

        [Serializable]
        public class RptBalSheet
        {
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public double amt { get; set; }
        }

        [Serializable]
        public class RptBgdvsActual
        {
            public string rptdesc { get; set; }
            public string storied { get; set; }
            public string lsize { get; set; }
            public double bgdamt { get; set; }
            public double bgdamtpsft { get; set; }
            public double mat { get; set; }
            public double amt { get; set; }
            public double acamt { get; set; }
            public double labor { get; set; }
            public double others { get; set; }
            public double reamt { get; set; }
            public string hdate { get; set; }
            public RptBgdvsActual ( ) { }
        }
       
       
        [Serializable]
        public class ProjectTrlBal
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string rescode { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double trnam { get; set; }
            public double cramt { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public ProjectTrlBal() { }
          
        }


        [Serializable]
        public class ProjectTrlBalDaywise
        {
            //Tarikul 
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double opdram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public double closam { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public ProjectTrlBalDaywise() { }

        }

      
        [Serializable]
        public class ProjectCost
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double cramt { get; set; }
            public double clsam { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public ProjectCost() { }
        }
        [Serializable]
        public class Trialbal02
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public Trialbal02() { }
        }
  

        [Serializable]
        public class CollectionBrackDown
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string pactdesc1 { get; set; }
            public double tsalamt { get; set; }
            public double tclramt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double tuclramt { get; set; }
            public double tmat { get; set; }
            public double noiamt { get; set; }
            public double stdamt { get; set; }
            public double cuamt { get; set; }
            public double gtotal { get; set; }
        }

        [Serializable]
        public class RatioAnalysis
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rdesc { get; set; }
            public double rstd { get; set; }
            public double ratio { get; set; }
        }

        [Serializable]
        public class ResPayt
        {
            public string grp { get; set; }
            public string sgrp { get; set; }
            public string comcod { get; set; }
            public string aarescode { get; set; }
            public double bgdam { get; set; }
            public double pream { get; set; }
            public double curam { get; set; }
            public string grpdesc { get; set; }
            public string sgrpdesc { get; set; }
            public double toam { get; set; }
            public string aaresdesc { get; set; }
            public ResPayt() { }
        }

        [Serializable]
        public class EClassMonthWiseProjectPayment
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public double opnamt { get; set; }
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

            public EClassMonthWiseProjectPayment() { }


        }
        [Serializable]
        public class EClassRealInOutFlow
        {
            public string comcod { get; set; }
            public string flow { get; set; }
            public string flowdesc { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
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


            public EClassRealInOutFlow() { }


        }

        [Serializable]
        public class GpNpCalc
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public double advsamt { get; set; }
            public double samt { get; set; }
            public double constamt { get; set; }
            public double landamt { get; set; }
            public double prodamt { get; set; }
            public double gpamt { get; set; }
            public double gpcostper { get; set; }
            public double gpper { get; set; }
            public double prjovamt { get; set; }
            public double hovamt { get; set; }
            public double tbankinamt { get; set; }
            public double tothamt { get; set; }
            public double tcostamt { get; set; }

            public double npbnoi { get; set; }
            public double npcostper { get; set; }
            public double npper { get; set; }
            public double noiamt { get; set; }
            public double npnoiper { get; set; }


            public GpNpCalc() { }


        }

        [Serializable]
        public class EClassMasterBgd
        {
            public string comcod { get; set; }
            public string rowid { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string catcode { get; set; }
            public string catmcode { get; set; }
            public string actcode { get; set; }
            public double tosalval { get; set; }
            public double salamt { get; set; }
            public double collamt { get; set; }
            public double perontosal { get; set; }
            public double perontocol { get; set; }
            public double cbgdamt { get; set; }
            public double bgdamt { get; set; }
            public double examt { get; set; }
            public double peronpgres { get; set; }
            public double ipamt { get; set; }
            public double rsalamt { get; set; }
            public double usoamt { get; set; }
            public double rbgdamt { get; set; }
            public double fsamt { get; set; }
            public double fsuamt { get; set; }
            public double invreqamt { get; set; }
            public double invblkamt { get; set; }
            public double intamt { get; set; }
            public double stinvamt { get; set; }
            public double libamt { get; set; }
            public double noiamt { get; set; }
            public double ltoamt { get; set; }
            public double lfrmamt { get; set; }
            public double tsalable { get; set; }
            public double tsold { get; set; }
            public double unsold { get; set; }
            public double conarea { get; set; }
            public double bgdsaamt { get; set; }
            public double np { get; set; }
            public string prjloc { get; set; }
            public double lanarea { get; set; }
            public string natland { get; set; }
            public double conprogress { get; set; }
            public string comdate { get; set; }
            public string catagory { get; set; }
            public double mremain { get; set; }
            public string actdesc { get; set; }
            public double acbgdamt { get; set; }
            public string catdesc { get; set; }
            public string catmdesc { get; set; }
            public string location { get; set; }
            public double stdpercnt { get; set; }
            public double bgdmar { get; set; }
            public double actmar { get; set; }
            public DateTime cdate { get; set; }
            public double eamt { get; set; }
            public double peroncpro { get; set; }

            public EClassMasterBgd() { }

        }

         [Serializable]
        public class RptProjectReport02
        {

            public string comcod { get; set; }         
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string rescode { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double trnam { get; set; }
            public double cramt { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public string sirunit { get; set; }

            public RptProjectReport02() { }



        }


         [Serializable]
         public class RptProjectRemainingCost
         {
             public string rowid { get; set; }
             public string comcod { get; set; }
          
             public string actcode { get; set; }
             public string actcode1 { get; set; }
             public string rescode1 { get; set; }
             public string rescode { get; set; }
             public double bgdqty { get; set; }
             public double bgdrat { get; set; }
             public double bgdam { get; set; }
             public double trnqty { get; set; }

             public double rerate { get; set; }
             public double remrat { get; set; }
             public double balqty { get; set; }
             public double reamt { get; set; }
             public double acrat { get; set; }
             public double maxrat { get; set; }
             public double incrate { get; set; }
             public double balamt { get; set; }
             public double nrate { get; set; }
             public double princ { get; set; }
             public double pridesc { get; set; }
             public double tbgdreq { get; set; }

             public string resunit { get; set; }
             public string resdesc { get; set; }
             public double fuinfla { get; set; }
             public double fusave { get; set; }
             public double infla { get; set; }
             public double bgdbalqty { get; set; }   

             public RptProjectRemainingCost() { }


         }

         [Serializable]
         public class RptCostOfSalesPerSft
         {
            
             public string comcod { get; set; }
             public string actcode { get; set; }
             public string grp { get; set; }
             public string grpdesc { get; set; }
             public string rescode { get; set; }            
             public double bgdam { get; set; }
             public double trnam { get; set; }
             public double bcncst { get; set; }
             public double bslcst { get; set; }
             public double cncst { get; set; }
             public double cnslcst { get; set; }
             public string resdesc { get; set; }
             public string resunit { get; set; }
             public RptCostOfSalesPerSft() { }


         }

         [Serializable]
         public class RptBankInterestAllocation
         {

             public string comcod { get; set; }
             public string actcode { get; set; }
             public string grp { get; set; }
             public string grpdesc { get; set; }       
             public double collamt { get; set; }
             public double examt { get; set; }
             public double diff { get; set; }
             public double costfund { get; set; }          
             public string actdesc { get; set; }
             public RptBankInterestAllocation() { }


         }

        [Serializable]
        public class ProjCancellationUnit{
           
            public string  comcod{ get; set; }
            public string actcode { get; set; }
            public string prjname { get; set; }
            public string rescode { get; set; }
            public string cusname { get; set; }
            public double payable { get; set; }
            public double payment { get; set; }
            public double netpay { get; set; }

            public ProjCancellationUnit()
            {

            }

        }

        [Serializable]

        public class CashFlowBankWise
        {
            public string flow { get; set; }
            public string flowdesc { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public double amt1  { get; set; }
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
            public double amt13 { get; set; }
            public double amt14 { get; set; }
            public double amt15 { get; set; }
            public double amt16 { get; set; }
            public double amt17 { get; set; }
            public double amt18 { get; set; }
            public double amt19 { get; set; }
            public double amt20 { get; set; }
            public double amt21 { get; set; }
            public double amt22 { get; set; }
            public double amt23 { get; set; }
            public double amt24 { get; set; }
            public double amt25 { get; set; }
          
            public double toamt { get; set; }

        }

        [Serializable]
        public class MMonPlnVsAchAllPro
        {
            public string grp { get; set; }
            public string actcode { get; set; }
            public double bdgamt { get; set; }
            public double masplan { get; set; }
            public double monplan { get; set; }
            public double excution { get; set; }
            public double acmasplan { get; set; }
            public double acmonplan { get; set; }
            public string grpdesc { get; set; }
            public string actdesc { get; set; }
        }

        [Serializable]
        public class CatWiseConProgressAllPro
        {
            public string pactcode { get; set; }
            public double bgdamt { get; set; }
            public double mplan { get; set; }
            public double mplanat { get; set; }
            public double eamt { get; set; }
            public double leamt { get; set; }
            public double perontw { get; set; }
            public double peronac { get; set; }
            public double peronlp { get; set; }
            public string pactdesc { get; set; }
           
        }
       

        [Serializable]
        public class CatWiseConProgress
        {
            public string bldcod { get; set; }
            public string flrcod { get; set; }
             public string flrdes { get; set; }
            public double bgdamt { get; set; }
            public double perwork { get; set; }
            public double mplan { get; set; }
            public double mplanat { get; set; }
            public double eamt { get; set; }
            public double leamt { get; set; }
            public double prcent { get; set; }
            public double bgdprcent { get; set; }
        }

        [Serializable]
        public class IndiProjCost12Month
        {
            public string grp { get; set; }
            public string rptcode { get; set; }
            public double amtup { get; set; }
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
            public double amt13 { get; set; }
            public double amt14 { get; set; }
            public double amt15 { get; set; }
            public double toamt { get; set; }
            public string grpdesc { get; set; }
            public string rptdesc { get; set; }
        }

        [Serializable]
        public class AnyCostAllProject
        {
            public string rescode { get; set; }
            public double topamt { get; set; }
            public double p1 { get; set; }
            public double p2 { get; set; }
            public double p3 { get; set; }
            public double p4 { get; set; }
            public double p5 { get; set; }
            public double p6 { get; set; }
            public double p7 { get; set; }
            public double p8 { get; set; }
            public double p9 { get; set; }
            public double p10 { get; set; }
            public double p11 { get; set; }
            public double p12 { get; set; }
            public string resdesc { get; set; }
        }


        [Serializable]
        public class ProjectStatus
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode1 { get; set; }
            public string pactdesc1 { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string prjloc { get; set; }
            public double lanarea { get; set; }
            public string natland { get; set; }
            public double tsalamt { get; set; }
            public double msalamt { get; set; }
            public double trecamt { get; set; }
            public double noiamt { get; set; }
            public double recamt { get; set; }
            public double balsalrec { get; set; }
            public double texpamt { get; set; }
            public double tpadvamt { get; set; }
            public double tlcamt { get; set; }
            public double tovamt { get; set; }
            public double tbankinamt { get; set; }
            public double tactamt { get; set; }
            public double tliamt { get; set; }
            public double lfrmhoff { get; set; }
            public double ltohoff { get; set; }
            public double treloanamt { get; set; }

        }


        [Serializable]
        public class BudgetVsExpensesAllProj
        {
            public string rptcode { get; set; }
            public double bgdamt { get; set; }
            public double acamt { get; set; }
            public double reamt { get; set; }
            public double percnt { get; set; }
            public double propercnt { get; set; }
            public string rptdesc { get; set; }
            
        }


        [Serializable]
        public class ProjectIncomeSt
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode1 { get; set; }
            public string mrescode { get; set; }
            public string rescode { get; set; }
            public double amt { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public double parcent { get; set; }

        }


        [Serializable]
        public class ProjectSummary
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string gunit { get; set; }
            public string gval { get; set; }
            public string gdesc { get; set; }
            public double bgdamt { get; set; }
            public double in_actamt { get; set; }
            public double accramt { get; set; }
            public double sft { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double re_actamt { get; set; }
            public double mplan_amt { get; set; }
            public double cost_bdgamt { get; set; }
            public double c_actamt { get; set; }
            public double colamt { get; set; }
            public double invamt { get; set; }
            public double net_invamt { get; set; }
            public double percentage { get; set; }

        }

        [Serializable]
        public class MontWiseProjectStatus
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double r1 { get; set; }
            public double r2 { get; set; }
            public double r3 { get; set; }
            public double r4 { get; set; }
            public double r5 { get; set; }
            public double r6 { get; set; }
            public double r7 { get; set; }
            public double r8 { get; set; }
            public double r9 { get; set; }
            public double r10 { get; set; }
            public double r11 { get; set; }
            public double r12 { get; set; }
            public double r13 { get; set; }
            public double r14 { get; set; }
            public double toramt { get; set; }
            public double tocollamt { get; set; }
            public double netamt { get; set; }
           
        }

        [Serializable]
        public class RptProjectAnalysis
        {
            public string pactcode { get; set; }
            public string actdesc { get; set; }
            public string prjgrp { get; set; }
            public string grpdesc { get; set; }
            public double salstg { get; set; }
            public double tosalval { get; set; }
            public double salamt { get; set; }
            public double tsaldue { get; set; }
            public double collamt { get; set; }
            public double tcoldue { get; set; }
            public double tsalcoldue { get; set; }
            public double salperstg { get; set; }
            public double colpersal { get; set; }
            public double colperstg { get; set; }
            public double bgdamt { get; set; }
            public double conarea { get; set; }
            public double acbgdamt { get; set; }
            public double cbgdamt { get; set; }
            public double bgdprofit { get; set; }
            public double colst { get; set; }

            public RptProjectAnalysis() { }

        }
    }
}
