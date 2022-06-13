<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceLoan.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.InterfaceLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input#ContentPlaceHolder1_txtSearch{
            height:29px;
        }
        .bw-100{
            width:100px!important;
        }
        .tbMenuWrp table {
            border: none !important;
            background: none !important;
        }

            .tbMenuWrp table tr {
                border: none !important;
                background: none !important;
            }

                .tbMenuWrp table tr td {
                    width: 140px;
                    float: left;
                    list-style: none;
                    margin: 2px 5px;
                    border: 0;
                    cursor: pointer;
                    background: #fff;
                    position: relative;
                    -webkit-border-radius: 5px;
                    -moz-border-radius: 5px;
                    border-radius: 5px;
                }

                    .tbMenuWrp table tr td label {
                        color: #000;
                        cursor: pointer;
                        font-weight: bold;
                        height: 100%;
                        margin: 1px 0;
                        padding: 2px;
                        width: 100%;
                    }

                        .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                            /*background: #12A5A6;*/
                            /*color: #fff;*/
                        }


                    .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
                        display: none;
                    }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background-color: #00ff21;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border-radius: 50%;
            color: #fff;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
        }

        .rptPurInt span.lbldata2 {
            display: block;
            font-size: 12px;
            color: #fff;
            line-height: 22px;
            margin: 5px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td:nth-child(1) {
            background: #0179a8 !important;
        }

        .tbMenuWrp table tr td:nth-child(2) {
            background: #5f4b8b !important;
        }

        .tbMenuWrp table tr td:nth-child(3) {
            background: #b76ba3 !important;
        }

        .tbMenuWrp table tr td:nth-child(4) {
            background: #00a28a !important;
        }

        .tbMenuWrp table tr td:nth-child(5) {
            background: #f7c46c !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="section">
                <div class="card mt-5">
                    <div class="card-header">
                        <div class="form-inline">

                            <asp:Label ID="Label1" runat="server" CssClass="mr-sm-2">From</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm mr-2"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                            <asp:Label ID="Label2" runat="server" CssClass="mr-2">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm  mr-2"></asp:TextBox>

                            <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            <div class="input-group input-group-alt input-group-sm mr-2">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">ID Card</asp:Label>
                                </div>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm" placeholder="ID Card"></asp:TextBox>
                                <div class="input-group-prepend ">
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn  btn-primary btn-sm">Ok</asp:LinkButton></li>
                                </div>
                            </div>

              <asp:Label ID="Label19" runat="server" CssClass="mr-2">Loan Type</asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-control-sm" style="width:350px;" >  
            <asp:ListItem Value="">Please Select</asp:ListItem>  
            <asp:ListItem>Home Loan</asp:ListItem>  
            <asp:ListItem>Car Loan</asp:ListItem>  

        </asp:DropDownList>  

                
                            <button type="button" class="btn btn-primary  ml-auto bw-100 btn-sm" data-toggle="modal" data-target="#myModal">
                                Apply Loan
                            </button>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="tbMenuWrp nav nav-tabs rptPurInt text-center text-white">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0"><h4 ><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Queue</span></asp:ListItem>
                                                <asp:ListItem Value="1"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Process</span></asp:ListItem>
                                                <asp:ListItem Value="2"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Approval</span></asp:ListItem>
                                                <asp:ListItem Value="3"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Generate</span></asp:ListItem>
                                                <asp:ListItem Value="4"><h4><span class="font-weight-bold text-white">44</span></h4><span class=" font-weight-bold text-white">Loan Completed</span></asp:ListItem>



                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div>
                                <asp:Panel ID="pnlallReq" runat="server" Visible="false">
                                    <div class="row">
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="modal" id="myModal" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Apply Loan</h6>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row mt-2">
                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server">Loan Id</asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm " placeholder="Loan ID" Enabled="false"></asp:TextBox>
                                    </div>



                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="">Statutory Deduction</asp:Label>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm" placeholder="Statutory Deduction"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server">Rate & Intrest</asp:Label>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control form-control-sm" placeholder="Rate & Intrest"></asp:TextBox>
                                    </div>

                                       <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">Purpose of loan</asp:Label>
                                          <textarea class="form-control form-control-sm"  rows="4" style="min-height:60px;"></textarea>
                                    </div>
                                </div>

                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" >Loan Amount</asp:Label>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control form-control-sm" placeholder="Loan ID" ></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server">Previous loan Amount</asp:Label>
                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server">Provident Fund</asp:Label>
                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" >Effective Date</asp:Label>
                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control form-control-sm " placeholder="Effective Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="">Installment Number</asp:Label>
                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control form-control-sm" placeholder="Installment Number"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server">Gross Monthly Salary</asp:Label>
                                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group ">
                                        <asp:Label ID="Label14" runat="server">Income Tax</asp:Label>
                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label15" runat="server">Loan Type</asp:Label>
                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control form-control-sm" placeholder="Loan type"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server">Amount Per Installment</asp:Label>
                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control form-control-sm" placeholder="Amt Per Installment"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server">Other Income</asp:Label>
                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control form-control-sm" placeholder="Other Income"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server">Other Deduction</asp:Label>
                                        <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control form-control-sm" placeholder="Other Deduction"></asp:TextBox>
                                    </div>
                       
                                </div>
                            </div>
                            <div class="rowmt-2">
                                <div class="d-flex justify-content-center">
<button class="btn btn-success btn-sm m-2 p2  bw-100" type="submit" >Save</button>
                                    <button class="btn btn-primary btn-sm p2 m-2 bw-100" type="submit" >Update</button>
                
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

