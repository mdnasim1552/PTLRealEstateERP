<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAccMktQuarterlyBgd.aspx.cs" Inherits="RealERPWEB.F_28_MPro.RptAccMktQuarterlyBgd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            try {

                var gridViewScroll = new GridViewScroll({
                    elementID: "gvQuartBgd",
                    width: 1450,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 2,
                    freezeColumnCount: 12,

                });
                gridViewScroll.enhance();

            }

            catch (e) {
                alert(e);
            }
        }

    </script>

    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
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
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="lblYear" runat="server">Year</asp:Label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px"></asp:LinkButton>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Page</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvQuartBgd" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging" ShowFooter="True"
                            CssClass="table-striped table-bordered grvContentarea" OnRowCreated="gvQuartBgd_RowCreated" OnRowDataBound="gvQuartBgd_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblActCode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Project">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblActDesc" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ATL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblATLQ1" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atlq1")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblBTLQ1" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "btlq1")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTTLQ1" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlq1")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Q1" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotQ1" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totq1")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ATL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblATLQ2" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atlq2")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblBTLQ2" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "btlq2")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTTLQ2" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlq2")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Q2" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotQ2" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totq2")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ATL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblATLQ3" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atlq3")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblBTLQ3" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "btlq3")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTTLQ3" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlq3")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Q3" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotQ3" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totq3")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ATL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblATLQ4" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atlq4")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblBTLQ4" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "btlq4")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TTL" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTTLQ4" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlq4")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Q4" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblTotQ4" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totq4")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooterNew" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeaderNew" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
