using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RealEntity;
using System.Collections;

namespace RealERPRDLC
{
   public class RDLCAccountSetup
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

                #region Inventory
                case "R_12_Inv.rptTopSheetCashPurchase": Rpt1a = SetrptTopSheetCashPurchase(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion


                case "R_09_PIMP.RptSubConWrkOrder": Rpt1a = RptSubConWrkOrder (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptsubConBillPrj": Rpt1a = RptsubConBillPrj (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptAccountCode2": Rpt1a = RptAccountcode2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintSubBill": Rpt1a = rptPrintSubBill (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSalPerWise": Rpt1a = RptSalPerWise (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSalPerProjWise": Rpt1a = RptSalPerProjWise (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonWiseCol": Rpt1a = RptMonWiseCol (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonColBarChart": Rpt1a = RptMonColBarChart (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonColLineChart": Rpt1a = RptMonColLineChart (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonSalesBarChart": Rpt1a = RptMonSalesBarChart (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonSalesLineChart": Rpt1a = RptMonSalesLineChart (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankInterest": Rpt1a = RptBankInterest (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptFlrWiseConPrg": Rpt1a = RptFlrWiseConPrg (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRecAndPayment": Rpt1a = SetRptRecAndPayment (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonWiseColBuyer": Rpt1a = SetRptMonWiseColBuyer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRecAndPaymentCredence": Rpt1a = SetRptRecAndPaymentCredence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;  
                case "R_17_Acc.RptRecAndPaymentEntrust": Rpt1a = SetRptRecAndPaymentEntrust(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;  
                case "R_17_Acc.RptRecAndPaymentCube": Rpt1a = SetRptRecAndPaymentCube(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;  
                case "R_17_Acc.RptRecAndPayCustomized": Rpt1a = SetRptRecAndPayCustomized(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_21_Mkt.RptClietList": Rpt1a = RptClietList (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_81_Rec.RptCreateOffLt": Rpt1a = RptCreateOffLt (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_81_Rec.RptCreateOffLtAcme": Rpt1a = SetRptCreateOffLtAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_81_Rec.RptEmpAss": Rpt1a = RptEmpAss (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
              //  case "R_81_Hrm.R_91_ACR.RptEmpEvaluation": Rpt1a = RptEmpEvaluation (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_81_Rec.RptConfmlt": Rpt1a = RptConfmlt (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptConAssess": Rpt1a = RptConAssess (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCustApp": Rpt1a = RptCustApp (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptBookingApp2": Rpt1a = RptBookingApp2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSaleDeclaration": Rpt1a = RptSaleDeclaration(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCustomerinf": Rpt1a = RptCustomerinf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSuppAss": Rpt1a = RptSuppAss (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBundle": Rpt1a = RptBundle (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSuplierBgd": Rpt1a = RptSuplierBgd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillRegTrack": Rpt1a = RptBillRegTrack (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupPayment": Rpt1a = RptSupPayment (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptOrderStatus": Rpt1a = RptOrderStatus (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPOMRRBillStatus": Rpt1a = RptPOMRRBillStatus (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurSumVsPay": Rpt1a = RptPurSumVsPay (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_04_Bgd.RptPrjFloorWise": Rpt1a = RptPrjFloorWise (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptConBundle": Rpt1a = RptConBundle (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProBalSheet": Rpt1a = RptProBalSheet (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptBgdVsAct": Rpt1a = RptBgdVsAct (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_32_Mis.RptProBalSheetCredence": Rpt1a = RptProBalSheetCredence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_99_AllInterface.RptSalesJournal": Rpt1a = RptSalesJournal (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptCkhDeposit": Rpt1a = RptCkhDeposit (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptUpdateCol": Rpt1a = RptUpdateCol (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptUpdatePur": Rpt1a = RptUpdatePur (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptConBillUp": Rpt1a = RptConBillUp (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptUpdateMatTrans": Rpt1a = RptUpdateMatTrans (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptReqSts": Rpt1a = RptReqSts (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.Rptcrateappanorder": Rpt1a = Rptcrateappanorder (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptPurOrder": Rpt1a = RptPurOrder (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptCashRcv": Rpt1a = RptCashRcv (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptBillCon": Rpt1a = RptBillCon (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_89_Payroll.RptSecuritySalarySuvstu": Rpt1a = RptSecuritySalarySuvstu (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptAddTopSheet": Rpt1a = RptAddTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptDailyLateAtt": Rpt1a = RptDailyLateAtt (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.Overtime": Rpt1a = Overtime(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalDisbursement": Rpt1a = RptSalDisbursement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_84_Lea.RptEmpLeavStatus": Rpt1a = SetRptEmpLeavStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
              //  case "R_81_Hrm.R_84_Lea.RptMonWiseEmpLeave": Rpt1a = SetRptMonWiseEmpLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptMonWiseEmpLeaveBR": Rpt1a = SetRptMonWiseEmpLeaveBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                
                

                    
                #region KPI
                case "R_47_kpi.RptKeyResultArea": Rpt1a = SetRptKeyResultArea(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion
            }
            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport RptProBalSheetCredence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptBalSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptKeyResultArea(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_47_Kpi.EclassKeyResult>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_47_Kpi.EclassKRANOte>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptRecAndPaymentCredence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptAddTopSheet(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddTopSheet>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptDailyLateAtt ( LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset )
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.DailyLate>)rptDataSet));
            return rpt1a;
        }
       
        private static LocalReport SetrptTopSheetCashPurchase(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassTopSheetCashPurchase>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRecAndPayment ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRecAndPayCustomized(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRecAndPaymentCube( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>)RptDataSet));
            return Rpt1a;
        }
        
        private static LocalReport SetRptRecAndPaymentEntrust( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonWiseColBuyer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseColBuyer>)RptDataSet));
            return Rpt1a;
        }
       
        private static LocalReport RptCreateOffLt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Hashtable hshParm = (Hashtable)RptDataSet;

            //test dsfdsfdsf s
            //Rpt1a.SetParameters(new ReportParameter("companyname", hshParm["companyname"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.CreateOffLt>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_81_Rec.SalInfo>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_81_Hrm.C_81_Rec.CreateOffLt>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptCreateOffLtAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            //Hashtable hshParm = (Hashtable)RptDataSet;
            //test dsfdsfdsf s
            //Rpt1a.SetParameters(new ReportParameter("companyname", hshParm["companyname"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.CreateOffLt>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_81_Rec.SalInfo>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_81_Hrm.C_81_Rec.CreateOffLt>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport RptEmpAss(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.EmpAssesment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport Overtime(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EmpMonthSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptSalDisbursement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.Disbursement>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.DisbursementSummary>)RptDataSet2));
            return Rpt1a;
        }
       
        //private static LocalReport RptEmpEvaluation ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        //{

        //    Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_81_Hrm.C_91_ACR.EMpEvaluation>)RptDataSet));
        //    Rpt1a.DataSources.Add (new ReportDataSource ("DataSet2", (List<RealEntity.C_81_Hrm.C_91_ACR.Rptnumber>)RptDataSet2));
        //    return Rpt1a;
        //}
       
        private static LocalReport RptConfmlt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.RptConfmLt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptConAssess(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.EmpAssesment>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport RptCustomerinf(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.Customerinf>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.CustomerImagePath>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport RptCustApp ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptCustApp>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptBookingApp2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptCustBookApp2>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.RptNomineeBookApp2>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales_02.RptNominatedBookApp2>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport RptSaleDeclaration(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptCustBookApp2>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport RptSuppAss(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.EmpAssesment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptPrjFloorWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.PrjFoorWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptSuplierBgd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.suppbgd>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptBillRegTrack ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillRegTrack>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport RptSupPayment ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptSupPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptOrderStatus ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptOrderStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptPOMRRBillStatus( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPayment.EclassRptPOMRRBillStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptPurSumVsPay( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPayment.EclassPurchasesummaryvspayment>)RptDataSet));
            return Rpt1a;
        }

       
        private static LocalReport RptBundle(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.BundleRpt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptConBundle(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.BundleRpt>)RptDataSet));
            return Rpt1a;
        }
       
        private static LocalReport RptProBalSheet ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptBalSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptBgdVsAct ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptBgdvsActual>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptSubConWrkOrder ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.SubConWrkOrder>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptsubConBillPrj ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.SubconbillPrjwise>)RptDataSet));
            return Rpt1a;
        }
       
       
        private static LocalReport RptAccountcode2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;

            //test dsfdsfdsf s
            Rpt1a.SetParameters(new ReportParameter("companyname", hshParm["companyname"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("DSAccCode", (List<RealEntity.C_17_Acc.EClassAccounts.EClassAccCode>)RptDataSet2));           
            return Rpt1a;
        }


        private static LocalReport rptPrintSubBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;


            Rpt1a.SetParameters(new ReportParameter("companyname", hshParm["companyname"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("billno", hshParm["billno"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("billdate", hshParm["billdate"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("reqnam", hshParm["reqnam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("reqanam", hshParm["reqanam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("appnam", hshParm["appnam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ordnam", hshParm["ordnam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("mrrnam", hshParm["mrrnam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("billnam", hshParm["billnam"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassSupplierBill>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport RptSalPerWise ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptSalPerWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptSalPerProjWise ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptSalPerWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptMonWiseCol ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseCol>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptMonColBarChart ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseCol>)RptDataSet));
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet2", (List<RealEntity.C_17_Acc.RptMonthValue>)RptDataSet2));
            
            return Rpt1a;
        }

        private static LocalReport RptMonColLineChart ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseCol>)RptDataSet));
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet2", (List<RealEntity.C_17_Acc.RptMonthValue>)RptDataSet2));
            
            return Rpt1a;
        }

        private static LocalReport RptMonSalesBarChart ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseCol>)RptDataSet));
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet2", (List<RealEntity.C_17_Acc.RptMonthValue>)RptDataSet2));
            
            return Rpt1a;
        }

        private static LocalReport RptMonSalesLineChart ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.RptMonWiseCol>)RptDataSet));
            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet2", (List<RealEntity.C_17_Acc.RptMonthValue>)RptDataSet2));

            return Rpt1a;
        }


        private static LocalReport RptBankInterest ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassBankInterest>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptFlrWiseConPrg ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.FlrWiseConPrg>)RptDataSet));
            return Rpt1a;
        }
       
        private static LocalReport RptClietList ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassClentEntry>)RptDataSet));
            return Rpt1a;
        }
       

        private static LocalReport RptSalesJournal ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.SalesJournal>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptCkhDeposit ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptCkhDeposit>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptUpdateCol ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptUpdateCol>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptUpdatePur ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptUpdatePur>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptConBillUp ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptConBillUp>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptUpdateMatTrans ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptUpdateMatTrans>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptReqSts ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptReqSts>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport Rptcrateappanorder ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.Rptcrateappanorder>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptPurOrder ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptPurOrder>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptCashRcv ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptCashRcv>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptBillCon ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_99_AllInterface.RptBillCon>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport RptSecuritySalarySuvstu ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        {

            Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SecuritySalarySuvstu>)RptDataSet));
            return Rpt1a;
        }


       //NAhid 20210414

        private static LocalReport SetRptEmpLeavStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus>)RptDataSet));
             
            return Rpt1a;
        }

        private static LocalReport SetRptMonWiseEmpLeave(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>)RptDataSet));
             
            return Rpt1a;
        }
        private static LocalReport SetRptMonWiseEmpLeaveBR(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>)RptDataSet));
             
            return Rpt1a;
        }
       
     
      
    }
}
