<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLinkRevenueACost.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptLinkRevenueACost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
            margin-left: 0px;
        }
        .style60
        {
            width: 97px;
        }
        .style61
        {
            width: 109px;
        }
        .style62
        {
            width: 94px;
        }
        .style63
        {
            width: 27px;
        }
    
    .style50
    {
        color: white;
    }
        .style64
        {}
        .style65
        {
            width: 56px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="Yellow"
                    Style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY"
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;
            </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="False"
                    Font-Underline="True" OnClick="lnkPrint_Click" Style="border-left-width: 2px;
                    border-left-color: #ffff00; text-align: center; color: #FFFFFF;" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr><td class="style64">
                    <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                        <table style="width:100%;">
                            <tr>
                                <td class="style65">
                                    <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" 
                                        Font-Size="12px" style="text-align: left" Text="Project Name:" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblActDesc" runat="server" BackColor="#000066" 
                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="300px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewCost" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjFC" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodfc" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvItemdesc2" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnum2" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Land">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvlandsize" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sft.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvconktosft" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sft.Per Floor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsftperf" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" MGC %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpercntge" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sft./Floor">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvlsizes" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No.of Floor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvstonum" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Sft.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtcsft" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvtcsft" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total (%)">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPercent" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perent")).ToString("#,##0.00;(#,##00.00); ")+" %" %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="792px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvItemdescc" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnumc" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqtyc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvratec" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Orginal Cost">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgveamtc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFeAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="App. Add Cost">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvaaddamtc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aadam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFaaddAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expected Add Cost">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvexaddamtc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eadam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFexaddAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Save Cost">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvsaveamtc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "savam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFsaveAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revised Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgvtotalamtc" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtotalAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost Per Sft">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcospersft" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costpsft")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtotalCpSft" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewSalesRevenue" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSaleableAreaa" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="Saleable Area (Total):" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjFCS" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodfcs" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvItemdesc3" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnum3" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Land">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvlandsizes" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sft.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvconktosfts" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sft Per F">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsftperfs" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MGC %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpercntges" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total SFT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvlsizess" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sto Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvstonums" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T S Sft.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtssfts" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvtssfts" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brochure Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvBQTY" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvBDesc" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc"))%>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSaleableAreaDistributot" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="Saleable Area Distribution:" 
                                                Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                                BorderWidth="1px">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style60">
                                                            <asp:Label ID="lblLownertext" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Text="Land Owner Share:" Width="116px"></asp:Label>
                                                        </td>
                                                        <td class="style61">
                                                            <asp:Label ID="lblLownerval" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Yellow" Height="16px" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style62">
                                                            <asp:Label ID="lblCompanytext" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Height="16px" Text="Company Share:" Width="116px"></asp:Label>
                                                        </td>
                                                        <td class="style63">
                                                            <asp:Label ID="lblCompanyval" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Yellow" Height="16px" Width="116px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrj" runat="server" AutoGenerateColumns="False" 
                                              ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTotal1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvItemdesc" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnum" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvSize" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtotalsh" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Land Owner">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvlowner" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lowner")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlownersh" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcompany" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "company")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcompanysh" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase From Land Owner">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvpurfrmlanowner" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pflowner")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpfrmlownersh" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Adjustment">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvadj" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjmnt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFadjmnt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtotalcompany" runat="server" Font-Size="11px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalcom")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtcompanysh" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSaleRevenue" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="White" Text="Sale Revenue:" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjsalrev" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" 
                                                                Style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodsalrev" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTotal2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvItemdesc4" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizessalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisessalrev" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgratesalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamtsalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamtsalrev" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brochure Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgbroratsalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblSodInformation" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="Sold Information" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjSoldInfo" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodsoldinfo" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItemdescsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>' 
                                                                Width="200px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnumsoldinfo" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizessoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisessoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamtsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamtsoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Average Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgavgratsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblUnSodInformation" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="UnSold Information" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvFeaPrjusoldinfo" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodusoldinfo" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItemdescusoldinfo" runat="server" 
                                                                BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>' 
                                                                Width="200px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitusoldinfo" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizesusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisesusoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrateusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamtusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamtusoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Average Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgavgratusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                              <asp:View ID="ViewSalesLand" runat="server">
                                <table style="width:100%;">
                                  
                                    <tr>
                                        <td class="style64">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="White" Text="Sale Revenue:" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="grvSaleRevLand" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodsalrev" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                     
                                                        <ItemTemplate>
                                                         
                                                            <asp:TextBox ID="txtgvItemdescsalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>' 
                                                                Width="200px"></asp:TextBox>

                                                       
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizessalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>

                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisessalrev" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                     

                                                     <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgratesalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                   
                                                   
                                                   
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamtsalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />

                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamtsalrev" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Brochure Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgbroratsalrev" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style64">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="Sold Information" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="grvSoldLand" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodsoldinfo" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                     
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItemdescsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>' 
                                                                Width="200px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitnumsoldinfo" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizessoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisessoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamtsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamtsoldinfo" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Average Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgavgratsoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style64">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="White" Text="UnSold Information" Width="206px"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="grvUnSoldLAnd" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="651px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodusoldinfo" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                       
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItemdescusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>' 
                                                                Width="200px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnitusoldinfo" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtglsizesusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlsisesusoldinfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrateusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamtusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>' 
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamtusoldinfo" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Average Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgavgratusoldinfo" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style64">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style64">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
