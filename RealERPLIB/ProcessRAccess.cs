using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace RealERPLIB
{
    public class ProcessRAccess
    {
        DataAccess _dataAccess;
        private Hashtable _errObj;

        public ProcessRAccess()
        {
            _dataAccess = new DataAccess();
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
        public DataSet DataRelation(string procedure, string comCode, string CallType, string TblName, string desc1,
            string desc2, string desc3, string desc4, string desc5)
        {//frmControlcode,frmCodeBooks,
            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", TblName));
                cmd.Parameters.Add(new SqlParameter("@Desc1", desc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", desc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", desc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", desc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", desc5));

                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }//
        public DataSet DataRelationSales(string procedure, string comCode, string CallType, string TblName, string desc1,
            string desc2, string desc3, string desc4, string desc5) //** for SP_SALES_INFO Procedure**
        {//frmControlcode,frmCodeBooks,
            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", TblName));
                cmd.Parameters.Add(new SqlParameter("@CenterId", desc1));
                cmd.Parameters.Add(new SqlParameter("@Desc1", desc2));
                cmd.Parameters.Add(new SqlParameter("@Desc2", desc3));
                cmd.Parameters.Add(new SqlParameter("@Desc3", desc4));
                cmd.Parameters.Add(new SqlParameter("@Desc4", desc5));

                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }//

        public DataSet DataCodeBooks(string procedure, string comCode, string CallType, string TblName, string mainCode)
        {//frmControlcode,frmCodeBooks,
            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", TblName));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mainCode));
                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }//

        public bool DataUpdate(string procedure, string comCode, string caltype, string tblename, string desc1, string desc2,
            string desc3, string desc4, string desc5, string desc6, string desc7, string desc8)
        {//frmControlcode,frmCodeBooks,
            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", caltype));
                cmd.Parameters.Add(new SqlParameter("@TblName", tblename));
                cmd.Parameters.Add(new SqlParameter("@Desc1", desc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", desc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", desc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", desc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", desc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", desc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", desc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", desc8));
                bool result = _dataAccess.ExecuteCommand(cmd);
                if (!result)//_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                // this.SetError(exp);
                return false;
            }// try
        }

        public DataSet UpdateDataSet(string procedure, string ComCod, string CallType, string tblname, string Param1,
            string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8,
            string Param9, string Param10, string Param11, string Param12, string Param13, string Param14)
        {

            try
            {
                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", ComCod));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", tblname));
                cmd.Parameters.Add(new SqlParameter("@Desc1", Param1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", Param2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", Param3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", Param4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", Param5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", Param6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", Param7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", Param8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", Param9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", Param10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", Param11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", Param12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", Param13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", Param14));
                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }
        public bool UpdateBoolean(string procedure, string ComCod, string CallType, string tblname, string Param1,
            string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8,
            string Param9, string Param10, string Param11, string Param12, string Param13, string Param14)
        {

            try
            {

                string SQL = procedure;//"SP_ENTRY_ADVANCED_MEMO";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", ComCod));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", tblname));
                cmd.Parameters.Add(new SqlParameter("@Desc1", Param1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", Param2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", Param3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", Param4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", Param5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", Param6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", Param7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", Param8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", Param9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", Param10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", Param11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", Param12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", Param13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", Param14));
                bool result = _dataAccess.ExecuteCommand(cmd);
                if (!result)//_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                // this.SetError(exp);
                return false;
            }// try
        }

        public DataSet UpdateCode(string StrProc, string comCode, string CallType, string TblName, string mainCode, string desc2,
            string desc3, string desc4)
        {
            try
            {
                //"SP_ENTRY_PRINT_CODEBOOK"
                string SQL = StrProc;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", TblName));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mainCode));
                cmd.Parameters.Add(new SqlParameter("@Desc2", desc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", desc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", desc4));
                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                return null;
            }// try
        }//
        public bool UpdateCode1(string procedure, string comCode, string CallType, string TblName, string mainCode, string desc2, string desc3, string desc4, string desc5, string desc6, string desc7, string desc8)
        {
            try
            {

                string SQL = procedure;// "SP_ENTRY_PRINT_CODEBOOK";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", TblName));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mainCode));
                cmd.Parameters.Add(new SqlParameter("@Desc2", desc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", desc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", desc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", desc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", desc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", desc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", desc8));
                bool result = _dataAccess.ExecuteCommand(cmd);
                if (!result)//_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                return false;
            }// try
        }// 
        public DataSet UpdateDataSetAcc(string procedure, string ComCod, string CallType, string tblname, string Param1,
           string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8,
           string Param9, string Param10, string Param11, string Param12, string Param13, string Param14, string Param15,
            string Param16, string Param17, string Param18, string Param19, string Param20, string Param21, string Param22, string Param23)
        {

            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", ComCod));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", tblname));
                cmd.Parameters.Add(new SqlParameter("@Desc1", Param1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", Param2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", Param3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", Param4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", Param5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", Param6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", Param7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", Param8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", Param9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", Param10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", Param11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", Param12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", Param13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", Param14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", Param15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", Param16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", Param17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", Param18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", Param19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", Param20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", Param21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", Param22));
                cmd.Parameters.Add(new SqlParameter("@Desc23", Param23));

                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }
        public DataSet UpdateDataSetSvr(string procedure, string ComCod, string CallType, string tblname, string Param1,
           string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8,
           string Param9, string Param10, string Param11, string Param12, string Param13, string Param14, string Param15, string Param16)
        {

            try
            {

                string SQL = procedure;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", ComCod));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@TblName", tblname));
                cmd.Parameters.Add(new SqlParameter("@Desc1", Param1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", Param2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", Param3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", Param4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", Param5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", Param6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", Param7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", Param8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", Param9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", Param10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", Param11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", Param12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", Param13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", Param14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", Param15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", Param16));
                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {

                return null;
            }// try
        }

    }
}
