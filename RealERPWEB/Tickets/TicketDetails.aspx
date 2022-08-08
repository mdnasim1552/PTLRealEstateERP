<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="RealERPWEB.Tickets.TicketDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-title {
            font-size: 1.30rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid mt-3">
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

                <header class="page-title-bar mt-5">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item active">
                                <a href="../../Tickets/Index"><i class="breadcrumb-icon fa fa-angle-left mr-2"></i>Task List</a>
                            </li>
                            <li class="breadcrumb-item active">
                                <a href="#">Task Details Ticket ID <span id="tickteID" class="badge badge-success" runat="server"></span></a>
                            </li>
                        </ol>
                    </nav>
                 
                </header>


                <div class="row">
                    <div class="col-md-12">
                        <h4 class="page-title">Ticket Details</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-8 col-lg-7">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <p class="mt-3 mb-1">Fixed By</p>
                                        <h6 id="assignEngName" runat="server" class="font-weight-bold"></h6>
                                    </div>
                                    <div class="col-md-6">
                                        <p class="mt-3 mb-1">Company Name</p>
                                        <h6 id="companyName" runat="server"></h6>
                                    </div>
                                    <div class="col-md-3">
                                        <p class="mt-3 mb-1">Creation Date</p>
                                        <h6 id="creatDate" runat="server"></h6>
                                    </div>
                                    <div class="col-md-4 font-weight-bold">
                                        <p class="mt-3 mb-1">Creation by</p>
                                        <h7 id="creatBy" runat="server"></h7>
                                    </div>
                                </div>
                                <h6 class="mt-4">Ticket Details:</h6>
                                <p id="ticketDesc" class="mb-4" runat="server"></p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 col-lg-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-title mb-3">Attachments</div>

                                <asp:Panel ID="Panel2" runat="server">

                                    <asp:Image ID="BindImg" runat="server" CssClass="img img-thumbnail" />

                                </asp:Panel>


                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Literal ID="embedPdf" runat="server" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
