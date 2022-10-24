using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_22_Sal
{
   public class EClassGrandNoteSheet
    {


        [Serializable]
        public class EClassBaseGrandNoteSheet
        {
            public string bid { get; set; }
            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }

            public double pv { get; set; }
            public double fv { get; set; }



            public EClassBaseGrandNoteSheet()
            {


            }

            public EClassBaseGrandNoteSheet(string bid, string monthid, string grp, string ymon, double pv, double fv)
            {
                this.bid = bid;
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;
                this.fv = fv;


            }

        }
        [Serializable]
        public class EClassCoffGrandNoteSheet
        {
            public string cid { get; set; }
            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }

            public double pv { get; set; }
            public double fv { get; set; }



            public EClassCoffGrandNoteSheet()
            {


            }

            public EClassCoffGrandNoteSheet(string cid, string monthid, string grp, string ymon, double pv, double fv)
            {
                this.cid = cid;
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;
                this.fv = fv;


            }

        }
        [Serializable]

        public class EClassRevGrandNoteSheet
        {
            public string rid { get; set; }
            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }

            public double pv { get; set; }
            public double fv { get; set; }



            public EClassRevGrandNoteSheet()
            {


            }

            public EClassRevGrandNoteSheet(string rid, string monthid, string grp, string ymon, double pv, double fv)
            {
                this.rid = rid;
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;
                this.fv = fv;


            }

        }



    }
}
