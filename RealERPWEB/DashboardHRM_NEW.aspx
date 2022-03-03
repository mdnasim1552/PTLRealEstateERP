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

            <div class="row">
                <div class="card-header text-center">
                    <h3>HRM Interface</h3>
                </div>
            </div>

            <div class="card-body">
                <div class="row">
                    <div class="col-9">

                        <div class="d-flex justify-content-between ">
                            <a class="btn btn-success btn-sm btn-w" href="#">New Joining</a>
                            <a class="btn btn-outline-success btn-sm btn-w" href="#">Employee Entry</a>
                            <a class="btn btn-outline-success btn-sm btn-w" href="#">Offer Letter</a>
                            <a class="btn btn-outline-success btn-sm btn-w" href="#">Personal Information</a>
                            <a class="btn btn-outline-success btn-sm btn-w" href="#">Appoinment Letter</a>
                        </div>

                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Appoinment</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Employee Agreement</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Image Upload</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Joinning Letter</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Confirmation Letter</a>
                        </div>

                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Attendance</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Attendance Upload</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Absent Approval</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Late Approval</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">LP Approval</a>
                        </div>
                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Leave</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Manual Leave</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Leave Apply</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Leave Process</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Employee Confirmation</a>
                        </div>

                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Loan</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Loan Installment</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Promotion</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Increment Input</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Increment Update</a>
                        </div>

                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Payroll</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Employee Transfer</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Employee Resign</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Deduction</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Addition</a>
                        </div>

                        <div class="d-flex justify-content-between mt-3">
                            <a class="btn btn-success btn-sm btn-w" href="#">Approval</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Salary Hold</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Salary Lock</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Salary Transfer Statement</a>
                            <a class="btn  btn-outline-success btn-sm btn-w" href="#">Employee Settlement</a>
                        </div>


                    </div>
                    <div class="col-3">
                        <div class="from-group row  mb-1">
                            <a class="btn  btn-info btn-sm btn-block text-white">Reports</a>
                        </div>
                    

                    
                        <a class="btn btn-outline-success btn-sm btn-block" href="#">Employee Leave Status</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Employee Loan Status</a>
                        <a class="btn btn-outline-success btn-sm btn-block">New Joiner List</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Employee Resign</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Actual Salary</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Pay Slip</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Salary Reconciliation</a>
                        <a class="btn btn-outline-success btn-sm btn-block">Other Reports</a>
                    </div>
                </div>
            </div>
        </section>


    </div>
</asp:Content>
