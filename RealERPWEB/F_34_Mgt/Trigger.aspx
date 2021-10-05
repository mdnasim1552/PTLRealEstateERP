<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" AutoEventWireup="true" CodeBehind="Trigger.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.Trigger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" id="lblDaterange" runat="server">From</label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" for="lblDateto" id="lblDateto" runat="server">To Date</label>
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>


                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary" OnClick="lnkok_Click">Update</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

