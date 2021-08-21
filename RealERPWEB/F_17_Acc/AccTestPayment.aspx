
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccTestPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccTestPayment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 512px;
        }
        .style6
        {
            width: 111px;
        }
        .style7
        {
            width: 37px;
        }
        .style8
        {
            width: 127px;
        }
        .style10
        {
            width: 58px;
        }
        .style14
        {
        }
        .style17
        {
            width: 5px;
        }
        .style18
        {
            width: 94px;
        }
        .style25
        {
            width: 132px;
            height: 26px;
        }
        .style26
        {
        }
        .style27
        {
            width: 256px;
        }
        .style28
        {
            width: 18px;
        }
        
          .AutoExtender
       {
            font-family: Verdana, Helvetica, sans-serif;
            margin:0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
          
            background-color: White;
           
           
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        
        .style29
        {
            height: 26px;
        }
        
        .style31
        {
            width: 55px;
        }
        .style32
        {
            width: 265px;
        }
        .style33
        {
            width: 1px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#<%=this.txtScrchConCode.ClientID %>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                var ter = k1.textBoxHandler(event);







            });

            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);


            $(document).keyup(function (e) {

                var key = e.keyCode;
                if (key == 112) {
                        //key F1
                    $('#<%=this.txtScrchConCode.ClientID%>').focus();
                }
                else if (key == 113) {
                    //key F2
                    $('#<%=this.txtserceacc.ClientID%>').focus();

                }

                else if (key == 115) {
                    //key F4

                    $('#<%=this.txtserchReCode.ClientID%>').focus();
                }
                else if (key == 119) {
                    //key F8
                    $('#<%=this.lnkFinalUpdate.ClientID%>').focus();
                  

                }

                else if (key == 120) {
                  //key F9
                    $('#<%=this.lnkOk.ClientID%>').focus();

                }

               

            });

           
           
            
        }

       
    </script>


 <table style="width:95%; height: 2px;">
            <tr>
                <td class="style5">
                    <asp:Label runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Post Dated Cheque" Width="590px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    ID="lblGeneralAcc" ></asp:Label>
                    
                </td>
                <td class="style7">
                                        <asp:Label ID="lblprint" runat="server"></asp:Label>
                </td>
                <td class="style6">
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
                        style="text-align: center; font-weight: 700; height: 15px;" Width="60px" 
                        Font-Size="12px" TabIndex="23">PRINT</asp:LinkButton>
                </td>
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
                            <table style="border-color: #99CCFF; width:91%; height: 12px; border-top-style: groove;">
                               
                                <tr>
                                    <td align="left" valign="top" style="text-align: center">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                            Text="Current Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtcurrentvou" runat="server" BorderStyle="None" 
                                            ReadOnly="True" TabIndex="43" Width="40px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" BorderStyle="None" 
                                            Enabled="False" TabIndex="44" ToolTip="You Can Change Voucher Number." 
                                            Width="45px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Text="Voucher Date" Width="75px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtEntryDate" runat="server" BorderStyle="None" 
                                            Width="80px" AutoPostBack="True" ontextchanged="txtEntryDate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtEntryDate" PopupButtonID="Image2" >
                                        </cc1:CalendarExtender> 
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Image ID="Image2" runat="server" Height="16" 
                                            ImageUrl="~/Image/calender.png" Width="16" />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="11px" Font-Underline="False" Height="16px" 
                                            Width="90px" TabIndex="1">Prev.Voucher</asp:LinkButton>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtScrchPre" runat="server" BorderStyle="None" TabIndex="2" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:ImageButton ID="ibtnFindPrv" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindPrv_Click" 
                                            TabIndex="3" />
                                    </td>
                                    <td class="style8" align="left" valign="top">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" 
                                            Height="19px" Width="260px" TabIndex="4">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style191" valign="top">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            onclick="lnkOk_Click"   Width="90px" Font-Size="12px" TabIndex="8"  
                                            Text="Ok"></asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="left" class="style196" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow">Search</asp:LinkButton>
                                    </td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" Text="Control Accounts" 
                                            Width="95px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtScrchConCode" runat="server" BorderStyle="None" 
                                            TabIndex="5" Width="80px" Placeholder=" Press F1"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:ImageButton ID="ibtnFindConCode" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindConCode_Click" 
                                            TabIndex="6" />
                                    </td>
                                    <td align="left" valign="top" colspan="4">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" 
                                            onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="467px" 
                                            TabIndex="7">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style191" align="left" valign="top">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style196" valign="top">
                                       <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                        <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 0" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                      
                                        <asp:CheckBox ID="chkPettyCash" runat="server" AutoPostBack="True" 
                                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                            oncheckedchanged="chkPrint_CheckedChanged" 
                                            style="text-align: left; margin-left: 0px;" TabIndex="56" Text="Single Issue" 
                                            Width="120px" />
                                      
                                    </td>
                                    <td class="style195" align="right" valign="top" colspan="6">
                                        
                                         <asp:Panel ID="PanelChk" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="style25">
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True" 
                                                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                            Font-Bold="True"  Font-Size="12px" 
                                                            ForeColor="White" oncheckedchanged="chkPrint_CheckedChanged" 
                                                            style="text-align: left; margin-left: 0px;" Text="Cheque Print" 
                                                            Width="120px" TabIndex="55" />
                                                    </td>
                                                    <td class="style29">
                                                        <asp:DropDownList ID="ddlChqList" runat="server" Height="19px" TabIndex="56" 
                                                            Visible="False" Width="265px" style="margin-left: 2px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
                                                    <td align="left" class="style10" valign="top">
                                                        <asp:TextBox ID="txtserceacc" runat="server" Width="70px" BorderStyle="None" 
                                                            TabIndex="9" Placeholder=" Press F2"></asp:TextBox>
                                                    </td>
                                                    <td align="left" class="style14" valign="top">
                                                        <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="button" 
                                                            onclick="lnkAcccode_Click" Width="115px" Font-Size="12px" TabIndex="10">Head of Account</asp:LinkButton>
                                                    </td>
                                                    <td align="left" class="style119" valign="top" colspan="17">
                                                        <asp:DropDownList ID="ddlacccode" runat="server" AutoPostBack="True" onselectedindexchanged="ddlacccode_SelectedIndexChanged" 
                                                            Width="540px" TabIndex="11">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlacccode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlacccode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        &nbsp;</td>
                                                    <td align="left" class="style129" valign="top">
                                                        &nbsp;</td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style10">
                                                        <asp:TextBox ID="txtserchReCode" runat="server" 
                                                            Width="70px" BorderStyle="None" TabIndex="12" Placeholder=" Press F4"></asp:TextBox>
                                                    </td>
                                                    <td class="style14">
                                                        <asp:LinkButton ID="lnkRescode" runat="server" CssClass="button" 
                                                            onclick="lnkRescode_Click" Width="115px" Font-Size="12px" TabIndex="13">Sub of Account</asp:LinkButton>
                                                    </td>
                                                    <td class="style119" colspan="17">
                                                        <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlresuorcecode_SelectedIndexChanged" 
                                                            Width="540px" TabIndex="14">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlresuorcecode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlresuorcecode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style129">
                                                        &nbsp;</td>
                                                    <td class="style200">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style10">
                                                        &nbsp;</td>
                                                    <td class="style14">
                                                        <asp:Label ID="lblDramt" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Amount:" Width="80px" ></asp:Label>
                                                    </td>
                                                    <td class="style28">
                                                        <asp:TextBox ID="txtDrAmt" runat="server" BorderStyle="None" Width="90px" 
                                                            TabIndex="15" Placeholder=" Press F8"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="label2" ForeColor="White" 
                                                            Text="Cheque No.:" Width="70px"></asp:Label>
                                                    </td>
                                                    <td class="style27">
                                                        <asp:TextBox ID="txtChequeNo" runat="server" BorderStyle="None" Width="90px" 
                                                            TabIndex="16" style="margin-left: 0px"></asp:TextBox>
                                                    </td>
                                                    <td class="style17">
                                                        <asp:Label ID="lblChequeDate" runat="server" CssClass="label2" 
                                                            ForeColor="White" Text="Date:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtChequeDate" runat="server" BorderStyle="None" Width="80px" 
                                                            TabIndex="17" AutoPostBack="True" 
                                                            ontextchanged="txtChequeDate_TextChanged"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server" 
                                                            Enabled="True" TargetControlID="txtChequeDate" PopupButtonID="Image3" 
                                                            Format="dd.MM.yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style26" colspan="4">
                                                        <asp:Image ID="Image3" runat="server" Height="16" 
                                                            ImageUrl="~/Image/calender.png" Width="16" />
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style119">
                                                        &nbsp;</td>
                                                    <td class="style119">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style129">
                                                        &nbsp;</td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style10">
                                                        &nbsp;</td>
                                                    <td class="style14">
                                                        <asp:Label ID="lblremarks" runat="server" CssClass="label2" ForeColor="White" 
                                                            Height="17px" Text="Remarks:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtremarks" runat="server" BorderStyle="None" Width="230px" 
                                                            TabIndex="18"></asp:TextBox>
                                                    </td>
                                                    <td class="style14">
                                                        <asp:Label ID="lblPayto" runat="server" CssClass="label2" Text="Pay To:" 
                                                            Width="70px"></asp:Label>
                                                    </td>
                                                    <td class="style14" colspan="4">
                                                        <asp:TextBox ID="txtRecAndPayto" runat="server" BorderStyle="None" 
                                                            Width="260px" TabIndex="19"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtRecAndPayto_AutoCompleteExtender" 
                                                            runat="server" CompletionListCssClass="AutoExtender" 
                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                            CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15" 
                                                            DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                                            MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails" 
                                                            ServicePath="~/AutoCompleted.asmx" TargetControlID="txtRecAndPayto">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="button" Font-Bold="True" 
                                                            Font-Size="12px" onclick="lbtnAdd_Click" TabIndex="20" Width="78px">Add Table</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style135">
                                                        &nbsp;</td>
                                                    <td class="style200">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style10">
                                                        <asp:TextBox ID="txtserchBill" runat="server" BorderStyle="None" TabIndex="40" 
                                                            Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td class="style14">
                                                        <asp:LinkButton ID="lnkBillNo" runat="server" CssClass="button" 
                                                            Font-Size="12px" onclick="lnkBillNo_Click" TabIndex="41" Width="115px">Bill No</asp:LinkButton>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddlBillList" runat="server" style="margin-left: 2px" 
                                                            TabIndex="42" Width="230px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style14">
                                                        &nbsp;</td>
                                                    <td class="style14" colspan="4">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                    <td class="style133">
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
                                                ShowFooter="True" Width="337px" 
                                onrowdeleting="dgv1_RowDeleting" 
                                onrowcancelingedit="dgv1_RowCancelingEdit" onrowediting="dgv1_RowEditing" 
                                onrowupdating="dgv1_RowUpdating">
                                                <PagerSettings Position="Top" />
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" /> 
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid" runat="server"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:CommandField ShowEditButton="True" />
                                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Head of Accounts">
                                                    <EditItemTemplate>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panelgrd" runat="server">
                                                                        <table style="width:100%;">
                                                                            <tr>
                                                                                <td class="style33">
                                                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                                                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" Text="Accounts Head" 
                                                                                        Width="80px"></asp:Label>
                                                                                </td>
                                                                                <td class="style32">
                                                                                    <asp:TextBox ID="txtgrdserceacc" runat="server" BorderStyle="None" TabIndex="50" 
                                                                                        Width="70px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ibtngrdFindAccCode" runat="server" Height="18px" 
                                                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtngrdFindAccCode_Click" 
                                                                                        TabIndex="51" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlgrdacccode" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlgrdacccode_SelectedIndexChanged" TabIndex="52" 
                                                                                        Width="350px">
                                                                                    </asp:DropDownList>
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
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style33">
                                                                                    <asp:Label ID="lblcontrolAccHead0" runat="server" CssClass="label2" 
                                                                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" Text="Details Head" 
                                                                                        Width="80px"></asp:Label>
                                                                                </td>
                                                                                <td class="style32">
                                                                                    <asp:TextBox ID="txtgrdserresource" runat="server" BorderStyle="None" TabIndex="53" 
                                                                                        Width="70px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ibtngrdFindResource" runat="server" Height="18px" 
                                                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtngrdFindResource_Click" 
                                                                                        TabIndex="54" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" AutoPostBack="True" TabIndex="55" 
                                                                                        Width="350px">
                                                                                    </asp:DropDownList>
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
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditItemTemplate>
                                                   <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" 
                                                            onclick="lnkTotal_Click" style="color: #FFFFFF" Font-Underline="False">Total :</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblAccdesc1" runat="server" 
                                                                        Font-Size="11px" 
                                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "          " + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "") %>' 
                                                                        Width="250px" TabIndex="75" ></asp:Label>
                                               
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Issue No">
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisuno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                           
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                                            Width="60px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                               

                                                 <asp:TemplateField HeaderText="Cheque No">
                                                  
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvChequeno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                           
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                                            Width="100px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cheque Date">
                                                   
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvChequeDate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>' 
                                                            Width="80px" Font-Size="12px"  ForeColor="Black" TabIndex="77"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtgvChequeDate_CalendarExtender" runat="server" 
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvChequeDate">
                                                        </cc1:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextbox" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>' 
                                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="78"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                             Font-Bold="True" Font-Size="12px" 
                                                            Width="80px" ForeColor="White"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle ForeColor="White" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Remarks">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                            Width="80px" ForeColor="Black" TabIndex="79"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Payto">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                                            Width="140px" ForeColor="Black" TabIndex="80"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Acvounum" Visible="False">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvacvounm" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>' 
                                                            Width="50px" ForeColor="Black" TabIndex="80"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill No">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvBillno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                                            Width="100px" ForeColor="Black" TabIndex="99"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Instade Of Issue">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvinsissueno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>' 
                                                            Width="80px" ForeColor="Black" TabIndex="99"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                           <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                 <asp:Panel ID="Panel4" runat="server" BorderColor="#996633" BorderStyle="Solid" 
                                            BorderWidth="2px" Visible="False" Width="896px">
                                            <table style="width:99%;">
                                                <tr>
                                                    <td  valign="top" style="text-align: right" class="style18">
                                                        <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style159" colspan="4">
                                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Height="42px" TextMode="MultiLine" Width="600px" 
                                                            TabIndex="21"></asp:TextBox>
                                                    </td>
                                                    <td class="style199">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" 
                                                            Font-Bold="True" Font-Size="12px" onclick="lnkFinalUpdate_Click" 
                                                            Width="90px" TabIndex="22">Final Update</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style18">
                                                        &nbsp;</td>
                                                    <td class="style161">
                                                        &nbsp;</td>
                                                    <td class="style198">
                                                        &nbsp;</td>
                                                    <td class="style198">
                                                        &nbsp;</td>
                                                    <td class="style117">
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


