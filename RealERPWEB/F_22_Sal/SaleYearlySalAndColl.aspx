<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SaleYearlySalAndColl.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SaleYearlySalAndColl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style29
        {            width: 65px;
        }
        .style30
        {
            color: #FFFFFF;
        }
        .style27
        {
            width: 77px;
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
        .style65
        {
            width: 14px;
        }
        .style64
        {
            width: 18px;
        }
        .style66
        {
            width: 54px;
        }
        .style67
        {
            width: 485px;
        }
        .style68
        {
            width: 221px;
        }
        .style69
        {
            width: 145px;
        }
        .style71
        {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvSalAndColl.ClientID %>');
            
            gv.Scrollable();

        }

    </script>
 <table style="width: 98%;">
        <tr>
            <td class="style67">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="TRANSACTION STATEMENT  INFORMATION VIEW/EDIT" Width="500px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style68">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style29">
                                        <asp:Label ID="lblFdate" runat="server" CssClass="style30" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="From :" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderColor="#660033" 
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" 
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " 
                                            TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style65">
                                        <asp:Label ID="lblTdate" runat="server" CssClass="style30" Font-Bold="True" 
                                            Font-Size="12px" Text="To:"></asp:Label>
                                    </td>
                                    <td align="left" class="style64">
                                        <asp:TextBox ID="txttodate" runat="server" BorderColor="#660033" 
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" 
                                            TabIndex="1" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy" 
                                            TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" class="style64">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" TabIndex="5" Width="29px">Ok</asp:LinkButton>
                                    </td>
                                    <td align="left" class="style66">
                                        &nbsp;</td>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style69">
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
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View runat="Server" ID="ViewMonCollection">
                            
                            
                            
                                <table style="width:100%;">
                                   
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvSalAndColl" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="616px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>

                                                  <asp:TemplateField HeaderText="Sl. No.">
                                                      <FooterTemplate>
                                                          <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                              ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                                      </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNomon" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Description">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                           
                                                            <asp:HyperLink ID="HygvResDesc" runat="server" Font-Underline="false"  ForeColor="Black" Target="_blank"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>' 
                                                                Width="180px"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtoamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:Label ID="lgvFtoamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                   
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                   
                                                    <asp:TemplateField HeaderText="amt1">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt1" runat="server" BackColor="Transparent" Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="amt2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt2" runat="server" BackColor="Transparent" Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt3">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt3" runat="server" BackColor="Transparent" Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt4">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt4" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>

                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt5">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt5" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt6">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt6" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt7">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt7" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt8">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt8" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt9">
                                                        
                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt9" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt9" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt10">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt10" runat="server" BackColor="Transparent"   Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt10" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt11">
                                                         <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt11" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt11" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt12">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvamt12" runat="server" BackColor="Transparent"  Font-Size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lgvFamt12" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>

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
                            
                            
                            
                            </asp:View>

                        </asp:MultiView>
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


