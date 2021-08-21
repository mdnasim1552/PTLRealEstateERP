<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GenPage.aspx.cs" Inherits="RealERPWEB.GenPage" %>


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

        .adminpermission h3:first-line {
            display: none;
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
                                    <li><a href="F_05_Busi/YearlyPlanningSt.aspx?Type=Income">01. Financial Performance Budget (ABP)
                                    </a></li>
                                    <li><a href="F_05_Busi/YearlyPlanningSt.aspx?Type=CBudget">02. Cash Budget (ABP)</a></li>

                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlLandProcure" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>02. Land Data Bank</h5>
                                    </li>
                                    <li><a href="F_01_LPA/PriLandProposal.aspx">01. Land Proposal Report</a></li>                                  
                                    <li><a href="F_01_LPA/RptLandDevProposal.aspx?Type=PrjInfo">02. Project Information</a></li>
                                     <li><a href="F_02_Fea/RptProjectFeasibility.aspx">03. Land Feasibility</a></li>                                  
                                    <li><a href="F_01_LPA/RptAllProTopSheet.aspx">04. Land Data Bank</a></li>

                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlpreconst" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>03. Pre-Construction Plannig</h5>
                                    </li>
                                    <li><a href="#">01. Under construction</a></li> 
                                   

                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlbgd" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>04. Budgetary Control</h5>
                                    </li>
                                    <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&prjcode=">01. Budgeted Income Statement -(Work)</a></li>
                                    <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgd&prjcode=">02. Budgeted Income Statement -(Resource)</a></li>
                                    <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=WrkVsResource&prjcode=">03. Budgeted Work Vs. Resource</a></li>
                                   <%-- <li><a href="F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=4">04. Cost-Analysis Sheet</a></li>--%>
                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlProPlan" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>05. Project Planning</h5>
                                    </li>
                                   
                                    <li><a href="F_08_PPlan/ProTarget.aspx">01. Construction Planing (Show Only)</a></li>
                                    <li><a href="F_08_PPlan/RptProTarget.aspx">02. Construction Planing</a></li>
                                     <li><a href="#">03. Under construction</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlProImp" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>06. Project Implementation</h5>
                                    </li>
                                    <li><a href="F_32_Mis/RptMisProIncomeExe.aspx?Type=MasPVsMonPVsExAllPro">01. Plan VS Execution-all</a></li>
                                    <li><a href="F_32_Mis/RptConstruProgressSum.aspx">02. Progress Report</a></li>
                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlInven" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>07. Inventory</h5>
                                    </li>

                                    <li><a href="F_12_Inv/RptProjectStock.aspx?Type=inv">01. Materials Stock Information(Inventory)</a></li>
                                     <li><a href="F_12_Inv/RptMatStock.aspx">02. Materials Stock Information(Individual)</a></li>
                                     <li><a href="F_12_Inv/RptMaterialStock.aspx?Type=inv">03. Materials Stock Information(Project Wise)</a></li>


                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlCentral" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>08. Central Warehouse</h5>
                                    </li>
                                   <li><a href="#">01. Under construction</a></li>


                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlProCure" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>09. Procurement</h5>
                                    </li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur">01. Day Wise Purchase</a></li>
                                    <li><a href="F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment">02. Supplier Overall Position</a></li>



                                    <%-- <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum">01. Purchase Summary</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur">02. Day Wise Purchase</a></li>
                                    <li><a href="F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment">03. Supplier Overall Position</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk">04. Purchase Tracking</a></li>
                                    <li><a href="F_14_Pro/RptDateWiseReq.aspx?Type=PendingStatus">05. Pending Status</a></li>
                                    <li><a href="F_14_Pro/RptMatPurHistory.aspx">06. Purchase History-Materials Wise</a></li>
                                    <li><a href="F_14_Pro/RptPurchaseStatus02.aspx?Type=Purchase">07. Purchase Summary with Opening</a></li>
                                    
                                    <li><a href="F_14_Pro/RptSupCreditLimit.aspx?Type=RptSupCredit">08. Supplier Overall Position-2</a></li>
                                    <li><a href="F_14_Pro/RptDeliveryEfficiency.aspx">09. Materials Delivery Efficiency Report</a></li>--%>
                                </ul>

                            </asp:Panel>

                                    <asp:Panel ID="pnlAcc" runat="server" Visible="False">
                                        <ul class="colMid">
                                            <li>
                                                <h5>10. General Accounts</h5>
                                            </li>
                                           
                                            <li><a href="F_17_Acc/RptAccDayTransData.aspx">01. Daily transaction</a></li>
                                            <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran">02. Cash & Bank Transaction</a></li>

                                            <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=ProTrans">03. Daily Transaction -Project</a></li>
                                            <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=BankPosition">04. Bank Position</a></li>
                                            <li><a href="F_17_Acc/RptAccDTransBankSt.aspx">05. Bank Reconcilaition Statement</a></li>
                                            <li><a href="F_17_Acc/AccLedger.aspx?Type=Ledger&sircode&RType=GLedger">06. Ledger</a></li>
                                            <li><a href="F_17_Acc/RptAccSpLedger.aspx?Type=DetailLedger">07. Special Ledger</a></li>


                                            <li><a href="F_17_Acc/AccLedger.aspx?Type=SubLedger&sircode">08. Subsidiary Ledger</a></li>
                                            <li><a href="F_17_Acc/AccControlSchedule.aspx?Type=Type02">09. Accounts Control Schedule</a></li>
                                            <li><a href="F_17_Acc/AccDetailsSchedule.aspx">10. Accounts Details Schedule</a></li>
                                            <li><a href="F_17_Acc/RptAccBudget.aspx?Type=WbgdVsAc">11. Working Budget Vs. Achievement</a></li>
                                            <li><a href="F_17_Acc/RptAccBudget.aspx?Type=WbgdVsAcDetials">12. Working Budget Vs. Achievement Details</a></li>
                                           <%-- <li><a href="F_17_Acc/RptAccPaySlip.aspx">15. Pay Slip</a></li>--%>
                                            <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay">13. Receipts & Payment</a></li>
                                        </ul>

                                    </asp:Panel>
                                    <%--<asp:Panel ID="pnlMgtAcc" runat="server" Visible="False">
                                        <ul class="colMid">
                                            <li>
                                                <h5>11. Project</h5>
                                            </li>
                                            <li><a href="F_32_Mis/RptProjectStatus.aspx?Type=PrjStatus">01. Project Status Report</a></li>
                                            <li><a href="F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk">02. Budgeted Income Statement</a></li>
                                            <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=BE">03. Budget Vs Expenses</a></li>
                                            <li><a href="F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal">04. Project Trial Balance</a></li>
                                            <li><a href="F_12_Inv/RptPrurVarAna.aspx?Type=IssueBasis">05. Material Evaluation - Based on Issue</a></li>
                                            <li><a href="F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis">06. Material Evaluation - Based on Progress</a></li>
                                            <li><a href="F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur">07. Day Wise Purchase</a></li>
                                            <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=ProTrans">08. Daily Transaction -Project</a></li>
                                           
                                            <li><a href="F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost">11. Additional Budget for Influation</a></li>



                                            <li><a href="F_32_Mis/RptMisMasterBgd.aspx?Type=ComProCost">13. Any Cost-All Project (5 Years)</a></li>

                                            <li><a href="F_32_Mis/RptMisMasterBgd.aspx?Type=ColVsExpenses">14. Investment Plan - All Project</a></li>


                                         
                                        </ul>

                                    </asp:Panel>--%>




                            <asp:Panel ID="pnlMkt" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>12. Marketing</h5>
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
                                        <h5>13. Sales</h5>
                                    </li>
                                    <li><a href="F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect">01. Account Receivable (AR-1)</a></li>
                                    <li><a href="F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=RptDayWSale">02. Day Wise Sales</a></li>


                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlCR" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>14. Credit Realization(CR)</h5>
                                    </li>
                                  
                                
                                    <li><a href="F_23_CR/RptReceivedList02.aspx?Type=AllProDuesCollect&prjcode=">01.Dues Collection -Summary</a></li>
                                    <li><a href="F_23_CR/RptReceivedList02.aspx?Type=DuesCollect&prjcode=">02.Dues Collection Statment</a></li>
                                    <li><a href="F_23_CR/RptReceivedList02.aspx?Type=AllProDuesCollect&prjcode=">03. Customer Dues Information</a></li>

                                </ul>

                            </asp:Panel>


                            <asp:Panel ID="pnlCC" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>15. Customer Care</h5>
                                    </li>
                                    <li><a href="F_24_CC/RptClientModification.aspx?WType=CliModfi">01. Client Modification Report</a></li>
                                    <li><a href="F_22_Sal/RptSalInterest.aspx?Type=registration">02. Registration Clearence</a></li>
                                    <li><a href="F_24_CC/RptLoanStatus.aspx?Type=Registration">03. Registration Status</a></li>
                                    <li><a href="F_25_Reg/RptRegclearacne.aspx?Type=Regiscl">04. Registration Status- All Project</a></li>
                                </ul>

                            </asp:Panel>
                            <asp:Panel ID="pnlFxtAst" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>16. Fixed Assets</h5>
                                    </li>
                                    <li><a href="F_29_Fxt/RptFixAsset02.aspx">01. Fixed Asset Report</a></li>
                                    <li><a href="F_29_Fxt/RptFxtAsstStatus.aspx?Type=Fix">02. Fixed Assets Status</a></li>
                                    <li><a href="F_29_Fxt/RptFxtAsstStatus.aspx?Type=DepCost">03. Depreciation Cost</a></li>
                                </ul>

                            </asp:Panel>

                            <%--<asp:Panel ID="pnlDailyAct" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>17. Daily Activities Evaluation</h5>
                                    </li>
                                    <li><a href="#">01. Daily Activities</a> </li>

                                </ul>

                            </asp:Panel>--%>

                            <asp:Panel ID="pnlMis" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>17. MIS</h5>
                                    </li>
                                    
                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay">01. Receipts & Payment(Honoured)</a></li>
                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=Mains">02. Trial Balance</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=BS">03. Statement Of Financial Position</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=IS">04. Statement Of Comprehensive Income</a></li>
                                    <li><a href="F_17_Acc/RptBankCheque.aspx?Type=CashFlow">05. Statement Of Cash Flow (Direct)</a></li>
                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlDoc" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>18. Financial Statement</h5>
                                    </li>

                                    <li><a href="F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay">01. Receipts & Payment(Honoured)</a></li>
                                    <li><a href="F_17_Acc/AccTrialBalance.aspx?Type=Mains">02. Trial Balance</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=BS">03. Statement Of Financial Position</a></li>
                                    <li><a href="F_17_Acc/AccFinalReports.aspx?RepType=IS">04. Statement Of Comprehensive Income</a></li>
                                    <li><a href="F_17_Acc/RptBankCheque.aspx?Type=CashFlow">05. Statement Of Cash Flow (Direct)</a></li>

                                </ul>

                            </asp:Panel>

                            <asp:Panel ID="pnlHR" runat="server" Visible="False">
                                <ul class="colMid">
                                    <li>
                                        <h5>19. HR Management</h5>
                                    </li>
                                    <li><a href="F_81_Hrm/F_92_Mgt/AllEmpList.aspx">01. Members</a></li>
                                     <li><a href="F_81_Hrm/F_97_MIS/RptMgtInterface.aspx">02. Summary</a></li>
                                     <li><a href="F_81_Hrm/F_99_MgtAct/RptgroupAttendance.aspx">03. Attendance</a></li>
                                     <li><a href="F_81_Hrm/F_83_Att/RptWeekPresence.aspx">04. Attendance(W)</a></li>

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

