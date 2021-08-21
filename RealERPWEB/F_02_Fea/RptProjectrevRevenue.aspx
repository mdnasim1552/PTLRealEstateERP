<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectrevRevenue.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptProjectrevRevenue" %>
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY REPORT" 
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
                       
                           
                                <asp:GridView ID="gvFeaProPricing" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" Width="785px" 
                                    onrowdatabound="gvFeaProPricing_RowDataBound">
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
                                      
                                         <asp:TemplateField HeaderText="Unit Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                      Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "udesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")).Trim(): "")  %>'   
                                                          Width="150px"></asp:Label>                                      
                                                  
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnitn" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQty" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                       

                                          <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgSize" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        

                                        <asp:TemplateField HeaderText="Rate/SFT">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Font-size="11px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Unit Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parking">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Utility">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUtility" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Co-Operative">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coopamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Others">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOthers" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otham")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Facing">
                                            <ItemTemplate>
                                                <asp:Label ID="lgFacing" runat="server" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Label ID="lgview" runat="server" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

