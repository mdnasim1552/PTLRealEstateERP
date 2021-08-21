<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GenPageLand.aspx.cs" Inherits="RealERPWEB.GenPageLand" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        ul.colMid {
            list-style: none;
        }

            ul.colMid li {
                margin: 3px 0;
            }

                ul.colMid li a {
                }
                .adminpermission h3:first-line{
                    display:none;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="container lbl2SubMenu headTagh3 moduleItemWrp cstepopertion">
                <div class="contentPart">
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <h3>C. 	General Reports</h3>
                            <asp:Panel ID="pnlBusinessPlan" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>01. Business Planning</h5>
                                    </li>
                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=SalesTarVsAch">01. Sales-Capacity, BEP, Target & Achievement</a></li>
                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=CollTarVsAch">02. Collection-Capacity, BEP, Target & Achievement</a></li>
                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=ConPlan">03. Construction-Capacity, BEP, Target & Achievement</a></li>
                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=LPPlan">04. Land-Capacity, BEP, Target & Achievement</a></li>
                                    <li><a href="F_05_Busi/YearlyPlanningSt.aspx?Type=Income">05. Financial Performance Budget (ABP)
                                    </a></li>
                                    <li><a href="F_05_Busi/YearlyPlanningSt.aspx?Type=CBudget">06. Cash Budget (ABP)</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlLandProcure" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>02. Land Procurement</h5>
                                    </li>
                                    <li><a href="F_01_LPA/PriLandProposal.aspx">01. Initial Land Proposal</a></li>
                                    <li><a href="F_01_LPA/RptLandDevProposal.aspx?Type=Cost">02. Cost Details</a></li>
                                    <li><a href="F_01_LPA/RptLandDevProposal.aspx?Type=Revenue">03. Revenue Details</a></li>
                                    <li><a href="F_01_LPA/RptLandDevTopSheet.aspx">04. Summary Sheet of Land Proposal</a></li>
                                    <li><a href="F_01_LPA/RptAllProTopSheet.aspx">05. Top Sheet of Land Proposal</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlFeasibility" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>03. Feasibility</h5>
                                    </li>
                                    <li><a href="F_02_Fea/RptProjectFeasibility03.aspx?Type=Cost">01. Cost Details</a></li>
                                    <li><a href="F_02_Fea/RptProjectFeasibility03.aspx?Type=Revenue">02. Revenue Details</a></li>
                                    <li><a href="F_02_Fea/RptProFeasibilityAll.aspx?Type=FeInSumm">03. Income Statement</a></li>
                                    <li><a href="F_02_Fea/RptProductPricing.aspx">04. Product Pricing</a></li>
                                    <li><a href="F_02_Fea/RptPrjFeasibility04.aspx?Type=PriceList02">05. Price List</a></li>
                                    <li><a href="F_02_Fea/RptPrjFeasibility04.aspx?Type=SoldUSold">06. Sales Statement</a></li>
                                    <li><a href="F_02_Fea/RptPrjFeasibility04.aspx?Type=GPNPALLPRO">07. Feasibility Top Sheet</a></li>
                                    <li><a href="F_02_Fea/RptProFeasibilityAll.aspx?Type=FeTopSheet">08. BEP Top Sheet</a></li>
                                    <li><a href="F_02_Fea/RptRevsiFeasibility.aspx?Type=RevFeaCL">09. Reivised Feasibility</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlbgd" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>04. Budgetary Control</h5>
                                    </li>
                                    <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=LandPurReg">01. Land Purchase Register</a></li>
                                    <li><a href="F_01_LPA/RptLandProcurement.aspx?Type=LandSt">02. Land Procurement Status</a></li>
                                    <li><a href="F_01_LPA/RptLandProcurement.aspx?Type=LandStSum">03. Land Procurement Status-Summary</a></li>
                                    <li><a href="F_17_Acc/RptAccPaySlip.aspx">04. Pay Slip</a></li>
                                  

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlfinance" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>05.Finance</h5>
                                    </li>
                                    <li><a href="F_03_Fin/EntryYearlySalAndColl.aspx">01. Expected Collection Next 12 Month</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlProPlan" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>06. Project Planning</h5>
                                    </li>
                                    <li><a href="F_08_PPlan/PrjCompFlowchart.aspx">01. Project Completion Flow Chart</a></li>
                                    <li><a href="F_08_PPlan/ProTarget.aspx">02. Construction Planing (Show Only)</a></li>
                                    <li><a href="F_08_PPlan/RptProTarget.aspx">03. Construction Planing</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlProImp" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>07. Project Implementation</h5>
                                    </li>
                                    <li><a href="F_32_Mis/RptMisProIncomeExe.aspx?Type=MasPVsMonPVsExAllPro">01. Master Plan, Monthly Plan & Execution- All Project</a></li>
                                    <li><a href="F_32_Mis/RptPrjCostPerSFT.aspx?Type=ProTarVsAchievement">02. Construction Target Vs. Achievement</a></li>
                                    <li><a href="F_32_Mis/RptConstruProgress.aspx">03. Floor Wise Construction Progress</a></li>

                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=ConTarVsAch">04. Construction Status</a></li>
                                    <li><a href="F_09_PImp/RptResBgdBal.aspx">06. Budget Balance (Resource)</a></li>
                                    <li><a href="F_09_PImp/RptSubConBill.aspx?Type=SubBill">07. R/A Bill- Details</a></li>


                                    <li><a href="F_09_PImp/RptSubConBillStatus.aspx">08. R/A Bill Summary</a></li>
                                    <li><a href="F_09_PImp/RptSubContractorSd.aspx?Type=BillDetails">09. R/A Bill - Finalization</a></li>

                                </ul>

                            </asp:Panel>

                          <%--  <asp:Panel ID="pnlInven" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>08. Inventory</h5>
                                    </li>
                                    <li><a href="F_12_Inv/RptPrurVarAna.aspx?Type=IssueBasis">01. Material Evaluation - Based on Issue</a></li>
                                    <li><a href="F_32_Mis/RptPrjCostPerSFT.aspx?Type=ProTarVsAchievement">02. Material Evaluation - Based on Progress</a></li>
                                    <li><a href="F_12_Inv/RptInvResourceConsum.aspx">03. Ind. Material Consumtion</a></li>


                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlCentral" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>09. Central Warehouse</h5>
                                    </li>
                                    <li><a href="F_13_Cen/CentralStore.aspx?Type=Stockrptqbasis">01. Materials Stock - Quantity Basis</a></li>
                                    <li><a href="F_13_Cen/CentralStore.aspx?Type=Stockrptamtbasis">02. Materials Stock - Amount Basis</a></li>


                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlProCure" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>10. Procurement</h5>
                                    </li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum">01. Purchase Summary</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur">02. Day Wise Purchase</a></li>
                                    <li><a href="F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment">03. Supplier Overall Position</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk">04. Purchase Tracking</a></li>
                                    <li><a href="F_14_Pro/RptDateWiseReq.aspx?Type=PendingStatus">05. Pending Status</a></li>
                                    <li><a href="F_14_Pro/RptMatPurHistory.aspx">06. Purchase History-Materials Wise</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus02.aspx?Type=Purchase">07. Purchase Summary with Opening</a></li>
                                    
                                    <li><a href="F_14_Pro/RptSupCreditLimit.aspx?Type=RptSupCredit">08. Supplier Overall Position-2</a></li>
                                    <li><a href="F_14_Pro/RptDeliveryEfficiency.aspx">09. Materials Delivery Efficiency Report</a></li>

                                </ul>

                            </asp:Panel>--%>

                            <asp:Panel ID="pnlAcc" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>11. General Accounts</h5>
                                    </li>
                                    <li><a href="F_17_Acc/TransectionPrint.aspx?Type=AccVoucher&Mod=Accounts">01. Voucher Print</a></li>
                                    <li><a href="F_17_Acc/TransectionPrint.aspx?Type=AccCheque&Mod=Accounts">02. Cheque Print</a></li>
                                    <li><a href="F_17_Acc/RptAccDayTransData.aspx">03. Daily transaction</a></li>
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran">04. Cash & Bank Transaction</a></li>

                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=ProTrans">05. Daily Transaction -Project</a></li>
                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=BankPosition">06. Bank Position</a></li>
                                    <li><a href="F_17_Acc/RptAccDTransBankSt.aspx">07. Bank Reconcilaition Statement</a></li>
                                    <li><a href="F_17_Acc/AccLedger.aspx?Type=Ledger&RType=GLedger">08. Ledger</a></li>
                                    <li><a href="F_17_Acc/RptAccSpLedger.aspx?Type=DetailLedger">09. Special Ledger</a></li>


                                    <li><a href="F_17_Acc/AccLedger.aspx?Type=SubLedger">10. Subsidiary Ledger</a></li>
                                    <li><a href="F_17_Acc/AccControlSchedule.aspx?Type=Type02">11. Accounts Control Schedule</a></li>
                                    <li><a href="F_17_Acc/AccDetailsSchedule.aspx">12. Accounts Details Schedule</a></li>
                                    <li><a href="F_17_Acc/RptAccBudget.aspx?Type=WbgdVsAc">13. Working Budget Vs. Achievement</a></li>
                                    <li><a href="F_17_Acc/RptAccBudget.aspx?Type=WbgdVsAcDetials">14. Working Budget Vs. Achievement Details</a></li>
                                    <li><a href="F_17_Acc/RptAccPaySlip.aspx">15. Pay Slip</a></li>
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=IssuedVsCollect">16. Receipts & Payment(Actual)</a></li>
                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlMgtAcc" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>12. Project</h5>
                                    </li>
                                    <li><a href="F_32_Mis/RptProjectStatus.aspx?Type=PrjStatus">01. Project Status Report</a></li>
                                    <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk">02. Budgeted Income Statement</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=BE">03. Budget Vs Expenses</a></li>
                                    <li><a href="F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal">04. Project Trial Balance</a></li>
                                    <li><a href="F_12_Inv/RptPrurVarAna.aspx?Type=IssueBasis">05. Material Evaluation - Based on Issue</a></li>
                                    <li><a href="F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis">06. Material Evaluation - Based on Progress</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur">07. Day Wise Purchase</a></li>
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=ProTrans">08. Daily Transaction -Project</a></li>
                                    <li><a href="F_32_Mis/RptConstruProgress.aspx">09. Floor Wise Construction Progress</a></li>
                                    <li><a href="F_32_Mis/IncomeSt.aspx?Type=CB">10. Income Statement (Cash Basis)</a></li>
                                    <li><a href="F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost">11. Additional Budget for Influation</a></li>

                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=PS">12. Project Report</a></li>
                                    
                                    
                                     
                                    <li><a href="F_32_Mis/RptMisMasterBgd.aspx?Type=ComProCost">13. Any Cost-All Project (5 Years)</a></li>
                                    
                                    <li><a href="F_32_Mis/RptMisMasterBgd.aspx?Type=ColVsExpenses">14. Investment Plan - All Project</a></li>

                                     
                                    <li><a href="F_12_Inv/RptInvResourceConsum.aspx">15. Ind. Material Consumtion</a></li>
                                    

                                    
                                    
                                    <li><a href="F_32_Mis/ProjectSummary.aspx">16. Project Summary - At a glance</a></li>
                                     <li><a href="F_32_Mis/RptAccIncome.aspx?Type=IncomeMonthly">17. Income Statement(12 Month)</a></li>
                                    <li><a href="F_32_Mis/RptSalesDuPeriod.aspx">18. Income Statement - Investment Basis</a></li>
                                    <li><a href="F_32_Mis/RptInComeStExe.aspx">19. Income Statement (Execution Basis)</a></li>
                                    
                                    <li><a href="F_32_Mis/IncomeSt.aspx?Type=AB">20. Income Statement (Actural Basis)</a></li>

                                    
                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlAudit" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>13. Audit</h5>
                                    </li>
                                    <li><a href="F_17_Acc/RptAccDayTransData.aspx">01. Daily transaction</a></li>
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay">02. Receipts & Payment(Honoured)</a></li>
                                    <li><a href="F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=RptDayWSale">03. Day Wise Sales</a></li>
                                    <li><a href="F_22_Sal/RptTransactionSt.aspx?Type=TransDateWise">04. Day Wise Collection</a></li>

                                    <li><a href="F_22_Sal/RptSalInterest.aspx?Type=registration">05. Registration Clearence</a></li>



                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlMkt" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>14. Marketing</h5>
                                    </li>
                                    <li><a href="F_21_Mkt/RptProWiseClOffered.aspx?Type=SalesDeamnd">01. Sales Demand Analysis</a></li>
                                    <li><a href="F_21_Mkt/RptProWiseClOffered.aspx?Type=SalesDeci">02. Sales Decision</a></li>
                                    <li><a href="F_21_Mkt/RptProWiseClOffered.aspx?Type=Capacity">03. Day Wise Sales</a></li>
                                    <li><a href="F_21_Mkt/RptMktAppointment.aspx?Type=DiscussHis&UType=Mgt">04. Client History</a></li>

                                    <li><a href="F_21_Mkt/RptMktAppointment.aspx?Type=OffPerformance&UType=Mgt">05. Sales Person History</a></li>



                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlSales" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>15. Sales</h5>
                                    </li>
                                    <li><a href="F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect">01. Revenue Status</a></li>
                                    <li><a href="F_22_Sal/RptSalSummery.aspx?Type=dSaleVsColl">02. Sales Target Vs Achievement </a></li>
                                    <li><a href="F_23_CR/RptCustomerDues.aspx">03. Customer Dues</a></li>
                                    <li><a href="F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold">04. Sold & Unslod Informations</a></li>
                                    <li><a href="F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=RptDayWSale">05. Day Wise Sales</a></li>
                                    <li><a href="F_22_Sal/RptSalesInventory.aspx">06. Sales Inventory(Summary)</a></li>
                                    <li><a href="F_22_Sal/RptRateChart.aspx">07. Sales Inventory(Details)</a></li>
                                    <li><a href="F_22_Sal/RptAvailChart.aspx?Type=Details">08. Availability Chart 1</a></li>
                                    
                                    


                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlCR" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>16. Credit Realization(CR)</h5>
                                    </li>
                                    <li><a href="F_22_Sal/RptMktMoneyReceipt.aspx?Type=CustCare">01. Money Receipt</a></li>
                                    <li><a href="F_23_CR/RptCustInvoice.aspx?Type=Invoice01">02. Customer Invoice - 01</a></li>
                                    <li><a href="F_22_Sal/RptTransactionSt.aspx?Type=TransDateWise">03. Day Wise Collection</a></li>
                                    <li><a href="F_22_Sal/RptTransactionSt.aspx?Type=TransSummary">04. Day Wise Collection Summary</a></li>
                                    <li><a href="F_22_Sal/RptSalInterest.aspx?Type=interest">05. Delay Charge</a></li>
                                    <li><a href="F_23_CR/RptCustPayStatus.aspx?Type=ClLedger">06. Client Ledger</a></li>
                                    <li><a href="F_23_CR/RptReceivedList03.aspx?Type=AllProDuesCollect">07. Revenue Status</a></li>
                                    <li><a href="F_23_CR/RptReceivedList02.aspx?Type=AllProDuesCollect">08. Dues Collection -Summary </a></li>
                                    <li><a href="F_23_CR/RptReceivedList02.aspx?Type=AllProDuesCollect">09. Dues Collection Statment </a></li>

                                </ul>

                            </asp:Panel>


                            <%--<asp:Panel ID="pnlCC" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>17. Customer Care</h5>
                                    </li>
                                    <li><a href="F_24_CC/RptClientModification.aspx?WType=CliModfi">01. Client Modification Report</a></li>


                                </ul>

                            </asp:Panel>--%>
                            <asp:Panel ID="pnlReg" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>18. Registration</h5>
                                    </li>
                                    <li><a href="F_22_Sal/RptSalInterest.aspx?Type=registration">01. Registration Clearance</a></li>
                                    <li><a href="F_24_CC/RptLoanStatus.aspx?Type=Registration">02. Registration Status</a></li>
                                    <li><a href="F_25_Reg/RptRegclearacne.aspx?Type=Regiscl">03. Registration Status- All Project</a></li>


                                </ul>

                            </asp:Panel>
                            <%--<asp:Panel ID="pnlFxtAst" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>19. Fixed Assets</h5>
                                    </li>
                                    <li><a href="F_29_Fxt/RptFxtStore.aspx?Type=Stockrptqbasis">01. Materials Stock- Quantity Basis</a></li>
                                    <li><a href="F_29_Fxt/RptFxtStore.aspx?Type=Stockrptamtbasis">02. Materials Stock- Amount Basis</a></li>


                                </ul>

                            </asp:Panel>--%>

                           <%-- <asp:Panel ID="pnlDailyAct" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>20. Daily Activities Evaluation</h5>
                                    </li>
                                    <li><a href="#">01. Daily Activities</a> </li>

                                </ul>

                            </asp:Panel>--%>

                            <asp:Panel ID="pnlMis" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>21. MIS</h5>
                                    </li>
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay">01. Receipts & Payment(Honoured)</a></li>
                                    
                                    
                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=Mains">02. Trial Balance</a></li>
                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=HOTB">03. Head Office Trial Balance</a></li>

                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=Details">04. Details Of Balance Sheet</a></li>
                                    <li><a href="F_17_Acc/RptAccSales.aspx">05. Sales, Received, Receivable & Cost</a></li>
                                    <li><a href="F_34_Mgt/PurReqAdjst.aspx?Type=ProMargin">06. System Generated Sales (Upto Date)</a></li>
                                    <li><a href="F_32_Mis/RptSalesDuPeriod.aspx">07. System Generated Sales (Period)</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlDoc" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>Financial Statement(IAS & BAS)</h5>
                                    </li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=BS">01. Statement Of Financial Position</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=IS">02. Statement Of Comprehensive Income</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=SHEQUITY">03. Statement Of Share Holder's Equity</a></li>
                                    <li><a href="F_17_Acc/RptBankCheque.aspx?Type=CashFlow">04. Statement Of Cash Flow (Direct)</a></li>
                                    <li><a href="F_17_Acc/RptBankCheque.aspx?Type=CashFlow02">05. Statement Of Cash Flow (Indirect)</a></li>
                                    <li><a href="F_17_Acc/RptBankCheque.aspx?Type=FinNote">06. Accounting Policies</a></li>
                                    <li><a href="F_19_Audit/EntrySEC.aspx">08. Compliance of SEC</a></li>
                                    <li><a href="F_34_Mgt/EntryFinResult.aspx">09. Financial Results (5 Years)</a></li>
                                   

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlHR" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>23. HR Management</h5>
                                    </li>
                                    <li><a href="F_22_Sal/RptTarVsAchievement.aspx?Type=HRInfo">01. HR Information</a></li>

                                </ul>

                            </asp:Panel>






                        </div>
                        <div class="col-md-4"></div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
