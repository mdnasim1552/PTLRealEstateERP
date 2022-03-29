<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="TimeOfLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.TimeOfLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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


            <div class="card-fluid container-data  mt-2">
                <div class="row" id="Div1" runat="server">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                               Page Under Manintance 
                                <br />

                                 


                            </div>



                        </div>
                    </div>
                </div>
                <div class="row" id="warning" runat="server" visible="false">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                The supervisor setup incomplete
                                <br />

                                Please update your supervisor  
                                <asp:HyperLink ID="hylnkUserProfileEdit" class="alert-link" runat="server" NavigateUrl="~/F_81_Hrm/F_82_App/EmpProfileEdit.aspx" Target="_blank" ToolTip="Edit Your Profile"><i class="fas fa-user-edit">&nbsp;Click</i></asp:HyperLink>



                            </div>



                        </div>
                    </div>
                </div>

            </div>

            <div class="row" id="Lvform" runat="server"  >
                <div class="col-12 col-lg-12 col-xl-3">
                    <section class="card card-fluid" style="min-height: 650px">
                        <header class="card-header">Application for Time Of Leave</header>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Date   
                                </label>
                                <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtaplydate"></cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Remaning Time   
                                </label>
                                <asp:TextBox ID="txtTimeLVRem" runat="server" ReadOnly="true" AutoPostBack="true" class="form-control"></asp:TextBox>

                            </div>
                            <div class="row">
                                <div class="col-md-6 pl-0">
                                    <div class="form-group">
                                        <label for="sel1" id="frmdate" runat="server">From Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtFromTime" runat="server" TextMode="Time"  OnTextChanged="txtFromTime_TextChanged" AutoPostBack="true" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-6 pl-0">
                                    <div class="form-group">
                                        <label for="sel1" id="Label1" runat="server">To Time <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtToTime" runat="server" TextMode="Time" AutoPostBack="true" OnTextChanged="txtToTime_TextChanged" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Use Time   (hh:mm:ss)
                                </label>

                                <asp:TextBox ID="txtUseTime" runat="server" ReadOnly="true" AutoPostBack="true" class="form-control bg-danger" ForeColor="White"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label for="Reason">Reason/Remarks</label>
                                <asp:TextBox ID="txtLeavLreasons" runat="server" placeholder="Reason" TextMode="MultiLine" class="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group text-right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-sm btn-primary" />
                            </div>

                        </div>
                    </section>
                </div>

                <div class="col-12 col-lg-12 col-xl-9">
                    <section class="card card-fluid mb-0" style="min-height: 650px; flex-grow: 1; overflow: auto;">
                        <header class="card-header">Time of Leave History</header>

                         

                    </section>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
