<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="OtherReqEntry02.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.OtherReqEntry02" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style type="text/css">
        .style15
        {
            color: #FFFFFF;
        }
        .style16
        {
            width: 94px;
        }
        .style53
        {
            width: 5px;
            height: 3px;
        }
        .style55
        {
            width: 122px;
        }
        .style57
        {
            width: 116px;
        }
        .style58
        {
            width: 110px;
        }
        .style59
        {
            width: 109px;
        }
        .style65
        {
            width: 30px;
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
        .style70
        {
            width: 9px;
        }
        .style72
        {
            text-align: right;
            width: 56px;
            height: 3px;
        }
        .style73
        {
            width: 89px;
            height: 3px;
        }
        .style74
        {
            width: 6px;
        }
        .style77
        {
            width: 50px;
            text-align: left;
        }
        .style78
        {
            text-align: right;
            width: 56px;
            height: 27px;
        }
        .style79
        {
            width: 89px;
            height: 27px;
        }
        .style80
        {
            width: 9px;
            height: 27px;
        }
        .style81
        {
            height: 27px;
        }
        .style82
        {
            width: 5px;
            height: 27px;
        }
        .style83
        {
            width: 9px;
            height: 3px;
        }
        .style84
        {
            height: 3px;
        }
    </style>
   
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 908px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="lblTitle1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                                
                    
                    
                    
                    
                    Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1; " Text="Ohters Requisition"
                                Width="450px" BorderStyle="Inset" BackColor="Transparent" 
                    BorderWidth="2px"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="False" 
                    OnClick="lnkPrint_Click" CssClass="button" Font-Names="Verdana">PRINT</asp:LinkButton>
            </td>
            <td class="style16">
                &nbsp;</td>
        </tr>
    </table>
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <asp:Panel ID="PnlReq" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                BorderWidth="1px">
            <table style="width:900px" >
                <tr>
                    <td class="style34" align="right" colspan="2">
                        <asp:Label ID="lblCurNo0" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: left" Text=" Req. No.:" 
                            Width="75px"></asp:Label>
                    </td>
                    <td class="style43">
                        <asp:Label ID="lblCurReqNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                            Text="REQ00-" Width="45px"></asp:Label>
                    </td>
                    <td class="style43">
                        <asp:TextBox ID="txtCurReqNo2" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                            Width="45px">00000</asp:TextBox>
                    </td>
                    <td class="style43">
                        <asp:Label ID="lblmrfno" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" style="TEXT-ALIGN: right" Text="M.R.F. No.:" Width="78px"></asp:Label>
                    </td>
                    <td class="style43">
                        <asp:TextBox ID="txtMRFNo" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Bold="True" Font-Size="12px" Width="100px" TabIndex="1"></asp:TextBox>
                    </td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style43">
                        <asp:Label ID="lblCurDate" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" Text="Req.Date:" 
                            Width="60px"></asp:Label>
                    </td>
                    <td class="style43">
                        <asp:TextBox ID="txtCurReqDate" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" style="margin-left: 0px" 
                            ToolTip="(dd.mm.yyyy)" Width="80px" TabIndex="2"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" 
                            Format="dd.MM.yyyy" TargetControlID="txtCurReqDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="style43" colspan="2">
                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" 
                            onclick="lbtnOk_Click" 
                           Width="52px" 
                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="White" style="text-align: center" 
                            TabIndex="6">Ok</asp:LinkButton>
                    </td>
                    <td class="style43" colspan="2">
                        &nbsp;</td>
                    <td class="style43">
                        &nbsp;</td>
                    <td class="style34" align="right" width="85px">
                        &nbsp;</td>
                    <td width="125px">
                        &nbsp;</td>
                    <td class="style47">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="style34">
                        <asp:TextBox ID="txtSrchMrfNo" runat="server" BorderStyle="None" 
                            Font-Bold="True" Font-Size="12px" Width="50px" TabIndex="3"></asp:TextBox>
                    </td>
                    <td align="right" class="style74">
                        <asp:LinkButton ID="lbtnPrevReqList" runat="server" CssClass="style15" 
                            Font-Bold="True" Font-Size="12px" onclick="lbtnPrevReqList_Click" 
                            style="text-align: right; height: 15px;" Width="55px" TabIndex="4">Req. List:</asp:LinkButton>
                    </td>
                    <td class="style43" colspan="9">
                        <asp:DropDownList ID="ddlPrevReqList" runat="server" Font-Size="12px" 
                            Width="300px" TabIndex="5">
                        </asp:DropDownList>
                    </td>
                    <td class="style34" colspan="3">
                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Maroon" style="color: #FFFFFF;"></asp:Label>
                    </td>
                    <td class="style47" colspan="2">
                        </td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style19">
                        </td>
                </tr>
                
            </table>
          </asp:Panel>
          
            <table style="width:100%;">
                <tr>
                    <td class="style18" colspan="14">
                        <asp:Panel ID="PnlDetails" runat="server" Visible="False" BorderColor="Yellow" 
                            BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%; ">
                                <tr>
                                    <td class="style78">
                                        <asp:Label ID="Label8" runat="server" CssClass="style15" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Project Name:" Width="115px"></asp:Label>
                                    </td>
                                    <td class="style79">
                                        <asp:TextBox ID="txtProSearch" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px" 
                                            TabIndex="7"></asp:TextBox>
                                    </td>
                                    <td class="style80">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindProject_Click" 
                                            Width="21px" TabIndex="8" />
                                    </td>
                                    <td class="style81">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlProjectName_SelectedIndexChanged" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            Width="300px" TabIndex="9">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style81" width="80px">
                                    </td>
                                    <td class="style81">
                                    </td>
                                    <td class="style81">
                                    </td>
                                    <td class="style80">
                                    </td>
                                    <td class="style80">
                                    </td>
                                    <td class="style80">
                                    </td>
                                    <td class="style81">
                                    </td>
                                    <td class="style81">
                                    </td>
                                    <td class="style81">
                                    </td>
                                    <td class="style82">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style72">
                                        <asp:Label ID="lblResList0" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="TEXT-ALIGN: left; color: #FFFFFF;" Text="Non Materials List:" 
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td class="style73">
                                        <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px" 
                                            TabIndex="10"></asp:TextBox>
                                    </td>
                                    <td class="style83">
                                        <asp:ImageButton ID="ImgbtnFindRes2" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindRes_Click" 
                                            Width="21px" TabIndex="11" />
                                    </td>
                                    <td class="style84">
                                        <asp:DropDownList ID="ddlResList" runat="server" Font-Size="12px" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            Width="300px" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" onclick="lbtnSelectRes_Click" 
                                            style="text-align: center; height: 15px;" Width="54px" TabIndex="13">Select</asp:LinkButton>
                                    </td>
                                    <td class="style84" width="80px">
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style83">
                                    </td>
                                    <td class="style83">
                                    </td>
                                    <td class="style83">
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style53">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style18" colspan="14">
                        <asp:GridView ID="gvOtherReq" runat="server" AutoGenerateColumns="False" 
                            onrowdeleting="gvOtherReq_RowDeleting" ShowFooter="True" Width="542px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Description of Materials">
                                    <FooterTemplate>
                                        <table style="width:21%;">
                                            <tr>
                                                <td class="style77">
                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" onclick="lbtnTotal_Click" style="text-align:left; " 
                                                        Width="80px">Total</asp:LinkButton>
                                                </td>
                                                <td class="style65">
                                                    <asp:LinkButton ID="lbtnUpdateResReq" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lbtnUpdateResReq_Click" 
                                                        Width="80px">Final Update</asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' 
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bgd. Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBgdAmt" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPaidamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaAmt" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalAmt" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBalamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposed Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFProposedamt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvProposedamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFadjustamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtgvadjustamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="style18" colspan="14">
                        <asp:Panel ID="PnlNarration" runat="server" BorderColor="Maroon" 
                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="style15" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" Text="Narration:" 
                                            Width="80px" TabIndex="14"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReqNarr" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="55px" 
                                            TextMode="MultiLine" Width="415px" TabIndex="15"></asp:TextBox>
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
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style42">
                        &nbsp;</td>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style55">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style39">
                        &nbsp;</td>
                    <td class="style38">
                        &nbsp;</td>
                    <td class="style47">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
      
</asp:Content>


