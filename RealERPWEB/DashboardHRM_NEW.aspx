<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="DashboardHRM_NEW.aspx.cs" Inherits="RealERPWEB.DashboardHRM_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



        <style>
            .card {
                min-height: 65px;
                margin-right: 5px;
                margin-bottom: 0;
                text-decoration: none !important;
                color: darkblue;
                background: white;
                font-size:15px;
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
                height:600px;
                overflow-y: auto;
            }

            .list-group-item:hover {
                box-shadow: inset 4px 0 0 0 #346cb0;
            }

            .left-most {
                margin-right: 20px;
                margin-bottom: 24px;
                color: white;
            }

                .left-most:hover {
                    color: white;
                }

            .left-most-height {
                height: 430px;
            }
        </style>

    <div class="mt-5">
        <div class="row">
            <div class="col-md-2">

                <div class="d-flex flex-column left-most-height">
                    <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-danger">
                                <div class="card-body">
                                    New Joinning
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                                <a href="#" class="card left-most bg-warning">
                                <div class="card-body">
                          Appoinments
                                </div>
                            </a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-info">
                                <div class="card-body">
                              Attendance
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-blue">
                                <div class="card-body">
                               
                          Leave
                                </div>
                            </a>
                        </div>
                    </div>

                     <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-success">
                                <div class="card-body">
              Loan
                                </div>
                            </a>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-purple">
                                <div class="card-body">
                                  Payroll
                                </div>
                            </a>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-12">
                            <a href="#" class="card left-most bg-success">
                                <div class="card-body">
                             
                                 Approval
                                </div>
                            </a>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-md-7">
 
                <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntryForm")%>" class="card card-first text-dark">
                            <div class="card-body">
                              Employee Entry
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/CreateOfferLt?Type=OLCreate")%>" class="card">
                            <div class="card-body" >
                                Offer Letter
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid=")%>" class="card">
                            <div class="card-body">
                                    Personal Information
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/LetterOfAppoinment?Type=LCreate")%>" class="card">
                            <div class="card-body">
                                Appoinment Letter
                            </div>
                        </a>
                    </div>
          
                </div>
              
                <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HREmpEntry?Type=Aggrement")%>"  class="card card-first text-dark">
                            <div class="card-body">
                             Employee Agreement
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/ImgUpload?Type=Entry&empid=")%>"  class="card">
                            <div class="card-body" >
                                Image Upload
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntryForm")%>"  class="card">
                            <div class="card-body">
                                    Joinning Letter
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/ConfirmLetter?Type=Confmletter")%>"  class="card">
                            <div class="card-body">
                                Confirmation Letter
                            </div>
                        </a>
                    </div>
          
                </div>
               <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAtten")%>" class="card card-first text-dark">
                            <div class="card-body">
                            Attendance Upload
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MabsentApp02")%>" class="card">
                            <div class="card-body" >
                              Absent Approval
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MLateAppDay")%>" class="card">
                            <div class="card-body">
                                    Late Approval
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MabsentApp")%>" class="card">
                            <div class="card-body">
                               LP Approval
                            </div>
                        </a>
                    </div>
          
                </div>
               <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT")%>" class="card card-first text-dark">
                            <div class="card-body">
                          Mannual Leave
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT")%>" class="card">
                            <div class="card-body" >
                             Leave Apply
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceLeavApp?Type=Ind")%>" class="card">
                            <div class="card-body">
                                    Leave Process
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/HREmpConfirmation")%>" class="card">
                            <div class="card-body">
                              Employee Confirmation
                            </div>
                        </a>
                    </div>
          
                </div>
                 
               <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanInfo?Type=Entry")%>" class="card card-first text-dark">
                            <div class="card-body">
                          Loan Installment
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpPro")%>" class="card">
                            <div class="card-body" >
                                Promotion
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_93_AnnInc/AnnualIncrement")%>" class="card">
                            <div class="card-body">
                                    Increment Input
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_93_AnnInc/HrIncrementUpdate")%>" class="card">
                            <div class="card-body">
                               Increment Update
                            </div>
                        </a>
                    </div>
          
                </div>
                  <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_87_Tra/HREmpTransfer")%>" class="card card-first text-dark">
                            <div class="card-body">
                         Employee Transfer
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                       <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RetiredEmployee")%>" class="card">
                            <div class="card-body" >
                            Employee Resign
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction")%>" class="card">
                            <div class="card-body">
                                   Deduction
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn")%>" class="card">
                            <div class="card-body">
                             Other Earning
                            </div>
                        </a>
                    </div>
          
                </div>
                <div class="row no-gutters mb-4">
                    <div class="col-md-3">
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/HrLeaveApprovalForm")%>" class="card card-first text-dark">
                            <div class="card-body">
                         Salary Hold
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary?Type=Mgt")%>" class="card">
                            <div class="card-body" >
                           Salary Lock
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary?Type=Entry")%>" class="card">
                            <div class="card-body">
                                   Salary Transfer Statement
                            </div>
                        </a>
                    </div>
                    <div class="col-md-3">
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RptSettlementStatus")%>"class="card">
                            <div class="card-body">
                             Employee Settelment
                            </div>
                        </a>
                    </div>
          
                </div>
            </div>
            <div class="col-md-3">
        
                <div class="row">
                    <div class="col-md-12">
                        <div class="list-group bg-white">
                           
                            <a href="#" class=" btn  btn-info btn-sm btn-block text-white">Reports
                            </a>
                           
                         <a href="<%=this.ResolveUrl("~//F_81_Hrm/F_84_Lea/RptHREmpLeave?Type=EmpLeaveSt")%>" class="list-group-item list-group-item-action">Employee Leave Status
                            </a>
                           
                         <a href="<%=this.ResolveUrl("~//F_81_Hrm/F_85_Lon/EmpLoanStatus?Type=Report&comcod=")%>" class="list-group-item list-group-item-action">Employee Loan Status
                            </a>
    
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02?Type=JoinigdWise&comcod=")%>" class="list-group-item list-group-item-action">New Joinner List
                            </a>
             
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RetiredEmployee")%>"" class="list-group-item list-group-item-action">Employee Resign
                            </a>

        
                         <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Payroll")%>" class="list-group-item list-group-item-action">Actual Salary
                            </a>
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Payslip")%>" class="list-group-item list-group-item-action">Pay Slip
                            </a>
                          <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalaryReconciliation")%>" class="list-group-item list-group-item-action">Salary Reconciliation
                            </a>
                           <a href="#" class="list-group-item list-group-item-action">Other Reports
                            </a>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
