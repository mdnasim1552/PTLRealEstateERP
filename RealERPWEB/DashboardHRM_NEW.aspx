<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="DashboardHRM_NEW.aspx.cs" Inherits="RealERPWEB.DashboardHRM_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .btn-w {
            min-width: 19% !important;
        }
    </style>
    <div class="card-fluid container-data  mt-5">
        <section class="card card-fluid" style="height: 650px">

            <div class="row p-2">
                <div class="col-lg-9">

                    <div class="d-flex justify-content-between ">
                        <a class="btn btn-success btn-sm btn-w">New Joining</a>
                        <a class="btn btn-outline-success btn-sm btn-w">Employee Entry</a>
                        <a class="btn btn-outline-success btn-sm btn-w">Offer Letter</a>
                        <a class="btn btn-outline-success btn-sm btn-w">Personal Information</a>
                        <a class="btn btn-outline-success btn-sm btn-w">Appoinment Letter</a>
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Appoinment</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Employee Agreement</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Image Upload</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Joinning Letter</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Confirmation Letter</a>
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Attendance</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Attendance Upload</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Absent Approval</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Late Approval</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">LP Approval</a>
                    </div>
                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Leave</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Manual Leave</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Leave Apply</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Leave Process</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Employee Confirmation</a>
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Loan</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Loan Installment</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Promotion</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Increment Input</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Increment Update</a>
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Payroll</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Employee Transfer</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Employee Resign</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Deduction</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Addition</a>
                    </div>

                    <div class="d-flex justify-content-between mt-3">
                        <a class="btn btn-success btn-sm btn-w">Approval</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Salary Hold</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Salary Lock</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Salary Transfer Statement</a>
                        <a class="btn  btn-outline-success btn-sm btn-w">Employee Settlement</a>
                    </div>


                </div>
                <div class="col-lg-3">
                    <a class="btn  btn-info btn-sm btn-block">Reports</a>
                    <a class="btn  btn-outline-success btn-sm btn-block">Membours</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Attendance Report</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Employee Leave Status</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Employee Loan Status</a>
                    <a class="btn btn-outline-success btn-sm btn-block">New Joiner List</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Employee Resign</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Actual Salary</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Pay Slip</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Salary Reconciliation</a>
                    <a class="btn btn-outline-success btn-sm btn-block">Other Reports</a>
                </div>
            </div>
        </section>


    </div>
</asp:Content>
