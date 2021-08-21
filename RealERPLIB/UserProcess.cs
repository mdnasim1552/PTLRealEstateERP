using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace RealERPLIB
{
    public class UserProcess
    {
        DataAccess _dataAccess;
        private Hashtable _errObj;

        public UserProcess()
        {
            _dataAccess = new DataAccess();
            _errObj = new Hashtable();
        }
        #region Error
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
        #endregion
        public DataSet GeneralInfoReport(string comCode, string SQLprocName, string CallType,
            string mParm1, string mParm2, string mParm3, string mParm4, string mParm5)
        {
            try
            {
                this.ClearErrors();
                string SQL = SQLprocName;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mParm1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mParm2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mParm3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mParm4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mParm5));
                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)//_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return null;
            }// try

        }
    }
}
