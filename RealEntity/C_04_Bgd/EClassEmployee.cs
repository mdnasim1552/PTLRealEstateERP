using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RealEntity.C_04_Bgd
{
     public class EClassEmployee
    {
            public string employee_code { set; get; }
            public string first_name { set; get; }
            public string middle_name { set; get; }
            public string last_name { set; get; }
            public string designation { set; get; }
            public string department { set; get; }
            public string present_address { set; get; }
            public string permament_address { set; get; }


            public EClassEmployee() 
            { }

            public EClassEmployee(string empcode, string fname, string mname, string lname, string designation, string deptname, string paddress, string peradd)
           
            {
                this.employee_code = empcode;
                this.first_name = fname;
                this.middle_name = mname;
                this.last_name = lname;
                this.designation = designation;
                this.department = deptname;
                this.present_address = paddress;
                this.permament_address = peradd;
            
            
            }


    }

     //comcod, isircode, flrcod, bgdqty, adqty1, adqty2, adqty3, adqty4, adqty5, toadqty, bgdrate, bgdamt, adamt1,
     //adamt2, adamt3, adamt4, adamt5, toadamt,  tobgdqty, tobgdamt, flrdes		, isirdesc, isirunit	
    [Serializable]
     public class Additionalbug
     {
        public string comcod { set; get; }
        public string isircode { set; get; }
        public string flrcod { set; get; }
        public double bgdqty { set; get; }
        public double adqty1 { set; get; }
        public double adqty2 { set; get; }
        public double adqty3 { set; get; }
        public double adqty4 { set; get; }
        public double adqty5 { set; get; }
        public double toadqty { set; get; }
        public double bgdrate { set; get; }
        public double bgdamt { set; get; }
        public double adamt1 { set; get; }
        public double adamt2 { set; get; }
        public double adamt3 { set; get; }
        public double adamt4 { set; get; }
        public double adamt5 { set; get; }
        public double toadamt { set; get; }
        public double tobgdamt { set; get; }
        public string flrdes { set; get; }
        public string isirdesc { set; get; }
        public string isirunit { set; get; }
         public Additionalbug()  { }
     }
    [Serializable]
    public class BugIncoStatement
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string actcode { get; set; }
        public string rescode1 { get; set; }
        public string rescode { get; set; }
        public double bgdam { get; set; }
        public double acamt { get; set; }
        public string actdesc { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
    }
    [Serializable]
    //comcod, bldcod, acgcode, rowid, rptcod, flrcod, flrdes, rptqty, rptrat, rptamt, peramt, rptdesc, rptdesc1, rptunit  
    public class BugIncmStatement
    {
        public string comcod { get; set; }
        public string bldcod { get; set; }
        public string acgcode { get; set; }
        public string rowid { get; set; }
        public string rptcod { get; set; }
        public string flrcod { get; set; }
        public string flrdes { get; set; }
        public double rptqty { get; set; }
        public double rptrat { get; set; }
        public double rptamt { get; set; }
        public double peramt { get; set; }
        public string rptdesc { get; set; }
        public string rptdesc1 { get; set; }
        public string rptunit { get; set; }
        public BugIncmStatement() { }
    }

    [Serializable]
    public class PrjFoorWise
    {
        public string comcod { get; set; }
        public string bldcod { get; set; }
        public string rptcod { get; set; }
        public string sirdesc { get; set; }
        public string unit { get; set; }
        public double rptamt { get; set; }
        public double qty1 { get; set; }
        public double qty2 { get; set; }
        public double qty3 { get; set; }
        public double qty4 { get; set; }
        public double qty5 { get; set; }
        public double qty6 { get; set; }
        public double qty7 { get; set; }
        public double qty8 { get; set; }
        public double qty9 { get; set; }
        public double qty10 { get; set; }
        public double qty11 { get; set; }
        public double qty12 { get; set; }
        public double qty13 { get; set; }
        public double qty14 { get; set; }
        public double qty15 { get; set; }
        public double qty16 { get; set; }
        public double qty17 { get; set; }
        public double qty18 { get; set; }
        public double qty19 { get; set; }
        public double qty20 { get; set; }
        public double toqty { get; set; }
        public double rate { get; set; }
      
        public PrjFoorWise()
        { }
    }

    [Serializable]
    public class PrjFoorDesc
    {
        public string bldcod { get; set; }
        public string flrcod { get; set; }
        public string flrdes { get; set; }
        public PrjFoorDesc()
        { }
    }
    [Serializable]
    public class BgdFlrWise
    {
        public string flrcod { get; set; }
        public string flrdesc { get; set; }
        public double bgdamt { get; set; }
        public double concost { get; set; }
        public double salcost { get; set; }
        public BgdFlrWise ( ){ }
    }
    [Serializable]
    //nayan
   // comcod, bldcod, rptcod, flrcod, flrdes, rptqty, rptrat, rptamt, peramt, rptdesc, rptdesc1, rptunit
    public class BugbasAns
    {
        public string comcod { get; set; }
        public string bldcod { get; set; }
        public string flrcod { get; set; }
        public string flrdes { get; set; }
        public double rptqty { get; set; }
        public double rptrat { get; set; }
        public double rptamt { get; set; }
        public double peramt { get; set; }
        public string rptdesc { get; set; }
        public string rptdesc1 { get; set; }
        public string rptunit { get; set; }
        public BugbasAns() { }
    }
    //nayan
    // a.comcod, a.pactcode, a.grp,  a.gcod, gunit='',  a.gval, grpdesc=( CASE WHEN a.grp='01000' THEN 'A. '+b.prgdesc WHEN a.grp='02000' THEN 'B. '+b.prgdesc WHEN a.grp='03000' THEN 'C. '+b.prgdesc  ELSE '' end), gdesc=isnull(c.prgdesc, ''),  pactdesc=d.actdesc
    [Serializable]
    public class BProjInfo
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string grp { get; set; }
        public string gcod { get; set; }
        public string gunit { get; set; }
        public string gval { get; set; }
        public string grpdesc { get; set; }
        public string gdesc { get; set; }
        public string pactdesc { get; set; }
        public BProjInfo() { }
    }
    //nayan
    //comcod, grpcod, grpdesc, pactcode, usircode, udesc, munit, uqty, pqty, usize, uamt, urate, pamt, cooperative, utility,  tamt, bstat, minbam , sustatus nayan
    [Serializable]
    public class BgdSales
    {
        public string comcod { get; set; }
        public string grpcod { get; set; }
        public string grpdesc { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string udesc { get; set; }
        public string munit { get; set; }
        public double uqty { get; set; }
        public double pqty { get; set; }
        public double usize { get; set; }
        public double uamt { get; set; }
        public double urate { get; set; }
        public double pamt { get; set; }
        public double cooperative { get; set; }
        public double utility { get; set; }
        public double tamt { get; set; }
        public string bstat { get; set; }
        public double minbam { get; set; }
        public string sustatus { get; set; }
        public BgdSales() { }
    }

    //a.comcod, a.grp, a.actcode,  a.rescode , a.acgcode, a.bgdqty, a.bgdrate, a.bgdam, a.devcost, a.salcost, a.actdesc , resdesc=isnull(b.udesc,a.resdesc), 
    //resunit=isnull(b.munit,a.resunit) ,a.acgdesc
    [Serializable]
    public class BugCostDetails
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string actcode { get; set; }
        public string rescode { get; set; }
        public string acgcode { get; set; }
        public double bgdqty { get; set; }
        public double bgdam { get; set; }
        public double bgdrate { get; set; }
        public double devcost { get; set; }
        public double salcost { get; set; }
        public string actdesc { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string acgdesc { get; set; }
        public BugCostDetails() { }
    }

    // a.comcod, a.grp, a.actcode,  a.rescode , a.acgcode, a.bgdqty, a.bgdrate, a.bgdam, a.devcost, a.salcost, a.actdesc , resdesc=isnull(b.udesc,a.resdesc), 
    //resunit=isnull(b.munit,a.resunit) ,a.acgdesc
    [Serializable]
    public class BugcostDetails01
    {
        public string comcod { get; set; }
        public string grp { get; set; }
        public string actcode { get; set; }
        public string rescode { get; set; }
        public string acgcode { get; set; }
        public double bgdqty { get; set; }
        public double bgdam { get; set; }
        public double bgdrate { get; set; }
        public double devcost { get; set; }
        public double salcost { get; set; }
        public string actdesc { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string acgdesc { get; set; }
        public string colst { get; set; }
        public BugcostDetails01() { }
    }
    
    [Serializable]
    public class BgdProInfo
    {
        public string comcod { get; set; }
        public string gcod { get; set; }
        public string gdesc { get; set; }
        public string gdesc1 { get; set; }
        public string gunit { get; set; }
        public string pactcode { get; set; }
        public BgdProInfo() { }
    }
}
