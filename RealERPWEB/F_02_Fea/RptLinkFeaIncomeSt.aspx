
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLinkFeaIncomeSt.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptLinkFeaIncomeSt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .style50
    {
        color: white;
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
    margin-left: 0px;
}
        .style57
        {
            width: 12px;
        }
        .style58
        {
            width: 89px;
        }
        .style59
        {
            width: 14px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY INCOME STATEMENT" 
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;</td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server"  BorderColor="Yellow" 
                             BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style58">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="Project Name:" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:Label ID="lblActDesc" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Text="Label" Width="300px"></asp:Label>
                                    </td>
                                    <td class="style59">
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
                       
                           
                                <asp:GridView ID="gvFeaIncomeSt" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" 
                                    onrowdatabound="gvFeaIncomeSt_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfoCode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Info Desc" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfodesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>




                                      
                                         <asp:TemplateField HeaderText="Items Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                          
                                                          
                                                          Width="250px"></asp:Label>                                      
                                                  
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Orginal Value">
                                          
                                            <ItemTemplate>
                                              <asp:HyperLink ID="HLgOamt"   runat="server" Target="_blank"  BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px"  style="font-size:12px; color:Black; text-decoration:none; text-align:right;"  
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orgamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revised Value">
                                          
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvrevisedvalue" runat="server" Target="_blank"  BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="font-size:12px; color:Black; text-decoration:none; text-align:right;" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Orginal">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Revised">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

