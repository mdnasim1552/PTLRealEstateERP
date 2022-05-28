<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="DashboardOperation.aspx.cs" Inherits="ASITSTDWEB.DashboardOperation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card {
            min-height: 70px;
            margin-right: 5px;
            margin-bottom: 0;
            text-decoration: none !important;
            color: darkblue;
            background: white;
        }

            .card:hover {
                background-color: #f5f5f5;
                transform: scale(1.04);
            }

        .card-first {
            background-color: #ADC3C0;
        }

            .card-first:hover {
                background-color: #576069;
            }

        .card-body {
            margin: 0;
            padding: 0;
            text-align: center;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        h3 {
            font-family: Cambria;
            text-align: center;
        }

        .list-group {
            height: 430px;
            overflow-y: auto;
        }

        .list-group-item:hover {
            box-shadow: inset 4px 0 0 0 #346cb0;
        }

        .left-most {
            margin-right: 20px;
            margin-bottom: 20px;
            color: white;
        }

            .left-most:hover {
                color: white;
            }
            .left-most-height{
                height: 430px;
            }
    </style>

    <div class="mt-3">
        <div class="row">
            <div class="col-md-2">
                <div class="row m-3">
                    <div class="col-md-12">
                        <h3>Dashboard</h3>
                         

                    </div>

                </div>
                <div class="d-flex flex-column left-most-height">
                    <div class="row">
                        <div class="col-md-12">
                            <a href="F_32_Mis/SalesInformation" class="card left-most bg-danger">
                                <div class="card-body">
                                    <i class="fa fa-cart-plus mr-3"></i>
                                    Sales
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="F_32_Mis/PurInformation" class="card left-most bg-warning">
                                <div class="card-body">
                                    <i class="fa fa-cart-plus mr-3"></i>
                                    Purchase
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="F_32_Mis/AccDashBoard" class="card left-most bg-primary">
                                <div class="card-body">
                                    <i class="fa fa-cart-plus mr-3"></i>
                                    Accounts
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="F_32_Mis/RptComDuesAll?Type=Deb" class="card left-most bg-info">
                                <div class="card-body">
                                    <i class="fa fa-cart-plus mr-3"></i>
                                    Accounts-R
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="F_32_Mis/RptComDuesAll?Type=Cr" class="card left-most bg-success">
                                <div class="card-body">
                                    <i class="fa fa-cart-plus mr-3"></i>
                                    Accounts-P
                                </div>
                            </a>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-md-7">
                <div class="row m-3">
                    <div class="col-md-12">
                        <h3><a href="SettingAll">Operation</a></h3>
                    </div>

                </div>
                <div class="row no-gutters">
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_03_Bgd/BgdPrjAna?InputType=BgdMain")%>" class="card card-first text-dark">
                            <div class="card-body">
                                Construction
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/BgdMaster?InputType=BgdMain")%>" class="card">
                            <div class="card-body" >
                                General
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_16_Bill/ExecutionTrans")%>" class="card">
                            <div class="card-body">
                                Execution
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_16_Bill/SubConBill?Type=All")%>" class="card">
                            <div class="card-body">
                                Sub-Con Bill
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccConBillTrans?Type=All")%>" class="card">
                            <div class="card-body">
                                Bill Finalization
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccConBillUpdate?Type=Entry")%>" class="card">
                            <div class="card-body">
                                Con Bill Update
                            </div>
                        </a>
                    </div>
                </div>
                <hr />
                <div class="row no-gutters mb-2">
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTopPurchaseOrder?Type=All")%>" class="card card-first text-dark">
                            <div class="card-body">
                                Purchase Order
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReceivedTrans?Type=All")%>" class="card">
                            <div class="card-body">
                                Received
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_12_Inv/PurBillTrans?Type=All")%>" class="card">
                            <div class="card-body">
                                Bill Confirmation
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccCreditor?Type=All")%>" class="card">
                            <div class="card-body">
                                Payable
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccPaymentTrans?Type=All")%>" class="card">
                            <div class="card-body">
                                Pay Bill
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalPur?Type=Pur")%>" class="card">
                            <div class="card-body">
                                Cash Purchase
                            </div>
                        </a>
                    </div>
                </div>
                <div class="row no-gutters mb-2">
                    <div class="col-md-2">
                        <a href="#" class="card card-first text-dark">
                            <div class="card-body">
                                Inventory
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccProjectTrnsMat?Type=All")%>" class="card">
                            <div class="card-body">
                                Transfer
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_15_Pur/PurMatIssue?Type=Entry")%>" class="card">
                            <div class="card-body">
                                Issue
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccAdjMatTrns?Type=All")%>" class="card">
                            <div class="card-body">
                                Adjustment
                            </div>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccCreditor?Type=PRet")%>" class="card">
                            <div class="card-body">
                                Purchase Return
                            </div>
                        </a>
                    </div>

                </div>
                <div class="row no-gutters mb-2">
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/QuotationTop")%>" class="card card-first text-dark">
                            <div class="card-body">
                                Quotation
                            </div>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/ReceivableListCons")%>" class="card">
                            <div class="card-body">
                                Receivable
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <%--<a href="<%=this.ResolveUrl("~/F_23_CR/CustOthMoneyReceipt?Type=Billing")%>" class="card">
                            <div class="card-body">
                                Collection
                            </div>
                        </a>--%>
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccCollectionTopSheet?Type=Report")%>" class="card">
                            <div class="card-body">
                                Collection
                            </div>
                        </a>

                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDepositTopSheet?Type=Report")%>" class="card">
                            <div class="card-body">
                                Cheque Deposit
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalesTopSheet?Type=Report")%>" class="card">
                            <div class="card-body">
                                Collection Update
                            </div>
                        </a>
                    </div>

                </div>
                <hr />
                <div class="row no-gutters">
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("StandardWinMenuH")%>" class="card card-first text-dark">
                            <div class="card-body">
                                General
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/GlTransection")%>" class="card">
                            <div class="card-body">
                                Journal
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccRecPay?Type=Pay")%>" class="card">
                            <div class="card-body">
                                Payment
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccRecPay?Type=Rec")%>" class="card">
                            <div class="card-body">
                                Receipts
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccContraTrans")%>" class="card">
                            <div class="card-body">
                                Contra
                            </div>
                        </a>
                    </div>
                    <div class="col-md-2">
                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccPaymentPostDated?tcode=99&tname=Payment Voucher&Type=Acc")%>" class="card">
                            <div class="card-body">
                                PDC Issue
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="row m-3">
                    <div class="col-md-12">
                        <h3><a href="RptDefault?Type=All">Reports</a></h3>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="list-group">
                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptDocument")%>" class="list-group-item list-group-item-action">01. Documents
                            </a>
                           
                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptPurchase?Type=Pur")%>" class="list-group-item list-group-item-action">02. Purchase Register
                            </a>
                           
                            <a href="<%=this.ResolveUrl("~/F_15_Pur/RptLCStatus")%>" class="list-group-item list-group-item-action">03. Sales Register
                            </a>
                           <%-- <a href="<%=this.ResolveUrl("~/F_17_Acc/RptStockDet?Type=RawInv")%>" class="list-group-item list-group-item-action">05. Inventory

                            </a>--%>
                            <a href="<%=this.ResolveUrl("~/F_12_Inv/RptProjectStock?Type=inv")%>" class="list-group-item list-group-item-action">05. Inventory
                            </a>
                           <%-- <a href="<%=this.ResolveUrl("~/F_17_Acc/VatChalan")%>" class="list-group-item list-group-item-action">05. Inventory
                            </a>--%>
                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccountAll?Type=TB")%>" class="list-group-item list-group-item-action">06. Accounts Report
                            </a>
                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFincStatmnt?Type=Report")%>" class="list-group-item list-group-item-action"> 07. Financial Position 
                            </a>
                          <%--  <a href="<%=this.ResolveUrl("~/F_12_Inv/RptCostingSheetAll")%>" class="list-group-item list-group-item-action">Cras justo odio

                            </a>
                            <a href="<%=this.ResolveUrl("~/F_12_Inv/RptCostingSheetAll")%>" class="list-group-item list-group-item-action">Cras justo odio
                            </a>
                            <a href="<%=this.ResolveUrl("~/F_70_NBR/RptVatAll")%>" class="list-group-item list-group-item-action">Cras justo odio
                            </a>--%>
                            <a href="<%=this.ResolveUrl("~/F_32_Mis/ProjTrialBalanc?Type=PrjTrailBal")%>" class="list-group-item list-group-item-action">08. Trial Balance(P)
                            </a>
                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptProjBudVsExp")%>" class="list-group-item list-group-item-action">09. Project Budget
                            </a>
                             <a href="<%=this.ResolveUrl("~/F_16_Bill/RptProBillStatus")%>" class="list-group-item list-group-item-action">10. Bill Progress

                            </a>
                             <a href="<%=this.ResolveUrl("~/F_16_Bill/RptBill?Type=BillWiseResource")%>" class="list-group-item list-group-item-action">11. Bill Wise Resource 
                            </a>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
