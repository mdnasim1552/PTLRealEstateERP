<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpLoanInfo.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.EmpLoanInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style5
        {
            width: 163px;
        }
        .style23
        {
            text-align: right;
        }
        .style24
        {
            width: 164px;
        }
        .style25
        {
            width: 78px;
            height: 24px;
        }
        .style29
        {
            width: 83px;
            height: 24px;
        }
        .style30
        {
            height: 24px;
        }
        .style31
        {
            width: 85px;
            height: 24px;
        }
    .style22
    {
        color: #FFFFFF;
    }
        .txtboxformat
        {}
        .style33
        {
            height: 24px;
            width: 60px;
        }
        .style34
        {
            height: 24px;
            width: 112px;
        }
        .style35
        {
            height: 24px;
            width: 111px;
        }
        .style37
        {
            height: 24px;
            width: 87px;
        }
        .style38
        {
            width: 56px;
            height: 24px;
        }
        .style39
        {
            width: 49px;
            height: 24px;
        }
        .style40
        {
            width: 82px;
            height: 24px;
        }
        .style41
        {
            width: 154px;
        }
        .style45
        {
            width: 111px;
        }
        .style46
        {
            width: 112px;
        }
    </style>
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 930px;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE LOAN INFORMATION" Width="500px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>
                                                                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
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
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
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
            <table style="width:930px;">
                <tr>
                    <td class="style16" colspan="12">
                        <asp:Panel ID="Panel1" runat="server" Width="930px">
                            <table style=" height: 41px; width: 526px;">
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td class="style19">
                                        &nbsp;</td>
                                    <td class="style17">
                                        <asp:LinkButton ID="lbtnPrevLoanList" runat="server" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnPrevLoanList_Click" 
                                            style="text-align: right; height: 15px; color: #FFFFFF;" Width="90px">Prev. Loan List:</asp:LinkButton>
                                    </td>
                                    <td class="style18" colspan="5">
                                        <asp:DropDownList ID="ddlPrevLoanList" runat="server" Font-Size="12px" 
                                            Width="400px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style24">
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
                                    <td class="style35">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        </td>
                                    <td class="style19">
                                        </td>
                                    <td class="style17">
                                        <asp:Label ID="Label7" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Loan Date:" Width="90px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style21" colspan="2">
                                        <asp:TextBox ID="txtCurDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Loan No:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style41">
                                        <asp:Label ID="lblCurNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                            Width="50px"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                            Width="35px"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td class="style24">
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="height: 17px; margin-left: 4px;">Ok</asp:LinkButton>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td class="style35">
                                        </td>
                                    <td class="style34">
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="TEXT-ALIGN: right" Text="Employee List:" Width="90px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtsrchEmp" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnEmpList" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" Width="16px" />
                                    </td>
                                    <td class="style22" colspan="3">
                                        <asp:DropDownList ID="ddlEmpList" runat="server" AutoPostBack="True" 
                                            Width="292px" Height="20px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ListSearchExt2" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlEmpList">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblEmpName" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="288px"></asp:Label>
                                    </td>
                                    <td class="style24">
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
                                    <td class="style45">
                                        </td>
                                    <td class="style46">
                                        </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                 <tr>
                    <td class="style16" colspan="12">
                        <asp:Panel ID="pnlloan" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px" Visible="False" Width="943px">
                            <table style="width:939px;">
                                <tr>
                                    <td class="style30">
                                        </td>
                                    <td class="style31">
                                        <asp:Label ID="Label9" runat="server" CssClass="style23" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Text=" Total Amount:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="txtToamt" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" Width="80px" style="text-align:right;"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:Label ID="Label11" runat="server" CssClass="style23" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Text="Ins. Amount:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:TextBox ID="txtinsamt" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" Width="80px" style="text-align:right;"></asp:TextBox>
                                    </td>
                                    <td class="style38">
                                        <asp:Label ID="Label12" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Start Date:" Width="69px"></asp:Label>
                                    </td>
                                    <td class="style33">
                                        <asp:TextBox ID="txtstrdate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtstrdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style39">
                                        <asp:Label ID="Label10" runat="server" CssClass="style23" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Text="Duration:" Width="70px"></asp:Label>
                                    </td>
                                         <td class="style37">
                                             <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True" 
                                                 CssClass="ddl" Font-Bold="True" Font-Size="12px" Width="100px">
                                                 <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                 <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                 <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                 <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                 <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                 <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                 <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                 <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                 <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                 <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                 <asp:ListItem Value="11">11  Month</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                    <td class="style30">
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style22" 
                                            Font-Bold="True" Font-Size="12px" onclick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                        </td>
                                         <td class="style30">
                                             &nbsp;</td>
                                    <td class="style30">
                                        </td>
                                </tr>
                            </table>
                        </asp:Panel>
                     </td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
                        <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" 
                            BackColor="#003366" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                            CssClass="style22" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" 
                            oncheckedchanged="chkVisible_CheckedChanged" Text="Gen. Installment" 
                            Visible="False" />
                    </td>
                    <td>
                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White"></asp:Label>
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
                <tr>
                    <td class="style16" colspan="12">
                        <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" style="margin-right: 0px" Width="292px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." FooterText="Total ">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                               
                             
                              
                                <asp:TemplateField HeaderText="Installment Date.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblinstdate" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lndate")).ToString("dd-MMM-yyyy") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Installment Amt.">
                                     <FooterTemplate>
                                         <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right" Width="120px"></asp:Label>
                                     </FooterTemplate>
                                    <ItemTemplate>
                                             <asp:TextBox ID="gvtxtamt" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="12px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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


