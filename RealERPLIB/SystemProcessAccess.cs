using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
namespace RealERPLIB
{
   public  class SystemProcessAccess
    {


       SystemDataAccess da;
       private Hashtable _errObj;




       public SystemProcessAccess()
       {
           da = new  SystemDataAccess();
           _errObj = new Hashtable();
       }

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
       
       public DataSet  GetDailyAttenDance( string cmd)
       {
           try
           {
               this.ClearErrors();
               DataSet ds = da.GetDataSet(cmd);

               if (ds == null) 
               {
                   this.SetError(da.ErrorObject);
               }
               return ds;
           }

           catch (Exception ex) 
           {
               this.SetError(ex);
               return null;
           
           
           }
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


       



    }


 
}
