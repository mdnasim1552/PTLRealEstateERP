<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BankBalance.aspx.cs" Inherits="RealERPWEB.F_32_Mis.BankBalance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style6
        {
            width: 21px;
        }
        .style7
        {
            width: 121px;
        }
        .style8
        {
        }
        .style12
        {
            width: 31px;
        }
        .style19
        {
            width: 34px;
        }
        .style20
        {
            width: 58px;
        }
        .style21
        {
        }
        .style23
        {
            width: 44px;
            }
        .style27
        {
            width: 68px;
        }
        .style61
        {
            width: 690px;
        }
        .style62
        {
            width: 75px;
        }
        .style64
        {
            width: 46px;
        }
        .style65
        {
            width: 78px;
        }
        .style66
        {
            width: 24px;
        }
        </style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <table style="width:98%;" >
    <tr>
        <td class="style61">
                    <asp:Label ID="lblAccLedger" runat="server" Text=" Bank Balance" 
                        CssClass="HeaderTitle" Width="679px"></asp:Label>
                </td>
        <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                </td>
        <td class="style62">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
        <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click" Font-Size="12px">PRINT</asp:LinkButton>
                </td>
    </tr>
    </table>
    <%--<tr>
        <td colspan="12">--%>
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                            
                    <table style="width:100%;">
                    <tr>
                    
                    <td colspan="11">
                        <asp:Panel ID="Panel1" runat="server">
                        <table style="width:100%;">
                        
                        <tr>
                            
                            <td class="style8">
                                &nbsp;</td>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style20">
                                &nbsp;</td>
                            <td class="style12">
                                &nbsp;</td>
                            <td class="style66">
                                <asp:Label ID="lbldateRange" runat="server" CssClass="label2" Text="Date Range" 
                                    Width="80px"></asp:Label>
                            </td>
                            <td class="style64" >
                                <asp:TextBox ID="txtFromdat" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFromdat_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromdat">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style6">
                                <asp:Label ID="lblTo" runat="server" CssClass="label2" Text="To"></asp:Label>
                            </td>
                            
                            <td class="style65">
                                <asp:TextBox ID="txtTodat" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTodat_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTodat">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="button" Width="71px" 
                                    onclick="lnkok_Click">Ok</asp:LinkButton>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FF3300" Text="Please Wait......" Width="157px" 
                                            style="color: #FFFF99"></asp:Label>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style23">
                                &nbsp;</td>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style20">
                                
                            </td>
                            <td class="style12">
                                
                            </td>
                            <td class="style66" valign="bottom" align="left">
                                
                                <asp:Label ID="lblreportlevel0" runat="server" CssClass="label2" 
                                    Text="Report Level" Width="80px"></asp:Label>
                                
                            </td>
                            <td colspan="2" align="left" valign="top">
                                
                                <asp:DropDownList ID="ddlRptlbl0" runat="server" Height="18px" 
                                    Width="105px">
                                    <asp:ListItem Value="2">Main</asp:ListItem>
                                     <asp:ListItem Value="4">Level2</asp:ListItem>
                                    <asp:ListItem Value="8">Level3</asp:ListItem>
                                     <asp:ListItem Value="12" Selected="True" >Details</asp:ListItem>
                                    
                                </asp:DropDownList>
                                
                            </td>
                            <td align="left" colspan="2" valign="top">
                                <asp:Label ID="lblmsg" runat="server" BackColor="#FFECFF" BorderColor="#996633" 
                                    BorderStyle="Solid" BorderWidth="0px" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#FF0066"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="style8">
                                &nbsp;</td>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style20">
                                &nbsp;</td>
                            <td class="style12">
                                &nbsp;</td>
                            <td class="style66">
                                &nbsp;</td>
                            <td class="style21" colspan="4">
                                &nbsp;&nbsp;</td>
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
                            <td class="style8" colspan="11">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DDFFEE" ShowFooter="True" Width="911px" 
                                    >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Dr. &lt;br&gt; Cr.&lt;br&gt;Balance" 
                                            FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Description of Account">
                                            <ItemTemplate>
                                                 <asp:HyperLink id="HLgvDesc" runat="server" Width="200px"  CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                        Font-Underline="False"  Target="_blank" 
                                                    ></asp:HyperLink> 
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel" 
                                                    
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#66CCFF" />
                                    <HeaderStyle BackColor="#66CCFF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                &nbsp;</td>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style20">
                                &nbsp;</td>
                            <td class="style12">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style64">
                              
                            </td>
                            <td class="style6">
                                &nbsp;</td>
                            <td class="style7">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
       <%-- </td>
    </tr--%>
    
<%--</table>--%>

















</asp:Content>

