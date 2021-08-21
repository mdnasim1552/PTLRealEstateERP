
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PrjInformation.aspx.cs" Inherits="RealERPWEB.F_07_Ten.PrjInformation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    
    
    <style type="text/css">
        .style5
        {
            width: 333px;
        }
        .style6
        {
            width: 90px;
        }
        .style7
        {
            width: 8px;
        }
    </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 98%;">
        <tr>
            <td class="style18">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="PROJECT INFORMATION VIEW/EDIT" Width="462px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" 
                    BackColor="#3366FF" ></asp:Label>
            </td>
            <td class="style21">
                &nbsp;</td>
            <td class="style19" align="right">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td class="style30">
                &nbsp;</td>
        </tr>
        </table>
                
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style6">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name:" 
                                                        style="color: #FFFFFF; text-align: right;" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style7">
                                                    &nbsp;</td>
                                                <td class="style5">
                                                    <asp:Label ID="lblProjectmDesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Visible="False" Width="350px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style26">
                                                    &nbsp;</td>
                                                <td class="style29">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style6">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" 
                                                        Width="100px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td class="style7">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" />
                                                </td>
                                                <td valign="top" class="style5">
                                                    <asp:DropDownList ID="ddlPrjName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="350px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                                        Width="350px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        onclick="lbtnOk_Click" 
                                                        style="text-align: center; color: #FFFFFF; font-size: 14px;" Width="40px" 
                                                        Height="16px" BackColor="#003366" BorderColor="White" BorderStyle="Solid" 
                                                        BorderWidth="1px">Ok</asp:LinkButton>
                                                </td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style26">
                                                    &nbsp;</td>
                                                <td class="style29">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:Label ID="lmsg" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White"  BackColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="430px" style="margin-right: 0px">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                        Width="200px" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                         
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Unit">
                                                
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtResunit" runat="server" BackColor="Transparent" 
                                                       BorderStyle="None" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>' 
                                                        Width="50px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lUpdatPerInfo_Click" 
                                                        style="text-decaration:none;">Update Information</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" 
                                                       BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                        Width="150px" style="text-align: left"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
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
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style20">
                                    <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" 
                                        QueryPattern="Contains" TargetControlID="ddlPrjName">
                                    </cc1:ListSearchExtender>
                                </td>
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style50">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                                <td class="style49">
                                    </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>





