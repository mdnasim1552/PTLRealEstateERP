<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkBudATenAnaLysis.aspx.cs" Inherits="RealERPWEB.F_07_Ten.LinkBudATenAnaLysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style19
        {
            width: 9px;
            height: 23px;
        }
        .style20
        {
            width: 82px;
            height: 23px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
        }
        .style23
        {
            height: 23px;
        }
        .style27
        {
            height: 17px;
        }
        .style32
        {
            height: 23px;
            width: 279px;
        }
        .style33
        {
            height: 23px;
            width: 47px;
        }
        .style34
        {
            height: 23px;
            width: 179px;
        }
        .style35
        {
            height: 23px;
            width: 21px;
        }
        .style36
        {
            height: 23px;
            width: 93px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<table style="width: 99%;">
        <tr>
            <td class="style26">
                <asp:Label ID="lblHeadtitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="PROJECT SCHEDULE VS ANALYSIS" Width="567px" Style="border-bottom: 1px solid white;
                    border-top: 1px solid white;" Height="16px"></asp:Label>
            </td>
            <td class="style28">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style27">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td class="style27">
                &nbsp;
            </td>
            <td class="style27">
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <fieldset style="border: 1px solid yellow;">
                            <legend>
                                <asp:Label ID="Label1" runat="server" Text="Information Field" Style="color: white;
                                    font-size: 12px; font-weight: bold;"></asp:Label></legend>
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style19">
                                        </td>
                                        <td class="style20">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                                color: #FFFFFF;" Text="Project Name:" Width="100px"></asp:Label>
                                        </td>
                                        <td class="style32">
                                            <asp:Label ID="lblProject" runat="server" BackColor="#000066" 
                                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                        </td>
                                        <td class="style33">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                                color: #FFFFFF;" Text="Item:"></asp:Label>
                                        </td>
                                        <td class="style34">
                                            <asp:Label ID="lblItem" runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="Yellow" Width="200px"></asp:Label>
                                        </td>
                                        <td class="style35">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                                color: #FFFFFF;" Text="Floor:"></asp:Label>
                                        </td>
                                        <td class="style36">
                                            <asp:Label ID="lblflrdes" runat="server" BackColor="#000066" 
                                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="Yellow" Width="100px"></asp:Label>
                                        </td>
                                        <td class="style23">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewScheduleVsAnalysis" runat="server">
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
