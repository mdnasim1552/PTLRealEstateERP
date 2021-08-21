using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_21_Mkt
{
    public class EClassAdvertisement
    {


        [Serializable]

        public class EPaper
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }

            public EPaper(string _gcod, string _gdesc)
            {
                this.gcod = _gcod;
                this.gdesc = _gdesc;
            }
        }

        [Serializable]
        public class EPaperadDes
        {

            public string papcod { get; set; }
            public string descode { get; set; }
            public string papdesc { get; set; }
            public string addesc { get; set; }
            public double amount { get; set; }

            public EPaperadDes()
            {

            }

            public EPaperadDes(string papcod, string descode, string papdesc, string addesc, double amount)
            {

                this.papcod = papcod;
                this.descode = descode;
                this.papdesc = papdesc;
                this.addesc = addesc;
                this.amount = amount;
            }


        }

        [Serializable]
        public class EGetLastAdNo
        {
            public string maxadno { get; set; }
            public string maxadno1 { get; set; }



            public EGetLastAdNo()
            {

            }

            public EGetLastAdNo(string maxadno, string maxadno1)
            {
                this.maxadno = maxadno;
                this.maxadno1 = maxadno1;
            }


        }

        [Serializable]
        public class EClassAddUser
        {
            public string comcod { get; set; }
            public string adno { get; set; }
            public DateTime addate { get; set; }
            public string refno { get; set; }

            public string postedbyid { get; set; }
            public string postrmid { get; set; }
            public string postseson { get; set; }
            public DateTime posteddat { get; set; }
            public string editbyid { get; set; }
            public string edittrmid { get; set; }
            public string editseson { get; set; }
            public DateTime editdat { get; set; }

            public EClassAddUser()
            {

            }

            public EClassAddUser(string comcod, string adno, DateTime addate, string refno,
                string postedbyid, string postrmid, string postseson, DateTime posteddat, string editbyid,
                DateTime editdat, string edittrmid, string editseson)
            {
                this.comcod = comcod;
                this.adno = adno;
                this.addate = addate;
                this.refno = refno;
                this.postedbyid = postedbyid;
                this.postrmid = postrmid;
                this.posteddat = posteddat;
                this.postseson = postseson;
                this.editbyid = editbyid;
                this.edittrmid = edittrmid;
                this.editseson = editseson;
                this.editdat = editdat;
            }


        }

        [Serializable]

        public class EPaperName
        {
            public string papcod { get; set; }
            public string papname { get; set; }

            public EPaperName()
            {

            }

            public EPaperName(string papcod, string papname)
            {
                this.papcod = papcod;
                this.papname = papname;
            }
        }

        [Serializable]
        public class EClassClentEntry
        {

            public string userid { get; set; }
            public string branch { get; set; }
            public string brname { get; set; }
            public string leadtype { get; set; }
            public string leaddesc { get; set; }
            public string adno { get; set; }
            public string addesc { get; set; }
            public string name { get; set; }
            public string mob { get; set; }
            public string email { get; set; }
            public string info { get; set; }
            public string locat { get; set; }
            public string pro { get; set; }
            public string locaid { get; set; }
            public string proid { get; set; }
            public double size { get; set; }
            public string sendto { get; set; }
            public string entdate { get; set; }
            public string empid { get; set; }

            public string rmks { get; set; }
            public bool chk { get; set; }
            public string clstatus { get; set; }
            public string postedbyid { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string leadst { get; set; }
            public string leadstatus { get; set; }

            public string assignid { get; set; }
            public string assignname { get; set; }
            public string altphone { get; set; }
            public string nbrclno { get; set; }
            public string prodtype { get; set; }
            public string reqsize { get; set; }
            public string mettingdat { get; set; }
            public string visitdat { get; set; }
            public string createdept { get; set; }
            public string compname { get; set; }
            public string designation { get; set; }
            public bool chekstatus { get; set; }
            public EClassClentEntry()
            {

            }

            //public EClassClentEntry ( string userid, string adno, string name, string mob, string email, string info, string locaid, string proid,
            //     string locat, string pro, double size, string sendto )
            //{

            //    this.userid = userid;
            //    this.adno = adno;
            //    this.locaid = locaid;
            //    this.proid = proid;
            //    this.name = name;
            //    this.mob = mob;
            //    this.email = email;
            //    this.info = info;
            //    this.locat = locat;
            //    this.pro = pro;
            //    this.size = size;
            //    this.sendto = sendto;
            //}


        }

        [Serializable]
        public class EClassLeadClentEntry
        {

            public string userid { get; set; }
            public string branch { get; set; }
            public string brname { get; set; }
            public string leadtype { get; set; }
            public string leaddesc { get; set; }
            public string adno { get; set; }
            public string addesc { get; set; }
            public string name { get; set; }
            public string mob { get; set; }
            public string email { get; set; }
            public string info { get; set; }
            public string locat { get; set; }
            public string pro { get; set; }
            public string locaid { get; set; }
            public string proid { get; set; }
            public double size { get; set; }
            public string sendto { get; set; }
            public string entdate { get; set; }
            public string empid { get; set; }

            public string rmks { get; set; }
            public bool chk { get; set; }
            public string clstatus { get; set; }
            public string postedbyid { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string leadst { get; set; }
            public string leadstatus { get; set; }

            public string assignid { get; set; }
            public string assignname { get; set; }
            public string altphone { get; set; }
            public string nbrclno { get; set; }
            public string prodtype { get; set; }
            public string reqsize { get; set; }
            public string mettingdat { get; set; }
            public string visitdat { get; set; }
            public string createdept { get; set; }
            public string compname { get; set; }
            public string designation { get; set; }
            public bool chekstatus { get; set; }
          
            public string deptdesc { get; set; }
            public string proddesc { get; set; }

            public EClassLeadClentEntry()
            {

            }

            //public EClassClentEntry ( string userid, string adno, string name, string mob, string email, string info, string locaid, string proid,
            //     string locat, string pro, double size, string sendto )
            //{

            //    this.userid = userid;
            //    this.adno = adno;
            //    this.locaid = locaid;
            //    this.proid = proid;
            //    this.name = name;
            //    this.mob = mob;
            //    this.email = email;
            //    this.info = info;
            //    this.locat = locat;
            //    this.pro = pro;
            //    this.size = size;
            //    this.sendto = sendto;
            //}


        }

        [Serializable]

        public class EempList
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string emplist { get; set; }

            public EempList()
            {

            }

        }
        [Serializable]
        public class EclassClientdetails
        {
            public string prosdesc { get; set; }
            public string proscod { get; set; }
            public string phone { get; set; }
            public string createdate { get; set; }
            public string profesdesc { get; set; }
            public string locdes { get; set; }
            public string grddesc { get; set; }
            public string email { get; set; }
            public string visited { get; set; }
            public string clstatus { get; set; }
            public string imgpath { get; set; }
            public string professcode { get; set; }
            public string intlocation { get; set; }
            public string grade { get; set; }

            public EclassClientdetails() { }
            public EclassClientdetails(string prosdesc, string proscod, string phone, string createdate, string profesdesc, string locdes, string grddesc, string email,
               string visited, string clstatus, string imgpath, string professcode, string intlocation, string grade)
            {
                this.prosdesc = prosdesc;
                this.proscod = proscod;
                this.phone = phone;
                this.createdate = createdate;
                this.profesdesc = profesdesc;
                this.locdes = locdes;
                this.grddesc = grddesc;
                this.email = email;
                this.visited = visited;
                this.clstatus = clstatus;
                this.imgpath = imgpath;
                this.professcode = professcode;
                this.intlocation = intlocation;
                this.grade = grade;


            }

        }


        [Serializable]
        public class EclassLOdetails
        {
            public string teamcode { get; set; }
            public string teamdesc { get; set; }

            public string prosdesc { get; set; }
            public string proscod { get; set; }
            public string phone { get; set; }
            public string createdate { get; set; }
            public string profesdesc { get; set; }
            public string locdes { get; set; }
            public string grddesc { get; set; }
            public string email { get; set; }
            public string fstatus { get; set; }
            public string clstatus { get; set; }
            public string imgpath { get; set; }
            public string professcode { get; set; }
            public string intlocation { get; set; }
            public string grade { get; set; }

            public EclassLOdetails() { }
            public EclassLOdetails(string prosdesc, string proscod, string phone, string createdate, string profesdesc, string locdes, string grddesc, string email,
               string fstatus, string clstatus, string imgpath, string professcode, string intlocation, string grade)
            {
                this.prosdesc = prosdesc;
                this.proscod = proscod;
                this.phone = phone;
                this.createdate = createdate;
                this.profesdesc = profesdesc;
                this.locdes = locdes;
                this.grddesc = grddesc;
                this.email = email;
                this.fstatus = fstatus;
                this.clstatus = clstatus;
                this.imgpath = imgpath;
                this.professcode = professcode;
                this.intlocation = intlocation;
                this.grade = grade;


            }

        }



        [Serializable]
        public class EclassClientDiscuss
        {
            public DateTime cdate { get; set; }
            public string cdate1 { get; set; }
            public DateTime napnt { get; set; }
            public string napnt1 { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public double usize { get; set; }
            public double rate { get; set; }
            public double ofpamt { get; set; }
            public double ofothamt { get; set; }
            public double ofuamt { get; set; }
            public double oftuamt { get; set; }
            public string destintion { get; set; }
            public string calovtime { get; set; }
            public string discus { get; set; }
            public string prosdesc { get; set; }
            public string proscod { get; set; }
            public string phone { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public double bgdrate { get; set; }
            public double uamt { get; set; }
            public double pamt { get; set; }
            public double othamt { get; set; }
            public double tuamt { get; set; }
            public double dinper { get; set; }
            public DateTime createdate { get; set; }
            public string locdes { get; set; }
            public string grddesc { get; set; }
            public string profesdesc { get; set; }
            public string calltype { get; set; }
        }

        [Serializable]
        public class EclassLODiscuss
        {
            public string prosdesc { get; set; }
            public string proscod { get; set; }
            public DateTime cdate { get; set; }
            public string cdate1 { get; set; }
            public DateTime napnt { get; set; }
            public string napnt1 { get; set; }
            public double ofpamt { get; set; }
            public double ofrtamt { get; set; }
            public double bookamt { get; set; }
            public string lstatuscode { get; set; }
            public string lstatus { get; set; }
            public string discus { get; set; }
            public string kpigrp { get; set; }
            public string kpigrpdesc { get; set; }
            public string partcilistcode { get; set; }
            public string partcilist { get; set; }
            public string nfollowupcode { get; set; }

            public string nfollowup { get; set; }
            public string ndissub { get; set; }
            public string disgnote { get; set; }
            public string subgnote { get; set; }
        }

        [Serializable]
        public class EAdvTopSheet
        {

            public string papcod { get; set; }
            public string descode { get; set; }
            public string papdesc { get; set; }
            public string addesc { get; set; }
            public double amount { get; set; }
            public DateTime addate { get; set; }
            public string adno { get; set; }


            public EAdvTopSheet()
            {

            }
        }

        [Serializable]
        public class EClassRptCallCenterLead
        {
            public string comcod { get; set; }
            public string clustid { get; set; }

            public string teamid { get; set; }

            public string empid { get; set; }
            public double totlead { get; set; }
            public double pvisitd { get; set; }
            public double pvisits { get; set; }
            public double cmeetingd { get; set; }
            public double cmeetings { get; set; }
            public double followup { get; set; }
            public double junk { get; set; }

            public string clustname { get; set; }
            public string teamname { get; set; }
            public string empname { get; set; }


            public EClassRptCallCenterLead()
            {

            }



        }
    }
}