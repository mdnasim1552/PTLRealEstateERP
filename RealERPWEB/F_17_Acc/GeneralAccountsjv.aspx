
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GeneralAccountsjv.aspx.cs" Inherits="RealERPWEB.F_17_Acc.GeneralAccountsjv" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<script language="JavaScript" src="JS/JScript.js" type="text/javascript"></script>--%>
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
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
            width: 71px;
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
            width: 97px;
        }
        .style202
        {
            width: 131px;
        }
        .style204
        {
            width: 46px;
        }
        .style205
        {
            width: 49px;
        }
        .style206
        {
            width: 9px;
        }
        </style>
   
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
   
   <script language="javascript" type="text/javascript">
       $(document).ready(function () {
           //For navigating using left and right arrow of the keyboard
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           $('#<%=this.txtScrchConCode.ClientID %>').focus();
         
       });
       function pageLoaded() {

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });
           var gridview = $('#<%=this.dgv1.ClientID %>');
           $.keynavigation(gridview);
       };
       
   </script>
  
 
        <table style="width:95%; height: 2px;">
            <tr>
                <td class="style186">
                    <asp:Label ID="lblGeneralAcc" runat="server" Text="general Accounts" 
                        CssClass="label" Width="622px" BackColor="Blue" ForeColor="#FFFF99"></asp:Label>
                </td>
                <td class="style18">
                                        <asp:Label ID="lblprint" runat="server"></asp:Label>
                </td>
                <td class="style19">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click" ForeColor="White" 
                        style="text-align: center; font-weight: 700; height: 16px;" Width="60px" 
                        Font-Size="12px">PRINT</asp:LinkButton>
                </td>
            </tr>
            </table>           
  
                    
                    
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                        <ContentTemplate>
                            <table style="border-color: #99CCFF; width:91%; height: 12px; border-top-style: groove;">
                                <tr>
                                    <td align="left" valign="top" style="text-align: center">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                            Text="Current Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtcurrentvou" runat="server" BorderStyle="None" 
                                            ReadOnly="True" TabIndex="4" Width="40px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" BorderStyle="None" 
                                            Enabled="False" ReadOnly="True" TabIndex="5" 
                                            ToolTip="You Can Change Voucher Number." Width="45px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Text="Voucher Date" Width="75px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtEntryDate" runat="server" BorderStyle="None" 
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate0_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="11px" Font-Underline="False" Height="16px" 
                                            Width="90px" onclick="lnkPrivVou_Click">Prev.Voucher</asp:LinkButton>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtScrchPre" runat="server" BorderStyle="None" TabIndex="1" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:ImageButton ID="ibtnFindPrv" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindPrv_Click" TabIndex="2" />
                                    </td>
                                    <td class="style8" align="left" valign="top">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" 
                                            Height="19px" Width="260px" TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style191" valign="top">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            onclick="lnkOk_Click"   Width="90px" Font-Size="12px" TabIndex="9">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="left" class="style196" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" Text="Control Accounts" 
                                            Width="95px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtScrchConCode" runat="server" BorderStyle="None" 
                                            TabIndex="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:ImageButton ID="ibtnFindConCode" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindConCode_Click" 
                                            TabIndex="7" style="width: 18px" />
                                    </td>
                                    <td align="left" valign="top" colspan="4">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" 
                                            onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="467px" 
                                            TabIndex="8">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style191" align="left" valign="top">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style196" valign="top">
                                        &nbsp;</td>
                                    <td align="left" style="width: 0" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:CheckBox ID="chkPrint" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: left; margin-left: 0px;" 
                                            Text="Cheque Print" Visible="False" Width="95px" TabIndex="10" 
                                            />
                                    </td>
                                    <td class="style195" align="right" valign="top" colspan="6">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style191">
                                        &nbsp;</td>
                                    <td>
                                        </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel2" runat="server" 
                                BorderColor="#CC0099" BorderStyle="Ridge" 
                                            BorderWidth="2px" Visible="False" Width="897px">
                                            <table style="width:92%; height: 13px;">
                                                <tr>
                                                    <td align="left" class="style24" valign="top">
                                                        <asp:TextBox ID="txtserceacc" runat="server" Width="70px" 
                                                            TabIndex="11" BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                    <td align="left" class="style25" valign="top">
                                                        <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="button" 
                                                            onclick="lnkAcccode_Click" Width="120px" Font-Size="12px" TabIndex="12">Head of Account</asp:LinkButton>
                                                    </td>
                                                    <td align="left" class="style119" valign="top" colspan="3">
                                                        <asp:DropDownList ID="ddlacccode" runat="server" AutoPostBack="True" onselectedindexchanged="ddlacccode_SelectedIndexChanged" 
                                                            Width="440px" TabIndex="13">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlacccode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlacccode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lblDramt" runat="server" CssClass="label2" Text="Dr. Amount" 
                                                            Width="71px" ForeColor="White"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style129" valign="top">
                                                        <asp:TextBox ID="txtDrAmt"  runat="server" Width="90px" BorderStyle="None" 
                                                            TabIndex="20"></asp:TextBox>
                                                    </td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style24">
                                                        <asp:TextBox ID="txtserchReCode" runat="server" Visible="False" 
                                                            Width="70px" TabIndex="14" BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                    <td class="style25">
                                                        <asp:LinkButton ID="lnkRescode" runat="server" CssClass="button" 
                                                            onclick="lnkRescode_Click" Visible="False" Width="120px" Font-Size="12px" 
                                                            TabIndex="15">Sub of Account</asp:LinkButton>
                                                    </td>
                                                    <td class="style119" colspan="3">
                                                        <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" onselectedindexchanged="ddlresuorcecode_SelectedIndexChanged" 
                                                            Visible="False" Width="440px" TabIndex="16">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlresuorcecode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlresuorcecode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCramt" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Cr. Amount" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style129">
                                                        <asp:TextBox ID="txtCrAmt" runat="server" Width="90px" BorderStyle="None" 
                                                            TabIndex="22"></asp:TextBox>
                                                    </td>
                                                    <td class="style200">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style24">
                                                        <asp:TextBox ID="txtSearchSpeci" runat="server" Visible="False" 
                                                            Width="70px" TabIndex="17" BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                    <td class="style25">
                                                        <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="button" 
                                                            onclick="lnkSpecification_Click" Visible="False" Width="120px" 
                                                            Font-Size="12px" TabIndex="18">Specification</asp:LinkButton>
                                                    </td>
                                                    <td class="style119">
                                                        <asp:DropDownList ID="ddlSpclinf" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSpclinf_SelectedIndexChanged" 
                                                            Visible="False" Width="220px" TabIndex="19">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style202">
                                                        <asp:Label ID="lblrate" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Rate" Visible="False" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style119">
                                                        <asp:TextBox ID="txtrate" runat="server" Visible="False" 
                                                            ReadOnly="True" Width="80px" BorderStyle="None" TabIndex="29"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblqty" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Quantity" Visible="False" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style129">
                                                        <asp:TextBox ID="txtqty" runat="server" 
                                                            Visible="False" Width="90px" BorderStyle="None" TabIndex="30"></asp:TextBox>
                                                    </td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style24">
                                                        &nbsp;</td>
                                                    <td class="style25">
                                                        <asp:Label ID="lblremarks" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Remarks" Width="81px" Height="17px"></asp:Label>
                                                    </td>
                                                    <td class="style133">
                                                        <asp:TextBox ID="txtremarks" runat="server" Width="220px" BorderStyle="None" 
                                                            TabIndex="24"></asp:TextBox>
                                                    </td>
                                                    <td class="style202">
                                                        &nbsp;</td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style135">
                                                        <asp:LinkButton ID="lnkOk0" runat="server" CssClass="button" Font-Bold="True" 
                                                            onclick="lnkOk0_Click" Width="78px" Font-Size="12px" TabIndex="21">Add Table</asp:LinkButton>
                                                    </td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style24">
                                                        <asp:TextBox ID="txtserchBill" runat="server" BorderStyle="None" TabIndex="26" 
                                                            Visible="False" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td class="style25">
                                                        <asp:LinkButton ID="lnkBillNo" runat="server" CssClass="button" 
                                                            Font-Size="12px" onclick="lnkBillNo_Click" TabIndex="27" Visible="False" 
                                                            Width="120px">Bill No</asp:LinkButton>
                                                    </td>
                                                    <td class="style133">
                                                        <asp:DropDownList ID="ddlBillList" runat="server" style="margin-left: 2px" 
                                                            TabIndex="28" Visible="False" Width="220px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style202">
                                                        &nbsp;</td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style135">
                                                        &nbsp;</td>
                                                    <td class="style200">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                               <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" style="text-align: left" Width="685px">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid" runat="server" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccCod" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResCod" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpclCod" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head of Accounts">
                                                   <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" 
                                                            onclick="lnkTotal_Click" style="color: #FFFFFF">Total :</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblAccdesc1" runat="server" 
                                                                        Font-Size="11px" 
                                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>' 
                                                                        Width="400px" Font-Names="Verdana"></asp:Label>
                                                                                                                             
                                                        <asp:Label ID="lblAccdesc" runat="server" 
                                                            Font-Size="11px"  Visible="False"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                            Width="50px" Font-Names="Verdana"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Details Description" Visible="False">
                                                 <ItemTemplate>
                                                   <asp:Label ID="lblResdesc" runat="server" 
                                                                        Font-Size="10px" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>' 
                                                                        Width="300px"></asp:Label>
                                                 </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpcldesc" runat="server" 
                                                            Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>' 
                                                            Width="80px" TabIndex="78"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            Visible="False" Width="60px" Font-Size="12px" style="text-align: right"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                            
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="60px" Font-Size="12px" ForeColor="Black" style="text-align: right" 
                                                            TabIndex="79"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle  HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Visible="False" Width="80px" Font-Size="12px" ReadOnly="True" style="text-align: right"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black" 
                                                            style="text-align: right" TabIndex="80"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr.Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" 
                                                            
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="90px" Font-Size="12px" ForeColor="Black" style="text-align: right" 
                                                            TabIndex="81"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                                            Width="90px" ForeColor="White" style="text-align: right"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <FooterStyle ForeColor="White" HorizontalAlign="right"/>
                                                    <ItemStyle  HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr.Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="90px" Font-Size="12px" ForeColor="Black" style="text-align: right" 
                                                            TabIndex="82"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                             Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                                            Width="90px" ForeColor="White" style="text-align: right"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <FooterStyle ForeColor="White" />
                                                    <ItemStyle  HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                             Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                            Width="80px" ForeColor="Black" TabIndex="83"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Reconcilation" Visible="false">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrecndat" runat="server" 
                                                            Font-Size="12px" 
                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>' 
                                                            Width="80px" TabIndex="78"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="RpCode" Visible="false">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrpcode" runat="server" 
                                                            Font-Size="12px" 
                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>' 
                                                            Width="80px" TabIndex="60"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill No">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                                            Width="100px" ForeColor="Black" TabIndex="99"></asp:Label>
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
                                 <asp:Panel ID="Panel4" runat="server" BorderColor="#996633" BorderStyle="Solid" 
                                            BorderWidth="2px" Visible="False" Width="896px">
                                            <table style="width:99%;">
                                                <tr>
                                                    <td class="style204">
                                                        <asp:Label ID="lblRefNum" runat="server" CssClass="label2" 
                                                            Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style161">
                                                        <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="120px" TabIndex="29"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" 
                                                            Text="Other ref. (if any)" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style198">
                                                        <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="120px" TabIndex="30"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPayto" runat="server" CssClass="label2" Text="Pay To" 
                                                            Width="70px"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style117">
                                                        <asp:TextBox ID="txtPayto" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="160px" TabIndex="31"></asp:TextBox>
                                                    </td>
                                                    <td class="style197">
                                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" 
                                                            Font-Bold="True" Font-Size="12px" onclick="lnkFinalUpdate_Click" TabIndex="33" 
                                                            Width="90px">Final Update</asp:LinkButton>
                                                    </td>
                                                    <td class="style199">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td  valign="top" style="text-align: right" class="style204">
                                                        <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style159" colspan="5">
                                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Height="42px" TextMode="MultiLine" Width="640px" 
                                                            TabIndex="32" MaxLength="500"></asp:TextBox>
                                                    </td>
                                                    <td class="style199">
                                                        <asp:Label ID="lblisunum" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style204">
                                                        &nbsp;</td>
                                                    <td class="style161">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style198">
                                                        &nbsp;</td>
                                                    <td class="style117" colspan="2">
                                                        &nbsp;</td>
                                                    <td class="style197">
                                                        &nbsp;</td>
                                                    <td class="style199">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
</asp:Content>


