<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSales01.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSales01" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
<link href="CSS/HeaderLebel.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style18
        {
            text-align: center;
        }
        .style19
        {
        }
        .style8
        {
            text-align: left;
        }
        .style20
        {
            width: 591px;
        }
        .style188
        {
            text-align: left;
        }
        .style196
        {
            width: 168px;
        }
        .style197
        {
            text-align: left;
            width: 305px;
        }
        .style198
        {
            text-align: center;
        }
        .style202
        {
            width: 41px;
        }
        .style203
        {
            width: 132px;
        }
        .style204
        {
            width: 57px;
        }
        .style205
        {
            width: 59px;
        }
        .style207
        {
            width: 64px;
        }
        .style208
        {
            width: 50px;
        }
        .style210
        {
            width: 87px;
        }
        .style211
        {
        }
        .style212
        {
            width: 45px;
        }
        .style213
        {
            width: 55px;
        }
        .style214
        {
            width: 77px;
        }
        .style215
        {
            width: 66px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <table style="width:95%; height: 2px;" >
            <tr>
                <td class="style20">
                    <asp:Label ID="lblGeneralAcc" runat="server" Text="sales account information view/edit" 
                        CssClass="label" Width="476px"></asp:Label>
                </td>
                <td class="style203" >
                                        <asp:Label ID="lblprint" runat="server"></asp:Label>
                </td>
                <td class="style202" >
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="style202" >
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click" ForeColor="White" 
                        style="text-align: center; font-weight: 700; " Width="60px" 
                        Font-Size="12px">PRINT</asp:LinkButton></td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            
                    
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
                            <table style="border-color: #99CCFF; width:64%; height: 20px; border-top-style: groove;">
                                <tr>
                                    <td align="right" class="style18" valign="top" colspan="3">
                                        <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" 
                                            ImageUrl="~/Image/movie_26.gif" onclick="ibtnvounu_Click" Width="145px" />
                                    </td>
                                    <td align="right" class="style19" valign="top" colspan="3">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" 
                                            Text="Voucher Date" Width="100px"></asp:Label>
                                    </td>
                                    <td align="left" class="style187" valign="top">
                                        <asp:TextBox ID="txtEntryDate" runat="server" AutoPostBack="True" 
                                            BorderStyle="None" Width="97px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy (ddd)" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="11px" Height="16px" onclick="lnkPrivVou_Click" 
                                            Width="93px">Prev.Voucher</asp:LinkButton>
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" 
                                            Height="19px" Width="185px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style197" valign="top">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            onclick="lnkOk_Click" Width="84px">Ok</asp:LinkButton>
                                    </td>
                                    <td align="center" class="style8" valign="top">
                                        &nbsp;</td>
                                    <td class="style196">
                                        &nbsp;</td>
                                    <td class="style68">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style18" align="right" valign="top">
                                        <asp:Label ID="lblcurVounum0" runat="server" CssClass="label2" 
                                            Text="Current Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="right" class="style188" valign="top">
                                        <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="True" 
                                            BorderStyle="None" Height="18px" ontextchanged="txtEntryDate_TextChanged" 
                                            ReadOnly="True" Width="62px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style188" valign="top">
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True" 
                                            BorderStyle="None" Height="18px" ontextchanged="txtEntryDate_TextChanged" 
                                            ToolTip="You Can Change Voucher Number." Width="44px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style19" valign="top">
                                        <asp:TextBox ID="txtScrchConCode" runat="server" BorderStyle="None" 
                                            Width="40px" Height="18px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style19" valign="top">
                                        <asp:ImageButton ID="ibtnFindConCode" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindConCode_Click" />
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Text="Control Accounts" Width="100px"></asp:Label>
                                    </td>
                                    <td align="right" class="style188" colspan="3" valign="top">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" AutoPostBack="True" Height="19px" 
                                            onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="390px">
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td align="left" class="style197" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style8">
                                        &nbsp;</td>
                                    <td class="style196">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style18" align="right" valign="top">
                                        &nbsp;</td>
                                    <td align="right" class="style188" valign="top">
                                        &nbsp;</td>
                                    <td align="right" class="style188" valign="top">
                                        &nbsp;</td>
                                    <td align="left" class="style19" valign="top" colspan="3">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" CssClass="label3" 
                                             Font-Size="12px" ForeColor="White" Height="16px"></asp:Label>
                                    </td>
                                    <td align="right" class="style188" valign="top">
                                        &nbsp;</td>
                                    <td align="left" class="style8" valign="top">
                                        &nbsp;</td>
                                    <td align="left" class="style8" valign="top">
                                        &nbsp;</td>
                                    <td align="left" class="style197" valign="top">
                                        &nbsp;</td>
                                    <td class="style8" align="left" valign="top">
                                        &nbsp;</td>
                                    <td class="style196">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style18" colspan="13" valign="top">
                                        <asp:Panel ID="PnlMonDuePriod" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="style204">
                                                        &nbsp;</td>
                                                    <td class="style215">
                                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="label2" Text="From :" 
                                                            Width="50px"></asp:Label>
                                                    </td>
                                                    <td class="style205">
                                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoPostBack="True" 
                                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="97px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" 
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style214">
                                                        <asp:Label ID="lbltodate" runat="server" CssClass="label2" Height="16px" 
                                                            Text="To:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style207">
                                                        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" 
                                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="97px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style208">
                                                        <asp:LinkButton ID="lnkOk0" runat="server" CssClass="button" Font-Bold="True" 
                                                            onclick="lnkOk0_Click" Width="78px">Ok</asp:LinkButton>
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
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table style="border-color: #99CCFF; width:94%; ">
                                <tr>
                                    <td class="style22">
                                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" 
                                            ShowFooter="True" style="text-align: left" Width="937px">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkvmrno" runat="server" 
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="1" %>' 
                                                            Height="16px" Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAccCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUcode" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc. Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) %>' 
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcPactdesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Description">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnVouOk" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" onclick="lbtnVouOk_Click">Select Item</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUdesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Ref.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrmrks" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MR. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmrno" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="qty" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBaName" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCheNo" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCheDate" runat="server" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Cr. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcramt" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvdramt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Acc . Desc1" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcPactdesc1r" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) %>' 
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
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
                                    <td class="style34">
                                        <asp:GridView ID="dgvVou" runat="server" AutoGenerateColumns="False" 
                                            ShowFooter="True" style="text-align: left" Width="701px">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAccCod1" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUcode1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc. Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcPactdesc1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Description">
                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUdesc1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="qty" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty1" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Cr. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcramt1" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvdramt1" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" Font-Bold="True" 
                                                            Font-Size="12px" ForeColor="White" onclick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrmrks1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>' 
                                                            Width="80px"></asp:Label>
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
                                <tr>
                                    <td class="style198">
                                        <asp:Panel ID="PnlRmrks" runat="server" BorderColor="Maroon" 
                                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style210">
                                                        <asp:Label ID="lblRefNum" runat="server" CssClass="label2" 
                                                            Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style214">
                                                        <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" 
                                                            BorderStyle="None" Width="166px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style212">
                                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" 
                                                            Text="Other ref.(if any)" Width="101px"></asp:Label>
                                                    </td>
                                                    <td class="style213">
                                                        <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="257px"></asp:TextBox>
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
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style210">
                                                        <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style211" colspan="4">
                                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" 
                                                            BorderStyle="None" Height="42px" TextMode="MultiLine" Width="400px"></asp:TextBox>
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
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
   
</asp:Content>


