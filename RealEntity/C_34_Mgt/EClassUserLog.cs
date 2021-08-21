using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_34_Mgt
{

    [Serializable]
    public  class EClassUserLog
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string gennum { get; set; }
        public string number { get; set; }
        public double amt { get; set; }
        public double delamt { get; set; }
        public string valdat { get; set; }
        public string entryid { get; set; }
        public string entryuser { get; set; }
        public string entrydat { get; set; }
        public string postedtime { get; set; }
        public string postrmid { get; set; }
        public string editbyid { get; set; }
        public string edituser { get; set; }
        public string editdat { get; set; }
        public string delbyid { get; set; }
        public string deluser { get; set; }
        public string deldat { get; set; }
        public string deletedtime { get; set; }
        public string pactcode { get; set; }


        public EClassUserLog() { }


    }


    [Serializable]
    public class EClassUserLogSummary   
    {
      
        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string entryid { get; set; }
        public string entryuser { get; set; }
        public string tcount { get; set; }
       // public byte[] usrimg { get; set; }

        public EClassUserLogSummary() { }
    }


    [Serializable]
    public class EClassUserLogHRM  
    {
        public string comcod { get; set; }
        //public string grp { get; set; }
        //public string grpdesc { get; set; }
        //public string gennum { get; set; }
        public string number { get; set; }
        public double amt { get; set; }
        public double delamt { get; set; }
        public string valdat { get; set; }
        //public string entryid { get; set; }
        public string entryuser { get; set; }
        public string entrydat { get; set; }
        public string postedtime { get; set; }
        public string postrmid { get; set; }
        //public string editbyid { get; set; }
        public string edituser { get; set; }
        public string editdat { get; set; }
        //public string delbyid { get; set; }
        public string deluser { get; set; }
        public string deldat { get; set; }
        public string deletedtime { get; set; }
        //public string pactcode { get; set; }


        public EClassUserLogHRM() { }


    }




}
