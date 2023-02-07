using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_22_Sal
{
    public class EClassSales
    {
        [Serializable]
        public class SalesInventory

        {

            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }           
            public double soldqty { get; set; }
            public double mgtbook { get; set; }
            public double usoldqty { get; set; }
            public string grp1 { get; set; }
            public string prodesc { get; set; }
            public string loc { get; set; }
            public double uqty { get; set; }

            public SalesInventory()
            {
            }

        }


        [Serializable]
        public class SalesInterest
        {

            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rowid { get; set; }
            public string inscod { get; set; }
            public string insdes { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public DateTime cinsdat { get; set; }
            public DateTime trdat { get; set; }
            public DateTime paydate { get; set; }
            public DateTime recndat { get; set; }
            public string day { get; set; }
            public double cinsam { get; set; }
            public double pamount { get; set; }
            public double cumdue { get; set; }
            public double cumpaid { get; set; }
            public double cumbalance { get; set; }
            public double interest { get; set; }
            public double cuminterest { get; set; }
            public double dueamt { get; set; }
            public double discharge { get; set; }
            public double delcharge { get; set; }
            public double benifit { get; set; }

            public SalesInterest()
            {
            }

        }

        [Serializable]

        public class SoldUnsoftInfGroupWise
        {
            public string comcod { get; set; }
            public string flrdesc { get; set; }
            public string pactcode { get; set; }
            public double tqty { get; set; }
            public double tusize { get; set; }
            public double sqty { get; set; }
            public double susize { get; set; }
            public double usqty { get; set; }
            public double usize { get; set; }
            public double usuamt { get; set; }
            public double suamt { get; set; }
            public double parking { get; set; }
            public double utility { get; set; }
            public double cooperative { get; set; }
            public double tsalamt { get; set; }
            public double tramt { get; set; }
            public double balance { get; set; }

            public SoldUnsoftInfGroupWise()
            {

            }


        }
        [Serializable]

        public class SoldUnsoltInfavg
        {
            public string comcod { get; set; }
            public string flrdesc { get; set; }
            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public double tqty { get; set; }
            public double tusize { get; set; }
            public double sqty { get; set; }
            public double susize { get; set; }
            public double usqty { get; set; }
            public double usize { get; set; }
            public double usuamt { get; set; }
            public double tramt { get; set; }
            public double suamt { get; set; }
            public double parking { get; set; }
            public double recievable { get; set; }
            public double expcarutilyrate { get; set; }
            public double cooperative { get; set; }
            public double ctwcarutility { get; set; }
            public double Inccarutilyrate { get; set; }
            public double expuscarutilityrate { get; set; }
            public double incunsoldcarutility { get; set; }
            public double incunsoldavgrate { get; set; }

            public SoldUnsoltInfavg()
            {

            }


        }
        [Serializable]

        public class SalesvsAchievement
        {
            public string comcod { get; set; }
            public string flrdesc { get; set; }
            public string pactdesc { get; set; }
            public int rowsl { get; set; }
            public int rowid { get; set; }
            public string pactcode { get; set; }
            public string typecode { get; set; }
            public string typedesc { get; set; }
            public string udesc { get; set; }
            public double usize { get; set; }
            public string custname { get; set; }
            public double srate { get; set; }
            public string steamdesc { get; set; }
            public string downstatus { get; set; }
            public double tsalval { get; set; }
            public double loamt { get; set; }
            public double tramt { get; set; }
            public double netvalue { get; set; }
            public double downpamt { get; set; }
            public double ramt { get; set; }
            public DateTime saldate { get; set; }
            public DateTime schdate { get; set; } 



            public SalesvsAchievement()
            {

            }


        }
        [Serializable]

        public class SalesSurveyEntry
        {
            public string comcod { get; set; }
            public string flrdesc { get; set; }
            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public string location { get; set; }
            public string aptsize { get; set; }
            public string storied { get; set; }
            public double landarea { get; set; }
            public double aptunit { get; set; }
            public double askingprice { get; set; }
            public double selprice { get; set; }
            public double utlityprice { get; set; }
            public double prkingpirce { get; set; }
            public DateTime hoverdate { get; set; }
            public string procatagory { get; set; }
            public string buildtype { get; set; }
            public string companyname { get; set; }
            public string comments { get; set; }



            public SalesSurveyEntry()
            {

            }


        }
        [Serializable]
        public class TransactionSt
        {
            public string comcod { get; set; }
            
            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public string orgmrno { get; set; }
            public DateTime orgdate { get; set; }
            public string orgchqno { get; set; }
            public double orgamt { get; set; }
            public string mrno { get; set; }
            public DateTime chqdate { get; set; }
            public string chqno { get; set; }
            public double chqamt { get; set; }
          
            public TransactionSt()
            {

            }


        }
        [Serializable]
        public class SalesOpening
        {
            public string comcod { get; set; }

            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public string usircode { get; set; }
            public double schamt { get; set; }
            public string munit { get; set; }
            public double usize { get; set; }
            public double opnamt { get; set; }
           

            public SalesOpening()
            {

            }


        }
        [Serializable]
        public class CollectionStatement
        {
            public string comcod { get; set; }

            
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string usircode { get; set; }
           
            public string udesc { get; set; }
            public string custname { get; set; }
            public double opnam { get; set; }
            public double curinsam { get; set; }
            public double curbkam { get; set; }
            public double totalam { get; set; }
         


            public CollectionStatement()
            {

            }


        }
        [Serializable]
        public class PaymentStatusReconcile
        {
            public string comcod { get; set; }


            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public DateTime recdate { get; set; }
            public DateTime recndate { get; set; }
            public DateTime mrdate { get; set; }
            public string refno { get; set; }
            public string bankname { get; set; }
            public double paidamt { get; set; }
            public double loamt { get; set; }
          



            public PaymentStatusReconcile()
            {

            }


        }
        [Serializable]
        public class PaymentStatusRevenue
        {
            public string comcod { get; set; }


            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double uamt { get; set; }

            public PaymentStatusRevenue()
            {

            }
        }
        [Serializable]
        public class PrjWiseCollection
        {
            public string comcod { get; set; }


            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string aptdesc { get; set; }
           
            public double ramt { get; set; }

            public PrjWiseCollection()
            {

            }
        }
        [Serializable]
        public class PrjWiseCollectiontilldate
        {
            public string comcod { get; set; }


            public string pactcode { get; set; }
            public string musircode { get; set; }
            public string pactdesc { get; set; }
            public string aptdesc { get; set; }

            public double ramt { get; set; }

            public PrjWiseCollectiontilldate()
            {

            }
        }

        [Serializable]
        public class soldunsoldstatus
        {
            public string comcod { get; set; }

            public string pactdesc { get; set; }
            public string catdesc { get; set; }

            public double tunit { get; set; }
            public double tsize { get; set; }
            public double soldunit { get; set; }
            public double soldsize { get; set; }
            public double unsoldunit { get; set; }
            public double unsoldsize { get; set; }

            public soldunsoldstatus()
            {

            }
        }

    }
}
