//using BDACCRDLC.C_81_Hrm.C_89_Pay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Data;
using Microsoft.Reporting.WinForms;
using RealEntity;


namespace RealERPRDLC
{
    public class RptHRSetup
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("RealERPRDLC." + RptName + ".rdlc");
            LocalReport Rpt1a = new LocalReport();
            Rpt1a.DisplayName = RptName;
            Rpt1a.LoadReportDefinition(stream1);
            Rpt1a.DataSources.Clear();

            switch (Rpt1a.DisplayName.Trim())
            {
               
              
                #region RD_84_leav
                case "R_81_Hrm.R_84_Lea.RptLeaveApp": Rpt1a = SetRptLeaveApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.rptEmpLeaveCard": Rpt1a = SetrptEmpLeaveCard(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_84_Lea.rptBirthday": Rpt1a = setRptBirthday(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion
                case "R_81_Hrm.R_83_Att.RptNewEmpStatus": Rpt1a = SetRptEmpStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptNewEmpStatusCPDL": Rpt1a = SetRptNewEmpStatusCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptNewEmpStatusEdi": Rpt1a = SetRptEmpStatusEdi(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                // case "R_81_Hrm.R_83_Att.RptMonAttendance": Rpt1a = SetRptMonAttendance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_85_Lon.rptLoanApp": Rpt1a = SetRptLoanApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #region RD_89_Pay
                case "R_81_Hrm.R_89_Pay.RptSalarySheet": Rpt1a = SetRptSalarySheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBankFordLetter": Rpt1a = SetRptBankFord(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion

                #region Attendance
                case "R_81_Hrm.R_83_Att.RptAttnLog": Rpt1a = SetRptAttnLog(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.LetterDefault01": Rpt1a = SetRptLetterDefault(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.DailyAttenSumry": Rpt1a = SetRptDailyAttenSumry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptDailyAttendenceCHL": Rpt1a = SetRptDailyAttendenceCHL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptAitPurpose": Rpt1a = SetRptAitPurpose(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptAitPurposePeb": Rpt1a = SetRptAitPurposePeb(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptEmpAitCertificate": Rpt1a = SetRptEmpAitCertificate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpMonthSumm": Rpt1a = SetRptEmpMonthSumm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_Mkt.RptSourceWiseLeads": Rpt1a = SetRptSourceWiseLeads(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                
                //Attendence by Parbaz
                case "R_81_Hrm.R_83_Att.RptMonAttendance": Rpt1a = SetRptMonAttendance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonAttendanceAlli": Rpt1a = SetRptMonAttendanceAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptDailyAllEmpAttn": Rpt1a = SetRptDailyAllEmpAttn(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptDailyAllEmpAttn02": Rpt1a = SetRptDailyAllEmpAttn02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptHRMonthlyLateSumBR": Rpt1a = SetRptHRMonthlyLateSumBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptHRMonthlyLateSum": Rpt1a = SetRptHRMonthlyLateSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.rptMonthyLateAttnEmp": Rpt1a = SetRptMonthyLateAttnEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.rptMonthyEarlyLeaveEmp": Rpt1a = SetRptMonthyEarlyLeaveEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptAttendenceSheetEarly": Rpt1a = SetRptAttendenceSheetEarly(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonAttendanceBTI": Rpt1a = SetRptMonAttendanceBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonAttendanceBTIEXCEL": Rpt1a = SetRptMonAttendanceBTIEXCEL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_83_Att.RptMonAttendanceBTI02": Rpt1a = SetRptMonAttendanceBTI02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonAttendanceBTI02EXCEL": Rpt1a = SetRptMonAttendanceBTI02EXCEL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                //LeaveApp by Parbaz
                case "R_81_Hrm.R_84_Lea.EmpLeavApp": Rpt1a = SetEmpLeavApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptHrEmpLeave02": Rpt1a = SetRptHrEmpLeave02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptHrEmpLeave03": Rpt1a = SetRptHrEmpLeave03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.rptYearlyHoliday": Rpt1a = SetrptYearlyHoliday(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.rptYearlyHolidayGov": Rpt1a = SetrptYearlyHolidayGov(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_90_PF.RptMonthWisePFAlliance": Rpt1a = SetRptPFAlliance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_90_PF.RptProvidedFund": Rpt1a = SetRptProvidedFund(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_93_AnnInc.RptIncrementStatus": Rpt1a = SetRptIncrementStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                    #endregion

                    /// test 
                case "R_81_Hrm.Rnd": Rpt1a = SetupRND(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                /// 


                //epic monthly attendance create by robi
                case "R_81_Hrm.R_83_Att.RptMonAttendanceEPIC": Rpt1a = SetRptMonAttendanceEPIC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

            }
            Rpt1a.Refresh();
            return Rpt1a;
        }
        private static LocalReport SetRptMonAttendance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            //rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenter>)rptDataSet2));
            return rpt1a;
        }
        private static LocalReport SetRptSourceWiseLeads(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenterLeads>)rptDataSet));
            //rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenter>)rptDataSet2));
            return rpt1a;
        }

        //private static LocalReport SetRptMonAttendance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        //{
        //    rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
        //    return rpt1a;
        //}
        private static LocalReport SetRptMonAttendanceAlli(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptDailyAllEmpAttn(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptDailyAllEmpAttn02(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptHRMonthlyLateSumBR(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptHRMonthlyLateSum(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMonthyLateAttnEmp(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.EmpSatausLate>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMonthyEarlyLeaveEmp(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.EmpSatausLate>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptAttendenceSheetEarly(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.DailyLate>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetEmpLeavApp(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptHrEmpLeave02(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptProvidedFund(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_90_PF.ProvidedFund>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptPFAlliance(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_90_PF.PFAlliance>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptIncrementStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_93_AnnInc.AnnIncReport.AnnualIncrementStatus>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptHrEmpLeave03(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetrptYearlyHoliday(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.yearlyholiday>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetrptYearlyHolidayGov(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.yearlyholiday>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptDailyAttendenceCHL(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLDayWize>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpStatusEdi(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpAttnIdWise>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptLoanApp(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpBasicInf>)rptDataSet));
            return rpt1a;
        }
        
        private static LocalReport SetRptEmpStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpAttnIdWise>)rptDataSet));
            return rpt1a;
        } 
        private static LocalReport SetRptNewEmpStatusCPDL(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpAttnIdWise>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptEmpAitCertificate(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptEmpMonthSumm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EmpMonthSummary>)RptDataSet));
            return Rpt1a;
        }



        
        private static LocalReport SetRptAitPurposePeb(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAitPurpose(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyAttenSumry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<MFGOBJ.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttnSumry>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAttnLog(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.EmpAttendncLog>)RptDataSet));
            return Rpt1a;
        }

        
        private static LocalReport SetRptLetterDefault(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            return Rpt1a;
        }

        private static LocalReport SetRptSalarySheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Hashtable reportP = (Hashtable)RptDataSet2;
          

            //Rpt1a.SetParameters(new ReportParameter("netpay", (string)reportP["netpay"]));
            //Rpt1a.SetParameters(new ReportParameter("netpayatax", (string)reportP["netpayatax"]));
            //Rpt1a.SetParameters(new ReportParameter("CompName", (string)reportP["CompName"]));
            //Rpt1a.SetParameters(new ReportParameter("txtccaret", (string)reportP["txtccaret"]));
            //Rpt1a.SetParameters(new ReportParameter("txtl1", (string)reportP["txtl1"]));
            //Rpt1a.SetParameters(new ReportParameter("txtl2", (string)reportP["txtl2"]));
            //Rpt1a.SetParameters(new ReportParameter("txttk", (string)reportP["txttk"]));
            //Rpt1a.SetParameters(new ReportParameter("frmdate", (string)reportP["frmdate"]));
            //Rpt1a.SetParameters(new ReportParameter("todate", (string)reportP["todate"]));
            //Rpt1a.SetParameters(new ReportParameter("statementofs", (string)reportP["statementofs"]));
            //Rpt1a.SetParameters(new ReportParameter("preparedby", (string)reportP["preparedby"]));
            //Rpt1a.SetParameters(new ReportParameter("checkedby", (string)reportP["checkedby"]));
            //Rpt1a.SetParameters(new ReportParameter("approvedby", (string)reportP["approvedby"]));      
            //Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));
            //Rpt1a.DataSources.Add(new ReportDataSource("DataSalarySheet", (List<RealERPRDLC.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBankFord(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Hashtable reportP = (Hashtable)RptDataSet2;
            ////Rpt1a.EnableExternalImages = true;
            ////byte[] logo = (byte[])reportP["ComLogo"];
            ////Rpt1a.SetParameters(new ReportParameter("ComLogo", Convert.ToBase64String(logo)));

            //Rpt1a.SetParameters(new ReportParameter("BankAdd", (string)reportP["BankAdd"]));
            //Rpt1a.SetParameters(new ReportParameter("subject", (string)reportP["subject"]));
            //Rpt1a.SetParameters(new ReportParameter("Det1", (string)reportP["Det1"]));
            //Rpt1a.SetParameters(new ReportParameter("Det2", (string)reportP["Det2"]));
            //Rpt1a.SetParameters(new ReportParameter("Det3", (string)reportP["Det3"]));
            //Rpt1a.SetParameters(new ReportParameter("Footer", (string)reportP["Footer"]));
            //Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));
            //Rpt1a.DataSources.Add(new ReportDataSource("DataSetFrdLetter", (List<RealERPRDLC.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.BankFord>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptLeaveApp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Hashtable reportP = (Hashtable)RptDataSet2;

            //Rpt1a.SetParameters(new ReportParameter("CompName", (string)reportP["comnam"]));
            //Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["headertxt"]));
            //Rpt1a.SetParameters(new ReportParameter("Footer", ""));

            //Rpt1a.SetParameters(new ReportParameter("Empname", (string)reportP["Empname"]));
            //Rpt1a.SetParameters(new ReportParameter("depertment", (string)reportP["depertment"]));
            //Rpt1a.SetParameters(new ReportParameter("empId", (string)reportP["empId"]));
            //Rpt1a.SetParameters(new ReportParameter("Designation", (string)reportP["Designation"]));
            //Rpt1a.SetParameters(new ReportParameter("Locaion", (string)reportP["Locaion"]));
            //Rpt1a.DataSources.Add(new ReportDataSource("DSLeaveApp", (List<RealERPRDLC.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.LeaveApp>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport SetRptMonAttendanceBTI02(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMonAttendanceBTI02EXCEL(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMonAttendanceEPIC(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.RptMntAttenReport>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptMonAttendanceBTI(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptMonAttendanceBTIEXCEL(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetrptEmpLeaveCard(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveRule>)rptDataSet));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.currentLeaveInfo>)rptDataSet2));
            rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.currentLeaveInfo>)userDataset));

            return rpt1a;
        }


        private static LocalReport setRptBirthday(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.birthdayDate>)rptDataSet));

            return rpt1a;
        }

        private static LocalReport SetupRND(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.IndvPf.Empinfo>)rptDataSet));

            return rpt1a;
        }


        
    }
}
