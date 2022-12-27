<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectStockEva.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptProjectStockEva" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {


            $(function () {
                $('[id*=chkResourcelist]').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });
            //$(function () {
            //    $('[id*=listGroup]').multiselect({
            //        includeSelectAllOption: true
            //    })

            //    $('[id*=lstProject]').multiselect({
            //        includeSelectAllOption: true
            //    })
            //});




            //var name = (1/2" PVC pipe);
            //name = name.replace("''", "").toLowerCase();
            //alert(name);



<%--            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvMatStock.ClientID %>');
            gv.Scrollable();

            var gv1 = $('#<%=this.gvMatStockSpec.ClientID%>');
            gv1.Scrollable();--%>


        }

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                    </div>
                                </div>




                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3" Style="width: 336px;">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4" Style="margin-left: -147px;">ok</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblresName" runat="server" CssClass="lblTxt lblName" Text="Material"></asp:Label>
                                        <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnresource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnresource_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>

                                    </div>
                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1200</asp:ListItem>
                                            <asp:ListItem>1500</asp:ListItem>
                                            <asp:ListItem>3000</asp:ListItem>
                                            <asp:ListItem>6000</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>













                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
