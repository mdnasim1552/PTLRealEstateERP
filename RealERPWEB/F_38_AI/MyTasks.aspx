<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MyTasks.aspx.cs" Inherits="RealERPWEB.F_38_AI.MyTasks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #mytask {
            margin-top: -15px !important;
            margin-bottom: -20px !important;
        }

            #mytask p {
                margin-top: 8px !important;
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
            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <h4>My Tasks</h4>
                    </div>
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                <asp:RadioButtonList ID="btnMyTasks" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="btnMyTasks_SelectedIndexChanged">
                                    <asp:ListItem Value="1"><a class="nav-link" href="#">List </a></asp:ListItem>
                                    <asp:ListItem Value="2"><a class="nav-link" href="#">Board</a></asp:ListItem>
                                    <asp:ListItem Value="3"> <a class="nav-link" href="#">Calendar</a></asp:ListItem>
                                    <asp:ListItem Value="4"> <a class="nav-link" href="#">Files</a></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row d-flex" id="mytask">
                        <p>Last Task Completed on Sunday</p>
                        <span class="float-right ml-auto">
                            <p>InCompleted Tasks</p>
                        </span>
                    </div>
                </div>
                <div class="card-body">
                    <asp:MultiView runat="server" ID="MultiView1">
                        <asp:View runat="server" ID="ListView"></asp:View>
                        <asp:View runat="server" ID="BordView">
                            <div class="row">
                                <div class="col-md-2">
                                    <p>Recently assigned</p>
                                </div>
                                <div class="col-md-2">
                                    <p>+ &nbsp; ... &nbsp; To Day</p>
                                </div>
                                <div class="col-md-2">
                                    <p>+ &nbsp; ... &nbsp; To Next week</p>
                                </div>
                                <div class="col-md-2">
                                    <p>+ &nbsp; ... &nbsp; To Later</p>
                                </div>
                                <div class="col-md-2">
                                    <p>+ &nbsp; ... &nbsp; + Add Section</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="card" style="height: 200px;">
                                    </div>
                                    <div class="card" style="height: 200px;">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="CalendarView"></asp:View>
                        <asp:View runat="server" ID="FilesView"></asp:View>
                    </asp:MultiView>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
