using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEntity.C_08_PPlan;
using RealERPLIB;

namespace RealEntity
{
    public class UserProjPlan
    {
        

            DataAccess _dataAccess = new DataAccess ();
            ProcessAccess _ProAccess = new ProcessAccess ();
            Common ObjCommon = new Common ();



            #region SalesDash_Board


            public List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> GetPayment ( string comcod,string pactcode,string rescode )
            {
                List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> lst = new List<RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment> ();

                //string comcod = ObjCommon.GetCompCode();
                SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_PROJECTTARGET", "GETPAYMENTDETAILS", pactcode, rescode);

                while (dr.Read ())
                {
                    var objpap = new RealEntity.C_08_PPlan.E_CLassPaymetSch.OwnerInstallment()
                    {
                        insdate = DateTime.Parse(dr["insdate"].ToString()),
                        insamt = double.Parse(dr["insamt"].ToString())
                    };
                    lst.Add (objpap);
                }

                return lst;

            }

        public List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ProjectName> GetProject(string comcod,string srchTxt)
        {
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ProjectName> lst=new List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ProjectName>();

            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt);
            while (dr.Read())
            {
                var obj = new RealEntity.C_08_PPlan.E_CLassPaymetSch.ProjectName()
                {
                    actcode = dr["actcode"].ToString(),
                    actdesc = dr["actdesc"].ToString()
                };
                lst.Add(obj);
            }
            return lst;
        }

        public List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ResourceDes> GetResource ( string comcod)
        {
            List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ResourceDes> lst = new List<RealEntity.C_08_PPlan.E_CLassPaymetSch.ResourceDes> ();

            SqlDataReader dr = _ProAccess.GetSqlReader (comcod, "SP_ENTRY_PROJECTTARGET", "GETRESOURCEDETAILS");
            while (dr.Read ())
            {
                var obj = new RealEntity.C_08_PPlan.E_CLassPaymetSch.ResourceDes ()
                {
                    sircode = dr["sircode"].ToString (),
                    sirdesc = dr["sirdesc"].ToString ()
                };
                lst.Add (obj);
            }
            return lst;
        }
            #endregion

        }
    }



