﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPurchaseSummaryVsPayment.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseSummaryVsPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

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
                    <div class="row pb-4">
                        <div class="col-md-2">
                            <asp:Label ID="Label15" runat="server" CssClass="form-label" Text="From"></asp:Label>

                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"
                                TabIndex="7"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="To"></asp:Label>

                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"
                                TabIndex="7"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                       



                        <div class="col-md-1 ml-2" style="margin-top: 21px;">
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
                                ShowFooter="True" AllowPaging="True"  PageSize="30" 
                                >
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
                                    
                                    
                                    <asp:TemplateField HeaderText="Particulars">

                                        <ItemTemplate>
                                            <asp:Label ID="lgPart" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "particular")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    


                                    <asp:TemplateField HeaderText="Amount">

                                        <ItemTemplate>
                                            <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 12px; text-align: right; font-weight:bold;" Font-Size="12px"
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
