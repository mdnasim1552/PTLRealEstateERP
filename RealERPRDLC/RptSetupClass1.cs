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
using RealEntity.C_34_Mgt;
using System.Data;
using RealERPLIB;


namespace RealERPRDLC
{
    public class RptSetupClass1
    {

        public static List<EClassSalPurAcc> RptDataSet { get; private set; }
        public static LocalReport rpt1a { get; private set; }

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
                #region LAND LPA
                case "R_01_LPA.RptLandFeasibility": Rpt1a = SetRptLandFeasibility(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_LPA.RptLandInformation": Rpt1a = SetRptLandInformation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_02_Fea.rptProjectFeasibility": Rpt1a = SetrptProjectFeasibility(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_01_LPA.RptlandDataBank": Rpt1a = SetRptlandDataBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_02_Fea.RptProjectTopSheet": Rpt1a = SetRptProjectTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_02_Fea.RptFeaProject": Rpt1a = SetRptFeaProject(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_02_Fea.RptProjFeasibilityManama": Rpt1a = SetRptProjFeasibilityManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_02_Fea.rptCostAnlys": Rpt1a = SetrptCostAnlys(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Budgetary Control
                case "R_04_Bgd.RptAdditionalBudget": Rpt1a = SetRptAdditionalBudget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptBgdWkVsActual": Rpt1a = SetRptBgdWkVsActual(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptBugIncmStatement": Rpt1a = SetRptBugIncmStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptBgdFlrWise": Rpt1a = SetRptBgdFlrWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptResourceBasis": Rpt1a = SetRptResourceBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptPrjInfo": Rpt1a = SetRptPrjInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptPrjInfoEnt": Rpt1a = SetRptPrjInfoEnt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjectBgdGrDet": Rpt1a = SetRptProjectBgdGrDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptMaterialsReq": Rpt1a = SetRptMaterialsReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptMaterialsReqDetails": Rpt1a = SetRptMaterialsReqDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjectBgd": Rpt1a = SetRptProjectBgd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjectBgdFinlay": Rpt1a = SetRptProjectBgdFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjBgdCon": Rpt1a = SetRptProjBgdCon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptWorkVsResource": Rpt1a = SetRptWorkVsResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptPrjBudgetedCost": Rpt1a = SetRptPrjBudgetedCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptInResource": Rpt1a = SetRptInResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptInWork": Rpt1a = SetRptInWork(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptWorkSchd": Rpt1a = SetRptWorkSchd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptOtherReqStatus01": Rpt1a = SetRptOtherReqStatus01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjectBgdVsAlloc": Rpt1a = SetRptProjectBgdVsAlloc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptWorkVsResVsAllocDet": Rpt1a = SetRptWorkVsResVsAllocDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptBudgetBalanceResource": Rpt1a = SetRptBudgetBalanceResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptPrjFloorWise": Rpt1a = SetRptPrjFloorWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptStdAnaSheet": Rpt1a = SetRptStdAnaSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_05_Busi.RptYePlanIncomeSt": Rpt1a = SetRptYePlanIncomeSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //case "R_04_Bgd.RptDetailsBudget": Rpt1a = SetRptDetailsBudget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptProjectBgdResGrWiseDet": Rpt1a = SetRptProjectBgdResGrWiseDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //Crystal to RDLC
                case "R_04_Bgd.RptBgdCostDifWork": Rpt1a = SetRptBgdCostDifWork(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptMasterBudget": Rpt1a = SetRptMasterBudget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptDetailsBudget": Rpt1a = SetRptDetailsBudget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.rptOtherReqStatusSuvastu": Rpt1a = SetrptOtherReqStatusSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.rptOtherReqStatusTanvir": Rpt1a = SetrptOtherReqStatusTanvir(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.rptSubConRat": Rpt1a = SetrptSubConRat(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptConLavel": Rpt1a = SetRptConLavel(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.rptFloorResource": Rpt1a = SetrptFloorResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_04_Bgd.RptLandPurRegister": Rpt1a = SetRptLandPurRegister(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Tender
                case "R_07_Ten.RptTenderProposal": Rpt1a = SetRptTenderProposal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_07_Ten.RptCivilConBOQ": Rpt1a = SetRptCivilConBOQ(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_07_Ten.RptCivilConBOQTender": Rpt1a = SetRptCivilConBOQTender(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Project Design


                case "R_08_PPlan.RptProjectDesign": Rpt1a = SetRptProjectDesign(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_08_PPlan.RptProTargetTimeBasis": Rpt1a = SetRptProTargetTimeBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion
                #region Project Implementation
                case "R_09_PIMP.RptWorkOrder": Rpt1a = SetRptWorkOrder(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderCPDL": Rpt1a = SetRptWorkOrderCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_09_PIMP.RptWorkOrder2": Rpt1a = SetRptWorkOrder2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderP2PBN": Rpt1a = SetRptWorkOrderP2PBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderSuvastu": Rpt1a = SetRptWorkOrderSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderAcme": Rpt1a = SetRptWorkOrderAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderAcmeConst": Rpt1a = SetRptWorkOrderAcmeConst(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptProposalFromAcmeConst": Rpt1a = SetRptProposalFromAcmeConst(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_90_PF.RptIndvPfAlli": Rpt1a = SetRptIndvPf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptLabIssueSubCon": Rpt1a = SetRptLabIssueSubCon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubConBillFinalization": Rpt1a = SetRptSubConBillFinalization(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubConWiseBillReport": Rpt1a = SetRptSubConBillWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptBillApprovalSheet": Rpt1a = SetRptBillApprovalSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptLabIssue": Rpt1a = SetRptrptLabIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptLabIssueUrban": Rpt1a = SetrptLabIssueUrban(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptLabIssueSuvastu": Rpt1a = SetrptLabIssueSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptLabIssueAcme": Rpt1a = SetrptLabIssueAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptLabIssueAssure": Rpt1a = SetrptLabIssueAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptLabIssueRup": Rpt1a = SetRptLabIssueRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptTopSheet": Rpt1a = SetRptTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillAcme": Rpt1a = SetRptConBillAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillEdi": Rpt1a = SetRptConBillEdi(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillSuvastu": Rpt1a = SetRptConBillSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillRup": Rpt1a = SetRptConBillRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBill": Rpt1a = SetRptConBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillAlli": Rpt1a = SetRptConBillAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillBridge": Rpt1a = SetRptConBillBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillBridgeWithoutLogo": Rpt1a = SetRptConBillBridgeWithoutLogo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillAssure": Rpt1a = SetRptConBillAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillCredence": Rpt1a = SetRptConBillCrdence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillEdisonErp": Rpt1a = SetRptConBillEdisonErp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillAssure02": Rpt1a = SetRptConBillAssure02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillFinaly": Rpt1a = SetRptConBillFinaly(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillEpic": Rpt1a = SetRptConBillEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_09_PIMP.RptSubConSD": Rpt1a = SetRptSubConSD(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptImpExeStatus1": Rpt1a = SetRptImpExeStatus1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptPrjWiseSubcontractorbill": Rpt1a = SetRptPrjWiseSubcontractorbill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptTarVsPlan": Rpt1a = SetRptTarVsPlan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptPurIssueEntry": Rpt1a = SetrptPurIssueEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubcontractorPrjWisebill": Rpt1a = SetRptSubcontractorPrjWisebill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptImplemenPlan": Rpt1a = SetRptImplemenPlan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptBgdVsExe": Rpt1a = SetRptBgdVsExe(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillWorkWiseInns": Rpt1a = SetRptConBillWorkWiseInns(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptWorkOrderP2P": Rpt1a = SetRptWorkOrderP2P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubconbillreqP2p": Rpt1a = SetRptSubconbillreqP2p(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubConBillTopSheet": Rpt1a = SetRptSubConBillTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillManama": Rpt1a = SetRptConBillManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillIntech": Rpt1a = SetRptConBillIntech(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubConOverAll2": Rpt1a = SetRptSubConOverAll2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_09_PIMP.RptSubConOverAll": Rpt1a = SetRptSubConOverAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.rptsubconbill": Rpt1a = Setrptsubconbill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptPerodSubConBill": Rpt1a = SetRptPerodSubConBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptConBillCPDL": Rpt1a = SetRptConBillCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion
                #region Procurement Module
                case "R_14_Pro.RptPurMktSurvey02": Rpt1a = SetRptPurMktSurvey02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurvey03": Rpt1a = SetRptPurMktSurvey03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurvey05": Rpt1a = SetRptPurMktSurvey05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptPurMktSurveyFinlay02": Rpt1a = SetRptPurMktSurveyFinlay02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyFinlay03": Rpt1a = SetRptPurMktSurveyFinlay03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyFinlay05": Rpt1a = SetRptPurMktSurveyFinlay05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptPurMktSurveyP_2_P": Rpt1a = SetRptPurMktSurveyP_2_P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyP2P02": Rpt1a = SetRptPurMktSurveyP2P02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyP2P05": Rpt1a = SetRptPurMktSurveyP2P05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break; 
                
                case "R_14_Pro.RptPurMktSurveyCPDL03": Rpt1a = SetRptPurMktSurveyCPDL03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyCPDL04": Rpt1a = SetRptPurMktSurveyCPDL04(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyCPDL05": Rpt1a = SetRptPurMktSurveyCPDL05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break; 

                case "R_14_Pro.RptPurMktSurveyEpic02": Rpt1a = SetRptPurMktSurveyEpic02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyEpic03": Rpt1a = SetRptPurMktSurveyEpic03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyEpic05": Rpt1a = SetRptPurMktSurveyEpic05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;                
                
                case "R_14_Pro.RptPurMktSurveyCPDL03C": Rpt1a = SetRptPurMktSurveyCPDL03C(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyCPDL04C": Rpt1a = SetRptPurMktSurveyCPDL04C(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyCPDL05C": Rpt1a = SetRptPurMktSurveyCPDL05C(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptSupplierDetials": Rpt1a = SetRptSupplierDetials(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptConDetials": Rpt1a = SetRptConDetials(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupMonthAss": Rpt1a = RptSupMonthAss(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSubContEnlistmentForm": Rpt1a = RptRptSubContEnlistmentForm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMatRateVar": Rpt1a = RptMatRateVar(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptPurMktSurvey": Rpt1a = GetRptPurMktSurvey(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyEdison": Rpt1a = GetRptPurMktSurveyEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMktSurveyMatWiseSupList": Rpt1a = GetRptMktSurveyMatWiseSupList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMktSurveySupWiseMatList": Rpt1a = GetRptMktSurveySupWiseMatList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMatPurHistory": Rpt1a = GetRptMatPurHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptIndSupPurchae": Rpt1a = GetRptIndSupPurchae(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptWorkOrderSupHistory": Rpt1a = GetRptWorkOrderSupHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptWorkOrdHisoryResource": Rpt1a = GetRptWorkOrdHisoryResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPayStatusSupwise": Rpt1a = GetRptPayStatusSupwise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupplierPayable": Rpt1a = GetRptSupplierPayable(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupplierDueStatus": Rpt1a = GetRptSupplierDueStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptsDayWiseAdvanced": Rpt1a = GetRptsDayWiseAdvanced(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptAdvancedVsPayment": Rpt1a = GetRptAdvancedVsPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptOrderVsReceived": Rpt1a = GetRptOrderVsReceived(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSubBillDetails": Rpt1a = GetRptSubBillDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptReqAppStatus": Rpt1a = GetRptReqAppStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptRequisitionStatus1": Rpt1a = GetRptRequisitionStatus1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptRequisitionStatus2": Rpt1a = GetRptRequisitionStatus2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurchaseSummary02": Rpt1a = GetRptPurchaseSummary02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupCreditLimit": Rpt1a = GetRptSupCreditLimit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMatGrpwisePayable": Rpt1a = GetRptMatGrpwisePayable(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptGrpwisePayable": Rpt1a = GetRptGrpwisePayable(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurApprovEntryEdison": Rpt1a = SetRptPurApprovEntryEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMaterialTransferTracking": Rpt1a = GetRptPurTransTrack(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_15_DPayReg.RptBillForward": Rpt1a = SetRptBillForward(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //Convert RDLC Report by Parbaz
                case "R_14_Pro.RptPurchaseTra": Rpt1a = SetRptPurchaseTra(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptOrderTracking": Rpt1a = SetRptOrderTracking(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyManama02": Rpt1a = SetRptPurMktSurveyManama02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyManama03": Rpt1a = SetRptPurMktSurveyManama03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurMktSurveyManama05": Rpt1a = SetRptPurMktSurveyManama05(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptSuppCheqHistory": Rpt1a = SetRptSuppCheqHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptPurAprovEntry": Rpt1a = SetRptPurAprovEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //RptLinkRptSupplierChequeDetails 
                case "R_14_Pro.RptLinkRptSupplierChequeHistory": Rpt1a = SetRptLinkRptSupplierChequeHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptLinkRptSupplierChequeDetails": Rpt1a = SetRptLinkRptSupplierChequeDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptWorkOrderStatus1": Rpt1a = SetRptWorkOrderStatus1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptWorkOrderStatus2": Rpt1a = SetRptWorkOrderStatus2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptWorkOrderStatus3": Rpt1a = SetRptWorkOrderStatus3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptBillInfoInns": Rpt1a = SetRptBillInfoInns(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillInfoManama": Rpt1a = SetRptBillInfoManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillInfoLanco": Rpt1a = SetRptBillInfoLanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillInfoJbs": Rpt1a = SetRptBillInfoJbs(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillInfoFinlay": Rpt1a = SetRptBillInfoFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillInfoCPDL": Rpt1a = SetRptBillInfoCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break; 

                case "R_14_Pro.RptBillAlliInfo": Rpt1a = SetRptBillAlliInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillConfirmationBridge": Rpt1a = SetRptBillConfirmationBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillConfirmBridgeWithoutLogo": Rpt1a = SetRptBillConfirmBridgeWithoutLogo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillConfirmation03": Rpt1a = SetRptBillConfirmation03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillConfirmation02": Rpt1a = SetRptBillConfirmation02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSuplistWithMat": Rpt1a = SetRptSuplistWithMat(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptMatWiseSupList": Rpt1a = SetRptSuplistWithMat(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBudgetTracking": Rpt1a = SetRptBudgetTracking(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurchaseTrack02": Rpt1a = SetRptPurchaseTrack02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptDayWisePurchase": Rpt1a = SetRptDayWisePurchase(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptDayWisePurchaseEdison": Rpt1a = SetRptDayWisePurchaseEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurchaseSummary": Rpt1a = SetRptPurchaseSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.rptPurchaseBillTk": Rpt1a = SetrptPurchaseBillTk(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptBillTracking": Rpt1a = SetRptBillTracking(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.RptPrjWiseMrfHistory": Rpt1a = SetRptPrjWiseMrfHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptDateWiseReqCheckHistory": Rpt1a = SetRptDateWiseReqCheckHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_14_Pro.rptDeliveryEfficiency": Rpt1a = SetrptDeliveryEfficiency(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_15_DPayReg.RptChequeIssue": Rpt1a = SetRptChequeIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Inventory
                case "R_12_Inv.rptProMatStock": Rpt1a = GetrptProMatStock(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptProMatStock2": Rpt1a = GetrptProMatStock2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptProMatStock2Leisure": Rpt1a = GetrptProMatStock2Leisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptPurMrrEntry": Rpt1a = GetrptPurMrrEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptPurMrrEntryFinlay": Rpt1a = GetrptPurMrrEntryFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptPurMrrEdison": Rpt1a = GetrptPurMrrEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptPurMrrEntryBridge": Rpt1a = GetrptPurMrrEntryBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMaterialTrnsferReq": Rpt1a = GetRptMaterialTrnsferReq(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMaterialTrnsGatepass": Rpt1a = GetRptMaterialTrnsGatepass(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMatTransferRec": Rpt1a = GetRptMatTransferRec(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMaterialTrnsfer": Rpt1a = GetRptMaterialTrnsfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMaterialTrnsferP2P": Rpt1a = SetRptMaterialTrnsferP2P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptPurMrrEntryCPDL": Rpt1a = SetrptPurMrrEntryCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_12_Inv.RptMatIssue": Rpt1a = SetRptMatIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMatIssueBridge": Rpt1a = SetRptMatIssueBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMatIssueStatus": Rpt1a = GetRptMatIssueStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptProPhyStock": Rpt1a = SetRptProPhyStock(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInterComTransStatus": Rpt1a = SetRptInterComTransStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMaterialsStock": Rpt1a = SetRptMaterialsStock(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntry02": Rpt1a = SetRptReqEntry02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryEpic": Rpt1a = SetRptReqEntryEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntry03": Rpt1a = SetRptReqEntry03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryInns": Rpt1a = SetRptReqEntryInns(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryTropical": Rpt1a = SetRptReqEntryTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryCons": Rpt1a = SetRptRptReqEntryCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntry07": Rpt1a = SetRptReqEntry07(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntry08": Rpt1a = SetRptReqEntry08(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryManama": Rpt1a = SetRptReqEntryManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryJBS": Rpt1a = SetRptReqEntryJBS(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_12_Inv.rptMatProjWise": Rpt1a = SetRptMaterialStockPrjWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptPurchaseStatus1": Rpt1a = SetRptPurchaseStatus1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_12_Inv.rptPurVarAa": Rpt1a = SetRptPurVarAa(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.rptInResource": Rpt1a = SetrptInResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMatTransStatus": Rpt1a = SetRptMatTransStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptPurchaseOrder": Rpt1a = SetRptPurchaseOrder(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptMatTransStatusUrban": Rpt1a = SetRptMatTransStatusUrban(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptPurchaseOrderJBS": Rpt1a = SetRptPurchaseOrderJBS(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_12_Inv.RptReqEntryEdison": Rpt1a = SetRptReqEntryEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntry02Assure": Rpt1a = SetRptReqEntry02Assure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptReqEntryiNTECH": Rpt1a = SetRptReqEntryiNTECH(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInvenAmtBasis": Rpt1a = SetRptInvenAmtBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInvenQtyBasis": Rpt1a = SetRptInvenQtyBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInvenAmtBasisDetails": Rpt1a = SetRptInvenAmtBasisDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInvenAmtBasisPeriodic": Rpt1a = SetRptInvenAmtBasisPeriodic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptInvenQtyBasisPeriodic": Rpt1a = SetRptInvenQtyBasisPeriodic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion
                #region F_16_Bill
                case "R_16_Bill.RptBillEntry": Rpt1a = SetRptBillEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptBillRateEntry": Rpt1a = SetRptBillRateEntry(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptBillInvoice": Rpt1a = SetRptBillInvoice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptUpconSabCon": Rpt1a = SetRptUpconSabCon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptBillInvoiceP2P": Rpt1a = SetRptBillInvoiceP2P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptBillInvoiceAcme": Rpt1a = SetRptBillInvoiceAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Payment Register
                case "R_15_DPayReg.RptBillAproved": Rpt1a = SetRptBillAproved(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_DPayReg.RptBillIssue": Rpt1a = SetRptBillIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_15_DPayReg.RptSupPaymentProposal": Rpt1a = SetRptSupPaymentProposal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptSubConBillStu": Rpt1a = SetRptSubConBillStu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptProjWisBill": Rpt1a = SetRptProjWisBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptGrpWiseBill": Rpt1a = SetRptGrpWiseBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_09_PIMP.RptProjWBillQty": Rpt1a = SetRptProjWBillQty(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_16_Bill.RptProjStarus": Rpt1a = SetRptProjStarus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion

                case "R_81_Hrm.R_82_App.RptEmployeeAllInfo": Rpt1a = SetRptEmployeeAllInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptOtherCollHistory": Rpt1a = SetRptOtherCollHistory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptDateWiseBill": Rpt1a = SetRptDateWiseBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptPurOrderTopSheet": Rpt1a = SetRptPurOrderTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_14_Pro.RptSupAdvanceDetails": Rpt1a = SetRptSupAdvanceDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMonthlyProbCollection": Rpt1a = SetRptMonthlyProbCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDuesAllReports": Rpt1a = SetRptDuesAllReports(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpOfferLetter": Rpt1a = SetRptEmpOfferLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpIdCard": Rpt1a = SetRptEmpIdCard(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpApointmentLetter": Rpt1a = SetRptEmpApointmentLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_94_Task.RptTaskInfoDet": Rpt1a = SetRptTaskInfoDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptAllDuesInfo": Rpt1a = SetRptAllDuesInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptInterfaceLeave": Rpt1a = SetRptInterfaceLeave(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptInterfaceAttApp": Rpt1a = SetRptInterfaceAttApp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptGroupAtt": Rpt1a = SetRptGroupAtt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptReqAdjustment": Rpt1a = SetRptReqAdjustment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSoldUnsoldUnitAvgPrice": Rpt1a = SetRptSoldUnsoldUnitAvgPrice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalesVsAchivement": Rpt1a = SetRptSalesVsAchivement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalesVsAchivementLO": Rpt1a = SetRptSalesVsAchivementLO(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptPaymentSystem": Rpt1a = SetRptPaymentSystem(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
               
                case "R_22_Sal.RptSalesCollectionStatement": Rpt1a = RptSalesCollectionStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptComSalesServey": Rpt1a = SetRptComSalesServey(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptIndentIssueStatus": Rpt1a = SetRptIndentIssueStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_12_Inv.RptIndentIssueStatusSummary": Rpt1a = SetRptIndentIssueStatusSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptTransactionSt": Rpt1a = SetRptTransactionSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalesOpening": Rpt1a = SetRptSalesOpening(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptTopSheetFactory": Rpt1a = SetRptTopSheetFactory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptLetterOfAllotmentCPDL": Rpt1a = SetRptLetterOfAllotmentCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalEncashment": Rpt1a = SetRptSalEncashment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


    


                #region General Accounts 17
                case "R_17_Acc.TransectionPrint": Rpt1a = SetRptTrnPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCheque": Rpt1a = SetRptCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeSuvastu": Rpt1a = SetRptChequeSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCheqCredence": Rpt1a = SetRptCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptChequeIBBL": Rpt1a = SetRptChequeIBBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeSBL": Rpt1a = SetRptChequeSBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIndvPf": Rpt1a = SetRptIndvPf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptNoteIncoStatement": Rpt1a = SetRptNoteIncoStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccountcode2": Rpt1a = RptAccountcode2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccSubCode": Rpt1a = SetRptAccSubCode(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPostDateCqSuvastu": Rpt1a = SetRptPostDateCqSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyTrans": Rpt1a = SetRptDailyTrans(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeAcme": Rpt1a = SetRptChequeAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeManama": Rpt1a = SetRptChequeManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeTCL": Rpt1a = SetRptChequeTCL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeGreenwood": Rpt1a = SetRptChequeGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeGreenwoodSHBL": Rpt1a = SetRptChequeGreenwoodSHBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeGreenwoodSHIBL": Rpt1a = SetRptChequeGreenwoodSHIBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeGreenwoodFSIBL": Rpt1a = SetRptChequeGreenwoodFSIBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.PrintChqFinlayBRAC": Rpt1a = SetPrintChqFinlayBRAC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeOneBankBti": Rpt1a = SetRptChequeOneBankBti(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeDhakaBankCPDL": Rpt1a = SetRptChequeDhakaBankCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeUCBCPDL": Rpt1a = SetRptChequeUCBCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeIBBLCPDL": Rpt1a = SetRptChequeIBBLCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeAIBLCPDL": Rpt1a = SetRptChequeAIBLCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeTBLCPDL": Rpt1a = SetRptChequeTBLCPDLL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_17_Acc.RptTrialBl1": Rpt1a = SetRptTrialBl1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptVoucherPrint": Rpt1a = SetRptVoucherPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptGeneralAdminOverhad": Rpt1a = SetRptGeneralHead(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankCheque": Rpt1a = SetRptBankCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccIncomeStAlli": Rpt1a = SetRptAccIncomeStAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRatioAnalysis": Rpt1a = SetRptRatioAnalysis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBalanceSheet2": Rpt1a = SetRptBalanceSheet2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccIncomeSt": Rpt1a = SetRptAccIncomeSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptVouTopSheet": Rpt1a = SetRptRptVouTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptLedger": Rpt1a = SetRptLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptLedgerTanvir": Rpt1a = SetRptLedgerTanvir(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptAccLedger": Rpt1a = SetRptAccLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerWqty": Rpt1a = SetRptAccLedgerWqty(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerBridge": Rpt1a = SetRptAccLedgerBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerTerra": Rpt1a = SetRptAccLedgerTerra(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerRup": Rpt1a = SetRptRptAccLedgerRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccSLedger": Rpt1a = SetRptAccSLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerCube": Rpt1a = SetRptAccLedgerCube(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccLedgerIntech": Rpt1a = SetRptAccLedgerIntech(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                case "R_17_Acc.RptPaymentIncSch": Rpt1a = SetRptPaymentIncSch(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSPLedger": Rpt1a = SetRptSPLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSPLedgerRup": Rpt1a = SetRptSPLedgerRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSPLedger02": Rpt1a = SetRptSPLedger02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSPLedger02Intech": Rpt1a = SetRptSPLedger02Intech(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSPLedgerIntect": Rpt1a = SetRptSPLedgerIntect(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_17_Acc.RptPettyCashBillApprSheet": Rpt1a = SetRptPettyCashBillApprSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPendingBill": Rpt1a = SetRptPendingBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashBankPosGrp": Rpt1a = SetRptCashBankPosGrp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCsahBankGrpMonth": Rpt1a = SetRptCsahBankGrpMonth(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashBankGrpMonthDts": Rpt1a = SetRptCashBankGrpMonthDts(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptGeneralBill": Rpt1a = SetRptGeneralBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashCrPur": Rpt1a = SetRptCashCrPur(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptLabourDetails": Rpt1a = SetRptLabourDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBgdvsExpense": Rpt1a = SetRptBgdvsExpense(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccFinalReports": Rpt1a = SetRptAccFinalReports(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeInHand": Rpt1a = SetRptChequeInHand(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPostDatedChqInHand": Rpt1a = SetRptPostDatedChqInHand(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCollChqStatus": Rpt1a = SetRptCollChqStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RrptChqReceivedVsClr": Rpt1a = SetRrptChqReceivedVsClr(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRealCollDetails": Rpt1a = SetRptRealCollDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPrintVoucher": Rpt1a = SetRptPrintVoucher(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPrintVoucher01": Rpt1a = SetRptPrintVoucher01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPrintVoucher02": Rpt1a = SetRptPrintVoucher02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIssuedCheque": Rpt1a = SetRptIssuedCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIssuedChequeBridge": Rpt1a = SetRptIssuedChequeBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIssuedChequeCP": Rpt1a = SetRptIssuedChequeCP(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIssuedChequeCPALL": Rpt1a = SetRptIssuedChequeCPALL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                    
                case "R_17_Acc.RptIssueClearence": Rpt1a = SetRptIssueClearence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIssueVsClr": Rpt1a = SetRptIssueVsClr(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPaymentChqClearance": Rpt1a = SetRptPaymentChqClearance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPostDatCheque": Rpt1a = SetRptPostDatCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChqIssuedGrpWise": Rpt1a = SetRptChqIssuedGrpWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyTransaction": Rpt1a = SetRptDailyTransaction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankPosition": Rpt1a = SetRptBankPosition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRecAndPaymentActual": Rpt1a = SetRptRecAndPaymentActual(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptProjectwiseReceptsandPayment": Rpt1a = SetRptProjectwiseReceptsandPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptOpPayment": Rpt1a = SetRptOpPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRecAndPaymentAlli": Rpt1a = SetRptRecAndPaymentAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankBalance02": Rpt1a = SetRptBankBalance02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankBalance02Cube": Rpt1a = SetRptBankBalance02Cube(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRecAndPaymentProj": Rpt1a = SetRptRecAndPaymentProj(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashFlow02": Rpt1a = SetRptCashFlow02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccBalConfirmation": Rpt1a = SetRptAccBalConfirmation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDetailShedule": Rpt1a = SetRptDetailShedule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSalesDetailSchdule": Rpt1a = SetRptSalesDetailSchdule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAllSupPayment": Rpt1a = SetRptAllSupPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAllSupPaymentBridge": Rpt1a = SetRptAllSupPaymentBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccConTrialBalance": Rpt1a = SetRptAccConTrialBalance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSalesReg02": Rpt1a = SetRptSalesReg02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccDetTrialBalance": Rpt1a = SetRptAccDetTrialBalance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_99_AllInterface.RptNetDuesInfo": Rpt1a = SetRptNetDuesInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccProjectReport": Rpt1a = SetRptAccProjectReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccProjectReport01": Rpt1a = SetRptAccProjectReport01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptReceiptPayable": Rpt1a = SetRptReceiptPayable(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIncomestIndPrj": Rpt1a = SetRptIncomestIndPrj(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptMonWiseCollection": Rpt1a = SetRptMonWiseCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccFinalReportsDetails": Rpt1a = SetRptAccFinalReportsDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeSuvastuPBL": Rpt1a = SetRptChequeSuvastuPBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptProjCostSales": Rpt1a = SetRptProjCostSales(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankStatementInfo": Rpt1a = SetRptBankStatementInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_17_Acc.RptInterComTransStatu": Rpt1a = SetRptInterComTransStatu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSupPayment": Rpt1a = SetRptSupPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSupPayment02": Rpt1a = SetRptSupPayment02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSupplierProposedPayment": Rpt1a = SetRptSupProPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSubConProposedPayment": Rpt1a = SetRptSubConProposedPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBillRegister": Rpt1a = SetRptBillRegister(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptRegBillStatus": Rpt1a = SetRptRegBillStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPendingCliMod": Rpt1a = SetRptPendingCliMod(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankInterest2": Rpt1a = SetRptBankInterest(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonthlyIsuVsPay1": Rpt1a = SetRptMonthlyIssueVsPay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIsuVsPaySum1": Rpt1a = SetRptIssueVsPaySum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyPayment1": Rpt1a = SetRptDailyPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyPaymentCostWise1": Rpt1a = SetRptDailyPaymentCostWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyPaymentDetails1": Rpt1a = SetRptDailyPaymentDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAllSupaConPayment": Rpt1a = SetRptAllSupaConPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPendingConBill": Rpt1a = SetRptPendingConBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccAitVatSdAllSupp": Rpt1a = SetRptAitVatSdDeductionAllSupp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonthlySuppBill": Rpt1a = SetRptMonthlySuppBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonthlySubConBill": Rpt1a = SetRptMonthlySubConBill(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptATIVat": Rpt1a = SetRptATIVat(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccAitVatSd": Rpt1a = SetRptAccATIVatDeduction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAitVatProjWise": Rpt1a = SetRptAccATITxVatProjWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPaySlipSupplier": Rpt1a = SetRptPaySlipSupplier(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPaySlipSupplierShuvastu":
                    Rpt1a = SetRptPaySlipSupplierShuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptPaySlipSubContractor": Rpt1a = SetRptPaySlipSubContractor(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyProjectTransactionList": Rpt1a = SetRptProjectTransaction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBankReconc": Rpt1a = SetRptBankReconc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyProjectTransaction": Rpt1a = SetRptProjectTransaction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptProjectCostPerSft": Rpt1a = SetRptProjectCostPerSft(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptTransactionLink": Rpt1a = SetRptTransactionLink(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCostOfSalesPerSft": Rpt1a = SetRptCostOfSalesPerSft(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptProjectSpecification": Rpt1a = SetRptProjectSecification(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptProjectPettyCash": Rpt1a = SetRptProjectPettyCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptTranLinkPost": Rpt1a = SetRptTranLinkPost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonthlySubConBillAssure": SetRptMonthlySubConBillAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAllSupaConPaymentAlli": SetRptAllSupaConPaymentAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                // case "R_17_Acc.RptProjectWiseCollection": SetRptProjectWiseCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyTransactionCredence": Rpt1a = SetRptDailyTransactionCredence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptInvoicep2p360": Rpt1a = SetRptInvoiceP2P360(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //MIS Module
                case "R_17_Acc.RptAccConSchedule01": Rpt1a = SetRptAccConSchedule01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIncomeSt": Rpt1a = SetRptIncomeSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptIncomeStSinglePrj": Rpt1a = SetRptIncomeSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBalanceSheetAlli": Rpt1a = SetRptBalanceSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBalanceSheetUddl": Rpt1a = SetRptBalanceSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptBalanceSheetCredence": Rpt1a = SetRptBalanceSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptShareQty": Rpt1a = SetRptShareQty(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashFlowIndirect": Rpt1a = SetRptCashFlowIndirect(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptNoteSheet": Rpt1a = SetRptNoteSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDetailSheduleTB": Rpt1a = SetRptDetailSheduleTB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPrjWiseMaterialCosting": Rpt1a = SetRptPrjWiseMaterialCosting(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                // Voucher Print
                case "R_17_Acc.rptPrintVocherAlli": Rpt1a = SetrptPrintVocherAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherAlli02": Rpt1a = SetrptPrintVocherAlli02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherAlli03": Rpt1a = SetrptPrintVocherAlli03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher1": Rpt1a = SetrptPrintVoucher1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher2": Rpt1a = SetrptPrintVoucher2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher3": Rpt1a = SetrptPrintVoucher3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher4": Rpt1a = SetrptPrintVoucher4(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher5": Rpt1a = SetrptPrintVoucher5(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher6": Rpt1a = SetrptPrintVoucher6(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucher7": Rpt1a = SetrptPrintVoucher7(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherTanvir": Rpt1a = SetrptPrintVoucherTanvir(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherBridge": Rpt1a = SetrptPrintVoucherBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherISBL": Rpt1a = SetrptPrintVoucherISBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherISBL02": Rpt1a = SetrptPrintVoucherISBL02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherFinlay": Rpt1a = SetrptPrintVoucherFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherFinlay02": Rpt1a = SetrptPrintVoucherFinlay02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherCPDL": Rpt1a = SetrptPrintVoucherCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherCPDL02": Rpt1a = SetrptPrintVoucherCPDL02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherEpic": Rpt1a = SerptPrintVoucherEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherSuvastu": Rpt1a = SetrptPrintVocherSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherSuvastu02": Rpt1a = SetrptPrintVocherSuvastu02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherSuvastu03": Rpt1a = SetrptPrintVocherSuvastu03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherCredence01": Rpt1a = SetrptPrintVocherCredence01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherCredence02": Rpt1a = SetrptPrintVocherCredence02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherCredence03": Rpt1a = SetrptPrintVocherCredence03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherDefault": Rpt1a = SetrptPrintVoucherDefault(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherManama": Rpt1a = SetrptPrintVoucherManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVoucherCube": Rpt1a = SetrptPrintVoucherCube(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //
                case "R_17_Acc.rptPrintVocherEntrust01": Rpt1a = SetrptPrintVocherEntrust01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherEntrust02": Rpt1a = SetrptPrintVocherEntrust02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherEntrust03": Rpt1a = SetrptPrintVocherEntrust03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.rptPrintVocherJBS01": Rpt1a = SetrptPrintVocherJBS01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherJBS02": Rpt1a = SetrptPrintVocherJBS02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherJBS03": Rpt1a = SetrptPrintVocherJBS03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.rptPrintVocherIntech01": Rpt1a = SetrptPrintVocherIntech01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherIntech02": Rpt1a = SetrptPrintVocherIntech02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherIntech03": Rpt1a = SetrptPrintVocherIntech03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherGreenwood01": Rpt1a = SetrptPrintVocherGreenwood01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherGreenwood02": Rpt1a = SetrptPrintVocherGreenwood02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherGreenwood03": Rpt1a = SetrptPrintVocherGreenwood03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherJVTropical": Rpt1a = SetrptPrintVocherJVTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherBDTropical": Rpt1a = SetrptPrintVocherBDTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptPrintVocherCCTropical": Rpt1a = SetrptPrintVocherCCTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                // Post Voucher Print
                case "R_17_Acc.rptBankVoucher": Rpt1a = SetrptBankVoucher(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher1": Rpt1a = SetrptBankVoucher1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher2": Rpt1a = SetrptBankVoucher2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher3": Rpt1a = SetrptBankVoucher3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher4": Rpt1a = SetrptBankVoucher4(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher5": Rpt1a = SetrptBankVoucher5(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucher6": Rpt1a = SetrptBankVoucher6(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherAlliance": Rpt1a = SetrptBankVoucherAlliance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherLeisure": Rpt1a = SetrptBankVoucherLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherCredence": Rpt1a = SetrptBankVoucherCredence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherEntrust": Rpt1a = SetrptBankVoucherEntrust(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherP2P": Rpt1a = SetrptBankVoucherP2P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherTropical": Rpt1a = SetrptBankVoucherTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherSuvastu": Rpt1a = SetrptBankVoucherSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherAcme": Rpt1a = SetrptBankVoucherAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherManama": Rpt1a = SetrptBankVoucherManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherTanvir": Rpt1a = SetrptBankVoucherTanvir(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherGreenwood": Rpt1a = SetrptBankVoucherGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherIntech": Rpt1a = SetrptBankVoucherIntech(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBankVoucherEpic": Rpt1a = SetrptBankVoucherEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_17_Acc.RptAccCashbook1": Rpt1a = SetRptAccCashbook1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccCashbook1Credence": Rpt1a = SetRptAccCashbook1Credence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDailyPayProposal": Rpt1a = SetRptDailyPayProposal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptDelDailyTransaction": Rpt1a = SetRptDelDailyTransaction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccBankPosition02": Rpt1a = SetRptAccBankPosition02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptBudVsAchivDet": Rpt1a = SetrptBudVsAchivDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptAccBudVsExpen": Rpt1a = SetrptAccBudVsExpen(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPaySlipSupplierLeisure": Rpt1a = SetRptPaySlipSupplierLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptPurNotUpdated": Rpt1a = SetRptPurNotUpdated(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeDepositAllBank": Rpt1a = SetRptChequeDepositAllBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptChequeDepositBank02": Rpt1a = SetRptChequeDepositBank02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_17_Acc.RptMonthWiseBankLedger": Rpt1a = SetRptMonthWiseBankLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptMonSalPerTarWise": Rpt1a = SetRptMonSalPerTarWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptVoucherTopSheet": Rpt1a = SetRptVoucherTopSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptVoucherTopSheetFinaly": Rpt1a = SetRptVoucherTopSheetFinaly(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.rptOthersAccCode": Rpt1a = SetrptOthersAccCode(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccCodeBookAcme": Rpt1a = SetRptAccCodeBookAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_17_Acc.RptCashBank": Rpt1a = SetRptCashBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptCashBankWithdraw": Rpt1a = SetRptCashBankWithdraw(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccOpening": Rpt1a = SetRptAccOpening(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAccOpeningDetails": Rpt1a = SetRptAccOpeningDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSupplierOvAllPSummary": Rpt1a = SetRptSupplierOvAllPSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptSupplierOvAllPSummaryDetails": Rpt1a = SetRptSupplierOvAllPSummaryDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion
                #region CRM & KPI 21
                case "R_21_Mkt.RptCallCenterLead": SetRptCallCenterLead(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_Mkt.RptSourceWiseLeads": SetRptSourceWiseLeads(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_Mkt.RptCRMClientInfo": SetRptCrmClientInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_MKT.RptYearlySales": SetRptYearlySales(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_MKT.RptProspectWorking": SetRptProspectWorking(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_MKT.RptProspectTransfer": SetRptProspectTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_MKT.ClientLetter": SetRptClientLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_21_MKT.RptDailyWorkStatus": SetRptDailyWorkStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Sales
                case "R_22_Sal.RptSalesTarget": Rpt1a = SetRptSalesYearlyTarget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptUnitWiseCost": Rpt1a = SetRptUnitWiseCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCurrentDues": Rpt1a = SetRptCurrentDues(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptConMonthlyAss": Rpt1a = RptConMonthlyAss(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDummyPaySchidule": Rpt1a = RptRptDummyPaySchidule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDummyPaywithoutDiscount": Rpt1a = SetRptDummyPaywithoutDiscount(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptBgdSales": Rpt1a = SetRptBgdSales(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCustPaySchedule": Rpt1a = SetRptCustPaySchedule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCustPayScheduleCPDL": Rpt1a = SetRptCustPayScheduleCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCustPayScheduleEpic": Rpt1a = SetRptCustPayScheduleEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalSumary": Rpt1a = SetRptSalSumary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalSumAmtBasis": Rpt1a = SetRptSalSumAmtBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSaleSoldUsold": Rpt1a = SetRptSaleSoldUsold(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDayWiseSales": Rpt1a = SetRptDayWiseSales(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDWiseRealCollection": Rpt1a = SetRptDWiseRealCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptBookingtDues": Rpt1a = SetRptBookingtDues(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCollDetailsInfo": Rpt1a = SetRptCollDetailsInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptOSalesSummary": Rpt1a = SetRptOSalesSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.rptDailySaleVsCollTarget": Rpt1a = SetrptDailySaleVsCollTarget(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptReceivedList": Rpt1a = SetRptReceivedList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptCalTValAvgVal": Rpt1a = SetRptCalTValAvgVal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalRegisClearence": Rpt1a = SetRptSalRegisClearence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalRegisClearence02": Rpt1a = SetRptSalRegisClearence02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptUnsoldUnit": Rpt1a = SetRptUnsoldUnit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopNew": Rpt1a = SetRptEnvelopPrintNew(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopOffice": Rpt1a = SetRptEnvelopOffice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopBirthday": Rpt1a = SetRptEnvelopBirthday(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopAniversary": Rpt1a = SetRptEnvelopAniversary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopCongratulation": Rpt1a = SetRptEnvelopCongratulation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_22_Sal.RptSalSummeryDetails": Rpt1a = RptSalSummeryDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDummyPaySchidule02": Rpt1a = RptRptDummyPaySchidule02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDummyPaywithoutDiscount02": Rpt1a = SetRptDummyPaywithoutDiscount02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptAccSummaryInflow": Rpt1a = SetRptAccSummaryInflow(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptAccSumCustPayStatus": Rpt1a = SetRptAccSumCustPayStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_22_Sal.RptCustomerBillInfo": Rpt1a = SetRptCustomerBillInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptTransStatement02": Rpt1a = SetRptTransStatement02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptTransStatement02Finlay": Rpt1a = SetRptTransStatement02Finlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptTransStatement03": Rpt1a = SetRptTransStatement03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptTransStatementCash": Rpt1a = SetRptRptTransStatementCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_22_Sal.RptDetailMoneyRecept": Rpt1a = SetRptDetailMoneyRecept(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceipt1": Rpt1a = SetRptMoneyReceipt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptWecon": Rpt1a = SetRptMoneyReceiptWecon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceipt": Rpt1a = SetRptMoneyReceipt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptDuesCollAll": Rpt1a = SetRptDuesCollAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptIntech": Rpt1a = SetRptMoneyReceiptIntech(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptCPDL": Rpt1a = SetRptMoneyReceiptCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptEPIC": Rpt1a = SetRptMoneyReceiptEPIC(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptFinlay": Rpt1a = SetRptMoneyReceiptFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptFinlay2": Rpt1a = SetRptMoneyReceiptFinlay2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptAcme": Rpt1a = SetRptMoneyReceiptAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptAcme02": Rpt1a = SetRptMoneyReceiptAcme02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break; 
                case "R_22_Sal.RptAcknowledgementSlipCPDL": Rpt1a = SetRptAcknowledgementSlipCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //Uzzal Money receipt
                case "R_22_Sal.RptMoneyReceiptLeisure": Rpt1a = SetRptMoneyReceiptLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptEdison": Rpt1a = SetRptMoneyReceiptEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptSuvastu": Rpt1a = SetRptMoneyReceiptSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceiptTro": Rpt1a = SetRptMoneyReceiptTro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMoneyReceipt360": Rpt1a = SetRptMoneyReceipt360(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_22_Sal.RptSalClntInterest": Rpt1a = SetRptSalClntInterest(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalClntInterestBr": Rpt1a = SetRptSalClntInterestBr(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalClntInterestRup": Rpt1a = SetRptSalClntInterestRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalClntInterestEdison": Rpt1a = SetRptSalClntInterestEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEarlybenefitADelay": Rpt1a = SetRptEarlybenefitADelay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEarlybenefitADelayCPDL": Rpt1a = SetRptEarlybenefitADelayCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_22_Sal.rptUnitFxInf": Rpt1a = SetrptUnitFxInf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptMonthWiseNewSales": Rpt1a = SetRptMonthWiseNewSales(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalPaySchedule": Rpt1a = SetRptSalPaySchedule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalPayScheduleCPDL": Rpt1a = SetRptSalPayScheduleCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalPayScheduleLanco": Rpt1a = SetRptSalPayScheduleLanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //periodic sales collection robi
                case "R_22_Sal.RptPeriodicSalesWithCollection": Rpt1a = SetRptPeriodicSalesWithCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                // Sold & Unsold Information (Group Wise) Create by robi

                case "R_22_Sal.RptSoldUnsoftInfGroupWise": Rpt1a = SetRptSoldUnsoftInfGroupWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //GrandNotesSheet
                case "R_22_Sal.RptGrandNotesSheet": Rpt1a = SetRptGrandNotesSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSampleNotesSheet": Rpt1a = SetRptSampleNotesSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptGrandNotesSheetSummary": Rpt1a = SetRptGrandNotesSheetSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptSalClntInterestFinlay": Rpt1a = SetRptSalClntInterestFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //availbility 
                case "R_22_Sal.RptAvailbilityPrint": Rpt1a = SetRptAvailbilityPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion
                #region Credit Realization(CR)
                case "R_23_CR.RptCustomer_Due_inf": Rpt1a = SetRptCustomer_Due_inf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustomerDues": Rpt1a = SetRptRptCustomerDues(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustomer_Due_inf01": Rpt1a = SetRptCustomer_Due_inf01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptPrjclientStatus": Rpt1a = SetRptPrjclientStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustInvoice": Rpt1a = SetRptCustInvoice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptWeeklyCollection": Rpt1a = SetRptWeeklyCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptSalesStatusGen": Rpt1a = SetRptSalesStatusGen(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptResreceivable": Rpt1a = SetRptResreceivable(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptPaymentStatus02": Rpt1a = SetRptPaymentStatus02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDelMonyRec": Rpt1a = SetRptDelMonyRec(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptPaymentStatus": Rpt1a = SetRptPaymentStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptPaymentStatusFinlay": Rpt1a = SetRptPaymentStatusFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptPaymentStatusEdison": Rpt1a = SetRptPaymentStatusEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_23_CR.RptCustomerDewsOverd": Rpt1a = SetRptCustomerDewsOverd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDuesCollStatsTerranova": Rpt1a = SetRptDuesCollStatsTerranova(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDuesAOverDues": Rpt1a = SetRptDuesAOverDues(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDuesAOverDuesInd": Rpt1a = SetRptDuesAOverDuesInd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerTropical": Rpt1a = SetRptClientLedgerTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerRup": Rpt1a = SetRptClientLedgerRup(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerLeisure": Rpt1a = SetRptClientLedgerLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedger": Rpt1a = SetRptClientLedger(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerSuvastu": Rpt1a = SetRptClientLedgerSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerBridge": Rpt1a = SetRptClientLedgerBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerManama": Rpt1a = SetRptClientLedgerManama(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_23_CR.RptYearlyCollectionForecasting": Rpt1a = SetRptYearlyCollction(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDishonourCheque": Rpt1a = SetRptDishonourCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptSalClPayDetails": Rpt1a = SetRptSalClPayDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustDuesInfo": Rpt1a = SetRptCustDuesInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustDuesInfoAssure": Rpt1a = SetRptCustDuesInfoAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustDuesInfoLeisure": Rpt1a = SetRptCustDuesInfoLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptAllProDuesCollection": Rpt1a = SetRptAllProDuesCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptDuesCollection02": Rpt1a = SetRptDuesCollection02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptInfAndActReceived": Rpt1a = SetRptInfAndActReceived(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptMonCollcSchedule": Rpt1a = SetRptMonCollcSchedule(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptMonthlyCollReceiptType": Rpt1a = SetRptMonCollReceiptType(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptProjectWiseCollection": SetRptProjectWiseCollection(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustomerInvoice02S": SetRptCustomerInvoice02S(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustomerInvoice02": SetRptCustomerInvoice02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptClientLedgerBridge02": Rpt1a = SetRptClientLedgerBridge02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerLeisure02": Rpt1a = SetRptClientLedgerLeisure02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerRup02": Rpt1a = SetRptClientLedgerRup02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedger02": Rpt1a = SetRptClientLedger02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedger02P2P": Rpt1a = SetRptClientLedger02P2P(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedger02Lanco": Rpt1a = SetRptClientLedger02Lanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerAssure": Rpt1a = SetRptClientLedgerAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedger02Cube": Rpt1a = SetRptClientLedger02Cube(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientLedgerFinlay": Rpt1a = SetRptClientLedgerFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptRptClientLedgerRup02": Rpt1a = SetRptClientLedgerRup02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptMonCollcScheduleSummaryENG": Rpt1a = SetRptMonCollcScheduleSummaryENG(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptMonCollcScheduleSummaryBAN": Rpt1a = SetRptMonCollcScheduleSummaryBAN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptMonCollcScheduleDetENG": Rpt1a = SetRptMonCollcScheduleDetENG(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptMonCollcScheduleDetBAN": Rpt1a = SetRptMonCollcScheduleDetBAN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptPrjwiseCollofSummDetails": Rpt1a = SetRptPrjwiseCollofSummDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //Convert RDLC
                case "R_23_CR.RptCliMod": Rpt1a = SetRptCliMod(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustDetailsInfo": Rpt1a = SetRptCustDetailsInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCustAssociationFee": Rpt1a = SetRptCustAssociationFee(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptNetADWork": Rpt1a = SetRptNetADWork(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientDOBMrrDay": Rpt1a = SetRptClientDOBMrrDay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptClientInfo": Rpt1a = SetRptClientInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptEarlyPayBenifit": Rpt1a = SetRptEarlyPayBenifit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.rptCustomerInvoice03": Rpt1a = SetrptCustomerInvoice03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.rptCustomerInvoice04": Rpt1a = SetrptCustomerInvoice04(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptProClientSt": Rpt1a = SetRptProClientSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_23_CR.RptUtilityAndOtherCollAll": Rpt1a = SetRptUtilityAndOtherCollAll(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptUtilityAndOtherCollInd": Rpt1a = SetRptUtilityAndOtherCollInd(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_23_CR.RptCollectionStatusLO": Rpt1a = SetRptCollectionStatusLO(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion
                #region Customer Care
                case "R_24_CC.RptAddWorkSuvastu": Rpt1a = SetRptAddWorkSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptSalesPermissionCost": Rpt1a = SetRptSalesPermissionCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptCostRegistration": Rpt1a = SetrptCostRegistration(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptLoanStatus": Rpt1a = SetRptLoanStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptRegisrationStatusAllPro": Rpt1a = SetrptRegisraionStatusAllPro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptCustLetter": SetRptCustLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptCustLetter01": SetRptCustLetter01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //Crystal to RDLC
                case "R_24_CC.rptClientChoice": SetrptClientChoice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptMaintenanceWrk": SetRptMaintenanceWrk(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptMaintenanceWrkAssure": SetRptMaintenanceWrkAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptMaintenanceWrkSan": SetRptMaintenanceWrkSan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptMaintenanceWrkEpic": SetRptMaintenanceWrkEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptCustLnStatus": SetRptCustLnStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptHandOverWork": SetRptHandOverWork(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptComplain": SetRptComplain(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.rptCastingLeltter": SetrptCastingLeltter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_24_CC.RptClientModification": Rpt1a = SetRptClientModification(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region R_26_Alert

                case "R_26_Alert.RptGenModuleDesc": SetRptGenModuleDesc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region Marketing Procurement
                case "R_28_MPro.RptMktRequisition": Rpt1a = SetRptMktRequisition(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_28_MPro.RptMktPurMarketSurvey": Rpt1a = SetRptMktPurMarketSurvey(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_28_MPro.RptMktPurMRR": Rpt1a = SetRptMktPurMRR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_28_MPro.RptMktPurchaseTracking": Rpt1a = SetRptMktPurchaseTracking(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_28_MPro.RptMktMatIssue": Rpt1a = SetRptMktMatIssue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion

                #region MIS Module
                case "R_32_Mis.RptProjectSummary": Rpt1a = SetRptProjectSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectWisResource": Rpt1a = SetRptProjectWisResource(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptTrailsBal3": Rpt1a = SetRptTrailsBal3(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptTrialBalance2": Rpt1a = SetRptTrialBalance2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region R_32_Mis
                case "R_32_Mis.RptProjTrialBalance": Rpt1a = SetRptProjTrialBalance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjTrialBalanceDaywise": Rpt1a = SetRptProjTrialBalanceDaywise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_32_Mis.RptProjCost": Rpt1a = SetRptProjCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptCollBrkDown": Rpt1a = SetRptCollBrkDown(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjRecPay": Rpt1a = SetRptProjRecPay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptMonthWiseProjectPayment": Rpt1a = SetRptMonthWiserojectPyment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptMonthWiseProjectPaymentDet": Rpt1a = SetRptMonthWiserojectPymentDet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptRealInOutFlow": Rpt1a = SetRptRealInOutFlow(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptGpNpCalc": Rpt1a = SetRptGpNpCalc(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptInvestmentPlan": Rpt1a = SetRptInvestmentPlan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptInvestmentPlan02": Rpt1a = SetRptInvestmentPlan02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectReport02": Rpt1a = SetRptProjectReport02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectRemainingCost": Rpt1a = SetRptProjectRemainingCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptCostOfFund": Rpt1a = SetRptCostOfFund(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjCancellationUnit": Rpt1a = SetRptProjCancellationUnit(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptCashFlowBridge": Rpt1a = SetRptCashFlowBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptMasMonPlanVsAcvment": Rpt1a = SetRptMasMonPlanVsAcvment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptConProgramSum": Rpt1a = SetRptConProgramSum(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptConProgram": Rpt1a = SetRptConProgram(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptSourceAndUtilities": Rpt1a = SetRptSourceAndUtilities(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptComProCost": Rpt1a = SetRptComProCost(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectStatus": Rpt1a = SetRptProjectStatust(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectStatusLand": Rpt1a = SetRptProjectStatusLand(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptmBgdVsExp": Rpt1a = SetRptmBgdVsExp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptPrjIncomeSt": Rpt1a = SetRptPrjIncomeSt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProSummary": Rpt1a = SetRptProSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptMonProjectStatus": Rpt1a = SetRptMonProjectStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_32_Mis.RptProjectAnalysis": Rpt1a = SetRptProjectAnalysis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_45_GrAcc.RptGrpMisRecPayment": Rpt1a = SetRptGrpMisRecPayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region R_34_Mgt
                case "R_34_Mgt.RptOtherReqPrint": Rpt1a = SetRptOtherReqPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqPrintSuvasto": Rpt1a = SetRptOtherReqPrintSuvasto(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqStatus": Rpt1a = SetRptOtherReqStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqStatusLanco": Rpt1a = SetRptOtherReqStatusLanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqStatusISBL": Rpt1a = SetRptOtherReqStatusISBL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqStatusFinlay": Rpt1a = SetRptOtherReqStatusFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.UserLogDetails": Rpt1a = SetRptUserLogDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.rptActiveSimUser": Rpt1a = SetrptActiveSimUser(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_34_Mgt.RptOtherReqPrintFinlay": Rpt1a = SetRptOtherReqPrintFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_17_Acc.RptAdvancedAgainstLoan": Rpt1a = SetRptAdvancedAgainstLoan(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;



                #endregion

                #region R_38_AI

                case "R_38_AI.RptAIInvoicePrint": Rpt1a = SetRptAIInvoicePrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_38_AI.RptOngoingProjectPrint": Rpt1a = SetRptOngoingProjectPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                #region F_41_GAcc
                case "R_41_GAcc.RptProProgBillStatus": Rpt1a = SetRptProProgBillStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion

                #region Human Resource Management
                case "R_81_Hrm.R_91_ACR.RptEmpperformance": Rpt1a = SetRptEmpperformance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_81_Rec.RptAssYearly": Rpt1a = SetRptAssYearly(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_82_App.RptEmpAllInformationENG": Rpt1a = SetRptEmpAllInformationBAN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_82_App.RptHRCodeBookInfo": Rpt1a = SetRptHRCodeBookInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //Bonus Sheet
                case "R_81_Hrm.R_89_Pay.RptBankFordLetter": Rpt1a = SetRptBankFord(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheet": Rpt1a = SetRptBonusSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetTro": Rpt1a = SetRptBonusSheetTro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetTerranova": Rpt1a = SetRptBonusSheetTera(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetGreenWood": Rpt1a = SetRptBonusSheetGreenWood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebCash": Rpt1a = SetRptBonusSheetPebCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebProj": Rpt1a = SetRptBonusSheetPebProj(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebBank": Rpt1a = SetRptBonusSheetPebBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebFactory": Rpt1a = SetRptBonusSheetPebFactory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebFactoryCash": Rpt1a = SetRptBonusSheetPebFactoryCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetPebFactoryBank": Rpt1a = SetRptBonusSheetPebFactoryBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpBonusAssure": Rpt1a = SetRptEmpBonusAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetAcme": Rpt1a = SetRptBonusSheetAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetBTI": Rpt1a = SetRptBonusSheetBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetEdison": Rpt1a = SetRptBonusSheetEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSheetFinlay": Rpt1a = SetRptBonusSheetFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptSalaryGrndT": Rpt1a = SetRptSalaryGrndT(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryGrndTotalTropical": Rpt1a = SetRptSalaryGrndTotalTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptEmpMonthSumm": Rpt1a = SetRptEmpMonthSumm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptAitPurpose": Rpt1a = SetRptAitPurpose(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpAitCertificate": Rpt1a = SetRptEmpAitCertificate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                // case "R_81_Hrm.R_91_ACR.RptEmpEva": Rpt1a = RptRptEmpEva (Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //  case "R_81_Hrm.R_91_ACR.RptEmpEvalu": Rpt1a = SetRptEmpEvalu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_91_ACR.RptEmpEvaluation": Rpt1a = RptRptEmpEvaluation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.EmpFinalSettmnt": Rpt1a = SetEmpFinalSettmnt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptEmpMonthPresence": Rpt1a = RptEmpMonthPresence(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptIncomeSatatement": Rpt1a = SetRptSalaryIncomeStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalStatNagad": Rpt1a = SetrptSalStatementNagad(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSumm": Rpt1a = SetrptRptSalSumm(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBankStatement": Rpt1a = SetrptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBankStatementCPDL": Rpt1a = SetrptBankStatementCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.rptBankStatementEdison": Rpt1a = SetrptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBankStatementFinlay": Rpt1a = SetrptBankStatement(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBankStatementFinlayExcel": Rpt1a = SetRptBankStatementFinlayExcel(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_81_Hrm.R_89_Pay.rptBankStatementPEB": Rpt1a = SetrptBankStatementPEB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBankStatementAlli": Rpt1a = SetrptBankStatementAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetter": Rpt1a = SetrptForLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetterCPDL": Rpt1a = SetrptForLetterCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetterBridge": Rpt1a = SetrptForLetterBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetterTerra": Rpt1a = SetrptForLetterTerra(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetterGreenwood": Rpt1a = SetrptForLetterGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptForLetterEdison": Rpt1a = SetrptForLetter(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.rptAccTransfer": Rpt1a = SetrptAccTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipGreenwood": Rpt1a = SetRptPaySlipGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummaryBridge": Rpt1a = SetrptRptSalSummaryBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummaryAlliance": Rpt1a = SetRptSalSummaryAlliance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummary2": Rpt1a = SetRptSalSummary2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummaryFinlay": Rpt1a = SetRptSalSummaryFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummDetails": Rpt1a = SetRptSalSummDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBankStatementGreenwood": Rpt1a = SetrptBankStatementGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBankStatementBridge": Rpt1a = SetrptBankStatementBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                // bonus summary
                case "R_81_Hrm.R_89_Pay.RptBonusSummaryPEB": Rpt1a = SetRptBonusSummaryPEB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSummaryBridge": Rpt1a = SetRptBonusSummaryBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSummaryAlli": Rpt1a = SetRptBonusSummaryAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptBonousSummaryAssure": Rpt1a = SetrptBonousSummaryAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                // Allowance
                case "R_81_Hrm.R_86_All.rptMobileAllowanceAssure": Rpt1a = SetrptMobileAllowanceAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_86_All.rptMobileAllowance": Rpt1a = SetrptMobileAllowance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_86_All.rptMobileAllowAcme": Rpt1a = SetrptMobileAllowAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_86_All.rptMobileAllowanceTro": Rpt1a = SetrptMobileAllowanceTro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_86_All.rptMobileAllowAlli": Rpt1a = SetrptMobileAllowAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_87_Tra.RptHREmpTransferReport": Rpt1a = SetRptHREmpTransferReport(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                //Crystal to Rdlc by Parbaz
                //Salary Sheet
                case "R_81_Hrm.R_89_Pay.RptSalaryDetails4": Rpt1a = SetRptSalaryDetails4(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsRG": Rpt1a = SetRptSalaryDetailsRG(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryRatulPro": Rpt1a = SetRptSalaryRatulPro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsBridge": Rpt1a = SetRptSalaryDetailsBridge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsTropical": Rpt1a = SetRptSalaryDetailsTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsGreenwood": Rpt1a = SetRptSalaryDetailsGreenwood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsTerranova": Rpt1a = SetRptSalaryDetailsTerranova(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryAssure": Rpt1a = SetRptSalaryAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryAssureTourism": Rpt1a = SetRptSalaryAssureTourism(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryAssSecurity01": Rpt1a = SetRptSalaryAssSecurity01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryLeisure": Rpt1a = SetRptSalaryLeisure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsAlliance": Rpt1a = SetRptSalaryDetailsAlliance(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsAcme": Rpt1a = SetRptSalaryDetailsAcme(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsAcmeAI": Rpt1a = SetRptSalaryDetailsAcmeAI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastuHeadOffice": Rpt1a = SetRptSalaryDetailsSuvastuHeadOffice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastu": Rpt1a = SetRptSalaryDetailsSuvastu(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBCash": Rpt1a = SetRptSalaryDetailsPEBCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBBank": Rpt1a = SetRptSalaryDetailsPEBBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEB": Rpt1a = SetRptSalaryDetailsPEB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryCashDriver": Rpt1a = SetRptSalaryDetailsPEBFactoryCashDriver(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryDriverBank": Rpt1a = SetRptSalaryDetailsPEBFactoryDriverBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactory": Rpt1a = SetRptSalaryDetailsPEBFactory(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryCash": Rpt1a = SetRptSalaryDetailsPEBFactoryCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank": Rpt1a = SetRptSalaryDetailsPEBHeadOfficeBank(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank2": Rpt1a = SetRptSalaryDetailsPEBHeadOfficeBank2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeCheque": Rpt1a = SetRptSalaryDetailsPEBHeadOfficeCheque(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOffice": Rpt1a = SetRptSalaryDetailsPEBHeadOffice(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeCash": Rpt1a = SetRptSalaryDetailsPEBHeadOfficeCash(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptCashPay02GreenWood": Rpt1a = SetRptCashPay02GreenWood(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptCashPay02Edison": Rpt1a = SetRptRptCashPay02Edison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptCashPay02Finlay": Rpt1a = SetRptRptCashPay02Finlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptCashPay02FinlayExcel": Rpt1a = SetRptCashPay02FinlayExcel(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsEdisonReal": Rpt1a = SetRptSalaryDetailsEdisonReal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryEntrust": Rpt1a = SetRptSalaryEntrust(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsJbs": Rpt1a = SetRptSalaryDetailsJbs(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryBTI": Rpt1a = SetRptSalaryBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsLanco": Rpt1a = SetRptSalaryDetailsLanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsCPDL": Rpt1a = SetRptSalaryDetailsCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryHOBTI": Rpt1a = SetRptSalaryHOBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryHOBTIExcel": Rpt1a = SetRptSalaryHOBTIExcel(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryHOBTIPdf": Rpt1a = SetRptSalaryHOBTIPdf(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDPBTI": Rpt1a = SetRptSalaryDPBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryCTGBTI": Rpt1a = SetRptSalaryCTGBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalarySicolBTI": Rpt1a = SetRptSalarySicolBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryCPBTI": Rpt1a = SetRptSalaryCPBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalSummaryPEB": Rpt1a = SetRptSalSummaryPEB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptGrpSummary": Rpt1a = SetRptGrpSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryFinlay": Rpt1a = SetRptSalaryFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryEpic": Rpt1a = SetRptSalaryEpic(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "R_81_Hrm.R_89_Pay.RptSalLACA": Rpt1a = SetRptSalLACA(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalLACAAssure": Rpt1a = SetRptSalLACAAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBankStmentComWise": Rpt1a = SetRptBankStmentComWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBankStmentBankWise": Rpt1a = SetRptBankStmentBankWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpLeaveStatus": Rpt1a = SetRptEmpLeaveStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptMonthWiseSalSheet": Rpt1a = SetRptMonthWiseSalSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptMonthWiseTax": Rpt1a = SetRptMonthWiseTax(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptMonthWiseTaxFinlay": Rpt1a = SetRptMonthWiseTaxFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //Employee Attendence
                case "R_81_Hrm.R_84_Lea.RptTimeOff": Rpt1a = SetRptTimeOff(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptLeaveDateRange": Rpt1a = SetRptLeaveDateRange(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptAllEmpLeavStatus": Rpt1a = SetRptAllEmpLeavStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptAllEmpLeavStatusBR": Rpt1a = SetRptAllEmpLeavStatusBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptMonWiseLeaveBR": Rpt1a = SetRptMonWiseEmpLeaveBR(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsManama02": Rpt1a = SetRptSalaryDetailsManama02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryDetailsManamaBN": Rpt1a = SetRptSalaryDetailsManamaBN(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_87_Tra.RptEmployeeTransfer": Rpt1a = SetRptEmployeeTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_87_Tra.RptEmployeeTransfer02": Rpt1a = SetRptEmployeeTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_85_Lon.rptEmpLoanStatus": Rpt1a = SetrptEmpLoanStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_85_Lon.rptEmpLoanInsDetails": Rpt1a = SetrptEmpLoanStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonthlyLateAttn03": Rpt1a = SetRptMonthlyLateAttn03(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptDeptWiseEmp": Rpt1a = SetRptDeptWiseEmp(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptAttendanceSummary": Rpt1a = SetRptAttendanceSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptHRAllEmpStatus": Rpt1a = SetRptHRAllEmpStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptRetiredEmployee": Rpt1a = SetRptRetiredEmployee(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptEmpConfirmation": Rpt1a = SetRptEmpConfirmation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_83_Att.RptMonAttnSumEmpWise": Rpt1a = SetRptMonAttnSumEmpWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //Pay Slip
                case "R_81_Hrm.R_89_Pay.RptPaySlip1": Rpt1a = SetRptPaySlip1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipTro": Rpt1a = SetRptPaySlipTro(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipPEB": Rpt1a = SetRptPaySlipPEB(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipAssure": Rpt1a = SetRptPaySlipAssure(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlip": Rpt1a = SetRptPaySlip(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipEdisonReal": Rpt1a = SetRptPaySlipEdisonReal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipBTI": Rpt1a = SetRptPaySlipBTI(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipFinlay": Rpt1a = SetRptPaySlipFinlay(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipLanco": Rpt1a = SetRptPaySlipLanco(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptPaySlipCPDL": Rpt1a = SetRptPaySlipCPDL(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                //Bonus Sheet
                case "R_81_Hrm.R_89_Pay.RptBonusBridge1": Rpt1a = SetRptBonusBridge1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusAlli": Rpt1a = SetRptBonusAlli(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusTropical": Rpt1a = SetRptBonusTropical(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptBonusSigSheet": Rpt1a = SetRptBonusSigSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSignatureSheet": Rpt1a = SetRptSignatureSheet(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpBonus": Rpt1a = SetRptEmpBonus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptEmpBonusEdison": Rpt1a = SetRptEmpBonusEdison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.RptSalaryReconciliation": Rpt1a = SetRptSalaryReconciliation(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                //nahid 20210703
                case "R_81_Hrm.R_92_Mgt.RptEmpListJoinDateWise": Rpt1a = SetRptEmpListJDateWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpListBirthDateWise": Rpt1a = SetRptEmpListBirthDateWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptInactiveEmplists": Rpt1a = SetRptInactiveEmplists(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RpTotalEmplists": Rpt1a = SetRpTotalEmplists(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_92_Mgt.RptTransList": Rpt1a = SetRptTransList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpSpList": Rpt1a = SetRptEmpSpList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptDateWiseEmpHold": Rpt1a = SetRptDateWiseEmpHold(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptJoiningStatus": Rpt1a = SetRptJoiningStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptPabxInfoList": Rpt1a = SetRptPabxInfoList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_93_AnnInc.RptIncrementStatus": Rpt1a = SetRptIncrementStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_93_AnnInc.RptIncrementLetter": Rpt1a = SetRptIncrementStatus(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_92_Mgt.RptEmpSattelment": Rpt1a = SetRptEmpSattelment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_92_Mgt.RptEmpSattelmentBangla": Rpt1a = SetRptEmpSattelmentBangla(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_84_Lea.RptEmployeeLeaveRecord": Rpt1a = SetRptEmployeeLeaveRecord(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_84_Lea.RptYearlyLeaveRecord": Rpt1a = SetRptYearlyLeaveRecord(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptAllBankSummary": Rpt1a = SetAllBankSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptGrossRecon": Rpt1a = SetGrossRecon(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.rptModePayment": Rpt1a = SetModePayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptModePaymentExcel": Rpt1a = SetrptModePaymentExcel(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "R_81_Hrm.R_89_Pay.rptNetComparison": Rpt1a = SetNetComparison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptGrossComparison": Rpt1a = SetGrossComparison(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptTotalSal": Rpt1a = SetTotalSal(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_89_Pay.rptSalsumDept": Rpt1a = SetSalsumDept(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;




                //hrm envelop
                case "R_81_Hrm.R_97_MIS.RptHrmPaySlipEnvelop": Rpt1a = SetRptHrmPaySlipEnvelop(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_81_Hrm.R_97_MIS.RptPromotionEnvelop": Rpt1a = SetRptPromotionEnvelop(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_22_Sal.RptEnvelopMoneyReceipt": Rpt1a = SetRptEnvelopMoneyReceipt(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #endregion

                #region Management
                case "R_34_Mgt.UserLoginInfo": Rpt1a = SetUserLoginInfo(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion

                #region Fixed Asset
                case "R_29_Fxt.RptFixedAssetTransfer": Rpt1a = SetRptFixedAssetTransfer(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptDepricationCharge": Rpt1a = SetRptDepricationCharge(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptFixAssetEquipWise": Rpt1a = SetRptFixAssetEquipWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptFixAssetLocWise": Rpt1a = SetRptFixAssetLocWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptFixAssetUserWise": Rpt1a = SetRptFixAssetUserWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptMatStkQtyBasis": Rpt1a = SetRptMatStkQtyBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptMatStkAmtBasis": Rpt1a = SetRptMatStkAmtBasis(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.rptFxtAsstStatusDetails": Rpt1a = SetrptFxtAsstStatusDetails(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.rptFxtAsstStatusWD": Rpt1a = SetrptFxtAsstStatusWD(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.rptFxtAsstValue": Rpt1a = SetrptFxtAsstValue(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptEquipStResWise": Rpt1a = SetRptEquipStResWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptEquipStEmpWise": Rpt1a = SetRptEquipStEmpWise(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptEquipStSummary": Rpt1a = SetRptEquipStSummary(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptFixedAsstDate": Rpt1a = SetRptFixedAsstDate(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_29_Fxt.RptDeptITStock": Rpt1a = SetRptDeptITStock(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                #endregion


                #region InterfaceArea
                case "R_99_AllInterface.RptGbFinalApproval": Rpt1a = SetRptGbFinalApproval(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                #endregion

                case "R_02_Fea.rptEstmtProfitLoss": Rpt1a = SetrptEstmtProfitLoss(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                #region Services
                case "R_70_Services.RptQuotationPrint": Rpt1a = SetRptQuotationPrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "R_70_Services.RptInvoicePrint": Rpt1a = SetRptInvoicePrint(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                    #endregion



            }

            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSheetFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalSummaryFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>)RptDataSet));
            return Rpt1a;
        }
        //R_81_Hrm.R_89_Pay.RptSalSummaryFinlay
        private static LocalReport SetRptSalaryEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipLanco(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPabxInfoList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptYearlyLeaveRecord(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.YearlyLeaveRecord>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetAllBankSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptModePaymentExcel(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BankDesc>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetModePayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BankDesc>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetTotalSal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetSalsumDept(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.DeptWiseSal>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetNetComparison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetGrossComparison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>)RptDataSet2));

            return Rpt1a;
        }


        private static LocalReport SetGrossRecon(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAIInvoicePrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_38_AI.AIallPrint.InvoicePrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptOngoingProjectPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_38_AI.AIallPrint.RptOngoingProject>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmployeeTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_87_Tra.EmployeeTransInfo01>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptDayWisePurchase(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptDayWisePurchase>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptDayWisePurchaseEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptDayWisePurchase>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptPurchaseSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetrptPurchaseBillTk(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillTracking>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptBillTracking(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.GeneralBillTracking>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptPrjWiseMrfHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPrjWiseMrfHistory>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptDateWiseReqCheckHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.DateWiseReqCheckHistory>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetrptDeliveryEfficiency(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.DeliveryEffciency>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.ChequeSheet01>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptComSalesServey(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesSurveyEntry>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptIndentIssueStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassMaterial.IndentStatus>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptIndentIssueStatusSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassMaterial.IndentStatusSummary>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptTransactionSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.TransactionSt>)RptDataSet));

            return Rpt1a;
        }private static LocalReport SetRptSalesOpening(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesOpening>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptTopSheetFactory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalSummaryInfo>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptLetterOfAllotmentCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.AllotmentInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.Rptalloreport>)RptDataSet2));

            return Rpt1a;
        }



        private static LocalReport SetRptSalEncashment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalEncashment>)RptDataSet));

            return Rpt1a;
        }



        private static LocalReport SetRptPurchaseTrack02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EclassReqAllOrderList>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.EclassReqAllMrrList>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptBudgetTracking(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EclassBudgetTracking>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptSuplistWithMat(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EclassSuplistWithMat>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptBalanceSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>)RptDataSet));
            // Session["tbl"] = RptDataSet;
            //Rpt1a.SubreportProcessing += new SubreportProcessingEventHandler(loadSubreport);
            return Rpt1a;
        }

        //private static void loadSubreport(object sender, SubreportProcessingEventArgs e)
        //{
        //    // DataTable dt = (DataTable)ViewState["tblAcc"];     
        //    //var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>();       
        //    ////  List<RealEntity.C_17_Acc.EClassFinanStatement.CashFlowIndirect>)RptDataSet




        //    e.DataSources.Add(new ReportDataSource("DataSet1"));


        //}






        private static LocalReport SetRptCashFlowIndirect(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.CashFlowIndirect>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptShareQty(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.IncomeStatementSHE>)RptDataSet));
            return Rpt1a;
        }
        //private static LocalReport SetRptBalanceSheetUddl(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        //{
        //    Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>)RptDataSet));
        //    return Rpt1a;
        //}


        private static LocalReport SetRptIncomeSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.NoteIncoStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTenderProposal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_08_PPlan.BO_Class_Con.RptTenderProposal>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCivilConBOQ(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_07_Ten.RptCivilConBOQ>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCivilConBOQTender(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_07_Ten.RptCivilConBOQ>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptClientModification(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.RptClientModification>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCashBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptCashBank>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCashBankWithdraw(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptCashBank>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccOpening(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.AccOpening>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccOpeningDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.AccOpening>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSupplierOvAllPSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptSupplierOverAllPSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSupplierOvAllPSummaryDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptSupplierOverAllPSummaryDetails>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptProjectDesign(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_08_PPlan.BO_Class_Con.ProjectDesign>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProTargetTimeBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_08_PPlan.BO_Class_Con.ProjectTargetAnalysis>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptAccConSchedule01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.AccControlSchedule01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport RptSalSummeryDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalSummeryDetails>)RptDataSet));
            return Rpt1a;
        }

        //envelop

        private static LocalReport SetRptEnvelopPrintNew(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEnvelopMoneyReceipt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEnvelopOffice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEnvelopBirthday(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEnvelopAniversary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEnvelopCongratulation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EnvelopModel>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptWorkOrder2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkOrderCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptWorkOrderP2PBN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeIBBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt1", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam2", hshParm["projnam2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam3", hshParm["projnam3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam4", hshParm["projnam4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam5", hshParm["projnam5"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projamt1", hshParm["projamt1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt2", hshParm["projamt2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt3", hshParm["projamt3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt4", hshParm["projamt4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt5", hshParm["projamt5"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));


            return Rpt1a;
        }

        private static LocalReport SetPrintChqFinlayBRAC(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("compName", hshParm["compName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt1", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));

            return Rpt1a;
        }
        private static LocalReport SetRptChequeSBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            //Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));

            return Rpt1a;
        }

        private static LocalReport SetRptNoteSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.FinancialPosition02>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDetailSheduleTB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.DetailsScheduleTB>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPrjWiseMaterialCosting(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.PrjWiseMaterialCost>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptPrintVocherAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptPrintVocherAlli02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherAlli03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucher1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucher2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucher3(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucher4(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucher5(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVoucherManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVoucherCube(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }




        private static LocalReport SetrptPrintVoucher6(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVocherEntrust01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherEntrust02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptPrintVocherEntrust03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVocherJBS01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherJBS02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVocherJBS03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherIntech01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherIntech02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptPrintVocherIntech03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherGreenwood01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherGreenwood02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherGreenwood03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherJVTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherBDTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherCCTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptPrintVoucher7(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherTanvir(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherISBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherISBL02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherFinlay02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a; 
        }
        private static LocalReport SetrptPrintVoucherCPDL02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;  
        }
        private static LocalReport SerptPrintVoucherEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherSuvastu02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherSuvastu03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherCredence01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherCredence02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVocherCredence03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPrintVoucherDefault(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherIntech(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherAlliance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher3(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher4(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher5(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucher6(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherCredence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherEntrust(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherP2P(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankVoucherTanvir(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankVoucherEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccCashbook1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.AccCashBankBook1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccCashbook1Credence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.AccCashBankBook1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyPayProposal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDelDailyTransaction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccBankPosition02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.BankPosition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBudVsAchivDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.WorkingBudgetVsAchievement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptAccBudVsExpen(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.WorkingBudgetVsAchievement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipSupplierLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPurNotUpdated(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.PurchaseNotYetUpdated>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeDepositAllBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeDepositBank02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthWiseBankLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.MonthWiseBankLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonSalPerTarWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.MonSalPerTarWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptVoucherTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassAccVoucher.VouTopSheetSum>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptVoucherTopSheetFinaly(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassAccVoucher.VouTopSheetSum>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetrptOthersAccCode(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.CodeBookInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccCodeBookAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.CodeBookInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInvoiceP2P360(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.InvoiceP2P360>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyTransactionCredence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPurMktSurveyP2P02(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyP2P05(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyP_2_P(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }        
        private static LocalReport SetRptPurMktSurveyCPDL03(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyCPDL05(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyCPDL04(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyEpic02(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyEpic03(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyEpic05(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyCPDL03C(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset) 
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }private static LocalReport SetRptPurMktSurveyCPDL04C(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset) 
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }private static LocalReport SetRptPurMktSurveyCPDL05C(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)rptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)rptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptUserLogDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassUserLog>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_34_Mgt.EClassUserLogSummary>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetrptActiveSimUser(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_33_Doc.ActiveSimUser>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptEstmtProfitLoss(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_02_Fea.EClasFeasibility.ProfitAndLoss>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_02_Fea.EClasFeasibility.AgeingDays>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_02_Fea.EClasFeasibility.ProfitAndLoss>)UserDataset));
            return Rpt1a;
        }


        private static LocalReport SetRptBillInvoiceP2P(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.BillEmtry>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInvoiceAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.BillEmtry>)RptDataSet));
            return Rpt1a; 
        }

        private static LocalReport SetRptWorkOrderP2P(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)RptDataSet));
            return Rpt1a;
        }
        
       private static LocalReport SetRptWorkOrderAcmeConst(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.Workorder03>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProposalFromAcmeConst(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.Workorder03>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSubconbillreqP2p(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.SubConBillReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSubConBillTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.SubConBillTopSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillIntech(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSubConOverAll2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.RptSubConOverAll2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSubConOverAll(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.RptSubConBillOverAll>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport Setrptsubconbill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.SubConAllBill>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPerodSubConBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.SubConAllBill>)RptDataSet));
            return Rpt1a;
        } 
        private static LocalReport SetRptConBillCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollReceiptType(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.RptMonthlyCollecionReceiptType>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectWiseCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.RptProjectWiseCollectionStatus>)RptDataSet));
            return Rpt1a;
        }
        //RptCustomerInvoice02S
        private static LocalReport SetRptCustomerInvoice02S(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptCustomerInvoice02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_23_CRR.EClassCutomer.Customer_Invoice01>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptProjCancellationUnit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjCancellationUnit>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSourceWiseLeads(LocalReport Rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ESourceWiseLeadsclass.CallCenterLeads>)rptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCashFlowBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CashFlowBankWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMasMonPlanVsAcvment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.MMonPlnVsAchAllPro>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConProgramSum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CatWiseConProgressAllPro>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConProgram(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CatWiseConProgress>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSourceAndUtilities(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.IndiProjCost12Month>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptComProCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.AnyCostAllProject>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectStatust(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectStatusLand(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptmBgdVsExp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.BudgetVsExpensesAllProj>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPrjIncomeSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectIncomeSt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonProjectStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.MontWiseProjectStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectAnalysis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptProjectAnalysis>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptGrpMisRecPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataSet)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_45_GrAcc.RptGrpMis.RptGrpRecPayment>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_45_GrAcc.RptGrpMis.RptGrpRecPaymentBank>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptCallCenterLead(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.EClassAdvertisement.EClassRptCallCenterLead>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCrmClientInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.CrmClientInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptInResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptInResource>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMatTransStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassMaterial.MatTransStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMatTransStatusUrban(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassMaterial.MatTransStatus>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptPurchaseOrder(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.PurchaseOrderInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition>)RptDataSet2));

            return Rpt1a;
        }
        private static LocalReport SetRptPurchaseOrderJBS(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.PurchaseOrderInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition>)RptDataSet2));

            return Rpt1a;
        }

        private static LocalReport SetRptPurVarAa(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptPurVarA>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPurchaseStatus1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptDayWisePurchase>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMaterialStockPrjWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialStock>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptOtherReqStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptOtherReqStatusLanco(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptOtherReqStatusISBL(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)rptDataSet));
            return rpt1a;
        }  
        private static LocalReport SetRptOtherReqStatusFinlay(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)rptDataSet));
            return rpt1a; 
        }
        private static LocalReport SetRptAllSupaConPaymentAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassSupOrConPayment>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptMonthlySubConBillAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptMonthlySubConBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptSalStatementNagad(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalaryStatementNagad>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptRptSalSumm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalarySumm>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptRptSalSummaryBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalSummaryAlliance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalSummary2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalSummDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSummaryPEB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSummaryBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSummaryAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBonousSummaryAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankStatementCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankStatementFinlayExcel(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankStatementGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankStatementBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptBankStatementPEB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptBankStatementAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptForLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptForLetterCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptForLetterGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptForLetterBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptForLetterTerra(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptAccTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlip1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipTro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipPEB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipEdisonReal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlip(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusBridge1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSigSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSignatureSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpBonus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpBonusEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryReconciliation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalaryReconciliation>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollcSchedule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassMonthlyCollectionSchedule>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassMonthlyCollectionScheduleProject>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptReqEntry03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqEntryInns(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqEntryTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRptReqEntryCons(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTranLinkPost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptTranLinkPost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReqEntry02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqEntryEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a; 
        }


        private static LocalReport SetRptReqEntry07(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptReqEntry08(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqEntryManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReqEntryEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReqEntry02Assure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqEntryiNTECH(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReqEntryJBS(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInvenAmtBasisPeriodic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInvenQtyBasisPeriodic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.InventoryQtyBasisPeriodic>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInvenAmtBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInvenQtyBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInvenAmtBasisDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectPettyCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptProjectPettyCash>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCostOfFund(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptBankInterestAllocation>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectSecification(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptProjectSpecification>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCostOfSalesPerSft(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptProjectCostPersft>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTransactionLink(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptTransactionLink>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectRemainingCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptProjectRemainingCost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectCostPerSft(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptProjectCostPersft>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankReconc(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptBankReconc>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectTransaction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptTransactionList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectReport02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.RptProjectReport02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPaySlipSubContractor(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            return Rpt1a;
        }

        private static LocalReport SetRptPaySlipSupplier(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaySlipSupplierShuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptAccATITxVatProjWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptAitTaxVatProjectWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccATIVatDeduction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptAitVatSdDeduction>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptATIVat(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.TdsVdsSdDeducProjWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthlySubConBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptMonthlySubConBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthlySuppBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptMontlySupplierBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAitVatSdDeductionAllSupp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassAitVatSdDeduction>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPendingConBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassPendingConBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAllSupaConPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassSupOrConPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyPaymentDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptDailyPaymentDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyPaymentCostWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptDailyPaymentSummaryCostWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptDailyPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIssueVsPaySum(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassRptIssueVsPaymentSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthlyIssueVsPay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassRptIssueVsPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMatStkAmtBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassMaterialStock>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptFxtAsstValue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEquipStResWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEquipStEmpWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEquipStSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptFixedAsstDate(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetRegister>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDeptITStock(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.RptDeptITStock>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptFxtAsstStatusWD(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptFxtAsstStatusDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.FixedAssetsStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMatStkQtyBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassMaterialStock>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFixAssetUserWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetRegister>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFixAssetLocWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetRegister>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFixAssetEquipWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetRegister>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDepricationCharge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassDepricationCost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFixedAssetTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetTransfer>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptUnsoldUnit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptUnsoldUnit>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptCalTValAvgVal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptCalTValAvgVal>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInfAndActReceived(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassSalesSatusReport>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDuesCollection02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAllProDuesCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillForward(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillForward>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptInvestmentPlan02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.EClassMasterBgd>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInvestmentPlan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.EClassMasterBgd>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCustDuesInfoLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassCustomerDuesInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCustDuesInfoAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassCustomerDuesInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCustDuesInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassCustomerDuesInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptGpNpCalc(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.GpNpCalc>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalClPayDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptPurTransTrack(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EClassMaterialTransferTacking>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalRegisClearence02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EClassSaleRegisClearance>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalRegisClearence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EClassSaleRegisClearance>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptCustLetter01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassClientLetterInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptClientChoice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassClientChoice>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMaintenanceWrk(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMaintenanceWrkEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>)RptDataSet));
            return Rpt1a; 
        }
        private static LocalReport SetRptMaintenanceWrkAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMaintenanceWrkSan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustLnStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.LoanStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHandOverWork(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptComplain(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptCastingLeltter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.HandOverLetter>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptGenModuleDesc(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_29_Fxt.EClassFixedAsset.EquipmentUseStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassClientLetterInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptRegisraionStatusAllPro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassRegistrationStatusAllPro>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLoanStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassCustRegistrationStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDishonourCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassListOfReturnCheque>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRealInOutFlow(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.EClassRealInOutFlow>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthWiserojectPymentDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.EClassMonthWiseProjectPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthWiserojectPyment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.EClassMonthWiseProjectPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankInterest(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.BankInterest>)RptDataSet));
            return Rpt1a;
        }





        private static LocalReport SetRptSubConProposedPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassSubContractorProposedPayment>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport GetRptGrpwisePayable(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptMatGrpwisePayable>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptMatGrpwisePayable(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptMatGrpwisePayable>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport GetRptSupCreditLimit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptSupCreditLimit>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSupPayment02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RptSupPayment02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSupPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RptSupPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptPurchaseSummary02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptRequisitionStatus2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptRequisitionStatus2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptRequisitionStatus1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptRequisitionStatus1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport GetRptReqAppStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptReqAppStatus>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptMatIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMatIssue>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMatIssueBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMatIssue>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport GetRptMatIssueStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.RptMatIssStatus>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptInterComTransStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.InterComMaterial>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProPhyStock(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.ProjStock>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptImplemenPlan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassExecution.MonthlyPlan>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptBgdVsExe(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassExecution.BudgetVsExecution>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptConBillWorkWiseInns(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ContractorBillWorkWise>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptSubcontractorPrjWisebill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassExecution.SubConPrjBillDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTarVsPlan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ImpPlan02>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptPurIssueEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassExecution.WorkExecution>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptPrjWiseSubcontractorbill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.PrjwiseSubConBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectBgdResGrWiseDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BudMasterResGroupWiseDate>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBgdCostDifWork(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BgdProjectAnalysis>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMasterBudget(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BugMasterDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDetailsBudget(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BugMasterDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptOtherReqStatusSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptOtherReqStatusTanvir(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassOtherReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptSubConRat(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConLavel(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptFloorResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.LandBgdStdAna>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLandPurRegister(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.LandBgdStdAna>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBankStatementInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.BankStatementInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInterComTransStatu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Hashtable reportP = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("companyname", (string)reportP["companyname"]));
            Rpt1a.SetParameters(new ReportParameter("date", (string)reportP["date"]));
            Rpt1a.SetParameters(new ReportParameter("actdesc", (string)reportP["actdesc"]));

            Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.RptInvList.InterComMaterial>)RptDataSet));
            return Rpt1a;

        }



        private static LocalReport SetRptProjCostSales(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ProjCostSales>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjRecPay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ResPayt>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeSuvastuPBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt1", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam2", hshParm["projnam2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam3", hshParm["projnam3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam4", hshParm["projnam4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam5", hshParm["projnam5"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projamt1", hshParm["projamt1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt2", hshParm["projamt2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt3", hshParm["projamt3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt4", hshParm["projamt4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt5", hshParm["projamt5"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));

            return Rpt1a;
        }

        private static LocalReport SetRptMonWiseCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.MonthWisseSales>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccFinalReportsDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.bugdvExpensis>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptCollBrkDown(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIncomestIndPrj(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.InStIP>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTrialBalance2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.Trialbal02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReceiptPayable(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RecePaya>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectCost>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjTrialBalance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectTrlBal>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjTrialBalanceDaywise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectTrlBalDaywise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccProjectReport(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EClassProjectReport>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccProjectReport01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.PrijectCostN>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptNetDuesInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_99_AllInterface.Netdues01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccDetTrialBalance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.TrialBalDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalesReg02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SaleReg2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccConTrialBalance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.TrialHeadOf>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAllSupPaymentBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.AdvancedSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAllSupPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.AdvancedSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalesDetailSchdule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SaleDetailsSchedule>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDetailShedule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.AccDetailsSchedul>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccBalConfirmation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBal001>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCashFlow02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassFinanStatement.CashFlowIndirect>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRecAndPaymentProj(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptOtherReqPrintSuvasto(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.GenBillReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRecAndPaymentAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptBankBalance02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptBankBalance02Cube(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>)UserDataset));
            return Rpt1a;
        }

        private static LocalReport SetRptOpPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.OppPayment1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRecAndPaymentActual(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.IssuedVsColl>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectwiseReceptsandPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PWRPDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankPosition(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.BankPosition>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyTransaction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptChqIssuedGrpWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.DaywiseGpIssue>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPostDatCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.DayWiseissueCheek>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPaymentChqClearance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ChequeStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptUpconSabCon(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDuesCollStatsTerranova(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.DueCollStatmentRe>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDuesAOverDues(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassDuesAOverDues>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDuesAOverDuesInd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassDuesAOverDuesIndPro>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptClientLedgerTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptClientLedgerRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedgerLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedgerSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptClientLedgerBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptClientLedgerBridge02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptClientLedgerLeisure02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedgerRup02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrjwiseCollofSummDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.ProjWiseColSummaryDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCliMod(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.ClientModification>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustDetailsInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.UtilityOtherCharges>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustAssociationFee(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.AssociationFee>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptNetADWork(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.ClientModification>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientDOBMrrDay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.ClientDOBMrrDay>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientInfos>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEarlyPayBenifit(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassEarlyBenifit>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptCustomerInvoice03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.InvoicePrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptCustomerInvoice04(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassCutomer.InvoicePrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProClientSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.ProjectWiseClientStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptUtilityAndOtherCollAll(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.RptUtilityAndOtherCollection>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptUtilityAndOtherCollInd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.RptUtilityAndOtherCollection>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCollectionStatusLO(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CR.EClassLand.RptLandownerColStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollcScheduleSummaryENG(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.MonCollScheSummmay>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollcScheduleSummaryBAN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.MonCollScheSummmay>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollcScheduleDetENG(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.MonthlyColScheduleDet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonCollcScheduleDetBAN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.MonthlyColScheduleDet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedger02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }private static LocalReport SetRptClientLedger02P2P(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedgerFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedger02Lanco(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedgerAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptClientLedger02Cube(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptClientLedgerManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>)UserDataset));
            return Rpt1a;
        }


        private static LocalReport SetRptCustomerDewsOverd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.CustDewsOverDews>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIssueVsClr(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ReceiClear>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIssueClearence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ChqIssuClear>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptIssuedChequeBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ListIsssuChq>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptIssuedChequeCP(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ListIsssuChq>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptIssuedChequeCPALL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ListIsssuChq>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptIssuedCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ListIsssuChq>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjStarus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.ProjectStarus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrintVoucher02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrintVoucher01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrintVoucher(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetrptDailySaleVsCollTarget(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalSummery>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptOSalesSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.EClassSalesOpening>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptCollDetailsInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptCollDetailsinfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBookingtDues(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptBookingtDues>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptDWiseRealCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.SaleSummarySum>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRealCollDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RealCollDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRrptChqReceivedVsClr(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ChequeReceiClear>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCollChqStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.CqeCollChqSt>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPostDatedChqInHand(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ChequeInHand01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeInHand(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.ChequeInHand01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDelMonyRec(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.TrnStatInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptDayWiseSales(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.DaywiseSale>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSaleSoldUsold(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.SoldUnsold>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalSumAmtBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.SalasSumaryA>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalSumary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.SalesSumaryR>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCustPaySchedule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.PaymentScheduleN>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustPayScheduleCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.PaymentScheduleN>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustPayScheduleEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.PaymentScheduleN>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptResreceivable(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.Resreceivable>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPaymentStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.PaymentStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaymentStatusFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.PaymentStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaymentStatusEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.PaymentStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPaymentStatus02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.ClientPaymentStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptYePlanIncomeSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.YearlyPlan>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptStdAnaSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_04_Bgd.EClassBudget.BugdAna>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrjFloorWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.PrjFoorWise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccFinalReports(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.bugdvExpensis>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBudgetBalanceResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.bugdBalance>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWorkVsResVsAllocDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BugPlanInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectBgdVsAlloc(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BudBalAfterAppro>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOtherReqStatus01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.OtherReqStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWorkSchd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.WorkList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInWork(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptInResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.IndiMateDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrjBudgetedCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BudgTotalCost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWorkVsResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.WorkvsRes>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjBgdCon(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.ProjBgdCon>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectBgd(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BudgetInmStaSum>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProjectBgdFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.BudgetInmStaSum>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptMaterialsReqDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptMaterialsReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.EClassBudget.MatReq>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectBgdGrDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BugCostDetails>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBgdSales(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BgdSales>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPrjInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BProjInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPrjInfoEnt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BgdProInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptResourceBasis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BugbasAns>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOtherReqPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.GenBillReq>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_34_Mgt.GenBillSupdesc>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptOtherReqPrintFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.GenBillReq>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_34_Mgt.GenBillSupdesc>)RptDataSet2));
            return Rpt1a; 
        }
        private static LocalReport SetRptAdvancedAgainstLoan(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptAdvancedAgainstLoan>)RptDataSet));
            
            return Rpt1a; 
        }

        private static LocalReport SetRptBillRateEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.BillingRateEntry>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillInvoice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.BillEmtry>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptBillEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_16_Bill.BO_BillEntry.BillEmtry>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProProgBillStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_41_GAcc.ProProgBillStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCashCrPur(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptCashCrPur>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptLabourDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.Rptlabour>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptBgdvsExpense(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RptBgdvsExpense>)RptDataSet));
            return Rpt1a;

        }


        private static LocalReport SetRptDummyPaywithoutDiscount(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var rptlist = (RealEntity.C_22_Sal.EClassSales_02.Eclassdummyschedule)RptDataSet;
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", rptlist.Lst01EClassDumPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", rptlist.Lst02EClassAcPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", rptlist.Lst04EClasActual));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet4", rptlist.Lst05EClasActual01));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", rptlist.Lst06EClasInterestCalculation));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet6", rptlist.Lst03EClassSehedual));
            return Rpt1a;
        }

        private static LocalReport RptRptDummyPaySchidule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var rptlist = (RealEntity.C_22_Sal.EClassSales_02.Eclassdummyschedule)RptDataSet;
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", rptlist.Lst01EClassDumPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", rptlist.Lst02EClassAcPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", rptlist.Lst04EClasActual));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet4", rptlist.Lst05EClasActual01));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", rptlist.Lst06EClasInterestCalculation));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet6", rptlist.Lst03EClassSehedual));
            return Rpt1a;
        }
        private static LocalReport SetRptDummyPaywithoutDiscount02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var rptlist = (RealEntity.C_22_Sal.EClassSales_02.Eclassdummyschedule)RptDataSet;
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", rptlist.Lst01EClassDumPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", rptlist.Lst02EClassAcPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", rptlist.Lst04EClasActual));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet4", rptlist.Lst05EClasActual01));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", rptlist.Lst07EClassInterestDummyPay02));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet6", rptlist.Lst03EClassSehedual));
            return Rpt1a;
        }

        private static LocalReport RptRptDummyPaySchidule02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var rptlist = (RealEntity.C_22_Sal.EClassSales_02.Eclassdummyschedule)RptDataSet;
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", rptlist.Lst01EClassDumPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", rptlist.Lst02EClassAcPaSchdule));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", rptlist.Lst04EClasActual));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet4", rptlist.Lst05EClasActual01));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", rptlist.Lst07EClassInterestDummyPay02));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet6", rptlist.Lst03EClassSehedual));
            return Rpt1a;
        }

        private static LocalReport SetRptProjWBillQty(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.ProjwRptBillQty>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccSummaryInflow(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EclassSunOfFlow>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccSumCustPayStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.AccCustPayLedgerCHL>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.AccCustPayLedgerCHL>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptMoneyReceiptTro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceipt360(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSalClntInterest(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptSalClntInterestEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptSalClntInterestRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)UserDataset));
            return Rpt1a;
        }


        private static LocalReport SetRptEarlybenefitADelay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", (List<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEarlybenefitADelayCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EClassInterestDummyPay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.EClassClientSum>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetrptUnitFxInf(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.BudgetnSales>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthWiseNewSales(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptMonWiseNewSales>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalPaySchedule(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptSalPayScheduleCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptSalPayScheduleLanco(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales_02.RptSalPaySchedules>)UserDataset));
            return Rpt1a;
        }
        private static LocalReport SetRptPeriodicSalesWithCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.perodicsalesColl>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAvailbilityPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.RptAvailability>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSoldUnsoftInfGroupWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SoldUnsoftInfGroupWise>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptGrandNotesSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)RptDataSet2));
            
            return Rpt1a;
        } private static LocalReport SetRptSampleNotesSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassCoffGrandNoteSheet>)RptDataSet));
           
            return Rpt1a;
        }
        private static LocalReport SetRptGrandNotesSheetSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassGrandNoteSheet.EClassBaseGrandNoteSheet>)RptDataSet));
            
            return Rpt1a;
        }private static LocalReport SetRptSalClntInterestFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)UserDataset));
            
            return Rpt1a;
        }

        private static LocalReport SetRptSalClntInterestBr(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesInterest>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDetailMoneyRecept(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceipt1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDuesCollAll(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.DuesCollAll>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptWecon(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceipt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptIntech(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptEPIC(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAcknowledgementSlipCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptFinlay2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a; 
        }
        private static LocalReport SetRptMoneyReceiptAcme02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMoneyReceiptLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCustomerBillInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.CustomerBillInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTransStatement02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }

        
        private static LocalReport SetRptRptTransStatementCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptTransStatement03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptTransStatement02Finlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptGrpWiseBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.SubConGrpWBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjWisBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.SubConProjWBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSubConBillStu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.SubConBillStatus>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport GetRptSubBillDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptSubBillDetails>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptAdvancedVsPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptAdvVsPayment>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptOrderVsReceived(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptOrderVsReceived>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptsDayWiseAdvanced(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptDayWiseAdv>)RptDataSet));

            return Rpt1a;

        }

        private static LocalReport GetRptSupplierDueStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptSuppDueStatus>)RptDataSet));

            return Rpt1a;

        }

        private static LocalReport GetRptSupplierPayable(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.RptSuppPayable>)RptDataSet));

            return Rpt1a;

        }

        private static LocalReport GetRptPayStatusSupwise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPaymentSupWise>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptWorkOrdHisoryResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptWorkOrdHisResource>)RptDataSet));

            return Rpt1a;

        }

        private static LocalReport GetRptWorkOrderSupHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptWorkOrdHisSup>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptIndSupPurchae(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptIndSupPurchase>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptMatPurHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPurHisMatWise>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptMktSurveySupWiseMatList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptSupWiseMatList>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptMktSurveyMatWiseSupList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptMatWiseSupList>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport GetRptPurMktSurveyEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptPurMktSurvey>)RptDataSet));
            // Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.EClassSupAmt>)RptDataSet2));
            return Rpt1a;

        }

        private static LocalReport GetRptPurMktSurvey(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptPurMktSurvey>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetRptMaterialTrnsfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetRptMatTransferRec(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport GetRptMaterialTrnsGatepass(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.PurGetPass>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetRptMaterialTrnsferReq(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.PurMatReq>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport SetRptMaterialTrnsferP2P(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.PurEqisition.RptMatTransReq>)RptDataSet));
            return Rpt1a;

        }



        private static LocalReport GetrptPurMrrEntryBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport GetrptPurMrrEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetrptPurMrrEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport GetrptPurMrrEntryFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptPurMrrEntryCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport GetrptProMatStock2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.ErptStock>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetrptProMatStock2Leisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.ErptStock>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport GetrptProMatStock(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.ErptStock>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport RptRptSubContEnlistmentForm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EClassSuppaContractior02>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport RptMatRateVar(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EClassMatRateVar>)RptDataSet));
            return Rpt1a;

        }



        private static LocalReport SetRptSupPaymentProposal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SupPayPro>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptImpExeStatus1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassExecution.EmplemanPlan>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSubConSD(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ContractorBillDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptrptLabIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptLabIssueUrban(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptLabIssueSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptLabIssueAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptLabIssueAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLabIssueRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.ConRaBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.EClassSubConBillFinalTopSheet>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBillAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillEdi(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptConBillRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBillAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBillBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptConBillBridgeWithoutLogo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptConBillAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptConBillAssure02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillFinaly(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConBillEpic(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBillCrdence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptConBillEdisonErp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillApprovalSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.BillApproval>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSubConBillWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.SubConBill.Subconbillwise>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSubConBillFinalization(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLabIssueSubCon(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.Labissue>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWeeklyCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalesStatusGen(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.SalesStatus>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptProjectTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_02_Fea.EClasFeasibility.ProjectTopSheet01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptFeaProject(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_02_Fea.EClasFeasibility.EClassProjectFeasibility>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptCostAnlys(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_02_Fea.EClasFeasibility.ProdCostAnalysis>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptlandDataBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_01_LPA.BO_Fesibility.Landdatabank01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPendingBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EclassPendingBill.ClassPendingBil>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptCashBankPosGrp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpReport>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCsahBankGrpMonth(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpMonthRpt>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptGeneralBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccVoucher.EClassGenReq>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCashBankGrpMonthDts(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.CashBankGrpMonthDtsRpt>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptAddWorkSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalesPermissionCost(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.RptSalesPermissionCost>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptCostRegistration(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_24_CC.RptCstRegstration>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLandFeasibility(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_01_LPA.BO_Fesibility.LandFesibility>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLandInformation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_01_LPA.BO_Fesibility.EClassLandInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetrptProjectFeasibility(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_02_Fea.EClasFeasibility.EClassProFeasibility>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjFeasibilityManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            DataTable dtm = new DataTable();
            dtm = ASITUtility03.ListToDataTable((List<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>)RptDataSet);
            DataView dv = new DataView();
            dv = dtm.Copy().DefaultView;
            dv.RowFilter = ("grp like 'A%'");
            DataTable dt = dv.ToTable();

            DataView dv1 = dtm.Copy().DefaultView;
            dv1.RowFilter = ("grp like 'B%'");
            DataTable dt1 = dv1.ToTable();

            DataView dv2 = dtm.Copy().DefaultView;
            dv2.RowFilter = ("grp like 'C%'");
            DataTable dt2 = dv2.ToTable();

            DataView dv3 = dtm.Copy().DefaultView;
            dv3.RowFilter = ("grp like 'D%'");
            DataTable dt3 = dv3.ToTable();

            DataView dv4 = dtm.Copy().DefaultView;
            dv4.RowFilter = ("grp like 'E%'");
            DataTable dt4 = dv4.ToTable();

            var list1 = dt.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();
            var list2 = dt1.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();
            var list3 = dt2.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();
            var list4 = dt3.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();
            var list5 = dt4.DataTableToList<RealEntity.C_02_Fea.EClasFeasibility.ProjectFeasibility>();

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", list1));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", list2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", list3));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet4", list4));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet5", list5));
            return Rpt1a;
        }

        private static LocalReport SetEmpFinalSettmnt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpFinalSettlmnt>)RptDataSet));

            return Rpt1a;
        }




        private static LocalReport SetRptSalaryIncomeStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.Incomesalary>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport RptEmpMonthPresence(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.MonthlyPresent>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport RptRptEmpEvaluation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_91_ACR.EMpEvaluation>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_91_ACR.Rptnumber>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_81_Hrm.C_91_ACR.RptEmpDetails>)UserDataset));
            return Rpt1a;
        }


        //private static LocalReport SetRptEmpEvalu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        //{
        //    // Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EMpEvaluationn>)RptDataSet));
        //    return Rpt1a;
        //}

        private static LocalReport SetRptPettyCashBillApprSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.EclassPettyCash>)RptDataSet));
            return Rpt1a;
        }
        //private static LocalReport SetRptPendingBill ( LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset )
        //{
        //    Rpt1a.DataSources.Add (new ReportDataSource ("DataSet1", (List<RealEntity.C_17_Acc.>)RptDataSet));
        //    return Rpt1a;
        //}

        private static LocalReport SetRptBugIncmStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BugIncmStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBgdFlrWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BgdFlrWise>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSPLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSPLedgerIntect(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSPLedger02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSPLedger02Intech(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSPLedgerRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>)RptDataSet));
            return Rpt1a;
        }




        private static LocalReport SetRptLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptLedgerTanvir(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptAccLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccLedgerWqty(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccLedgerBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccLedgerTerra(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRptAccLedgerRup(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccLedgerCube(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccLedgerIntech(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptAccSLedger(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetLedger", (List<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptPaymentIncSch(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.LedgerinSch>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptVoucherPrintJV(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PaymentVouClas1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptVoucherPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.PaymentVouClas1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTrialBl1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetTrialBl", (List<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBl1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSheetAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }

        public static LocalReport GetLocalReport(string v, List<EClassSalPurAcc> rptlist, object p)
        {
            throw new NotImplementedException();
        }

        public static LocalReport GetLocalReport(string v)
        {
            throw new NotImplementedException();
        }

        private static LocalReport SetRptSalaryGrndT(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryGrandT>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSalaryGrndTotalTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryGrandT>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetails4(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsRG(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryRatulPro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsTropical(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsManama02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsManamaBN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsTerranova(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsEdisonReal(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSalaryDetailsJbs(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryEntrust(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsLanco(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }private static LocalReport SetRptSalaryDetailsCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryHOBTIExcel(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryHOBTIPdf(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryHOBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDPBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryCTGBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSalarySicolBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryCPBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryAssureTourism(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryAssSecurity01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryLeisure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsAlliance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsAcmeAI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsSuvastuHeadOffice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsPEBCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBFactoryDriverBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBFactory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBFactoryCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBHeadOfficeBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsPEBHeadOfficeBank2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBHeadOfficeCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBHeadOffice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalaryDetailsPEBHeadOfficeCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRptCashPay02Edison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRptCashPay02Finlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCashPay02FinlayExcel(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptCashPay02GreenWood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalSummaryPEB(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptGrpSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptGrpSalSummary>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalLACA(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalLACAAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBankStmentComWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EClassBankStatment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBankStmentBankWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EClassBankStatment>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpLeaveStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthWiseSalSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTax>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthWiseTax(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTax>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonthWiseTaxFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTaxFinlay>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptSalaryDetailsPEBFactoryCashDriver(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpMonthSumm(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EmpMonthSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAitPurpose(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpAitCertificate(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptBonusSheetGreenWood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSheetTera(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptBonusSheetTro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSheetBTI(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetPebCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetPebProj(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetPebBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetPebFactory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBonusSheetPebFactoryCash(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBonusSheetPebFactoryBank(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpBonusAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet01>)RptDataSet));
            return Rpt1a;
        }


        #region Project Implementation

        private static LocalReport SetRptWorkOrder(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptWorkOrderSuvastu(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptWorkOrderAcme(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>)rptDataSet));
            return rpt1a;
        }
        #endregion

        private static LocalReport SetRptEmpAllInformationBAN(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.EmpAllInformation>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHRCodeBookInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.CodeBookInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAssYearly(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.RptAssYearly>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpperformance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.Empperformance>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptDailyTrans(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.DailyPaymentTrn>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPostDateCqSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt1", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam2", hshParm["projnam2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam3", hshParm["projnam3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam4", hshParm["projnam4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam5", hshParm["projnam5"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projamt1", hshParm["projamt1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt2", hshParm["projamt2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt3", hshParm["projamt3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt4", hshParm["projamt4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt5", hshParm["projamt5"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeSuvastu(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt1", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam2", hshParm["projnam2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam3", hshParm["projnam3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam4", hshParm["projnam4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam5", hshParm["projnam5"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("projamt1", hshParm["projamt1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt2", hshParm["projamt2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt3", hshParm["projamt3"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt4", hshParm["projamt4"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projamt5", hshParm["projamt5"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));



            return Rpt1a;
        }

        private static LocalReport SetRptTrailsBal3(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.TrailsBal3>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBgdWkVsActual(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.BugIncoStatement>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptBankFord(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable reportP = (Hashtable)RptDataSet2;
            //Rpt1a.EnableExternalImages = true;
            //byte[] logo = (byte[])reportP["ComLogo"];
            //Rpt1a.SetParameters(new ReportParameter("ComLogo", Convert.ToBase64String(logo)));

            Rpt1a.SetParameters(new ReportParameter("BankAdd", (string)reportP["BankAdd"]));
            Rpt1a.SetParameters(new ReportParameter("subject", (string)reportP["subject"]));
            Rpt1a.SetParameters(new ReportParameter("Det1", (string)reportP["Det1"]));
            Rpt1a.SetParameters(new ReportParameter("Det2", (string)reportP["Det2"]));
            Rpt1a.SetParameters(new ReportParameter("Det3", (string)reportP["Det3"]));
            Rpt1a.SetParameters(new ReportParameter("Footer", (string)reportP["Footer"]));
            Rpt1a.SetParameters(new ReportParameter("txtuserinfo", (string)reportP["txtuserinfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetFrdLetter", (List<RealEntity.C_81_Hrm.BankFor.BankFord>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptRptVouTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAccIncomeSt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.NoteIncoStatement>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBalanceSheet2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptProjectWisResource(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.ProjectWisRes>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAccIncomeStAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.CompIncome>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_17_Acc.EClassAccounts.CompIncome01>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptRptCustomerDues(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.Customer_Dues_info>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptCurrentDues(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EclassCurrentDues>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport RptConMonthlyAss(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.MonthlyConAss>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetRptUnitWiseCost(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EclassGetUnitWiseCost>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptSupplierDetials(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.SuppDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptConDetials(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.ConDetails>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport RptSupMonthAss(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptSupMonthAss>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurvey03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurvey02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurvey05(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyFinlay02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyFinlay03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyFinlay05(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptSalesYearlyTarget(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EClassYearlyTarget>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptGeneralHead(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.GeneralAdminOverH>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptAdditionalBudget(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_04_Bgd.Additionalbug>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCustomer_Due_inf(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.Customer_Dues_info>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCustInvoice(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.CustInovoce>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport SetrptAddWorkSuvastu(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.CustInovoce>)rptDataSet));
            return rpt1a;
        }


        private static LocalReport SetRptPrjclientStatus(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales_02.EclassPrjClientStatus>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptCustomer_Due_inf01(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSales_03.Customer_Dues_info>)rptDataSet));
            return rpt1a;
        }

        private static LocalReport SetRptNoteIncoStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.NoteIncoStatement>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillAproved01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillAproved(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillAproved01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptIndvPf(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.IndvPf.PaymentScheduleList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptCheque(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeTCL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeGreenwood(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }
        private static LocalReport SetRptChequeOneBankBti(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["ckdate"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["prjdesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["amt"].ToString()));
            return Rpt1a;
        } 
        private static LocalReport SetRptChequeDhakaBankCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        { 
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", " = "+hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));
            return Rpt1a;
        } 
        private static LocalReport SetRptChequeUCBCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        { 
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", " = "+hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeIBBLCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", " = " + hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));
            return Rpt1a;
        }
        
        private static LocalReport SetRptChequeAIBLCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", " = " + hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ProjectDesc", hshParm["ProjectDesc"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("totalAmount", hshParm["totalAmount"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeTBLCPDLL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            //Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo1", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date1", hshParm["date1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("projnam1", hshParm["projnam1"].ToString()));

            Rpt1a.SetParameters(new ReportParameter("naration", hshParm["naration"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("Chequeno", hshParm["Chequeno"].ToString()));

            return Rpt1a;
        }







        private static LocalReport SetRptChequeGreenwoodSHBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeGreenwoodSHIBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptChequeGreenwoodFSIBL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("bankName", hshParm["bankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("payTo", hshParm["payTo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord", hshParm["amtWord"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("date", hshParm["date"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amt", hshParm["amt"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("amtWord1", hshParm["amtWord1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("acpayee", hshParm["acpayee"].ToString()));
            return Rpt1a;
        }




        private static LocalReport SetRptTrnPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccVoucher>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBankCheque(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {
            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.BankCheque>)rptDataSet));
            return rpt1a;
        }
        private static LocalReport RptAccountcode2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DSAccCode", (List<RealEntity.C_17_Acc.EClassAccounts.EClassAccCode>)RptDataSet2));

            Hashtable hshParm = (Hashtable)RptDataSet;
            Rpt1a.SetParameters(new ReportParameter("companyname", hshParm["companyname"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetRptAccSubCode(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.AccSubCode>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptProjectSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_32_Mis.EClassAcc_03.Customer_ProjectSumm>)RptDataSet));
            return Rpt1a;

        }
        private static LocalReport SetUserLoginInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.Userinfo>)RptDataSet));
            return Rpt1a;

        }


        private static LocalReport SetRptRatioAnalysis(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable hasRatio = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("CompanyName", hasRatio["CompanyName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("header", hasRatio["header"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("txtuserinfo", hasRatio["txtuserinfo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("CurrentDate", hasRatio["CurrentDate"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSetRatio", (List<RealEntity.C_17_Acc.EClassDB_BO.AccRatioAnalysis>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptSupProPayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassSupplierProposedPayment>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptReceivedList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.Sales_BO.AccountsReceivable2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptYearlyCollction(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_23_CRR.EClassSalesStatus.EClassYearlyColletionForcasting>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptMaterialsStock(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_12_Inv.EMaterialsStock>)RptDataSet));
            return Rpt1a;

        }

        private static LocalReport SetRptBillRegister(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.OnlineBillReg>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptRegBillStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.RegChequeHistory>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPendingCliMod(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassDB_BO.EClassPendingCliMod>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPurApprovEntryEdison(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetrptMobileAllowanceAssure(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptMobileAllowance(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptMobileAllowAcme(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>)RptDataSet));
            return Rpt1a;


        }


        private static LocalReport SetrptMobileAllowanceTro(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>)RptDataSet));
            return Rpt1a;
        }




        private static LocalReport SetrptMobileAllowAlli(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPurchaseTra(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {


            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptPurTrack01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptOrderTracking(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptOrderTrack01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptPurMktSurveyManama03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptPurMktSurveyManama05(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptSuppCheqHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.SupplierCheqHistory>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPurAprovEntry(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLinkRptSupplierChequeHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.SupplierCheqHistory01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLinkRptSupplierChequeDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.SupplierCheqDetails>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptWorkOrderStatus1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWorkOrderStatus2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptWorkOrderStatus3(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus02>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptBillInfoInns(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInfoManama(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInfoLanco(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInfoJbs(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInfoFinlay(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillInfoCPDL(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillAlliInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillConfirmationBridge(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillConfirmBridgeWithoutLogo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptBillConfirmation03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptBillConfirmation02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetRptPurMktSurveyManama02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.MkrServay02>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.MkrServay03>)RptDataSet2));
            return Rpt1a;
        }


        private static LocalReport SetRptHREmpTransferReport(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_87_Tra.EmployeeTransInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMonWiseEmpLeaveBR(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptAllEmpLeavStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptLeaveDateRange(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptTimeOff(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.applytimeoff>)RptDataSet));
            return Rpt1a;
        }



        private static LocalReport SetRptAllEmpLeavStatusBR(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetrptEmpLoanStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpLoanStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthlyLateAttn03(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpInfoData>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDeptWiseEmp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpInfoData>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAttendanceSummary(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.EmpDailyAttenSummary>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHRAllEmpStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptRetiredEmployee(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpConfirmation(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonAttnSumEmpWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptMonAttnSumEmpWise>)RptDataSet));
            return Rpt1a;
        }
        // NAhid


        private static LocalReport SetRptEmpListJDateWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpListBirthDateWise(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInactiveEmplists(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRpTotalEmplists(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTransList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpTransList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpSpList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDateWiseEmpHold(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptJoiningStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptIncrementStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_93_AnnInc.AnnIncReport.AnnualIncrementStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptGbFinalApproval(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_99_AllInterface.GbFinalApproval>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpSattelment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptEmpSattelmentBangla(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetRptEmployeeLeaveRecord(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveRecord>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptYearlySales(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.EClassYearlySalesCRM>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProspectWorking(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectWorking>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptProspectTransfer(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectTransfer>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptClientLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            // Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectTransfer>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetRptMktRequisition(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktPurchaseRequisition>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDailyWorkStatus(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_21_Mkt.ECRMClientInfo.EClassDailyWorkStatus>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMktPurMarketSurvey(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>)RptDataSet2));
            return Rpt1a;
        }
        private static LocalReport SetRptMktPurMRR(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktPurchaseMrr>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMktPurchaseTracking(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktPurchaseTrack>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMktMatIssue(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktMatIssue>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPurOrderTopSheet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.PurOrderTopSheet>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSupAdvanceDetails(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.RptSupAdvanceDetails>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptOtherCollHistory(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPur.OtherCollHistory>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDateWiseBill(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_14_Pro.EClassPayment.EclassRptDateWiseBill>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmployeeAllInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmployeeAllInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptMonthlyProbCollection(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptMonthlyProbCollection>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptDuesAllReports(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_17_Acc.EClassAccounts.RptDuesReportAll>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpOfferLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpOfferLetter>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpIdCard(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeIDCardInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptEmpApointmentLetter(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpOfferLetter>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptTaskInfoDet(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmptaskDesk>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptAllDuesInfo(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_99_AllInterface.AllDues>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInterfaceLeave(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EInterfaceLeave>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInterfaceAttApp(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EInterfaceAttApp>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptHrmPaySlipEnvelop(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptPromotionEnvelop(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_97_MIS.Mgt_ManPower.HrmEnvelopPrint>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptReqAdjustment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_34_Mgt.EClassEnventory.RequisationAdjust>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSoldUnsoldUnitAvgPrice(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SoldUnsoltInfavg>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptSalesVsAchivement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>)RptDataSet));

            return Rpt1a;
        }
        private static LocalReport SetRptSalesVsAchivementLO(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>)RptDataSet));

            return Rpt1a;
        }private static LocalReport SetRptPaymentSystem(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.PaymentStatusReconcile>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_22_Sal.EClassSales.PaymentStatusRevenue>)RptDataSet2));

            return Rpt1a;
        }
        private static LocalReport RptSalesCollectionStatement(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.CollectionStatement>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport RptSalesVsAchivementDPC(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>)RptDataSet));

            return Rpt1a;
        }

        private static LocalReport SetRptGroupAtt(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.ERptGroupAtt>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet2", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.Elvlateabbs02>)RptDataSet2));
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet3", (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.AttgraphLbl>)UserDataset));
            return Rpt1a;
        }


        private static LocalReport SetRptQuotationPrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealERPEntity.C_70_Services.EClass_Quotation.EQuotationinfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetRptInvoicePrint(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<RealERPEntity.C_70_Services.EClass_Quotation.EQuotationinfo>)RptDataSet));
            return Rpt1a;
        }
    }
}
