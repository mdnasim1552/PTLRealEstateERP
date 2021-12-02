<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UnitConversion.aspx.cs" Inherits="RealERPWEB.F_07_Ten.UnitConversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">
                    <div class="row mt-2">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblBaseUnit" runat="server" class="control-label  lblmargin-top9px" Text="Base Unit"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlbUnit" runat="server" CssClass="custom-select  chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblConUnit" runat="server" class="control-label  lblmargin-top9px" Text="Con. Unit"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlcUnit" runat="server" CssClass="custom-select  chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lbtnAdd_Click">ADD</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvunit" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left" Width="396px">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtngvDelete" runat="server" Font-Bold="True" CssClass=" btn btn-xs" ToolTip="Delete Unit"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStyle" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bcodesc")) %>'
                                        Width="110px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvqty" runat="server" Style="text-align: right;"
                                        Text='1.00'
                                        Width="30"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conversion Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ccodesc")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Con. Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvConrat" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrat")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Note">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="110px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                        Font-Size="13px" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger btn-xs">Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
