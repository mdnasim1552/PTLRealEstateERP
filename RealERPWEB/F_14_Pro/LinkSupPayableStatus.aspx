<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkSupPayableStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.LinkSupPayableStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style20
        {
            width: 10px;
            height: 2px;
        }
        .style32
        {
            width: 1px;
        }
        .style34
        {
            width: 514px;
            height: 9px;
        }
        .style35
        {
            width: 150px;
            height: 2px;
        }
        .style36
        {
            width: 73px;
        }
        .style37
        {
            width: 24px;
        }
        .style57
        {
            width: 3px;
        }
        .style58
        {
            height: 2px;
        }
        .style59
        {
            width: 201px;
            height: 2px;
        }
        .style60
        {
            width: 164px;
            height: 2px;
        }
        .style70
        {
            width: 20px;
        }
        .txtboxformat
        {
        }
        .style73
        {
            width: 72px;
        }
        .style74
        {
            width: 255px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <table style="width: 97%;">
        <tr>
            <td class="style34">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Monthly Supplier & Group Wise Payable" Width="500px" Style="border-bottom: 1px solid white;
                    border-top: 1px solid white;"></asp:Label>
            </td>
            <td class="style32">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style70">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="11">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px"
                            Width="1000px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td class="style36">
                                        <asp:Label ID="lbldesciption" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                            color: #FFFFFF;" Text="Description:"></asp:Label>
                                    </td>
                                    <td class="style36" colspan="9">
                                        <asp:Label ID="lblvalDescription" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="320px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        &nbsp;</td>
                                    <td class="style73">
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
                                    <td class="style37">
                                        &nbsp;
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                            color: #FFFFFF;" Text="Size:"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="80px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lbldate" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                            color: #FFFFFF;" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="190px"></asp:Label>
                                    </td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style57">
                                        &nbsp;</td>
                                    <td class="style73">
                                        &nbsp;</td>
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
                    <td colspan="11">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="vSupPayable" runat="server">
                                <asp:GridView ID="gvPayableStatus" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvPayableStatus_PageIndexChanging" ShowFooter="True" Width="510px"
                                    OnRowDataBound="gvPayableStatus_RowDataBound">
                                    <PagerSettings Position="Top" />
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                          <ItemTemplate>

                                         <asp:HyperLink ID="hlnkgvresdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px">
                                                </asp:HyperLink>
                                           </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="During the Period ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperiodAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "periodam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperiodAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpayableAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetpayableAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                        Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvPayableSum" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" Width="510px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Style="text-align: left" 
                                                 
                                                    Width="80px" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvgroup" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mwgdesc")) %>'
                                                    Width="150px">
                                                </asp:HyperLink></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpeninggp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOpeninggp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                              <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="During the Period ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperiodAmtgp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "periodam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperiodAmtgp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle  HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpayableAmtgp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetpayableAmtgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle  HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                        Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td class="style58">
                    </td>
                    <td class="style59">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style20">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style60">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style58">
                    </td>
                    <td class="style35">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
