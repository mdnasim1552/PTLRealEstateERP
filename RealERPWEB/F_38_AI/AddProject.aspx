﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="RealERPWEB.F_38_AI.AddProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-select {
            width: 100%;
        }

        .chzn-container {
            width: 100% !important;
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


            <div class="card">
                <div class="card-header">
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12">

                            <div class="from-group">
                                <asp:Label ID="Label4" runat="server">Client Name  &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-sm btn-info"><i class="fa fa-plus "></i></asp:LinkButton></asp:Label>
                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">MD. HARUN-UR RASHID (FCMA)</asp:ListItem>
                                    <asp:ListItem Value="1">MD. EMDADUL HAQUE</asp:ListItem>
                                    <asp:ListItem Value="2">UZZAL KUMAR PRAMANIK</asp:ListItem>
                                    <asp:ListItem Value="3">MD. AHASAN ULLAH NAHID</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="from-group">

                                <asp:Label ID="Label1" runat="server">Project Name</asp:Label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="from-group">

                                <asp:Label ID="Label7" runat="server">Project Label</asp:Label>
                                <asp:DropDownList ID="ddProjectlb" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Pilot</asp:ListItem>
                                    <asp:ListItem Value="1">sow</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="from-group">

                                <asp:Label ID="Label2" runat="server">Project Type</asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Image Annotation</asp:ListItem>
                                    <asp:ListItem Value="1">Software Development</asp:ListItem>
                                    <asp:ListItem Value="2">Web Desgin</asp:ListItem>
                                    <asp:ListItem Value="3">3D Animation</asp:ListItem>
                                    <asp:ListItem Value="4">Graphis Desgin</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="from-group">

                                <asp:Label ID="Label3" runat="server">Work Type</asp:Label>
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Image Annotation</asp:ListItem>
                                    <asp:ListItem Value="1">Software Development</asp:ListItem>
                                    <asp:ListItem Value="2">Web Desgin</asp:ListItem>
                                    <asp:ListItem Value="3">3D Animation</asp:ListItem>
                                    <asp:ListItem Value="4">Graphis Desgin</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12">
                            <div class="from-group">
                                <asp:Label ID="lblSrtTime" runat="server">StartTime
                                                   
                                </asp:Label>
                                <asp:TextBox ID="tblstarttime" runat="server" CssClass="form-control form-control-sm" TextMode="DateTimeLocal"></asp:TextBox>
                            </div>
                            <div class="from-group">
                                <asp:Label ID="Label5" runat="server">DeadLine </asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="DateTimeLocal" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="from-group">
                                <asp:Label ID="Label6" runat="server">Quantity</asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm" TextMode="Number"></asp:TextBox>
                            </div>

                            <div class="from-group">
                                 <asp:Label ID="Label15" runat="server">Currency Type</asp:Label>
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control chzn-select">
                                <asp:ListItem Value="0">BDT</asp:ListItem>
                                <asp:ListItem Value="1">USD</asp:ListItem>

                            </asp:DropDownList>
                            </div>

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                        </div>
                        <div class="col-lg-3">
                        </div>
                        <div class="col-lg-3 ">
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="tblClientName" runat="server">Client Name  &nbsp;<asp:LinkButton ID="btnClient" runat="server">+</asp:LinkButton></asp:Label>
                            <asp:DropDownList ID="ddClientName" runat="server" CssClass="form-control chzn-select">
                                <asp:ListItem Value="0">MD. HARUN-UR RASHID (FCMA)</asp:ListItem>
                                <asp:ListItem Value="1">MD. EMDADUL HAQUE</asp:ListItem>
                                <asp:ListItem Value="2">UZZAL KUMAR PRAMANIK</asp:ListItem>
                                <asp:ListItem Value="3">MD. AHASAN ULLAH NAHID</asp:ListItem>


                            </asp:DropDownList>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-lg-3 mt20">
                            <asp:Label ID="Label10" runat="server">Project Type</asp:Label>
                            <asp:DropDownList ID="ddProjectType" runat="server" CssClass="form-control chzn-select">
                                <asp:ListItem Value="0">Image Annotation</asp:ListItem>
                                <asp:ListItem Value="1">Software Development</asp:ListItem>
                                <asp:ListItem Value="2">Web Desgin</asp:ListItem>
                                <asp:ListItem Value="3">3D Animation</asp:ListItem>
                                <asp:ListItem Value="4">Graphis Desgin</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 mt20">
                            <asp:Label ID="Label13" runat="server">Quantity</asp:Label>
                            <asp:TextBox ID="tblQuantity" runat="server" CssClass="form-control form-control-sm" TextMode="Number"></asp:TextBox>
                            <%--<asp:TextBox ID="tblQuantity" runat="server" CssClass="form-control form-control-sm" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>--%>
                        </div>
                        <div class="col-lg-3 mt20">

                            <%--<cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy hh:mm:ss" TargetControlID="tblstarttime"></cc1:CalendarExtender>--%>
                        </div>
                        <div class="col-lg-3 mt20">
                            <asp:Label ID="Label12" runat="server">DeadLine </asp:Label>
                            <asp:TextBox ID="tblDeadLine" runat="server" TextMode="DateTimeLocal" CssClass="form-control form-control-sm"></asp:TextBox>

                            <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="tblDeadLine"></cc1:CalendarExtender>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 mt20">
                           
                        </div>
                        <div class="col-lg-3 mt20">
                            <asp:Label ID="Label11" runat="server">Cost</asp:Label>
                            <asp:TextBox ID="tblCost" runat="server" CssClass="form-control form-control-sm" TextMode="Number"></asp:TextBox>
                            <%-- <cc1:CalendarExtender  runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>--%>
                        </div>
                        <div class="col-lg-6 mt20">
                            <asp:Label ID="Label14" runat="server">Remarks</asp:Label>
                            <asp:TextBox ID="tblRemarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-5 mt20">
                            <asp:Label ID="Label16" runat="server">Images</asp:Label>

                            <ajaxToolkit:AjaxFileUpload Width="600px" ID="AjaxFileUpload1" runat="server" ThrobberID="myThrobber" MaximumNumberOfFiles="10" AllowedFileTypes="jpg,jpeg,pdf"></ajaxToolkit:AjaxFileUpload>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
