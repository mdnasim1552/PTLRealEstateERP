<%@ Page Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPurchaseStatusSupMatGroup.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseStatusSupMatGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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


        #ContentPlaceHolder1_gvStocjEvaluation_lblActualStock_0 {
            font-weight: bold;
        }

        .totalcount {
            font-weight: bold;
        }
    </style>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            $('[id*=chkResourcelist]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
            });

            $('.chzn-select').chosen({ search_contains: true });

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

            $('[id*=chkResourcelist]').multiselect({
                includeSelectAllOption: true,

                enableCaseInsensitiveFiltering: true
            });
        }


    </script>
    <style type="text/css">
        .table th, .table td {
            padding: 2px;
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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3" style="display:none;">
                            <div class="form-group">
                                <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName" Text=""></asp:Label>
                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click">Project Name&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="form-control form-control-sm"></asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="" OnClick="imgbtnFindSupplier_Click">Supplier&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblresName" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="lbtnresource" runat="server" OnClick="lbtnresource_Click" TabIndex="2">Material Group&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label15" runat="server" CssClass="form-label" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm" TabIndex="7"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="7"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ml-2" style="margin-top: 21px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_OnClick" CssClass="btn btn-sm btn-primary primaryBtn"
                                TabIndex="6">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-2" style="min-height: 480px;">
                <div class="card-body">
                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvpurvspay" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" AllowPaging="True" PageSize="30">
                                <PagerSettings Position="Bottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lgmgrpcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lgssirdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt" runat="server"
                                                Style="font-size: 12px; text-align: right; font-weight: bold;" Font-Size="12px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="fgvamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" HorizontalAlign="Right" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
