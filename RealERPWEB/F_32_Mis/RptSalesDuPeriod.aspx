<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalesDuPeriod.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptSalesDuPeriod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-4  pading5px asitCol4">

                                        <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>

                                        <asp:Label ID="Label2" CssClass=" smLbl_to" runat="server" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click" Width="44px">Show</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvsaldperiod" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="margin-right: 0px" Width="678px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Description" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000"  />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Expenditure">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExpenditure" runat="server" Font-Bold="True"
                                            Font-Size="12px"  ForeColor="#000"  Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvexpenditure" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Margin">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmargin" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prmar")).ToString("#, ##0.00;(#, ##0.00); ")+"%" %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFSales" runat="server" Font-Bold="True"
                                            Font-Size="12px"  ForeColor="#000"  Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSales" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Margin Amt">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFMaramt" runat="server" Font-Bold="True"
                                            Font-Size="12px"  ForeColor="#000"  Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMaramt" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maram")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
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





            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style45">&nbsp;</td>
                                    <td class="style41">
                                        <asp:Label ID="Label5" runat="server" CssClass="style40" Font-Bold="True"
                                            Style="text-align: right" Text="From.:" Width="100px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style42">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                        
                                    </td>
                                    <td class="style43">
                                        <asp:Label ID="Label6" runat="server" CssClass="style40" Font-Bold="True"
                                            Style="text-align: right" Text="To:" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


