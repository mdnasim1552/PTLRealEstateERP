<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesOpening02.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesOpening02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.lbltextColor
{
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-style: normal;
	color: #000080;
	margin-left: 0px;
	text-align:right;
	font-weight:bold;
	margin-top: 0px;
}

        .style5
        {
            width: 80px;
        }
        .style6
        {
        }
        .style8
        {
            width: 92px;
        }
        .style9
        {
            width: 75px;
        }
        .style10
        {
            width: 8px;
        }
        .style58
        {
            width: 75px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
 
 
 <table style="width: 99%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="SALES OPENING INFORMATION VIEW/EDIT" Width="636px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>
                                                        <asp:Label ID="lbljavascript" 
                    runat="server"></asp:Label>
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
            <td>
                &nbsp;</td>
        </tr>
        
        </table>
         
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style5">
                                        <asp:Label ID="Label5" runat="server" 
                                            Text="Receive No:" Width="80px" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </td>
                                    <td align="left" class="style9">
                                        <asp:Label ID="lblReceiveNo" runat="server" BackColor="White" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="Label6" runat="server" 
                                            Text="Opening Date:" Width="80px" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" 
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" 
                                            Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtOpeningDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="color: #FFFFFF"></asp:Label>
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
                                    <td class="style5">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Project:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style6" colspan="3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Height="24px" Width="275px" TabIndex="1">
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
                                    <td class="style5">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#993300" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style9">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="80px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style10">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Unit Name:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtSearchUnit" runat="server" BorderStyle="None" Width="100px" 
                                            TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td colspan="5">
                                        <asp:ImageButton ID="imgbtnFindUnit" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="imgbtnFindUnit_Click" TabIndex="3" />
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
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvSOpening" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="831px" AllowPaging="True" 
                            onpageindexchanging="gvSOpening_PageIndexChanging" PageSize="15">
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


                                  <asp:TemplateField HeaderText="Project Name">
                                      <FooterTemplate>
                                          <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                              ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                      </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProjectName" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Description of Item">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>

                                    <asp:Label ID="lblgvusize" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" 
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px">
                                   </asp:Label>                                      
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvCustName" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Opening Amt.">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFToAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                    <ItemTemplate>

                                    <asp:TextBox ID="txtopnamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" 
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px" style="text-align: right; font-size:11px">
                                   </asp:TextBox>                                      
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                              
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle HorizontalAlign="Left" Font-Bold="True" Font-Size="12px" 
                                ForeColor="White" />
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

