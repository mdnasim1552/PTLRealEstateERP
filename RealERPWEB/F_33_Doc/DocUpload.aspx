<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="DocUpload.aspx.cs" Inherits="RealERPWEB.F_33_Doc.DocUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script type="text/javascript">


        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {

                $('.chzn-select').chosen({ search_contains: true });

            }
            catch (e) {
                alert(e);
            }

        };
        $('.chzn-select').chosen({ search_contains: true });
    </script>



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
            <div class="card mt-5">
     
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-4">
                       <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Label ID="Label20" runat="server" Text="Type:"></asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="ID No:"> </asp:Label>

                                <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="form-control form-control-sm" ></asp:Label>

                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">

                                <asp:Label ID="Label13" runat="server"> Date</asp:Label>

                                <asp:TextBox ID="txtDate" runat="server" ToolTip="(dd-MMM-yyyy)" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                            </div>
                        </div>


                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Label ID="lbltitle" runat="server"
                                    Text="Short Name:" ></asp:Label>

                                <asp:TextBox ID="txtsName" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                       <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" Visible="false">
                                </asp:DropDownList>

                                
                           
                                       <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                                      <asp:Label ID="Label19" runat="server" Text="Details:"></asp:Label>

                                    <asp:TextBox ID="txtDetails1" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                                 <asp:Label ID="Label11" runat="server" Text="Documents:"></asp:Label>
                                                      <asp:FileUpload ID="imgFileUpload" runat="server" 
                                        onchange="submitform();"/>
                              
              
                            </div>
           
                        </div>
                                         <div class="col-lg-12">
                                      <asp:Image ID="EmpImg" runat="server" Height="60px" Width="60px" />
                            </div>

                        <div class="col-lg-12">
                            <div class="form-group">
                                
                                    <asp:LinkButton ID="lbtnUpdateImg" CssClass="btn btn-success btn-sm mt20" runat="server" OnClick="lbtnUpdateImg_Click"
                                       >Update</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                        </div>

                        <div class="col-lg-8">

                        </div>
                    </div>
                </div>
            </div>
 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>






