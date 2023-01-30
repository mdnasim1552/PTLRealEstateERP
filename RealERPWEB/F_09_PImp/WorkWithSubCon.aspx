<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="WorkWithSubCon.aspx.cs" Inherits="RealERPWEB.F_09_PImp.WorkWithSubCon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>


    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <style>
        .multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        #ContentPlaceHolder1_divgrp {
            width: 395px !important;
        }



        .mt20 {
            margin-top: 20px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

           <%-- //var gvisu = $('#<%=this.grvissue.ClientID %>');--%>
            $.keynavigation(gvisu);
            // gvisu.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });

            $(function () {
                //$('[id*=lstfloor]').multiselect({
                //    includeSelectAllOption: true,
                //    enableCaseInsensitiveFiltering: true,
                //});

            });

            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });


            //gvisu.gridviewScroll({
            //           width: 1165,
            //           height: 420,
            //           arrowsize: 30,
            //           railsize: 16,
            //           barsize: 8,
            //           headerrowcount: 2,
            //           varrowtopimg: "../Image/arrowvt.png",
            //           varrowbottomimg: "../Image/arrowvb.png",
            //           harrowleftimg: "../Image/arrowhl.png",
            //           harrowrightimg: "../Image/arrowhr.png",
            //           freezesize: 5


            //       });

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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlist" runat="server" CssClass=" form-control  form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblContractorList" runat="server" Text="Contractor List"></asp:Label>
                                <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>  
                                <asp:LinkButton ID="ibtnFindContractorList" runat="server" OnClick="ibtnFindContractorList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlcontractorlist" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="Label3" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        
                        <div class="col-md-1.5">
                            <div class="form-group">

                                <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                            </div>
                        </div>                    
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Text="Page Size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click" Visible="true">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevISSList" runat="server" OnClick="lbtnPrevISSList_Click">Prev. List:</asp:LinkButton>
                                <asp:TextBox ID="txtSrcPreBill" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnPreBillList" runat="server" OnClick="ibtnPreBillList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="chzn-select form-control  form-control-sm"></asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblCurNo1" runat="server" Text="Bill ID:"></asp:Label>
                                <asp:Label ID="lblbillid" runat="server" CssClass="form-control form-control-sm" Text="LIS00-"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="txtBillid" runat="server" CssClass="form-control form-control-sm mt20" TabIndex="3">000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" Text="Ref No"></asp:Label>
                                <asp:DropDownList ID="ddltxtRefno" runat="server" CssClass="chzn-select form-control  form-control-sm " TabIndex="3"></asp:DropDownList>                              
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm mt20" TabIndex="3">000</asp:TextBox>
                            </div>
                        </div>
                     </div>   
                </div>
            </div>


            <asp:Panel ID="PnlRes" runat="server" Visible="True">
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblGroup" runat="server" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCatagory" runat="server" Text="Catagory"></asp:Label>
                                        <asp:DropDownList ID="ddlcatagory" runat="server" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblLabour" runat="server" Text="Labour"></asp:Label>
                                        <asp:DropDownList ID="ddlWorkList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblfloorno" runat="server" Text="Floor No"></asp:Label>
                                        <asp:ListBox ID="lstfloor" runat="server" CssClass="form-control form-control-sm chzn-select select2" SelectionMode="Multiple"></asp:ListBox>
                                    </div>

                                </div>
                                <div class="col-md-2" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-sm btn-primary">Select</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </asp:Panel>

            <div class="card card-fluid mb-1" style="min">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Panel ID="PnlNarration" runat="server">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblNaration" runat="server" Text="Narration:"></asp:Label>
                                                <asp:TextBox ID="txtISSNarr" runat="server" CssClass="form-control from-control-sm" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblTrade" runat="server" Text="Trade"></asp:Label>
                                                <asp:DropDownList ID="ddltrade" runat="server" CssClass="form-control from-control-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>

                                                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>

                                                <asp:Label ID="lblApprovalDate" runat="server" Text="Approv.Date:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

    