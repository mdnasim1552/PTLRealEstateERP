using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using RealERPLIB;

namespace RealEntity
{
    public class UserManMkt
    {

        DataAccess _dataAccess = new DataAccess();
        ProcessAccess _ProAccess = new ProcessAccess();
        Common ObjCommon = new Common();

       

        #region SalesDash_Board


        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> GetPaperAndDesCode(string comcod)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> ();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "GETADDETAILS", "", "", "", "", "", "", "", "", "");

            while (dr.Read())
            {
                RealEntity.C_21_Mkt.EClassAdvertisement.EPaper objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EPaper (dr["gcod"].ToString (), dr["gdesc"].ToString ());
                lst.Add (objpap);
            }

            return lst;

        }

        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo> GetLastAdNo ( string comcod, string addate )
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo> ();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "ADVERNO", addate, "", "", "", "", "", "", "", "");

            while (dr.Read ())
            {
                RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EGetLastAdNo (dr["maxadno"].ToString (), dr["maxadno1"].ToString ());
                lst.Add (objpap);
            }

            return lst;
           

        }


        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName> GetPaperName ( string comcod )
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName> ();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "ADINFO");

            while (dr.Read ())
            {
                RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EPaperName (dr["papcod"].ToString (), dr["papname"].ToString ());
                lst.Add (objpap);
            }

            return lst;

        }

        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> GetAddUserInfo ( string comcod, string frmdate, string todate,string mob )
        {
            List <RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "GETADUSER", frmdate, todate, mob);

            while (dr.Read())
            {
                //RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry (dr["userid"].ToString (), dr["name"].ToString (), dr["adno"].ToString (), dr["mob"].ToString (),
                //    dr["email"].ToString(), dr["info"].ToString (),dr["locat"].ToString(),dr["pro"].ToString(),dr["locaid"].ToString(),dr["proid"].ToString(),
                //   double.Parse(dr["size"].ToString()),dr["sendto"].ToString());

                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry()
                {
                        userid = dr["userid"].ToString(),
                        name = dr["name"].ToString(),
                        adno = dr["adno"].ToString(),
                        addesc = dr["addesc"].ToString(),
                        branch = dr["branch"].ToString(),
                        brname = dr["brname"].ToString(),
                        leadtype = dr["leadtype"].ToString(),
                        leaddesc = dr["leaddesc"].ToString(), 
                        mob = dr["mob"].ToString (),
                        email = dr["email"].ToString(),  
                        info = dr["info"].ToString (),  
                        locat = dr["locat"].ToString(),  
                        pro = dr["pro"].ToString(),   
                        locaid = dr["locaid"].ToString(), 
                        proid = dr["proid"].ToString(),
                        size = double.Parse (dr["size"].ToString ()),   
                        sendto = dr["sendto"].ToString(),
                        pactcode = dr["pactcode"].ToString(),
                        pactdesc = dr["pactdesc"].ToString(),
                        leadst = dr["leadst"].ToString(),
                        leadstatus = dr["leadstatus"].ToString(),
                        assignid = dr["assignid"].ToString(),
                        assignname = dr["assignname"].ToString()



                };
                lst.Add (objpap);
            }

            return lst;

        }

        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> GetProAndLocatio ( string comcod )
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EPaper> ();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "LOCATPRO");

            while (dr.Read ())
            {
                RealEntity.C_21_Mkt.EClassAdvertisement.EPaper objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EPaper (dr["gcod"].ToString (), dr["gdesc"].ToString ());
                lst.Add (objpap);
            }

            return lst;

        }
        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EempList> GetEmpList ( string comcod )
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EempList> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EempList> ();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "LOADEMP");
            while (dr.Read ())
            {
                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EempList ()
                {
                    empid = dr["empid"].ToString (),
                    empname = dr["empname"].ToString (),
                    desig = dr["desig"].ToString (),
                    emplist = dr["emplist"].ToString()
                };
                lst.Add (objpap);
            }

            return lst;

        }


        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> GetAllEmp ( string comcod,string qtype)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> ();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "EMPLIST", qtype);

            while (dr.Read ())
            {
                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry ()
                {
                    userid = dr["userid"].ToString (),
                    adno = dr["adno"].ToString (),
                    name = dr["name"].ToString (),
                    mob = dr["mob"].ToString (),
                    email = dr["email"].ToString (),
                    info = dr["info"].ToString (),
                    locat = dr["locat"].ToString (),
                    pro = dr["pro"].ToString (),
                    locaid = dr["locaid"].ToString (),
                    proid = dr["proid"].ToString (),
                    size = double.Parse (dr["size"].ToString ()),
                    sendto = dr["sendto"].ToString (),
                    rmks = dr["rmks"].ToString(),
                    chk = bool.Parse(dr["chk"].ToString()),
                    clstatus = dr["clstatus"].ToString()

                    
                };
                lst.Add (objpap);
            }

            return lst;

        }


        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet> GetAdvTopSheet ( string comcod,string frmdate,string todate )
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet> ();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "DETALLADDETAILS", frmdate, todate);

            while (dr.Read ())
            {
                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EAdvTopSheet ()
                {
                    
                    adno = dr["adno"].ToString (),
                    papdesc = dr["papdesc"].ToString(),
                    addate = DateTime.Parse(dr["addate"].ToString()),
                    amount = double.Parse(dr["amount"].ToString())
                    
                };
                lst.Add (objpap);
            }

            return lst;

        }

        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> GetLeadAddUserInfo(string comcod, string frmdate, string todate, string mob, string ledtype)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_MKT_TEAM", "GETLEADADUSER", frmdate, todate, mob, ledtype);

            while (dr.Read())
            {
                //RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry (dr["userid"].ToString (), dr["name"].ToString (), dr["adno"].ToString (), dr["mob"].ToString (),
                //    dr["email"].ToString(), dr["info"].ToString (),dr["locat"].ToString(),dr["pro"].ToString(),dr["locaid"].ToString(),dr["proid"].ToString(),
                //   double.Parse(dr["size"].ToString()),dr["sendto"].ToString());createdept, deptdesc=isnull(j.gdesc,''), proddesc

                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry()
                {
                    userid = dr["userid"].ToString(),
                    name = dr["name"].ToString(),
                    adno = dr["adno"].ToString(),
                    addesc = dr["addesc"].ToString(),
                    branch = dr["branch"].ToString(),
                    brname = dr["brname"].ToString(),
                    leadtype = dr["leadtype"].ToString(),
                    leaddesc = dr["leaddesc"].ToString(),
                    mob = dr["mob"].ToString(),
                    email = dr["email"].ToString(),
                    info = dr["info"].ToString(),
                    locat = dr["locat"].ToString(),
                    pro = dr["pro"].ToString(),
                    locaid = dr["locaid"].ToString(),
                    proid = dr["proid"].ToString(),
                    size = double.Parse(dr["size"].ToString()),
                    sendto = dr["sendto"].ToString(),
                    pactcode = dr["pactcode"].ToString(),
                    pactdesc = dr["pactdesc"].ToString(),
                    leadst = dr["leadst"].ToString(),
                    leadstatus = dr["leadstatus"].ToString(),
                    assignid = dr["assignid"].ToString(),
                    assignname = dr["assignname"].ToString(),
                    createdept = dr["createdept"].ToString(),
                    deptdesc = dr["deptdesc"].ToString(),
                    proddesc = dr["proddesc"].ToString()




                };
                lst.Add(objpap);
            }

            return lst;

        }


        public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> GetCenterWiseClient(string comcod, string frmdate, string todate, string mob, string ledtype, string branch)
        {
            List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry>();

            //string comcod = ObjCommon.GetCompCode();
            SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_MKT_TEAM", "GETCENTERWISECLIENT", frmdate, todate, mob, ledtype, branch);

            while (dr.Read())
            {
                //RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry (dr["userid"].ToString (), dr["name"].ToString (), dr["adno"].ToString (), dr["mob"].ToString (),
                //    dr["email"].ToString(), dr["info"].ToString (),dr["locat"].ToString(),dr["pro"].ToString(),dr["locaid"].ToString(),dr["proid"].ToString(),
                //   double.Parse(dr["size"].ToString()),dr["sendto"].ToString());createdept, deptdesc=isnull(j.gdesc,''), proddesc

                var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassLeadClentEntry()
                {
                    userid = dr["userid"].ToString(),
                    name = dr["name"].ToString(),
                    adno = dr["adno"].ToString(),
                    addesc = dr["addesc"].ToString(),
                    branch = dr["branch"].ToString(),
                    brname = dr["brname"].ToString(),
                    leadtype = dr["leadtype"].ToString(),
                    leaddesc = dr["leaddesc"].ToString(),
                    mob = dr["mob"].ToString(),
                    email = dr["email"].ToString(),
                    info = dr["info"].ToString(),
                    locat = dr["locat"].ToString(),
                    pro = dr["pro"].ToString(),
                    locaid = dr["locaid"].ToString(),
                    proid = dr["proid"].ToString(),
                    size = double.Parse(dr["size"].ToString()),
                    sendto = dr["sendto"].ToString(),
                    pactcode = dr["pactcode"].ToString(),
                    pactdesc = dr["pactdesc"].ToString(),
                    leadst = dr["leadst"].ToString(),
                    leadstatus = dr["leadstatus"].ToString(),
                    assignid = dr["assignid"].ToString(),
                    assignname = dr["assignname"].ToString(),
                    createdept = dr["createdept"].ToString(),
                    deptdesc = dr["deptdesc"].ToString(),
                    proddesc = dr["proddesc"].ToString()


                };
                lst.Add(objpap);
            }

            return lst;

        }
        //public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> GetExistingClient ( string comcod, string phonno)
        //{
        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> ();

        //    //string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_MKT_TEAM", "GETEXISTINGUSER",null,null,null, phonno);

        //    while (dr.Read ())
        //    {
                
        //        var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry ()
        //        {
        //            //userid = dr["userid"].ToString (),
        //            //adno = dr["adno"].ToString (),
        //            name = dr["name"].ToString (),
        //            mob = dr["mob"].ToString (),
        //            email = dr["email"].ToString (),
        //            //info = dr["info"].ToString (),
        //            locat = dr["locat"].ToString (),
        //            pro = dr["pro"].ToString (),
        //            locaid = dr["locaid"].ToString (),
        //            proid = dr["proid"].ToString (),
        //            size = double.Parse (dr["size"].ToString ()),
        //           // sendto = dr["sendto"].ToString ()
        //        };
        //        lst.Add (objpap);
        //    }

        //    return lst;

        //}


        //public List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> GetLeadAddUserInfo(string comcod, string frmdate, string todate, string mob, string ledtype)
        //{
        //    List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry> lst = new List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>();

        //    //string comcod = ObjCommon.GetCompCode();
        //    SqlDataReader dr = _ProAccess.GetSqlReader(comcod, "SP_ENTRY_MKT_TEAM", "GETLEADADUSER", frmdate, todate, mob, ledtype);

        //    while (dr.Read())
        //    {
        //        //RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry (dr["userid"].ToString (), dr["name"].ToString (), dr["adno"].ToString (), dr["mob"].ToString (),
        //        //    dr["email"].ToString(), dr["info"].ToString (),dr["locat"].ToString(),dr["pro"].ToString(),dr["locaid"].ToString(),dr["proid"].ToString(),
        //        //   double.Parse(dr["size"].ToString()),dr["sendto"].ToString());

        //        var objpap = new RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry()
        //        {
        //            userid = dr["userid"].ToString(),
        //            name = dr["name"].ToString(),
        //            adno = dr["adno"].ToString(),
        //            addesc = dr["addesc"].ToString(),
        //            branch = dr["branch"].ToString(),
        //            brname = dr["brname"].ToString(),
        //            leadtype = dr["leadtype"].ToString(),
        //            leaddesc = dr["leaddesc"].ToString(),
        //            mob = dr["mob"].ToString(),
        //            email = dr["email"].ToString(),
        //            info = dr["info"].ToString(),
        //            locat = dr["locat"].ToString(),
        //            pro = dr["pro"].ToString(),
        //            locaid = dr["locaid"].ToString(),
        //            proid = dr["proid"].ToString(),
        //            size = double.Parse(dr["size"].ToString()),
        //            sendto = dr["sendto"].ToString(),
        //            pactcode = dr["pactcode"].ToString(),
        //            pactdesc = dr["pactdesc"].ToString(),
        //            leadst = dr["leadst"].ToString(),
        //            leadstatus = dr["leadstatus"].ToString(),
        //            assignid = dr["assignid"].ToString(),
        //            assignname = dr["assignname"].ToString()



        //        };
        //        lst.Add(objpap);
        //    }

        //    return lst;

        //}
        #endregion
        
    }
}
