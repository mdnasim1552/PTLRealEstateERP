﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateTicket.aspx.cs" Inherits="RealERPWEB.Tickets.CreateTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
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
                <div class="container moduleItemWrpper">
                    <div class="contentPart">
                        <div class="mt-5">
                            <div>
                                <div>
                                    <h4>Create Ticket </h4>
                                </div>
                                <div>
                                    <asp:Label ID="TicketID" runat="server" Visible="false" Text="0"></asp:Label>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="ticket type">Ticket Type</label>
                                                <asp:DropDownList ID="ddlTicketType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="10116">Service</asp:ListItem>
                                                    <asp:ListItem Value="10111">Data Delete</asp:ListItem>
                                                    <asp:ListItem Value="10112">Data Correction</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="create date">Create Date</label>
                                                <asp:TextBox ID="txtTdate" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy h: mm tt" TargetControlID="txtTdate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div class="form-group">
                                                <label for="task desc" id="tsklbl" runat="server">
                                                    Ticket Description       
                                                </label>
                                                <asp:TextBox ID="txtTicketDesc" required="required" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <div class="form-group">
                                                <label for="image">Image Upload </label>
                                                <asp:FileUpload ID="imageUpload" runat="server" />
                                                <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click"></asp:Button>--%>
                                                <asp:Image ID="imageShow" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-12">
                                            <div class="form-group">
                                                <label for="priority">Priority</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlPriority">
                                                    <asp:ListItem Value="99101">Normal</asp:ListItem>
                                                    <asp:ListItem Value="99102">Important</asp:ListItem>
                                                    <asp:ListItem Value="99103">Urgent</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div>
                                    <button type="button" runat="server" id="btnSave" data-dismiss="modal" aria-hidden="true" onserverclick="btnSave_ServerClick" class="btn btn-primary">Save changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


