<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkBgdEstStdAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.LinkBgdEstStdAna" %>
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
        .style8
        {
            width: 1223px;
        }
        .style10
        {
            width: 113px;
        }
        .style11
        {
            width: 8px;
        }
        .style12
        {
            width: 126px;
        }
        .style13
        {
            width: 1302px;
        }
        .style18
        {
            width: 693px;
        }
        .style19
        {
            width: 684px;
        }
        .style20
        {
            width: 672px;
        }
        .style21
        {
            width: 606px;
        }
        .style22
        {
            width: 790px;
        }
    </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 98%;">
        <tr>
            <td class="style18">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="ESTIMATED STANDARD ANALYSIS" Width="462px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" 
                    BackColor="#3366FF" ></asp:Label>
            </td>
            <td class="style21">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                </td>
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
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Project Name:" 
                                                        Width="80px"></asp:Label>
                                                </td>
                                                <td class="style10">
                                                    <asp:Label ID="lblValProjectName" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style22">
                                                    <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White"></asp:Label>
                                                </td>
                                                <td class="style12" valign="top">
                                                    &nbsp;</td>
                                                <td class="style8">
                                                    &nbsp;</td>
                                                <td class="style13">
                                                    &nbsp;</td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Work Name:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style10">
                                                    <asp:Label ID="lblValWorkName" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style22">
                                                    &nbsp;</td>
                                                <td class="style12" valign="top">
                                                    &nbsp;</td>
                                                <td class="style8">
                                                    &nbsp;</td>
                                                <td class="style13">
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
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Floor Name:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style10">
                                                    <asp:Label ID="lblValFloor" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style22">
                                                    &nbsp;</td>
                                                <td class="style12" valign="top">
                                                    &nbsp;</td>
                                                <td class="style8">
                                                    &nbsp;</td>
                                                <td class="style13">
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
                                                    <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True" 
                                                        BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                                        Font-Bold="True" Font-Size="12px" ForeColor="#660033" Height="16px" 
                                                        oncheckedchanged="chkAllSInf_CheckedChanged" style="color: #FFFFFF" 
                                                        TabIndex="7" Text="Show All" Width="80px" />
                                                </td>
                                                <td class="style10">
                                                    &nbsp;</td>
                                                <td class="style22">
                                                    &nbsp;</td>
                                                <td class="style12" valign="top">
                                                    &nbsp;</td>
                                                <td class="style8">
                                                    &nbsp;</td>
                                                <td class="style13">
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
                                <td colspan="11">
                                    <asp:GridView ID="gvEstAna" runat="server" AutoGenerateColumns="False" 
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
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkcode")) %>' 
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Resource" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="170px">
                                                                            
                                                                            
                                                </asp:Label></ItemTemplate>


                                                   <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdate" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lUpdate_Click" 
                                                        style="text-decaration:none;">Update </asp:LinkButton>
                                                </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                           
                                            <asp:TemplateField HeaderText="Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtbasenumber" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bnumber"))  %>'
                                                    Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         
                                           
                                            <asp:TemplateField  HeaderText="Unit">
                                                
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnTotql" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lnkbtnTotql_Click" 
                                                        style="text-decaration:none;">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvunit" runat="server" BackColor="Transparent" 
                                                       BorderStyle="None" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' 
                                                        Width="50px" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Length <br /> 1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlength" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnght")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="60px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Quantity  <br /> 2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Weight  <br /> 3">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvweight" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "weight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                Width="60px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                           <FooterTemplate>
                                                <asp:Label ID="lgvFWeight" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Weight  <br /> 4=1*2*3 ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalweight" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toweight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoWeight" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Total Number  <br /> 5">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtobase" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbase")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        
                                         <asp:TemplateField HeaderText="Total Qty   <br /> 6=4*5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        

                                            <asp:TemplateField HeaderText="Wastage (%)  <br /> 7">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvwastage" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wastage")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox></ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Purchase Qty  <br /> 8=6+(6*7)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtotalqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgtotalqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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
                                    &nbsp;</td>
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






