<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TradingInterestCal.aspx.cs" Inherits="RealERPWEB.F_22_Sal.TradingInterestCal" %>

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
        .style24
        {
            height: 23px;
            width: 656px;
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
 
      
        .style31
        {
            width: 76px;
            height: 23px;
        }
        .style35
        {
            height: 23px;
            width: 85px;
        }
        .style36
        {
            height: 23px;
            width: 391px;
        }
        
      
        .style37
        {
            width: 77px;
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
                    ForeColor="Yellow" Text="Interest Comfirmation" Width="523px"
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
                                            style="text-align: left; color: #FFFFFF;" Text="Project Name:" 
                                            Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtSrcProject" runat="server" 
                                            Font-Bold="True" Width="80px" BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="imgbtnFindProject_Click" />
                                    </td>
                                    <td class="style24" colspan="3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="350px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td class="style23">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" Width="45px">Ok</asp:LinkButton>
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
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                </tr>
                                 <tr>
                                    <td class="style20">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                            style="text-align: left; color: #FFFFFF;" Text="Unit Name:" 
                                            Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtScrUnit" runat="server" 
                                            Font-Bold="True" Width="80px" BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        <asp:ImageButton ID="imgbtnUnit" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="imgbtnUnit_Click" />
                                    </td>
                                    <td class="style24" colspan="3">
                                        <asp:DropDownList ID="ddlUnitCode" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="350px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        
                                    </td>
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
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                </tr>
                              <tr>
                                    <td class="style20">
                                        <asp:Label ID="Label6" runat="server" Text="Rate Interest :" 
                                            Width="80px" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="TxtIntrstRate" runat="server" style="margin-left: 0px" 
                                            Width="80px" BorderStyle="None">18%</asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style31">
                                        
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;" Text="Rate of Overhead :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style37">
                                        <asp:TextBox ID="TxtIntrstRate2" runat="server" style="margin-left: 0px" 
                                            Width="80px" BorderStyle="None">12%</asp:TextBox>
                                        </td>
                                    <td class="style36">
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
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
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
                                            <asp:GridView ID="gvInstCalTrd" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="696px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDate" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="White" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText="Principal">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvProc" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="White" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Balance" FooterText="Total: ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbalAmt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="White" />
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvday" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daydif")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDays" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Bank Interest">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvIns" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intamt")).ToString("#,##0;(#,##0); ")%>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFInst" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Overhead Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOvamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFOvamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerSettings Position="Top" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
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


