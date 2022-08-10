using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace RealERPLIB
{
    public delegate int Mydelegate(int val1, int val2);

    public class ProcessAccess
    {
        DataAccess _dataAccess;
        private Hashtable _errObj;
        private static int toamt = 0;
        //private double todramt =0.000000;
        //private double tocramt =0.000000;
        private static double todramt;
        private static double tocramt;
        public static double hitcnumber;

        public int Toamt
        {
            get { return toamt; }
            set { toamt = value; }

        }
        public int Prod(int val1, int val2)
        {
            return (val1 * val2);

        }

        public double ToDramt
        {
            get { return todramt; }
            set { todramt = value; }
        }

        public double ToCramt
        {
            get { return tocramt; }
            set { tocramt = value; }

        }

        public double HitcNumber
        {
            get { return hitcnumber; }
            set { hitcnumber = value; }
        }
        public ProcessAccess()
        {
            _dataAccess = new DataAccess();
            _errObj = new Hashtable();
        }


        public ProcessAccess(string DbName)
        {
            _dataAccess = new DataAccess(DbName);
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


        public SqlDataReader GetSqlReader(string comCode = "", string SQLprocName = "", string CallType = "", string mDesc1 = "",
            string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "", string mDesc7 = "",
            string mDesc8 = "", string mDesc9 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));

                SqlDataReader result = _dataAccess.ExecuteReader(cmd);
                if (result == null)  //_result==false
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

        public SqlDataReader GetSqlReader2(string comCode, string McomCode, string SQLprocName, string CallType, string mDesc1 = "", string mDesc2 = "",

          string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "", string mDesc7 = "", string mDesc8 = "", string mDesc9 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@Comp2", McomCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));

                SqlDataReader result = _dataAccess.ExecuteReader(cmd);
                if (result == null)  //_result==false
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


        public DataSet GetTransInfo(string comCode, string SQLprocName, string CallType, string mDesc1 = "", string mDesc2 = "",
     string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "", string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));

                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
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

        //public bool UpdateTransInfo(string comcod, string v1, string v2, string pactcode, string mktco, object bgdqty, object p1, object p2, string yearmon, string v3, string v4, string v5, string v6, string v7, string v8, string v9)
        //{
        //    throw new NotImplementedException();
        //}

        public bool UpdateTransInfo(string comCode, string SQLprocName, string CallType,
             string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
             string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
             string mDesc13 = "", string mDesc14 = "", string mUserID = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }


        public bool UpdateTransInfoTrigger(string SQLprocName, string CallType,
          string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
          string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
          string mDesc13 = "", string mDesc14 = "", string mUserID = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }

        //public bool UpdateTransInfo02(string comCode, string SQLprocName, string CallType,
        //     string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6,
        //     string mDesc7, string mDesc8, string mDesc9, string mDesc10, string mDesc11, string mDesc12,
        //     string mDesc13, string mDesc14, string mDesc15, string mDesc16, string mDesc17, string mDesc18,
        //     string mDesc19, string mDesc20, string mUserID)
        //{
        //    try
        //    {
        //        this.ClearErrors();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = SQLprocName;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
        //        cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
        //        cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
        //        cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
        //        cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
        //        cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
        //        cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
        //        cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
        //        cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
        //        cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
        //        cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
        //        cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
        //        cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
        //        cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
        //        cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
        //        cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
        //        cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
        //        cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
        //        cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
        //        cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
        //        cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
        //        cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
        //        cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
        //        bool _result = _dataAccess.ExecuteCommand(cmd);
        //        if (_result == false)  //_result==false
        //        {
        //            this.SetError(_dataAccess.ErrorObject);
        //        }
        //        return _result;
        //    }
        //    catch (Exception exp)
        //    {
        //        this.SetError(exp);
        //        return false;
        //    }// try
        //}


        public bool UpdateTransInfo2(string comCode, string SQLprocName, string CallType,
            string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6,
            string mDesc7, string mDesc8, string mDesc9, string mDesc10, string mDesc11, string mDesc12,
            string mDesc13, string mDesc14, string mDesc15, string mDesc16, string mDesc17, string mDesc18,
           string mDesc19, string mDesc20, string mUserID)
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));

                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }



        public bool UpdateTransInfo3(string comCode, string SQLprocName, string CallType,
          string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
          string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
          string mDesc13 = "", string mDesc14 = "", string mDesc15 = "", string mDesc16 = "", string mDesc17 = "", string mDesc18 = "",
         string mDesc19 = "", string mDesc20 = "", string mDesc21 = "", string mDesc22 = "",
            string mUserID = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));


                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }


        public bool UpdateTransInfo01(string comCode, string SQLprocName, string CallType,
           string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
           string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
           string mDesc13 = "", string mDesc14 = "", string mDesc15 = "", string mDesc16 = "", string mDesc17 = "", string mDesc18 = "",
           string mDesc19 = "", string mDesc20 = "", string mDesc21 = "", string mDesc22 = "", string mDesc23 = "", string mDesc24 = "",
           string mDesc25 = "", string mDesc26 = "", string mDesc27 = "", string mDesc28 = "", string mDesc29 = "", string mDesc30 = "",
           string mDesc31 = "", string mDesc32 = "", string mDesc33 = "", string mDesc34 = "", string mDesc35 = "", string mDesc36 = "",
           string mDesc37 = "", string mDesc38 = "", string mDesc39 = "", string mDesc40 = "", string mDesc41 = "", string mDesc42 = "",
           string mDesc43 = "", string mDesc44 = "", string mDesc45 = "", string mDesc46 = "", string mDesc47 = "", string mDesc48 = "",
           string mDesc49 = "", string mDesc50 = "", string mDesc51 = "", string mDesc52 = "", string mDesc53 = "", string mDesc54 = "", string mDesc55 = "", string mDesc56 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));
                cmd.Parameters.Add(new SqlParameter("@Desc23", mDesc23));
                cmd.Parameters.Add(new SqlParameter("@Desc24", mDesc24));
                cmd.Parameters.Add(new SqlParameter("@Desc25", mDesc25));
                cmd.Parameters.Add(new SqlParameter("@Desc26", mDesc26));
                cmd.Parameters.Add(new SqlParameter("@Desc27", mDesc27));
                cmd.Parameters.Add(new SqlParameter("@Desc28", mDesc28));
                cmd.Parameters.Add(new SqlParameter("@Desc29", mDesc29));
                cmd.Parameters.Add(new SqlParameter("@Desc30", mDesc30));
                cmd.Parameters.Add(new SqlParameter("@Desc31", mDesc31));
                cmd.Parameters.Add(new SqlParameter("@Desc32", mDesc32));
                cmd.Parameters.Add(new SqlParameter("@Desc33", mDesc33));
                cmd.Parameters.Add(new SqlParameter("@Desc34", mDesc34));
                cmd.Parameters.Add(new SqlParameter("@Desc35", mDesc35));
                cmd.Parameters.Add(new SqlParameter("@Desc36", mDesc36));
                cmd.Parameters.Add(new SqlParameter("@Desc37", mDesc37));
                cmd.Parameters.Add(new SqlParameter("@Desc38", mDesc38));
                cmd.Parameters.Add(new SqlParameter("@Desc39", mDesc39));
                cmd.Parameters.Add(new SqlParameter("@Desc40", mDesc40));
                cmd.Parameters.Add(new SqlParameter("@Desc41", mDesc41));
                cmd.Parameters.Add(new SqlParameter("@Desc42", mDesc42));
                cmd.Parameters.Add(new SqlParameter("@Desc43", mDesc43));
                cmd.Parameters.Add(new SqlParameter("@Desc44", mDesc44));
                cmd.Parameters.Add(new SqlParameter("@Desc45", mDesc45));
                cmd.Parameters.Add(new SqlParameter("@Desc46", mDesc46));
                cmd.Parameters.Add(new SqlParameter("@Desc47", mDesc47));
                cmd.Parameters.Add(new SqlParameter("@Desc48", mDesc48));
                cmd.Parameters.Add(new SqlParameter("@Desc49", mDesc49));
                cmd.Parameters.Add(new SqlParameter("@Desc50", mDesc50));
                cmd.Parameters.Add(new SqlParameter("@Desc51", mDesc51));
                cmd.Parameters.Add(new SqlParameter("@Desc52", mDesc52));
                cmd.Parameters.Add(new SqlParameter("@Desc53", mDesc53));
                cmd.Parameters.Add(new SqlParameter("@Desc54", mDesc54));
                cmd.Parameters.Add(new SqlParameter("@Desc55", mDesc55));
                cmd.Parameters.Add(new SqlParameter("@Desc56", mDesc56));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }




        public bool UpdateXmlTransInfo(string comCode, string SQLprocName, string CallType, DataSet parmXml01 = null, DataSet parmXml02 = null, byte[] parmbyte = null,
  string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
             string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
             string mDesc13 = "", string mDesc14 = "", string mDesc15 = "", string mDesc16 = "", string mDesc17 = "", string mDesc18 = "", string mDesc19 = "", string mDesc20 = "", string mDesc21 = "", string mDesc22 = "", string mUserID = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add("@Dxml01", SqlDbType.Xml).Value = (parmXml01 == null ? null : parmXml01.GetXml());
                cmd.Parameters.Add("@Dxml02", SqlDbType.Xml).Value = (parmXml02 == null ? null : parmXml02.GetXml());
                cmd.Parameters.Add(new SqlParameter("@Dbin01", parmbyte));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));
                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }


        public bool UpdateTransInfoHRSal(string comCode, string SQLprocName, string CallType,
          string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "",
          string mDesc7 = "", string mDesc8 = "", string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "",
          string mDesc13 = "", string mDesc14 = "", string mDesc15 = "", string mDesc16 = "", string mDesc17 = "", string mDesc18 = "",
          string mDesc19 = "", string mDesc20 = "", string mDesc21 = "", string mDesc22 = "", string mDesc23 = "", string mDesc24 = "",
          string mDesc25 = "", string mDesc26 = "", string mDesc27 = "", string mDesc28 = "", string mDesc29 = "", string mDesc30 = "",
          string mDesc31 = "", string mDesc32 = "", string mDesc33 = "", string mDesc34 = "", string mDesc35 = "", string mDesc36 = "",
          string mDesc37 = "", string mDesc38 = "", string mDesc39 = "", string mDesc40 = "", string mDesc41 = "", string mDesc42 = "",
          string mDesc43 = "", string mDesc44 = "", string mDesc45 = "", string mDesc46 = "", string mDesc47 = "", string mDesc48 = "",
          string mDesc49 = "", string mDesc50 = "", string mDesc51 = "", string mDesc52 = "", string mDesc53 = "", string mDesc54 = "",
          string mDesc55 = "", string mDesc56 = "", string mDesc57 = "", string mDesc58 = "", string mDesc59 = "", string mDesc60 = "",
          string mDesc61 = "", string mDesc62 = "", string mDesc63 = "", string mDesc64 = "", string mDesc65 = "", string mDesc66 = "",
          string mDesc67 = "", string mDesc68 = "", string mDesc69 = "", string mDesc70 = "", string mDesc71 = "", string mDesc72 = "",
          string mDesc73 = "", string mDesc74 = "", string mDesc75 = "", string mDesc76 = "", string mDesc77 = "", string mDesc78 = "",
          string mDesc79 = "", string mDesc80 = "", string mDesc81 = "", string mDesc82 = "", string mDesc83 = "", string mDesc84 = "",
          string mDesc85 = "", string mDesc86 = "", string mDesc87 = "", string mDesc88 = "", string mDesc89 = "", string mDesc90 = "",
          string mDesc91 = "", string mDesc92 = "", string mDesc93 = "", string mDesc94 = "", string mDesc95 = "", string mDesc96 = "",
          string mDesc97 = "", string mDesc98 = "", string mDesc99 = "", string mDesc100 = "", string mDesc101 = "", string mDesc102 = "",

          string mUserID = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));
                cmd.Parameters.Add(new SqlParameter("@Desc23", mDesc23));
                cmd.Parameters.Add(new SqlParameter("@Desc24", mDesc24));
                cmd.Parameters.Add(new SqlParameter("@Desc25", mDesc25));
                cmd.Parameters.Add(new SqlParameter("@Desc26", mDesc26));
                cmd.Parameters.Add(new SqlParameter("@Desc27", mDesc27));
                cmd.Parameters.Add(new SqlParameter("@Desc28", mDesc28));
                cmd.Parameters.Add(new SqlParameter("@Desc29", mDesc29));
                cmd.Parameters.Add(new SqlParameter("@Desc30", mDesc30));
                cmd.Parameters.Add(new SqlParameter("@Desc31", mDesc31));
                cmd.Parameters.Add(new SqlParameter("@Desc32", mDesc32));
                cmd.Parameters.Add(new SqlParameter("@Desc33", mDesc33));
                cmd.Parameters.Add(new SqlParameter("@Desc34", mDesc34));
                cmd.Parameters.Add(new SqlParameter("@Desc35", mDesc35));
                cmd.Parameters.Add(new SqlParameter("@Desc36", mDesc36));
                cmd.Parameters.Add(new SqlParameter("@Desc37", mDesc37));
                cmd.Parameters.Add(new SqlParameter("@Desc38", mDesc38));
                cmd.Parameters.Add(new SqlParameter("@Desc39", mDesc39));
                cmd.Parameters.Add(new SqlParameter("@Desc40", mDesc40));
                cmd.Parameters.Add(new SqlParameter("@Desc41", mDesc41));
                cmd.Parameters.Add(new SqlParameter("@Desc42", mDesc42));
                cmd.Parameters.Add(new SqlParameter("@Desc43", mDesc43));
                cmd.Parameters.Add(new SqlParameter("@Desc44", mDesc44));
                cmd.Parameters.Add(new SqlParameter("@Desc45", mDesc45));
                cmd.Parameters.Add(new SqlParameter("@Desc46", mDesc46));
                cmd.Parameters.Add(new SqlParameter("@Desc47", mDesc47));
                cmd.Parameters.Add(new SqlParameter("@Desc48", mDesc48));
                cmd.Parameters.Add(new SqlParameter("@Desc49", mDesc49));
                cmd.Parameters.Add(new SqlParameter("@Desc50", mDesc50));
                cmd.Parameters.Add(new SqlParameter("@Desc51", mDesc51));
                cmd.Parameters.Add(new SqlParameter("@Desc52", mDesc52));
                cmd.Parameters.Add(new SqlParameter("@Desc53", mDesc53));
                cmd.Parameters.Add(new SqlParameter("@Desc54", mDesc54));
                cmd.Parameters.Add(new SqlParameter("@Desc55", mDesc55));
                cmd.Parameters.Add(new SqlParameter("@Desc56", mDesc56));
                cmd.Parameters.Add(new SqlParameter("@Desc57", mDesc57));
                cmd.Parameters.Add(new SqlParameter("@Desc58", mDesc58));
                cmd.Parameters.Add(new SqlParameter("@Desc59", mDesc59));
                cmd.Parameters.Add(new SqlParameter("@Desc60", mDesc60));
                cmd.Parameters.Add(new SqlParameter("@Desc61", mDesc61));
                cmd.Parameters.Add(new SqlParameter("@Desc62", mDesc62));
                cmd.Parameters.Add(new SqlParameter("@Desc63", mDesc63));
                cmd.Parameters.Add(new SqlParameter("@Desc64", mDesc64));
                cmd.Parameters.Add(new SqlParameter("@Desc65", mDesc65));
                cmd.Parameters.Add(new SqlParameter("@Desc66", mDesc66));
                cmd.Parameters.Add(new SqlParameter("@Desc67", mDesc67));
                cmd.Parameters.Add(new SqlParameter("@Desc68", mDesc68));
                cmd.Parameters.Add(new SqlParameter("@Desc69", mDesc69));
                cmd.Parameters.Add(new SqlParameter("@Desc70", mDesc70));
                cmd.Parameters.Add(new SqlParameter("@Desc71", mDesc71));
                cmd.Parameters.Add(new SqlParameter("@Desc72", mDesc72));
                cmd.Parameters.Add(new SqlParameter("@Desc73", mDesc73));
                cmd.Parameters.Add(new SqlParameter("@Desc74", mDesc74));
                cmd.Parameters.Add(new SqlParameter("@Desc75", mDesc75));
                cmd.Parameters.Add(new SqlParameter("@Desc76", mDesc76));
                cmd.Parameters.Add(new SqlParameter("@Desc77", mDesc77));
                cmd.Parameters.Add(new SqlParameter("@Desc78", mDesc78));
                cmd.Parameters.Add(new SqlParameter("@Desc79", mDesc79));
                cmd.Parameters.Add(new SqlParameter("@Desc80", mDesc80));
                cmd.Parameters.Add(new SqlParameter("@Desc81", mDesc81));
                cmd.Parameters.Add(new SqlParameter("@Desc82", mDesc82));
                cmd.Parameters.Add(new SqlParameter("@Desc83", mDesc83));
                cmd.Parameters.Add(new SqlParameter("@Desc84", mDesc84));
                cmd.Parameters.Add(new SqlParameter("@Desc85", mDesc85));
                cmd.Parameters.Add(new SqlParameter("@Desc86", mDesc86));
                cmd.Parameters.Add(new SqlParameter("@Desc87", mDesc87));
                cmd.Parameters.Add(new SqlParameter("@Desc88", mDesc88));
                cmd.Parameters.Add(new SqlParameter("@Desc89", mDesc89));
                cmd.Parameters.Add(new SqlParameter("@Desc90", mDesc90));
                cmd.Parameters.Add(new SqlParameter("@Desc91", mDesc91));
                cmd.Parameters.Add(new SqlParameter("@Desc92", mDesc92));
                cmd.Parameters.Add(new SqlParameter("@Desc93", mDesc93));
                cmd.Parameters.Add(new SqlParameter("@Desc94", mDesc94));
                cmd.Parameters.Add(new SqlParameter("@Desc95", mDesc95));
                cmd.Parameters.Add(new SqlParameter("@Desc96", mDesc96));
                cmd.Parameters.Add(new SqlParameter("@Desc97", mDesc97));
                cmd.Parameters.Add(new SqlParameter("@Desc98", mDesc98));
                cmd.Parameters.Add(new SqlParameter("@Desc99", mDesc99));
                cmd.Parameters.Add(new SqlParameter("@Desc100", mDesc100));
                cmd.Parameters.Add(new SqlParameter("@Desc101", mDesc101));
                cmd.Parameters.Add(new SqlParameter("@Desc102", mDesc102));
                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));

                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }


        public bool UpdateTransHREMPInfo3(string comCode, string SQLprocName, string CallType,
       string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6,
       string mDesc7, string mDesc8, string mDesc9, string mDesc10, string mDesc11, string mDesc12,
       string mDesc13, string mDesc14, string mDesc15, string mDesc16, string mDesc17, string mDesc18,
      string mDesc19, string mDesc20, string mDesc21, string mDesc22, string mDesc23, string mDesc24,
            string mDesc25, string mUserID, string mDesc26 = "", string mDesc27 = "", string mDesc28 = "", string mDesc29 = "", string mDesc30 = "", string mDesc31 = "", string mDesc32 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));
                cmd.Parameters.Add(new SqlParameter("@Desc23", mDesc23));
                cmd.Parameters.Add(new SqlParameter("@Desc24", mDesc24));
                cmd.Parameters.Add(new SqlParameter("@Desc25", mDesc25));

                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                cmd.Parameters.Add(new SqlParameter("@Desc26", mDesc26));
                cmd.Parameters.Add(new SqlParameter("@Desc27", mDesc27));
                cmd.Parameters.Add(new SqlParameter("@Desc28", mDesc28));
                cmd.Parameters.Add(new SqlParameter("@Desc29", mDesc29));
                cmd.Parameters.Add(new SqlParameter("@Desc30", mDesc30));
                cmd.Parameters.Add(new SqlParameter("@Desc31", mDesc31));
                cmd.Parameters.Add(new SqlParameter("@Desc32", mDesc32));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }



        public bool InsertClientPhoto(string comCode, string empid, byte[] image, byte[] sign)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into hrimginf (comcod, empid,  empimage, empsign) values (@Comp1, @Desc1, @Desc2, @Desc3)";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", image);
                cmd.Parameters.AddWithValue("@Desc3", sign);

                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;

            }
        }


        public bool UpdateClientPhoto(string comCode, string empid, byte[] image, byte[] sign)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update hrimginf set empimage=@Desc2, empsign=@Desc3 where comcod=@Comp1 and empid=@Desc1";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", image);
                cmd.Parameters.AddWithValue("@Desc3", sign);

                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }
        }


        public bool UpdateClientPhotoonly(string comCode, string empid, byte[] image)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update hrimginf set empimage=@Desc2 where comcod=@Comp1 and empid=@Desc1";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", image);


                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }
        }
        public bool UpdateClientSignOnly(string comCode, string empid, byte[] sign)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update hrimginf set empsign=@Desc2 where comcod=@Comp1 and empid=@Desc1";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", sign);

                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }
        }

        public bool InsertUserPhoto(string comCode, string empid, byte[] image, byte[] sign)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into usrimginf (comcod, usrid,  usrimg, usrsign) values (@Comp1, @Desc1, @Desc2, @Desc3)";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", image);
                cmd.Parameters.AddWithValue("@Desc3", sign);
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;

            }
        }

        public bool UpdateUserPhoto(string comCode, string empid, byte[] image, byte[] sign)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update usrimginf set usrimg=@Desc2, usrsign=@Desc3 where comcod=@Comp1 and usrid=@Desc1";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", empid);
                cmd.Parameters.AddWithValue("@Desc2", image);
                cmd.Parameters.AddWithValue("@Desc3", sign);
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }
        }



        public DataSet GetTransInfoNew(string comCode, string SQLprocName, string CallType, DataSet parmXml01 = null, DataSet parmXml02 = null, byte[] parmBin01 = null,
     string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "", string mDesc7 = "", string mDesc8 = "",
     string mDesc9 = "", string mDesc10 = "", string mDesc11 = "", string mDesc12 = "", string mDesc13 = "", string mDesc14 = "", string mDesc15 = "",
     string mDesc16 = "", string mDesc17 = "", string mDesc18 = "", string mDesc19 = "", string mDesc20 = "", string @userid = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add("@Dxml01", SqlDbType.Xml).Value = (parmXml01 == null ? null : parmXml01.GetXml());
                cmd.Parameters.Add("@Dxml02", SqlDbType.Xml).Value = (parmXml02 == null ? null : parmXml02.GetXml());
                cmd.Parameters.Add(new SqlParameter("@Dbin01", parmBin01));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@UserID", @userid));


                DataSet result = _dataAccess.GetDataSet(cmd);
                if (result == null)  //_result==false
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


        public bool UpdateTransInfoDoc(string comCode, string SQLprocName, string CallType,
            string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6,
            string mDesc7, string mDesc8, string mDesc9, byte[] mDesc10, string mDesc11, string mDesc12,
            string mDesc13, string mDesc14, string mUserID)
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }



        public bool UpdateTransInfoXML(string comCode, string SQLprocName, string CallType,
          string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6,
          string mDesc7, string mDesc8, string mDesc9, string mDesc10, string mDesc11, string mDesc12,
          string mDesc13, string mDesc14, string mDesc15, string mDesc16, string mDesc17, string mDesc18,
         string mDesc19, string mDesc20, string mDesc21, string mDesc22, string mUserID)
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", mDesc12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", mDesc13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", mDesc14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", mDesc15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", mDesc16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", mDesc17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", mDesc18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", mDesc19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", mDesc20));
                cmd.Parameters.Add(new SqlParameter("@Desc21", mDesc21));
                cmd.Parameters.Add(new SqlParameter("@Desc22", mDesc22));

                cmd.Parameters.Add(new SqlParameter("@UserID", mUserID));
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }


        public bool InsertCompPhoto(string comCode, byte[] image)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into GETCOMPID (comcod,  comlogo) values (@Comp1, @Desc1)";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", image);
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;


            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;

            }
        }
        public bool UpdateCompPhoto(string comCode, byte[] image)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update compinf set comlogo=@Desc1 where comcod=@Comp1";
                cmd.Parameters.AddWithValue("@Comp1", comCode);
                cmd.Parameters.AddWithValue("@Desc1", image);
                bool _result = _dataAccess.ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError(_dataAccess.ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }
        }



        public bool SysExpiryUpdate(string comCode, DateTime date)
        {
            try
            {

                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";


                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("update Tbl_Company set EXPDATE='" + date + "' where ID=" + comCode, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet GetcheckUser(string comcode, string userId)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.Parameters.Add(new SqlParameter("@Comp1", comcode));
                        command.Parameters.Add(new SqlParameter("@CallType", "GETCHECKUER"));
                        command.Parameters.Add(new SqlParameter("@Desc1", userId));
                        command.Connection = connection;
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            SqlDataAdapter adp = new SqlDataAdapter();
                            adp.SelectCommand = command;

                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            return ds;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }

                    }

                }
                catch (Exception ex)
                {
                    this.SetError(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }

        public bool InsertTicket(string companyType, string txtTicketDesc, string ticketType, string taskProgress, string priority, string createTask, string username, string txtRemarks, string proposedUser, string compUser)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(" insert into tbl_tasks (comcod,taskdesc,tasktype,taskprogress, taskpriority, createtask ,createuser, remarks, proposeduser, comuser) values ('" + companyType.Trim() + "', '" + txtTicketDesc.Trim() + "', '" + ticketType.Trim() + "', '" + taskProgress.Trim() + "', '" + priority.Trim() + "', '" + createTask.Trim() + "', '" + username.Trim() + "', '" + txtRemarks.Trim() + "','" + proposedUser.Trim() + "','" + compUser + "')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataSet InsertTicketSP(string comcod, string txtTicketDesc, string ticketType, string taskProgress, string priority, string createTask, string username, string txtRemarks, string proposedUser, string compUser)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Comp1", comcod));
                        command.Parameters.Add(new SqlParameter("@CallType", "INSERTTICKETS"));
                        command.Parameters.Add(new SqlParameter("@Desc1", txtTicketDesc));
                        command.Parameters.Add(new SqlParameter("@Desc2", ticketType));
                        command.Parameters.Add(new SqlParameter("@Desc3", taskProgress));
                        command.Parameters.Add(new SqlParameter("@Desc4", priority));
                        command.Parameters.Add(new SqlParameter("@Desc5", createTask));
                        command.Parameters.Add(new SqlParameter("@Desc6", username));
                        command.Parameters.Add(new SqlParameter("@Desc7", txtRemarks));
                        command.Parameters.Add(new SqlParameter("@Desc8", proposedUser));
                        command.Parameters.Add(new SqlParameter("@Desc9", compUser));

                        command.Connection = connection;
                        try
                        {
                            DataSet result = _dataAccess.GetDataSetTicket(command);
                            if (result == null)  //_result==false
                            {
                                this.SetError(_dataAccess.ErrorObject);
                            }
                            return result;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }

                    }






                }
                catch (Exception ex)
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool InsertTicketAttach(string comcod, string ticketId, string attachPath, string postedDate, string userId, string terminal, string refId)
        {
            try
            {

                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(" insert into tbl_tasks_attach (comcod,ticketId,attachPath, postedDate, userId ,terminal, refId) " +
                            "values ('" + comcod.Trim() + "', '" + ticketId.Trim() + "', '" + attachPath.Trim() + "', '" + postedDate.Trim() + "', '" + userId.Trim() + "', '" + terminal.Trim() + "', '" + refId.Trim() + "')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataSet GetTicketData(string comCod)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.Parameters.Add(new SqlParameter("@Comp1", comCod));
                        command.Parameters.Add(new SqlParameter("@CallType", "GETTICKETS"));
                        command.Connection = connection;
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            SqlDataAdapter adp = new SqlDataAdapter();
                            adp.SelectCommand = command;

                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            return ds;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }

                    }

                }
                catch (Exception ex)
                {
                    this.SetError(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }

        public bool UpdateTicket(string comCod, string createuser, string ticketType, string cdate, string ticketId, string ticketProgress, string ticketBack)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.Parameters.Add(new SqlParameter("@Comp1", comCod));
                        command.Parameters.Add(new SqlParameter("@CallType", "UPDATETASKINFO_QCDONE"));
                        command.Parameters.Add(new SqlParameter("@Desc1", ""));
                        command.Parameters.Add(new SqlParameter("@Desc2", ""));
                        command.Parameters.Add(new SqlParameter("@Desc3", cdate));
                        command.Parameters.Add(new SqlParameter("@Desc4", createuser));
                        command.Parameters.Add(new SqlParameter("@Desc5", ticketId));
                        command.Parameters.Add(new SqlParameter("@Desc6", ticketProgress));
                        command.Parameters.Add(new SqlParameter("@Desc7", ticketBack));
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool TicketBack(string comCod, string createuser, string ticketType, string cdate, string ticketId, string ticketProgress, string ticketBack)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.Parameters.Add(new SqlParameter("@Comp1", comCod));
                        command.Parameters.Add(new SqlParameter("@CallType", "UPDATETASKINFO_WPBACK"));
                        command.Parameters.Add(new SqlParameter("@Desc1", ""));
                        command.Parameters.Add(new SqlParameter("@Desc2", ""));
                        command.Parameters.Add(new SqlParameter("@Desc3", cdate));
                        command.Parameters.Add(new SqlParameter("@Desc4", createuser));
                        command.Parameters.Add(new SqlParameter("@Desc5", ticketId));
                        command.Parameters.Add(new SqlParameter("@Desc6", ticketProgress));
                        command.Parameters.Add(new SqlParameter("@Desc7", ticketBack));
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataSet GetTicketDataByID(string comcod, string ticketID)
        {
            try
            {
                string connectionString = @"Server=123.200.23.58\MSSQL2K14;Database=DB_PINBOARD;uid=sa;pwd=@*asit1qaz`123#;";

                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_TASK_MANAGMENT";
                        command.Parameters.Add(new SqlParameter("@Comp1", comcod));
                        command.Parameters.Add(new SqlParameter("@CallType", "GETTASKSDATABYID"));
                        command.Parameters.Add(new SqlParameter("@Desc1", ticketID));
                        command.Connection = connection;
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            SqlDataAdapter adp = new SqlDataAdapter();
                            adp.SelectCommand = command;

                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            return ds;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }

                    }

                }
                catch (Exception ex)
                {
                    this.SetError(ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
    }

}
