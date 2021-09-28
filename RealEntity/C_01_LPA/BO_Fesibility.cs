using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_01_LPA
{
    public class BO_Fesibility
    {
        [Serializable]
        public class LandFesibility
        {
            public string comcod { get; set; }
            public string gph { get; set; }
            public string mprgcod { get; set; }
            public string prgcod { get; set; }
            public string prgval { get; set; }
            public string pactcode { get; set; }
            public string prgdesc { get; set; }
            public string prgdesc1 { get; set; }
            public LandFesibility() { }
        }

        [Serializable]
        public class Landdatabank01
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }
            //public double salamt { get; set; }
            //public double pcostamt { get; set; }
            //public double opcostamt { get; set; }
            //public double addcostamt { get; set; }
            //public double tocost { get; set; }
            //public double rfundamt { get; set; }
            //public double gp { get; set; }
            //public double np { get; set; }
            //public double gpper { get; set; }
            //public double npper { get; set; }
            //public DateTime cdate { get; set; }
            public string laddress { get; set; }
            //public string lsize { get; set; }
            //public string storied { get; set; }
            //public string duration { get; set; }
            //public string devshare { get; set; }
            public string pactdesc { get; set; }
            //public string link { get; set; }
            //public double lwnamt { get; set; }
            //public double mgtamt { get; set; }
            //public string lwnname { get; set; }
            //public double lwnmobile { get; set; }
            //public string prjdone { get; set; }
            public string location { get; set; }
            public string category { get; set; }
            public double totallanare { get; set; }
            public Landdatabank01() { }
        }
        [Serializable]
        public class testscroll
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double totamt { get; set; }
            public double amt01 { get; set; }
            public double amt02 { get; set; }
            public double amt03 { get; set; }
            public testscroll() { }
        }
        [Serializable]
        public class LandNotification
        {


            public string lid { get; set; }
            public string sircode {get;set;}
            public string ldetails { get; set; }
            public DateTime generated { get; set; }
            public string generated1 { get; set; }
            public string dealname { get; set; }
            public string ownname { get; set; }

            public string prio { get; set; }
            public string assoc { get; set; }
            public string lfollowup { get; set; }
            public string lstatus { get; set; }
            public string cphone { get; set; }
            public int followdday { get; set; }





            public LandNotification()
            {


            }

        }
        [Serializable]
        public class CrmNotifications
        {


             public string pid { get; set; }
            public string proscod {get;set;}       
         
          
            public DateTime cdate {get;set;}
            public string cdate1 {get;set;}
          
            public string discus { get; set; }
            public string lstatus { get; set; }
            public string nfollowup { get; set; }
            public DateTime napnt { get; set; }

            public string napnt1 { get; set; }
            public string assoc { get; set; }
            public string lfollowup { get; set; }          
            public string ndissub { get; set; }
            public int disgnote { get; set; }
            public int subgnote { get; set; }
         




           




         


           

            public CrmNotifications()
            {

            }


            //  comcod	sircode	 usercode	appbydat	empid	desig		LeadSrcCod		active	prefcod	prefdesc	teamcod		LeadTcod	LeadType	LeadScod	LeadStatus	lstatus	countrycode	discode	zonecode	pocode	blockcode	areacode		gcod	apttyp	aptsize	pactcode	ldiscuss		fllupby	pactdesc	note	pactdesc

        }


       [Serializable]
        public class EclassPreLandownerDiscuss
       {
       
       public string grp { get; set; }
       public string gpdesc { get; set; }
       public string gcod { get; set; }
       public string gdesc { get; set; }
       public string gph { get; set; }
       public string gval { get; set; }
       public string gtime { get; set; }
       public string gdesc1 { get; set; }

       public EclassPreLandownerDiscuss()
       { 
       
       
       
       }

       public EclassPreLandownerDiscuss(string grp, string gpdesc, string gcod, string gdesc, string gph, string gval, string gtime, string gdesc1)
       {
           this.grp = grp;
           this.gpdesc = gpdesc;
           this.gcod = gcod;
           this.gdesc = gdesc;
           this.gph = gph;
           this.gval = gval;
           this.gtime = gtime;
           this.gdesc1 = gdesc1;


       }

       
       
       
       }



        [Serializable]
        public class EClassZDTM

        {
            public string gcod { get; set; }
            public string gdesc { get; set; }


            public EClassZDTM()
            {


            }





        }



    



        [Serializable]
        public class EClassLandInfo

        {
            public string gcod { get; set; }
            public string lsircode { get; set; }
            public string ocsdhagno { get; set; }
            public string csdhagno { get; set; }
            public string osadhagno { get; set; }
            public string sadhagno { get; set; }
            public string rsdhagno { get; set; }
            public string bsdhagno { get; set; }
            public string bskhotianno { get; set; }
            public double cslarea { get; set; }
            public double bslarea { get; set; }
            public double bsklarea { get; set; }
            public string jblrefno { get; set; }
            public double budarea { get; set; }
            public double purarea { get; set; }
            public double restlarea { get; set; }


            public EClassLandInfo()
            {


            }

            public EClassLandInfo(string gcod, string lsircode , string ocsdhagno, string csdhagno, string osadhagno, string sadhagno, string rsdhagno, string bsdhagno, string bskhotianno,  double cslarea, double bslarea, double bsklarea, string jblrefno, double budarea, double purarea, double restlarea)
            {
                this.gcod =gcod;
                this.lsircode =lsircode;
                this.ocsdhagno =ocsdhagno;
                this.csdhagno = csdhagno;
                this.osadhagno = osadhagno;
                this.sadhagno =sadhagno;                
                this.rsdhagno = rsdhagno;
                this.bsdhagno = bsdhagno;
                this.bskhotianno = bskhotianno;
                this.cslarea = cslarea;
                this.bslarea = bslarea;
                this.jblrefno = jblrefno;
                this.budarea = budarea;
                this.purarea = purarea;
                this.restlarea = restlarea;

            }

        


        }




     


    }
}
