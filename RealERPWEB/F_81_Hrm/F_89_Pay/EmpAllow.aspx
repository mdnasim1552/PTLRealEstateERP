<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpAllow.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.EmpAllow" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

    .style16
    {
        width: 9px;
    }
    .style15
    {
        width: 44px;
    }
    .style19
    {
        width: 66px;
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
    .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 12px;
	    font-weight: normal;
	margin-right: 0px;
   text-align: left;
}
    .style24
    {
        width: 10px;
    }
        .ddl
{
	border: 1px solid #7393B1;
	font-size: 11px;
	font-style: normal;
	background-color: #FFFFFF;
    text-align: left;
    font-weight: 700;
}
.ddl
{
	font-weight: normal;
	border-style: none;
	border-width:thick;
	font-size:12px;
}

        .style25
        {
            width: 332px;
        }

    </style>
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE ALLOWANCE INFORMATION" Width="432px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style25">
                                                    <asp:Label ID="lblprint" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button" ForeColor="White">PRINT</asp:LinkButton>
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
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel4" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="lbllMonth" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Month:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style19">
                                        <asp:TextBox ID="txtMonth" runat="server" CssClass="ddl" Width="100px" 
                                            MaxLength="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtMonth_CalendarExtender" runat="server" 
                                            Enabled="True" Format="yyyyMM" TargetControlID="txtMonth">
                                        </cc1:CalendarExtender>
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
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Department:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style19">
                                        <asp:TextBox ID="txtSrcDept" runat="server" CssClass="txtboxformat" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="ibtnFindDepartment" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindDepartment_Click" 
                                            Width="16px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDeptName" runat="server" CssClass="ddl" 
                                            Font-Bold="True" Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlDeptName_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlDeptName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblDepartment" runat="server" BackColor="White" Font-Size="12px" 
                                            ForeColor="Blue" Height="16px" Visible="False" Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" onclick="lnkbtnShow_Click" 
                                            style="text-align: center;" Width="50px">Ok</asp:LinkButton>
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
                                </tr>
                                <tr>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style19">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="100px" 
                                            Visible="False">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style24">
                                        &nbsp;</td>
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
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvEmpAllow" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="831px" AllowPaging="True" 
                            onpageindexchanging="gvEmpAllow_PageIndexChanging">
                            <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCardno" runat="server" Height="16px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>' 
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:LinkButton ID="lTotal" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lTotal_Click" 
                                            style="text-decaration:none;">Total</asp:LinkButton>
                                    </FooterTemplate>
                                   
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Emp. Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Height="16px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:LinkButton ID="lUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lUpdate_Click" 
                                            style="text-decaration:none;">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overtime">
                                   
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvovertime" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04021")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvFOvertime" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Tiffin Allow">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtiffallow" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04022")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvFtiffallow" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Holiday Allow">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvholidays" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04023")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFholidays" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Others Allow">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvothersallow" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04024")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFothersallow" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Daily Allow">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdailyallow" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04025")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdailyallow" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Cell Phone">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcellphone" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04026")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcellphone" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Cell Bill">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcellbill" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04027")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcellbill" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="A">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgva" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04028")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFa" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="B">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvb" runat="server" BackColor="Transparent" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "txt04029")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFb" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"  Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalamt" runat="server"  Height="20px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
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
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

