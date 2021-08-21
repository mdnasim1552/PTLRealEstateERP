<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPaymntPro.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPaymntPro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style18
        {
            width: 102px;
        }
        .style19
        {
            width: 29px;
        }
        .style24
        {
            width: 30px;
        }
        .style25
        {
            width: 55px;
        }
        .style135
        {
            text-align: center;
        }
        .style191
        {
            width: 4px;
        }
        .style195
        {
            text-align: left;
        }
        .style196
        {
            width: 76px;
        }
        .style197
        {
            width: 194px;
        }
        .style198
        {
            width: 134px;
        }
        .style199
        {
            width: 576px;
        }
        .style200
        {
            width: 7px;
        }
        .style201
        {
            width: 21px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 95%; height: 2px;">
        <tr>
            <td class="style186">
                <asp:Label ID="lblGeneralAcc" runat="server" Text="payment proposal" CssClass="label"
                    Width="622px" BackColor="Blue" ForeColor="#FFFF99"></asp:Label>
            </td>
            <td class="style18">
                <asp:Label ID="lblprint" runat="server"></asp:Label>
            </td>
            <td class="style19">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" OnClick="lnkPrint_Click"
                    ForeColor="White" Style="text-align: center; font-weight: 700" Width="60px" Font-Size="12px">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
   
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border-color: #99CCFF; width: 91%; height: 12px; border-top-style: groove;">
                <tr>
                    <td align="left" valign="top" colspan="3" style="text-align: center">
                        <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" ImageUrl="~/Image/movie_26.gif"
                            OnClick="ibtnvounu_Click" Width="145px" Visible="False" />
                    </td>
                    <td align="left" style="width: 93px" valign="top" width="280px">
                        &nbsp;
                    </td>
                    <td align="right" valign="top">
                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" ForeColor="White" Text="Proposal Date"
                            Width="100px" Font-Bold="True" Font-Size="12px"></asp:Label>
                    </td>
                    <td align="left" valign="top" class="style201">
                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="ddl" Width="97px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy ddd" TargetControlID="txtEntryDate">
                        </cc1:CalendarExtender>
                        <br />
                    </td>
                    <td align="left" class="style8" valign="top">
                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" Font-Bold="True"
                            Font-Size="11px" Height="16px" OnClick="lnkPrivVou_Click" Width="99px">Prev.Proposal</asp:LinkButton>
                    </td>
                    <td class="style200" align="left" valign="top">
                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" CssClass="ddl"
                            Height="19px" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="style191" valign="top">
                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" Font-Size="12px"
                            OnClick="lnkOk_Click" Width="90px">Ok</asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr valign="top">
                    <td align="left" class="style196" valign="top">
                        <asp:Label ID="lbllstVouno" runat="server" CssClass="label2" Text="Last Proposal No."
                            Width="120px"></asp:Label>
                    </td>
                    <td align="left" colspan="2" valign="top">
                        <asp:TextBox ID="txtLastVou" runat="server" BackColor="Aqua" CssClass="ddl" ReadOnly="True"
                            Width="90px"></asp:TextBox>
                    </td>
                    <td align="left" style="width: 93px" valign="top" width="280px">
                        &nbsp;
                    </td>
                    <td align="right" valign="top">
                        &nbsp;
                    </td>
                    <td align="left" valign="top" colspan="3">
                        &nbsp;
                    </td>
                    <td class="style191" align="left" valign="top">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style196" valign="top">
                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" Text="Current Proposal No."
                            Width="120px"></asp:Label>
                    </td>
                    <td align="left" style="width: 0" valign="top" width="280px">
                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="ddl" ReadOnly="True" Width="40px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="ddl" ToolTip="You Can Change Voucher Number."
                            Width="40px" Enabled="False"></asp:TextBox>
                    </td>
                    <td align="left" style="width: 93px" valign="top" width="280px">
                        &nbsp;
                    </td>
                    <td class="style195" align="right" valign="top" colspan="4">
                        <asp:Label ID="lblmsg" runat="server" CssClass="label3" Width="500px" Font-Names="Verdana"
                            Font-Size="12px" Height="16px"></asp:Label>
                    </td>
                    <td class="style191">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" BorderColor="#CC0099" BorderStyle="Ridge" BorderWidth="2px"
                Visible="False" Width="897px">
                <table style="width: 92%; height: 13px;">
                    <tr>
                        <td align="left" class="style24" valign="top">
                            <asp:TextBox ID="txtserceacc" runat="server" CssClass="ddl" Width="70px"></asp:TextBox>
                        </td>
                        <td align="left" class="style25" valign="top">
                            <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="button" OnClick="lnkAcccode_Click"
                                Width="120px" Font-Size="12px">Head of Account</asp:LinkButton>
                        </td>
                        <td align="left" class="style119" valign="top" colspan="3">
                            <asp:DropDownList ID="ddlacccode" runat="server" AutoPostBack="True" CssClass="ddl"
                                OnSelectedIndexChanged="ddlacccode_SelectedIndexChanged" Width="440px">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ddlacccode_ListSearchExtender" runat="server" Enabled="True"
                                QueryPattern="Contains" TargetControlID="ddlacccode">
                            </cc1:ListSearchExtender>
                        </td>
                        <td align="left" class="style122" valign="top">
                            <asp:Label ID="lblDramt" runat="server" CssClass="label2" Text="Dr. Amount" Width="71px"
                                ForeColor="White"></asp:Label>
                        </td>
                        <td align="left" class="style129" valign="top">
                            <asp:TextBox ID="txtDrAmt" runat="server" CssClass="ddl" Width="80px" Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            <asp:TextBox ID="txtserchReCode" runat="server" CssClass="ddl" Visible="False" Width="70px"></asp:TextBox>
                        </td>
                        <td class="style25">
                            <asp:LinkButton ID="lnkRescode" runat="server" CssClass="button" OnClick="lnkRescode_Click"
                                Visible="False" Width="120px" Font-Size="12px">Sub of Account</asp:LinkButton>
                        </td>
                        <td class="style119" colspan="3">
                            <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" CssClass="ddl"
                                OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged" Visible="False"
                                Width="440px">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ddlresuorcecode_ListSearchExtender" runat="server" Enabled="True"
                                QueryPattern="Contains" TargetControlID="ddlresuorcecode">
                            </cc1:ListSearchExtender>
                        </td>
                        <td class="style122">
                            &nbsp;
                        </td>
                        <td class="style129">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            <asp:TextBox ID="txtSearchSpeci" runat="server" CssClass="ddl" Visible="False" Width="70px"></asp:TextBox>
                        </td>
                        <td class="style25">
                            <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="button" OnClick="lnkSpecification_Click"
                                Visible="False" Width="120px" Font-Size="12px">Specification</asp:LinkButton>
                        </td>
                        <td class="style119">
                            <asp:DropDownList ID="ddlSpclinf" runat="server" AutoPostBack="True" CssClass="ddl"
                                OnSelectedIndexChanged="ddlSpclinf_SelectedIndexChanged" Visible="False" Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td class="style119">
                            <asp:Label ID="lblrate" runat="server" CssClass="label2" ForeColor="White" Text="Rate"
                                Visible="False" Width="71px"></asp:Label>
                        </td>
                        <td class="style119">
                            <asp:TextBox ID="txtrate" runat="server" CssClass="ddl" Visible="False" ReadOnly="True"
                                Width="80px" Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td class="style122">
                            <asp:Label ID="lblqty" runat="server" CssClass="label2" ForeColor="White" Text="Quantity"
                                Visible="False" Width="71px"></asp:Label>
                        </td>
                        <td class="style129">
                            <asp:TextBox ID="txtqty" runat="server" CssClass="ddl" Visible="False" Width="80px"
                                Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <asp:Label ID="lblremarks" runat="server" CssClass="label2" ForeColor="White" Text="Remarks"
                                Width="81px" Height="17px"></asp:Label>
                        </td>
                        <td class="style133">
                            <asp:TextBox ID="txtremarks" runat="server" CssClass="ddl" Width="220px"></asp:TextBox>
                        </td>
                        <td class="style133">
                            &nbsp;
                        </td>
                        <td class="style133">
                            &nbsp;
                        </td>
                        <td class="style134">
                            &nbsp;
                        </td>
                        <td class="style135">
                            <asp:LinkButton ID="lnkOk0" runat="server" CssClass="button" Font-Bold="True" OnClick="lnkOk0_Click"
                                Width="78px" Font-Size="12px">Add Table</asp:LinkButton>
                        </td>
                        <td class="style136">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" BackColor="#99CCFF"
                BorderColor="#7FBF41" Height="16px" ShowFooter="True" Width="674px">
                <RowStyle BackColor="#99CCFF" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Head of Accounts">
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" OnClick="lnkTotal_Click"
                                Style="color: #FFFFFF">Total :</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL" Font-Size="11px"
                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                Width="400px" Font-Names="Verdana"></asp:Label>
                            <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL" Font-Size="11px"
                                Visible="False" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                Width="50px" Font-Names="Verdana"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details Description" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                Width="300px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specification" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Visible="False" Width="60px"
                                Font-Size="12px" ReadOnly="True"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="60px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Visible="False" Width="80px" Font-Size="12px" ReadOnly="True"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dr.Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="90px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Bold="True"
                                Font-Size="12px" ReadOnly="True" Width="90px" ForeColor="White"></asp:TextBox>
                        </FooterTemplate>
                        <FooterStyle ForeColor="White" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid To">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvpaidto" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paidto")) %>'
                                Width="120px" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purpose">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvpurpose" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purpose")) %>'
                                Width="120px" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                Width="80px" ForeColor="Black"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#333300" />
                <HeaderStyle BackColor="#006699" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True"
                    ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFCCFF" />
            </asp:GridView>
            <asp:Panel ID="Panel4" runat="server" BorderColor="#996633" BorderStyle="Solid" BorderWidth="2px"
                Visible="False" Width="896px">
                <table style="width: 99%;">
                    <tr>
                        <td class="style158">
                            <asp:Label ID="lblRefNum" runat="server" CssClass="label2" Text="Ref./Cheq No/Slip No."
                                Width="120px"></asp:Label>
                        </td>
                        <td class="style161">
                            <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" CssClass="ddl"
                                Width="166px"></asp:TextBox>
                        </td>
                        <td class="style198">
                            &nbsp;
                        </td>
                        <td class="style198">
                            <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" Text="Other ref. (if any)"
                                Width="100px"></asp:Label>
                        </td>
                        <td class="style117">
                            <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" CssClass="ddl"
                                Width="240px"></asp:TextBox>
                        </td>
                        <td class="style197">
                            &nbsp;
                        </td>
                        <td class="style199">
                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" Font-Bold="True"
                                Font-Size="12px" OnClick="lnkFinalUpdate_Click" Width="90px">Final Update</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: right">
                            <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" Width="100px"></asp:Label>
                        </td>
                        <td class="style159" colspan="4">
                            <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" CssClass="ddl"
                                Height="42px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                        </td>
                        <td class="style199">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style158">
                            &nbsp;
                        </td>
                        <td class="style161">
                            &nbsp;
                        </td>
                        <td class="style198">
                            &nbsp;
                        </td>
                        <td class="style198">
                            &nbsp;
                        </td>
                        <td class="style117">
                            &nbsp;
                        </td>
                        <td class="style197">
                            &nbsp;
                        </td>
                        <td class="style199">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
