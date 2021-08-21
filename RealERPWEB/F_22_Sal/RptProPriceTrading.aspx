<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProPriceTrading.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptProPriceTrading" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
}
        .style20
        {
            width: 82px;
            height: 23px;
        }
        .style21
        {
            width: 81px;
            height: 23px;
        }
        .style23
        {
            height: 23px;
        }
        .style26
        {
            width: 475px;
            height: 17px;
        }
        .style27
        {
            height: 17px;
        }
        .style28
        {
            height: 17px;
            width: 213px;
        }
 
      
        .style29
        {
            height: 23px;
            width: 21px;
        }
        
      
        .style30
        {
            height: 23px;
            width: 48px;
        }
        
      
        .style31
        {
            width: 62px;
            height: 23px;
        }
        
      
        </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



 <table style="width: 99%;">
        <tr>
            <td class="style26">
                <asp:Label ID="lblHeadtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Product Pricing (Trading) Report" Width="523px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style28">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style27">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td class="style27">
                &nbsp;</td>
            <td class="style27">
                </td>
        </tr>
        
        </table>
                
                

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="10">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style20">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                                            style="text-align: left; color: #FFFFFF;" Text="Rate Interest :" 
                                            Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="TxtIntrstRate" runat="server" style="margin-left: 0px" 
                                            Width="80px" BorderStyle="None">18%</asp:TextBox>
                                    </td>
                                    <td class="style29">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;" Text="Rate of Overhead :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style31">
                                        <asp:TextBox ID="TxtIntrstRate0" runat="server" style="margin-left: 0px" 
                                            Width="80px" BorderStyle="None">12%</asp:TextBox>
                                        
                                    </td>
                                    <td class="style23">
                                        <asp:LinkButton ID="lbtnOk0" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" Width="45px">Ok</asp:LinkButton>
                                        </td>
                                    <td class="style30">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                </tr>
                            
                                <tr>
                                    <td class="style20">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right; " Text="Page Size:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="10" 
                                            Width="90px">
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
                                    <td class="style29">
                                        &nbsp;</td>
                                    <td class="style31">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style30">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                </tr>
                            
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="10">
                                            <asp:GridView ID="gvRptTrading" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvRptTrading_PageIndexChanging" 
                                     ShowFooter="True">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Description">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="txtPactdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Developer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvDevName" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "devname")) %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvLoc" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Location")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgUnit" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Storied">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvStrid" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Storied")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFStrid" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Facing">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvUFacing" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                                        Width="40px" ></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvUSize" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lFUsize" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Floor No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvFlr" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrno")) %>'
                                                        Width="70px" ></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Parking Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvPqty" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parqty")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfParkingqty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handover Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvHanoDat" runat="server" style="text-align: left; font-size:11px;" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "handodat"))%>' 
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvPurDat" runat="server" style="text-align: left; font-size:11px;" 
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")%>' 
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Count Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvCDat" runat="server" style="text-align: left; font-size:11px;" 
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "countdat")).ToString("dd-MMM-yyyy")%>' 
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Duration(Days)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvDay" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: Right" 
                                                       Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daydif")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" ></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvuamt" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvPamt" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paramt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Utility">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvUtility" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:Label>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvPUtility" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                             
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="BD">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvBD" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bd")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:Label>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFBD" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Commision">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvComm" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comm")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:Label>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFComm" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Others Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvOthCost" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:Label>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFOthCost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTVal" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTVal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Bank Interest">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBint" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankin")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvBAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Overhead Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOVTamt" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "overhead")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOvAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Total Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTCost" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTCost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvARate" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acrate")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPay" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFPAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dues Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDamt" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dues")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Purchase Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPType" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ptype")) %>' 
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle ForeColor="White" HorizontalAlign="Left" />
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
                                    </tr>
                                </table>
                            
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
                </tr>
            </table>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

