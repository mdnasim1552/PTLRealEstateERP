<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmPolicy.aspx.cs" Inherits="RealERPWEB.F_21_MKT.CrmPolicy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server">
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

            <div class="card mt-4" style="min-height: 500px">
                <div class="card-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="grvPolicy" AllowPaging="True" CssClass="table-striped  table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" OnPageIndexChanging="grvPolicy_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Policy Description">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPolCode" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblPolDesc" Width="120px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Policy Details">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPolDetails" runat="server" CssClass="border-0" Width="450px" Wrap="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "policydesc")) %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Policy Day">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPolDay" runat="server" CssClass="border-0" type="number"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "policyday")).ToString("#,##0;(#,##0); ") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                                <RowStyle CssClass="grvRowsNew" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
