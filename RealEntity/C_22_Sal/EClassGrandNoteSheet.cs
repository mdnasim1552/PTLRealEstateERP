using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_22_Sal
{
   public class EClassGrandNoteSheet
    {

        #region Grandnotesheet
        [Serializable]
        public class EClassBaseGrandNoteSheet
        {

            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }
           
            public double pv { get; set; }
            public double fv { get; set; }



            public EClassBaseGrandNoteSheet()
            {
            
            
            }

            public EClassBaseGrandNoteSheet(string monthid, string grp, string ymon, double pv, double fv)
            {
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;


            }

        }
        [Serializable]
        public class EClassCoffGrandNoteSheet
        {

            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }

            public double pv { get; set; }
            public double fv { get; set; }



            public EClassCoffGrandNoteSheet()
            {


            }

            public EClassCoffGrandNoteSheet(string monthid, string grp, string ymon, double pv, double fv)
            {
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;


            }

        }
        [Serializable]

        public class EClassRevGrandNoteSheet
        {

            public string monthid { get; set; }
            public string grp { get; set; }
            public string ymon { get; set; }

            public double pv { get; set; }
            public double fv { get; set; }



            public EClassRevGrandNoteSheet()
            {


            }

            public EClassRevGrandNoteSheet(string monthid, string grp, string ymon, double pv, double fv)
            {
                this.monthid = monthid;
                this.grp = grp;
                this.ymon = ymon;
                this.pv = pv;


            }

        }

        #endregion
        

    }
}
