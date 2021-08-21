<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMisDailyActiviteis.aspx.cs" Inherits="RealERPWEB.F_35_MgtAct.RptMisDailyActiviteis" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style18
        {
            color: #FFFFFF;
            text-align: right;
        }
        .style59
        {
            width: 64px;
        }
        .style21
        {
            width: 92px;
        }
        .style19
        {
            width: 105px;
        }
                        
        .StyleCheckBoxList
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="HeaderText" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Management Interface" Width="600px" Style="border-bottom: 1px solid blue;
                    border-top: 1px solid blue;"></asp:Label>
            </td>
            <td class="style22">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" CssClass="style18" Font-Bold="True" Font-Size="12px"
                                            Text="Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:TextBox ID="txtDate" runat="server" BorderStyle="None" CssClass="txtboxformat"
                                            Width="85px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbltodate" runat="server" CssClass="label2" Height="16px" Text="To:"
                                            Width="5px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" Width="97px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style21">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Underline="False" ForeColor="White"
                                            OnClick="lbtnOk_Click" Style="font-size: small; font-weight: 700; height: 17px;
                                            text-align: center;" Width="30px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style19">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Style="text-align: center"
                                                    Text="Please wait . . . . . . ." Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td class="style19">
                                    </td>
                                    <td class="style19">
                                    </td>
                                    <td class="style19">
                                    </td>
                                    <td class="style19">
                                    </td>
                                    <td class="style19">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSales" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="A. Sales" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvDayWSale_RowDataBound" ShowFooter="True" 
                            Style="margin-right: 0px" Width="16px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Capacity Avaialable">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcapacityotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmasbgdotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Break-even">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbepotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Target Sales">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvtsaleamt" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                            Style=" background-color: Transparent; color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target As on Today">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatosaleamt" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosaleamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Sales">
                                    <ItemTemplate>
                                      <asp:HyperLink ID="hlnkgvDSAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achieved in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperotsal" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotsal")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Graph">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvgraph" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerSettings Position="TopAndBottom" />
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
                        <asp:Label ID="lblCollectionStatus" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="B. Collection Status" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvrcoll_RowDataBound" ShowFooter="True" Width="267px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptcoderc" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            Style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Capacity Avaialable">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcapacityotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmasbgdotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Break-even">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbepotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target Collection">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvtcollamt" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                            Style="background-color: Transparent; color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target As on Today">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatocollamt" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tastcollamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achievement">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvaccollAmt" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                            Style="background-color: Transparent; color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achieved in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperotcoll" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotcoll")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Graph">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvgraphcoll" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                            Style="background-color: Transparent; color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>' 
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                        <asp:Label ID="lblChequeInHand" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="C. Cheque In Hand" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvchequeinhand" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="341px" OnRowDataBound="gvchequeinhand_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                               

                                 <asp:TemplateField HeaderText="In Hand(Returned Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhrchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Hand(Fresh Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhfchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Hand(Post Dated Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhpchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvamtcin" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                            ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqinhand")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRecAPayEncash" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="D. Receipt &amp; Payment(Encashment Basis)" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvarecandpay" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvarecandpay_RowDataBound" ShowFooter="True" Width="128px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrecam" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvbalpam" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="text-align: right; background-color: Transparent" Target="_blank" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
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
                        <asp:Label ID="lblBankPosition" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="E. Bank Position" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvBankPosition_RowDataBound" ShowFooter="True" Width="399px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum2" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvbankposition" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="background-color: Transparent" Target="_blank" 
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))  %>' 
                                            Width="140px"> 

                                            
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbankbalbp" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closbal")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Liabilities">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbankliabp" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closlia")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Loan Limit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnetcbolia" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avloan")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                        <asp:Label ID="lblRecAPayIssue" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="F. Receipt &amp; Payment(Issue Basis)" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvarecandpayis" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvarecandpayis_RowDataBound" ShowFooter="True" Width="144px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrecamis" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamis" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvbalamis" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="text-align: right; background-color: Transparent" Target="_blank" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
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
                        <asp:Label ID="lblPDCCheque" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="G. PDC Issue Status" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvpdc" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvpdc_RowDataBound" ShowFooter="True" Width="399px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvCompanypaysum" runat="server" __designer:wfdid="w38" 
                                            Font-Size="12px" Font-Underline="False" Style="font-size: 11px; color: Black;" 
                                            Target="_blank" 
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))   %>' 
                                            Width="140px"> 


                                            
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtpdc" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due to Pay">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDC">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpdc" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdcam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                        <asp:Label ID="lblProcurement" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="H. Procurement" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvprocure" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="341px" OnRowDataBound="gvprocure_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre8" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Comcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcomcodepro" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvreqpro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                            Font-Underline="false"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"  Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Material Not Yet Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartpro" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            Style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>' 
                                            Width="140px"> 
                                            
                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Materials Received">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvbillpro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"  Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Bill Completed">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvourchasepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"   Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Purchase History Material Wise">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvpurhmwisepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="100px" ></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText=" Purchase History Supplier Wise">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvpurhswisepro" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                            Style="background-color: Transparent; color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>





                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
               <tr>
                    <td>
                        <asp:Label ID="lblConstruction" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="I. Construction" Visible="False" 
                            Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvMMPlanVsAch" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="gvMMPlanVsAch_RowDataBound" Width="16px">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcomnamecons" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="100px">
                                        </asp:HyperLink></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Target As Per Master Plan (Upto Date)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmasterplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmasPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monthly Work Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Monthly Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Mas. Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMasPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromasp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Monthly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMonthPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromonp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor Wise Progress">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvflrwiseprogress" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="System Generated Target">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvsysgentarget" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Inflation Effect">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvineffect" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <asp:Label ID="lblFeasibility" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="J. Bussiness Position(Today)" Visible="False" 
                            Width="300px"></asp:Label>
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
                    <td>
                        <asp:GridView ID="gvFeasibility" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound=" gvFeasibility_RowDataBound" ShowFooter="True" 
                            Width="399px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum4" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvFeadesc" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="text-align: left; background-color: Transparent" Target="_blank" 
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim() : "")%>' 
                                                  
                                            Width="180px"> 

                                            
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfeacost" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvfearevnue" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oramt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfeamargin" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maramt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfeapercnt" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chngeper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                        <asp:Label ID="lblStock" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="K. Sold Status" Visible="False" 
                            Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvpstk01" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="341px" OnRowDataBound="gvpstk_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcomname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="100px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Total Revenue">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtstkamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="UnSold Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsoldam")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sold Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldam")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receivable">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvatodues" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Not Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvptoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcurduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delay & Return Charge">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdlyadcharge1"))%>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
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
                        <asp:Label ID="lblProjectStatus" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="L. Project Status (Upto Date)" Visible="False" 
                            Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvProjectStatus01" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProjectStatus01_RowDataBound"
                            ShowFooter="True" Width="273px">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcomnameps1" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="100px"> 
                                            
                                          
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                              
                              
                                <asp:TemplateField HeaderText="Total Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtps1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetexpenses01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netexpamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Liabilities">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvliaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liaamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Net Position">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetloantamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netlnamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project Report With Quantity">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvprorptwqty" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="90px"> 
                                            
                                          
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Project Budget Vs. Expenses">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvprobgdvsexp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="90px"> 
                                            
                                          
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Project Transaction - Day Wise">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvprotrdaywise" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="90px"> 
                                            
                                          
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>  
                <tr>
                    <td>
                        <asp:Label ID="lblMonProStatus" runat="server" BackColor="#000066" BorderColor="Yellow"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                            Text="M.  Project Status(During the Period)" Visible="False" Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvmonprost" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmonprost_RowDataBound"
                            ShowFooter="True" Width="273px">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo14" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcomnamemps" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname1")) %>'
                                            Width="100px"> 
                                            
                                          
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcostmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Collection ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcollamtmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Position">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetpositionmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
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
                 
               <%-- <tr>
                    <td>
                        <asp:Label ID="lblProStatus02" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="Cost Details (N-1)" Visible="False" 
                            Width="300px"></asp:Label>
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
                       
                        <asp:GridView ID="gvProjectStatus02" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="273px" 
                            onrowdatabound="gvProjectStatus02_RowDataBound">
                            <PagerSettings Position="Top" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo12" runat="server" Font-Bold="True" Height="16px" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                               
                                <asp:TemplateField HeaderText="Construction + Admin + Selling Exp.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExpAmt" runat="server" Font-Size="11PX" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPAdvAmt" runat="server" Font-Size="11PX" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "padvamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Land Cost(Non-Refundable)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLCNFAmt" runat="server" Font-Size="11PX" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOvmt" runat="server" Font-Size="11PX" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Interest">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIAmt" runat="server" Font-Size="11PX" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankinamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtexamt" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "texpamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                   <asp:TemplateField HeaderText="Liabilities">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvliaamt" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liaamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Net Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetexpenses02" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netexpamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
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
                </tr>--%>
                
                
             
               
               
                <tr>
                    <td>
                        <asp:Label ID="lblInventorystock" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" 
                            Text="N. Inventory Status Report" Visible="False" Width="300px"></asp:Label>
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
                    <td>
                        <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInventory_RowDataBound"
                            ShowFooter="True" Width="341px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre6" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcomcodeinv" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCompanyinv" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="140px"> 
                                            
                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Issue Basis">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvissuebasisinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuebasis")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Progress Basis">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvprobasisinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "probasis")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Material Consumption">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvmatconinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matcon")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
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
                    <td>
                        <asp:Label ID="lblMarketing" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="O. Marketing Information" 
                            Visible="False" Width="300px"></asp:Label>
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
                    <td>
                        <asp:GridView ID="gvcomwclients" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvcomwclients_RowDataBound" ShowFooter="True" 
                            Width="234px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre11" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescription" runat="server" 
                                            Style="text-align: Left; background-color: Transparent;" 
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>' 
                                            Width="200px" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" 
                                            Font-Size="11px"> 
                                            
                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Demand Analysis">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvCliHis" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" Font-Size="11px" Font-Underline="false" 
                                            Style=" background-color: Transparent; text-align: right;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="70px" BorderWidth="0px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Decision">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvSalDem" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Font-Underline="false" 
                                            Style="text-align: right; background-color: Transparent" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="70px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Capacity Analysis">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvSalDec" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" Font-Size="11px" Font-Underline="false" 
                                            Style=" background-color: Transparent; text-align: right;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="70px" BorderWidth="0px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client History">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvCliCap" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" Font-Size="11px" Font-Underline="false" 
                                            Style=" background-color: Transparent; text-align: right;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="70px" BorderWidth="0px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Person History">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvSalPHis" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Font-Underline="false" 
                                            Style="text-align: right; background-color: Transparent" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="70px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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
                        <asp:Label ID="lblFixedAssets" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="P. Fixed Asset Status" 
                            Visible="False" Width="300px"></asp:Label>
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
                    <td>
                        <asp:GridView ID="gvFxtAssets" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFxtAssets_RowDataBound"
                            ShowFooter="True" Width="223px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre7" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcomcodefxt" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvCompanyfxt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="140px">
                                        </asp:HyperLink></ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclosamtfxt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
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
                    <td>
                        <asp:Label ID="lblFinancialst" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="Q. Finalcial Statement" Visible="False" 
                            Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvFinalcialst" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvFinalcialst_RowDataBound" ShowFooter="True" Width="341px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre8" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCompanyfin" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            Style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>' 
                                            Width="120px"> 
                                            
                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Income Statement">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvincomest" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText=" Balance Sheet">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvbalancesheet" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText=" Real Inflow & Outflow">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvinaoutflow" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Real Payment Summary">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvrealpsum" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Investment Plan">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvinplanfin" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                               
                                </asp:TemplateField>



                               

                                <asp:TemplateField HeaderText="Project Report-At a glance">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvpstaaglance" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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
                        <asp:Label ID="lblHrMgt" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="R. HR Management" Visible="False" 
                            Width="300px"></asp:Label>
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
                        <asp:GridView ID="gvHremp" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="37px" OnRowDataBound="gvHremp_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkgvcomname" runat="server"  BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))   %>'
                                                
                                            Width="140px"> 
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoemp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toemp")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetsalary" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerSettings Position="Top" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

