using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using RealERPLIB;

namespace RealERPLIB
{
   public class UserLogin
    {

       DataAccessOLDB da = new DataAccessOLDB();
       private Hashtable _errObj;


       public Hashtable ErrorObject
       {
           get
           {
               return this._errObj;
           }
       }

       private void SetError(Exception exp)
       {
           this._errObj["Src"] = exp.Source;
           this._errObj["Msg"] = exp.Message;
           this._errObj["Location"] = exp.StackTrace;
       }

       private void SetError(Hashtable errObject)
       {
           this._errObj["Src"] = errObject["Src"];
           this._errObj["Msg"] = errObject["Msg"];
           this._errObj["Location"] = errObject["Location"];
       }

       private void ClearErrors()
       {
           this._errObj["Src"] = string.Empty;
           this._errObj["Msg"] = string.Empty;
           this._errObj["Location"] = string.Empty;
       }
       
       public DataSet GetNameAdd()
       {
           //"select comcod, comnam, comsnam, comadd= comadd1+'<br />'+comadd2+' '+comadd3, comadd1,comadd2, comadd3, comadd4  from compinf order by comcod asc";
           string cmd = "select slnum, comcod, comnam, comsnam,  comadd1+'<br />'+comadd2+' '+comadd3 as comadd, comadd1,comadd2, comadd3, comadd4, compcod , combranch from compinf order by slnum, comcod asc";
           DataSet ds = da.GetDataSet(cmd);
           return ds;
        }

       public DataSet GetCompanyAndAdd() 
       {
           string cmd = "select slnum, comcod, comnam, comsnam,  comadd1+'<br />'+comadd2+' '+comadd3 as comadd, comadd1,comadd2, comadd3, comadd4, compcod  from compinf where compcod IS Null order by slnum, comcod asc";
           DataSet ds = da.GetDataSet(cmd);
           return ds;
       
       }


        public DataSet GoupCompany() 
        {

            string cmd = "select  comnam, comadd+'<br />'+comphone+' '+comweb as comadd from groupinf";
            DataSet ds = da.GetDataSet(cmd);
            return ds;
        
        }

        public DataSet ExpDate() 
        {

            string cmd = "select  date as expdate from expdinf";
            DataSet ds = da.GetDataSet(cmd);
            return ds;
        
        }

        public DataSet GetHitCounter()
        {

            string cmd = "select  cnumber  from hcntinf";
            DataSet ds = da.GetDataSet(cmd);
            return ds;

        }

        public DataSet GetHitCounterLimit() 
        {

            string cmd = "select  cntstep, cntval, cntdes   from hcntlmt order by cntstep";
            DataSet ds = da.GetDataSet(cmd);
            return ds;
        }

        //public bool UpdateHitCounter(string cnumber) 
        //{
        //    try
        //    {
        //        string cmd = "update hcntinf set cnumber1='" + cnumber + "'";
        //        bool result = da.ExecuteCommand(cmd);
            
        //        if (result == false)  //_result==false
        //        {
        //            this.SetError(da.ErrorObject);
        //        }
        //        return result;

        //    }
        //    catch (Exception exp) 
        //    {
        //        this.SetError(exp);
        //        return false;
            
            
        //    }
           
        //}


        public void UpdateHitCounter(string cnumber)
        {
            
                string cmd = "update hcntinf set cnumber='" + cnumber + "'";
               da.ExecuteCommand(cmd);
        }






    }

    
}
