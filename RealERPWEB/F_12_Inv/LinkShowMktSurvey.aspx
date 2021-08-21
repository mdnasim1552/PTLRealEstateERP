<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkShowMktSurvey.aspx.cs" Inherits="RealERPWEB.F_12_Inv.LinkShowMktSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style11
        {
            width: 23px;
        }
        .style12
        {
            width: 185px;
        }
        .style13
        {
            width: 84px;
        }
        .style14
        {
            width: 64px;
        }
        .style15
        {
            width: 299px;
        }
        .style23
        {
            width: 521px;
        }
        .style24
        {
            width: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="UNIT FIXATION INFORMATION VIEW/EDIT" Width="686px" Style="border-bottom: 1px solid white;
                    border-top: 1px solid white;"></asp:Label>
            </td>
            <td>
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
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
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
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 1000px;">
                                <tr>
                                    <td class="style24">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        <asp:Label ID="lblMurketSurvey" runat="server" BackColor="#000066" BorderColor="Yellow"
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="style15" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Yellow" Height="16px" Style="text-align: left" Text="Market Survey"
                                            Width="200px"></asp:Label>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="lblsurveyby" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True"
                                            ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="style14">
                                        &nbsp;
                                    </td>
                                    <td class="style13">
                                        &nbsp;
                                    </td>
                                    <td class="style11">
                                        &nbsp;
                                    </td>
                                    <td class="style15">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style62">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                       <asp:GridView ID="gvMSRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="274px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Materials Description ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlMSRPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                        Font-Bold="True" Font-Size="14px" 
                                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                        Width="120px">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier Name">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Concern  Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRCperson" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRPhone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRMobile" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Purchase Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Credit Period (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Period (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRDel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delivery")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit Limit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPayLimit" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paylimit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvbrand" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Height="16px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                                        Width="35px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                    </td>
                </tr>
                <tr>
                                            <td class="style38">
                                                <asp:Label ID="lblReqNarr" runat="server" CssClass="style21" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Text=" Selected Vendor Justification:" 
                                                    Width="100px" ForeColor="White"></asp:Label>
                                            </td>
                                            <td class="style39" colspan="2">
                                                <asp:TextBox ID="txtMSRNarr" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" Width="400px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td class="style25">
                                                &nbsp;</td>
                                            <td class="style36">
                                                &nbsp;</td>
                                            <td class="style35">
                                                &nbsp;</td>
                                            <td class="style35">
                                            </td>
                                            <td class="style37">
                                                &nbsp;
                                            </td>
                                            <td class="style32">
                                                &nbsp;
                                            </td>
                                        </tr>
                <tr>
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
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
