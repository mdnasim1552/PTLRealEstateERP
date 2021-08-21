using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
  [Serializable]
    public class EClassModule
    {
        public string itemcod{get;set;}
        public string itemdesc{get;set;}
        public string itemurl{get;set;}
        public string itemtips{get;set;}
        public bool itemslct{get;set;}
        public string fbold{get;set;}
        public string formid { get; set; }

        public EClassModule() { 
        
        }

        public EClassModule(string itemcod, string itemdesc, string itemurl, bool  itemslct, string fbold) 
        {

            this.itemcod = itemcod;
            this.itemdesc = itemdesc;
            this.itemurl = itemurl;
            this.itemslct = itemslct;
            this.fbold = fbold;
        }
        public EClassModule(string itemcod, string itemdesc, string itemurl, bool itemslct, string fbold, string formid)
        {

            this.itemcod = itemcod;
            this.itemdesc = itemdesc;
            this.itemurl = itemurl;
            this.itemslct = itemslct;
            this.fbold = fbold;
            this.formid = formid;
        }



    }


    [Serializable]
    public class EClassCompInf
    {
        public string comcod { get; set; }
        public string comnam { get; set; }

        public double ttlstap { get; set; }

        public double present { get; set; }

        public double late { get; set; }

        public double earlyLev { get; set; }

        public double onlev { get; set; }

        public double absnt { get; set; }
        public EClassCompInf()
        {

        }
        public EClassCompInf(string comcod, string comnam, double ttlstap, double present, double late, double earlyLev, double onlev, double absnt)
        {
            this.comcod = comcod;
            this.comnam = comnam;
            this.ttlstap = ttlstap;
            this.present = present;
            this.late = late;
            this.earlyLev = earlyLev;
            this.onlev = onlev;
            this.absnt = absnt;
        }



    }


    [Serializable]
    public class EClassComModule
    {
        public string moduleid { get; set; }
        public string modulenam { get; set; }
        public string url { get; set; }


        public EClassComModule()
        {

        }

        public EClassComModule(string moduleid, string modulenam, string url)
        {

            this.moduleid = moduleid;
            this.modulenam = modulenam;
            this.url = url;

        }



    }

    [Serializable]
    public class EclassShortCut
    {
        public string formid { get; set; }

    }
    [Serializable]
    public class EclassUserPermissionPages
    {
        public string rowid { get; set; }
        public string comcod { get; set; }
        public string usrid { get; set; }
        public string frmid { get; set; }
        public string frmname { get; set; }
        public string qrytype { get; set; }
        public string entry { get; set; }
        public string printable { get; set; }
        public string deleteCk { get; set; }
        public string dscrption { get; set; }
        public string urlinf { get; set; }
        public string floc { get; set; }
        public string qrytype1 { get; set; }

    }


}
